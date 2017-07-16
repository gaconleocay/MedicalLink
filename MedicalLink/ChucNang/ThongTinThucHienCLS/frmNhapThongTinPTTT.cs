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

namespace MedicalLink.ChucNang.ThongTinThucHienCLS
{
    public partial class frmNhapThongTinPTTT : Form
    {
        #region Khai bao
        private classNhapThongTinPTTT currentThongtinPTTT;
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private long currentthuchienclsid;
        #endregion
        public frmNhapThongTinPTTT()
        {
            InitializeComponent();
        }

        public frmNhapThongTinPTTT(classNhapThongTinPTTT thongtinPTTT)
        {
            try
            {
                InitializeComponent();
                this.currentThongtinPTTT = thongtinPTTT;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #region Load
        private void frmNhapThongTinPTTT_Load(object sender, EventArgs e)
        {
            try
            {
                Load_ThongTinVeBenhNhan();
                Load_PhuongPhapPTTT();
                Load_LoaiPTTT();
                Load_NguoiThucHien();
                dtTGThucHien.DateTime = DateTime.Now;
                Load_ThongTinThucHienCanLamSangDichVu();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void Load_ThongTinVeBenhNhan()
        {
            try
            {
                string laythongtin = "select hsba.patientcode, hsba.patientname, TO_CHAR(hsba.hosobenhandate, 'HH:mm yyyy-MM-dd') as hosobenhandate, to_char(hsba.birthday, 'yyyy') as namsinh, hsba.nghenghiepname, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, hsba.chandoanravien_code, hsba.chandoanravien, hsba.chandoanravien_kemtheo_code, hsba.chandoanravien_kemtheo from hosobenhan hsba where hsba.hosobenhanid=" + this.currentThongtinPTTT.hosobenhanid + ";";
                DataTable dataThongTin = condb.GetDataTable_HIS(laythongtin);
                if (dataThongTin != null && dataThongTin.Rows.Count > 0)
                {
                    txtVaoVienLuc.Text = dataThongTin.Rows[0]["hosobenhandate"].ToString();
                    txtMaBenhNhan.Text = dataThongTin.Rows[0]["patientcode"].ToString();
                    txtTenBenhNhan.Text = dataThongTin.Rows[0]["patientname"].ToString();
                    txtNamSinh.Text = dataThongTin.Rows[0]["namsinh"].ToString();
                    txtNgheNghiep.Text = dataThongTin.Rows[0]["nghenghiepname"].ToString();
                    txtDiaChi.Text = dataThongTin.Rows[0]["diachi"].ToString();
                    txtChanDoanChinh_ICD.Text = dataThongTin.Rows[0]["chandoanravien_code"].ToString();
                    txtChanDoanChinh_Ten.Text = dataThongTin.Rows[0]["chandoanravien"].ToString();
                    txtChanDoanPhu_ICD.Text = dataThongTin.Rows[0]["chandoanravien_kemtheo"].ToString();
                    txtChanDoanPhu_Ten.Text = dataThongTin.Rows[0]["chandoanravien_kemtheo_code"].ToString();
                }
                txtPhongChiDinh.Text = this.currentThongtinPTTT.departmentname;
                txtChanDoanCLS_Ten.Text = this.currentThongtinPTTT.chandoancls_name;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void Load_PhuongPhapPTTT()
        {
            try
            {
                string getphuongphap = "select pttt_phuongphapdbid, pttt_phuongphapcode, pttt_phuongphapname from pttt_phuongphap order by pttt_phuongphapname;";
                DataTable dataPhuongPhap = condb.GetDataTable_HIS(getphuongphap);
                cboPhuongPhapPTTT.Properties.DataSource = dataPhuongPhap;
                cboPhuongPhapPTTT.Properties.DisplayMember = "pttt_phuongphapname";
                cboPhuongPhapPTTT.Properties.ValueMember = "pttt_phuongphapcode";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void Load_LoaiPTTT()
        {
            try
            {
                List<classLoaiPTTTDTO> lstLoaiPTTT = new List<classLoaiPTTTDTO>();
                classLoaiPTTTDTO loaipttt_1 = new classLoaiPTTTDTO();
                loaipttt_1.loaiptttid = 1;
                loaipttt_1.loaiptttten = "Đặc biệt";
                lstLoaiPTTT.Add(loaipttt_1);
                classLoaiPTTTDTO loaipttt_2 = new classLoaiPTTTDTO();
                loaipttt_2.loaiptttid = 2;
                loaipttt_2.loaiptttten = "Loại 1A";
                lstLoaiPTTT.Add(loaipttt_2);
                classLoaiPTTTDTO loaipttt_3 = new classLoaiPTTTDTO();
                loaipttt_3.loaiptttid = 3;
                loaipttt_3.loaiptttten = "Loại 1B";
                lstLoaiPTTT.Add(loaipttt_3);
                classLoaiPTTTDTO loaipttt_4 = new classLoaiPTTTDTO();
                loaipttt_4.loaiptttid = 4;
                loaipttt_4.loaiptttten = "Loại 1C";
                lstLoaiPTTT.Add(loaipttt_4);
                classLoaiPTTTDTO loaipttt_5 = new classLoaiPTTTDTO();
                loaipttt_5.loaiptttid = 5;
                loaipttt_5.loaiptttten = "Loại 2A";
                lstLoaiPTTT.Add(loaipttt_5);
                classLoaiPTTTDTO loaipttt_6 = new classLoaiPTTTDTO();
                loaipttt_6.loaiptttid = 6;
                loaipttt_6.loaiptttten = "Loại 2B";
                lstLoaiPTTT.Add(loaipttt_6);
                classLoaiPTTTDTO loaipttt_7 = new classLoaiPTTTDTO();
                loaipttt_7.loaiptttid = 7;
                loaipttt_7.loaiptttten = "Loại 2C";
                lstLoaiPTTT.Add(loaipttt_7);
                classLoaiPTTTDTO loaipttt_9 = new classLoaiPTTTDTO();
                loaipttt_9.loaiptttid = 9;
                loaipttt_9.loaiptttten = "Loại 1";
                lstLoaiPTTT.Add(loaipttt_9);
                classLoaiPTTTDTO loaipttt_10 = new classLoaiPTTTDTO();
                loaipttt_10.loaiptttid = 10;
                loaipttt_10.loaiptttten = "Loại 2";
                lstLoaiPTTT.Add(loaipttt_10);
                classLoaiPTTTDTO loaipttt_8 = new classLoaiPTTTDTO();
                loaipttt_8.loaiptttid = 8;
                loaipttt_8.loaiptttten = "Loại 3";
                lstLoaiPTTT.Add(loaipttt_8);

                cboLoaiPTTT.Properties.DataSource = lstLoaiPTTT;
                cboLoaiPTTT.Properties.DisplayMember = "loaiptttten";
                cboLoaiPTTT.Properties.ValueMember = "loaiptttid";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void Load_NguoiThucHien()
        {
            try
            {
                string getnguoithuchien = "select nv.userhisid, nv.usercode, nv.username, (nv.usercode || ' - ' || nv.username) as usercodename from nhompersonnel nv inner join (select usercode, departmentid from tbldepartment) ude on ude.usercode=nv.usercode inner join (select departmentid from department where departmentgroupid=" + this.currentThongtinPTTT.departmentgroupid + ") de on de.departmentid=ude.departmentid group by nv.userhisid, nv.usercode, nv.username order by nv.username; ";
                DataTable dataNguoiThucHien = condb.GetDataTable_HIS(getnguoithuchien);

                cboMoChinh.Properties.DataSource = dataNguoiThucHien;
                cboMoChinh.Properties.DisplayMember = "usercodename";
                cboMoChinh.Properties.ValueMember = "userhisid";

                cboPhu1.Properties.DataSource = dataNguoiThucHien;
                cboPhu1.Properties.DisplayMember = "usercodename";
                cboPhu1.Properties.ValueMember = "userhisid";

                cboPhu2.Properties.DataSource = dataNguoiThucHien;
                cboPhu2.Properties.DisplayMember = "usercodename";
                cboPhu2.Properties.ValueMember = "userhisid";

                cboGayMe.Properties.DataSource = dataNguoiThucHien;
                cboGayMe.Properties.DisplayMember = "usercodename";
                cboGayMe.Properties.ValueMember = "userhisid";

                cboGiupViec1.Properties.DataSource = dataNguoiThucHien;
                cboGiupViec1.Properties.DisplayMember = "usercodename";
                cboGiupViec1.Properties.ValueMember = "userhisid";

                cboGiupViec2.Properties.DataSource = dataNguoiThucHien;
                cboGiupViec2.Properties.DisplayMember = "usercodename";
                cboGiupViec2.Properties.ValueMember = "userhisid";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void Load_ThongTinThucHienCanLamSangDichVu()
        {
            try
            {
                string kiemtrathuchien = "select thuchienclsid, medicalrecordid, patientid, maubenhphamid, servicepriceid, thuchienclsdate, phauthuatvien, bacsigayme, phumo1, phumo2, phumo3, phumo4, mota, pttt_hangid, phuongphappttt_code from thuchiencls where servicepriceid=" + this.currentThongtinPTTT.servicepriceid + ";";
                DataTable dataThucHIenCLS = condb.GetDataTable_HIS(kiemtrathuchien);
                if (dataThucHIenCLS != null && dataThucHIenCLS.Rows.Count > 0)
                {
                    this.currentthuchienclsid = Utilities.Util_TypeConvertParse.ToInt64(dataThucHIenCLS.Rows[0]["thuchienclsid"].ToString());
                    dtTGThucHien.DateTime = Utilities.Util_TypeConvertParse.ToDateTime(dataThucHIenCLS.Rows[0]["thuchienclsdate"].ToString());
                    cboLoaiPTTT.EditValue = dataThucHIenCLS.Rows[0]["pttt_hangid"];
                    cboPhuongPhapPTTT.EditValue = dataThucHIenCLS.Rows[0]["phuongphappttt_code"];

                    cboMoChinh.EditValue = dataThucHIenCLS.Rows[0]["phauthuatvien"];
                    cboPhu1.EditValue = dataThucHIenCLS.Rows[0]["phumo1"];
                    cboPhu2.EditValue = dataThucHIenCLS.Rows[0]["phumo2"];
                    cboGayMe.EditValue = dataThucHIenCLS.Rows[0]["bacsigayme"];
                    cboGiupViec1.EditValue = dataThucHIenCLS.Rows[0]["phumo3"];
                    cboGiupViec2.EditValue = dataThucHIenCLS.Rows[0]["phumo4"];
                    txtMoTa.Text = dataThucHIenCLS.Rows[0]["mota"].ToString() ;
                }
                else
                {
                    this.currentthuchienclsid = 0;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        //
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                string luulaithuchien = "";
                string thuchienclsdate = DateTime.ParseExact(dtTGThucHien.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (this.currentthuchienclsid == 0) //them moi
                {
                    luulaithuchien = "INSERT INTO thuchiencls(medicalrecordid, medicalrecordid_gmhs, patientid, maubenhphamid, servicepriceid, thuchienclsdate, phauthuatvien, bacsigayme, phumo1, phumo2, phumo3, phumo4, mota, pttt_hangid, phuongphappttt_code, phuongphappttt) VALUES ('" + this.currentThongtinPTTT.medicalrecordid + "', '" + this.currentThongtinPTTT.medicalrecordid + "', '" + this.currentThongtinPTTT.patientid + "', '" + this.currentThongtinPTTT.maubenhphamid + "', '" + this.currentThongtinPTTT.servicepriceid + "', '" + thuchienclsdate + "', '" + cboMoChinh.EditValue + "', '" + cboGayMe.EditValue + "', '" + cboPhu1.EditValue + "', '" + cboPhu2.EditValue + "', '" + cboGiupViec1.EditValue + "', '" + cboGiupViec2.EditValue + "', '" + txtMoTa.Text + "', '" + cboLoaiPTTT.EditValue + "', '" + cboPhuongPhapPTTT.EditValue + "', '" + cboPhuongPhapPTTT.Text + "');";
                }
                else
                {
                    luulaithuchien = "UPDATE thuchiencls SET thuchienclsdate='" + thuchienclsdate + "', phauthuatvien='" + cboMoChinh.EditValue + "',  bacsigayme = '" + cboGayMe.EditValue + "', phumo1 = '" + cboPhu1.EditValue + "', phumo2 = '" + cboPhu2.EditValue + "', phumo3 = '" + cboGiupViec1.EditValue + "', phumo4 = '" + cboGiupViec2.EditValue + "', mota = '" + txtMoTa.Text + "', pttt_hangid = '" + cboLoaiPTTT.EditValue + "', phuongphappttt_code = '" + cboPhuongPhapPTTT.EditValue + "', phuongphappttt = '" + cboPhuongPhapPTTT.Text + "' WHERE thuchienclsid = " + this.currentthuchienclsid + "; ";
                }
                if (condb.ExecuteNonQuery_HIS(luulaithuchien))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THAO_TAC_THANH_CONG);
                    frmthongbao.Show();
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
    }
}
