using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang.GopBenhAn
{
    public partial class frmChonDotDieuTri : Form
    {
        private List<MedicalrecordGopBADTO> lst_Phong { get; set; }
        private long medicalrecordid_B { get; set; }
        private Base.ConnectDatabase condb = new Base.ConnectDatabase();
        // khai báo 1 hàm delegate
        public delegate void GetString(long medicalrecordid_A);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;



        public frmChonDotDieuTri()
        {
            InitializeComponent();
        }
        public frmChonDotDieuTri(List<MedicalrecordGopBADTO> _lst_Phong, long _medicalrecordid_B)
        {
            try
            {
                InitializeComponent();
                this.lst_Phong = _lst_Phong;
                this.medicalrecordid_B = _medicalrecordid_B;

                cboMaDieuTri.Properties.DataSource = this.lst_Phong;
                cboMaDieuTri.Properties.DisplayMember = "medicalrecordid";
                cboMaDieuTri.Properties.ValueMember = "medicalrecordid";
                if (this.lst_Phong.Count == 1)
                {
                    cboMaDieuTri.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {           
                if (cboMaDieuTri.EditValue != null && cboMaDieuTri.EditValue.ToString() != "")
                {
                    MyGetData(Utilities.Util_TypeConvertParse.ToInt64(cboMaDieuTri.EditValue.ToString()));
                    this.Close();
                    //    MedicalrecordGopBADTO _maDieuTri = this.lst_Phong.Where(o => o.medicalrecordid == Utilities.Util_TypeConvertParse.ToInt64(cboMaDieuTri.EditValue.ToString())).FirstOrDefault();

                    //   string _updateCoPhong = "update maubenhpham set vienphiid=" + _maDieuTri.vienphiid + ",hosobenhanid=" + _maDieuTri.hosobenhanid + ",medicalrecordid=" + _maDieuTri.medicalrecordid + ",patientid=" + _maDieuTri.patientid + " where medicalrecordid=" + this.medicalrecordid_B + "; update serviceprice set vienphiid=" + _maDieuTri.vienphiid + ",hosobenhanid=" + _maDieuTri.hosobenhanid + ",medicalrecordid=" + _maDieuTri.medicalrecordid + " where medicalrecordid=" + this.medicalrecordid_B + "; update bill set vienphiid=" + _maDieuTri.vienphiid + ",hosobenhanid=" + _maDieuTri.hosobenhanid + ",medicalrecordid=" + _maDieuTri.medicalrecordid + ",patientid=" + _maDieuTri.patientid + " where medicalrecordid=" + this.medicalrecordid_B + "; ";
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
    }
}
