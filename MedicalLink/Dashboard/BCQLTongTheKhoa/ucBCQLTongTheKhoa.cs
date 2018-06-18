using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using MedicalLink.Dashboard.BCQLTongTheKhoa;
using DevExpress.XtraSplashScreen;


namespace MedicalLink.Dashboard
{
    public partial class ucBCQLTongTheKhoa : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string thoiGianTu = "";
        string thoiGianDen = "";
        private long tickCurrentVal = 0;
        private long thoiGianCapNhat = 0;
        internal string KhoangThoiGianLayDuLieu { get; set; }
        private List<BCDashboardQLTongTheKhoa> dataBCQLTongTheKhoaSDO { get; set; }
        string lstPhongChonLayBC { get; set; }
        string lstKhoaChonLayBC { get; set; }

        #endregion

        #region Load
        public ucBCQLTongTheKhoa()
        {
            InitializeComponent();
        }

        private void ucBaoCaoTongTheKhoa_Load(object sender, EventArgs e)
        {
            KhoangThoiGianLayDuLieu = GlobalStore.KhoangThoiGianLayDuLieu;
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucKhoa();
            LoadDuLieuMacDinh();
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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

                BCDashboardQLTongTheKhoa dataRow_2 = new BCDashboardQLTongTheKhoa();
                dataRow_2.BNDangDT_stt = 2;
                dataRow_2.BNDangDT_name = "SL bệnh nhân chuyển đi";
                dataRow_2.BNDangDT_value = "0";

                BCDashboardQLTongTheKhoa dataRow_3 = new BCDashboardQLTongTheKhoa();
                dataRow_3.BNDangDT_stt = 3;
                dataRow_3.BNDangDT_name = "SL bệnh nhân chuyển đến";
                dataRow_3.BNDangDT_value = "0";

                BCDashboardQLTongTheKhoa dataRow_4 = new BCDashboardQLTongTheKhoa();
                dataRow_4.BNDangDT_stt = 4;
                dataRow_4.BNDangDT_name = "SL bệnh nhân ra viện";
                dataRow_4.BNDangDT_value = "0";
                dataRow_4.RaVienChuaTT_name = "Số lượng";
                dataRow_4.RaVienChuaTT_value = "0";
                dataRow_4.DaTT_name = "Số lượng";
                dataRow_4.DaTT_value = "0";
                dataRow_4.DoanhThu_name = "Số lượng";
                dataRow_4.DoanhThu_value = "0";
                dataRow_4.DoanhThuGM_name = "Số lượng";
                dataRow_4.DoanhThuGM_value = "0";
                dataRow_4.DoanhThuTongGM_name = "Số lượng";
                dataRow_4.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_5 = new BCDashboardQLTongTheKhoa();
                dataRow_5.BNDangDT_stt = 5;
                dataRow_5.BNDangDT_name = "Tổng tiền";
                dataRow_5.BNDangDT_value = "0";
                dataRow_5.RaVienChuaTT_name = "Tổng tiền";
                dataRow_5.RaVienChuaTT_value = "0";
                dataRow_5.DaTT_name = "Tổng tiền";
                dataRow_5.DaTT_value = "0";
                dataRow_5.DoanhThu_name = "Tổng tiền";
                dataRow_5.DoanhThu_value = "0";
                dataRow_5.DoanhThuGM_name = "Tổng tiền";
                dataRow_5.DoanhThuGM_value = "0";
                dataRow_5.DoanhThuTongGM_name = "Tổng tiền";
                dataRow_5.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_6 = new BCDashboardQLTongTheKhoa();
                dataRow_6.BNDangDT_stt = 6;
                dataRow_6.BNDangDT_name = "Khám bệnh";
                dataRow_6.BNDangDT_value = "0";
                dataRow_6.RaVienChuaTT_name = "Khám bệnh";
                dataRow_6.RaVienChuaTT_value = "0";
                dataRow_6.DaTT_name = "Khám bệnh";
                dataRow_6.DaTT_value = "0";
                dataRow_6.DoanhThu_name = "Khám bệnh";
                dataRow_6.DoanhThu_value = "0";
                dataRow_6.DoanhThuGM_name = "Khám bệnh";
                dataRow_6.DoanhThuGM_value = "0";
                dataRow_6.DoanhThuTongGM_name = "Khám bệnh";
                dataRow_6.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_7 = new BCDashboardQLTongTheKhoa();
                dataRow_7.BNDangDT_stt = 7;
                dataRow_7.BNDangDT_name = "Xét nghiệm";
                dataRow_7.BNDangDT_value = "0";
                dataRow_7.RaVienChuaTT_name = "Xét nghiệm";
                dataRow_7.RaVienChuaTT_value = "0";
                dataRow_7.DaTT_name = "Xét nghiệm";
                dataRow_7.DaTT_value = "0";
                dataRow_7.DoanhThu_name = "Xét nghiệm";
                dataRow_7.DoanhThu_value = "0";
                dataRow_7.DoanhThuGM_name = "Xét nghiệm";
                dataRow_7.DoanhThuGM_value = "0";
                dataRow_7.DoanhThuTongGM_name = "Xét nghiệm";
                dataRow_7.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_8 = new BCDashboardQLTongTheKhoa();
                dataRow_8.BNDangDT_stt = 8;
                dataRow_8.BNDangDT_name = "CĐHA-TDCN";
                dataRow_8.BNDangDT_value = "0";
                dataRow_8.RaVienChuaTT_name = "CĐHA-TDCN";
                dataRow_8.RaVienChuaTT_value = "0";
                dataRow_8.DaTT_name = "CĐHA-TDCN";
                dataRow_8.DaTT_value = "0";
                dataRow_8.DoanhThu_name = "CĐHA-TDCN";
                dataRow_8.DoanhThu_value = "0";
                dataRow_8.DoanhThuGM_name = "CĐHA-TDCN";
                dataRow_8.DoanhThuGM_value = "0";
                dataRow_8.DoanhThuTongGM_name = "CĐHA-TDCN";
                dataRow_8.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_9 = new BCDashboardQLTongTheKhoa();
                dataRow_9.BNDangDT_stt = 9;
                dataRow_9.BNDangDT_name = "PTTT";
                dataRow_9.BNDangDT_value = "0";
                dataRow_9.RaVienChuaTT_name = "PTTT";
                dataRow_9.RaVienChuaTT_value = "0";
                dataRow_9.DaTT_name = "PTTT";
                dataRow_9.DaTT_value = "0";
                dataRow_9.DoanhThu_name = "PTTT";
                dataRow_9.DoanhThu_value = "0";
                dataRow_9.DoanhThuGM_name = "PTTT";
                dataRow_9.DoanhThuGM_value = "0";
                dataRow_9.DoanhThuTongGM_name = "PTTT";
                dataRow_9.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_10 = new BCDashboardQLTongTheKhoa();
                dataRow_10.BNDangDT_stt = 10;
                dataRow_10.BNDangDT_name = "DV KTC";
                dataRow_10.BNDangDT_value = "0";
                dataRow_10.RaVienChuaTT_name = "DV KTC";
                dataRow_10.RaVienChuaTT_value = "0";
                dataRow_10.DaTT_name = "DV KTC";
                dataRow_10.DaTT_value = "0";
                dataRow_10.DoanhThu_name = "DV KTC";
                dataRow_10.DoanhThu_value = "0";
                dataRow_10.DoanhThuGM_name = "DV KTC";
                dataRow_10.DoanhThuGM_value = "0";
                dataRow_10.DoanhThuTongGM_name = "DV KTC";
                dataRow_10.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_11 = new BCDashboardQLTongTheKhoa();
                dataRow_11.BNDangDT_stt = 11;
                dataRow_11.BNDangDT_name = "Giường thường";
                dataRow_11.BNDangDT_value = "0";
                dataRow_11.RaVienChuaTT_name = "Giường thường";
                dataRow_11.RaVienChuaTT_value = "0";
                dataRow_11.DaTT_name = "Giường thường";
                dataRow_11.DaTT_value = "0";
                dataRow_11.DoanhThu_name = "Giường thường";
                dataRow_11.DoanhThu_value = "0";
                dataRow_11.DoanhThuGM_name = "Giường thường";
                dataRow_11.DoanhThuGM_value = "0";
                dataRow_11.DoanhThuTongGM_name = "Giường thường";
                dataRow_11.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_12 = new BCDashboardQLTongTheKhoa();
                dataRow_12.BNDangDT_stt = 12;
                dataRow_12.BNDangDT_name = "Giường yêu cầu";
                dataRow_12.BNDangDT_value = "0";
                dataRow_12.RaVienChuaTT_name = "Giường yêu cầu";
                dataRow_12.RaVienChuaTT_value = "0";
                dataRow_12.DaTT_name = "Giường yêu cầu";
                dataRow_12.DaTT_value = "0";
                dataRow_12.DoanhThu_name = "Giường yêu cầu";
                dataRow_12.DoanhThu_value = "0";
                dataRow_12.DoanhThuGM_name = "Giường yêu cầu";
                dataRow_12.DoanhThuGM_value = "0";
                dataRow_12.DoanhThuTongGM_name = "Giường yêu cầu";
                dataRow_12.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_13 = new BCDashboardQLTongTheKhoa();
                dataRow_13.BNDangDT_stt = 13;
                dataRow_13.BNDangDT_name = "DV khác";
                dataRow_13.BNDangDT_value = "0";
                dataRow_13.RaVienChuaTT_name = "DV khác";
                dataRow_13.RaVienChuaTT_value = "0";
                dataRow_13.DaTT_name = "DV khác";
                dataRow_13.DaTT_value = "0";
                dataRow_13.DoanhThu_name = "DV khác";
                dataRow_13.DoanhThu_value = "0";
                dataRow_13.DoanhThuGM_name = "DV khác";
                dataRow_13.DoanhThuGM_value = "0";
                dataRow_13.DoanhThuTongGM_name = "DV khác";
                dataRow_13.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_14 = new BCDashboardQLTongTheKhoa();
                dataRow_14.BNDangDT_stt = 14;
                dataRow_14.BNDangDT_name = "Máu, chế phẩm";
                dataRow_14.BNDangDT_value = "0";
                dataRow_14.RaVienChuaTT_name = "Máu, chế phẩm";
                dataRow_14.RaVienChuaTT_value = "0";
                dataRow_14.DaTT_name = "Máu, chế phẩm";
                dataRow_14.DaTT_value = "0";
                dataRow_14.DoanhThu_name = "Máu, chế phẩm";
                dataRow_14.DoanhThu_value = "0";
                dataRow_14.DoanhThuGM_name = "Máu, chế phẩm";
                dataRow_14.DoanhThuGM_value = "0";
                dataRow_14.DoanhThuTongGM_name = "Máu, chế phẩm";
                dataRow_14.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_15 = new BCDashboardQLTongTheKhoa();
                dataRow_15.BNDangDT_stt = 15;
                dataRow_15.BNDangDT_name = "Vật tư";
                dataRow_15.BNDangDT_value = "0";
                dataRow_15.RaVienChuaTT_name = "Vật tư";
                dataRow_15.RaVienChuaTT_value = "0";
                dataRow_15.DaTT_name = "Vật tư";
                dataRow_15.DaTT_value = "0";
                dataRow_15.DoanhThu_name = "Vật tư";
                dataRow_15.DoanhThu_value = "0";
                dataRow_15.DoanhThuGM_name = "Vật tư";
                dataRow_15.DoanhThuGM_value = "0";
                dataRow_15.DoanhThuTongGM_name = "Vật tư";
                dataRow_15.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_16 = new BCDashboardQLTongTheKhoa();
                dataRow_16.BNDangDT_stt = 16;
                dataRow_16.BNDangDT_name = "Thuốc";
                dataRow_16.BNDangDT_value = "0";
                dataRow_16.RaVienChuaTT_name = "Thuốc";
                dataRow_16.RaVienChuaTT_value = "0";
                dataRow_16.DaTT_name = "Thuốc";
                dataRow_16.DaTT_value = "0";
                dataRow_16.DoanhThu_name = "Thuốc";
                dataRow_16.DoanhThu_value = "0";
                dataRow_16.DoanhThuGM_name = "Thuốc";
                dataRow_16.DoanhThuGM_value = "0";
                dataRow_16.DoanhThuTongGM_name = "Thuốc";
                dataRow_16.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_17 = new BCDashboardQLTongTheKhoa();
                dataRow_17.BNDangDT_stt = 17;
                dataRow_17.BNDangDT_name = "Tỷ lệ thuốc";
                dataRow_17.BNDangDT_value = "0";
                dataRow_17.RaVienChuaTT_name = "Tỷ lệ thuốc";
                dataRow_17.RaVienChuaTT_value = "0";
                dataRow_17.DaTT_name = "Tỷ lệ thuốc";
                dataRow_17.DaTT_value = "0";
                dataRow_17.DoanhThu_name = "Tỷ lệ thuốc";
                dataRow_17.DoanhThu_value = "0";
                dataRow_17.DoanhThuGM_name = "Tỷ lệ thuốc";
                dataRow_17.DoanhThuGM_value = "0";
                dataRow_17.DoanhThuTongGM_name = "Tỷ lệ thuốc";
                dataRow_17.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_18 = new BCDashboardQLTongTheKhoa();
                dataRow_18.BNDangDT_stt = 18;
                dataRow_18.BNDangDT_name = "Tạm ứng";
                dataRow_18.BNDangDT_value = "0";
                dataRow_18.RaVienChuaTT_name = "Tạm ứng";
                dataRow_18.RaVienChuaTT_value = "0";
                dataRow_18.DaTT_name = "Tạm ứng";
                dataRow_18.DaTT_value = "0";
                dataRow_18.DoanhThu_name = "Tạm ứng";
                dataRow_18.DoanhThu_value = "0";
                dataRow_18.DoanhThuGM_name = "Tạm ứng";
                dataRow_18.DoanhThuGM_value = "0";
                dataRow_18.DoanhThuTongGM_name = "Tạm ứng";
                dataRow_18.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_19 = new BCDashboardQLTongTheKhoa();
                dataRow_19.BNDangDT_stt = 19;
                dataRow_19.BNDangDT_name = "PTTT yêu cầu";
                dataRow_19.BNDangDT_value = "0";
                dataRow_19.RaVienChuaTT_name = "PTTT yêu cầu";
                dataRow_19.RaVienChuaTT_value = "0";
                dataRow_19.DaTT_name = "PTTT yêu cầu";
                dataRow_19.DaTT_value = "0";
                dataRow_19.DoanhThu_name = "PTTT yêu cầu";
                dataRow_19.DoanhThu_value = "0";
                dataRow_19.DoanhThuGM_name = "PTTT yêu cầu";
                dataRow_19.DoanhThuGM_value = "0";
                dataRow_19.DoanhThuTongGM_name = "PTTT yêu cầu";
                dataRow_19.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_20 = new BCDashboardQLTongTheKhoa();
                dataRow_20.BNDangDT_stt = 20;
                dataRow_20.BNDangDT_name = "VT TT riêng";
                dataRow_20.BNDangDT_value = "0";
                dataRow_20.RaVienChuaTT_name = "VT TT riêng";
                dataRow_20.RaVienChuaTT_value = "0";
                dataRow_20.DaTT_name = "VT TT riêng";
                dataRow_20.DaTT_value = "0";
                dataRow_20.DoanhThu_name = "VT TT riêng";
                dataRow_20.DoanhThu_value = "0";
                dataRow_20.DoanhThuGM_name = "VT TT riêng";
                dataRow_20.DoanhThuGM_value = "0";
                dataRow_20.DoanhThuTongGM_name = "VT TT riêng";
                dataRow_20.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_21 = new BCDashboardQLTongTheKhoa();
                dataRow_21.BNDangDT_stt = 21;
                dataRow_21.BNDangDT_name = "Chi phí Xét nghiệm";
                dataRow_21.BNDangDT_value = "0";
                dataRow_21.RaVienChuaTT_name = "Chi phí Xét nghiệm";
                dataRow_21.RaVienChuaTT_value = "0";
                dataRow_21.DaTT_name = "Chi phí Xét nghiệm";
                dataRow_21.DaTT_value = "0";
                dataRow_21.DoanhThu_name = "Chi phí Xét nghiệm";
                dataRow_21.DoanhThu_value = "0";
                dataRow_21.DoanhThuGM_name = "Chi phí Xét nghiệm";
                dataRow_21.DoanhThuGM_value = "0";
                dataRow_21.DoanhThuTongGM_name = "Chi phí Xét nghiệm";
                dataRow_21.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_22 = new BCDashboardQLTongTheKhoa();
                dataRow_22.BNDangDT_stt = 22;
                dataRow_22.BNDangDT_name = "Chi phí CĐHA";
                dataRow_22.BNDangDT_value = "0";
                dataRow_22.RaVienChuaTT_name = "Chi phí CĐHA";
                dataRow_22.RaVienChuaTT_value = "0";
                dataRow_22.DaTT_name = "Chi phí CĐHA";
                dataRow_22.DaTT_value = "0";
                dataRow_22.DoanhThu_name = "Chi phí CĐHA";
                dataRow_22.DoanhThu_value = "0";
                dataRow_22.DoanhThuGM_name = "Chi phí CĐHA";
                dataRow_22.DoanhThuGM_value = "0";
                dataRow_22.DoanhThuTongGM_name = "Chi phí CĐHA";
                dataRow_22.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_23 = new BCDashboardQLTongTheKhoa();
                dataRow_23.BNDangDT_stt = 23;
                dataRow_23.BNDangDT_name = "Chi phí khoa/phòng";
                dataRow_23.BNDangDT_value = "0";
                dataRow_23.RaVienChuaTT_name = "Chi phí khoa/phòng";
                dataRow_23.RaVienChuaTT_value = "0";
                dataRow_23.DaTT_name = "Chi phí khoa/phòng";
                dataRow_23.DaTT_value = "0";
                dataRow_23.DoanhThu_name = "Chi phí khoa/phòng";
                dataRow_23.DoanhThu_value = "0";
                dataRow_23.DoanhThuGM_name = "Chi phí khoa/phòng";
                dataRow_23.DoanhThuGM_value = "0";
                dataRow_23.DoanhThuTongGM_name = "Chi phí khoa/phòng";
                dataRow_23.DoanhThuTongGM_value = "0";


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
                dataBCSDO.Add(dataRow_19);
                dataBCSDO.Add(dataRow_20);

                gridControlDataQLTTKhoa.DataSource = dataBCSDO;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Custom
        public void GetDataCaiDatNangCao(string thoigian)
        {
            KhoangThoiGianLayDuLieu = thoigian;
        }
        private void dateTuNgay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTuNgay.Value < Utilities.TypeConvertParse.ToDateTime(KhoangThoiGianLayDuLieu))
                {
                    dateTuNgay.Value = Utilities.TypeConvertParse.ToDateTime(KhoangThoiGianLayDuLieu);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Thời gian không được nhỏ hơn\n khoảng thời gian lấy dữ liệu");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void bandedGridViewDataQLTTKhoa_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void bandedGridViewDataQLTTKhoa_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                int typeID = 0;
                //loai:
                //=1: BN hien dien
                //=2: BN chuyen di
                //=3: BN chuyen den
                //=4: BN ra vien
                //=5: SL BN da ra vien chua thanh toan
                //=6: SL BN da thanh toan trong ngay
                //=7: SL BN thanh toan trong ngay tinh theo doanh thu
                int rowHandle = bandedGridViewDataQLTTKhoa.FocusedRowHandle;
                string columeFieldName = bandedGridViewDataQLTTKhoa.FocusedColumn.FieldName.ToString();

                //BN hien dien
                if (rowHandle == 0 && columeFieldName == "BNDangDT_value")
                {
                    typeID = 1;
                }
                //BN chuyen di
                else if (rowHandle == 1 && columeFieldName == "BNDangDT_value")
                {
                    typeID = 2;
                }
                //BN chuyen den
                else if (rowHandle == 2 && columeFieldName == "BNDangDT_value")
                {
                    typeID = 3;
                }
                //Bn ra vien
                else if (rowHandle == 3 && columeFieldName == "BNDangDT_value")
                {
                    typeID = 4;
                }
                //SL Bn ra vien chua thanh toan
                else if (rowHandle == 3 && columeFieldName == "RaVienChuaTT_value")
                {
                    typeID = 5;
                }
                //SL BN ra vien da thanh toan
                else if (rowHandle == 3 && columeFieldName == "DaTT_value")
                {
                    typeID = 6;
                }
                //Doanh thu slbn
                else if (rowHandle == 3 && columeFieldName == "DoanhThu_value")
                {
                    typeID = 7;
                }
                //Daonh thu GMHT
                else if (rowHandle == 3 && columeFieldName == "DoanhThuGM_value")
                {
                    typeID = 8;
                }
                //Doanh thu tong 
                else if (rowHandle == 3 && columeFieldName == "DoanhThuTongGM_value")
                {
                    typeID = 9;
                }

                if (typeID != 0 && thoiGianTu != "" && thoiGianDen != "")
                {
                    Dashboard.BCQLTongTheKhoa.BCQLTongTheKhoaBNDetail frmDetail = new Dashboard.BCQLTongTheKhoa.BCQLTongTheKhoaBNDetail(typeID, thoiGianTu, thoiGianDen, this.lstPhongChonLayBC, this.lstKhoaChonLayBC, KhoangThoiGianLayDuLieu);
                    frmDetail.ShowDialog();
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
                        List<ClassCommon.classUserDepartment> lstdsphongthuockhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
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
        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                lstPhongChonLayBC = "";
                lstKhoaChonLayBC = "";

                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                    {
                        lstKhoaChonLayBC += lstKhoaCheck[i] + ",";
                    }
                    lstKhoaChonLayBC += lstKhoaCheck[lstKhoaCheck.Count - 1];
                }

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
                    gridControlDataQLTTKhoa.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataBCQLTongTheKhoaSDO != null && dataBCQLTongTheKhoaSDO.Count > 0)
                {
                    BCQLTongTheKhoa.BCTongTheKhoaFullSize fullSize = new BCQLTongTheKhoa.BCTongTheKhoaFullSize(dataBCQLTongTheKhoaSDO, chkcomboListDSKhoa.Text);
                    fullSize.ShowDialog();
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
