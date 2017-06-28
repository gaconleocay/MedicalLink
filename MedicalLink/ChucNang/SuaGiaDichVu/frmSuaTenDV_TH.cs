using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class frmSuaTenDV_TH : Form
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private ucSuaMaTenGiaDV ucsuagiadv;
        private int kieu_capnhat;
        private DataView dv_dmdv;
        private int trangthai_kt;
        private string current_loaidichvu; //"": dich vu; !="": thuoc, vat tu


        public frmSuaTenDV_TH()
        {
            InitializeComponent();
        }

        //sua 1 row : kieusua=1; kieusua=2 sửa nhiều
        //trangthai_kt: =0 chua duyet VP; = 1 da duyet VP
        public frmSuaTenDV_TH(ucSuaMaTenGiaDV control, int kieusua, int trangthai_kt)
        {
            InitializeComponent();
            this.ucsuagiadv = control;
            this.kieu_capnhat = kieusua;
            this.trangthai_kt = trangthai_kt;
        }

        #region Load
        private void frmSuaTenDV_TH_Load(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = ucsuagiadv.gridViewDSDV.FocusedRowHandle;
                string trangthai_kt = Convert.ToString(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "trangthai").ToString());
                long servicepriceid = Convert.ToInt64(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                string madv = Convert.ToString(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "madv").ToString());
                string tendv = Convert.ToString(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "tendv_bhyt").ToString());
                long mabn = Convert.ToInt64(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "mabn").ToString());
                long mavp = Convert.ToInt64(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "mavp").ToString());
                string tenbn = Convert.ToString(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "tenbn").ToString());
                long maphieu = Convert.ToInt64(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "maphieu").ToString());

                current_loaidichvu = Convert.ToString(ucsuagiadv.gridViewDSDV.GetRowCellValue(rowHandle, "huongdansudung").ToString());

                if (kieu_capnhat == 1)
                {
                    // Ẩn 1 vài layoutControl
                    layoutControlItemTen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    btnSuaTenMoi_Row.Visible = true;
                    btnSuaTenMoi_Nhieu.Visible = false;

                    lblServicepriceID.Text = servicepriceid.ToString();
                    lblMaPhieuChiDinh.Text = maphieu.ToString();
                    lblMaBenhNhan.Text = mabn.ToString();
                    lblMaVienPhi.Text = mavp.ToString();
                    lblTenBenhNhan.Text = tenbn;
                    lblMaDichVu.Text = madv;
                    lblTenDichVuBHYT.Text = tendv;

                    txtMaDV_Moi.Focus();
                    DanhMucDichVu_Load(current_loaidichvu, madv);
                }
                else if (kieu_capnhat == 2)
                {
                    layoutControlItemTen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItemPhieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    btnSuaTenMoi_Nhieu.Visible = true;
                    btnSuaTenMoi_Row.Visible = false;

                    lblTatCaDichVuMa.Text = madv;
                    lblMaDichVu.Text = madv;
                    lblTenDichVuBHYT.Text = tendv;

                    txtMaDV_Moi.Focus();
                    DanhMucDichVu_Load(current_loaidichvu, madv);
                }
                else
                {
                    layoutControlItemTen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItemTen.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    btnSuaTenMoi_Row.Visible = false;
                    layoutControlItemID.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItemPhieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    btnSuaTenMoi_Nhieu.Visible = false;
                }

                lblTenBHYT_Moi.Text = "";
                lblTenVP_Moi.Text = "";
                lblTenYC_Moi.Text = "";
                lblTenNNN_Moi.Text = "";
                lblGiaBHYT.Text = "";
                lblGiaVP.Text = "";
                lblGiaYC.Text = "";
                lblGiaNNN.Text = "";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        void DanhMucDichVu_Load(string loaidichvu, string madv)
        {
            try
            {
                string sqldsdv = "";

                if (loaidichvu == "") //dich vu ky thuat
                {
                    if (this.trangthai_kt == 0)
                    {
                        sqldsdv = "SELECT servicepricecode as dv_ma, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicelock=0 and isremove is null and servicepricegroupcode <> '' and ServiceGroupType in (1,2,3,4,11) ORDER BY servicepricegroupcode;";
                    }
                    else
                    {
                        sqldsdv = "SELECT sef.servicepricecode as dv_ma, sef.servicepricenamebhyt as dv_tenbhyt, sef.servicepricenamenhandan as dv_tenvp, sef.servicepricenamenuocngoai as dv_tennnn, sef.servicepricefeebhyt as gia_bhyt, sef.servicepricefeenhandan as gia_vp, sef.servicepricefee as gia_yc, sef.servicepricefeenuocngoai as gia_nnn FROM servicepriceref sef inner join (select sefc.servicepricefee, sefc.servicepricefeebhyt, sefc.servicepricefeenhandan from servicepriceref sefc where sefc.servicepricecode='" + madv + "') serf on serf.servicepricefee=sef.servicepricefee and serf.servicepricefeebhyt=sef.servicepricefeebhyt and serf.servicepricefeenhandan=sef.servicepricefeenhandan WHERE sef.servicelock=0 and sef.isremove is null and sef.servicepricegroupcode <> '' and sef.ServiceGroupType in (1,2,3,4,11) ORDER BY sef.servicepricegroupcode; ";
                    }
                }
                else //thuoc, vat tu
                {
                    if (this.trangthai_kt == 0)
                    {
                        sqldsdv = "SELECT medicinecode as dv_ma, medicinename as dv_tenbhyt, medicinename as dv_tenvp, medicinename as dv_tennnn, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM medicine_ref WHERE servicelock is null and isremove=0 ORDER BY datatype, medicinename;";
                    }
                    else
                    {
                        sqldsdv = "SELECT mef.medicinecode as dv_ma, mef.medicinename as dv_tenbhyt, mef.medicinename as dv_tenvp, mef.medicinename as dv_tennnn, mef.servicepricefeebhyt as gia_bhyt, mef.servicepricefeenhandan as gia_vp, mef.servicepricefee as gia_yc, mef.servicepricefeenuocngoai as gia_nnn FROM medicine_ref mef inner join (select mefc.servicepricefee, mefc.servicepricefeebhyt, mefc.servicepricefeenhandan from medicine_ref mefc where mefc.medicinecode='" + madv + "') serf on serf.servicepricefee=mef.servicepricefee and serf.servicepricefeebhyt=mef.servicepricefeebhyt and serf.servicepricefeenhandan=mef.servicepricefeenhandan WHERE mef.servicelock is null and mef.isremove=0 ORDER BY mef.datatype, mef.medicinename;     ";
                    }
                }
                dv_dmdv = new DataView(condb.GetDataTable_HIS(sqldsdv));

                cbbTenDV_Moi.Properties.DataSource = dv_dmdv;
                cbbTenDV_Moi.Properties.DisplayMember = "dv_tenbhyt";
                cbbTenDV_Moi.Properties.ValueMember = "dv_ma";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void txtMaDV_Moi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (txtMaDV_Moi.Text.Trim() != "")
                //{
                //    btnSuaTenMoi_Row.Enabled = true;
                //    btnSuaTenMoi_Nhieu.Enabled = true;
                //}
                //else
                //{
                //    btnSuaTenMoi_Row.Enabled = false;
                //    btnSuaTenMoi_Nhieu.Enabled = false;
                //}
            }
            catch (Exception)
            {
            }
        }

        private void cbbTenDV_Moi_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                cbbTenDV_Moi.Properties.Buttons[1].Visible = false;
                cbbTenDV_Moi.EditValue = null;
                txtMaDV_Moi.Text = "";
                txtMaDV_Moi.Focus();
                txtMaDV_Moi.SelectAll();

                lblTenBHYT_Moi.Text = "";
                lblTenVP_Moi.Text = "";
                lblTenYC_Moi.Text = "";
                lblTenNNN_Moi.Text = "";
                lblGiaBHYT.Text = "";
                lblGiaVP.Text = "";
                lblGiaYC.Text = "";
                lblGiaNNN.Text = "";

            }
        }

        private void cbbTenDV_Moi_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == PopupCloseMode.Normal)
                {
                    if (cbbTenDV_Moi.EditValue != null)
                    {
                        txtMaDV_Moi.Text = cbbTenDV_Moi.EditValue.ToString();
                        cbbTenDV_Moi.Properties.Buttons[1].Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void cbbTenDV_Moi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbbTenDV_Moi.EditValue != null)
                {
                    txtMaDV_Moi.Text = cbbTenDV_Moi.EditValue.ToString();
                    cbbTenDV_Moi.Properties.Buttons[1].Visible = true;
                }
            }
        }

        private void txtMaDV_Moi_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strValue = (sender as DevExpress.XtraEditors.TextEdit).Text.ToUpper();
                LoadData_DichVu_TheoMa(txtMaDV_Moi.Text.Trim());
            }
        }
        private void LoadData_DichVu_TheoMa(string dv_ma_timkiem)
        {
            try
            {
                string sql_dv = "";
                if (this.current_loaidichvu == "")
                {
                     sql_dv = "SELECT servicepricecode as dv_ma, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn,servicepricename as dv_tenyc, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicepricecode ='" + dv_ma_timkiem + "';";
                }
                else
                {
                    sql_dv = "SELECT medicinecode as dv_ma, medicinename as dv_tenbhyt, medicinename as dv_tenvp, medicinename as dv_tennnn, medicinename as dv_tenyc, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM medicine_ref WHERE medicinecode='" + dv_ma_timkiem + "';";
                }
                DataView data_tk = new DataView(condb.GetDataTable_HIS(sql_dv));
                if (data_tk.Count > 0)
                {
                    cbbTenDV_Moi.EditValue = data_tk[0]["dv_ma"].ToString();
                    lblTenBHYT_Moi.Text = cbbTenDV_Moi.Text;
                    lblTenVP_Moi.Text = data_tk[0]["dv_tenvp"].ToString();
                    lblTenYC_Moi.Text = data_tk[0]["dv_tenyc"].ToString();
                    lblTenNNN_Moi.Text = data_tk[0]["dv_tennnn"].ToString();
                    lblGiaBHYT.Text = data_tk[0]["gia_bhyt"].ToString();
                    lblGiaVP.Text = data_tk[0]["gia_vp"].ToString();
                    lblGiaYC.Text = data_tk[0]["gia_yc"].ToString();
                    lblGiaNNN.Text = data_tk[0]["gia_nnn"].ToString();
                }
                else
                {
                    cbbTenDV_Moi.EditValue = null;
                    lblTenBHYT_Moi.Text = "";
                    lblTenVP_Moi.Text = "";
                    lblTenYC_Moi.Text = "";
                    lblTenNNN_Moi.Text = "";
                    lblGiaBHYT.Text = "";
                    lblGiaVP.Text = "";
                    lblGiaYC.Text = "";
                    lblGiaNNN.Text = "";
                }

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        //hien thi noi dung lay tu searchlookedit
        private void cbbTenDV_Moi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbTenDV_Moi.EditValue != null)
                {
                    //lblTenBHYT_Moi.Text = cbbTenDV_Moi.Text;
                    LoadData_DichVu_TheoMa(cbbTenDV_Moi.EditValue.ToString());
                    //var tenvp = this.cbbTenDV_Moi.Properties.View.GetFocusedRowCellValue("dv_tenvp");
                    //lblTenVP_Moi.Text = (tenvp ?? "").ToString();
                    //object giabhyt = this.cbbTenDV_Moi.Properties.View.GetFocusedRowCellValue("gia_bhyt");
                    //lblGiaBHYT.Text = (giabhyt ?? "").ToString();
                    //object giavp = this.cbbTenDV_Moi.Properties.View.GetFocusedRowCellValue("gia_vp");
                    //lblGiaVP.Text = (giavp ?? "").ToString();
                    //object giayc = this.cbbTenDV_Moi.Properties.View.GetFocusedRowCellValue("gia_yc");
                    //lblGiaYC.Text = (giayc ?? "").ToString();
                    //object giannn = this.cbbTenDV_Moi.Properties.View.GetFocusedRowCellValue("gia_nnn");
                    //lblGiaNNN.Text = (giannn ?? "").ToString();

                    btnSuaTenMoi_Row.Enabled = true;
                    btnSuaTenMoi_Nhieu.Enabled = true;
                }
                else
                {
                    txtMaDV_Moi.Text = "";
                    txtMaDV_Moi.Focus();
                    txtMaDV_Moi.SelectAll();

                    lblTenBHYT_Moi.Text = "";
                    lblTenVP_Moi.Text = "";
                    lblTenYC_Moi.Text = "";
                    lblTenNNN_Moi.Text = "";
                    lblGiaBHYT.Text = "";
                    lblGiaVP.Text = "";
                    lblGiaYC.Text = "";
                    lblGiaNNN.Text = "";
                    btnSuaTenMoi_Row.Enabled = false;
                    btnSuaTenMoi_Nhieu.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #region Click Sua
        private void btnSuaTenMoi_Row_Click(object sender, EventArgs e)
        {  // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                // Querry thực hiện
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa dịch vụ mã: [" + lblMaDichVu.Text + "] thành mã: [" + txtMaDV_Moi.Text + "] ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string sqlupdate_ten = "UPDATE serviceprice SET servicepricecode='" + txtMaDV_Moi.Text + "', servicepricename='" + lblTenVP_Moi.Text + "', servicepricename_bhyt='" + cbbTenDV_Moi.Text + "', servicepricename_nhandan='" + lblTenVP_Moi.Text + "', servicepricename_nuocngoai='" + lblTenVP_Moi.Text + "' WHERE servicepriceid='" + lblServicepriceID.Text + "' ;";

                    //Log
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', ' Update 1 danh mục dv servicepriceid=" + lblServicepriceID.Text + " từ mã [" + lblMaDichVu.Text + "]-[" + lblTenDichVuBHYT.Text + "] thành: [" + txtMaDV_Moi.Text + "]-[" + cbbTenDV_Moi.Text + "]' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_HIS(sqlupdate_ten);
                    condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    MessageBox.Show("Sửa mã,tên dịch vụ từ [" + lblMaDichVu.Text + "] thành mã: [" + txtMaDV_Moi.Text + "] thành công", "Thông báo !");
                    this.Visible = false;

                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void btnSuaTenMoi_Nhieu_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int dem = 0;
            try
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa tất cả dịch vụ mã: [" + lblMaDichVu.Text + "] thành mã: [" + txtMaDV_Moi.Text + "] ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < ucsuagiadv.gridViewDSDV.RowCount; i++)
                    {
                        if (ucsuagiadv.gridViewDSDV.GetRowCellValue(i, "madv").ToString() == lblMaDichVu.Text)
                        {
                            string sqlupdate_ten = "UPDATE serviceprice SET servicepricecode='" + txtMaDV_Moi.Text + "', servicepricename='" + lblTenYC_Moi.Text + "', servicepricename_bhyt='" + cbbTenDV_Moi.Text + "', servicepricename_nhandan='" + lblTenVP_Moi.Text + "', servicepricename_nuocngoai='" + lblTenNNN_Moi.Text + "' WHERE servicepriceid='" + ucsuagiadv.gridViewDSDV.GetRowCellValue(i, "servicepriceid").ToString() + "' ;";
                            //Log
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', ' Update 1 danh mục dv servicepriceid=" + ucsuagiadv.gridViewDSDV.GetRowCellValue(i, "servicepriceid").ToString() + " từ mã [" + lblMaDichVu.Text + "]-[" + lblTenDichVuBHYT.Text + "] thành: [" + txtMaDV_Moi.Text + "]-[" + cbbTenDV_Moi.Text + "]' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery_HIS(sqlupdate_ten);
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                            dem += 1;
                        }
                    }

                    //Log all
                    string sqlinsert_log_all = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', ' Sửa " + dem + " mã,tên dịch vụ từ [" + lblMaDichVu.Text + "] thành mã: [" + txtMaDV_Moi.Text + "] thành công' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery_MeL(sqlinsert_log_all);

                    MessageBox.Show("Sửa " + dem + " mã,tên dịch vụ từ [" + lblMaDichVu.Text + "] thành mã: [" + txtMaDV_Moi.Text + "] thành công", "Thông báo !");
                    this.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion

    }
}
