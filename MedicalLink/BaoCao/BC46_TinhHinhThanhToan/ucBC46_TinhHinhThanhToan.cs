using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.BaoCao
{
    public partial class ucBC46_TinhHinhThanhToan : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion


        public ucBC46_TinhHinhThanhToan()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC46_TinhHinhThanhToan_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                LoadDanhSachKhoa();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
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
                string _tieuchi_vp = "";
                string _tieuchi_vp_rv = " and vienphidate_ravien>='2017-01-01 00:00:00' ";
                string _lstKhoaChonLayBC = " and departmentgroupid in (0";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //TIeu chi
                if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //Khoa
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        _lstKhoaChonLayBC += "," + lstKhoaCheck[i];
                    }
                    _lstKhoaChonLayBC += ")";
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }

                string _sql_timkiem = $@"SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	rv.sl_rvchuatt_bh,
	rv.sl_rvchuatt_vp,
	datt.sl_ttdung_bh,
	datt.sl_ttdung_vp,
	datt.sl_ttsau1_bh,
	datt.sl_ttsau1_vp,	
	datt.sl_ttsau2_bh,
	datt.sl_ttsau2_vp,
	datt.sl_ttsau3_bh,
	datt.sl_ttsau3_vp,
	datt.sl_ttsau4_bh,
	datt.sl_ttsau4_vp,
	datt.sl_ttsau5_bh,
	datt.sl_ttsau5_vp,
	datt.sl_ttsau6_bh,
	datt.sl_ttsau6_vp,
	datt.sl_ttsau7_bh,
	datt.sl_ttsau7_vp,
	datt.sl_tthon7_bh,
	datt.sl_tthon7_vp
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp
	INNER JOIN (select vp.departmentgroupid,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=0 then 1 else 0 end) as sl_ttdung_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=0 then 1 else 0 end) as sl_ttdung_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=1 then 1 else 0 end) as sl_ttsau1_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=1 then 1 else 0 end) as sl_ttsau1_vp,	
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=2 then 1 else 0 end) as sl_ttsau2_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=2 then 1 else 0 end) as sl_ttsau2_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=3 then 1 else 0 end) as sl_ttsau3_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=3 then 1 else 0 end) as sl_ttsau3_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=4 then 1 else 0 end) as sl_ttsau4_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=4 then 1 else 0 end) as sl_ttsau4_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=5 then 1 else 0 end) as sl_ttsau5_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=5 then 1 else 0 end) as sl_ttsau5_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=6 then 1 else 0 end) as sl_ttsau6_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=6 then 1 else 0 end) as sl_ttsau6_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=7 then 1 else 0 end) as sl_ttsau7_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=7 then 1 else 0 end) as sl_ttsau7_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt>7 then 1 else 0 end) as sl_tthon7_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt>7 then 1 else 0 end) as sl_tthon7_vp
				from (select vienphiid,departmentgroupid,doituongbenhnhanid,
					(case when duyet_ngayduyet_vp is not null then ((duyet_ngayduyet_vp::date)-(vienphidate_ravien::date)) else -1 end) as songaytt
					 from vienphi where vienphistatus>0 and vienphistatus_vp=1 " + _tieuchi_vp + _lstKhoaChonLayBC + ") vp group by vp.departmentgroupid) datt on datt.departmentgroupid=degp.departmentgroupid LEFT JOIN (select departmentgroupid,sum(case when doituongbenhnhanid=1 then 1 else 0 end) as sl_rvchuatt_bh,sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as sl_rvchuatt_vp from vienphi where vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 " + _tieuchi_vp_rv + " group by departmentgroupid) rv on rv.departmentgroupid=degp.departmentgroupid;";
                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBC.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlDataBC.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
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

        #region Export va Print
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

                string fileTemplatePath = "BC_46_TinhHinhThanhToanBNRaVien.xlsx";
                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
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

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);
                string fileTemplatePath = "BC_46_TinhHinhThanhToanBNRaVien.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }


        #endregion

        #region Custom
        private void gridViewDataBC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
