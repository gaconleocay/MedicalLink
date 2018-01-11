using MedicalLink.Base;
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
        private string billid { get; set; }
        private string departmentgroupid { get; set; }
        private string departmentid { get; set; }
        private string userid { get; set; }
        private string vienphiid { get; set; }
        private string patientid { get; set; }
        #endregion
        public frmSuaPhieuTamUng()
        {
            InitializeComponent();
        }

        #region Load
        public frmSuaPhieuTamUng(string _billid, string _departmentgroupid, string _departmentid, string _userid, string _vienphiid, string _patientid)
        {
            InitializeComponent();
            this.billid = _billid;
            this.departmentgroupid = _departmentgroupid;
            this.departmentid = _departmentid;
            this.userid = _userid;
            this.vienphiid = _vienphiid;
            this.patientid = _patientid;
        }

        private void frmSuaPhieuTamUng_Load(object sender, EventArgs e)
        {
            try
            {
                lblBillID.Text = this.billid;
                cboPhong.EditValue = this.departmentid;
                cboKhoa.EditValue = this.departmentgroupid;
                LoadDanhMucKhoa();
                LoadDanhMucNguoiSuDung();
                cboNguoiThu.EditValue = this.userid;
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
                string _getDSKhoa = "select departmentgroupid,departmentgroupname from departmentgroup where departmentgrouptype in (1,4,11) order by departmentgroupname;";
                DataTable _DSKhoa = condb.GetDataTable_HIS(_getDSKhoa);
                if (_DSKhoa != null && _DSKhoa.Rows.Count > 0)
                {
                    cboKhoa.Properties.DataSource = _DSKhoa;
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
                    if (cboKhoa.EditValue.ToString() != this.departmentgroupid || cboPhong.EditValue.ToString() != this.departmentid || cboNguoiThu.EditValue.ToString() != this.userid)
                    {
                        string _sqlUpdate = "UPDATE bill SET departmentgroupid='" + cboKhoa.EditValue + "', departmentid='" + cboPhong.EditValue + "', userid='" + cboNguoiThu.EditValue + "' WHERE billid='" + this.billid + "' ;";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate))
                        {
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, patientid, logtype) VALUES ('" + Base.SessionLogin.SessionUsercode + "', 'Sửa hóa đơn tạm ứng ID=" + this.billid + " từ departmentgroupid: " + this.departmentgroupid + " => " + cboKhoa.EditValue + "; departmentid: " + this.departmentid + " => " + cboPhong.EditValue + "; userid: " + this.userid + " => " + cboNguoiThu.EditValue + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"+this.vienphiid + "' , '" + this.patientid + "', 'TOOL_21');";
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                            MessageBox.Show("Cập nhật thành công", "Thông báo");
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
