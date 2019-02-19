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
    public partial class DM_HangSanXuat : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private int selectID { get; set; }
        public DM_HangSanXuat()
        {
            InitializeComponent();
        }

        #region Load
        private void DM_HangSanXuat_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhMucNuocSanXuat();
                LoadDanhSachData();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhMucNuocSanXuat()
        {
            try
            {
                string _sqlData = $@"select nuocsanxuatid,nuocsanxuatcode,nuocsanxuatname from pm_nuocsanxuat where isremove=0 and islock=0 ORDER BY nuocsanxuatname;";
                DataTable _dtDSKho = condb.GetDataTable_Phr(_sqlData);
                cbbnuocsanxuat.Properties.DataSource = _dtDSKho;
                cbbnuocsanxuat.Properties.DisplayMember = "nuocsanxuatname";
                cbbnuocsanxuat.Properties.ValueMember = "nuocsanxuatid";
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
                string _sqlData = $@"select row_number () over (order by hsx.hangsanxuatname) as stt,
	hsx.hangsanxuatid,
	hsx.hangsanxuatcode,
	hsx.hangsanxuatname,
	hsx.nuocsanxuatid,
	nsx.nuocsanxuatname,
	hsx.islock,
	hsx.isremove
from pm_hangsanxuat hsx
	left join pm_nuocsanxuat nsx on nsx.nuocsanxuatid=hsx.nuocsanxuatid
WHERE hsx.isremove=0;";
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
                //kiem tra ton tai
                string _sqlinsert = "";
                if (this.selectID != 0)//sua
                {
                    _sqlinsert = String.Format("UPDATE public.pm_hangsanxuat SET  hangsanxuatcode='{0}', hangsanxuatname='{1}', nuocsanxuatid='{2}', nuocsanxuatname='{3}', islock='{4}', lastuserupdated='{5}' WHERE hangsanxuatid='{6}';", txthangsanxuatcode.Text.Trim(), txthangsanxuatname.Text.Trim(), cbbnuocsanxuat.EditValue, cbbnuocsanxuat.Text, (chkIslock.Checked == true ? 1 : 0), Base.SessionLogin.SessionUsercode, this.selectID);
                }
                else
                {
                    _sqlinsert = String.Format("INSERT INTO public.pm_hangsanxuat(hangsanxuatcode, hangsanxuatname, nuocsanxuatid, nuocsanxuatname, lastuserupdated) VALUES ('{0}','{1}','{2}','{3}','{4}');", txthangsanxuatcode.Text.Trim(), txthangsanxuatname.Text.Trim(), cbbnuocsanxuat.EditValue, cbbnuocsanxuat.Text, Base.SessionLogin.SessionUsercode);
                }
                if (condb.ExecuteNonQuery_Phr(_sqlinsert))
                {
                    ResetControl();
                    btnXoa.Enabled = false;
                    LoadDanhSachData();

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
                string _hangsanxuatid = gridViewData.GetRowCellValue(rowHandle, "hangsanxuatid").ToString();

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string _sqlXoa = "DELETE FROM pm_hangsanxuat WHERE hangsanxuatid=" + this.selectID + ";";
                    if (condb.ExecuteNonQuery_Phr(_sqlXoa))
                    {
                        ResetControl();
                        btnXoa.Enabled = false;
                        LoadDanhSachData();

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
                this.selectID = O2S_Common.TypeConvert.Parse.ToInt32(gridViewData.GetRowCellValue(rowHandle, "hangsanxuatid").ToString());
                txthangsanxuatid.Text = this.selectID.ToString();
                txthangsanxuatcode.Text = gridViewData.GetRowCellValue(rowHandle, "hangsanxuatcode").ToString();
                txthangsanxuatname.Text = gridViewData.GetRowCellValue(rowHandle, "hangsanxuatname").ToString();
                cbbnuocsanxuat.EditValue = gridViewData.GetRowCellValue(rowHandle, "nuocsanxuatid");
                chkIslock.Checked = (gridViewData.GetRowCellValue(rowHandle, "islock").ToString() == "1" ? true : false);

                btnXoa.Enabled = true;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        #region Process
        private void ResetControl()
        {
            try
            {
                this.selectID = 0;
                txthangsanxuatid.ResetText();
                txthangsanxuatcode.ResetText();
                txthangsanxuatname.ResetText();
                cbbnuocsanxuat.EditValue = null;
                chkIslock.Checked = false;
                txthangsanxuatcode.Focus();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

    }
}
