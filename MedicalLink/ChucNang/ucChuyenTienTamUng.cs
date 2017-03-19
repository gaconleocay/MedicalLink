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
    public partial class ucChuyenTienTamUng : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public ucChuyenTienTamUng()
        {
            InitializeComponent();
            // Hiển thị Text Hint
            txtChuyenTienVP1.ForeColor = SystemColors.GrayText;
            txtChuyenTienVP1.Text = "Chuyển từ VP";
            this.txtChuyenTienVP1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.txtChuyenTienVP1.Enter += new System.EventHandler(this.textBox1_Enter);

            txtSoTheBHYT.ForeColor = SystemColors.GrayText;
            txtSoTheBHYT.Text = "Số thẻ BHYT";
            this.txtSoTheBHYT.Leave += new System.EventHandler(this.textBoxBHYT_Leave);
            this.txtSoTheBHYT.Enter += new System.EventHandler(this.textBoxBHYT_Enter);
        }
        // Hiển thị Text Hint 1
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (txtChuyenTienVP1.Text.Length == 0)
            {
                txtChuyenTienVP1.Text = "Chuyển từ VP";
                txtChuyenTienVP1.ForeColor = SystemColors.GrayText;
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (txtChuyenTienVP1.Text == "Chuyển từ VP")
            {
                txtChuyenTienVP1.Text = "";
                txtChuyenTienVP1.ForeColor = SystemColors.WindowText;
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
        private void txtChuyenTienVP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtChuyenTienVP1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSoTheBHYT.Text = "";
                btnChuyenTienTK.PerformClick();
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
                    btnChuyenTienTK.PerformClick();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnChuyenTienTK_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlquerry = "";
                if (txtChuyenTienVP1.Text.Trim() != "Chuyển từ VP")
                {
                    // Querry lấy dữ liệu về phiếu tạm ứng tiền khi nhập mã VP
                    sqlquerry = "select distinct bill.billid as maphieuthu, bill.patientid as mabenhnhan, bill.vienphiid as mavienphi, bill.patientname as tenbenhnhan, 'Tạm ứng' as loaiphieu, bill.datra as sotien, bill.billdate as ngaythu, case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else '' end as trangthai from bill, vienphi where bill.loaiphieuthuid=2 and vienphi.vienphiid=bill.vienphiid and bill.vienphiid=" + txtChuyenTienVP1.Text + " order by maphieuthu;";
                }
                else if (txtChuyenTienVP1.Text.Trim()  == "Chuyển từ VP" && txtSoTheBHYT.Text.Trim().Length == 15)
                {
                    // Querry lấy dữ liệu về phiếu tạm ứng tiền khi nhập So the BHYT
                    sqlquerry = "select distinct bill.billid as maphieuthu, bill.patientid as mabenhnhan, bill.vienphiid as mavienphi, bill.patientname as tenbenhnhan, 'Tạm ứng' as loaiphieu, bill.datra as sotien, bill.billdate as ngaythu, case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else '' end as trangthai from bill, vienphi,bhyt where bill.loaiphieuthuid=2 and vienphi.vienphiid=bill.vienphiid and bhyt.bhytid=vienphi.bhytid and bhyt.bhytcode='" + txtSoTheBHYT.Text.Trim().ToUpper() + "' order by maphieuthu;";
                }

                DataView dv = new DataView(condb.getDataTable(sqlquerry));
                gridControlChuyenTien.DataSource = dv;

                if (gridViewChuyenTien.RowCount == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
                /*
                NpgsqlCommand command = new NpgsqlCommand(sqlquerry, conn);
                command.CommandType = CommandType.Text;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(command);
                dt = new DataTable();
                */
                txtChuyenTienVP2.Enabled = false;
                txtChuyenTienVP2.Text = "";
                btnChuyenTienOK.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void gridViewChuyenTien_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }
        private void gridControlChuyenTien_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột, lấy giá trị tại biến "maphieuthu"
                var rowHandle = gridViewChuyenTien.FocusedRowHandle;
                //string obj = gridViewChuyenTien.GetRowCellValue(rowHandle, "maphieuthu").ToString();
                string trangth = Convert.ToString(gridViewChuyenTien.GetRowCellValue(rowHandle, "trangthai").ToString());
                if (trangth == "Đã duyệt VP")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_NHAN_DA_DUYET_VIEN_PHI);
                    frmthongbao.Show();
                }
                else
                {
                    txtChuyenTienVP2.Enabled = true;
                    btnChuyenTienOK.Enabled = true;
                    //txtChuyenTienVP2.EditValue = obj.ToString();
                }
            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        private void btnChuyenTienOK_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // lấy giá trị tại dòng click chuột, lấy giá trị tại biến "maphieuthu"
                var rowHandle = gridViewChuyenTien.FocusedRowHandle;
                long maphieuth = Convert.ToInt64(gridViewChuyenTien.GetRowCellValue(rowHandle, "maphieuthu").ToString());
                string sotientu = Convert.ToString(gridViewChuyenTien.GetRowCellValue(rowHandle, "sotien").ToString());
                long mavienphi = Convert.ToInt64(gridViewChuyenTien.GetRowCellValue(rowHandle, "mavienphi").ToString());
                //string trangth = Convert.ToString(gridViewChuyenTien.GetRowCellValue(rowHandle, "trangthai").ToString());

                // Querry thực hiện
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn chuyển " + sotientu.ToString() + " từ BN VP " + txtChuyenTienVP1.Text + " sang BN VP " + txtChuyenTienVP2.Text + " không ???", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // thực thi câu lệnh update và lưu log
                        string sqlxecute = "update bill set vienphiid=" + txtChuyenTienVP2.Text + ", patientid=(select patientid from vienphi where vienphiid=" + txtChuyenTienVP2.Text + "), patientname=(select patientname from hosobenhan where hosobenhanid=(select hosobenhanid from vienphi where vienphiid=" + txtChuyenTienVP2.Text + ")), billremark='Tạm ứng cho viện phí VP' || right(('00000000' || '" + txtChuyenTienVP2.Text + "'),9), lydohuyphieu='Chuyển từ mã VP: ' || " + mavienphi + " where billid=" + maphieuth;
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển TƯ " + sotientu + " từ mã VP: " + mavienphi + " sang mã VP: " + txtChuyenTienVP2.Text + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlxecute);
                        condb.ExecuteNonQuery(sqlinsert_log);
                        MessageBox.Show("Đã chuyển " + sotientu.ToString() + " từ BN VP " + mavienphi + " sang BN VP " + txtChuyenTienVP2.Text, "Thông báo");
                        // load lại dữ liệu của form
                        gridControlChuyenTien.DataSource = null;
                        btnChuyenTienTK_Click(null, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể thực hiện được!", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        // Đổi màu row khi được chọn
        private void gridViewChuyenTien_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void txtChuyenTienVP2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChuyenTienOK.Focus();
            }
        }

        private void ucChuyenTien_Load(object sender, EventArgs e)
        {
            txtChuyenTienVP1.Focus();
        }



    }
}
