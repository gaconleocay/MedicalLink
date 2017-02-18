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
            string en_percode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(percode, true);
            bool result = false;
            try
            {
                if (SessionLogin.SessionUsercode == "admin")
                {
                    result = true;
                }
                else
                {
                    var checkPhanQuyen = SessionLogin.SessionlstPhanQuyenChucNang.Where(s => s.permissioncode.Contains(en_percode)).ToList();
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
        public static List<ClassCommon.classPermission> GetPhanQuyenChucNang()
        {
            List<ClassCommon.classPermission> lstPhanQuyen = new List<ClassCommon.classPermission>();
            try
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
                        itemPer.permissioncode = dv[i]["permissioncode"].ToString();
                        itemPer.permissionname = dv[i]["permissionname"].ToString();
                        itemPer.permissioncheck = Convert.ToBoolean(dv[i]["permissioncheck"]);
                        lstPhanQuyen.Add(itemPer);
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
                string en_usercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true);
                string sqlper = "SELECT ude.departmentgroupid,de.departmentgroupcode, de.departmentgroupname,de.departmentgrouptype, ude.departmentid,de.departmentcode,de.departmentname,ude.departmenttype, ude.usercode FROM tools_tbluser_departmentgroup ude inner join tools_depatment de on ude.departmentid=de.departmentid WHERE usercode = '" + en_usercode + "' ORDER BY de.departmentgroupname,de.departmentname,ude.departmenttype;";
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
