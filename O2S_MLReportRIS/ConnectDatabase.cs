using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MLReportRIS
{
    public class ConnectDatabase
    {
        #region Khai bao
        private string serverhost =O2S_Common.EncryptAndDecrypt.MD5EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
        private string serveruser = O2S_Common.EncryptAndDecrypt.MD5EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
        private string serverpass = O2S_Common.EncryptAndDecrypt.MD5EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
        private string serverdb = O2S_Common.EncryptAndDecrypt.MD5EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);

        NpgsqlConnection conn;
        NpgsqlConnection conn_MeL;
        private bool kiemtraketnoi = false;
        #endregion

        //#region Database HIS 111
        //public DataTable GetDataTable_HIS(string sql)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        O2S_License.DAL.PostgreSQL.ConnectDatabase.ParamDB1 = new O2S_License.DTO.PostgreSQLParamDTO();
        //        O2S_License.DAL.PostgreSQL.ConnectDatabase.ParamDB1.Server = serverhost;
        //        O2S_License.DAL.PostgreSQL.ConnectDatabase.ParamDB1.UserId = serveruser;
        //        O2S_License.DAL.PostgreSQL.ConnectDatabase.ParamDB1.Port = 5432;
        //        O2S_License.DAL.PostgreSQL.ConnectDatabase.ParamDB1.Password = serverpass;
        //        O2S_License.DAL.PostgreSQL.ConnectDatabase.ParamDB1.Database = serverdb;
        //        dt = O2S_License.DAL.PostgreSQL.ConnectDatabase.GetDataTable(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Error("Loi getDataTable: " + ex.ToString());
        //    }
        //    return dt;
        //}
        //public bool ExecuteNonQuery_HIS(string sql)
        //{
        //    bool result = false;
        //    try
        //    {
        //        //Connect();
        //        //if (kiemtraketnoi == true)
        //        //{
        //        //    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
        //        //    cmd.ExecuteNonQuery();
        //        //    Disconnect();
        //        //    result = true;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery: " + ex.ToString() + " \nSQL=[" + sql + "]");
        //    }
        //    return result;
        //}

        //#endregion



        #region Database HIS
        public void Connect()
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
                //Logging.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
        }
        public void Disconnect()
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
                // Logging.Error("Loi dong ket noi den CSDL: " + ex.ToString());
            }
        }
        public DataTable GetDataTable_HIS(string sql)
        {
            Connect();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            try
            {
                if (kiemtraketnoi == true)
                {
                    da.Fill(dt);
                    Disconnect();
                }
            }
            catch (Exception ex)
            {
                // Logging.Error("Loi getDataTable: " + ex.ToString());
            }
            return dt;
        }
        public bool ExecuteNonQuery_HIS(string sql)
        {
            bool result = false;
            try
            {
                Connect();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    Disconnect();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery: " + ex.ToString() + " \nSQL=[" + sql + "]");
            }
            return result;
        }
        public bool ExecuteNonQuery_Error_HIS(string sql)
        {
            bool result = false;
            try
            {
                Connect();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    Disconnect();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery_Error_HIS: " + ex.ToString() + " \nSQL=[" + sql + "]");
            }
            return result;
        }
        public NpgsqlDataReader getDataReader(string sql)
        {
            try
            {
                Connect();
                NpgsqlCommand com = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = com.ExecuteReader();
                //dr.Read();
                Disconnect();
                return dr;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error("Loi getDataReader: " + ex.ToString() + " \nSQL=[" + sql + "]");
                return null;
            }

        }

        #endregion
    }
}
