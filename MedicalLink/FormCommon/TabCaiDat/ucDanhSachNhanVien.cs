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

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucDanhSachNhanVien : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string curentUsercodeid;
        private string curentUserHisId;
        private string worksheetName = "tools_tblnhanvien";
        private DataView dmUser_Import;

        public ucDanhSachNhanVien()
        {
            InitializeComponent();
            btnNVOK.Enabled = false;
            txtNVID.Enabled = false;
            txtNVName.Enabled = false;
            txtIDHIS.Enabled = false;
        }

        private void btnNVThem_Click(object sender, EventArgs e)
        {
            txtNVID.Text = "";
            txtNVName.Text = "";
            txtIDHIS.Text = "";
            btnNVOK.Enabled = true;
            txtNVID.Enabled = true;
            txtNVName.Enabled = true;
            txtIDHIS.Enabled = true;
            txtNVID.Focus();
        }

        // Load danh sách nhân viên
        private void ucDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                string sqldsnv = "SELECT nhanvienid as stt, usercode as manv, username as tennv, userhisid FROM tools_tblnhanvien ORDER BY manv";
                DataView dv = new DataView(condb.GetDataTable_HIS(sqldsnv));
                if (dv.Count > 0)
                {
                    gridControlDSNV.DataSource = dv;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridViewDSNV_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSNV.FocusedRowHandle;
                curentUsercodeid = gridViewDSNV.GetRowCellValue(rowHandle, "manv").ToString();
                this.curentUserHisId = gridViewDSNV.GetRowCellValue(rowHandle, "userhisid").ToString();
                txtNVID.Enabled = true;
                txtNVName.Enabled = true;
                btnNVOK.Enabled = true;
                txtIDHIS.Enabled = true;
                txtNVID.Text = curentUsercodeid;
                txtNVName.Text = gridViewDSNV.GetRowCellValue(rowHandle, "tennv").ToString();
                txtIDHIS.Text = gridViewDSNV.GetRowCellValue(rowHandle, "userhisid").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        // Thêm, sửa danh sách nhân viên
        private void btnNVOK_Click_1(object sender, EventArgs e)
        {
            string en_txtNVID = txtNVID.Text.Trim().ToLower();
            string en_txtNVName = txtNVName.Text.Trim();
            string en_pass = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);

            try
            {
                if (txtNVID.Text != curentUsercodeid)
                {
                    if (CheckAccTonTai(txtNVID.Text.Trim(), txtIDHIS.Text.Trim()))
                    {
                        string sql = "INSERT INTO tools_tblnhanvien(usercode, username, userpassword, userstatus, usergnhom, usernote, userhisid) VALUES ('" + en_txtNVID + "','" + en_txtNVName + "','" + en_pass + "','0','2','Nhân viên', '" + txtIDHIS.Text.Trim() + "');";
                        if (condb.ExecuteNonQuery_HIS(sql))
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                            frmthongbao.Show();
                        }
                        gridControlDSNV.DataSource = null;
                        ucDanhSachNhanVien_Load(null, null);
                    }
                }
                else
                {
                    string sql = "UPDATE tools_tblnhanvien SET usercode='" + en_txtNVID + "', username='" + en_txtNVName + "', userpassword='" + en_pass + "', userstatus='0', usergnhom='2', usernote='' , userhisid = '" + txtIDHIS.Text.Trim() + "' WHERE usercode='" + en_txtNVID + "';";
                    if (condb.ExecuteNonQuery_HIS(sql))
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.SUA_THANH_CONG);
                        frmthongbao.Show();
                    }
                    gridControlDSNV.DataSource = null;
                    ucDanhSachNhanVien_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private bool CheckAccTonTai(string usercode, string userHisid)
        {
            bool result = true;
            try
            {
                string sqlcheckUserCode = "select usercode,userhisid from tools_tblnhanvien where usercode='" + usercode + "';";
                DataTable datacheck = condb.GetDataTable_HIS(sqlcheckUserCode);
                if (datacheck != null && datacheck.Rows.Count > 0)
                {
                    result = false;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.TEN_TAI_KHOA_DA_TON_TAI);
                    frmthongbao.Show();
                    return false;
                }

                string sqlcheckUserHisId = "select usercode,userhisid from tools_tblnhanvien where userhisid='" + userHisid + "';";
                DataTable datacheckHisId = condb.GetDataTable_HIS(sqlcheckUserHisId);
                if (datacheckHisId != null && datacheckHisId.Rows.Count > 0)
                {
                    result = false;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.MA_HIS_ID_DA_TON_TAI);
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
        private void gridViewDSNV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        //Import tu file Excel
        private void btnThemTuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    MedicalLink.Base.ReadExcelFile _excel = new MedicalLink.Base.ReadExcelFile(openFileDialogSelect.FileName);
                    var data = _excel.GetDataTable("SELECT USERCODE,USERNAME,USERPASSWORD,USERSTATUS,USERGNHOM,USERNOTE,USERHISID FROM [" + worksheetName + "$]");
                    if (data != null)
                    {
                        int dem_update = 0;
                        int dem_insert = 0;
                        //gridViewDichVu_Import.DataSource = data;
                        dmUser_Import = new DataView(data);

                        for (int i = 0; i < dmUser_Import.Count; i++)
                        {
                            string en_txtNVCode = dmUser_Import[i]["USERCODE"].ToString().Trim();
                            string en_txtNVName = dmUser_Import[i]["USERNAME"].ToString().Trim();
                            string en_pass = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);
                            if (dmUser_Import[i]["USERCODE"].ToString() != "")
                            {
                                condb.Connect();
                                string sql_kt = "SELECT usercode FROM tools_tblnhanvien WHERE usercode='" + en_txtNVCode + "';";
                                DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                                if (dv_kt.Count > 0) //update
                                {
                                    string sql_updateUser = "UPDATE tools_tblnhanvien SET username='" + en_txtNVName + "', userhisid='" + dmUser_Import[i]["USERHISID"] + "' WHERE usercode='" + en_txtNVCode + "';";
                                    try
                                    {
                                        condb.ExecuteNonQuery_HIS(sql_updateUser);
                                        dem_update += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    string sql_insertDVKT = "INSERT INTO tools_tblnhanvien(usercode, username, userpassword, userstatus, usergnhom, usernote,userhisid) VALUES ('" + en_txtNVCode + "','" + en_txtNVName + "','" + en_pass + "','0','3','Nhân viên', '" + dmUser_Import[i]["USERHISID"] + "');";
                                    try
                                    {
                                        condb.ExecuteNonQuery_HIS(sql_insertDVKT);
                                        dem_insert += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Thêm mới [ " + dem_insert + " ] & cập nhật [ " + dem_update + " ] nhân viên thành công.");
                        gridControlDSNV.DataSource = null;
                        ucDanhSachNhanVien_Load(null, null);
                    }
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                Base.Logging.Error(ex);
            }
        }

        private void gridViewDSNV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

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
        void itemXoaNguoiDung_Click(object sender, EventArgs e)
        {
            var rowHandle = gridViewDSNV.FocusedRowHandle;
            string usercode = Convert.ToString(gridViewDSNV.GetRowCellValue(rowHandle, "manv").ToString());

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản: " + usercode + " không?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string sqlxoatk = "DELETE FROM tools_tblnhanvien WHERE usercode='" + usercode + "';";
                    condb.ExecuteNonQuery_HIS(sqlxoatk);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Đã xóa bỏ tài khoản: " + usercode);
                    frmthongbao.Show();
                    gridControlDSNV.DataSource = null;
                    ucDanhSachNhanVien_Load(null, null);
                }
                catch (Exception ex)
                {
                    Base.Logging.Warn(ex);
                }
            }
        }

        private void txtIDHIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


    }
}
