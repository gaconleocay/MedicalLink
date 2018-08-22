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
    public partial class BC117_MoYCBangPPKinhHienVi : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        //private string DanhMucDichVu_String { get; set; }

        #endregion

        public BC117_MoYCBangPPKinhHienVi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC117_MoYCBangPPKinhHienVi_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuMacDinh();
                //LoadDanhSachDichVu();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                gridControlDataBC.Visible = true;
                gridControlDataBC.Dock = DockStyle.Fill;
                gridControlBNDetail.Visible = false;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        //private void LoadDanhSachDichVu()
        //{
        //    try
        //    {
        //        //load danh muc dich vu
        //        //List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_115_DV").ToList();
        //        //if (lstOtherList != null && lstOtherList.Count > 0)
        //        //{
        //        //    for (int i = 0; i < lstOtherList.Count - 1; i++)
        //        //    {
        //        //        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
        //        //    }
        //        //    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}
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
                //string lstdichvu_ser = " and servicepricecode in (" + this.DanhMucDichVu_String + ") ";
                string trangthai_vp = "";
                string _tieuchi_hsba = "";
                string _tieuchi_mbp = "";
                string _hosobenhanstatus = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between ''" + datetungay + "'' and ''" + datedenngay + "'' ";
                    tieuchi_ser = " and servicepricedate >= '" + datetungay + "' ";
                    _tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between ''" + datetungay + "'' and ''" + datedenngay + "'' ";
                    tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                    _tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
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
                    _hosobenhanstatus = " and hosobenhanstatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                    _hosobenhanstatus = " and hosobenhanstatus=1 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and vienphistatus_vp=1 ";
                    _hosobenhanstatus = " and hosobenhanstatus=1 ";
                }

                if (radioXemTongHop.Checked)//tong hop
                {
                    string sql_timkiem = @"SELECT row_number () over (order by nv.username) as stt, nv.username,nv.usercode, '' as departmentgroupname, sum(case when PT.user_loai='mochinhid' then PT.soluong else 0 end) as sl_mochinh, sum(case when PT.user_loai='mochinhid' then PT.soluong*800000 else 0 end) as tien_mochinh, sum(case when PT.user_loai='phuid' then PT.soluong else 0 end) as sl_phu, sum(case when PT.user_loai='phuid' then PT.soluong*500000 else 0 end) as tien_phu, sum(case when PT.user_loai='bacsigaymeid' then PT.soluong else 0 end) as sl_bacsigayme, sum(case when PT.user_loai='bacsigaymeid' then PT.soluong*350000 else 0 end) as tien_bacsigayme, sum(case when PT.user_loai='ktvphumeid' then PT.soluong else 0 end) as sl_ktvphume, sum(case when PT.user_loai='ktvphumeid' then PT.soluong*175000 else 0 end) as tien_ktvphume, sum(case when PT.user_loai='dungcuvienid' then PT.soluong else 0 end) as sl_dungcuvien, sum(case when PT.user_loai='dungcuvienid' then PT.soluong*175000 else 0 end) as tien_dungcuvien, sum(case PT.user_loai when 'mochinhid' then PT.soluong*800000 when 'phuid' then PT.soluong*500000 when 'bacsigaymeid' then PT.soluong*350000 when 'ktvphumeid' then PT.soluong*175000 when 'dungcuvienid' then PT.soluong*175000 else 0 end) as thuclinh, '' as kynhan, '' as ghichu FROM (select pttt.mochinhid as userid, 'mochinhid' as user_loai, sum(pttt.soluong) as soluong from (select vienphiid,soluong,mochinhid from ml_thuchienpttt where servicepricecode='U11970-3701' and mochinhid>0 " + tieuchi_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.mochinhid union all select pttt.phu1id as userid, 'phuid' as user_loai, sum(pttt.soluong) as soluong from (select vienphiid,(case when phu2id>0 then soluong/2 else soluong end) as soluong,phu1id from ml_thuchienpttt where servicepricecode='U11970-3701' and phu1id>0 " + tieuchi_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.phu1id union all select pttt.phu2id as userid, 'phuid' as user_loai, sum(pttt.soluong) as soluong from (select vienphiid,(case when phu1id>0 then soluong/2 else soluong end) as soluong,phu2id from ml_thuchienpttt where servicepricecode='U11970-3701' and phu2id>0 " + tieuchi_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.phu2id union all select pttt.bacsigaymeid as userid, 'bacsigaymeid' as user_loai, sum(pttt.soluong) as soluong from (select vienphiid,soluong,bacsigaymeid from ml_thuchienpttt where servicepricecode='U11970-3701' and bacsigaymeid>0 " + tieuchi_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.bacsigaymeid union all select pttt.ktvphumeid as userid, 'ktvphumeid' as user_loai, sum(pttt.soluong) as soluong from (select vienphiid,soluong,ktvphumeid from ml_thuchienpttt where servicepricecode='U11970-3701' and ktvphumeid>0 " + tieuchi_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.ktvphumeid union all select pttt.dungcuvienid as userid, 'dungcuvienid' as user_loai, sum(pttt.soluong) as soluong from (select vienphiid,soluong,dungcuvienid from ml_thuchienpttt where servicepricecode='U11970-3701' and dungcuvienid>0 " + tieuchi_ser + ") pttt inner join (select * from dblink('myconn','select vienphiid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + "') as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid group by pttt.dungcuvienid) PT INNER JOIN ml_nhanvien nv ON nv.userhisid=PT.userid GROUP BY nv.usercode,nv.username; ";

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
                else//chi tiet
                {
                    string _sql_timkiemCT = "";

                    DataTable _dataBCChiTiet = condb.GetDataTable_HIS(_sql_timkiemCT);
                    if (_dataBCChiTiet != null && _dataBCChiTiet.Rows.Count > 0)
                    {
                        gridControlBNDetail.DataSource = _dataBCChiTiet;
                    }
                    else
                    {
                        gridControlBNDetail.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
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
                if (radioXemTongHop.Checked)
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

                    string fileTemplatePath = "BC_117_MoYCBangPPKinhHienVi.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
                }
                else
                {
                    DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBNDetail);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("DANH SÁCH BỆNH NHÂN CHỈ ĐỊNH DỊCH VỤ", _dataBaoCao);
                }
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

                string fileTemplatePath = "BC_117_MoYCBangPPKinhHienVi.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Events
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlDataBC.Visible = true;
                    gridControlDataBC.DataSource = null;
                    gridControlDataBC.Dock = DockStyle.Fill;
                    gridControlBNDetail.Visible = false;
                    btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void radioXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemChiTiet.Checked)
                {
                    radioXemTongHop.Checked = false;
                    gridControlBNDetail.Visible = true;
                    gridControlBNDetail.DataSource = null;
                    gridControlBNDetail.Dock = DockStyle.Fill;
                    gridControlDataBC.Visible = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
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
