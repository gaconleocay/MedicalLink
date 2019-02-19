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
    public partial class DM_NhaCungCap : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private int selectID { get; set; }
        public DM_NhaCungCap()
        {
            InitializeComponent();
        }

        #region Load
        private void DM_NhaCungCap_Load(object sender, EventArgs e)
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
                string _sqlData = $@"select row_number () over (order by nhacungcapname) as stt,* FROM pm_nhacungcap WHERE isremove=0;";
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
                    _sqlinsert = String.Format("UPDATE pm_nhacungcap SET nhacungcapcode='{0}', nhacungcapname='{1}', address='{2}', phone='{3}', remark='{4}', islock='{5}', lastuserupdated='{6}' WHERE nhacungcapid='{7}';", txtnhacungcapcode.Text.Trim(), txtnhacungcapname.Text.Trim(), txtaddress.Text.Replace("'", "''"), txtphone.Text, txtremark.Text, (chkIslock.Checked == true ? 1 : 0), Base.SessionLogin.SessionUsercode, this.selectID);
                }
                else
                {
                    _sqlinsert = String.Format("INSERT INTO public.pm_nhacungcap(nhacungcapcode, nhacungcapname, address, phone, remark, lastuserupdated) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');", txtnhacungcapcode.Text.Trim(), txtnhacungcapname.Text.Trim(), txtaddress.Text.Replace("'", "''"), txtphone.Text, txtremark.Text, Base.SessionLogin.SessionUsercode);
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
                string _nhacungcapid = gridViewData.GetRowCellValue(rowHandle, "nhacungcapid").ToString();

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string _sqlXoa = "DELETE FROM pm_nhacungcap WHERE nhacungcapid=" + this.selectID + ";";
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
                this.selectID = O2S_Common.TypeConvert.Parse.ToInt32(gridViewData.GetRowCellValue(rowHandle, "nhacungcapid").ToString());
                txtnhacungcapid.Text = this.selectID.ToString();
                txtnhacungcapcode.Text = gridViewData.GetRowCellValue(rowHandle, "nhacungcapcode").ToString();
                txtnhacungcapname.Text = gridViewData.GetRowCellValue(rowHandle, "nhacungcapname").ToString();
                txtaddress.EditValue = gridViewData.GetRowCellValue(rowHandle, "address");
                txtphone.EditValue = gridViewData.GetRowCellValue(rowHandle, "phone");
                txtremark.EditValue = gridViewData.GetRowCellValue(rowHandle, "remark");
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
                txtnhacungcapid.ResetText();
                txtnhacungcapcode.ResetText();
                txtnhacungcapname.ResetText();
                txtaddress.ResetText();
                txtphone.ResetText();
                txtremark.ResetText();
                chkIslock.Checked = false;
                txtnhacungcapcode.Focus();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

    }
}
