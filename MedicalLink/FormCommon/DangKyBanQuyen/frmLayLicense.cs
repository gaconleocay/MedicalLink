using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Globalization;
using MedicalLink.Base;


namespace MedicalLink.FormCommon.DangKyBanQuyen
{
    public partial class frmLayLicense : Form
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public frmLayLicense()
        {
            InitializeComponent();
        }

        private void txtPasswordMoKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Kiem tra pass dung hay sai?
                    if (txtPasswordMoKhoa.Text.Trim() == MedicalLink.Base.KeyTrongPhanMem.LayLicense_key && SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                    {
                        btnTaoLicense.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frmLayLicense_Load(object sender, EventArgs e)
        {
            try
            {
                txtPasswordMoKhoa.Focus();
                btnTaoLicense.Enabled = false;
                DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                DateTime denNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                dtKeyTuNgay.Value = tuNgay;
                dtKeyDenNgay.Value = denNgay;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnTaoLicense_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaMay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    string datetungay = DateTime.ParseExact(dtKeyTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss");
                    string datedenngay = DateTime.ParseExact(dtKeyDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss");

                    string MaMayVaThoiGianSuDung = MedicalLink.Base.KeyTrongPhanMem.SaltEncrypt + "$" + txtMaMay.Text + "$" + datetungay + "$" + datedenngay;

                    txtKeyKichHoat.Text = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Encrypt(MaMayVaThoiGianSuDung, true);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
                throw;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtKeyKichHoat.Text); //Copy text to Clipboard
                //string strClip = Clipboard.GetText(); //Get text from Clipboard
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frmLayLicense_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
