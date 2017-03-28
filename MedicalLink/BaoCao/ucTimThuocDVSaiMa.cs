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

namespace MedicalLink.ChucNang
{
    public partial class ucTimThuocDVSaiMa : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucTimThuocDVSaiMa()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                gridControlDichVu.DataSource = null;
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    string sql_timkiem = "SELECT row_number() over() as stt, serv.servicepriceid, serv.medicalrecordid, vp.patientid, serv.vienphiid, serv.hosobenhanid, serv.maubenhphamid, de.departmentgroupname, de.departmentname, serv.servicepricecode, serv.servicepricename, serv.servicepricename_bhyt, serv.servicepricemoney, serv.servicepricemoney_nhandan, serv.servicepricemoney_bhyt, serv.servicepricemoney_nuocngoai, serv.soluong,serv.servicepricedate FROM (SELECT ser.servicepriceid, ser.medicalrecordid, ser.vienphiid, ser.hosobenhanid, ser.maubenhphamid, ser.departmentid, ser.servicepricecode, ser.servicepricename, ser.servicepricename_bhyt, ser.servicepricemoney, ser.servicepricemoney_nhandan, ser.servicepricemoney_bhyt, ser.servicepricemoney_nuocngoai, ser.soluong, ser.servicepricedate FROM serviceprice ser WHERE ser.servicepricedate >= '" + datetungay + "' and ser.servicepricedate <= '" + datedenngay + "' and ser.servicepricecode not in (SELECT serf.service_code FROM tools_servicefull serf)) serv inner join vienphi vp on vp.vienphiid=serv.vienphiid inner join tools_depatment de on de.departmentid=serv.departmentid and de.departmenttype in (2,3,9);  ";
                    DataView dv = new DataView(condb.getDataTable(sql_timkiem));
                    if (dv != null && dv.Count > 0)
                    {
                        gridControlDichVu.DataSource = dv;
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlDichVu.DataSource = null;
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDichVu.RowCount > 0)
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
                                    gridViewDichVu.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridViewDichVu.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridViewDichVu.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridViewDichVu.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridViewDichVu.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridViewDichVu.ExportToMht(exportFilePath);
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

        private void gridViewDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

    }
}
