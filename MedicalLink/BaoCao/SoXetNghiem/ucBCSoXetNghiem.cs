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
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using Aspose.Cells;

namespace MedicalLink.BaoCao
{
    public partial class ucBCSoXetNghiem : UserControl
    {
        #region Khai bao
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private DataTable dataBaoCao_VS { get; set; }
        private DataTable dataBaoCao_SHTQ { get; set; }
        private DataTable dataBaoCao_NTVD { get; set; }
        private DataTable dataBaoCao_MD { get; set; }
        private DataTable dataBaoCao_KM { get; set; }

        private SoXetNghiem_ChiSo_SHTQDTO maChiSo_SHTQ { get; set; }
        private SoXetNghiem_ChiSo_NTVDDTO maChiSo_NTVD { get; set; }
        private SoXetNghiem_ChiSo_KMDTO maChiSo_KM { get; set; }

        #endregion
        public ucBCSoXetNghiem()
        {
            InitializeComponent();
        }

        #region Load
        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDataPhongThucHien();
                LoadMaChiSoXetNghiem();
                gridControlSoViSinh.Visible = false;
                gridControlSoSHTQ.Visible = false;
                gridControlSoNTVD.Visible = false;
                gridControlSoMienDich.Visible = false;
                gridControlSoKhiMau.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDataPhongThucHien()
        {
            try
            {
                //var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 6).OrderBy(o => o.departmentname).ToList();
                //if (lstDSPhong != null && lstDSPhong.Count > 0)
                //{
                //    cboLoaiSoXN.Properties.DataSource = lstDSPhong;
                //    cboLoaiSoXN.Properties.DisplayMember = "departmentname";
                //    cboLoaiSoXN.Properties.ValueMember = "departmentid";
                //}
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "DS_SOXETNGHIEM").ToList();

                cboLoaiSoXN.Properties.DataSource = lstOtherList;
                cboLoaiSoXN.Properties.DisplayMember = "tools_otherlistname";
                cboLoaiSoXN.Properties.ValueMember = "tools_otherlistvalue";
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadMaChiSoXetNghiem()
        {
            try
            {
                //So Sinh hoa thuong quy
                this.maChiSo_SHTQ = new SoXetNghiem_ChiSo_SHTQDTO();
                Workbook workbook = new Workbook(@"Templates\So_XetNghiem_SinhHoaThuongQuy.xlsx");
                Worksheet worksheet = workbook.Worksheets[0];
                DataTable _data_Excel = worksheet.Cells.ExportDataTable(8, 0, 2, worksheet.Cells.MaxDataColumn, true);
                if (_data_Excel != null && _data_Excel.Rows.Count > 0)
                {
                    this.maChiSo_SHTQ.KETQUA_NA1 = _data_Excel.Rows[0]["KETQUA_NA1"].ToString();
                    this.maChiSo_SHTQ.KETQUA_CL1 = _data_Excel.Rows[0]["KETQUA_CL1"].ToString();
                    this.maChiSo_SHTQ.KETQUA_K1 = _data_Excel.Rows[0]["KETQUA_K1"].ToString();
                    this.maChiSo_SHTQ.KETQUA_NA2 = _data_Excel.Rows[0]["KETQUA_NA2"].ToString();
                    this.maChiSo_SHTQ.KETQUA_CL2 = _data_Excel.Rows[0]["KETQUA_CL2"].ToString();
                    this.maChiSo_SHTQ.KETQUA_K2 = _data_Excel.Rows[0]["KETQUA_K2"].ToString();
                }
                //So Nuoc tieu va dich khac
                this.maChiSo_NTVD = new SoXetNghiem_ChiSo_NTVDDTO();
                Workbook workbook_NTVD = new Workbook(@"Templates\So_XetNghiem_NuocTieuVaDichKhac.xlsx");
                Worksheet worksheet__NTVD = workbook_NTVD.Worksheets[0];
                DataTable _data_Excel_NTVD = worksheet__NTVD.Cells.ExportDataTable(8, 0, 2, worksheet__NTVD.Cells.MaxDataColumn, true);
                if (_data_Excel_NTVD != null && _data_Excel_NTVD.Rows.Count > 0)
                {
                    this.maChiSo_NTVD.KETQUA_UBG = _data_Excel_NTVD.Rows[0]["KETQUA_UBG"].ToString();
                    this.maChiSo_NTVD.KETQUA_BIL = _data_Excel_NTVD.Rows[0]["KETQUA_BIL"].ToString();
                    this.maChiSo_NTVD.KETQUA_KET = _data_Excel_NTVD.Rows[0]["KETQUA_KET"].ToString();
                    this.maChiSo_NTVD.KETQUA_PRO = _data_Excel_NTVD.Rows[0]["KETQUA_PRO"].ToString();
                    this.maChiSo_NTVD.KETQUA_NIT = _data_Excel_NTVD.Rows[0]["KETQUA_NIT"].ToString();
                    this.maChiSo_NTVD.KETQUA_LEU = _data_Excel_NTVD.Rows[0]["KETQUA_LEU"].ToString();
                    this.maChiSo_NTVD.KETQUA_GLU = _data_Excel_NTVD.Rows[0]["KETQUA_GLU"].ToString();
                    this.maChiSo_NTVD.KETQUA_SG = _data_Excel_NTVD.Rows[0]["KETQUA_SG"].ToString();
                    this.maChiSo_NTVD.KETQUA_PH = _data_Excel_NTVD.Rows[0]["KETQUA_PH"].ToString();
                    this.maChiSo_NTVD.KETQUA_BLO = _data_Excel_NTVD.Rows[0]["KETQUA_BLO"].ToString();
                }

                //So Khi mau
                this.maChiSo_KM = new SoXetNghiem_ChiSo_KMDTO();
                Workbook workbook_KM = new Workbook(@"Templates\So_XetNghiem_KhiMau.xlsx");
                Worksheet worksheet_KM = workbook_KM.Worksheets[0];
                DataTable _data_Excel_KM = worksheet_KM.Cells.ExportDataTable(8, 0, 2, worksheet_KM.Cells.MaxDataColumn, true);
                if (_data_Excel_KM != null && _data_Excel_KM.Rows.Count > 0)
                {
                    this.maChiSo_KM.KETQUA_FIO2 = _data_Excel_KM.Rows[0]["KETQUA_FIO2"].ToString();
                    this.maChiSo_KM.KETQUA_PTEM = _data_Excel_KM.Rows[0]["KETQUA_PTEM"].ToString();
                    this.maChiSo_KM.KETQUA_AG = _data_Excel_KM.Rows[0]["KETQUA_AG"].ToString();
                    this.maChiSo_KM.KETQUA_AO2 = _data_Excel_KM.Rows[0]["KETQUA_AO2"].ToString();
                    this.maChiSo_KM.KETQUA_HCO = _data_Excel_KM.Rows[0]["KETQUA_HCO"].ToString();
                    this.maChiSo_KM.KETQUA_CTC_B = _data_Excel_KM.Rows[0]["KETQUA_CTC_B"].ToString();
                    this.maChiSo_KM.KETQUA_CTO2 = _data_Excel_KM.Rows[0]["KETQUA_CTO2"].ToString();
                    this.maChiSo_KM.KETQUA_BB = _data_Excel_KM.Rows[0]["KETQUA_BB"].ToString();
                    this.maChiSo_KM.KETQUA_BEE = _data_Excel_KM.Rows[0]["KETQUA_BEE"].ToString();
                    this.maChiSo_KM.KETQUA_BE = _data_Excel_KM.Rows[0]["KETQUA_BE"].ToString();
                    this.maChiSo_KM.KETQUA_CTC_P = _data_Excel_KM.Rows[0]["KETQUA_CTC_P"].ToString();
                    this.maChiSo_KM.KETQUA_CHC = _data_Excel_KM.Rows[0]["KETQUA_CHC"].ToString();
                    this.maChiSo_KM.KETQUA_BARO = _data_Excel_KM.Rows[0]["KETQUA_BARO"].ToString();
                    this.maChiSo_KM.KETQUA_BILI = _data_Excel_KM.Rows[0]["KETQUA_BILI"].ToString();
                    this.maChiSo_KM.KETQUA_HHB = _data_Excel_KM.Rows[0]["KETQUA_HHB"].ToString();
                    this.maChiSo_KM.KETQUA_MET = _data_Excel_KM.Rows[0]["KETQUA_MET"].ToString();
                    this.maChiSo_KM.KETQUA_COH = _data_Excel_KM.Rows[0]["KETQUA_COH"].ToString();
                    this.maChiSo_KM.KETQUA_O2H = _data_Excel_KM.Rows[0]["KETQUA_O2H"].ToString();
                    this.maChiSo_KM.KETQUA_SO2 = _data_Excel_KM.Rows[0]["KETQUA_SO2"].ToString();
                    this.maChiSo_KM.KETQUA_THB = _data_Excel_KM.Rows[0]["KETQUA_THB"].ToString();
                    this.maChiSo_KM.KETQUA_CL = _data_Excel_KM.Rows[0]["KETQUA_CL"].ToString();
                    this.maChiSo_KM.KETQUA_CA2 = _data_Excel_KM.Rows[0]["KETQUA_CA2"].ToString();
                    this.maChiSo_KM.KETQUA_K = _data_Excel_KM.Rows[0]["KETQUA_K"].ToString();
                    this.maChiSo_KM.KETQUA_NA = _data_Excel_KM.Rows[0]["KETQUA_NA"].ToString();
                    this.maChiSo_KM.KETQUA_HCT = _data_Excel_KM.Rows[0]["KETQUA_HCT"].ToString();
                    this.maChiSo_KM.KETQUA_PCO = _data_Excel_KM.Rows[0]["KETQUA_PCO"].ToString();
                    this.maChiSo_KM.KETQUA_PO2 = _data_Excel_KM.Rows[0]["KETQUA_PO2"].ToString();
                    this.maChiSo_KM.KETQUA_PH = _data_Excel_KM.Rows[0]["KETQUA_PH"].ToString();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Xuat file va in
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";
                string fileTemplatePath = "So_XetNghiem_ViSinh.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.BienTrongBaoCao.DEPARTMENTNAME;
                reportitem_kho.value = cboLoaiSoXN.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                DataTable _dataBaoCao = new DataTable();
                if (cboLoaiSoXN.EditValue.ToString() == "SO_VS")
                {
                    fileTemplatePath = "So_XetNghiem_ViSinh.xlsx";
                    _dataBaoCao = this.dataBaoCao_VS;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_SHTQ")
                {
                    fileTemplatePath = "So_XetNghiem_SinhHoaThuongQuy.xlsx";
                    _dataBaoCao = this.dataBaoCao_SHTQ;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_NTVD")
                {
                    fileTemplatePath = "So_XetNghiem_NuocTieuVaDichKhac.xlsx";
                    _dataBaoCao = this.dataBaoCao_NTVD;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_MD")
                {
                    fileTemplatePath = "So_XetNghiem_MienDich.xlsx";
                    _dataBaoCao = this.dataBaoCao_MD;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_KM")
                {
                    fileTemplatePath = "So_XetNghiem_KhiMau.xlsx";
                    _dataBaoCao = this.dataBaoCao_KM;
                }

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";
                string fileTemplatePath = "So_XetNghiem_ViSinh.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.BienTrongBaoCao.DEPARTMENTNAME;
                reportitem_kho.value = cboLoaiSoXN.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                DataTable _dataBaoCao = new DataTable();
                if (cboLoaiSoXN.EditValue.ToString() == "SO_VS")
                {
                    fileTemplatePath = "So_XetNghiem_ViSinh.xlsx";
                    _dataBaoCao = this.dataBaoCao_VS;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_SHTQ")
                {
                    fileTemplatePath = "So_XetNghiem_SinhHoaThuongQuy.xlsx";
                    _dataBaoCao = this.dataBaoCao_SHTQ;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_NTVD")
                {
                    fileTemplatePath = "So_XetNghiem_NuocTieuVaDichKhac.xlsx";
                    _dataBaoCao = this.dataBaoCao_NTVD;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_MD")
                {
                    fileTemplatePath = "So_XetNghiem_MienDich.xlsx";
                    _dataBaoCao = this.dataBaoCao_MD;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_KM")
                {
                    fileTemplatePath = "So_XetNghiem_KhiMau.xlsx";
                    _dataBaoCao = this.dataBaoCao_KM;
                }

                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Custom
        private void cboPhongThucHien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiSoXN.EditValue.ToString() == "SO_VS")
                {
                    gridControlSoViSinh.Visible = true;
                    gridControlSoSHTQ.Visible = false;
                    gridControlSoNTVD.Visible = false;
                    gridControlSoMienDich.Visible = false;
                    gridControlSoKhiMau.Visible = false;

                    gridControlSoViSinh.Dock = DockStyle.Fill;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_SHTQ")
                {
                    gridControlSoViSinh.Visible = false;
                    gridControlSoSHTQ.Visible = true;
                    gridControlSoNTVD.Visible = false;
                    gridControlSoMienDich.Visible = false;
                    gridControlSoKhiMau.Visible = false;

                    gridControlSoSHTQ.Dock = DockStyle.Fill;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_NTVD")
                {
                    gridControlSoViSinh.Visible = false;
                    gridControlSoSHTQ.Visible = false;
                    gridControlSoNTVD.Visible = true;
                    gridControlSoMienDich.Visible = false;
                    gridControlSoKhiMau.Visible = false;

                    gridControlSoNTVD.Dock = DockStyle.Fill;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_MD")
                {
                    gridControlSoViSinh.Visible = false;
                    gridControlSoSHTQ.Visible = false;
                    gridControlSoNTVD.Visible = false;
                    gridControlSoMienDich.Visible = true;
                    gridControlSoKhiMau.Visible = false;

                    gridControlSoMienDich.Dock = DockStyle.Fill;
                }
                else if (cboLoaiSoXN.EditValue.ToString() == "SO_KM")
                {
                    gridControlSoViSinh.Visible = false;
                    gridControlSoSHTQ.Visible = false;
                    gridControlSoNTVD.Visible = false;
                    gridControlSoMienDich.Visible = false;
                    gridControlSoKhiMau.Visible = true;

                    gridControlSoKhiMau.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboLoaiSoXN.EditValue == null)
            {
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_SO_XET_NGHIEM);
                frmthongbao.Show();
                return;
            }
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

            string tieuchi_mbp = "";
            string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            if (cboTieuChi.Text == "Theo ngày chỉ định")
            {
                tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
            }
            else if (cboTieuChi.Text == "Theo ngày trả kết quả")
            {
                tieuchi_mbp = " and maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
            }

            if (cboLoaiSoXN.EditValue.ToString() == "SO_VS")
            {
                long _tools_otherlistid = GlobalStore.lstOtherList_Global.Where(o => o.tools_otherlistcode == "SO_VS").FirstOrDefault().tools_otherlistid;
                LayDuLieuSo_ViSinh(tieuchi_mbp, _tools_otherlistid);
            }
            else if (cboLoaiSoXN.EditValue.ToString() == "SO_SHTQ")
            {
                long _tools_otherlistid = GlobalStore.lstOtherList_Global.Where(o => o.tools_otherlistcode == "SO_SHTQ").FirstOrDefault().tools_otherlistid;
                LayDuLieuSo_SinhHoaThuongQuy(tieuchi_mbp, _tools_otherlistid);
            }
            else if (cboLoaiSoXN.EditValue.ToString() == "SO_NTVD")
            {
                long _tools_otherlistid = GlobalStore.lstOtherList_Global.Where(o => o.tools_otherlistcode == "SO_NTVD").FirstOrDefault().tools_otherlistid;
                LayDuLieuSo_NuocTieuVaDichKhac(tieuchi_mbp, _tools_otherlistid);
            }
            else if (cboLoaiSoXN.EditValue.ToString() == "SO_MD")
            {
                long _tools_otherlistid = GlobalStore.lstOtherList_Global.Where(o => o.tools_otherlistcode == "SO_MD").FirstOrDefault().tools_otherlistid;
                LayDuLieuSo_MienDich(tieuchi_mbp, _tools_otherlistid);
            }
            else if (cboLoaiSoXN.EditValue.ToString() == "SO_KM")
            {
                long _tools_otherlistid = GlobalStore.lstOtherList_Global.Where(o => o.tools_otherlistcode == "SO_KM").FirstOrDefault().tools_otherlistid;
                LayDuLieuSo_KhiMau(tieuchi_mbp, _tools_otherlistid);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion


    }
}
