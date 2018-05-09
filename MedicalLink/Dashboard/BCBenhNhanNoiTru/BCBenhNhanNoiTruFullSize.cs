using DevExpress.XtraGrid.Views.Grid;
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

namespace MedicalLink.Dashboard.BCBenhNhanNoiTru
{
    public partial class BCBenhNhanNoiTruFullSize : Form
    {
        DataTable dataBCPTTT { get; set; }
        string tungaydenngay { get; set; }
        public BCBenhNhanNoiTruFullSize()
        {
            InitializeComponent();
        }
        public BCBenhNhanNoiTruFullSize(DataView dataBCTongTheKhoa, string tungaydenngay)
        {
            InitializeComponent();
            this.tungaydenngay = tungaydenngay;
            this.dataBCPTTT = dataBCTongTheKhoa.Table;
            gridControlDataBNNT.DataSource = dataBCPTTT;
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataBCPTTT != null)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = this.tungaydenngay;
                    thongTinThem.Add(reportitem);

                    string fileTemplatePath = "BC_BenhNhanNoiTru_01.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void bandedGridViewDataBNNT_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }



    }
}
