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
using MedicalLink.BaoCao.BCPhauThuatThuThuat;

namespace MedicalLink.BaoCao
{
    public partial class ucBC48_PhauThuatThuThuatChiTiet : UserControl
    {
        #region Declaration
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private List<ClassCommon.ToolsOtherListDTO> lstOtherList { get; set; }

        #endregion

        #region Load
        public ucBC48_PhauThuatThuThuatChiTiet()
        {
            InitializeComponent();
        }

        private void BCPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuMacDinh();
                LoadDanhMucKhoa();
                LoadDanhSachBaoCao();
                LoadDanhSachExport();
                LoadDanhSachInAn();
                LoadDanhSachCauHinhBaoCao();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //gridBand_ThuocChiTietTrongGoi.Visible = false;
                //gridBand_VatTuChiTietTrongGoi.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachBaoCao()
        {
            try
            {
                List<ClassCommon.classPermission> lstBaoCaoPTTT = Base.SessionLogin.LstPhanQuyen_BaoCaoIn.Where(o => o.permissioncode != "BAOCAO_009").ToList(); ;

                cboLoaiBaoCao.Properties.DataSource = lstBaoCaoPTTT;
                cboLoaiBaoCao.Properties.DisplayMember = "permissionname";
                cboLoaiBaoCao.Properties.ValueMember = "permissioncode";

                if (Base.SessionLogin.LstPhanQuyen_BaoCaoIn.Count > 0)
                {
                    cboLoaiBaoCao.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void LoadDanhSachExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo phẫu thuật thủ thuật"));
                //menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền phẫu thuật thủ thuật"));
                dropDownExport.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachInAn()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo phẫu thuật thủ thuật"));
                //menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền phẫu thuật thủ thuật"));
                dropDownPrint.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_InAn_Click;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
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
                O2S_Common.Logging.LogSystem.Error(ex);
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
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_LOAI_BAO_CAO);
                    frmthongbao.Show();
                    return;
                }
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005" || cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")
                {
                    if (chkcomboListDSKhoa.EditValue == null)
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                    if (chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count == 0)
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                }
                gridControlDataBCPTTT.DataSource = null;
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
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
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    chkcomboListDSKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    chkcomboListDSKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    chkcomboListDSKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac    ------
                {
                    chkcomboListDSKhoa.Enabled = true;
                    chkcomboListDSPhong.Enabled = true;
                    bandedGridColumn_tyle.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    chkcomboListDSKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    chkcomboListDSKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac    ---------
                {
                    chkcomboListDSKhoa.Enabled = true;
                    chkcomboListDSPhong.Enabled = true;
                    bandedGridColumn_tyle.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day (mo chinh + giup viec1)
                {
                    chkcomboListDSKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
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
                        lstDSPhong.AddRange(Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.TypeConvertParse.ToInt64(dsKhoa_temp[i])).OrderBy(o => o.departmentname).ToList());
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
                O2S_Common.Logging.LogSystem.Error(ex);
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
                    tbnExportBCPTTTTheoFilter_Click();
                }
                //else if (tenbaocao == "Báo cáo thanh toán tiền phẫu thuật thủ thuật")
                //{
                //    tbnExportBCThanhToanPTTTTheoFilter_Click();
                //}
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_48_PhauThuatThuThuat_ChiTiet_CHUNG.xlsx";
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        //private void tbnExportBCThanhToanPTTTTheoFilter_Click()
        //{
        //    try
        //    {
        //        string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
        //        string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

        //        List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
        //        ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
        //        reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
        //        reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
        //        thongTinThem.Add(reportitem);
        //        ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
        //        reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
        //        reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
        //        thongTinThem.Add(reportitem_khoa);

        //        string fileTemplatePath = "BC_48_PhauThuatThuThuat_ChiTiet_CHUNG.xlsx";

        //        DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
        //        Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
        //        export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Error(ex);
        //    }
        //}

        #endregion

        #region In An
        private void Item_InAn_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Báo cáo phẫu thuật thủ thuật")
                {
                    tbnInAnBCPTTT_Filter_Click();
                }
                //else if (tenbaocao == "Báo cáo thanh toán tiền phẫu thuật thủ thuật")
                //{
                //    tbnInAnBCThanhToanPTTT_Filter_Click();
                //}
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void tbnInAnBCPTTT_Filter_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_48_PhauThuatThuThuat_ChiTiet_CHUNG.xlsx";
                
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        //private void tbnInAnBCThanhToanPTTT_Filter_Click()
        //{
        //    try
        //    {
        //        string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
        //        string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

        //        List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
        //        ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
        //        reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
        //        reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
        //        thongTinThem.Add(reportitem);
        //        ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
        //        reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
        //        reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
        //        thongTinThem.Add(reportitem_khoa);

        //        string fileTemplatePath = "BC_48_PhauThuatThuThuat_ChiTiet_CHUNG.xlsx";
        
        //        DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
        //        Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Error(ex);
        //    }
        //}
        #endregion

    }
}
