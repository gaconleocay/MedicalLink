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
using MedicalLink.DatabaseProcess;
using DevExpress.XtraPrinting;
using MedicalLink.Utilities;

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucBCSoChiTietBenhNhan : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<BCSoChiTietBenhNhan> lstDataSoChiTietBN { get; set; }
        #endregion

        #region Load
        public ucBCSoChiTietBenhNhan()
        {
            InitializeComponent();
        }

        private void ucBCSoChiTietBenhNhan_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tieuchi_date = "";
                string trangthaibenhan = " ";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_date = " vienphidate between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_date = " vienphidate_ravien between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_date = " duyet_ngayduyet_vp between '" + tungay + "' and '" + denngay + "' ";
                }

                if (cboTrangThai.Text == "Đang điều trị")
                {
                    trangthaibenhan = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    trangthaibenhan = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    trangthaibenhan = " and vienphistatus<>0 and vienphistatus_vp=1 ";
                }

                gridControlDataBCCTBenhNhan.DataSource = null;
                LayDuLieuBaoCao_ChayMoi(tieuchi_date, trangthaibenhan, tungay, denngay);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void bandedGridViewDataBNNT_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Xuat bao cao
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (bandedGridViewDataBCCTBenhNhan.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "BC_SoChiTietBenhNhan_01.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, Utilities.Util_DataTable.ListToDataTable(this.lstDataSoChiTietBN));
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Chay du lieu
        private void LayDuLieuBaoCao_ChayMoi(string tieuchi_date, string trangthaibenhan, string tungay, string denngay)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string getDataPT = " SELECT (rank() OVER (PARTITION BY VMS.khoanoitrudautien ORDER BY VMS.vienphidate)) as stt, VMS.khoanoitrudautien, DEGP.departmentgroupname, HSBA.patientcode, HSBA.patientname, HSBA.namsinh, HSBA.gioitinhname, BH.bhytcode, BH.macskcbbd, VMS.vienphidate, TO_CHAR(VMS.vienphidate_ravien,'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, VMS.chandoanravien_code, VMS.vienphiid, VMS.duyet_sothutuduyet_vp, TO_CHAR(VMS.duyet_ngayduyet_vp,'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, VMS.bhyt_tuyenbenhvien, BH.bhyt_loaiid, VMS.loaivienphiid, BH.du5nam6thangluongcoban, VMS.money_xetnghiem, VMS.money_cdhatdcn, VMS.money_thuoc, VMS.money_mau, VMS.money_pttt, VMS.money_vattu, VMS.money_dvktc, VMS.money_khambenh, VMS.money_giuong, VMS.money_vanchuyen, VMS.money_khac, (VMS.money_tong_bh+VMS.money_tong_vp) as money_tongcong, VMS.money_tong_bh, VMS.money_tong_vp, (case when VMS.doituongbenhnhanid=5 then (VMS.money_xetnghiem + VMS.money_cdhatdcn + VMS.money_thuoc + VMS.money_mau + VMS.money_pttt + VMS.money_vattu + VMS.money_dvktc + VMS.money_khambenh + VMS.money_giuong + VMS.money_vanchuyen + VMS.money_khac) else 0 end) as money_mienphi, 0 as money_bnthanhtoan, 0 as money_bhytthanhtoan, VMS.money_haophi, BIL.hoadon_thutien, BIL.thutien_kytruoc, BIL.tamung_kytruoc, BIL.tamung_trongky, BIL.thutien_trongky, BIL.hoanung_trongky, 0 as tylenop, (BIL.thutien_kytruoc + BIL.thutien_trongky + BIL.tamung_kytruoc + BIL.tamung_trongky - BIL.hoanung_trongky) as soducuoiky FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoanoitrudautien,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,chandoanravien_code,duyet_sothutuduyet_vp,bhyt_tuyenbenhvien, (money_xetnghiem_bh + money_xetnghiem_vp) as money_xetnghiem, (money_cdha_bh + money_cdha_vp + money_tdcn_bh + money_tdcn_vp) as money_cdhatdcn, (money_thuoc_bh + money_thuoc_vp) as money_thuoc, (money_mau_bh + money_mau_vp) as money_mau, (money_pttt_bh + money_pttt_vp) as money_pttt, (money_vattu_bh + money_vattu_vp) as money_vattu, (money_dvktc_bh + money_dvktc_vp) as money_dvktc, (money_khambenh_bh + money_khambenh_vp) as money_khambenh, (money_giuong_bh + money_giuong_vp) as money_giuong, (money_vanchuyen_bh + money_vanchuyen_vp) as money_vanchuyen, (money_khac_bh + money_khac_vp + money_phuthu_bh + money_phuthu_vp) as money_khac, money_haophi, (money_khambenh_bh+money_xetnghiem_bh+money_cdha_bh+money_tdcn_bh+money_thuoc_bh+money_mau_bh+money_pttt_bh+money_vattu_bh+money_dvktc_bh+money_giuong_bh+money_vanchuyen_bh+money_khac_bh+money_phuthu_bh) as money_tong_bh, (money_khambenh_vp+money_xetnghiem_vp+money_cdha_vp+money_tdcn_vp+money_thuoc_vp+money_mau_vp+money_pttt_vp+money_vattu_vp+money_dvktc_vp+money_giuong_vp+money_vanchuyen_vp+money_khac_vp+money_phuthu_vp) as money_tong_vp from vienphi_money_sobn where " + tieuchi_date + trangthaibenhan + " ) VMS LEFT JOIN (select b.vienphiid, STRING_AGG(case when b.loaiphieuthuid=0 then (b.billgroupcode || '/' || b.billcode || '/' || round(cast(b.datra as numeric),0)) end, '; ') as hoadon_thutien, sum(case when b.loaiphieuthuid=0 and b.billdate<'" + tungay + "' then b.datra else 0 end) as thutien_kytruoc, sum(case when b.loaiphieuthuid=2 and b.billdate<'" + tungay + "' then b.datra else 0 end) as tamung_kytruoc, sum(case when b.loaiphieuthuid=2 and b.billdate between '" + tungay + "' and '" + denngay + "' then b.datra else 0 end) as tamung_trongky, sum(case when b.loaiphieuthuid=0 and b.billdate between '" + tungay + "' and '" + denngay + "' then b.datra else 0 end) as thutien_trongky, sum(case when b.loaiphieuthuid=1 and b.billdate between '" + tungay + "' and '" + denngay + "' then b.datra else 0 end) as hoanung_trongky from bill b where b.dahuyphieu=0 group by b.vienphiid) BIL on BIL.vienphiid=VMS.vienphiid LEFT JOIN (select hosobenhanid,patientcode,patientname,to_char(birthday, 'yyyy') as namsinh,gioitinhname from hosobenhan) HSBA on HSBA.hosobenhanid=VMS.hosobenhanid INNER JOIN (select bhytid,bhytcode,macskcbbd,bhyt_loaiid,du5nam6thangluongcoban from bhyt) BH on BH.bhytid=VMS.bhytid INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) DEGP on DEGP.departmentgroupid=VMS.khoanoitrudautien;  ";
                DataTable dataBaoCaoDSSoBenhNhan = condb.GetDataTable_HIS(getDataPT);
                if (dataBaoCaoDSSoBenhNhan != null && dataBaoCaoDSSoBenhNhan.Rows.Count > 0)
                {
                    this.lstDataSoChiTietBN = new List<ClassCommon.BCSoChiTietBenhNhan>();

                    this.lstDataSoChiTietBN = Util_DataTable.DataTableToList<ClassCommon.BCSoChiTietBenhNhan>(dataBaoCaoDSSoBenhNhan);
                    foreach (var item_data in this.lstDataSoChiTietBN)
                    {
                        ClassCommon.TinhBHYTThanhToanDTO tinhBHYT = new ClassCommon.TinhBHYTThanhToanDTO();
                        tinhBHYT.bhytcode = item_data.bhytcode;
                        tinhBHYT.bhyt_loaiid = item_data.bhyt_loaiid;
                        tinhBHYT.bhyt_tuyenbenhvien = item_data.bhyt_tuyenbenhvien;
                        tinhBHYT.du5nam6thangluongcoban = item_data.du5nam6thangluongcoban;
                        tinhBHYT.loaivienphiid = item_data.loaivienphiid;

                        item_data.tylenop = 100 - TinhMucHuongBHYT.TinhMucHuongTheoTheBHYT(tinhBHYT);
                        item_data.money_bnthanhtoan = ((item_data.money_tong_bh * item_data.tylenop) / 100) + item_data.money_tong_vp;
                        item_data.money_bhytthanhtoan = item_data.money_tongcong - item_data.money_bnthanhtoan;
                        if (item_data.vienphidate_ravien == "0001-01-01 12:01:00")
                        {
                            item_data.vienphidate_ravien = null;
                        }
                    }
                    //for (int i = 0; i < dataBaoCaoDSSoBenhNhan.Rows.Count; i++)
                    //{
                    //    ClassCommon.BCSoChiTietBenhNhan dataDSBenhNhan = new ClassCommon.BCSoChiTietBenhNhan();
                    //    dataDSBenhNhan.stt = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["stt"].ToString());
                    //    dataDSBenhNhan.khoanoitrudautien = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["khoanoitrudautien"].ToString());
                    //    dataDSBenhNhan.departmentgroupname = dataBaoCaoDSSoBenhNhan.Rows[i]["departmentgroupname"].ToString();
                    //    dataDSBenhNhan.patientcode = dataBaoCaoDSSoBenhNhan.Rows[i]["patientcode"].ToString();
                    //    dataDSBenhNhan.patientname = dataBaoCaoDSSoBenhNhan.Rows[i]["patientname"].ToString();
                    //    dataDSBenhNhan.namsinh = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["namsinh"].ToString());
                    //    dataDSBenhNhan.gioitinhname = dataBaoCaoDSSoBenhNhan.Rows[i]["gioitinhname"].ToString();
                    //    dataDSBenhNhan.bhytcode = dataBaoCaoDSSoBenhNhan.Rows[i]["bhytcode"].ToString();
                    //    dataDSBenhNhan.macskcbbd = dataBaoCaoDSSoBenhNhan.Rows[i]["macskcbbd"].ToString();
                    //    dataDSBenhNhan.vienphidate = Utilities.Util_TypeConvertParse.ToDateTime(dataBaoCaoDSSoBenhNhan.Rows[i]["vienphidate"].ToString());
                    //    if (dataBaoCaoDSSoBenhNhan.Rows[i]["vienphidate_ravien"].ToString() != null && dataBaoCaoDSSoBenhNhan.Rows[i]["vienphidate_ravien"].ToString() != "" && dataBaoCaoDSSoBenhNhan.Rows[i]["vienphidate_ravien"].ToString() != "0001-01-01 12:01:00")
                    //    {
                    //        dataDSBenhNhan.vienphidate_ravien = dataBaoCaoDSSoBenhNhan.Rows[i]["vienphidate_ravien"].ToString();
                    //    }
                    //    dataDSBenhNhan.chandoanravien_code = dataBaoCaoDSSoBenhNhan.Rows[i]["chandoanravien_code"].ToString();
                    //    dataDSBenhNhan.vienphiid = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["vienphiid"].ToString());
                    //    dataDSBenhNhan.duyet_sothutuduyet_vp = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["duyet_sothutuduyet_vp"].ToString());
                    //    if (dataBaoCaoDSSoBenhNhan.Rows[i]["duyet_ngayduyet_vp"].ToString() != null && dataBaoCaoDSSoBenhNhan.Rows[i]["duyet_ngayduyet_vp"].ToString() != "" && dataBaoCaoDSSoBenhNhan.Rows[i]["duyet_ngayduyet_vp"].ToString() != "0001-01-01 12:01:00")
                    //    {
                    //        dataDSBenhNhan.duyet_ngayduyet_vp = dataBaoCaoDSSoBenhNhan.Rows[i]["duyet_ngayduyet_vp"].ToString();
                    //    }
                    //    dataDSBenhNhan.bhyt_tuyenbenhvien = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["bhyt_tuyenbenhvien"].ToString());
                    //    dataDSBenhNhan.bhyt_loaiid = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["bhyt_loaiid"].ToString());
                    //    dataDSBenhNhan.loaivienphiid = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["loaivienphiid"].ToString());
                    //    dataDSBenhNhan.du5nam6thangluongcoban = Utilities.Util_TypeConvertParse.ToInt32(dataBaoCaoDSSoBenhNhan.Rows[i]["du5nam6thangluongcoban"].ToString());
                    //    dataDSBenhNhan.money_xetnghiem = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_xetnghiem"].ToString());
                    //    dataDSBenhNhan.money_cdhatdcn = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_cdhatdcn"].ToString());
                    //    dataDSBenhNhan.money_thuoc = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_thuoc"].ToString());
                    //    dataDSBenhNhan.money_mau = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_mau"].ToString());
                    //    dataDSBenhNhan.money_pttt = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_pttt"].ToString());
                    //    dataDSBenhNhan.money_vattu = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_vattu"].ToString());
                    //    dataDSBenhNhan.money_dvktc = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_dvktc"].ToString());
                    //    dataDSBenhNhan.money_khambenh = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_khambenh"].ToString());
                    //    dataDSBenhNhan.money_giuong = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_giuong"].ToString());
                    //    dataDSBenhNhan.money_vanchuyen = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_vanchuyen"].ToString());
                    //    dataDSBenhNhan.money_khac = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_khac"].ToString());
                    //    dataDSBenhNhan.money_tongcong = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_tongcong"].ToString());
                    //    dataDSBenhNhan.money_mienphi = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_mienphi"].ToString());
                    //    dataDSBenhNhan.money_haophi = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["money_haophi"].ToString());
                    //    dataDSBenhNhan.hoadon_thutien = dataBaoCaoDSSoBenhNhan.Rows[i]["hoadon_thutien"].ToString();
                    //    dataDSBenhNhan.thutien_kytruoc = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["thutien_kytruoc"].ToString());
                    //    dataDSBenhNhan.tamung_kytruoc = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["tamung_kytruoc"].ToString());
                    //    dataDSBenhNhan.tamung_trongky = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["tamung_trongky"].ToString());
                    //    dataDSBenhNhan.thutien_trongky = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["thutien_trongky"].ToString());
                    //    dataDSBenhNhan.hoanung_trongky = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["hoanung_trongky"].ToString());
                    //    dataDSBenhNhan.tylenop = 100 - TinhMuaHuongBHYT.TinhMucHuongTheoTheBHYT(dataDSBenhNhan.bhytcode, dataDSBenhNhan.bhyt_loaiid, dataDSBenhNhan.loaivienphiid, dataDSBenhNhan.du5nam6thangluongcoban, dataDSBenhNhan.bhyt_tuyenbenhvien);
                    //    dataDSBenhNhan.soducuoiky = Utilities.Util_TypeConvertParse.ToDouble(dataBaoCaoDSSoBenhNhan.Rows[i]["soducuoiky"].ToString());

                    //    dataDSBenhNhan.money_bnthanhtoan = (dataDSBenhNhan.money_tongcong * dataDSBenhNhan.tylenop) / 100;
                    //    dataDSBenhNhan.money_bhytthanhtoan = dataDSBenhNhan.money_tongcong - dataDSBenhNhan.money_bnthanhtoan;

                    //    this.lstDataSoChiTietBN.Add(dataDSBenhNhan);
                    //}

                    gridControlDataBCCTBenhNhan.DataSource = this.lstDataSoChiTietBN;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
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

        private void cboTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    cboTrangThai.Properties.Items.Clear();
                    cboTrangThai.Properties.Items.Add("Đã thanh toán");
                    cboTrangThai.Text = "Đã thanh toán";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    cboTrangThai.Properties.Items.Clear();
                    cboTrangThai.Properties.Items.Add("Ra viện chưa thanh toán");
                    cboTrangThai.Properties.Items.Add("Đã thanh toán");
                    cboTrangThai.Properties.Items.Add("Tất cả");
                    cboTrangThai.Text = "Tất cả";
                }
                else
                {
                    cboTrangThai.Properties.Items.Clear();
                    cboTrangThai.Properties.Items.Add("Đang điều trị");
                    cboTrangThai.Properties.Items.Add("Ra viện chưa thanh toán");
                    cboTrangThai.Properties.Items.Add("Đã thanh toán");
                    cboTrangThai.Properties.Items.Add("Tất cả");
                    cboTrangThai.Text = "Tất cả";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));

                //DataSet dataSetDSBN = new DataSet();
                //PrintableComponentLink com = new PrintableComponentLink(new PrintingSystem());
                //com.Component = gridControlDataBCCTBenhNhan;
                //com.CreateDocument();
                //PrintTool pt = new PrintTool(com.PrintingSystemBase);
                //pt.ShowRibbonPreviewDialog();
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;

                thongTinThem.Add(reportitem);
                string fileTemplatePath = "BC_SoChiTietBenhNhan_01.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, Utilities.Util_DataTable.ListToDataTable(this.lstDataSoChiTietBN));
                //export.ExportExcelTemplate_ReportTemps("", fileTemplatePath, thongTinThem, Utilities.Util_DataTable.ListToDataTable(this.lstDataSoChiTietBN));

                //DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                //spreadsheetControl.AllowDrop = false;
                //string fullPath = Environment.CurrentDirectory + "\\ReportTemps\\" + fileTemplatePath;
                //spreadsheetControl.LoadDocument(fullPath, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                //DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl.Document;
                //spreadsheetControl.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();

        }
    }
}
