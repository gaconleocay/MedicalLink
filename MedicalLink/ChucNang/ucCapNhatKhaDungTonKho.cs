using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using System.IO;
using MedicalLink.Base;
using DevExpress.XtraSplashScreen;


namespace MedicalLink.ChucNang
{
    public partial class ucCapNhatKhaDungTonKho : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private long tickCurrentVal = 0;
        long thoiGianCapNhat;

        public ucCapNhatKhaDungTonKho()
        {
            InitializeComponent();

        }

        // hien thi mau cua cell row khi click
        private void gridViewLogUpdateKDTK_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        #region Su kien check radio
        private void radioCheDoThongThuong_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChayDung.Checked == true)
                {
                    chkChayDung.Checked = false;
                    timerThoiGianConLai.Stop();
                    lblThoiGianConLai.Text = "Đã tắt chế độ tự động cập nhật";
                    chkChayDung.Text = "Chạy";
                    chkChayDung.ForeColor = Color.Blue;
                    chkChayDung.Image = imageCollectionIcon.Images[0];
                }
                else
                {
                    if (radioCheDoThongThuong.Checked == true)
                    {
                        spinEditCheDoThuong.Enabled = true;
                        spinEditCheDoThuong.Value = 20; // set giá trị mặc định ở chế độ đơn giản

                        radioCheDoThongMinh.Checked = false;
                        spinEditKhoang1.Enabled = false;
                        spinEditKhoang2.Enabled = false;
                        spinEditKhoang3.Enabled = false;
                        spinEditKhoang4.Enabled = false;

                        lblCheDoChay.Text = "Đang cài đặt ở chế độ đơn giản";
                        thoiGianCapNhat = Convert.ToInt64(spinEditCheDoThuong.Value.ToString()) * 60;
                        lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";

                    }
                }

            }
            catch (Exception)
            {

            }
        }

        private void radioCheDoThongMinh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChayDung.Checked == true)
                {
                    chkChayDung.Checked = false;
                    timerThoiGianConLai.Stop();
                    lblThoiGianConLai.Text = "Đã tắt chế độ tự động cập nhật";
                    chkChayDung.Text = "Chạy";
                    chkChayDung.ForeColor = Color.Blue;
                    chkChayDung.Image = imageCollectionIcon.Images[0];
                }
                else
                {
                    long datetime = Convert.ToInt64(DateTime.Now.ToString("HHmmss"));

                    if (radioCheDoThongMinh.Checked == true)
                    {
                        spinEditKhoang1.Enabled = true;
                        spinEditKhoang2.Enabled = true;
                        spinEditKhoang3.Enabled = true;
                        spinEditKhoang4.Enabled = true;
                        spinEditKhoang1.Value = 10;// set giá trị mặc định ở chế độ nâng cao
                        spinEditKhoang2.Value = 30;
                        spinEditKhoang3.Value = 10;
                        spinEditKhoang4.Value = 30;

                        spinEditCheDoThuong.Enabled = false;
                        radioCheDoThongThuong.Checked = false;
                        lblCheDoChay.Text = "Đang cài đặt ở chế độ nâng cao";

                        if ((datetime >= 0 && datetime < 070000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang4.Value.ToString()) * 60;
                            lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";

                        }
                        else if ((datetime >= 070001 && datetime < 120000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang1.Value.ToString()) * 60;
                            lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";

                        }
                        else if ((datetime >= 120001 && datetime < 133000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang2.Value.ToString()) * 60;
                            lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";

                        }
                        else if ((datetime >= 133001 && datetime < 173000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang3.Value.ToString()) * 60;
                            lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";

                        }
                        else if ((datetime >= 173001 && datetime < 235959))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang4.Value.ToString()) * 60;
                            lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";

                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Load
        private void ucCapNhatKhaDungTonKho_Load(object sender, EventArgs e)
        {
            try
            {
                radioCheDoThongThuong.Checked = true;
                spinEditCheDoThuong.Enabled = true;
                spinEditCheDoThuong.Value = 20; // set giá trị mặc định ở chế độ đơn giản

                radioCheDoThongMinh.Checked = false;
                spinEditKhoang1.Enabled = false;
                spinEditKhoang2.Enabled = false;
                spinEditKhoang3.Enabled = false;
                spinEditKhoang4.Enabled = false;
                timerClock.Start();

                DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                dtTuNgay.Value = tuNgay;
                dtDenNgay.Value = denNgay;


            }
            catch (Exception)
            {

            }
        }
        #endregion

        private void spinEditCheDoThuong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lblCheDoChay.Text = "Đang cài đặt ở chế độ đơn giản";
                thoiGianCapNhat = Convert.ToInt64(spinEditCheDoThuong.Value.ToString()) * 60;
                lblThoiGianConLai.Text = "Tự động cập nhật sau " + thoiGianCapNhat.ToString() + " giây";
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblThoiGianHienTai.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        #region Button Chạy
        private void btnChay_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkChayDung.Checked == true)
                {
                    //tickCurrentVal = ;
                    if (radioCheDoThongThuong.Checked == true) // chay o che do thong thuong
                    {
                        thoiGianCapNhat = Convert.ToInt64(spinEditCheDoThuong.Value.ToString()) * 60;
                        tickCurrentVal = thoiGianCapNhat;
                        lblCheDoChay.Text = "Đang chạy ở chế độ đơn giản";
                    }
                    else if (radioCheDoThongMinh.Checked == true)
                    {
                        lblCheDoChay.Text = "Đang chạy ở chế độ nâng cao";
                        long datetime = Convert.ToInt64(DateTime.Now.ToString("HHmmss"));
                        if ((datetime >= 0 && datetime < 070000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang4.Value.ToString()) * 60;
                            tickCurrentVal = thoiGianCapNhat;
                        }
                        else if ((datetime >= 070001 && datetime < 120000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang1.Value.ToString()) * 60;
                            tickCurrentVal = thoiGianCapNhat;
                        }
                        else if ((datetime >= 120001 && datetime < 133000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang2.Value.ToString()) * 60;
                            tickCurrentVal = thoiGianCapNhat;
                        }
                        else if ((datetime >= 133001 && datetime < 173000))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang3.Value.ToString()) * 60;
                            tickCurrentVal = thoiGianCapNhat;
                        }
                        else if ((datetime >= 173001 && datetime < 235959))
                        {
                            thoiGianCapNhat = Convert.ToInt64(spinEditKhoang4.Value.ToString()) * 60;
                            tickCurrentVal = thoiGianCapNhat;
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        //TODO
                    }
                    timerThoiGianConLai.Enabled = true;                       // Enable the timer
                    timerThoiGianConLai.Start();
                    chkChayDung.Image = null;
                    chkChayDung.ForeColor = Color.Blue;

                }
                else
                {
                    timerThoiGianConLai.Stop();
                    lblThoiGianConLai.Text = "Đã tắt chế độ tự động cập nhật";
                    chkChayDung.Text = "Chạy";
                    chkChayDung.ForeColor = Color.Blue;
                    chkChayDung.Image = imageCollectionIcon.Images[0];
                    if (radioCheDoThongThuong.Checked == true)
                    {
                        lblCheDoChay.Text = "Đang cài đặt ở chế độ đơn giản";
                    }
                    else
                    {
                        lblCheDoChay.Text = "Đang cài đặt ở chế độ nâng cao";
                    }

                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region time
        private void timerThoiGianConLai_Tick(object sender, EventArgs e)
        {
            try
            {
                chkChayDung.Text = "Dừng lại";
                chkChayDung.ForeColor = Color.Black;
                lblThoiGianConLai.Text = "Tự động cập nhật sau " + tickCurrentVal + " giây";
                tickCurrentVal--;
                chkChayDung.Image = imageCollectionIcon.Images[1];

                if (tickCurrentVal == 0)
                {
                    btnChay_CheckedChanged(null, null);
                    tickCurrentVal = thoiGianCapNhat;
                    try
                    {
                        //Chay lenh SQL update"
                        string sqlkiemtra = "select medicinestorerefid, soluongtonkho, soluongkhadung from medicine_store_ref where soluongkhadung>soluongtonkho;";
                        DataView dv_kiemtra = new DataView(condb.GetDataTable_HIS(sqlkiemtra));
                        //gridControlSuaThoiGianRaVien.DataSource = dv;
                        if (dv_kiemtra.Count > 0)
                        {
                            // Luu lai vao DB
                            String dthientai = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string sql_insertlog_update = "INSERT INTO tools_tbllog_updatekhadung(tgcapnhat, khothuoc_id, kho_id, kho_ma, kho_ten, thuoc_id, thuoc_ma, thuoc_ten, thuoc_dvt, slkhadung, sltonkho, gianhap, giaban, loguser) SELECT '" + dthientai + "' as tgcapnhat, thuoc.*, '" + SessionLogin.SessionUsercode + "' as loguser FROM dblink('myconn','SELECT msf.medicinestorerefid as khothuoc_id, msf.medicinestoreid as kho_id, ms.medicinestorecode as kho_ma, ms.medicinestorename as kho_ten, msf.medicinerefid as thuoc_id, me.medicinecode as thuoc_ma, me.medicinename as thuoc_ten, me.donvitinh as thuoc_dvt, msf.soluongkhadung as slkhadung, msf.soluongtonkho as sltonkho, me.gianhap as gianhap, me.giaban as giaban FROM medicine_store_ref msf inner join medicine_ref me on me.medicinerefid=msf.medicinerefid inner join medicine_store ms on ms.medicinestoreid=msf.medicinestoreid WHERE msf.soluongkhadung > msf.soluongtonkho') AS thuoc(khothuoc_id integer,kho_id integer,kho_ma text,kho_ten text,thuoc_id integer,thuoc_ma text,thuoc_ten text,thuoc_dvt text,slkhadung double precision,sltonkho double precision,gianhap double precision,giaban double precision); ";
                            condb.ExecuteNonQuery_MeLToHIS(sql_insertlog_update);
                            // Update KD, TK
                            string sqlupdatekdtk = "UPDATE medicine_store_ref SET soluongkhadung = soluongtonkho WHERE soluongkhadung > soluongtonkho;";
                            condb.ExecuteNonQuery_HIS(sqlupdatekdtk);

                        }
                    }
                    catch (Exception ex)
                    {
                        O2S_Common.Logging.LogSystem.Warn(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region btn Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string datetungay = "";
                string datedenngay = "";
                // Lấy từ ngày, đến ngày
                datetungay = DateTime.ParseExact(dtTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                datedenngay = DateTime.ParseExact(dtDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string sqltimkiem = "SELECT *,slkhadung - sltonkho as slcapnhat_display FROM tools_tbllog_updatekhadung WHERE tgcapnhat > '" + datetungay + "' and tgcapnhat <'" + datedenngay + "';";
                DataView dv_timkiem = new DataView(condb.GetDataTable_MeL(sqltimkiem));
                gridControlLogUpdateKDTK.DataSource = dv_timkiem;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        // Đánh số thứ tự dòng
        private void gridViewLogUpdateKDTK_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column == stt)
            {
                e.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        #region Export data
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewLogUpdateKDTK.RowCount > 0)
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
                                    gridViewLogUpdateKDTK.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridViewLogUpdateKDTK.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridViewLogUpdateKDTK.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridViewLogUpdateKDTK.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridViewLogUpdateKDTK.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridViewLogUpdateKDTK.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion


    }
}
