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
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhMucKhoa();
                LoadDanhSachBaoCao();
                LoadDanhSachExport();
                LoadDanhSachInAn();
                LoadDanhSachCauHinhBaoCao();
                LoadControlDuyetPTT();
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
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
        private void LoadControlDuyetPTT()
        {
            try
            {
                bool _enable = false;
                if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_05"))
                {
                    _enable = true;
                }
                if (CheckPermission.ChkPerModule("SYS_05"))
                {
                    btnPTTT_Gui.Visible = true;
                    btnPTTT_HuyGui.Visible = true;
                }
                else
                {
                    btnPTTT_Gui.Visible = !_enable;
                    btnPTTT_HuyGui.Visible = !_enable;
                }
                btnPTTT_TiepNhan.Visible = _enable;
                btnPTTT_HuyTiepNhan.Visible = _enable;
                btnPTTT_Duyet.Visible = _enable;
                btnPTTT_HuyDuyet.Visible = _enable;
                btnPTTT_KhoaMoKhoa.Visible = _enable;

                if (KiemTraTrangThaiKhoaGuiPTTT() == 1)
                {
                    btnPTTT_KhoaMoKhoa.Text = "Mở khóa gửi YC";
                }
                else
                {
                    btnPTTT_KhoaMoKhoa.Text = "Khóa gửi YC";
                }
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
                if (cboLoaiBaoCao.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_LOAI_BAO_CAO);
                    frmthongbao.Show();
                    return;
                }
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005" || cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")
                {
                    if (chkcomboListDSKhoa.EditValue == null)
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

        #endregion

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
                chkcomboListDSKhoa.EditValue = null;

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    chkcomboListDSKhoa.Enabled = false;
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
                    chkcomboListDSKhoa.Enabled = false;
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
                    chkcomboListDSKhoa.Enabled = false;
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
                    chkcomboListDSKhoa.Enabled = false;
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
                    chkcomboListDSKhoa.Enabled = true;
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
                    chkcomboListDSKhoa.Enabled = false;
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
                    chkcomboListDSKhoa.Enabled = false;
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
                    chkcomboListDSKhoa.Enabled = true;
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
                    chkcomboListDSKhoa.Enabled = false;
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
                if (chkcomboListDSKhoa.EditValue != null && chkcomboListDSKhoa.EditValue.ToString() != "")
                {
                    //Load danh muc phong thuoc khoa
                    List<ClassCommon.classUserDepartment> lstDSPhong = new List<classUserDepartment>();
                    string[] dsKhoa_temp = chkcomboListDSKhoa.EditValue.ToString().Split(',');
                    for (int i = 0; i < dsKhoa_temp.Length; i++)
                    {
                        lstDSPhong.AddRange(Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt64(dsKhoa_temp[i])).OrderBy(o => o.departmentname).ToList());
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
                    else if (val == "2")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[3]);
                        e.Graphics.DrawImage(imMenu.Images[3], pos);
                    }
                    else if (val == "3")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[4]);
                        e.Graphics.DrawImage(imMenu.Images[4], pos);
                    }
                    //else if (val == "99")
                    //{
                    //    e.Handled = true;
                    //    Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[5]);
                    //    e.Graphics.DrawImage(imMenu.Images[5], pos);
                    //}
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
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
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
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
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
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
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
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
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
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
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
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
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

        #region Menu popup
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
                        DXMenuItem item_GoDuyetPTTTChon = new DXMenuItem("Gỡ duyệt PTTT đã chọn");
                        item_GoDuyetPTTTChon.Image = imMenu.Images[1];
                        item_GoDuyetPTTTChon.Click += new EventHandler(GoDuyetPTTTDaChon_Click);
                        e.Menu.Items.Add(item_GoDuyetPTTTChon);
                        item_GoDuyetPTTTChon.BeginGroup = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Duyet PTTT
        private void btnPTTT_Gui_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraTrangThaiKhoaGuiPTTT() == 1)
                {
                    MessageBox.Show(Base.ThongBaoLable.DA_KHOA_YC_GUI_PTTT, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstChuaGui_PTTT = lstServicepriceids.Where(o => o.duyetpttt_stt == 0).ToList();
                    if (_lstChuaGui_PTTT != null && _lstChuaGui_PTTT.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=1 WHERE servicepriceid in (" + ConvertListObjToListString(_lstChuaGui_PTTT) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Gửi yêu cầu duyệt PTTT thành công SL=" + _lstChuaGui_PTTT.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CapNhatTools_DuyetPTTT(lstServicepriceids, 1);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_DA_DUOC_GUI_YC_DUYET_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void btnPTTT_HuyGui_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraTrangThaiKhoaGuiPTTT() == 1)
                {
                    MessageBox.Show(Base.ThongBaoLable.DA_KHOA_YC_GUI_PTTT, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstChuaGui_PTTT = lstServicepriceids.Where(o => o.duyetpttt_stt == 1).ToList();
                    if (_lstChuaGui_PTTT != null && _lstChuaGui_PTTT.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=0 WHERE servicepriceid in (" + ConvertListObjToListString(_lstChuaGui_PTTT) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Hủy gửi yêu cầu duyệt PTTT thành công SL=" + _lstChuaGui_PTTT.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CapNhatTools_DuyetPTTT(lstServicepriceids, 0);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DANG_GUI_YC_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void btnPTTT_TiepNhan_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstChuaGui_PTTT = lstServicepriceids.Where(o => o.duyetpttt_stt == 1).ToList();
                    if (_lstChuaGui_PTTT != null && _lstChuaGui_PTTT.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=2 WHERE servicepriceid in (" + ConvertListObjToListString(_lstChuaGui_PTTT) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Tiếp nhận yêu cầu duyệt PTTT thành công SL=" + _lstChuaGui_PTTT.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CapNhatTools_DuyetPTTT(lstServicepriceids, 2);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DANG_GUI_YC_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void btnPTTT_HuyTiepNhan_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstChuaGui_PTTT = lstServicepriceids.Where(o => o.duyetpttt_stt == 2).ToList();
                    if (_lstChuaGui_PTTT != null && _lstChuaGui_PTTT.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=1 WHERE servicepriceid in (" + ConvertListObjToListString(_lstChuaGui_PTTT) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Hủy tiếp nhận yêu cầu duyệt PTTT thành công SL=" + _lstChuaGui_PTTT.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CapNhatTools_DuyetPTTT(lstServicepriceids, 1);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DA_TIEP_NHAN_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void btnPTTT_Duyet_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstChuaGui_PTTT = lstServicepriceids.Where(o => o.duyetpttt_stt == 2).ToList();
                    if (_lstChuaGui_PTTT != null && _lstChuaGui_PTTT.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=3, duyetpttt_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstChuaGui_PTTT) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Duyệt PTTT thành công SL=" + _lstChuaGui_PTTT.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CapNhatTools_DuyetPTTT(lstServicepriceids, 3);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DA_TIEP_NHAN_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void btnPTTT_HuyDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstChuaGui_PTTT = lstServicepriceids.Where(o => o.duyetpttt_stt == 3).ToList();
                    if (_lstChuaGui_PTTT != null && _lstChuaGui_PTTT.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=2 WHERE servicepriceid in (" + ConvertListObjToListString(_lstChuaGui_PTTT) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Hủy duyệt PTTT thành công SL=" + _lstChuaGui_PTTT.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CapNhatTools_DuyetPTTT(lstServicepriceids, 2);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DA_DUYET_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void btnPTTT_Khoa_Click(object sender, EventArgs e)
        {
            try
            {
                string _toolsoptionvalue = "1";
                string _btnPTTT_KhoaMoKhoaText = "Mở khóa gửi YC";
                if (btnPTTT_KhoaMoKhoa.Text == "Mở khóa gửi YC")
                {
                    _toolsoptionvalue = "0";
                    _btnPTTT_KhoaMoKhoaText = "Khóa gửi YC";
                }
                string _updateKhoa = "UPDATE tools_option SET toolsoptionvalue='" + _toolsoptionvalue + "' WHERE toolsoptioncode='REPORT_08_KhoaGuiYeuCau';";
                if (condb.ExecuteNonQuery_MeL(_updateKhoa))
                {
                    MessageBox.Show(btnPTTT_KhoaMoKhoa.Text + " PTTT thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPTTT_KhoaMoKhoa.Text = _btnPTTT_KhoaMoKhoaText;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion




    }
}
