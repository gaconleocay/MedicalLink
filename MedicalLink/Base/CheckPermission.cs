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
        static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
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
                    var checkPhanQuyen = SessionLogin.SessionLstPhanQuyenNguoiDung.Where(s => s.permissioncode.Contains(percode)).ToList();
                    if (checkPhanQuyen != null && checkPhanQuyen.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return result;
        }

        //Lay danh sach phan quyen khi nguoi dung dang nhap
        public static List<ClassCommon.classPermission> GetListPhanQuyenNguoiDung()
        {
            List<ClassCommon.classPermission> lstPhanQuyen = new List<ClassCommon.classPermission>();
            try
            {
                if (SessionLogin.SessionUsercode == KeyTrongPhanMem.AdminUser_key)
                {
                    lstPhanQuyen = Base.listChucNang.getDanhSachChucNang();
                    foreach (var item in lstPhanQuyen)
                    {
                        item.permissioncheck = true;
                    }
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    string sqlper = "SELECT permissionid, permissioncode, permissionname, userid, usercode, permissioncheck FROM tools_tbluser_permission WHERE usercode = '" + en_usercode + "' and permissioncheck='1';";
                    DataView dv = new DataView(condb.getDataTable(sqlper));
                    if (dv.Count > 0)
                    {
                        for (int i = 0; i < dv.Count; i++)
                        {
                            ClassCommon.classPermission itemPer = new ClassCommon.classPermission();
                            //itemPer.permissionid = Convert.ToInt32(dv[i]["permissionid"]);
                            itemPer.permissioncode = Base.EncryptAndDecrypt.Decrypt(dv[i]["permissioncode"].ToString(), true);
                            itemPer.permissionname = Base.EncryptAndDecrypt.Decrypt(dv[i]["permissionname"].ToString(), true);
                            itemPer.en_permissioncode = dv[i]["permissioncode"].ToString();
                            itemPer.en_permissionname = dv[i]["permissionname"].ToString();
                            itemPer.permissioncheck = Convert.ToBoolean(dv[i]["permissioncheck"]);
                            lstPhanQuyen.Add(itemPer);
                        }
                        foreach (var item_chucnang in lstPhanQuyen)
                        {
                            var chucnang = Base.listChucNang.getDanhSachChucNang().Where(o => o.permissioncode == item_chucnang.permissioncode).SingleOrDefault();
                            if (chucnang != null)
                            {
                                item_chucnang.permissiontype = chucnang.permissiontype;
                                item_chucnang.permissionnote = chucnang.permissionnote;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstPhanQuyen;
        }

        public static List<ClassCommon.classUserDepartment> GetPhanQuyen_KhoaPhong()
        {
            List<ClassCommon.classUserDepartment> lstPhanQuyenKhoaPhong = new List<ClassCommon.classUserDepartment>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT de.departmentgroupid,de.departmentgroupcode, de.departmentgroupname,de.departmentgrouptype, de.departmentid,de.departmentcode,de.departmentname,de.departmenttype FROM  tools_depatment de WHERE de.departmentgrouptype in (1,4,9,10,11) and de.departmenttype in (2,3,6,7,9) ORDER BY de.departmentgroupname,de.departmentname,de.departmenttype;";
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    sqlper = "SELECT ude.departmentgroupid,de.departmentgroupcode, de.departmentgroupname,de.departmentgrouptype, ude.departmentid,de.departmentcode,de.departmentname,ude.departmenttype, ude.usercode FROM tools_tbluser_departmentgroup ude inner join tools_depatment de on ude.departmentid=de.departmentid WHERE usercode = '" + en_usercode + "' ORDER BY de.departmentgroupname,de.departmentname,ude.departmenttype;";
                }
                DataView dv = new DataView(condb.getDataTable(sqlper));
                if (dv.Count > 0)
                {
                    for (int i = 0; i < dv.Count; i++)
                    {
                        ClassCommon.classUserDepartment itemUdepart = new ClassCommon.classUserDepartment();
                        itemUdepart.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmentgroupid"].ToString());
                        itemUdepart.departmentgroupcode = dv[i]["departmentgroupcode"].ToString();
                        itemUdepart.departmentgroupname = dv[i]["departmentgroupname"].ToString();
                        itemUdepart.departmentgrouptype = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmentgrouptype"].ToString());
                        itemUdepart.departmentid = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmentid"].ToString());
                        itemUdepart.departmentcode = dv[i]["departmentcode"].ToString();
                        itemUdepart.departmentname = dv[i]["departmentname"].ToString();
                        itemUdepart.departmenttype = Utilities.Util_TypeConvertParse.ToInt32(dv[i]["departmenttype"].ToString());
                        lstPhanQuyenKhoaPhong.Add(itemUdepart);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstPhanQuyenKhoaPhong;
        }

        public static List<ClassCommon.classUserMedicineStore> GetPhanQuyen_KhoThuoc()
        {
            List<ClassCommon.classUserMedicineStore> lstPhanQuyen_KhoThuoc = new List<ClassCommon.classUserMedicineStore>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM medicine_store ms WHERE ms.medicinestoretype in (1,2,3,4,7) ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    sqlper = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM medicine_store ms INNER JOIN tools_tbluser_medicinestore ttm on ms.medicinestoreid=ttm.medicinestoreid WHERE ttm.usercode = '" + en_usercode + "' ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                }

                DataView dataKhoThuoc = new DataView(condb.getDataTable(sqlper));
                if (dataKhoThuoc.Count > 0)
                {
                    for (int i = 0; i < dataKhoThuoc.Count; i++)
                    {
                        ClassCommon.classUserMedicineStore userMedicineStore = new ClassCommon.classUserMedicineStore();
                        userMedicineStore.MedicineStoreCheck = false;
                        userMedicineStore.MedicineStoreId = Utilities.Util_TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoreid"].ToString());
                        userMedicineStore.MedicineStoreCode = dataKhoThuoc[i]["medicinestorecode"].ToString();
                        userMedicineStore.MedicineStoreName = dataKhoThuoc[i]["medicinestorename"].ToString();
                        userMedicineStore.MedicineStoreType = Utilities.Util_TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoretype"].ToString());
                        userMedicineStore.MedicineStoreTypeName = dataKhoThuoc[i]["medicinestoretypename"].ToString();

                        lstPhanQuyen_KhoThuoc.Add(userMedicineStore);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstPhanQuyen_KhoThuoc;
        }

        public static List<ClassCommon.classUserMedicinePhongLuu> GetPhanQuyen_PhongLuu()
        {
            List<ClassCommon.classUserMedicinePhongLuu> lstPhanQuyen_PhongLuu = new List<ClassCommon.classUserMedicinePhongLuu>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, (ms.medicinestorename || '-' ||pl.medicinephongluuname) as medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename from medicinephongluu pl inner join medicine_store ms on pl.medicinestoreid=ms.medicinestoreid where pl.medicinephongluucode<>'' and pl.medicinephongluuname<>'' order by ms.medicinestorename, pl.medicinephongluuname;";
                }
                else
                {
                    string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                    sqlper = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, (ms.medicinestorename || '-' ||pl.medicinephongluuname) as medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename FROM medicinephongluu pl INNER JOIN tools_tbluser_medicinephongluu ttm on pl.medicinephongluuid=ttm.medicinephongluuid inner join medicine_store ms on pl.medicinestoreid=ms.medicinestoreid WHERE ttm.usercode = '" + en_usercode + "' ORDER BY ms.medicinestorename, pl.medicinephongluuname;";
                }

                DataView dataPhongluu = new DataView(condb.getDataTable(sqlper));
                if (dataPhongluu.Count > 0)
                {
                    for (int i = 0; i < dataPhongluu.Count; i++)
                    {
                        ClassCommon.classUserMedicinePhongLuu userMedicineStore = new ClassCommon.classUserMedicinePhongLuu();
                        userMedicineStore.MedicinePhongLuuCheck = false;
                        userMedicineStore.MedicinePhongLuuId = Utilities.Util_TypeConvertParse.ToInt32(dataPhongluu[i]["medicinephongluuid"].ToString());
                        userMedicineStore.MedicinePhongLuuCode = dataPhongluu[i]["medicinephongluucode"].ToString();
                        userMedicineStore.MedicinePhongLuuName = dataPhongluu[i]["medicinephongluuname"].ToString();
                        userMedicineStore.MedicineStoreId = Utilities.Util_TypeConvertParse.ToInt32(dataPhongluu[i]["medicinestoreid"].ToString());
                        userMedicineStore.MedicineStoreCode = dataPhongluu[i]["medicinestorecode"].ToString();
                        userMedicineStore.MedicineStoreName = dataPhongluu[i]["medicinestorename"].ToString();

                        lstPhanQuyen_PhongLuu.Add(userMedicineStore);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstPhanQuyen_PhongLuu;
        }

    }
}
