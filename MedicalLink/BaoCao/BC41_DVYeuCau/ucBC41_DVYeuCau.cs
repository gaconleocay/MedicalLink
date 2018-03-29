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

namespace MedicalLink.BaoCao
{
    public partial class ucBC41_DVYeuCau : UserControl
    {
        #region Khai Bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }

        #endregion

        public ucBC41_DVYeuCau()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC41_DVYeuCau_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDuLieuMacDinh();
                LoadDanhSachKhoa();
                LoadDanhSachNguoiThu();
                cbbTieuChi_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
                DataTable _dataThuNgan = condb.GetDataTable_HIS(sql_ngthu);
                chkcomboListNguoiDuyet.Properties.DataSource = _dataThuNgan;
                chkcomboListNguoiDuyet.Properties.DisplayMember = "username";
                chkcomboListNguoiDuyet.Properties.ValueMember = "userhisid";
                chkcomboListNguoiDuyet.CheckAll();
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
                string _select_bill = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
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
                }
                else if (cbbTieuChi.Text == "Theo ngày thu tiền")
                {
                    _tieuchi_bill = " inner join (select billid,vienphiid,billgroupcode,billcode from bill where billdate between '" + datetungay + "' and '" + datedenngay + "'  and dahuyphieu=0) b on b.billid=ser.billid_clbh_thutien ";
                    _select_bill = " b.billgroupcode, b.billcode, ";
                }

                //NGuoi duyet
                if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _listuserid = " and duyet_nguoiduyet_vp in (";
                    List<Object> lstNhanVienCheck = chkcomboListNguoiDuyet.Properties.Items.GetCheckedValues();
                    if (lstNhanVienCheck.Count > 0)
                    {
                        for (int i = 0; i < lstNhanVienCheck.Count - 1; i++)
                        {
                            _listuserid += "'" + lstNhanVienCheck[i] + "', ";
                        }
                        _listuserid += "'" + lstNhanVienCheck[lstNhanVienCheck.Count - 1] + "') ";
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                        frmthongbao.Show();
                        return;
                    }
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

                //Lay du lieu bao cao - ngay 13/3
                if (radioXemTongHop.Checked) //xem tong hop
                {
                    string _sql_timkiem = @"select row_number () over (order by ser.bhyt_groupcode,ser.servicepricename) as stt,
	                    ser.servicepricecode,
	                    ser.servicepricename,
	                    (case ser.loaidoituong
		                    when 0 then 'BHYT'
		                    when 1 then 'Viện phí'
		                    when 2 then 'Đi kèm'
		                    when 3 then 'Yêu cầu'
		                    when 4 then 'BHYT+YC '
		                    when 5 then 'Hao phí giường, CK'
		                    when 6 then 'BHYT+phụ thu'
		                    when 7 then 'Hao phí PTTT'
		                    when 8 then 'Đối tượng khác'
		                    when 9 then 'Hao phí khác'
		                    when 20 then 'Thanh toán riêng'
		                    end) as loaidoituong,
	                    (case ser.bhyt_groupcode 
			                    when '01KB' then '01-Khám bệnh'
			                    when '03XN' then '02-Xét nghiệm'
			                    when '04CDHA' then '04-CĐHA'
			                    when '05TDCN' then '05-Thăm dò chức năng'
			                    when '06PTTT' then '06-PTTT'
			                    when '07KTC' then '07-DV KTC'
			                    else '99-Khác'
			                    end) as bhyt_groupcode,
	                    sum(ser.soluong) as soluong,
	                    ser.servicepricemoney,
	                    sum(ser.servicepricemoney*ser.soluong) as thanhtien,
	                    ser.servicepricemoney_bhyt,
	                    sum(ser.servicepricemoney_bhyt*ser.soluong) as thanhtien_bhyt,
	                    sum((ser.servicepricemoney-ser.servicepricemoney_bhyt)*ser.soluong) as chenhlech,
	                    '0' as isgroup		
                    from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,bhyt_groupcode,billid_clbh_thutien from serviceprice where loaidoituong in (3,4) and departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + ") ser  inner join (select vienphiid from vienphi where 1 = 1 " + _tieuchi_vp + _listuserid + ") vp on vp.vienphiid = ser.vienphiid  " + _tieuchi_bill + " group by ser.servicepricecode,ser.servicepricename,ser.loaidoituong,ser.bhyt_groupcode,ser.servicepricemoney,ser.servicepricemoney_bhyt; ";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBaoCao_TH.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBaoCao_TH.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else if (radioXemChiTiet.Checked)//xem chi tiet
                {
                    string _sql_timkiem = @"select row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname,ser.servicepricename) as stt,
	                    hsba.patientid,
	                    ser.vienphiid,
	                    hsba.patientname,
	                    bh.bhytcode,
	                    ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	                    ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi,
	                    ser.servicepricecode,
	                    ser.servicepricename,
	                    ncd.username as nguoichidinh,
	                    ser.servicepricedate,
	                    (case ser.loaidoituong
		                    when 0 then 'BHYT'
		                    when 1 then 'Viện phí'
		                    when 2 then 'Đi kèm'
		                    when 3 then 'Yêu cầu'
		                    when 4 then 'BHYT+YC '
		                    when 5 then 'Hao phí giường, CK'
		                    when 6 then 'BHYT+phụ thu'
		                    when 7 then 'Hao phí PTTT'
		                    when 8 then 'Đối tượng khác'
		                    when 9 then 'Hao phí khác'
		                    when 20 then 'Thanh toán riêng'
		                    end) as loaidoituong,
	                    ser.soluong,
	                    ser.servicepricemoney,
	                    (ser.servicepricemoney*ser.soluong) as thanhtien,
	                    ser.departmentid,		
	                    de.departmentname,
	                    ser.departmentgroupid,
	                    degp.departmentgroupname,
	                    '0' as isgroup
                    from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,servicepricedate,maubenhphamid,billid_clbh_thutien from serviceprice where loaidoituong in (3,4) and  departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + ") ser inner join (select hosobenhanid, patientid, patientname, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname from hosobenhan) hsba on hsba.hosobenhanid = ser.hosobenhanid inner join(select vienphiid, hosobenhanid, bhytid from vienphi where 1 = 1 " + _tieuchi_vp + _listuserid + ") vp on vp.hosobenhanid = hsba.hosobenhanid inner join(select bhytid, bhytcode from bhyt) bh on bh.bhytid = vp.bhytid inner join(select departmentgroupid, departmentgroupname from departmentgroup) degp on degp.departmentgroupid = ser.departmentgroupid left join(select departmentid, departmentname from department) de on de.departmentid = ser.departmentid " + _tieuchi_bill + " inner join (select maubenhphamid,userid from maubenhpham) mbp on mbp.maubenhphamid=ser.maubenhphamid LEFT JOIN(select userhisid, username from nhompersonnel) ncd ON ncd.userhisid = mbp.userid; ";

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
                else if (radioXemDSBenhNhan.Checked) //xem danh sach benh nhan
                {
                    string _sql_timkiem = @"SELECT row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname) as stt, " + _select_bill + " hsba.patientid, ser.vienphiid, hsba.patientname, bh.bhytcode, to_char(hsba.birthday,'yyyy') as year, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi, sum(ser.servicepricemoney*ser.soluong) as thanhtien, ser.departmentid, de.departmentname, ser.departmentgroupid, degp.departmentgroupname, '0' as isgroup FROM (select hosobenhanid,vienphiid,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,billid_clbh_thutien from serviceprice where loaidoituong in (3,4) and departmentid in (" + _lstPhongChonLayBC + ") " + _tieuchi_ser + ") ser inner join (select hosobenhanid,patientid,patientname,bhytcode,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select vienphiid,hosobenhanid,bhytid from vienphi where 1=1 " + _tieuchi_vp + _listuserid + ") vp on vp.hosobenhanid=hsba.hosobenhanid " + _tieuchi_bill + " inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid GROUP BY hsba.patientid,ser.vienphiid,hsba.patientname,bh.bhytcode,hsba.birthday,hsba.hc_sonha,hsba.hc_thon,hsba.hc_xacode,hsba.hc_xaname,hsba.hc_huyencode,hsba.hc_huyenname,ser.departmentid,de.departmentname,ser.departmentgroupid, " + _select_bill + " degp.departmentgroupname;";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataDSBN.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataDSBN.DataSource = null;
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

        #region Export va Print
        private void tbnExport_Click(object sender, EventArgs e)
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
                ClassCommon.reportExcelDTO report_khoa = new ClassCommon.reportExcelDTO();
                report_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                report_khoa.value = chkcomboListDSKhoa.Text; ;
                thongTinThem.Add(report_khoa);
                //ClassCommon.reportExcelDTO report_doituong = new ClassCommon.reportExcelDTO();
                //report_doituong.name = Base.bienTrongBaoCao.LOAIDOITUONG;
                //report_doituong.value = cboNguoiDuyet.Text;
                //thongTinThem.Add(report_doituong);

                string fileTemplatePath = "";
                DataTable data_XuatBaoCao = new DataTable();
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_41_ThongKeSuDungDichVuYeuCau_ChiTiet.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_CT();
                }
                else if (radioXemTongHop.Checked)
                {
                    fileTemplatePath = "BC_41_ThongKeSuDungDichVuYeuCau_TongHop.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_TH();
                }
                else if (radioXemDSBenhNhan.Checked)
                {
                    fileTemplatePath = "BC_41_ThongKeSuDungDichVuYeuCau_DSBN.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_DSBN();
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
                ClassCommon.reportExcelDTO report_khoa = new ClassCommon.reportExcelDTO();
                report_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                report_khoa.value = chkcomboListDSKhoa.Text; ;
                thongTinThem.Add(report_khoa);
                //ClassCommon.reportExcelDTO report_doituong = new ClassCommon.reportExcelDTO();
                //report_doituong.name = Base.bienTrongBaoCao.LOAIDOITUONG;
                //report_doituong.value = cboNguoiDuyet.Text;
                //thongTinThem.Add(report_doituong);


                string fileTemplatePath = "";
                DataTable data_XuatBaoCao = new DataTable();
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_41_ThongKeSuDungDichVuYeuCau_ChiTiet.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_CT();
                }
                else if (radioXemTongHop.Checked)
                {
                    fileTemplatePath = "BC_41_ThongKeSuDungDichVuYeuCau_TongHop.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_TH();
                }
                else if (radioXemDSBenhNhan.Checked)
                {
                    fileTemplatePath = "BC_41_ThongKeSuDungDichVuYeuCau_DSBN.xlsx";
                    data_XuatBaoCao = ExportExcel_GroupColume_DSBN();
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
        private DataTable ExportExcel_GroupColume_TH()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.BC41_DVYeuCau_TongHopDTO> lstData_XuatBaoCao = new List<ClassCommon.BC41_DVYeuCau_TongHopDTO>();
                List<ClassCommon.BC41_DVYeuCau_TongHopDTO> lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.BC41_DVYeuCau_TongHopDTO>(this.dataBaoCao);

                List<ClassCommon.BC41_DVYeuCau_TongHopDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.bhyt_groupcode).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BC41_DVYeuCau_TongHopDTO data_groupname = new ClassCommon.BC41_DVYeuCau_TongHopDTO();

                    List<ClassCommon.BC41_DVYeuCau_TongHopDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.bhyt_groupcode == item_group.bhyt_groupcode).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;
                    decimal sum_thanhtien_bhyt = 0;
                    decimal sum_chenhlech = 0;

                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                        sum_thanhtien_bhyt += item_tinhsum.thanhtien_bhyt;
                        sum_chenhlech += item_tinhsum.chenhlech;
                    }

                    data_groupname.bhyt_groupcode = item_group.bhyt_groupcode;
                    data_groupname.stt = item_group.bhyt_groupcode;
                    data_groupname.soluong = sum_soluong;
                    data_groupname.thanhtien = sum_thanhtien;
                    data_groupname.thanhtien_bhyt = sum_thanhtien_bhyt;
                    data_groupname.chenhlech = sum_chenhlech;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }
        private DataTable ExportExcel_GroupColume_CT()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstData_XuatBaoCao = new List<ClassCommon.BC41_DVYeuCau_ChiTietDTO>();
                List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.BC41_DVYeuCau_ChiTietDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstGroup_Khoa = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BC41_DVYeuCau_ChiTietDTO data_khoa_name = new ClassCommon.BC41_DVYeuCau_ChiTietDTO();
                    List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstData_Khoa = lstDataDoanhThu.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
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
                    List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstGroup_Phong = lstData_Khoa.GroupBy(o => o.departmentid).Select(n => n.First()).ToList();
                    foreach (var item_phong in lstGroup_Phong)
                    {
                        ClassCommon.BC41_DVYeuCau_ChiTietDTO data_phong_name = new ClassCommon.BC41_DVYeuCau_ChiTietDTO();
                        List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstData_Phong = lstData_Khoa.Where(o => o.departmentid == item_phong.departmentid).ToList();
                        decimal sum_soluong_phong = 0;
                        decimal sum_thanhtien_phong = 0;

                        foreach (var item_tinhsum in lstData_Phong)
                        {
                            sum_soluong_phong += item_tinhsum.soluong;
                            sum_thanhtien_phong += item_tinhsum.thanhtien;
                        }
                        data_phong_name.departmentname = item_phong.departmentname;
                        data_phong_name.stt = "' - " + item_phong.departmentname;
                        data_phong_name.soluong = sum_soluong_phong;
                        data_phong_name.thanhtien = sum_thanhtien_phong;
                        data_phong_name.isgroup = 2;
                        lstData_XuatBaoCao.Add(data_phong_name);
                        //Benh nhan
                        List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstGroup_BN = lstData_Phong.GroupBy(o => o.vienphiid).Select(n => n.First()).ToList();
                        foreach (var item_bn in lstGroup_BN)
                        {
                            ClassCommon.BC41_DVYeuCau_ChiTietDTO data_bn_name = new ClassCommon.BC41_DVYeuCau_ChiTietDTO();
                            List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstData_bn = lstData_Phong.Where(o => o.vienphiid == item_bn.vienphiid).ToList();
                            decimal sum_soluong_bn = 0;
                            decimal sum_thanhtien_bn = 0;

                            List<ClassCommon.BC41_DVYeuCau_ChiTietDTO> lstData_DichVu = new List<ClassCommon.BC41_DVYeuCau_ChiTietDTO>();
                            for (int i = 0; i < lstData_bn.Count; i++)
                            {
                                ClassCommon.BC41_DVYeuCau_ChiTietDTO data_bn_dichvu = new ClassCommon.BC41_DVYeuCau_ChiTietDTO();
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
                                data_bn_dichvu.nguoichidinh = lstData_bn[i].nguoichidinh;

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
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }
        private DataTable ExportExcel_GroupColume_DSBN()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.BC41_DVYeuCau_DSBNDTO> lstData_XuatBaoCao = new List<ClassCommon.BC41_DVYeuCau_DSBNDTO>();
                List<ClassCommon.BC41_DVYeuCau_DSBNDTO> lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.BC41_DVYeuCau_DSBNDTO>(this.dataBaoCao);
                //Khoa
                List<ClassCommon.BC41_DVYeuCau_DSBNDTO> lstGroup_Khoa = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_khoa in lstGroup_Khoa)
                {
                    ClassCommon.BC41_DVYeuCau_DSBNDTO data_khoa_name = new ClassCommon.BC41_DVYeuCau_DSBNDTO();
                    List<ClassCommon.BC41_DVYeuCau_DSBNDTO> lstData_Khoa = lstDataDoanhThu.Where(o => o.departmentgroupid == item_khoa.departmentgroupid).ToList();
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
                    List<ClassCommon.BC41_DVYeuCau_DSBNDTO> lstGroup_Phong = lstData_Khoa.GroupBy(o => o.departmentid).Select(n => n.First()).ToList();
                    foreach (var item_phong in lstGroup_Phong)
                    {
                        ClassCommon.BC41_DVYeuCau_DSBNDTO data_phong_name = new ClassCommon.BC41_DVYeuCau_DSBNDTO();
                        List<ClassCommon.BC41_DVYeuCau_DSBNDTO> lstData_Phong = lstData_Khoa.Where(o => o.departmentid == item_phong.departmentid).ToList();
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
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
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
                        List<ClassCommon.classUserDepartment> lstdsphongthuockhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
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





        #endregion

        private void cbbTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    chkcomboListNguoiDuyet.Enabled = true;
                }
                else
                {
                    chkcomboListNguoiDuyet.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
    }
}
