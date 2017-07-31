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
using MedicalLink.DatabaseProcess;

namespace MedicalLink.BaoCao
{
    public partial class ucBCTienBNPhaiThanhToan : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ClassCommon.TienBNPhaiThanhToanDTO> lstDataBaoCaoCurrent { get; set; }
        public ucBCTienBNPhaiThanhToan()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCTienBNPhaiThanhToan_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlDataBaoCao.DataSource = null;
        }
        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string doituongbenhnhan_vp = "";
                string doituongbenhnhan_ser = "";

                string sql_timkiem = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboDoiTuongBN.Text == "ĐT BHYT")
                {
                    doituongbenhnhan_vp = " and doituongbenhnhanid=1 ";
                }
                else if (cboDoiTuongBN.Text == "ĐT Viện phí")
                {
                    doituongbenhnhan_vp = " and doituongbenhnhanid<>1 ";
                }
                else if (cboDoiTuongBN.Text == "ĐT BHYT+DV Viện phí")
                {
                    doituongbenhnhan_vp = " and doituongbenhnhanid=1 ";
                    doituongbenhnhan_ser = " and loaidoituong in (1,2,3,8) ";
                }
                else if (cboDoiTuongBN.Text == "ĐT BHYT+DV BHYT")
                {
                    doituongbenhnhan_vp = " and doituongbenhnhanid=1 ";
                    doituongbenhnhan_ser = " and loaidoituong in (0,2,4,6) ";
                }

                sql_timkiem = "";
                MessageBox.Show("Chức năng đang phát triển!");

                DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);

                this.lstDataBaoCaoCurrent = new List<ClassCommon.TienBNPhaiThanhToanDTO>();

                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    this.lstDataBaoCaoCurrent = Util_DataTable.DataTableToList<ClassCommon.TienBNPhaiThanhToanDTO>(dataBaoCao);
                    foreach (var item_data in this.lstDataBaoCaoCurrent)
                    {

                        // item_data.money_tong = item_data.money_tong_bh + item_data.money_tong_vp;
                        ClassCommon.TinhBHYTThanhToanDTO tinhBHYT = new ClassCommon.TinhBHYTThanhToanDTO();
                        tinhBHYT.bhytcode = item_data.bhytcode;
                        tinhBHYT.bhyt_loaiid = item_data.bhyt_loaiid;
                        tinhBHYT.bhyt_tuyenbenhvien = item_data.bhyt_tuyenbenhvien;
                        tinhBHYT.chiphi_goidvktc = 0;
                        tinhBHYT.chiphi_trongpvql = item_data.money_tong_bh;
                        tinhBHYT.du5nam6thangluongcoban = item_data.du5nam6thangluongcoban;
                        tinhBHYT.loaivienphiid = item_data.loaivienphiid;
                        tinhBHYT.thangluongcoban = item_data.thangluongcoban;

                        //
                        decimal tyle_bntt = 0;

                        item_data.money_tong_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_khambenh_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_xetnghiem_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_cdhatdcn_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_pttt_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_dvktc_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_mau_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_thuoc_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_vattu_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_giuong_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_phuthu_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_vanchuyen_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_khac_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);

                        //item_data.money_bhyttt = TinhMucHuongBHYT.TinhSoTienBHYTThanhToan(tinhBHYT);
                        //item_data.money_bntt = item_data.money_tong - item_data.money_bhyttt;

                        //item_data.money_thieu = item_data.money_bntt - item_data.money_datra - item_data.money_tamung + item_data.money_hoanung;
                    }

                    gridControlDataBaoCao.DataSource = this.lstDataBaoCaoCurrent;
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
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_TienBenhNhanPhaiThanhToan_ChiTiet.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
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

                string fileTemplatePath = "BC_TienBenhNhanPhaiThanhToan_ChiTiet.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.TienBNPhaiThanhToanDTO> lstData_XuatBaoCao = new List<ClassCommon.TienBNPhaiThanhToanDTO>();

                List<ClassCommon.TienBNPhaiThanhToanDTO> lstData_Group = this.lstDataBaoCaoCurrent.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.TienBNPhaiThanhToanDTO data_groupname = new ClassCommon.TienBNPhaiThanhToanDTO();
                    List<ClassCommon.TienBNPhaiThanhToanDTO> lstData_doanhthu = this.lstDataBaoCaoCurrent.Where(o => o.departmentgroupid == item_group.departmentgroupid).ToList();
                    decimal money_tong_bntt = 0;
                    decimal money_khambenh_bntt = 0;
                    decimal money_xetnghiem_bntt = 0;
                    decimal money_cdhatdcn_bntt = 0;
                    decimal money_pttt_bntt = 0;
                    decimal money_dvktc_bntt = 0;
                    decimal money_mau_bntt = 0;
                    decimal money_thuoc_bntt = 0;
                    decimal money_vattu_bntt = 0;
                    decimal money_giuong_bntt = 0;
                    decimal money_phuthu_bntt = 0;
                    decimal money_vanchuyen_bntt = 0;
                    decimal money_khac_bntt = 0;

                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        money_tong_bntt += item_tinhsum.money_tong_bntt;
                        money_khambenh_bntt += item_tinhsum.money_khambenh_bntt;
                        money_xetnghiem_bntt += item_tinhsum.money_xetnghiem_bntt;
                        money_cdhatdcn_bntt += item_tinhsum.money_cdhatdcn_bntt;
                        money_pttt_bntt += item_tinhsum.money_pttt_bntt;
                        money_dvktc_bntt += item_tinhsum.money_dvktc_bntt;
                        money_mau_bntt += item_tinhsum.money_mau_bntt;
                        money_thuoc_bntt += item_tinhsum.money_thuoc_bntt;
                        money_vattu_bntt += item_tinhsum.money_vattu_bntt;
                        money_giuong_bntt += item_tinhsum.money_giuong_bntt;
                        money_phuthu_bntt += item_tinhsum.money_phuthu_bntt;
                        money_vanchuyen_bntt += item_tinhsum.money_vanchuyen_bntt;
                        money_khac_bntt += item_tinhsum.money_khac_bntt;

                    }

                    data_groupname.departmentgroupid = item_group.departmentgroupid;
                    data_groupname.stt = item_group.departmentgroupname;
                    data_groupname.money_tong_bntt = money_tong_bntt;
                    data_groupname.money_khambenh_bntt = money_khambenh_bntt;
                    data_groupname.money_xetnghiem_bntt = money_xetnghiem_bntt;
                    data_groupname.money_cdhatdcn_bntt = money_cdhatdcn_bntt;
                    data_groupname.money_pttt_bntt = money_pttt_bntt;
                    data_groupname.money_dvktc_bntt = money_dvktc_bntt;
                    data_groupname.money_mau_bntt = money_mau_bntt;
                    data_groupname.money_thuoc_bntt = money_thuoc_bntt;
                    data_groupname.money_vattu_bntt = money_vattu_bntt;
                    data_groupname.money_giuong_bntt = money_giuong_bntt;
                    data_groupname.money_phuthu_bntt = money_phuthu_bntt;
                    data_groupname.money_vanchuyen_bntt = money_vanchuyen_bntt;
                    data_groupname.money_khac_bntt = money_khac_bntt;
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



    }
}
