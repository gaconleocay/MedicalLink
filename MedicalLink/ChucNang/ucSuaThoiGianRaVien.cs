using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaThoiGianRaVien : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        public ucSuaThoiGianRaVien()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã viện phí
            txtMaVienPhi.ForeColor = SystemColors.GrayText;
            txtMaVienPhi.Text = "Mã viện phí";
            this.txtMaVienPhi.Leave += new System.EventHandler(this.txtMaVienPhi_Leave);
            this.txtMaVienPhi.Enter += new System.EventHandler(this.txtMaVienPhi_Enter);
            // Hiển thị Text Hint Mã viện phí
            txtMaBN.ForeColor = SystemColors.GrayText;
            txtMaBN.Text = "Mã BN";
            this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
            this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
        }

        #region Load
        // Hiển thị Text Hint Mã viện phí
        private void txtMaVienPhi_Leave(object sender, EventArgs e)
        {
            if (txtMaVienPhi.Text.Length == 0)
            {
                txtMaVienPhi.Text = "Mã viện phí";
                txtMaVienPhi.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtMaVienPhi_Enter(object sender, EventArgs e)
        {
            if (txtMaVienPhi.Text == "Mã viện phí")
            {
                txtMaVienPhi.Text = "";
                txtMaVienPhi.ForeColor = SystemColors.WindowText;
            }
        }

        // Hiển thị Text Hint Mã bệnh nhân
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            if (txtMaBN.Text.Length == 0)
            {
                txtMaBN.Text = "Mã BN";
                txtMaBN.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã bệnh nhân
        private void txtMaBN_Enter(object sender, EventArgs e)
        {
            if (txtMaBN.Text == "Mã BN")
            {
                txtMaBN.Text = "";
                txtMaBN.ForeColor = SystemColors.WindowText;
            }
        }

        private void usSuaThoiGianRaVien_Load(object sender, EventArgs e)
        {
            txtMaBN.Focus();
        }
        #endregion

        #region Custom
        // Chặn chỉ cho nhập vào ký tự dạng số
        private void txtMaVienPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMaVienPhi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
            }
        }

        private void txtMaBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
            }
        }
        private void gridViewSuaThoiGianRV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void txtMaVienPhi_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaVienPhi.Text != "Mã viện phí")
            {
                // Hiển thị Text Hint Mã BN
                txtMaBN.ForeColor = SystemColors.GrayText;
                txtMaBN.Text = "Mã BN";
                this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
                this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
            }
        }

        private void txtMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaBN.Text != "")
            {
                // Hiển thị Text Hint Mã VP
                txtMaVienPhi.ForeColor = SystemColors.GrayText;
                txtMaVienPhi.Text = "Mã viện phí";
                this.txtMaVienPhi.Leave += new System.EventHandler(this.txtMaVienPhi_Leave);
                this.txtMaVienPhi.Enter += new System.EventHandler(this.txtMaVienPhi_Enter);
            }
        }

        private void dateThoiGianVao_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewSuaThoiGianRV.FocusedRowHandle;
                string _tg_vaovien = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianvaovien").ToString();
                if (dateThoiGianVao.Value != Convert.ToDateTime(_tg_vaovien))
                {
                    btnSuaThoiGianOK.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void dateThoiGianRa_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewSuaThoiGianRV.FocusedRowHandle;
                string _tg_ravien = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianravien").ToString();
                if (dateThoiGianRa.Value != Convert.ToDateTime(_tg_ravien))
                {
                    btnSuaThoiGianOK.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string _timkiemtheo = " mrd.patientid=" + txtMaBN.Text + " ";
                if (txtMaBN.Text== "Mã BN")
                {
                    _timkiemtheo = " mrd.vienphiid=" + txtMaVienPhi.Text + " ";
                }
                string sqlquerry = "select distinct hsba.hosobenhanid, mrd.medicalrecordid as madieutri, mrd.patientid as mabenhnhan, mrd.vienphiid as mavienphi, hsba.patientname as tenbenhnhan, (case when vp.vienphistatus_vp=1 then 'Đã duyệt VP' else (case when mrd.medicalrecordstatus=99 then 'Đã đóng BA' else 'Đang điều trị' end) end) as trangthai, mrd.thoigianvaovien as thoigianvaovien, (case when mrd.thoigianravien<>'0001-01-01 00:00:00' then mrd.thoigianravien end) as thoigianravien, degp.departmentgroupname as tenkhoa, de.departmentname as tenphong, case mrd.nextdepartmentid when 0 then '1' else '0' end as lakhoacuoi, case mrd.hinhthucvaovienid when 0 then '1' else '0' end as lakhoadau FROM medicalrecord mrd inner join hosobenhan hsba on hsba.hosobenhanid=mrd.hosobenhanid inner join vienphi vp on vp.vienphiid=mrd.vienphiid left join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid left join department de on de.departmentid=mrd.departmentid WHERE " + _timkiemtheo + " ORDER BY madieutri;";
                DataView dv = new DataView(condb.GetDataTable_HIS(sqlquerry));
                gridControlSuaThoiGianRaVien.DataSource = dv;
                btnSuaThoiGianOK.Enabled = false;

                if (gridViewSuaThoiGianRV.RowCount == 0)
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridControlSuaThoiGianRaVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewSuaThoiGianRV.RowCount > 0)
                {
                    btnSuaThoiGianOK.Enabled = false;
                    dateThoiGianRa.Enabled = true;
                    var rowHandle = gridViewSuaThoiGianRV.FocusedRowHandle;
                    string _tg_vaovien = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianvaovien").ToString();
                    string _tg_ravien = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianravien").ToString();
                    string trangth = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "trangthai").ToString();
                    int khoacuoi = Convert.ToInt16(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "lakhoacuoi").ToString());

                    if (trangth == "Đã duyệt VP")
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_NHAN_DA_DUYET_VIEN_PHI);
                        frmthongbao.Show();
                    }
                    else if (trangth == "Đã đóng BA")
                    {
                        dateThoiGianVao.Value = Convert.ToDateTime(_tg_vaovien);
                        dateThoiGianRa.Value = Convert.ToDateTime(_tg_ravien);
                    }
                    else
                    {
                        dateThoiGianVao.Value = Convert.ToDateTime(_tg_vaovien);
                        if (_tg_ravien != "")
                        {
                            dateThoiGianRa.Value = Convert.ToDateTime(_tg_ravien);
                        }
                        else
                        {
                            dateThoiGianRa.Enabled = false;
                            //dateThoiGianRa.Value = Convert.ToDateTime("0001-01-01 00:00:00");
                        }
                        //else
                        //{
                        //    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao("bệnh nhân chưa ra viện!");
                        //    frmthongbao.Show();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnSuaThoiGianOK_Click(object sender, EventArgs e)
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                var rowHandle = gridViewSuaThoiGianRV.FocusedRowHandle;
                int _medirecordId = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "madieutri").ToString());
                int _mavienphi = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "mavienphi").ToString());
                int _hosobenhanid = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "hosobenhanid").ToString());
                int _lakhoacuoi = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "lakhoacuoi").ToString());
                int _lakhoadau = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "lakhoadau").ToString());
                string _tg_vaovien = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianvaovien").ToString();
                string _tg_ravien = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianravien").ToString();

                string _dateThoiGianRa = "0001-01-01 00:00:00";
                if (dateThoiGianRa.Enabled == true)
                {
                    _dateThoiGianRa = dateThoiGianRa.Text;
                }

                string _sqlUpdate = "";
                if (_lakhoadau == 1 && _lakhoacuoi == 1)
                {
                    _sqlUpdate = "UPDATE medicalrecord SET thoigianvaovien='" + dateThoiGianVao.Text + "', thoigianravien='" + _dateThoiGianRa + "' WHERE medicalrecordid= '" + _medirecordId + "'; UPDATE vienphi SET vienphidate='" + dateThoiGianVao.Text + "', vienphidate_ravien='" + _dateThoiGianRa + "' WHERE vienphiid=" + _mavienphi + "; UPDATE hosobenhan SET hosobenhandate = '" + dateThoiGianVao.Text + "', hosobenhandate_ravien = '" + _dateThoiGianRa + "' WHERE hosobenhanid = " + _hosobenhanid + "; ";
                }
                else if (_lakhoadau == 1 && _lakhoacuoi == 0)
                {
                    _sqlUpdate = "UPDATE medicalrecord SET thoigianvaovien='" + dateThoiGianVao.Text + "', thoigianravien='" + _dateThoiGianRa + "' WHERE medicalrecordid= '" + _medirecordId + "'; UPDATE vienphi SET vienphidate='" + dateThoiGianVao.Text + "' WHERE vienphiid=" + _mavienphi + "; UPDATE hosobenhan SET hosobenhandate = '" + dateThoiGianVao.Text + "' WHERE hosobenhanid = " + _hosobenhanid + "; ";
                }
                else if (_lakhoadau == 0 && _lakhoacuoi == 1)
                {
                    _sqlUpdate = "UPDATE medicalrecord SET thoigianvaovien='" + dateThoiGianVao.Text + "', thoigianravien='" + _dateThoiGianRa + "' WHERE medicalrecordid= '" + _medirecordId + "'; UPDATE vienphi SET vienphidate_ravien='" + _dateThoiGianRa + "' WHERE vienphiid=" + _mavienphi + "; UPDATE hosobenhan SET hosobenhandate_ravien = '" + _dateThoiGianRa + "' WHERE hosobenhanid = " + _hosobenhanid + "; ";
                }
                else
                {
                    _sqlUpdate = "UPDATE medicalrecord SET thoigianvaovien='" + dateThoiGianVao.Text + "', thoigianravien='" + _dateThoiGianRa + "' WHERE medicalrecordid= '" + _medirecordId + "'; ";
                }

                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa TG ra BN VP: " + _mavienphi + " mã ĐT: " + _medirecordId + " từ TG vào: " + _tg_vaovien + " thành TG: " + dateThoiGianVao.Text + "; TG ra từ: " + _tg_ravien + " thành TG: " + _dateThoiGianRa + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', '"+ _mavienphi + "', 'TOOL_01');";
                if (condb.ExecuteNonQuery_HIS(_sqlUpdate))
                {
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.SUA_THANH_CONG);
                    frmthongbao.Show();
                    gridControlSuaThoiGianRaVien.DataSource = null;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion


    }
}
