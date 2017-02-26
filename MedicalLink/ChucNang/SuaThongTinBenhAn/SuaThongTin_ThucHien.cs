using DevExpress.XtraSplashScreen;
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

namespace MedicalLink.ChucNang
{
    public partial class SuaThongTin_ThucHien : Form
    {
        ucSuaThongTinBenhAn SuaThongTinBenhAn;
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //DataView dv_tinhhuyenxa;
        DataView dv_tinh, dv_huyen, dv_xa;

        long vienphiid, patientid, bhytid, hosobenhanid;
        string PatientName, NgaySinh, GioiTinh_Code, GioiTinh, SoNha, ThonPho, NoiLamViec, TrangThai;
        string SoTheBHYT, HanTheTu, HanTheDen, NoiDKKCBBD;
        string xa_code, xa_name, huyen_code, huyen_name, tinh_code, tinh_name;
        public SuaThongTin_ThucHien()
        {
            InitializeComponent();
        }
        public SuaThongTin_ThucHien(ucSuaThongTinBenhAn control)
        {
            try
            {
                InitializeComponent();
                if (control != null)
                {
                    SuaThongTinBenhAn = control;
                }

            }
            catch (Exception)
            {
            }
        }

        #region Load
        private void SuaThongTin_ThucHien_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                if (SuaThongTinBenhAn != null)
                {
                    LoadDataTinh_VeMay();

                    var rowHandle = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.FocusedRowHandle;
                    patientid = Convert.ToInt64(SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "patientid").ToString());
                    vienphiid = Convert.ToInt64(SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "vienphiid").ToString());
                    bhytid = Convert.ToInt64(SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "bhytid").ToString());
                    hosobenhanid = Convert.ToInt64(SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hosobenhanid").ToString());

                    PatientName = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "patientname").ToString();
                    NgaySinh = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "ngaysinh").ToString();
                    GioiTinh_Code = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "gioitinhcode").ToString();
                    GioiTinh = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "gioitinh").ToString();
                    SoNha = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_sonha").ToString();
                    ThonPho = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_thon").ToString();
                    NoiLamViec = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "noilamviec").ToString();
                    TrangThai = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "trangthai").ToString();
                    xa_code = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_xacode").ToString();
                    xa_name = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_xaname").ToString();
                    huyen_code = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_huyencode").ToString();
                    huyen_name = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_huyenname").ToString();
                    tinh_code = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_tinhcode").ToString();
                    tinh_name = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hc_tinhname").ToString();

                    SoTheBHYT = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "sothebhyt").ToString();
                    HanTheTu = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hanthetu").ToString();
                    HanTheDen = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "hantheden").ToString();
                    NoiDKKCBBD = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "noidkkcbbd").ToString();

                    LoadThongTinBenhNhan();
                    LoadXaHuyenTinh();
                    LoadThongTinVeTheBHYT();
                    btnSuaThongTinBN.Enabled = false;
                    btnSuaThongTinBHYT.Enabled = false;
                    if (txtTinh.Text.Trim() != "")
                    {
                        LoadDataHuyen_VeMay(txtTinh.Text.Trim());
                    }
                    if (txtTinh.Text.Trim() != "" && txtHuyen.Text.Trim() != "")
                    {
                        LoadDataXa_VeMay(txtTinh.Text.Trim(), txtHuyen.Text.Trim());
                    }
                    LoadXaHuyenTinh();

                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void LoadThongTinBenhNhan()
        {
            try
            {
                lblPatientId.Text = patientid.ToString();
                lblVienphiId.Text = vienphiid.ToString();
                lblTrangThai.Text = TrangThai;
                txtPatientName.Text = PatientName;
                DateTime dtngaysinh = Convert.ToDateTime(NgaySinh);
                dtNgaySinh.EditValue = dtngaysinh;
                txtGioiTinh.Text = GioiTinh_Code;
                cbbGioiTinh.Text = GioiTinh;
                txtSoNha.Text = SoNha;
                txtThonPho.Text = ThonPho;
                txtNoiLamViec.Text = NoiLamViec;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadXaHuyenTinh()
        {
            try
            {                       
                txtTinh.Text = tinh_code;
                cboTinh.Text = tinh_name;
                txtHuyen.Text = huyen_code;
                cboHuyen.Text = huyen_name;
                txtXa.Text = xa_code;
                cboXa.Text = xa_name;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadThongTinVeTheBHYT()
        {
            try
            {
                txtSoTheBHYT.Text = SoTheBHYT;
                //DateTime dt_tu = Convert.ToDateTime(NgaySinh);
                //DateTime dt_den = Convert.ToDateTime(NgaySinh);
                dtHanTheTu.EditValue = Convert.ToDateTime(HanTheTu);
                dtHanTheDen.EditValue = Convert.ToDateTime(HanTheDen);
                txtNoiDKKCBBD.Text = NoiDKKCBBD;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void LoadDataTinh_VeMay()
        {
            try
            {
                //string sql_loadtinh = "SELECT DISTINCT hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname FROM hosobenhan;";
                //dv_tinhhuyenxa = new DataView(condb.getDataTable(sql_loadtinh));

                string sql_loadtinh = "SELECT DISTINCT hc_tinhcode, hc_tinhname FROM hosobenhan WHERE hc_tinhcode <> '' and hc_tinhname <>''  ORDER BY hc_tinhname;";
                dv_tinh = new DataView(condb.getDataTable(sql_loadtinh));
                cboTinh.DataSource = dv_tinh;
                cboTinh.DisplayMember = "hc_tinhname";
                cboTinh.ValueMember = "hc_tinhcode";

                //if (txtTinh.Text.Trim() != "")
                //{
                //    string sql_loadhuyen = "SELECT DISTINCT hc_huyencode, hc_huyenname FROM hosobenhan WHERE hc_huyencode <> '' and hc_huyenname <>'' and hc_tinhcode='" + txtTinh.Text.Trim() + "' ;";
                //    DataView dv_huyen = new DataView(condb.getDataTable(sql_loadhuyen));
                //    cboHuyen.DataSource = dv_huyen;
                //    cboHuyen.DisplayMember = "hc_huyenname";
                //    cboHuyen.ValueMember = "hc_huyencode";
                //}

                //if (txtHuyen.Text.Trim() != "")
                //{
                //    string sql_loadxa = "SELECT DISTINCT hc_xacode, hc_xaname FROM hosobenhan WHERE hc_xacode <> '' and hc_xaname <>'' and hc_tinhcode='" + txtHuyen.Text.Trim() + "' ;";
                //    DataView dv_xa = new DataView(condb.getDataTable(sql_loadxa));

                //    cboXa.DataSource = dv_xa;
                //    cboXa.DisplayMember = "hc_xaname";
                //    cboXa.ValueMember = "hc_xacode";
                //}

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        //Cap nhat thong tin hanh chinh
        private void btnSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                try
                {
                    string datengaysinh = DateTime.ParseExact(dtNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                    string datenamsinh = datengaysinh.Substring(0,4);
                    // Querry thực hiện
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin về BN:\n " + PatientName + " - " + patientid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string sqlupdate_ttbn = "UPDATE hosobenhan SET patientname = '" + txtPatientName.Text.Trim() + "', birthday='" + datengaysinh + "', birthday_year='" + datenamsinh + "', gioitinhcode = '" + txtGioiTinh.Text.Trim() + "', gioitinhname='" + cbbGioiTinh.Text.Trim() + "', hc_tinhcode = '" + txtTinh.Text.Trim() + "', hc_tinhname='" + cboTinh.Text.Trim() + "', hc_huyencode='" + txtHuyen.Text.Trim() + "', hc_huyenname='" + cboHuyen.Text.Trim() + "', hc_xacode='" + txtXa.Text.Trim() + "', hc_xaname='" + cboXa.Text.Trim() + "', hc_sonha='" + txtSoNha.Text.Trim() + "', hc_thon='" + txtThonPho.Text.Trim() + "', noilamviec='" + txtNoiLamViec.Text.Trim() + "' WHERE hosobenhanid = '" + hosobenhanid + "';";
                        //Log
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thông tin về BN từ: " + PatientName + "; " + NgaySinh + "; " + GioiTinh + "; " + tinh_code + "; " + tinh_name + "; " + huyen_code + "; " + huyen_name + "; " + xa_code + "; " + xa_name + "; " + SoNha + "; " + ThonPho + "; " + NoiLamViec + " thành: " + txtPatientName.Text + "; " + datengaysinh + "; " + cbbGioiTinh.Text + "; " + txtTinh.Text + "; " + cboTinh.Text + "; " + txtHuyen.Text + "; " + cboHuyen.Text + "; " + txtXa.Text + "; " + cboXa.Text + "; " + txtSoNha.Text + "; " + txtThonPho.Text + "; " + txtNoiLamViec.Text + "  ' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                        condb.ExecuteNonQuery(sqlupdate_ttbn);
                        condb.ExecuteNonQuery(sqlinsert_log);
                        MessageBox.Show("Sửa thông tin về bệnh nhân thành công!", "Thông báo !");
                        //this.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        //Cap nhat the BHYT
        private void btnSuaThongTinBHYT_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                string datetungay = DateTime.ParseExact(dtHanTheTu.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                string datedenngay = DateTime.ParseExact(dtHanTheDen.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                // Querry thực hiện
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin về thẻ BHYT BN:\n " + PatientName + " - " + patientid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string sqlupdate_bhyt = "UPDATE bhyt SET bhytcode = '" + txtSoTheBHYT.Text.Trim() + "', macskcbbd = '" + txtNoiDKKCBBD.Text.Trim() + "', bhytfromdate = '" + datetungay + "', bhytutildate = '" + datedenngay + "' WHERE bhytid = '" + bhytid + "';";
                    //Log
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thông tin về thẻ BHYT từ: " + SoTheBHYT + "; " + HanTheTu + "; " + HanTheDen + "; " + NoiDKKCBBD + " thành: " + txtSoTheBHYT.Text + "; " + datetungay + "; " + datedenngay + "; " + NoiDKKCBBD + "' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                    condb.ExecuteNonQuery(sqlupdate_bhyt);
                    condb.ExecuteNonQuery(sqlinsert_log);
                    MessageBox.Show("Sửa thông tin về thẻ bảo hiểm y tế thành công!", "Thông báo !");
                    this.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SuKienThayDoiTrangThai_BHYT()
        {
            try
            {
                //string datetungay = DateTime.ParseExact(dtHanTheTu.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                //string datetungay = DateTime.ParseExact(dtHanTheTu.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                //string datedenngay = DateTime.ParseExact(dtHanTheDen.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

                if (txtSoTheBHYT.Text.Trim() != SoTheBHYT || dtHanTheTu.Text != HanTheTu || dtHanTheDen.Text != HanTheDen || txtNoiDKKCBBD.Text.Trim() != NoiDKKCBBD)
                {
                    btnSuaThongTinBHYT.Enabled = true;
                }
                if (txtSoTheBHYT.Text.Trim() == SoTheBHYT && dtHanTheTu.Text == HanTheTu && dtHanTheDen.Text == HanTheDen && txtNoiDKKCBBD.Text.Trim() == NoiDKKCBBD)
                {
                    btnSuaThongTinBHYT.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SuKienThayDoiTrangThai_BenhNhan()
        {
            try
            {
                if (txtPatientName.Text.Trim() != PatientName || dtNgaySinh.Text != NgaySinh || cbbGioiTinh.Text != GioiTinh || txtSoNha.Text != SoNha || txtThonPho.Text != ThonPho || txtNoiLamViec.Text != NoiLamViec || cboXa.Text != xa_name || cboHuyen.Text != huyen_name || cboTinh.Text != tinh_name)
                {
                    btnSuaThongTinBN.Enabled = true;
                }
                if (txtPatientName.Text.Trim() == PatientName && dtNgaySinh.Text == NgaySinh && cbbGioiTinh.Text == GioiTinh && txtSoNha.Text == SoNha && txtThonPho.Text == ThonPho && txtNoiLamViec.Text == NoiLamViec && cboXa.Text == xa_name && cboHuyen.Text == huyen_name && cboTinh.Text == tinh_name)
                {
                    btnSuaThongTinBN.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Su kien Editvalue change
        private void txtSoTheBHYT_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BHYT();
        }

        private void dtHanTheTu_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BHYT();
        }

        private void dtHanTheDen_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BHYT();
        }

        private void txtNoiDKKCBBD_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BHYT();
        }

        private void txtPatientName_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BenhNhan();
        }

        private void dtNgaySinh_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BenhNhan();
        }

        private void dtGioiTinh_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BenhNhan();
        }

        private void txtSoNha_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BenhNhan();
        }

        private void txtThonPho_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BenhNhan();
        }

        private void txtNoiLamViec_EditValueChanged(object sender, EventArgs e)
        {
            SuKienThayDoiTrangThai_BenhNhan();
        }

        private void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cboTinh.Text != tinh_name)
                //{
                SuKienThayDoiTrangThai_BenhNhan();
                for (int i = 0; i < dv_tinh.Count; i++)
                {
                    if (dv_tinh[i]["hc_tinhname"] == cboTinh.Text)
                    {
                        txtTinh.Text = dv_tinh[i]["hc_tinhcode"].ToString();
                    }
                }
                if (txtTinh.Text.Trim() != "")
                {
                    LoadDataHuyen_VeMay(txtTinh.Text.Trim());
                }
                //}         
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cboHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cboHuyen.Text != huyen_name)
                //{
                    SuKienThayDoiTrangThai_BenhNhan();
                    for (int i = 0; i < dv_huyen.Count; i++)
                    {
                        if (dv_huyen[i]["hc_huyenname"] == cboHuyen.Text)
                        {
                            txtHuyen.Text = dv_huyen[i]["hc_huyencode"].ToString();
                        }
                    }
                    if (txtTinh.Text.Trim() != "" && txtHuyen.Text.Trim() != "")
                    {
                        LoadDataXa_VeMay(txtTinh.Text.Trim(), txtHuyen.Text.Trim());
                    }
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cboXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cboXa.Text != xa_name)
                //{
                    SuKienThayDoiTrangThai_BenhNhan();
                    for (int i = 0; i < dv_xa.Count; i++)
                    {
                        if (dv_xa[i]["hc_xaname"] == cboXa.Text)
                        {
                            txtXa.Text = dv_xa[i]["hc_xacode"].ToString();
                        }
                    }
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cbbGioiTinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbGioiTinh.Text.Trim() == "Nam")
                {
                    txtGioiTinh.Text = "01";
                }
                else if (cbbGioiTinh.Text.Trim() == "Nữ")
                {
                    txtGioiTinh.Text = "02";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Bat du lieu nhap vao
        private void txtNoiDKKCBBD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtHuyen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtXa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        //private void cboTinh_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LoadDataHuyen_VeMay();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //private void cboHuyen_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LoadDataXa_VeMay();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //private void cboXa_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtXa.Text = cboXa.ValueMember;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void LoadDataHuyen_VeMay(string tinh_code_tk)
        {
            try
            {
                if (txtTinh.Text.Trim() != "")
                {
                    string sql_loadhuyen = "SELECT DISTINCT hc_huyencode, hc_huyenname FROM hosobenhan WHERE hc_huyencode <> '' and hc_huyenname <>'' and hc_tinhcode='" + tinh_code_tk + "'  ORDER BY hc_huyenname;";
                    dv_huyen = new DataView(condb.getDataTable(sql_loadhuyen));
                    cboHuyen.DataSource = dv_huyen;
                    cboHuyen.DisplayMember = "hc_huyenname";
                    cboHuyen.ValueMember = "hc_huyencode";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void LoadDataXa_VeMay(string tinh_code_tk, string huyen_code_tk)
        {
            try
            {
                if (txtHuyen.Text.Trim() != "")
                {
                    string sql_loadxa = "SELECT DISTINCT hc_xacode, hc_xaname FROM hosobenhan WHERE hc_xacode <> '' and hc_xaname <>'' and hc_tinhcode='" + tinh_code_tk + "' and hc_huyencode='" + huyen_code_tk + "' ORDER BY hc_xaname ;";
                    dv_xa = new DataView(condb.getDataTable(sql_loadxa));

                    cboXa.DataSource = dv_xa;
                    cboXa.DisplayMember = "hc_xaname";
                    cboXa.ValueMember = "hc_xacode";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region KeyDown
        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dtNgaySinh.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dtNgaySinh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cbbGioiTinh.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbbGioiTinh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboTinh.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void cboTinh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboHuyen.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void cboHuyen_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cboXa.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void cboXa_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtSoNha.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtSoNha_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtThonPho.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtThonPho_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNoiLamViec.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtNoiLamViec_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSuaThongTinBN.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtSoTheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dtHanTheTu.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dtHanTheTu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dtHanTheDen.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dtHanTheDen_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNoiDKKCBBD.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtNoiDKKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSuaThongTinBHYT.Focus();
                }
            }
            catch (Exception)
            {
            }
        }
#endregion



    }
}
