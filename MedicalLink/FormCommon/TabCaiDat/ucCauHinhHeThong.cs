using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucCauHinhHeThong : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        string toolsoptionid = "";
        #region Load
        public ucCauHinhHeThong()
        {
            InitializeComponent();
        }

        private void ucCauHinhHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl(false);
                LoadDanhSachOption();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void EnableAndDisableControl(bool result)
        {
            try
            {
                btnOptionOK.Enabled = result;

                txtOptionCode.ReadOnly = true;
                txtOptionName.Enabled = result;
                txtOptionValue.Enabled = result;
                txtOptionNote.Enabled = result;
                chkLook.Enabled = result;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadDanhSachOption()
        {
            try
            {
                string sqldsnv = "SELECT toolsoptionid, toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote, toolsoptionlook FROM tools_option ORDER BY toolsoptionid;";
                DataView dataOption = new DataView(condb.GetDataTable_MeL(sqldsnv));
                if (dataOption != null && dataOption.Count > 0)
                {
                    gridControlDSOption.DataSource = dataOption;
                }
                else
                {
                    gridControlDSOption.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        private void gridViewDSOption_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void gridViewDSOption_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void HienThiThongBao(string tenThongBao)
        {
            try
            {
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(tenThongBao);
                frmthongbao.Show();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewDSOption_Click(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl(true);
                var rowHandle = gridViewDSOption.FocusedRowHandle;
                toolsoptionid = gridViewDSOption.GetRowCellValue(rowHandle, "toolsoptionid").ToString();
                txtOptionCode.Text = gridViewDSOption.GetRowCellValue(rowHandle, "toolsoptioncode").ToString();
                txtOptionName.Text = gridViewDSOption.GetRowCellValue(rowHandle, "toolsoptionname").ToString();
                txtOptionValue.Text = gridViewDSOption.GetRowCellValue(rowHandle, "toolsoptionvalue").ToString();
                txtOptionNote.Text = gridViewDSOption.GetRowCellValue(rowHandle, "toolsoptionnote").ToString();

                if (gridViewDSOption.GetRowCellValue(rowHandle, "toolsoptionlook").ToString() == "1")
                {
                    chkLook.Checked = true;
                }
                else
                {
                    chkLook.Checked = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnOptionOK_Click(object sender, EventArgs e)
        {
            try
            {
                string toolsoptionlook = "0";
                if (chkLook.Checked)
                {
                    toolsoptionlook = "1";
                }
                if (toolsoptionid != "")
                {
                    string sqlupdate = "UPDATE tools_option SET toolsoptionname='" + txtOptionName.Text.Trim() + "', toolsoptionvalue='" + txtOptionValue.Text.Trim() + "', toolsoptionnote='" + txtOptionNote.Text.Trim() + "', toolsoptionlook='" + toolsoptionlook + "', toolsoptiondate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', toolsoptioncreateuser='" + SessionLogin.SessionUsername + "' WHERE toolsoptionid='" + toolsoptionid + "'; ";
                    if (condb.ExecuteNonQuery_MeL(sqlupdate))
                    {
                        HienThiThongBao(MedicalLink.Base.ThongBaoLable.SUA_THANH_CONG);
                    }
                }
                else
                {
                    string sqlupdate = "INSERT INTO tools_option(toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote, toolsoptionlook, toolsoptiondate, toolsoptioncreateuser) VALUES ('" + txtOptionCode.Text.Trim() + "', '" + txtOptionName.Text.Trim() + "', '" + txtOptionValue.Text.Trim() + "', '" + txtOptionNote.Text.Trim() + "', '" + toolsoptionlook + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + SessionLogin.SessionUsername + "');";
                    if (condb.ExecuteNonQuery_MeL(sqlupdate))
                    {
                        HienThiThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                    }
                }

                gridControlDSOption.DataSource = null;
                ucCauHinhHeThong_Load(null, null);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnOptionAdd_Click(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl(true);
                txtOptionCode.ReadOnly = false;

                toolsoptionid = "";
                txtOptionCode.Text = "";
                txtOptionName.Text = "";
                txtOptionValue.Text = "";
                txtOptionNote.Text = "";
                txtOptionCode.Focus();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnOptionDefault_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả option hiện tại và trở về mặc định?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (hoi == DialogResult.Yes)
                {
                    string sql_delete_option = "delete from tools_option;";
                    string sql_default_option = "INSERT INTO tools_option(toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote, toolsoptionlook, toolsoptiondate, toolsoptioncreateuser) VALUES ('ThoiGianCapNhatTbl_tools_bndangdt_tmp', 'Thời gian tự động cập nhật dữ liệu bảng tools_bndangdt_tmp', '0', 'Bảng tools_bndangdt_tmp phục vụ báo cáo Dashboard: BC QL tổng thể khoa; BC BN nội trú. Thời gian tính bằng phút - số', '0', now(), 'Administrator'); INSERT INTO tools_option(toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote, toolsoptionlook, toolsoptiondate, toolsoptioncreateuser) VALUES ('KhoangThoiGianLayDuLieu', 'Khoảng thời gian lấy dữ liệu báo cáo Dashboard', '2016-01-01 00:00:00', 'Khoảng thời gian lấy dữ liệu báo cáo Dashboard từ -> hiện tại. Định dạng: yyyy-MM-dd HH:mm:ss. VD:  2016-01-01 00:00:00. Phục vụ cho báo cáo: REPORT_08; REPORT_09', '0', now(), 'Administrator');";
                    if (condb.ExecuteNonQuery_MeL(sql_delete_option) && condb.ExecuteNonQuery_MeL(sql_default_option))
                    {
                        HienThiThongBao(MedicalLink.Base.ThongBaoLable.THAO_TAC_THANH_CONG);
                        ucCauHinhHeThong_Load(null,null);
                    }
                    else
                    {
                        HienThiThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }


    }
}
