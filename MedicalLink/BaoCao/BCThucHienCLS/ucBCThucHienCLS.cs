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

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucBCThucHienCLS : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        DataTable dataBCPTTT { get; set; }
        bool kiemtrasuadulieu = false;
        #endregion

        #region Load
        public ucBCThucHienCLS()
        {
            InitializeComponent();
        }

        private void ucBCThucHienCLS_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucPhongThucHien();
            LoadDanhSachExport();
        }

        private void LoadDanhMucPhongThucHien()
        {
            try
            {
                var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7 || o.departmenttype == 6).OrderBy(o => o.departmentname).ToList();
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                    chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                    chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                }

                chkcomboListDSPhong.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Xuất báo cáo Cận lâm sàng"));
                menu.Items.Add(new DXMenuItem("Xuất báo cáo thanh toán tiền cận lâm sàng"));
                // ... add more items
                dropDownExport.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_Export_Click;
                // setup initial selection
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_THUC_HIEN);
                    frmthongbao.Show();
                    return;
                }
                gridControlDataBCPTTT.DataSource = null;
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
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

        private void bandedGridViewDataBCPTTT_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                kiemtrasuadulieu = true;
                var rowHandle = bandedGridViewDataBCPTTT.FocusedRowHandle;
                long thuchienclsid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "thuchienclsid").ToString());
                long medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                long maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                long patientid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "patientid").ToString());
                long servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                string thuchienclsdate = Utilities.Util_TypeConvertParse.ToDateTime( bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "ngay_thuchien").ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                long mochinh_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "mochinh_idbs").ToString());
                long gayme_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "gayme_idbs").ToString());
                long phu1_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "phu1_idbs").ToString());
                long phu2_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "phu2_idbs").ToString());
                long giupviec1_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "giupviec1_idbs").ToString());
                long giupviec2_idbs = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataBCPTTT.GetRowCellValue(rowHandle, "giupviec2_idbs").ToString());

                if (thuchienclsid == 0) //kiemtra xem co ban ghi nao hay ko?
                {
                    string sqlkiemtra = "select thuchienclsid from thuchiencls where servicepriceid=" + servicepriceid + ";";
                    DataTable dataKiemTra = condb.GetDataTable(sqlkiemtra);
                    if (dataKiemTra != null && dataKiemTra.Rows.Count > 0)
                    {
                        thuchienclsid= Utilities.Util_TypeConvertParse.ToInt64(dataKiemTra.Rows[0]["thuchienclsid"].ToString());
                    }
                }

                string luulaithuchien = "";
                if (thuchienclsid == 0) //them moi
                {
                    luulaithuchien = "INSERT INTO thuchiencls(medicalrecordid, medicalrecordid_gmhs, patientid, maubenhphamid, servicepriceid, thuchienclsdate, phauthuatvien, bacsigayme, phumo1, phumo2, phumo3, phumo4) VALUES ('" + medicalrecordid + "', '" + medicalrecordid + "', '" + patientid + "', '" + maubenhphamid + "', '" + servicepriceid + "', '" + thuchienclsdate + "', '" + mochinh_idbs + "', '" + gayme_idbs + "', '" + phu1_idbs + "', '" + phu2_idbs + "', '" + giupviec1_idbs + "', '" + giupviec2_idbs + "');";
                }
                else
                {
                    luulaithuchien = "UPDATE thuchiencls SET thuchienclsdate='" + thuchienclsdate + "', phauthuatvien='" + mochinh_idbs + "',  bacsigayme = '" + gayme_idbs + "', phumo1 = '" + phu1_idbs + "', phumo2 = '" + phu2_idbs + "', phumo3 = '" + giupviec1_idbs + "', phumo4 = '" + giupviec2_idbs + "' WHERE thuchienclsid = " + thuchienclsid + "; ";
                }

                condb.ExecuteNonQuery(luulaithuchien);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        void Item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Xuất báo cáo Cận lâm sàng")
                {
                    tbnExportBCCLS_Click();
                }
                if (tenbaocao == "Xuất báo cáo thanh toán tiền cận lâm sàng")
                {
                    tbnExportBCThanhToanCLS_Click();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void tbnExportBCCLS_Click()
        {
            try
            {
                if (this.kiemtrasuadulieu)
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi();
                }
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CLS.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void tbnExportBCThanhToanCLS_Click()
        {
            try
            {
                if (this.kiemtrasuadulieu)
                {
                    gridControlDataBCPTTT.DataSource = null;
                    LayDuLieuBaoCao_ChayMoi();
                }
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_ThanhToanCLS.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
