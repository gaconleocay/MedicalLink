using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.BaoCao
{
    public partial class ucBCChiDinhPTTT_G304 : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string danhSachIdKhoa = "";
        public ucBCChiDinhPTTT_G304()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCChiDinhPTTT_G304_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                mmeMaDV.Text = "G304";
                chkcomboListDSKhoa.Enabled = false;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadDanhSachKhoa()
        {
            try
            {
                string sql_lstKhoa = "SELECT departmentgroupid,departmentgroupcode,departmentgroupname,departmentgrouptype FROM departmentgroup WHERE departmentgrouptype in(4,11,1) ORDER BY departmentgroupname;";
                DataView lstDSKhoa = new DataView(condb.getDataTable(sql_lstKhoa));
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachPhong()
        {
            try
            {
                string sql_lstPhong = "SELECT departmentid,departmentcode,departmentname,departmenttype FROM department WHERE departmenttype=2 AND iskhonghoatdong=0 ORDER BY departmentname;";
                DataView lstDSPhong = new DataView(condb.getDataTable(sql_lstPhong));
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSPhong;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentid";
                }
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void LayDanhSachLocTheoKhoa()
        {
            try
            {
                string danhsachkhoacheck = "";
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count - 1; i++)
                {
                    if (chkcomboListDSKhoa.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                        danhsachkhoacheck += chkcomboListDSKhoa.Properties.Items[i].Value + ",";
                    }
                }
                if (danhsachkhoacheck != "")
                {
                    danhSachIdKhoa = " and ser.departmentgroupid in (" + danhsachkhoacheck.Substring(0, danhsachkhoacheck.Length - 1) + ") ";
                }
                else
                {
                    danhSachIdKhoa = " ";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LayDanhSachLocTheoPhong()
        {
            try
            {
                string danhsachkhoacheck = "";
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count - 1; i++)
                {
                    if (chkcomboListDSKhoa.Properties.Items[i].CheckState == CheckState.Checked)
                    {
                        danhsachkhoacheck += chkcomboListDSKhoa.Properties.Items[i].Value + ",";
                    }
                }
                if (danhsachkhoacheck != "")
                {
                    danhSachIdKhoa = " and ser.departmentid in (" + danhsachkhoacheck.Substring(0, danhsachkhoacheck.Length - 1) + ") ";
                }
                else
                {
                    danhSachIdKhoa = " ";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            gridControlDSDV.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string dsnhomdv = "'" + mmeMaDV.Text + "'";
            string tieuchi, loaivienphiid, doituongbenhnhanid;
            string datetungay = "";
            string datedenngay = "";
            try
            {
                gridControlDSDV.DataSource = null;
                if (cbbLoaiBA.Text == "Nội trú")
                {
                    LayDanhSachLocTheoKhoa();
                }
                else if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    LayDanhSachLocTheoPhong();
                }

                // Lấy Tiêu chí thời gian: tieuchi
                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi = "and ser.servicepricedate";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi = "and vp.vienphidate";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi = "and vp.vienphidate_ravien";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi = "and vp.duyet_ngayduyet_vp ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")
                {
                    tieuchi = "and vp.duyet_ngayduyet ";
                }
                else
                {
                    tieuchi = " ";
                }

                // Lấy loaivienphiid
                if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    loaivienphiid = "and vp.loaivienphiid=1 ";
                }
                else if (cbbLoaiBA.Text == "Nội trú")
                {
                    loaivienphiid = "and vp.loaivienphiid=0 ";
                }
                else
                {
                    loaivienphiid = " ";
                }

                // Lấy trường đối tượng BN loaidoituong
                if (chkBHYT.Checked == true && chkVP.Checked == false)
                {
                    doituongbenhnhanid = "and vp.doituongbenhnhanid=1 ";
                }
                else if (chkBHYT.Checked == false && chkVP.Checked == true)
                {
                    doituongbenhnhanid = "and vp.doituongbenhnhanid<>1 ";
                }
                else if (chkBHYT.Checked == true && chkVP.Checked == true)
                {
                    doituongbenhnhanid = " ";
                }
                else
                {
                    doituongbenhnhanid = " ";
                }


                // Lấy từ ngày, đến ngày
                datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string sqlquerry = "SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricename) as stt, ser.servicepriceid, vp.patientid, vp.vienphiid, mbp.maubenhphamid, hsba.patientname, degp.departmentgroupname as tenkhoaravien, dep.departmentname as tenphongravien, ser.servicepricecode, ser.servicepricename, ser.servicepricemoney, ser.servicepricemoney_bhyt, ser.soluong, ser.soluong * ser.servicepricemoney as thanhtien, ser.soluong * ser.servicepricemoney_bhyt as thanhtienbhyt, ser.servicepricedate, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet_vp, vp.duyet_ngayduyet, nv.usercode as mauserchidinh, mbp.userid, nv.username as tenuserchidinh, serf.servicepricegroupcode, (case when ser.billid_thutien<>0 or ser.billid_clbh_thutien<>0 then 'Đã thu tiền' else '' end) as trangthaithutien, bh.bhytcode, serf.bhyt_groupcode, serf.servicegrouptype, ser.chiphidauvao, ser.chiphimaymoc, ser.chiphipttt FROM serviceprice ser INNER JOIN vienphi vp ON ser.vienphiid=vp.vienphiid INNER JOIN hosobenhan hsba ON vp.hosobenhanid=hsba.hosobenhanid INNER JOIN departmentgroup degp ON vp.departmentgroupid=degp.departmentgroupid INNER JOIN department dep ON vp.departmentid=dep.departmentid and dep.departmenttype in (2,3,9) INNER JOIN departmentgroup kcd ON ser.departmentgroupid=kcd.departmentgroupid INNER JOIN department pcd ON ser.departmentid=pcd.departmentid and pcd.departmenttype in (2,3,9) INNER JOIN maubenhpham mbp ON mbp.maubenhphamid=ser.maubenhphamid and mbp.maubenhphamgrouptype=4 INNER JOIN servicepriceref serf ON ser.servicepricecode=serf.servicepricecode LEFT JOIN bhyt bh ON vp.bhytid=bh.bhytid LEFT JOIN tools_tblnhanvien nv ON nv.userhisid=mbp.userid WHERE serf.servicepricegroupcode in (" + dsnhomdv + ") " + tieuchi + " >= '" + datetungay + "' " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + ";";

                DataView data_BCChiDinhPTTT = new DataView(condb.getDataTable(sqlquerry));
                if (data_BCChiDinhPTTT != null && data_BCChiDinhPTTT.Count > 0)
                {
                    gridControlDSDV.DataSource = data_BCChiDinhPTTT;
                }
                else
                {
                    Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO_THEO_YEU_CAU);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDSDV.RowCount > 0)
            {
                try
                {
                    using (SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                        if (saveDialog.ShowDialog() != DialogResult.Cancel)
                        {
                            string exportFilePath = saveDialog.FileName;
                            string fileExtenstion = new FileInfo(exportFilePath).Extension;

                            switch (fileExtenstion)
                            {
                                case ".xls":
                                    gridControlDSDV.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDSDV.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDSDV.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDSDV.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDSDV.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDSDV.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                }
            }
            else
            {
                Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.KHONG_CO_DU_LIEU);
            }

        }

        private void cbbLoaiBA_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbLoaiBA.Text == "Nội trú")
                {
                    lblKhoaPhong.Text = "Khoa chỉ định";
                    chkcomboListDSKhoa.Enabled = true;
                    LoadDanhSachKhoa();
                }
                else if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    lblKhoaPhong.Text = "Phòng chỉ định";
                    chkcomboListDSKhoa.Enabled = true;
                    LoadDanhSachPhong();
                }
                else
                {
                    lblKhoaPhong.Text = "Khoa chỉ định";
                    chkcomboListDSKhoa.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
