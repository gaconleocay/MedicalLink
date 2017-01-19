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

namespace MedicalLink.FormCommon
{
    public partial class frmThayPass : Form
    {
        public string serverhost = ConfigurationManager.AppSettings["ServerHost"].ToString();
        public string serveruser = ConfigurationManager.AppSettings["Username"].ToString();
        public string serverpass = ConfigurationManager.AppSettings["Password"].ToString();
        public string serverdb = ConfigurationManager.AppSettings["Database"].ToString();
        DataTable dt;
        public frmThayPass()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtPasswordOld.Text == "" || txtPasswordNew1.Text == "" || txtPasswordNew2.Text == "")
                MessageBox.Show("Xin vui lòng nhập đầy đủ thông tin");
            else if (txtPasswordNew1.Text == "") MessageBox.Show("Bạn chưa nhập mật khẩu mới");
            else if (txtPasswordNew2.Text == "") MessageBox.Show("Bạn chưa nhập lại mật khẩu mới");
            else if (txtPasswordNew1.Text != txtPasswordNew2.Text) MessageBox.Show("Mật khẩu mới của bạn không trùng khớp");
            else
            {
                // Thực hiện đổi pass
                try
                {
                    NpgsqlConnection conn = new NpgsqlConnection("Server=" + serverhost + ";User Id=" + serveruser + "; " +
"Password=" + serverpass + ";Database=" + serverdb + ";");
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    // Querry lấy dữ liệu về bn có VP nhập vào
                    string sqlquerry = "select * from tools_tbluser where usercode='" + SessionLogin.SessionUsercode + "' and userpassword='" + txtPasswordOld.Text + "'";
                    NpgsqlCommand command = new NpgsqlCommand(sqlquerry, conn);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0 && txtPasswordNew1.Text == txtPasswordNew2.Text)
                    {
                        string sqlupdate = "update tools_tbluser set userpassword='" + txtPasswordNew1.Text + "' where usercode='" + SessionLogin.SessionUsercode + "'";
                        NpgsqlCommand command1 = new NpgsqlCommand(sqlupdate, conn);
                        command1.CommandType = CommandType.Text;
                        command1.ExecuteNonQuery();
                        MessageBox.Show("Thay đổi mật khẩu thành công !");
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Sai mật khẩu cũ !");
                    }
                    conn.Close();
                    //gridControlMoBenhAn.DataSource = dt;
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra");
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
            if(checkEditHienThi.Checked==true)
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
