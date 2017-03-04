using DevExpress.XtraSplashScreen;
using MedicalLink.Base;
using MedicalLink.ChucNang.ImportDMDichVu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang
{
    public partial class ucImportDMDichVu : UserControl
    {
        //Cap nhat danh muc dich vu
        private void CapNhatDanhMucDichVuProcess()
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string chonkieuimport = cbbChonKieu.Text.Trim();
                if (cbbChonKieu.Text.Trim() != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Hãy backup trước khi thực hiện.\nNhấn \"YES\" để tiếp tục, nhấn \"NO\" để quay lại backup ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        switch (chonkieuimport)
                        {
                            case "Tên dịch vụ BHYT":
                                {
                                    CapNhatProcess_TenDichVu_BHYT();
                                    break;
                                }
                            case "Tên dịch vụ viện phí":
                                {
                                    CapNhatProcess_TenDichVu_VP();
                                    break;
                                }
                            case "Tên dịch vụ PTTT":
                                {
                                    CapNhatProcess_TenDichVu_PTTT();
                                    break;
                                }
                            case "Mã DM BYT (mã User)":
                                {
                                    CapNhatProcess_MaUser();
                                    break;
                                }
                            case "Mã STT thầu BHYT":
                                {
                                    CapNhatProcess_MaSTTThau();
                                    break;
                                }
                            case "Đơn vị tính":
                                {
                                    CapNhatProcess_DonViTinh();
                                    break;
                                }
                            case "Hạng PTTT":
                                {
                                    CapNhatProcess_HangPTTT();
                                    break;
                                }
                            case "Loại PTTT":
                                {
                                    CapNhatProcess_LoaiPTTT();
                                    break;
                                }
                            case "Khóa dịch vụ":
                                {
                                    CapNhatProcess_KhoaDichVu();
                                    break;
                                }
                            case "Giá Viện phí":
                                {
                                    CapNhatProcess_GiaVienPhi();
                                    break;
                                }
                            case "Giá BHYT":
                                {
                                    CapNhatProcess_GiaBHYT();
                                    break;
                                }
                            case "Giá Yêu cầu":
                                {
                                    CapNhatProcess_GiaYeuCau();
                                    break;
                                }
                            case "Giá Người nước ngoài":
                                {
                                    CapNhatProcess_GiaNguoiNuocNgoai();
                                    break;
                                }
                            case "Cả 4 giá (VP+BH+YC+NN)":
                                {
                                    CapNhatProcess_CaBonLoaiGia();
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin";
                }

                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            { }
        }

        #region Xu ly Cap nhat
        //Ten dich vu BHYT
        private void CapNhatProcess_TenDichVu_BHYT()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    if (gridViewDichVu.GetRowCellValue(i, "TEN_BH").ToString().Trim() != "")
                    {
                        condb.connect();
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                        DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                            {
                                // Lấy ID dịch vụ:
                                string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                try
                                {
                                    // Update tên dịch vụ
                                    string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceNameBHYT = '" + gridViewDichVu.GetRowCellValue(i, "TEN_BH") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                    condb.ExecuteNonQuery(sqlupdatetendv);
                                    count_dv += dv_kt.Count;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục tên dịch vụ BHYT thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }
                // Thông báo đã Update Tên dịch vụ
                MessageBox.Show("Update " + count_dv + " danh mục \"Tên dịch vụ BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }
        }
        //Ten dich vu Vien phi
        private void CapNhatProcess_TenDichVu_VP()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    if (gridViewDichVu.GetRowCellValue(i, "TEN_VP").ToString().Trim() != "")
                    {
                        condb.connect();
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                        DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                            {
                                // Lấy ID dịch vụ:
                                string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                try
                                {
                                    // Update tên dịch vụ
                                    string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceName = '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "', ServicePriceNameNhanDan = '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                    condb.ExecuteNonQuery(sqlupdatetendv);
                                    count_dv += dv_kt.Count;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục tên dịch vụ viện phí thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }
                // Thông báo đã Update Tên dịch vụ
                MessageBox.Show("Update " + count_dv + " danh mục \"Tên dịch vụ viện phí\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }
        }
        //Ten dich vu PTTT
        private void CapNhatProcess_TenDichVu_PTTT()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    if (gridViewDichVu.GetRowCellValue(i, "TEN_PTTT").ToString().Trim() != "")
                    {
                        condb.connect();
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                        DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                            {
                                // Lấy ID dịch vụ:
                                string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                try
                                {
                                    // Update tên dịch vụ
                                    string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceNameNuocNgoai = '" + gridViewDichVu.GetRowCellValue(i, "TEN_PTTT") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                    condb.ExecuteNonQuery(sqlupdatetendv);
                                    count_dv += dv_kt.Count;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục tên dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }
                // Thông báo đã Update Tên dịch vụ
                MessageBox.Show("Update " + count_dv + " danh mục \"Tên dịch vụ\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }
        }

        // Mã DM BYT (mã User)
        private void CapNhatProcess_MaUser()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_dv = 0;
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    condb.connect();
                    string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                    DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                        {
                            // Lấy ID dịch vụ:
                            string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                            try
                            {
                                // Update mã User
                                string sqlupdatemauser = "UPDATE ServicePriceRef SET ServicePriceCodeUser = '" + gridViewDichVu.GetRowCellValue(i, "MA_DV_USER") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                condb.ExecuteNonQuery(sqlupdatemauser);
                                count_dv += dv_kt.Count;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục mã user dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }

                // Thông báo đã Update mã dịch vụ
                MessageBox.Show("Update " + count_dv + " danh mục \"Mã DM BYT (mã User)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }

        }

        //Mã STT thầu BHYT
        private void CapNhatProcess_MaSTTThau()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_dv = 0;
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    condb.connect();
                    string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                    DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                        {
                            // Lấy ID dịch vụ:
                            string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                            try
                            {
                                // Update mã STT Thầu BHYT
                                string sqlupdatesttthau = "UPDATE ServicePriceRef SET ServicePriceSTTUser = '" + gridViewDichVu.GetRowCellValue(i, "MA_DV_STTTHAU") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                condb.ExecuteNonQuery(sqlupdatesttthau);
                                count_dv += dv_kt.Count;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục mã STT Thầu BHYT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }

                // Thông báo đã Update mã STT Thầu BHYT
                MessageBox.Show("Update " + count_dv + " danh mục \"Mã STT Thầu BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }

        }

        //Don vi tinh
        private void CapNhatProcess_DonViTinh()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_dv = 0;
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    condb.connect();
                    string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                    DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                    if (dv_kt.Count > 0)
                    {
                        for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                        {
                            // Lấy ID dịch vụ:
                            string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                            try
                            {
                                // Update Đơn vị tính
                                string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceUnit = '" + gridViewDichVu.GetRowCellValue(i, "DVT") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                condb.ExecuteNonQuery(sqlupdatedvt);
                                count_dv += dv_kt.Count;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục đơn vị tính dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Đơn vị tính\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }

        }

        //Hang PTTT
        private void CapNhatProcess_HangPTTT()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_dv = 0;
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    if (gridViewDichVu.GetRowCellValue(i, "HANG_PTTT") != null && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "HANG_PTTT").ToString()) == true)
                    {
                        condb.connect();
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                        DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                            {
                                // Lấy ID dịch vụ:
                                string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                try
                                {
                                    // Update Đơn Loai PTTT
                                    string sqlupdateloaipttt = "UPDATE ServicePriceRef SET PTTT_HangID = '" + gridViewDichVu.GetRowCellValue(i, "HANG_PTTT").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                    condb.ExecuteNonQuery(sqlupdateloaipttt);
                                    count_dv += dv_kt.Count;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục hạng PTTT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Hạng PTTT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }

        }

        //Loai PTTT
        private void CapNhatProcess_LoaiPTTT()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_dv = 0;
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    if (gridViewDichVu.GetRowCellValue(i, "LOAI_PTTT") != null && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "LOAI_PTTT").ToString()) == true)
                    {
                        condb.connect();
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                        DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                            {
                                // Lấy ID dịch vụ:
                                string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                try
                                {
                                    // Update Đơn Loai PTTT
                                    string sqlupdateloaipttt = "UPDATE ServicePriceRef SET PTTT_LoaiID = '" + gridViewDichVu.GetRowCellValue(i, "LOAI_PTTT").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                    condb.ExecuteNonQuery(sqlupdateloaipttt);
                                    count_dv += dv_kt.Count;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục loại PTTT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Loại PTTT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }

        }

        //Khoa dich vu
        private void CapNhatProcess_KhoaDichVu()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count_dv = 0;
            try
            {
                for (int i = 0; i < gridViewDichVu.RowCount; i++)
                {
                    if (gridViewDichVu.GetRowCellValue(i, "KHOA") != null && (gridViewDichVu.GetRowCellValue(i, "KHOA").ToString().Trim() == "0" || gridViewDichVu.GetRowCellValue(i, "KHOA").ToString().Trim() == "1"))
                    {
                        condb.connect();
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                        DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                            {
                                // Lấy ID dịch vụ:
                                string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                try
                                {
                                    // Update Đơn vị tính
                                    string sqlupdatedvt = "UPDATE ServicePriceRef SET ServiceLock = '" + gridViewDichVu.GetRowCellValue(i, "KHOA").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                    condb.ExecuteNonQuery(sqlupdatedvt);
                                    count_dv += dv_kt.Count;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục khóa dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Khóa dịch vụ\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }
        }

        //Gia vien phi
        private void CapNhatProcess_GiaVienPhi()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (cbbChonLoai.Text.Trim() == "Sửa giá")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (gridViewDichVu.GetRowCellValue(i, "GIA_VP") != null && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString()) == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        // Update Giá Nhân dân
                                        string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatedvt);
                                        count_dv += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá nhân dân của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá Viện phí\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá Viện phí" + ex, "Thông báo");
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (gridViewDichVu.GetRowCellValue(i, "GIA_VP") != null && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString()) == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        if (Base.classCheckInputString.CheckFormatDatetime(gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() ?? "") == true && (gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "1" || gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "0"))
                                        {
                                            // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                            string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan_OLD = ServicePriceFeeNhanDan WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sql_chuyen_giaNhanDan);
                                            // Update Giá Nhân dân
                                            string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() + "', ServicePriceFee_OLD_DATE = '" + gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() + "', ServicePriceFee_OLD_Type = '" + gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sqlupdatedvt);
                                            count_dv += dv_kt.Count;
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá nhân dân của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá Viện phí\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá Viện phí" + ex, "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại cập nhật", "Thông báo");
            }
        }

        //Gia BHYT
        private void CapNhatProcess_GiaBHYT()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (cbbChonLoai.Text.Trim() == "Sửa giá")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (gridViewDichVu.GetRowCellValue(i, "GIA_BH") != null && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString()) == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        // Update Giá BHYT
                                        string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeBHYT = '" + gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatedvt);
                                        count_dv += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá BHYT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá BHYT" + ex, "Thông báo");
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        if (Base.classCheckInputString.CheckFormatDatetime(gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() ?? "") == true && (gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "1" || gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "0"))
                                        {
                                            // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                            string sql_chuyen_giaBHYT = "UPDATE ServicePriceRef SET ServicePriceFeeBHYT_OLD = ServicePriceFeeBHYT WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sql_chuyen_giaBHYT);
                                            // Update Giá BHYT
                                            string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeBHYT = '" + gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString().Trim() + "', ServicePriceFee_OLD_DATE = '" + gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() + "', ServicePriceFee_OLD_Type = '" + gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sqlupdatedvt);
                                            count_dv += dv_kt.Count;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá BHYT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá BHYT" + ex, "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại cập nhật", "Thông báo");
            }
        }

        //Gia Yeu cau
        private void CapNhatProcess_GiaYeuCau()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (cbbChonLoai.Text.Trim() == "Sửa giá")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        // Update Giá Nhân dân
                                        string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFee = '" + gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                        condb.ExecuteNonQuery(sqlupdatedvt);
                                        count_dv += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá yêu cầu của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá Yêu cầu\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá Yêu cầu" + ex, "Thông báo");
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        if (Base.classCheckInputString.CheckFormatDatetime(gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() ?? "") == true && (gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "1" || gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "0"))
                                        {
                                            // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                            string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFee_OLD = ServicePriceFee  WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sql_chuyen_giaNhanDan);

                                            // Update Giá Nhân dân
                                            string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFee = '" + gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() + "', ServicePriceFee_OLD_DATE = '" + gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() + "', ServicePriceFee_OLD_Type = '" + gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sqlupdatedvt);
                                            count_dv += dv_kt.Count;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá yêu cầu của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá Yêu cầu\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá Yêu cầu" + ex, "Thông báo");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại cập nhật", "Thông báo");
            }
        }

        //Gia nguoi nuoc ngoai
        private void CapNhatProcess_GiaNguoiNuocNgoai()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (cbbChonLoai.Text.Trim() == "Sửa giá")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        // Update Giá Người nước ngoài
                                        string sqlupdategiaNNN = "UPDATE ServicePriceRef SET ServicePriceFeeNuocNgoai = '" + gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                        condb.ExecuteNonQuery(sqlupdategiaNNN);
                                        count_dv += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá người nước ngoài của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá Người nước ngoài
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá Người nước ngoài\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá Người nước ngoài" + ex, "Thông báo");
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        if (Base.classCheckInputString.CheckFormatDatetime(gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() ?? "") == true && (gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "1" || gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "0"))
                                        {
                                            // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                            string sql_chuyen_giaNNN = "UPDATE ServicePriceRef SET ServicePriceFeeNuocNgoai_OLD = ServicePriceFeeNuocNgoai WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sql_chuyen_giaNNN);

                                            // Update Giá Người nước ngoài
                                            string sqlupdategiaNNN = "UPDATE ServicePriceRef SET ServicePriceFeeNuocNgoai = '" + gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() + "', ServicePriceFee_OLD_DATE = '" + gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() + "', ServicePriceFee_OLD_Type = '" + gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sqlupdategiaNNN);
                                            count_dv += dv_kt.Count;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá người nước ngoài của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá Người nước ngoài\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá Người nước ngoài" + ex, "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại cập nhật", "Thông báo");
            }
        }

        //Ca 4 loai gia: BHYT+VP+YC+NNN
        private void CapNhatProcess_CaBonLoaiGia()
        {
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (cbbChonLoai.Text.Trim() == "Sửa giá")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() ?? "") == true && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString().Trim() ?? "") == true && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() ?? "") == true && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        // Update cả 4 loại giá
                                        string sqlupdategiaNNN = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() + "', ServicePriceFeeBHYT = '" + gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString().Trim() + "', ServicePriceFee = '" + gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() + "', ServicePriceFeeNuocNgoai = '" + gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                        condb.ExecuteNonQuery(sqlupdategiaNNN);
                                        count_dv += dv_kt.Count;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " 4 loại giá của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Update " + count_dv + " danh mục \"4 loại giá (VP+BH+YC+NN)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá" + ex, "Thông báo");
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                    {
                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() ?? "") == true && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString().Trim() ?? "") == true && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() ?? "") == true && Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() ?? "") == true)
                        {
                            condb.connect();
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                            DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                for (int j = 0; j < dv_kt.Count; j++)   //Phòng trường hợp có mã trùng nhau
                                {
                                    // Lấy ID dịch vụ:
                                    string id_dv = dv_kt[j]["ServicePriceRefID"].ToString();
                                    try
                                    {
                                        if (Base.classCheckInputString.CheckFormatDatetime(gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG").ToString().Trim() ?? "") == true && (gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "1" || gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH").ToString().Trim() == "0"))
                                        {
                                            // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                            string sql_chuyen_gia = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan_OLD = ServicePriceFeeNhanDan, ServicePriceFeeBHYT_OLD = ServicePriceFeeBHYT, ServicePriceFee_OLD = ServicePriceFee, ServicePriceFeeNuocNgoai_OLD = ServicePriceFeeNuocNgoai WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sql_chuyen_gia);
                                            // Update cả 4 loại giá
                                            string sqlupdategia = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + gridViewDichVu.GetRowCellValue(i, "GIA_VP") + "', ServicePriceFeeBHYT = '" + gridViewDichVu.GetRowCellValue(i, "GIA_BH") + "', ServicePriceFee = '" + gridViewDichVu.GetRowCellValue(i, "GIA_YC") + "', ServicePriceFeeNuocNgoai = '" + gridViewDichVu.GetRowCellValue(i, "GIA_NNN") + "', ServicePriceFee_OLD_DATE = '" + gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG") + "', ServicePriceFee_OLD_Type = '" + gridViewDichVu.GetRowCellValue(i, "THEO_NGAY_CHI_DINH") + "' WHERE ServicePriceRefID = '" + id_dv + "' ;";
                                            condb.ExecuteNonQuery(sqlupdategia);
                                            count_dv += dv_kt.Count;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " 4 loại giá của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Update " + count_dv + " danh mục \"4 loại giá (VP+BH+YC+NN)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra trong quá trình cập nhật giá" + ex, "Thông báo");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại cập nhật", "Thông báo");
            }
        }
        #endregion

        //Them moi danh muc dich vu
        internal void ThemMoiDanhMucDichVuProcess()
        {
            try
            {
                //string chonkieuimport = cbbChonKieu.Text.Trim();
                // Lấy thời gian hiện tại
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                int dem_dv_themmoi = 0;
                int dem_dv_trungma = 0;

                DialogResult dialogResult = MessageBox.Show("Hãy backup trước khi thực hiện.\nNhấn \"YES\" để tiếp tục, nhấn \"NO\" để quay lại backup ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    List<MedicalLink.ChucNang.ImportDMDichVu.ClassDichVu> lstDichVuTrungMa = new List<ChucNang.ImportDMDichVu.ClassDichVu>();
                    try
                    {
                        for (int i = 0; i < gridViewDichVu.RowCount; i++)
                        {
                            //if (gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() != null && gridViewDichVu.GetRowCellValue(i, "MA_DV").ToString().Trim() != "")
                            if (gridViewDichVu.GetRowCellValue(i, "MA_DV").ToString().Trim() != "" && gridViewDichVu.GetRowCellValue(i, "LOAI_DV").ToString().Trim() != "")
                            {
                                condb.connect();
                                string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode, servicepricename FROM ServicePriceRef WHERE ServicePriceCode= '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "' ;";
                                DataView dv_kt = new DataView(condb.getDataTable(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    dem_dv_trungma += dv_kt.Count;
                                    for (int j = 0; j < dv_kt.Count; j++)
                                    {
                                        ClassDichVu dichvu_them = new ClassDichVu();
                                        dichvu_them.MA_DV = dv_kt[j]["ServicePriceCode"].ToString();
                                        dichvu_them.TEN_VP = dv_kt[j]["servicepricename"].ToString();
                                        lstDichVuTrungMa.Add(dichvu_them);
                                    }
                                }
                                else if (dv_kt.Count == 0)
                                {
                                    if (gridViewDichVu.GetRowCellValue(i, "MA_NHOM").ToString().Trim() != null && gridViewDichVu.GetRowCellValue(i, "MA_NHOM").ToString().Trim() != "")
                                    {
                                        string giaVienPhi = gridViewDichVu.GetRowCellValue(i, "GIA_VP").ToString().Trim() ?? "0";
                                        string giaBHYT = gridViewDichVu.GetRowCellValue(i, "GIA_BH").ToString().Trim() ?? "0";
                                        string giaYeuCau = gridViewDichVu.GetRowCellValue(i, "GIA_YC").ToString().Trim() ?? "0";
                                        string giaNNN = gridViewDichVu.GetRowCellValue(i, "GIA_NNN").ToString().Trim() ?? "0";

                                        if (giaVienPhi == "")
                                        {
                                            giaVienPhi = "0";
                                        }
                                        if (Base.classCheckInputString.CheckISNumber(giaVienPhi) == false)
                                        {
                                            giaVienPhi = "0";
                                        }
                                        if (giaBHYT == "")
                                        {
                                            giaBHYT = "0";
                                        }
                                        if (Base.classCheckInputString.CheckISNumber(giaBHYT) == false)
                                        {
                                            giaBHYT = "0";
                                        }
                                        if (giaYeuCau == "")
                                        {
                                            giaYeuCau = "0";
                                        }
                                        if (Base.classCheckInputString.CheckISNumber(giaYeuCau) == false)
                                        {
                                            giaYeuCau = "0";
                                        }
                                        if (giaNNN == "")
                                        {
                                            giaNNN = "0";
                                        }
                                        if (Base.classCheckInputString.CheckISNumber(giaNNN) == false)
                                        {
                                            giaNNN = "0";
                                        }
                                        int la_nhom = 0;
                                        int loai_pttt = 0;
                                        int loai_dv = 0;
                                        string[] dspth_temp;
                                        string listPhongThucHien = "";

                                        if (gridViewDichVu.GetRowCellValue(i, "LA_NHOM").ToString().Trim() == "1")
                                        {
                                            la_nhom = 1;
                                        }
                                        if (Base.classCheckInputString.CheckISNumber(gridViewDichVu.GetRowCellValue(i, "LOAI_PTTT").ToString().Trim() ?? "") == true)
                                        {
                                            loai_pttt = Convert.ToInt16(gridViewDichVu.GetRowCellValue(i, "LOAI_PTTT").ToString().Trim());
                                        }
                                        //Loại DV:
                                        //1 = Khám bệnh
                                        //2: Xét nghiệm
                                        //3: Chẩn đoán hình ảnh
                                        //4: Chuyên khoa
                                        if (gridViewDichVu.GetRowCellValue(i, "LOAI_DV").ToString().Trim().ToUpper() == "KB")
                                        {
                                            loai_dv = 1;
                                        }
                                        else if (gridViewDichVu.GetRowCellValue(i, "LOAI_DV").ToString().Trim().ToUpper() == "XN")
                                        {
                                            loai_dv = 2;
                                        }
                                        else if (gridViewDichVu.GetRowCellValue(i, "LOAI_DV").ToString().Trim().ToUpper() == "CDHA")
                                        {
                                            loai_dv = 3;
                                        }
                                        else if (gridViewDichVu.GetRowCellValue(i, "LOAI_DV").ToString().Trim().ToUpper() == "CK")
                                        {
                                            loai_dv = 4;
                                        }
                                        //Phong thuc hien
                                        dspth_temp = gridViewDichVu.GetRowCellValue(i, "PHONG_THUCHIEN").ToString().Split(';');
                                        for (int m = 0; m < dspth_temp.Length; m++)
                                        {
                                            var phongId = lstDanhSachPhongThucHien.FirstOrDefault(o => o.departmentcode == dspth_temp[m].ToString());
                                            if (phongId!=null)
                                            {
                                                listPhongThucHien += phongId.departmentid + ";";
                                            }
                                        }
                                        listPhongThucHien = listPhongThucHien.Substring(0, (listPhongThucHien.Length - 1));

                                        //TODO Them moi
                                        string sql_insertserviceref = "INSERT INTO ServicePriceRef ( ServicePriceRefID_Master, ServicePriceGroupCode, ServicePriceCode, ServicePriceCodeUser, ServicePriceSTTUser, ServicePriceCode_NG, BHYT_GroupCode, Report_GroupCode, CK_GroupCode, Report_TKCode, ServicePriceName, ServicePriceNameNhanDan, ServicePriceNameBHYT, ServicePriceNameNuocNgoai, ServicePriceFee, ServicePriceFeeNhanDan, ServicePriceFeeBHYT, ServicePriceFeeNuocNgoai, ListDepartmentPhongThucHien, ListDepartmentPhongThucHienKhamGoi, ServicePriceFee_OLD, ServicePriceFeeNhanDan_OLD, ServicePriceFeeBHYT_OLD, ServicePriceFeeNuocNgoai_OLD, ServicePriceFee_OLD_Type, KhongChuyenDoiTuongHaoPhi, LuonChuyenDoiTuongHaoPhi, CDHA_SoLuongThuoc, CDHA_SoLuongVatTu, PTTT_DinhMucVTTH, PTTT_DinhMucThuoc, TyLeLaiChiDinh, TyLeLaiThucHien, TinhToanLaiGiaDVKTC,  ServicePriceUnit, LayMauPhongThucHien, ServicePriceType, ServiceGroupType, ServicePricePrintOrder, ServiceLock, ServicePriceBHYTQuyDoi, ServicePriceBHYTQuyDoi_TT, ServicePriceBHYTDinhMuc, PTTT_HangID)  VALUES( '0', '" + gridViewDichVu.GetRowCellValue(i, "MA_NHOM") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_DV_USER") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_DV_STTTHAU") + "', '', '" + gridViewDichVu.GetRowCellValue(i, "NHOM_BHYT") + "', '" + gridViewDichVu.GetRowCellValue(i, "NHOM_BAOCAO") + "', '', '" + gridViewDichVu.GetRowCellValue(i, "NHOM_TAIKHOAN") + "', '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "TEN_BH") + "', '" + gridViewDichVu.GetRowCellValue(i, "TEN_PTTT") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_YC") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_BH") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_NNN") + "', '" + listPhongThucHien + "', '', '', '', '', '', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '" + gridViewDichVu.GetRowCellValue(i, "DVT") + "', '0', '" + la_nhom + "', '" + loai_dv + "', '0', '0', '', '', '', '" + loai_pttt + "')";
                                        condb.ExecuteNonQuery(sql_insertserviceref);

                                        string sql_insertketqua = "INSERT INTO serviceref4price(servicepricecode, servicecode) VALUES ('" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_CLS") + "') ;";
                                        condb.ExecuteNonQuery(sql_insertketqua);
                                        dem_dv_themmoi += 1;

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Có lỗi xảy ra ", "Thông báo");
                                }
                            }
                        }

                        //lưu lại log
                        if (dem_dv_themmoi > 0)
                        {
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Insert " + dem_dv_themmoi + " dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery(sqlinsert_log);
                        }

                        if (dem_dv_trungma != 0)
                        {
                            MessageBox.Show("Thêm mới thành công SL=" + dem_dv_themmoi + ".\nDịch vụ có mã tồn tại trong database SL=" + dem_dv_trungma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            gridControlDichVu.DataSource = null;
                            //gridControlDichVu = new DevExpress.XtraGrid.GridControl();
                            //gridViewDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
                            gridControlDichVu.DataSource = lstDichVuTrungMa;
                            btnUpdateDVOK.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Thêm mới thành công SL=" + dem_dv_themmoi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
            }
        }

        //Xuat danh muc tu DB ra Excel - xem lai...
        private void XuatDanhMucTuDBSangExCelProcess()
        {
            try
            {
                //DevExpress.XtraGrid.GridControl gridControlData = new DevExpress.XtraGrid.GridControl();
                //DevExpress.XtraGrid.Views.Grid.GridView gridViewData = new DevExpress.XtraGrid.Views.Grid.GridView();

                condb.connect();
                string export_servicepriceref = "SELECT CASE servicepriceref.servicegrouptype WHEN 1 THEN 'KHÁM BỆNH' WHEN 2 THEN 'XÉT NGHIỆM' WHEN 3 THEN 'CHẨN ĐOÁN HÌNH ẢNH' WHEN 4 THEN 'CHUYÊN KHOA'  WHEN 5 THEN 'THUỐC TRONG DANH MỤC' WHEN 6 THEN 'THUỐC NGOÀI DANH MỤC' ELSE 'KHÁC' END AS LOAI_DV, servicepriceref.servicepricecode AS MA_DV, servicepriceref.servicepricegroupcode AS MA_NHOM, servicepriceref.servicepricecodeuser AS MA_DV_USER, servicepriceref.servicepricesttuser AS MA_DV_STTTHAU, servicepriceref.servicepricenamenhandan AS TEN_VP, servicepriceref.servicepricenamebhyt AS TEN_BH,servicepriceref.servicepriceunit AS DVT, servicepriceref.servicepricefeebhyt AS GIA_BH, servicepriceref.servicepricefeenhandan AS GIA_VP, servicepriceref.servicepricefee AS GIA_YC, servicepriceref.servicepricefeenuocngoai AS GIA_NNN, servicepriceref.servicepricefee_old_date AS THOIGIAN_APDUNG, servicepriceref.servicepricefee_old_type AS THEO_NGAY_CHI_DINH, servicepriceref.pttt_hangid AS LOAI_PTTT, servicepriceref.servicelock AS KHOA, servicepriceref.bhyt_groupcode AS NHOM_BHYT, servicepriceref.ServicePriceType AS LA_NHOM FROM servicepriceref WHERE isremove is null and servicepriceref.servicepricecode <>'' and servicepriceref.servicegrouptype in (1,2,3,4) ORDER BY servicepriceref.servicegrouptype, servicepriceref.servicepricegroupcode;";
                DataView dv_dataserviceref = new DataView(condb.getDataTable(export_servicepriceref));
                //DataTable dv_ddd = new DataTable(condb.getDataTable(export_servicepriceref));
                if (dv_dataserviceref != null && dv_dataserviceref.Count > 0)
                {
                    gridControlDataSerRef.DataSource = dv_dataserviceref;
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportDataGridViewToFile(gridControlDataSerRef, gridViewDataSerRef);
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
