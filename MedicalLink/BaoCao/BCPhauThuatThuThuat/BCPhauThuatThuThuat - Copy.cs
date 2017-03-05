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
using DevExpress.Utils.Menu;

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BCPhauThuatThuThuat : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
       DataTable dataBCPTTT { get; set; }

        #endregion

        #region Load
        public BCPhauThuatThuThuat()
        {
            InitializeComponent();
        }

        private void BCPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            EnableControl();
            LoadDanhMucKhoa();
            LoadDanhSachBaoCao();
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
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyenKhoaPhong.Where(o => o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
        private void LoadDanhSachBaoCao()
        {
            try
            {

                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo Phẫu thuật - Chung", OnExportClick));
                menu.Items.Add(new DXMenuItem("Báo cáo Phẫu thuật - Khoa Gây mê hồi tỉnh", OnExportClick));
                menu.Items.Add(new DXMenuCheckItem("Báo cáo Phẫu thuật - Khoa Tai mũi họng", false, null, OnExportClick));
                menu.Items.Add(new DXMenuItem("Báo cáo Phẫu thuật - Khoa Răng hàm mặt", OnExportClick));
                menu.Items.Add(new DXMenuCheckItem("Báo cáo Phẫu thuật - Khoa Mắt", false, null, OnExportClick));
                menu.Items.Add(new DXMenuCheckItem("Báo cáo Thủ thuật - Chung", false, null, OnExportClick));
                menu.Items.Add(new DXMenuCheckItem("Báo cáo Thủ thuật - Khoa Mắt", false, null, OnExportClick));
                btndropExport.DropDownControl = menu;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

     
        #endregion
        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

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
                if (cboTieuChi.Text == "Theo thời gian thực hiện" && cboLoai.Text == "Phẫu thuật")
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi(0,1);
                }
                else if (cboTieuChi.Text == "Theo thời gian thực hiện" && cboLoai.Text == "Thủ thuật")
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi(0, 2);
                }
                else if (cboTieuChi.Text == "Theo thời gian thực hiện" && cboLoai.Text == "Tất cả")
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi(0, 0);
                }
                else if (cboTieuChi.Text == "Theo thời gian thanh toán" && cboLoai.Text == "Phẫu thuật")
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi(1, 1);
                }
                else if (cboTieuChi.Text == "Theo thời gian thanh toán" && cboLoai.Text == "Thủ thuật")
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi(1, 2);
                }
                else if (cboTieuChi.Text == "Theo thời gian thanh toán" && cboLoai.Text == "Tất cả")
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi(1, 0);
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Vui lòng chọn tiêu chí và loại.";
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
                            dateDenNgay.Value = Convert.ToDateTime(MedicalLink.Utilities.Util_DateTime.GetLastDayOfMonth(2).ToString("yyyy-MM-dd") + " 23:59:59");
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
