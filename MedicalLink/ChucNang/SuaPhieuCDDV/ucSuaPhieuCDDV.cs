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
using MedicalLink.ClassCommon;
using MedicalLink.ChucNang.XyLyMauBenhPham;

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

        #region Custom
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

        #endregion

        #region Tim kiem
        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                gridControlDS_PhieuDichVu.DataSource = null;
                gridControlChiTiet.DataSource = null;
                string[] dsdv_temp;
                string dsphieu = "";
                string sqlquerry = "";
                string timkiemtheo = "";
                string maubenhphamgrouptype = "";
                if (chkPhieuDichVu.Checked && chkPhieuThuocVT.Checked == false)
                {
                    maubenhphamgrouptype = " and mbp.maubenhphamgrouptype in (0,1,2,4) ";
                }
                else if (chkPhieuDichVu.Checked == false && chkPhieuThuocVT.Checked)
                {
                    maubenhphamgrouptype = " and mbp.maubenhphamgrouptype in (5,6) ";
                }
                else
                {
                    maubenhphamgrouptype = " and mbp.maubenhphamgrouptype<>3 ";
                }

                if ((mmeMaPhieuYC.Text == "Nhập mã phiếu dịch vụ/thuốc/VT cách nhau bởi dấu phẩy (,)") && (txtMaBN.Text == "Mã bệnh nhân") && (txtMaVP.Text == "Mã viện phí"))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
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

                        timkiemtheo = " mbp.maubenhphamid in(" + dsphieu + ") ";
                    }
                    catch (Exception ex)
                    {
                        Base.Logging.Warn(ex);
                    }
                }
                // Tìm kiếm theo mã viện phí
                else if (txtMaVP.Text != "Mã viện phí")
                {
                    timkiemtheo = " vp.vienphiid = " + txtMaVP.Text.Trim() + maubenhphamgrouptype + " ";
                }
                // Tìm kiếm theo mã bệnh nhân
                else if (txtMaBN.Text != "Mã bệnh nhân")
                {
                    timkiemtheo = " vp.patientid = " + txtMaBN.Text.Trim() + maubenhphamgrouptype + " ";
                }

                sqlquerry = "SELECT mbp.maubenhphamid, mbp.medicalrecordid, hsba.patientid, vp.vienphiid, hsba.patientname, mbp.maubenhphamtype, mbp.phieudieutriid, mbp.maubenhphamgrouptype as maubenhphamgrouptypeid, (case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, (case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamfinishdate, (case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, mbp.dathutien as dathutienid, kcd.departmentgroupname, pcd.departmentname, mbp.isdeleted, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai, (case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien, COALESCE(vp.vienphistatus_vp,0) as vienphistatus_vp, mbp.medicinestorebillid, (case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype, mbp.maubenhphamphieutype as maubenhphamphieutypeid FROM maubenhpham mbp INNER JOIN hosobenhan hsba on mbp.hosobenhanid=hsba.hosobenhanid INNER JOIN vienphi vp on vp.hosobenhanid=hsba.hosobenhanid INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=mbp.departmentid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=mbp.departmentgroupid WHERE " + timkiemtheo + " ORDER BY mbp.maubenhphamgrouptype,mbp.maubenhphamid; ";

                DataView dv = new DataView(condb.GetDataTable_HIS(sqlquerry));

                if (dv.Count > 0 && dv != null)
                {
                    gridControlDS_PhieuDichVu.DataSource = dv;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Xuat excel
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

        #endregion

        #region Custom Comtrol
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
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void gridViewChiTiet_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void gridControlDS_PhieuDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                string sql_serviceprice = "";
                var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                string maubenhphamid = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString();
                long maubenhphamgrouptypeid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamgrouptypeid").ToString());

                if (maubenhphamgrouptypeid == 4) //pttt
                {
                    sql_serviceprice = "SELECT ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode, ser.servicepricename, ser.soluongbacsi, ser.soluong, ser.servicepricemoney, ser.servicepricemoney_bhyt, ser.servicepricemoney_nhandan, ser.servicepricemoney_nuocngoai, pttt.phauthuatthuthuatdate, pttt.phauthuatthuthuatdate_ketthuc as thoigiantraketqua, '4' as maubenhphamgrouptypeid FROM serviceprice ser left join phauthuatthuthuat pttt on pttt.servicepriceid=ser.servicepriceid WHERE ser.maubenhphamid ='" + maubenhphamid + "'; ";
                }
                else
                {
                    sql_serviceprice = " SELECT ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode, ser.servicepricename, ser.soluongbacsi, ser.soluong, ser.servicepricemoney, ser.servicepricemoney_bhyt, ser.servicepricemoney_nhandan, ser.servicepricemoney_nuocngoai, '' as phauthuatthuthuatdate, se.servicetimetrakq as thoigiantraketqua, '1' as maubenhphamgrouptypeid FROM serviceprice ser left join service se on se.servicepriceid=ser.servicepriceid WHERE ser.maubenhphamid ='" + maubenhphamid + "' GROUP BY ser.servicepriceid,ser.maubenhphamid,ser.servicepricecode,ser.servicepricename,ser.soluongbacsi,ser.soluong,ser.servicepricemoney,ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney_nuocngoai,se.servicetimetrakq;";
                }
                DataView dv_ct = new DataView(condb.GetDataTable_HIS(sql_serviceprice));
                gridControlChiTiet.DataSource = dv_ct;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Events
        private void gridViewDS_PhieuDichVu_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_01"))
                    {
                        DXMenuItem itemSuaThoiGian = new DXMenuItem("Sửa thời gian chỉ định/sử dụng/trả kết quả");
                        itemSuaThoiGian.Image = imMenu.Images[4];
                        itemSuaThoiGian.Click += new EventHandler(itemSuaThoiGian_Click);
                        e.Menu.Items.Add(itemSuaThoiGian);
                    }
                    if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_03"))
                    {
                        DXMenuItem itemSuaThoiGian_TT = new DXMenuItem("Sửa thời gian chỉ định/sử dụng/trả kết quả (Đã ra viện)");
                        itemSuaThoiGian_TT.Image = imMenu.Images[6];
                        itemSuaThoiGian_TT.Click += new EventHandler(itemSuaThoiGian_TT_Click);
                        e.Menu.Items.Add(itemSuaThoiGian_TT);
                        itemSuaThoiGian_TT.BeginGroup = true;
                    }
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Xóa phiếu chỉ định");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[1];
                    itemXoaPhieuChiDinh.Click += new EventHandler(itemXoaPhieuChiDinh_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                    itemXoaPhieuChiDinh.BeginGroup = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewChiTiet_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_01"))
                    {
                        DXMenuItem itemSuaThoiGian = new DXMenuItem("Sửa thời gian trả kết quả dịch vụ");
                        itemSuaThoiGian.Image = imMenu.Images[4];
                        itemSuaThoiGian.Click += new EventHandler(repositoryItemButton_SuaDV_Click);
                        e.Menu.Items.Add(itemSuaThoiGian);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        //Sua thoi gian chi dinh dich vu
        private void repositoryItemButtonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckPermission.ChkPerModule("THAOTAC_01"))
                {
                    itemSuaThoiGian_Click(null, null);
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BAN_KHONG_CO_QUYEN_CHINH_SUA);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void itemSuaThoiGian_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                string trangthai = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "trangthai").ToString();

                SuaPhieuCDDVDTO _filter = new SuaPhieuCDDVDTO();

                _filter.vienphiid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "vienphiid").ToString());
                _filter.maubenhphamid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                _filter.phieudieutriid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "phieudieutriid").ToString());
                _filter.thoigianchidinh = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamdate").ToString());
                _filter.thoigiansudung = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamdate_sudung").ToString());
                _filter.maubenhphamfinishdate = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamfinishdate").ToString());
                _filter.medicalrecordid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "medicalrecordid").ToString());

                if (trangthai == "Đang điều trị")
                {
                    XyLyMauBenhPham.frmSuaThoiGianChiDinh frmsuaTGCD = new XyLyMauBenhPham.frmSuaThoiGianChiDinh(_filter);
                    frmsuaTGCD.ShowDialog();
                    gridControlDS_PhieuDichVu.DataSource = null;
                    btnTimKiem_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Hồ sơ bệnh án đã đóng. \nKhông cho phép sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void itemSuaThoiGian_TT_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                SuaPhieuCDDVDTO _filter = new SuaPhieuCDDVDTO();

                _filter.vienphiid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "vienphiid").ToString());
                _filter.maubenhphamid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                _filter.phieudieutriid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "phieudieutriid").ToString());
                _filter.thoigianchidinh = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamdate").ToString());
                _filter.thoigiansudung = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamdate_sudung").ToString());
                _filter.maubenhphamfinishdate = Convert.ToDateTime(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamfinishdate").ToString());
                _filter.medicalrecordid = Convert.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                //Khong can kiem tra trang thai
                XyLyMauBenhPham.frmSuaThoiGianChiDinh frmsuaTGCD = new XyLyMauBenhPham.frmSuaThoiGianChiDinh(_filter);
                frmsuaTGCD.ShowDialog();
                gridControlDS_PhieuDichVu.DataSource = null;
                btnTimKiem_Click(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        //Sua thoi gian tra KQ dich vu
        private void repositoryItemButton_SuaDV_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChiTiet.FocusedRowHandle;

                SuaPhieuCDDVTraKetQuaDTO _filter = new SuaPhieuCDDVTraKetQuaDTO();
                _filter.servicepriceid = Convert.ToInt64(gridViewChiTiet.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                _filter.servicepricename = gridViewChiTiet.GetRowCellValue(rowHandle, "servicepricename").ToString();
                _filter.maubenhphamgrouptypeid = Convert.ToInt64(gridViewChiTiet.GetRowCellValue(rowHandle, "maubenhphamgrouptypeid").ToString());

                if (gridViewChiTiet.GetRowCellValue(rowHandle, "thoigiantraketqua") != null && gridViewChiTiet.GetRowCellValue(rowHandle, "thoigiantraketqua").ToString() != "")
                {
                    _filter.thoigiantraketqua = Convert.ToDateTime(gridViewChiTiet.GetRowCellValue(rowHandle, "thoigiantraketqua").ToString());
                    if (gridViewChiTiet.GetRowCellValue(rowHandle, "phauthuatthuthuatdate") != null && gridViewChiTiet.GetRowCellValue(rowHandle, "phauthuatthuthuatdate").ToString() != "")
                    {
                        _filter.phauthuatthuthuatdate = Convert.ToDateTime(gridViewChiTiet.GetRowCellValue(rowHandle, "phauthuatthuthuatdate").ToString());
                    }
                    else
                    {
                        _filter.phauthuatthuthuatdate = Convert.ToDateTime("0001-01-01 00:00:00"); ;
                    }

                    frmSuaThoiGianTraKQ_DV _frmDV = new frmSuaThoiGianTraKQ_DV(_filter);
                    _frmDV.ShowDialog();
                    gridControlChiTiet.DataSource = null;
                    gridControlDS_PhieuDichVu_Click(null, null);
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_CHUA_TRA_KET_QUA);
                    frmthongbao.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Xoa phieu CDDV
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
        private void itemXoaPhieuChiDinh_Click(object sender, EventArgs e)
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
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa phiếu và thuốc/vật tư mã: " + maubenhphamid + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_12');";
                                string update_medicine_store_bill = "UPDATE medicine_store_bill SET isremove=1 WHERE maubenhphamid='" + maubenhphamid + "';";

                                if (maubenhphamphieutypeid == 0) //phieu chi dinh
                                {
                                    //Lay so luong thuoc da xuat
                                    string laysoluongthuoc = "SELECT me.medicinestorerefid, me.medicinestorebillid, me.medicinestorebillcode, me.accept_soluong FROM medicine me WHERE me.medicinestorebillid=" + medicinestorebillid_ex + "; ";
                                    DataView listsoluongthuoc = new DataView(condb.GetDataTable_HIS(laysoluongthuoc));
                                    if (listsoluongthuoc != null && listsoluongthuoc.Count > 0)
                                    {
                                        for (int i = 0; i < listsoluongthuoc.Count; i++)
                                        {
                                            string update_medicine_store_ref = "UPDATE medicine_store_ref SET soluongtonkho=soluongtonkho + " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + ", soluongkhadung=soluongkhadung + " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + " WHERE medicinestorerefid= '" + listsoluongthuoc[i]["medicinestorerefid"].ToString() + "';";
                                            condb.ExecuteNonQuery_HIS(update_medicine_store_ref);
                                        }
                                    }
                                }
                                else //phieu tra
                                {
                                    //Lay so luong thuoc da xuat
                                    string laysoluongthuoc = "SELECT me.medicinestorerefid, me.medicinestorebillid, me.medicinestorebillcode, me.accept_soluong FROM medicine me WHERE me.medicinestorebillid=" + medicinestorebillid_ex + "; ";
                                    DataView listsoluongthuoc = new DataView(condb.GetDataTable_HIS(laysoluongthuoc));
                                    if (listsoluongthuoc != null && listsoluongthuoc.Count > 0)
                                    {
                                        for (int i = 0; i < listsoluongthuoc.Count; i++)
                                        {
                                            string update_medicine_store_ref = "UPDATE medicine_store_ref SET soluongtonkho=soluongtonkho - " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + ", soluongkhadung=soluongkhadung - " + Utilities.Util_TypeConvertParse.ToDecimal(listsoluongthuoc[i]["accept_soluong"].ToString()) + " WHERE medicinestorerefid= '" + listsoluongthuoc[i]["medicinestorerefid"].ToString() + "';";
                                            condb.ExecuteNonQuery_HIS(update_medicine_store_ref);
                                        }
                                    }
                                }
                                string delete_medicine = "DELETE FROM medicine WHERE medicinestorebillid in (select medicinestorebillid from medicine_store_bill where maubenhphamid=" + maubenhphamid + ");";
                                //-------                    
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);

                                if (condb.ExecuteNonQuery_HIS(delete_medicine) && condb.ExecuteNonQuery_HIS(delete_maubenhpham) && condb.ExecuteNonQuery_HIS(delete_serviceprice) && condb.ExecuteNonQuery_HIS(update_medicine_store_bill))
                                {
                                    MessageBox.Show("Xóa phiếu thuốc/vật tư mã: " + maubenhphamid + " thành công.\nVui lòng kiểm tra lại", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
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
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa phiếu và dịch vụ mã: " + maubenhphamid + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_12');";
                            condb.ExecuteNonQuery_HIS(sqlxecute_mbp);
                            condb.ExecuteNonQuery_HIS(sqlexcute_ser);
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
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



        #endregion


        #region Menu Popup

        #endregion
    }
}
