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
using System.IO;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaPhoiThanhToan : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucSuaPhoiThanhToan()
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
        internal void btnBNBKTimKiem_Click(object sender, EventArgs e)
        {
            string sqlquerry = "";
            string timkiemtheo = "";
            try
            {
                if (txtBNBKMaVP.Text == "Mã viện phí") // tìm theo mã BN
                {
                    timkiemtheo = " vp.patientid='" + txtBNBKMaBN.Text.Trim() + "' ";
                }
                else // tìm theo mã VP
                {
                    timkiemtheo = " vp.vienphiid='" + txtBNBKMaVP.Text.Trim() + "' ";
                }

                sqlquerry = "SELECT ser.servicepriceid as servicepriceid, ser.maubenhphamid as maubenhpham_ma, case ser.maubenhphamphieutype when 0 then '' else 'Phiếu trả' end as loaiphieu,ser.servicepricecode as dichvu_ma, ser.servicepricename as dichvu_ten, ser.soluong as soluong,ser.loaidoituong as loaihinhthanhtoan,ser.servicepricemoney as gia_yc, ser.servicepricemoney_nhandan as gia_vp, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nuocngoai as gia_nnn, ser.servicepricedate as thoigianchidinh, vp.patientid as mabn,vp.vienphiid as mavp, hsba.patientname as tenbn, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh,kcc.departmentgroupname as khoacuoicung,pcc.departmentname as phongcuoicung,vp.vienphidate as thoigianvaovien, vp.vienphidate_ravien as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai FROM serviceprice ser inner join vienphi vp on ser.vienphiid=vp.vienphiid inner join hosobenhan hsba on vp.hosobenhanid=hsba.hosobenhanid inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcd on ser.departmentgroupid=kcd.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd on ser.departmentid=pcd.departmentid inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcc on vp.departmentgroupid=kcc.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcc on vp.departmentid=pcc.departmentid WHERE " + timkiemtheo + " ORDER BY ser.servicepriceid;";

                DataView dv = new DataView(condb.GetDataTable_HIS(sqlquerry));

                if (dv.Count > 0 && dv != null)
                {
                    gridControlSuaPhoiThanhToan.DataSource = dv;
                }
                else
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
            try
            {
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string trangthai = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "trangthai").ToString();

                if (trangthai != "Đã duyệt VP")
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        e.Menu.Items.Clear();

                        DXSubMenuItem menuChuyenTT = new DXSubMenuItem("Chuyển loại hình thanh toán"); // caption menu
                        menuChuyenTT.Image = imMenu.Images[0]; // icon cho menu
                        e.Menu.Items.Add(menuChuyenTT);

                        DXMenuItem itemHaoPhi = new DXMenuItem("Hao phí giường, Công khám");
                        itemHaoPhi.Image = imMenu.Images[5];
                        menuChuyenTT.Items.Add(itemHaoPhi);
                        itemHaoPhi.Click += new EventHandler(itemHaoPhi_Click);// thêm sự kiện click

                        DXMenuItem itemBHYT = new DXMenuItem("BHYT");
                        itemBHYT.Image = imMenu.Images[2];
                        menuChuyenTT.Items.Add(itemBHYT);
                        itemBHYT.Click += new EventHandler(itemBHYT_Click);

                        DXMenuItem itemBHYTYC = new DXMenuItem("BHYT + Yêu cầu");
                        itemBHYTYC.Image = imMenu.Images[10];
                        menuChuyenTT.Items.Add(itemBHYTYC);
                        itemBHYTYC.Click += new EventHandler(itemBHYTYC_Click);

                        DXMenuItem itemVP = new DXMenuItem("Viện phí");
                        itemVP.Image = imMenu.Images[3];
                        menuChuyenTT.Items.Add(itemVP);
                        itemVP.Click += new EventHandler(itemVP_Click);

                        DXMenuItem itemYC = new DXMenuItem("Yêu cầu");
                        itemYC.Image = imMenu.Images[4];
                        menuChuyenTT.Items.Add(itemYC);
                        itemYC.Click += new EventHandler(itemYC_Click);

                        DXMenuItem itemDKPTTT = new DXMenuItem("Đi kèm PTTT");
                        itemDKPTTT.Image = imMenu.Images[7];
                        menuChuyenTT.Items.Add(itemDKPTTT);
                        itemDKPTTT.Click += new EventHandler(itemDKPTTT_Click);

                        DXMenuItem itemHPPTTT = new DXMenuItem("Hao phí PTTT");
                        itemHPPTTT.Image = imMenu.Images[9];
                        menuChuyenTT.Items.Add(itemHPPTTT);
                        itemHPPTTT.Click += new EventHandler(itemHPPTTT_Click);

                        DXMenuItem itemHPKhac = new DXMenuItem("Hao phí khác");
                        itemHPKhac.Image = imMenu.Images[8];
                        menuChuyenTT.Items.Add(itemHPKhac);
                        itemHPKhac.Click += new EventHandler(itemHPKhac_Click);

                        DXMenuItem itemSuaSoLuong = new DXMenuItem("Sửa số lượng");
                        itemSuaSoLuong.Image = imMenu.Images[3];
                        //itemXoaToanBA.Shortcut = Shortcut.F6;
                        itemSuaSoLuong.Click += new EventHandler(itemSuaSoLuong_Click);
                        e.Menu.Items.Add(itemSuaSoLuong);
                        itemSuaSoLuong.BeginGroup = true;

                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_AN_DA_THANH_TOAN);
                    frmthongbao.Show();
                }


            }
            catch
            {
            }
        }

        #region Menu event
        void itemHaoPhi_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='5' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 5 (Hao phí giường, công khám)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang Hao phí giường, công khám thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemBHYT_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='0' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 0 (BHYT)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang BHYT thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemBHYTYC_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='4' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 4 (BHYT + Yêu cầu)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang BHYT + Yêu cầu thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemVP_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='1' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 1 (Viện phí)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang Viện phí thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemYC_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='3' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 3 (Yêu cầu)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang Yêu cầu thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemDKPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='2' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 2 (Đi kèm PTTT)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang Đi kèm PTTT thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemHPPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='7' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 7 (Hao phí PTTT)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang Hao phí PTTT thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        void itemHPKhac_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewSuaPhoiThanhToan.FocusedRowHandle;
                string loaitt_old = gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "loaihinhthanhtoan").ToString();
                int servicepriceid = Convert.ToInt32(gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET loaidoituong='9' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Chuyển loại hình thanh toán của servicepriceid=" + servicepriceid + " từ " + loaitt_old + " sang 9 (Hao phí khác)','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_11');";
                condb.ExecuteNonQuery_HIS(sqlxecute);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chuyển loại hình thanh toán sang Hao phí khác thành công!");
                frmthongbao.Show();
                // load lại dữ liệu của form
                gridControlSuaPhoiThanhToan.DataSource = null;
                btnBNBKTimKiem_Click(null, null);

            }
            catch (Exception)
            {

            }
        }

        // Sua so luong
        void itemSuaSoLuong_Click(object sender, EventArgs e)
        {
            try
            {
                MedicalLink.ChucNang.SuaPhoiThanhToan_SoLuong frmSoLuong = new ChucNang.SuaPhoiThanhToan_SoLuong(this);
                frmSoLuong.ShowDialog();
            }
            catch (Exception)
            {

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

        #region Export data
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewSuaPhoiThanhToan.RowCount > 0)
                {
                    try
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                            if (saveDialog.ShowDialog() != DialogResult.Cancel)
                            {
                                string exportFilePath = saveDialog.FileName;
                                string fileExtenstion = new FileInfo(exportFilePath).Extension;

                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        gridViewSuaPhoiThanhToan.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewSuaPhoiThanhToan.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewSuaPhoiThanhToan.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewSuaPhoiThanhToan.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewSuaPhoiThanhToan.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewSuaPhoiThanhToan.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        private void gridViewSuaPhoiThanhToan_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

            }
        }

        private void gridViewSuaPhoiThanhToan_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                if (e.IsGetData)
                {
                    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                    if (e.Column.FieldName == "TTTT")
                    {
                        int loaihinhtt_text = Convert.ToInt16(view.GetRowCellValue(e.ListSourceRowIndex, "loaihinhthanhtoan").ToString());
                        if (loaihinhtt_text == 0)
                            e.Value = "BHYT";
                        else if (loaihinhtt_text == 1)
                            e.Value = "Viện phí";
                        else if (loaihinhtt_text == 2)
                            e.Value = "Đi kèm";
                        else if (loaihinhtt_text == 3)
                            e.Value = "Yêu cầu";
                        else if (loaihinhtt_text == 4)
                            e.Value = "BHYT + Yêu cầu";
                        else if (loaihinhtt_text == 5)
                            e.Value = "Hao phí giường, Công khám";
                        else if (loaihinhtt_text == 6)
                            e.Value = "BHYT + Phụ thu";
                        else if (loaihinhtt_text == 7)
                            e.Value = "Hao phí PTTT";
                        else if (loaihinhtt_text == 8)
                            e.Value = "Đối tượng khác";
                        else if (loaihinhtt_text == 9)
                            e.Value = "Hao phí khác";
                    }

                }
            }
            catch (Exception)
            {
            }
        }




    }
}
