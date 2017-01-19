using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    internal static class KetNoiSCDLProcess
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #region Tao bang
        internal static void CreateTableTblUser()
        {
            try
            {
                // Them table: tools_tbluser
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser ( userid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, CONSTRAINT tools_tbluser_pkey PRIMARY KEY (userid));";
                condb.ExecuteNonQuery(sql_tbluser);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblPermission()
        {
            try
            {
                //Them table: tools_tbluser_permission
                string sql_tblper = "CREATE TABLE IF NOT EXISTS tools_tbluser_permission ( userpermissionid serial NOT NULL, permissionid integer, permissioncode text, permissionname text, userid integer, usercode text, permissioncheck boolean, userpermissionnote text, CONSTRAINT userpermissionid_pkey PRIMARY KEY (userpermissionid));";
                condb.ExecuteNonQuery(sql_tblper);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblDepartment()
        {
            try
            {
                //Them table: khoa/phong va cp nhat lai danh muc khoa phong
                string sql_toolsdepatment = "CREATE TABLE IF NOT EXISTS tools_depatment (tools_depatmentid serial NOT NULL, departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentid integer, departmentcode text, departmentname text, CONSTRAINT tools_depatment_pkey PRIMARY KEY (tools_depatmentid));";
                string sql_deletepatient = "DELETE FROM tools_depatment;";
                string sql_insert = "INSERT INTO tools_depatment(departmentgroupid, departmentgroupcode, departmentgroupname, departmentid, departmentcode, departmentname) SELECT departmentgroup.departmentgroupid as departmentgroupid, departmentgroup.departmentgroupcode as departmentgroupcode, departmentgroup.departmentgroupname as departmentgroupname, department.departmentid as departmentid, department.departmentcode as departmentcode, department.departmentname as departmentname FROM departmentgroup,department WHERE department.departmentgroupid = departmentgroup.departmentgroupid ;";

                condb.ExecuteNonQuery(sql_toolsdepatment);
                condb.ExecuteNonQuery(sql_deletepatient);
                condb.ExecuteNonQuery(sql_insert);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblLog()
        {
            try
            {
                //Them table: tools_tbllog
                string sql_tbllog = "CREATE TABLE IF NOT EXISTS tools_tbllog (logid serial NOT NULL,loguser text, logvalue text,ipaddress text,computername text,softversion text,logtime timestamp without time zone,CONSTRAINT tools_tbllog_pkey PRIMARY KEY (logid));";

                condb.ExecuteNonQuery(sql_tbllog);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblUpdateKhaDung()
        {
            try
            {
                string sql_tbllog_updatekhadung = "CREATE TABLE IF NOT EXISTS tools_tbllog_updatekhadung (logupdateid serial NOT NULL, tgcapnhat timestamp without time zone, khothuoc_id integer, kho_id integer, kho_ma text, kho_ten text, thuoc_id integer, thuoc_ma text, thuoc_ten text, thuoc_dvt text, slkhadung double precision, sltonkho double precision, slcapnhat double precision, trangthaicapnhat integer, gianhap text, giaban text, loguser text, CONSTRAINT tools_tbllog_updatekhadung_pkey PRIMARY KEY (logupdateid));";
                condb.ExecuteNonQuery(sql_tbllog_updatekhadung);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblServiceFull()
        {
            try
            {
                // Insert danh mục dịch vụ
                string tools_servicefull = "CREATE TABLE IF NOT EXISTS tools_servicefull (tools_servicefullid serial NOT NULL, service_groupcode text, service_bhytgroupcode text, service_code text, service_name text, service_name_nhandan text, service_name_bhyt text, service_name_nnn text, service_dvt text, service_gia text, service_gia_nhandan text,service_gia_bhyt text,service_gia_nnn text,service_lock integer, CONSTRAINT tools_servicefull_pkey PRIMARY KEY (tools_servicefullid));";
                string sql_delete = "DELETE FROM tools_servicefull;";
                string sql_insert_ser = "INSERT INTO tools_servicefull(service_groupcode, service_bhytgroupcode, service_code, service_name, service_name_nhandan, service_name_bhyt, service_name_nnn, service_dvt, service_gia, service_gia_nhandan, service_gia_bhyt, service_gia_nnn, service_lock) SELECT servicepriceref.servicepricegroupcode as service_groupcode, servicepriceref.bhyt_groupcode as service_bhytgroupcode, servicepriceref.servicepricecode as service_code, servicepriceref.servicepricename as service_name, servicepriceref.servicepricenamenhandan as service_name_nhandan, servicepriceref.servicepricenamebhyt as service_name_bhyt, servicepriceref.servicepricenamenuocngoai as service_name_nnn, servicepriceref.servicepriceunit as service_dvt, servicepriceref.servicepricefee as service_gia, servicepriceref.servicepricefeenhandan as service_gia_nhandan, servicepriceref.servicepricefeebhyt as service_gia_bhyt, servicepriceref.servicepricefeenuocngoai as service_gia_nnn, servicepriceref.servicelock as  service_lock FROM servicepriceref WHERE servicepriceref.servicepricecode <>'' ORDER BY servicepriceref.servicepricegroupcode;";
                string sql_insert_medi = "INSERT INTO tools_servicefull(service_groupcode, service_bhytgroupcode, service_code, service_name, service_name_nhandan, service_name_bhyt, service_name_nnn, service_dvt, service_gia, service_gia_nhandan, service_gia_bhyt, service_gia_nnn, service_lock) SELECT medicine_ref.medicinegroupcode as service_groupcode, medicine_ref.bhyt_groupcode as service_bhytgroupcode, medicine_ref.medicinecode as service_code, medicine_ref.medicinename as service_name, medicine_ref.medicinename as service_name_nhandan, medicine_ref.medicinename as service_name_bhyt, medicine_ref.medicinename as service_name_nnn, medicine_ref.donvitinh as service_dvt, medicine_ref.servicepricefee as service_gia, medicine_ref.servicepricefeenhandan as service_gia_nhandan, medicine_ref.servicepricefeebhyt as service_gia_bhyt, medicine_ref.servicepricefeenuocngoai as service_gia_nnn, medicine_ref.isremove as service_lock FROM medicine_ref WHERE medicine_ref.medicinecode <>'' ORDER BY medicine_ref.medicinegroupcode;";
                condb.ExecuteNonQuery(tools_servicefull);
                condb.ExecuteNonQuery(sql_delete);
                condb.ExecuteNonQuery(sql_insert_ser);
                condb.ExecuteNonQuery(sql_insert_medi);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblClients()
        {
            try
            {
                //Tao bang luu tru danh sach may tram/license
                string sql_tbltools_clients = "CREATE TABLE IF NOT EXISTS tools_clients (clientid serial NOT NULL, clientcode text NOT NULL, clientname text, clientlicense text, clientstatus integer, clientnhom integer, clientnote text, CONSTRAINT tools_client_pkey PRIMARY KEY (clientid));";

                condb.ExecuteNonQuery(sql_tbltools_clients);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableColumeBackupDichVu()
        {
            try
            {
                //Thêm cột để chứa dữ liệu backup giá dịch vụ cũ (có thể bỏ ở bản sau vì chạy đc 1 lần thôi)
                string sql_insert_colum = "ALTER TABLE ServicePriceRef ADD Tools_TGApDung_bak_1 timestamp without time zone; ALTER TABLE ServicePriceRef ADD Tools_gia_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNhanDan_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaBHYT_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNuocNgoai_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_KieuApDung_bak_1 integer DEFAULT 0;";

                condb.ExecuteNonQuery(sql_insert_colum); // có thể bỏ
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblDVKTBHYTChenh()
        {
            try
            {
                string sql_insert_dvbhyt = "CREATE TABLE IF NOT EXISTS tools_dvktbhytchenh (dvktbhytchenhid serial NOT NULL, MaDDVKT_CODE text , MaDVKT_Cu text, TenDVKT_Cu text, MaDVKTBHYT_Cu text, DonGia_Cu double precision, MaDVKT_TuongDuong text, MaDVKT_Moi text, TenDVKT_Moi text, MaDVKTBHYT_Moi text, DonGia_Moi double precision, is_lock double precision, CONSTRAINT tools_dvktbhytchenh_pkey PRIMARY KEY (dvktbhytchenhid));";

                condb.ExecuteNonQuery(sql_insert_dvbhyt);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        internal static void CreateTableTblDVKTBHYTChenhNew()
        {
            try
            {
                string sql_insert_dvbhyt = "CREATE TABLE IF NOT EXISTS tools_dvktbhytchenh_new (dvktbhytchenhnewid SERIAL NOT NULL, servicecode text, dvkt_code_cu text, dvkt_code_moi text, dvkt_ten text, dongia_cu_1 double precision, dongia_hientai  double precision, dongia_moi_2  double precision, CONSTRAINT tools_dvktbhytchenh_new_pkey PRIMARY KEY (dvktbhytchenhnewid));";

                condb.ExecuteNonQuery(sql_insert_dvbhyt);
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi khi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Cap nhat sua chua bang
        internal static void UpdateTableWithVersion()
        {
            try
            {
                //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                //string version = fvi.FileVersion;
                //if (version == "1.1.0.15")
                //{
                string sql_updateuser = "ALTER TABLE tools_tbluser ADD userhisid integer;";
                condb.ExecuteNonQuery_Error(sql_updateuser);
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Tao View

        internal static void CreateViewServicepriceDichVu()
        {
            try
            {
                string sqlViewDV = "";
                condb.ExecuteNonQuery(sqlViewDV);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Loi tao View Serviceprice Dich vu" + ex.ToString());
            }
        }
        internal static void CreateViewServicepriceThuoc()
        {
            try
            {
                string sqlViewThuoc = "";
                condb.ExecuteNonQuery(sqlViewThuoc);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Loi tao View Serviceprice Thuoc" + ex.ToString());
            }
        }
        #endregion
    }
}
