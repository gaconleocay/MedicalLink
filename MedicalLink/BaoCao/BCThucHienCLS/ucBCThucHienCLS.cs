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

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucBCThucHienCLS : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        DataTable dataBCPTTT { get; set; }
        bool kiemtrasuadulieu = false;
        private DataTable dataNguoiThucHien { get; set; }
        #endregion

        #region Load
        public ucBCThucHienCLS()
        {
            InitializeComponent();
        }

        private void ucBCThucHienCLS_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucPhongThucHien();
            LoadDanhSachExport();
            LoadDanhSachBaoCao();
        }

        private void LoadDanhMucPhongThucHien()
        {
            try
            {
                var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7 || o.departmenttype == 6).OrderBy(o => o.departmentname).ToList();
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                    chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                    chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                }

                chkcomboListDSPhong.CheckAll();
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
                menu.Items.Add(new DXMenuItem("Báo cáo Cận lâm sàng"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền cận lâm sàng"));
                menu.Items.Add(new DXMenuItem("Báo cáo Cận lâm sàng - Theo filter trên lưới"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền cận lâm sàng - Theo filter trên lưới"));
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
        private void LoadDanhSachBaoCao()
        {
            try
            {
                List<ClassCommon.classPermission> lstBaoCaoCLS = new List<classPermission>();
                List<ClassCommon.classPermission> kiemtra = Base.SessionLogin.SessionLstPhanQuyen_BaoCao.Where(o => o.permissioncode == "BAOCAO_009").ToList(); ;
                if (kiemtra != null && kiemtra.Count > 0)
                {
                    lstBaoCaoCLS.AddRange(kiemtra);
                }
                ClassCommon.classPermission baocaoCLS = new classPermission();
                baocaoCLS.permissioncode = "ALLL";
                baocaoCLS.permissionname = "Tất cả";
                lstBaoCaoCLS.Add(baocaoCLS);

                cboLoaiBaoCao.Properties.DataSource = lstBaoCaoCLS;
                cboLoaiBaoCao.Properties.DisplayMember = "permissionname";
                cboLoaiBaoCao.Properties.ValueMember = "permissioncode";
                cboLoaiBaoCao.EditValue = "ALLL";
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
                if (chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_THUC_HIEN);
                    frmthongbao.Show();
                    return;
                }
                gridControlDataBCPTTT.DataSource = null;
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
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

        private void bandedGridViewDataBCPTTT_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                kiemtrasuadulieu = true;
                var rowHandle = bandedGridViewDataBCPTTT.FocusedRowHandle;
                long thuchienclsid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "thuchienclsid").ToString());
                long medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                long maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                long patientid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "patientid").ToString());
                long servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                string thuchienclsdate = Utilities.Util_TypeConvertParse.ToDateTime(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "ngay_thuchien").ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                long mochinh_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "mochinh_idbs").ToString());
                long gayme_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "gayme_idbs").ToString());
                long phu1_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "phu1_idbs").ToString());
                long phu2_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "phu2_idbs").ToString());
                long giupviec1_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "giupviec1_idbs").ToString());
                long giupviec2_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "giupviec2_idbs").ToString());

                if (thuchienclsid == 0) //kiemtra xem co ban ghi nao hay ko?
                {
                    string sqlkiemtra = "select thuchienclsid from thuchiencls where servicepriceid=" + servicepriceid + ";";
                    DataTable dataKiemTra = condb.GetDataTable_HIS(sqlkiemtra);
                    if (dataKiemTra != null && dataKiemTra.Rows.Count > 0)
                    {
                        thuchienclsid = Utilities.Util_TypeConvertParse.ToInt64(dataKiemTra.Rows[0]["thuchienclsid"].ToString());
                    }
                }

                string luulaithuchien = "";
                if (thuchienclsid == 0) //them moi
                {
                    luulaithuchien = "INSERT INTO thuchiencls(medicalrecordid, medicalrecordid_gmhs, patientid, maubenhphamid, servicepriceid, thuchienclsdate, phauthuatvien, bacsigayme, phumo1, phumo2, phumo3, phumo4, tools_userid, tools_username) VALUES ('" + medicalrecordid + "', '" + medicalrecordid + "', '" + patientid + "', '" + maubenhphamid + "', '" + servicepriceid + "', '" + thuchienclsdate + "', '" + mochinh_idbs + "', '" + gayme_idbs + "', '" + phu1_idbs + "', '" + phu2_idbs + "', '" + giupviec1_idbs + "', '" + giupviec2_idbs + "', '"+ SessionLogin.SessionUserID + "', '"+ SessionLogin.SessionUsername + "');";
                }
                else
                {
                    luulaithuchien = "UPDATE thuchiencls SET thuchienclsdate='" + thuchienclsdate + "', phauthuatvien='" + mochinh_idbs + "',  bacsigayme = '" + gayme_idbs + "', phumo1 = '" + phu1_idbs + "', phumo2 = '" + phu2_idbs + "', phumo3 = '" + giupviec1_idbs + "', phumo4 = '" + giupviec2_idbs + "', tools_userid='" + SessionLogin.SessionUserID + "', tools_username='" + SessionLogin.SessionUsername + "' WHERE thuchienclsid = " + thuchienclsid + "; ";
                }

                condb.ExecuteNonQuery_HIS(luulaithuchien);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #region Xuat bao cao
        void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.kiemtrasuadulieu)
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi();
                }
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Báo cáo Cận lâm sàng")
                {
                    tbnExportBCCLS_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền cận lâm sàng")
                {
                    tbnExportBCThanhToanCLS_Click();
                }
                else if (tenbaocao == "Báo cáo Cận lâm sàng - Theo filter trên lưới")
                {
                    tbnExportBCCLSTheoFilter_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền cận lâm sàng - Theo filter trên lưới")
                {
                    tbnExportBCThanhToanCLSTheoFilter_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void tbnExportBCCLS_Click()
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCThanhToanCLS_Click()
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanCLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCCLSTheoFilter_Click()
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                }
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCThanhToanCLSTheoFilter_Click()
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanCLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                }
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

        private void cboLoaiBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009") //bao cao Noi soi da day
                {
                    chkcomboListDSPhong.Enabled = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = false;
                    gridBand_phumo2.Visible = false;
                    //gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    bandedGridColumn_gv1_tien.Visible = false;
                    bandedGridColumn_gv1nsdd_tien.Visible = true;
                }
                else
                {
                    chkcomboListDSPhong.Enabled = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    //gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    bandedGridColumn_gv1_tien.Visible = true;
                    bandedGridColumn_gv1nsdd_tien.Visible = false;

                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
    }
}
