using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucImportDMDichVu : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string worksheetName = "DanhMucDichVu";      // Tên Sheet 

        internal List<ClassCommon.classDanhMucPhong> lstDanhSachPhongThucHien { get; set; }



        public ucImportDMDichVu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucImportDMDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                cbbChonLoai.ReadOnly = true;
                radioButtonCapNhat.Checked = true;
                radioButtonThemMoi.Checked = false;
                cbbChonKieu.EditValue = "";
                cbbChonLoai.EditValue = "";
                btnUpdateDVOK.Enabled = false;
                LoadDanhMucPhongThucHienDichVu();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu để import mới
            gridControlDichVu.DataSource = null;
            gridViewDichVu.Columns.Clear();
            gridControlDichVu.Refresh();

            if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialogSelect.FileName;
                ReadExcelFile _excel = new ReadExcelFile(openFileDialogSelect.FileName);
                var data = _excel.GetDataTable("SELECT LOAI_DV, MA_DV, MA_NHOM, MA_DV_USER, MA_DV_STTTHAU, TEN_VP, TEN_BH, TEN_PTTT, DVT, GIA_BH, GIA_VP, GIA_YC, GIA_NNN, THOIGIAN_APDUNG, THEO_NGAY_CHI_DINH, HANG_PTTT, LOAI_PTTT, KHOA, NHOM_BHYT, NHOM_BAOCAO, NHOM_TAIKHOAN, PHONG_THUCHIEN, LA_NHOM, MA_CLS FROM [" + worksheetName + "$]");
                if (data != null)
                {
                    gridControlDichVu.DataSource = data;
                }

                // Thông báo hiển thị dữ liệu
                if (gridViewDichVu.RowCount > 0)
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Hiển thị dữ liệu thành công !";
                    btnUpdateDVOK.Enabled = true;
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "              Có lỗi xảy ra ! \nKiểm tra lại định dạng file excel";
                }
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        // update danh mục dịch vụ
        private void btnUpdateDVOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCapNhat.Checked == true && radioButtonThemMoi.Checked == false)
                {
                    CapNhatDanhMucDichVuProcess();
                }
                else if (radioButtonCapNhat.Checked == false && radioButtonThemMoi.Checked == true)
                {
                    ThemMoiDanhMucDichVuProcess();
                }
                else
                {
                    MessageBox.Show("Không xác định được loại yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
            }
        }

        // Đánh số thứ tự ở cột Indicator gridView
        private void gridViewDichVu_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridControlDichVu_Click(object sender, EventArgs e)
        {

        }

        // Backup lại bảng giá dịch vụ cũ
        private void btnBackupGia_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // Xóa cột dữ liệu backup cũ đi
            //    //string sql_drop_colume = "ALTER TABLE ServicePriceRef DROP COLUMN Tools_TGApDung_bak_1; ALTER TABLE ServicePriceRef DROP COLUMN Tools_gia_bak_1; ALTER TABLE ServicePriceRef DROP COLUMN Tools_giaNhanDan_bak_1; ALTER TABLE ServicePriceRef DROP COLUMN Tools_giaBHYT_bak_1; ALTER TABLE ServicePriceRef DROP COLUMN Tools_giaNuocNgoai_bak_1; ALTER TABLE ServicePriceRef DROP COLUMN Tools_KieuApDung_bak_1;";
            //    //condb.ExecuteNonQuery(sql_drop_colume);

            //    // Thêm cột để chứa dữ liệu backup giá dịch vụ cũ
            //string sql_insert_colum = "ALTER TABLE ServicePriceRef ADD Tools_TGApDung_bak_1 timestamp without time zone; ALTER TABLE ServicePriceRef ADD Tools_gia_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNhanDan_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaBHYT_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_giaNuocNgoai_bak_1 text; ALTER TABLE ServicePriceRef ADD Tools_KieuApDung_bak_1 integer DEFAULT 0;";
            //condb.ExecuteNonQuery(sql_insert_colum);
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Vui lòng cập nhật lại cấu trúc cơ sở dữ liệu !", "Thông báo");
            //}
            try
            {
                string sql_bak_gia = "UPDATE ServicePriceRef SET Tools_TGApDung_bak_1 = ServicePriceFee_OLD_DATE, Tools_gia_bak_1 = ServicePriceFee_OLD, Tools_giaBHYT_bak_1 = ServicePriceFeeBHYT_OLD, Tools_giaNhanDan_bak_1 = ServicePriceFeeNhanDan_OLD, Tools_giaNuocNgoai_bak_1 = ServicePriceFeeNuocNgoai_OLD, Tools_KieuApDung_bak_1 = ServicePriceFee_OLD_Type;";
                condb.ExecuteNonQuery(sql_bak_gia);
                MessageBox.Show("Backup thành công giá cũ sang cột dự phòng", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình backup lại giá cũ" + ex, "Thông báo");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                XuatDanhMucTuDBSangExCelProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra" + ex, "Thông báo");
            }
        }

        private void cbbChonKieu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbChonKieu.EditValue == "Giá Viện phí" || cbbChonKieu.EditValue == "Giá BHYT" || cbbChonKieu.EditValue == "Giá Yêu cầu" || cbbChonKieu.EditValue == "Giá Người nước ngoài" || cbbChonKieu.EditValue == "Cả 4 giá (VP+BH+YC+NN)")
                {
                    cbbChonLoai.ReadOnly = false;
                    cbbChonLoai.EditValue = "";
                }
                else
                {
                    cbbChonLoai.ReadOnly = true;
                    cbbChonLoai.EditValue = "";
                }
            }
            catch (Exception)
            {

            }
        }

        private void radioButtonThemMoi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonThemMoi.Checked == true)
                {
                    radioButtonCapNhat.Checked = false;
                    cbbChonKieu.ReadOnly = true;
                    cbbChonLoai.ReadOnly = true;
                    cbbChonKieu.EditValue = "";
                    cbbChonLoai.EditValue = "";
                }
                else
                {
                    //radioButtonCapNhat.Checked = true;
                    //cbbChonKieu.ReadOnly = false;
                    //cbbChonLoai.ReadOnly = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void radioButtonCapNhat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCapNhat.Checked == true)
                {
                    radioButtonThemMoi.Checked = false;
                    cbbChonKieu.ReadOnly = false;
                    cbbChonLoai.ReadOnly = true;
                }
                else
                {
                    //radioButtonThemMoi.Checked = true;
                    //cbbChonKieu.ReadOnly = true;
                    //cbbChonLoai.ReadOnly = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void gridViewDichVu_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void LoadDanhMucPhongThucHienDichVu()
        {
            try
            {
                lstDanhSachPhongThucHien = new List<classDanhMucPhong>();

                condb.connect();
                string sql_kt = "SELECT departmentid, departmentgroupid, departmentcode,departmentname,departmenttype FROM department WHERE departmenttype in (6,7);";
                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                if (dv_kt.Count > 0)
                {
                    for (int i=0;i<dv_kt.Count;i++)
                    {
                        classDanhMucPhong danhmucphong = new classDanhMucPhong();
                        danhmucphong.departmentid = dv_kt[i]["departmentid"].ToString();
                        danhmucphong.departmentgroupid = dv_kt[i]["departmentgroupid"].ToString();
                        danhmucphong.departmentcode = dv_kt[i]["departmentcode"].ToString();
                        danhmucphong.departmentname = dv_kt[i]["departmentname"].ToString();
                        danhmucphong.departmenttype = dv_kt[i]["departmenttype"].ToString();
                        lstDanhSachPhongThucHien.Add(danhmucphong);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
