﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using O2S_Common.DataObjects;

namespace O2S_MLReportRIS
{
    public partial class frmMain : Form
    {
        ConnectDatabase condb = new ConnectDatabase();
        private DataTable dataBaoCao { get; set; }

        public frmMain()
        {
            InitializeComponent();
        }

        #region Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDataPhongThucHien();
        }
        private void LoadDataPhongThucHien()
        {
            try
            {
                string _sqlPhongthuchien = "SELECT departmentid,departmentname FROM department WHERE departmenttype=7 ORDER BY departmentname;";
                DataTable _dataPhong = this.condb.GetDataTable_HIS(_sqlPhongthuchien);

                cboPhongThucHien.Properties.DataSource = _dataPhong;
                cboPhongThucHien.Properties.DisplayMember = "departmentname";
                cboPhongThucHien.Properties.ValueMember = "departmentid";
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboPhongThucHien.Text == null || cboPhongThucHien.Text == "")
            {
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao("Chưa chọn phòng thực hiện");
                frmthongbao.Show();
                return;
            }

            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tieuchi_mbp = "";
                string tieuchi_vp = "";

                gridControlSoCDHA.DataSource = null;
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày trả kết quả")
                {
                    tieuchi_mbp = " and maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " where vienphistatus>0 and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi_vp = " where vienphistatus_vp=1 and vienphistatus>0 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                string sql_timkiem = " SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, A.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as COBHYT, A.CHANDOAN, (KYC.DEPARTMENTGROUPNAME || ' - ' || PYC.DEPARTMENTNAME) AS NOIGUI, A.YEUCAU, A.KETQUA, A.MAUBENHPHAMDATE, A.maubenhphamfinishdate, A.vienphidate_ravien, A.duyet_ngayduyet_vp, (case when A.servicetimetrakq is not null then A.servicetimetrakq else (a.maubenhphamfinishdate) end) as servicetimetrakq, NTKQ.USERNAME AS NGUOIDOC, (CASE WHEN A.DEPARTMENTID_DES=244 THEN 'X' ELSE '' END) AS PHIM_20X25, (CASE WHEN A.DEPARTMENTID_DES IN (245,246) THEN 'X' ELSE '' END) AS PHIM_35X43, A.isthuocdikem, A.isvattudikem FROM (SELECT mbp.maubenhphamid, mbp.hosobenhanid, mbp.departmentgroupid, mbp.departmentid, mbp.departmentid_des, mbp.chandoan, mbp.usertrakq, mbp.userthuchien, mbp.maubenhphamdate, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, se.servicename as yeucau, se.servicevalue as ketqua, (case when se.servicetimetrakq<>'0001-01-01 00:00:00' then se.servicetimetrakq end) as servicetimetrakq, se.serviceusertrakq, (case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle'))>0 then 'X' end) as isthuocdikem, (case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle'))>0 then 'X' end) as isvattudikem FROM (select maubenhphamid,hosobenhanid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq, userthuchien,maubenhphamdate,maubenhphamfinishdate from maubenhpham where maubenhphamgrouptype=1 and departmentid_des='" + cboPhongThucHien.EditValue.ToString() + "' " + tieuchi_mbp + ") mbp LEFT JOIN (select servicepriceid,servicename,servicevalue,maubenhphamid,servicetimetrakq,serviceusertrakq from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)) se ON se.maubenhphamid=mbp.maubenhphamid INNER JOIN (select vienphiid,hosobenhanid,vienphidate_ravien,duyet_ngayduyet_vp from vienphi " + tieuchi_vp + ") vp on vp.hosobenhanid=mbp.hosobenhanid ) A INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=A.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pyc ON pyc.departmentid=A.departmentid LEFT JOIN (select userhisid,usercode,username from nhompersonnel) ntkq ON ntkq.usercode=A.serviceusertrakq; ";
                dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = this.dataBaoCao;
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao("Không tìm thấy bản ghi nào");
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + datetungay + " - " + datedenngay + " )";
                string fileTemplatePath = "So_CDHA_XQuangCTMRI.xlsx";

                List<reportExcelDTO> thongTinThem = new List<reportExcelDTO>();
                reportExcelDTO reportitem = new reportExcelDTO();
                reportitem.name = "THOIGIANBAOCAO";
                reportitem.value = tungaydenngay;
                reportExcelDTO reportitem_kho = new reportExcelDTO();
                reportitem_kho.name = "DEPARTMENTNAME";
                reportitem_kho.value = cboPhongThucHien.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                reportExcelDTO reportitem_tenbc = new reportExcelDTO();
                reportitem_tenbc.name = "TENBAOCAO";

                switch (cboPhongThucHien.EditValue.ToString())
                {
                    case "244": //SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY ("Phòng Chụp XQuang")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY";

                        break;
                    case "245"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32 ("Phòng Chụp CT")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32";

                        break;
                    case "246"://SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI ("Phòng Chụp Cộng Hưởng Từ")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI";

                        break;
                    case "247"://SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM ("Phòng Siêu Âm")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "422"://SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP ("Phòng Chụp XQuang Can Thiệp")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "2471"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128 ("Phòng Chụp CT 64") //chua them
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128";

                        break;
                    default:
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH";

                        break;
                }
                thongTinThem.Add(reportitem_tenbc);

                Utilities.Excel.ExcelExport.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

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
        private void cboPhongThucHien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //phong Sieu am va phong X-quang can thiep.
                if (O2S_Common.TypeConvert.Parse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 247 || O2S_Common.TypeConvert.Parse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 422)
                {
                    gridBandCoPhim.Visible = false;
                }
                else
                {
                    gridBandCoPhim.Visible = true;
                }
                //hien thi THuoc/vat tu di kem: phong XQ, CT, MRI
                if (O2S_Common.TypeConvert.Parse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 244 || O2S_Common.TypeConvert.Parse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 245 || O2S_Common.TypeConvert.Parse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 246)
                {
                    gridBandThuocVTDiKem.Visible = true;
                }
                else
                {
                    gridBandThuocVTDiKem.Visible = false;
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
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + datetungay + " - " + datedenngay + " )";
                string fileTemplatePath = "So_CDHA_XQuangCTMRI.xlsx";

                List<reportExcelDTO> thongTinThem = new List<reportExcelDTO>();
                reportExcelDTO reportitem = new reportExcelDTO();
                reportitem.name = "THOIGIANBAOCAO";
                reportitem.value = tungaydenngay;
                reportExcelDTO reportitem_kho = new reportExcelDTO();
                reportitem_kho.name = "DEPARTMENTNAME";
                reportitem_kho.value = cboPhongThucHien.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                reportExcelDTO reportitem_tenbc = new reportExcelDTO();
                reportitem_tenbc.name = "TENBAOCAO";

                switch (cboPhongThucHien.EditValue.ToString())
                {
                    case "244": //SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY ("Phòng Chụp XQuang")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG THƯỜNG QUY";

                        break;
                    case "245"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32 ("Phòng Chụp CT")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 1-32";

                        break;
                    case "246"://SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI ("Phòng Chụp Cộng Hưởng Từ")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : MRI";

                        break;
                    case "247"://SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM ("Phòng Siêu Âm")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : SIÊU ÂM";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "422"://SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP ("Phòng Chụp XQuang Can Thiệp")
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : XQUANG CAN THIỆP";
                        fileTemplatePath = "So_CDHA_SieuAmXQuangCanThiep.xlsx";
                        break;
                    case "2471"://SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128 ("Phòng Chụp CT 64") //chua them
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH : CT 64-128";

                        break;
                    default:
                        reportitem_tenbc.value = "SỔ CHẨN ĐOÁN HÌNH ẢNH";

                        break;
                }
                thongTinThem.Add(reportitem_tenbc);

                Utilities.Prints.PrintPreview.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        #endregion


    }
}
