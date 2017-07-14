using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.BaoCao
{
    public partial class ucSoPhauThuatThuThuat : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucSoPhauThuatThuThuat()
        {
            InitializeComponent();
        }

        #region Load
        private void ucSoPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlSoCDHA.DataSource = null;
        }

        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi_thoigianchidinh = "";
                string tieuchi_thoigianthuchien = "";
                string khoachidinh = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_thoigianchidinh = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi_thoigianthuchien = " WHERE pttt.phauthuatthuthuatdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    khoachidinh = " and departmentgroupid=14 ";
                }

                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt, hsba.patientcode, hsba.patientname, ser.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as COBHYT, (pttt.chandoantruocphauthuat || (case when pttt.chandoantruocphauthuat_code<>'' then ' (' || pttt.chandoantruocphauthuat_code || ')' end)) as chandoantruocphauthuat, (pttt.chandoansauphauthuat || (case when pttt.chandoantruocphauthuat_code<>'' then ' (' || pttt.chandoansauphauthuat_code || ')' end)) as chandoansauphauthuat, ser.servicepricecode, ser.servicepricename, pttt.phuongphappttt, (case pttt.pttt_phuongphapvocamid when 1 then 'Gây mê tĩnh mạch' when 2 then 'Gây mê nội khí quản' when 3 then 'Gây mê tại chỗ' when 4 then 'Tiền mê + gây tê tại chỗ' when 5 then 'Gây tê tủy sống' when 6 then 'Gây tê' when 7 then 'Gây tê màng ngoài cứng' when 8 then 'Gây tê đám rối thần kinh' when 9 then 'Gây tê Codan' when 10 then 'Gây tê nhãn cầu' when 11 then 'Gây tê cạnh sống' when 99 then 'Khác' end) as pttt_phuongphapvocam, ser.servicepricedate, pttt.phauthuatthuthuatdate, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, bspt.username as phauthuatvien, bsgm.username as bacsigayme, giupviec.username as giupviec, nnhap.username as ghichu FROM (select servicepriceid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') " + tieuchi_thoigianchidinh + khoachidinh + " ) ser left join (select servicepriceid,chandoantruocphauthuat_code,chandoantruocphauthuat,chandoansauphauthuat_code,chandoansauphauthuat,phauthuatthuthuatdate,phuongphappttt,pttt_phuongphapvocamid,pttt_hangid,phauthuatvien,bacsigayme,phumo3,userid from phauthuatthuthuat pttt) pttt on pttt.servicepriceid=ser.servicepriceid inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid not in (1,2,3,4)) serf on serf.servicepricecode=ser.servicepricecode inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid left join tools_tblnhanvien bspt on bspt.userhisid=pttt.phauthuatvien left join tools_tblnhanvien bsgm on bsgm.userhisid=pttt.bacsigayme left join tools_tblnhanvien giupviec on giupviec.userhisid=pttt.phumo3 left join tools_tblnhanvien nnhap on nnhap.userhisid=pttt.userid " + tieuchi_thoigianthuchien + ";  ";
                dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = this.dataBaoCao;
                }
                else
                {
                    gridControlSoCDHA.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                string fileTemplatePath = "So_PhauThuatThuThuat_CacKhoa.xlsx";
                if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    fileTemplatePath = "So_PhauThuatThuThuat_ThanNhanTao.xlsx";
                }

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void cboLoaiSoThuThuat_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiSoThuThuat.Text == "Thủ thuật các khoa")
                {
                    bandedGridColumn_PTVien.Caption = "Bác sỹ thủ thuật";
                    bandedGridColumn_GayMe.Caption = "Bác sỹ gây mê, tê";
                    bandedGridColumn_GiupViec.Visible = false;
                }
                else if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    bandedGridColumn_PTVien.Caption = "Chính";
                    bandedGridColumn_GayMe.Caption = "Phụ";
                    bandedGridColumn_GiupViec.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;

                thongTinThem.Add(reportitem);

                string fileTemplatePath = "So_PhauThuatThuThuat_CacKhoa.xlsx";
                if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    fileTemplatePath = "So_PhauThuatThuThuat_ThanNhanTao.xlsx";
                }

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate_ReportTemps("", fileTemplatePath, thongTinThem, this.dataBaoCao);

                DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                spreadsheetControl.AllowDrop = false;
                string fullPath = Environment.CurrentDirectory + "\\ReportTemps\\" + fileTemplatePath;
                spreadsheetControl.LoadDocument(fullPath, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl.Document;
                spreadsheetControl.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
    }
}
