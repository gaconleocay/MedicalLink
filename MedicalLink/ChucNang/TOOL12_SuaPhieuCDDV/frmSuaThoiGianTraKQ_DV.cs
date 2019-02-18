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
    public partial class frmSuaThoiGianTraKQ_DV : Form
    {
        #region Khai bao
        private SuaPhieuCDDVTraKetQuaDTO suaPhieu_TraKQ { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        #endregion

        public frmSuaThoiGianTraKQ_DV()
        {
            InitializeComponent();
        }
        public frmSuaThoiGianTraKQ_DV(SuaPhieuCDDVTraKetQuaDTO _filter)
        {
            InitializeComponent();
            this.suaPhieu_TraKQ = _filter;
        }

        #region Load
        private void frmSuaThoiGianTraKQ_DV_Load(object sender, EventArgs e)
        {
            try
            {
                lblServicepriceID.Text = this.suaPhieu_TraKQ.servicepriceid.ToString();
                lblServicepricename.Text = this.suaPhieu_TraKQ.servicepricename;
                if (this.suaPhieu_TraKQ.thoigiantraketqua.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                {
                    dateTGTraKetQua.Value = this.suaPhieu_TraKQ.thoigiantraketqua;
                }
                if (this.suaPhieu_TraKQ.maubenhphamgrouptypeid == 4)
                {
                    dateTGPTTT.Enabled = true;
                    if (this.suaPhieu_TraKQ.phauthuatthuthuatdate.ToString("yyyy-MM-dd HH:mm:ss") != "0001-01-01 00:00:00")
                    {
                        dateTGPTTT.Value = this.suaPhieu_TraKQ.phauthuatthuthuatdate;
                    }
                }
                else
                {
                    dateTGPTTT.Enabled = false;
                }
                dateTGPTTT.Focus();
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
                string tg_pttt = DateTime.ParseExact(dateTGPTTT.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tg_traketqua = DateTime.ParseExact(dateTGTraKetQua.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string sqlupdate_TG = "UPDATE phauthuatthuthuat SET phauthuatthuthuatdate='" + tg_pttt + "',phauthuatthuthuatdate_ketthuc='" + tg_traketqua + "'  WHERE servicepriceid=" + this.suaPhieu_TraKQ.servicepriceid + "; UPDATE service SET servicetimetrakq='" + tg_traketqua + "' WHERE servicepriceid=" + this.suaPhieu_TraKQ.servicepriceid + ";";

                //Log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype, vienphiid) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa Mã DV=" + this.suaPhieu_TraKQ.servicepriceid + "; Tên=" + this.suaPhieu_TraKQ.servicepricename + ". TG PTTT từ: " + this.suaPhieu_TraKQ.phauthuatthuthuatdate.ToString("yyyy-MM-dd HH:mm:ss") + "=> " + tg_pttt + " ; thời gian trả KQ từ: " + this.suaPhieu_TraKQ.thoigiantraketqua.ToString("yyyy-MM-dd HH:mm:ss") + " => " + tg_traketqua + " ', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_12', '0');";
                if (condb.ExecuteNonQuery_HIS(sqlupdate_TG))
                {
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    MessageBox.Show("Sửa thời gian PTTT/TG trả kết quả DV ID: [" + this.suaPhieu_TraKQ.servicepriceid + "] thành công", "Thông báo !");
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
        private void dateTGPTTT_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTGPTTT.Value != this.suaPhieu_TraKQ.phauthuatthuthuatdate)
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
                if (dateTGTraKetQua.Value != this.suaPhieu_TraKQ.thoigiantraketqua)
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
