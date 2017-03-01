using MedicalLink.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace MedicalLink.FormCommon
{
    public partial class frmMain : Form
    {
        private void KiemTraPhanQuyenNguoiDung()
        {
            try
            {
                List<ClassCommon.classPermission> lstDSChucNang = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 2).ToList();
                List<ClassCommon.classPermission> lstDSDashboard = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 5).ToList();

                if (SessionLogin.SessionUsercode != KeyTrongPhanMem.AdminUser_key)
                {
                    string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true) + "';";
                    DataView dv_per = new DataView(condb.getDataTable(sqlquerry_per));
                    if (dv_per != null && dv_per.Count > 0)
                    {
                        for (int i = 0; i < lstDSChucNang.Count; i++)
                        {
                            for (int j = 0; j < dv_per.Count; j++)
                            {
                                if (lstDSChucNang[i].permissioncode == EncryptAndDecrypt.Decrypt(dv_per[j]["permissioncode"].ToString(), true))
                                {
                                    lstDSChucNang[i].permissioncheck = true;
                                }
                            }
                        }
                        for (int i = 0; i < lstDSDashboard.Count; i++)
                        {
                            for (int j = 0; j < dv_per.Count; j++)
                            {
                                if (lstDSDashboard[i].permissioncode == EncryptAndDecrypt.Decrypt(dv_per[j]["permissioncode"].ToString(), true))
                                {
                                    lstDSDashboard[i].permissioncheck = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lstDSChucNang.Count; i++)
                    {
                        lstDSChucNang[i].permissioncheck = true;
                    }
                    for (int i = 0; i < lstDSDashboard.Count; i++)
                    {
                        lstDSDashboard[i].permissioncheck = true;
                    }
                }
                var lstchucnang = lstDSChucNang.Where(o => o.permissioncheck == true).ToList();
                var lstdashboard = lstDSDashboard.Where(o => o.permissioncheck == true).ToList();
                if (lstchucnang == null || lstchucnang.Count <= 0)
                {
                    tabMenuChucNang.PageVisible = false;
                }
                if (lstdashboard == null || lstdashboard.Count <= 0)
                {
                    tabMenuDashboard.PageVisible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                throw;
            }
        }

        private void EnableAndDisableChucNang(bool enabledisable)
        {
            try
            {
                tabMenuChucNang.PageVisible = enabledisable;
                tabMenuDashboard.PageVisible = enabledisable;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                throw;
            }
        }

    }
}
