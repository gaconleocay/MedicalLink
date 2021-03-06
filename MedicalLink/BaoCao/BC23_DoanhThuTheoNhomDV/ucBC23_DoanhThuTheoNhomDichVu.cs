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

namespace MedicalLink.BaoCao
{
    public partial class ucBC23_DoanhThuTheoNhomDichVu : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        public ucBC23_DoanhThuTheoNhomDichVu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDoanhThuTheoNhomDichVu_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlDataBaoCao.DataSource = null;
            LoadDanhSachNhomDichVu();
            LoadDanhSachKhoa();
            radioXemTongHop.Checked = true;
        }
        private void LoadDanhSachNhomDichVu()
        {
            try
            {
                if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                {
                    List<ClassCommon.ToolsOtherListDTO> dataNhomDichVu = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_23_NHOM_DV").ToList();
                    cboNhomDichVu.Properties.DataSource = dataNhomDichVu;
                    cboNhomDichVu.Properties.DisplayMember = "tools_otherlistname";
                    cboNhomDichVu.Properties.ValueMember = "tools_otherlistvalue";
                }
                else
                {
                    string sqlNhomDV = "select o.tools_otherlistcode, o.tools_otherlistname,o.tools_otherlistvalue from tools_othertypelist ot inner join tools_otherlist o on o.tools_othertypelistid=ot.tools_othertypelistid where ot.tools_othertypelistcode='REPORT_23_NHOM_DV'; ";
                    System.Data.DataTable dataNhomDichVu = condb.GetDataTable_MeL(sqlNhomDV);
                    cboNhomDichVu.Properties.DataSource = dataNhomDichVu;
                    cboNhomDichVu.Properties.DisplayMember = "tools_otherlistname";
                    cboNhomDichVu.Properties.ValueMember = "tools_otherlistvalue";
                }
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_ser = " and servicepricedate>'2018-01-01 00:00:00' ";
                string _tieuchi_vp = " and vienphidate>'2017-01-01 00:00:00' ";
                string _tieuchi_hsba = " and hosobenhandate>'2017-01-01 00:00:00' ";
                string khoachidinh = " and departmentgroupid in (";
                string _lstservicecode = "";
                string _trangthaibenhan = "";
                string sql_timkiem = "";
                string _bntronvien = "";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (chkcomboListDSKhoa.EditValue != null)
                {
                    List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                    {
                        khoachidinh += lstKhoaCheck[i] + ",";
                    }
                    khoachidinh += lstKhoaCheck[lstKhoaCheck.Count - 1] + ") ";
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }

                //Lay danh sach Dich vu
                if (cboNhomDichVu.EditValue != null)
                {
                    string _lstservicegroupcode = "";
                    string[] dsdv_temp = cboNhomDichVu.EditValue.ToString().Split(',');
                    for (int i = 0; i < dsdv_temp.Length - 1; i++)
                    {
                        _lstservicegroupcode += "'" + dsdv_temp[i].ToString().Trim() + "',";
                    }
                    _lstservicegroupcode += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString().Trim() + "' ";

                    string _sqlLayDSDV = "select servicepricecode from servicepriceref where servicepricegroupcode  in (" + _lstservicegroupcode + ");";
                    DataTable _dataDSDV = condb.GetDataTable_HIS(_sqlLayDSDV);
                    if (_dataDSDV.Rows.Count > 0)
                    {
                        _lstservicecode = " and servicepricecode in ('AdA1'";

                        for (int i = 0; i < _dataDSDV.Rows.Count; i++)
                        {
                            _lstservicecode += ",'" + _dataDSDV.Rows[i]["servicepricecode"].ToString() + "'";
                        }
                        _lstservicecode += ") ";
                    }
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }

                if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    _trangthaibenhan = " and vienphistatus=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaibenhan = " and vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    _trangthaibenhan = " and vienphistatus>0 and vienphistatus_vp=1 ";
                    _tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "'";
                }
                //BN tron vien
                if (chkBnTronVien.Checked == false)
                {
                    _bntronvien = " and coalesce(datronvien,0)=0 ";
                }

                if (radioXemTongHop.Checked) //xem tong hop
                {
                    sql_timkiem = $@"SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY dv.servicepricename)) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	dv.servicepricecode,
	dv.servicepricename,
	dv.loaidoituong,
	sum(dv.soluong) as soluong,
	dv.servicepricemoney,
	sum(dv.soluong * dv.servicepricemoney) as thanhtien
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp 
INNER JOIN 	
	(select 
		ser.departmentgroupid,
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
			end) as loaidoituong,
		sum(ser.soluong) as soluong,
		(case when vp.doituongbenhnhanid=4 
					then ser.servicepricemoney_nuocngoai
			else 
				(case ser.loaidoituong
					when 0 then ser.servicepricemoney_bhyt
					when 1 then ser.servicepricemoney_nhandan
					when 2 then ser.servicepricemoney_bhyt
					when 3 then ser.servicepricemoney
					when 4 then ser.servicepricemoney
					when 5 then ser.servicepricemoney
					when 6 then ser.servicepricemoney
					when 7 then ser.servicepricemoney_nhandan
					when 8 then ser.servicepricemoney_nhandan
					when 9 then ser.servicepricemoney_nhandan
					end)
			end) as servicepricemoney
	from (select vienphiid,servicepricecode,servicepricename,loaidoituong,departmentgroupid,departmentid,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong from serviceprice where 1=1 {_lstservicecode} {khoachidinh} {_tieuchi_ser}) ser
		inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_vp} {_trangthaibenhan} {_bntronvien}) vp on vp.vienphiid=ser.vienphiid 
	group by ser.departmentgroupid,ser.servicepricecode,ser.servicepricename,ser.loaidoituong,ser.servicepricemoney,ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney_nuocngoai,vp.doituongbenhnhanid
	order by ser.servicepricename) dv on dv.departmentgroupid=degp.departmentgroupid
GROUP BY degp.departmentgroupid,degp.departmentgroupname,dv.servicepricecode,dv.servicepricename,dv.loaidoituong,dv.servicepricemoney;";
                }
                else
                {
                    sql_timkiem = $@"select (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY ser.servicepricename)) as stt,
		vp.vienphiid,
		vp.patientid,
		hsba.patientname,
		to_char(hsba.birthday,'yyyy') as namsinh,
		hsba.gioitinhname as gioitinh,
		hsba.bhytcode,
		degp.departmentgroupname,
		de.departmentname,
		ser.servicepricedate,
		ser.departmentgroupid,
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
			end) as loaidoituong,
		ser.soluong as soluong,
		(case when vp.doituongbenhnhanid=4 
					then ser.servicepricemoney_nuocngoai
			else 
				(case ser.loaidoituong
					when 0 then ser.servicepricemoney_bhyt
					when 1 then ser.servicepricemoney_nhandan
					when 2 then ser.servicepricemoney_bhyt
					when 3 then ser.servicepricemoney
					when 4 then ser.servicepricemoney
					when 5 then ser.servicepricemoney
					when 6 then ser.servicepricemoney
					when 7 then ser.servicepricemoney_nhandan
					when 8 then ser.servicepricemoney_nhandan
					when 9 then ser.servicepricemoney_nhandan
					end)
			end) as servicepricemoney,
		(case when vp.doituongbenhnhanid=4 
					then (ser.servicepricemoney_nuocngoai * ser.soluong)
			else 
				(case ser.loaidoituong
					when 0 then (ser.servicepricemoney_bhyt * ser.soluong)
					when 1 then (ser.servicepricemoney_nhandan * ser.soluong)
					when 2 then (ser.servicepricemoney_bhyt * ser.soluong)
					when 3 then (ser.servicepricemoney * ser.soluong)
					when 4 then (ser.servicepricemoney * ser.soluong)
					when 5 then (ser.servicepricemoney * ser.soluong)
					when 6 then (ser.servicepricemoney * ser.soluong)
					when 7 then (ser.servicepricemoney_nhandan * ser.soluong)
					when 8 then (ser.servicepricemoney_nhandan * ser.soluong)
					when 9 then (ser.servicepricemoney_nhandan * ser.soluong)
					end)
			end) as thanhtien,
		(case when vp.datronvien=1 then 'BN trốn viện' else '' end) as bntronvien
	from (select vienphiid,servicepricecode,servicepricename,servicepricedate,loaidoituong,departmentgroupid,departmentid,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong from serviceprice where 1=1 {_lstservicecode} {khoachidinh} {_tieuchi_ser}) ser
		inner join (select vienphiid,doituongbenhnhanid,hosobenhanid,patientid,datronvien from vienphi where 1=1 {_tieuchi_vp} {_trangthaibenhan} {_bntronvien}) vp on vp.vienphiid=ser.vienphiid 
		inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid	
		left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) de on de.departmentid=ser.departmentid
		inner join (select hosobenhanid,patientname,birthday,gioitinhname,bhytcode from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid;";
                }
                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Cusstom
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

        #endregion

        #region Export
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
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                if (radioXemChiTiet.Checked)
                {
                    string fileTemplatePath = "BC_23_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
                    DataTable data_XuatBaoCao = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
                else
                {
                    string fileTemplatePath = "BC_23_DoanhThuTheoNhomDichVu_TongHop.xlsx";
                    System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Print
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
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                if (radioXemChiTiet.Checked)
                {
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                    string fileTemplatePath = "BC_23_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
                    Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
                else
                {
                    string fileTemplatePath = "BC_23_DoanhThuTheoNhomDichVu_TongHop.xlsx";
                    System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                    Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        #endregion

        #region Process
        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO> lstData_XuatBaoCao = new List<ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO>();
                List<ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO> lstDataDoanhThu = DataTables.DataTableToList<ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO>(Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao));

                List<ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO data_groupname = new ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO();
                    List<ClassCommon.BC23_DoanhThuTheoNhomDichVuDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.departmentgroupid == item_group.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;
                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                    }

                    data_groupname.departmentgroupid = item_group.departmentgroupid;
                    data_groupname.stt = item_group.departmentgroupname;
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

        #endregion

        #region Event Change
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    //gridControlDataBaoCao = new DevExpress.XtraGrid.GridControl();
                    //  gridViewDataBaoCao = new GridView();
                    radioXemChiTiet.Checked = false;

                    gridColumn_PhongChiDinh.Visible = false;
                    gridColumn_MaVP.Visible = false;
                    gridColumn_MaBN.Visible = false;
                    gridColumn_TenBN.Visible = false;
                    gridColumn_NamSinh.Visible = false;
                    gridColumn_GioiTinh.Visible = false;
                    gridColumn_TheBHYT.Visible = false;
                    gridColumn_TGChiDinh.Visible = false;
                    gridColumn_bntronvien.Visible = false;
                    btnTimKiem_Click(null, null);
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

                    gridColumn_PhongChiDinh.Visible = true;
                    gridColumn_MaVP.Visible = true;
                    gridColumn_MaBN.Visible = true;
                    gridColumn_TenBN.Visible = true;
                    gridColumn_NamSinh.Visible = true;
                    gridColumn_GioiTinh.Visible = true;
                    gridColumn_TheBHYT.Visible = true;
                    gridColumn_TGChiDinh.Visible = true;
                    gridColumn_bntronvien.Visible = true;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void cboTrangThaiVienPhi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    dateTuNgay.Enabled = true;
                    dateDenNgay.Enabled = true;
                }
                else
                {
                    dateTuNgay.Enabled = false;
                    dateDenNgay.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion


    }
}
