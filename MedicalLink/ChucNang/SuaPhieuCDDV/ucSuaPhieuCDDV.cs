using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaPhieuCDDV : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucSuaPhieuCDDV()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
            mmeMaPhieuYC.Text = "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)";
            this.mmeMaPhieuYC.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaPhieuYC.Enter += new System.EventHandler(this.mmeMaDV_Enter);
            // Hiển thị Text Hint Mã viện phí
            txtMaVP.ForeColor = SystemColors.GrayText;
            txtMaVP.Text = "Mã viện phí";
            this.txtMaVP.Leave += new System.EventHandler(this.txtMaVP_Leave);
            this.txtMaVP.Enter += new System.EventHandler(this.txtMaVP_Enter);
            // Hiển thị Text Hint Mã BN
            txtMaBN.ForeColor = SystemColors.GrayText;
            txtMaBN.Text = "Mã bệnh nhân";
            this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
            this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
        }

        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaPhieuYC.Text.Length == 0)
            {
                mmeMaPhieuYC.Text = "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)";
                mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaPhieuYC.Text == "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)")
            {
                mmeMaPhieuYC.Text = "";
                mmeMaPhieuYC.ForeColor = SystemColors.WindowText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtMaVP_Leave(object sender, EventArgs e)
        {
            if (txtMaVP.Text.Length == 0)
            {
                txtMaVP.Text = "Mã viện phí";
                txtMaVP.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtMaVP_Enter(object sender, EventArgs e)
        {
            if (txtMaVP.Text == "Mã viện phí")
            {
                txtMaVP.Text = "";
                txtMaVP.ForeColor = SystemColors.WindowText;
            }
        }

        // Hiển thị Text Hint Mã bệnh nhân
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            if (txtMaBN.Text.Length == 0)
            {
                txtMaBN.Text = "Mã bệnh nhân";
                txtMaBN.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã bệnh nhân
        private void txtMaBN_Enter(object sender, EventArgs e)
        {
            if (txtMaBN.Text == "Mã bệnh nhân")
            {
                txtMaBN.Text = "";
                txtMaBN.ForeColor = SystemColors.WindowText;
            }
        }

        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            gridControlDS_PhieuDichVu.DataSource = null;
            gridControlChiTiet.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string[] dsdv_temp;
            string dsphieu = "";
            string sqlquerry = "";

            if ((mmeMaPhieuYC.Text == "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)") && (txtMaBN.Text == "Mã bệnh nhân") && (txtMaVP.Text == "Mã viện phí"))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo!");
            }
            // Tìm kiếm theo mã phiếu thuốc/vt
            else if (mmeMaPhieuYC.Text != "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)")
            {
                try
                {
                    // Lấy dữ liệu danh sách dịch vụ nhập vào
                    dsdv_temp = mmeMaPhieuYC.Text.Split(',');
                    for (int i = 0; i < dsdv_temp.Length - 1; i++)
                    {
                        dsphieu += "'" + dsdv_temp[i].ToString() + "',";
                    }
                    dsphieu += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString() + "'";

                    sqlquerry = "SELECT maubenhpham.maubenhphamid as maubenhphamid,maubenhpham.medicalrecordid as medicalrecordid,maubenhpham.patientid as patientid,maubenhpham.vienphiid as vienphiid,hosobenhan.patientname as tenbenhnhan,maubenhpham.maubenhphamtype as maubenhphamtype,maubenhpham.maubenhphamgrouptype as maubenhphamgrouptype,maubenhpham.maubenhphamstatus as maubenhphamstatus,maubenhpham.maubenhphamdate as thoigianchidinh,maubenhpham.maubenhphamdate_sudung as thoigiansudung,maubenhpham.dathutien as dathutien,tools_depatment.departmentgroupname as khoachidinh,tools_depatment.departmentname as phongchidinh,maubenhpham.isdeleted as isdeleted, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai FROM maubenhpham,hosobenhan,vienphi,tools_depatment WHERE vienphi.hosobenhanid = hosobenhan.hosobenhanid and tools_depatment.departmentid = maubenhpham.departmentid and maubenhpham.hosobenhanid = hosobenhan.hosobenhanid and maubenhpham.maubenhphamid in(" + dsphieu + ") ORDER BY maubenhpham.maubenhphamid;";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra" + ex.ToString(), "Thông báo !");
                }

            }
            // Tìm kiếm theo mã viện phí
            else if (txtMaVP.Text != "Mã viện phí")
            {
                sqlquerry = "SELECT maubenhpham.maubenhphamid as maubenhphamid,maubenhpham.medicalrecordid as medicalrecordid,maubenhpham.patientid as patientid,maubenhpham.vienphiid as vienphiid,hosobenhan.patientname as tenbenhnhan,maubenhpham.maubenhphamtype as maubenhphamtype,maubenhpham.maubenhphamgrouptype as maubenhphamgrouptype,maubenhpham.maubenhphamstatus as maubenhphamstatus,maubenhpham.maubenhphamdate as thoigianchidinh,maubenhpham.maubenhphamdate_sudung as thoigiansudung,maubenhpham.dathutien as dathutien,tools_depatment.departmentgroupname as khoachidinh,tools_depatment.departmentname as phongchidinh,maubenhpham.isdeleted as isdeleted, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai FROM maubenhpham,hosobenhan,vienphi,tools_depatment WHERE vienphi.hosobenhanid = hosobenhan.hosobenhanid and tools_depatment.departmentid = maubenhpham.departmentid and maubenhpham.hosobenhanid = hosobenhan.hosobenhanid and maubenhpham.vienphiid = " + txtMaVP.Text.Trim() + " ORDER BY maubenhpham.maubenhphamid; ";

            }
            // Tìm kiếm theo mã bệnh nhân
            else if (txtMaBN.Text != "Mã bệnh nhân")
            {
                sqlquerry = "SELECT maubenhpham.maubenhphamid as maubenhphamid,maubenhpham.medicalrecordid as medicalrecordid,maubenhpham.patientid as patientid,maubenhpham.vienphiid as vienphiid,hosobenhan.patientname as tenbenhnhan,maubenhpham.maubenhphamtype as maubenhphamtype,maubenhpham.maubenhphamgrouptype as maubenhphamgrouptype,maubenhpham.maubenhphamstatus as maubenhphamstatus,maubenhpham.maubenhphamdate as thoigianchidinh,maubenhpham.maubenhphamdate_sudung as thoigiansudung,maubenhpham.dathutien as dathutien,tools_depatment.departmentgroupname as khoachidinh,tools_depatment.departmentname as phongchidinh,maubenhpham.isdeleted as isdeleted, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai FROM maubenhpham,hosobenhan,vienphi,tools_depatment WHERE vienphi.hosobenhanid = hosobenhan.hosobenhanid and tools_depatment.departmentid = maubenhpham.departmentid and maubenhpham.hosobenhanid = hosobenhan.hosobenhanid and maubenhpham.patientid = " + txtMaBN.Text.Trim() + " ORDER BY maubenhpham.maubenhphamid; ";
            }

            try
            {
                DataView dv = new DataView(condb.getDataTable(sqlquerry));
                gridControlDS_PhieuDichVu.DataSource = dv;

                if (gridViewDS_PhieuDichVu.RowCount == 0)
                {
                    MessageBox.Show("Không tìm thấy hồ sơ nào như yêu cầu \n             Vui lòng kiểm tra lại.", "Thông báo!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra" + ex.ToString(), "Thông báo !");
            }

            SplashScreenManager.CloseForm();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDS_PhieuDichVu.RowCount > 0)
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
                                    gridControlDS_PhieuDichVu.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDS_PhieuDichVu.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDS_PhieuDichVu.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDS_PhieuDichVu.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDS_PhieuDichVu.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDS_PhieuDichVu.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            MessageBox.Show("Export dữ liệu thành công!", "Thông báo!");
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
                MessageBox.Show("Không có dữ liệu!", "Thông báo!");
            }

        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
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

        private void txtMaVP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
            }
        }

        private void txtMaVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMaVP_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaVP.Text != "Mã viện phí")
            {
                // Hiển thị Text Hint Mã BN
                txtMaBN.ForeColor = SystemColors.GrayText;
                mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
                txtMaBN.Text = "Mã bệnh nhân";
                mmeMaPhieuYC.Text = "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)";
                this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
                this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
                this.mmeMaPhieuYC.Leave += new System.EventHandler(this.mmeMaDV_Leave);
                this.mmeMaPhieuYC.Enter += new System.EventHandler(this.mmeMaDV_Enter);
            }
        }

        private void txtMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMaBN.Text != "Mã bệnh nhân")
            {
                // Hiển thị Text Hint Mã BN
                txtMaVP.ForeColor = SystemColors.GrayText;
                mmeMaPhieuYC.ForeColor = SystemColors.GrayText;
                txtMaVP.Text = "Mã viện phí";
                mmeMaPhieuYC.Text = "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)";
                this.txtMaVP.Leave += new System.EventHandler(this.txtMaVP_Leave);
                this.txtMaVP.Enter += new System.EventHandler(this.txtMaVP_Enter);
                this.mmeMaPhieuYC.Leave += new System.EventHandler(this.mmeMaDV_Leave);
                this.mmeMaPhieuYC.Enter += new System.EventHandler(this.mmeMaDV_Enter);
            }
        }

        private void mmeMaPhieuYC_EditValueChanged(object sender, EventArgs e)
        {
            if (mmeMaPhieuYC.Text != "")
            {
                // Hiển thị Text Hint Mã BN
                txtMaVP.ForeColor = SystemColors.GrayText;
                txtMaBN.ForeColor = SystemColors.GrayText;
                txtMaVP.Text = "Mã viện phí";
                txtMaBN.Text = "Mã bệnh nhân";
                this.txtMaVP.Leave += new System.EventHandler(this.txtMaVP_Leave);
                this.txtMaVP.Enter += new System.EventHandler(this.txtMaVP_Enter);
                this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
                this.txtMaBN.Enter += new System.EventHandler(this.txtMaBN_Enter);
            }
        }


        // Chỉ cho nhập số và ký từ điểu khiển và dấu phẩy.
        private void mmeMaPhieuYC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void gridViewDS_PhieuDichVu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void gridViewChiTiet_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt_ct)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

            }
        }

        private void gridViewDS_PhieuDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridViewChiTiet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridControlDS_PhieuDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                string maubenhphamid = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString();
                string sql_serviceprice = "SELECT servicepriceid, maubenhphamid, servicepricecode, servicepricename, soluongbacsi, soluong, servicepricemoney, servicepricemoney_bhyt, servicepricemoney_nhandan, servicepricemoney_nuocngoai FROM serviceprice WHERE maubenhphamid ='" + maubenhphamid + "'; ";
                DataView dv_ct = new DataView(condb.getDataTable(sql_serviceprice));
                gridControlChiTiet.DataSource = dv_ct;
            }
            catch (Exception)
            {

            }
        }

        private void gridViewDS_PhieuDichVu_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
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

        private void gridViewDS_PhieuDichVu_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                //var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                //string trangthai = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "trangthai").ToString();

                //if (trangthai != "Đã duyệt VP")
                //{
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();

                    //DXSubMenuItem menuChuyenTT = new DXSubMenuItem("Xóa phiếu chỉ định"); // caption menu
                    //menuChuyenTT.Image = imMenu.Images[0]; // icon cho menu
                    //e.Menu.Items.Add(menuChuyenTT);

                    //DXMenuItem itemHaoPhi = new DXMenuItem("Hao phí giường, Công khám");
                    //itemHaoPhi.Image = imMenu.Images[5];
                    //menuChuyenTT.Items.Add(itemHaoPhi);
                    //itemHaoPhi.Click += new EventHandler(itemHaoPhi_Click);// thêm sự kiện click

                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Xóa phiếu chỉ định");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[1];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(itemXoaPhieuChiDinh_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);

                    DXMenuItem itemSuaThoiGian = new DXMenuItem("Sửa thời gian chỉ định/sử dụng");
                    itemSuaThoiGian.Image = imMenu.Images[4];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemSuaThoiGian.Click += new EventHandler(itemSuaThoiGian_Click);
                    e.Menu.Items.Add(itemSuaThoiGian);
                    //itemXoaPhieuChiDinh.BeginGroup = true;

                }
                //}
                //else
                //{
                //    MessageBox.Show("Bệnh nhân đã thanh toán, không thể sửa được.", "Thông báo!");
                //}

            }
            catch
            {
            }
        }

        void itemXoaPhieuChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                string maubenhphamid = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString();
                // Querry thực hiện
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu mã: " + maubenhphamid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    // Lấy thời gian
                    String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    // thực thi câu lệnh update và lưu log
                    string sqlxecute_mbp = "DELETE FROM maubenhpham WHERE maubenhphamid='" + maubenhphamid + "';";
                    string sqlexcute_ser = "DELETE FROM serviceprice WHERE maubenhphamid='" + maubenhphamid + "';";
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa phiếu và dịch vụ mã: " + maubenhphamid + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlxecute_mbp);
                    condb.ExecuteNonQuery(sqlexcute_ser);
                    condb.ExecuteNonQuery(sqlinsert_log);
                    MessageBox.Show("Xóa phiếu dịch vụ mã: " + maubenhphamid + " thành công.\nVui lòng kiểm tra lại", "Thông báo!");
                    // load lại dữ liệu của form
                    gridControlDS_PhieuDichVu.DataSource = null;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception)
            {

            }
        }
        //Sua thoi gian chi dinh dich vu
        void itemSuaThoiGian_Click(object sender, EventArgs e)
        {
            try
            {
                if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_01"))
                {
                    // lấy giá trị tại dòng click chuột
                    var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                    string trangthai = Convert.ToString(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "trangthai").ToString());
                    long maubenhphamid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                    DateTime thoigianchidinh = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "thoigianchidinh").ToString());
                    DateTime thoigiansudung = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "thoigiansudung").ToString());
                    if (trangthai == "Đang điều trị")
                    {
                        //truyền biến sang bên form thực hiện
                        MedicalLink.ChucNang.XyLyMauBenhPham.frmSuaThoiGianChiDinh frmsuaTGCD = new MedicalLink.ChucNang.XyLyMauBenhPham.frmSuaThoiGianChiDinh(maubenhphamid, thoigianchidinh, thoigiansudung);
                        frmsuaTGCD.ShowDialog();
                        gridControlDS_PhieuDichVu.DataSource = null;
                        btnTimKiem_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Hồ sơ bệnh án đã đóng. \nKhông cho phép sửa", "Thông báo!");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền sử dụng chức năng này", "Thông báo!");
                }

            }
            catch (Exception)
            {

            }
        }


    }
}
