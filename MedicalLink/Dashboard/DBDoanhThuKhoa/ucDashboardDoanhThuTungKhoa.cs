using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalLink.ClassCommon;

namespace MedicalLink.Dashboard
{
    public partial class ucDashboardDoanhThuTungKhoa : UserControl
    {
         #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string thoiGianTu = "";
        string thoiGianDen = "";
        private long tickCurrentVal = 0;
        private long thoiGianCapNhat = 0;
        string lstPhongChonLayBC { get; set; }

        internal string KhoangThoiGianLayDuLieu { get; set; }
        private List<BCDashboardQLTongTheKhoa> dataBCQLTongTheKhoaSDO { get; set; }

        #endregion

        #region Load
        public ucDashboardDoanhThuTungKhoa()
        {
            InitializeComponent();
        }

        private void ucDashboardDoanhThuTungKhoa_Load(object sender, EventArgs e)
        {
            KhoangThoiGianLayDuLieu = GlobalStore.KhoangThoiGianLayDuLieu;
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            EnableControl();
            LoadDanhMucKhoa();
           // LoadDuLieuMacDinh();
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
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyenKhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    chkcomboListDSKhoa.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                List<BCDashboardQLTongTheKhoa> dataBCSDO = new List<BCDashboardQLTongTheKhoa>();
                BCDashboardQLTongTheKhoa dataRow_1 = new BCDashboardQLTongTheKhoa();
                dataRow_1.BNDangDT_stt = 1;
                dataRow_1.BNDangDT_name = "SL bệnh nhân hiện diện";
                dataRow_1.BNDangDT_value = "0";
                dataRow_1.BNDangDT_unit = "";
                dataRow_1.RaVienChuaTT_stt = 1;
                dataRow_1.RaVienChuaTT_name = "Số lượng";
                dataRow_1.RaVienChuaTT_value = "0";
                dataRow_1.RaVienChuaTT_unit = "";
                dataRow_1.DaTT_value = "0";
                dataRow_1.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_2 = new BCDashboardQLTongTheKhoa();
                dataRow_2.BNDangDT_stt = 2;
                dataRow_2.BNDangDT_name = "SL bệnh nhân chuyển đi";
                dataRow_2.BNDangDT_value = "0";
                dataRow_2.BNDangDT_unit = "";
                dataRow_2.RaVienChuaTT_stt = 2;
                dataRow_2.RaVienChuaTT_name = "Tổng doanh thu";
                dataRow_2.RaVienChuaTT_value = "0";
                dataRow_2.RaVienChuaTT_unit = "";
                dataRow_2.DaTT_value = "0";
                dataRow_2.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_3 = new BCDashboardQLTongTheKhoa();
                dataRow_3.BNDangDT_stt = 3;
                dataRow_3.BNDangDT_name = "SL bệnh nhân chuyển đến";
                dataRow_3.BNDangDT_value = "0";
                dataRow_3.BNDangDT_unit = "";
                dataRow_3.RaVienChuaTT_stt = 3;
                dataRow_3.RaVienChuaTT_name = "Khám bệnh";
                dataRow_3.RaVienChuaTT_value = "0";
                dataRow_3.RaVienChuaTT_unit = "";
                dataRow_3.DaTT_value = "0";
                dataRow_3.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_4 = new BCDashboardQLTongTheKhoa();
                dataRow_4.BNDangDT_stt = 4;
                dataRow_4.BNDangDT_name = "SL bệnh nhân ra viện";
                dataRow_4.BNDangDT_value = "0";
                dataRow_4.BNDangDT_unit = "";
                dataRow_4.RaVienChuaTT_stt = 4;
                dataRow_4.RaVienChuaTT_name = "Xét nghiệm";
                dataRow_4.RaVienChuaTT_value = "0";
                dataRow_4.RaVienChuaTT_unit = "";
                dataRow_4.DaTT_value = "0";
                dataRow_4.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_5 = new BCDashboardQLTongTheKhoa();
                dataRow_5.BNDangDT_stt = 5;
                dataRow_5.BNDangDT_name = "Tổng tiền";
                dataRow_5.BNDangDT_value = "0";
                dataRow_5.BNDangDT_unit = "";
                dataRow_5.RaVienChuaTT_stt = 5;
                dataRow_5.RaVienChuaTT_name = "CĐHA-TDCN";
                dataRow_5.RaVienChuaTT_value = "0";
                dataRow_5.RaVienChuaTT_unit = "";
                dataRow_5.DaTT_value = "0";
                dataRow_5.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_6 = new BCDashboardQLTongTheKhoa();
                dataRow_6.BNDangDT_stt = 6;
                dataRow_6.BNDangDT_name = "Khám bệnh";
                dataRow_6.BNDangDT_value = "0";
                dataRow_6.BNDangDT_unit = "";
                dataRow_6.RaVienChuaTT_stt = 6;
                dataRow_6.RaVienChuaTT_name = "PTTT";
                dataRow_6.RaVienChuaTT_value = "0";
                dataRow_6.RaVienChuaTT_unit = "";
                dataRow_6.DaTT_value = "0";
                dataRow_6.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_7 = new BCDashboardQLTongTheKhoa();
                dataRow_7.BNDangDT_stt = 7;
                dataRow_7.BNDangDT_name = "Xét nghiệm";
                dataRow_7.BNDangDT_value = "0";
                dataRow_7.BNDangDT_unit = "";
                dataRow_7.RaVienChuaTT_stt = 7;
                dataRow_7.RaVienChuaTT_name = "DV KTC";
                dataRow_7.RaVienChuaTT_value = "0";
                dataRow_7.RaVienChuaTT_unit = "";
                dataRow_7.DaTT_value = "0";
                dataRow_7.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_8 = new BCDashboardQLTongTheKhoa();
                dataRow_8.BNDangDT_stt = 8;
                dataRow_8.BNDangDT_name = "CĐHA-TDCN";
                dataRow_8.BNDangDT_value = "0";
                dataRow_8.BNDangDT_unit = "";
                dataRow_8.RaVienChuaTT_stt = 8;
                dataRow_8.RaVienChuaTT_name = "Giường thường";
                dataRow_8.RaVienChuaTT_value = "0";
                dataRow_8.RaVienChuaTT_unit = "";
                dataRow_8.DaTT_value = "0";
                dataRow_8.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_9 = new BCDashboardQLTongTheKhoa();
                dataRow_9.BNDangDT_stt = 9;
                dataRow_9.BNDangDT_name = "PTTT";
                dataRow_9.BNDangDT_value = "0";
                dataRow_9.BNDangDT_unit = "";
                dataRow_9.RaVienChuaTT_stt = 9;
                dataRow_9.RaVienChuaTT_name = "Giường yêu cầu";
                dataRow_9.RaVienChuaTT_value = "0";
                dataRow_9.RaVienChuaTT_unit = "";
                dataRow_9.DaTT_value = "0";
                dataRow_9.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_10 = new BCDashboardQLTongTheKhoa();
                dataRow_10.BNDangDT_stt = 10;
                dataRow_10.BNDangDT_name = "DV KTC";
                dataRow_10.BNDangDT_value = "0";
                dataRow_10.BNDangDT_unit = "";
                dataRow_10.RaVienChuaTT_stt = 10;
                dataRow_10.RaVienChuaTT_name = "DV khác";
                dataRow_10.RaVienChuaTT_value = "0";
                dataRow_10.RaVienChuaTT_unit = "";
                dataRow_10.DaTT_value = "0";
                dataRow_10.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_11 = new BCDashboardQLTongTheKhoa();
                dataRow_11.BNDangDT_stt = 11;
                dataRow_11.BNDangDT_name = "Giường thường";
                dataRow_11.BNDangDT_value = "0";
                dataRow_11.BNDangDT_unit = "";
                dataRow_11.RaVienChuaTT_stt = 11;
                dataRow_11.RaVienChuaTT_name = "Máu, chế phẩm";
                dataRow_11.RaVienChuaTT_value = "0";
                dataRow_11.RaVienChuaTT_unit = "";
                dataRow_11.DaTT_value = "0";
                dataRow_11.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_12 = new BCDashboardQLTongTheKhoa();
                dataRow_12.BNDangDT_stt = 12;
                dataRow_12.BNDangDT_name = "Giường yêu cầu";
                dataRow_12.BNDangDT_value = "0";
                dataRow_12.BNDangDT_unit = "";
                dataRow_12.RaVienChuaTT_stt = 12;
                dataRow_12.RaVienChuaTT_name = "Vật tư";
                dataRow_12.RaVienChuaTT_value = "0";
                dataRow_12.RaVienChuaTT_unit = "";
                dataRow_12.DaTT_value = "0";
                dataRow_12.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_13 = new BCDashboardQLTongTheKhoa();
                dataRow_13.BNDangDT_stt = 13;
                dataRow_13.BNDangDT_name = "DV khác";
                dataRow_13.BNDangDT_value = "0";
                dataRow_13.BNDangDT_unit = "";
                dataRow_13.RaVienChuaTT_stt = 13;
                dataRow_13.RaVienChuaTT_name = "Thuốc";
                dataRow_13.RaVienChuaTT_value = "0";
                dataRow_13.RaVienChuaTT_unit = "";
                dataRow_13.DaTT_value = "0";
                dataRow_13.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_14 = new BCDashboardQLTongTheKhoa();
                dataRow_14.BNDangDT_stt = 14;
                dataRow_14.BNDangDT_name = "Máu, chế phẩm";
                dataRow_14.BNDangDT_value = "0";
                dataRow_14.BNDangDT_unit = "";
                dataRow_14.RaVienChuaTT_stt = 14;
                dataRow_14.RaVienChuaTT_name = "Tỷ lệ thuốc";
                dataRow_14.RaVienChuaTT_value = "0";
                dataRow_14.RaVienChuaTT_unit = "";
                dataRow_14.DaTT_value = "0";
                dataRow_14.DoanhThu_value = "0";

                BCDashboardQLTongTheKhoa dataRow_15 = new BCDashboardQLTongTheKhoa();
                dataRow_15.BNDangDT_stt = 15;
                dataRow_15.BNDangDT_name = "Vật tư";
                dataRow_15.BNDangDT_value = "0";
                dataRow_15.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_16 = new BCDashboardQLTongTheKhoa();
                dataRow_16.BNDangDT_stt = 16;
                dataRow_16.BNDangDT_name = "Thuốc";
                dataRow_16.BNDangDT_value = "0";
                dataRow_16.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_17 = new BCDashboardQLTongTheKhoa();
                dataRow_17.BNDangDT_stt = 17;
                dataRow_17.BNDangDT_name = "Tỷ lệ thuốc";
                dataRow_17.BNDangDT_value = "0";
                dataRow_17.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_18 = new BCDashboardQLTongTheKhoa();
                dataRow_18.BNDangDT_stt = 18;
                dataRow_18.BNDangDT_name = "Tạm ứng";
                dataRow_18.BNDangDT_value = "0";
                dataRow_18.BNDangDT_unit = "";

                dataBCSDO.Add(dataRow_1);
                dataBCSDO.Add(dataRow_2);
                dataBCSDO.Add(dataRow_3);
                dataBCSDO.Add(dataRow_4);
                dataBCSDO.Add(dataRow_5);
                dataBCSDO.Add(dataRow_6);
                dataBCSDO.Add(dataRow_7);
                dataBCSDO.Add(dataRow_8);
                dataBCSDO.Add(dataRow_9);
                dataBCSDO.Add(dataRow_10);
                dataBCSDO.Add(dataRow_11);
                dataBCSDO.Add(dataRow_12);
                dataBCSDO.Add(dataRow_13);
                dataBCSDO.Add(dataRow_14);
                dataBCSDO.Add(dataRow_15);
                dataBCSDO.Add(dataRow_16);
                dataBCSDO.Add(dataRow_17);
                dataBCSDO.Add(dataRow_18);

                chartControlDTKhoa.DataSource = dataBCSDO;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
                lstPhongChonLayBC = "";
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        lstPhongChonLayBC += lstPhongCheck[i] + ",";
                    }
                    lstPhongChonLayBC += lstPhongCheck[lstPhongCheck.Count - 1];
                }
                if (lstPhongChonLayBC == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
                else
                {
                    chartControlDTKhoa.DataSource = null; 
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

        private void dateTuNgay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTuNgay.Value < Utilities.Util_TypeConvertParse.ToDateTime(KhoangThoiGianLayDuLieu))
                {
                    dateTuNgay.Value = Utilities.Util_TypeConvertParse.ToDateTime(KhoangThoiGianLayDuLieu);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Thời gian không được nhỏ hơn\n khoảng thời gian lấy dữ liệu");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void chkcomboListDSKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListDSPhong.Properties.Items.Clear();
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    //Load danh muc phong theo khoa da chon
                    List<ClassCommon.classUserDepartment> lstDSPhong = new List<classUserDepartment>();
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        List<ClassCommon.classUserDepartment> lstdsphongthuockhoa = Base.SessionLogin.SessionlstPhanQuyenKhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
                        lstDSPhong.AddRange(lstdsphongthuockhoa);
                    }
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
                    chkcomboListDSPhong.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



    }
}
