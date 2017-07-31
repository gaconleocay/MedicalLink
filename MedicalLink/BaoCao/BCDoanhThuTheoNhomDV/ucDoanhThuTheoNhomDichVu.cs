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

namespace MedicalLink.BaoCao
{
    public partial class ucDoanhThuTheoNhomDichVu : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucDoanhThuTheoNhomDichVu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDoanhThuTheoNhomDichVu_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
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
                    DataTable dataNhomDichVu = condb.GetDataTable_MeL(sqlNhomDV);
                    cboNhomDichVu.Properties.DataSource = dataNhomDichVu;
                    cboNhomDichVu.Properties.DisplayMember = "tools_otherlistname";
                    cboNhomDichVu.Properties.ValueMember = "tools_otherlistvalue";
                }
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
        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string khoachidinh = " and departmentgroupid in (";
                string lstservicegroupcode = " servicepricegroupcode in (";
                string trangthaibenhan = "";
                string sql_timkiem = "";


                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }

                if (cboNhomDichVu.EditValue != null)
                {
                    string[] dsdv_temp = cboNhomDichVu.EditValue.ToString().Split(',');
                    for (int i = 0; i < dsdv_temp.Length - 1; i++)
                    {
                        lstservicegroupcode += "'" + dsdv_temp[i].ToString().Trim() + "',";
                    }
                    lstservicegroupcode += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString().Trim() + "') ";
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }

                if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    trangthaibenhan = " vienphistatus=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    trangthaibenhan = " vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    trangthaibenhan = " vienphistatus>0 and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                if (radioXemTongHop.Checked) //xem tong hop
                {
                    sql_timkiem = " SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname order by dv.servicepricegroupcode,dv.servicepricename)) as stt, degp.departmentgroupid, degp.departmentgroupname, dv.servicepricecode, dv.servicepricename, dv.servicepricegroupcode, dv.loaidoituong, sum(dv.soluong) as soluong, dv.servicepricemoney, sum(dv.soluong * dv.servicepricemoney) as thanhtien FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp inner JOIN (select ser.departmentgroupid, ser.servicepricecode, ser.servicepricename, serf.servicepricegroupcode, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, sum(ser.soluong) as soluong, (case when vp.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai else (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan when 2 then ser.servicepricemoney_bhyt when 3 then ser.servicepricemoney when 4 then ser.servicepricemoney when 5 then ser.servicepricemoney when 6 then ser.servicepricemoney when 7 then ser.servicepricemoney_nhandan when 8 then ser.servicepricemoney_nhandan when 9 then ser.servicepricemoney_nhandan end) end) as servicepricemoney from (select servicepricecode,servicepricegroupcode from servicepriceref where " + lstservicegroupcode + ") serf inner join (select vienphiid,servicepricecode,servicepricename,loaidoituong,departmentgroupid,departmentid,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong from serviceprice where bhyt_groupcode in ('06PTTT','07KTC','12NG') " + khoachidinh + ") ser on ser.servicepricecode=serf.servicepricecode inner join (select vienphiid,doituongbenhnhanid from vienphi where " + trangthaibenhan + " ) vp on vp.vienphiid=ser.vienphiid group by ser.departmentgroupid,ser.servicepricecode,ser.servicepricename,serf.servicepricegroupcode,ser.loaidoituong,ser.servicepricemoney,ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney_nuocngoai,vp.doituongbenhnhanid) dv on dv.departmentgroupid=degp.departmentgroupid GROUP BY degp.departmentgroupid,degp.departmentgroupname,dv.servicepricecode,dv.servicepricename,dv.servicepricegroupcode,dv.loaidoituong,dv.servicepricemoney; ";
                }
                else
                {
                    sql_timkiem = " select (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY serf.servicepricegroupcode,ser.servicepricename)) as stt, vp.vienphiid, vp.patientid, hsba.patientname, to_char(hsba.birthday, 'yyyy') as namsinh, hsba.gioitinhname as gioitinh, hsba.bhytcode, degp.departmentgroupname, de.departmentname, ser.servicepricedate, ser.departmentgroupid, ser.servicepricecode, ser.servicepricename, serf.servicepricegroupcode, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, ser.soluong as soluong, (case when vp.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai else (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan when 2 then ser.servicepricemoney_bhyt when 3 then ser.servicepricemoney when 4 then ser.servicepricemoney when 5 then ser.servicepricemoney when 6 then ser.servicepricemoney when 7 then ser.servicepricemoney_nhandan when 8 then ser.servicepricemoney_nhandan when 9 then ser.servicepricemoney_nhandan end) end) as servicepricemoney, (case when vp.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai * ser.soluong) else (case ser.loaidoituong when 0 then (ser.servicepricemoney_bhyt * ser.soluong) when 1 then (ser.servicepricemoney_nhandan * ser.soluong) when 2 then (ser.servicepricemoney_bhyt * ser.soluong) when 3 then (ser.servicepricemoney * ser.soluong) when 4 then (ser.servicepricemoney * ser.soluong) when 5 then (ser.servicepricemoney * ser.soluong) when 6 then (ser.servicepricemoney * ser.soluong) when 7 then (ser.servicepricemoney_nhandan * ser.soluong) when 8 then (ser.servicepricemoney_nhandan * ser.soluong) when 9 then (ser.servicepricemoney_nhandan * ser.soluong) end) end) as thanhtien from (select servicepricecode,servicepricegroupcode from servicepriceref where " + lstservicegroupcode + ") serf inner join (select vienphiid,servicepricecode,servicepricename,servicepricedate,loaidoituong,departmentgroupid,departmentid,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong from serviceprice where bhyt_groupcode in ('06PTTT','07KTC','12NG') " + khoachidinh + ") ser on ser.servicepricecode=serf.servicepricecode inner join (select vienphiid,doituongbenhnhanid,hosobenhanid,patientid from vienphi where " + trangthaibenhan + " ) vp on vp.vienphiid=ser.vienphiid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) de on de.departmentid=ser.departmentid inner join (select hosobenhanid,patientname,birthday,gioitinhname,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid; ";
                }
                this.dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
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

        #region Export
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
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_TongHop.xlsx";
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
                }
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.DoanhThuTheoNhomDichVuDTO> lstData_XuatBaoCao = new List<ClassCommon.DoanhThuTheoNhomDichVuDTO>();
                List<ClassCommon.DoanhThuTheoNhomDichVuDTO> lstDataDoanhThu = new List<ClassCommon.DoanhThuTheoNhomDichVuDTO>();
                lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.DoanhThuTheoNhomDichVuDTO>(Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao));

                List<ClassCommon.DoanhThuTheoNhomDichVuDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.DoanhThuTheoNhomDichVuDTO data_groupname = new ClassCommon.DoanhThuTheoNhomDichVuDTO();
                    List<ClassCommon.DoanhThuTheoNhomDichVuDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.departmentgroupid == item_group.departmentgroupid).ToList();
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
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }

        #endregion
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
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_TongHop.xlsx";
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
                }
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }


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
                    btnTimKiem_Click(null, null);
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
                    //gridControlDataBaoCao = new DevExpress.XtraGrid.GridControl();
                    // gridViewDataBaoCao = new GridView();
                    radioXemTongHop.Checked = false;

                    gridColumn_PhongChiDinh.Visible = true;
                    gridColumn_MaVP.Visible = true;
                    gridColumn_MaBN.Visible = true;
                    gridColumn_TenBN.Visible = true;
                    gridColumn_NamSinh.Visible = true;
                    gridColumn_GioiTinh.Visible = true;
                    gridColumn_TheBHYT.Visible = true;
                    gridColumn_TGChiDinh.Visible = true;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion


    }
}
