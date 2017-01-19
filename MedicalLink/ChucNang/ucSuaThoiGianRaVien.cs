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
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
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

        // Tìm kiếm để hiển thị danh sách mã điều trị của viện phí vừa nhập vào.
        private void btnSuaThoiGianRV_Click(object sender, EventArgs e)
        {
            string sqlquerry;
            try
            {
                if (txtMaVienPhi.Text == "Mã viện phí") // tìm theo mã BN
                {
                    sqlquerry = "select distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, department.departmentname as tenphong, case medicalrecord.nextdepartmentid when 0 then '1' else '0' end as lakhoacuoi FROM medicalrecord, hosobenhan,departmentgroup,department,vienphi where medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.departmentid=department.departmentid and medicalrecord.patientid=hosobenhan.patientid and vienphi.vienphiid=medicalrecord.vienphiid and medicalrecord.patientid=" + txtMaBN.Text + " order by madieutri";

                }
                else
                {
                    sqlquerry = "select distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, department.departmentname as tenphong, case medicalrecord.nextdepartmentid when 0 then '1' else '0' end as lakhoacuoi FROM medicalrecord, hosobenhan,departmentgroup,department,vienphi where medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.departmentid=department.departmentid and medicalrecord.patientid=hosobenhan.patientid and vienphi.vienphiid=medicalrecord.vienphiid and medicalrecord.vienphiid=" + txtMaVienPhi.Text + " order by madieutri";
                }
                DataView dv = new DataView(condb.getDataTable(sqlquerry));
                gridControlSuaThoiGianRaVien.DataSource = dv;
                dateThoiGianSua.Enabled = false;
                btnSuaThoiGianOK.Enabled = false;

                if (gridViewSuaThoiGianRV.RowCount == 0)
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

        // Click vào 1 row thì lấy thông tin row đó
        private void gridControlSuaThoiGianRaVien_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột, lấy giá trị tại biến "thoigianravien"
                var rowHandle = gridViewSuaThoiGianRV.FocusedRowHandle;
                string tgrv = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "thoigianravien").ToString();
                string trangth = gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "trangthai").ToString();
                int khoacuoi = Convert.ToInt16(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "lakhoacuoi").ToString());
                // Kiểm tra nếu chưa có TG ra viện thì hiển thị hộp thoại thông báo, đồng thời không có sửa ngày ra viện
                if (trangth == "Đã duyệt VP")
                {
                    //MessageBox.Show("Bệnh nhân đã duyệt viện phí", "Thông báo");
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Bệnh nhân đã duyệt viện phí";
                    dateThoiGianSua.Enabled = false;
                    btnSuaThoiGianOK.Enabled = false;
                }
                else if (trangth == "Đã đóng BA")
                {
                    dateThoiGianHienTai.Value = Convert.ToDateTime(tgrv);
                    dateThoiGianSua.Value = Convert.ToDateTime(tgrv);
                    dateThoiGianSua.Enabled = true;
                    btnSuaThoiGianOK.Enabled = true;
                }
                else
                {
                    if (tgrv != "1/1/0001 12:00:00 AM" && khoacuoi == 0)
                    {
                        dateThoiGianHienTai.Value = Convert.ToDateTime(tgrv);
                        dateThoiGianSua.Value = Convert.ToDateTime(tgrv);
                        dateThoiGianSua.Enabled = true;
                        btnSuaThoiGianOK.Enabled = true;
                    }
                    else
                    {
                        //MessageBox.Show("Bệnh nhân chưa ra viện", "Thông báo");
                        timerThongBao.Start();
                        lblThongBao.Visible = true;
                        lblThongBao.Text = "Bệnh nhân chưa ra viện";
                        dateThoiGianSua.Enabled = false;
                        btnSuaThoiGianOK.Enabled = false;
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        // Sửa thời gian ra viện và chuyển trạng thái BN đã đóng BA (=99) nếu trạng thái của mã DDT đó đang là mở
        private void btnSuaThoiGianOK_Click(object sender, EventArgs e)
        {
            // Lấy thời gian hiện tại
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                // lấy giá trị tại dòng click chuột, lấy giá trị tại biến "madieutri"
                var rowHandle = gridViewSuaThoiGianRV.FocusedRowHandle;
                int medireco = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "madieutri").ToString());
                int mavp = Convert.ToInt32(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "mavienphi").ToString());

                //int khoacuoi=Convert.ToInt16(gridViewSuaThoiGianRV.GetRowCellValue(rowHandle, "lakhoacuoi").ToString());
                try
                {
                    string sqlxecute = "update medicalrecord set thoigianravien='" + dateThoiGianSua.Text + "', medicalrecordstatus='99' where medicalrecordid= '" + medireco + "' and thoigianravien<>'0001-01-01 00:00:00'; update vienphi set vienphidate_ravien='" + dateThoiGianSua.Text + "' where vienphiid=" + mavp + " and vienphistatus<>0";
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa TG ra viện BN VP: " + mavp + " mã ĐT: " + medireco + " từ TG: " + dateThoiGianHienTai.Text + " thành TG: " + dateThoiGianSua.Text + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlxecute);
                    condb.ExecuteNonQuery(sqlinsert_log);
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Đã sửa thành công, xin vui lòng kiểm tra lại!";
                    gridControlSuaThoiGianRaVien.DataSource = null;
                    btnSuaThoiGianRV_Click(null, null);

                }
                catch (Exception)
                {
                    MessageBox.Show("Không thể thực hiện được. Kiểm tra lại BN đã ra viện hay chưa!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        // Hiển thị màu khi click chuột vào
        private void gridViewSuaThoiGianRV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void usSuaThoiGianRaVien_Load(object sender, EventArgs e)
        {
            txtMaBN.Focus();
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
                txtMaBN.ForeColor = SystemColors.GrayText;
                txtMaBN.Text = "Mã BN";
                this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
                this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
            }
        }

        private void txtMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaBN.Text !="")
            {
                // Hiển thị Text Hint Mã VP
                txtMaVienPhi.ForeColor = SystemColors.GrayText;
                txtMaVienPhi.Text = "Mã viện phí";
                this.txtMaVienPhi.Leave += new System.EventHandler(this.txtMaVienPhi_Leave);
                this.txtMaVienPhi.Enter += new System.EventHandler(this.txtMaVienPhi_Enter);
            }
        }

    }
}
