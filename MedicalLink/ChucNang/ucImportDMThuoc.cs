using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucImportDMThuoc : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string worksheetName = "DanhMucThuoc";      // Tên Sheet 
        public ucImportDMThuoc()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu để import mới
            gridControlThuoc.DataSource = null;
            gridViewThuoc.Columns.Clear();
            gridControlThuoc.Refresh();

            if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialogSelect.FileName;
                ReadExcelFile _excel = new ReadExcelFile(openFileDialogSelect.FileName);
                gridControlThuoc.DataSource = _excel.GetDataTable("SELECT MATHUOC, TENTHUOC, MATHUOC_USER, STT_THAU, NAMCUNGUNG, DANH_STT_SD, DUONGDUNG, DONGGOI, SODANGKY, SOLO FROM [" + worksheetName + "$]");

                // Thông báo hiển thị dữ liệu
                if (gridViewThuoc.RowCount > 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.HIEN_THI_DU_LIEU_THANH_CONG);
                    frmthongbao.Show();
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA_KIEM_TRA_LAI_TEMPLATE);
                    frmthongbao.Show();
                }

            }
        }

        // Sau khi chọn kiểu Import thì sẽ import dữ liệu vào DB. Nếu dữ liệu mới thì Insert, nếu dữ liệu cũ thì Update
        private void btnNVOK_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string chonkieuimport = cbbChonKieu.Text.Trim();
            // Lấy thời gian hiện tại
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            switch (chonkieuimport)
            {
                case "Tên thuốc":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT MedicineRefID, MedicineCode FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update tên thuốc
                                        string sqlupdatetenthuoc = "UPDATE medicine_ref SET MedicineName = '" + gridViewThuoc.GetRowCellValue(i, "TENTHUOC") + "', MedicineName_BYT = '" + gridViewThuoc.GetRowCellValue(i, "TENTHUOC") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatetenthuoc);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }

                            //lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục tên thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã Update Tên thuốc
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Tên thuốc\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Mã DM BYT (mã User)":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update mã DM BYT (mã User)
                                        string sqlupdatemauser = "UPDATE medicine_ref SET MedicineCodeUser = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC_USER") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatemauser);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục Mã DM BYT (mã user) thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã Update mã DM BYT (mã User)
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Mã DM BYT (mã user)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Mã STT thầu BHYT":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update mã STT Thầu BHYT
                                        string sqlupdatesttthau = "UPDATE medicine_ref SET STT_DauThau = '" + gridViewThuoc.GetRowCellValue(i, "STT_THAU") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatesttthau);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục mã STT thầu BHYT thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã Update mã STT Thầu BHYT
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Mã STT thầu BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Năm thầu":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update Năm thầu
                                        string sqlupdatenamthau = "UPDATE medicine_ref SET NamCungUng = '" + gridViewThuoc.GetRowCellValue(i, "NAMCUNGUNG") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatenamthau);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục năm thầu thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã Update Năm thầu
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Năm thầu\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Đánh STT ngày SD":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update Đánh STT ngày SD
                                        string sqlupdatesttngaysd = "UPDATE medicine_ref SET DanhSTTDungThuoc = '" + gridViewThuoc.GetRowCellValue(i, "DANH_STT_SD") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatesttngaysd);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục Đánh STT ngày dùng thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã Update Đánh STT ngày SD
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Đánh STT ngày dùng\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Đường dùng":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update mã đường dùng
                                        string sqlupdateduongdung = "UPDATE medicine_ref SET DangDung = '" + gridViewThuoc.GetRowCellValue(i, "DUONGDUNG") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdateduongdung);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục đường dùng thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã đường dùng
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Đường dùng\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }
                //
                case "Đóng gói":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update mã đóng gói
                                        string sqlupdatedonggoi = "UPDATE medicine_ref SET DongGoi = '" + gridViewThuoc.GetRowCellValue(i, "DONGGOI") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatedonggoi);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục đóng gói thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã update đóng gói
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Đóng gói\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Số đăng ký":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update số đăng ký
                                        string sqlupdatesdk = "UPDATE medicine_ref SET SoDangKy = '" + gridViewThuoc.GetRowCellValue(i, "SODANGKY") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatesdk);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục số đăng ký thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã update số đăng ký
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Số đăng ký\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }

                case "Số lô":
                    {
                        int count_thuoc = 0;
                        try
                        {
                            for (int i = 0; i < gridViewThuoc.RowCount; i++)
                            {
                                condb.connect();
                                string sql_kt = "SELECT * FROM medicine_ref WHERE medicinecode= '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    try
                                    {
                                        // Update số lô
                                        string sqlupdatesolo = "UPDATE medicine_ref SET SoLo = '" + gridViewThuoc.GetRowCellValue(i, "SOLO") + "' WHERE medicinecode = '" + gridViewThuoc.GetRowCellValue(i, "MATHUOC") + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatesolo);
                                        count_thuoc += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                            // Lưu lại log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_thuoc + " danh mục số lô thuốc thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);

                            // Thông báo đã update số lô
                            MessageBox.Show("Update " + count_thuoc + " danh mục \"Số lô\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                        }

                        break;
                    }


                case "Nồng độ":
                    {
                        //NongDo = 'nogndo'
                        break;
                    }

                case "Hàm lượng":
                    {
                        //LieuLuong = 'hamluong'
                        break;
                    }

                default:
                    {
                        MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
            }
            SplashScreenManager.CloseForm();
        }

        // Đánh số thứ tự ở cột Indicator gridView
        private void gridViewThuoc_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

    }
}
