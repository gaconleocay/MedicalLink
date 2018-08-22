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

namespace MedicalLink.BCQLTaiChinh
{
    public partial class BC101_TKTienKhamYCT7CN : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }

        #endregion

        public BC101_TKTienKhamYCT7CN()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC101_TKTienKhamYCT7CN_Load(object sender, EventArgs e)
        {
            LoadDuLieuMacDinh();
            LoadDanhSachDichVu();
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                gridControlDataBC.Visible = true;
                gridControlDataBC.Dock = DockStyle.Fill;
                gridControlBNDetail.Visible = false;
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
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_101_DV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    chkListDSDichVu.Properties.DataSource = lstOtherList;
                    chkListDSDichVu.Properties.DisplayMember = "tools_otherlistname";
                    chkListDSDichVu.Properties.ValueMember = "tools_otherlistvalue";
                }
                chkListDSDichVu.CheckAll();
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
                string tieuchi_ser = "";
                string tieuchi_vp = "";
                string lstdichvu_ser = "";
                string trangthai_vp = "";
                string _tieuchi_hsba = "";
                string _tieuchi_mbp = "";
                string _hosobenhanstatus = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    trangthai_vp = " and vienphistatus=0 ";
                    _hosobenhanstatus = " and hosobenhanstatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                    _hosobenhanstatus = " and hosobenhanstatus=1 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and vienphistatus_vp=1 ";
                    _hosobenhanstatus = " and hosobenhanstatus=1 ";
                }

                //dich vu
                //if (!chkListDSDichVu.Text.Contains("Tất cả"))
                //{
                lstdichvu_ser = " and servicepricecode in (";
                List<Object> lstDVCheck = chkListDSDichVu.Properties.Items.GetCheckedValues();
                for (int i = 0; i < lstDVCheck.Count - 1; i++)
                {
                    lstdichvu_ser += "" + lstDVCheck[i] + ",";
                }
                lstdichvu_ser += "" + lstDVCheck[lstDVCheck.Count - 1] + ") ";


                if (lstdichvu_ser.Contains("TATCA"))
                {
                    lstdichvu_ser = "";
                }

                if (radioXemTongHop.Checked)//tong hop
                {
                    string _sql_timkiemTH = @"SELECT row_number () over (order by ser.yyyymmdd) as stt, ser.ngaythangnam, sum(case when ser.departmentid in (209,210,211,354,355,205,409,206,207,208,354) then ser.soluong else 0 end) as kyeucau_sl, sum(case when ser.departmentid in (209,210,211,354,355,205,409,206,207,208,354) then (ser.soluong*ser.dongia) else 0 end) as kyeucau_thanhtien, sum(case when ser.departmentid in (201,202) then ser.soluong else 0 end) as kdalieu_sl, sum(case when ser.departmentid in (201,202) then (ser.soluong*ser.dongia) else 0 end) as kdalieu_thanhtien, sum(case when ser.departmentid=212 then ser.soluong else 0 end) as kmat_sl, sum(case when ser.departmentid=212 then (ser.soluong*ser.dongia) else 0 end) as kmat_thanhtien, sum(case when ser.departmentid=220 then ser.soluong else 0 end) as krhm_sl, sum(case when ser.departmentid=220 then (ser.soluong*ser.dongia) else 0 end) as krhm_thanhtien, sum(case when ser.departmentid=222 then ser.soluong else 0 end) as ktmh_sl, sum(case when ser.departmentid=222 then (ser.soluong*ser.dongia) else 0 end) as ktmh_thanhtien FROM (select vienphiid,maubenhphamid,departmentid,TO_CHAR(servicepricedate, 'dd/MM/yyyy') as ngaythangnam,TO_CHAR(servicepricedate, 'yyyymmdd') as yyyymmdd,soluong, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where departmentid in (209,210,211,354,355,205,409,206,207,208,201,202,212,220,222,354) and EXTRACT(DOW FROM servicepricedate) in (6,0) and bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser + ") ser inner join (select maubenhphamid,maubenhphamstatus from maubenhpham where maubenhphamgrouptype=2) mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0 GROUP BY ser.ngaythangnam,ser.yyyymmdd;";
                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiemTH);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBC.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBC.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else//chi tiet
                {
                    string _sql_timkiemCT = " SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate) as stt, ser.servicepriceid, vp.patientid, vp.vienphiid, hsba.patientname, hsba.bhytcode, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, mbp.maubenhphamid, mbp.maubenhphamdate, ser.servicepricecode, ser.servicepricename, ser.soluong, ser.dongia, (ser.soluong*ser.dongia) as thanhtien, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, vp.vienphidate, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus, (case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' end) trangthaithutien FROM (select servicepriceid,vienphiid,maubenhphamid,departmentgroupid,departmentid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,servicepricedate,maubenhphamphieutype,bhyt_groupcode,loaidoituong from serviceprice where departmentid in (209,210,211,354,355,205,409,206,207,208,201,202,212,220,222,354) and EXTRACT(DOW FROM servicepricedate) in (6,0) and bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + tieuchi_ser + lstdichvu_ser + ") ser INNER JOIN (select maubenhphamid,maubenhphamstatus,maubenhphamdate,userid,departmentid_des from maubenhpham where maubenhphamgrouptype=2 " + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 " + _tieuchi_hsba + _hosobenhanstatus + ") hsba on hsba.hosobenhanid=vp.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0;";

                    DataTable _dataBCChiTiet = condb.GetDataTable_HIS(_sql_timkiemCT);
                    if (_dataBCChiTiet != null && _dataBCChiTiet.Rows.Count > 0)
                    {
                        gridControlBNDetail.DataSource = _dataBCChiTiet;
                    }
                    else
                    {
                        gridControlBNDetail.DataSource = null;
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

        #region In va xuat file
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                    item_phong.name = Base.bienTrongBaoCao.LST_DICHVU;
                    item_phong.value = chkListDSDichVu.Text;
                    thongTinThem.Add(item_phong);

                    string fileTemplatePath = "BC_101_TKTienKhamYeuCauThu7ChuNhat.xlsx";

                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataBaoCao);
                }
                else
                {
                    DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBNDetail);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("DANH SÁCH BỆNH NHÂN CHỈ ĐỊNH DỊCH VỤ", _dataBaoCao);
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
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.bienTrongBaoCao.LST_DICHVU;
                item_phong.value = chkListDSDichVu.Text;
                thongTinThem.Add(item_phong);

                string fileTemplatePath = "BC_101_TKTienKhamYeuCauThu7ChuNhat.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        #endregion

        #region Events
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlDataBC.Visible = true;
                    gridControlDataBC.DataSource = null;
                    gridControlDataBC.Dock = DockStyle.Fill;
                    gridControlBNDetail.Visible = false;
                    btnPrint.Enabled = true;
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
                    gridControlBNDetail.Visible = true;
                    gridControlBNDetail.DataSource = null;
                    gridControlBNDetail.Dock = DockStyle.Fill;
                    gridControlDataBC.Visible = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void bandedGridViewDataBC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
