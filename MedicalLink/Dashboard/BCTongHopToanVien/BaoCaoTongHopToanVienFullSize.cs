using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.BCTongHopToanVien
{
    public partial class BaoCaoTongHopToanVienFullSize : Form
    {
        DataTable dataBaoCao { get; set; }
        string tungaydenngay { get; set; }
        int kieuxem { get; set; }
        public BaoCaoTongHopToanVienFullSize()
        {
            InitializeComponent();
        }
        public BaoCaoTongHopToanVienFullSize(DataTable dataBaoCao, string tungaydenngay, int kieuxem)
        {
            InitializeComponent();
            this.dataBaoCao = dataBaoCao;
            this.tungaydenngay = tungaydenngay;
            this.kieuxem = kieuxem;
            gridControlDataBNNT.DataSource = this.dataBaoCao;
            VisiableColumGridControl(kieuxem);
        }

        private void VisiableColumGridControl(int kieuxem)
        {
            try
            {
                if (kieuxem == 2)
                {
                    bandedGridColumn_loaivienphi.Visible = false;
                    bandedGridColumn_patientid.Visible = true;
                    bandedGridColumn_vienphiid.Visible = true;
                    bandedGridColumn_patientname.Visible = true;
                }
                else
                {
                    bandedGridColumn_loaivienphi.Visible = true;
                    bandedGridColumn_patientid.Visible = false;
                    bandedGridColumn_vienphiid.Visible = false;
                    bandedGridColumn_patientname.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = this.tungaydenngay;
                thongTinThem.Add(reportitem);
                string fileTemplatePath = "BC_TongHopToanVien_01.xlsx";

                if (this.kieuxem == 2)
                {
                    fileTemplatePath = "BC_TongHopToanVien_02.xlsx";
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
