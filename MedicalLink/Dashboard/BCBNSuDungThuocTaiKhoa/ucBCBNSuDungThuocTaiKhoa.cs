using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.Utils.Menu;
using System.IO;

namespace MedicalLink.Dashboard
{
    /// <summary>
    /// BC doanh thu theo khoa ra viện
    /// </summary>
    public partial class ucBCBNSuDungThuocTaiKhoa : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        private List<ClassCommon.classMedicineRef> lstMedicineStore { get; set; }

        #endregion

        #region Load
        public ucBCBNSuDungThuocTaiKhoa()
        {
            InitializeComponent();
        }

        private void ucBCBNSuDungThuocTaiKhoa_Load(object sender, EventArgs e)
        {
            //KhoangThoiGianLayDuLieu = GlobalStore.KhoangThoiGianLayDuLieu;
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            EnableControl();
            LoadDanhMucKhoa();
            LoadDanhMucThuocVatTu();
        }

        private void EnableControl()
        {
            try
            {
                radioThang.Checked = false;
                radioQuy.Checked = false;
                radioNam.Checked = false;
                cboChonNhanh.Enabled = false;
                cboChonNhanh.Properties.Items.Clear();
                radioThuoc.Checked = true;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    cboKhoa.Properties.DataSource = lstDSKhoa;
                    cboKhoa.Properties.DisplayMember = "departmentgroupname";
                    cboKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    cboKhoa.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhMucThuocVatTu()
        {
            try
            {
                string sql_getmeref = "SELECT mef.medicinerefid, mef.medicinerefid_org, mef.medicinecode, mef.medicinename, mef.giaban FROM medicine_ref mef WHERE mef.isremove=0;";
                DataView dataStore = new DataView(condb.GetDataTable_HIS(sql_getmeref));
                lstMedicineStore = new List<ClassCommon.classMedicineRef>();

                if (dataStore != null && dataStore.Count > 0)
                {
                    for (int i = 0; i < dataStore.Count; i++)
                    {
                        ClassCommon.classMedicineRef medicinestore = new ClassCommon.classMedicineRef();
                        medicinestore.medicinerefid = Utilities.TypeConvertParse.ToInt64(dataStore[i]["medicinerefid"].ToString());
                        medicinestore.medicinerefid_org = Utilities.TypeConvertParse.ToInt64(dataStore[i]["medicinerefid_org"].ToString());
                        medicinestore.medicinecode = dataStore[i]["medicinecode"].ToString();
                        medicinestore.medicinename = dataStore[i]["medicinename"].ToString();
                        medicinestore.giaban = Utilities.TypeConvertParse.ToDecimal(dataStore[i]["giaban"].ToString());
                        lstMedicineStore.Add(medicinestore);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void radioThang_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioThang.Checked)
                {
                    cboChonNhanh.Enabled = true;
                    radioQuy.Checked = false;
                    radioNam.Checked = false;
                    cboChonNhanh.Properties.Items.Clear();
                    cboChonNhanh.Text = "";
                    cboChonNhanh.Properties.Items.Add("Tháng 1");
                    cboChonNhanh.Properties.Items.Add("Tháng 2");
                    cboChonNhanh.Properties.Items.Add("Tháng 3");
                    cboChonNhanh.Properties.Items.Add("Tháng 4");
                    cboChonNhanh.Properties.Items.Add("Tháng 5");
                    cboChonNhanh.Properties.Items.Add("Tháng 6");
                    cboChonNhanh.Properties.Items.Add("Tháng 7");
                    cboChonNhanh.Properties.Items.Add("Tháng 8");
                    cboChonNhanh.Properties.Items.Add("Tháng 9");
                    cboChonNhanh.Properties.Items.Add("Tháng 10");
                    cboChonNhanh.Properties.Items.Add("Tháng 11");
                    cboChonNhanh.Properties.Items.Add("Tháng 12");
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioQuy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioQuy.Checked)
                {
                    cboChonNhanh.Enabled = true;
                    radioThang.Checked = false;
                    radioNam.Checked = false;
                    cboChonNhanh.Properties.Items.Clear();
                    cboChonNhanh.Text = "";
                    cboChonNhanh.Properties.Items.Add("Quý 1");
                    cboChonNhanh.Properties.Items.Add("Quý 2");
                    cboChonNhanh.Properties.Items.Add("Quý 3");
                    cboChonNhanh.Properties.Items.Add("Quý 4");
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioNam_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioNam.Checked)
                {
                    cboChonNhanh.Enabled = true;
                    radioThang.Checked = false;
                    radioQuy.Checked = false;
                    cboChonNhanh.Properties.Items.Clear();
                    cboChonNhanh.Text = "";
                    for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year; i++)
                    {
                        cboChonNhanh.Properties.Items.Add("Năm " + i);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
                else
                {
                    gridControlDataQLTTKhoa.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void cboChonNhanh_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioNam.Checked)
                {
                    dateTuNgay.Value = new DateTime(Convert.ToInt16(cboChonNhanh.Text.Trim().ToString().Substring(4)), 1, 1, 0, 0, 0);
                    dateDenNgay.Value = new DateTime(Convert.ToInt16(cboChonNhanh.Text.Trim().ToString().Substring(4)), 12, 31, 23, 59, 59);
                }
                else
                {
                    switch (cboChonNhanh.EditValue.ToString())
                    {
                        case "Tháng 1":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 1, 31, 23, 59, 59);
                            break;
                        case "Tháng 2":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 2, 1, 0, 0, 0);
                            dateDenNgay.Value = Convert.ToDateTime(MedicalLink.Utilities.DateTimes.GetLastDayOfMonth(2).ToString("yyyy-MM-dd") + " 23:59:59");
                            break;
                        case "Tháng 3":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 3, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 3, 31, 23, 59, 59);
                            break;
                        case "Tháng 4":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 4, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 4, 30, 23, 59, 59);
                            break;
                        case "Tháng 5":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 5, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 5, 31, 23, 59, 59);
                            break;
                        case "Tháng 6":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 6, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 6, 30, 23, 59, 59);
                            break;
                        case "Tháng 7":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 7, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 7, 31, 23, 59, 59);
                            break;
                        case "Tháng 8":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 8, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 8, 31, 23, 59, 59);
                            break;
                        case "Tháng 9":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 9, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 9, 30, 23, 59, 59);
                            break;
                        case "Tháng 10":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 10, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 10, 31, 23, 59, 59);
                            break;
                        case "Tháng 11":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 11, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 11, 30, 23, 59, 59);
                            break;
                        case "Tháng 12":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 12, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
                            break;

                        case "Quý 1":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 3, 31, 23, 59, 59);
                            break;
                        case "Quý 2":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 4, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 6, 30, 23, 59, 59);
                            break;
                        case "Quý 3":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 7, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 9, 30, 23, 59, 59);
                            break;
                        case "Quý 4":
                            dateTuNgay.Value = new DateTime(DateTime.Now.Year, 10, 1, 0, 0, 0);
                            dateDenNgay.Value = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void dateTuNgay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (dateTuNgay.Value < Utilities.Util_TypeConvertParse.ToDateTime(KhoangThoiGianLayDuLieu))
                //{
                //    dateTuNgay.Value = Utilities.Util_TypeConvertParse.ToDateTime(KhoangThoiGianLayDuLieu);
                //    timerThongBao.Start();
                //    lblThongBao.Visible = true;
                //    lblThongBao.Text = "Thời gian không được nhỏ hơn\n khoảng thời gian lấy dữ liệu";
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioThuoc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioThuoc.Checked)
                {
                    radioVatTu.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioVatTu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioVatTu.Checked)
                {
                    radioThuoc.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void bandedGridViewDataQLTTKhoa_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void bandedGridViewDataQLTTKhoa_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemBNDangDT = new DXMenuItem("Xem chi tiết sử dụng - BN đang điều trị");
                    itemBNDangDT.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemBNDangDT.Click += new EventHandler(itemXemBNDangDT_Click);
                    e.Menu.Items.Add(itemBNDangDT);
                    DXMenuItem itemRaVienChuaTT = new DXMenuItem("Xem chi tiết sử dụng - BN ra viện chưa thanh toán");
                    itemRaVienChuaTT.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemRaVienChuaTT.Click += new EventHandler(itemRaVienChuaTT_Click);
                    e.Menu.Items.Add(itemRaVienChuaTT);
                    DXMenuItem itemDaThanhToan = new DXMenuItem("Xem chi tiết sử dụng - BN đã thanh toán");
                    itemDaThanhToan.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemDaThanhToan.Click += new EventHandler(itemDaThanhToan_Click);
                    e.Menu.Items.Add(itemDaThanhToan);
                    DXMenuItem itemDoanhThu = new DXMenuItem("Xem chi tiết sử dụng - Doanh thu khoa");
                    itemDoanhThu.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemDoanhThu.Click += new EventHandler(itemDoanhThu_Click);
                    e.Menu.Items.Add(itemDoanhThu);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (bandedGridViewDataQLTTKhoa.RowCount > 0)
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
                                        bandedGridViewDataQLTTKhoa.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        bandedGridViewDataQLTTKhoa.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        bandedGridViewDataQLTTKhoa.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        bandedGridViewDataQLTTKhoa.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        bandedGridViewDataQLTTKhoa.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        bandedGridViewDataQLTTKhoa.ExportToMht(exportFilePath);
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
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
    }
}
