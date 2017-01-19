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
    public partial class ucKTTTDonThuocNoiTru : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string datetungay = "";
        string datedenngay = "";
        long soluonghoso = 0;
        //DataView dv_bhytgroup;
        public ucKTTTDonThuocNoiTru()
        {
            InitializeComponent();
        }

        // Mở file Excel hiển thị lên DataGridView
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlHSBA.DataSource = null;

                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    string sql_timkiem = "SELECT maubenhpham.maubenhphamid as maubenhphamid, maubenhpham.sophieu as sophieu, vienphi.vienphiid as vienphiid, vienphi.patientid as patientid, hosobenhan.hosobenhanid as hosobenhanid, hosobenhan.patientname as patientname, bhyt.bhytcode as bhytcode, tools_depatment.departmentgroupname as departmentgroupname, tools_depatment.departmentname as departmentname, maubenhpham.maubenhphamdate as maubenhphamdate, vienphi.duyet_ngayduyet_vp as vienphidate, case maubenhpham.maubenhphamgrouptype when 5 then 'Thuốc' else 'Vật tư' end as maubenhphamgrouptype, case maubenhpham.maubenhphamstatus when 1 then 'Đang yêu cầu' else 'Đã tổng hợp y lệnh' end as maubenhphamstatus FROM vienphi INNER JOIN maubenhpham ON vienphi.vienphiid=maubenhpham.vienphiid INNER JOIN tools_depatment ON maubenhpham.departmentid=tools_depatment.departmentid INNER JOIN hosobenhan ON hosobenhan.hosobenhanid=vienphi.hosobenhanid INNER JOIN bhyt ON bhyt.bhytid=vienphi.bhytid INNER JOIN medicine_store ON medicine_store.medicinestoreid=maubenhpham.medicinestoreid WHERE maubenhpham.isloaidonthuoc='0' and maubenhpham.maubenhphamstatus in (1,4) and maubenhpham.maubenhphamgrouptype in (5,6) and medicine_store.medicinestoretype<>4 and vienphi.duyet_ngayduyet_vp > '" + datetungay + "' and vienphi.duyet_ngayduyet_vp < '" + datedenngay + "' ORDER BY vienphi.duyet_ngayduyet_vp;";

                    DataView dv = new DataView(condb.getDataTable(sql_timkiem));
                    if (dv != null && dv.Count > 0)
                    {
                        gridControlHSBA.DataSource = dv;
                        soluonghoso = dv.Count;
                    }
                    else
                    {
                        soluonghoso = 0;
                        gridControlHSBA.DataSource = null;
                        timerThongBao.Start();
                        lblThongBao.Visible = true;
                        lblThongBao.Text = "Không tìm thấy hồ sơ nào như yêu cầu.";
                    }

                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            try
            {
                //Lấy thời gian lấy BC mặc định là ngày hiện tại
                DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateTuNgay.Value = date_tu;
                DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                dateDenNgay.Value = date_den;
                gridControlHSBA.DataSource = null;
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
                if (gridViewHSBA.RowCount > 0)
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
                                        gridViewHSBA.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewHSBA.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewHSBA.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewHSBA.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewHSBA.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewHSBA.ExportToMht(exportFilePath);
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
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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

        private void btnSuaTTHSBA_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                if (dateTuNgay.Text != "" && dateDenNgay.Text != "")
                {
                    string sqlupdate = "UPDATE medicalrecord SET medicalrecordstatus ='99' WHERE medicalrecordid IN (SELECT medicalrecord.medicalrecordid FROM vienphi INNER JOIN medicalrecord ON vienphi.medicalrecordid_end=medicalrecord.medicalrecordid  INNER JOIN tools_depatment ON medicalrecord.departmentid=tools_depatment.departmentid INNER JOIN hosobenhan ON hosobenhan.hosobenhanid=vienphi.hosobenhanid INNER JOIN bhyt ON bhyt.bhytid=vienphi.bhytid  WHERE medicalrecord.medicalrecordstatus <>'99' and vienphi.duyet_ngayduyet_vp > '" + datetungay + "' and vienphi.duyet_ngayduyet_vp < '" + datedenngay + "') ;";
                    condb.ExecuteNonQuery(sqlupdate);

                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Cập nhật thành công [ " + soluonghoso.ToString() + " ] hồ sơ";
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                SplashScreenManager.CloseForm();
            }
        }
    }
}
