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
using DevExpress.Utils.Menu;
using MedicalLink.Base;
using MedicalLink.ChucNang.MoBenhAn;
using System.Globalization;
using MedicalLink.ClassCommon;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.ChucNang
{
    public partial class ucMoBenhAn : UserControl
    {

        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        public ucMoBenhAn()
        {
            InitializeComponent();
        }

        #region Load      
        private void ucMoBenhAn_Load(object sender, EventArgs e)
        {
            txtMBAMaBenhNhan.Focus();
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }
        internal void gridControlMBA_TH_Load()
        {
            try
            {
                string sqlquerry = "select distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.medicalrecordid_next as madieutrisau, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then 'Kết thúc' else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, CASE medicalrecord.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where medicalrecord.departmentid=department.departmentid) END as tenphong, medicalrecord.departmentgroupid as idkhoa, medicalrecord.departmentid as idphong, case medicalrecord.nextdepartmentid when 0 then 'Khoa cuối' end as lakhoacuoi, medicalrecord.hosobenhanid as mahosobenhan, medicalrecord.loaibenhanid as loaibenhanid FROM medicalrecord, hosobenhan,departmentgroup,department WHERE medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.hosobenhanid=hosobenhan.hosobenhanid and vienphiid=" + lblmavienphi_frm1.Text + " order by madieutri;";
                DataView dv_madieutri = new DataView(condb.GetDataTable_HIS(sqlquerry));
                gridControlMBA_TH.DataSource = dv_madieutri;
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Tim kiem

        private void btnMBATimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                HienThiThongTinBenhNhanDangChon("", "", "");

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _tieuchi = " vp.vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                string _tukhoatimkiem = "";

                if (txtMBAMaBenhNhan.Text.Trim() != "")
                {
                    long _mabenhnhan = Utilities.TypeConvertParse.ToInt64(txtMBAMaBenhNhan.Text.ToUpper().Replace("BN", ""));
                    _tukhoatimkiem = " and vp.patientid='" + _mabenhnhan + "' ";
                }
                else if (txtMBAMaBenhNhan.Text.Trim() == "" && txtSoTheBHYT.Text.Trim().Length == 15)
                {
                    _tukhoatimkiem = " and bh.bhytcode='" + txtSoTheBHYT.Text.Trim().ToUpper() + "' ";
                }

                string _sqlquerry = " SELECT DISTINCT vp.vienphiid as mavienphi, vp.patientid as mabenhnhan, hsba.patientname as tenbenhnhan, '' as tenbenhnhan_khongdau, hsba.gioitinhname, (case when hsba.birthday_year<>0 then cast(hsba.birthday_year as text) else to_char(hsba.birthday,'dd/MM/yyyy') end) as namsinh, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi, to_char(vp.vienphidate, 'yyyy-MM-dd HH24:MI:ss') as ngayvaovien, (case when vp.vienphistatus<>0 then to_char(vp.vienphidate_ravien, 'yyyy-MM-dd HH24:MI:ss') end) as ngayravien, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai, degp.departmentgroupname as khoa, (CASE vp.departmentid WHEN '0' THEN 'Hành chính' ELSE de.departmentname END) as phong, bh.bhytcode, (vp.chandoanravien_code || ' - ' || vp.chandoanravien) as tenbenh FROM vienphi vp inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join bhyt bh on bh.bhytid=vp.bhytid inner join departmentgroup degp on degp.departmentgroupid=vp.departmentgroupid left join department de on de.departmentid=vp.departmentid WHERE " + _tieuchi + _tukhoatimkiem + " ORDER BY mavienphi desc;";

                DataTable _dataTimKiem = condb.GetDataTable_HIS(_sqlquerry);
                if (_dataTimKiem != null && _dataTimKiem.Rows.Count > 0)
                {
                    if (txtMBAMaBenhNhan.Text.Trim() == "" && txtSoTheBHYT.Text.Trim() == "" && txtTenBenhNhan.Text.Trim() != "")
                    {
                        List<MoBenhAnTimKiemDTO> lstHoSoBenhAn = Utilities.DataTables.DataTableToList<MoBenhAnTimKiemDTO>(_dataTimKiem);
                        if (lstHoSoBenhAn != null && lstHoSoBenhAn.Count > 0)
                        {
                            foreach (var item_HSBA in lstHoSoBenhAn)
                            {
                                item_HSBA.tenbenhnhan_khongdau = Utilities.Common.String.Convert.UnSignVNese(item_HSBA.tenbenhnhan).ToLower();
                            }
                            List<MoBenhAnTimKiemDTO> lstHoSoBenhAn_TK = lstHoSoBenhAn.Where(o => o.tenbenhnhan_khongdau.Contains(txtTenBenhNhan.Text.Trim().ToLower())).ToList();
                            gridControlMoBenhAn.DataSource = lstHoSoBenhAn_TK;
                        }
                    }
                    else
                    {
                        gridControlMoBenhAn.DataSource = _dataTimKiem;
                    }
                }
                else
                {
                    gridControlMoBenhAn.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Events
        private void gridControlMoBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlMBA_TH.DataSource = null;
                // lấy giá trị tại dòng click chuột, lấy giá trị tại biến "mavienphi", "trangthai"
                var rowHandle = gridViewMoBenhAn.FocusedRowHandle;
                string mavienphi = gridViewMoBenhAn.GetRowCellValue(rowHandle, "mavienphi").ToString();
                string trangth = gridViewMoBenhAn.GetRowCellValue(rowHandle, "trangthai").ToString();
                string mabenhnhan = gridViewMoBenhAn.GetRowCellValue(rowHandle, "mabenhnhan").ToString();
                string tenbenhnhan = gridViewMoBenhAn.GetRowCellValue(rowHandle, "tenbenhnhan").ToString();
                // Kiểm tra nếu đã duyệt VP thì không cho mở bệnh án, nếu không thì hiển thị frmMoBenhAn_ThucHien
                if (trangth == "Đã duyệt VP")
                {
                    HienThiThongTinBenhNhanDangChon(mabenhnhan, mavienphi, tenbenhnhan);
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_NHAN_DA_DUYET_VIEN_PHI);
                    frmthongbao.Show();
                }
                else
                {
                    HienThiThongTinBenhNhanDangChon(mabenhnhan, mavienphi, tenbenhnhan);
                    gridControlMBA_TH_Load();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        internal void HienThiThongTinBenhNhanDangChon(string mabenhnhan, string mavienphi, string tenbenhnhan)
        {
            try
            {
                labelmabenhnhan.Text = mabenhnhan;
                lblmavienphi_frm1.Text = mavienphi;
                labeltenbenhnhan.Text = tenbenhnhan;
            }
            catch (Exception)
            {
            }
        }
        private void gridViewMBA_TH_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    //GridView view = sender as GridView;
                    e.Menu.Items.Clear();
                    DXMenuItem itemMoBenhAn = new DXMenuItem("Mở bệnh án"); // caption menu
                    itemMoBenhAn.Image = imageCollectionMBA.Images["unlocked_01.png"]; // icon cho menu
                    itemMoBenhAn.Shortcut = Shortcut.F6; // phím tắt
                    itemMoBenhAn.Click += new EventHandler(moBenhAnItem_Click);// thêm sự kiện click
                    e.Menu.Items.Add(itemMoBenhAn);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        internal void moBenhAnItem_Click(object sender, EventArgs e)
        {
            try
            {
                int dem = 0;
                int ktbdt_khoasau = 0;
                int kt_khoacuoi = 0;
                var rowHandle = gridViewMBA_TH.FocusedRowHandle;
                string mabn = gridViewMBA_TH.GetRowCellValue(rowHandle, "mabenhnhan").ToString();
                string madt = gridViewMBA_TH.GetRowCellValue(rowHandle, "madieutri").ToString();
                string trangth = gridViewMBA_TH.GetRowCellValue(rowHandle, "trangthai").ToString();
                string lakhoacuoi = gridViewMBA_TH.GetRowCellValue(rowHandle, "lakhoacuoi").ToString();
                string mavp = gridViewMBA_TH.GetRowCellValue(rowHandle, "mavienphi").ToString();
                string hosobn = gridViewMBA_TH.GetRowCellValue(rowHandle, "mahosobenhan").ToString();
                string idkhoa = gridViewMBA_TH.GetRowCellValue(rowHandle, "idkhoa").ToString();
                string idphong = gridViewMBA_TH.GetRowCellValue(rowHandle, "idphong").ToString();
                string tenbn = gridViewMBA_TH.GetRowCellValue(rowHandle, "tenbenhnhan").ToString();
                string khoa = gridViewMBA_TH.GetRowCellValue(rowHandle, "tenkhoa").ToString();
                string phong = gridViewMBA_TH.GetRowCellValue(rowHandle, "tenphong").ToString();
                string madtsau = gridViewMBA_TH.GetRowCellValue(rowHandle, "madieutrisau").ToString();
                string loaibenhan = gridViewMBA_TH.GetRowCellValue(rowHandle, "loaibenhanid").ToString();
                // Kiểm tra mã ĐT sau tiếp nhận vào BĐT hay chưa

                // Kiểm tra là khoa cuối
                if (lakhoacuoi == "Khoa cuối")
                {
                    if (trangth == "Kết thúc")
                    {
                        kt_khoacuoi += 1;
                        // Truyền biến sang form frmMoBenhAn_ThucHien_TT
                        frmMoBenhAn_ThucHien_TT frmMBA_hoi = new frmMoBenhAn_ThucHien_TT(mabn, madt, mavp, hosobn, kt_khoacuoi, idkhoa, idphong, tenbn, khoa, phong, ktbdt_khoasau, madtsau, loaibenhan);
                        frmMBA_hoi.ShowDialog();
                        gridControlMBA_TH.DataSource = null;
                        gridControlMBA_TH_Load();
                    }
                    else // trangth="Đang điều trị"
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_AN_DANG_MO);
                        frmthongbao.Show();
                    }
                }
                else // Không phải là khoa cuối cùng. Thì sẽ duyệt tất cả các mã điều trị để kiểm tra xem mã điều trị cuối cùng đóng hay mở
                {
                    if (trangth == "Kết thúc")
                    {
                        // Duyệt tất cả các row trong table để kiểm tra xem khoa cuối bệnh án là kết thúc hay ko?
                        for (int i = 0; i < gridViewMBA_TH.RowCount; i++)
                        {
                            if (gridViewMBA_TH.GetRowCellValue(i, "trangthai").ToString() == "Kết thúc" && gridViewMBA_TH.GetRowCellValue(i, "lakhoacuoi").ToString() == "Khoa cuối")
                            {
                                dem += 1;
                            }
                        }
                        if (dem == 1)
                        {
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao("Khoa cuối cùng đang đóng. Yêu cầu mở ở khoa cuối cùng trước!");
                            frmthongbao.Show();
                        }
                        else // else = khoa cuối cùng đang mở thì thực hiện mở bệnh án.
                        {
                            // Phát sinh 2 trường hợp mở BA: nếu khoa sau đó chưa tiếp nhận vào BĐT thì xóa BA ở buồng ĐT.
                            for (int i = 0; i < gridViewMBA_TH.RowCount; i++)
                            {
                                if (gridViewMBA_TH.GetRowCellValue(i, "idphong").ToString() == "0" && gridViewMBA_TH.GetRowCellValue(i, "madieutri").ToString() == madtsau)
                                {
                                    ktbdt_khoasau += 1; //Khoa sau chưa tiếp nhận vào BĐT
                                }
                                else
                                {
                                    ktbdt_khoasau += 0; // Khoa sau đã tiếp nhận vào BĐT
                                }
                            }
                            kt_khoacuoi += 0;
                            // Truyền biến sang form frmMoBenhAn_ThucHien_TT
                            frmMoBenhAn_ThucHien_TT frmMBA_hoi = new frmMoBenhAn_ThucHien_TT(mabn, madt, mavp, hosobn, kt_khoacuoi, idkhoa, idphong, tenbn, khoa, phong, ktbdt_khoasau, madtsau, loaibenhan);
                            frmMBA_hoi.ShowDialog();
                            gridControlMBA_TH.DataSource = null;
                            gridControlMBA_TH_Load();
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_AN_DANG_MO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridControlMBA_TH_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewMBA_TH.RowCount > 0)
                {
                    moBenhAnItem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void repositoryItemButtonEdit_MBA_Click(object sender, EventArgs e)
        {
            try
            {
                moBenhAnItem_Click(null, null);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridViewMBA_TH_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridViewMBA_TH_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void txtMBAMaBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoTheBHYT.Text = "";
                txtTenBenhNhan.Text = "";
                btnMBATimKiem.PerformClick();
            }
        }
        private void txtTenBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtSoTheBHYT.Text = "";
                    txtMBAMaBenhNhan.Text = "";
                    btnMBATimKiem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void txtSoTheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                txtTenBenhNhan.Text = "";
                txtMBAMaBenhNhan.Text = "";
                if (e.KeyCode == Keys.Enter && txtSoTheBHYT.Text.Length == 15)
                {
                    btnMBATimKiem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void txtMBAMaBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
        }
        private void txtSoTheBHYT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string sotheBHYT = txtSoTheBHYT.Text.Trim();
                if (txtSoTheBHYT.Text.Length > 15)
                {
                    txtSoTheBHYT.Text = sotheBHYT;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridViewMoBenhAn_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        #endregion
    }
}
