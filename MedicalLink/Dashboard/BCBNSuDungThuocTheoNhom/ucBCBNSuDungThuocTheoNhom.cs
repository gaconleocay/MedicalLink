using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.Utils.Menu;
using System.IO;
using System.Globalization;

namespace MedicalLink.Dashboard
{
    /// <summary>
    /// BC doanh thu theo khoa ra viện
    /// </summary>
    public partial class ucBCBNSuDungThuocTheoNhom : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataSDThuoc_Khoa { get; set; }
        private DataTable dataSDThuoc_TongHop { get; set; }

        #endregion

        #region Load
        public ucBCBNSuDungThuocTheoNhom()
        {
            InitializeComponent();
        }

        private void ucBCBNSuDungThuocTheoNhom_Load(object sender, EventArgs e)
        {
            dateTuNgay_Khoa.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay_Khoa.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            dateTuNgay_TongHop.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay_TongHop.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucKhoa();
            PhanQuyenTabChucNang();
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    cboKhoa.Properties.DataSource = lstDSKhoa;
                    cboKhoa.Properties.DisplayMember = "departmentgroupname";
                    cboKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    cboKhoa.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void PhanQuyenTabChucNang()
        {
            try
            {
                xtraTabPage_Khoa.PageVisible = MedicalLink.Base.CheckPermission.ChkPerModule("DASHBOARD_11");
                xtraTabPage_TongHop.PageVisible = MedicalLink.Base.CheckPermission.ChkPerModule("DASHBOARD_12");
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        private void bandedGridView_Khoa_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        #region Tab Theo Khoa
        private void btnTimKiem_Khoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
                else
                {
                    gridControlData_Khoa.DataSource = null;
                    LayDuLieuBaoCao_Khoa();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnExport_Khoa_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay_Khoa.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay_Khoa.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_BNSuDungThuocTheoNhom_Khoa.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataSDThuoc_Khoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Tab Tong Hop
        private void btnTimKiem_TongHop_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlData_TongHop.DataSource = null;
                LayDuLieuBaoCao_TongHop();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnExport_TongHop_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay_TongHop.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay_TongHop.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_BNSuDungThuocTheoNhom_TongHop.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataSDThuoc_TongHop);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion
    }
}
