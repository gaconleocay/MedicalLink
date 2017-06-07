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
    public partial class ucBCSoXetNghiem : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBCSoXetNghiem()
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
            if (cboPhongThucHien.EditValue.ToString() != "253")
            {
                MessageBox.Show("Chưa có báo cáo cho phòng thực hiện này.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi = "";
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
                    string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, A.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as COBHYT, A.CHANDOAN, (KYC.DEPARTMENTGROUPNAME || ' - ' || PYC.DEPARTMENTNAME) AS NOIGUI, A.YEUCAU, A.KETQUA, A.MAUBENHPHAMDATE, (case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as maubenhphamfinishdate, nth.username AS nguoithuchien, nd.username as nguoiduyet FROM (SELECT mbp.maubenhphamid, mbp.hosobenhanid, mbp.departmentgroupid, mbp.departmentid, mbp.departmentid_des, mbp.chandoan, mbp.usertrakq, mbp.userthuchien, mbp.maubenhphamdate, mbp.maubenhphamfinishdate, se.servicename as yeucau, se.servicevalue as ketqua, se.servicedoer, se.servicecomment FROM maubenhpham mbp LEFT JOIN service se ON se.maubenhphamid=mbp.maubenhphamid WHERE se.servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) and mbp.maubenhphamgrouptype=0 and mbp.departmentid_des='" + cboPhongThucHien.EditValue.ToString() + "' and " + tieuchi + ">='" + datetungay + "' and " + tieuchi + "<='" + datedenngay + "' ) A INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=A.hosobenhanid LEFT JOIN departmentgroup kyc ON kyc.departmentgroupid=A.departmentgroupid LEFT JOIN department pyc ON pyc.departmentid=A.departmentid and pyc.departmenttype in (2,3,9) LEFT JOIN tools_tblnhanvien nth ON nth.usercode=A.servicedoer LEFT JOIN tools_tblnhanvien nd ON 'UP_OK:' || nd.usercode=A.servicecomment;  ";
                    dataBaoCao = condb.GetDataTable(sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlSoCDHA.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlSoCDHA.DataSource = null;
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
                var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 6).OrderBy(o => o.departmentname).ToList();
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
                string fileTemplatePath = "So_XetNghiem_ViSinh.xlsx";

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
                    case "253": //Xet nghiem vi sinh
                        reportitem_tenbc.value = "SỔ XÉT NGHIỆM VI SINH";
                        fileTemplatePath = "So_XetNghiem_ViSinh.xlsx";
                        break;
                    default:
                        reportitem_tenbc.value = "SỔ XÉT NGHIỆM";

                        break;
                }
                thongTinThem.Add(reportitem_tenbc);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
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
                //if (Utilities.Util_TypeConvertParse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 247 || Utilities.Util_TypeConvertParse.ToInt64(cboPhongThucHien.EditValue.ToString()) == 422)
                //{
                //    gridBandCoPhim.Visible = false;
                //}
                //else
                //{
                //    gridBandCoPhim.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
