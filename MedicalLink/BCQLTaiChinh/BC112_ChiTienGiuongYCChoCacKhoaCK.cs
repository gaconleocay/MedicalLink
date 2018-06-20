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
    public partial class BC112_ChiTienGiuongYCChoCacKhoaCK : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ChiTienGiuongYCChoCacKhoaCK> lstDataBaoCao { get; set; }
        private string DanhMucDichVuBC110_String { get; set; }
        private string DanhMucKhoaBC110_String { get; set; }
        private string DanhMucDichVuBC111_String { get; set; }

        //private decimal TongTienChi = 0;
        #endregion

        public BC112_ChiTienGiuongYCChoCacKhoaCK()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC112_ChiTienGiuongYCChoCacKhoaCK_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu BC110
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_110_DV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    for (int i = 0; i < lstOtherList.Count - 1; i++)
                    {
                        this.DanhMucDichVuBC110_String += lstOtherList[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVuBC110_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
                }
                //Load danh muc Khoa BC110
                List<ClassCommon.ToolsOtherListDTO> lstOther_Khoa = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_110_KHOA").ToList();
                if (lstOther_Khoa != null && lstOther_Khoa.Count > 0)
                {
                    for (int i = 0; i < lstOther_Khoa.Count - 1; i++)
                    {
                        this.DanhMucKhoaBC110_String += lstOther_Khoa[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucKhoaBC110_String += lstOther_Khoa[lstOther_Khoa.Count - 1].tools_otherlistvalue;
                }
                //load danh muc dich vu BC111
                List<ClassCommon.ToolsOtherListDTO> lstOtherListBC111 = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_111_DV").ToList();
                if (lstOtherListBC111 != null && lstOtherListBC111.Count > 0)
                {
                    for (int i = 0; i < lstOtherListBC111.Count - 1; i++)
                    {
                        this.DanhMucDichVuBC111_String += lstOtherListBC111[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVuBC111_String += lstOtherListBC111[lstOtherListBC111.Count - 1].tools_otherlistvalue;
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
                string lstdichvu_serBC110 = " and servicepricecode in (" + this.DanhMucDichVuBC110_String + ") ";
                string lstdichvu_serBC111 = " and servicepricecode in (" + this.DanhMucDichVuBC111_String + ") ";
                string lstkhoa_serBC110 = " and departmentgroupid in (" + this.DanhMucKhoaBC110_String + ") ";

                string trangthai_vp = "";
                string sql_timkiem = "";
                //string tieuchi_mbp = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    //tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
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

                sql_timkiem = @"SELECT degp.stt, degp.departmentgroupname, sum(T.thanhtien) as thanhtien, degp.tylehuong, sum(T.thanhtien)*(degp.tylehuong/100.0) as tienthuong, '' as kynhan FROM (SELECT degp.* FROM dblink('myconn_mel','SELECT code,stt,departmentgroupname,tylehuong FROM ml_bc112giuongyc') AS degp(code integer,stt integer,departmentgroupname text,tylehuong double precision)) degp LEFT JOIN (select 112 as code, sum(ser.soluong*ser.dongia*0.88) as thanhtien from (select vienphiid,soluong,servicepricecode, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia from serviceprice where bhyt_groupcode='12NG' " + tieuchi_ser + ") ser inner join (select servicepricecode from servicepriceref where servicepricegroupcode='G303YC') serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid UNION ALL select 112 as code, 0-sum(ser.soluong*ser.dongia*0.88) as thanhtien from (select vienphiid,soluong, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia from serviceprice where 1=1 " + tieuchi_ser + lstdichvu_serBC111 + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid UNION ALL select 112 as code, 0-sum(ser.soluong*80000) as thanhtien from (select vienphiid,soluong, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia from serviceprice where 1=1 " + tieuchi_ser + lstdichvu_serBC110 + lstkhoa_serBC110 + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid) T on T.code=degp.code GROUP BY degp.stt,degp.departmentgroupname,degp.tylehuong ORDER BY degp.stt;";

                DataTable _dataBaoCao = condb.GetDataTable_HISToMeL(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                this.lstDataBaoCao = Utilities.DataTables.DataTableToList<ChiTienGiuongYCChoCacKhoaCK>(_dataBaoCao);
                    gridControlDataBC.DataSource = this.lstDataBaoCao;
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
                _item_tien_string.name = "TIENTHUONG_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTienThuong(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                ClassCommon.reportExcelDTO _item_THANHTIEN = new ClassCommon.reportExcelDTO();
                _item_THANHTIEN.name = "THANHTIEN";
                _item_THANHTIEN.value = this.lstDataBaoCao[0].thanhtien.ToString().Replace(",",".");
                thongTinThem.Add(_item_THANHTIEN);

                string fileTemplatePath = "BC_112_ChiTienGiuongYCChoCacKhoaCK.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, new DataTable());
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

                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TIENTHUONG_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTienThuong(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                ClassCommon.reportExcelDTO _item_THANHTIEN = new ClassCommon.reportExcelDTO();
                _item_THANHTIEN.name = "THANHTIEN";
                _item_THANHTIEN.value = this.lstDataBaoCao[0].thanhtien.ToString();
                thongTinThem.Add(_item_THANHTIEN);

                string fileTemplatePath = "BC_112_ChiTienGiuongYCChoCacKhoaCK.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem);
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
        private decimal TinhTongTienThuong()
        {
            decimal _result = 0;
            try
            {
                foreach (var item in this.lstDataBaoCao)
                {
                    _result += item.tienthuong ?? 0;
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
