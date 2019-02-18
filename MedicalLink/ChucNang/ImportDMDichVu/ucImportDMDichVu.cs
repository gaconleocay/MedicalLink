using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using MedicalLink.Base;
using MedicalLink.ChucNang.ImportDMDichVu;
using Aspose.Cells;
using Excel = Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;


namespace MedicalLink.ChucNang
{
    public partial class ucImportDMDichVu : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private string worksheetName = "DanhMucDichVu";      // Tên Sheet 
        private List<classServicePriceRef> lstServicePriceRef { get; set; }
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
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogSelect.FileName;
                    gridControlDichVu.DataSource = null;
                    lstServicePriceRef = new List<classServicePriceRef>();
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets[worksheetName];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(6, 0, worksheet.Cells.MaxDataRow - 5, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null && data_Excel.Rows.Count > 0)
                    {
                        foreach (DataRow row in data_Excel.Rows)
                        {
                            if (row["STT"].ToString() != "")
                            {
                                classServicePriceRef servicePriceRef = new classServicePriceRef();
                                servicePriceRef.stt = Utilities.TypeConvertParse.ToInt64(row["STT"].ToString());
                                servicePriceRef.servicegrouptype = Utilities.TypeConvertParse.ToInt64(row["SERVICEGROUPTYPE"].ToString());
                                servicePriceRef.servicegrouptype_name = row["SERVICEGROUPTYPE_NAME"].ToString();
                                servicePriceRef.servicepricecode = row["SERVICEPRICECODE"].ToString().Trim();
                                servicePriceRef.servicepricegroupcode = row["SERVICEPRICEGROUPCODE"].ToString();
                                servicePriceRef.servicepricecodeuser = row["SERVICEPRICECODEUSER"].ToString();
                                servicePriceRef.servicepricesttuser = row["SERVICEPRICESTTUSER"].ToString();
                                servicePriceRef.servicepricename = row["SERVICEPRICENAME"].ToString();
                                servicePriceRef.servicepricenamebhyt = row["SERVICEPRICENAMEBHYT"].ToString();
                                servicePriceRef.servicepriceunit = row["SERVICEPRICEUNIT"].ToString();
                                servicePriceRef.servicepricefeebhyt = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEEBHYT"].ToString());
                                servicePriceRef.servicepricefeenhandan = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEENHANDAN"].ToString());
                                servicePriceRef.servicepricefee = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEE"].ToString());
                                servicePriceRef.servicepricefeenuocngoai = Utilities.TypeConvertParse.ToDecimal(row["SERVICEPRICEFEENUOCNGOAI"].ToString());
                                servicePriceRef.servicepricefee_old_date = row["SERVICEPRICEFEE_OLD_DATE"].ToString();
                                servicePriceRef.servicepricefee_old_type = Utilities.TypeConvertParse.ToInt64(row["SERVICEPRICEFEE_OLD_TYPE"].ToString());
                                servicePriceRef.servicepricefee_old_type_name = row["SERVICEPRICEFEE_OLD_TYPE_NAME"].ToString();
                                servicePriceRef.pttt_hangid = Utilities.TypeConvertParse.ToInt64(row["PTTT_HANGID"].ToString());
                                servicePriceRef.pttt_hangid_name = row["PTTT_HANGID_NAME"].ToString();
                                servicePriceRef.pttt_loaiid = Utilities.TypeConvertParse.ToInt64(row["PTTT_LOAIID"].ToString());
                                servicePriceRef.pttt_loaiid_name = row["PTTT_LOAIID_NAME"].ToString();
                                servicePriceRef.servicelock = Utilities.TypeConvertParse.ToInt64(row["SERVICELOCK"].ToString());
                                servicePriceRef.bhyt_groupcode = row["BHYT_GROUPCODE"].ToString();
                                servicePriceRef.bhyt_groupcode_name = row["BHYT_GROUPCODE_NAME"].ToString();
                                servicePriceRef.report_groupcode = row["REPORT_GROUPCODE"].ToString();
                                servicePriceRef.report_tkcode = row["REPORT_TKCODE"].ToString();
                                servicePriceRef.servicepricetype = Utilities.TypeConvertParse.ToInt64(row["SERVICEPRICETYPE"].ToString());
                                servicePriceRef.servicecode = row["SERVICECODE"].ToString();
                                lstServicePriceRef.Add(servicePriceRef);
                            }
                        }
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                        return;
                    }
                    gridControlDichVu.DataSource = lstServicePriceRef;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA_KIEM_TRA_LAI_TEMPLATE);
                frmthongbao.Show();
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        // update danh mục dịch vụ
        private void btnUpdateDVOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstServicePriceRef != null && lstServicePriceRef.Count > 0)
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
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
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
                condb.ExecuteNonQuery_HIS(sql_bak_gia);
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
                frmChonNhomDichVu frmchon = new frmChonNhomDichVu();
                frmchon.ShowDialog();
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }



    }
}
