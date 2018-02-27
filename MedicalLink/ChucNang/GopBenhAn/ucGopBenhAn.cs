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
using DevExpress.XtraSplashScreen;
using MedicalLink.Base;
using DevExpress.Utils.Menu;

namespace MedicalLink.ChucNang
{
    public partial class ucGopBenhAn : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new Base.ConnectDatabase();
        private List<MedicalrecordGopBADTO> lstHSBA_A { get; set; }
        private List<MedicalrecordGopBADTO> lstHSBA_B { get; set; }
        private long medicalrecordid_A { get; set; }
        #endregion
        public ucGopBenhAn()
        {
            InitializeComponent();
        }

        #region Custom
        private void gridViewHSDT_A_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void txtVienPhiId_A_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtVienPhiId_A_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTimKiem_A.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void txtVienPhiId_B_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtVienPhiId_B_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTimKiem_B.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }






        #endregion.

        #region Events
        private void btnTimKiem_A_Click(object sender, EventArgs e)
        {
            try
            {
                this.lstHSBA_A = new List<MedicalrecordGopBADTO>();
                gridControlPhieuDichVu_A.DataSource = null;
                if (txtVienPhiId_A.Text.Trim() == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }

                string _sqlHSBA = "SELECT mrd.medicalrecordid, vp.vienphiid, vp.patientid, vp.bhytid, hsba.patientname, coalesce(vp.vienphistatus_vp,0) as vienphistatus_vp, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus, mrd.thoigianvaovien, (case when thoigianravien<>'0001-01-01 00:00:00' then thoigianravien end) as thoigianravien, degp.departmentgroupname, mrd.departmentid, de.departmentname, hsba.hosobenhanid FROM medicalrecord mrd inner join vienphi vp on vp.vienphiid=mrd.vienphiid inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid left join department de on de.departmentid=mrd.departmentid where mrd.vienphiid='" + txtVienPhiId_A.Text + "' and vp.vienphiid='" + txtVienPhiId_A.Text + "' order by mrd.medicalrecordid;";
                DataTable _dataHSBA = condb.GetDataTable_HIS(_sqlHSBA);
                if (_dataHSBA != null && _dataHSBA.Rows.Count > 0)
                {
                    this.lstHSBA_A = Utilities.Util_DataTable.DataTableToList<MedicalrecordGopBADTO>(_dataHSBA);
                    gridControlHSDT_A.DataSource = this.lstHSBA_A;
                    gridControlPhieuDichVu_A.DataSource = null;
                }
                else
                {
                    gridControlHSDT_A.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnTimKiem_B_Click(object sender, EventArgs e)
        {
            try
            {
                this.lstHSBA_B = new List<MedicalrecordGopBADTO>();
                gridControlPhieuDichVu_B.DataSource = null;
                if (txtVienPhiId_B.Text.Trim() == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }

                string _sqlHSBA = "SELECT mrd.medicalrecordid, vp.vienphiid, vp.patientid, vp.bhytid, hsba.patientname, coalesce(vp.vienphistatus_vp,0) as vienphistatus_vp, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus, mrd.thoigianvaovien, (case when thoigianravien<>'0001-01-01 00:00:00' then thoigianravien end) as thoigianravien, degp.departmentgroupname, mrd.departmentid, de.departmentname, hsba.hosobenhanid FROM medicalrecord mrd inner join vienphi vp on vp.vienphiid=mrd.vienphiid inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid left join department de on de.departmentid=mrd.departmentid where mrd.vienphiid='" + txtVienPhiId_B.Text + "' and vp.vienphiid='" + txtVienPhiId_B.Text + "' order by mrd.medicalrecordid;";
                DataTable _dataHSBA = condb.GetDataTable_HIS(_sqlHSBA);
                if (_dataHSBA != null && _dataHSBA.Rows.Count > 0)
                {
                    this.lstHSBA_B = Utilities.Util_DataTable.DataTableToList<MedicalrecordGopBADTO>(_dataHSBA);
                    gridControlHSDT_B.DataSource = this.lstHSBA_B;
                }
                else
                {
                    gridControlHSDT_B.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnGopHSBVaoHSA_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.lstHSBA_A != null && this.lstHSBA_A.Count > 0) && (this.lstHSBA_B != null && this.lstHSBA_B.Count > 0))
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn gộp VP=" + this.lstHSBA_B[0].vienphiid + " sang VP=" + this.lstHSBA_A[0].vienphiid + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                        try
                        {
                            string _lstmedicalrecordid = "";
                            string _sqlBHYT_B = "SELECT bhytcode, macskcbbd, to_char(bhytfromdate,'yyyy-MM-dd HH24:MI:ss') as bhytfromdate, to_char(bhytutildate,'yyyy-MM-dd HH24:MI:ss') as bhytutildate, coalesce(du5nam6thangluongcoban,0) as du5nam6thangluongcoban, coalesce(dtcbh_luyke6thang,0) as dtcbh_luyke6thang, noisinhsong FROM bhyt WHERE bhytid='" + this.lstHSBA_B[0].bhytid + "';";
                            DataTable _dataBHYT_B = condb.GetDataTable_HIS(_sqlBHYT_B);
                            if (_dataBHYT_B != null && _dataBHYT_B.Rows.Count > 0)
                            {
                                //update BHYT
                                string _sqlUpdateBHYT_A = "UPDATE bhyt SET theghep_bhytcode='" + _dataBHYT_B.Rows[0]["bhytcode"].ToString() + "', theghep_bhytfromdate='" + _dataBHYT_B.Rows[0]["bhytfromdate"].ToString() + "', theghep_bhytutildate='" + _dataBHYT_B.Rows[0]["bhytutildate"].ToString() + "', theghep_macskcbbd='" + _dataBHYT_B.Rows[0]["macskcbbd"].ToString() + "', theghep_du5nam6thangluongcoban='" + _dataBHYT_B.Rows[0]["du5nam6thangluongcoban"].ToString() + "', theghep_dtcbh_luyke6thang='" + _dataBHYT_B.Rows[0]["dtcbh_luyke6thang"].ToString() + "', theghep_noisinhsong='" + _dataBHYT_B.Rows[0]["noisinhsong"].ToString() + "' WHERE bhytid='" + this.lstHSBA_A[0].bhytid + "'; UPDATE vienphi SET theghep_bhytcode='" + _dataBHYT_B.Rows[0]["bhytcode"].ToString() + "', theghep_bhytfromdate='" + _dataBHYT_B.Rows[0]["bhytfromdate"].ToString() + "', theghep_bhytutildate='" + _dataBHYT_B.Rows[0]["bhytutildate"].ToString() + "', theghep_macskcbbd='" + _dataBHYT_B.Rows[0]["macskcbbd"].ToString() + "', theghep_du5nam6thangluongcoban='" + _dataBHYT_B.Rows[0]["du5nam6thangluongcoban"].ToString() + "', theghep_dtcbh_luyke6thang='" + _dataBHYT_B.Rows[0]["dtcbh_luyke6thang"].ToString() + "', theghep_noisinhsong='" + _dataBHYT_B.Rows[0]["noisinhsong"].ToString() + "' WHERE vienphiid='" + this.lstHSBA_A[0].vienphiid + "';";
                                //Update: maubenhpham, serviceprice, serviceprice, bill
                                string _updateCoPhong = "";
                                foreach (var item_B in this.lstHSBA_B)
                                {
                                    _lstmedicalrecordid += item_B.medicalrecordid + "; ";
                                    List<MedicalrecordGopBADTO> _lst_Phong = this.lstHSBA_A.Where(o => o.departmentid == item_B.departmentid).ToList();
                                    if (_lst_Phong != null && _lst_Phong.Count > 0) //co phong
                                    {
                                        _updateCoPhong += "update maubenhpham set vienphiid=" + _lst_Phong[0].vienphiid + ",hosobenhanid=" + _lst_Phong[0].hosobenhanid + ",medicalrecordid=" + _lst_Phong[0].medicalrecordid + ",patientid=" + _lst_Phong[0].patientid + " where medicalrecordid=" + item_B.medicalrecordid + "; update serviceprice set vienphiid=" + _lst_Phong[0].vienphiid + ",hosobenhanid=" + _lst_Phong[0].hosobenhanid + ",medicalrecordid=" + _lst_Phong[0].medicalrecordid + " where medicalrecordid=" + item_B.medicalrecordid + "; update bill set vienphiid=" + _lst_Phong[0].vienphiid + ",hosobenhanid=" + _lst_Phong[0].hosobenhanid + ",medicalrecordid=" + _lst_Phong[0].medicalrecordid + ",patientid=" + _lst_Phong[0].patientid + " where medicalrecordid=" + item_B.medicalrecordid + "; ";
                                    }
                                    else //chua co phong
                                    {
                                        _updateCoPhong += "update maubenhpham set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + ",patientid=" + this.lstHSBA_A[0].patientid + " where medicalrecordid=" + item_B.medicalrecordid + "; update serviceprice set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + " where medicalrecordid=" + item_B.medicalrecordid + "; update bill set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + ",patientid=" + this.lstHSBA_A[0].patientid + " where medicalrecordid=" + item_B.medicalrecordid + "; update medicalrecord set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + ",patientid=" + this.lstHSBA_A[0].patientid + " where medicalrecordid=" + item_B.medicalrecordid + "; ";
                                    }
                                }
                                if (condb.ExecuteNonQuery_HIS(_updateCoPhong) && condb.ExecuteNonQuery_HIS(_sqlUpdateBHYT_A))
                                {
                                    //Log vào DB
                                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Gộp bệnh án thành công: VP=" + this.lstHSBA_B[0].vienphiid + " sang VP=" + this.lstHSBA_A[0].vienphiid + " DS medicalrecordid_B=[" + _lstmedicalrecordid + "]' ,'" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.lstHSBA_B[0].vienphiid + "', 'TOOL_22');";
                                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                    MessageBox.Show("Gộp bệnh án thành công: VP=" + this.lstHSBA_B[0].vienphiid + " sang VP=" + this.lstHSBA_A[0].vienphiid, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnTimKiem_A.PerformClick();
                                    btnTimKiem_B.PerformClick();
                                }
                                else
                                {
                                    MessageBox.Show("Gộp bệnh án thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_THE_THUC_HIEN_DUOC);
                                frmthongbao.Show();
                            }
                        }
                        catch (Exception ex)
                        {
                            MedicalLink.Base.Logging.Error(ex);
                        }
                        SplashScreenManager.CloseForm();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_THE_THUC_HIEN_DUOC);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void gridViewHSDT_B_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    //GridView view = sender as GridView;
                    e.Menu.Items.Clear();
                    DXMenuItem itemMoBenhAn = new DXMenuItem("Gộp đợt điều trị");
                    itemMoBenhAn.Image = imageCollectionMBA.Images[0];
                    itemMoBenhAn.Click += new EventHandler(GopDotDieuTri_Click);
                    e.Menu.Items.Add(itemMoBenhAn);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion

        #region Click View
        private void gridViewHSDT_A_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewHSDT_A.FocusedRowHandle;
                string _medicalrecordid = gridViewHSDT_A.GetRowCellValue(rowHandle, "medicalrecordid").ToString();
                string _sqlMBP = "SELECT row_number () over (order by mbp.maubenhphamgrouptype,mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.medicalrecordid, mbp.patientid, mbp.vienphiid, (case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 3 then 'Phiếu điều trị' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, (case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, degp.departmentgroupname, de.departmentname, (case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, mbp.maubenhphamdate, (case when mbp.maubenhphamdate_sudung<>'0001-01-01 00:00:00' then mbp.maubenhphamdate_sudung end) as maubenhphamdate_sudung, (case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien, (case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype FROM maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid WHERE maubenhphamgrouptype<>3 and medicalrecordid='" + _medicalrecordid + "';";
                DataTable _dataMBP = condb.GetDataTable_HIS(_sqlMBP);
                if (_dataMBP != null && _dataMBP.Rows.Count > 0)
                {
                    gridControlPhieuDichVu_A.DataSource = _dataMBP;
                }
                else
                {
                    gridControlPhieuDichVu_A.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void gridViewHSDT_B_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewHSDT_B.FocusedRowHandle;
                string _medicalrecordid = gridViewHSDT_B.GetRowCellValue(rowHandle, "medicalrecordid").ToString();
                string _sqlMBP = "SELECT row_number () over (order by mbp.maubenhphamgrouptype,mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.medicalrecordid, mbp.patientid, mbp.vienphiid, (case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 3 then 'Phiếu điều trị' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, (case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, degp.departmentgroupname, de.departmentname, (case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, mbp.maubenhphamdate, (case when mbp.maubenhphamdate_sudung<>'0001-01-01 00:00:00' then mbp.maubenhphamdate_sudung end) as maubenhphamdate_sudung, (case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien, (case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype FROM maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid WHERE maubenhphamgrouptype<>3 and medicalrecordid='" + _medicalrecordid + "';";
                DataTable _dataMBP = condb.GetDataTable_HIS(_sqlMBP);
                if (_dataMBP != null && _dataMBP.Rows.Count > 0)
                {
                    gridControlPhieuDichVu_B.DataSource = _dataMBP;
                }
                else
                {
                    gridControlPhieuDichVu_B.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }




        #endregion

        #region Process
        internal void GopDotDieuTri_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn gộp Mã điều trị này của VP=" + this.lstHSBA_B[0].vienphiid + " sang VP=" + this.lstHSBA_A[0].vienphiid + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    try
                    {
                        var rowHandle = gridViewHSDT_B.FocusedRowHandle;
                        long _medicalrecordid_B = Utilities.Util_TypeConvertParse.ToInt64(gridViewHSDT_B.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                        long _departmentid_B = this.lstHSBA_B.Where(o => o.medicalrecordid == _medicalrecordid_B).FirstOrDefault().departmentid;

                        string _sqlBHYT_B = "SELECT bhytcode, macskcbbd, to_char(bhytfromdate,'yyyy-MM-dd HH24:MI:ss') as bhytfromdate, to_char(bhytutildate,'yyyy-MM-dd HH24:MI:ss') as bhytutildate, coalesce(du5nam6thangluongcoban,0) as du5nam6thangluongcoban, coalesce(dtcbh_luyke6thang,0) as dtcbh_luyke6thang, noisinhsong FROM bhyt WHERE bhytid='" + this.lstHSBA_B[0].bhytid + "';";
                        DataTable _dataBHYT_B = condb.GetDataTable_HIS(_sqlBHYT_B);
                        if (_dataBHYT_B != null && _dataBHYT_B.Rows.Count > 0)
                        {
                            //update BHYT
                            string _sqlUpdateBHYT_A = "UPDATE bhyt SET theghep_bhytcode='" + _dataBHYT_B.Rows[0]["bhytcode"].ToString() + "', theghep_bhytfromdate='" + _dataBHYT_B.Rows[0]["bhytfromdate"].ToString() + "', theghep_bhytutildate='" + _dataBHYT_B.Rows[0]["bhytutildate"].ToString() + "', theghep_macskcbbd='" + _dataBHYT_B.Rows[0]["macskcbbd"].ToString() + "', theghep_du5nam6thangluongcoban='" + _dataBHYT_B.Rows[0]["du5nam6thangluongcoban"].ToString() + "', theghep_dtcbh_luyke6thang='" + _dataBHYT_B.Rows[0]["dtcbh_luyke6thang"].ToString() + "', theghep_noisinhsong='" + _dataBHYT_B.Rows[0]["noisinhsong"].ToString() + "' WHERE bhytid='" + this.lstHSBA_A[0].bhytid + "'; UPDATE vienphi SET theghep_bhytcode='" + _dataBHYT_B.Rows[0]["bhytcode"].ToString() + "', theghep_bhytfromdate='" + _dataBHYT_B.Rows[0]["bhytfromdate"].ToString() + "', theghep_bhytutildate='" + _dataBHYT_B.Rows[0]["bhytutildate"].ToString() + "', theghep_macskcbbd='" + _dataBHYT_B.Rows[0]["macskcbbd"].ToString() + "', theghep_du5nam6thangluongcoban='" + _dataBHYT_B.Rows[0]["du5nam6thangluongcoban"].ToString() + "', theghep_dtcbh_luyke6thang='" + _dataBHYT_B.Rows[0]["dtcbh_luyke6thang"].ToString() + "', theghep_noisinhsong='" + _dataBHYT_B.Rows[0]["noisinhsong"].ToString() + "' WHERE vienphiid='" + this.lstHSBA_A[0].vienphiid + "';";

                            //Update: maubenhpham, serviceprice, serviceprice, bill
                            string _updateCoPhong = "";

                            List<MedicalrecordGopBADTO> _lst_Phong = this.lstHSBA_A.Where(o => o.departmentid == _departmentid_B).ToList(); //kiemr tra trong HS A có phòng hay ko?
                            if (_lst_Phong != null && _lst_Phong.Count > 0) //co phong
                            {
                                //todo
                                GopBenhAn.frmChonDotDieuTri frm = new GopBenhAn.frmChonDotDieuTri(_lst_Phong, _medicalrecordid_B);
                                frm.MyGetData = new GopBenhAn.frmChonDotDieuTri.GetString(Get_Mdicalrecordid_A);
                                frm.ShowDialog();
                                if (this.medicalrecordid_A == 0)
                                {
                                    SplashScreenManager.CloseForm();
                                    return;
                                }
                                MedicalrecordGopBADTO _maDieuTri = _lst_Phong.Where(o => o.medicalrecordid == this.medicalrecordid_A).FirstOrDefault();

                                _updateCoPhong += "update maubenhpham set vienphiid=" + _maDieuTri.vienphiid + ",hosobenhanid=" + _maDieuTri.hosobenhanid + ",medicalrecordid=" + _maDieuTri.medicalrecordid + ",patientid=" + _maDieuTri.patientid + " where medicalrecordid=" + _medicalrecordid_B + "; update serviceprice set vienphiid=" + _maDieuTri.vienphiid + ",hosobenhanid=" + _maDieuTri.hosobenhanid + ",medicalrecordid=" + _maDieuTri.medicalrecordid + " where medicalrecordid=" + _medicalrecordid_B + "; update bill set vienphiid=" + _maDieuTri.vienphiid + ",hosobenhanid=" + _maDieuTri.hosobenhanid + ",medicalrecordid=" + _maDieuTri.medicalrecordid + ",patientid=" + _maDieuTri.patientid + " where medicalrecordid=" + _medicalrecordid_B + "; ";

                                //_updateCoPhong += "update maubenhpham set vienphiid=" + _lst_Phong[0].vienphiid + ",hosobenhanid=" + _lst_Phong[0].hosobenhanid + ",medicalrecordid=" + _lst_Phong[0].medicalrecordid + ",patientid=" + _lst_Phong[0].patientid + " where medicalrecordid=" + _medicalrecordid_B + "; update serviceprice set vienphiid=" + _lst_Phong[0].vienphiid + ",hosobenhanid=" + _lst_Phong[0].hosobenhanid + ",medicalrecordid=" + _lst_Phong[0].medicalrecordid + " where medicalrecordid=" + _medicalrecordid_B + "; update bill set vienphiid=" + _lst_Phong[0].vienphiid + ",hosobenhanid=" + _lst_Phong[0].hosobenhanid + ",medicalrecordid=" + _lst_Phong[0].medicalrecordid + ",patientid=" + _lst_Phong[0].patientid + " where medicalrecordid=" + _medicalrecordid_B + "; ";
                            }
                            else //chua co phong update them medicalrecord
                            {
                                _updateCoPhong += "update maubenhpham set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + ",patientid=" + this.lstHSBA_A[0].patientid + " where medicalrecordid=" + _medicalrecordid_B + "; update serviceprice set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + " where medicalrecordid=" + _medicalrecordid_B + "; update bill set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + ",patientid=" + this.lstHSBA_A[0].patientid + " where medicalrecordid=" + _medicalrecordid_B + "; update medicalrecord set vienphiid=" + this.lstHSBA_A[0].vienphiid + ",hosobenhanid=" + this.lstHSBA_A[0].hosobenhanid + ",patientid=" + this.lstHSBA_A[0].patientid + " where medicalrecordid=" + _medicalrecordid_B + "; ";
                            }

                            if (condb.ExecuteNonQuery_HIS(_updateCoPhong) && condb.ExecuteNonQuery_HIS(_sqlUpdateBHYT_A))
                            {
                                //Log vào DB
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Gộp bệnh án thành công: VP=" + this.lstHSBA_B[0].vienphiid + " sang VP=" + this.lstHSBA_A[0].vienphiid + " DS medicalrecordid_B=[" + _medicalrecordid_B + "]' ,'" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.lstHSBA_B[0].vienphiid + "', 'TOOL_22');";
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                MessageBox.Show("Gộp bệnh án thành công: VP=" + this.lstHSBA_B[0].vienphiid + " sang VP=" + this.lstHSBA_A[0].vienphiid, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnTimKiem_A.PerformClick();
                                btnTimKiem_B.PerformClick();
                            }
                            else
                            {
                                MessageBox.Show("Gộp bệnh án thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_THE_THUC_HIEN_DUOC);
                            frmthongbao.Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        MedicalLink.Base.Logging.Error(ex);
                    }
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        public void Get_Mdicalrecordid_A(long _medicalrecordid_A)
        {
            this.medicalrecordid_A = _medicalrecordid_A;
        }

        #endregion


    }
}
