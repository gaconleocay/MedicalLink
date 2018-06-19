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
    public partial class BC118_DSHuongTienDVYCCLC : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        private string DanhMucDichVu_String { get; set; }

        #endregion

        public BC118_DSHuongTienDVYCCLC()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC118_DSHuongTienDVYCCLC_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_118_DV").ToList();
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
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_ser = " and servicepricedate >= '" + datetungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
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

                sql_timkiem = @"SELECT row_number () over (order by nv.username) as stt, (nv.usercode || '-' || nv.username) as username, '' as departmentgroupname, sum(case when (PT.user_loai='mochinhid' and PT.dongia=2000000) then PT.soluong else 0 end) as mochinh_sl2, sum(case when (PT.user_loai='mochinhid' and PT.dongia=3000000) then PT.soluong else 0 end) as mochinh_sl3, sum(case when (PT.user_loai='mochinhid' and PT.dongia=5000000) then PT.soluong else 0 end) as mochinh_sl5, sum(case when (PT.user_loai='phuid' and PT.dongia=2000000) then PT.soluong else 0 end) as phu_sl2, sum(case when (PT.user_loai='phuid' and PT.dongia=3000000) then PT.soluong else 0 end) as phu_sl3, sum(case when (PT.user_loai='phuid' and PT.dongia=5000000) then PT.soluong else 0 end) as phu_sl5, sum(case PT.user_loai when 'mochinhid' then PT.soluong*PT.dongia else 0 end) as mochinh_tien, sum(case PT.user_loai when 'mochinhid' then PT.soluong*PT.dongia*0.02 else 0 end) as mochinh_thue2, sum(case PT.user_loai when 'mochinhid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.25 else 0 end) as mochinh_sauthue, sum(case PT.user_loai when 'phuid' then PT.soluong*PT.dongia else 0 end) as phu_tien, sum(case PT.user_loai when 'phuid' then PT.soluong*PT.dongia*0.02 else 0 end) as phu_thue2, sum(case PT.user_loai when 'phuid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.05 else 0 end) as phu_sauthue, (sum(case PT.user_loai when 'mochinhid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.25 else 0 end) + sum(case PT.user_loai when 'phuid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.05 else 0 end) as phu_sauthue) as thuclinh, '' as kynhan FROM (select pttt.mochinhid as userid, 'mochinhid' as user_loai, sum(pttt.soluong) as soluong, pttt.dongia from (select vienphiid,soluong,mochinhid,dongia from ml_thuchienpttt where mochinhid>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn_mel','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=ser.vienphiid group by pttt.mochinhid,pttt.dongia union all select pttt.phu1id as userid, 'phuid' as user_loai, sum(pttt.soluong) as soluong, pttt.dongia from (select vienphiid,soluong,phu1id,dongia from ml_thuchienpttt where phu1id>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn_mel','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=ser.vienphiid group by pttt.phu1id,pttt.dongia union all select pttt.phu2id as userid, 'phuid' as user_loai, sum(pttt.soluong) as soluong, pttt.dongia from (select vienphiid,soluong,phu2id,dongia from ml_thuchienpttt where phu2id>0 " + tieuchi_ser + lstdichvu_ser + ") pttt inner join (select * from dblink('myconn_mel','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=ser.vienphiid group by pttt.phu2id,pttt.dongia) PT INNER JOIN ml_nhanvien nv ON nv.userhisid=PT.userid GROUP BY nv.usercode,nv.username;";

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

                string fileTemplatePath = "BC_118_DSHuongTienDVYCCLC.xlsx";
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

                string fileTemplatePath = "BC_118_DSHuongTienDVYCCLC.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
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
