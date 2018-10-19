using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.BaoCao
{
    public partial class ucBC52_TGKhamBenhTrungBinh : UserControl
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion

        public ucBC52_TGKhamBenhTrungBinh()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC52_TGKhamBenhTrungBinh_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucKhoa();
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    chkcomboListDSKhoa.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Events
        private void chkcomboListDSKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListDSPhong.Properties.Items.Clear();
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    //Load danh muc phong theo khoa da chon
                    List<ClassCommon.classUserDepartment> lstDSPhong = new List<classUserDepartment>();
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        List<ClassCommon.classUserDepartment> lstdsphongthuockhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
                        lstDSPhong.AddRange(lstdsphongthuockhoa);
                    }
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
                    chkcomboListDSPhong.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_vp = " and vienphidate>='2018-01-01 00:00:00' ";
                string _tieuchi_mrd = " and thoigianvaovien>='2018-01-01 00:00:00' ";
                string _tieuchi_stt = " and sothutudate_start>='2018-01-01 00:00:00' ";
                string _tieuchi_ser = " and servicepricedate>='2018-01-01 00:00:00' ";
                string _doituongbenhnhanid = "";

                string _tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //tieu chi
                if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianvaovien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_stt = " and sothutudate_start>='" + _tungay + "' ";
                    _tieuchi_ser = " and servicepricedate>='" + _tungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianravien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_stt = " and sothutudate_start<='" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate<='" + _denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and vienphistatus>0 and duyet_ngayduyet_vp between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianravien<='" + _denngay + "' ";
                    _tieuchi_stt = " and sothutudate_start<='" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate<='" + _denngay + "' ";
                }
                //doi tuong Bn
                if (cboDoiTuong.Text == "BHYT")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid=1 ";
                }
                else if (cboDoiTuong.Text == "Viện phí")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid<>1 ";
                }

                string _sqlTimKiem = $@"SELECT TMP.sl_bn,
	(TMP.tg_chokham/TMP.sl_bn) as tg_chokham,
	(TMP.tg_khamls_qlcl/TMP.sl_bn) as tg_khamls_qlcl,
	(TMP.tg_khamls_it/TMP.sl_bn) as tg_khamls_it,
	(TMP.tg_khamlsxn_qlcl/TMP.sl_bn) as tg_khamlsxn_qlcl,
	(TMP.tg_khamlsxn_it/TMP.sl_bn) as tg_khamlsxn_it,
	(TMP.tg_khamlsxncdha_qlcl/TMP.sl_bn) as tg_khamlsxncdha_qlcl,
	(TMP.tg_khamlsxncdha_it/TMP.sl_bn) as tg_khamlsxncdha_it,
	(TMP.tg_khamlsxncdhatdcn_qlcl/TMP.sl_bn) as tg_khamlsxncdhatdcn_qlcl,
	(TMP.tg_khamlsxncdhatdcn_it/TMP.sl_bn) as tg_khamlsxncdhatdcn_it
FROM 
(SELECT
	count(vp.*) as sl_bn,
	sum(case when stt.sothutustatus>=1 then ((DATE_PART('day',stt.sothutudate_start-mrd.thoigianvaovien)*24+DATE_PART('hour',stt.sothutudate_start-mrd.thoigianvaovien))*60+DATE_PART('minute',stt.sothutudate_start-mrd.thoigianvaovien)) end) as tg_chokham,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamls_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamls_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxn_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdha_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdha_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 and serv.istdcn=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdhatdcn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 and serv.istdcn=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdhatdcn_it	
FROM 
	(select vienphiid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus from medicalrecord where loaibenhanid=24 {_doituongbenhnhanid} {_tieuchi_mrd}) mrd on mrd.vienphiid=vp.vienphiid
	left join (select medicalrecordid,sothutudate,sothutudate_start,sothutudate_end,sothutustatus from sothutuphongkham where 1=1 {_tieuchi_stt}) stt on stt.medicalrecordid=mrd.medicalrecordid
	left join (select ser.vienphiid,
				max(case when ser.bhyt_groupcode='01KB' then 1 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','05TDCN','07KTC') then 1 end) as iscdha,
				max(case when ser.bhyt_groupcode='05TDCN' then 1 end) as istdcn
			from	
				(select vienphiid,bhyt_groupcode from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} group by vienphiid,bhyt_groupcode) ser group by ser.vienphiid) serv on serv.vienphiid=vp.vienphiid) TMP;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlTimKiem);
                if (_dataBaoCao.Rows.Count > 0)
                {
                    gridControlBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Xuat bao cao and print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (bandedGridViewBaoCao.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                    string fileTemplatePath = "BC_52_TGKhamBenhTrungBinh.xlsx";
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewBaoCao);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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

                string fileTemplatePath = "BC_52_TGKhamBenhTrungBinh.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }


        #endregion

        #region Custom
        private void bandedGridViewBaoCao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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


        #endregion
    }
}
