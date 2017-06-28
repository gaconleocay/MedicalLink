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
                string chonkieuimport = cbbChonKieu.Text.Trim();
                if (cbbChonKieu.Text.Trim() != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Hãy backup dữ liệu trước khi thực hiện.\nNhấn \"YES\" để tiếp tục, nhấn \"NO\" để quay lại backup ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
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
                        SplashScreenManager.CloseForm();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                Base.Logging.Warn(ex);
            }
        }

        #region Xu ly Cap nhat
        //Ten dich vu BHYT
        private void CapNhatProcess_TenDichVu_BHYT()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceNameBHYT = '" + item_servicep.servicepricenamebhyt + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }
                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục tên dịch vụ BHYT thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }
                MessageBox.Show("Update " + count_dv + " danh mục \"Tên dịch vụ BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }
        //Ten dich vu Vien phi
        private void CapNhatProcess_TenDichVu_VP()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET servicepricenamenhandan = '" + item_servicep.servicepricenamenhandan + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }
                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục tên dịch vụ Nhân dân thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }
                MessageBox.Show("Update " + count_dv + " danh mục \"Tên dịch vụ Nhân dân\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }
        //Ten dich vu PTTT
        private void CapNhatProcess_TenDichVu_PTTT()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET servicepricenamenuocngoai = '" + item_servicep.servicepricenamenuocngoai + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục tên dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }
                // Thông báo đã Update Tên dịch vụ
                MessageBox.Show("Update " + count_dv + " danh mục \"Tên dịch vụ\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }

        // Mã DM BYT (mã User)
        private void CapNhatProcess_MaUser()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET servicepricecodeuser = '" + item_servicep.servicepricecodeuser + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục mã user dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }
                MessageBox.Show("Update " + count_dv + " danh mục \"Mã DM BYT (mã User)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }

        //Mã STT thầu BHYT
        private void CapNhatProcess_MaSTTThau()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET servicepricesttuser = '" + item_servicep.servicepricesttuser + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục mã STT Thầu BHYT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }

                // Thông báo đã Update mã STT Thầu BHYT
                MessageBox.Show("Update " + count_dv + " danh mục \"Mã STT Thầu BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }

        //Don vi tinh
        private void CapNhatProcess_DonViTinh()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET servicepriceunit = '" + item_servicep.servicepriceunit + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục đơn vị tính dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Đơn vị tính\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }

        //Hang PTTT
        private void CapNhatProcess_HangPTTT()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET pttt_hangid = '" + item_servicep.pttt_hangid + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục hạng PTTT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Hạng PTTT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }

        //Loai PTTT
        private void CapNhatProcess_LoaiPTTT()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET pttt_loaiid = '" + item_servicep.pttt_loaiid + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục loại PTTT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Loại PTTT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
            }
        }

        //Khoa dich vu
        private void CapNhatProcess_KhoaDichVu()
        {
            int count_dv = 0;
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                foreach (var item_servicep in lstServicePriceRef)
                {
                    try
                    {
                        string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                        DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                        if (dv_kt.Count > 0)
                        {
                            string sqlupdatetendv = "UPDATE ServicePriceRef SET servicelock = '" + item_servicep.servicelock + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                            count_dv += dv_kt.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                        Base.Logging.Warn(ex);
                    }
                }

                //lưu lại log
                if (count_dv > 0)
                {
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " danh mục khóa dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                }

                // Thông báo đã Update Đơn vị tính
                MessageBox.Show("Update " + count_dv + " danh mục \"Khóa dịch vụ\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Base.Logging.Error(ex);
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
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                string sqlupdatetendv = "UPDATE ServicePriceRef SET servicepricefeenhandan = '" + item_servicep.servicepricefeenhandan + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá nhân dân của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá Viện phí\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan_OLD = ServicePriceFeeNhanDan WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sql_chuyen_giaNhanDan);
                                // Update Giá Nhân dân
                                string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + item_servicep.servicepricefeenhandan + "', ServicePriceFee_OLD_DATE = '" + item_servicep.servicepricefee_old_date + "', ServicePriceFee_OLD_Type = '" + item_servicep.servicepricefee_old_type + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatedvt);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá nhân dân của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá Viện phí\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
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
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceFeeBHYT = '" + item_servicep.servicepricefeebhyt + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá BHYT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }

                    // Thông báo đã Update giá BHYT
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFeeBHYT_OLD = ServicePriceFeeBHYT WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sql_chuyen_giaNhanDan);
                                // Update Giá BHYT
                                string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeBHYT = '" + item_servicep.servicepricefeebhyt + "', ServicePriceFee_OLD_DATE = '" + item_servicep.servicepricefee_old_date + "', ServicePriceFee_OLD_Type = '" + item_servicep.servicepricefee_old_type + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatedvt);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá BHYT dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }

                    // Thông báo đã Update giá nhân dân
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá BHYT\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
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
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceFee = '" + item_servicep.servicepricefee + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá yêu cầu của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá Yêu cầu\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFee_OLD = ServicePriceFee WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sql_chuyen_giaNhanDan);
                                // Update Giá Yeu cau
                                string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFee = '" + item_servicep.servicepricefee + "', ServicePriceFee_OLD_DATE = '" + item_servicep.servicepricefee_old_date + "', ServicePriceFee_OLD_Type = '" + item_servicep.servicepricefee_old_type + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatedvt);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá yêu cầu của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }

                    // Thông báo đã Update giá yeu cau
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá Yêu cầu\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
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
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                string sqlupdatetendv = "UPDATE ServicePriceRef SET ServicePriceFeeNuocNgoai = '" + item_servicep.servicepricefeenuocngoai + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatetendv);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " giá người nước ngoài của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    MessageBox.Show("Update " + count_dv + " danh mục \"giá Người nước ngoài\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFeeNuocNgoai_OLD = ServicePriceFeeNuocNgoai WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sql_chuyen_giaNhanDan);
                                // Update Giá nguoi nuoc ngoai
                                string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeNuocNgoai = '" + item_servicep.servicepricefeenuocngoai + "', ServicePriceFee_OLD_DATE = '" + item_servicep.servicepricefee_old_date + "', ServicePriceFee_OLD_Type = '" + item_servicep.servicepricefee_old_type + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatedvt);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Backup và Update " + count_dv + " giá người nước ngoài của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    MessageBox.Show("Backup và Update " + count_dv + " danh mục \"giá Người nước ngoài\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
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
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                string sqlupdategiaNNN = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + item_servicep.servicepricefeenhandan + "', ServicePriceFeeBHYT = '" + item_servicep.servicepricefeebhyt + "', ServicePriceFee = '" + item_servicep.servicepricefee + "', ServicePriceFeeNuocNgoai = '" + item_servicep.servicepricefeenuocngoai + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdategiaNNN);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " 4 loại giá của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    MessageBox.Show("Update " + count_dv + " danh mục \"4 loại giá (VP+BH+YC+NN)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
                }
            }
            else if (cbbChonLoai.Text.Trim() == "Sửa giá mới")
            {
                int count_dv = 0;
                try
                {
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                            DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                            if (dv_kt.Count > 0)
                            {
                                // Thực hiện việc chuyển từ cột giá sang cột giá cũ
                                string sql_chuyen_giaNhanDan = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan_OLD = ServicePriceFeeNhanDan, ServicePriceFeeBHYT_OLD = ServicePriceFeeBHYT, ServicePriceFee_OLD = ServicePriceFee, ServicePriceFeeNuocNgoai_OLD = ServicePriceFeeNuocNgoai WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sql_chuyen_giaNhanDan);
                                // Update 4 loai gia
                                string sqlupdatedvt = "UPDATE ServicePriceRef SET ServicePriceFeeNhanDan = '" + item_servicep.servicepricefeenhandan + "', ServicePriceFeeBHYT = '" + item_servicep.servicepricefeebhyt + "', ServicePriceFee = '" + item_servicep.servicepricefee + "', ServicePriceFeeNuocNgoai = '" + item_servicep.servicepricefeenuocngoai + "', ServicePriceFee_OLD_DATE = '" + item_servicep.servicepricefee_old_date + "', ServicePriceFee_OLD_Type = '" + item_servicep.servicepricefee_old_type + "' WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                condb.ExecuteNonQuery_HIS(sqlupdatedvt);
                                count_dv += dv_kt.Count;
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                            Base.Logging.Warn(ex);
                        }
                    }
                    //lưu lại log
                    if (count_dv > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + count_dv + " 4 loại giá của dịch vụ thành công','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    MessageBox.Show("Update " + count_dv + " danh mục \"4 loại giá (VP+BH+YC+NN)\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra." + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Base.Logging.Error(ex);
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
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int dem_dv_themmoi = 0;
                int dem_dv_trungma = 0;

                DialogResult dialogResult = MessageBox.Show("Hãy backup trước khi thực hiện.\nNhấn \"YES\" để tiếp tục, nhấn \"NO\" để quay lại backup ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    foreach (var item_servicep in lstServicePriceRef)
                    {
                        try
                        {
                            if (item_servicep.servicepricecode != "" && item_servicep.servicegrouptype != 0)
                            {
                                string sql_kt = "SELECT ServicePriceRefID, ServicePriceCode FROM ServicePriceRef WHERE ServicePriceCode= '" + item_servicep.servicepricecode + "' ;";
                                DataView dv_kt = new DataView(condb.GetDataTable_HIS(sql_kt));
                                if (dv_kt.Count > 0)
                                {
                                    //Update
                                }
                                else if (dv_kt.Count == 0)
                                {
                                    //TODO Them moi
                                    string sql_insertserviceref = "INSERT INTO ServicePriceRef ( ServicePriceRefID_Master, ServicePriceGroupCode, ServicePriceCode, ServicePriceCodeUser, ServicePriceSTTUser, ServicePriceCode_NG, BHYT_GroupCode, Report_GroupCode, CK_GroupCode, Report_TKCode, ServicePriceName, ServicePriceNameNhanDan, ServicePriceNameBHYT, ServicePriceNameNuocNgoai, ServicePriceFee, ServicePriceFeeNhanDan, ServicePriceFeeBHYT, ServicePriceFeeNuocNgoai, ListDepartmentPhongThucHien, ListDepartmentPhongThucHienKhamGoi, ServicePriceFee_OLD, ServicePriceFeeNhanDan_OLD, ServicePriceFeeBHYT_OLD, ServicePriceFeeNuocNgoai_OLD, ServicePriceFee_OLD_Type, KhongChuyenDoiTuongHaoPhi, LuonChuyenDoiTuongHaoPhi, CDHA_SoLuongThuoc, CDHA_SoLuongVatTu, PTTT_DinhMucVTTH, PTTT_DinhMucThuoc, TyLeLaiChiDinh, TyLeLaiThucHien, TinhToanLaiGiaDVKTC,  ServicePriceUnit, LayMauPhongThucHien, ServicePriceType, ServiceGroupType, ServicePricePrintOrder, ServiceLock, ServicePriceBHYTQuyDoi, ServicePriceBHYTQuyDoi_TT, ServicePriceBHYTDinhMuc, PTTT_HangID)  VALUES( '0', '" + item_servicep.servicepricegroupcode + "', '" + item_servicep.servicepricecode + "', '" + item_servicep.servicepricecodeuser + "', '" + item_servicep.servicepricesttuser + "', '', '" + item_servicep.bhyt_groupcode + "', '" + item_servicep.report_groupcode + "', '', '" + item_servicep.report_tkcode + "', '" + item_servicep.servicepricename + "', '" + item_servicep.servicepricenamebhyt + "', '" + item_servicep.servicepricenamebhyt + "', '" + item_servicep.servicepricenamebhyt + "', '" + item_servicep.servicepricefee + "', '" + item_servicep.servicepricefeenhandan + "', '" + item_servicep.servicepricefeebhyt + "', '" + item_servicep.servicepricefeenuocngoai + "', '', '', '', '', '', '', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '" + item_servicep.servicepriceunit + "', '0', '" + item_servicep.servicepricetype + "', '" + item_servicep.servicegrouptype + "', '0', '0', '', '', '', '" + item_servicep.pttt_hangid + "')";
                                    condb.ExecuteNonQuery_HIS(sql_insertserviceref);

                                    string sql_insertketqua = "INSERT INTO serviceref4price(servicepricecode, servicecode) VALUES ('" + item_servicep.servicepricecode + "', '" + item_servicep.servicecode + "') ;";
                                    condb.ExecuteNonQuery_HIS(sql_insertketqua);
                                    dem_dv_themmoi += 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    //lưu lại log
                    if (dem_dv_themmoi > 0)
                    {
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Insert " + dem_dv_themmoi + " dịch vụ thành công. Update thành công=" + dem_dv_trungma + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    SplashScreenManager.CloseForm();
                    MessageBox.Show("Thêm mới thành công SL=" + dem_dv_themmoi + ".\nUpdate thành công=" + dem_dv_trungma, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                Base.Logging.Warn(ex);
            }
        }


    }
}
