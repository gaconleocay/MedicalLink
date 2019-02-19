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

namespace MedicalLink.QLDuoc
{
    public partial class DM_NuocSanXuat : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private int selectID { get; set; }
        public DM_NuocSanXuat()
        {
            InitializeComponent();
        }

        #region Load
        private void DM_NuocSanXuat_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachData();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhSachData()
        {
            try
            {
                string _sqlData = $@"select row_number () over (order by nuocsanxuatname) as stt,* from pm_nuocsanxuat WHERE isremove=0;";
                DataTable _dtDSKho = condb.GetDataTable_Phr(_sqlData);
                if (_dtDSKho != null && _dtDSKho.Rows.Count > 0)
                {
                    gridControlData.DataSource = _dtDSKho;
                }
                else
                {
                    gridControlData.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void gridViewData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        #endregion

        #region Events
        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetControl();
            btnXoa.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string _sqlinsert = "";
                if (this.selectID != 0)//sua
                {
                    _sqlinsert = "UPDATE pm_nuocsanxuat SET donvitinhname='" + txtnuocsanxuatname.Text.Trim() + "', islock='" + (chkIslock.Checked == true ? 1 : 0) + "', lastuserupdated='" + Base.SessionLogin.SessionUsercode + "' WHERE donvitinhid=" + this.selectID + ";";
                }
                else
                {
                    _sqlinsert = "INSERT INTO pm_nuocsanxuat(donvitinhname, lastuserupdated) VALUES ('" + txtnuocsanxuatname.Text.Trim() + "', '" + Base.SessionLogin.SessionUsercode + "');";
                }
                if (condb.ExecuteNonQuery_Phr(_sqlinsert))
                {
                    ResetControl();
                    btnXoa.Enabled = false;
                    LoadDanhSachDVT();

                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(ThongBaoLable.CAP_NHAT_THANH_CONG);
                    frmthongbao.Show();
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(ThongBaoLable.CAP_NHAT_THAT_BAI);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewData.FocusedRowHandle;
                string _donvitinhid = gridViewData.GetRowCellValue(rowHandle, "donvitinhid").ToString();

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string _sqlXoa = "DELETE FROM pm_nuocsanxuat WHERE donvitinhid=" + this.selectID + ";";
                    if (condb.ExecuteNonQuery_Phr(_sqlXoa))
                    {
                        ResetControl();
                        btnXoa.Enabled = false;
                        LoadDanhSachDVT();

                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(ThongBaoLable.CAP_NHAT_THANH_CONG);
                        frmthongbao.Show();
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(ThongBaoLable.CAP_NHAT_THAT_BAI);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewData_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewData.FocusedRowHandle;
                this.selectID = O2S_Common.TypeConvert.Parse.ToInt32(gridViewData.GetRowCellValue(rowHandle, "donvitinhid").ToString());
                txtnuocsanxuatid.Text = this.selectID.ToString();
                txtnuocsanxuatname.Text = gridViewData.GetRowCellValue(rowHandle, "donvitinhname").ToString();
                chkIslock.Checked = (gridViewData.GetRowCellValue(rowHandle, "islock").ToString() == "1" ? true : false);

                btnXoa.Enabled = true;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        private void ResetControl()
        {
            try
            {
                this.selectID = 0;
                txtnuocsanxuatid.ResetText();
                txtnuocsanxuatname.ResetText();
                chkIslock.Checked = false;
                txtnuocsanxuatname.Focus();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
