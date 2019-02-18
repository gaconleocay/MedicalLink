using MedicalLink.Base;
using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang.XyLyMauBenhPham
{
    public partial class frmSuaThoiGianChiDinh : Form
    {
        #region Khai bao
        private SuaPhieuCDDVDTO SuaPhieuCDDV { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        #endregion

        public frmSuaThoiGianChiDinh()
        {
            InitializeComponent();
        }
        public frmSuaThoiGianChiDinh(SuaPhieuCDDVDTO _filter)
        {
            InitializeComponent();
            this.SuaPhieuCDDV = _filter;
        }

        #region Load
        private void frmSuaThoiGianChiDinh_Load(object sender, EventArgs e)
        {
            try
            {
                lblMaPhieuChiDinh.Text = this.SuaPhieuCDDV.maubenhphamid.ToString();
                dateTGChiDinh.Value = this.SuaPhieuCDDV.thoigianchidinh;
                dateTGSuDung.Value = this.SuaPhieuCDDV.thoigiansudung; //phai load truoc khi Load Phieu dieu tri
                if (this.SuaPhieuCDDV.maubenhphamfinishdate.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                {
                    dateTGTraKetQua.Value = this.SuaPhieuCDDV.maubenhphamfinishdate;
                }
                dateTGChiDinh.Focus();
                LoadDotDieuTri();

                if (this.SuaPhieuCDDV.maubenhphamgrouptypeid != 3)
                {
                    LoadPhieuDieuTri();
                }
                else
                { EnableAndDisable(); }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadPhieuDieuTri()
        {
            try
            {
                string _tgiansudung = DateTime.ParseExact(dateTGSuDung.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                string _phieuDieutri = "SELECT maubenhphamid, sophieu || ' - ' || to_char(maubenhphamdate,'dd/MM/yyyy') as maubenhphamname FROM maubenhpham WHERE maubenhphamgrouptype=3 and vienphiid = '" + this.SuaPhieuCDDV.vienphiid + "' and to_char(maubenhphamdate,'yyyyMMdd') = '" + _tgiansudung + "'; ";
                DataTable _dataPhieuDT = condb.GetDataTable_HIS(_phieuDieutri);
                if (_dataPhieuDT != null && _dataPhieuDT.Rows.Count > 0)
                {
                    cboPhieuDieuTri.Properties.DataSource = _dataPhieuDT;
                    cboPhieuDieuTri.Properties.DisplayMember = "maubenhphamname";
                    cboPhieuDieuTri.Properties.ValueMember = "maubenhphamid";
                    cboPhieuDieuTri.ItemIndex = 0;
                }
                else
                {
                    cboPhieuDieuTri.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDotDieuTri()
        {
            try
            {
                string _tgiansudung = DateTime.ParseExact(dateTGSuDung.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _phieuDieutri = "SELECT mrd.medicalrecordid, degp.departmentgroupname, de.departmentname, (mrd.medicalrecordid || ' - ' || degp.departmentgroupname) as medicalrecordkhoa FROM medicalrecord mrd inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid inner join department de on de.departmentid=mrd.departmentid WHERE mrd.vienphiid = '" + this.SuaPhieuCDDV.vienphiid + "' and mrd.thoigianvaovien <='" + _tgiansudung + "' and (case when mrd.thoigianravien<>'0001-01-01 00:00:00' then mrd.thoigianravien >='" + _tgiansudung + "' else 1=1 end);";
                DataTable _dataDotDieuTri = condb.GetDataTable_HIS(_phieuDieutri);
                if (_dataDotDieuTri != null && _dataDotDieuTri.Rows.Count > 0)
                {
                    cboDotDieuTri.Properties.DataSource = _dataDotDieuTri;
                    cboDotDieuTri.Properties.DisplayMember = "medicalrecordkhoa";
                    cboDotDieuTri.Properties.ValueMember = "medicalrecordid";
                    //cboDotDieuTri.EditValue = this.SuaPhieuCDDV.medicalrecordid;
                }
                else
                {
                    cboDotDieuTri.Properties.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void EnableAndDisable()
        {
            try
            {
                lblTGTraKQCaPhieu.Visible = false;
                dateTGTraKetQua.Visible = false;
                lblPhieuDieuTri.Visible = false;
                cboPhieuDieuTri.Visible = false;
                //cboDotDieuTri.Visible = false;
                //cboDotDieuTri.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Events
        private void btnSuaThoiGian_Click(object sender, EventArgs e)
        {
            try
            {
                string _tg_chidinh = DateTime.ParseExact(dateTGChiDinh.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _tg_sudung = DateTime.ParseExact(dateTGSuDung.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _tg_traketqua = DateTime.ParseExact(dateTGTraKetQua.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _phieudieutriid = "";
                string _medicalrecordid = "";
                string _maubenhphamfinishdate = "";
                if (cboPhieuDieuTri.EditValue != null)
                {
                    if (this.SuaPhieuCDDV.phieudieutriid != Utilities.TypeConvertParse.ToInt64(cboPhieuDieuTri.EditValue.ToString()))
                    {
                        _phieudieutriid = ", phieudieutriid='" + cboPhieuDieuTri.EditValue.ToString() + "' ";
                    }
                }
                if (cboDotDieuTri.EditValue != null)
                {
                    if (this.SuaPhieuCDDV.medicalrecordid != Utilities.TypeConvertParse.ToInt64(cboDotDieuTri.EditValue.ToString()))
                    {
                        _medicalrecordid = ", medicalrecordid='" + cboDotDieuTri.EditValue.ToString() + "' ";
                    }
                }

                if (this.SuaPhieuCDDV.maubenhphamgrouptypeid != 3)
                {
                    _maubenhphamfinishdate = ", maubenhphamfinishdate='" + _tg_traketqua + "' ";
                }

                string sqlupdate_TG = "UPDATE serviceprice SET servicepricedate='" + _tg_chidinh + "' " + _medicalrecordid + " WHERE maubenhphamid='" + this.SuaPhieuCDDV.maubenhphamid + "' ; UPDATE maubenhpham SET maubenhphamdate='" + _tg_chidinh + "', maubenhphamdate_sudung='" + _tg_sudung + "' " + _maubenhphamfinishdate + _phieudieutriid + _medicalrecordid + " WHERE maubenhphamid='" + this.SuaPhieuCDDV.maubenhphamid + "' ;";

                //Log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype, vienphiid) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thời gian chỉ định Mã Viện phí=" + this.SuaPhieuCDDV.vienphiid + " - Mã phiếu chỉ định=" + this.SuaPhieuCDDV.maubenhphamid + ". từ: " + this.SuaPhieuCDDV.thoigianchidinh.ToString("yyyy-MM-dd HH:mm:ss") + "=> " + _tg_chidinh + " ; thời gian sử dụng từ: " + this.SuaPhieuCDDV.thoigiansudung.ToString("yyyy-MM-dd HH:mm:ss") + " => " + _tg_sudung + "; thời gian trả KQ từ: " + this.SuaPhieuCDDV.maubenhphamfinishdate.ToString("yyyy-MM-dd HH:mm:ss") + " => " + _tg_traketqua + "; ID phiếu điều trị từ: " + this.SuaPhieuCDDV.phieudieutriid + "=> " + _phieudieutriid + " ', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_12', '" + this.SuaPhieuCDDV.vienphiid + "');";
                if (condb.ExecuteNonQuery_HIS(sqlupdate_TG))
                {
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    MessageBox.Show("Sửa thời gian chỉ định/sử dụng/trả kết quả phiếu DV: [" + this.SuaPhieuCDDV.maubenhphamid + "] thành công", "Thông báo !");
                    this.Visible = false;
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

        #region Custom
        private void dateTGChiDinh_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTGChiDinh.Value != this.SuaPhieuCDDV.thoigianchidinh)
                {
                    btnSuaThoiGian.Enabled = true;
                }
                else
                {
                    btnSuaThoiGian.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void dateTGSuDung_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //Load danh sach Phieu dieu tri
                if (this.SuaPhieuCDDV.maubenhphamgrouptypeid != 3)
                {
                    LoadPhieuDieuTri();
                }
                LoadDotDieuTri();
                if (dateTGSuDung.Value != this.SuaPhieuCDDV.thoigiansudung)
                {
                    btnSuaThoiGian.Enabled = true;
                }
                else
                {
                    btnSuaThoiGian.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void dateTGTraKetQua_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTGTraKetQua.Value != this.SuaPhieuCDDV.maubenhphamfinishdate)
                {
                    btnSuaThoiGian.Enabled = true;
                }
                else
                {
                    btnSuaThoiGian.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion


    }
}
