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
using DevExpress.Utils.Menu;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucXuLyBNBoKhoa : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucXuLyBNBoKhoa()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã viện phí
            txtBNBKMaVP.ForeColor = SystemColors.GrayText;
            txtBNBKMaVP.Text = "Mã viện phí";
            this.txtBNBKMaVP.Leave += new System.EventHandler(this.txtBNBKMaVP_Leave);
            this.txtBNBKMaVP.Enter += new System.EventHandler(this.txtBNBKMaVP_Enter);
            // Hiển thị Text Hint Mã BN
            txtBNBKMaBN.ForeColor = SystemColors.GrayText;
            txtBNBKMaBN.Text = "Mã bệnh nhân";
            this.txtBNBKMaBN.Leave += new System.EventHandler(this.txtBNBKMaBN_Leave);
            this.txtBNBKMaBN.Enter += new System.EventHandler(this.txtBNBKMaBN_Enter);
        }

        #region Load
        private void ucXuLyBNBoKhoa_Load(object sender, EventArgs e)
        {
            txtBNBKMaBN.Focus();
        }

        #endregion

        #region Custom
        // Hiển thị Text Hint Mã viện phí
        private void txtBNBKMaVP_Leave(object sender, EventArgs e)
        {
            if (txtBNBKMaVP.Text.Length == 0)
            {
                txtBNBKMaVP.Text = "Mã viện phí";
                txtBNBKMaVP.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtBNBKMaVP_Enter(object sender, EventArgs e)
        {
            if (txtBNBKMaVP.Text == "Mã viện phí")
            {
                txtBNBKMaVP.Text = "";
                txtBNBKMaVP.ForeColor = SystemColors.WindowText;
            }
        }

        // Hiển thị Text Hint Mã bệnh nhân
        private void txtBNBKMaBN_Leave(object sender, EventArgs e)
        {
            if (txtBNBKMaBN.Text.Length == 0)
            {
                txtBNBKMaBN.Text = "Mã bệnh nhân";
                txtBNBKMaBN.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã bệnh nhân
        private void txtBNBKMaBN_Enter(object sender, EventArgs e)
        {
            if (txtBNBKMaBN.Text == "Mã bệnh nhân")
            {
                txtBNBKMaBN.Text = "";
                txtBNBKMaBN.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtBNBKMaBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtBNBKMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBNBKTimKiem.PerformClick();
            }
        }

        private void txtBNBKMaVP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBNBKTimKiem.PerformClick();
            }
        }

        private void gridViewBNBK_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void txtBNBKMaVP_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBNBKMaVP.Text != "Mã viện phí")
            {
                // Hiển thị Text Hint Mã BN
                txtBNBKMaBN.ForeColor = SystemColors.GrayText;
                txtBNBKMaBN.Text = "Mã bệnh nhân";
                this.txtBNBKMaBN.Leave += new System.EventHandler(this.txtBNBKMaBN_Leave);
                this.txtBNBKMaBN.Enter += new System.EventHandler(this.txtBNBKMaBN_Enter);
            }
        }


        private void txtBNBKMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBNBKMaBN.Text != "")
            {
                // Hiển thị Text Hint Mã BN
                txtBNBKMaVP.ForeColor = SystemColors.GrayText;
                txtBNBKMaVP.Text = "Mã viện phí";
                this.txtBNBKMaVP.Leave += new System.EventHandler(this.txtBNBKMaVP_Leave);
                this.txtBNBKMaVP.Enter += new System.EventHandler(this.txtBNBKMaVP_Enter);
            }
        }
        #endregion

        #region Tim kiem
        private void btnBNBKTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string _timkiem = "";
                if (txtBNBKMaVP.Text == "Mã viện phí") // tìm theo mã BN
                {
                    _timkiem = " and medicalrecord.patientid = " + txtBNBKMaBN.Text;
                }
                else // tìm theo mã VP
                {
                    _timkiem = " and medicalrecord.patientid = " + txtBNBKMaVP.Text;
                }
                string sqlquerry = "SELECT distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then 'Kết thúc' else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, medicalrecord.departmentgroupid, medicalrecord.departmentid, (CASE medicalrecord.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where medicalrecord.departmentid=department.departmentid) END) as tenphong, medicalrecord.loaibenhanid as loaibenhan FROM medicalrecord, hosobenhan,departmentgroup,department WHERE medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.hosobenhanid=hosobenhan.hosobenhanid " + _timkiem + " ORDER BY madieutri;";

                DataView dv = new DataView(condb.GetDataTable_HIS(sqlquerry));
                gridControlBNBK.DataSource = dv;

                if (gridViewBNBK.RowCount == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Events
        private void gridViewBNBK_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                e.Menu.Items.Clear();

                DXMenuItem itemChuyenHanhChinh = new DXMenuItem("Chuyển mã ĐT ra phòng hành chính"); // caption menu
                itemChuyenHanhChinh.Image = imMenu.Images["HanhChinh.png"]; // icon cho menu
                //itemChuyenHanhChinh.Shortcut = Shortcut.F6; // phím tắt
                itemChuyenHanhChinh.Click += new EventHandler(itemChuyenHanhChinh_Click);// thêm sự kiện click
                e.Menu.Items.Add(itemChuyenHanhChinh);

                DXMenuItem itemXoaMaDT = new DXMenuItem("Xóa mã điều trị nội trú");
                itemXoaMaDT.Image = imMenu.Images["XoaDT.png"];
                //itemXoaMaDT.Shortcut = Shortcut.F6;
                itemXoaMaDT.Click += new EventHandler(itemXoaMaDT_Click);
                e.Menu.Items.Add(itemXoaMaDT);

                DXMenuItem itemXoaMaDTCNT = new DXMenuItem("Xóa mã điều trị + Chuyển TT ngoại trú");
                itemXoaMaDTCNT.Image = imMenu.Images["XoaDTHC.png"];
                //itemXoaMaDTCNT.Shortcut = Shortcut.F6;
                itemXoaMaDTCNT.Click += new EventHandler(itemXoaMaDTCNT_Click);
                e.Menu.Items.Add(itemXoaMaDTCNT);

                DXMenuItem itemXoaToanBA = new DXMenuItem("Xóa toàn bộ bệnh án");
                itemXoaToanBA.Image = imMenu.Images["XoaBA.png"];
                //itemXoaToanBA.Shortcut = Shortcut.F6;
                itemXoaToanBA.Click += new EventHandler(itemXoaToanBA_Click);
                e.Menu.Items.Add(itemXoaToanBA);
                itemXoaToanBA.BeginGroup = true;

            }
        }

        private void itemChuyenHanhChinh_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                long madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                long mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                long mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                long loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());
                string tenph = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "tenphong").ToString());
                string departmentgroupid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentgroupid").ToString();
                string departmentid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentid").ToString();

                if (trangth == "Đang điều trị")
                {
                    if (!KiemTraTonTaiDichVu(mavp, departmentgroupid, departmentid))
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được!\nVì có dịch vụ phát sinh trong buồng điều trị!");
                        frmthongbao.Show();
                    }
                    else
                    {
                        if (tenph == "Hành chính")
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Mã điều trị đang ở phòng hành chính !");
                            frmthongbao.Show();
                        }
                        else
                        {
                            if (loaiba == 24)
                            {
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Bệnh nhân đang ở ngoại trú!");
                                frmthongbao.Show();
                            }
                            else
                            {
                                // Querry thực hiện
                                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn chuyển BN ra ngoài phòng hành chính ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    try
                                    {
                                        // thực thi câu lệnh update và lưu log
                                        string sqlxecute = "UPDATE medicalrecord SET medicalrecordstatus='0', departmentid='0' WHERE medicalrecordid='" + madt + "';";
                                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển BN: " + mabn + " mã VP: " + mavp + " mã điều trị: " + madt + " ra phòng hành chính','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"+ mavp + "', 'TOOL_05');";
                                        condb.ExecuteNonQuery_HIS(sqlxecute);
                                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển mã điều trị ra phòng hành chính thành công!");
                                        frmthongbao.Show();
                                        // load lại dữ liệu của form
                                        gridControlBNBK.DataSource = null;
                                        btnBNBKTimKiem_Click(null, null);
                                    }
                                    catch (Exception)
                                    {
                                        MessageBox.Show("Không thể thực hiện được! \nCó lỗi xảy ra", "Thông báo");
                                    }
                                }
                                //else if (dialogResult == DialogResult.No)
                                //{
                                //    //do something else
                                //}
                            }
                        }

                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Mã điều trị đã kết thúc!");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void itemXoaMaDT_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                long madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                long mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                long mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                long loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());
                string departmentgroupid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentgroupid").ToString();
                string departmentid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentid").ToString();

                if (trangth == "Đang điều trị")
                {
                    if (!KiemTraTonTaiDichVu(mavp, departmentgroupid, departmentid))
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Vì có dịch vụ phát sinh trong buồng điều trị");
                        frmthongbao.Show();
                    }
                    else
                    {
                        if (loaiba == 24)
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Bệnh nhân đang ở ngoại trú!");
                            frmthongbao.Show();
                        }
                        else
                        {
                            // Querry thực hiện
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mã điều trị: " + madt + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (dialogResult == DialogResult.Yes)
                            {
                                try
                                {
                                    // thực thi câu lệnh update và lưu log
                                    string sqlxecute = "DELETE FROM medicalrecord WHERE medicalrecordid='" + madt + "';";
                                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa mã điều trị: " + madt + " của BN: " + mabn + " mã VP: " + mavp + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_05');";
                                    condb.ExecuteNonQuery_HIS(sqlxecute);
                                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Xóa mã điều trị thành công!");
                                    frmthongbao.Show();
                                    // load lại dữ liệu của form
                                    gridControlBNBK.DataSource = null;
                                    btnBNBKTimKiem_Click(null, null);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Không thể thực hiện được! \nCó lỗi xảy ra", "Thông báo");
                                }
                            }
                            //else if (dialogResult == DialogResult.No)
                            //{
                            //    //do something else
                            //}
                        }

                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Mã điều trị đã kết thúc!");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void itemXoaMaDTCNT_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                long madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                long mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                long mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                long loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());
                string departmentgroupid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentgroupid").ToString();
                string departmentid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentid").ToString();

                if (trangth == "Đang điều trị")
                {
                    if (!KiemTraTonTaiDichVu(mavp, departmentgroupid, departmentid))
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Vì có dịch vụ phát sinh trong buồng điều trị");
                        frmthongbao.Show();
                    }
                    else
                    {
                        if (loaiba == 24)
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Bệnh nhân đang ở ngoại trú!");
                            frmthongbao.Show();
                        }
                        else
                        {
                            // Querry thực hiện
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mã điều trị: " + madt + " \nvà chuyển sang phơi thanh toán ngoại trú?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (dialogResult == DialogResult.Yes)
                            {
                                try
                                {
                                    // thực thi câu lệnh update và lưu log
                                    string sqldelete = "DELETE FROM medicalrecord WHERE medicalrecordid='" + madt + "';";
                                    string sqlchuyenngt = "UPDATE vienphi SET loaivienphiid='1' WHERE vienphiid ='" + mavp + "';";
                                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa và chuyển thành phơi TT ngoại trú mã điều trị: " + madt + " của BN: " + mabn + " mã VP: " + mavp + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_05');";
                                    condb.ExecuteNonQuery_HIS(sqldelete);
                                    condb.ExecuteNonQuery_HIS(sqlchuyenngt);
                                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Xóa mã điều trị và chuyển thành phơi ngoại trú thành công!");
                                    frmthongbao.Show();
                                    // load lại dữ liệu của form
                                    gridControlBNBK.DataSource = null;
                                    btnBNBKTimKiem_Click(null, null);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Không thể thực hiện được! \nCó lỗi xảy ra", "Thông báo");
                                }
                            }
                            //else if (dialogResult == DialogResult.No)
                            //{
                            //    //do something else
                            //}
                        }

                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được. Mã điều trị đã kết thúc");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void itemXoaToanBA_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                long madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                long mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                long mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                long loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());
                string departmentgroupid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentgroupid").ToString();
                string departmentid = gridViewBNBK.GetRowCellValue(rowHandle, "departmentid").ToString();

                if (trangth == "Đang điều trị")
                {
                    int _kiemtradichvu = KiemTraTonTaiDichVu(mavp);

                    if (_kiemtradichvu == -1)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được!\nVì bệnh nhân có phát sinh dịch vụ!");
                        frmthongbao.Show();
                    }
                    else
                    {
                        if (_kiemtradichvu == 1)
                        {
                            DialogResult dialogResult_CK = MessageBox.Show("Bệnh nhân chỉ có công khám bạn có muốn tiếp tục?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (dialogResult_CK == DialogResult.No)
                            {
                                return;
                            }
                        }
                        // Querry thực hiện
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ bệnh án của mã Viện phí: " + mavp + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                // thực thi câu lệnh delete và lưu log
                                string sqldeletemedi = "DELETE FROM medicalrecord WHERE vienphiid='" + mavp + "';";
                                string sqldeletevp = "DELETE FROM vienphi WHERE vienphiid='" + mavp + "';";
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa toàn bộ bệnh án của BN: " + mabn + " mã VP: " + mavp + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_05');";
                                condb.ExecuteNonQuery_HIS(sqldeletemedi);
                                condb.ExecuteNonQuery_HIS(sqldeletevp);
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Xóa toàn bộ bệnh án thành công.\nVui lòng kiểm tra lại");
                                frmthongbao.Show();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Không thể thực hiện được! \nCó lỗi xảy ra", "Thông báo");
                            }
                        }
                        //else if (dialogResult == DialogResult.No)
                        //{
                        //    //do something else
                        //}
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được.Mã điều trị đã kết thúc");
                    frmthongbao.Show();
                }

                // load lại dữ liệu của form
                gridControlBNBK.DataSource = null;
                btnBNBKTimKiem_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private bool KiemTraTonTaiDichVu(long _vienphiid, string _departmentgroupid, string _departmentid)
        {
            bool result = false;
            try
            {
                string sqlkiemtra = "select vienphiid from serviceprice where vienphiid='" + _vienphiid + "' and departmentgroupid='" + _departmentgroupid + "' and departmentid='" + _departmentid + "' ; ";
                DataTable _datakiemtra = condb.GetDataTable_HIS(sqlkiemtra);
                if (_datakiemtra != null && _datakiemtra.Rows.Count > 0)
                { }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return result;
        }

        /// <summary>
        /// result = 0: khong co dv; =1: co dv cong kham
        /// </summary>
        /// <param name="_vienphiid"></param>
        /// <returns></returns>
        private int KiemTraTonTaiDichVu(long _vienphiid)
        {
            int result = -1;
            try
            {
                string sqlkiemtra = "select vienphiid from serviceprice where vienphiid='" + _vienphiid + "' ; ";
                DataTable _datakiemtra = condb.GetDataTable_HIS(sqlkiemtra);
                if (_datakiemtra != null && _datakiemtra.Rows.Count > 0)
                {
                    string sqlkiemtra_CK = "select vienphiid from serviceprice where vienphiid='" + _vienphiid + "' and bhyt_groupcode<> '01KB' ; ";
                    DataTable _datakiemtra_CK = condb.GetDataTable_HIS(sqlkiemtra_CK);
                    if (_datakiemtra_CK != null && _datakiemtra_CK.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return result;
        }
        #endregion

    }
}
