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
using MedicalLink.Utilities.GridControl;
using MedicalLink.Utilities;
using MedicalLink.ClassCommon;

namespace MedicalLink.BaoCao
{
    public partial class ucTKSuDungDichVuTheoKhoa : UserControl
    {
        #region Khai Bao
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }

        #endregion

        public ucTKSuDungDichVuTheoKhoa()
        {
            InitializeComponent();
        }

        #region Load
        private void ucTKSuDungDichVuTheoKhoa_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDuLieuMacDinh();
                LoadDanhSachKhoa();
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
                radioXemChiTiet.Checked = true;
                radioXemTongHop.Checked = false;
                gridControlDataBaoCao.DataSource = null;
                gridControlDataBaoCao_TH.DataSource = null;

                gridControlDataBaoCao.Visible = true;
                gridControlDataBaoCao.Dock = DockStyle.Fill;
                gridControlDataBaoCao_TH.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                string _lstDSKhoaID = "";
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_34_KHOA").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    _lstDSKhoaID = lstOtherList[0].tools_otherlistvalue;
                }

                string _getDSKhoa = "select departmentgroupid,departmentgroupname from departmentgroup where departmentgroupid in ("+ _lstDSKhoaID + ");";
                System.Data.DataTable _DSKhoa = condb.GetDataTable_HIS(_getDSKhoa);
                if (_DSKhoa != null && _DSKhoa.Rows.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = _DSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                else
                {
                    var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                    if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                    {
                        chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                        chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                        chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                    }
                }          
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

        #region Tim Kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_hsba = "";
                string _doituong_ser = " and loaidoituong not in (5,7,9) ";
                string _lstPhongChonLayBC = "";
                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //tieu chi
                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_hsba = " where hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                // doi tuong
                if (cboDoiTuong.Text == "Viện phí")
                {
                    _doituong_ser = " and loaidoituong in (1,2,3,8,20) ";
                }
                else if (cboDoiTuong.Text == "BHYT")
                {
                    _doituong_ser = " and loaidoituong in (0,4,6)";
                }
                //phong
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        _lstPhongChonLayBC += lstPhongCheck[i] + ",";
                    }
                    _lstPhongChonLayBC += lstPhongCheck[lstPhongCheck.Count - 1];
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }

                //Lay du lieu bao cao
                if (radioXemTongHop.Checked) //xem tong hop
                {
                    string _sql_timkiem = "select row_number () over (order by ser.bhyt_groupcode,ser.servicepricename) as stt, ser.servicepricecode, ser.servicepricename, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, (case ser.bhyt_groupcode when '01KB' then '01-Khám bệnh' when '03XN' then '02-Xét nghiệm' when '04CDHA' then '04-CĐHA' when '05TDCN' then '05-Thăm dò chức năng' when '06PTTT' then '06-PTTT' when '07KTC' then '07-DV KTC' else '99-Khác' end) as bhyt_groupcode, sum(ser.soluong) as soluong, (case ser.loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan when 3 then servicepricemoney else servicepricemoney end) as servicepricemoney, sum((case ser.loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan when 3 then servicepricemoney else servicepricemoney end)*ser.soluong) as thanhtien, '0' as isgroup from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,bhyt_groupcode from serviceprice where departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + _doituong_ser + ") ser inner join (select hosobenhanid from hosobenhan " + _tieuchi_hsba + ") hsba on hsba.hosobenhanid=ser.hosobenhanid group by ser.servicepricecode,ser.servicepricename,ser.loaidoituong,ser.bhyt_groupcode,ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney; ";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBaoCao_TH.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBaoCao_TH.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else if (radioXemChiTiet.Checked)//xem chi tiet
                {
                    string _sql_timkiem = "select row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname,ser.servicepricename) as stt, hsba.patientid, ser.vienphiid, hsba.patientname, bh.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi, ser.servicepricecode, ser.servicepricename, ser.servicepricedate, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, ser.soluong, (case ser.loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan when 3 then servicepricemoney else servicepricemoney end) as servicepricemoney, ((case ser.loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan when 3 then servicepricemoney else servicepricemoney end)*ser.soluong) as thanhtien, ser.departmentid, de.departmentname, ser.departmentgroupid, degp.departmentgroupname, '0' as isgroup from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,servicepricedate from serviceprice where departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + _doituong_ser + ") ser inner join (select hosobenhanid,patientid,patientname,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname from hosobenhan " + _tieuchi_hsba + ") hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select vienphiid,hosobenhanid,bhytid from vienphi) vp on vp.hosobenhanid=hsba.hosobenhanid inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid;";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBaoCao.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBaoCao.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else if (radioXemDSBenhNhan.Checked)
                {
                    string _sql_timkiem = "SELECT row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname) as stt, hsba.patientid, ser.vienphiid, hsba.patientname, bh.bhytcode, to_char(hsba.birthday,'yyyy') as year, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi, sum((case ser.loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan when 3 then servicepricemoney else servicepricemoney end)*ser.soluong) as thanhtien, ser.departmentid, de.departmentname, ser.departmentgroupid, degp.departmentgroupname, '0' as isgroup FROM (select hosobenhanid,vienphiid,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid from serviceprice where departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + _doituong_ser + ") ser inner join (select hosobenhanid,patientid,patientname,bhytcode,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname from hosobenhan " + _tieuchi_hsba + ") hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select vienphiid,hosobenhanid,bhytid from vienphi) vp on vp.hosobenhanid=hsba.hosobenhanid inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid GROUP BY hsba.patientid,ser.vienphiid,hsba.patientname,bh.bhytcode,hsba.birthday,hsba.hc_sonha,hsba.hc_thon,hsba.hc_xacode,hsba.hc_xaname,hsba.hc_huyencode,hsba.hc_huyenname,ser.departmentid,de.departmentname,ser.departmentgroupid,degp.departmentgroupname; ";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataDSBN.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataDSBN.DataSource = null;
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

        #region Export va Print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO report_khoa = new ClassCommon.reportExcelDTO();
                report_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                report_khoa.value = chkcomboListDSKhoa.Text; ;
                thongTinThem.Add(report_khoa);
                ClassCommon.reportExcelDTO report_doituong = new ClassCommon.reportExcelDTO();
                report_doituong.name = Base.BienTrongBaoCao.LOAIDOITUONG;
                report_doituong.value = cboDoiTuong.Text;
                thongTinThem.Add(report_doituong);

                string fileTemplatePath = "";
                System.Data.DataTable data_XuatBaoCao = new System.Data.DataTable();
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_34_ThongKeSuDungDichVuUngBuou_ChiTiet.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_CT();
                }
                else if (radioXemTongHop.Checked)
                {
                    fileTemplatePath = "BC_34_ThongKeSuDungDichVuUngBuou_TongHop.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_TH();
                }
                else if (radioXemDSBenhNhan.Checked)
                {
                    fileTemplatePath = "BC_34_DanhSachBenhNhanUngBuou.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_DSBN();
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO report_khoa = new ClassCommon.reportExcelDTO();
                report_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                report_khoa.value = chkcomboListDSKhoa.Text; ;
                thongTinThem.Add(report_khoa);
                ClassCommon.reportExcelDTO report_doituong = new ClassCommon.reportExcelDTO();
                report_doituong.name = Base.BienTrongBaoCao.LOAIDOITUONG;
                report_doituong.value = cboDoiTuong.Text;
                thongTinThem.Add(report_doituong);


                string fileTemplatePath = "";
                System.Data.DataTable data_XuatBaoCao = new System.Data.DataTable();
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_34_ThongKeSuDungDichVuUngBuou_ChiTiet.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_CT();
                }
                else if (radioXemTongHop.Checked)
                {
                    fileTemplatePath = "BC_34_ThongKeSuDungDichVuUngBuou_TongHop.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_TH();
                }
                else if (radioXemDSBenhNhan.Checked)
                {
                    fileTemplatePath = "BC_34_DanhSachBenhNhanUngBuou.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_DSBN();
                }
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume_TH()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCTKSuDungDV_UngBuouTHDTO> lstData_XuatBaoCao = new List<ClassCommon.BCTKSuDungDV_UngBuouTHDTO>();
                List<ClassCommon.BCTKSuDungDV_UngBuouTHDTO> lstDataDoanhThu = DataTables.DataTableToList<ClassCommon.BCTKSuDungDV_UngBuouTHDTO>(this.dataBaoCao);

                List<ClassCommon.BCTKSuDungDV_UngBuouTHDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.bhyt_groupcode).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BCTKSuDungDV_UngBuouTHDTO data_groupname = new ClassCommon.BCTKSuDungDV_UngBuouTHDTO();

                    List<ClassCommon.BCTKSuDungDV_UngBuouTHDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.bhyt_groupcode == item_group.bhyt_groupcode).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;

                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                    }

                    data_groupname.bhyt_groupcode = item_group.bhyt_groupcode;
                    data_groupname.stt = item_group.bhyt_groupcode;
                    data_groupname.soluong = sum_soluong;
                    data_groupname.thanhtien = sum_thanhtien;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
        private DataTable ExportExcel_GroupColume_CT()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstData_XuatBaoCao = new List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO>();
                List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstDataDoanhThu = DataTables.DataTableToList<ClassCommon.BCTKSuDungDV_UngBuouCTDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstGroup_Khoa = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BCTKSuDungDV_UngBuouCTDTO data_khoa_name = new ClassCommon.BCTKSuDungDV_UngBuouCTDTO();
                    List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstData_Khoa = lstDataDoanhThu.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;

                    foreach (var item_tinhsum in lstData_Khoa)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                    }
                    data_khoa_name.departmentgroupname = item_khoa.departmentgroupname;
                    data_khoa_name.stt = item_khoa.departmentgroupname;
                    data_khoa_name.soluong = sum_soluong;
                    data_khoa_name.thanhtien = sum_thanhtien;
                    data_khoa_name.isgroup = 1;
                    lstData_XuatBaoCao.Add(data_khoa_name);
                    //Phong
                    List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstGroup_Phong = lstData_Khoa.GroupBy(o => o.departmentid).Select(n => n.First()).ToList();
                    foreach (var item_phong in lstGroup_Phong)
                    {
                        ClassCommon.BCTKSuDungDV_UngBuouCTDTO data_phong_name = new ClassCommon.BCTKSuDungDV_UngBuouCTDTO();
                        List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstData_Phong = lstData_Khoa.Where(o => o.departmentid == item_phong.departmentid).ToList();
                        decimal sum_soluong_phong = 0;
                        decimal sum_thanhtien_phong = 0;

                        foreach (var item_tinhsum in lstData_Phong)
                        {
                            sum_soluong_phong += item_tinhsum.soluong;
                            sum_thanhtien_phong += item_tinhsum.thanhtien;
                        }
                        data_phong_name.departmentname = item_phong.departmentname;
                        data_phong_name.stt ="' - "+ item_phong.departmentname;
                        data_phong_name.soluong = sum_soluong_phong;
                        data_phong_name.thanhtien = sum_thanhtien_phong;
                        data_phong_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_phong_name);
                        //Benh nhan
                        List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstGroup_BN = lstData_Phong.GroupBy(o => o.vienphiid).Select(n => n.First()).ToList();
                        foreach (var item_bn in lstGroup_BN)
                        {
                            ClassCommon.BCTKSuDungDV_UngBuouCTDTO data_bn_name = new ClassCommon.BCTKSuDungDV_UngBuouCTDTO();
                            List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstData_bn = lstData_Phong.Where(o => o.vienphiid == item_bn.vienphiid).ToList();
                            decimal sum_soluong_bn = 0;
                            decimal sum_thanhtien_bn = 0;

                            List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO> lstData_DichVu = new List<ClassCommon.BCTKSuDungDV_UngBuouCTDTO>();
                            for (int i = 0; i < lstData_bn.Count; i++)
                            {
                                ClassCommon.BCTKSuDungDV_UngBuouCTDTO data_bn_dichvu = new ClassCommon.BCTKSuDungDV_UngBuouCTDTO();
                                sum_soluong_bn += lstData_bn[i].soluong;
                                sum_thanhtien_bn += lstData_bn[i].thanhtien;

                                data_bn_dichvu.stt = (i + 1).ToString();
                                data_bn_dichvu.servicepricecode = lstData_bn[i].servicepricecode;
                                data_bn_dichvu.servicepricename = lstData_bn[i].servicepricename;
                                data_bn_dichvu.servicepricedate = lstData_bn[i].servicepricedate;
                                data_bn_dichvu.soluong = lstData_bn[i].soluong;
                                data_bn_dichvu.servicepricemoney = lstData_bn[i].servicepricemoney;
                                data_bn_dichvu.thanhtien = lstData_bn[i].thanhtien;
                                data_bn_dichvu.loaidoituong = lstData_bn[i].loaidoituong;

                                lstData_DichVu.Add(data_bn_dichvu);
                            }
                            data_bn_name.patientname = item_bn.patientname;
                            data_bn_name.patientid = item_bn.patientid;
                            data_bn_name.vienphiid = item_bn.vienphiid;
                            data_bn_name.bhytcode = item_bn.bhytcode;
                            data_bn_name.diachi = item_bn.diachi;
                            data_bn_name.stt = "";
                            data_bn_name.soluong = sum_soluong_bn;
                            data_bn_name.thanhtien = sum_thanhtien_bn;
                            data_bn_name.isgroup = 3;
                            lstData_XuatBaoCao.Add(data_bn_name);
                            lstData_XuatBaoCao.AddRange(lstData_DichVu);
                        }
                    }
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }
        private DataTable ExportExcel_GroupColume_DSBN()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO> lstData_XuatBaoCao = new List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO>();
                List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO> lstDataDoanhThu = DataTables.DataTableToList<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO> lstGroup_Khoa = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO data_khoa_name = new ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO();
                    List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO> lstData_Khoa = lstDataDoanhThu.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
                    decimal sum_thanhtien = 0;

                    foreach (var item_tinhsum in lstData_Khoa)
                    {
                        sum_thanhtien += item_tinhsum.thanhtien;
                    }
                    data_khoa_name.departmentgroupname = item_khoa.departmentgroupname;
                    data_khoa_name.stt = item_khoa.departmentgroupname;
                    data_khoa_name.thanhtien = sum_thanhtien;
                    data_khoa_name.isgroup = 1;
                    lstData_XuatBaoCao.Add(data_khoa_name);
                    //Phong
                    List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO> lstGroup_Phong = lstData_Khoa.GroupBy(o => o.departmentid).Select(n => n.First()).ToList();
                    foreach (var item_phong in lstGroup_Phong)
                    {
                        ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO data_phong_name = new ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO();
                        List<ClassCommon.BCTKSuDungDV_UngBuouDSBNDTO> lstData_Phong = lstData_Khoa.Where(o => o.departmentid == item_phong.departmentid).ToList();
                        decimal sum_thanhtien_phong = 0;

                        foreach (var item_tinhsum in lstData_Phong)
                        {
                            sum_thanhtien_phong += item_tinhsum.thanhtien;
                        }
                        data_phong_name.departmentname = item_phong.departmentname;
                        data_phong_name.stt = "' - " + item_phong.departmentname;
                        data_phong_name.thanhtien = sum_thanhtien_phong;
                        data_phong_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_phong_name);
                        lstData_XuatBaoCao.AddRange(lstData_Phong);
                    }
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        #endregion

        #region Event Change
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    radioXemDSBenhNhan.Checked = false;
                    gridControlDataBaoCao_TH.Visible = true;
                    gridControlDataBaoCao_TH.DataSource = null;
                    gridControlDataBaoCao_TH.Dock = DockStyle.Fill;
                    gridControlDataBaoCao.Visible = false;
                    gridControlDataDSBN.Visible = false;
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
                    radioXemDSBenhNhan.Checked = false;
                    gridControlDataBaoCao.Visible = true;
                    gridControlDataBaoCao.DataSource = null;
                    gridControlDataBaoCao.Dock = DockStyle.Fill;
                    gridControlDataBaoCao_TH.Visible = false;
                    gridControlDataDSBN.Visible = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void radioXemDSBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemDSBenhNhan.Checked)
                {
                    radioXemTongHop.Checked = false;
                    radioXemChiTiet.Checked = false;
                    gridControlDataDSBN.Visible = true;
                    gridControlDataDSBN.DataSource = null;
                    gridControlDataDSBN.Dock = DockStyle.Fill;
                    gridControlDataBaoCao_TH.Visible = false;
                    gridControlDataBaoCao.Visible = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }




        #endregion


    }
}
