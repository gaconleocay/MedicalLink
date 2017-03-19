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
    public partial class ucUpdateDataSerPrice : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string chonkieuimport;
        //DataView dv_bhytgroup;
        public ucUpdateDataSerPrice()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            chonkieuimport = cbbChonKieu.Text.Trim();
            string trangthaiVP = "";
            gridControlDichVu.DataSource = null;
            string datetungay = "";
            string datedenngay = "";
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            if (cbbChonKieu.Text != "" && dateTuNgay.Text != "" && dateDenNgay.Text != "" && cbbTrangThaiVP.Text != "")
            {
                // Lấy từ ngày, đến ngày
                datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cbbTrangThaiVP.Text.Trim() == "Đang điều trị")
                {
                    trangthaiVP = " and vienphi.vienphistatus=0 ";
                }
                else if (cbbTrangThaiVP.Text.Trim() == "Đóng BA nhưng chưa duyệt VP")
                {
                    trangthaiVP = " and vienphi.vienphidate_ravien != '0001-01-01 00:00:00' and (vienphi.vienphistatus_vp IS NULL or vienphi.vienphistatus_vp=0) ";
                }
                else if (cbbTrangThaiVP.Text.Trim() == "Đã duyệt viện phí")
                {
                    trangthaiVP = " and vienphi.vienphistatus_vp=1 ";
                }


                switch (chonkieuimport)
                {
                    case "Update nhóm BHYT-Thuốc/VT":
                        {
                            try
                            {
                                string sqlquerythuoc = "SELECT serviceprice.servicepriceid as servicepriceid, serviceprice.medicalrecordid as madieutri, serviceprice.vienphiid as mavienphi, serviceprice.hosobenhanid as hosobenhan, serviceprice.maubenhphamid as maubenhpham, serviceprice.servicepricecode as madichvu, serviceprice.servicepricename as tendichvu, serviceprice.servicepricemoney as gia, serviceprice.servicepricemoney_bhyt as gia_bhyt, serviceprice.servicepricemoney_nhandan as gia_nhandan, serviceprice.servicepricemoney_nuocngoai as gia_nnn, serviceprice.soluong as soluong, serviceprice.bhyt_groupcode as bhyt_groupcode, serviceprice.servicepricedate as ngaychidinh FROM serviceprice,medicine_ref, vienphi WHERE medicine_ref.medicinecode=serviceprice.servicepricecode and vienphi.vienphiid=serviceprice.vienphiid and serviceprice.servicepricedate > '" + datetungay + "' and serviceprice.servicepricedate < '" + datedenngay + "' and serviceprice.bhyt_groupcode='' " + trangthaiVP + " ORDER BY servicepriceid ;";
                                DataView dv_bhytgroup = new DataView(condb.getDataTable(sqlquerythuoc));

                                // Hiển thị
                                if (dv_bhytgroup.Count > 0)
                                {
                                    gridControlDichVu.DataSource = dv_bhytgroup;
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                                    frmthongbao.Show();
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                            }
                            break;
                        }
                    case "Update nhóm BHYT-Dịch vụ":
                        {
                            try
                            {
                                string sqlquerythuoc = "SELECT serviceprice.servicepriceid as servicepriceid, serviceprice.medicalrecordid as madieutri, serviceprice.vienphiid as mavienphi, serviceprice.hosobenhanid as hosobenhan, serviceprice.maubenhphamid as maubenhpham, serviceprice.servicepricecode as madichvu, serviceprice.servicepricename as tendichvu, serviceprice.servicepricemoney as gia, serviceprice.servicepricemoney_bhyt as gia_bhyt, serviceprice.servicepricemoney_nhandan as gia_nhandan, serviceprice.servicepricemoney_nuocngoai as gia_nnn, serviceprice.soluong as soluong, serviceprice.bhyt_groupcode as bhyt_groupcode, serviceprice.servicepricedate as ngaychidinh FROM serviceprice, servicepriceref, vienphi WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and vienphi.vienphiid=serviceprice.vienphiid and serviceprice.servicepricedate > '" + datetungay + "' and serviceprice.servicepricedate < '" + datedenngay + "' and serviceprice.bhyt_groupcode='' " + trangthaiVP + " ORDER BY servicepriceid ;";
                                DataView dv_bhytgroup = new DataView(condb.getDataTable(sqlquerythuoc));

                                // Hiển thị
                                if (dv_bhytgroup.Count > 0)
                                {
                                    gridControlDichVu.DataSource = dv_bhytgroup;
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
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
            SplashScreenManager.CloseForm();
        }

        // update danh mục dịch vụ
        private void btnUpdateDVOK_Click(object sender, EventArgs e)
        {
            chonkieuimport = cbbChonKieu.Text.Trim();
            int dem_sl = 0;
            // String lưu 1 mảng các ServicepriceID để load lại dữ liệu
            string arrayserID = "";
            // Lấy thời gian hiện tại
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            switch (chonkieuimport)
            {
                case "Update nhóm BHYT-Thuốc/VT":
                    {
                        try
                        {
                            if (gridViewDichVu.RowCount > 0)
                            {
                                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                {
                                    //Tìm mã bhytgroupcode từ medicine_ref
                                    string sqlget_bhytgroupcode = "SELECT medicine_ref.bhyt_groupcode as bhyt_groupcode FROM serviceprice, medicine_ref WHERE medicine_ref.medicinecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                    DataView dv_bhytgroupcode = new DataView(condb.getDataTable(sqlget_bhytgroupcode));
                                    if (dv_bhytgroupcode.Count > 0)
                                    {
                                        try
                                        {
                                            string update_bhyt_thuoc = "UPDATE serviceprice SET bhyt_groupcode = '" + dv_bhytgroupcode[0]["bhyt_groupcode"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                            condb.ExecuteNonQuery(update_bhyt_thuoc);
                                            dem_sl = dem_sl + 1;
                                        }
                                        catch (Exception)
                                        {
                                            continue;
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
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }
                        break;
                    }

                case "Update nhóm BHYT-Dịch vụ":
                    {
                        try
                        {
                            if (gridViewDichVu.RowCount > 0)
                            {
                                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                {
                                    //Tìm mã bhytgroupcode từ medicine_ref
                                    string sqlget_bhytgroupcode = "SELECT servicepriceref.bhyt_groupcode as bhyt_groupcode FROM serviceprice, servicepriceref WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                    DataView dv_bhytgroupcode = new DataView(condb.getDataTable(sqlget_bhytgroupcode));
                                    if (dv_bhytgroupcode.Count > 0)
                                    {
                                        try
                                        {
                                            string update_bhyt_dv = "UPDATE serviceprice SET bhyt_groupcode = '" + dv_bhytgroupcode[0]["bhyt_groupcode"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                            condb.ExecuteNonQuery(update_bhyt_dv);
                                            dem_sl = dem_sl + 1;
                                        }
                                        catch (Exception)
                                        {
                                            continue;
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
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }
                        break;
                    }

                default:
                    {
                        MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
            }

            if (gridViewDichVu.RowCount > 0)
            {
                // Load lại dữ liệu cho người dùng xem
                for (int i = 0; i < gridViewDichVu.RowCount - 1; i++)
                {
                    arrayserID += gridViewDichVu.GetRowCellValue(i, "servicepriceid").ToString() + ",";
                }
                arrayserID += gridViewDichVu.GetRowCellValue(gridViewDichVu.RowCount - 1, "servicepriceid").ToString();

                string sqlquerythuoc = "SELECT serviceprice.servicepriceid as servicepriceid, serviceprice.medicalrecordid as madieutri, serviceprice.vienphiid as mavienphi, serviceprice.hosobenhanid as hosobenhan, serviceprice.maubenhphamid as maubenhpham, serviceprice.servicepricecode as madichvu, serviceprice.servicepricename as tendichvu, serviceprice.servicepricemoney as gia, serviceprice.servicepricemoney_bhyt as gia_bhyt, serviceprice.servicepricemoney_nhandan as gia_nhandan, serviceprice.servicepricemoney_nuocngoai as gia_nnn, serviceprice.soluong as soluong, serviceprice.bhyt_groupcode as bhyt_groupcode, serviceprice.servicepricedate as ngaychidinh FROM serviceprice WHERE serviceprice.servicepriceid in (" + arrayserID + ")  ORDER BY servicepriceid;";
                DataView dv_bhytgroup = new DataView(condb.getDataTable(sqlquerythuoc));
                gridControlDichVu.DataSource = dv_bhytgroup;
                // Luu lai log
                if (dem_sl > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + dem_sl + " trường " + chonkieuimport + " thành công. ServicepriceID: " + arrayserID + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }
                // Thông báo đã Update Tên dịch vụ
                MessageBox.Show("Update " + dem_sl + " danh mục \"nhóm BHYT của thuốc/vật tư\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // Đánh số thứ tự ở cột Indicator gridView
        private void gridViewDichVu_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }


        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateTuNgay.Value = date_tu;
            DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            dateDenNgay.Value = date_den;
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDichVu.RowCount > 0)
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
                                    gridViewDichVu.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridViewDichVu.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridViewDichVu.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridViewDichVu.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridViewDichVu.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridViewDichVu.ExportToMht(exportFilePath);
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
    }
}
