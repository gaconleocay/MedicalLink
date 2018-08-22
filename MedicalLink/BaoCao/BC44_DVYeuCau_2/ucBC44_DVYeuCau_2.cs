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
using MedicalLink.Utilities.GridControl;
using MedicalLink.Utilities;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;

namespace MedicalLink.BaoCao
{
    public partial class ucBC44_DVYeuCau_2 : UserControl
    {
        #region Khai Bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }

        #endregion

        public ucBC44_DVYeuCau_2()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC44_DVYeuCau_2_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhSachKhoa();
                LoadDanhSachNguoiThu();
                cbbTieuChi_SelectedIndexChanged(null, null);
                LoadDanhSachExport();
                LoadDanhSachButonPrint();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachNguoiThu()
        {
            try
            {
                string sql_ngthu = "select userhisid,usercode,username from nhompersonnel where usergnhom='2';";
                System.Data.DataTable _dataThuNgan = condb.GetDataTable_HIS(sql_ngthu);
                chkcboListNguoiThuTien.Properties.DataSource = _dataThuNgan;
                chkcboListNguoiThuTien.Properties.DisplayMember = "username";
                chkcboListNguoiThuTien.Properties.ValueMember = "userhisid";
                chkcboListNguoiThuTien.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo Chi tiết"));
                menu.Items.Add(new DXMenuItem("Báo cáo Tổng hợp"));
                dropDownExport.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachButonPrint()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Báo cáo Chi tiết"));
                menu.Items.Add(new DXMenuItem("Báo cáo Tổng hợp"));
                dropDownPrint.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Print_Click;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Tim Kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _tieuchi_bill = "";
                string _lstPhongChonLayBC = "";
                string _listuserid = "";
                string _listuserid_thutien = "";
                //string _select_bill = "";
                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                //NGuoi thu tien
                //if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                //{
                List<Object> lstNhanVienCheck = chkcboListNguoiThuTien.Properties.Items.GetCheckedValues();
                if (lstNhanVienCheck.Count > 0 || chkTatCa.Checked)
                {
                    if (chkTatCa.Checked == false)
                    {
                        _listuserid_thutien = " and userid in (";
                        for (int i = 0; i < lstNhanVienCheck.Count - 1; i++)
                        {
                            _listuserid_thutien += "'" + lstNhanVienCheck[i] + "', ";
                        }
                        _listuserid_thutien += "'" + lstNhanVienCheck[lstNhanVienCheck.Count - 1] + "') ";
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }
                //}

                //tieu chi
                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_bill = " inner join (select billid,vienphiid,billgroupcode,billcode from bill where dahuyphieu=0 " + _listuserid_thutien + ") b on b.billid=ser.billid_clbh_thutien ";
                }
                else if (cbbTieuChi.Text == "Theo ngày thu tiền")
                {
                    _tieuchi_bill = " inner join (select billid,vienphiid,billgroupcode,billcode from bill where billdate between '" + datetungay + "' and '" + datedenngay + "'  and dahuyphieu=0 " + _listuserid_thutien + ") b on b.billid=ser.billid_clbh_thutien ";
                    //_select_bill = " b.billgroupcode, b.billcode, ";
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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }

                //Lay du lieu bao cao - ngay 4/5/2018
                string _sql_timkiem = @"SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname,de.departmentname,serf.report_tkcode order by ser.servicepricename)) as stt, ser.departmentgroupid, ser.departmentid, degp.departmentgroupname, de.departmentname, ser.servicepricecode, ser.servicepricename, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, serf.report_tkcode, tkgroup.billaccountrefname, sum(ser.soluong) as soluong, ser.servicepricemoney, sum(ser.servicepricemoney*ser.soluong) as thanhtien, ser.servicepricemoney_bhyt, sum(ser.servicepricemoney_bhyt*ser.soluong) as thanhtien_bhyt, sum(case when ser.servicepricemoney_bhyt<>0 then ((ser.servicepricemoney-ser.servicepricemoney_bhyt)*ser.soluong) else 0 end) as chenhlech, '0' as isgroup FROM (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,bhyt_groupcode,billid_clbh_thutien from serviceprice where loaidoituong in (3,4) and bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') and departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + ") ser inner join (select vienphiid from vienphi where 1=1 " + _tieuchi_vp + _listuserid + ") vp on vp.vienphiid=ser.vienphiid " + _tieuchi_bill + " inner join (select servicepricecode,servicepricegroupcode,servicepricename,report_tkcode from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode left join billaccountref tkgroup on tkgroup.billaccountrefcode=serf.report_tkcode inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid GROUP BY ser.servicepricecode,ser.servicepricename,ser.loaidoituong,serf.report_tkcode,tkgroup.billaccountrefname,ser.servicepricemoney,ser.servicepricemoney_bhyt,ser.departmentgroupid,ser.departmentid,degp.departmentgroupname,de.departmentname;";

                this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBaoCao_CT.DataSource = this.dataBaoCao;
                }
                else
                {
                    gridControlDataBaoCao_CT.DataSource = null;
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

        #region Export va Print
        void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Báo cáo Chi tiết")
                {
                    tbnExport_ChiTiet_Click();
                }
                else if (tenbaocao == "Báo cáo Tổng hợp")
                {
                    tbnExport_TongHop_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        void Item_Print_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Báo cáo Chi tiết")
                {
                    tbnPrint_ChiTiet_Click();
                }
                else if (tenbaocao == "Báo cáo Tổng hợp")
                {
                    tbnPrint_TongHop_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void tbnExport_ChiTiet_Click()
        {
            try
            {
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_44_ThongKeSuDungDichVuYeuCau_ChiTiet.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume_CT();

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExport_TongHop_Click()
        {
            try
            {
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_44_ThongKeSuDungDichVuYeuCau_TongHop.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume_TH();

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        private void tbnPrint_ChiTiet_Click()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_44_ThongKeSuDungDichVuYeuCau_ChiTiet.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume_CT();

                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void tbnPrint_TongHop_Click()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_44_ThongKeSuDungDichVuYeuCau_TongHop.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume_TH();

                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Process
        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume_CT()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_XuatBaoCao = new List<ClassCommon.BC44_DVYeuCau2_TongHopDTO>();
                List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstDataBC_Convert = DataTables.DataTableToList<ClassCommon.BC44_DVYeuCau2_TongHopDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstGroup_Khoa = lstDataBC_Convert.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList().OrderBy(or => or.departmentgroupname).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BC44_DVYeuCau2_TongHopDTO data_khoa_name = new ClassCommon.BC44_DVYeuCau2_TongHopDTO();
                    List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_Khoa = lstDataBC_Convert.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;
                    decimal sum_thanhtien_bhyt = 0;
                    decimal sum_chenhlech = 0;

                    foreach (var item_tinhsum in lstData_Khoa)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                        sum_thanhtien_bhyt += item_tinhsum.thanhtien_bhyt;
                        sum_chenhlech += item_tinhsum.chenhlech;
                    }
                    data_khoa_name.departmentgroupname = item_khoa.departmentgroupname;
                    data_khoa_name.stt = item_khoa.departmentgroupname;
                    data_khoa_name.soluong = sum_soluong;
                    data_khoa_name.thanhtien = sum_thanhtien;
                    data_khoa_name.thanhtien_bhyt = sum_thanhtien_bhyt;
                    data_khoa_name.chenhlech = sum_chenhlech;
                    data_khoa_name.isgroup = 1;
                    lstData_XuatBaoCao.Add(data_khoa_name);
                    //Phong
                    List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstGroup_Phong = lstData_Khoa.GroupBy(o => o.departmentid).Select(n => n.First()).ToList();
                    foreach (var item_phong in lstGroup_Phong)
                    {
                        ClassCommon.BC44_DVYeuCau2_TongHopDTO data_phong_name = new ClassCommon.BC44_DVYeuCau2_TongHopDTO();
                        List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_Phong = lstData_Khoa.Where(o => o.departmentid == item_phong.departmentid).ToList();
                        decimal sum_soluong_phong = 0;
                        decimal sum_thanhtien_phong = 0;
                        decimal sum_thanhtien_bhyt_phong = 0;
                        decimal sum_chenhlech_phong = 0;

                        foreach (var item_tinhsum in lstData_Phong)
                        {
                            sum_soluong_phong += item_tinhsum.soluong;
                            sum_thanhtien_phong += item_tinhsum.thanhtien;
                            sum_thanhtien_bhyt_phong += item_tinhsum.thanhtien_bhyt;
                            sum_chenhlech_phong += item_tinhsum.chenhlech;
                        }
                        data_phong_name.departmentname = item_phong.departmentname;
                        data_phong_name.stt = "' - " + item_phong.departmentname;
                        data_phong_name.soluong = sum_soluong_phong;
                        data_phong_name.thanhtien = sum_thanhtien_phong;
                        data_phong_name.thanhtien_bhyt = sum_thanhtien_bhyt_phong;
                        data_phong_name.chenhlech = sum_chenhlech_phong;
                        data_phong_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_phong_name);
                        //nhom Dich vu
                        List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstGroup_nhomTk = lstData_Phong.GroupBy(o => o.report_tkcode).Select(n => n.First()).ToList();
                        foreach (var item_nhomtk in lstGroup_nhomTk)
                        {
                            ClassCommon.BC44_DVYeuCau2_TongHopDTO data_nhomtk_name = new ClassCommon.BC44_DVYeuCau2_TongHopDTO();
                            List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_nhomtk = lstData_Phong.Where(o => o.report_tkcode == item_nhomtk.report_tkcode).ToList();

                            decimal sum_soluong_nhomtk = 0;
                            decimal sum_thanhtien_nhomtk = 0;
                            decimal sum_thanhtien_bhyt_nhomtk = 0;
                            decimal sum_chenhlech_nhomtk = 0;

                            foreach (var item_tinhsum in lstData_nhomtk)
                            {
                                sum_soluong_nhomtk += item_tinhsum.soluong;
                                sum_thanhtien_nhomtk += item_tinhsum.thanhtien;
                                sum_thanhtien_bhyt_nhomtk += item_tinhsum.thanhtien_bhyt;
                                sum_chenhlech_nhomtk += item_tinhsum.chenhlech;
                            }
                            data_nhomtk_name.departmentname = item_nhomtk.departmentname;
                            data_nhomtk_name.stt = "'   - " + item_nhomtk.report_tkcode + "-" + item_nhomtk.billaccountrefname;
                            data_nhomtk_name.soluong = sum_soluong_nhomtk;
                            data_nhomtk_name.thanhtien = sum_thanhtien_nhomtk;
                            data_nhomtk_name.thanhtien_bhyt = sum_thanhtien_bhyt_nhomtk;
                            data_nhomtk_name.chenhlech = sum_chenhlech_nhomtk;
                            data_nhomtk_name.isgroup = 3;
                            lstData_XuatBaoCao.Add(data_nhomtk_name);
                            lstData_XuatBaoCao.AddRange(lstData_nhomtk);
                        }
                    }
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }
        private DataTable ExportExcel_GroupColume_TH()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_XuatBaoCao = new List<ClassCommon.BC44_DVYeuCau2_TongHopDTO>();
                List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstDataBC_Convert = DataTables.DataTableToList<ClassCommon.BC44_DVYeuCau2_TongHopDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstGroup_Khoa = lstDataBC_Convert.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList().OrderBy(or => or.departmentgroupname).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BC44_DVYeuCau2_TongHopDTO data_khoa_name = new ClassCommon.BC44_DVYeuCau2_TongHopDTO();
                    List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_Khoa = lstDataBC_Convert.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;
                    decimal sum_thanhtien_bhyt = 0;
                    decimal sum_chenhlech = 0;

                    foreach (var item_tinhsum in lstData_Khoa)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                        sum_thanhtien_bhyt += item_tinhsum.thanhtien_bhyt;
                        sum_chenhlech += item_tinhsum.chenhlech;
                    }
                    data_khoa_name.departmentgroupname = item_khoa.departmentgroupname;
                    data_khoa_name.stt = item_khoa.departmentgroupname;
                    data_khoa_name.soluong = sum_soluong;
                    data_khoa_name.thanhtien = sum_thanhtien;
                    data_khoa_name.thanhtien_bhyt = sum_thanhtien_bhyt;
                    data_khoa_name.chenhlech = sum_chenhlech;
                    data_khoa_name.isgroup = 1;
                    lstData_XuatBaoCao.Add(data_khoa_name);
                    //Phong
                    List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstGroup_Phong = lstData_Khoa.GroupBy(o => o.departmentid).Select(n => n.First()).ToList();
                    foreach (var item_phong in lstGroup_Phong)
                    {
                        ClassCommon.BC44_DVYeuCau2_TongHopDTO data_phong_name = new ClassCommon.BC44_DVYeuCau2_TongHopDTO();
                        List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_Phong = lstData_Khoa.Where(o => o.departmentid == item_phong.departmentid).ToList();
                        decimal sum_soluong_phong = 0;
                        decimal sum_thanhtien_phong = 0;
                        decimal sum_thanhtien_bhyt_phong = 0;
                        decimal sum_chenhlech_phong = 0;

                        foreach (var item_tinhsum in lstData_Phong)
                        {
                            sum_soluong_phong += item_tinhsum.soluong;
                            sum_thanhtien_phong += item_tinhsum.thanhtien;
                            sum_thanhtien_bhyt_phong += item_tinhsum.thanhtien_bhyt;
                            sum_chenhlech_phong += item_tinhsum.chenhlech;
                        }
                        data_phong_name.departmentname = item_phong.departmentname;
                        data_phong_name.stt = "' - " + item_phong.departmentname;
                        data_phong_name.soluong = sum_soluong_phong;
                        data_phong_name.thanhtien = sum_thanhtien_phong;
                        data_phong_name.thanhtien_bhyt = sum_thanhtien_bhyt_phong;
                        data_phong_name.chenhlech = sum_chenhlech_phong;
                        data_phong_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_phong_name);
                        //nhom Dich vu
                        List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstGroup_nhomTk = lstData_Phong.GroupBy(o => o.report_tkcode).Select(n => n.First()).ToList();
                        foreach (var item_nhomtk in lstGroup_nhomTk)
                        {
                            ClassCommon.BC44_DVYeuCau2_TongHopDTO data_nhomtk_name = new ClassCommon.BC44_DVYeuCau2_TongHopDTO();
                            List<ClassCommon.BC44_DVYeuCau2_TongHopDTO> lstData_nhomtk = lstData_Phong.Where(o => o.report_tkcode == item_nhomtk.report_tkcode).ToList();

                            decimal sum_soluong_nhomtk = 0;
                            decimal sum_thanhtien_nhomtk = 0;
                            decimal sum_thanhtien_bhyt_nhomtk = 0;
                            decimal sum_chenhlech_nhomtk = 0;

                            foreach (var item_tinhsum in lstData_nhomtk)
                            {
                                sum_soluong_nhomtk += item_tinhsum.soluong;
                                sum_thanhtien_nhomtk += item_tinhsum.thanhtien;
                                sum_thanhtien_bhyt_nhomtk += item_tinhsum.thanhtien_bhyt;
                                sum_chenhlech_nhomtk += item_tinhsum.chenhlech;
                            }
                            data_nhomtk_name.departmentname = item_nhomtk.departmentname;
                            data_nhomtk_name.stt = "'   - " + item_nhomtk.report_tkcode + "-" + item_nhomtk.billaccountrefname;
                            data_nhomtk_name.soluong = sum_soluong_nhomtk;
                            data_nhomtk_name.thanhtien = sum_thanhtien_nhomtk;
                            data_nhomtk_name.thanhtien_bhyt = sum_thanhtien_bhyt_nhomtk;
                            data_nhomtk_name.chenhlech = sum_chenhlech_nhomtk;
                            data_nhomtk_name.isgroup = 3;
                            lstData_XuatBaoCao.Add(data_nhomtk_name);
                            //lstData_XuatBaoCao.AddRange(lstData_nhomtk);
                        }
                    }
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }

        #endregion

        #region Event Change
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
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void cbbTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbTieuChi.Text == "Theo ngày duyệt VP" || cbbTieuChi.Text == "Theo ngày thu tiền")
                {
                    chkcboListNguoiThuTien.Enabled = true;
                    chkTatCa.Enabled = true;
                }
                else
                {
                    chkcboListNguoiThuTien.Enabled = false;
                    chkTatCa.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void chkTatCa_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTatCa.Checked)
                {
                    chkcboListNguoiThuTien.Enabled = false;
                }
                else
                { chkcboListNguoiThuTien.Enabled = true; }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



        #endregion

    }
}
