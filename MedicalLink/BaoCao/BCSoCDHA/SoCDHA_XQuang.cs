using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MedicalLink.BaoCao.BCSoCDHA
{
    public partial class SoCDHA_XQuang : DevExpress.XtraReports.UI.XtraReport
    {
        public SoCDHA_XQuang()
        {
            InitializeComponent();

        //            lblMaNhanVien.DataBindings.Add("Text", DataSource, "manv")
        //lblTenNhanVien.DataBindings.Add("Text", DataSource, "tennv")
        //lblNoiSinh.DataBindings.Add("Text", DataSource, "tinhthanh")
        //lblNgaySinh.DataBindings.Add("Text", DataSource, "ngaysinh").FormatString = "{0:dd/MM/yyyy}"
        //lblDiaChi.DataBindings.Add("Text", DataSource, "diachi1")

            col_stt.DataBindings.Add("Text", DataSource, "stt");
            col_patientcode.DataBindings.Add("Text", DataSource, "patientcode");
            col_patientname.DataBindings.Add("Text", DataSource, "patientname");

        }

    }
}
