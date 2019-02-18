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
    public partial class ucBCVTTTRieng45TLCB : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        //private DataTable dataBaoCao { get; set; }
        public ucBCVTTTRieng45TLCB()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCVTTTRieng45TLCB_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _trangthaivienphi = "";
                string _orderby = "";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _orderby = " order by SER.servicepricedate";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _orderby = " order by VP.vienphidate";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    _orderby = " order by VP.vienphidate_ravien";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                    _orderby = " order by VP.duyet_ngayduyet_vp";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt BH")
                {
                    _tieuchi_vp = " and vienphistatus_bh=1 and duyet_ngayduyet between '" + datetungay + "' and '" + datedenngay + "' ";
                    _orderby = " order by VP.duyet_ngayduyet";
                }
                //
                if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    _trangthaivienphi = " vienphistatus=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaivienphi = " vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    _trangthaivienphi = " vienphistatus>0 and vienphistatus_vp=1 ";
                }

                string _sql_timkiem = " SELECT row_number () over (" + _orderby + ") as stt, VT.servicepriceid_thanhtoanrieng as servicepriceid, VP.patientid, VP.vienphiid, HSBA.patientname, HSBA.bhytcode, VP.vienphidate, (case when VP.vienphidate_ravien<>'0001-01-01 00:00:00' then VP.vienphidate_ravien end) as vienphidate_ravien, (case when VP.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then VP.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, KRV.departmentgroupname as khoaravien, SER.servicepricecode, SER.servicepricename_bhyt as servicepricename, SER.servicepricemoney, SER.soluong, (SER.servicepricemoney*SER.soluong) as thanhtien, SER.servicepricedate, KCD.departmentgroupname as khoachidinh, PCD.departmentname as phongchidinh, VT.thanhtien_vtyt, VT.thanhtien_vtyttran, (VT.thanhtien_vtyttran - (case when VT.stent2>36000000 then 18000000 else VT.stent2/2 end)) as stent1, (case when VT.stent2>36000000 then 18000000 else VT.stent2/2 end) as stent2, 0 as bhyt_tt, VT.ghichu, VP.bhyt_thangluongtoithieu FROM (select servicepriceid_thanhtoanrieng,vienphiid, sum(case when maubenhphamphieutype=0 then (soluong*servicepricemoney_bhyt) else 0-(soluong*servicepricemoney_bhyt) end) as thanhtien_vtyt, sum(case when stenphuthuoc=2 then (case when maubenhphamphieutype=0 then (soluong*servicepricemoney_bhyt) else 0-(soluong*servicepricemoney_bhyt) end) else 0 end) as stent2, sum(case when maubenhphamphieutype=0 then (soluong*(case when cast(servicepricebhytdinhmuc as numeric)>0 then cast(servicepricebhytdinhmuc as numeric) else servicepricemoney_bhyt end)) else 0-(soluong*(case when cast(servicepricebhytdinhmuc as numeric)>0 then cast(servicepricebhytdinhmuc as numeric) else servicepricemoney_bhyt end)) end) as thanhtien_vtyttran, string_agg(servicepricename_bhyt, '; ') as ghichu from serviceprice where servicepriceid_thanhtoanrieng>0 and loaidoituong=20 and bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') " + _tieuchi_ser + " group by servicepriceid_thanhtoanrieng,vienphiid) VT INNER JOIN (select vienphiid,patientid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,duyet_ngayduyet_vp,departmentgroupid,bhyt_thangluongtoithieu from vienphi where " + _trangthaivienphi + _tieuchi_vp + ") VP ON VP.vienphiid=VT.vienphiid INNER JOIN (select hosobenhanid,patientid,patientname,bhytcode from hosobenhan) HSBA ON HSBA.hosobenhanid=VP.hosobenhanid LEFT JOIN (select servicepriceid,vienphiid,servicepricecode,servicepricename_bhyt,servicepricedate,soluong, (case loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan else servicepricemoney end) as servicepricemoney, departmentgroupid,departmentid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC') and loaidoituong in (0,1,3,4,6) " + _tieuchi_ser + " ) SER ON SER.servicepriceid=VT.servicepriceid_thanhtoanrieng LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=SER.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=SER.departmentid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) Krv ON Krv.departmentgroupid=VP.departmentgroupid; ";

                DataTable dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBaoCao.DataSource = dataBaoCao;
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

        #region Export and Print
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

                string fileTemplatePath = "BC_VTYTTTRiengLonHon45TLCB.xlsx";
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
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

                string fileTemplatePath = "BC_VTYTTTRiengLonHon45TLCB.xlsx";
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

    }
}
