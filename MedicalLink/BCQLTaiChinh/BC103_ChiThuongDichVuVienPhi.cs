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
using MedicalLink.ClassCommon.BCQLTaiChinh;

namespace MedicalLink.BCQLTaiChinh
{
    public partial class BC103_ChiThuongDichVuVienPhi : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ChiThuongDichVuVienPhiDTO> lstDataBCVP { get; set; }
        //private string DanhMucDichVu_KB_String { get; set; }
        private string DanhMucDichVu_SADT_String { get; set; }
        private string DanhMucDichVu_XN_String { get; set; }
        #endregion

        public BC103_ChiThuongDichVuVienPhi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC103_ChiThuongDichVuVienPhi_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucDichVu();
        }
        private void LoadDanhMucDichVu()
        {
            try
            {
                ////KB
                //List<ClassCommon.ToolsOtherListDTO> lstOtherList_KB = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_103_DV_KB").ToList();
                //if (lstOtherList_KB != null && lstOtherList_KB.Count > 0)
                //{
                //    for (int i = 0; i < lstOtherList_KB.Count - 1; i++)
                //    {
                //        this.DanhMucDichVu_KB_String += lstOtherList_KB[i].tools_otherlistvalue + ",";
                //    }
                //    this.DanhMucDichVu_KB_String += lstOtherList_KB[lstOtherList_KB.Count - 1].tools_otherlistvalue;
                //}
                //SADT
                List<ClassCommon.ToolsOtherListDTO> lstOtherList_SADT = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_103_DV_SADT").ToList();
                if (lstOtherList_SADT != null && lstOtherList_SADT.Count > 0)
                {
                    for (int i = 0; i < lstOtherList_SADT.Count - 1; i++)
                    {
                        this.DanhMucDichVu_SADT_String += lstOtherList_SADT[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVu_SADT_String += lstOtherList_SADT[lstOtherList_SADT.Count - 1].tools_otherlistvalue;
                }
                //XN
                List<ClassCommon.ToolsOtherListDTO> lstOtherList_XN = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_103_DV_XN").ToList();
                if (lstOtherList_XN != null && lstOtherList_XN.Count > 0)
                {
                    for (int i = 0; i < lstOtherList_XN.Count - 1; i++)
                    {
                        this.DanhMucDichVu_XN_String += lstOtherList_XN[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVu_XN_String += lstOtherList_XN[lstOtherList_XN.Count - 1].tools_otherlistvalue;
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
                string lstdichvu_ser_kb = " and servicepricecode='15' "; //" and servicepricecode in (" + this.DanhMucDichVu_KB_String + ") ";
                string lstdichvu_ser_kbdtyc = " and servicepricecode='14' ";
                string lstdichvu_ser_kbyc = " and servicepricecode='14' ";
                string lstdichvu_ser_kbycth7cn = " and servicepricecode='U18843-5947' ";
                string lstdichvu_ser_sadt = " and servicepricecode in (" + this.DanhMucDichVu_SADT_String + ") ";
                string lstdichvu_ser_xn = "";//" and servicepricecode in (" + this.DanhMucDichVu_XN_String + ") ";
                string trangthai_vp = "";
                string sql_timkiem = "";
                string _createdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
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


                sql_timkiem = @"SELECT ser.departmentid as departmentgroupid, (ser.departmentid || 'KB') as keymapping, sum(ser.soluong) as soluong_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn, ser.dongia, sum(ser.soluong*ser.dongia) as thanhtien_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode='01KB' and departmentid not in (398,205,206,207,208,209,211) and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser_kb + ") ser inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.departmentid,ser.dongia UNION ALL SELECT ser.departmentid as departmentgroupid, (ser.departmentid || 'KB') as keymapping, sum(ser.soluong) as soluong_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn, ser.dongia, sum(ser.soluong*ser.dongia) as thanhtien_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode='01KB' and departmentid=398 and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser_kbdtyc + ") ser inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.departmentid,ser.dongia UNION ALL SELECT ser.departmentid as departmentgroupid, (ser.departmentid || 'KB') as keymapping, sum(ser.soluong) as soluong_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn, ser.dongia, sum(ser.soluong*ser.dongia) as thanhtien_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode='01KB' and departmentid in (205,206,207,208,209,211) and EXTRACT(DOW FROM servicepricedate) not in (6,0) and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser_kbyc + ") ser inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.departmentid,ser.dongia UNION ALL SELECT ser.departmentid as departmentgroupid, (ser.departmentid || 'KBTH7CN') as keymapping, sum(ser.soluong) as soluong_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn, ser.dongia, sum(ser.soluong*ser.dongia) as thanhtien_tong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong, sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode='01KB' and departmentid in (205,206,207,208,209,211) and EXTRACT(DOW FROM servicepricedate) in (6,0) and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser_kbycth7cn + ") ser inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.departmentid,ser.dongia UNION ALL SELECT ser.departmentgroupid, (ser.departmentgroupid || 'SADT') as keymapping, sum(ser.soluong) as soluong_tong, 0 as soluong_ngaythuong, 0 as soluong_th7cn, 0 as dongia, 0 as thanhtien_tong, sum(ser.soluong*ser.dongia) as thanhtien_ngaythuong, 0 as thanhtien_th7cn FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN') and departmentgroupid=46 and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser_sadt + ") ser inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.departmentgroupid UNION ALL SELECT ser.departmentgroupid, (ser.departmentgroupid || 'XN') as keymapping, sum(ser.soluong) as soluong_tong, 0 as soluong_ngaythuong, 0 as soluong_th7cn, 0 as dongia, 0 as thanhtien_tong, sum(ser.soluong*ser.dongia) as thanhtien_ngaythuong, 0 as thanhtien_th7cn FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode in ('03XN','07KTC') and departmentgroupid=46 and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser_xn + ") ser inner join (select servicepricecode from servicepriceref where servicegrouptype=2 and pttt_loaiid=0) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.departmentgroupid;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    List<ChiThuongDichVuVienPhiTmpDTO> lstDVVPTmp = Utilities.DataTables.DataTableToList<ChiThuongDichVuVienPhiTmpDTO>(_dataBaoCao);
                    foreach (var item in lstDVVPTmp)
                    {
                        string _sqlinsert = "INSERT INTO ml_datachithuongdvvp_tmp(keymapping,departmentgroupid,soluong_tong,soluong_ngaythuong,soluong_th7cn,dongia,thanhtien_tong,thanhtien_ngaythuong,thanhtien_th7cn,createusercode,createdate) VALUES ('" + item.keymapping + "','" + item.departmentgroupid + "','" + item.soluong_tong + "','" + item.soluong_ngaythuong + "','" + item.soluong_th7cn + "','" + item.dongia + "','" + item.thanhtien_tong + "','" + item.thanhtien_ngaythuong + "','" + item.thanhtien_th7cn + "','" + Base.SessionLogin.SessionUsercode + "','" + _createdate + "')";
                        condb.ExecuteNonQuery_MeL(_sqlinsert);
                    }
                    LayDuLieuBaoCaoTuTmp(Base.SessionLogin.SessionUsercode, _createdate);
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

        private void LayDuLieuBaoCaoTuTmp(string _createusercode, string _createdate)
        {
            try
            {
                string _sqldatatmp = @"SELECT row_number () over (order by CT1.stt) as stt, CT1.departmentgroupname, CT1.quyetdinh_so, TO_CHAR(CT1.quyetdinh_ngay,'dd/MM/yyyy') as quyetdinh_ngay, coalesce(TMP1.soluong_tong,0) as soluong_tong, coalesce(TMP1.soluong_ngaythuong,0) as soluong_ngaythuong, coalesce(TMP1.soluong_th7cn,0) as soluong_th7cn, TMP1.dongia, coalesce(TMP1.thanhtien_tong) as thanhtien_tong, coalesce(TMP1.thanhtien_ngaythuong) as thanhtien_ngaythuong, CT1.tylehuong, ((TMP1.thanhtien_ngaythuong*(CT1.tylehuong/100.0))) as tienhuong, (TMP1.thanhtien_th7cn) as thanhtien_th7cn, 0 as chiphi, (TMP1.thanhtien_th7cn*0.15) as tienthuong_th7cn, ((TMP1.soluong_th7cn*CT1.tienbsi_th7cn)) as tienbsi_th7cn, ((TMP1.thanhtien_ngaythuong*(CT1.tylehuong/100.0))+(TMP1.thanhtien_th7cn*0.15)+(TMP1.soluong_th7cn*CT1.tienbsi_th7cn)) as tonghuong, '' as kynhan FROM (select stt,departmentgroupname,quyetdinh_so,quyetdinh_ngay,tylehuong,tienbsi_th7cn from ml_chiathuongdvvp group by stt,departmentgroupname,quyetdinh_so,quyetdinh_ngay,tylehuong,tienbsi_th7cn) CT1 LEFT JOIN (select ct.stt as stt1, sum(tmp.soluong_tong) as soluong_tong, sum(tmp.soluong_ngaythuong) as soluong_ngaythuong, sum(tmp.soluong_th7cn) as soluong_th7cn, tmp.dongia, sum(tmp.thanhtien_tong) as thanhtien_tong, sum(tmp.thanhtien_ngaythuong) as thanhtien_ngaythuong, sum((tmp.thanhtien_ngaythuong*(ct.tylehuong/100.0))) as tienhuong, sum(tmp.thanhtien_th7cn) as thanhtien_th7cn, sum((tmp.soluong_th7cn*ct.tienbsi_th7cn)) as tienbsi_th7cn from ml_chiathuongdvvp ct inner join (select * from ml_datachithuongdvvp_tmp where createusercode='" + _createusercode + "' and createdate='" + _createdate + "') tmp on tmp.keymapping=ct.keymapping group by ct.stt,ct.departmentgroupname,ct.quyetdinh_so,ct.quyetdinh_ngay,tmp.dongia,ct.tylehuong ) TMP1 on TMP1.stt1=CT1.stt;";
                DataTable _dataBC = condb.GetDataTable_MeL(_sqldatatmp);

                if (_dataBC != null && _dataBC.Rows.Count > 0)
                {
                    //
                    this.lstDataBCVP = Utilities.DataTables.DataTableToList<ChiThuongDichVuVienPhiDTO>(_dataBC);
                    gridControlDataBC.DataSource = this.lstDataBCVP;
                    //xoa du lieu tam di
                    string _sqldelete = "DELETE FROM ml_datachithuongdvvp_tmp WHERE createusercode='" + _createusercode + "' and createdate='" + _createdate + "';";
                  condb.ExecuteNonQuery_MeL(_sqldelete);

                }
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

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                ClassCommon.reportExcelDTO _item_TONGHUONG = new ClassCommon.reportExcelDTO();
                _item_TONGHUONG.name = "TONGHUONG_STRING";
                _item_TONGHUONG.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTienHuong(_dataBaoCao), 0).ToString());
                thongTinThem.Add(_item_TONGHUONG);


                string fileTemplatePath = "BC_103_ChiThuongDichVuVienPhi.xlsx";

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

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                ClassCommon.reportExcelDTO _item_TONGHUONG = new ClassCommon.reportExcelDTO();
                _item_TONGHUONG.name = "TONGHUONG_STRING";
                _item_TONGHUONG.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTienHuong(_dataBaoCao), 0).ToString());
                thongTinThem.Add(_item_TONGHUONG);

                string fileTemplatePath = "BC_103_ChiThuongDichVuVienPhi.xlsx";
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
        private void gridViewDataBC_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var rowHandle = gridViewDataBC.FocusedRowHandle;
                int _stt = Utilities.TypeConvertParse.ToInt32(gridViewDataBC.GetRowCellValue(rowHandle, "stt").ToString());

                foreach (var item in this.lstDataBCVP)
                {
                    if (item.stt == _stt)
                    {
                        item.chiphi = Utilities.TypeConvertParse.ToDecimal(gridViewDataBC.GetRowCellValue(rowHandle, "chiphi").ToString());
                        item.tienthuong_th7cn = (item.thanhtien_th7cn - item.chiphi) * (decimal)0.15;
                        item.tonghuong = item.tienhuong + item.tienthuong_th7cn + item.tienbsi_th7cn;
                    }
                }

                gridControlDataBC.DataSource = null;
                gridControlDataBC.DataSource = this.lstDataBCVP;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private decimal TinhTongTienHuong(DataTable _dataBaoCao)
        {
            decimal _result = 0;
            try
            {
                List<ChiThuongDichVuVienPhiDTO> _lstTrichThuong = Utilities.DataTables.DataTableToList<ChiThuongDichVuVienPhiDTO>(_dataBaoCao);
                foreach (var item in _lstTrichThuong)
                {
                    _result += item.tonghuong ?? 0;
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