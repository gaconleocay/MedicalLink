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
using MedicalLink.ClassCommon.BCQLTaiChinh;

namespace MedicalLink.BCQLTaiChinh
{
    public partial class BC119_DSHuongTienDVMoYC : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        private string DanhMucDichVu_String { get; set; }

        #endregion

        public BC119_DSHuongTienDVMoYC()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC119_DSHuongTienDVMoYC_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_119_DV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    for (int i = 0; i < lstOtherList.Count - 1; i++)
                    {
                        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi_ser = "";
                string tieuchi_vp = "";
                //string tieuchi_mrd = "";
                string lstdichvu_ser = " and servicepricecode in (" + this.DanhMucDichVu_String + ") ";
                string trangthai_vp = "";
                string sql_timkiem = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between ''" + datetungay + "'' and ''" + datedenngay + "'' ";
                    tieuchi_ser = " and servicepricedate >= '" + datetungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between ''" + datetungay + "'' and ''" + datedenngay + "'' ";
                    tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between ''" + datetungay + "'' and ''" + datedenngay + "'' ";
                    tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                }
                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    trangthai_vp = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and vienphistatus_vp=1 ";
                }

                sql_timkiem = @" SELECT row_number () over (order by nv.username) as stt, nv.username,nv.usercode, '' as departmentgroupname, '' as sotaikhoan, sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as mochinh_sltt, sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as mochinh_sll1, sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as mochinh_sll2, sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as mochinh_sll3, sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as phu_sltt, sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as phu_sll1, sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as phu_sll2, sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as phu_sll3, sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as bacsigayme_sltt, sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as bacsigayme_sll1, sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as bacsigayme_sll2, sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as bacsigayme_sll3, sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ktvphume_sltt, sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ktvphume_sll1, sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ktvphume_sll2, sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ktvphume_sll3, sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as dungcuvien_sltt, sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as dungcuvien_sll1, sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as dungcuvien_sll2, sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as dungcuvien_sll3, sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ddhoitinh_sltt, sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ddhoitinh_sll1, sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ddhoitinh_sll2, sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ddhoitinh_sll3, sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ktvhoitinh_sltt, sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ktvhoitinh_sll1, sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ktvhoitinh_sll2, sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ktvhoitinh_sll3, sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ddhanhchinh_sltt, sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ddhanhchinh_sll1, sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ddhanhchinh_sll2, sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ddhanhchinh_sll3, sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as holy_sltt, sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as holy_sll1, sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as holy_sll2, sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as holy_sll3, sum(case PT.user_loai when 'mochinhid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*5000000 when 'U11621-4524' then PT.soluong*1500000 when 'U11622-4536' then PT.soluong*1300000 when 'U11622-4536' then PT.soluong*1200000 else 0 end) when 'phuid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*1300000 when 'U11621-4524' then PT.soluong*750000 when 'U11622-4536' then PT.soluong*600000 when 'U11622-4536' then PT.soluong*500000 else 0 end) when 'bacsigaymeid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*500000 when 'U11621-4524' then PT.soluong*400000 when 'U11622-4536' then PT.soluong*350000 when 'U11622-4536' then PT.soluong*325000 else 0 end) when 'ktvphumeid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*200000 when 'U11621-4524' then PT.soluong*160000 when 'U11622-4536' then PT.soluong*140000 when 'U11622-4536' then PT.soluong*130000 else 0 end) when 'dungcuvienid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*200000 when 'U11621-4524' then PT.soluong*160000 when 'U11622-4536' then PT.soluong*140000 when 'U11622-4536' then PT.soluong*130000 else 0 end) when 'ddhoitinhid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*30000 when 'U11621-4524' then PT.soluong*24000 when 'U11622-4536' then PT.soluong*21000 when 'U11622-4536' then PT.soluong*19500 else 0 end) when 'ktvhoitinhid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*30000 when 'U11621-4524' then PT.soluong*24000 when 'U11622-4536' then PT.soluong*21000 when 'U11622-4536' then PT.soluong*19500 else 0 end) when 'ddhanhchinhid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*30000 when 'U11621-4524' then PT.soluong*24000 when 'U11622-4536' then PT.soluong*21000 when 'U11622-4536' then PT.soluong*19500 else 0 end) when 'holyid' then (case PT.servicepricecode when 'U11620-4506' then PT.soluong*10000 when 'U11621-4524' then PT.soluong*8000 when 'U11622-4536' then PT.soluong*7000 when 'U11622-4536' then PT.soluong*6500 else 0 end) else 0 end) as thuclinh, '' as kynhan FROM (select pttt.mochinhid as userid, 'mochinhid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,mochinhid,servicepricecode from ml_thuchienpttt where mochinhid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.mochinhid,pttt.servicepricecode union all select pttt.phu1id as userid, 'phuid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,(case when phu2id>0 then soluong/2 else soluong end) as soluong,phu1id,servicepricecode from ml_thuchienpttt where phu1id>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.phu1id,pttt.servicepricecode union all select pttt.phu2id as userid, 'phuid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,(case when phu1id>0 then soluong/2 else soluong end) as soluong,phu2id,servicepricecode from ml_thuchienpttt where phu2id>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.phu2id,pttt.servicepricecode union all select pttt.bacsigaymeid as userid, 'bacsigaymeid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,bacsigaymeid,servicepricecode from ml_thuchienpttt where bacsigaymeid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.bacsigaymeid,pttt.servicepricecode union all select pttt.ktvphumeid as userid, 'ktvphumeid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,ktvphumeid,servicepricecode from ml_thuchienpttt where ktvphumeid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.ktvphumeid,pttt.servicepricecode union all select pttt.dungcuvienid as userid, 'dungcuvienid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,dungcuvienid,servicepricecode from ml_thuchienpttt where dungcuvienid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.dungcuvienid,pttt.servicepricecode union all select pttt.ddhoitinhid as userid, 'ddhoitinhid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,ddhoitinhid,servicepricecode from ml_thuchienpttt where ddhoitinhid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.ddhoitinhid,pttt.servicepricecode union all select pttt.ktvhoitinhid as userid, 'ktvhoitinhid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,ktvhoitinhid,servicepricecode from ml_thuchienpttt where ktvhoitinhid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.ktvhoitinhid,pttt.servicepricecode union all select pttt.ddhanhchinhid as userid, 'ddhanhchinhid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,ddhanhchinhid,servicepricecode from ml_thuchienpttt where ddhanhchinhid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.ddhanhchinhid,pttt.servicepricecode union all select pttt.holyid as userid, 'holyid' as user_loai, sum(pttt.soluong) as soluong, pttt.servicepricecode from (select vienphiid,soluong,holyid,servicepricecode from ml_thuchienpttt where holyid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.holyid,pttt.servicepricecode ) PT INNER JOIN ml_nhanvien nv ON nv.userhisid=PT.userid GROUP BY nv.usercode,nv.username;";

                this.dataBaoCao = condb.GetDataTable_MeLToHIS(sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBC.DataSource = this.dataBaoCao;
                }
                else
                {
                    gridControlDataBC.DataSource = null;
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
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_119_DSHuongTienDVMoYC.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
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
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_119_DSHuongTienDVMoYC.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
        private void bandedGridViewDataBC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Process
        private decimal TinhTongTien()
        {
            decimal _result = 0;
            try
            {
                for (int i = 0; i < this.dataBaoCao.Rows.Count; i++)
                {
                    decimal _thuclinh = Utilities.TypeConvertParse.ToDecimal(this.dataBaoCao.Rows[i]["thuclinh"].ToString());
                    _result += _thuclinh;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return _result;
        }

        #endregion


    }
}
