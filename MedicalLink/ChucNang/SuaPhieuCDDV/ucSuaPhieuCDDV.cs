﻿using System;
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

            string maubenhphamgrouptype = "";
            if (chkPhieuDichVu.Checked && chkPhieuThuocVT.Checked == false)
            {
                maubenhphamgrouptype = " and maubenhphamgrouptype in (0,1,2,4) ";
            }
            else if (chkPhieuDichVu.Checked == false && chkPhieuThuocVT.Checked)
            {
                maubenhphamgrouptype = " and maubenhphamgrouptype in (5,6) ";
            }
            else
            {
                maubenhphamgrouptype = " and maubenhphamgrouptype<>3 ";
            }

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

                    sqlquerry = "SELECT mbp.maubenhphamid, mbp.medicalrecordid, hsba.patientid, vp.vienphiid, hsba.patientname, mbp.maubenhphamtype, (case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, (case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung,(case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, mbp.dathutien as dathutienid, de.departmentgroupname, de.departmentname, mbp.isdeleted, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai,(case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien, COALESCE(vp.vienphistatus_vp,0) as vienphistatus_vp,medicinestorebillid,(case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype, mbp.maubenhphamphieutype as maubenhphamphieutypeid FROM maubenhpham mbp inner join hosobenhan hsba on mbp.hosobenhanid=hsba.hosobenhanid inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid inner join tools_depatment de on de.departmentid=mbp.departmentid WHERE mbp.maubenhphamid in(" + dsphieu + ") ORDER BY mbp.maubenhphamgrouptype, mbp.maubenhphamid;";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra" + ex.ToString(), "Thông báo !");
                }

            }
            // Tìm kiếm theo mã viện phí
            else if (txtMaVP.Text != "Mã viện phí")
            {
                sqlquerry = "SELECT mbp.maubenhphamid, mbp.medicalrecordid, hsba.patientid, vp.vienphiid, hsba.patientname, mbp.maubenhphamtype, (case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, (case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, (case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, mbp.dathutien as dathutienid, de.departmentgroupname, de.departmentname, mbp.isdeleted, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai,(case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien, COALESCE(vp.vienphistatus_vp,0) as vienphistatus_vp,medicinestorebillid,(case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype, mbp.maubenhphamphieutype as maubenhphamphieutypeid FROM maubenhpham mbp inner join hosobenhan hsba on mbp.hosobenhanid=hsba.hosobenhanid inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid inner join tools_depatment de on de.departmentid=mbp.departmentid WHERE vp.vienphiid = " + txtMaVP.Text.Trim() + " and mbp.maubenhphamgrouptype <>3 " + maubenhphamgrouptype + " ORDER BY mbp.maubenhphamgrouptype, mbp.maubenhphamid;";

            }
            // Tìm kiếm theo mã bệnh nhân
            else if (txtMaBN.Text != "Mã bệnh nhân")
            {
                sqlquerry = "SELECT mbp.maubenhphamid, mbp.medicalrecordid, hsba.patientid, vp.vienphiid, hsba.patientname, mbp.maubenhphamtype, (case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, (case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, (case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, mbp.dathutien as dathutienid, de.departmentgroupname, de.departmentname, mbp.isdeleted, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai,(case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien,COALESCE(vp.vienphistatus_vp,0) as vienphistatus_vp,medicinestorebillid,(case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype, mbp.maubenhphamphieutype as maubenhphamphieutypeid FROM maubenhpham mbp inner join hosobenhan hsba on mbp.hosobenhanid=hsba.hosobenhanid inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid inner join tools_depatment de on de.departmentid=mbp.departmentid WHERE vp.patientid = " + txtMaBN.Text.Trim() + " and mbp.maubenhphamgrouptype <>3 " + maubenhphamgrouptype + " ORDER BY mbp.maubenhphamgrouptype, mbp.maubenhphamid;";
            }

            try
            {
                DataView dv = new DataView(condb.getDataTable(sqlquerry));
                gridControlDS_PhieuDichVu.DataSource = dv;

                if (gridViewDS_PhieuDichVu.RowCount == 0)
                {
                    MessageBox.Show("Không tìm thấy hồ sơ nào như yêu cầu \n             Vui lòng kiểm tra lại.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
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
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        void itemXoaPhieuChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                long maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                long dathutienid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "dathutienid").ToString());
                long vienphistatus_vp = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "vienphistatus_vp").ToString());
                long medicinestorebillid_ex = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "medicinestorebillid").ToString());//medicinestorebillid_ex
                long maubenhphamphieutypeid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamphieutypeid").ToString()); //phieu tra
                string maubenhphamstatus = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamstatus").ToString();
                string maubenhphamgrouptype = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamgrouptype").ToString();

                if (dathutienid == 0 && vienphistatus_vp == 0)
                {
                    if (maubenhphamgrouptype == "Thuốc" || maubenhphamgrouptype == "Vật tư")
                    {
                        if (maubenhphamstatus == "Đã xuất tủ trực" && medicinestorebillid_ex != 0)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu mã: " + maubenhphamid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (dialogResult == DialogResult.Yes)
                            {
                                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                string delete_maubenhpham = "DELETE FROM maubenhpham WHERE maubenhphamid='" + maubenhphamid + "';";
                                string delete_serviceprice = "DELETE FROM serviceprice WHERE maubenhphamid='" + maubenhphamid + "';";
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa phiếu và thuốc/vật tư mã: " + maubenhphamid + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                                string update_medicine_store_bill = "UPDATE medicine_store_bill SET isremove=1 WHERE maubenhphamid='" + maubenhphamid + "';";

                                if (maubenhphamphieutypeid == 0) //phieu chi dinh
                                {
                                    //Lay so luong thuoc da xuat
                                    string laysoluongthuoc = "SELECT me.medicinestorerefid, me.medicinestorebillid, me.medicinestorebillcode, me.accept_soluong FROM medicine me WHERE me.medicinestorebillid=" + medicinestorebillid_ex + "; ";
                                    DataView listsoluongthuoc = new DataView(condb.getDataTable(laysoluongthuoc));
                                    if (listsoluongthuoc != null && listsoluongthuoc.Count > 0)
                                    {
                                        for (int i = 0; i < listsoluongthuoc.Count; i++)
                                        {
                                            string update_medicine_store_ref = "UPDATE medicine_store_ref SET soluongtonkho=soluongtonkho + " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + ", soluongkhadung=soluongkhadung + " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + " WHERE medicinestorerefid= '" + listsoluongthuoc[i]["medicinestorerefid"].ToString() + "';";
                                            condb.ExecuteNonQuery(update_medicine_store_ref);
                                        }
                                    }
                                }
                                else //phieu tra
                                {
                                    //Lay so luong thuoc da xuat
                                    string laysoluongthuoc = "SELECT me.medicinestorerefid, me.medicinestorebillid, me.medicinestorebillcode, me.accept_soluong FROM medicine me WHERE me.medicinestorebillid=" + medicinestorebillid_ex + "; ";
                                    DataView listsoluongthuoc = new DataView(condb.getDataTable(laysoluongthuoc));
                                    if (listsoluongthuoc != null && listsoluongthuoc.Count > 0)
                                    {
                                        for (int i = 0; i < listsoluongthuoc.Count; i++)
                                        {
                                            string update_medicine_store_ref = "UPDATE medicine_store_ref SET soluongtonkho=soluongtonkho - " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + ", soluongkhadung=soluongkhadung - " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + " WHERE medicinestorerefid= '" + listsoluongthuoc[i]["medicinestorerefid"].ToString() + "';";
                                            condb.ExecuteNonQuery(update_medicine_store_ref);
                                        }
                                    }
                                }
                                string delete_medicine = "DELETE FROM medicine WHERE medicinestorebillid in (select medicinestorebillid from medicine_store_bill where maubenhphamid=" + maubenhphamid + ");";
                                //-------                    
                                condb.ExecuteNonQuery(delete_medicine);
                                condb.ExecuteNonQuery(delete_maubenhpham);
                                condb.ExecuteNonQuery(delete_serviceprice);
                                condb.ExecuteNonQuery(update_medicine_store_bill);
                                condb.ExecuteNonQuery(sqlinsert_log);
                                MessageBox.Show("Xóa phiếu thuốc/vật tư mã: " + maubenhphamid + " thành công.\nVui lòng kiểm tra lại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                gridControlDS_PhieuDichVu.DataSource = null;
                                btnTimKiem_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Thực hiện thất bại. \nPhiếu thuốc/vật tư không kê từ tủ trực hoặc chưa được xuất.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu mã: " + maubenhphamid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (dialogResult == DialogResult.Yes)
                        {
                            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            // thực thi câu lệnh update và lưu log
                            string sqlxecute_mbp = "DELETE FROM maubenhpham WHERE maubenhphamid='" + maubenhphamid + "';";
                            string sqlexcute_ser = "DELETE FROM serviceprice WHERE maubenhphamid='" + maubenhphamid + "';";
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa phiếu và dịch vụ mã: " + maubenhphamid + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlxecute_mbp);
                            condb.ExecuteNonQuery(sqlexcute_ser);
                            condb.ExecuteNonQuery(sqlinsert_log);
                            MessageBox.Show("Xóa phiếu dịch vụ mã: " + maubenhphamid + " thành công.\nVui lòng kiểm tra lại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            gridControlDS_PhieuDichVu.DataSource = null;
                            btnTimKiem_Click(null, null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Thực hiện thất bại. \nPhiếu dịch vụ đã thu tiền hoặc bệnh án đã được duyệt viện phí.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực hiện thất bại. Có lỗi xảy ra.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MedicalLink.Base.Logging.Error(ex);
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
                    DateTime thoigianchidinh = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamdate").ToString());
                    DateTime thoigiansudung = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamdate_sudung").ToString());
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
                        MessageBox.Show("Hồ sơ bệnh án đã đóng. \nKhông cho phép sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn không có quyền sử dụng chức năng này", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void repositoryItemButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                itemXoaPhieuChiDinh_Click(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
