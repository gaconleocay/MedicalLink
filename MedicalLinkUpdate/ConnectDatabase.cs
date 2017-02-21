using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLinkUpdate
{
    class ConnectDatabase
    {
        private string serverhost = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
        private string serveruser = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
        private string serverpass = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
        private string serverdb = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);

        NpgsqlConnection conn;
        //internal DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private bool kiemtraketnoi = false;

        // Mở kết nối
        internal void connect()
        {
            try
            {
                if (conn == null)
                    conn = new NpgsqlConnection("Server=" + serverhost + ";Port=5432;User Id=" + serveruser + "; " + "Password=" + serverpass + ";Database=" + serverdb + ";CommandTimeout=1800000;");
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                kiemtraketnoi = true;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                MessageBox.Show("Không kết nối được cơ sở dữ liệu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Đóng kết nối
        internal void disconnect()
        {
            try
            {
                if ((conn != null) && (conn.State == ConnectionState.Open))
                    conn.Close();
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                MessageBox.Show("Có lỗi khi đóng kết nối đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // trả về một DataTable
        internal DataTable getDataTable(string sql)
        {
            connect();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            try
            {
                if (kiemtraketnoi == true)
                {
                    da.Fill(dt);
                    disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi dữ liệu đầu vào" + ex.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // thực thi câu lệnh truy vấn insert,delete,update
        internal bool ExecuteNonQuery(string sql)
        {
            bool result = false;
            try
            {
                connect();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    disconnect();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        internal bool ExecuteNonQuery_Error(string sql)
        {
            bool result = false;
            try
            {
                connect();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    disconnect();
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        // trả về DataReader
        internal NpgsqlDataReader getDataReader(string sql)
        {
            try
            {
                connect();
                NpgsqlCommand com = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = com.ExecuteReader();
                //dr.Read();
                disconnect();
                return dr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
