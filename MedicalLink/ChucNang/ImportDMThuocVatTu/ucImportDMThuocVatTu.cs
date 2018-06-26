using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using MedicalLink.Base;
using MedicalLink.ChucNang.ImportDMDichVu;
using Aspose.Cells;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using MedicalLink.ChucNang.ImportDMThuocVatTu;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.ChucNang
{
    public partial class ucImportDMThuocVatTu : UserControl
    {
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<MedicineRef> lstMedicineRef { get; set; }
        public ucImportDMThuocVatTu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucImportDMThuocVatTu_Load(object sender, EventArgs e)
        {
            try
            {
                //cbbChonLoai.ReadOnly = true;
                radioButtonCapNhat.Checked = true;
                radioButtonThemMoi.Checked = false;
                cbbChonKieu.EditValue = "";
                //cbbChonLoai.EditValue = "";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Events
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogSelect.FileName;
                    gridControlThuoc.DataSource = null;
                    this.lstMedicineRef = new List<MedicineRef>();
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets["DanhMucThuoc"];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(6, 0, worksheet.Cells.MaxDataRow - 5, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null && data_Excel.Rows.Count > 0)
                    {
                        foreach (DataRow row in data_Excel.Rows)
                        {
                            if (row["STT"].ToString() != "")
                            {
                                MedicineRef _medicineRef = new MedicineRef();
                                //_medicineRef.stt = Utilities.TypeConvertParse.ToInt64(row["STT"].ToString());
                                //_medicineRef.servicegrouptype = Utilities.TypeConvertParse.ToInt64(row["SERVICEGROUPTYPE"].ToString());
                                //_medicineRef.servicegrouptype_name = row["SERVICEGROUPTYPE_NAME"].ToString();
                                //_medicineRef.servicepricecode = row["SERVICEPRICECODE"].ToString().Trim();
                                //_medicineRef.servicepricegroupcode = row["SERVICEPRICEGROUPCODE"].ToString();
                                //_medicineRef.servicepricecodeuser = row["SERVICEPRICECODEUSER"].ToString();
                                //_medicineRef.servicepricesttuser = row["SERVICEPRICESTTUSER"].ToString();
                                //_medicineRef.servicepricename = row["SERVICEPRICENAME"].ToString();
                                //_medicineRef.servicepricenamebhyt = row["SERVICEPRICENAMEBHYT"].ToString();
                                //_medicineRef.servicepriceunit = row["SERVICEPRICEUNIT"].ToString();
                                //_medicineRef.servicepricefeebhyt = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEEBHYT"].ToString());
                                //_medicineRef.servicepricefeenhandan = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEENHANDAN"].ToString());
                                //_medicineRef.servicepricefee = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEE"].ToString());
                                //_medicineRef.servicepricefeenuocngoai = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEENUOCNGOAI"].ToString());
                                //_medicineRef.servicepricefee_old_date = row["SERVICEPRICEFEE_OLD_DATE"].ToString();
                                //_medicineRef.servicepricefee_old_type = Utilities.TypeConvertParse.ToInt64(row["SERVICEPRICEFEE_OLD_TYPE"].ToString());
                                //_medicineRef.servicepricefee_old_type_name = row["SERVICEPRICEFEE_OLD_TYPE_NAME"].ToString();
                                //_medicineRef.pttt_hangid = Utilities.TypeConvertParse.ToInt64(row["PTTT_HANGID"].ToString());
                                //_medicineRef.pttt_hangid_name = row["PTTT_HANGID_NAME"].ToString();
                                //_medicineRef.pttt_loaiid = Utilities.TypeConvertParse.ToInt64(row["PTTT_LOAIID"].ToString());
                                //_medicineRef.pttt_loaiid_name = row["PTTT_LOAIID_NAME"].ToString();
                                //_medicineRef.servicelock = Utilities.TypeConvertParse.ToInt64(row["SERVICELOCK"].ToString());
                                //_medicineRef.bhyt_groupcode = row["BHYT_GROUPCODE"].ToString();
                                //_medicineRef.bhyt_groupcode_name = row["BHYT_GROUPCODE_NAME"].ToString();
                                //_medicineRef.report_groupcode = row["REPORT_GROUPCODE"].ToString();
                                //_medicineRef.report_tkcode = row["REPORT_TKCODE"].ToString();
                                //_medicineRef.servicepricetype = Utilities.TypeConvertParse.ToInt64(row["SERVICEPRICETYPE"].ToString());
                                //_medicineRef.servicecode = row["SERVICECODE"].ToString();
                                this.lstMedicineRef.Add(_medicineRef);
                            }
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                        return;
                    }
                    gridControlThuoc.DataSource = this.lstMedicineRef;
                }
            }
            catch (Exception ex)
            {
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA_KIEM_TRA_LAI_TEMPLATE);
                frmthongbao.Show();
                Base.Logging.Error(ex);
            }
        }

        private void btnUpdateDVOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstMedicineRef != null && this.lstMedicineRef.Count > 0)
                {
                    if (radioButtonCapNhat.Checked == true && radioButtonThemMoi.Checked == false)
                    {
                        CapNhatDanhMucThuocVatTuProcess();
                    }
                    else if (radioButtonCapNhat.Checked == false && radioButtonThemMoi.Checked == true)
                    {
                        ThemMoiDanhMucThuocVatTuProcess();
                    }
                    else
                    {
                        MessageBox.Show("Không xác định được loại yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnBackupGia_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string sql_bak_gia = "UPDATE ServicePriceRef SET Tools_TGApDung_bak_1 = ServicePriceFee_OLD_DATE, Tools_gia_bak_1 = ServicePriceFee_OLD, Tools_giaBHYT_bak_1 = ServicePriceFeeBHYT_OLD, Tools_giaNhanDan_bak_1 = ServicePriceFeeNhanDan_OLD, Tools_giaNuocNgoai_bak_1 = ServicePriceFeeNuocNgoai_OLD, Tools_KieuApDung_bak_1 = ServicePriceFee_OLD_Type;";
            //    condb.ExecuteNonQuery_HIS(sql_bak_gia);
            //    MessageBox.Show("Backup thành công giá cũ sang cột dự phòng", "Thông báo");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Có lỗi xảy ra trong quá trình backup lại giá cũ" + ex, "Thông báo");
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                frmChonNhomThuocVatTu frmchon = new frmChonNhomThuocVatTu();
                frmchon.ShowDialog();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Cusstom
        private void radioButtonThemMoi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonThemMoi.Checked == true)
                {
                    radioButtonCapNhat.Checked = false;
                    cbbChonKieu.ReadOnly = true;
                    cbbChonKieu.EditValue = "";
                }
                else
                {
                    //radioButtonCapNhat.Checked = true;
                    //cbbChonKieu.ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void radioButtonCapNhat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCapNhat.Checked == true)
                {
                    radioButtonThemMoi.Checked = false;
                    cbbChonKieu.ReadOnly = false;
                }
                else
                {
                    //radioButtonThemMoi.Checked = true;
                    //cbbChonKieu.ReadOnly = true;
                }

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridViewDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Process
        private void CapNhatDanhMucThuocVatTuProcess()
        {
            try
            {
                string chonkieuimport = cbbChonKieu.Text.Trim();
                if (cbbChonKieu.Text.Trim() != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Hãy backup dữ liệu trước khi thực hiện.\nNhấn \"YES\" để tiếp tục, nhấn \"NO\" để quay lại backup ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                        switch (chonkieuimport)
                        {
                            case "Tên thuốc":
                                {
                                    CapNhatProcess_TenThuoc();
                                    break;
                                }
                            case "Mã DM BYT (mã User)":
                                {
                                    CapNhatProcess_MaDMBYT();
                                    break;
                                }
                            case "Mã STT thầu BHYT":
                                {
                                    CapNhatProcess_MaSTTThau();
                                    break;
                                }
                            case "Năm thầu":
                                {
                                    CapNhatProcess_NamThau();
                                    break;
                                }
                            case "Đánh STT ngày SD":
                                {
                                    CapNhatProcess_DanhSTTNgaySD();
                                    break;
                                }
                            case "Đường dùng":
                                {
                                    CapNhatProcess_DuongDung();
                                    break;
                                }
                            case "Đóng gói":
                                {
                                    CapNhatProcess_DongGoi();
                                    break;
                                }
                            case "Số đăng ký":
                                {
                                    CapNhatProcess_SoDangKy();
                                    break;
                                }
                            case "Số lô":
                                {
                                    CapNhatProcess_SoLo();
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                        }
                        SplashScreenManager.CloseForm();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                Base.Logging.Warn(ex);
            }
        }
        private void ThemMoiDanhMucThuocVatTuProcess()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int dem_dv_themmoi = 0;
                int dem_dv_trungma = 0;

                DialogResult dialogResult = MessageBox.Show("Hãy backup trước khi thực hiện.\nNhấn \"YES\" để tiếp tục, nhấn \"NO\" để quay lại backup ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    foreach (var item_servicep in this.lstMedicineRef)
                    {
                        try
                        {
                            if (item_servicep.medicinecode != "" && item_servicep.datatype != 0)
                            {
                                string sql_kt = "SELECT medicinerefid,medicinecode FROM medicine_ref WHERE medicinecode= '" + item_servicep.medicinecode + "' ;";
                                DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    //TODO Update
                                }
                                else if (dv_kt.Count == 0)
                                {
                                    //Them moi
                                    string sql_insertmedicineref = "";
                                    if (condb.ExecuteNonQuery_HIS(sql_insertmedicineref))
                                    {
                                        dem_dv_themmoi += 1;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    //lưu lại log
                    if (dem_dv_themmoi > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Insert " + dem_dv_themmoi + " thuuốc/vật tư thành công. Update thành công=" + dem_dv_trungma + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("Thêm mới thành công SL=" + dem_dv_themmoi + ".\nUpdate thành công=" + dem_dv_trungma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                Base.Logging.Error(ex);
            }
        }

        #region Cap nhat
        private void CapNhatProcess_TenThuoc()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT MedicineRefID, MedicineCode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update tên thuốc
                            string sqlupdatetenthuoc = "UPDATE medicine_ref SET MedicineName = '" + itemMedi.medicinename + "', MedicineName_BYT = '" + itemMedi.medicinename + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetenthuoc);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

                //lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục tên thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Tên thuốc\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_MaDMBYT()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update mã DM BYT (mã User)
                            string sqlupdatemauser = "UPDATE medicine_ref SET MedicineCodeUser = '" + itemMedi.medicinecodeuser + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatemauser);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục Mã DM BYT (mã user) thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Mã DM BYT (mã user)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_MaSTTThau()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update mã STT Thầu BHYT
                            string sqlupdatesttthau = "UPDATE medicine_ref SET STT_DauThau = '" + itemMedi.stt_dauthau + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatesttthau);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục mã STT thầu BHYT thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Mã STT thầu BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_NamThau()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update Năm thầu
                            string sqlupdatenamthau = "UPDATE medicine_ref SET NamCungUng = '" + itemMedi.namcungung + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatenamthau);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục năm thầu thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Năm thầu\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_DanhSTTNgaySD()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update Đánh STT ngày SD
                            string sqlupdatesttngaysd = "UPDATE medicine_ref SET DanhSTTDungThuoc = '" + itemMedi.danhsttdungthuoc + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatesttngaysd);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục Đánh STT ngày dùng thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Đánh STT ngày dùng\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_DuongDung()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update mã đường dùng
                            string sqlupdateduongdung = "UPDATE medicine_ref SET DangDung = '" + itemMedi.dangdung + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdateduongdung);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục đường dùng thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Đường dùng\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_DongGoi()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update mã đóng gói
                            string sqlupdatedonggoi = "UPDATE medicine_ref SET DongGoi = '" + itemMedi.donggoi + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatedonggoi);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục đóng gói thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Đóng gói\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_SoDangKy()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update số đăng ký
                            string sqlupdatesdk = "UPDATE medicine_ref SET SoDangKy = '" + itemMedi.sodangky + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatesdk);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục số đăng ký thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Số đăng ký\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void CapNhatProcess_SoLo()
        {
            string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_thuoc = 0;
            try
            {
                foreach (var itemMedi in this.lstMedicineRef)
                {
                    condb.Connect();
                    string sql_kt = "SELECT medicinecode FROM medicine_ref WHERE medicinecode= '" + itemMedi.medicinecode + "' ;";
                    DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        try
                        {
                            // Update số lô
                            string sqlupdatesolo = "UPDATE medicine_ref SET SoLo = '" + itemMedi.solo + "' WHERE medicinecode = '" + itemMedi.medicinecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatesolo);
                            count_thuoc += dv_kt.Count;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
                // Lưu lại log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục số lô thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_06');";
                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                MessageBox.Show("Update " + count_thuoc + " danh mục \"Số lô\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion


        #endregion
    }
}
