using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabTrangChu
{
    public partial class frmThayPass : Form
    {
        public string serverhost = ConfigurationManager.AppSettings["ServerHost"].ToString();
        public string serveruser = ConfigurationManager.AppSettings["Username"].ToString();
        public string serverpass = ConfigurationManager.AppSettings["Password"].ToString();
        public string serverdb = ConfigurationManager.AppSettings["Database"].ToString();
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public frmThayPass()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtPasswordOld.Text == "" || txtPasswordNew1.Text == "" || txtPasswordNew2.Text == "")
                MessageBox.Show("Xin vui lòng nhập đầy đủ thông tin.", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtPasswordNew1.Text == "") MessageBox.Show("Bạn chưa nhập mật khẩu mới", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtPasswordNew2.Text == "") MessageBox.Show("Bạn chưa nhập lại mật khẩu mới.", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtPasswordNew1.Text != txtPasswordNew2.Text) MessageBox.Show("Mật khẩu mới của bạn không trùng khớp.", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                try
                {
                    string en_txtUserID = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    string en_txtUserPasswordOld = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPasswordOld.Text.Trim(), true);
                    string en_txtUserPasswordNew = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPasswordNew1.Text.Trim(), true);

                    string sqlquerry = "select * from tools_tbluser where usercode='" + en_txtUserID + "' and userpassword='" + en_txtUserPasswordOld + "'";
                    DataView dataBC = new DataView(condb.getDataTable(sqlquerry));

                    if (dataBC.Count > 0 && txtPasswordNew1.Text == txtPasswordNew2.Text)
                    {
                        string sqlupdate_user = "UPDATE tools_tbluser SET userpassword='" + en_txtUserPasswordNew + "' WHERE usercode='" + en_txtUserID + "';";
                       if (condb.ExecuteNonQuery(sqlupdate_user))
                        {
                            MessageBox.Show("Thay đổi mật khẩu thành công.", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sai mật khẩu cũ.", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Base.Logging.Error(ex);
                }

            }
        }


        private void txtPasswordOld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPasswordNew1.Focus();
            }
        }

        private void txtPasswordNew1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPasswordNew2.Focus();
            }
        }

        private void txtPasswordNew2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnThayDoi.Focus();
            }
        }

        // bắt sự kiện khi check vào nút hiển thị mật khẩu
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditHienThi.Checked == true)
            {
                txtPasswordNew1.Properties.PasswordChar = '\0';
                txtPasswordNew2.Properties.PasswordChar = '\0';
            }
            else
            {
                txtPasswordNew1.Properties.PasswordChar = '*';
                txtPasswordNew2.Properties.PasswordChar = '*';
            }
        }

        private void frmThayPass_Load(object sender, EventArgs e)
        {
            lblTenUserDN.Text = SessionLogin.SessionUsercode;
        }


    }
}
