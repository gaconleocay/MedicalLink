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

        // Hiển thị danh sách mã điều trị của BN
        private void btnBNBKTimKiem_Click(object sender, EventArgs e)
        {
            string sqlquerry;
            try
            {
                if (txtBNBKMaVP.Text == "Mã viện phí") // tìm theo mã BN
                {
                    sqlquerry = "SELECT distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then 'Kết thúc' else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, CASE medicalrecord.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where medicalrecord.departmentid=department.departmentid) END as tenphong, medicalrecord.loaibenhanid as loaibenhan FROM medicalrecord, hosobenhan,departmentgroup,department WHERE medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.hosobenhanid=hosobenhan.hosobenhanid and medicalrecord.patientid=" + txtBNBKMaBN.Text + " ORDER BY madieutri;";
                }
                else // tìm theo mã VP
                {
                    sqlquerry = "SELECT distinct medicalrecord.medicalrecordid as madieutri, medicalrecord.patientid as mabenhnhan, medicalrecord.vienphiid as mavienphi, hosobenhan.patientname as tenbenhnhan, case medicalrecord.medicalrecordstatus when 99 then 'Kết thúc' else 'Đang điều trị' end as trangthai, medicalrecord.thoigianvaovien as thoigianvaovien, medicalrecord.thoigianravien as thoigianravien, departmentgroup.departmentgroupname as tenkhoa, CASE medicalrecord.departmentid WHEN '0' THEN 'Hành chính' ELSE (select department.departmentname from department where medicalrecord.departmentid=department.departmentid) END as tenphong, medicalrecord.loaibenhanid as loaibenhan FROM medicalrecord, hosobenhan,departmentgroup,department WHERE medicalrecord.departmentgroupid=departmentgroup.departmentgroupid and medicalrecord.hosobenhanid=hosobenhan.hosobenhanid and medicalrecord.vienphiid=" + txtBNBKMaVP.Text + " ORDER BY madieutri;";
                }

                DataView dv = new DataView(condb.GetDataTable(sqlquerry));
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

        // Tạo menu chức năng
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

        #region Chuyển mã ĐT ra phòng hành chính
        void itemChuyenHanhChinh_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                int madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                int mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                int mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                int codvnt = 0; //Convert.ToInt16(gridViewBNBK.GetRowCellValue(rowHandle, "codvnoitru").ToString());
                int loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());
                string tenph = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "tenphong").ToString());

                if (trangth == "Đang điều trị")
                {
                    if (codvnt == 1)
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
                                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển BN: " + mabn + " mã VP: " + mavp + " mã điều trị: " + madt + " ra phòng hành chính','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                                        condb.ExecuteNonQuery(sqlxecute);
                                        condb.ExecuteNonQuery(sqlinsert_log);
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
        #endregion

        #region Xóa mã điều trị nội trú
        void itemXoaMaDT_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                int madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                int mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                int mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                int codvnt = 0; //Convert.ToInt16(gridViewBNBK.GetRowCellValue(rowHandle, "codvnoitru").ToString());
                int loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());

                if (trangth == "Đang điều trị")
                {
                    if (codvnt == 1)
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
                                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa mã điều trị: " + madt + " của BN: " + mabn + " mã VP: " + mavp + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                                    condb.ExecuteNonQuery(sqlxecute);
                                    condb.ExecuteNonQuery(sqlinsert_log);
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
        #endregion

        #region Xóa mã điều trị + Chuyển TT ngoại trú
        void itemXoaMaDTCNT_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                int madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                int mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                int mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                int codvnt = 0; //Convert.ToInt16(gridViewBNBK.GetRowCellValue(rowHandle, "codvnoitru").ToString());
                int loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());

                if (trangth == "Đang điều trị")
                {
                    if (codvnt == 1)
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
                                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa và chuyển thành phơi TT ngoại trú mã điều trị: " + madt + " của BN: " + mabn + " mã VP: " + mavp + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                                    condb.ExecuteNonQuery(sqldelete);
                                    condb.ExecuteNonQuery(sqlchuyenngt);
                                    condb.ExecuteNonQuery(sqlinsert_log);
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
        #endregion

        #region Xóa toàn bộ bệnh án
        void itemXoaToanBA_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewBNBK.FocusedRowHandle;
                int madt = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "madieutri").ToString());
                int mabn = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mabenhnhan").ToString());
                int mavp = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "mavienphi").ToString());
                string trangth = Convert.ToString(gridViewBNBK.GetRowCellValue(rowHandle, "trangthai").ToString());
                int codvnt = 0; //Convert.ToInt16(gridViewBNBK.GetRowCellValue(rowHandle, "codvnoitru").ToString());
                int loaiba = Convert.ToInt32(gridViewBNBK.GetRowCellValue(rowHandle, "loaibenhan").ToString());

                if (trangth == "Đang điều trị")
                {
                    if (codvnt == 1)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Không thể thực hiện được!\nVì có dịch vụ phát sinh trong buồng điều trị!");
                        frmthongbao.Show();
                    }
                    else
                    {
                        // Querry thực hiện
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ bệnh án của mã Viện phí: " + mavp + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                // thực thi câu lệnh delete và lưu log
                                string sqldeletemedi = "DELETE FROM medicalrecord WHERE vienphiid='" + mavp + "';";
                                string sqldeletevp = "DELETE FROM vienphi WHERE vienphiid='" + mavp + "';";
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa toàn bộ bệnh án của BN: " + mabn + " mã VP: " + mavp + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                                condb.ExecuteNonQuery(sqldeletemedi);
                                condb.ExecuteNonQuery(sqldeletevp);
                                condb.ExecuteNonQuery(sqlinsert_log);
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
        #endregion

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
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
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

        private void ucXuLyBNBoKhoa_Load(object sender, EventArgs e)
        {
            txtBNBKMaBN.Focus();
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

    }
}
