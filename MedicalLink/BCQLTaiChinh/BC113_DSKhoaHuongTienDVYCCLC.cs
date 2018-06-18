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
    public partial class BC113_DSKhoaHuongTienDVYCCLC : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<DSKhoaHuongTienDVYCCLCDTO> lstBaoCao { get; set; }
        private string DanhMucDichVu_String { get; set; }
        //private string DanhMucKhoa_String { get; set; }

        #endregion

        public BC113_DSKhoaHuongTienDVYCCLC()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC113_DSKhoaHuongTienDVYCCLC_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_113_DV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    for (int i = 0; i < lstOtherList.Count - 1; i++)
                    {
                        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
                }
                ////Load danh muc Khoa
                //List<ClassCommon.ToolsOtherListDTO> lstOther_Khoa = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_113_KHOA").ToList();
                //if (lstOther_Khoa != null && lstOther_Khoa.Count > 0)
                //{
                //    for (int i = 0; i < lstOther_Khoa.Count - 1; i++)
                //    {
                //        this.DanhMucKhoa_String += lstOther_Khoa[i].tools_otherlistvalue + ",";
                //    }
                //    this.DanhMucKhoa_String += lstOther_Khoa[lstOther_Khoa.Count - 1].tools_otherlistvalue;
                //}
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
                string tieuchi_mbp = "";
                string tieuchi_mrd = "";
                string lstdichvu_ser = " and servicepricecode in (" + this.DanhMucDichVu_String + ") ";
                //string lstkhoa_ser = " and departmentgroupid in (" + this.DanhMucKhoa_String + ") ";
                string trangthai_vp = "";
                string sql_timkiem = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_mrd = " and thoigianvaovien>'2017-01-01 00:00:00' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_ser = " and servicepricedate >= '" + datetungay + "' ";
                    tieuchi_mrd = " and thoigianvaovien>'" + datetungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                    tieuchi_mrd = " and thoigianvaovien>'2017-01-01 00:00:00' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                    tieuchi_mrd = " and thoigianvaovien>'2017-01-01 00:00:00' ";
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

                sql_timkiem = @" SELECT row_number () over (order by degp.departmentgroupname) as stt, degp.departmentgroupid, degp.departmentgroupname as khoachuyenden, SER.soluong_2, SER.thanhtien_2, SER.soluong_3, SER.thanhtien_3, SER.soluong_5, SER.thanhtien_5, SER.thanhtien, (SER.thanhtien*0.02) as tienthue2, (SER.thanhtien-(SER.thanhtien*0.02)) as tiensauthue, ((SER.thanhtien-(SER.thanhtien*0.02))*0.05) as tientrich5, '' as kynhan FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp INNER JOIN (select mrd.backdepartmentid, sum(case when ser.dongia=2000000 then ser.soluong else 0 end) as soluong_2, sum(case when ser.dongia=2000000 then ser.soluong*ser.dongia else 0 end) as thanhtien_2, sum(case when ser.dongia=3000000 then ser.soluong else 0 end) as soluong_3, sum(case when ser.dongia=3000000 then ser.soluong*ser.dongia else 0 end) as thanhtien_3, sum(case when ser.dongia=5000000 then ser.soluong else 0 end) as soluong_5, sum(case when ser.dongia=5000000 then ser.soluong*ser.dongia else 0 end) as thanhtien_5, sum(ser.soluong*ser.dongia) as thanhtien from (select vienphiid,soluong,medicalrecordid, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia from serviceprice where 1=1 " + tieuchi_ser + lstdichvu_ser + ") ser inner join (select backdepartmentid,medicalrecordid,medicalrecordid_next from medicalrecord where 1=1 " + tieuchi_mrd + ") mrd on mrd.medicalrecordid=ser.medicalrecordid inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid group by mrd.backdepartmentid) SER on SER.backdepartmentid=degp.departmentgroupid; ";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    this.lstBaoCao = Utilities.DataTables.DataTableToList<DSKhoaHuongTienDVYCCLCDTO>(_dataBaoCao);
                    XuLyVaHienThiBaoCao(this.lstBaoCao);
                    //gridControlDataBC.DataSource = _dataBaoCao;
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
                //ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                //_item_tien_string.name = "TONGTIEN_STRING";
                //_item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                //thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_113_DSKhoaHuongTienDVYCCLC.xlsx";
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
                //ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                //_item_tien_string.name = "TONGTIEN_STRING";
                //_item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                //thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_113_DSKhoaHuongTienDVYCCLC.xlsx";
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
        private void XuLyVaHienThiBaoCao(List<DSKhoaHuongTienDVYCCLCDTO> _lstBaoCao)
        {
            try
            {
                this.lstBaoCao = new List<DSKhoaHuongTienDVYCCLCDTO>();
                DSKhoaHuongTienDVYCCLCDTO _gayme = new DSKhoaHuongTienDVYCCLCDTO();
                DSKhoaHuongTienDVYCCLCDTO _lanhdao = new DSKhoaHuongTienDVYCCLCDTO();
                foreach (var item in _lstBaoCao)
                {
                    _gayme.stt = _lstBaoCao.Count + 1;
                    _gayme.departmentgroupid = 21;
                    _gayme.khoachuyenden = "Khoa Gây Mê Hồi Tỉnh";
                    _gayme.soluong_2 += item.soluong_2;
                    _gayme.thanhtien_2 += item.thanhtien_2;
                    _gayme.soluong_3 += item.soluong_3;
                    _gayme.thanhtien_3 += item.thanhtien_3;
                    _gayme.soluong_5 += item.soluong_5;
                    _gayme.thanhtien_5 += item.thanhtien_5;
                    _gayme.thanhtien += item.thanhtien;
                    _gayme.tienthue2 += item.tienthue2;
                    _gayme.tiensauthue += item.tiensauthue;
                    _gayme.tientrich5 += item.tientrich5;
                    _gayme.kynhan = "";
                }

                this.lstBaoCao = _lstBaoCao;
                this.lstBaoCao.Add(_gayme);

                _lanhdao.stt = _lstBaoCao.Count + 2;
                _lanhdao.departmentgroupid = 0;
                _lanhdao.khoachuyenden = "Ban lãnh đạo quản lý";
                _lanhdao.soluong_2 += _gayme.soluong_2;
                _lanhdao.thanhtien_2 += _gayme.thanhtien_2;
                _lanhdao.soluong_3 += _gayme.soluong_3;
                _lanhdao.thanhtien_3 += _gayme.thanhtien_3;
                _lanhdao.soluong_5 += _gayme.soluong_5;
                _lanhdao.thanhtien_5 += _gayme.thanhtien_5;
                _lanhdao.thanhtien += _gayme.thanhtien;
                _lanhdao.tienthue2 += _gayme.tienthue2;
                _lanhdao.tiensauthue += _gayme.tiensauthue;
                _lanhdao.tientrich5 += _gayme.tientrich5;
                _lanhdao.kynhan = "";

                this.lstBaoCao.Add(_lanhdao);

                gridControlDataBC.DataSource = null;
                gridControlDataBC.DataSource = this.lstBaoCao;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }


        #endregion


    }
}
