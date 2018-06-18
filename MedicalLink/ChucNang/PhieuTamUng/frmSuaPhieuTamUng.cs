using MedicalLink.Base;
using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang.PhieuTamUng
{
    public partial class frmSuaPhieuTamUng : Form
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<DepartmentgroupDTO> lstDepartGroup { get; set; }
        private SuaPhieuTamUngDTO PhieuTamUng { get; set; }
        #endregion
        public frmSuaPhieuTamUng()
        {
            InitializeComponent();
        }

        #region Load

        public frmSuaPhieuTamUng(SuaPhieuTamUngDTO _tamung)
        {
            InitializeComponent();
            this.PhieuTamUng = _tamung;
        }
        private void frmSuaPhieuTamUng_Load(object sender, EventArgs e)
        {
            try
            {
                lblBillID.Text = this.PhieuTamUng.billid.ToString();
                cboPhong.EditValue = this.PhieuTamUng.departmentid;
                cboKhoa.EditValue = this.PhieuTamUng.departmentgroupid;
                LoadDanhMucKhoa();
                LoadDanhMucNguoiSuDung();
                cboNguoiThu.EditValue = this.PhieuTamUng.userid;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                string _getDSKhoa = "select departmentgroupid, departmentgroupname, COALESCE(cumthutien,'KHÁC') as cumthutien from departmentgroup where departmentgrouptype in (1,4,11) order by departmentgroupname;";
                DataTable _DSKhoa = condb.GetDataTable_HIS(_getDSKhoa);
                if (_DSKhoa != null && _DSKhoa.Rows.Count > 0)
                {
                    this.lstDepartGroup = Utilities.DataTables.DataTableToList<DepartmentgroupDTO>(_DSKhoa);
                    cboKhoa.Properties.DataSource = this.lstDepartGroup;
                    cboKhoa.Properties.DisplayMember = "departmentgroupname";
                    cboKhoa.Properties.ValueMember = "departmentgroupid";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhMucNguoiSuDung()
        {
            try
            {
                string _getDSUser = "select userhisid,usercode,username from nhompersonnel order by username;";
                DataTable _DSUSer = condb.GetDataTable_HIS(_getDSUser);
                if (_DSUSer != null && _DSUSer.Rows.Count > 0)
                {
                    cboNguoiThu.Properties.DataSource = _DSUSer;
                    cboNguoiThu.Properties.DisplayMember = "username";
                    cboNguoiThu.Properties.ValueMember = "userhisid";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Custom
        private void cboKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue != null)
                {
                    string _sqlPhong = "select departmentid,departmentname from department where departmentgroupid='" + cboKhoa.EditValue.ToString() + "' and departmenttype in (2,3,9) order by departmentname; ";
                    DataTable _DSPhong = condb.GetDataTable_HIS(_sqlPhong);
                    if (_DSPhong != null && _DSPhong.Rows.Count > 0)
                    {
                        cboPhong.Properties.DataSource = _DSPhong;
                        cboPhong.Properties.DisplayMember = "departmentname";
                        cboPhong.Properties.ValueMember = "departmentid";
                    }
                }
                else
                {
                    string _sqlPhong = "select departmentid,departmentname from department where departmenttype in (2,3,9) order by departmentname; ";
                    DataTable _DSPhong = condb.GetDataTable_HIS(_sqlPhong);
                    if (_DSPhong != null && _DSPhong.Rows.Count > 0)
                    {
                        cboPhong.Properties.DataSource = _DSPhong;
                        cboPhong.Properties.DisplayMember = "departmentname";
                        cboPhong.Properties.ValueMember = "departmentid";
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        #region Events
        private void btnSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.Text == "" || cboPhong.Text == "" || cboNguoiThu.Text == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    if (cboKhoa.EditValue.ToString() != this.PhieuTamUng.departmentgroupid.ToString() || cboPhong.EditValue.ToString() != this.PhieuTamUng.departmentid.ToString() || cboNguoiThu.EditValue.ToString() != this.PhieuTamUng.userid.ToString())
                    {
                        string _sqlUpdate = "UPDATE bill SET departmentgroupid='" + cboKhoa.EditValue + "', departmentid='" + cboPhong.EditValue + "', userid='" + cboNguoiThu.EditValue + "' WHERE billid='" + this.PhieuTamUng.billid + "' ;";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate))
                        {
                            string _cumthutien = this.lstDepartGroup.Where(o => o.departmentgroupid.ToString() == cboKhoa.EditValue.ToString()).FirstOrDefault().cumthutien;
                           // string _deleteBillEdit = 
                            string _slqBaoCao = "INSERT INTO tools_billedit(billid, billcode, billgroupcode, patientid, vienphiid, hosobenhanid, loaiphieuthuid, userid, username, billdate, cumthutien, departmentgroupid, departmentgroupname, departmentid, departmentname, patientname, userid_nhan, username_nhan, departmentgroupid_nhan, departmentgroupname_nhan, departmentid_nhan, departmentname_nhan, sotien, createusercode, createdate) VALUES ('" + this.PhieuTamUng.billid + "', '" + this.PhieuTamUng.billcode + "', '" + this.PhieuTamUng.billgroupcode + "', '" + this.PhieuTamUng.patientid + "', '" + this.PhieuTamUng.vienphiid + "', '" + this.PhieuTamUng.hosobenhanid + "', '2', '" + this.PhieuTamUng.userid + "', '" + this.PhieuTamUng.username + "', '" + this.PhieuTamUng.billdate + "', '" + _cumthutien + "', '" + this.PhieuTamUng.departmentgroupid + "', '" + this.PhieuTamUng.departmentgroupname + "', '" + this.PhieuTamUng.departmentid + "', '" + this.PhieuTamUng.departmentname + "', '" + this.PhieuTamUng.patientname + "', '" + cboNguoiThu.EditValue + "', '" + cboNguoiThu.Text + "', '" + cboKhoa.EditValue + "', '" + cboKhoa.Text + "', '" + cboPhong.EditValue + "', '" + cboPhong.Text + "', '" + this.PhieuTamUng.sotien + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, patientid, logtype) VALUES ('" + Base.SessionLogin.SessionUsercode + "', 'Sửa hóa đơn tạm ứng ID=" + this.PhieuTamUng.billid + " từ departmentgroupid: " + this.PhieuTamUng.departmentgroupid + " => " + cboKhoa.EditValue + "; departmentid: " + this.PhieuTamUng.departmentid + " => " + cboPhong.EditValue + "; userid: " + this.PhieuTamUng.userid + " => " + cboNguoiThu.EditValue + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.PhieuTamUng.vienphiid + "' , '" + this.PhieuTamUng.patientid + "', 'TOOL_21'); ";
                            if (condb.ExecuteNonQuery_MeL(_slqBaoCao))
                            {
                                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
    }
}
