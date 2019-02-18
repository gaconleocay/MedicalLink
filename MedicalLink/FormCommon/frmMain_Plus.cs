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
                Base.SessionLogin.LstPhanQuyen_ChucNang = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 2).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_BaoCaoKhoa = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 3 && o.tabMenuId == 5).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_BaoCaoDoanhThu = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 3 && o.tabMenuId == 6).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_BaoCaoIn = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 10).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_QLTaiChinh = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 3 && o.tabMenuId == 4).OrderBy(o => o.permissioncode).ToList();
                Base.SessionLogin.LstPhanQuyen_Dashboard = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 5).ToList();
                Base.SessionLogin.LstPhanQuyen_QlDuoc = Base.SessionLogin.LstPhanQuyenUser.Where(o => o.permissiontype == 7).ToList();

                //Enable and disable Tab
                if (SessionLogin.LstPhanQuyen_ChucNang != null && SessionLogin.LstPhanQuyen_ChucNang.Count > 0)
                {
                    tabMenuChucNang.PageVisible = true;
                }
                if ((SessionLogin.LstPhanQuyen_BaoCaoKhoa != null && SessionLogin.LstPhanQuyen_BaoCaoKhoa.Count > 0) || (SessionLogin.LstPhanQuyen_BaoCaoDoanhThu != null && SessionLogin.LstPhanQuyen_BaoCaoDoanhThu.Count > 0))
                {
                    tabMenuBaoCao.PageVisible = true;
                }
                if (SessionLogin.LstPhanQuyen_QLTaiChinh != null && SessionLogin.LstPhanQuyen_QLTaiChinh.Count > 0)
                {
                    tabMenuQLTaiChinh.PageVisible = true;
                }
                if (SessionLogin.LstPhanQuyen_Dashboard != null && SessionLogin.LstPhanQuyen_Dashboard.Count > 0)
                {
                    tabMenuDashboard.PageVisible = true;
                }
                if (SessionLogin.LstPhanQuyen_QlDuoc != null && SessionLogin.LstPhanQuyen_QlDuoc.Count > 0)
                {
                    tabMenuQLDuoc.PageVisible = true;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                tabMenuQLDuoc.PageVisible = enabledisable;

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
