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
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;

namespace MedicalLink.Dashboard
{
    /// <summary>
    /// BC Doanh thu theo Khoa phong ra vien
    /// </summary>
    public partial class ucBaoCaoTongHopToanVien : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //string thoiGianTu = "";
        //string thoiGianDen = "";
        //private long tickCurrentVal = 0;
        //private long thoiGianCapNhat = 0;
        DataTable dataBCBTongHopToanVien { get; set; }

        #endregion

        #region Load
        public ucBaoCaoTongHopToanVien()
        {
            InitializeComponent();
        }

        private void ucBaoCaoTongHopToanVien_Load(object sender, EventArgs e)
        {
            //KhoangThoiGianLayDuLieu = GlobalStore.KhoangThoiGianLayDuLieu;
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            EnableControl();
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
                // spinThoiGianCapNhat.Value = 0;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                //laytheo =0: theo khoa ra vien; =1: theo khoa chi dinh
                //Kieu xem =0: xem tong hop; =1: xem chi tiet theo khoa; = 2 xem chi tiet theo benh nhan
                int laytheo = 0;
                int kieuxem = 0;
                if (cboLayTheo.Text == "Theo khoa chỉ định")
                {
                    laytheo = 1;
                }

                if (cboKieuXem.Text == "Xem chi tiết theo khoa")
                {
                    kieuxem = 1;
                }
                else if (cboKieuXem.Text == "Xem chi tiết bệnh nhân")
                {
                    kieuxem = 2;
                }

                gridControlDataBNNT.DataSource = null;
                long thoiGianTu = Convert.ToInt64(DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));
                long thoiGianDen = Convert.ToInt64(DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd"));

                if (thoiGianTu > 20161231 && thoiGianDen > 20161231)
                {
                    LayDuLieuBaoCao_ChayMoi(laytheo, kieuxem);
                }
                else if (thoiGianTu < 20161231 && thoiGianDen <= 20161231)
                {
                    LayDuLieuBaoCao_Old(laytheo, kieuxem);
                }
                else
                {
                    MessageBox.Show("Vui lòng chia làm 2 khoảng từ trước ngày 31/12/2016 và sau ngày 01/01/2017", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        #region events
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
                    for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year; i++)
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

        private void timerTuDongCapNhat_Tick(object sender, EventArgs e)
        {
            try
            {
                //lblThoiGianConLai.Text = "Tự động cập nhật sau " + tickCurrentVal + " giây";
                //tickCurrentVal--;
                //if (tickCurrentVal == 0)
                //{
                //    if (GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp > 0)
                //    {
                //        //LayDuLieuBaoCao_DaChayDuLieu();
                //        LayDuLieuBaoCao_ChayMoi();
                //    }
                //    else
                //    {
                //        LayDuLieuBaoCao_ChayMoi();
                //    }
                //    tickCurrentVal = thoiGianCapNhat;
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnFullSize_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataBCBTongHopToanVien != null && dataBCBTongHopToanVien.Rows.Count > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    int kieuxem = 0;
                    if (cboKieuXem.Text == "Xem chi tiết bệnh nhân")
                    {
                        kieuxem = 2;
                    }
                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";
                    BCTongHopToanVien.BaoCaoTongHopToanVienFullSize fullSize = new BCTongHopToanVien.BaoCaoTongHopToanVienFullSize(dataBCBTongHopToanVien, tungaydenngay, kieuxem);
                    fullSize.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void cboLayTheo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLayTheo.Text == "Theo khoa chỉ định")
                {
                    cboKieuXem.Properties.Items.Clear();
                    cboKieuXem.Properties.Items.Add("Xem tổng hợp");
                    cboKieuXem.Properties.Items.Add("Xem chi tiết theo khoa");
                    cboKieuXem.Text = "Xem chi tiết theo khoa";
                }
                else
                {
                    cboKieuXem.Properties.Items.Clear();
                    cboKieuXem.Properties.Items.Add("Xem tổng hợp");
                    cboKieuXem.Properties.Items.Add("Xem chi tiết theo khoa");
                    cboKieuXem.Properties.Items.Add("Xem chi tiết bệnh nhân");
                    cboKieuXem.Text = "Xem tổng hợp";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        #region Cusstom
        private void bandedGridViewDataBNNT_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
    }
}
