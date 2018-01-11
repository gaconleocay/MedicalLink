using MedicalLink.Base;
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
        private long vienphiId { get; set; }
        private long maubenhphamId { get; set; }
        private long phieudieutriId { get; set; }
        private DateTime thoigianchidinh, thoigiansudung;
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public frmSuaThoiGianChiDinh()
        {
            InitializeComponent();
        }
        public frmSuaThoiGianChiDinh(long _mavienphi, long _maubenhphamid, DateTime _thoigianchidinh, DateTime _thoigiansudung, long _phieudieutriId)
        {
            InitializeComponent();
            this.vienphiId = _mavienphi;
            this.maubenhphamId = _maubenhphamid;
            this.thoigianchidinh = _thoigianchidinh;
            this.thoigiansudung = _thoigiansudung;
            this.phieudieutriId = _phieudieutriId;
        }

        #region Load
        private void frmSuaThoiGianChiDinh_Load(object sender, EventArgs e)
        {
            try
            {
                lblMaPhieuChiDinh.Text = this.maubenhphamId.ToString();
                dateTGChiDinh.Value = thoigianchidinh;
                dateTGSuDung.Value = thoigiansudung; //phai load truoc khi Load Phieu dieu tri
                dateTGChiDinh.Focus();
                LoadPhieuDieuTri();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadPhieuDieuTri()
        {
            try
            {
                string _tgiansudung = DateTime.ParseExact(dateTGSuDung.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                string _phieuDieutri = "SELECT maubenhphamid, sophieu || ' - ' || to_char(maubenhphamdate,'dd/MM/yyyy') as maubenhphamname FROM maubenhpham WHERE maubenhphamgrouptype=3 and vienphiid = '" + this.vienphiId + "' and to_char(maubenhphamdate,'yyyyMMdd') = '" + _tgiansudung + "'; ";
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion
        //Sua thoi gian
        private void btnSuaThoiGian_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string tg_chidinh = DateTime.ParseExact(dateTGChiDinh.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tg_sudung = DateTime.ParseExact(dateTGSuDung.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _phieudieutriid = "";
                if (this.phieudieutriId != Utilities.Util_TypeConvertParse.ToInt64(cboPhieuDieuTri.EditValue.ToString()))
                {
                    _phieudieutriid = ", phieudieutriid='" + cboPhieuDieuTri.EditValue.ToString() + "' ";
                }

                string sqlupdate_TG = "UPDATE serviceprice SET servicepricedate = '" + tg_chidinh + "' WHERE maubenhphamid = '" + this.maubenhphamId + "' ; UPDATE maubenhpham SET maubenhphamdate = '" + tg_chidinh + "', maubenhphamdate_sudung='" + tg_sudung + "' " + _phieudieutriid + " WHERE maubenhphamid = '" + this.maubenhphamId + "' ;";

                //Log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thời gian chỉ định Mã Viện phí=" + this.vienphiId + " - Mã phiếu chỉ định=" + this.maubenhphamId + ". từ: " + this.thoigianchidinh.ToString("yyyy-MM-dd HH:mm:ss") + "=> " + tg_chidinh + " ; thời gian sử dụng từ: " + this.thoigiansudung.ToString("yyyy-MM-dd HH:mm:ss") + " => " + tg_sudung + "; ID phiếu điều trị từ: " + this.phieudieutriId + "=> " + cboPhieuDieuTri.EditValue.ToString() + " ', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_12');";
                condb.ExecuteNonQuery_HIS(sqlupdate_TG);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                MessageBox.Show("Sửa thời gian chỉ định/sử dụng phiếu DV: [" + this.maubenhphamId + "] thành công", "Thông báo !");
                this.Visible = false;
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #region Custom
        private void dateTGChiDinh_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dateTGChiDinh.Value != this.thoigianchidinh)
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void dateTGSuDung_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //Load danh sach Phieu dieu tri
                LoadPhieuDieuTri();
                if (dateTGSuDung.Value != this.thoigiansudung)
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
    }
}
