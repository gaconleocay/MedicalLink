using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using DevExpress.XtraSplashScreen;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace MedicalLink.ChucNang
{
    public partial class ucBCThongKeTheoICD10 : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        int kieuXem; // 0=xem chi tiết; 1=xem tổng hợp

        public ucBCThongKeTheoICD10()
        {
            InitializeComponent();
        }

        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            gridControlDSDV.DataSource = null;
            gridControlDSDV_TH.DataSource = null;
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            string trangthaiVP = "";
            string loaivienphiid = "";
            string doituongbenhnhanid = "";
            string datetungay = "";
            string datedenngay = "";
            string[] dsIcd_temp;
            string dsIcd = "";
            string tktheomaIdc = "";
            string tktheomaIdc_Phu = "";
            string sql_timtheomaIcd = "";

            if ((cbbTrangThaiVP.Text == "") || (cbbLoaiBA.Text == ""))
            {
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                frmthongbao.Show();
            }
            else
            {
                // Lấy Tiêu chí trạng thai vien phi: vienphistatus
                if (cbbTrangThaiVP.Text.Trim() == "Đóng BA nhưng chưa duyệt VP")
                {
                    trangthaiVP = " and vp.vienphistatus<>0 and COALESCE(vp.vienphistatus_vp)=0 ";                                 
                    //and vp.vienphidate_ravien != '0001-01-01 00:00:00' and (vp.vienphistatus_vp IS NULL or vp.vienphistatus_vp=0) and vp.vienphistatus<>2 ";
                }
                else if (cbbTrangThaiVP.Text.Trim() == "Đã duyệt viện phí")
                {
                    trangthaiVP = " and vp.vienphistatus_vp=1 ";
                }
                else if (cbbTrangThaiVP.Text.Trim() == "Đã đóng BA")
                {
                    trangthaiVP = " and vp.vienphistatus<>0 ";
                }

                // Lấy loaivienphiid
                if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    loaivienphiid = "and vp.loaivienphiid=1 ";
                }
                else if (cbbLoaiBA.Text == "Nội trú")
                {
                    loaivienphiid = "and vp.loaivienphiid=0 ";
                }
                else
                {
                    loaivienphiid = " ";
                }

                // Lấy trường đối tượng BN: loaidoituong
                if (cbbDoiTuong.Text == "BHYT")
                {
                    doituongbenhnhanid = "and vp.doituongbenhnhanid=1 ";
                }
                else if (cbbDoiTuong.Text == "Viện phí")
                {
                    doituongbenhnhanid = "and vp.doituongbenhnhanid<>1 ";
                }
                else if (cbbDoiTuong.Text == "Tất cả")
                {
                    doituongbenhnhanid = " ";
                }

                // Lấy danh sách mã ICD10 nhập vào.
                txtMaICD.Text = txtMaICD.Text.ToUpper();
                if (txtMaICD.Text != "")
                {
                    dsIcd_temp = txtMaICD.Text.Split(',');
                    for (int i = 0; i < dsIcd_temp.Length - 1; i++)
                    {
                        dsIcd += "'" + dsIcd_temp[i].ToString() + "',";
                    }
                    dsIcd += "'" + dsIcd_temp[dsIcd_temp.Length - 1].ToString() + "'";

                    tktheomaIdc = " vp.chandoanravien_code in (" + dsIcd + ")";
                    tktheomaIdc_Phu = "or vp.chandoanravien_kemtheo_code in (" + dsIcd + ")";
                }
                if (tktheomaIdc != "" || tktheomaIdc_Phu !="")
                {
                    sql_timtheomaIcd = " and (" + tktheomaIdc + tktheomaIdc_Phu+")";
                }

                // Lấy từ ngày, đến ngày
                datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                // */ Lấy xong dữ liệu các trường chọn

                // Thực thi câu lệnh SQL
                try
                {
                    // nếu xem chi tiết:
                    if (kieuXem == 0)
                    {
                        gridControlDSDV_TH.DataSource = null;
                        string sqlquerry_ct = "SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY vp.chandoanravien_code) as stt, vp.chandoanravien_code as benhchinh_ma, vp.chandoanravien as benhchinh_ten, vp.chandoanravien_kemtheo_code as benhkemtheo_ma, vp.chandoanravien_kemtheo as benhkemtheo_ten, vp.patientid as mabn, vp.vienphiid as mavp,hsba.patientname as tenbn, degp.departmentgroupname as khoarv, de.departmentname as phongrv, vp.vienphidate as tgvaovien, vp.vienphidate_ravien as tgravien FROM vienphi vp inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join department de on de.departmentid=vp.departmentid inner join departmentgroup degp on degp.departmentgroupid=vp.departmentgroupid WHERE vp.vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + trangthaiVP + sql_timtheomaIcd + ";";

                        DataView dv = new DataView(condb.GetDataTable_HIS(sqlquerry_ct));
                        gridControlDSDV.DataSource = dv;

                        if (gridViewDSDV.RowCount == 0)
                        {
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                            frmthongbao.Show();
                        }
                    }
                    else // xem tổng hợp
                    {
                        gridControlDSDV.DataSource = null;
                        string sqlquerry_th = "SELECT ROW_NUMBER() OVER (ORDER BY vp.chandoanravien_code) as stt, vp.vienphiid as mavp, vp.chandoanravien_code as benhchinh_ma, vp.chandoanravien as benhchinh_ten, vp.chandoanravien_kemtheo_code as benhkemtheo_ma, vp.chandoanravien_kemtheo as benhkemtheo_ten FROM vienphi vp WHERE vp.vienphidate_ravien > '" + datetungay + "' and vp.vienphidate_ravien < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + trangthaiVP + " ;";
                        DataView dv_chitiet = new DataView(condb.GetDataTable_HIS(sqlquerry_th));

                        string sqlquerry_mabenh_1 = "SELECT DISTINCT '' as stt, vp.chandoanravien_code as benhchinh_ma, icd10.icd10name as benhchinh_ten, '' as soluong_chinh, '' as soluong_kt, '' as soluong_tong, '' as soluong_kkb, '' as soluong_nt FROM vienphi vp, icd10 WHERE icd10.icd10code = vp.chandoanravien_code and vp.vienphidate_ravien > '" + datetungay + "' and vp.vienphidate_ravien < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + trangthaiVP + " ORDER BY benhchinh_ma ;";
                        DataView dv_mabenh_1 = new DataView(condb.GetDataTable_HIS(sqlquerry_mabenh_1));

                        string sqlquerry_mabenh_2 = "SELECT DISTINCT '' as stt, vp.chandoanravien_kemtheo_code as benhkemtheo_ma, icd10.icd10name as benhchinh_ten, '' as soluong_chinh, '' as soluong_kt, '' as soluong_tong, '' as soluong_kkb, '' as soluong_nt FROM vienphi vp, icd10 WHERE icd10.icd10code = vp.chandoanravien_kemtheo_code and chandoanravien_kemtheo_code <>'' and vp.vienphidate_ravien > '" + datetungay + "' and vp.vienphidate_ravien < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + trangthaiVP + " ORDER BY benhkemtheo_ma;";
                        DataView dv_mabenh_2 = new DataView(condb.GetDataTable_HIS(sqlquerry_mabenh_2));

                        // SUM tong cua benh chinh
                        if (dv_mabenh_1.Count > 0)
                        {
                            for (int i = 0; i < dv_mabenh_1.Count; i++)
                            {
                                int dem = 0;
                                for (int j = 0; j < dv_chitiet.Count; j++)
                                {
                                    if (dv_mabenh_1[i]["benhchinh_ma"].ToString() == dv_chitiet[j]["benhchinh_ma"].ToString())
                                    {
                                        dem = dem + 1;
                                    }
                                }
                                dv_mabenh_1[i]["stt"] = i + 1;
                                dv_mabenh_1[i]["soluong_chinh"] = dem.ToString();

                            }
                            // hien thi
                            //gridControlDSDV_TH.DataSource = dv_mabenh_1;
                        }
                        else
                        {
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                            frmthongbao.Show();
                        }
                        // SUM tong ma benh kem theo
                        if (dv_mabenh_2.Count > 0)
                        {
                            for (int i = 0; i < dv_mabenh_2.Count; i++)
                            {
                                int dem = 0;
                                for (int j = 0; j < dv_chitiet.Count; j++)
                                {
                                    if (dv_mabenh_2[i]["benhkemtheo_ma"].ToString() == dv_chitiet[j]["benhkemtheo_ma"].ToString())
                                    {
                                        dem += 1;
                                    }
                                }
                                dv_mabenh_2[i]["stt"] = i + 1;
                                dv_mabenh_2[i]["soluong_kt"] = dem.ToString();
                            }
                            // hien thi
                            //gridControlDSDV_TH.DataSource = dv_mabenh_2;
                        }

                        // Tong hop cua 2 dataview: 
                        if (dv_mabenh_1.Count > 0 && dv_mabenh_2.Count > 0)
                        {
                            for (int i = 0; i < dv_mabenh_1.Count; i++)
                            {
                                int dem = 0;
                                for (int j = 0; j < dv_mabenh_2.Count; j++)
                                {
                                    if (dv_mabenh_1[i]["benhchinh_ma"].ToString() == dv_mabenh_2[j]["benhkemtheo_ma"].ToString())
                                    {
                                        dem += Convert.ToInt16(dv_mabenh_2[j]["soluong_kt"].ToString());
                                    }
                                }

                                /*
                                 // Add row của bệnh kèm theo (nếu bệnh kèm theo chưa có trong Dataview)
                                if (dem==0)
                                {
                                    DataRowView rowView = dv_mabenh_1.AddNew();
                                    rowView["stt"] = dv_mabenh_1.Count + 2;
                                    rowView["benhchinh_ma"] = dv_mabenh_2[j]["benhkemtheo_ma"].ToString();
                                    rowView["benhchinh_ten"] = 39450;
                                    rowView["soluong_chinh"] = "BMW";
                                    rowView["soluong_kt"] = "530i";
                                    rowView["soluong_tong"] = 39450;
                                    rowView["soluong_kkb"] = "530i";
                                    rowView["soluong_nt"] = 39450;
                                    rowView.EndEdit();
                                }
                                 * */
                                dv_mabenh_1[i]["soluong_kt"] = dem.ToString();
                            }
                            // Tong hop ma benh chinh va ma benh kem theo
                            for (int i = 0; i < dv_mabenh_1.Count; i++)
                            {
                                dv_mabenh_1[i]["soluong_tong"] = Convert.ToInt64(dv_mabenh_1[i]["soluong_chinh"].ToString()) + Convert.ToInt64(dv_mabenh_1[i]["soluong_kt"].ToString());
                            }

                            // hien thi
                            gridControlDSDV_TH.DataSource = dv_mabenh_1;
                        }

                    }
                }
                catch (Exception ex)
                {
                     O2S_Common.Logging.LogSystem.Error(ex);
                }
            }
            SplashScreenManager.CloseForm();
        }

        private void ucBCDSBNSDdv_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            kieuXem = 0;
            // Ẩn layoutControl tong hop
            layoutControlChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlTongHop.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDSDV.RowCount > 0)
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
                                    gridControlDSDV.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDSDV.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDSDV.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDSDV.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDSDV.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDSDV.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
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
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                frmthongbao.Show();
            }
            //
            if (gridViewDSDV_TH.RowCount > 0)
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
                                    gridControlDSDV_TH.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDSDV_TH.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDSDV_TH.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDSDV_TH.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDSDV_TH.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDSDV_TH.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
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
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                frmthongbao.Show();
            }

        }

        private void radioKieuXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            if (edit.SelectedIndex == 0) // xem chi tiet
            {
                kieuXem = 0;
                // Ẩn layoutControl tong hop
                layoutControlChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlTongHop.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtMaICD.Enabled = true;
                lblTKIcd.Enabled = true;
            }

            else // xem tong hop
            {
                kieuXem = 1;
                // Ẩn layoutControl chi tiet
                layoutControlTongHop.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtMaICD.Enabled = false;
                lblTKIcd.Enabled = false;
                txtMaICD.Text = "";
            }
        }

        private void gridViewDSDV_TH_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }
        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void lblTKIcd_MouseHover(object sender, EventArgs e)
        {
            //lblTKIcd.Text = "Nhập mã ICD10 cách nhau bởi dấu phẩy (,)";
        }



    }
}
