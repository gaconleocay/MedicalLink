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
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.ChucNang
{
    public partial class ucBCChiDinhPTTT : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string danhSachIdKhoa = "";
        //private List<MedicalLink.ClassCommon.classBCChiDinhPTTT_User> lstUser { get; set; }
        private DataTable data_BCChiDinhPTTT { get; set; }
        public ucBCChiDinhPTTT()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaDV.Text.Length == 0)
            {
                mmeMaDV.Text = "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)";
                mmeMaDV.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaDV.Text == "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)")
            {
                mmeMaDV.Text = "";
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
        }

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

        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            gridControlDSDV.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string[] dsnhomdv_temp;
                string dsnhomdv = "";
                string tieuchi = "";
                string loaivienphiid = "";
                string doituongbenhnhanid = "";
                string datetungay = "";
                string datedenngay = "";
                string sqlquerry = ""; //TODO cau lenh SQL

                if ((mmeMaDV.Text == "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)") || (cbbTieuChi.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    //LayDanhSachLocTheoKhoa();
                    if (cbbLoaiBA.Text == "Nội trú")
                    {
                        LayDanhSachLocTheoKhoa();
                    }
                    else if (cbbLoaiBA.Text == "Ngoại trú")
                    {
                        LayDanhSachLocTheoPhong();
                    }
                    else
                    {
                        danhSachIdKhoa = " ";
                    }
                    // Lấy dữ liệu danh sách dịch vụ nhập vào
                    dsnhomdv_temp = mmeMaDV.Text.Split(',');
                    for (int i = 0; i < dsnhomdv_temp.Length - 1; i++)
                    {
                        dsnhomdv += "'" + dsnhomdv_temp[i].ToString().Trim() + "',";
                    }
                    dsnhomdv += "'" + dsnhomdv_temp[dsnhomdv_temp.Length - 1].ToString().Trim() + "'";

                    // dsnhomdv = dsnhomdv.Replace("'',", "").Replace(",''","");

                    // Lấy Tiêu chí thời gian: tieuchi
                    if (cbbTieuChi.Text == "Theo ngày chỉ định")
                    {
                        tieuchi = " and ser.servicepricedate ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày vào viện")
                    {
                        tieuchi = " and vp.vienphidate ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày ra viện")
                    {
                        tieuchi = " and vp.vienphidate_ravien ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                    {
                        tieuchi = " and vp.duyet_ngayduyet_vp ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")
                    {
                        tieuchi = " and vp.duyet_ngayduyet ";
                    }

                    // Lấy loaivienphiid
                    if (cbbLoaiBA.Text == "Ngoại trú")
                    {
                        loaivienphiid = " and vp.loaivienphiid=1 ";
                    }
                    else if (cbbLoaiBA.Text == "Nội trú")
                    {
                        loaivienphiid = " and vp.loaivienphiid=0 ";
                    }

                    // Lấy trường đối tượng BN loaidoituong
                    if (chkBHYT.Checked == true && chkVP.Checked == false)
                    {
                        doituongbenhnhanid = " and vp.doituongbenhnhanid=1 ";
                    }
                    else if (chkBHYT.Checked == false && chkVP.Checked == true)
                    {
                        doituongbenhnhanid = " and vp.doituongbenhnhanid<>1 ";
                    }
                    else if (chkBHYT.Checked == true && chkVP.Checked == true)
                    {
                        doituongbenhnhanid = " ";
                    }

                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");


                    if (chkPTTTAll.Checked)
                    {
                        sqlquerry = "SELECT ROW_NUMBER () OVER (ORDER BY sef.servicepricegroupcode,ser.servicepricecode) as stt, ser.servicepriceid as servicepriceid, vp.patientid as mabn, vp.vienphiid as mavp, mbp.maubenhphamid as maubenhphamid, hsba.patientname as tenbn, krv.departmentgroupname as tenkhoaravien, prv.departmentname as tenphongravien, ser.servicepricecode as madv, ser.servicepricename as tendv, ser.servicepricemoney as dongia, ser.servicepricemoney_bhyt as dongiabhyt, ser.soluong as soluong, ser.soluong * ser.servicepricemoney as thanhtien, ser.soluong * ser.servicepricemoney_bhyt as thanhtienbhyt, ser.servicepricedate as thoigianchidinh, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, vp.vienphidate as thoigianvaovien, vp.vienphidate_ravien as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, mbp.userid as iduserchidinh, ncd.usercode as mauserchidinh, ncd.username as tenuserchidinh, sef.servicepricegroupcode as manhomdichvu, (case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' else '' end) as trangthaithutien, hsba.bhytcode as sothebhyt, case sef.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end as bhyt_groupcode, sef.servicegrouptype as servicegrouptype, (select sum(ser_dk.servicepricemoney * ser_dk.soluong) from serviceprice ser_dk where ser_dk.servicepriceid_master=ser.servicepriceid and ser_dk.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle','093TDTUngthu') and ser_dk.loaidoituong=2 and ser_dk.vienphiid=ser.vienphiid) as thuocdikem, (select sum(ser_dk.servicepricemoney * ser_dk.soluong) from serviceprice ser_dk where ser_dk.servicepriceid_master=ser.servicepriceid and ser_dk.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser_dk.loaidoituong=2 and ser_dk.vienphiid=ser.vienphiid) as vattudikem, ser.chiphidauvao as chiphidauvao, ser.chiphimaymoc as chiphimaymoc, ser.chiphipttt as chiphipttt, mayyte.mayytename as mayytename, mayyte.chiphiliendoanh as chiphiliendoanh FROM serviceprice ser INNER JOIN vienphi vp ON ser.vienphiid=vp.vienphiid INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan) hsba ON vp.hosobenhanid=hsba.hosobenhanid INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON vp.departmentid=prv.departmentid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON vp.departmentgroupid=krv.departmentgroupid INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd ON ser.departmentid=pcd.departmentid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON vp.departmentgroupid=kcd.departmentgroupid INNER JOIN maubenhpham mbp ON mbp.maubenhphamid=ser.maubenhphamid INNER JOIN servicepriceref sef ON ser.servicepricecode=sef.servicepricecode LEFT JOIN mayyte ON ser.mayytedbid=mayyte.mayytedbid LEFT JOIN nhompersonnel ncd on ncd.userhisid=mbp.userid WHERE sef.bhyt_groupcode in ('06PTTT','07KTC') " + tieuchi + " >= '" + datetungay + "' " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + " ORDER BY sef.servicepricegroupcode;  ";
                    }
                    else
                    {
                        sqlquerry = "SELECT ROW_NUMBER () OVER (ORDER BY sef.servicepricegroupcode,ser.servicepricecode) as stt, ser.servicepriceid as servicepriceid, vp.patientid as mabn, vp.vienphiid as mavp, mbp.maubenhphamid as maubenhphamid, hsba.patientname as tenbn, krv.departmentgroupname as tenkhoaravien, prv.departmentname as tenphongravien, ser.servicepricecode as madv, ser.servicepricename as tendv, ser.servicepricemoney as dongia, ser.servicepricemoney_bhyt as dongiabhyt, ser.soluong as soluong, ser.soluong * ser.servicepricemoney as thanhtien, ser.soluong * ser.servicepricemoney_bhyt as thanhtienbhyt, ser.servicepricedate as thoigianchidinh, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, vp.vienphidate as thoigianvaovien, vp.vienphidate_ravien as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, mbp.userid as iduserchidinh, ncd.usercode as mauserchidinh, ncd.username as tenuserchidinh, sef.servicepricegroupcode as manhomdichvu, (case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' else '' end) as trangthaithutien, hsba.bhytcode as sothebhyt, case sef.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end as bhyt_groupcode, sef.servicegrouptype as servicegrouptype, (select sum(ser_dk.servicepricemoney * ser_dk.soluong) from serviceprice ser_dk where ser_dk.servicepriceid_master=ser.servicepriceid and ser_dk.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle','093TDTUngthu') and ser_dk.loaidoituong=2 and ser_dk.vienphiid=ser.vienphiid) as thuocdikem, (select sum(ser_dk.servicepricemoney * ser_dk.soluong) from serviceprice ser_dk where ser_dk.servicepriceid_master=ser.servicepriceid and ser_dk.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser_dk.loaidoituong=2 and ser_dk.vienphiid=ser.vienphiid) as vattudikem, ser.chiphidauvao as chiphidauvao, ser.chiphimaymoc as chiphimaymoc, ser.chiphipttt as chiphipttt, mayyte.mayytename as mayytename, mayyte.chiphiliendoanh as chiphiliendoanh FROM serviceprice ser INNER JOIN vienphi vp ON ser.vienphiid=vp.vienphiid INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan) hsba ON vp.hosobenhanid=hsba.hosobenhanid INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON vp.departmentid=prv.departmentid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON vp.departmentgroupid=krv.departmentgroupid INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd ON ser.departmentid=pcd.departmentid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON vp.departmentgroupid=kcd.departmentgroupid INNER JOIN maubenhpham mbp ON mbp.maubenhphamid=ser.maubenhphamid INNER JOIN servicepriceref sef ON ser.servicepricecode=sef.servicepricecode LEFT JOIN mayyte ON ser.mayytedbid=mayyte.mayytedbid LEFT JOIN nhompersonnel ncd on ncd.userhisid=mbp.userid WHERE sef.servicepricegroupcode in (" + dsnhomdv + ") " + tieuchi + " >= '" + datetungay + "' " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + " ; ";
                    }

                    data_BCChiDinhPTTT = condb.GetDataTable_HIS(sqlquerry);
                    if (data_BCChiDinhPTTT.Rows.Count > 0 && data_BCChiDinhPTTT != null)
                    {
                        gridControlDSDV.DataSource = data_BCChiDinhPTTT;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Load
        private void ucBCChiDinhPTTT_Load(object sender, EventArgs e)
        {
            try
            {
                //Lấy thời gian lấy BC mặc định là ngày hiện tại
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //LoadDanhSachKhoa();
                // CheckDSKhoa();
                // LayDanhSachNguoiDung();
                chkcomboListDSKhoa.Enabled = false;
            }
            catch (Exception)
            {
            }
        }

        private void LoadDanhSachKhoa()
        {
            try
            {
                string sql_lstKhoa = "SELECT departmentgroupid,departmentgroupcode,departmentgroupname,departmentgrouptype FROM departmentgroup WHERE departmentgrouptype in(4,11,1) ORDER BY departmentgroupname;";
                DataView lstDSKhoa = new DataView(condb.GetDataTable_HIS(sql_lstKhoa));
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
                DataView lstDSPhong = new DataView(condb.GetDataTable_HIS(sql_lstPhong));
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
        private void CheckDSKhoa()
        {
            try
            {
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count; i++)
                {
                    chkcomboListDSKhoa.Properties.Items[i].CheckState = CheckState.Checked;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        //private void LayDanhSachNguoiDung()
        //{
        //    try
        //    {
        //        lstUser = new List<ClassCommon.classBCChiDinhPTTT_User>();
        //        string sql_laynguoidung = "SELECT DISTINCT userhisid, usercode, username FROM nhompersonnel;";
        //        DataView data_dsnguoidung = new DataView(condb.GetDataTable_HIS(sql_laynguoidung));
        //        if (data_dsnguoidung != null && data_dsnguoidung.Count > 0)
        //        {
        //            for (int i = 0; i < data_dsnguoidung.Count; i++)
        //            {
        //                ClassCommon.classBCChiDinhPTTT_User user = new ClassCommon.classBCChiDinhPTTT_User();
        //                user.userid = Convert.ToInt64(data_dsnguoidung[i]["userhisid"].ToString());
        //                user.usercode = data_dsnguoidung[i]["usercode"].ToString();
        //                user.username = data_dsnguoidung[i]["username"].ToString();
        //                lstUser.Add(user);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Warn(ex);
        //    }
        //}
        #endregion

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (data_BCChiDinhPTTT != null && data_BCChiDinhPTTT.Rows.Count > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);

                    string fileTemplatePath = "BC_BenhNhanSuDungDichVuPTTTTheoNhom.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_BCChiDinhPTTT);
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void chkPTTTAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPTTTAll.Checked)
                {
                    mmeMaDV.Text = "";
                    mmeMaDV.ReadOnly = true;
                }
                else
                {
                    mmeMaDV.Text = "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)";
                    mmeMaDV.ReadOnly = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnChonNhomDV_Click(object sender, EventArgs e)
        {
            try
            {
                string dsnhomdichvu = "";
                MedicalLink.ChucNang.BCPTTT.frmLayNhomDichVu frmchonnhom = new ChucNang.BCPTTT.frmLayNhomDichVu(dsnhomdichvu);
                frmchonnhom.MyGetData = new ChucNang.BCPTTT.frmLayNhomDichVu.GetString(TranPatiDataDSDichVu);
                frmchonnhom.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void TranPatiDataDSDichVu(string dsnhomdichvu)
        {
            try
            {
                mmeMaDV.Text = "";
                mmeMaDV.Text = dsnhomdichvu;
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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

        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
