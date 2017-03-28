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

namespace MedicalLink.ChucNang
{
    public partial class ucMoBenhAn : UserControl
    {

        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucMoBenhAn()
        {
            InitializeComponent();
            // Hiển thị Text Hint
            txtMBAMaBenhNhan.ForeColor = SystemColors.GrayText;
            txtMBAMaBenhNhan.Text = "Mã bệnh nhân";
            this.txtMBAMaBenhNhan.Leave += new System.EventHandler(this.textBox1_Leave);
            this.txtMBAMaBenhNhan.Enter += new System.EventHandler(this.textBox1_Enter);
            HienThiThongTinBenhNhanDangChon(null, null, null);

            txtSoTheBHYT.ForeColor = SystemColors.GrayText;
            txtSoTheBHYT.Text = "Số thẻ BHYT";
            this.txtSoTheBHYT.Leave += new System.EventHandler(this.textBoxBHYT_Leave);
            this.txtSoTheBHYT.Enter += new System.EventHandler(this.textBoxBHYT_Enter);
        }

        // Hiển thị Text Hint
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtMBAMaBenhNhan.Text.Length == 0)
            {
                txtMBAMaBenhNhan.Text = "Mã bệnh nhân";
                txtMBAMaBenhNhan.ForeColor = SystemColors.GrayText;
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtMBAMaBenhNhan.Text == "Mã bệnh nhân")
            {
                txtMBAMaBenhNhan.Text = "";
                txtMBAMaBenhNhan.ForeColor = SystemColors.WindowText;
            }
        }

        //Hien thi text hint BHYT
        private void textBoxBHYT_Leave(object sender, EventArgs e)
        {
            if (txtSoTheBHYT.Text.Length == 0)
            {
                txtSoTheBHYT.Text = "Số thẻ BHYT";
                txtSoTheBHYT.ForeColor = SystemColors.GrayText;
            }
        }
        private void textBoxBHYT_Enter(object sender, EventArgs e)
        {
            if (txtSoTheBHYT.Text == "Số thẻ BHYT")
            {
                txtSoTheBHYT.Text = "";
                txtSoTheBHYT.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtMBAMaBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoTheBHYT.Text = "";
                btnMBATimKiem.PerformClick();
            }
        }

        private void txtMBAMaBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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
            catch (Exception)
            {
            }
        }
        private void txtSoTheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && txtSoTheBHYT.Text.Length == 15)
                {
                    btnMBATimKiem.PerformClick();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void btnMBATimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlMBA_TH.DataSource = null;
                HienThiThongTinBenhNhanDangChon(null, null, null);
                string sqlquerry = "";
                if (txtMBAMaBenhNhan.Text != "Mã bệnh nhân")
                {
                    sqlquerry = "SELECT DISTINCT vienphi.vienphiid as mavienphi, vienphi.patientid as mabenhnhan,hosobenhan.patientname as tenbenhnhan, vienphi.vienphidate as ngayvaovien, vienphi.vienphidate_ravien as ngayravien, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, departmentgroup.departmentgroupname as khoa, CASE vienphi.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where vienphi.departmentid=department.departmentid) END as phong, bhyt.bhytcode FROM vienphi,hosobenhan,departmentgroup,department,bhyt WHERE vienphi.hosobenhanid=hosobenhan.hosobenhanid and bhyt.bhytid=vienphi.bhytid and vienphi.departmentgroupid=departmentgroup.departmentgroupid and vienphi.patientid=" + txtMBAMaBenhNhan.Text + " order by mavienphi desc";
                }
                else if (txtMBAMaBenhNhan.Text == "Mã bệnh nhân" && txtSoTheBHYT.Text.Trim().Length == 15)
                {
                    sqlquerry = "SELECT DISTINCT vienphi.vienphiid as mavienphi, vienphi.patientid as mabenhnhan,hosobenhan.patientname as tenbenhnhan, vienphi.vienphidate as ngayvaovien, vienphi.vienphidate_ravien as ngayravien, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, departmentgroup.departmentgroupname as khoa, CASE vienphi.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where vienphi.departmentid=department.departmentid) END as phong,bhyt.bhytcode FROM vienphi,hosobenhan,departmentgroup,department,bhyt WHERE vienphi.hosobenhanid=hosobenhan.hosobenhanid and vienphi.bhytid=bhyt.bhytid and vienphi.departmentgroupid=departmentgroup.departmentgroupid and bhyt.bhytcode='" + txtSoTheBHYT.Text.Trim().ToUpper() + "' order by mavienphi desc";
                }

                DataView dv = new DataView(condb.getDataTable(sqlquerry));
                gridControlMoBenhAn.DataSource = dv;

                if (gridViewMoBenhAn.RowCount == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_NHAN_DA_DUYET_VIEN_PHI);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        // Đổi màu row khi kích chuột vào dòng đó, và đổi cỡ chữ
        private void gridViewMoBenhAn_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void ucMoBenhAn_Load(object sender, EventArgs e)
        {
            txtMBAMaBenhNhan.Focus();
        }

        internal void gridControlMBA_TH_Load()
        {
            try
            {
                string sqlquerry = "select distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.medicalrecordid_next as madieutrisau, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then 'Kết thúc' else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, CASE medicalrecord.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where medicalrecord.departmentid=department.departmentid) END as tenphong, medicalrecord.departmentgroupid as idkhoa, medicalrecord.departmentid as idphong, case medicalrecord.nextdepartmentid when 0 then 'Khoa cuối' else 'None' end as lakhoacuoi, medicalrecord.hosobenhanid as mahosobenhan, medicalrecord.loaibenhanid as loaibenhanid FROM medicalrecord, hosobenhan,departmentgroup,department WHERE medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.hosobenhanid=hosobenhan.hosobenhanid and vienphiid=" + lblmavienphi_frm1.Text + " order by madieutri;";
                DataView dv_madieutri = new DataView(condb.getDataTable(sqlquerry));
                gridControlMBA_TH.DataSource = dv_madieutri;
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        internal void HienThiThongTinBenhNhanDangChon(string mabenhnhan, string mavienphi, string tenbenhnhan)
        {
            try
            {
                if (mabenhnhan != null && mavienphi != null && tenbenhnhan != null)
                {
                    labelmabenhnhan.Text = mabenhnhan;
                    lblmavienphi_frm1.Text = mavienphi;
                    labeltenbenhnhan.Text = tenbenhnhan;
                }
                else
                {
                    labelmabenhnhan.Text = "";
                    lblmavienphi_frm1.Text = "";
                    labeltenbenhnhan.Text = "";
                }

            }
            catch (Exception)
            {
            }
        }

        private void gridViewMBA_TH_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void gridViewMBA_TH_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        internal void moBenhAnItem_Click(object sender, EventArgs e)
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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_AN_DANG_MO);
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
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Khoa cuối cùng đang đóng. Yêu cầu mở ở khoa cuối cùng trước!");
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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_AN_DANG_MO);
                    frmthongbao.Show();
                }
            }
        }

        private void repositoryItemButtonEdit_MBA_Click(object sender, EventArgs e)
        {
            try
            {
                moBenhAnItem_Click(null, null);
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }

    }
}
