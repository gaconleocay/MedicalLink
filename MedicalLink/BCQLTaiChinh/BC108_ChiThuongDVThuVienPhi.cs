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
    public partial class BC108_ChiThuongDVThuVienPhi : UserControl
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        // private string DanhMucDichVu_String { get; set; }
        #endregion

        public BC108_ChiThuongDVThuVienPhi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC108_ChiThuongDVThuVienPhi_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuMacDinh();
                //LoadDanhSachDichVu
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        //private void LoadDanhSachDichVu()
        //{
        //    try
        //    {
        //List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_106_DV").ToList();
        //if (lstOtherList != null && lstOtherList.Count > 0)
        //{
        //    for (int i = 0; i < lstOtherList.Count - 1; i++)
        //    {
        //        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
        //    }
        //    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        O2S_Common.Logging.LogSystem.Error(ex);
        //    }
        //}
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tieuchi_ser = "";
                string tieuchi_vp = "";
                string _tieuchi_mbp = "";
                string trangthai_vp = "";
                string _tieuchi_hsba = "";
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
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
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
                    string sql_timkiem = @"SELECT 1 as stt, 'Thần kinh (ĐNĐ) 03/18' as departmentgroupname, '' as quyetdinh_so, '21/6/2012' as quyetdinh_ngaythang, sum(ser.soluong) as soluong, ser.dongia, sum(ser.soluong*ser.dongia) as tongtienthu, 30 as tyle, sum(ser.soluong*ser.dongia*0.3) as thuong, '' as kynhan, '' as ghichu FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where servicepricecode in ('TD37018','TD37019') " + tieuchi_ser + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.dongia UNION ALL SELECT 2 as stt, 'Thận Nhân Tạo' as departmentgroupname, '' as quyetdinh_so, '' as quyetdinh_ngaythang, sum(ser.soluong) as soluong, ser.dongia, sum(ser.soluong*ser.dongia) as tongtienthu, 39.6 as tyle, sum(ser.soluong*ser.dongia*0.396) as thuong, '' as kynhan, '' as ghichu FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where servicepricecode='PT11437030' and departmentgroupid=14 " + tieuchi_ser + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid GROUP BY ser.dongia UNION ALL SELECT 3 as stt, 'Nội soi TMH (KBYC)' as departmentgroupname, '1557' as quyetdinh_so, '26/9/2016' as quyetdinh_ngaythang, sum(ser.soluong) as soluong, ser.dongia, sum(ser.soluong*ser.dongia) as tongtienthu, 0 as tyle, sum(ser.soluong*18000) as thuong, '' as kynhan, '' as ghichu FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,maubenhphamid, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where servicepricecode in ('U30001-3222','U19816-4240') " + tieuchi_ser + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select maubenhphamid from maubenhpham where userthuchien in (504,508) and departmentid_des=279 " + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid GROUP BY ser.dongia UNION ALL SELECT 4 as stt, 'Nội soi TMH khoa TMH' as departmentgroupname, '' as quyetdinh_so, '' as quyetdinh_ngaythang, sum(ser.soluong) as soluong, ser.dongia, sum(ser.soluong*ser.dongia) as tongtienthu, 0 as tyle, sum(ser.soluong*10000) as thuong, '' as kynhan, '' as ghichu FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,maubenhphamid, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where servicepricecode in ('U30001-3222','U19816-4240') " + tieuchi_ser + ") ser inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select maubenhphamid from maubenhpham where userthuchien not in (504,508) and departmentid_des=279 " + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid GROUP BY ser.dongia;";

                    DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                    if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBC.DataSource = _dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBC.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else//chi tiet
                {
                    string _sql_timkiemCT = "SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate) as stt, ser.servicepriceid, vp.patientid, vp.vienphiid, hsba.patientname, hsba.bhytcode, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, ngcd.username as nguoichidinh, mbp.maubenhphamid, mbp.maubenhphamdate, pth.departmentname as phongthuchien, ngth.username as nguoithuchien, (case when ser.servicepricecode in ('TD37018','TD37019') then 'Thần kinh (ĐNĐ) 03/18' when (ser.servicepricecode='PT11437030' and ser.departmentgroupid=14) then 'Thận Nhân Tạo' when (ser.servicepricecode in ('U30001-3222','U19816-4240') and mbp.userthuchien in (504,508)) then 'Nội soi TMH (KBYC)' when (ser.servicepricecode in ('U30001-3222','U19816-4240') and mbp.userthuchien not in (504,508)) then 'Nội soi TMH khoa TMH' end) as nhombaocao, ser.servicepricecode, ser.servicepricename, ser.soluong, ser.dongia, (ser.soluong*ser.dongia) as thanhtien, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, vp.vienphidate, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus, (case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' end) trangthaithutien FROM (select servicepriceid,vienphiid,maubenhphamid,departmentgroupid,departmentid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,servicepricedate,maubenhphamphieutype,bhyt_groupcode,loaidoituong from serviceprice where (servicepricecode in ('TD37018','TD37019','U30001-3222','U19816-4240') or (servicepricecode='PT11437030' and departmentgroupid=14)) " + tieuchi_ser + ") ser INNER JOIN (select maubenhphamid,maubenhphamstatus,maubenhphamdate,userid,departmentid_des,userthuchien from maubenhpham where maubenhphamgrouptype in (1,4) " + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 " + _tieuchi_hsba + _hosobenhanstatus + ") hsba on hsba.hosobenhanid=vp.hosobenhanid LEFT JOIN (select userhisid,username from nhompersonnel) ngcd ON ngcd.userhisid=mbp.userid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pth ON pth.departmentid=mbp.departmentid_des LEFT JOIN (select userhisid,username from nhompersonnel) ngth ON ngth.userhisid=mbp.userthuchien WHERE (case when ser.servicepricecode in ('U30001-3222','U19816-4240') then mbp.departmentid_des=279 end) or ser.servicepricecode<>'U30001-3222';";

                    DataTable _dataBCChiTiet = condb.GetDataTable_HIS(_sql_timkiemCT);
                    if (_dataBCChiTiet != null && _dataBCChiTiet.Rows.Count > 0)
                    {
                        gridControlBNDetail.DataSource = _dataBCChiTiet;
                    }
                    else
                    {
                        gridControlBNDetail.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                    reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                    _item_tien_string.name = "TONGTIEN_STRING";
                    _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                    thongTinThem.Add(_item_tien_string);

                    DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                    string fileTemplatePath = "BC_108_ChiThuongDVThuVienPhi.xlsx";
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_108_ChiThuongDVThuVienPhi.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Process
        private decimal TinhTongTien()
        {
            decimal _result = 0;
            try
            {
                for (int i = 0; i < gridViewDataBC.RowCount; i++)
                {
                    _result += Utilities.TypeConvertParse.ToDecimal(gridViewDataBC.GetRowCellValue(i, "thuong").ToString());
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return _result;
        }

        #endregion


    }
}
