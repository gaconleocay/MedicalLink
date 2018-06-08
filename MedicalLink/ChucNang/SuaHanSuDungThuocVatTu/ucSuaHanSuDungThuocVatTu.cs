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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using System.Globalization;
using MedicalLink.Utilities;
using MedicalLink.Base;
using Aspose.Cells;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaHanSuDungThuocVatTu : UserControl
    {
        #region Khai bao
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable DataBaoCao { get; set; }
        private List<ClassCommon.DanhMucThuocVatTuDTO> lstDanhMucThuocVatTu { get; set; }

        #endregion
        public ucSuaHanSuDungThuocVatTu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucSuaHanSuDungThuocVatTu_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachKhoThuoc();
                btnLuuLai.Enabled = false;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachKhoThuoc()
        {
            try
            {
                string _sqlKhoThuoc = @"select medicinestoreid,medicinestorename from medicine_store where medicinestoretype in (1,3,2,7) order by medicinestorename;";
                DataTable _dataDSKhoThuoc = condb.GetDataTable_HIS(_sqlKhoThuoc);

                cboKhoThuocVT.Properties.DataSource = _dataDSKhoThuoc;
                cboKhoThuocVT.Properties.DisplayMember = "medicinestorename";
                cboKhoThuocVT.Properties.ValueMember = "medicinestoreid";
                if (_dataDSKhoThuoc.Rows.Count == 1)
                {
                    cboKhoThuocVT.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Tim Kiem va Hien Thi
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboKhoThuocVT.EditValue == null)
            {
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHO_THUOC);
                frmthongbao.Show();
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                try
                {
                    string medicinekiemkeid = " ";
                    string sql_getThuoc = "";
                    string sql_getkiemke = "select COALESCE(max(medicinekiemkeid),0) as medicinekiemkeid from medicinekiemke where medicinestoreid=" + cboKhoThuocVT.EditValue + " and medicinekiemkestatus=0;";
                    DataView dataKiemKe = new DataView(condb.GetDataTable_HIS(sql_getkiemke));
                    if (dataKiemKe != null && dataKiemKe.Count > 0 && Utilities.Util_TypeConvertParse.ToInt64(dataKiemKe[0]["medicinekiemkeid"].ToString()) > 0)
                    {
                        medicinekiemkeid = " and medicinekiemkeid=" + dataKiemKe[0]["medicinekiemkeid"] + " ";
                    }
                    sql_getThuoc = "SELECT row_number() OVER (order by me.medicinegroupcode,me.medicinename,me.medicinecode) as stt, me.medicinerefid, me.medicinerefid_org, me.medicinegroupcode, me.medicinecode, me.medicinename, me.donvitinh, me.servicepricefeebhyt, msref.soluongtonkho, msref.soluongkhadung, me.hansudung, me.solo, me.hoatchat, me.sodangky FROM (select medicinerefid,medicinerefid_org,medicinegroupcode,medicinecode,medicinename,donvitinh,hansudung,solo,servicepricefeebhyt,hoatchat,sodangky from medicine_ref) me inner join (select medicinerefid,soluongtonkho,soluongkhadung from medicine_store_ref where medicinestoreid=" + cboKhoThuocVT.EditValue + " and (soluongtonkho>0 or soluongkhadung>0) and medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + ") msref on me.medicinerefid=msref.medicinerefid;";

                    this.DataBaoCao = condb.GetDataTable_HIS(sql_getThuoc);
                    if (this.DataBaoCao != null && this.DataBaoCao.Rows.Count > 0)
                    {
                        gridControlThuocTuTruc.DataSource = this.DataBaoCao;
                    }
                    else
                    {
                        gridControlThuocTuTruc.DataSource = null;
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
        }

        #endregion

        #region Events
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(ThongBao.WaitForm1));
            try
            {
                int _update_count = 0;
                foreach (var item_dv in this.lstDanhMucThuocVatTu)
                {
                    if (item_dv.stt != null && item_dv.hansudung!=null)
                    {
                        string _sqlupdate = @"UPDATE medicine_ref SET hansudung='" + item_dv.hansudung.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE medicinecode='" + item_dv.medicinecode + "'; UPDATE medicine_store_ref SET hansudung = '" + item_dv.hansudung.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE medicinerefid = '" + item_dv.medicinerefid + "';";
                        try
                        {
                            if (condb.ExecuteNonQuery_HIS(_sqlupdate))
                            {
                                _update_count += 1;
                                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update Hạn sử dụng thuốc/vật tư mã " + item_dv.medicinecode + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_26');";
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                            }
                        }
                        catch (Exception ex)
                        {
                            MedicalLink.Base.Logging.Error("Loi update Han su dung thuoc/vat tu " + _sqlupdate);
                            continue;
                        }
                    }
                }
                MessageBox.Show("Cập nhật [" + _update_count + "] danh mục thuốc/vật tư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
            btnTimKiem_Click(null, null);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    this.lstDanhMucThuocVatTu = new List<ClassCommon.DanhMucThuocVatTuDTO>();
                    gridControlThuocTuTruc.DataSource = null;
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets["DMThuocVatTu"];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(3, 0, worksheet.Cells.MaxDataRow -2, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null)
                    {
                        this.lstDanhMucThuocVatTu = Utilities.Util_DataTable.DataTableToList<ClassCommon.DanhMucThuocVatTuDTO>(data_Excel);
                        gridControlThuocTuTruc.DataSource = this.lstDanhMucThuocVatTu;
                        btnLuuLai.Enabled = true;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                        btnLuuLai.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                frmthongbao.Show();
                btnLuuLai.Enabled = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DataBaoCao != null)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "0_ToolsCapNhatHanSuDungThuocVatTu_Export.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.DataBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewThuocTuTruc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        #endregion

    }
}
