using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    internal static class KetNoiSCDLProcess
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        internal static bool CapNhatCoSoDuLieu()
        {
            bool result = true;
            try
            {
                result = KetNoiSCDLProcess.CreateTableTblUser();
                result = KetNoiSCDLProcess.CreateTableTblPermission();
                result = KetNoiSCDLProcess.CreateTableTblDepartment();
                result = KetNoiSCDLProcess.CreateTableTblLog();
                result = KetNoiSCDLProcess.CreateTableTblUpdateKhaDung();
                result = KetNoiSCDLProcess.CreateTableTblServiceFull();
                result = KetNoiSCDLProcess.CreateTableLicense();
                result = KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenh();
                result = KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenhNew();
                result = KetNoiSCDLProcess.CreateViewServicepriceDichVu();
                result = KetNoiSCDLProcess.CreateViewServicepriceThuoc();
                result = KetNoiSCDLProcess.CreateTable_DangDT_Tmp();
                result = KetNoiSCDLProcess.CreateTable_RaVienChuaTT_Tmp();
                result = KetNoiSCDLProcess.CreateTable_RaVienDaTT_Tmp();
                result = KetNoiSCDLProcess.CreateTableOption();
                result = KetNoiSCDLProcess.CreateTable_ViewVienPhiMoney();
                //result= KetNoiSCDLProcess.UpdateTableUser();
                result = KetNoiSCDLProcess.CreateTableUserDepartmentgroup();
                result = KetNoiSCDLProcess.CreateTableSersion();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi Update DB" + ex.ToString());
            }
            return result;
        }

        #region Tao bang
        private static bool CreateTableTblUser()
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
        private static bool CreateTableTblPermission()
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
        private static bool CreateTableTblDepartment()
        {
            bool result = false;
            try
            {
                string sql_toolsdepatment = "CREATE TABLE IF NOT EXISTS tools_depatment (tools_depatmentid serial NOT NULL, departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentgrouptype integer, departmentid integer, departmentcode text, departmentname text, departmenttype integer, CONSTRAINT tools_depatment_pkey PRIMARY KEY (tools_depatmentid));";
                string sql_deletepatient = "DELETE FROM tools_depatment;";
                string sql_insert = "INSERT INTO tools_depatment(departmentgroupid, departmentgroupcode, departmentgroupname, departmentgrouptype, departmentid, departmentcode, departmentname, departmenttype) SELECT degp.departmentgroupid as departmentgroupid, degp.departmentgroupcode as departmentgroupcode, degp.departmentgroupname as departmentgroupname, degp.departmentgrouptype, de.departmentid as departmentid, de.departmentcode as departmentcode, de.departmentname as departmentname, de.departmenttype FROM departmentgroup degp,department de WHERE de.departmentgroupid = degp.departmentgroupid ORDER BY degp.departmentgroupid;";

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
        private static bool CreateTableTblLog()
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
        private static bool CreateTableTblUpdateKhaDung()
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
        private static bool CreateTableTblServiceFull()
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
        private static bool CreateTableColumeBackupDichVu()
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
        private static bool CreateTableTblDVKTBHYTChenh()
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
        private static bool CreateTableTblDVKTBHYTChenhNew()
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
        private static bool CreateTable_DangDT_Tmp()
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "CREATE TABLE IF NOT EXISTS tools_dangdt_tmp(dangdtid serial NOT NULL, departmentgroupid double precision, bn_chuyendi double precision, bn_chuyenden double precision, ravien_slbn double precision, dangdt_slbn_bh double precision, dangdt_slbn_vp double precision, dangdt_slbn double precision, dangdt_tienkb double precision, dangdt_tienxn double precision, dangdt_tiencdhatdcn double precision, dangdt_tienpttt double precision, dangdt_tiendvktc double precision, dangdt_tiengiuong double precision, dangdt_tienkhac double precision, dangdt_tienvattu double precision, dangdt_tienmau double precision, dangdt_tienthuoc_bhyt double precision, dangdt_tienthuoc_vp double precision, dangdt_tienthuoc double precision, dangdt_tongtien_bhyt double precision, dangdt_tongtien_vp double precision, dangdt_tongtien double precision, dangdt_tamung double precision, dangdt_date timestamp without time zone, loaibaocao text, khoangdl_tu timestamp without time zone, chaytudong integer, CONSTRAINT tools_dangdt_tmp_pkey PRIMARY KEY (dangdtid));";
                if (condb.ExecuteNonQuery(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTable_DangDT_Tmp" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTable_RaVienChuaTT_Tmp()
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "CREATE TABLE IF NOT EXISTS tools_ravienchuatt_tmp(ravienchuattid serial NOT NULL, departmentgroupid double precision, ravienchuatt_slbn_bh double precision, ravienchuatt_slbn_vp double precision, ravienchuatt_slbn double precision, ravienchuatt_tienkb double precision, ravienchuatt_tienxn double precision, ravienchuatt_tiencdhatdcn double precision, ravienchuatt_tienpttt double precision, ravienchuatt_tiendvktc double precision, ravienchuatt_tiengiuong double precision, ravienchuatt_tienkhac double precision, ravienchuatt_tienvattu double precision, ravienchuatt_tienmau double precision, ravienchuatt_tienthuoc_bhyt double precision, ravienchuatt_tienthuoc_vp double precision, ravienchuatt_tienthuoc double precision, ravienchuatt_tongtien_bhyt double precision, ravienchuatt_tongtien_vp double precision, ravienchuatt_tongtien double precision, ravienchuatt_tamung double precision, ravienchuatt_date timestamp without time zone, loaibaocao text, khoangdl_tu timestamp without time zone, chaytudong integer, CONSTRAINT tools_ravienchuatt_tmp_pkey PRIMARY KEY (ravienchuattid));";
                if (condb.ExecuteNonQuery(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTable_RaVienChuaTT_Tmp" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTable_RaVienDaTT_Tmp()
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "CREATE TABLE IF NOT EXISTS tools_raviendatt_tmp(raviendattid serial NOT NULL, departmentgroupid double precision, raviendatt_slbn_bh double precision, raviendatt_slbn_vp double precision, raviendatt_slbn double precision, raviendatt_tienkb double precision, raviendatt_tienxn double precision, raviendatt_tiencdhatdcn double precision, raviendatt_tienpttt double precision, raviendatt_tiendvktc double precision, raviendatt_tiengiuong double precision, raviendatt_tienkhac double precision, raviendatt_tienvattu double precision, raviendatt_tienmau double precision, raviendatt_tienthuoc_bhyt double precision, raviendatt_tienthuoc_vp double precision, raviendatt_tienthuoc double precision, raviendatt_tongtien_bhyt double precision, raviendatt_tongtien_vp double precision, raviendatt_tongtien double precision, raviendatt_tamung double precision, loaibaocao text, raviendatt_date timestamp without time zone, chaytudong integer, CONSTRAINT tools_raviendatt_tmp_pkey PRIMARY KEY (raviendattid));";
                if (condb.ExecuteNonQuery(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTable_RaVienDaTT_Tmp" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOption()
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
        private static bool CreateTableUserDepartmentgroup()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_tbluser_departmentgroup(userdepgid serial NOT NULL, departmentgroupid integer, departmentid integer, departmenttype integer, usercode text,  userdepgidnote text, CONSTRAINT tbluser_departmentgroup_pkey PRIMARY KEY (userdepgid));";
                if (condb.ExecuteNonQuery(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableUserDepartmentgroup" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableSersion()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_version (versionid serial NOT NULL, appversion text, updateapp bytea, appsize integer, sqlversion text, updatesql bytea, sqlsize integer, sync_flag integer, update_flag integer, CONSTRAINT tools_version_pkey PRIMARY KEY (versionid));";
                if (condb.ExecuteNonQuery(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableSersion" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableLicense()
        {
            bool result = false;
            try
            {
                string sql_tbltools_license = "CREATE TABLE IF NOT EXISTS tools_license (licenseid serial NOT NULL, datakey text, licensekey text, CONSTRAINT tools_license_pkey PRIMARY KEY (licenseid));";
                if (condb.ExecuteNonQuery(sql_tbltools_license))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableLicense" + ex.ToString());
            }
            return result;
        }



        #endregion

        #region Cap nhat sua chua bang
        private static bool UpdateTableUser()
        {
            bool result = false;
            try
            {
                string sqlKiemtra = "SELECT userhisid from tools_tbluser";
                try
                {
                    DataView dataKiemTra = new DataView(condb.getDataTable(sqlKiemtra));
                    result = true;
                }
                catch (Exception ex)
                {
                    MedicalLink.Base.Logging.Error("Đã tồn tại cột userhisid trong tools_tbluser" + ex.ToString());
                    throw;
                    string sql_updateuser = "ALTER TABLE tools_tbluser ADD userhisid integer;";
                    if (condb.ExecuteNonQuery_Error(sql_updateuser))
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi UpdateTableWithVersion" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region Tao View

        private static bool CreateViewServicepriceDichVu()
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
        private static bool CreateViewServicepriceThuoc()
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
        private static bool CreateTable_ViewVienPhiMoney() //v3.0
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "CREATE OR REPLACE VIEW vienphi_money AS SELECT vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_khambenh_bh,  sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_khambenh_vp, sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_xetnghiem_bh,  sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_xetnghiem_vp,  sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_cdha_bh,  sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_cdha_vp,  sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_tdcn_bh,  sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_tdcn_vp,   sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_pttt_bh,  sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_pttt_vp,   (sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) + sum(case when ser.loaidoituong in (2) and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') then ser.servicepricemoney_nhandan * ser.soluong else 0 end)) as money_dvktc_bh,  sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_dvktc_vp,  sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_mau_bh,  sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_mau_vp,  (sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu') and ser.loaidoituong in (0,4,6) and ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt * ser.soluong else 0 end) -  sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu') and ser.loaidoituong in (0,4,6) and ser.maubenhphamphieutype=1 then ser.servicepricemoney_bhyt * ser.soluong else 0 end)) as money_thuoc_bh,  (sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu') and ser.loaidoituong in (1,3,8) and ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan * ser.soluong else 0 end) - sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu') and ser.loaidoituong in (1,3,8) and ser.maubenhphamphieutype=1 then ser.servicepricemoney_nhandan * ser.soluong else 0 end)  + sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu') and ser.loaidoituong in (4,6) and ser.maubenhphamphieutype=0 then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) - sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu') and ser.loaidoituong in (4,6) and ser.maubenhphamphieutype=1 then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end)) as money_thuoc_vp,  (sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT') and ser.loaidoituong in (0,4,6) and ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt * ser.soluong else 0 end) - sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT') and ser.loaidoituong in (0,4,6) and ser.maubenhphamphieutype=1 then ser.servicepricemoney_bhyt * ser.soluong else 0 end)) as money_vattu_bh,  (sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM') and ser.loaidoituong in (1,3,8) and ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan * ser.soluong else 0 end) -  sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM') and ser.loaidoituong in (1,3,8) and ser.maubenhphamphieutype=1 then ser.servicepricemoney_nhandan * ser.soluong else 0 end)  + sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT') and ser.loaidoituong in (4,6) and ser.maubenhphamphieutype=0 then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) - sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT') and ser.loaidoituong in (4,6) and ser.maubenhphamphieutype=1 then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end)) as money_vattu_vp,  sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_giuong_bh,  sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_giuong_vp, sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_vanchuyen_bh,  sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_vanchuyen_vp, sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_khac_bh,  sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_khac_vp,  sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) as money_phuthu_bh,  sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end) + sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end) as money_phuthu_vp,  (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung   FROM vienphi vp left join serviceprice ser on vp.vienphiid=ser.vienphiid  WHERE vp.vienphidate >'2016-01-01 00:00:00' and ser.thuockhobanle=0  GROUP BY vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp ORDER BY vp.vienphiid DESC;";
                if (condb.ExecuteNonQuery(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTable_ViewVienPhiMoney" + ex.ToString());
            }
            return result;
        }

        #endregion
    }
}
