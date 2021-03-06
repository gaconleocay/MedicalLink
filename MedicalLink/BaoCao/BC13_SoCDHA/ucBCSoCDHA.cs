﻿using System;
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
    public partial class ucBCSoCDHA : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private string ThoiGianGioiHanDuLieu { get; set; }
        public ucBCSoCDHA()
        {
            InitializeComponent();
        }

        #region Load
        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            LoadDaLieuMacDinh();
            LoadDataPhongThucHien();
            LoadDanhSachKhoa();
            LoadThoiGianGioiHanDuLieu();
        }
        private void LoadDaLieuMacDinh()
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                lblTheoLoai.Visible = false;
                chkcomboListDSKhoa.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDataPhongThucHien()
        {
            try
            {
                var lstDSPhong = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7).OrderBy(o => o.departmentname).ToList();
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    cboPhongThucHien.Properties.DataSource = lstDSPhong;
                    cboPhongThucHien.Properties.DisplayMember = "departmentname";
                    cboPhongThucHien.Properties.ValueMember = "departmentid";
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                chkcomboListDSKhoa.CheckAll();
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
                string _sqlDMDichVu = "select toolsoptionvalue from tools_option where toolsoptioncode='REPORT_13_TGLayDuLieu';";
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

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tieuchi_mbp = " and maubenhphamdate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string tieuchi_vp = " and vienphidate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string tieuchi_hsba = " and hosobenhandate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_se = " and servicedate>'" + this.ThoiGianGioiHanDuLieu + "' ";
                string _tieuchi_trakqtp = "";
                string _theokhoagui = "";
                string _theokhoatrakq = "";
                string _tieuchi_pacs = " 1<>1 ";
                //Phong thuc hien
                if (cboPhongThucHien.EditValue == null)
                {
                    SplashScreenManager.CloseForm();
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_THUC_HIEN);
                    frmthongbao.Show();
                    return;
                }

                if (chkcomboListDSKhoa.Visible && chkTatCa.Checked == false)
                {
                    List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                    if (lstKhoaCheck.Count > 0)
                    {
                        string _lstdepartmentgroup = "0";
                        for (int i = 0; i < lstKhoaCheck.Count; i++)
                        {
                            _lstdepartmentgroup += "," + lstKhoaCheck[i];
                        }
                        _lstdepartmentgroup = _lstdepartmentgroup.Replace("0,", "");
                        if (lblTheoLoai.Text == "Khoa gửi")
                        {
                            _theokhoagui = " and departmentgroupid in (" + _lstdepartmentgroup + ") ";
                        }
                        if (lblTheoLoai.Text == "Khoa trả KQ")
                        {
                            _theokhoatrakq = " and (case when ntkq.departmentgroupid is not null then ntkq.departmentgroupid else ntkqcc.departmentgroupid end) in (" + _lstdepartmentgroup + ") ";
                        }
                    }
                    else
                    {
                        SplashScreenManager.CloseForm();
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                }

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_se = " and servicedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày trả kết quả")
                {
                    tieuchi_mbp = " and maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày trả kết quả từng phần")
                {
                    _tieuchi_trakqtp = " and tmp.servicetimetrakq between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_pacs = " 1=1 and to_timestamp(readingdate,'yyyyMMddHH24MIss') between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphistatus>0 and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi_vp = " and vienphistatus_vp=1 and vienphistatus>0 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                string sql_timkiem = $@"SELECT
		ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt,
		hsba.patientcode,
		hsba.patientname,
		A.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
		(case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt,
		a.chandoan,
		(kyc.departmentgroupname || ' - ' || pyc.departmentname) as noigui,
		a.yeucau,
		a.ketqua,
		a.maubenhphamdate,
		a.maubenhphamfinishdate, --tra ket qua cuoi cung
		a.vienphidate_ravien,
		a.duyet_ngayduyet_vp,
		(case when A.servicetimetrakq is not null then A.servicetimetrakq end) as servicetimetrakq,
		(case when ntkq.username is not null then ntkq.username else ntkqcc.username end) as nguoidoc,
		(case when a.departmentid_des=244 then 'x' else '' end) as phim_20x25,
		(case when a.departmentid_des in (245,246) then 'x' else '' end) as phim_35x43,
		A.isthuocdikem,
		A.isvattudikem
FROM
	(select * from (SELECT mbp.maubenhphamid,
			mbp.hosobenhanid,
			mbp.departmentgroupid,
			mbp.departmentid,
			mbp.departmentid_des,
			mbp.chandoan,
			mbp.usertrakq, 
			mbp.userthuchien,
			mbp.maubenhphamdate,
			(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate,
			(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien,
			(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
			se.servicename as yeucau,
			se.servicevalue as ketqua,
			(case when pacs.readingdate is not null then pacs.readingdate else se.servicetimetrakq end) as servicetimetrakq,
			(case when pacs.readingdoctor1 is not null then pacs.readingdoctor1 else se.serviceusertrakq end) as serviceusertrakq,
			(case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle'))>0 then 'X' end) as isthuocdikem,
			(case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle'))>0 then 'X' end) as isvattudikem
	FROM (select maubenhphamid,hosobenhanid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate from maubenhpham where maubenhphamgrouptype=1 and
	departmentid_des='{cboPhongThucHien.EditValue.ToString()}' {tieuchi_mbp} {_theokhoagui}) mbp
		INNER JOIN (select servicepriceid,servicename,servicevalue,maubenhphamid,servicetimetrakq,serviceusertrakq,servicecode from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) {_tieuchi_se}) se ON se.maubenhphamid=mbp.maubenhphamid
		left join (select accessnumber,service_code,to_timestamp(readingdate,'yyyyMMddHH24MIss') at time zone 'utc' as readingdate,readingdoctor1,readingdr1name from resresulttab where {_tieuchi_pacs}) pacs ON pacs.accessnumber=mbp.maubenhphamid::text and pacs.service_code=se.servicecode
		INNER JOIN (select vienphiid,hosobenhanid,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 {tieuchi_vp}) vp on vp.hosobenhanid=mbp.hosobenhanid
		) tmp where 1=1 {_tieuchi_trakqtp}) A
	INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan where 1=1 {tieuchi_hsba}) hsba ON hsba.hosobenhanid=A.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pyc ON pyc.departmentid=A.departmentid
	LEFT JOIN (select userhisid,usercode,username,departmentgroupid from nhompersonnel) ntkq ON ntkq.usercode=A.serviceusertrakq
	LEFT JOIN (select userhisid,username,departmentgroupid from nhompersonnel) ntkqcc ON ntkqcc.userhisid=COALESCE(A.usertrakq,A.userthuchien)
WHERE 1=1 {_theokhoatrakq};";
                DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = dataBaoCao;
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                    gridControlSoCDHA.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Export
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + datetungay + " - " + datedenngay + " )";
                string fileTemplatePath = "So_CDHA_XQuangCTMRI.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.BienTrongBaoCao.DEPARTMENTNAME;
                reportitem_kho.value = cboPhongThucHien.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                ClassCommon.reportExcelDTO reportitem_tenbc = new ClassCommon.reportExcelDTO();
                reportitem_tenbc.name = Base.BienTrongBaoCao.TENBAOCAO;

                switch (cboPhongThucHien.EditValue.ToString())
                {
                    case "244": //SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY ("Phòng Chụp XQuang")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY";

                        break;
                    case "245"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32 ("Phòng Chụp CT")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32";

                        break;
                    case "246"://SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI ("Phòng Chụp Cộng Hưởng Từ")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI";

                        break;
                    case "247"://SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM ("Phòng Siêu Âm")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "422"://SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP ("Phòng Chụp XQuang Can Thiệp")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "2471"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128 ("Phòng Chụp CT 64") //chua them
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128";

                        break;
                    default:
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH";

                        break;
                }
                thongTinThem.Add(reportitem_tenbc);
                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);

                //Utilities.PrintPreview.PrintPreviewDev frmPrint = new Utilities.PrintPreview.PrintPreviewDev();
                //MedicalLink.BaoCao.BCSoCDHA.SoCDHA_XQuang rpSoCDHA = new BCSoCDHA.SoCDHA_XQuang();
                //rpSoCDHA.DataSource = this.dataBaoCao;
                //frmPrint.documentViewerData.PrintingSystem = rpSoCDHA.PrintingSystem;
                //rpSoCDHA.CreateDocument();
                //frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + datetungay + " - " + datedenngay + " )";
                string fileTemplatePath = "So_CDHA_XQuangCTMRI.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.BienTrongBaoCao.DEPARTMENTNAME;
                reportitem_kho.value = cboPhongThucHien.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                ClassCommon.reportExcelDTO reportitem_tenbc = new ClassCommon.reportExcelDTO();
                reportitem_tenbc.name = Base.BienTrongBaoCao.TENBAOCAO;

                switch (cboPhongThucHien.EditValue.ToString())
                {
                    case "244": //SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY ("Phòng Chụp XQuang")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY";

                        break;
                    case "245"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32 ("Phòng Chụp CT")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32";

                        break;
                    case "246"://SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI ("Phòng Chụp Cộng Hưởng Từ")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI";

                        break;
                    case "247"://SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM ("Phòng Siêu Âm")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "422"://SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP ("Phòng Chụp XQuang Can Thiệp")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "2471"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128 ("Phòng Chụp CT 64") //chua them
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128";

                        break;
                    default:
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH";

                        break;
                }
                thongTinThem.Add(reportitem_tenbc);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);

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
        private void cboPhongThucHien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                long _phongthuchien = Utilities.TypeConvertParse.ToInt64(cboPhongThucHien.EditValue.ToString());
                //phong Sieu am va phong X-quang can thiep.
                if (_phongthuchien == 247 || _phongthuchien == 422)
                {
                    gridBandCoPhim.Visible = false;
                }
                else
                {
                    gridBandCoPhim.Visible = true;
                }
                //hien thi THuoc/vat tu di kem: phong XQ, CT, MRI
                if (_phongthuchien == 244 || _phongthuchien == 245 || _phongthuchien == 246)
                {
                    gridBandThuocVTDiKem.Visible = true;
                }
                else
                {
                    gridBandThuocVTDiKem.Visible = false;
                }

                //Kiem tra phan quyen nhom BC
                bool _ktFilterKhoa = false;
                ClassCommon.ToolsOtherListDTO _itemTheoKhoaGui = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_13_SoCDHA" && o.tools_otherlistcode == "TheoKhoaGui").FirstOrDefault();
                ClassCommon.ToolsOtherListDTO _itemTheoKhoaTraKetQua = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_13_SoCDHA" && o.tools_otherlistcode == "TheoKhoaTraKetQua").FirstOrDefault();

                if (_itemTheoKhoaGui.tools_otherlistvalue.Contains(_phongthuchien.ToString() + ","))//theo khoa gui
                {
                    lblTheoLoai.Text = "Khoa gửi";
                    _ktFilterKhoa = true;
                    LoadDanhSachKhoaGui_User();
                }
                if (_itemTheoKhoaTraKetQua.tools_otherlistvalue.Contains(_phongthuchien.ToString() + ","))
                {
                    lblTheoLoai.Text = "Khoa trả KQ";
                    _ktFilterKhoa = true;
                    LoadDanhSachKhoaTraKQ_User();
                }

                if (_ktFilterKhoa)
                {
                    lblTheoLoai.Visible = true;
                    chkcomboListDSKhoa.Visible = true;
                    chkTatCa.Visible = true;
                }
                else
                {
                    lblTheoLoai.Visible = false;
                    chkcomboListDSKhoa.Visible = false;
                    chkTatCa.Visible = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void chkTatCa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTatCa.Checked)
                {
                    chkcomboListDSKhoa.Enabled = false;
                }
                else
                {
                    chkcomboListDSKhoa.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Process
        private void LoadDanhSachKhoaGui_User()
        {
            try
            {
                if (Base.SessionLogin.SessionUsercode != "admin")
                {
                    string _sqlDSKhoa = "select departmentgroupid,departmentgroupcode,departmentgroupname from tools_tbluser_rpt13 where iskhoagui=1 and usercode='" + Base.SessionLogin.SessionUsercode + "';";
                    DataTable _dataDSKhoa = condb.GetDataTable_MeL(_sqlDSKhoa);
                    chkcomboListDSKhoa.Properties.DataSource = _dataDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                    chkcomboListDSKhoa.CheckAll();
                }
                else
                {
                    LoadDanhSachKhoa();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachKhoaTraKQ_User()
        {
            try
            {
                if (Base.SessionLogin.SessionUsercode != "admin")
                {
                    string _sqlDSKhoa = "select departmentgroupid,departmentgroupcode,departmentgroupname from tools_tbluser_rpt13 where iskhoatra=1 and usercode='" + Base.SessionLogin.SessionUsercode + "';";
                    DataTable _dataDSKhoa = condb.GetDataTable_MeL(_sqlDSKhoa);
                    chkcomboListDSKhoa.Properties.DataSource = _dataDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                    chkcomboListDSKhoa.CheckAll();
                }
                else
                {
                    LoadDanhSachKhoa();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }



        //private bool KiemTraPhong_LaCDHA()
        //{
        //    bool result = false;
        //    try
        //    {
        //        //var _PhongThucHien = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentid == Utilities.TypeConvertParse.ToInt32(cboPhongThucHien.EditValue.ToString())).FirstOrDefault();
        //        //if (_PhongThucHien != null && _PhongThucHien.departmenttype == 7)//Phong=CDHA
        //        //{
        //        //    result = true;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Warn(ex);
        //    }
        //    return result;
        //}

        #endregion


    }
}
