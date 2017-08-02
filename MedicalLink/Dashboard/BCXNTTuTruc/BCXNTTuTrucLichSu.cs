using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.BCXNTTuTruc
{
    public partial class BCXNTTuTrucLichSu : Form
    {
        private long medicinestoreid { get; set; }
        private long medicinerefid_org { get; set; }
        private List<ClassCommon.classMedicineRef> lstMedicineStore { get; set; }
        List<ClassCommon.classMedicineRef> lstMedicineCurrent { get; set; }
        private string lstmedicineref_string = "";

        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public BCXNTTuTrucLichSu()
        {
            InitializeComponent();
        }
        public BCXNTTuTrucLichSu(long medicinestoreid, long medicinerefid_org, List<ClassCommon.classMedicineRef> lstMedicineStore)
        {
            InitializeComponent();
            this.medicinestoreid = medicinestoreid;
            this.medicinerefid_org = medicinerefid_org;
            this.lstMedicineStore = lstMedicineStore;
        }

        private void BCXNTTuTrucLichSu_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                GetDanhSachCacLoThuocCon();

                btnTimKiem_Click(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void GetDanhSachCacLoThuocCon()
        {
            try
            {
                if (this.medicinerefid_org != 0)
                {
                    this.lstMedicineCurrent = this.lstMedicineStore.Where(o => o.medicinerefid_org == this.medicinerefid_org).ToList();
                    if (lstMedicineCurrent != null && lstMedicineCurrent.Count > 0)
                    {
                        for (int i = 0; i < lstMedicineCurrent.Count - 1; i++)
                        {
                            this.lstmedicineref_string += lstMedicineCurrent[i].medicinerefid.ToString() + ",";
                        }
                        this.lstmedicineref_string += lstMedicineCurrent[lstMedicineCurrent.Count - 1].medicinerefid.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewBNDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewBCXNTTTLichSu.RowCount > 0)
                {
                    try
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                            if (saveDialog.ShowDialog() != DialogResult.Cancel)
                            {
                                string exportFilePath = saveDialog.FileName;
                                string fileExtenstion = new FileInfo(exportFilePath).Extension;

                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        gridViewBCXNTTTLichSu.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewBCXNTTTLichSu.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewBCXNTTTLichSu.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewBCXNTTTLichSu.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewBCXNTTTLichSu.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewBCXNTTTLichSu.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridViewBNDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string sqlgetdata = "SELECT me.medicinerefid, me.medicinedate, me.medicinestorebillid, me.medicinestorebillcode, (case me.medicinestorebilltype when 204 then 'Xuất đơn' when 217 then 'Nhập trả (BS)' when 2 then 'Nhập (kho)' else '' end) as medicinestorebilltype, me.medicinestorebillstatus, me.medicinestorebillremark, me.medicinestoreid, me.accept_soluong, me.accept_money + ((me.accept_money*me.accept_vat)/100) as accept_money, me.solo, me.sodangky, me.medicinekiemkeid FROM (select medicinerefid,medicinedate,medicinestorebillid,medicinestorebillcode,medicinestorebilltype,medicinestorebillstatus, medicinestorebillremark,medicinestoreid,accept_soluong,accept_money,accept_vat,solo,sodangky,medicinekiemkeid from medicine where isremove=0 and medicinestoreid='" + this.medicinestoreid + "' and medicinerefid in (" + this.lstmedicineref_string + ") and medicinestorebilltype in (2,204,217) and medicinedate>='" + thoiGianTu + "' and medicinedate<='" + thoiGianDen + "') me inner join (select medicinestorebillid from medicine_store_bill where coalesce(isremove,0)=0) ser on ser.medicinestorebillid=me.medicinestorebillid ORDER BY me.medicinedate DESC; ";
                DataView dataResults = new DataView(condb.GetDataTable_HIS(sqlgetdata));
                List<ClassCommon.classLichSuNhapXuatThuoc> lstMedicinestore = new List<ClassCommon.classLichSuNhapXuatThuoc>();
                if (dataResults != null && dataResults.Count > 0)
                {
                    for (int i = 0; i < dataResults.Count; i++)
                    {
                        ClassCommon.classLichSuNhapXuatThuoc medicinestore = new ClassCommon.classLichSuNhapXuatThuoc();
                        medicinestore.stt = i + 1;
                        medicinestore.medicinerefid = Utilities.Util_TypeConvertParse.ToInt64(dataResults[i]["medicinerefid"].ToString());
                        medicinestore.medicinecode = this.lstMedicineCurrent.Where(o => o.medicinerefid == medicinestore.medicinerefid).FirstOrDefault().medicinecode;
                        medicinestore.medicinename = this.lstMedicineCurrent.Where(o => o.medicinerefid == medicinestore.medicinerefid).FirstOrDefault().medicinename;
                        medicinestore.medicinedate = Utilities.Util_TypeConvertParse.ToDateTime(dataResults[i]["medicinedate"].ToString());
                        medicinestore.medicinestorebillcode = dataResults[i]["medicinestorebillcode"].ToString();
                        medicinestore.medicinestorebilltype = dataResults[i]["medicinestorebilltype"].ToString();
                        medicinestore.accept_soluong = Utilities.Util_TypeConvertParse.ToDecimal(dataResults[i]["accept_soluong"].ToString());
                        medicinestore.accept_money = Utilities.Util_TypeConvertParse.ToDecimal(dataResults[i]["accept_money"].ToString());
                        medicinestore.medicinestorebillremark = dataResults[i]["medicinestorebillremark"].ToString();
                        medicinestore.solo = dataResults[i]["solo"].ToString();
                        medicinestore.sodangky = dataResults[i]["sodangky"].ToString();
                        medicinestore.medicinekiemkeid = Utilities.Util_TypeConvertParse.ToInt64(dataResults[i]["medicinekiemkeid"].ToString());
                        lstMedicinestore.Add(medicinestore);
                    }
                }
                gridControlBCXNTTTLichSu.DataSource = lstMedicinestore;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        //SELECT ROW_NUMBER () OVER (ORDER BY me.medicinedate desc) as stt, mef.medicinecode, mef.medicinename, me.medicinedate, me.medicinestorebillid, me.medicinestorebillcode, (case me.medicinestorebilltype when 204 then 'Xuất đơn' when 217 then 'Nhập trả (BS)' when 2 then 'Nhập (kho)' else '' end) as medicinestorebilltype, me.medicinestorebillstatus, me.medicinestorebillremark, me.medicinestoreid, me.accept_soluong, me.accept_money + ((me.accept_money*me.accept_vat)/100) as accept_money, me.solo, me.sodangky, me.medicinekiemkeid FROM medicine me inner join medicine_ref mef on mef.medicinerefid=me.medicinerefid " + datatype_string + " WHERE me.isremove=0 and me.medicinestoreid='" + this.medicinestoreid + "' and mef.medicinerefid_org='" + this.medicinerefid_org + "' and me.medicinestorebilltype in (2,204,217) and me.medicinedate>='" + thoiGianTu + "' and me.medicinedate<='" + thoiGianDen + "' ORDER BY me.medicinedate DESC;
    }
}
