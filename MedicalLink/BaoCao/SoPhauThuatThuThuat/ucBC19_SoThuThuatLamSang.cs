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
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;

namespace MedicalLink.BaoCao
{
    public partial class ucBC19_SoThuThuatLamSang : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        //private DataTable dataBaoCao { get; set; }
        public ucBC19_SoThuThuatLamSang()
        {
            InitializeComponent();
        }

        #region Load
        private void ucSoThuThuatLamSang_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlSoCDHA.DataSource = null;
            LoadDanhSachKhoa();
            LoadDanhSachButtonExport();
            LoadDanhSachButonPrint();
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11 && o.departmentgroupid != 14).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
        private void LoadDanhSachButtonExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Sổ thủ thuật lâm sàng"));
                if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    menu.Items.Add(new DXMenuItem("Sổ thủ thuật lâm sàng - Thận nhân tạo"));
                }
                dropDownExport.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachButonPrint()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Sổ thủ thuật lâm sàng"));
                if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    menu.Items.Add(new DXMenuItem("Sổ thủ thuật lâm sàng - Thận nhân tạo"));
                }
                dropDownPrint.DropDownControl = menu;
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Print_Click;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tieuchi_thoigianchidinh = "";
                string tieuchi_thoigianthuchien = "";
                string khoachidinh = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_thoigianchidinh = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi_thoigianthuchien = " and pttt.phauthuatthuthuatdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                if (cboLoaiSoThuThuat.Text == "Thủ thuật các khoa")
                {
                    khoachidinh = " and departmentgroupid in (";
                    List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                    {
                        khoachidinh += lstKhoaCheck[i] + ",";
                    }
                    khoachidinh += lstKhoaCheck[lstKhoaCheck.Count - 1] + ") ";
                }
                else if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                    {
                        List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_19_TNT").ToList();
                        if (lstOtherList != null && lstOtherList.Count > 0)
                        {
                            khoachidinh = " and departmentgroupid in ( " + lstOtherList[0].tools_otherlistvalue + ") ";
                        }
                        else
                        {
                            khoachidinh = " and departmentgroupid=14 ";
                        }
                    }
                    else
                    {
                        khoachidinh = " and departmentgroupid=14 ";
                    }
                }

                string sql_timkiem = " SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt, hsba.patientcode, hsba.patientname, ser.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT, (pttt.chandoantruocphauthuat || (case when pttt.chandoantruocphauthuat_code<>'' then ' (' || pttt.chandoantruocphauthuat_code || ')' end)) as chandoantruocphauthuat, (pttt.chandoansauphauthuat || (case when pttt.chandoantruocphauthuat_code<>'' then ' (' || pttt.chandoansauphauthuat_code || ')' end)) as chandoansauphauthuat, ser.servicepricecode, ser.servicepricename, pttt.phuongphappttt, (case pttt.pttt_phuongphapvocamid when 1 then 'Gây mê tĩnh mạch' when 2 then 'Gây mê nội khí quản' when 3 then 'Gây tê tại chỗ' when 4 then 'Tiền mê + gây tê tại chỗ' when 5 then 'Gây tê tủy sống' when 6 then 'Gây tê' when 7 then 'Gây tê màng ngoài cứng' when 8 then 'Gây tê đám rối thần kinh' when 9 then 'Gây tê Codan' when 10 then 'Gây tê nhãn cầu' when 11 then 'Gây tê cạnh sống' when 99 then 'Khác' end) as pttt_phuongphapvocam, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, ser.servicepricedate, pttt.phauthuatthuthuatdate, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, bspt.username as phauthuatvien, bsgm.username as bacsigayme, phu.username as phumo1, giupviec.username as giupviec, nnhap.username as ghichu FROM (select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') " + tieuchi_thoigianchidinh + khoachidinh + " ) ser left join (select (row_number() OVER (PARTITION BY servicepriceid ORDER BY phauthuatthuthuatid desc)) as stt,servicepriceid,chandoantruocphauthuat_code,chandoantruocphauthuat,chandoansauphauthuat_code,chandoansauphauthuat,phauthuatthuthuatdate,phuongphappttt,pttt_phuongphapvocamid,pttt_hangid,phauthuatvien,bacsigayme,phumo1,phumo3,userid_gmhs from phauthuatthuthuat pttt) pttt on pttt.servicepriceid=ser.servicepriceid inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4 and pttt_loaiid in (0,5,6,7,8) and servicepricegroupcode<>'PTYC') serf on serf.servicepricecode=ser.servicepricecode inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.vienphiid=ser.vienphiid left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid left join nhompersonnel bspt on bspt.userhisid=pttt.phauthuatvien left join nhompersonnel bsgm on bsgm.userhisid=pttt.bacsigayme left join nhompersonnel phu on phu.userhisid=pttt.phumo1 left join nhompersonnel giupviec on giupviec.userhisid=pttt.phumo3 left join nhompersonnel nnhap on nnhap.userhisid=pttt.userid_gmhs WHERE coalesce(pttt.stt,1)=1 " + tieuchi_thoigianthuchien + "; ";
                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlSoCDHA.DataSource = null;
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

        #endregion

        #region In va xuat file
        void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Sổ thủ thuật lâm sàng")
                {
                    ExportSoThuThuatLamSang();
                }
                else if (tenbaocao == "Sổ thủ thuật lâm sàng - Thận nhân tạo")
                {
                    ExportSoThuThuatLamSang_TNT();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void ExportSoThuThuatLamSang()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
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

                string fileTemplatePath = "So_ThuThuatLamSang_CacKhoa.xlsx";
                //if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                //{
                //    fileTemplatePath = "So_ThuThuatLamSang_ThanNhanTao.xlsx";
                //}
                DataTable _dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void ExportSoThuThuatLamSang_TNT()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
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

                string fileTemplatePath = "So_ThuThuatLamSang_ThanNhanTao.xlsx";
                DataTable _dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        void Item_Print_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Sổ thủ thuật lâm sàng")
                {
                    PrintSoThuThuatLamSang();
                }
                else if (tenbaocao == "Sổ thủ thuật lâm sàng - Thận nhân tạo")
                {
                    PrintSoThuThuatLamSang_TNT();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void PrintSoThuThuatLamSang()
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
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "So_ThuThuatLamSang_CacKhoa.xlsx";
                //if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                //{
                //    fileTemplatePath = "So_ThuThuatLamSang_ThanNhanTao.xlsx";
                //}
                DataTable _dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataExportFilter);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }
        private void PrintSoThuThuatLamSang_TNT()
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
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.BienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "So_ThuThuatLamSang_ThanNhanTao.xlsx";
                DataTable _dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewSoCDHA);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataExportFilter);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void cboLoaiSoThuThuat_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiSoThuThuat.Text == "Thủ thuật các khoa")
                {
                    bandedGridColumn_PTVien.Caption = "Bác sỹ thủ thuật";
                    bandedGridColumn_GayMe.Visible = true;
                    bandedGridColumn_Phu.Visible = false;
                    bandedGridColumn_GiupViec.Visible = false;
                    chkcomboListDSKhoa.Enabled = true;
                }
                else if (cboLoaiSoThuThuat.Text == "Thủ thuật Thận nhân tạo")
                {
                    bandedGridColumn_PTVien.Caption = "Chính";
                    bandedGridColumn_GayMe.Visible = false;
                    bandedGridColumn_Phu.Visible = true;
                    bandedGridColumn_GiupViec.Visible = true;
                    chkcomboListDSKhoa.Enabled = false;
                }
                LoadDanhSachButtonExport();
                LoadDanhSachButonPrint();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion
    }
}
