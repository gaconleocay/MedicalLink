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

namespace MedicalLink.HeThong
{
    public partial class ucQuanLyNguoiDung : UserControl
    {
        //Sử dụng  lớp kết nối.Ta khai báo một đối tượng thuộc lớp kết nối ConnectDatabase
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string codeid;
        string name;
        string pass;
        string gnhom;
        string de_pass;
        string en_permissioncode;
        string en_permissionname;
        List<MedicalLink.ClassCommon.classPermission> lstPer;

        public ucQuanLyNguoiDung()
        {
            InitializeComponent();
            btnUserOK.Enabled = false;
            txtUserID.Enabled = false;
            txtUsername.Enabled = false;
            txtUserPassword.Enabled = false;
            cbbUserNhom.Enabled = false;
        }

        // Load dữ liệu danh sách người dùng
        private void ucQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                //Sử dụng phương thức của lớp kết nối để load dữ liệu lên DataGridView
                string sql = "select usercode, username, userpassword, case usergnhom when '0' then 'Admin' when '1' then 'Quản trị hệ thống' when 2 then 'Nhân viên' end as usergnhom from tools_tbluser where usergnhom in (1,2) order by usercode";
                DataView dv = new DataView(condb.getDataTable(sql));

                if (dv.Count > 0)
                {
                    //Giải mã hiển thị lên Gridview
                    for (int i = 0; i < dv.Count; i++)
                    {
                        //itemcode += dataGridView1.Rows[i].Cells[2].Value.ToString();
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
                MessageBox.Show(ex.ToString());
            }

        }

        // Khi click vào tên người dùng thì hiển thị lên textbox thông tin
        private void gridControlDSUser_Click(object sender, EventArgs e)
        {
            //string de_permissioncode;
            var rowHandle = gridViewDSUser.FocusedRowHandle;
            try
            {
                codeid = gridViewDSUser.GetRowCellValue(rowHandle, "usercode").ToString();
                gnhom = gridViewDSUser.GetRowCellValue(rowHandle, "usergnhom").ToString();
                name = gridViewDSUser.GetRowCellValue(rowHandle, "username").ToString();
                pass = gridViewDSUser.GetRowCellValue(rowHandle, "userpassword").ToString();
                de_pass = MedicalLink.Base.EncryptAndDecrypt.Decrypt(pass, true);
                string en_codeid = MedicalLink.Base.EncryptAndDecrypt.Encrypt(codeid, true);

                //txtUserID.Enabled = true;
                txtUserID.Enabled = true;
                txtUsername.Enabled = true;
                txtUserPassword.Enabled = true;
                cbbUserNhom.Enabled = true;
                btnUserOK.Enabled = true;
                txtUserID.Text = codeid;
                txtUsername.Text = name;
                txtUserPassword.Text = de_pass;
                cbbUserNhom.Text = gnhom.ToString();

                // Khi click vào tài khoản thì hiển thị bảng phân quyền
                gridControlChucNang.DataSource = null;
                gridControlChucNang_Data(null, null);
                string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + en_codeid + "';";
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

        //Khi ấn nút thêm thì hiển thị textbox
        private void btnUserThem_Click(object sender, EventArgs e)
        {
            txtUserID.ResetText();
            txtUsername.ResetText();
            txtUserPassword.ResetText();
            txtUserID.Enabled = true;
            txtUsername.Enabled = true;
            txtUserPassword.Enabled = true;
            cbbUserNhom.Enabled = true;
            cbbUserNhom.Text = "Nhân viên";
            txtUserID.Focus();
            btnUserOK.Enabled = true;
            // Load bang phan quyen
            gridControlChucNang.DataSource = null;
            gridControlChucNang_Data(null, null);
        }

        // Tạo sự kiện khi kích OK
        private void btnUserOK_Click(object sender, EventArgs e)
        {
            // Mã hóa tài khoản
            string en_txtUserID = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUserID.Text.Trim(), true);
            string en_txtUsername = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
            string en_txtUserPassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUserPassword.Text.Trim(), true);
            string sqlinsert_per;
            string sqlupdate_per;
            string sqlinsert_user;
            string sqlupdate_user;

            try
            {
                if (txtUserID.Text != codeid) // Nếu mã tài khoản khác tài khoản của row thì...
                {
                    try
                    {
                        // Kiểm tra xem trong DB có mã User này chưa ?
                        string sqlkiemtra = "SELECT * FROM tools_tbluser WHERE usercode='" + en_txtUserID + "';";
                        DataView dv = new DataView(condb.getDataTable(sqlkiemtra));
                        if (dv.Count > 0) // Kiểm tra nếu trong DB đã tồn tại mã user
                        {
                            timerThongBao.Start();
                            lblThongBao.Visible = true;
                            lblThongBao.Text = "Tên người dùng đã tồn tại";
                            txtUserID.Focus();
                        }
                        else //Nếu chưa tồn tại mã user thì cho thêm mới
                        {
                            // usergnhom=1: Quan tri he thong
                            // usergnhom=2: User
                            // usergnhom=99: key license

                            // Insert user quyền IT.
                            if (cbbUserNhom.Text == "Quản trị hệ thống")
                            {
                                sqlinsert_user = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + en_txtUserID + "','" + en_txtUsername + "','" + en_txtUserPassword + "','0','1','');";
                            }
                            else // insert quyền nhân viên
                            {
                                sqlinsert_user = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + en_txtUserID + "','" + en_txtUsername + "','" + en_txtUserPassword + "','0','2','Nhân viên');";
                            }
                            // Thực thi lệnh thêm mới user
                            condb.ExecuteNonQuery(sqlinsert_user);

                            //Insert table phan quyen
                            for (int i = 0; i < lstPer.Count; i++)
                            {
                                //Mã hóa thông tin phân quyền
                                en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode.ToString(), true);
                                en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissionname.ToString(), true);

                                if (lstPer[i].permissioncheck == true)
                                {
                                    sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', '');";
                                }
                                else
                                {
                                    sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'false', '');";
                                }
                                condb.ExecuteNonQuery(sqlinsert_per);
                            }

                            gridControlDSUser.DataSource = null;
                            ucQuanLyNguoiDung_Load(null, null);
                            timerThongBao.Start();
                            lblThongBao.Visible = true;
                            lblThongBao.Text = "Thêm mới thành công";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else //Update 
                {
                    try
                    {
                        if (cbbUserNhom.Text == "Quản trị hệ thống")
                        {
                            sqlupdate_user = "UPDATE tools_tbluser SET usercode='" + en_txtUserID + "', username='" + en_txtUsername + "', userpassword='" + en_txtUserPassword + "', userstatus='0', usergnhom='1', usernote='' WHERE usercode='" + en_txtUserID + "';";
                        }
                        else // Update quyền nhân viên
                        {
                            sqlupdate_user = "UPDATE tools_tbluser SET usercode='" + en_txtUserID + "', username='" + en_txtUsername + "', userpassword='" + en_txtUserPassword + "', userstatus='0', usergnhom='2', usernote='Nhân viên' WHERE usercode='" + en_txtUserID + "';";
                        }
                        //Thực thi câu lệnh Update user
                        condb.ExecuteNonQuery(sqlupdate_user);

                        // Update phân quyền
                        for (int i = 0; i < lstPer.Count; i++)
                        {
                            string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode, true);
                            string sqlkiemtratontai = "SELECT * FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                            DataView dvkt = new DataView(condb.getDataTable(sqlkiemtratontai));
                            if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                            {
                                if (lstPer[i].permissioncheck == true)
                                {
                                    sqlupdate_per = "UPDATE tools_tbluser_permission SET permissioncheck='true' WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                                }
                                else
                                {
                                    sqlupdate_per = "UPDATE tools_tbluser_permission SET permissioncheck='false' WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                                }
                                condb.ExecuteNonQuery(sqlupdate_per);
                            }
                            else //nếu không có quyền đó thì Insert
                            {
                                //Mã hóa thông tin phân quyền
                                en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode.ToString(), true);
                                en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissionname.ToString(), true);

                                if (lstPer[i].permissioncheck == true)
                                {
                                    sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', '');";
                                }
                                else
                                {
                                    sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'false', '');";
                                }
                                condb.ExecuteNonQuery(sqlinsert_per);
                            }
                        }

                        gridControlDSUser.DataSource = null;
                        ucQuanLyNguoiDung_Load(null, null);
                        timerThongBao.Start();
                        lblThongBao.Visible = true;
                        lblThongBao.Text = "Cập nhật thành công";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra", "Thông báo");
            }
        }

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

        private void gridViewDSUser_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        //Tạo Mune chức năng xóa người dùng
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
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản: " + codeid + " không?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string sqlxoatk = "DELETE FROM tools_tbluser WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(codeid.ToString(), true) + "';";
                    string sqlxoatk_pq = "DELETE FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(codeid.ToString(), true) + "';";
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa tài khoản: " + codeid + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";

                    condb.ExecuteNonQuery(sqlxoatk);
                    condb.ExecuteNonQuery(sqlxoatk_pq);
                    condb.ExecuteNonQuery(sqlinsert_log);

                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Đã xóa bỏ tài khoản: " + codeid;
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

        // Add chức năng cho bảng phân quyền
        #region Add phan quyen
        private void gridControlChucNang_Data(object sender, EventArgs e)
        {
            try
            {
                // lstPer: lưu trữ danh sách phân quyền
                lstPer = new List<ClassCommon.classPermission>();
                lstPer = MedicalLink.Base.listChucNang.getDanhSachChucNang();

                gridControlChucNang.DataSource = lstPer;
                gridControlChucNang.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void gridViewChucNang_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

    }
}
