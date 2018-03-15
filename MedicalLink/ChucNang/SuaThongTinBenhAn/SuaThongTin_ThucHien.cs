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
        #region Khai bao
        ucSuaThongTinBenhAn SuaThongTinBenhAn;
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //DataView dv_tinhhuyenxa;
        // DataView dv_tinh, dv_huyen, dv_xa;

        long vienphiid, patientid, bhytid, hosobenhanid, medicalrecordid;
        string PatientName, NgaySinh, GioiTinh_Code, GioiTinh, SoNha, ThonPho, NoiLamViec, TrangThai;
        string SoTheBHYT, HanTheTu, HanTheDen, NoiDKKCBBD, NoiChuyenDen, SoTheBHYT_2, HanTheTu_2, HanTheDen_2, NoiDKKCBBD_2;
        string xa_code, huyen_code, tinh_code, xa_name, huyen_name, tinh_name;
        #endregion

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

                    int rowHandle = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.FocusedRowHandle;

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
                    //
                    SoTheBHYT_2 = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "theghep_bhytcode").ToString();
                    HanTheTu_2 = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "theghep_bhytfromdate").ToString();
                    HanTheDen_2 = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "theghep_bhytutildate").ToString();
                    NoiDKKCBBD_2 = SuaThongTinBenhAn.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "theghep_macskcbbd").ToString();

                    LoadThongTinBenhNhan();
                    LoadXaHuyenTinh();
                    LoadThongTinVeTheBHYT();
                    btnSuaThongTinBN.Enabled = false;
                    btnSuaThongTinBHYT.Enabled = false;
                    if (cboTinh.EditValue != null)
                    {
                        LoadDataHuyen_VeMay(cboTinh.EditValue.ToString());
                    }
                    if (cboTinh.EditValue != null && cboHuyen.EditValue != null)
                    {
                        LoadDataXa_VeMay(cboTinh.EditValue.ToString(), cboHuyen.EditValue.ToString());
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

                //Load noi chuyen den
                string sqlnoichuyenden = "select medicalrecordid, noigioithieucode from medicalrecord where loaibenhanid=24 and hosobenhanid=" + this.hosobenhanid + " order by medicalrecordid limit 1;";
                DataTable dataNCD = condb.GetDataTable_HIS(sqlnoichuyenden);
                if (dataNCD != null && dataNCD.Rows.Count > 0)
                {
                    this.NoiChuyenDen = dataNCD.Rows[0]["noigioithieucode"].ToString();
                    txtNoiChuyenDen.Text = this.NoiChuyenDen;
                    cboNoiChuyenDen.EditValue = this.NoiChuyenDen;
                    this.medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(dataNCD.Rows[0]["medicalrecordid"].ToString());
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadXaHuyenTinh()
        {
            try
            {
                cboTinh.EditValue = tinh_code;
                cboHuyen.EditValue = huyen_code;
                cboXa.EditValue = xa_code;
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
                dtHanTheTu.EditValue = Convert.ToDateTime(HanTheTu);
                dtHanTheDen.EditValue = Convert.ToDateTime(HanTheDen);
                txtNoiDKKCBBD.Text = NoiDKKCBBD;
                //   
                if (SoTheBHYT_2 != null && SoTheBHYT_2 != "")
                {
                    txtSoTheBHYT_2.Text = SoTheBHYT_2;
                    dtHanTheTu_2.EditValue = Convert.ToDateTime(HanTheTu_2);
                    dtHanTheDen_2.EditValue = Convert.ToDateTime(HanTheDen_2);
                    txtNoiDKKCBBD_2.Text = NoiDKKCBBD_2;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void LoadDataTinh_VeMay()
        {
            try
            {
                cboTinh.Properties.DataSource = SuaThongTinBenhAn.lstDanhMucTinhHuyenXa.GroupBy(o => o.hc_tinhcode).Select(n => n.First()).ToList();
                cboTinh.Properties.DisplayMember = "hc_tinhname";
                cboTinh.Properties.ValueMember = "hc_tinhcode";

                cboNoiChuyenDen.Properties.DataSource = SuaThongTinBenhAn.lstDanhMucSoSoYTe;
                cboNoiChuyenDen.Properties.DisplayMember = "benhvienname";
                cboNoiChuyenDen.Properties.ValueMember = "benhvienkcbbd";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDataHuyen_VeMay(string tinh_code_tk)
        {
            try
            {
                cboHuyen.Properties.DataSource = SuaThongTinBenhAn.lstDanhMucTinhHuyenXa.Where(s => s.hc_tinhcode == tinh_code_tk).GroupBy(o => o.hc_huyencode).Select(n => n.First()).ToList();
                cboHuyen.Properties.DisplayMember = "hc_huyenname";
                cboHuyen.Properties.ValueMember = "hc_huyencode";
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
                cboXa.Properties.DataSource = SuaThongTinBenhAn.lstDanhMucTinhHuyenXa.Where(s => s.hc_tinhcode == tinh_code_tk && s.hc_huyencode == huyen_code_tk).GroupBy(o => o.hc_xacode).Select(n => n.First()).ToList();
                cboXa.Properties.DisplayMember = "hc_xaname";
                cboXa.Properties.ValueMember = "hc_xacode";
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Events
        private void btnSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                try
                {
                    string datengaysinh = DateTime.ParseExact(dtNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                    // string datenamsinh = datengaysinh.Substring(0,4);
                    string datenamsinh = "0";
                    string noichuyendencode = "";
                    // Querry thực hiện
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin về BN:\n " + PatientName + " - " + patientid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string sqlupdate_ttbn = "UPDATE hosobenhan SET patientname = '" + txtPatientName.Text.Trim() + "', birthday='" + datengaysinh + "', birthday_year='" + datenamsinh + "', gioitinhcode = '" + txtGioiTinh.Text.Trim() + "', gioitinhname='" + cbbGioiTinh.Text.Trim() + "', hc_tinhcode = '" + cboTinh.EditValue.ToString() + "', hc_tinhname='" + cboTinh.Text.Trim() + "', hc_huyencode='" + cboHuyen.EditValue.ToString() + "', hc_huyenname='" + cboHuyen.Text.Trim() + "', hc_xacode='" + cboXa.EditValue.ToString() + "', hc_xaname='" + cboXa.Text.Trim() + "', hc_sonha='" + txtSoNha.Text.Trim() + "', hc_thon='" + txtThonPho.Text.Trim() + "', noilamviec='" + txtNoiLamViec.Text.Trim() + "' WHERE hosobenhanid = '" + hosobenhanid + "';";

                        if (cboNoiChuyenDen.EditValue != null)
                        {
                            noichuyendencode = cboNoiChuyenDen.EditValue.ToString();
                        }

                        //Cap nhat noi chuyen den
                        string update_noichuyenden = "UPDATE medicalrecord SET noigioithieucode='" + noichuyendencode + "' WHERE medicalrecordid=" + this.medicalrecordid + "; ";
                        //Log
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thông tin về BN từ: " + PatientName + "; " + NgaySinh + "; " + GioiTinh + "; " + tinh_code + "; " + tinh_name + "; " + huyen_code + "; " + huyen_name + "; " + xa_code + "; " + xa_name + "; " + SoNha + "; " + ThonPho + "; " + NoiLamViec + " thành: " + txtPatientName.Text + "; " + datengaysinh + "; " + cbbGioiTinh.Text + "; " + cboTinh.EditValue.ToString() + "; " + cboTinh.Text + "; " + cboTinh.EditValue.ToString() + "; " + cboHuyen.Text + "; " + cboXa.EditValue.ToString() + "; " + cboXa.Text + "; " + txtSoNha.Text + "; " + txtThonPho.Text + "; " + txtNoiLamViec.Text + "  ' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_13');";
                        string sqlinsert_log2 = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thông tin Nơi chuyển đến từ: " + this.NoiChuyenDen + " thành: " + noichuyendencode + " ' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_13');";
                        condb.ExecuteNonQuery_HIS(sqlupdate_ttbn);
                        condb.ExecuteNonQuery_HIS(update_noichuyenden);
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        condb.ExecuteNonQuery_MeL(sqlinsert_log2);
                        MessageBox.Show("Sửa thông tin về bệnh nhân thành công!", "Thông báo !");
                        //this.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Base.Logging.Error(ex);
                }
            }
            catch (Exception)
            {

            }
        }
        private void btnSuaThongTinBHYT_Click(object sender, EventArgs e)
        {
            string _updateThe2 = "";
            try
            {
                string datetungay = DateTime.ParseExact(dtHanTheTu.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                string datedenngay = DateTime.ParseExact(dtHanTheDen.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin về thẻ BHYT BN:\n " + PatientName + " - " + patientid + " ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    if (txtSoTheBHYT_2.Text != this.SoTheBHYT_2)
                    {
                        string datetungay_2 = DateTime.ParseExact(dtHanTheTu_2.Text==""? "01/01/0001": dtHanTheTu_2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";
                        string datedenngay_2 = DateTime.ParseExact(dtHanTheDen_2.Text == "" ? "01/01/0001" : dtHanTheDen_2.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00";

                        _updateThe2 = " UPDATE vienphi SET theghep_bhytcode = '" + txtSoTheBHYT_2.Text.Trim() + "', theghep_macskcbbd = '" + txtNoiDKKCBBD_2.Text.Trim() + "', theghep_bhytfromdate = '" + datetungay_2 + "', theghep_bhytutildate = '" + datedenngay_2 + "' WHERE vienphiid = '" + this.vienphiid + "'; ";
                    }

                    string sqlupdate_bhyt = "UPDATE bhyt SET bhytcode = '" + txtSoTheBHYT.Text.Trim() + "', macskcbbd = '" + txtNoiDKKCBBD.Text.Trim() + "', bhytfromdate = '" + datetungay + "', bhytutildate = '" + datedenngay + "' WHERE bhytid = '" + bhytid + "'; " + _updateThe2 + " ";

                    if (condb.ExecuteNonQuery_HIS(sqlupdate_bhyt))
                    {
                        //Log
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa thông tin về thẻ BHYT từ: " + SoTheBHYT + "; " + HanTheTu + "; " + HanTheDen + "; " + NoiDKKCBBD + " thành: " + txtSoTheBHYT.Text + "; " + datetungay + "; " + datedenngay + "; " + NoiDKKCBBD + "' , '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_13');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        MessageBox.Show("Sửa thông tin về thẻ bảo hiểm y tế thành công!", "Thông báo !");
                        this.Visible = false;
                    }
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
                if (txtSoTheBHYT.Text.Trim() != SoTheBHYT || dtHanTheTu.Text != HanTheTu || dtHanTheDen.Text != HanTheDen || txtNoiDKKCBBD.Text.Trim() != NoiDKKCBBD || txtSoTheBHYT_2.Text.Trim() != SoTheBHYT_2 || dtHanTheTu_2.Text != HanTheTu_2 || dtHanTheDen_2.Text != HanTheDen_2 || txtNoiDKKCBBD_2.Text.Trim() != NoiDKKCBBD_2)
                {
                    btnSuaThongTinBHYT.Enabled = true;
                }
                if (txtSoTheBHYT.Text.Trim() == SoTheBHYT && dtHanTheTu.Text == HanTheTu && dtHanTheDen.Text == HanTheDen && txtNoiDKKCBBD.Text.Trim() == NoiDKKCBBD && txtSoTheBHYT_2.Text.Trim() == SoTheBHYT_2 && dtHanTheTu_2.Text == HanTheTu_2 && dtHanTheDen_2.Text == HanTheDen_2 && txtNoiDKKCBBD_2.Text.Trim() == NoiDKKCBBD_2)
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

        #endregion

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

        private void cboNoiChuyenDen_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SuKienThayDoiTrangThai_BenhNhan();
                txtNoiChuyenDen.Text = cboNoiChuyenDen.EditValue.ToString();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
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
        #endregion

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
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
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
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void txtNoiChuyenDen_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SuKienThayDoiTrangThai_BenhNhan();
                    cboNoiChuyenDen.EditValue = txtNoiChuyenDen.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Custom

        private void cboTinh_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDataHuyen_VeMay(cboTinh.EditValue.ToString());
                SuKienThayDoiTrangThai_BenhNhan();
                cboXa.EditValue = null;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void cboHuyen_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDataXa_VeMay(cboTinh.EditValue.ToString(), cboHuyen.EditValue.ToString());
                SuKienThayDoiTrangThai_BenhNhan();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void cboXa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SuKienThayDoiTrangThai_BenhNhan();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

    }
}
