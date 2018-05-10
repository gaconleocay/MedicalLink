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
                MedicalLink.Base.Logging.Warn("KiemTraPhanQuyenNguoiDung");
                Base.SessionLogin.LstPhanQuyen_ChucNang = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 2).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_BaoCaoKhoa = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 3 && o.tabMenuId == 5).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_BaoCaoDoanhThu = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 3 && o.tabMenuId == 6).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_BaoCaoIn = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 10).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_QLTaiChinh = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 3 && o.tabMenuId == 4).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_Dashboard = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 5).ToList();

                MedicalLink.Base.Logging.Warn("chay xong lay phan quyen:" + SessionLogin.LstPhanQuyen_ChucNang.Count);
                //Enable and disable Tab
                if (SessionLogin.LstPhanQuyen_ChucNang != null && SessionLogin.LstPhanQuyen_ChucNang.Count > 0)
                {
                    MedicalLink.Base.Logging.Warn(" tabMenuChucNang.PageVisible = true;");
                    tabMenuChucNang.PageVisible = true;
                }
                if ((SessionLogin.LstPhanQuyen_BaoCaoKhoa != null && SessionLogin.LstPhanQuyen_BaoCaoKhoa.Count > 0) || (SessionLogin.LstPhanQuyen_BaoCaoDoanhThu != null && SessionLogin.LstPhanQuyen_BaoCaoDoanhThu.Count > 0))
                {
                    MedicalLink.Base.Logging.Warn(" tabMenuBaoCao.PageVisible = true;");
                    tabMenuBaoCao.PageVisible = true;
                }
                if (SessionLogin.LstPhanQuyen_QLTaiChinh != null && SessionLogin.LstPhanQuyen_QLTaiChinh.Count > 0)
                {
                    MedicalLink.Base.Logging.Warn(" tabMenuQLTaiChinh.PageVisible = true;");
                    tabMenuQLTaiChinh.PageVisible = true;
                }
                if (SessionLogin.LstPhanQuyen_Dashboard != null && SessionLogin.LstPhanQuyen_Dashboard.Count > 0)
                {
                    MedicalLink.Base.Logging.Warn(" tabMenuDashboard.PageVisible = true;");
                    tabMenuDashboard.PageVisible = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void EnableAndDisableChucNang(bool enabledisable)
        {
            try
            {
                tabMenuChucNang.PageVisible = enabledisable;
                tabMenuBaoCao.PageVisible = enabledisable;
                tabMenuQLTaiChinh.PageVisible = enabledisable;
                tabMenuDashboard.PageVisible = enabledisable;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
