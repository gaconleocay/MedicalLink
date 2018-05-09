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

namespace MedicalLink.BCQLTaiChinh
{
    public partial class ucBC101_TKTienKhamYCT7CN : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBC101_TKTienKhamYCT7CN()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC101_TKTienKhamYCT7CN_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlSoCDHA.DataSource = null;
            LoadDanhSachPhongThucHien();
        }
        private void LoadDanhSachPhongThucHien()
        {
            try
            {
                if (cboLoaiDichVu.Text == "Xét nghiệm")
                {
                    var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 6).OrderBy(p => p.departmenttype).ToList();
                    if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                    {
                        chkListDSPhongThucHien.Properties.DataSource = lstDSKhoa;
                        chkListDSPhongThucHien.Properties.DisplayMember = "departmentname";
                        chkListDSPhongThucHien.Properties.ValueMember = "departmentid";
                    }
                }
                else
                {
                    var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7).OrderBy(p => p.departmenttype).ToList();
                    if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                    {
                        chkListDSPhongThucHien.Properties.DataSource = lstDSKhoa;
                        chkListDSPhongThucHien.Properties.DisplayMember = "departmentname";
                        chkListDSPhongThucHien.Properties.ValueMember = "departmentid";
                    }
                }

                chkListDSPhongThucHien.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region TIm kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_tgchidinh_ser = "";
                string _tieuchi_tgchidinh_mbp = "";
                string _tieuchi_tgthuchien_mbp = "";
                string _phongthuchien = "";
                string sql_timkiem = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_tgchidinh_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_tgchidinh_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    _tieuchi_tgthuchien_mbp = " and maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //phogn thuc hien
                _phongthuchien = " and departmentid_des in (";
                List<Object> lstKhoaCheck = chkListDSPhongThucHien.Properties.Items.GetCheckedValues();
                for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                {
                    _phongthuchien += lstKhoaCheck[i] + ",";
                }
                _phongthuchien += lstKhoaCheck[lstKhoaCheck.Count - 1] + ") ";

                if (cboLoaiDichVu.Text == "Xét nghiệm")
                {
                    sql_timkiem = @"SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt, hsba.patientcode, hsba.patientname, ser.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT, mbp.chandoan as chandoantruocphauthuat, mbp.chandoan as chandoansauphauthuat, ser.servicepricecode, ser.servicepricename, ser.servicepricename as phuongphappttt, '' as pttt_phuongphapvocam, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, ser.servicepricedate, mbp.maubenhphamfinishdate as phauthuatthuthuatdate, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, ntkq_cc.username as phauthuatvien, bsgm.username as bacsigayme FROM (select servicepriceid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('03XN','07KTC') " + _tieuchi_tgchidinh_ser + " ) ser inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 " + _tieuchi_tgchidinh_mbp + _tieuchi_tgthuchien_mbp + _phongthuchien + ") mbp on mbp.maubenhphamid=ser.maubenhphamid left join (select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3 from thuchiencls) cls on cls.servicepriceid=ser.servicepriceid inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('03XN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.hosobenhanid=hsba.hosobenhanid left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme;";
                }
                else
                {
                    sql_timkiem = @"SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt, hsba.patientcode, hsba.patientname, ser.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT, mbp.chandoan as chandoantruocphauthuat, mbp.chandoan as chandoansauphauthuat, ser.servicepricecode, ser.servicepricename, ser.servicepricename as phuongphappttt, '' as pttt_phuongphapvocam, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, ser.servicepricedate, (case when se.servicetimetrakq<>'0001-01-01 00:00:00' then se.servicetimetrakq else ((case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end)) end) as phauthuatthuthuatdate, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, COALESCE(ntkq.username,ntkq_cc.username) as phauthuatvien, bsgm.username as bacsigayme FROM (select servicepriceid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN','07KTC') " + _tieuchi_tgchidinh_ser + " ) ser inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 " + _tieuchi_tgchidinh_mbp + _tieuchi_tgthuchien_mbp + _phongthuchien + ") mbp on mbp.maubenhphamid=ser.maubenhphamid left join (select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3 from thuchiencls) cls on cls.servicepriceid=ser.servicepriceid inner join (select servicepriceid,servicetimetrakq,serviceusertrakq from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)) se on se.servicepriceid=ser.servicepriceid inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.hosobenhanid=hsba.hosobenhanid left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme left join nhompersonnel ntkq ON ntkq.usercode=se.serviceusertrakq;";
                }
                this.dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
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

        #endregion

        #region In va xuat file
        private void tbnExport_Click(object sender, EventArgs e)
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
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                item_phong.value = chkListDSPhongThucHien.Text;
                thongTinThem.Add(item_phong);

                string fileTemplatePath = "BC_43_SoPhauThuatThuThuat_CLS.xlsx";

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
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
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                item_phong.value = chkListDSPhongThucHien.Text;
                thongTinThem.Add(item_phong);

                string fileTemplatePath = "BC_43_SoPhauThuatThuThuat_CLS.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        #endregion

        #region Custom
        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        private void cboLoaiDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachPhongThucHien();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion


    }
}
