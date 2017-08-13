using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucQuanLyNguoiDung : UserControl
    {
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string currentUserCode;
        private List<MedicalLink.ClassCommon.classPermission> lstPer { get; set; }
        private List<MedicalLink.ClassCommon.classUserDepartment> lstUserDepartment { get; set; }
        private List<MedicalLink.ClassCommon.classPermission> lstPerBaoCao { get; set; }
        private List<MedicalLink.ClassCommon.classUserMedicineStore> lstUserMedicineStore { get; set; }
        private List<MedicalLink.ClassCommon.classUserMedicinePhongLuu> lstUserMedicinePhongLuu { get; set; }

        public ucQuanLyNguoiDung()
        {
            InitializeComponent();
        }

        #region Load
        private void ucQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl(false);
                LoadDanhSachNguoiDung();
                LoadDanhSachChucNang();
                LoadDanhSachKhoaPhong();
                LoadDanhSachBaoCao();
                LoadDanhSachKhoThuoc();
                LoadDanhSachPhongLuu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachNguoiDung()
        {
            try
            {
                string sql = "select usercode, username, userpassword, case usergnhom when '0' then 'Admin' when '1' then 'Quản trị hệ thống' when 2 then 'Nhân viên' end as usergnhom from tools_tbluser where usergnhom in (1,2) order by usercode";
                DataView dv = new DataView(condb.GetDataTable_MeL(sql));

                if (dv.Count > 0)
                {
                    //Giải mã hiển thị lên Gridview
                    for (int i = 0; i < dv.Count; i++)
                    {
                        string usercode_de = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[i]["usercode"].ToString(), true);
                        string username_de = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[i]["username"].ToString(), true);
                        dv[i]["usercode"] = usercode_de;
                        dv[i]["username"] = username_de;
                    }
                    gridControlDSUser.DataSource = dv;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachChucNang()
        {
            try
            {
                lstPer = new List<ClassCommon.classPermission>();
                lstPer = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype != 10).ToList();
                gridControlChucNang.DataSource = lstPer;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachKhoaPhong()
        {
            try
            {
                string sql = "SELECT degp.departmentgroupid, degp.departmentgroupname, de.departmentid, de.departmentcode, de.departmentname, de.departmenttype, (case de.departmenttype when 2 then 'Phòng khám' when 3 then 'Buồng điều trị' when 6 then 'Phòng xét nghiệm' when 7 then 'Phòng CĐHA' when 9 then 'BĐT ngoại trú' else '' end) as departmenttypename FROM department de inner join departmentgroup degp on de.departmentgroupid=degp.departmentgroupid WHERE degp.departmentgrouptype in (1,4,9,10,11) and de.departmenttype in (2,3,6,7,9) ORDER BY degp.departmentgroupid,de.departmenttype, de.departmentname; ";
                DataView dataPhong = new DataView(condb.GetDataTable_HIS(sql));
                lstUserDepartment = new List<ClassCommon.classUserDepartment>();
                for (int i = 0; i < dataPhong.Count; i++)
                {
                    ClassCommon.classUserDepartment userDepartment = new ClassCommon.classUserDepartment();
                    userDepartment.departmentcheck = false;
                    userDepartment.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt32(dataPhong[i]["departmentgroupid"].ToString());
                    userDepartment.departmentgroupname = dataPhong[i]["departmentgroupname"].ToString();
                    userDepartment.departmentid = Utilities.Util_TypeConvertParse.ToInt32(dataPhong[i]["departmentid"].ToString());
                    userDepartment.departmentcode = dataPhong[i]["departmentcode"].ToString();
                    userDepartment.departmentname = dataPhong[i]["departmentname"].ToString();
                    userDepartment.departmenttype = Utilities.Util_TypeConvertParse.ToInt32(dataPhong[i]["departmenttype"].ToString());
                    userDepartment.departmenttypename = dataPhong[i]["departmenttypename"].ToString();
                    lstUserDepartment.Add(userDepartment);
                }
                gridControlKhoaPhong.DataSource = lstUserDepartment;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachBaoCao()
        {
            try
            {
                lstPerBaoCao = new List<ClassCommon.classPermission>();
                lstPerBaoCao = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 10).ToList();
                gridControlBaoCao.DataSource = lstPerBaoCao;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachKhoThuoc()
        {
            try
            {
                string sql = "SELECT ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename, ms.medicinestoretype, (case ms.medicinestoretype when 1 then 'Kho tổng' when 2 then 'Kho ngoại trú' when 3 then 'Kho nội trú' when 4 then 'Nhà thuốc' when 7 then 'Kho vật tư' end) as medicinestoretypename FROM medicine_store ms WHERE ms.medicinestoretype in (1,2,3,4,7) ORDER BY ms.medicinestoretype,ms.medicinestorename; ";
                DataView dataKhoThuoc = new DataView(condb.GetDataTable_HIS(sql));
                lstUserMedicineStore = new List<ClassCommon.classUserMedicineStore>();
                for (int i = 0; i < dataKhoThuoc.Count; i++)
                {
                    ClassCommon.classUserMedicineStore userMedicineStore = new ClassCommon.classUserMedicineStore();
                    userMedicineStore.MedicineStoreCheck = false;
                    userMedicineStore.MedicineStoreId = Utilities.Util_TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoreid"].ToString());
                    userMedicineStore.MedicineStoreCode = dataKhoThuoc[i]["medicinestorecode"].ToString();
                    userMedicineStore.MedicineStoreName = dataKhoThuoc[i]["medicinestorename"].ToString();
                    userMedicineStore.MedicineStoreType = Utilities.Util_TypeConvertParse.ToInt32(dataKhoThuoc[i]["medicinestoretype"].ToString());
                    userMedicineStore.MedicineStoreTypeName = dataKhoThuoc[i]["medicinestoretypename"].ToString();

                    lstUserMedicineStore.Add(userMedicineStore);
                }
                gridControlKhoThuoc.DataSource = lstUserMedicineStore;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachPhongLuu()
        {
            try
            {
                string sql = "SELECT pl.medicinephongluuid, pl.medicinephongluucode, pl.medicinephongluuname, ms.medicinestoreid, ms.medicinestorecode, ms.medicinestorename from medicinephongluu pl inner join medicine_store ms on pl.medicinestoreid=ms.medicinestoreid where pl.medicinephongluucode<>'' and pl.medicinephongluuname<>'' order by ms.medicinestorename, pl.medicinephongluuname; ";
                DataView dataPhongLuu = new DataView(condb.GetDataTable_HIS(sql));
                lstUserMedicinePhongLuu = new List<ClassCommon.classUserMedicinePhongLuu>();
                for (int i = 0; i < dataPhongLuu.Count; i++)
                {
                    ClassCommon.classUserMedicinePhongLuu userMedicinePhongLuu = new ClassCommon.classUserMedicinePhongLuu();
                    userMedicinePhongLuu.MedicinePhongLuuCheck = false;
                    userMedicinePhongLuu.MedicinePhongLuuId = Utilities.Util_TypeConvertParse.ToInt32(dataPhongLuu[i]["medicinephongluuid"].ToString());
                    userMedicinePhongLuu.MedicinePhongLuuCode = dataPhongLuu[i]["medicinephongluucode"].ToString();
                    userMedicinePhongLuu.MedicinePhongLuuName = dataPhongLuu[i]["medicinephongluuname"].ToString();
                    userMedicinePhongLuu.MedicineStoreId = Utilities.Util_TypeConvertParse.ToInt32(dataPhongLuu[i]["medicinestoreid"].ToString());
                    userMedicinePhongLuu.MedicineStoreCode = dataPhongLuu[i]["medicinestorecode"].ToString();
                    userMedicinePhongLuu.MedicineStoreName = dataPhongLuu[i]["medicinestorename"].ToString();

                    lstUserMedicinePhongLuu.Add(userMedicinePhongLuu);
                }
                gridControlPhongLuu.DataSource = lstUserMedicinePhongLuu;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        private void EnableAndDisableControl(bool value)
        {
            try
            {
                btnUserOK.Enabled = value;
                txtUserCode.Enabled = value;
                txtUsername.Enabled = value;
                txtUserPassword.Enabled = value;
                cbbUserNhom.Enabled = value;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridControlDSUser_Click(object sender, EventArgs e)
        {
            var rowHandle = gridViewDSUser.FocusedRowHandle;
            try
            {
                EnableAndDisableControl(true);
                txtUserCode.ReadOnly = true;
                currentUserCode = gridViewDSUser.GetRowCellValue(rowHandle, "usercode").ToString();
                txtUserCode.Text = currentUserCode;
                txtUsername.Text = gridViewDSUser.GetRowCellValue(rowHandle, "username").ToString(); ;
                txtUserPassword.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(gridViewDSUser.GetRowCellValue(rowHandle, "userpassword").ToString(), true);
                cbbUserNhom.Text = gridViewDSUser.GetRowCellValue(rowHandle, "usergnhom").ToString();

                gridControlChucNang.DataSource = null;
                gridControlKhoaPhong.DataSource = null;
                gridControlBaoCao.DataSource = null;
                gridControlKhoThuoc.DataSource = null;
                gridControlPhongLuu.DataSource = null;

                LoadDanhSachChucNang();
                LoadDanhSachKhoaPhong();
                LoadDanhSachBaoCao();
                LoadDanhSachKhoThuoc();
                LoadDanhSachPhongLuu();

                LoadPhanQuyenChucNang();
                LoadPhanQuyenKhoaPhong();
                LoadPhanQuyenBaoCao();
                LoadPhanQuyenKhoThuoc();
                LoadPhanQuyenPhongLuu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenChucNang()
        {
            try
            {
                gridControlChucNang.DataSource = null;
                string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "';";
                DataView dv = new DataView(condb.GetDataTable_MeL(sqlquerry_per));
                //Load dữ liệu list phân quyền + tích quyền của use đang chọn lấy trong DB
                if (dv != null && dv.Count > 0)
                {
                    for (int i = 0; i < lstPer.Count; i++)
                    {
                        for (int j = 0; j < dv.Count; j++)
                        {
                            if (lstPer[i].permissioncode.ToString() == EncryptAndDecrypt.Decrypt(dv[j]["permissioncode"].ToString(), true))
                            {
                                lstPer[i].permissioncheck = Convert.ToBoolean(dv[j]["permissioncheck"]);
                            }
                        }
                    }
                }
                gridControlChucNang.DataSource = lstPer;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenKhoaPhong()
        {
            try
            {
                gridControlKhoaPhong.DataSource = null;
                string sqlquerry_khoaphong = "SELECT userdepgid,departmentgroupid,departmentid,departmenttype,usercode FROM tools_tbluser_departmentgroup WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "';";
                DataView dv_khoaphong = new DataView(condb.GetDataTable_MeL(sqlquerry_khoaphong));
                if (dv_khoaphong != null && dv_khoaphong.Count > 0)
                {
                    for (int i = 0; i < lstUserDepartment.Count; i++)
                    {
                        for (int j = 0; j < dv_khoaphong.Count; j++)
                        {
                            if (lstUserDepartment[i].departmentid.ToString() == dv_khoaphong[j]["departmentid"].ToString())
                            {
                                lstUserDepartment[i].departmentcheck = true;
                            }
                        }
                    }
                }
                gridControlKhoaPhong.DataSource = lstUserDepartment;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenBaoCao()
        {
            try
            {
                gridControlBaoCao.DataSource = null;
                string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "' and userpermissionnote='BAOCAO';";
                DataView dv = new DataView(condb.GetDataTable_MeL(sqlquerry_per));
                //Load dữ liệu list phân quyền + tích quyền của use đang chọn lấy trong DB
                if (dv != null && dv.Count > 0)
                {
                    for (int i = 0; i < lstPerBaoCao.Count; i++)
                    {
                        for (int j = 0; j < dv.Count; j++)
                        {
                            if (lstPerBaoCao[i].permissioncode.ToString() == EncryptAndDecrypt.Decrypt(dv[j]["permissioncode"].ToString(), true))
                            {
                                lstPerBaoCao[i].permissioncheck = Convert.ToBoolean(dv[j]["permissioncheck"]);
                            }
                        }
                    }
                }
                gridControlBaoCao.DataSource = lstPerBaoCao;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenKhoThuoc()
        {
            try
            {
                gridControlKhoThuoc.DataSource = null;
                string sqlquerry_khoaphong = "SELECT usermestid,medicinestoreid,medicinestoretype,usercode FROM tools_tbluser_medicinestore WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "';";
                DataView dv_khothuoc = new DataView(condb.GetDataTable_MeL(sqlquerry_khoaphong));
                if (dv_khothuoc != null && dv_khothuoc.Count > 0)
                {
                    for (int i = 0; i < lstUserMedicineStore.Count; i++)
                    {
                        for (int j = 0; j < dv_khothuoc.Count; j++)
                        {
                            if (lstUserMedicineStore[i].MedicineStoreId.ToString() == dv_khothuoc[j]["medicinestoreid"].ToString())
                            {
                                lstUserMedicineStore[i].MedicineStoreCheck = true;
                            }
                        }
                    }
                }
                gridControlKhoThuoc.DataSource = lstUserMedicineStore;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPhanQuyenPhongLuu()
        {
            try
            {
                gridControlPhongLuu.DataSource = null;
                string sqlquerry_phongluu = "SELECT userphongluutid,medicinephongluuid,medicinestoreid,usercode FROM tools_tbluser_medicinephongluu WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode, true).ToString() + "';";
                DataView dv_phongluu = new DataView(condb.GetDataTable_MeL(sqlquerry_phongluu));
                if (dv_phongluu != null && dv_phongluu.Count > 0)
                {
                    for (int i = 0; i < lstUserMedicinePhongLuu.Count; i++)
                    {
                        for (int j = 0; j < dv_phongluu.Count; j++)
                        {
                            if (lstUserMedicinePhongLuu[i].MedicinePhongLuuId.ToString() == dv_phongluu[j]["medicinephongluuid"].ToString())
                            {
                                lstUserMedicinePhongLuu[i].MedicinePhongLuuCheck = true;
                            }
                        }
                    }
                }
                gridControlPhongLuu.DataSource = lstUserMedicinePhongLuu;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnUserThem_Click(object sender, EventArgs e)
        {
            currentUserCode = null;

            txtUserCode.ResetText();
            txtUsername.ResetText();
            txtUserPassword.ResetText();
            EnableAndDisableControl(true);
            cbbUserNhom.Text = "Nhân viên";
            txtUserCode.ReadOnly = false;
            txtUserCode.Focus();
            btnUserOK.Enabled = true;

            gridControlChucNang.DataSource = null;
            gridControlKhoaPhong.DataSource = null;
            gridControlBaoCao.DataSource = null;
            gridControlKhoThuoc.DataSource = null;
            gridControlPhongLuu.DataSource = null;

            LoadDanhSachChucNang();
            LoadDanhSachKhoaPhong();
            LoadDanhSachBaoCao();
            LoadDanhSachKhoThuoc();
            LoadDanhSachPhongLuu();
        }

        #region Tạo sự kiện khi kích OK
        private void btnUserOK_Click(object sender, EventArgs e)
        {
            // Mã hóa tài khoản
            string en_txtUsercode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUserCode.Text.Trim().ToLower(), true);
            string en_txtUsername = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
            string en_txtUserPassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUserPassword.Text.Trim(), true);
            try
            {
                if (currentUserCode == null)//them moi
                {
                    if (CheckAccTonTai(en_txtUsercode))
                    {
                        CreateNewUser(en_txtUsercode, en_txtUsername, en_txtUserPassword);
                        CreateNewUserPermission(en_txtUsercode);
                        CreateNewUserDepartment(en_txtUsercode);
                        CreateNewUserBaoCao(en_txtUsercode);
                        CreateNewUserMedicineStore(en_txtUsercode);
                        CreateNewUserMedicinePhongLuu(en_txtUsercode);
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                        frmthongbao.Show();
                        //LoadDanhSachNguoiDung();
                        ucQuanLyNguoiDung_Load(null, null);
                    }
                }
                else //Update 
                {
                    UpdateUser(en_txtUsercode, en_txtUsername, en_txtUserPassword);
                    UpdateUserPermission(en_txtUsercode);
                    UpdateUserDepartment(en_txtUsercode);
                    UpdateUserBaoCao(en_txtUsercode);
                    UpdateUserMedicineStore(en_txtUsercode);
                    UpdateUserMedicinePhongLuu(en_txtUsercode);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private bool CheckAccTonTai(string usercode)
        {
            bool result = true;
            try
            {
                string sqlcheckUserCode = "select usercode from tools_tbluser where usercode='" + usercode + "';";
                DataTable datacheck = condb.GetDataTable_MeL(sqlcheckUserCode);
                if (datacheck != null && datacheck.Rows.Count > 0)
                {
                    result = false;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.TEN_TAI_KHOA_DA_TON_TAI);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return result;
        }

        private void CreateNewUser(string en_txtUserID, string en_txtUsername, string en_txtUserPassword)
        {
            try
            {
                string sqlinsert_user = "";
                // usergnhom=1: Quan tri he thong
                // usergnhom=2: User
                if (cbbUserNhom.Text == "Quản trị hệ thống")
                {
                    sqlinsert_user = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + en_txtUserID + "','" + en_txtUsername + "','" + en_txtUserPassword + "','0','1','');";
                }
                else
                {
                    sqlinsert_user = "INSERT INTO tools_tbluser(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + en_txtUserID + "','" + en_txtUsername + "','" + en_txtUserPassword + "','0','2','Nhân viên');";
                }
                condb.ExecuteNonQuery_MeL(sqlinsert_user);
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewUserPermission(string en_txtUserID)
        {
            try
            {
                string sqlinsert_per = "";
                for (int i = 0; i < lstPer.Count; i++)
                {
                    sqlinsert_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode.ToString(), true);
                    string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissionname.ToString(), true);
                    if (lstPer[i].permissioncheck == true)
                    {
                        sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', '');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_per);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewUserDepartment(string en_txtUserID)
        {
            try
            {
                string sqlinsert_userdepartment = "";
                for (int i = 0; i < lstUserDepartment.Count; i++)
                {
                    sqlinsert_userdepartment = "";
                    if (lstUserDepartment[i].departmentcheck == true)
                    {
                        sqlinsert_userdepartment = "INSERT INTO tools_tbluser_departmentgroup(departmentgroupid, departmentid, departmenttype, usercode, userdepgidnote) VALUES ('" + lstUserDepartment[i].departmentgroupid + "','" + lstUserDepartment[i].departmentid + "','" + lstUserDepartment[i].departmenttype + "','" + en_txtUserID + "','');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_userdepartment);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewUserBaoCao(string en_txtUserID)
        {
            try
            {
                string sqlinsert_per = "";
                for (int i = 0; i < lstPerBaoCao.Count; i++)
                {
                    sqlinsert_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissioncode.ToString(), true);
                    string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissionname.ToString(), true);
                    if (lstPerBaoCao[i].permissioncheck == true)
                    {
                        sqlinsert_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', 'BAOCAO');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_per);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewUserMedicineStore(string en_txtUserID)
        {
            try
            {
                string sqlinsert_usermedicinestore = "";
                for (int i = 0; i < lstUserMedicineStore.Count; i++)
                {
                    sqlinsert_usermedicinestore = "";
                    if (lstUserMedicineStore[i].MedicineStoreCheck == true)
                    {
                        sqlinsert_usermedicinestore = "INSERT INTO tools_tbluser_medicinestore(medicinestoreid, medicinestoretype, usercode, userdepgidnote) VALUES ('" + lstUserMedicineStore[i].MedicineStoreId + "','" + lstUserMedicineStore[i].MedicineStoreType + "','" + en_txtUserID + "','');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_usermedicinestore);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void CreateNewUserMedicinePhongLuu(string en_txtUserID)
        {
            try
            {
                string sqlinsert_usermedicinephongluu = "";
                for (int i = 0; i < lstUserMedicinePhongLuu.Count; i++)
                {
                    sqlinsert_usermedicinephongluu = "";
                    if (lstUserMedicinePhongLuu[i].MedicinePhongLuuCheck == true)
                    {
                        sqlinsert_usermedicinephongluu = "INSERT INTO tools_tbluser_medicinephongluu(medicinephongluuid, medicinestoreid, usercode, userdepgidnote) VALUES ('" + lstUserMedicinePhongLuu[i].MedicinePhongLuuId + "','" + lstUserMedicinePhongLuu[i].MedicineStoreId + "','" + en_txtUserID + "','');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_usermedicinephongluu);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }

        private void UpdateUser(string en_txtUserID, string en_txtUsername, string en_txtUserPassword)
        {
            try
            {
                string sqlupdate_user = "";
                if (cbbUserNhom.Text == "Quản trị hệ thống")
                {
                    sqlupdate_user = "UPDATE tools_tbluser SET usercode='" + en_txtUserID + "', username='" + en_txtUsername + "', userpassword='" + en_txtUserPassword + "', userstatus='0', usergnhom='1', usernote='' WHERE usercode='" + en_txtUserID + "';";
                }
                else
                {
                    sqlupdate_user = "UPDATE tools_tbluser SET usercode='" + en_txtUserID + "', username='" + en_txtUsername + "', userpassword='" + en_txtUserPassword + "', userstatus='0', usergnhom='2', usernote='Nhân viên' WHERE usercode='" + en_txtUserID + "';";
                }
                condb.ExecuteNonQuery_MeL(sqlupdate_user);
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdateUserPermission(string en_txtUserID)
        {
            try
            {
                string sqlupdate_per = "";
                for (int i = 0; i < lstPer.Count; i++)
                {
                    sqlupdate_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissioncode, true);
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                    DataView dvkt = new DataView(condb.GetDataTable_MeL(sqlkiemtratontai));
                    if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstPer[i].permissioncheck == false)
                        {
                            sqlupdate_per = "DELETE FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                            condb.ExecuteNonQuery_MeL(sqlupdate_per);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstPer[i].permissioncheck == true)
                        {
                            string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPer[i].permissionname.ToString(), true);
                            sqlupdate_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', '');";
                            condb.ExecuteNonQuery_MeL(sqlupdate_per);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdateUserDepartment(string en_txtUserID)
        {
            try
            {
                string sqlupdate_userdepartment = "";
                for (int i = 0; i < lstUserDepartment.Count; i++)
                {
                    sqlupdate_userdepartment = "";
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_departmentgroup WHERE usercode='" + en_txtUserID + "' and departmentid='" + lstUserDepartment[i].departmentid + "' ;";
                    DataView dvkt = new DataView(condb.GetDataTable_MeL(sqlkiemtratontai));
                    if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstUserDepartment[i].departmentcheck == false) //xoa
                        {
                            sqlupdate_userdepartment = "DELETE FROM tools_tbluser_departmentgroup WHERE usercode='" + en_txtUserID + "' and departmentid='" + lstUserDepartment[i].departmentid + "' ;";
                            condb.ExecuteNonQuery_MeL(sqlupdate_userdepartment);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstUserDepartment[i].departmentcheck == true)
                        {
                            sqlupdate_userdepartment = "INSERT INTO tools_tbluser_departmentgroup(departmentgroupid, departmentid, departmenttype, usercode, userdepgidnote) VALUES ('" + lstUserDepartment[i].departmentgroupid + "','" + lstUserDepartment[i].departmentid + "','" + lstUserDepartment[i].departmenttype + "','" + en_txtUserID + "','');";
                            condb.ExecuteNonQuery_MeL(sqlupdate_userdepartment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdateUserBaoCao(string en_txtUserID)
        {
            try
            {
                string sqlupdate_per = "";
                for (int i = 0; i < lstPerBaoCao.Count; i++)
                {
                    sqlupdate_per = "";
                    string en_permissioncode = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissioncode, true);
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                    DataView dvkt = new DataView(condb.GetDataTable_MeL(sqlkiemtratontai));
                    if (dvkt.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstPerBaoCao[i].permissioncheck == false)
                        {
                            sqlupdate_per = "DELETE FROM tools_tbluser_permission WHERE usercode='" + en_txtUserID + "' and permissioncode='" + en_permissioncode + "' ;";
                            condb.ExecuteNonQuery_MeL(sqlupdate_per);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstPerBaoCao[i].permissioncheck == true)
                        {
                            string en_permissionname = MedicalLink.Base.EncryptAndDecrypt.Encrypt(lstPerBaoCao[i].permissionname.ToString(), true);
                            sqlupdate_per = "INSERT INTO tools_tbluser_permission(permissioncode, permissionname, usercode, permissioncheck, userpermissionnote) VALUES ('" + en_permissioncode + "', '" + en_permissionname + "', '" + en_txtUserID + "', 'true', 'BAOCAO');";
                            condb.ExecuteNonQuery_MeL(sqlupdate_per);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdateUserMedicineStore(string en_txtUserID)
        {
            try
            {
                string sqlupdate_usermedicinestore = "";
                for (int i = 0; i < lstUserMedicineStore.Count; i++)
                {
                    sqlupdate_usermedicinestore = "";
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_medicinestore WHERE usercode='" + en_txtUserID + "' and medicinestoreid='" + lstUserMedicineStore[i].MedicineStoreId + "' ;";
                    DataView dvkt_medi = new DataView(condb.GetDataTable_MeL(sqlkiemtratontai));
                    if (dvkt_medi.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstUserMedicineStore[i].MedicineStoreCheck == false) //xoa
                        {
                            sqlupdate_usermedicinestore = "DELETE FROM tools_tbluser_medicinestore WHERE usercode='" + en_txtUserID + "' and medicinestoreid='" + lstUserMedicineStore[i].MedicineStoreId + "' ;";
                            condb.ExecuteNonQuery_MeL(sqlupdate_usermedicinestore);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstUserMedicineStore[i].MedicineStoreCheck == true)
                        {
                            sqlupdate_usermedicinestore = "INSERT INTO tools_tbluser_medicinestore(medicinestoreid, medicinestoretype, usercode, userdepgidnote) VALUES ('" + lstUserMedicineStore[i].MedicineStoreId + "','" + lstUserMedicineStore[i].MedicineStoreType + "','" + en_txtUserID + "','');";
                            condb.ExecuteNonQuery_MeL(sqlupdate_usermedicinestore);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }
        private void UpdateUserMedicinePhongLuu(string en_txtUserID)
        {
            try
            {
                string sqlupdate_usermedicinephongluu = "";
                for (int i = 0; i < lstUserMedicinePhongLuu.Count; i++)
                {
                    sqlupdate_usermedicinephongluu = "";
                    string sqlkiemtratontai = "SELECT * FROM tools_tbluser_medicinephongluu WHERE usercode='" + en_txtUserID + "' and medicinephongluuid='" + lstUserMedicinePhongLuu[i].MedicinePhongLuuId + "' ;";
                    DataView dvkt_medi = new DataView(condb.GetDataTable_MeL(sqlkiemtratontai));
                    if (dvkt_medi.Count > 0) //Nếu có quyền đó rồi thì Update
                    {
                        if (lstUserMedicinePhongLuu[i].MedicinePhongLuuCheck == false) //xoa
                        {
                            sqlupdate_usermedicinephongluu = "DELETE FROM tools_tbluser_medicinephongluu WHERE usercode='" + en_txtUserID + "' and medicinephongluuid='" + lstUserMedicinePhongLuu[i].MedicinePhongLuuId + "' ;";
                            condb.ExecuteNonQuery_MeL(sqlupdate_usermedicinephongluu);
                        }
                    }
                    else //nếu không có quyền đó thì Insert
                    {
                        if (lstUserMedicinePhongLuu[i].MedicinePhongLuuCheck == true)
                        {
                            sqlupdate_usermedicinephongluu = "INSERT INTO tools_tbluser_medicinephongluu(medicinephongluuid, medicinestoreid, usercode, userdepgidnote) VALUES ('" + lstUserMedicinePhongLuu[i].MedicinePhongLuuId + "','" + lstUserMedicinePhongLuu[i].MedicineStoreId + "','" + en_txtUserID + "','');";
                            condb.ExecuteNonQuery_MeL(sqlupdate_usermedicinephongluu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
            }
        }

        #endregion

        #region Custome
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUsername.Focus();
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUserPassword.Focus();
            }
        }

        private void txtUserPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnUserOK.PerformClick();
        }

        #endregion
        //Tạo Menu chức năng xóa người dùng
        private void gridViewDSUser_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Row)
            {
                e.Menu.Items.Clear();
                DXMenuItem itemXoaNguoiDung = new DXMenuItem("Xóa tài khoản");
                itemXoaNguoiDung.Image = imMenu.Images["Xoa.png"];
                itemXoaNguoiDung.Click += new EventHandler(itemXoaNguoiDung_Click);
                e.Menu.Items.Add(itemXoaNguoiDung);
            }
        }

        void itemXoaNguoiDung_Click(object sender, EventArgs e)
        {
            if (currentUserCode == null)
            {
                return;
            }
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản: " + currentUserCode + " không?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    string sqlxoatk = "DELETE FROM tools_tbluser WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlxoatk_chucnang = "DELETE FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlxoatk_khoaphong = "DELETE FROM tools_tbluser_departmentgroup WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlxoatk_khothuoc = "DELETE FROM tools_tbluser_medicinestore WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlxoatk_phongluu = "DELETE FROM tools_tbluser_medicinephongluu WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(currentUserCode.ToString(), true) + "';";
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Xóa tài khoản: " + currentUserCode + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";

                    condb.ExecuteNonQuery_MeL(sqlxoatk);
                    condb.ExecuteNonQuery_MeL(sqlxoatk_chucnang);
                    condb.ExecuteNonQuery_MeL(sqlxoatk_khoaphong);
                    condb.ExecuteNonQuery_MeL(sqlxoatk_khothuoc);
                    condb.ExecuteNonQuery_MeL(sqlxoatk_phongluu);
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);

                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Đã xóa bỏ tài khoản: " + currentUserCode);
                    frmthongbao.Show();
                    gridControlDSUser.DataSource = null;
                    ucQuanLyNguoiDung_Load(null, null);
                }
                catch (Exception ex)
                {
                    Base.Logging.Warn(ex);
                }
            }
        }

        #region GridView Design
        private void gridViewChucNang_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewDSUser_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewKhoThuoc_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewChucNang_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewDSUser_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewBaoCao_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewKhoThuoc_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        private void gridViewPhongLuu_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion










    }
}
