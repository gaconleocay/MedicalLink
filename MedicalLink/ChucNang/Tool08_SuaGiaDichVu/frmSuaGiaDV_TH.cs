using MedicalLink.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang
{
    public partial class frmSuaGiaDV_TH : Form
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        int servicepriceid_th;
        string tendv_th;
        string madv_th;
        int mabn_th;
        int mavp_th;
        string tenbn_th;
        int maphieu_th;
        string dongiayeucau_th;
        string dongiavienphi_th;
        string dongiabhyt_th;
        string dongiannn_th;

        #endregion
        public frmSuaGiaDV_TH()
        {
            InitializeComponent();
        }

        #region Load
        //sua 1 row
        public frmSuaGiaDV_TH(int servicepriceid, string tendv, string madv, int mabn, int mavp, string tenbn, int maphieu, string dongia, string dongiavienphi, string dongiabhyt, string dongiannn)
        {
            InitializeComponent();
            // Ẩn 1 vài layoutControl
            layoutControlItemTen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;


            servicepriceid_th = servicepriceid;
            tendv_th = tendv;
            madv_th = madv;
            mabn_th = mabn;
            mavp_th = mavp;
            tenbn_th = tenbn;
            maphieu_th = maphieu;
            dongiayeucau_th = dongia;
            dongiavienphi_th = dongiavienphi;
            dongiabhyt_th = dongiabhyt;
            dongiannn_th = dongiannn;

            lblTenDichVu.Text = "[" + madv + "] - " + tendv;
            lblMaBenhNhan.Text = mabn.ToString();
            lblTenBenhNhan.Text = tenbn;
            lblMaVienPhi.Text = mavp.ToString();
            lblMaPhieuChiDinh.Text = maphieu.ToString();
            lblServicepriceID.Text = servicepriceid.ToString();
            txtGiaVP.Text = dongia;
            txtGiaBHYT.Text = dongiabhyt;
            txtGiaYC.Text = dongiavienphi;
            txtGiaNNN.Text = dongiannn;
        }
        //sua nhieu row
        public frmSuaGiaDV_TH(int servicepriceid, string tendv, string madv, string dongia, string dongiavienphi, string dongiabhyt, string dongiannn)
        {
            InitializeComponent();
            layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItemPhieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            servicepriceid_th = servicepriceid;

            lblTenDichVu.Text = "[" + madv + "] - " + tendv;
            lblServicepriceID.Text = servicepriceid.ToString();
            txtGiaVP.Text = dongia;
            txtGiaBHYT.Text = dongiabhyt;
            txtGiaYC.Text = dongiavienphi;
            txtGiaNNN.Text = dongiannn;


        }

        #endregion

        #region Events 
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void tbnSuaGiaMoi_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa dv mã: [" + madv_th + "] thành giá mới ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string sqlupdate_gia = "UPDATE serviceprice SET servicepricemoney_nhandan='" + txtGiaVP.Text.Replace(".", "").Replace(",", ".") + "', servicepricemoney='" + txtGiaYC.Text.Replace(".", "").Replace(",", ".") + "', servicepricemoney_bhyt='" + txtGiaBHYT.Text.Replace(".", "").Replace(",", ".") + "', servicepricemoney_nuocngoai='" + txtGiaNNN.Text.Replace(".", "").Replace(",", ".") + "' WHERE servicepriceid='" + servicepriceid_th + "' ;";

                    //Log
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype, vienphiid, patientid) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update 1 danh mục dv servicepriceid=" + servicepriceid_th + " mã : " + madv_th + " giá YC: " + dongiayeucau_th + "; giá VP: " + dongiavienphi_th + "; giá BHYT: " + dongiabhyt_th + "; giá NNN: " + dongiannn_th + " thành giá VP: " + txtGiaVP.Text.Replace(",", ".") + "; giá YC: " + txtGiaYC.Text.Replace(",", ".") + "; giá BHYT: " + txtGiaBHYT.Text.Replace(",", ".") + "; giá NNN: " + txtGiaNNN.Text.Replace(",", ".") + " ', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_08','" + this.mavp_th + "','" + this.mabn_th + "');";
                    if (condb.ExecuteNonQuery_HIS(sqlupdate_gia))
                    {
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        MessageBox.Show("Sửa giá dịch vụ [" + madv_th + "] thành công", "Thông báo !");
                        this.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void txtGiaVP_EditValueChanged(object sender, EventArgs e)
        {
            if (txtGiaVP.Text != dongiayeucau_th || txtGiaBHYT.Text != dongiabhyt_th || txtGiaYC.Text != dongiavienphi_th || txtGiaNNN.Text != dongiannn_th)
            {
                txtGiaVP.Text = txtGiaVP.Text.Replace(".","");
                btnSuaGiaMoi.Enabled = true;
            }
            else
            {
                btnSuaGiaMoi.Enabled = false;
            }
        }

        private void txtGiaYC_EditValueChanged(object sender, EventArgs e)
        {
            if (txtGiaVP.Text != dongiayeucau_th || txtGiaBHYT.Text != dongiabhyt_th || txtGiaYC.Text != dongiavienphi_th || txtGiaNNN.Text != dongiannn_th)
            {
                txtGiaYC.Text = txtGiaYC.Text.Replace(".", "");
                btnSuaGiaMoi.Enabled = true;
            }
            else
            {
                btnSuaGiaMoi.Enabled = false;
            }
        }

        private void txtGiaBHYT_EditValueChanged(object sender, EventArgs e)
        {
            if (txtGiaVP.Text != dongiayeucau_th || txtGiaBHYT.Text != dongiabhyt_th || txtGiaYC.Text != dongiavienphi_th || txtGiaNNN.Text != dongiannn_th)
            {
                txtGiaBHYT.Text = txtGiaBHYT.Text.Replace(".", "");
                btnSuaGiaMoi.Enabled = true;
            }
            else
            {
                btnSuaGiaMoi.Enabled = false;
            }
        }

        private void txtGiaNNN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtGiaVP.Text != dongiayeucau_th || txtGiaBHYT.Text != dongiabhyt_th || txtGiaYC.Text != dongiavienphi_th || txtGiaNNN.Text != dongiannn_th)
            {
                txtGiaNNN.Text = txtGiaNNN.Text.Replace(".", "");
                btnSuaGiaMoi.Enabled = true;
            }
            else
            {
                btnSuaGiaMoi.Enabled = false;
            }
        }
        #endregion

        #region Custom
        private void txtGiaVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == ','))
            {
                e.Handled = true;
            }
        }
        private void txtGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaYC.Focus();
            }
        }
        private void txtGiaVP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaBHYT.Focus();
            }
        }
        private void txtGiaBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGiaNNN.Focus();
            }
        }
        private void txtGiaNNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSuaGiaMoi.Focus();
            }
        }





        #endregion
    }
}
