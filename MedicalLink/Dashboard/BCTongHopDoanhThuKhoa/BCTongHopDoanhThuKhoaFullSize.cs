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

namespace MedicalLink.Dashboard.BCTongHopDoanhThuKhoa
{
    public partial class BCTongHopDoanhThuKhoaFullSize : Form
    {
        #region Khai bao
        private DataTable dataBaoCao { get; set; }
        private string tungaydenngay { get; set; }

        #endregion
        public BCTongHopDoanhThuKhoaFullSize()
        {
            InitializeComponent();
        }

        public BCTongHopDoanhThuKhoaFullSize(DataTable dataBaoCao, string tungaydenngay)
        {
            InitializeComponent();
            this.dataBaoCao = dataBaoCao;
            this.tungaydenngay = tungaydenngay;
            gridControlTTDTKhoa.DataSource = this.dataBaoCao;
            lblTenThongTinChiTiet.Text = "BÁO CÁO TỔNG HỢP DOANH THU KHOA";
        }

        #region Custom
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Events
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = this.tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_TongHopDoanhThuTheoKhoa_01.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

    }
}
