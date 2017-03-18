using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using MedicalLink.ClassCommon;

namespace MedicalLink.Base
{
    public class ConnectDatabase
    {
        public string serverhost = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
        public string serveruser = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
        public string serverpass = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
        public string serverdb = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);

        NpgsqlConnection conn;
        //private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private bool kiemtraketnoi = false;

        // Mở kết nối
        public void connect()
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
               // MessageBox.Show("Không kết nối được cơ sở dữ liệu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
        }

        // Đóng kết nối
        public void disconnect()
        {
            try
            {
                if ((conn != null) && (conn.State == ConnectionState.Open))
                    conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                MessageBox.Show("Có lỗi khi đóng kết nối đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.Error("Loi dong ket noi den CSDL: " + ex.ToString());
            }
        }

        // trả về một DataTable
        public DataTable getDataTable(string sql)
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
                Logging.Error("Loi getDataTable: " + ex.ToString());
            }
            return dt;
        }

        // thực thi câu lệnh truy vấn insert,delete,update
        public bool ExecuteNonQuery(string sql)
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
                MessageBox.Show("Có lỗi khi thực thi đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.Error("Loi ExecuteNonQuery: " + ex.ToString());
            }
            return result;
        }

        public bool ExecuteNonQuery_Error(string sql)
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
                Logging.Error("Loi ExecuteNonQuery_Error: " + ex.ToString());
            }
            return result;
        }



        // trả về DataReader
        public NpgsqlDataReader getDataReader(string sql)
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
                Logging.Error("Loi get dataReader ve: " + ex.ToString());
                return null;
            }

        }
    }
}

// daolekwan.wordpress.com/2013/06/10/cclass-ket-noi-trong-c/