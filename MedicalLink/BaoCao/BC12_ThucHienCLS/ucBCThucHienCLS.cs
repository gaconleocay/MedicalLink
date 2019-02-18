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
using MedicalLink.Utilities.GUIGridView;
using MedicalLink.Utilities.BandGridView;
using MedicalLink.BaoCao.BCThucHienCLS;

namespace MedicalLink.BaoCao
{
    public partial class ucBCThucHienCLS : UserControl
    {
        #region Declaration
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        DataTable dataBCPTTT { get; set; }
        private bool kiemtrasuadulieu = false;
        private DataTable dataNguoiThucHien { get; set; }
        private Utilities.BandGridView.GridCheckMarksSelection helper;
        private bool _duyetPTTT = false;
        private string ThoiGianGioiHanDuLieu { get; set; }

        #endregion

        #region Load
        public ucBCThucHienCLS()
        {
            InitializeComponent();
            helper = new GridCheckMarksSelection(bandedGridViewDataBCPTTT);
        }

        private void ucBCThucHienCLS_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            //LoadDanhMucPhongThucHien();
            LoadDanhSachExport();
            LoadDanhSachButonPrint();
            LoadDanhSachBaoCao();
            //KiemTraQuyenDuyetPTTT();
            LoadControlDuyetPTT();
            LoadThoiGianGioiHanDuLieu();
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
                dropDownExport.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
                // setup initial selection
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachButonPrint()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo Cận lâm sàng"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền cận lâm sàng"));
                menu.Items.Add(new DXMenuItem("Báo cáo Cận lâm sàng - Theo filter trên lưới"));
                menu.Items.Add(new DXMenuItem("Báo cáo thanh toán tiền cận lâm sàng - Theo filter trên lưới"));
                dropDownPrint.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Print_Click;
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
                List<ClassCommon.classPermission> lstBaoCaoCLS = new List<classPermission>();
                List<ClassCommon.classPermission> kiemtra = Base.SessionLogin.LstPhanQuyen_BaoCaoIn.Where(o => o.permissioncode == "BAOCAO_009").ToList(); ;
                if (kiemtra != null && kiemtra.Count > 0)
                {
                    lstBaoCaoCLS.AddRange(kiemtra);
                }
                ClassCommon.classPermission baocaoCLSXN = new classPermission();
                baocaoCLSXN.permissioncode = "XN";
                baocaoCLSXN.permissionname = "Xét nghiệm";
                lstBaoCaoCLS.Add(baocaoCLSXN);
                ClassCommon.classPermission baocaoCLSCDHA = new classPermission();
                baocaoCLSCDHA.permissioncode = "CDHA";
                baocaoCLSCDHA.permissionname = "Chẩn đoán hình ảnh";
                lstBaoCaoCLS.Add(baocaoCLSCDHA);

                cboLoaiBaoCao.Properties.DataSource = lstBaoCaoCLS;
                cboLoaiBaoCao.Properties.DisplayMember = "permissionname";
                cboLoaiBaoCao.Properties.ValueMember = "permissioncode";
                cboLoaiBaoCao.EditValue = "XN";
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        //private void KiemTraQuyenDuyetPTTT()
        //{
        //    try
        //    {
        //        if (!MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_05"))
        //        {
        //            helper.CheckMarkColumn.ColumnEdit.ReadOnly = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Error(ex);
        //    }
        //}
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
                //if (KiemTraTrangThaiKhoaGuiPTTT() == 1)
                //{
                //    btnPTTT_KhoaMoKhoa.Text = "Mở khóa gửi YC";
                //}
                //else
                //{
                //    btnPTTT_KhoaMoKhoa.Text = "Khóa gửi YC";
                //}
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadThoiGianGioiHanDuLieu()
        {
            try
            {
                string _sqlDMDichVu = "select toolsoptionvalue from tools_option where toolsoptioncode='REPORT_12_TGLayDuLieu';";
                DataTable _dataTG = condb.GetDataTable_MeL(_sqlDMDichVu);
                if (_dataTG.Rows.Count > 0)
                {
                    this.ThoiGianGioiHanDuLieu = _dataTG.Rows[0]["toolsoptionvalue"].ToString();
                }
                else
                {
                    this.ThoiGianGioiHanDuLieu = "" + this.ThoiGianGioiHanDuLieu + "";
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiBaoCao.EditValue.ToString() != "BAOCAO_009" && chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count == 0)
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_THUC_HIEN);
                    frmthongbao.Show();
                    return;
                }
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void bandedGridViewDataBCPTTT_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (!this._duyetPTTT && helper.SelectedCount == 0)
                {
                    kiemtrasuadulieu = true;
                    var rowHandle = bandedGridViewDataBCPTTT.FocusedRowHandle;
                    long thuchienclsid = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "thuchienclsid").ToString());
                    long medicalrecordid = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                    long maubenhphamid = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                    long patientid = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "patientid").ToString());
                    long servicepriceid = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                    string thuchienclsdate = Utilities.TypeConvertParse.ToDateTime(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "ngay_thuchien").ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                    long mochinh_idbs = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "mochinh_idbs").ToString());
                    long gayme_idbs = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "gayme_idbs").ToString());
                    long phu1_idbs = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "phu1_idbs").ToString());
                    long phu2_idbs = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "phu2_idbs").ToString());
                    long giupviec1_idbs = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "giupviec1_idbs").ToString());
                    long giupviec2_idbs = Utilities.TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "giupviec2_idbs").ToString());

                    if (thuchienclsid == 0) //kiemtra xem co ban ghi nao hay ko?
                    {
                        string sqlkiemtra = "select thuchienclsid from thuchiencls where servicepriceid=" + servicepriceid + ";";
                        DataTable dataKiemTra = condb.GetDataTable_HIS(sqlkiemtra);
                        if (dataKiemTra != null && dataKiemTra.Rows.Count > 0)
                        {
                            thuchienclsid = Utilities.TypeConvertParse.ToInt64(dataKiemTra.Rows[0]["thuchienclsid"].ToString());
                        }
                    }

                    string luulaithuchien = "";
                    if (thuchienclsid == 0) //them moi
                    {
                        luulaithuchien = "INSERT INTO thuchiencls(medicalrecordid, medicalrecordid_gmhs, patientid, maubenhphamid, servicepriceid, thuchienclsdate, phauthuatvien, bacsigayme, phumo1, phumo2, phumo3, phumo4, tools_userid, tools_username) VALUES ('" + medicalrecordid + "', '" + medicalrecordid + "', '" + patientid + "', '" + maubenhphamid + "', '" + servicepriceid + "', '" + thuchienclsdate + "', '" + mochinh_idbs + "', '" + gayme_idbs + "', '" + phu1_idbs + "', '" + phu2_idbs + "', '" + giupviec1_idbs + "', '" + giupviec2_idbs + "', '" + SessionLogin.SessionUserID + "', '" + SessionLogin.SessionUsername + "');";
                    }
                    else
                    {
                        luulaithuchien = "UPDATE thuchiencls SET thuchienclsdate='" + thuchienclsdate + "', phauthuatvien='" + mochinh_idbs + "',  bacsigayme = '" + gayme_idbs + "', phumo1 = '" + phu1_idbs + "', phumo2 = '" + phu2_idbs + "', phumo3 = '" + giupviec1_idbs + "', phumo4 = '" + giupviec2_idbs + "', tools_userid='" + SessionLogin.SessionUserID + "', tools_username='" + SessionLogin.SessionUsername + "' WHERE thuchienclsid = " + thuchienclsid + "; ";
                    }

                    condb.ExecuteNonQuery_HIS(luulaithuchien);
                }
                this._duyetPTTT = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Custom
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
                else if (cboLoaiBaoCao.EditValue.ToString() == "XN")
                {
                    chkcomboListDSPhong.Enabled = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    //gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    bandedGridColumn_gv1_tien.Visible = true;
                    bandedGridColumn_gv1nsdd_tien.Visible = false;
                    LoadDanhMucPhongThucHien("XN");
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "CDHA")
                {
                    chkcomboListDSPhong.Enabled = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    //gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    bandedGridColumn_gv1_tien.Visible = true;
                    bandedGridColumn_gv1nsdd_tien.Visible = false;
                    LoadDanhMucPhongThucHien("CDHA");
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhMucPhongThucHien(string _loaiBC)
        {
            try
            {
                if (_loaiBC == "XN")
                {
                    var lstDSPhong = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 6).OrderBy(o => o.departmentname).ToList();
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
                }
                else if (_loaiBC == "CDHA")
                {
                    var lstDSPhong = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7).OrderBy(o => o.departmentname).ToList();
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
                }
                chkcomboListDSPhong.CheckAll();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
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
                    else if (val == "99")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[5]);
                        e.Graphics.DrawImage(imMenu.Images[5], pos);
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
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
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void bandedGridViewDataBCPTTT_ShowFilterPopupCheckedListBox(object sender, FilterPopupCheckedListBoxEventArgs e)
        {
            //try
            //{
            //    if (e.Column.FieldName != "servicepricename") return;
            //    // Hide the "Select All" item.
            //    e.CheckedComboBox.SelectAllItemVisible = false;
            //    // Locate and disable checked items that contain specific values.
            //    for (int i = 0; i < e.CheckedComboBox.Items.Count; i++)
            //    {
            //        DevExpress.XtraEditors.Controls.CheckedListBoxItem item = e.CheckedComboBox.Items[i];
            //        string itemValue = (string)(item.Value as FilterItem).Value;
            //        //if (itemValue == "Seafood" || itemValue == "Condiments")
            //        //{
            //        //    e.CheckedComboBox.Items[i].Enabled = false;
            //        //}
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }
        #endregion

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
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
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
                O2S_Common.Logging.LogSystem.Error(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
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
                O2S_Common.Logging.LogSystem.Error(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
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
                O2S_Common.Logging.LogSystem.Error(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

        #region In bao cao
        void Item_Print_Click(object sender, EventArgs e)
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
                    tbnPrintBCCLS_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền cận lâm sàng")
                {
                    tbnPrintBCThanhToanCLS_Click();
                }
                else if (tenbaocao == "Báo cáo Cận lâm sàng - Theo filter trên lưới")
                {
                    tbnPrintBCCLS_Filter_Click();
                }
                else if (tenbaocao == "Báo cáo thanh toán tiền cận lâm sàng - Theo filter trên lưới")
                {
                    tbnPrintBCThanhToanCLS_Filter_Click();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void tbnPrintBCCLS_Click()
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                }
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBCPTTT);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void tbnPrintBCThanhToanCLS_Click()
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanCLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                }
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBCPTTT);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void tbnPrintBCCLS_Filter_Click()
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay.xlsx";
                }
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void tbnPrintBCThanhToanCLS_Filter_Click()
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
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanCLS.xlsx";

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009")//thu thuat noi soi da day
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_NoiSoiDaDay_ThanhToan.xlsx";
                }
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }


        #endregion

        #region Duyet PTTT
        private void btnPTTT_Gui_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiem tra trang thai 
                if (KiemTraTrangThaiKhoaGuiPTTT(GetIdKhoaPhongTheoLoaiBC()))
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
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_DA_DUOC_GUI_YC_DUYET_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void btnPTTT_HuyGui_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiem tra trang thai 
                if (KiemTraTrangThaiKhoaGuiPTTT(GetIdKhoaPhongTheoLoaiBC()))
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
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DANG_GUI_YC_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
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
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DANG_GUI_YC_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
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
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DA_TIEP_NHAN_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
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
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DA_TIEP_NHAN_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
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
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_KO_PHAI_TT_DA_DUYET_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnPTTT_Khoa_Click(object sender, EventArgs e)
        {
            try
            {
                frmKhoaGuiYeuCau_CLS _frm = new frmKhoaGuiYeuCau_CLS();
                _frm.ShowDialog();
                //string _toolsoptionvalue = "1";
                //string _btnPTTT_KhoaMoKhoaText = "Mở khóa gửi YC";
                //if (btnPTTT_KhoaMoKhoa.Text == "Mở khóa gửi YC")
                //{
                //    _toolsoptionvalue = "0";
                //    _btnPTTT_KhoaMoKhoaText = "Khóa gửi YC";
                //}
                //string _updateKhoa = "UPDATE tools_option SET toolsoptionvalue='" + _toolsoptionvalue + "' WHERE toolsoptioncode='REPORT_12_KhoaGuiYeuCau';";
                //if (condb.ExecuteNonQuery_MeL(_updateKhoa))
                //{
                //    MessageBox.Show(btnPTTT_KhoaMoKhoa.Text + " PTTT thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    btnPTTT_KhoaMoKhoa.Text = _btnPTTT_KhoaMoKhoaText;
                //}
                //else
                //{
                //    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                //    frmthongbao.Show();
                //}
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion



    }
}
