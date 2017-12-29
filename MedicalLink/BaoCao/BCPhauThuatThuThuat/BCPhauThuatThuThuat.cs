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
using MedicalLink.Utilities.GridControl;
using MedicalLink.Utilities.BandGridView;
using MedicalLink.Utilities.GUIGridView;

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BCPhauThuatThuThuat : UserControl
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBCPTTT { get; set; }
        private List<ClassCommon.ToolsOtherListDTO> lstOtherList { get; set; }
        private Utilities.BandGridView.GridCheckMarksSelection helper;

        #endregion

        #region Load
        public BCPhauThuatThuThuat()
        {
            InitializeComponent();
            helper = new GridCheckMarksSelection(bandedGridViewDataBCPTTT);
        }

        private void BCPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucKhoa();
            LoadDanhSachBaoCao();
            LoadDanhSachExport();
            LoadDanhSachInAn();
            LoadDanhSachCauHinhBaoCao();
        }

        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
                List<ClassCommon.classPermission> lstBaoCaoPTTT = Base.SessionLogin.SessionLstPhanQuyen_BaoCao.Where(o => o.permissioncode != "BAOCAO_009").ToList(); ;

                cboLoaiBaoCao.Properties.DataSource = lstBaoCaoPTTT;
                cboLoaiBaoCao.Properties.DisplayMember = "permissionname";
                cboLoaiBaoCao.Properties.ValueMember = "permissioncode";

                if (Base.SessionLogin.SessionLstPhanQuyen_BaoCao.Count > 0)
                {
                    cboLoaiBaoCao.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void LoadDanhSachExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo phẫu thuật thủ thuật"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền phẫu thuật thủ thuật"));
                menu.Items.Add(new DXMenuItem("Báo cáo phẫu thuật thủ thuật - Theo filter trên lưới"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền phẫu thuật thủ thuật - Theo filter trên lưới"));
                // ... add more items
                dropDownExport.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
                // setup initial selection
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachInAn()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo phẫu thuật thủ thuật"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền phẫu thuật thủ thuật"));
                // ... add more items
                dropDownPrint.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_InAn_Click;
                // setup initial selection
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachCauHinhBaoCao()
        {
            try
            {
                if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                {
                    this.lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_08_LOAIBC").ToList();
                }
                else
                {
                    string sqlNhomDV = "select o.tools_otherlistcode, o.tools_otherlistname,o.tools_otherlistvalue from tools_othertypelist ot inner join tools_otherlist o on o.tools_othertypelistid=ot.tools_othertypelistid where ot.tools_othertypelistcode='REPORT_08_LOAIBC'; ";
                    DataTable dataLoaiBaoCao = condb.GetDataTable_MeL(sqlNhomDV);
                    if (dataLoaiBaoCao != null && dataLoaiBaoCao.Rows.Count > 0)
                    {
                        this.lstOtherList = new List<ToolsOtherListDTO>();
                        for (int i = 0; i < dataLoaiBaoCao.Rows.Count; i++)
                        {
                            ClassCommon.ToolsOtherListDTO otherList = new ToolsOtherListDTO();
                            otherList.tools_otherlistcode = dataLoaiBaoCao.Rows[i]["tools_otherlistcode"].ToString();
                            otherList.tools_otherlistname = dataLoaiBaoCao.Rows[i]["tools_otherlistname"].ToString();
                            otherList.tools_otherlistvalue = dataLoaiBaoCao.Rows[i]["tools_otherlistvalue"].ToString();
                            this.lstOtherList.Add(otherList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiBaoCao.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_LOAI_BAO_CAO);
                    frmthongbao.Show();
                    return;
                }
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005" || cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")
                {
                    if (cboKhoa.EditValue == null)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                    if (chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count == 0)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                }
                gridControlDataBCPTTT.DataSource = null;
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #region Custom
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
        private void cboLoaiBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                cboKhoa.EditValue = null;

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac    ------
                {
                    cboKhoa.Enabled = true;
                    chkcomboListDSPhong.Enabled = true;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = false;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac    ---------
                {
                    cboKhoa.Enabled = true;
                    chkcomboListDSPhong.Enabled = true;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day (mo chinh + giup viec1)
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = false;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void cboKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListDSPhong.Properties.Items.Clear();
                if (cboKhoa.EditValue != null)
                {
                    //Load danh muc phong thuoc khoa
                    var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt64(cboKhoa.EditValue.ToString())).OrderBy(o => o.departmentname).ToList();
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
        private void bandedGridViewDataBCPTTT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "img_duyetstt")
                {
                    string val = bandedGridViewDataBCPTTT.GetRowCellValue(e.RowHandle, "duyetpttt_stt").ToString();
                    if (val == "1")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[2]);
                        e.Graphics.DrawImage(imMenu.Images[2], pos);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Xuat bao cao
        private void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Báo cáo phẫu thuật thủ thuật")
                {
                    tbnExportBCPTTT_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền phẫu thuật thủ thuật")
                {
                    tbnExportBCThanhToanPTTT_Click();
                }
                else if (tenbaocao == "Báo cáo phẫu thuật thủ thuật - Theo filter trên lưới")
                {
                    tbnExportBCPTTTTheoFilter_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền phẫu thuật thủ thuật - Theo filter trên lưới")
                {
                    tbnExportBCThanhToanPTTTTheoFilter_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void tbnExportBCPTTT_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_GMHT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TMH.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_RHM.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_KHOAKHAC.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_CHUNG.xlsx";
                }
                //else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                //{
                //    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                //}
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCThanhToanPTTT_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_RHM_ThanhToan.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_MAT_ThanhToan.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                //else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                //{
                //    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                //}
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCPTTTTheoFilter_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_GMHT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TMH.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_RHM.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_KHOAKHAC.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_CHUNG.xlsx";
                }
                //else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                //{
                //    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                //}
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void tbnExportBCThanhToanPTTTTheoFilter_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                //else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                //{
                //    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                //}
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region In An
        private void Item_InAn_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Báo cáo phẫu thuật thủ thuật")
                {
                    tbnInAnBCPTTT_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền phẫu thuật thủ thuật")
                {
                    tbnInAnBCThanhToanPTTT_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void tbnInAnBCPTTT_Click()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_GMHT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TMH.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_RHM.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_KHOAKHAC.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_CHUNG.xlsx";
                }
                //else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                //{
                //    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                //}
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void tbnInAnBCThanhToanPTTT_Click()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_RHM_ThanhToan.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_MAT_ThanhToan.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanPhauThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanThuThuat.xlsx";
                }
                //else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                //{
                //    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                //}
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        private void bandedGridViewDataBCPTTT_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_05"))
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        e.Menu.Items.Clear();
                        DXMenuItem item_DuyetPTTTChon = new DXMenuItem("Duyệt PTTT đã chọn");
                        item_DuyetPTTTChon.Image = imMenu.Images[0];
                        item_DuyetPTTTChon.Click += new EventHandler(DuyetPTTTDaChon_Click);
                        e.Menu.Items.Add(item_DuyetPTTTChon);
                        //DXMenuItem item_DuyetAll = new DXMenuItem("Duyệt tất cả PTTT");
                        //item_DuyetAll.Image = imMenu.Images[0];
                        //item_DuyetAll.Click += new EventHandler(DuyetTatCaPTTT_Click);
                        //e.Menu.Items.Add(item_DuyetAll);
                        DXMenuItem item_GoDuyetPTTTChon = new DXMenuItem("Gỡ duyệt PTTT đã chọn");
                        item_GoDuyetPTTTChon.Image = imMenu.Images[1];
                        item_GoDuyetPTTTChon.Click += new EventHandler(GoDuyetPTTTDaChon_Click);
                        e.Menu.Items.Add(item_GoDuyetPTTTChon);
                        item_GoDuyetPTTTChon.BeginGroup = true;
                        //DXMenuItem item_GoDuyetAll = new DXMenuItem("Gỡ duyệt tất cả PTTT");
                        //item_GoDuyetAll.Image = imMenu.Images[1];
                        //item_GoDuyetAll.Click += new EventHandler(GoDuyetTatCaPTTT_Click);
                        //e.Menu.Items.Add(item_GoDuyetAll);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }


    }
}
