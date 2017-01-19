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

namespace MedicalLink.ChucNang
{
    public partial class ucTimThuocDVSaiMa : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //DataView dv_bhytgroup;
        public ucTimThuocDVSaiMa()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlDichVu.DataSource = null;
                string datetungay = "";
                string datedenngay = "";
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    string sql_timkiem = "SELECT serviceprice.servicepriceid, serviceprice.medicalrecordid, vienphi.patientid,  serviceprice.vienphiid, serviceprice.hosobenhanid, serviceprice.maubenhphamid,tools_depatment.departmentgroupname, tools_depatment.departmentname, serviceprice.servicepricecode, serviceprice.servicepricename, serviceprice.servicepricename_bhyt,serviceprice.servicepricemoney,serviceprice.servicepricemoney_nhandan,serviceprice.servicepricemoney_bhyt, serviceprice.servicepricemoney_nuocngoai, serviceprice.soluong,serviceprice.servicepricedate FROM serviceprice, vienphi,tools_depatment WHERE vienphi.vienphiid = serviceprice.vienphiid and serviceprice.departmentid=tools_depatment.departmentid and serviceprice.servicepricedate > '" + datetungay + "' and serviceprice.servicepricedate < '" + datedenngay + "' and serviceprice.servicepricecode not in (SELECT tools_servicefull.service_code FROM tools_servicefull)";
                    DataView dv = new DataView(condb.getDataTable(sql_timkiem));
                    if (dv != null && dv.Count > 0)
                    {
                        gridControlDichVu.DataSource = dv;
                    }
                    else
                    {
                        gridControlDichVu.DataSource = null;
                    }

                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm();
            }



        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateTuNgay.Value = date_tu;
            DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            dateDenNgay.Value = date_den;
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
                            timerThongBao.Start();
                            lblThongBao.Visible = true;
                            lblThongBao.Text = "Export dữ liệu thành công!";
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
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Không có dữ liệu!";
            }
        }

        private void gridViewDichVu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
