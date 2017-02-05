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
        internal static bool CreateTableTblUser()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser ( userid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, userhisid integer, CONSTRAINT tools_tbluser_pkey PRIMARY KEY (userid));";
                if (condb.ExecuteNonQuery(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblUser" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblPermission()
        {
            bool result = false;
            try
            {
                string sql_tblper = "CREATE TABLE IF NOT EXISTS tools_tbluser_permission ( userpermissionid serial NOT NULL, permissionid integer, permissioncode text, permissionname text, userid integer, usercode text, permissioncheck boolean, userpermissionnote text, CONSTRAINT userpermissionid_pkey PRIMARY KEY (userpermissionid));";
                if (condb.ExecuteNonQuery(sql_tblper))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblPermission" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblDepartment()
        {
            bool result = false;
            try
            {
                string sql_toolsdepatment = "CREATE TABLE IF NOT EXISTS tools_depatment (tools_depatmentid serial NOT NULL, departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentid integer, departmentcode text, departmentname text, CONSTRAINT tools_depatment_pkey PRIMARY KEY (tools_depatmentid));";
                string sql_deletepatient = "DELETE FROM tools_depatment;";
                string sql_insert = "INSERT INTO tools_depatment(departmentgroupid, departmentgroupcode, departmentgroupname, departmentid, departmentcode, departmentname) SELECT departmentgroup.departmentgroupid as departmentgroupid, departmentgroup.departmentgroupcode as departmentgroupcode, departmentgroup.departmentgroupname as departmentgroupname, department.departmentid as departmentid, department.departmentcode as departmentcode, department.departmentname as departmentname FROM departmentgroup,department WHERE department.departmentgroupid = departmentgroup.departmentgroupid ;";

                if (condb.ExecuteNonQuery(sql_toolsdepatment) && condb.ExecuteNonQuery(sql_deletepatient) && condb.ExecuteNonQuery(sql_insert))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblDepartment" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblLog()
        {
            bool result = false;
            try
            {
                string sql_tbllog = "CREATE TABLE IF NOT EXISTS tools_tbllog (logid serial NOT NULL,loguser text, logvalue text,ipaddress text,computername text,softversion text,logtime timestamp without time zone,CONSTRAINT tools_tbllog_pkey PRIMARY KEY (logid));";
                if (condb.ExecuteNonQuery(sql_tbllog))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblLog" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblUpdateKhaDung()
        {
            bool result = false;
            try
            {
                string sql_tbllog_updatekhadung = "CREATE TABLE IF NOT EXISTS tools_tbllog_updatekhadung (logupdateid serial NOT NULL, tgcapnhat timestamp without time zone, khothuoc_id integer, kho_id integer, kho_ma text, kho_ten text, thuoc_id integer, thuoc_ma text, thuoc_ten text, thuoc_dvt text, slkhadung double precision, sltonkho double precision, slcapnhat double precision, trangthaicapnhat integer, gianhap text, giaban text, loguser text, CONSTRAINT tools_tbllog_updatekhadung_pkey PRIMARY KEY (logupdateid));";
                if (condb.ExecuteNonQuery(sql_tbllog_updatekhadung))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblUpdateKhaDung" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblServiceFull()
        {
            bool result = false;
            try
            {
                string tools_servicefull = "CREATE TABLE IF NOT EXISTS tools_servicefull (tools_servicefullid serial NOT NULL, service_groupcode text, service_bhytgroupcode text, service_code text, service_name text, service_name_nhandan text, service_name_bhyt text, service_name_nnn text, service_dvt text, service_gia text, service_gia_nhandan text,service_gia_bhyt text,service_gia_nnn text,service_lock integer, CONSTRAINT tools_servicefull_pkey PRIMARY KEY (tools_servicefullid));";
                string sql_delete = "DELETE FROM tools_servicefull;";
                string sql_insert_ser = "INSERT INTO tools_servicefull(service_groupcode, service_bhytgroupcode, service_code, service_name, service_name_nhandan, service_name_bhyt, service_name_nnn, service_dvt, service_gia, service_gia_nhandan, service_gia_bhyt, service_gia_nnn, service_lock) SELECT servicepriceref.servicepricegroupcode as service_groupcode, servicepriceref.bhyt_groupcode as service_bhytgroupcode, servicepriceref.servicepricecode as service_code, servicepriceref.servicepricename as service_name, servicepriceref.servicepricenamenhandan as service_name_nhandan, servicepriceref.servicepricenamebhyt as service_name_bhyt, servicepriceref.servicepricenamenuocngoai as service_name_nnn, servicepriceref.servicepriceunit as service_dvt, servicepriceref.servicepricefee as service_gia, servicepriceref.servicepricefeenhandan as service_gia_nhandan, servicepriceref.servicepricefeebhyt as service_gia_bhyt, servicepriceref.servicepricefeenuocngoai as service_gia_nnn, servicepriceref.servicelock as  service_lock FROM servicepriceref WHERE servicepriceref.servicepricecode <>'' ORDER BY servicepriceref.servicepricegroupcode;";
                string sql_insert_medi = "INSERT INTO tools_servicefull(service_groupcode, service_bhytgroupcode, service_code, service_name, service_name_nhandan, service_name_bhyt, service_name_nnn, service_dvt, service_gia, service_gia_nhandan, service_gia_bhyt, service_gia_nnn, service_lock) SELECT medicine_ref.medicinegroupcode as service_groupcode, medicine_ref.bhyt_groupcode as service_bhytgroupcode, medicine_ref.medicinecode as service_code, medicine_ref.medicinename as service_name, medicine_ref.medicinename as service_name_nhandan, medicine_ref.medicinename as service_name_bhyt, medicine_ref.medicinename as service_name_nnn, medicine_ref.donvitinh as service_dvt, medicine_ref.servicepricefee as service_gia, medicine_ref.servicepricefeenhandan as service_gia_nhandan, medicine_ref.servicepricefeebhyt as service_gia_bhyt, medicine_ref.servicepricefeenuocngoai as service_gia_nnn, medicine_ref.isremove as service_lock FROM medicine_ref WHERE medicine_ref.medicinecode <>'' ORDER BY medicine_ref.medicinegroupcode;";
                if (condb.ExecuteNonQuery(tools_servicefull) && condb.ExecuteNonQuery(sql_delete) && condb.ExecuteNonQuery(sql_insert_ser) && condb.ExecuteNonQuery(sql_insert_medi))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblServiceFull" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblClients()
        {
            bool result = false;
            try
            {
                string sql_tbltools_clients = "CREATE TABLE IF NOT EXISTS tools_clients (clientid serial NOT NULL, clientcode text NOT NULL, clientname text, clientlicense text, clientstatus integer, clientnhom integer, clientnote text, CONSTRAINT tools_client_pkey PRIMARY KEY (clientid));";
                if (condb.ExecuteNonQuery(sql_tbltools_clients))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblClients" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableColumeBackupDichVu()
        {
            bool result = false;
            try
            {
                //Thêm cột để chứa dữ liệu backup giá dịch vụ cũ (có thể bỏ ở bản sau vì chạy đc 1 lần thôi)
                string sql_insert_colum = "ALTER TABLE ServicePriceRef ADD Tools_TGApDung_bak_1 timestamp without time zone; ALTER TABLE ServicePriceRef ADD Tools_gia_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNhanDan_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaBHYT_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNuocNgoai_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_KieuApDung_bak_1 integer DEFAULT 0;";

                if (condb.ExecuteNonQuery(sql_insert_colum)) // có thể bỏ
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableColumeBackupDichVu" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblDVKTBHYTChenh()
        {
            bool result = false;
            try
            {
                string sql_insert_dvbhyt = "CREATE TABLE IF NOT EXISTS tools_dvktbhytchenh (dvktbhytchenhid serial NOT NULL, MaDDVKT_CODE text , MaDVKT_Cu text, TenDVKT_Cu text, MaDVKTBHYT_Cu text, DonGia_Cu double precision, MaDVKT_TuongDuong text, MaDVKT_Moi text, TenDVKT_Moi text, MaDVKTBHYT_Moi text, DonGia_Moi double precision, is_lock double precision, CONSTRAINT tools_dvktbhytchenh_pkey PRIMARY KEY (dvktbhytchenhid));";
                condb.ExecuteNonQuery(sql_insert_dvbhyt);
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblDVKTBHYTChenh" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateTableTblDVKTBHYTChenhNew()
        {
            bool result = false;
            try
            {
                string sql_insert_dvbhyt = "CREATE TABLE IF NOT EXISTS tools_dvktbhytchenh_new (dvktbhytchenhnewid SERIAL NOT NULL, servicecode text, dvkt_code_cu text, dvkt_code_moi text, dvkt_ten text, dongia_cu_1 double precision, dongia_hientai  double precision, dongia_moi_2  double precision, CONSTRAINT tools_dvktbhytchenh_new_pkey PRIMARY KEY (dvktbhytchenhnewid));";
                if (condb.ExecuteNonQuery(sql_insert_dvbhyt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblDVKTBHYTChenhNew" + ex.ToString());
            }
            return result;
        }

        //Bao cao Quan ly tong the khoa - BỎ
        internal static bool CreateTableBCTongTheKhoa()
        {
            bool result = false;
            try
            {
                string sql_insert_bctongthe = "CREATE TABLE IF NOT EXISTS tools_bctongthekhoa(bctongthekhoaid serial NOT NULL, departmentgroupid integer, dang_dt_slbn integer,bn_chuyendi integer, bn_chuyenden integer, ravien_slbn integer, dang_dt_tiendv double precision, dang_dt_tienthuoc double precision,dang_dt_tamung double precision, ravien_chuatt_slbn integer, ravien_chuatt_tiendv double precision, ravien_chuatt_tienthuoc double precision, ravien_datt_slbn integer, ravien_datt_tiendv double precision, ravien_datt_tienthuoc double precision, bctongthekhoaid_date timestamp without time zone, CONSTRAINT tools_bctongthekhoa_pkey PRIMARY KEY (bctongthekhoaid));";
                if (condb.ExecuteNonQuery(sql_insert_bctongthe))
                {
                    result = true;
                }
                //try
                //{
                //    string sql_insert_index1 = "CREATE INDEX departmentgroupid_idx ON tools_bctongthekhoa USING btree (departmentgroupid);";
                //    condb.ExecuteNonQuery(sql_insert_index1);

                //    string sql_insert_index2 = "CREATE INDEX bctongthekhoaid_date_idx ON tools_bctongthekhoa USING btree (bctongthekhoaid_date);";
                //    condb.ExecuteNonQuery(sql_insert_index2);
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableBCTongTheKhoa" + ex.ToString());
            }
            return result;
        }

        internal static bool CreateTableBCBNDangDTTmp()
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "CREATE TABLE IF NOT EXISTS tools_bndangdt_tmp(bndangdtid serial NOT NULL, departmentgroupid integer, vienphiid integer, doituongbenhnhanid integer, serviceprice_dichvu double precision, serviceprice_thuoc double precision, tam_ung double precision, bndangdt_date timestamp without time zone, CONSTRAINT tools_bndangdt_tmp_pkey PRIMARY KEY (bndangdtid));";
                if (condb.ExecuteNonQuery(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableBCBNDangDTTmp" + ex.ToString());
            }
            return result;
        }

        internal static bool CreateTableOption()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_option(toolsoptionid serial NOT NULL, toolsoptioncode text, toolsoptionname text, toolsoptionvalue text, toolsoptionnote text, toolsoptionlook integer, toolsoptiondate timestamp without time zone, toolsoptioncreateuser text, CONSTRAINT tools_option_pkey PRIMARY KEY (toolsoptionid));";
                if (condb.ExecuteNonQuery(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableOption" + ex.ToString());
            }
            return result;
        }



        #endregion

        #region Cap nhat sua chua bang
        internal static bool UpdateTableWithVersion()
        {
            bool result = false;
            try
            {
                //System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                //string version = fvi.FileVersion;
                //if (version == "1.1.0.15")
                //{
                string sql_updateuser = "ALTER TABLE tools_tbluser ADD userhisid integer;";
                if (condb.ExecuteNonQuery_Error(sql_updateuser))
                {
                    result = true;
                }
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi UpdateTableWithVersion" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region Tao View

        internal static bool CreateViewServicepriceDichVu()
        {
            bool result = false;
            try
            {
                string sqlViewDV = "CREATE OR REPLACE VIEW serviceprice_dichvu AS SELECT ser.servicepriceid, ser.medicalrecordid, ser.vienphiid, ser.hosobenhanid, ser.maubenhphamid, ser.maubenhphamphieutype, ser.servicepriceid_master, ser.doituongbenhnhanid, ser.servicepricedate,ser.servicepricecode, ser.servicepricename, ser.loaidoituong, ser.departmentgroupid, ser.departmentid,case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt * ser.soluong when 4 then ser.servicepricemoney_bhyt * ser.soluong when 6 then ser.servicepricemoney_bhyt * ser.soluong else 0 end as sotien_bhyt,case ser.loaidoituong when 1 then ser.servicepricemoney_nhandan * ser.soluong when 2 then ser.servicepricemoney * ser.soluong when 3 then ser.servicepricemoney * ser.soluong when 4 then (ser.servicepricemoney-ser.servicepricemoney_bhyt) * ser.soluong else 0 end as sotien_nhandan,(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt * ser.soluong when 4 then ser.servicepricemoney_bhyt * ser.soluong when 6 then ser.servicepricemoney_bhyt * ser.soluong else 0 end) + (case ser.loaidoituong when 1 then ser.servicepricemoney_nhandan * ser.soluong when 2 then ser.servicepricemoney * ser.soluong when 3 then ser.servicepricemoney * ser.soluong when 4 then (ser.servicepricemoney-ser.servicepricemoney_bhyt) * ser.soluong else 0 end) as sotien_tong FROM serviceprice ser WHERE ser.bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu')and ser.servicepricedate>'2016-01-01'ORDER BY ser.servicepriceid desc;";
                if (condb.ExecuteNonQuery(sqlViewDV))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Loi tao View Serviceprice Dich vu" + ex.ToString());
            }
            return result;
        }
        internal static bool CreateViewServicepriceThuoc()
        {
            bool result = false;
            try
            {
                string sqlViewThuoc = "CREATE OR REPLACE VIEW serviceprice_thuoc AS SELECT ser.servicepriceid, ser.medicalrecordid, ser.vienphiid, ser.hosobenhanid, ser.maubenhphamid, ser.maubenhphamphieutype, ser.servicepriceid_master, ser.doituongbenhnhanid, ser.servicepricedate,ser.servicepricecode, ser.servicepricename, ser.loaidoituong, ser.departmentgroupid, ser.departmentid, case ser.maubenhphamphieutype when 0 then (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt * ser.soluong when 4 then ser.servicepricemoney_bhyt * ser.soluong when 6 then ser.servicepricemoney_bhyt * ser.soluong else 0 end) else (case ser.loaidoituong when 0 then 0 - (ser.servicepricemoney_bhyt * ser.soluong) when 4 then 0 - (ser.servicepricemoney_bhyt * ser.soluong) when 6 then 0 - (ser.servicepricemoney_bhyt * ser.soluong) else 0 end) end as sotien_bhyt, case ser.maubenhphamphieutype when 0 then (case ser.loaidoituong when 1 then ser.servicepricemoney_nhandan * ser.soluong when 2 then ser.servicepricemoney * ser.soluong when 3 then ser.servicepricemoney * ser.soluong when 4 then (ser.servicepricemoney-ser.servicepricemoney_bhyt) * ser.soluong else 0 end) else (case ser.loaidoituong when 1 then 0 - (ser.servicepricemoney_nhandan * ser.soluong) when 2 then 0 - (ser.servicepricemoney * ser.soluong) when 3 then 0 - (ser.servicepricemoney * ser.soluong) when 4 then 0 - ((ser.servicepricemoney-ser.servicepricemoney_bhyt) * ser.soluong) else 0 end) end as sotien_nhandan, (case ser.maubenhphamphieutype when 0 then (case ser.loaidoituong  when 0 then ser.servicepricemoney_bhyt * ser.soluong  when 4 then ser.servicepricemoney_bhyt * ser.soluong  when 6 then ser.servicepricemoney_bhyt * ser.soluong  else 0 end) else (case ser.loaidoituong  when 0 then 0 - (ser.servicepricemoney_bhyt * ser.soluong)  when 4 then 0 - (ser.servicepricemoney_bhyt * ser.soluong)  when 6 then 0 - (ser.servicepricemoney_bhyt * ser.soluong)  else 0 end) end) + (case ser.maubenhphamphieutype when 0 then (case ser.loaidoituong  when 1 then ser.servicepricemoney_nhandan * ser.soluong  when 2 then ser.servicepricemoney * ser.soluong  when 3 then ser.servicepricemoney * ser.soluong  when 4 then (ser.servicepricemoney-ser.servicepricemoney_bhyt) * ser.soluong  else 0 end) else (case ser.loaidoituong  when 1 then 0 - (ser.servicepricemoney_nhandan * ser.soluong)  when 2 then 0 - (ser.servicepricemoney * ser.soluong)  when 3 then 0 - (ser.servicepricemoney * ser.soluong)  when 4 then 0 - ((ser.servicepricemoney-ser.servicepricemoney_bhyt) * ser.soluong)  else 0 end) end) as sotien_tong FROM serviceprice ser WHERE ser.bhyt_groupcode in ('091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','09TDT','08MA') and ser.servicepricedate>'2016-01-01'ORDER BY ser.servicepriceid desc;";
                if (condb.ExecuteNonQuery(sqlViewThuoc))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Loi tao View Serviceprice Thuoc" + ex.ToString());
            }
            return result;
        }
        #endregion
    }
}
