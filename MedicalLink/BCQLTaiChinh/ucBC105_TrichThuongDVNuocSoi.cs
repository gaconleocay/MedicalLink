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
    public partial class ucBC105_TrichThuongDVNuocSoi : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<TrichThuongDVNuocSoiDTO> lstDataBaoCao { get; set; }
        private string DanhMucDichVu_String { get; set; }
        //private decimal TongTienChi = 0;
        #endregion

        public ucBC105_TrichThuongDVNuocSoi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC105_TrichThuongDVNuocSoi_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_105_DV").ToList();
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
                string lstdichvu_ser = " and servicepricecode in (" + this.DanhMucDichVu_String + ") ";
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

                sql_timkiem = @"SELECT row_number() over() as stt, '' as departmentgroupname, sum(ser.soluong*ser.dongia) as tongthu, 0 as tongchi, 0 as phantramhuong, 0 as thuclinh, '' as kynhan FROM (select vienphiid,departmentid,soluong,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia from serviceprice where bhyt_groupcode='12NG' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    XuLyDuLieuVaHienThi(_dataBaoCao);
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
        private void XuLyDuLieuVaHienThi(DataTable _dataBaocao)
        {
            try
            {
                this.lstDataBaoCao = new List<TrichThuongDVNuocSoiDTO>();

                List<TrichThuongDVNuocSoiDTO> _lstBaoCao = Utilities.Util_DataTable.DataTableToList<TrichThuongDVNuocSoiDTO>(_dataBaocao);


                TrichThuongDVNuocSoiDTO _item_KhoaDD = new TrichThuongDVNuocSoiDTO();
                _item_KhoaDD.stt = 1;
                _item_KhoaDD.departmentgroupname = "Khoa Dinh dưỡng";
                _item_KhoaDD.tongthu = _lstBaoCao[0].tongthu;
                _item_KhoaDD.tongchi = _lstBaoCao[0].tongchi;
                _item_KhoaDD.phantramhuong = (decimal)10.5;
                _item_KhoaDD.thuclinh = (_item_KhoaDD.tongthu - _item_KhoaDD.tongchi) * (decimal)0.105;
                _item_KhoaDD.kynhan = "";
                this.lstDataBaoCao.Add(_item_KhoaDD);

                TrichThuongDVNuocSoiDTO _item_ToLoHoi = new TrichThuongDVNuocSoiDTO();
                _item_ToLoHoi.stt = 1;
                _item_ToLoHoi.departmentgroupname = "Tổ lò hơi";
                _item_ToLoHoi.tongthu = _lstBaoCao[0].tongthu;
                _item_ToLoHoi.tongchi = _lstBaoCao[0].tongchi;
                _item_ToLoHoi.phantramhuong = (decimal)4.5;
                _item_ToLoHoi.thuclinh = (_item_ToLoHoi.tongthu - _item_ToLoHoi.tongchi) * (decimal)0.045;
                _item_ToLoHoi.kynhan = "";
                this.lstDataBaoCao.Add(_item_ToLoHoi);

                gridControlDataBC.DataSource = null;
                gridControlDataBC.DataSource = this.lstDataBaoCao;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
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
                item_phong.name = Base.bienTrongBaoCao.LST_DICHVU;
                item_phong.value = this.DanhMucDichVu_String;
                thongTinThem.Add(item_phong);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTHUCLINH_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(TinhTongTienThucLinh(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                ClassCommon.reportExcelDTO _item_TONGTHU = new ClassCommon.reportExcelDTO();
                _item_TONGTHU.name = "TONGTHU";
                _item_TONGTHU.value = this.lstDataBaoCao[0].tongthu.ToString();
                thongTinThem.Add(_item_TONGTHU);

                ClassCommon.reportExcelDTO _item_TONGCHI = new ClassCommon.reportExcelDTO();
                _item_TONGCHI.name = "TONGCHI";
                _item_TONGCHI.value = this.lstDataBaoCao[0].tongchi.ToString();
                thongTinThem.Add(_item_TONGCHI);

                string fileTemplatePath = "BC_105_TrichThuongDVNuocSoi.xlsx";
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
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.bienTrongBaoCao.LST_DICHVU;
                item_phong.value = this.DanhMucDichVu_String;
                thongTinThem.Add(item_phong);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTHUCLINH_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(TinhTongTienThucLinh(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                ClassCommon.reportExcelDTO _item_TONGTHU = new ClassCommon.reportExcelDTO();
                _item_TONGTHU.name = "TONGTHU";
                _item_TONGTHU.value = this.lstDataBaoCao[0].tongthu.ToString();
                thongTinThem.Add(_item_TONGTHU);

                ClassCommon.reportExcelDTO _item_TONGCHI = new ClassCommon.reportExcelDTO();
                _item_TONGCHI.name = "TONGCHI";
                _item_TONGCHI.value = this.lstDataBaoCao[0].tongchi.ToString();
                thongTinThem.Add(_item_TONGCHI);


                string fileTemplatePath = "BC_105_TrichThuongDVNuocSoi.xlsx";
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
        private decimal TinhTongTienThucLinh()
        {
            decimal _result = 0;
            try
            {
                foreach (var item in this.lstDataBaoCao)
                {
                    _result += item.thuclinh ?? 0;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return _result;
        }

        private void gridViewDataBC_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            try
            {
                int rowHandle = gridViewDataBC.FocusedRowHandle;
                string columeFieldName = gridViewDataBC.FocusedColumn.FieldName.ToString();

                if (columeFieldName == "tongchi")
                {
                    frmBC105_NhapTongChi frmCauHinh = new frmBC105_NhapTongChi();
                    frmCauHinh.MyGetData = new frmBC105_NhapTongChi.GetTongChi(GetTongChiFunction);
                    frmCauHinh.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        public void GetTongChiFunction(decimal _TongChi)
        {
            try
            {
                this.lstDataBaoCao[0].tongchi = _TongChi;
                this.lstDataBaoCao[0].thuclinh = (this.lstDataBaoCao[0].tongthu - this.lstDataBaoCao[0].tongchi) * (decimal)0.105;

                this.lstDataBaoCao[1].tongchi = _TongChi;
                this.lstDataBaoCao[1].thuclinh = (this.lstDataBaoCao[1].tongthu - this.lstDataBaoCao[1].tongchi) * (decimal)0.045;

                gridControlDataBC.DataSource = null;
                gridControlDataBC.DataSource = this.lstDataBaoCao;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion



    }
}
