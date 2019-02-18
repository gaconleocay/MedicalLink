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
    public partial class ucBNThieuTienTamUng : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private List<ClassCommon.BNThieuTienTamUngDTO> lstDataBaoCaoCurrent { get; set; }
        public ucBNThieuTienTamUng()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBNThieuTienTamUng_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlDataBaoCao.DataSource = null;
            LoadDanhSachKhoa();
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
                string departmentgroupid = " and departmentgroupid in (";
                string doituongbenhnhan = "";
                string sql_timkiem = "";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (chkcomboListDSKhoa.EditValue != null)
                {
                    List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                    {
                        departmentgroupid += lstKhoaCheck[i] + ",";
                    }
                    departmentgroupid += lstKhoaCheck[lstKhoaCheck.Count - 1] + ") ";
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }

                if (cboDoiTuongBN.Text == "BHYT")
                {
                    doituongbenhnhan = " and doituongbenhnhanid=1 ";
                }
                else if (cboDoiTuongBN.Text == "Viện phí")
                {
                    doituongbenhnhan = " and doituongbenhnhanid<>1 ";
                }

                sql_timkiem = "select (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY vms.vienphidate)) as stt, vms.patientid, hsba.patientcode, vms.vienphiid, hsba.patientname, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, bh.bhytcode, bh.macskcbbd, vms.bhyt_tuyenbenhvien, bh.bhyt_loaiid, vms.loaivienphiid, bh.du5nam6thangluongcoban, vms.bhyt_thangluongtoithieu as thangluongcoban, vms.vienphidate, (case when vms.vienphistatus>0 then vms.vienphidate_ravien end) as vienphidate_ravien, degp.departmentgroupid, degp.departmentgroupname, de.departmentname, b.money_tamung, b.money_datra, b.money_hoanung, vms.money_tong_bh, vms.money_tong_vp, 0 as money_tong, 0 as money_bhyttt, 0 as money_bntt, 0 as money_thieu, 0 as tyle_bntt, 0 as isgroup from (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,departmentgroupid,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu, (money_khambenh_bh+money_xetnghiem_bh+money_cdha_bh+money_tdcn_bh+money_thuoc_bh+money_mau_bh+money_pttt_bh+money_vattu_bh+money_dvktc_bh+money_giuong_bh+money_vanchuyen_bh+money_khac_bh+money_phuthu_bh) as money_tong_bh, (money_khambenh_vp+money_xetnghiem_vp+money_cdha_vp+money_tdcn_vp+money_thuoc_vp+money_mau_vp+money_pttt_vp+money_vattu_vp+money_dvktc_vp+money_giuong_vp+money_vanchuyen_vp+money_khac_vp+money_phuthu_vp) as money_tong_vp from vienphi_money_sobn where vienphidate between '" + datetungay + "' and '" + datedenngay + "' " + departmentgroupid + doituongbenhnhan + " ) vms inner join (select hosobenhanid,patientcode,patientname,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vms.hosobenhanid inner join (select bhytid,bhytcode,macskcbbd,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=vms.bhytid left join (select vienphiid, sum(case when loaiphieuthuid=2 then datra else 0 end) as money_tamung, sum(case when loaiphieuthuid=0 then datra else 0 end) as money_datra, sum(case when loaiphieuthuid=1 then datra else 0 end) as money_hoanung from bill where billdate>='" + datetungay + "' and dahuyphieu=0 group by vienphiid ) b on b.vienphiid=vms.vienphiid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vms.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=vms.departmentid;";
                System.Data.DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);

                this.lstDataBaoCaoCurrent = new List<ClassCommon.BNThieuTienTamUngDTO>();

                List<ClassCommon.BNThieuTienTamUngDTO> lstDataBaoCao = new List<ClassCommon.BNThieuTienTamUngDTO>();
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    lstDataBaoCao = DataTables.DataTableToList<ClassCommon.BNThieuTienTamUngDTO>(dataBaoCao);

                    foreach (var item_data in lstDataBaoCao)
                    {
                        item_data.money_tong = item_data.money_tong_bh + item_data.money_tong_vp;

                        ClassCommon.TinhBHYTThanhToanDTO tinhBHYT = new ClassCommon.TinhBHYTThanhToanDTO();
                        tinhBHYT.bhytcode = item_data.bhytcode;
                        tinhBHYT.bhyt_loaiid = item_data.bhyt_loaiid;
                        tinhBHYT.bhyt_tuyenbenhvien = item_data.bhyt_tuyenbenhvien;
                        tinhBHYT.chiphi_goidvktc = 0;
                        tinhBHYT.chiphi_trongpvql = item_data.money_tong_bh;
                        tinhBHYT.du5nam6thangluongcoban = item_data.du5nam6thangluongcoban;
                        tinhBHYT.loaivienphiid = item_data.loaivienphiid;
                        tinhBHYT.bhyt_thangluongtoithieu = item_data.thangluongcoban;


                        item_data.money_bhyttt = TinhMucHuongBHYT.TinhSoTienBHYTThanhToan(tinhBHYT);
                        item_data.money_bntt = item_data.money_tong - item_data.money_bhyttt;

                        //item_data.money_tong = item_data.money_tong_bh + item_data.money_tong_vp;
                        //item_data.tyle_bntt = 100 - TinhMucHuongBHYT.TinhMucHuongTheoTheBHYT(item_data.bhytcode, item_data.bhyt_loaiid, item_data.loaivienphiid, item_data.du5nam6thangluongcoban, item_data.bhyt_tuyenbenhvien);
                        //item_data.money_bntt = ((item_data.money_tong_bh * item_data.tyle_bntt) / 100) + item_data.money_tong_vp;
                        //item_data.money_bhyttt = item_data.money_tong - item_data.money_bntt;
                        item_data.money_thieu = item_data.money_bntt - item_data.money_datra - item_data.money_tamung + item_data.money_hoanung;
                    }

                    this.lstDataBaoCaoCurrent = lstDataBaoCao.Where(o => o.money_thieu > 0).ToList();
                    for (int i = 0; i < this.lstDataBaoCaoCurrent.Count; i++)
                    {
                        this.lstDataBaoCaoCurrent[i].stt = (i + 1).ToString();
                    }
                    gridControlDataBaoCao.DataSource = this.lstDataBaoCaoCurrent;
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

        #region Export
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
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

                string fileTemplatePath = "BC_BenhNhanThieuTienTamUng.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BNThieuTienTamUngDTO> lstData_XuatBaoCao = new List<ClassCommon.BNThieuTienTamUngDTO>();
                //List<ClassCommon.BNThieuTienTamUngDTO> lstDataDoanhThu = new List<ClassCommon.BNThieuTienTamUngDTO>();
                //lstDataDoanhThu = Util_DataTables.DataTableToList<ClassCommon.BNThieuTienTamUngDTO>(this.dataBaoCao);

                List<ClassCommon.BNThieuTienTamUngDTO> lstData_Group = this.lstDataBaoCaoCurrent.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BNThieuTienTamUngDTO data_groupname = new ClassCommon.BNThieuTienTamUngDTO();
                    List<ClassCommon.BNThieuTienTamUngDTO> lstData_doanhthu = this.lstDataBaoCaoCurrent.Where(o => o.departmentgroupid == item_group.departmentgroupid).ToList();
                    decimal money_tong = 0;
                    decimal money_bhyttt = 0;
                    decimal money_bntt = 0;
                    decimal money_tamung = 0;
                    decimal money_thieu = 0;

                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        money_tong += item_tinhsum.money_tong;
                        money_bhyttt += item_tinhsum.money_bhyttt;
                        money_bntt += item_tinhsum.money_bntt;
                        money_tamung += item_tinhsum.money_tamung;
                        money_thieu += item_tinhsum.money_thieu;
                    }

                    data_groupname.departmentgroupid = item_group.departmentgroupid;
                    data_groupname.stt = item_group.departmentgroupname;
                    data_groupname.vienphidate = null;
                    data_groupname.vienphidate_ravien = null;
                    data_groupname.money_tong = money_tong;
                    data_groupname.money_bhyttt = money_bhyttt;
                    data_groupname.money_bntt = money_bntt;
                    data_groupname.money_tamung = money_tamung;
                    data_groupname.money_thieu = money_thieu;
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

                string fileTemplatePath = "BC_BenhNhanThieuTienTamUng.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

    }
}
