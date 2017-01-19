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
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaTGDuyetVP : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucSuaTGDuyetVP()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã BN
            txtMaBenhNhan.ForeColor = SystemColors.GrayText;
            txtMaBenhNhan.Text = "Mã BN";
            this.txtMaBenhNhan.Leave += new System.EventHandler(this.txtMaBenhNhan_Leave);
            this.txtMaBenhNhan.Enter += new System.EventHandler(this.txtMaBenhNhan_Enter);
            // Hiển thị Text Hint Mã VP
            txtMaVienPhi.ForeColor = SystemColors.GrayText;
            txtMaVienPhi.Text = "Mã viện phí";
            this.txtMaVienPhi.Leave += new System.EventHandler(this.txtMaVienPhi_Leave);
            this.txtMaVienPhi.Enter += new System.EventHandler(this.txtMaVienPhi_Enter);
        }
        // Hiển thị Text Hint
        private void txtMaBenhNhan_Leave(object sender, EventArgs e)
        {
            if (txtMaBenhNhan.Text.Length == 0)
            {
                txtMaBenhNhan.Text = "Mã BN";
                txtMaBenhNhan.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint
        private void txtMaBenhNhan_Enter(object sender, EventArgs e)
        {
            if (txtMaBenhNhan.Text == "Mã BN")
            {
                txtMaBenhNhan.Text = "";
                txtMaBenhNhan.ForeColor = SystemColors.WindowText;
            }
        }

        // Hiển thị Text Hint mã VP
        private void txtMaVienPhi_Leave(object sender, EventArgs e)
        {
            if (txtMaVienPhi.Text.Length == 0)
            {
                txtMaVienPhi.Text = "Mã viện phí";
                txtMaVienPhi.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint mã Vp
        private void txtMaVienPhi_Enter(object sender, EventArgs e)
        {
            if (txtMaVienPhi.Text == "Mã viện phí")
            {
                txtMaVienPhi.Text = "";
                txtMaVienPhi.ForeColor = SystemColors.WindowText;
            }
        }

        //Tìm kiếm các mã Vp theo mã BN
        private void btnTimKiemMaVP_Click(object sender, EventArgs e)
        {
            string sqltimkiem;
            try
            {
                if (txtMaVienPhi.Text=="Mã viện phí")// nếu không nhập mã VP
                {
                    sqltimkiem = "SELECT DISTINCT vienphi.vienphiid AS mavienphi, vienphi.patientid AS mabenhnhan, hosobenhan.patientname AS tenbenhnhan, vienphi.vienphidate AS thoigianvaovien, vienphi.vienphidate_ravien AS thoigianravien, CASE vienphi.vienphistatus_vp WHEN 1 THEN 'Đã duyệt VP' ELSE 'Chưa duyệt VP' END AS trangthai, vienphi.duyet_ngayduyet_vp AS thoigiANDuyetkt, departmentgroup.departmentgroupname AS tenkhoa, department.departmentname AS tenphong FROM vienphi,hosobenhan,departmentgroup,department WHERE vienphi.hosobenhanid = hosobenhan.hosobenhanid AND vienphi.departmentgroupid=departmentgroup.departmentgroupid AND vienphi.departmentid=department.departmentid AND vienphi.patientid=" + txtMaBenhNhan.Text + " ORDER BY mavienphi;";
                }
                else
                {
                    sqltimkiem = "SELECT DISTINCT vienphi.vienphiid AS mavienphi, vienphi.patientid AS mabenhnhan, hosobenhan.patientname AS tenbenhnhan, vienphi.vienphidate AS thoigianvaovien, vienphi.vienphidate_ravien AS thoigianravien, CASE vienphi.vienphistatus_vp WHEN 1 THEN 'Đã duyệt VP' ELSE 'Chưa duyệt VP' END AS trangthai, vienphi.duyet_ngayduyet_vp AS thoigiANDuyetkt, departmentgroup.departmentgroupname AS tenkhoa, department.departmentname AS tenphong FROM vienphi,hosobenhan,departmentgroup,department WHERE vienphi.hosobenhanid = hosobenhan.hosobenhanid AND vienphi.departmentgroupid=departmentgroup.departmentgroupid AND vienphi.departmentid=department.departmentid AND vienphi.vienphiid=" + txtMaVienPhi.Text + " ORDER BY mavienphi;";
                }
                DataView dv = new DataView(condb.getDataTable(sqltimkiem));
                gridControlSuaTGDuyetVP.DataSource = dv;
                dateThoiGianSua.Enabled = false;
                btnSuaTGDuyetOK.Enabled = false;

                if (gridViewSuaTGDuyetVP.RowCount==0)
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Không tìm thấy hồ sơ nào như yêu cầu \n             Vui lòng kiểm tra lại.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ucSuaTGDuyetVP_Load(object sender, EventArgs e)
        {
            txtMaBenhNhan.Focus();
        }

        //Chặn không cho nhập chữ
        private void txtMaBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiemMa.PerformClick();
            }
        }

        private void txtMaBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

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
                btnTimKiemMa.PerformClick();
            }
        }

        //Màu cảu row khi focus
        private void gridViewSuaTGDuyetVP_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridControlSuaTGDuyetVP_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewSuaTGDuyetVP.FocusedRowHandle;
                string thoigianduyet = gridViewSuaTGDuyetVP.GetRowCellValue(rowHandle, "thoigianduyetkt").ToString();
                string trangth = gridViewSuaTGDuyetVP.GetRowCellValue(rowHandle, "trangthai").ToString();
                if (trangth == "Đã duyệt VP")
                {
                    dateThoiGianHienTai.Value = Convert.ToDateTime(thoigianduyet);
                    dateThoiGianSua.Value = Convert.ToDateTime(thoigianduyet);
                    dateThoiGianSua.Enabled = true;
                    btnSuaTGDuyetOK.Enabled = true;
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Bệnh nhân chưa duyệt kế toán viện phí";
                }
            }
            catch
            {

            }
        }

        private void btnSuaTGDuyetOK_Click(object sender, EventArgs e)
        {
            // Lấy thời gian hiện tại
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                var rowHandle = gridViewSuaTGDuyetVP.FocusedRowHandle;
                int mavp = Convert.ToInt32(gridViewSuaTGDuyetVP.GetRowCellValue(rowHandle, "mavienphi").ToString());

                try
                {
                    string sqlupdate = "UPDATE vienphi SET duyet_ngayduyet_bh='" + dateThoiGianSua.Text + "',  duyet_ngayduyet_vp='" + dateThoiGianSua.Text + "' WHERE vienphiid='" + mavp + "';";
                    condb.ExecuteNonQuery(sqlupdate);
                    string sqlluulog = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa TG duyệt VP BN: " + txtMaBenhNhan.Text + " mã VP: " + mavp + " từ TG: " + dateThoiGianHienTai.Text + " thành TG: " + dateThoiGianSua.Text + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlluulog);
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Đã sửa thành công, xin vui lòng kiểm tra lại!";
                    gridControlSuaTGDuyetVP.DataSource = null;
                    btnTimKiemMaVP_Click(null, null);

                }
                catch
                {

                }
            }
            catch
            {

            }

        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void txtMaVienPhi_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaVienPhi.Text != "Mã viện phí")
            {
                // Hiển thị Text Hint Mã BN
                txtMaBenhNhan.ForeColor = SystemColors.GrayText;
                txtMaBenhNhan.Text = "Mã BN";
                this.txtMaBenhNhan.Leave += new System.EventHandler(this.txtMaBenhNhan_Leave);
                this.txtMaBenhNhan.Enter += new System.EventHandler(this.txtMaBenhNhan_Enter);
            }
        }

        private void txtMaBenhNhan_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaBenhNhan.Text != "")
            {
                // Hiển thị Text Hint Mã BN
                txtMaVienPhi.ForeColor = SystemColors.GrayText;
                txtMaVienPhi.Text = "Mã viện phí";
                this.txtMaVienPhi.Leave += new System.EventHandler(this.txtMaVienPhi_Leave);
                this.txtMaVienPhi.Enter += new System.EventHandler(this.txtMaVienPhi_Enter);
            }
        }

    }
}
