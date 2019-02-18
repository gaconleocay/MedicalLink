using MedicalLink.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Base
{
    public static class CheckPermission
    {
        static DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        public static bool ChkPerModule(string percode)
        {
            //string en_percode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(percode, true);
            bool result = false;
            try
            {
                if (SessionLogin.SessionUsercode == KeyTrongPhanMem.AdminUser_key)
                {
                    result = true;
                }
                else
                {
                    var checkPhanQuyen = SessionLogin.LstPhanQuyenUser.Where(s => s.permissioncode.Contains(percode)).ToList();
                    if (checkPhanQuyen != null && checkPhanQuyen.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        //Lay danh sach phan quyen khi nguoi dung dang nhap
        public static List<ClassCommon.classPermission> GetListPhanQuyenUser()
        {
            List<ClassCommon.classPermission> lstPhanQuyen = new List<ClassCommon.classPermission>();
            try
            {
                if (SessionLogin.SessionUsercode == KeyTrongPhanMem.AdminUser_key)
                {
                    lstPhanQuyen = Base.ListChucNang.getDanhSachChucNang();
                    foreach (var item in lstPhanQuyen)
                    {
                        item.permissioncheck = true;
                    }
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    string sqlper = "SELECT permissionid, permissioncode, permissionname, userid, usercode, permissioncheck FROM tools_tbluser_permission WHERE usercode = '" + en_usercode + "' and permissioncheck='1';";
                    DataView dv = new DataView(condb.GetDataTable_MeL(sqlper));
                    if (dv.Count > 0)
                    {
                        for (int i = 0; i < dv.Count; i++)
                        {
                            ClassCommon.classPermission itemPer = new ClassCommon.classPermission();
                            //itemPer.permissionid = Convert.ToInt32(dv[i]["permissionid"]);
                            itemPer.permissioncode = Base.EncryptAndDecrypt.Decrypt(dv[i]["permissioncode"].ToString(), true);
                            //itemPer.permissionname = Base.EncryptAndDecrypt.Decrypt(dv[i]["permissionname"].ToString(), true);
                            itemPer.en_permissioncode = dv[i]["permissioncode"].ToString();
                            itemPer.en_permissionname = dv[i]["permissionname"].ToString();
                            itemPer.permissioncheck = Convert.ToBoolean(dv[i]["permissioncheck"]);
                            lstPhanQuyen.Add(itemPer);
                        }
                        foreach (var item_chucnang in lstPhanQuyen)
                        {
                            var chucnang = Base.ListChucNang.getDanhSachChucNang().Where(o => o.permissioncode == item_chucnang.permissioncode).SingleOrDefault();
                            if (chucnang != null)
                            {
                                item_chucnang.permissionname = chucnang.permissionname;
                                item_chucnang.permissiontype = chucnang.permissiontype;
                                item_chucnang.tabMenuId = chucnang.tabMenuId;
                                item_chucnang.permissionnote = chucnang.permissionnote;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return lstPhanQuyen;
        }

        public static List<ClassCommon.classUserDepartment> GetPhanQuyen_KhoaPhong()
        {
            List<ClassCommon.classUserDepartment> lstPhanQuyenKhoaPhong = new List<ClassCommon.classUserDepartment>();
            try
            {
                DataView dataDepartment = new DataView();
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    string sqlper_his = "SELECT degp.departmentgroupid, degp.departmentgroupcode, degp.departmentgroupname, degp.departmentgrouptype, de.departmentid, de.departmentcode, de.departmentname, de.departmenttype FROM department de inner join departmentgroup degp on degp.departmentgroupid=de.departmentgroupid and degp.departmentgrouptype in (1,4,9,10,11) WHERE de.departmenttype in (2,3,6,7,9) ORDER BY degp.departmentgroupname, de.departmentname, de.departmenttype;";
                    dataDepartment = new DataView(condb.GetDataTable_HIS(sqlper_his));
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    string sqlper_mel = "SELECT ude.departmentgroupid, degp.departmentgroupcode, degp.departmentgroupname, degp.departmentgrouptype, ude.departmentid, de.departmentcode, de.departmentname, ude.departmenttype, ude.usercode FROM tools_tbluser_departmentgroup ude inner join dblink('myconn','SELECT departmentid, departmentcode, departmentname, departmenttype FROM department') AS de(departmentid integer, departmentcode text, departmentname text, departmenttype integer) on de.departmentid=ude.departmentid inner join dblink('myconn','SELECT departmentgroupid, departmentgroupcode, departmentgroupname, departmentgrouptype FROM departmentgroup') AS degp(departmentgroupid integer, departmentgroupcode text, departmentgroupname text, departmentgrouptype integer) on degp.departmentgroupid=ude.departmentgroupid WHERE usercode = '" + en_usercode + "' ORDER BY degp.departmentgroupname,de.departmentname,ude.departmenttype;";
                    dataDepartment = new DataView(condb.GetDataTable_MeLToHIS(sqlper_mel));
                }
                if (dataDepartment.Count > 0)
                {
                    for (int i = 0; i < dataDepartment.Count; i++)
                    {
                        ClassCommon.classUserDepartment itemUdepart = new ClassCommon.classUserDepartment();
                        itemUdepart.departmentgroupid = Utilities.TypeConvertParse.ToInt32(dataDepartment[i]["departmentgroupid"].ToString());
                        itemUdepart.departmentgroupcode = dataDepartment[i]["departmentgroupcode"].ToString();
                        itemUdepart.departmentgroupname = dataDepartment[i]["departmentgroupname"].ToString();
                        itemUdepart.departmentgrouptype = Utilities.TypeConvertParse.ToInt32(dataDepartment[i]["departmentgrouptype"].ToString());
                        itemUdepart.departmentid = Utilities.TypeConvertParse.ToInt32(dataDepartment[i]["departmentid"].ToString());
                        itemUdepart.departmentcode = dataDepartment[i]["departmentcode"].ToString();
                        itemUdepart.departmentname = dataDepartment[i]["departmentname"].ToString();
                        itemUdepart.departmenttype = Utilities.TypeConvertParse.ToInt32(dataDepartment[i]["departmenttype"].ToString());
                        lstPhanQuyenKhoaPhong.Add(itemUdepart);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return lstPhanQuyenKhoaPhong;
        }

        public static List<ClassCommon.classUserMedicineStore> GetPhanQuyen_KhoThuoc()
        {
            List<ClassCommon.classUserMedicineStore> lstPhanQuyen_KhoThuoc = new List<ClassCommon.classUserMedicineStore>();
            try
            {
                DataView dataKhoThuoc = new DataView();
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                  string  sqlper_his = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM medicine_store ms WHERE ms.medicinestoretype in (1,2,3,4,7) ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                    dataKhoThuoc = new DataView(condb.GetDataTable_HIS(sqlper_his));
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                   string sqlper_mel = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM tools_tbluser_medicinestore ttm inner join dblink('myconn','SELECT medicinestoreid, medicinestorecode, medicinestorename, medicinestoretype FROM medicine_store') AS ms(medicinestoreid integer, medicinestorecode text, medicinestorename text, medicinestoretype integer) on ms.medicinestoreid=ttm.medicinestoreid WHERE ttm.usercode = '" + en_usercode + "' ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                    dataKhoThuoc = new DataView(condb.GetDataTable_MeLToHIS(sqlper_mel));
                }

                if (dataKhoThuoc.Count > 0)
                {
                    for (int i = 0; i < dataKhoThuoc.Count; i++)
                    {
                        ClassCommon.classUserMedicineStore userMedicineStore = new ClassCommon.classUserMedicineStore();
                        userMedicineStore.MedicineStoreCheck = false;
                        userMedicineStore.MedicineStoreId = Utilities.TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoreid"].ToString());
                        userMedicineStore.MedicineStoreCode = dataKhoThuoc[i]["medicinestorecode"].ToString();
                        userMedicineStore.MedicineStoreName = dataKhoThuoc[i]["medicinestorename"].ToString();
                        userMedicineStore.MedicineStoreType = Utilities.TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoretype"].ToString());
                        userMedicineStore.MedicineStoreTypeName = dataKhoThuoc[i]["medicinestoretypename"].ToString();

                        lstPhanQuyen_KhoThuoc.Add(userMedicineStore);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return lstPhanQuyen_KhoThuoc;
        }

        public static List<ClassCommon.classUserMedicinePhongLuu> GetPhanQuyen_PhongLuu()
        {
            List<ClassCommon.classUserMedicinePhongLuu> lstPhanQuyen_PhongLuu = new List<ClassCommon.classUserMedicinePhongLuu>();
            try
            {
                DataView dataPhongluu = new DataView();
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    string sqlper_his = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, (ms.medicinestorename || '-' ||pl.medicinephongluuname) as medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename from medicinephongluu pl inner join medicine_store ms on pl.medicinestoreid=ms.medicinestoreid where pl.medicinephongluucode<>'' and pl.medicinephongluuname<>'' order by ms.medicinestorename, pl.medicinephongluuname;";
                    dataPhongluu = new DataView(condb.GetDataTable_HIS(sqlper_his));
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    string sqlper_mel = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, (ms.medicinestorename || '-' ||pl.medicinephongluuname) as medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename FROM tools_tbluser_medicinephongluu ttm inner join dblink('myconn','SELECT medicinephongluuid, medicinephongluucode, medicinestoreid, medicinephongluuname FROM medicinephongluu') AS pl(medicinephongluuid integer, medicinephongluucode text, medicinestoreid integer, medicinephongluuname text) on pl.medicinephongluuid=ttm.medicinephongluuid inner join dblink('myconn','SELECT medicinestoreid, medicinestorecode, medicinestorename FROM medicine_store') AS ms(medicinestoreid integer, medicinestorecode text, medicinestorename text) on pl.medicinestoreid=ms.medicinestoreid WHERE ttm.usercode = '" + en_usercode + "' ORDER BY ms.medicinestorename, pl.medicinephongluuname;";
                    dataPhongluu = new DataView(condb.GetDataTable_MeLToHIS(sqlper_mel));
                }

                if (dataPhongluu.Count > 0)
                {
                    for (int i = 0; i < dataPhongluu.Count; i++)
                    {
                        ClassCommon.classUserMedicinePhongLuu userMedicineStore = new ClassCommon.classUserMedicinePhongLuu();
                        userMedicineStore.MedicinePhongLuuCheck = false;
                        userMedicineStore.MedicinePhongLuuId = Utilities.TypeConvertParse.ToInt32(dataPhongluu[i]["medicinephongluuid"].ToString());
                        userMedicineStore.MedicinePhongLuuCode = dataPhongluu[i]["medicinephongluucode"].ToString();
                        userMedicineStore.MedicinePhongLuuName = dataPhongluu[i]["medicinephongluuname"].ToString();
                        userMedicineStore.MedicineStoreId = Utilities.TypeConvertParse.ToInt32(dataPhongluu[i]["medicinestoreid"].ToString());
                        userMedicineStore.MedicineStoreCode = dataPhongluu[i]["medicinestorecode"].ToString();
                        userMedicineStore.MedicineStoreName = dataPhongluu[i]["medicinestorename"].ToString();

                        lstPhanQuyen_PhongLuu.Add(userMedicineStore);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return lstPhanQuyen_PhongLuu;
        }

    }
}
