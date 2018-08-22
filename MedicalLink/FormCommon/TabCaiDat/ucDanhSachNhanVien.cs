using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Base;
using DevExpress.XtraSplashScreen;
using Aspose.Cells;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucDanhSachNhanVien : UserControl
    {
        #region Khai bao
        private ConnectDatabase condb = new ConnectDatabase();
        private string CurentUserCodeid;
        private string CurentUserHisId;
        #endregion

        public ucDanhSachNhanVien()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                btnLuuLai.Enabled = false;
                txtusercode.Enabled = false;
                txtusername.Enabled = false;
                txtuserhisid.Enabled = false;
                cboNhomNhanVien.Enabled = false;
                cboNhomBaoCao.Enabled = false;
                cboKhoa.Enabled = false;

                //lay danh sach nhan vien
                LayDanhSachNhanVien();
                LoadDanhSachKhoa();
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void LayDanhSachNhanVien()
        {
            try
            {
                string _sqldsnv = "SELECT row_number () over (order by userhisid) as stt, * FROM nhompersonnel;";
                DataTable _dataDS = condb.GetDataTable_HIS(_sqldsnv);
                if (_dataDS != null && _dataDS.Rows.Count > 0)
                {
                    gridControlDSNV.DataSource = _dataDS;
                }
                else
                {
                    gridControlDSNV.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                string _sqlKhoa = "select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname;";
                DataTable _dataKhoa = condb.GetDataTable_HIS(_sqlKhoa);
                cboKhoa.Properties.DataSource = _dataKhoa;
                cboKhoa.Properties.DisplayMember = "departmentgroupname";
                cboKhoa.Properties.ValueMember = "departmentgroupid";
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Events
        private void btnNVThem_Click(object sender, EventArgs e)
        {
            try
            {
                txtusercode.Text = "";
                txtusername.Text = "";
                txtuserhisid.Text = "";
                btnLuuLai.Enabled = true;
                txtusercode.Enabled = true;
                txtusername.Enabled = true;
                txtuserhisid.Enabled = true;
                cboNhomNhanVien.Enabled = true;
                cboNhomBaoCao.Enabled = true;
                cboKhoa.Enabled = true;
                txtusercode.Focus();

                this.CurentUserCodeid = "";
                this.CurentUserHisId = "";
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void gridViewDSNV_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Row)
            {
                e.Menu.Items.Clear();
                DXMenuItem itemXoaNguoiDung = new DXMenuItem("Xóa tài khoản");
                itemXoaNguoiDung.Image = imMenu.Images["Xoa.png"];
                itemXoaNguoiDung.Click += new EventHandler(itemXoaNguoiDung_Click);
                e.Menu.Items.Add(itemXoaNguoiDung);
            }
        }
        private void itemXoaNguoiDung_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSNV.RowCount > 0)
                {
                    var rowHandle = gridViewDSNV.FocusedRowHandle;
                    string _usercode = Convert.ToString(gridViewDSNV.GetRowCellValue(rowHandle, "usercode").ToString());

                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản: " + _usercode + " không?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {

                        string _sqlxoaHIS = "DELETE FROM nhompersonnel WHERE usercode='" + _usercode + "';";
                        string _sqlXoaML = "DELETE FROM ml_nhanvien WHERE usercode='" + _usercode + "';";
                        if (condb.ExecuteNonQuery_HIS(_sqlxoaHIS) && condb.ExecuteNonQuery_MeL(_sqlXoaML))
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.XOA_THANH_CONG);
                            frmthongbao.Show();
                            LayDanhSachNhanVien();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void gridViewDSNV_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSNV.RowCount > 0)
                {
                    var rowHandle = gridViewDSNV.FocusedRowHandle;
                    this.CurentUserCodeid = gridViewDSNV.GetRowCellValue(rowHandle, "usercode").ToString();
                    this.CurentUserHisId = gridViewDSNV.GetRowCellValue(rowHandle, "userhisid").ToString();

                    txtusercode.Text = this.CurentUserCodeid;
                    txtusername.Text = gridViewDSNV.GetRowCellValue(rowHandle, "username").ToString();
                    txtuserhisid.Text = this.CurentUserHisId;
                    cboNhomNhanVien.Text = gridViewDSNV.GetRowCellValue(rowHandle, "usergnhom_name").ToString();
                    cboNhomBaoCao.Text = gridViewDSNV.GetRowCellValue(rowHandle, "nhom_bcten").ToString();
                    //cboKhoa.EditValue = null;
                    int _khoaID= Utilities.TypeConvertParse.ToInt32(gridViewDSNV.GetRowCellValue(rowHandle, "departmentgroupid").ToString()); ;
                    cboKhoa.EditValue = _khoaID;

                    txtusercode.Enabled = true;
                    txtusername.Enabled = true;
                    btnLuuLai.Enabled = true;
                    txtuserhisid.Enabled = true;
                    cboNhomNhanVien.Enabled = true;
                    cboNhomBaoCao.Enabled = true;
                    cboKhoa.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Luu lai
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtusercode.Text.Trim() == "")//chua nhap gi
                {
                    return;
                }
                string _usercode = txtusercode.Text.Trim().ToLower();
                string _username = txtusername.Text.Trim();
                string en_userpassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);
                string _usergnhom = "0";
                string _nhom_bcid = "0";
                string _departmentgroupid = "0";

                //nhom nhan vien
                if (cboNhomNhanVien.Text == "Bác sĩ")
                {
                    _usergnhom = "1";
                }
                else if (cboNhomNhanVien.Text == "Thu ngân")
                {
                    _usergnhom = "2";
                }
                else if (cboNhomNhanVien.Text == "CNTT")
                {
                    _usergnhom = "3";
                }
                else if (cboNhomNhanVien.Text == "Ban lãnh đạo")
                {
                    _usergnhom = "4";
                }
                else if (cboNhomNhanVien.Text == "Khác")
                {
                    _usergnhom = "99";
                }
                //nhom bao cao
                if (cboNhomBaoCao.Text == "Nhân viên hợp đồng")
                {
                    _nhom_bcid = "1";
                }
                else if (cboNhomBaoCao.Text == "Nhân viên bệnh viện")
                {
                    _nhom_bcid = "2";
                }
                else if (cboNhomBaoCao.Text == "Khác")
                {
                    _nhom_bcid = "99";
                }
                //khoa
                if (cboKhoa.EditValue != null)
                {
                    _departmentgroupid = cboKhoa.EditValue.ToString();
                }

                if (txtusercode.Text != this.CurentUserCodeid) //them moi
                {
                    if (CheckAccTonTai(txtusercode.Text.Trim(), txtuserhisid.Text.Trim()))
                    {
                        string _sqlInsertHIS = @"INSERT INTO nhompersonnel(usercode,username,userpassword,userstatus,usernote,userhisid,usergnhom,usergnhom_name,nhom_bcid,nhom_bcten,departmentgroupid,departmentgroupname) VALUES('" + _usercode + "','" + _username + "','" + en_userpassword + "','0','','" + txtuserhisid.Text.Trim() + "','" + _usergnhom + "','" + cboNhomNhanVien.Text + "','" + _nhom_bcid + "','" + cboNhomBaoCao.Text + "','" + _departmentgroupid + "','" + cboKhoa.Text + "');";
                        string _sqlInsertMeL = _sqlInsertHIS.Replace("nhompersonnel", "ml_nhanvien");

                        if (condb.ExecuteNonQuery_HIS(_sqlInsertHIS) && condb.ExecuteNonQuery_MeL(_sqlInsertMeL))
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.THEM_MOI_THANH_CONG);
                            frmthongbao.Show();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                        gridControlDSNV.DataSource = null;
                        LayDanhSachNhanVien();
                    }
                }
                else
                {
                    string _sqlUpdateHIS = "UPDATE nhompersonnel SET username='" + _username + "', userpassword='" + en_userpassword + "', userstatus='0', usernote='' , userhisid = '" + txtuserhisid.Text.Trim() + "', usergnhom='" + _usergnhom + "', usergnhom_name='" + cboNhomNhanVien.Text + "', nhom_bcid='" + _nhom_bcid + "',nhom_bcten='" + cboNhomBaoCao.Text + "',departmentgroupid='" + _departmentgroupid + "',departmentgroupname='" + cboKhoa.Text + "' WHERE usercode='" + _usercode + "';";
                    string _sqlUpdateMeL = _sqlUpdateHIS.Replace("nhompersonnel", "ml_nhanvien");

                    if (condb.ExecuteNonQuery_HIS(_sqlUpdateHIS) && condb.ExecuteNonQuery_MeL(_sqlUpdateMeL))
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.SUA_THANH_CONG);
                        frmthongbao.Show();
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.CO_LOI_XAY_RA);
                        frmthongbao.Show();
                    }
                    gridControlDSNV.DataSource = null;
                    LayDanhSachNhanVien();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Custom
        private void txtIDHIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void gridViewDSNV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        #endregion

        #region Process
        private bool CheckAccTonTai(string usercode, string userHisid)
        {
            bool result = true;
            try
            {
                string sqlcheckUserCode = "select usercode,userhisid from nhompersonnel where usercode='" + usercode + "';";
                DataTable datacheck = condb.GetDataTable_HIS(sqlcheckUserCode);
                if (datacheck != null && datacheck.Rows.Count > 0)
                {
                    result = false;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.TEN_TAI_KHOA_DA_TON_TAI);
                    frmthongbao.Show();
                    return false;
                }

                string sqlcheckUserHisId = "select usercode,userhisid from nhompersonnel where userhisid='" + userHisid + "';";
                DataTable datacheckHisId = condb.GetDataTable_HIS(sqlcheckUserHisId);
                if (datacheckHisId != null && datacheckHisId.Rows.Count > 0)
                {
                    result = false;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(ThongBaoLable.MA_HIS_ID_DA_TON_TAI);
                    frmthongbao.Show();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return result;
        }

        #endregion

        #region Import and export
        private void btnThemTuExcel_Click(object sender, EventArgs e)
        {
            if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                try
                {
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets["NhanVien"];
                    DataTable _dataUser_Import = worksheet.Cells.ExportDataTable(3, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
                    _dataUser_Import.TableName = "DATA";
                    if (_dataUser_Import != null && _dataUser_Import.Rows.Count > 0)
                    {
                        List<DanhSachNhanVienDTO> _lstNhanVien = Utilities.DataTables.DataTableToList<DanhSachNhanVienDTO>(_dataUser_Import);

                        int dem_update = 0;
                        int dem_insert = 0;

                        foreach (var _itemIns in _lstNhanVien)
                        {
                            //string _usercode = _itemIns.USERCODE;
                            string _username = _itemIns.USERNAME ?? "";
                            string _userstatus = (_itemIns.USERSTATUS ?? 0).ToString();
                            string _usergnhom = (_itemIns.USERGNHOM ?? 0).ToString();
                            string _usernote = _itemIns.USERNOTE ?? "";
                            string _userhisid = (_itemIns.USERHISID ?? 0).ToString();
                            string _usergnhom_name = _itemIns.USERGNHOM_NAME ?? "";
                            string _nhom_bcid = (_itemIns.NHOM_BCID ?? 0).ToString();
                            string _nhom_bcten = _itemIns.NHOM_BCTEN ?? "";
                            string _departmentgroupid = (_itemIns.DEPARTMENTGROUPID ?? 0).ToString();
                            string _departmentgroupname = _itemIns.DEPARTMENTGROUPNAME ?? "";

                            if ((_itemIns.STT != null && _itemIns.STT != 0) && _itemIns.USERCODE != null)
                            {
                                //kiem tra ton tai
                                string _sql_kt = "SELECT usercode FROM nhompersonnel WHERE usercode='" + _itemIns.USERCODE + "';";
                                DataTable _dataKiemTra = condb.GetDataTable_HIS(_sql_kt);
                                if (_dataKiemTra != null && _dataKiemTra.Rows.Count > 0)
                                {
                                    //update
                                    string _sqlUpdateHIS = @"UPDATE nhompersonnel SET username='" + _username + "', userstatus='" + _userstatus + "', usernote='" + _usernote + "' , userhisid = '" + _userhisid + "', usergnhom='" + _usergnhom + "', usergnhom_name='" + _usergnhom_name + "', nhom_bcid='" + _nhom_bcid + "', nhom_bcten='" + _nhom_bcten + "',departmentgroupid='" + _departmentgroupid + "',departmentgroupname='" + _departmentgroupname + "' WHERE usercode='" + _itemIns.USERCODE + "';";
                                    try
                                    {
                                        string _sqlUpdateMeL = _sqlUpdateHIS.Replace("nhompersonnel", "ml_nhanvien");
                                        if (condb.ExecuteNonQuery_HIS(_sqlUpdateHIS) && condb.ExecuteNonQuery_MeL(_sqlUpdateMeL))
                                        {
                                            dem_update += 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        continue;
                                        Base.Logging.Error(ex);
                                    }
                                }
                                else
                                {
                                    //Insert
                                    string _sqlInsertHIS = @"INSERT INTO nhompersonnel(usercode,username,userpassword,userstatus,usernote,userhisid,usergnhom,usergnhom_name,nhom_bcid,nhom_bcten,departmentgroupid,departmentgroupname) VALUES('" + _itemIns.USERCODE + "','" + _username + "','" + Base.EncryptAndDecrypt.Encrypt("", true) + "','" + _userstatus + "','" + _usernote + "','" + _userhisid + "','" + _usergnhom + "','" + _usergnhom_name + "','" + _nhom_bcid + "','" + _nhom_bcten + "','" + _departmentgroupid + "','" + _departmentgroupname + "');";
                                    try
                                    {
                                        string _sqlInsertMeL = _sqlInsertHIS.Replace("nhompersonnel", "ml_nhanvien");
                                        if (condb.ExecuteNonQuery_HIS(_sqlInsertHIS) && condb.ExecuteNonQuery_MeL(_sqlInsertMeL))
                                        {
                                            dem_insert += 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        continue;
                                        Base.Logging.Error(ex);
                                    }
                                }
                            }
                        }

                        MessageBox.Show("Thêm mới [ " + dem_insert + " ] & cập nhật [ " + dem_update + " ] nhân viên thành công.");
                        gridControlDSNV.DataSource = null;
                        LayDanhSachNhanVien();
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                    }
                }
                catch (Exception ex)
                {
                    Base.Logging.Error(ex);
                }
                SplashScreenManager.CloseForm();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "0_ToolsNhanVien_Export.xlsx";
                string _sqllayds = "SELECT row_number () over (order by userhisid) as stt, * FROM nhompersonnel;";
                string _sqlKhoa = "select departmentgroupid as khoa_id,departmentgroupname as khoa_ten from departmentgroup order by departmentgroupname;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqllayds);
                DataTable _dataKhoa = condb.GetDataTable_HIS(_sqlKhoa);

                List<DataTable> _lstDataTable = new List<DataTable>();
                _lstDataTable.Add(_dataBaoCao);
                _lstDataTable.Add(_dataKhoa);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _lstDataTable);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion


    }
}
