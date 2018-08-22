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
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;
using MedicalLink.DatabaseProcess;
using DevExpress.XtraPrinting;

namespace MedicalLink.BaoCao
{
    public partial class ucBCTinhHinhRaVaoVien : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataDanhBenhNhan { get; set; }
        #endregion

        #region Load
        public ucBCTinhHinhRaVaoVien()
        {
            InitializeComponent();
        }

        private void ucBangKeTongHopHoaDon_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                this.dataDanhBenhNhan = new DataTable();
                string sql_getdata = "";
                string tieuchi_ravien = "";
                string tieuchi_vaovien = "";
                string orderby = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vaovien = " and thoigianvaovien between '" + tungay + "' and '" + denngay + "' ";
                    orderby = " mrd.thoigianvaovien ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_ravien = " and vienphidate_ravien between '"+tungay+"' and '"+denngay+"' ";
                    orderby = " vp.vienphidate_ravien ";
                }

                sql_getdata = "select row_number () over (order by " + orderby + ") as stt, vp.patientid, vp.vienphiid, hsba.patientname, hsba.namsinh, hsba.gioitinhname, bh.bhytcode, kvv.departmentgroupname as departmentgroupname_noitru, mrd.thoigianvaovien as vienphidate_noitru, (case when cast(to_char(mrd.thoigianvaovien, 'HH24MI') as numeric)>0 and cast(to_char(mrd.thoigianvaovien, 'HH24MI') as numeric)<1201 then 'Buổi sáng' else 'Buổi chiều' end) as buoi_noitru, krv.departmentgroupname as departmentgroupname_ravien, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when cast(to_char(vp.vienphidate_ravien, 'HH24MI') as numeric)>0 and cast(to_char(vp.vienphidate_ravien, 'HH24MI') as numeric)<1201 then 'Buổi sáng' else (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then 'Buổi chiều' end) end) as buoi_ravien, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as vienphistatus from (select patientid,vienphiid,hosobenhanid,vienphidate_ravien,departmentgroupid,vienphistatus,vienphistatus_vp,bhytid from vienphi where loaivienphiid=0 " + tieuchi_ravien + ") vp inner join (select hosobenhanid,patientname,to_char(birthday, 'yyyy') as namsinh,gioitinhname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid inner join (select hosobenhanid,departmentgroupid,thoigianvaovien from medicalrecord where hinhthucvaovienid=2 " + tieuchi_vaovien + ") mrd on mrd.hosobenhanid=vp.hosobenhanid inner join (select departmentgroupid,departmentgroupname from departmentgroup) kvv on kvv.departmentgroupid=mrd.departmentgroupid inner join (select departmentgroupid,departmentgroupname from departmentgroup) krv on krv.departmentgroupid=vp.departmentgroupid;";

                this.dataDanhBenhNhan = condb.GetDataTable_HIS(sql_getdata);

                if (this.dataDanhBenhNhan != null && this.dataDanhBenhNhan.Rows.Count > 0)
                {
                    gridControlDataBaoCao.DataSource = this.dataDanhBenhNhan;
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Xuat bao cao
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDataBaoCao.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "BC_BenhNhanRaVaoVien.xlsx";
                    //Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    //export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataDanhBenhNhan);
                    DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;

                thongTinThem.Add(reportitem);
                string fileTemplatePath = "BC_BenhNhanRaVaoVien.xlsx";
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();

        }

        private void gridViewDSHoaDon_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
