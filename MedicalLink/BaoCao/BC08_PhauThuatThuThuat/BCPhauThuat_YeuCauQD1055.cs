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
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;

namespace MedicalLink.BaoCao
{
    public partial class BCPhauThuat_YeuCauQD1055 : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBCPhauThuat_YC { get; set; }

        #endregion

        #region Load
        public BCPhauThuat_YeuCauQD1055()
        {
            InitializeComponent();
        }

        private void BCPhauThuat_YeuCauQD1055_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhSachButtonExport();
            LoadDanhSachButtonPrint();
            LoadDanhSachDichVu();
        }

        private void LoadDanhSachButtonExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("BC phẫu thuật thủ thuật yêu cầu QĐ1055"));
                menu.Items.Add(new DXMenuItem("BC thanh toán tiền phẫu thuật thủ thuật yêu cầu QĐ1055"));
                menu.Items.Add(new DXMenuItem("BC phẫu thuật thủ thuật yêu cầu QĐ1055 - Theo filter trên lưới"));
                menu.Items.Add(new DXMenuItem("BC thanh toán tiền phẫu thuật thủ thuật yêu cầu QĐ1055 - Theo filter trên lưới"));
                // ... add more items
                dropDownExport.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
                // setup initial selection
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachButtonPrint()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("BC phẫu thuật thủ thuật yêu cầu QĐ1055"));
                menu.Items.Add(new DXMenuItem("BC thanh toán tiền phẫu thuật thủ thuật yêu cầu QĐ1055"));
                // ... add more items
                dropDownPrint.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Print_Click;
                // setup initial selection
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void LoadDanhSachDichVu()
        {
            try
            {
                string servicepricecode = "'U18851-4427','U19154-1951','U18765-1454','U17261-2902','U17265-3936','U30001-5346','U30001-1207','U30001-2954'";
                if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                {
                    List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_20_DV").ToList();
                    if (lstOtherList != null && lstOtherList.Count > 0)
                    {
                        servicepricecode = lstOtherList[0].tools_otherlistvalue;
                    }
                }
                string sqlservice = "select servicepricecode, servicepricename from servicepriceref where servicepricecode in (" + servicepricecode + ") order by servicepricename;";
                DataTable dataService = condb.GetDataTable_HIS(sqlservice);
                chkcomboListDichVu.Properties.DataSource = dataService;
                chkcomboListDichVu.Properties.DisplayMember = "servicepricename";
                chkcomboListDichVu.Properties.ValueMember = "servicepricecode";
                chkcomboListDichVu.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string lstServicecheck = " ";
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _tieuchi_pttt = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    _tieuchi_pttt = " and phauthuatthuthuatdate between '" + tungay + "' and '" + denngay + "' ";
                }
                else
                {
                    _tieuchi_ser = " and servicepricedate between '" + tungay + "' and '" + denngay + "' ";
                }

                List<Object> lstPhongCheck = chkcomboListDichVu.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        lstServicecheck += "'" + lstPhongCheck[i] + "', ";
                    }
                    lstServicecheck += "'" + lstPhongCheck[lstPhongCheck.Count - 1] + "'";


                    string _sqlChayDuLieu = "SELECT row_number () over (order by A.ngay_thuchien) as stt, A.patientid, A.vienphiid, hsba.patientname, (case when hsbA.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam, (case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, hsba.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, A.ngay_chidinh, A.ngay_thuchien, kcd.departmentgroupname as khoachuyenden, krv.departmentgroupname as khoaravien, A.servicepricecode, A.servicepricename, A.loaipttt_db, A.loaipttt_l1, A.loaipttt_l2, A.loaipttt_l3, A.loaipttt, A.soluong, A.servicepricefee, A.tyle, round(cast(A.thuoc_tronggoi as numeric),0) as thuoc_tronggoi, round(cast(A.vattu_tronggoi as numeric),0) as vattu_tronggoi, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) - (A.mochinh_tien * (A.tyle/100)) - ((A.gayme_tien * (A.tyle/100))*4)) as numeric),0) as lai, mc.username as mochinh_tenbs, (A.mochinh_tien * (A.tyle/100)) as mochinh_tien, gm.username as gayme_tenbs, (A.gayme_tien * (A.tyle/100)) as gayme_tien, p1.username as phu1_tenbs, (A.gayme_tien * (A.tyle/100)) as phu1_tien, (A.gayme_tien * (A.tyle/100)) as khoachuyenden_tien, (A.gayme_tien * (A.tyle/100)) as banlanhdao_tien, A.ngay_vaovien, A.ngay_ravien, A.ngay_thanhtoan, nnth.username as nguoinhapthuchien FROM (SELECT vp.patientid, vp.vienphiid, vp.hosobenhanid, vp.bhytid, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as ngay_chidinh, pttt.phauthuatthuthuatdate as ngay_thuchien, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, ser.servicepricecode, (case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, (case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) end) as thuoc_tronggoi, (case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) end) as vattu_tronggoi, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, ser.soluong as soluong, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, pttt.phauthuatvien as mochinh_tenbs, ((case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee * ser.soluong * 0.25) as mochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee * ser.soluong * 0.05) as gayme_tien, pttt.phumo1 as phu1_tenbs, vp.vienphidate as ngay_vaovien, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan, pttt.userid as nguoinhapthuchien FROM (select servicepricecode,vienphiid,departmentgroupid,departmentid,servicepricedate,medicalrecordid,servicepricename,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney,loaipttt,servicepriceid,soluong, chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid,loaidoituong,servicepricemoney_nhandan from serviceprice where servicepricecode in (" + lstServicecheck + ") " + _tieuchi_ser + ") ser left join (select (row_number() OVER (PARTITION BY servicepriceid ORDER BY phauthuatthuthuatid desc)) as stt,servicepriceid, phauthuatthuthuatdate,phauthuatvien,bacsigayme,phumo1,phumo2,phume,dungcuvien,phume2,phumo3,dieuduong,phumo4,userid from phauthuatthuthuat) pttt on pttt.servicepriceid=ser.servicepriceid inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphistatus_vp, vienphidate_ravien,duyet_ngayduyet_vp from vienphi where 1=1 " + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select tinhtoanlaigiadvktc,pttt_loaiid,servicepricecode from servicepriceref where servicepricecode in (" + lstServicecheck + ")) serf on serf.servicepricecode=ser.servicepricecode WHERE coalesce(pttt.stt,1)=1 " + _tieuchi_pttt + ") A INNER JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname, hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba on hsbA.hosobenhanid=A.hosobenhanid LEFT JOIN departmentgroup KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN departmentgroup KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN departmentgroup krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN nhompersonnel mc ON mc.userhisid=A.mochinh_tenbs LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_tenbs LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_tenbs LEFT JOIN nhompersonnel nnth ON nnth.userhisid=A.nguoinhapthuchien;";

                    this.dataBCPhauThuat_YC = condb.GetDataTable_HIS(_sqlChayDuLieu);
                    if (this.dataBCPhauThuat_YC.Rows.Count > 0)
                    {
                        gridControlDataBCPTTT.DataSource = this.dataBCPhauThuat_YC;
                    }
                    else
                    {
                        gridControlDataBCPTTT.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_DICH_VU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Cusstom
        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Xuat bao cao
        private void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "BC phẫu thuật thủ thuật yêu cầu QĐ1055")
                {
                    tbnExportBCPTTT_Click();
                }
                else if (tenbaocao == "BC thanh toán tiền phẫu thuật thủ thuật yêu cầu QĐ1055")
                {
                    tbnExportBCThanhToanPTTT_Click();
                }
                else if (tenbaocao == "BC phẫu thuật thủ thuật yêu cầu QĐ1055 - Theo filter trên lưới")
                {
                    tbnExportBCPTTTTheoFilter_Click();
                }
                else if (tenbaocao == "BC thanh toán tiền phẫu thuật thủ thuật yêu cầu QĐ1055 - Theo filter trên lưới")
                {
                    tbnExportBCThanhToanPTTTTheoFilter_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void tbnExportBCPTTT_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_PhauThuat_YeuCauQD1055.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPhauThuat_YC);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCThanhToanPTTT_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_PhauThuat_YeuCauQD1055_ThanhToan.xlsx";

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPhauThuat_YC);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCPTTTTheoFilter_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_PhauThuat_YeuCauQD1055.xlsx";

                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void tbnExportBCThanhToanPTTTTheoFilter_Click()
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_PhauThuat_YeuCauQD1055_ThanhToan.xlsx";

                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(bandedGridViewDataBCPTTT);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region In bao cao
        private void Item_Print_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "BC phẫu thuật thủ thuật yêu cầu QĐ1055")
                {
                    tbnPrintBCPTTT_Click();
                }
                else if (tenbaocao == "BC thanh toán tiền phẫu thuật thủ thuật yêu cầu QĐ1055")
                {
                    tbnPrintBCThanhToanPTTT_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void tbnPrintBCPTTT_Click()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_PhauThuat_YeuCauQD1055.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBCPhauThuat_YC);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void tbnPrintBCThanhToanPTTT_Click()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_PhauThuat_YeuCauQD1055_ThanhToan.xlsx";

                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBCPhauThuat_YC);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

    }
}
