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
                result = KetNoiSCDLProcess.CreateTableTblLog();
                result = KetNoiSCDLProcess.CreateTableTblUpdateKhaDung();
                result = KetNoiSCDLProcess.CreateTableLicense();
                result = KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenh();
                result = KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenhNew();
                result = KetNoiSCDLProcess.CreateTableOption();
                result = KetNoiSCDLProcess.CreateTableTblNhanVien();
                result = KetNoiSCDLProcess.CreateTableUserMedicineStore();
                result = KetNoiSCDLProcess.CreateTableUserMedicinePhongLuu();
                result = KetNoiSCDLProcess.CreateTableOtherTypeList();
                result = KetNoiSCDLProcess.CreateTableOtherList();
                //result = KetNoiSCDLProcess.CreateTableToolsServicepricePttt();


                //result= KetNoiSCDLProcess.UpdateTableUser();
                result = KetNoiSCDLProcess.CreateTableUserDepartmentgroup();
                result = KetNoiSCDLProcess.CreateTableVersion();
                //result = KetNoiSCDLProcess.CreateFunctionByteaImport();
                // result = KetNoiSCDLProcess.Createview_tools_serviceprice_pttt();
                result = KetNoiSCDLProcess.CreateTableNhomPersonnel();
                result = KetNoiSCDLProcess.CreateTableIhsServicespttt();
                result = KetNoiSCDLProcess.CreateViewVienPhimoney_TM();
                //result = KetNoiSCDLProcess.CapNhat_OtherList();
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
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
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
                if (condb.ExecuteNonQuery_MeL(sql_tblper))
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
        private static bool CreateTableTblLog()
        {
            bool result = false;
            try
            {
                string sql_tbllog = "CREATE TABLE IF NOT EXISTS tools_tbllog (logid serial NOT NULL,loguser text, logvalue text,ipaddress text,computername text,softversion text,logtime timestamp without time zone,CONSTRAINT tools_tbllog_pkey PRIMARY KEY (logid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbllog))
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
                if (condb.ExecuteNonQuery_MeL(sql_tbllog_updatekhadung))
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
        private static bool CreateTableColumeBackupDichVu()
        {
            bool result = false;
            try
            {
                //Thêm cột để chứa dữ liệu backup giá dịch vụ cũ (có thể bỏ ở bản sau vì chạy đc 1 lần thôi)
                string sql_insert_colum = "ALTER TABLE ServicePriceRef ADD Tools_TGApDung_bak_1 timestamp without time zone; ALTER TABLE ServicePriceRef ADD Tools_gia_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNhanDan_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaBHYT_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNuocNgoai_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_KieuApDung_bak_1 integer DEFAULT 0;";

                if (condb.ExecuteNonQuery_HIS(sql_insert_colum)) // có thể bỏ
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
                condb.ExecuteNonQuery_MeL(sql_insert_dvbhyt);
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
                if (condb.ExecuteNonQuery_MeL(sql_insert_dvbhyt))
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
        private static bool CreateTableOption()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_option(toolsoptionid serial NOT NULL, toolsoptioncode text, toolsoptionname text, toolsoptionvalue text, toolsoptionnote text, toolsoptionlook integer, toolsoptiondate timestamp without time zone, toolsoptioncreateuser text, CONSTRAINT tools_option_pkey PRIMARY KEY (toolsoptionid));";
                if (condb.ExecuteNonQuery_MeL(sqloption))
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
                if (condb.ExecuteNonQuery_MeL(sqloption))
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
        private static bool CreateTableVersion()
        {
            bool result = false;
            try
            {
                string sqloption = "CREATE TABLE IF NOT EXISTS tools_version (versionid serial NOT NULL, appversion text, app_link text,  app_type integer, updateapp bytea, appsize integer, sqlversion text, updatesql bytea, sqlsize integer, sync_flag integer,  update_flag integer,  CONSTRAINT tools_version_pkey PRIMARY KEY (versionid));   ";
                if (condb.ExecuteNonQuery_MeL(sqloption))
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
                if (condb.ExecuteNonQuery_MeL(sql_tbltools_license))
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
        private static bool CreateTableTblNhanVien()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS nhompersonnel ( nhanvienid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, userhisid integer, CONSTRAINT nhompersonnel_pkey PRIMARY KEY (nhanvienid));";
                if (condb.ExecuteNonQuery_HIS(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableTblNhanVien" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserMedicineStore()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser_medicinestore( usermestid serial NOT NULL, medicinestoreid integer, medicinestoretype integer, usercode text, userdepgidnote text, CONSTRAINT tools_tbluser_medicinestore_pkey PRIMARY KEY (usermestid) );";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableUserMedicineStore" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableUserMedicinePhongLuu()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_tbluser_medicinephongluu( userphongluutid serial NOT NULL, medicinephongluuid integer, medicinestoreid integer, usercode text, userdepgidnote text, CONSTRAINT tools_tbluser_medicinephongluu_pkey PRIMARY KEY (userphongluutid) );";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableUserMedicinePhongLuu" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableToolsServicepricePttt()
        {
            bool result = false;
            try
            {
                string ServicePttt = "CREATE TABLE IF NOT EXISTS ihs_servicespttt ( servicepriceptttid serial NOT NULL, vienphiid integer, patientid integer, bhytid integer, hosobenhanid integer, loaivienphiid integer, vienphistatus integer, khoaravien integer, phongravien integer, doituongbenhnhanid integer, vienphidate timestamp without time zone, vienphidate_ravien timestamp without time zone, duyet_ngayduyet timestamp without time zone, vienphistatus_vp integer, duyet_ngayduyet_vp timestamp without time zone, vienphistatus_bh integer, duyet_ngayduyet_bh timestamp without time zone, bhyt_tuyenbenhvien integer, departmentid integer, departmentgroupid integer, departmentgroup_huong integer, money_khambenh_bh double precision, money_khambenh_vp double precision, money_xetnghiem_bh double precision, money_xetnghiem_vp double precision, money_cdha_bh double precision, money_cdha_vp double precision, money_tdcn_bh double precision, money_tdcn_vp double precision, money_pttt_bh double precision, money_pttt_vp double precision, money_mau_bh double precision, money_mau_vp double precision, money_giuongthuong_bh double precision, money_giuongthuong_vp double precision, money_giuongyeucau_bh double precision, money_giuongyeucau_vp double precision, money_vanchuyen_bh double precision, money_vanchuyen_vp double precision, money_khac_bh double precision, money_khac_vp double precision, money_phuthu_bh double precision, money_phuthu_vp double precision, money_thuoc_bh double precision, money_thuoc_vp double precision, money_vattu_bh double precision, money_vattu_vp double precision, money_vtthaythe_bh double precision, money_vtthaythe_vp double precision, money_dvktc_bh double precision, money_dvktc_vp double precision, money_chiphikhac double precision, money_hpngaygiuong double precision, money_hppttt double precision, money_hpdkpttt_gm_thuoc double precision, money_hpdkpttt_gm_vattu double precision, money_dkpttt_thuoc_bh double precision, money_dkpttt_thuoc_vp double precision, money_dkpttt_vattu_bh double precision, money_dkpttt_vattu_vp double precision, CONSTRAINT ihs_servicespttt_pkey PRIMARY KEY (servicepriceptttid) );";
                if (condb.ExecuteNonQuery_HIS(ServicePttt))
                {

                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableToolsServicepricePttt" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOtherTypeList()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_othertypelist ( tools_othertypelistid serial NOT NULL, tools_othertypelistcode text, tools_othertypelistname text, tools_othertypeliststatus integer, CONSTRAINT tools_othertypelist_pkey PRIMARY KEY (tools_othertypelistid) ); ";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableOtherTypeList" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableOtherList()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS tools_otherlist ( tools_otherlistid serial NOT NULL, tools_othertypelistid integer, tools_otherlistcode text, tools_otherlistname text, tools_otherlistvalue text, tools_otherliststatus integer, CONSTRAINT tools_otherlist_pkey PRIMARY KEY (tools_otherlistid) );";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableOtherList" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableNhomPersonnel()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS nhompersonnel ( nhanvienid serial NOT NULL, usercode text NOT NULL, username text, userpassword text, userstatus integer, usergnhom integer, usernote text, userhisid integer, CONSTRAINT nhompersonnel_pkey PRIMARY KEY (nhanvienid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableNhomPersonnel" + ex.ToString());
            }
            return result;
        }
        private static bool CreateTableIhsServicespttt()
        {
            bool result = false;
            try
            {
                string sql_tbluser = "CREATE TABLE IF NOT EXISTS ihs_servicespttt (servicepriceptttid serial NOT NULL, vienphiid integer, patientid integer, bhytid integer, hosobenhanid integer, loaivienphiid integer, vienphistatus integer, khoaravien integer, phongravien integer, doituongbenhnhanid integer, vienphidate timestamp without time zone, vienphidate_ravien timestamp without time zone, duyet_ngayduyet timestamp without time zone, vienphistatus_vp integer, duyet_ngayduyet_vp timestamp without time zone, vienphistatus_bh integer, duyet_ngayduyet_bh timestamp without time zone, bhyt_tuyenbenhvien integer, departmentid integer, departmentgroupid integer, departmentgroup_huong integer, money_khambenh_bh double precision, money_khambenh_vp double precision, money_xetnghiem_bh double precision, money_xetnghiem_vp double precision, money_cdha_bh double precision, money_cdha_vp double precision, money_tdcn_bh double precision, money_tdcn_vp double precision, money_pttt_bh double precision, money_pttt_vp double precision, money_mau_bh double precision, money_mau_vp double precision, money_giuongthuong_bh double precision, money_giuongthuong_vp double precision, money_giuongyeucau_bh double precision, money_giuongyeucau_vp double precision, money_nuocsoi_bh double precision, money_nuocsoi_vp double precision, money_xuatan_bh double precision, money_xuatan_vp double precision, money_diennuoc_bh double precision, money_diennuoc_vp double precision, money_vanchuyen_bh double precision, money_vanchuyen_vp double precision, money_khac_bh double precision, money_khac_vp double precision, money_phuthu_bh double precision, money_phuthu_vp double precision, money_thuoc_bh double precision, money_thuoc_vp double precision, money_vattu_bh double precision, money_vattu_vp double precision, money_vtthaythe_bh double precision, money_vtthaythe_vp double precision, money_dvktc_bh double precision, money_dvktc_vp double precision, money_chiphikhac double precision, money_hpngaygiuong double precision, money_hppttt double precision, money_hpdkpttt_gm_thuoc double precision, money_hpdkpttt_gm_vattu double precision, money_dkpttt_thuoc_bh double precision, money_dkpttt_thuoc_vp double precision, money_dkpttt_vattu_bh double precision, money_dkpttt_vattu_vp double precision, money_hppttt_goi_thuoc double precision, money_hppttt_goi_vattu double precision, CONSTRAINT ihs_servicespttt_pkey PRIMARY KEY (servicepriceptttid));";
                if (condb.ExecuteNonQuery_MeL(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateTableIhsServicespttt" + ex.ToString());
            }
            return result;
        }

        #endregion

        #region Cap nhat sua chua bang
        #endregion

        #region Tao View

        private static bool Createview_tools_serviceprice_pttt() //ngay 19/5 sử dụng ihs_servicespttt ngay  v 1.15 ngay 22/5
        {
            bool result = false;
            try
            {
                string sql_insert_bcbndangdt = "  ";
                if (condb.ExecuteNonQuery_HIS(sql_insert_bcbndangdt))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi tao VIEW Createview_tools_serviceprice_pttt" + ex.ToString());
            }
            return result;
        }

        private static bool CreateViewVienPhimoney_TM()
        {
            bool result = false;
            try
            {
                string sql_tbluser = " CREATE OR REPLACE VIEW vienphi_money_tm AS SELECT vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid as khoaravien, vp.departmentid as phongravien, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, vp.vienphistatus_bh, vp.duyet_ngayduyet_bh, vp.bhyt_tuyenbenhvien, vp.bhyt_thangluongtoithieu, ser.departmentid, ser.departmentgroupid, sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (0,4,6) then (case when vp.loaivienphiid=0 and (ser.lankhambenh = 0 or ser.lankhambenh is null) then ser.servicepricemoney_bhyt*ser.soluong when vp.loaivienphiid=1 then ser.servicepricemoney_bhyt*ser.soluong else 0 end) else 0 end) as money_khambenh_bh, sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-ser.servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_khambenh_vp, sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_xetnghiem_bh, sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_xetnghiem_vp, sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_cdha_bh, sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_cdha_vp, sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_tdcn_bh, sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_tdcn_vp, sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_pttt_bh, sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt else 0 end)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_pttt_vp, sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_mau_bh, sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end) when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) as money_mau_vp, sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_giuong_bh, sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_giuong_vp, sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_vanchuyen_bh, sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_vanchuyen_vp, sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_khac_bh, sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_khac_vp, sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_phuthu_bh, sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_phuthu_vp, sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_thuoc_bh, sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end) when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) as money_thuoc_vp, sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_vattu_bh, sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) else (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end) when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) as money_vattu_vp, (sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) ) as money_vtthaythe_bh, (sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) else (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end) when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) + sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2 and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) else (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end) else 0 end) ) as money_vtthaythe_vp, sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_dvktc_bh, (sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) + sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) else (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end) else 0 end) ) as money_dvktc_vp, sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) then (case when ser.maubenhphamphieutype=0 then servicepricemoney_bhyt*ser.soluong else 0-(servicepricemoney_bhyt*ser.soluong) end) else 0 end) as money_dkpttt_thuoc_bh, sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.loaidoituong in (1,3)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) else (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end) else 0 end) as money_dkpttt_thuoc_vp, (sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_bhyt*ser.soluong else 0-(servicepricemoney_bhyt*ser.soluong) end) else 0 end) + sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_bhyt*ser.soluong else 0-(servicepricemoney_bhyt*ser.soluong) end) else 0 end)) as money_dkpttt_vattu_bh, (sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) else (case when ser.maubenhphamphieutype=0 then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end) else 0 end) + sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (1,3)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_bhyt*ser.soluong else 0-(servicepricemoney_bhyt*ser.soluong) end) else 0 end) + sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid<>1 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_bhyt*ser.soluong else 0-(servicepricemoney_bhyt*ser.soluong) end) else 0 end) ) as money_dkpttt_vattu_vp FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,departmentgroupid,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,vienphistatus_vp,duyet_ngayduyet_vp,vienphistatus_bh,duyet_ngayduyet_bh,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu from vienphi where vienphidate >= '2017-01-01 00:00:00' ) vp left join serviceprice ser on vp.vienphiid=ser.vienphiid and ser.thuockhobanle=0 GROUP BY vp.vienphiid,vp.patientid,vp.bhytid,vp.hosobenhanid,vp.loaivienphiid,vp.vienphistatus,vp.departmentgroupid,vp.departmentid,vp.doituongbenhnhanid,vp.vienphidate,vp.vienphidate_ravien,vp.duyet_ngayduyet,vp.vienphistatus_vp,vp.duyet_ngayduyet_vp,vp.vienphistatus_bh,vp.duyet_ngayduyet_bh,vp.bhyt_tuyenbenhvien,vp.bhyt_thangluongtoithieu,ser.departmentid,ser.departmentgroupid,(case when ser.departmentid in (34,335,269,285) then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) else ser.departmentgroupid end); ";
                if (condb.ExecuteNonQuery_HIS(sql_tbluser))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateViewVienPhimoney_TM" + ex.ToString());
            }
            return result;
        }

        #endregion

        #region Tao Function

        private static bool CreateFunctionByteaImport()
        {
            bool result = false;
            try
            {
                string sqloption = " create or replace function bytea_import(p_path text, p_result out bytea) language plpgsql as $$ declare l_oid oid; r record; begin p_result := ''; select lo_import(p_path) into l_oid; for r in ( select data from pg_largeobject where loid = l_oid order by pageno ) loop p_result = p_result || r.data; end loop; perform lo_unlink(l_oid); end;$$;";
                if (condb.ExecuteNonQuery_HIS(sqloption))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CreateFunctionByteaimport" + ex.ToString());
            }
            return result;
        }
        #endregion

        #region CapNhatDuLieu
        private static bool CapNhat_OtherList()
        {
            bool result = false;
            try
            {
                string sql_insertOtherTypeList = "";
                string sql_insertOtherList = "";

                string sqlAddColume = "Alter table tools_othertypelist add tools_othertypelistnote text; Alter table tools_otherlist add tools_otherlistnote text;";
                try
                {
                    condb.ExecuteNonQuery_MeL(sqlAddColume);
                }
                catch (Exception)
                {
                    //continue;
                }
                string sql_dataOtherTypeList = "select * from tools_othertypelist;";
                string sql_dataOtherList = "select * from tools_otherlist;";
                DataTable dataOtherTypeList = condb.GetDataTable_MeL(sql_dataOtherTypeList);
                DataTable dataOtherList = condb.GetDataTable_MeL(sql_dataOtherList);

                List<ClassCommon.ToolsOtherListDTO> lstOtherTypeList = Utilities.Util_DataTable.DataTableToList<ClassCommon.ToolsOtherListDTO>(dataOtherTypeList);
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = Utilities.Util_DataTable.DataTableToList<ClassCommon.ToolsOtherListDTO>(dataOtherList);



            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error("Lỗi CapNhat_OtherList" + ex.ToString());
            }
            return result;
        }

        #endregion
    }
}
