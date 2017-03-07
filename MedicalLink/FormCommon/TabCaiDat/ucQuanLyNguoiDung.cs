using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucQuanLyNguoiDung : UserControl
    {
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string currentUserCode;
        private List<MedicalLink.ClassCommon.classPermission> lstPer { get; set; }
        private List<MedicalLink.ClassCommon.classUserDepartment> lstUserDepartment { get; set; }
        private List<MedicalLink.ClassCommon.classPermission> lstPerBaoCao { get; set; }
        public ucQuanLyNguoiDung()
        {
            InitializeComponent();
        }

        #region Load
        private void ucQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl(false);
                LoadDanhSachNguoiDung();
                LoadDanhSachChucNang();
                LoadDanhSachKhoaPhong();
                LoadDanhSachBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachNguoiDung()
        {
            try
            {
                string sql = "select usercode, username, userpassword, case usergnhom when '0' then 'Admin' when '1' then 'Quản trị hệ thống' when 2 then 'Nhân viên' end as usergnhom from tools_tbluser where usergnhom in (1,2) order by usercode";
                DataView dv = new DataView(condb.getDataTable(sql));

                if (dv.Count > 0)
                {
                    //Giải mã hiển thị lên Gridview
                    for (int i = 0; i < dv.Count; i++)
                    {
                        string usercode_de = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[i]["usercode"].ToString(), true);
                        string username_de = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[i]["username"].ToString(), true);
                        dv[i]["usercode"] = usercode_de;
                        dv[i]["username"] = username_de;
                    }
                    gridControlDSUser.DataSource = dv;
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Danh sách rỗng";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachChucNang()
        {
            try
            {
                lstPer = new List<ClassCommon.classPermission>();
                lstPer = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o=>o.permissiontype!=10).ToList();
                gridControlChucNang.DataSource = lstPer;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachKhoaPhong()
        {
            try
            {
                string sql = "SELECT degp.departmentgroupid, degp.departmentgroupname, de.departmentid, de.departmentcode, de.departmentname, de.departmenttype FROM department de inner join departmentgroup degp on de.departmentgroupid=degp.departmentgroupid WHERE degp.departmentgrouptype in (1,4,10,11) and de.departmenttype in (2,3,9) ORDER BY degp.departmentgroupid,de.departmenttype, de.departmentname;";
                DataView dataPhong = new DataView(condb.getDataTable(sql));
                lstUserDepartment = new List<ClassCommon.classUserDepartment>();
                for (int i = 0; i < dataPhong.Count; i++)
                {
                    ClassCommon.classUserDepartment userDepartment = new ClassCommon.classUserDepartment();
                    userDepartment.departmentcheck = false;
                    userDepartment.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt32(dataPhong[i]["departmentgroupid"].ToString());
                    userDepartment.departmentgroupname = dataPhong[i]["departmentgroupname"].ToString();
                    userDepartment.departmentid = Utilities.Util_TypeConvertParse.ToInt32(dataPhong[i]["departmentid"].ToString());
                    userDepartment.departmentcode = dataPhong[i]["departmentcode"].ToString();
                    userDepartment.departmentname = dataPhong[i]["departmentname"].ToString();
                    userDepartment.departmenttype = Utilities.Util_TypeConvertParse.ToInt32(dataPhong[i]["departmenttype"].ToString());
                    lstUserDepartment.Add(userDepartment);
                }
                gridControlKhoaPhong.DataSource = lstUserDepartment;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachBaoCao()
        {
            try
            {
                lstPerBaoCao = new List<ClassCommon.classPermission>();
                lstPerBaoCao = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype== 10).ToList();
                gridControlBaoCao.DataSource = lstPerBaoCao;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void EnableAndDisableControl(bool value)
        {
            try
            {
                btnUserOK.Enabled = value;
                txtUserID.Enabled = value;
                txtUsername.Enabled = value;
                txtUserPassword.Enabled = value;
                cbbUserNhom.Enabled = value;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridControlDSUser_Click(object sender, EventArgs e)
        {
            var rowHandle = gridViewDSUser.FocusedRowHandle;
            try
            {
                EnableAndDisableControl(true);
                txtUserID.ReadOnly = true;
                currentUserCode = gridViewDSUser.GetRowCellValue(rowHandle, "usercode").ToString();
                txtUserID.Text = currentUserCode;
                txtUsername.Text = gridViewDSUser.GetRowCellValue(rowHandle, "username").ToString(); ;
                txtUserPassword.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(gridViewDSUser.GetRowCellValue(rowHandle, "userpassword").ToString(), true);
                cbbUserNhom.Text = gridViewDSUser.GetRowCellValue(rowHandle, "usergnhom").ToString();

                LoadDanhSachChucNang();
                LoadDanhSachKhoaPhong();
                LoadDanhSachBaoCao();
                LoadPhanQuyenChucNang();
                LoadPhanQuyenKhoaPhong();
                LoadPhanQuyenBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenChucNang()
        {
            try
            {
                gridControlChucNang.DataSource = null;
                string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "';";
                DataView dv = new DataView(condb.getDataTable(sqlquerry_per));
                //Load dữ liệu list phân quyền + tích quyền của use đang chọn lấy trong DB
                if (dv != null && dv.Count > 0)
                {
                    for (int i = 0; i < lstPer.Count; i++)
                    {
                        for (int j = 0; j < dv.Count; j++)
                        {
                            if (lstPer[i].permissioncode.ToString() == EncryptAndDecrypt.Decrypt(dv[j]["permissioncode"].ToString(), true))
                            {
                                lstPer[i].permissioncheck = Convert.ToBoolean(dv[j]["permissioncheck"]);
                            }
                        }
                    }
                }
                gridControlChucNang.DataSource = lstPer;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenKhoaPhong()
        {
            try
            {
                gridControlKhoaPhong.DataSource = null;
                string sqlquerry_khoaphong = "SELECT userdepgid,departmentgroupid,departmentid,departmenttype,usercode FROM tools_tbluser_departmentgroup WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "';";
                DataView dv_khoaphong = new DataView(condb.getDataTable(sqlquerry_khoaphong));
                if (dv_khoaphong != null && dv_khoaphong.Count > 0)
                {
                    for (int i = 0; i < lstUserDepartment.Count; i++)
                    {
                        for (int j = 0; j < dv_khoaphong.Count; j++)
                        {
                            if (lstUserDepartment[i].departmentid.ToString() == dv_khoaphong[j]["departmentid"].ToString())
                            {
                                lstUserDepartment[i].departmentcheck = true;
                            }
                        }
                    }
                }
                gridControlKhoaPhong.DataSource = lstUserDepartment;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenBaoCao()
        {
            try
            {
                gridControlBaoCao.DataSource = null;
                string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "' and userpermissionnote='BAOCAO';";
                DataView dv = new DataView(condb.getDataTable(sqlquerry_per));
                //Load dữ liệu list phân quyền + tích quyền của use đang chọn lấy trong DB
                if (dv != null && dv.Count > 0)
                {
                    for (int i = 0; i < lstPerBaoCao.Count; i++)
                    {
                        for (int j = 0; j < dv.Count; j++)
                        {
                            if (lstPerBaoCao[i].permissioncode.ToString() == EncryptAndDecrypt.Decrypt(dv[j]["permissioncode"].ToString(), true))
                            {
                                lstPerBaoCao[i].permissioncheck = Convert.ToBoolean(dv[j]["permissioncheck"]);
                            }
                        }
                    }
                }
                gridControlBaoCao.DataSource = lstPerBaoCao;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        //Khi ấn nút thêm thì hiển thị textbox
        private void btnUserThem_Click(object sender, EventArgs e)
        {
            txtUserID.ResetText();
            txtUsername.ResetText();
            txtUserPassword.ResetText();
            EnableAndDisableControl(true);
            cbbUserNhom.Text = "Nhân viên";
            txtUserID.ReadOnly = false;
            txtUserID.Focus();
            btnUserOK.Enabled = true;
            gridControlChucNang.DataSource = null;
            gridControlKhoaPhong.DataSource = null;
            LoadDanhSachChucNang();
            LoadDanhSachKhoaPhong();
            currentUserCode = null;
        }

        #region Tạo sự kiện khi kích OK
        private void btnUserOK_Click(object sender, EventArgs e)
        {
            // Mã hóa tài khoản
            string en_txtUserID = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUserID.Text.Trim(), true);
            string en_txtUsername = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
            string en_txtUserPassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUserPassword.Text.Trim(), true);
            try
            {
                if (currentUserCode == null)//them moi
                {
                    CreateNewUser(en_txtUserID, en_txtUsername, en_txtUserPassword);
                    CreateNewPermission(en_txtUserID);
                    CreateNewUserDepartment(en_txtUserID);
                    CreateNewPermissionBaoCao(en_txtUserID);
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Thêm mới thành công";
                    LoadDanhSachNguoiDung();
                }
                else //Update 
                {
                    UpdateUser(en_txtUserID, en_txtUsername, en_txtUserPassword);
                    UpdatePermission(en_txtUserID);
                    UpdateUserDepartment(en_txtUserID);
                    UpdatePermissionBaoCao(en_txtUserID);
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Cập nhật thành công";
                }      
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void CreateNewUser(string en_txtUserID, string en_txtUsername, string en_txtUserPassword)
        {
            try
            {
                string sqlinsert_user = "";
                // usergnhom=1: Quan tri he thong
                // usergnhom=2: User
                if (cbbUserNhom.Text == "Quản trị hệ thống")
                {
                    sqlinsert_user = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + en_txtUserID + "','" + en_txtUsername + "','" + en_txtUserPassword + "','0','1','');";
                }
                else
                {
                    sqlinsert_user = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + en_txtUserID + "','" + en_txtUsername + "','" + en_txtUserPassword + "','0','2','Nhân viên');";
                }
                condb.ExecuteNonQuery(sqlinsert_user);
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewPermission(string en_txtUserID)
        {
            try
            {
                string sqlinsert_per = "";
                for (int i = 0; i < lstPer.Count; i++)
                {
                    sqlinsert_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode.ToString(), true);
                    string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissionname.ToString(), true);
                    if (lstPer[i].permissioncheck == true)
                    {
                        sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', '');";
                        condb.ExecuteNonQuery(sqlinsert_per);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewUserDepartment(string en_txtUserID)
        {
            try
            {
                string sqlinsert_userdepartment = "";
                for (int i = 0; i < lstUserDepartment.Count; i++)
                {
                    sqlinsert_userdepartment = "";
                    if (lstUserDepartment[i].departmentcheck == true)
                    {
                        sqlinsert_userdepartment = "INSERT INTO tools_tbluser_departmentgroup(departmentgroupid, departmentid, departmenttype, usercode, userdepgidnote) VALUES ('" + lstUserDepartment[i].departmentgroupid + "','" + lstUserDepartment[i].departmentid + "','" + lstUserDepartment[i].departmenttype + "','" + en_txtUserID + "','');";
                        condb.ExecuteNonQuery(sqlinsert_userdepartment);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewPermissionBaoCao(string en_txtUserID)
        {
            try
            {
                string sqlinsert_per = "";
                for (int i = 0; i < lstPerBaoCao.Count; i++)
                {
                    sqlinsert_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissioncode.ToString(), true);
                    string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissionname.ToString(), true);
                    if (lstPerBaoCao[i].permissioncheck == true)
                    {
                        sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', 'BAOCAO');";
                        condb.ExecuteNonQuery(sqlinsert_per);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }

        private void UpdateUser(string en_txtUserID, string en_txtUsername, string en_txtUserPassword)
        {
            try
            {
                string sqlupdate_user = "";
                if (cbbUserNhom.Text == "Quản trị hệ thống")
                {
                    sqlupdate_user = "UPDATE tools_tbluser SET usercode='" + en_txtUserID + "', username='" + en_txtUsername + "', userpassword='" + en_txtUserPassword + "', userstatus='0', usergnhom='1', usernote='' WHERE usercode='" + en_txtUserID + "';";
                }
                else
                {
                    sqlupdate_user = "UPDATE tools_tbluser SET usercode='" + en_txtUserID + "', username='" + en_txtUsername + "', userpassword='" + en_txtUserPassword + "', userstatus='0', usergnhom='2', usernote='Nhân viên' WHERE usercode='" + en_txtUserID + "';";
                }
                condb.ExecuteNonQuery(sqlupdate_user);
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdatePermission(string en_txtUserID)
        {
            try
            {
                string sqlupdate_per = "";
                for (int i = 0; i < lstPer.Count; i++)
                {
                    sqlupdate_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode, true);
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                    DataView dvkt = new DataView(condb.getDataTable(sqlkiemtratontai));
                    if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstPer[i].permissioncheck == false)
                        {
                            sqlupdate_per = "DELETE FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                            condb.ExecuteNonQuery(sqlupdate_per);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstPer[i].permissioncheck == true)
                        {
                            string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissionname.ToString(), true);
                            sqlupdate_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', '');";
                            condb.ExecuteNonQuery(sqlupdate_per);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdateUserDepartment(string en_txtUserID)
        {
            try
            {
                string sqlupdate_userdepartment = "";
                for (int i = 0; i < lstUserDepartment.Count; i++)
                {
                    sqlupdate_userdepartment = "";
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_departmentgroup WHERE usercode='" + en_txtUserID + "' and departmentid='" + lstUserDepartment[i].departmentid + "' ;";
                    DataView dvkt = new DataView(condb.getDataTable(sqlkiemtratontai));
                    if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstUserDepartment[i].departmentcheck == false) //xoa
                        {
                            sqlupdate_userdepartment = "DELETE FROM tools_tbluser_departmentgroup WHERE usercode='" + en_txtUserID + "' and departmentid='" + lstUserDepartment[i].departmentid + "' ;";
                            condb.ExecuteNonQuery(sqlupdate_userdepartment);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstUserDepartment[i].departmentcheck == true)
                        {
                            sqlupdate_userdepartment = "INSERT INTO tools_tbluser_departmentgroup(departmentgroupid, departmentid, departmenttype, usercode, userdepgidnote) VALUES ('" + lstUserDepartment[i].departmentgroupid + "','" + lstUserDepartment[i].departmentid + "','" + lstUserDepartment[i].departmenttype + "','" + en_txtUserID + "','');";
                            condb.ExecuteNonQuery(sqlupdate_userdepartment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdatePermissionBaoCao(string en_txtUserID)
        {
            try
            {
                string sqlupdate_per = "";
                for (int i = 0; i < lstPerBaoCao.Count; i++)
                {
                    sqlupdate_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissioncode, true);
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                    DataView dvkt = new DataView(condb.getDataTable(sqlkiemtratontai));
                    if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstPerBaoCao[i].permissioncheck == false)
                        {
                            sqlupdate_per = "DELETE FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                            condb.ExecuteNonQuery(sqlupdate_per);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstPerBaoCao[i].permissioncheck == true)
                        {
                            string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissionname.ToString(), true);
                            sqlupdate_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', 'BAOCAO');";
                            condb.ExecuteNonQuery(sqlupdate_per);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }

        #endregion
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUsername.Focus();
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUserPassword.Focus();
            }
        }

        private void txtUserPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnUserOK.PerformClick();
        }

        //Tạo Menu chức năng xóa người dùng
        private void gridViewDSUser_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
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

        // Sự kiện Click vào Menu Xóa tài khoản
        void itemXoaNguoiDung_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản: " + currentUserCode + " không?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string sqlxoatk = "DELETE FROM tools_tbluser WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlxoatk_pq = "DELETE FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa tài khoản: " + currentUserCode + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";

                    condb.ExecuteNonQuery(sqlxoatk);
                    condb.ExecuteNonQuery(sqlxoatk_pq);
                    condb.ExecuteNonQuery(sqlinsert_log);

                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Đã xóa bỏ tài khoản: " + currentUserCode;
                    gridControlDSUser.DataSource = null;
                    ucQuanLyNguoiDung_Load(null, null);
                }
                catch
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Có lỗi xảy ra";
                }
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void gridViewChucNang_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var rowHandle = gridViewChucNang.FocusedRowHandle;
                //string permissioncode = gridViewChucNang.GetRowCellValue(rowHandle, "permissioncode").ToString();
                //var phanquyen = lstPer.Where(o => o.permissioncode == permissioncode).SingleOrDefault();
                //if (phanquyen != null && gridViewChucNang.IsRowSelected(rowHandle))
                //{
                //    phanquyen.permissioncheck = true;
                //}
                //else
                //{
                //    phanquyen.permissioncheck = false;
                //}
                if (lstPer[rowHandle].permissioncheck)
                {
                    lstPer[rowHandle].permissioncheck = false;
                }
                else
                {
                    lstPer[rowHandle].permissioncheck = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Warn(ex);
            }
        }

        private void gridViewKhoaPhong_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var rowHandle = gridViewKhoaPhong.FocusedRowHandle;
                if (lstUserDepartment[rowHandle].departmentcheck)
                {
                    lstUserDepartment[rowHandle].departmentcheck = false;
                }
                else
                {
                    lstUserDepartment[rowHandle].departmentcheck = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Warn(ex);
            }
        }
        private void gridViewBaoCao_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                var rowHandle = gridViewBaoCao.FocusedRowHandle;
                if (lstPerBaoCao[rowHandle].permissioncheck)
                {
                    lstPerBaoCao[rowHandle].permissioncheck = false;
                }
                else
                {
                    lstPerBaoCao[rowHandle].permissioncheck = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Warn(ex);
            }
        }

        #region GridView Design
        private void gridViewChucNang_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewDSUser_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewChucNang_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewDSUser_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewBaoCao_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }


        #endregion



    }
}
