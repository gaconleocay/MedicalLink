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

namespace MedicalLink.BaoCao
{
    public partial class ucBCSoCDHA : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBCSoCDHA()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboPhongThucHien.EditValue == null)
            {
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_THUC_HIEN);
                frmthongbao.Show();
                return;
            }

            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi = "";
                gridControlSoCDHA.DataSource = null;
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi = " mbp.maubenhphamdate ";
                }
                else if (cboTieuChi.Text == "Theo ngày trả kết quả")
                {
                    tieuchi = " mbp.maubenhphamfinishdate ";
                }

                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, A.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, A.chandoan, (kyc.departmentgroupname || ' - ' || pyc.departmentname) as noigui, A.yeucau, A.ketqua, A.maubenhphamdate, A.maubenhphamfinishdate, ntkq.username as nguoidoc, (case when A.departmentid_des=244 then 'x' else '' end) as phim_20x25, (case when A.departmentid_des in (245,246) then 'x' else '' end) as phim_35x43 FROM (SELECT mbp.maubenhphamid, mbp.hosobenhanid, mbp.departmentgroupid, mbp.departmentid, mbp.departmentid_des, mbp.chandoan, mbp.usertrakq, mbp.userthuchien, mbp.maubenhphamdate, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, se.servicename as yeucau, se.servicevalue as ketqua FROM maubenhpham mbp LEFT JOIN service se ON se.maubenhphamid=mbp.maubenhphamid WHERE se.servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) and mbp.maubenhphamgrouptype=1 and mbp.departmentid_des='" + cboPhongThucHien.EditValue.ToString() + "' and " + tieuchi + ">='" + datetungay + "' and " + tieuchi + "<='" + datedenngay + "' ) A INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=A.hosobenhanid LEFT JOIN departmentgroup kyc ON kyc.departmentgroupid=A.departmentgroupid LEFT JOIN department pyc ON pyc.departmentid=A.departmentid LEFT JOIN tools_tblnhanvien ntkq ON ntkq.userhisid=COALESCE(A.usertrakq, A.userthuchien); ";
                    dataBaoCao = condb.GetDataTable(sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlSoCDHA.DataSource = this.dataBaoCao;
                    }
                    else
                    {
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

        #region Load
        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlSoCDHA.DataSource = null;
            LoadDataPhongThucHien();
        }
        private void LoadDataPhongThucHien()
        {
            try
            {
                var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7).OrderBy(o => o.departmentname).ToList();
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    cboPhongThucHien.Properties.DataSource = lstDSPhong;
                    cboPhongThucHien.Properties.DisplayMember = "departmentname";
                    cboPhongThucHien.Properties.ValueMember = "departmentid";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";
                string fileTemplatePath = "So_CDHA_XQuangCTMRI.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                reportitem_kho.value = cboPhongThucHien.Text.ToUpper();
                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                ClassCommon.reportExcelDTO reportitem_tenbc = new ClassCommon.reportExcelDTO();
                reportitem_tenbc.name = Base.bienTrongBaoCao.TENBAOCAO;

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

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);

                //Utilities.PrintPreview.PrintPreviewDev frmPrint = new Utilities.PrintPreview.PrintPreviewDev();
                //MedicalLink.BaoCao.BCSoCDHA.SoCDHA_XQuang rpSoCDHA = new BCSoCDHA.SoCDHA_XQuang();
                //rpSoCDHA.DataSource = this.dataBaoCao;
                //frmPrint.documentViewerData.PrintingSystem = rpSoCDHA.PrintingSystem;
                //rpSoCDHA.CreateDocument();
                //frmPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

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

        private void cboPhongThucHien_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                //phong Sieu am va phong X-quang can thiep.
                if (Utilities.Util_TypeConvertParse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 247 || Utilities.Util_TypeConvertParse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 422)
                {
                    gridBandCoPhim.Visible = false;
                }
                else
                {
                    gridBandCoPhim.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
