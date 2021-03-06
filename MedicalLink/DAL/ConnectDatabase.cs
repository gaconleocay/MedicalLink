﻿using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using MedicalLink.Base;

namespace MedicalLink.DAL
{
    public class ConnectDatabase
    {
        #region Khai bao
        private string serverhost = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
        private string serveruser = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
        private string serverpass = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
        private string serverdb = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);
        private string serverhost_MeL = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost_MeL"].ToString().Trim() ?? "", true);
        private string serveruser_MeL = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username_MeL"].ToString().Trim(), true);
        private string serverpass_MeL = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password_MeL"].ToString().Trim(), true);
        private string serverdb_MeL = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database_MeL"].ToString().Trim(), true);
        private string serverdb_Phr = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database_Phr"].ToString().Trim(), true);
        NpgsqlConnection conn;
        NpgsqlConnection conn_MeL;
        NpgsqlConnection conn_Phr;
        private bool kiemtraketnoi = false;

        #endregion

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
                O2S_Common.Logging.LogSystem.Error("Loi ket noi den CSDL: " + ex.ToString());
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
                MessageBox.Show("Có lỗi khi đóng kết nối đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi dong ket noi den CSDL: " + ex.ToString());
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
                MessageBox.Show("Có lỗi dữ liệu đầu vào" + ex.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi getDataTable: " + ex.ToString());
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
                MessageBox.Show("Có lỗi khi thực thi đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery: " + ex.ToString() + " \nSQL=["+ sql + "]");
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
                O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery_Error: " + ex.ToString());
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
                O2S_Common.Logging.LogSystem.Error("Loi get dataReader ve: " + ex.ToString());
                return null;
            }

        }

        #endregion

        #region Database MediLink
        public void Connect_MeL()
        {
            try
            {
                if (conn_MeL == null)
                    conn_MeL = new NpgsqlConnection("Server=" + serverhost_MeL + ";Port=5432;User Id=" + serveruser_MeL + "; " + "Password=" + serverpass_MeL + ";Database=" + serverdb_MeL + ";CommandTimeout=1800000;");
                if (conn_MeL.State == ConnectionState.Closed)
                    conn_MeL.Open();
                kiemtraketnoi = true;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                O2S_Common.Logging.LogSystem.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
        }
        public void Disconnect_MeL()
        {
            try
            {
                if ((conn_MeL != null) && (conn_MeL.State == ConnectionState.Open))
                    conn_MeL.Close();
                conn_MeL.Dispose();
                conn_MeL = null;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                O2S_Common.Logging.LogSystem.Error("Loi dong ket noi den CSDL: " + ex.ToString());
            }
        }
        public DataTable GetDataTable_MeL(string sql)
        {
            Connect_MeL();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn_MeL);
            DataTable dt = new DataTable();
            try
            {
                if (kiemtraketnoi == true)
                {
                    da.Fill(dt);
                    Disconnect_MeL();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi dữ liệu đầu vào" + ex.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi getDataTable: " + ex.ToString());
            }
            return dt;
        }
        public bool ExecuteNonQuery_MeL(string sql)
        {
            bool result = false;
            try
            {
                Connect_MeL();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn_MeL);
                    cmd.ExecuteNonQuery();
                    Disconnect_MeL();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thực thi đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery: " + ex.ToString() + " \nSQL=[" + sql + "]");
            }
            return result;
        }

        #endregion
        #region Database MediLink
        public void Connect_Phr()
        {
            try
            {
                if (conn_Phr == null)
                    conn_Phr = new NpgsqlConnection("Server=" + serverhost_MeL + ";Port=5432;User Id=" + serveruser_MeL + "; " + "Password=" + serverpass_MeL + ";Database=" + serverdb_Phr + ";CommandTimeout=1800000;");
                if (conn_Phr.State == ConnectionState.Closed)
                    conn_Phr.Open();
                kiemtraketnoi = true;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                O2S_Common.Logging.LogSystem.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
        }
        public void Disconnect_Phr()
        {
            try
            {
                if ((conn_Phr != null) && (conn_Phr.State == ConnectionState.Open))
                    conn_Phr.Close();
                conn_Phr.Dispose();
                conn_Phr = null;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                O2S_Common.Logging.LogSystem.Error("Loi dong ket noi den CSDL: " + ex.ToString());
            }
        }
        public DataTable GetDataTable_Phr(string sql)
        {
            Connect_Phr();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn_Phr);
            DataTable dt = new DataTable();
            try
            {
                if (kiemtraketnoi == true)
                {
                    da.Fill(dt);
                    Disconnect_Phr();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi dữ liệu đầu vào" + ex.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi getDataTable: " + ex.ToString());
            }
            return dt;
        }
        public bool ExecuteNonQuery_Phr(string sql)
        {
            bool result = false;
            try
            {
                Connect_Phr();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn_Phr);
                    cmd.ExecuteNonQuery();
                    Disconnect_Phr();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thực thi đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                O2S_Common.Logging.LogSystem.Error("Loi ExecuteNonQuery: " + ex.ToString() + " \nSQL=[" + sql + "]");
            }
            return result;
        }

        #endregion

        #region Sử dụng DB_LINK để kết nối MeL sang HIS
        public DataTable GetDataTable_MeLToHIS(string sql)
        {
            DataTable result = new DataTable();
            try
            {
                //dblink_connect
                Execute_MeL_Connect_HIS();
                //Chay SQL thuc thi
                result = GetDataTable_MeL(sql);
                //Disconnect
                Execute_MeL_Disconnect_HIS();
            }
            catch (Exception ex)
            {
                Execute_MeL_Disconnect_HIS();
                Execute_MeL_Connect_HIS();
                result = GetDataTable_MeL(sql);
                Execute_MeL_Disconnect_HIS();
            }
            return result;
        }
        public bool ExecuteNonQuery_MeLToHIS(string sql)
        {
            bool result = false;
            try
            {
                //dblink_connect
                Execute_MeL_Connect_HIS();
                //Chay SQL thuc thi
                result = ExecuteNonQuery_MeL(sql);
                //Disconnect
                Execute_MeL_Disconnect_HIS();
            }
            catch (Exception ex)
            {
                //Logging.Error("Loi getDataTable Dblink: " + ex.ToString());
                Execute_MeL_Disconnect_HIS();
                Execute_MeL_Connect_HIS();
                result = ExecuteNonQuery_MeL(sql);
                Execute_MeL_Disconnect_HIS();
            }
            return result;
        }

        public void Execute_MeL_Connect_HIS()
        {
            try
            {
                string dblink_connect = "SELECT dblink_connect('myconn', 'dbname=" + serverdb + " port=5432 host=" + serverhost + " user=" + serveruser + " password=" + serverpass + "');";
                GetDataTable_MeL(dblink_connect);
            }
            catch (Exception)
            {
            }
        }
        public void Execute_MeL_Disconnect_HIS()
        {
            try
            {
                string dblink_dis = "SELECT dblink_disconnect('myconn');";
                GetDataTable_MeL(dblink_dis);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Sử dụng DB_LINK Kết nối từ DB HIS sang DB MedicalLink
        public DataTable GetDataTable_HISToMeL(string sql)
        {
            DataTable result = new DataTable();
            try
            {
                //dblink_connect
                Execute_HIS_Connect_MeL();
                //Chay SQL thuc thi
                result = GetDataTable_HIS(sql);
                //Disconnect
                Execute_HIS_Disconnect_MeL();
            }
            catch (Exception ex)
            {
                Execute_HIS_Disconnect_MeL();
                Execute_HIS_Connect_MeL();
                result = GetDataTable_HIS(sql);
                Execute_HIS_Disconnect_MeL();
                O2S_Common.Logging.LogSystem.Error("Loi GetDataTable_Dblink_MeL: " + ex.ToString());
            }
            return result;
        }
        //public bool ExecuteNonQuery_Dblink_MeL(string sql)
        //{
        //    bool result = false;
        //    try
        //    {
        //        //dblink_connect
        //        Execute_Dblink_Connect_HIS();
        //        //Chay SQL thuc thi
        //        result = ExecuteNonQuery_MeL(sql);
        //        //Disconnect
        //        Execute_Dblink_Disconnect_HIS();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Common.Logging.LogSystem.Error("Loi getDataTable Dblink: " + ex.ToString());
        //        Execute_Dblink_Disconnect_HIS();
        //        Execute_Dblink_Connect_HIS();
        //        result = ExecuteNonQuery_MeL(sql);
        //        Execute_Dblink_Disconnect_HIS();
        //    }
        //    return result;
        //}
        public void Execute_HIS_Connect_MeL()
        {
            try
            {
                string dblink_connect = "SELECT dblink_connect('myconn_mel', 'dbname=" + serverdb_MeL + " port=5432 host=" + serverhost_MeL + " user=" + serveruser_MeL + " password=" + serverpass_MeL + "');";
                GetDataTable_HIS(dblink_connect);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error("Loi Execute_Dblink_Connect_MeL: " + ex.ToString());
            }
        }
        public void Execute_HIS_Disconnect_MeL()
        {
            try
            {
                string dblink_dis = "SELECT dblink_disconnect('myconn_mel');";
                GetDataTable_HIS(dblink_dis);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error("Loi Execute_Dblink_Disconnect_MeL: " + ex.ToString());
            }
        }
        #endregion


    }
}

// daolekwan.wordpress.com/2013/06/10/cclass-ket-noi-trong-c/