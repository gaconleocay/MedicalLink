using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using DevExpress.XtraSplashScreen;
using MedicalLink.Base;

namespace MedicalLink.BaoCao
{
    public partial class ucBC_53_BNKhongLamDVKTPK : UserControl
    {
        #region Declaration
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        #endregion

        public ucBC_53_BNKhongLamDVKTPK()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC53_BNKhongLamDVKTPK_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_vp = " and vienphidate>='2018-01-01 00:00:00' ";
                string _tieuchi_ser = " and servicepricedate>='2018-01-01 00:00:00' ";
                string _tieuchi_mrd = " and thoigianvaovien>='2018-01-01 00:00:00' ";
                string _trangthai_vp = "";

                string _tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //Tieu chi
                if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate>='" + _tungay + "' ";
                    _tieuchi_mrd = " and thoigianvaovien between '" + _tungay + "' and '" + _denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianravien between '" + _tungay + "' and '" + _denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and duyet_ngayduyet_vp between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianravien between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                }
                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    _trangthai_vp = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    _trangthai_vp = " and vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    _trangthai_vp = " and vienphistatus>0 and vienphistatus_vp=1 ";
                }

                string _sqlTimKiem = $@"SELECT row_number () over (order by de.departmentgroupid,de.departmentname) as stt,
	de.departmentname,
	count(mrd.*) as tong_sl,
	sum(case when vp.doituongbenhnhanid=1 then 1 else 0 end) as bh_sl,
	sum(case when vp.doituongbenhnhanid=1 and mrd.xutrikhambenhid=4 then 1 else 0 end) as bh_nhapvien,
	sum(case when vp.doituongbenhnhanid=1 and mrd.hinhthucravienid=5 then 1 else 0 end) as bh_chuyenvien,
	sum(case when vp.doituongbenhnhanid=1 and serv.isxn=0 then 1 else 0 end) as bh_koxn,
	sum(case when vp.doituongbenhnhanid=1 and serv.iscdha=0 then 1 else 0 end) as bh_kocdha,
	sum(case when vp.doituongbenhnhanid=1 and serv.isxn=0 and serv.iscdha=0 then 1 else 0 end) as bh_koxncdha,
	sum(case when vp.doituongbenhnhanid=1 and serv.ispttt=0 then 1 else 0 end) as bh_kopttt,
	sum(case when vp.doituongbenhnhanid=1 and serv.isxn=0 and serv.iscdha=0 and serv.ispttt=0 then 1 else 0 end) as bh_koptttcls,
	sum(case when vp.doituongbenhnhanid<>1 then 1 else 0 end) as vp_sl,
	sum(case when vp.doituongbenhnhanid<>1 and mrd.xutrikhambenhid=4 then 1 else 0 end) as vp_nhapvien,
	sum(case when vp.doituongbenhnhanid<>1 and mrd.hinhthucravienid=5 then 1 else 0 end) as vp_chuyenvien,
	sum(case when vp.doituongbenhnhanid<>1 and serv.isxn=0 then 1 else 0 end) as vp_koxn,
	sum(case when vp.doituongbenhnhanid<>1 and serv.iscdha=0 then 1 else 0 end) as vp_kocdha,
	sum(case when vp.doituongbenhnhanid<>1 and serv.isxn=0 and serv.iscdha=0 then 1 else 0 end) as vp_koxncdha,
	sum(case when vp.doituongbenhnhanid<>1 and serv.ispttt=0 then 1 else 0 end) as vp_kopttt,
	sum(case when vp.doituongbenhnhanid<>1 and serv.isxn=0 and serv.iscdha=0 and serv.ispttt=0 then 1 else 0 end) as vp_koptttcls	
FROM (select vienphiid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp	
	inner join (select vienphiid,departmentid,xutrikhambenhid,hinhthucravienid from medicalrecord where loaibenhanid=24 {_tieuchi_mrd}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select ser.vienphiid,
				max(case when ser.bhyt_groupcode='01KB' then 1 else 0 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1  else 0 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','07KTC','05TDCN') then 1  else 0 end) as iscdha,
				max(case when ser.bhyt_groupcode='06PTTT' then 1  else 0 end) as ispttt
			from	
				(select vienphiid,bhyt_groupcode from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') {_tieuchi_ser} group by vienphiid,bhyt_groupcode) ser group by ser.vienphiid) serv on serv.vienphiid=vp.vienphiid 
	left join (select departmentid,departmentname,departmentgroupid from department where departmenttype in (2,3,9)) de on de.departmentid=mrd.departmentid
GROUP BY de.departmentgroupid,de.departmentname;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlTimKiem);
                if (_dataBaoCao.Rows.Count > 0)
                {
                    gridControlBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region In va xuat excel
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
                ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                string fileTemplatePath = "BC_53_BNKhongLamDVKTPK.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
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

                string fileTemplatePath = "BC_53_BNKhongLamDVKTPK.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }




        #endregion


    }
}
