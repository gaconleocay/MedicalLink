using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.ChucNang.CapNhatThangLuongCoBan
{
    public partial class ucCapNhatThangLuongCoBan : UserControl
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #endregion
        public ucCapNhatThangLuongCoBan()
        {
            InitializeComponent();
        }

        private void ucCapNhatThangLuongCoBan_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi = "";
                string datetungay_string = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay_string = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                gridControlDSVienPhi.DataSource = null;
                if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi = " and vienphidate between '" + datetungay_string + "' and '" + datedenngay_string + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi = " and vienphidate_ravien between '" + datetungay_string + "' and '" + datedenngay_string + "' ";
                }

                string sqlchaydulieu = "select ROW_NUMBER () OVER (ORDER BY (case when vp.vienphistatus=0 then '1' else (case when COALESCE(vp.vienphistatus_vp,0)=0 then '2' else '3' end) end)) as stt, vp.vienphiid, vp.patientid, hsba.patientname, vp.vienphidate, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, vp.bhyt_thangluongtoithieu, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when COALESCE(vp.vienphistatus_vp,0)=0 then 'Ra viện chưa thanh toán' else 'Đã thanh toán' end) end) as trangthaivienphi, degp.departmentgroupname, de.departmentname from (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_thangluongtoithieu,departmentgroupid,departmentid from vienphi where bhyt_thangluongtoithieu='" + txtLuongCoBan.Text.Trim() + "' " + tieuchi + ") vp inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=vp.departmentid;";

                DataTable dataDulieu = condb.GetDataTable_HIS(sqlchaydulieu);
                if (dataDulieu != null && dataDulieu.Rows.Count > 0)
                {
                    gridControlDSVienPhi.DataSource = dataDulieu;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
                btnCapNhat.Enabled = false;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSVienPhi.RowCount > 0)
                {
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportDataGridViewToFile(gridControlDSVienPhi, gridViewDSVienPhi);
                    btnCapNhat.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLuongCoBan_Moi.Text.Trim() != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật tháng lương cơ bản?", "Thông báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                        string datetungay_string = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        string datedenngay_string = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                        string dateupdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        string tieuchi = "";
                        string tieuchi_dlink = "";
                        decimal _15phantram_tlcb = (Utilities.TypeConvertParse.ToDecimal(txtLuongCoBan_Moi.Text.Trim()) / 100) * 15;

                        if (cboTieuChi.Text == "Theo ngày vào viện")
                        {
                            tieuchi = " and vienphidate between '" + datetungay_string + "' and '" + datedenngay_string + "' ";
                            tieuchi_dlink = " and vienphidate between ''" + datetungay_string + "'' and ''" + datedenngay_string + "'' ";
                        }
                        else if (cboTieuChi.Text == "Theo ngày ra viện")
                        {
                            tieuchi = " and vienphidate_ravien between '" + datetungay_string + "' and '" + datedenngay_string + "' ";
                            tieuchi_dlink = " and vienphidate_ravien between ''" + datetungay_string + "'' and ''" + datedenngay_string + "'' ";
                        }
                        string sqlbackupdulieu = "insert into tools_vienphi_tltt(vienphiid,thangluong_old,dateupdate) SELECT VP.vienphiid, vp.bhyt_thangluongtoithieu as thangluong_old, '" + dateupdate + "' as dateupdate FROM dblink('myconn','select vp.vienphiid,vp.bhyt_thangluongtoithieu from (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_thangluongtoithieu,departmentgroupid,departmentid from vienphi where bhyt_thangluongtoithieu=''" + txtLuongCoBan.Text.Trim() + "'' " + tieuchi_dlink + ") vp inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid') AS VP(vienphiid integer, bhyt_thangluongtoithieu double precision); ";
                        string sqlCapnhatdulieu = "Update vienphi set bhyt_thangluongtoithieu='" + txtLuongCoBan_Moi.Text.Trim() + "', bhyt_gioihanbhyttrahoantoan='" + _15phantram_tlcb.ToString().Replace(",",".") + "' where vienphiid in (select vp.vienphiid from (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_thangluongtoithieu,departmentgroupid,departmentid from vienphi where bhyt_thangluongtoithieu='" + txtLuongCoBan.Text.Trim() + "' " + tieuchi + ") vp inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid ); ";
                        //Log
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Cập nhật thành công lương cơ bản từ " + txtLuongCoBan.Text + " sang lương cơ bản " + txtLuongCoBan_Moi.Text + " SL=" + gridViewDSVienPhi.RowCount + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + dateupdate + "', 'TOOLS_20');";

                        if (condb.ExecuteNonQuery_MeLToHIS(sqlbackupdulieu))
                        {
                            if (condb.ExecuteNonQuery_HIS(sqlCapnhatdulieu))
                            {
                                MessageBox.Show("Cập nhật thành công SL=" + gridViewDSVienPhi.RowCount + "!", "Thông báo !");
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                btnCapNhat.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thất bại!", "Thông báo !");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Thông báo !");
                        }
                        SplashScreenManager.CloseForm();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chưa nhập mức lương cơ bản mới!");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void txtLuongCoBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void gridViewDSVienPhi_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }
    }
}
