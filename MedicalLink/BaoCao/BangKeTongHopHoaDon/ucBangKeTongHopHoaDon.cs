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

namespace MedicalLink.BaoCao
{
    public partial class ucBangKeTongHopHoaDon : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataDanhSachSo { get; set; }
        //private decimal tongTienBaoCao = 0;

        #endregion

        #region Load
        public ucBangKeTongHopHoaDon()
        {
            InitializeComponent();
        }

        private void ucBangKeTongHopHoaDon_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhSachNguoiThu();
        }
        private void LoadDanhSachNguoiThu()
        {
            try
            {
                string sql_ngthu = "select userhisid,usercode,username from nhompersonnel where usergnhom='2';";
                DataTable _dataThuNgan = condb.GetDataTable_HIS(sql_ngthu);
                chkcomboListNguoiThu.Properties.DataSource = _dataThuNgan;
                chkcomboListNguoiThu.Properties.DisplayMember = "username";
                chkcomboListNguoiThu.Properties.ValueMember = "userhisid";
                chkcomboListNguoiThu.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                gridControlDSHoaDon.DataSource = null;
                this.dataDanhSachSo = new DataTable();

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                int loaiphieuthuid = 0; //thu tien
                string _listuserid = "";
                if (cboLoaiSo.Text == "Hoàn ứng")
                {
                    loaiphieuthuid = 1;
                }
                else if (cboLoaiSo.Text == "Tạm ứng")
                {
                    loaiphieuthuid = 2;
                }
                List<Object> lstNhanVienCheck = chkcomboListNguoiThu.Properties.Items.GetCheckedValues();
                if (lstNhanVienCheck.Count > 0 || chkTatCa.Checked)
                {
                    if (chkTatCa.Checked == false)
                    {
                        _listuserid = " and userid in (";
                        for (int i = 0; i < lstNhanVienCheck.Count - 1; i++)
                        {
                            _listuserid += "'" + lstNhanVienCheck[i] + "', ";
                        }
                        _listuserid += "'" + lstNhanVienCheck[lstNhanVienCheck.Count - 1] + "') ";
                    }
                    string sql_getdata = " SELECT ROW_NUMBER () OVER (ORDER BY O.billgroupcode) as stt, O.billgroupcode, '' as billgroupdate, (O.sophieuden-O.sophieutu+1) as sophieusudung, (O.sophieutu || '-' || O.sophieuden) as sophieutu_den, O.billcode_huy, O.tongtien_thu, O.miengiam, O.userid, O.nguoithu FROM (SELECT b.billgroupcode, (select min(cast(billcode as numeric)) from bill where billdate between '" + tungay + "' and '" + denngay + "' " + _listuserid + " and loaiphieuthuid='" + loaiphieuthuid + "' and billgroupcode=b.billgroupcode) as sophieutu, (select max(cast(billcode as numeric)) from bill where billdate between '" + tungay + "' and '" + denngay + "' " + _listuserid + " and loaiphieuthuid='" + loaiphieuthuid + "' and billgroupcode=b.billgroupcode) as sophieuden, string_agg(case when b.dahuyphieu=1 then b.billcode end,'; ') as billcode_huy, sum(case when b.dahuyphieu=0 then (b.datra-b.miengiam) else 0 end) as tongtien_thu, sum(case when b.dahuyphieu=0 then b.miengiam else 0 end) as miengiam, b.userid, ngthu.username as nguoithu FROM (select billgroupcode,dahuyphieu,datra,billcode,(case when miengiam<>'' then cast(replace(miengiam,',','') as numeric) else 0 end) as miengiam,userid from bill where billdate between '" + tungay + "' and '" + denngay + "' " + _listuserid + " and loaiphieuthuid='" + loaiphieuthuid + "') b LEFT JOIN nhompersonnel ngthu ON ngthu.userhisid=b.userid group by b.billgroupcode,b.userid,ngthu.username) O ;";

                    this.dataDanhSachSo = condb.GetDataTable_HIS(sql_getdata);

                    if (this.dataDanhSachSo != null && this.dataDanhSachSo.Rows.Count > 0)
                    {
                        gridControlDSHoaDon.DataSource = this.dataDanhSachSo;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Xuat bao cao va In an
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSHoaDon.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);

                    ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                    reportitem_tientong.name = "TONGTIEN_THU_STRING";
                    reportitem_tientong.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(TinhTongTienThucThu(), 0).ToString());
                    thongTinThem.Add(reportitem_tientong);

                    string fileTemplatePath = "BC_BangKeTongHopHoaDon.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataDanhSachSo);
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
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                reportitem_tientong.name = "TONGTIEN_THU_STRING";
                reportitem_tientong.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(TinhTongTienThucThu(), 0).ToString());
                thongTinThem.Add(reportitem_tientong);

                string fileTemplatePath = "BC_BangKeTongHopHoaDon.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataDanhSachSo);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();

        }

        private decimal TinhTongTienThucThu()
        {
            decimal result = 0;
            try
            {
                if (this.dataDanhSachSo != null && this.dataDanhSachSo.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dataDanhSachSo.Rows.Count; i++)
                    {
                        result += Utilities.Util_TypeConvertParse.ToDecimal(this.dataDanhSachSo.Rows[i]["tongtien_thu"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }
        #endregion

        #region Custom
        private void gridViewDSHoaDon_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void chkTatCa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTatCa.Checked)
                {
                    chkcomboListNguoiThu.Enabled = false;
                }
                else
                { chkcomboListNguoiThu.Enabled = true; }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
    }
}
