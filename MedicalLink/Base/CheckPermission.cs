﻿using MedicalLink.Base;
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
        //Phan quyen khoa phong nguoi dung
        public static List<ClassCommon.classUserDepartment> GetPhanQuyenKhoaPhong()
        {
            List<ClassCommon.classUserDepartment> lstPhanQuyenKhoaPhong = new List<ClassCommon.classUserDepartment>();
            try
            {
                string sqlper = "";
                if (SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    sqlper = "SELECT de.departmentgroupid,de.departmentgroupcode, de.departmentgroupname,de.departmentgrouptype, de.departmentid,de.departmentcode,de.departmentname,de.departmenttype FROM  tools_depatment de WHERE de.departmentgrouptype in (1,4,10,11) and de.departmenttype in (2,3,9) ORDER BY de.departmentgroupname,de.departmentname,de.departmenttype;";
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
    }
}
