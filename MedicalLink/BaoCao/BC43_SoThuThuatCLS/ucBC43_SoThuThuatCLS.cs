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
    public partial class ucBC43_SoThuThuatCLS : UserControl
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        //private DataTable dataBaoCao { get; set; }
        private string ThoiGianGioiHanDuLieu { get; set; }
        #endregion

        public ucBC43_SoThuThuatCLS()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC43_SoThuThuatCLS_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlSoCDHA.DataSource = null;
            LoadDanhSachPhongThucHien();
            LoadThoiGianGioiHanDuLieu();
        }
        private void LoadDanhSachPhongThucHien()
        {
            try
            {
                if (cboLoaiDichVu.Text == "Xét nghiệm")
                {
                    var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 6).OrderBy(p => p.departmenttype).ToList();
                    if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                    {
                        chkListDSPhongThucHien.Properties.DataSource = lstDSKhoa;
                        chkListDSPhongThucHien.Properties.DisplayMember = "departmentname";
                        chkListDSPhongThucHien.Properties.ValueMember = "departmentid";
                    }
                }
                else
                {
                    var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7).OrderBy(p => p.departmenttype).ToList();
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadThoiGianGioiHanDuLieu()
        {
            try
            {
                string _sqlDMDichVu = "select toolsoptionvalue from tools_option where toolsoptioncode='REPORT_43_TGLayDuLieu';";
                DataTable _dataTG = condb.GetDataTable_MeL(_sqlDMDichVu);
                if (_dataTG.Rows.Count > 0)
                {
                    this.ThoiGianGioiHanDuLieu = _dataTG.Rows[0]["toolsoptionvalue"].ToString();
                }
                else
                {
                    this.ThoiGianGioiHanDuLieu = "" + this.ThoiGianGioiHanDuLieu + "";
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_date_ser = " and servicepricedate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_date_mbp = " and maubenhphamdate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_date_vp = " and vienphidate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_date_thuchien = " and thuchienclsdate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_se = " and servicedate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_hsba = " and hosobenhandate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _join_thuchiencls = " left join ";
                string _tieuchi_pacs = " 1<>1 ";
                string _tieuchi_trakqtp = "";

                string _phongthuchien = "";
                string sql_timkiem = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //cboTieuChi
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_date_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_date_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày trả kết quả")
                {
                    _tieuchi_date_mbp = " and maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày trả kết quả từng phần")
                {
                    _tieuchi_trakqtp = " and tmp.servicetimetrakq between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_pacs = " 1=1 and to_timestamp(readingdate,'yyyyMMddHH24MIss') between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    _tieuchi_date_thuchien = " and thuchienclsdate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _join_thuchiencls = " inner join ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    _tieuchi_date_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày tiếp nhận")
                {
                    _tieuchi_date_mbp = " and maubenhphamdate_thuchien between '" + datetungay + "' and '" + datedenngay + "' ";
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
                    sql_timkiem = @"SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt, hsba.patientcode, hsba.patientname, ser.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT, mbp.chandoan as chandoantruocphauthuat, mbp.chandoan as chandoansauphauthuat, ser.servicepricecode, ser.servicepricename, ser.servicepricename as phuongphappttt, (case cls.ppvocamid when 1 then 'Gây mê tĩnh mạch' when 2 then 'Gây mê nội khí quản' when 3 then 'Gây tê tại chỗ' when 4 then 'Tiền mê + gây tê tại chỗ' when 5 then 'Gây tê tủy sống' when 6 then 'Gây tê' when 7 then 'Gây tê màng ngoài cứng' when 8 then 'Gây tê đám rối thần kinh' when 9 then 'Gây tê Codan' when 10 then 'Gây tê nhãn cầu' when 11 then 'Gây tê cạnh sống' when 12 then 'Gây tê hậu nhãn cầu' when 99 then 'Khác' end) as pttt_phuongphapvocam, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, ser.servicepricedate, mbp.maubenhphamfinishdate as phauthuatthuthuatdate, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, ntkq_cc.username as phauthuatvien, bsgm.username as bacsigayme FROM (select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') " + _tieuchi_date_ser + " ) ser inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 " + _tieuchi_date_mbp + _phongthuchien + ") mbp on mbp.maubenhphamid=ser.maubenhphamid " + _join_thuchiencls + " (select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3,ppvocamid from thuchiencls where 1=1 " + _tieuchi_date_thuchien + ") cls on cls.servicepriceid=ser.servicepriceid inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi where 1=1 " + _tieuchi_date_vp + " ) vp on vp.vienphiid=ser.vienphiid left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme;";
                }
                else//cdha
                {
                    sql_timkiem = $@"SELECT ROW_NUMBER () OVER (ORDER BY A.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		A.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
		(case when A.doituongbenhnhanid=1 then 'x' end) as cobhyt,
		A.chandoan as chandoantruocphauthuat,
		A.chandoan as chandoansauphauthuat,
		A.servicepricecode,
		A.servicepricename,
		A.servicepricename as phuongphappttt,
		(case A.ppvocamid
			when 1 then 'Gây mê tĩnh mạch'
			when 2 then 'Gây mê nội khí quản'
			when 3 then 'Gây tê tại chỗ'
			when 4 then 'Tiền mê + gây tê tại chỗ'
			when 5 then 'Gây tê tủy sống'
			when 6 then 'Gây tê'
			when 7 then 'Gây tê màng ngoài cứng'
			when 8 then 'Gây tê đám rối thần kinh'
			when 9 then 'Gây tê Codan'
			when 10 then 'Gây tê nhãn cầu'
			when 11 then 'Gây tê cạnh sống'
			when 12 then 'Gây tê hậu nhãn cầu'
			when 99 then 'Khác'
			end) as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		A.servicepricedate,
		(case when A.servicetimetrakq is not null then A.servicetimetrakq else ((case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end)) end) as phauthuatthuthuatdate,
		(case serf.pttt_loaiid 
			when 1 then 'Phẫu thuật đặc biệt' 
			when 2 then 'Phẫu thuật loại 1' 
			when 3 then 'Phẫu thuật loại 2' 
			when 4 then 'Phẫu thuật loại 3' 
			when 5 then 'Thủ thuật đặc biệt' 
			when 6 then 'Thủ thuật loại 1' 
			when 7 then 'Thủ thuật loại 2' 
			when 8 then 'Thủ thuật loại 3' 
			else '' end) as loaipttt,
		COALESCE(ntkq.username,ntkq_cc.username) as phauthuatvien,
		bsgm.username as bacsigayme
FROM
	(select * from 
		(select vp.vienphiid,
		vp.doituongbenhnhanid,
		ser.hosobenhanid,
		ser.maubenhphamid,
		ser.servicepricedate,
		ser.servicepricecode,
		ser.servicepricename,
		ser.departmentgroupid,
		ser.departmentid,
		mbp.maubenhphamfinishdate,
		mbp.chandoan,
		mbp.usertrakq as usertrakq_cc,
		cls.phauthuatvien,
		cls.bacsigayme,
		cls.ppvocamid,
		(case when pacs.readingdate is not null then pacs.readingdate else se.servicetimetrakq end) as servicetimetrakq,
		(case when pacs.readingdoctor1 is not null then pacs.readingdoctor1 else se.serviceusertrakq end) as serviceusertrakq
	from 
		(select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') {_tieuchi_date_ser}) ser
		inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 {_tieuchi_date_mbp} {_phongthuchien}) mbp on mbp.maubenhphamid=ser.maubenhphamid
		{_join_thuchiencls} (select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3,ppvocamid from thuchiencls where 1=1 {_tieuchi_date_thuchien}) cls on cls.servicepriceid=ser.servicepriceid
		inner join (select servicepriceid,servicename,servicevalue,maubenhphamid,servicetimetrakq,serviceusertrakq,servicecode from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) {_tieuchi_se}) se on se.maubenhphamid=mbp.maubenhphamid
		left join (select accessnumber,service_code,to_timestamp(readingdate,'yyyyMMddHH24MIss') at time zone 'utc' as readingdate,readingdoctor1,readingdr1name from resresulttab where {_tieuchi_pacs}) pacs ON pacs.accessnumber=mbp.maubenhphamid::text and pacs.service_code=se.servicecode
		inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_date_vp}) vp on vp.vienphiid=ser.vienphiid) tmp where 1=1 {_tieuchi_trakqtp}) A
	inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') and pttt_loaiid>0) serf on serf.servicepricecode=A.servicepricecode
	inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=A.hosobenhanid	
	left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=A.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=A.departmentid 
	left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=A.usertrakq_cc
	left join nhompersonnel bsgm on bsgm.userhisid=A.bacsigayme
	left join nhompersonnel ntkq ON ntkq.usercode=A.serviceusertrakq;";
                }
                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlSoCDHA.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.BienTrongBaoCao.DEPARTMENTNAME;
                item_phong.value = chkListDSPhongThucHien.Text;
                thongTinThem.Add(item_phong);

                string fileTemplatePath = "BC_43_SoPhauThuatThuThuat_CLS.xlsx";

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.BienTrongBaoCao.DEPARTMENTNAME;
                item_phong.value = chkListDSPhongThucHien.Text;
                thongTinThem.Add(item_phong);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);

                string fileTemplatePath = "BC_43_SoPhauThuatThuThuat_CLS.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion


    }
}
