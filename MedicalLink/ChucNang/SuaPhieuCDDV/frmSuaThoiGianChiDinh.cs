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
        long maubenhphamid;
        DateTime thoigianchidinh, thoigiansudung;
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public frmSuaThoiGianChiDinh()
        {
            InitializeComponent();
        }
        public frmSuaThoiGianChiDinh(long _maubenhphamid, DateTime _thoigianchidinh, DateTime _thoigiansudung)
        {
            InitializeComponent();
            this.maubenhphamid = _maubenhphamid;
            this.thoigianchidinh = _thoigianchidinh;
            this.thoigiansudung = _thoigiansudung;
        }

        private void frmSuaThoiGianChiDinh_Load(object sender, EventArgs e)
        {
            try
            {
                lblMaPhieuChiDinh.Text = this.maubenhphamid.ToString();
                dateTGChiDinh.Value = thoigianchidinh;
                dateTGSuDung.Value = thoigiansudung;
                dateTGChiDinh.Focus();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        //Sua thoi gian
        private void btnSuaThoiGian_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string tg_chidinh = DateTime.ParseExact(dateTGChiDinh.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tg_sudung = DateTime.ParseExact(dateTGSuDung.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string sqlupdate_TG = "UPDATE serviceprice SET servicepricedate = '" + tg_chidinh + "' WHERE maubenhphamid = '" + this.maubenhphamid + "' ; UPDATE maubenhpham SET maubenhphamdate = '" + tg_chidinh + "', maubenhphamdate_sudung='" + tg_sudung + "' WHERE maubenhphamid = '" + this.maubenhphamid + "' ;";

                //Log
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thời gian chỉ định từ: " + this.thoigianchidinh.ToString("yyyy-MM-dd HH:mm:ss") + "=> " + tg_chidinh + " ; thời gian sử dụng từ: " + this.thoigiansudung.ToString("yyyy-MM-dd HH:mm:ss") + " => " + tg_sudung + " ', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                condb.ExecuteNonQuery_HIS(sqlupdate_TG);
                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                MessageBox.Show("Sửa thời gian chỉ định/sử dụng phiếu DV: [" + this.maubenhphamid + "] thành công", "Thông báo !");
                this.Visible = false;
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

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

    }
}
