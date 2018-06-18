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
using DevExpress.Utils.Menu;

namespace MedicalLink.BaoCao
{
    public partial class ucBCDoanhThuTheoLoaiHinhDV : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBCDoanhThuTheoLoaiHinhDV()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCDoanhThuTheoLoaiHinhDV_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

            LoadDanhSachExport();
            LoadDanhSachInAn();
            LoadDanhSachKhoa();
        }
        private void LoadDanhSachExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Chi tiết"));
                menu.Items.Add(new DXMenuItem("Tổng hợp"));
                dropDownExport.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachInAn()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Chi tiết"));
                menu.Items.Add(new DXMenuItem("Tổng hợp"));
                dropDownPrint.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_InAn_Click;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _trangthaivienphi = "";
                string _tieuchi_bill = "";
                string lstKhoacheck = " and departmentgroupid in (";
                string _loaihinhthanhtoan = " loaidoituong in (1,3,4) ";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " where vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " where vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " where vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày thu tiền")
                {
                    _tieuchi_bill = " inner join (select billid from bill where dahuyphieu=0 and billdate between '" + datetungay + "' and '" + datedenngay + "') bi on bi.billid=se.billid_clbh_thutien ";
                }
                //
                if (cbbTieuChi.Text == "Theo ngày chỉ định" || cbbTieuChi.Text == "Theo ngày thu tiền")
                {
                    if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                    {
                        _trangthaivienphi = " where vienphistatus=0 ";
                    }
                    else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                    {
                        _trangthaivienphi = " where vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                    }
                    else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                    {
                        _trangthaivienphi = " where vienphistatus>0 and vienphistatus_vp=1 ";
                    }
                }
                else
                {
                    if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                    {
                        _trangthaivienphi = " and vienphistatus=0 ";
                    }
                    else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                    {
                        _trangthaivienphi = " and vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                    }
                    else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                    {
                        _trangthaivienphi = " and vienphistatus>0 and vienphistatus_vp=1 ";
                    }
                }
                //loai hinh thanh toan
                if (cboLoaiHinhThanhToan.Text == "Yêu cầu")
                {
                    _loaihinhthanhtoan = " loaidoituong=3 ";
                }
                else if (cboLoaiHinhThanhToan.Text == "Viện phí")
                {
                    _loaihinhthanhtoan = " loaidoituong=1 ";
                }
                else if (cboLoaiHinhThanhToan.Text == "BHYT+YC")
                {
                    _loaihinhthanhtoan = " loaidoituong=4 ";
                }

                List<Object> lstPhongCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        lstKhoacheck += "'" + lstPhongCheck[i] + "',";
                    }
                    lstKhoacheck += "'" + lstPhongCheck[lstPhongCheck.Count - 1] + "') ";
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }

                string _sql_timkiem = " SELECT row_number () over (order by serf.servicepricename) as stt, serf.servicepricegroupcode, serf.bhyt_groupcode, serf.servicegrouptype, (case serf.servicegrouptype when 1 then 'Khám bệnh' when 2 then 'Xét nghiệm' when 3 then 'CĐHA' when 4 then 'Chuyên khoa' end) as servicegrouptype_name, ser.departmentgroupid, degp.departmentgroupname, serf.servicepricecode, serf.servicepricename, serf.servicepricenamebhyt, serf.servicepriceunit, ser.soluong, ser.servicepricemoney, ser.thanhtien_dv, ser.servicepricemoney_bhyt, ser.thanhtien_bh, ser.thanhtien_chenh, ser.thanhtien_chenh as tienthucthu, '0' as isgroup FROM (select servicepricegroupcode,bhyt_groupcode,servicegrouptype,servicepricetype,servicepricecode,servicepricename,servicepricenamebhyt,servicepriceunit,servicepricefee,servicepricefeenhandan,servicepricefeebhyt,servicepricefeenuocngoai from servicepriceref where servicegrouptype in (1,2,3,4)) serf inner join (select se.servicepricecode, se.departmentgroupid, sum(se.soluong) as soluong, se.servicepricemoney, (se.servicepricemoney*sum(se.soluong)) as thanhtien_dv, se.servicepricemoney_bhyt, (se.servicepricemoney_bhyt*sum(se.soluong)) as thanhtien_bh, ((se.servicepricemoney-se.servicepricemoney_bhyt)*sum(se.soluong)) as thanhtien_chenh, se.bhyt_groupcode from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,bhyt_groupcode,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,billid_clbh_thutien from serviceprice where " + _loaihinhthanhtoan + " and bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG') " + _tieuchi_ser + lstKhoacheck + " ) se inner join (select vienphiid from vienphi " + _tieuchi_vp + _trangthaivienphi + " ) vp on vp.vienphiid=se.vienphiid " + _tieuchi_bill + " group by se.servicepricecode,se.departmentgroupid,se.bhyt_groupcode,se.servicepricemoney_bhyt,se.servicepricemoney) ser on ser.servicepricecode=serf.servicepricecode inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid WHERE ser.soluong>0 ORDER BY serf.servicegrouptype,serf.servicepricegroupcode,serf.servicepricename; ";

                this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBaoCao.DataSource = this.dataBaoCao;
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
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

        #region Export and Print
        private void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                System.Data.DataTable data_XuatBaoCao = new System.Data.DataTable();
                string fileTemplatePath = "";
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Chi tiết")
                {
                    fileTemplatePath = "BC_37_DoanhThuTheoLoaiHinhDichVu_ChiTiet.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_ChiTiet();
                }
                else if (tenbaocao == "Tổng hợp")
                {
                    fileTemplatePath = "BC_37_DoanhThuTheoLoaiHinhDichVu_TongHop.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_TongHop();
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void Item_InAn_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));

                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                System.Data.DataTable data_XuatBaoCao = new System.Data.DataTable();
                string fileTemplatePath = "";
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Chi tiết")
                {
                    fileTemplatePath = "BC_37_DoanhThuTheoLoaiHinhDichVu_ChiTiet.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_ChiTiet();
                }
                else if (tenbaocao == "Tổng hợp")
                {
                    fileTemplatePath = "BC_37_DoanhThuTheoLoaiHinhDichVu_TongHop.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_TongHop();
                }
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume_ChiTiet()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstData_XuatBaoCao = new List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO>();

                List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstDataBC_Tmp = DataTables.DataTableToList<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstGroup_Khoa = lstDataBC_Tmp.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList().OrderBy(n => n.departmentgroupname).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO data_khoa_name = new ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO();
                    List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstData_Khoa = lstDataBC_Tmp.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien_dv = 0;
                    decimal sum_thanhtien_bh = 0;
                    decimal sum_thanhtien_chenh = 0;
                    decimal sum_thucthu = 0;

                    foreach (var item_tinhsum in lstData_Khoa)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien_dv += item_tinhsum.thanhtien_dv;
                        sum_thanhtien_bh += item_tinhsum.thanhtien_bh;
                        sum_thanhtien_chenh += item_tinhsum.thanhtien_chenh;
                        sum_thucthu += item_tinhsum.tienthucthu;

                    }
                    //data_khoa_name.servicepricename = item_khoa.departmentgroupname;
                    data_khoa_name.stt = item_khoa.departmentgroupname;
                    data_khoa_name.soluong = sum_soluong;
                    data_khoa_name.thanhtien_dv = sum_thanhtien_dv;
                    data_khoa_name.thanhtien_bh = sum_thanhtien_bh;
                    data_khoa_name.thanhtien_chenh = sum_thanhtien_chenh;
                    data_khoa_name.tienthucthu = sum_thucthu;
                    data_khoa_name.isgroup = 1;
                    lstData_XuatBaoCao.Add(data_khoa_name);
                    //Nhom dich vu
                    List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstGroup_NhomDV = lstData_Khoa.GroupBy(o => o.servicepricegroupcode).Select(n => n.First()).ToList();
                    //
                    List<ClassCommon.ServicepricegroupcodeDTO> lstNhomDichVu = LayTenNhomDichVu();
                    foreach (var item_phong in lstGroup_NhomDV)
                    {
                        ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO data_nhom_name = new ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO();
                        List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstData_NhomDV = lstData_Khoa.Where(o => o.servicepricegroupcode == item_phong.servicepricegroupcode).ToList();
                        decimal sum_soluong_nhom = 0;
                        decimal sum_thanhtien_dv_nhom = 0;
                        decimal sum_thanhtien_bh_nhom = 0;
                        decimal sum_thanhtien_chenh_nhom = 0;
                        decimal sum_thucthu_nhom = 0;

                        //foreach (var item_tinhsum in lstData_NhomDV)
                        for (int i = 0; i < lstData_NhomDV.Count; i++)
                        {
                            lstData_NhomDV[i].stt = (i + 1).ToString();
                            sum_soluong_nhom += lstData_NhomDV[i].soluong;
                            sum_thanhtien_dv_nhom += lstData_NhomDV[i].thanhtien_dv;
                            sum_thanhtien_bh_nhom += lstData_NhomDV[i].thanhtien_bh;
                            sum_thanhtien_chenh_nhom += lstData_NhomDV[i].thanhtien_chenh;
                            sum_thucthu_nhom += lstData_NhomDV[i].tienthucthu;
                        }
                        List<ClassCommon.ServicepricegroupcodeDTO> _lsttennhom = lstNhomDichVu.Where(o => o.servicepricecode == item_phong.servicepricegroupcode).ToList();
                        //data_phong_name.servicepricename = item_phong.servicepricename;
                        data_nhom_name.stt = "' - " + item_phong.servicepricegroupcode;
                        if (_lsttennhom != null && _lsttennhom.Count > 0)
                        {
                            data_nhom_name.stt += " (" + _lsttennhom[0].servicepricename + ")";
                        }
                        data_nhom_name.soluong = sum_soluong_nhom;
                        data_nhom_name.thanhtien_dv = sum_thanhtien_dv_nhom;
                        data_nhom_name.thanhtien_bh = sum_thanhtien_bh_nhom;
                        data_nhom_name.thanhtien_chenh = sum_thanhtien_chenh_nhom;
                        data_nhom_name.tienthucthu = sum_thucthu_nhom;
                        data_nhom_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_nhom_name);
                        lstData_XuatBaoCao.AddRange(lstData_NhomDV);
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
        private DataTable ExportExcel_GroupColume_TongHop()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstData_XuatBaoCao = new List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO>();

                List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstDataBC_Tmp = DataTables.DataTableToList<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstGroup_Khoa = lstDataBC_Tmp.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList().OrderBy(n => n.departmentgroupname).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO data_khoa_name = new ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO();
                    List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstData_Khoa = lstDataBC_Tmp.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien_dv = 0;
                    decimal sum_thanhtien_bh = 0;
                    decimal sum_thanhtien_chenh = 0;
                    decimal sum_thucthu = 0;

                    foreach (var item_tinhsum in lstData_Khoa)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien_dv += item_tinhsum.thanhtien_dv;
                        sum_thanhtien_bh += item_tinhsum.thanhtien_bh;
                        sum_thanhtien_chenh += item_tinhsum.thanhtien_chenh;
                        sum_thucthu += item_tinhsum.tienthucthu;

                    }
                    //data_khoa_name.servicepricename = item_khoa.departmentgroupname;
                    data_khoa_name.stt = item_khoa.departmentgroupname;
                    data_khoa_name.soluong = sum_soluong;
                    data_khoa_name.thanhtien_dv = sum_thanhtien_dv;
                    data_khoa_name.thanhtien_bh = sum_thanhtien_bh;
                    data_khoa_name.thanhtien_chenh = sum_thanhtien_chenh;
                    data_khoa_name.tienthucthu = sum_thucthu;
                    data_khoa_name.isgroup = 1;
                    lstData_XuatBaoCao.Add(data_khoa_name);
                    //Nhom dich vu
                    List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstGroup_NhomDV = lstData_Khoa.GroupBy(o => o.servicepricegroupcode).Select(n => n.First()).ToList();
                    //
                    List<ClassCommon.ServicepricegroupcodeDTO> lstNhomDichVu = LayTenNhomDichVu();
                    foreach (var item_phong in lstGroup_NhomDV)
                    {
                        ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO data_nhom_name = new ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO();
                        List<ClassCommon.BCDoanhThuTheoLoaiHinhDVDTO> lstData_NhomDV = lstData_Khoa.Where(o => o.servicepricegroupcode == item_phong.servicepricegroupcode).ToList();
                        decimal sum_soluong_nhom = 0;
                        decimal sum_thanhtien_dv_nhom = 0;
                        decimal sum_thanhtien_bh_nhom = 0;
                        decimal sum_thanhtien_chenh_nhom = 0;
                        decimal sum_thucthu_nhom = 0;

                        //foreach (var item_tinhsum in lstData_NhomDV)
                        for (int i = 0; i < lstData_NhomDV.Count; i++)
                        {
                            lstData_NhomDV[i].stt = (i + 1).ToString();
                            sum_soluong_nhom += lstData_NhomDV[i].soluong;
                            sum_thanhtien_dv_nhom += lstData_NhomDV[i].thanhtien_dv;
                            sum_thanhtien_bh_nhom += lstData_NhomDV[i].thanhtien_bh;
                            sum_thanhtien_chenh_nhom += lstData_NhomDV[i].thanhtien_chenh;
                            sum_thucthu_nhom += lstData_NhomDV[i].tienthucthu;
                        }
                        List<ClassCommon.ServicepricegroupcodeDTO> _lsttennhom = lstNhomDichVu.Where(o => o.servicepricecode == item_phong.servicepricegroupcode).ToList();
                        //data_phong_name.servicepricename = item_phong.servicepricename;
                        data_nhom_name.stt = "' - " + item_phong.servicepricegroupcode;
                        if (_lsttennhom != null && _lsttennhom.Count > 0)
                        {
                            data_nhom_name.stt += " (" + _lsttennhom[0].servicepricename + ")";
                        }
                        data_nhom_name.soluong = sum_soluong_nhom;
                        data_nhom_name.thanhtien_dv = sum_thanhtien_dv_nhom;
                        data_nhom_name.thanhtien_bh = sum_thanhtien_bh_nhom;
                        data_nhom_name.thanhtien_chenh = sum_thanhtien_chenh_nhom;
                        data_nhom_name.tienthucthu = sum_thucthu_nhom;
                        data_nhom_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_nhom_name);
                        //lstData_XuatBaoCao.AddRange(lstData_NhomDV);
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

        private List<ClassCommon.ServicepricegroupcodeDTO> LayTenNhomDichVu()
        {
            List<ClassCommon.ServicepricegroupcodeDTO> lstDataBC_Tmp = new List<ClassCommon.ServicepricegroupcodeDTO>();
            try
            {
                string _sqlDSNhom = "SELECT servicepricecode,servicepricename FROM servicepriceref WHERE servicepricecode in (select servicepricegroupcode from servicepriceref where servicegrouptype in (1,2,3,4) group by servicepricegroupcode);";
                System.Data.DataTable _dataDSNhom = condb.GetDataTable_HIS(_sqlDSNhom);
                if (_dataDSNhom != null && _dataDSNhom.Rows.Count > 0)
                {
                    lstDataBC_Tmp = DataTables.DataTableToList<ClassCommon.ServicepricegroupcodeDTO>(_dataDSNhom);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return lstDataBC_Tmp;
        }

        #endregion

        #region Custom
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


        #endregion

    }
}
