using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using System.IO;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucXuLyMaVPTrang : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        //DataView dv_bhytgroup;
        public ucXuLyMaVPTrang()
        {
            InitializeComponent();
        }

        private void ucXuLyMaVPTrang_Load(object sender, EventArgs e)
        {
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateTuNgay.Value = date_tu;
            DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            dateDenNgay.Value = date_den;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string chonLoaiBenhAn = "";
                string phatSinhChiDinh = cbbPhatSinhChiDinh.Text.Trim();
                gridControlVPTrang.DataSource = null;
                string datetungay = "";
                string datedenngay = "";

                if (cbbLoaiBenhAn.Text != "" && dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    if (cbbLoaiBenhAn.Text.Trim() == "Ngoại trú")
                    {
                        chonLoaiBenhAn = "and vp.loaivienphiid=1 ";
                    }
                    else if (cbbLoaiBenhAn.Text.Trim() == "Nội trú")
                    {
                        chonLoaiBenhAn = "and vp.loaivienphiid=0 ";
                    }
                    else
                    {
                        chonLoaiBenhAn = " ";
                    }

                    switch (phatSinhChiDinh)
                    {
                        case "Không phát sinh dịch vụ":
                            {
                                try
                                {
                                    string sqlquerythuoc = "SELECT A.* FROM (SELECT vp.vienphiid as vienphiid, vp.patientid as patientid, hs.patientname as patientname, bh.bhytcode as bhytcode, dep.departmentgroupname as departmentgroupname, dep.departmentname as departmentname, vp.vienphidate as tgvaovien, vp.vienphidate_ravien as tgravien, case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Chưa duyệt VP' end as trangthaivp, (select count(servi.*) from serviceprice servi inner join maubenhpham mbp on mbp.maubenhphamid=servi.maubenhphamid and mbp.maubenhphamstatus<>0 where servi.vienphiid=vp.vienphiid) as codichvu FROM vienphi vp inner join hosobenhan hs on vp.hosobenhanid=hs.hosobenhanid inner join bhyt bh on bh.bhytid=vp.bhytid inner join tools_depatment dep on dep.departmentid=vp.departmentid WHERE vp.vienphidate >='" + datetungay + "' and vp.vienphidate<='" + datedenngay + "'" + chonLoaiBenhAn + " GROUP BY vp.vienphiid, vp.patientid, hs.patientname, bh.bhytcode, dep.departmentgroupname, dep.departmentname ORDER BY vp.vienphiid desc) A WHERE A.codichvu=0;";
                                    DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));
                                    if (dv_bhytgroup.Count > 0)
                                    {
                                        gridControlVPTrang.DataSource = dv_bhytgroup;
                                    }
                                    else
                                    {
                                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                        frmthongbao.Show();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                                    frmthongbao.Show();
                                    MedicalLink.Base.Logging.Error(ex);
                                }
                                break;
                            }
                        case "Có đơn chỉ định và trả thuốc/VT":
                            {
                                try
                                {
                                    MessageBox.Show("Chức năng đang phát triển", "Thông báo");
                                    //string sqlquerythuoc = "SELECT serviceprice.servicepriceid as servicepriceid, serviceprice.medicalrecordid as madieutri, serviceprice.vienphiid as mavienphi, serviceprice.hosobenhanid as hosobenhan, serviceprice.maubenhphamid as maubenhpham, serviceprice.servicepricecode as madichvu, serviceprice.servicepricename as tendichvu, serviceprice.servicepricemoney as gia, serviceprice.servicepricemoney_bhyt as gia_bhyt, serviceprice.servicepricemoney_nhandan as gia_nhandan, serviceprice.servicepricemoney_nuocngoai as gia_nnn, serviceprice.soluong as soluong, serviceprice.bhyt_groupcode as bhyt_groupcode, serviceprice.servicepricedate as ngaychidinh FROM serviceprice, servicepriceref, vienphi WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and vienphi.vienphiid=serviceprice.vienphiid and serviceprice.servicepricedate > '" + datetungay + "' and serviceprice.servicepricedate < '" + datedenngay + "' and serviceprice.bhyt_groupcode='' " + phatSinhChiDinh + " ORDER BY servicepriceid ;";
                                    //DataView dv_bhytgroup = new DataView(condb.getDataTable(sqlquerythuoc));

                                    //// Hiển thị
                                    //if (dv_bhytgroup.Count > 0)
                                    //{
                                    //    gridControlDichVu.DataSource = dv_bhytgroup;
                                    //}
                                    //else
                                    //{
                                    //    timerThongBao.Start();
                                    //    lblThongBao.Visible = true;
                                    //    lblThongBao.Text = "Không có bản ghi nào được tìm thấy";
                                    //}

                                }
                                catch (Exception ex)
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                                    frmthongbao.Show();

                                    MedicalLink.Base.Logging.Error(ex);
                                }
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();

                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        // update danh mục dịch vụ
        private void btnUpdateDVOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridControlVPTrang != null && gridViewVPTrang.RowCount > 0)
                {
                    string lstVienphiId = "";
                    for (int i = 0; i < gridViewVPTrang.RowCount - 1; i++)
                    {
                        //long vienphiId = Convert.ToInt64(gridViewVPTrang.GetRowCellValue(i, "vienphiid").ToString());
                        lstVienphiId += gridViewVPTrang.GetRowCellValue(i, "vienphiid").ToString() + ",";
                    }
                    lstVienphiId += gridViewVPTrang.GetRowCellValue(gridViewVPTrang.RowCount - 1, "vienphiid").ToString();
                    // Lấy thời gian hiện tại
                    String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (cbbPhatSinhChiDinh.Text.Trim() == "Có đơn chỉ định và trả thuốc/VT")
                    {
                        DialogResult hoi = MessageBox.Show("Hồ sơ bệnh án có đơn thuốc, bạn có thực sự muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (hoi == DialogResult.Yes)
                        {
                            MessageBox.Show("Chức năng đang phát triển", "Thông báo");
                        }
                    }
                    else
                    {
                        DialogResult hoi = MessageBox.Show("Bạn có thực sự muốn xóa hồ sơ bệnh án?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (hoi == DialogResult.Yes)
                        {
                            string xoaVienPhi = "DELETE FROM vienphi vp WHERE vp.vienphiid in (" + lstVienphiId + ");";
                            string xoaMauBenhPham = "DELETE FROM maubenhpham mbp WHERE mbp.vienphiid in (" + lstVienphiId + ");";
                            if (condb.ExecuteNonQuery_HIS(xoaVienPhi) && condb.ExecuteNonQuery_HIS(xoaMauBenhPham))
                            {
                                //Log vào DB
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa hồ sơ bệnh án: SL= " + gridViewVPTrang.RowCount + "; Chi tiết mã VP: " + lstVienphiId + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                MessageBox.Show("Xóa [" + gridViewVPTrang.RowCount + "] HSSBA thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                gridControlVPTrang.DataSource = null;
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra ", "Thông báo");
                            }
                        }
                    }
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


        // Đánh số thứ tự ở cột Indicator gridView
        private void gridViewDichVu_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewVPTrang.RowCount > 0)
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
                                    gridViewVPTrang.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridViewVPTrang.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridViewVPTrang.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridViewVPTrang.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridViewVPTrang.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridViewVPTrang.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                            frmthongbao.Show();
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
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                frmthongbao.Show();
            }
        }

        private void gridViewVPTrang_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
