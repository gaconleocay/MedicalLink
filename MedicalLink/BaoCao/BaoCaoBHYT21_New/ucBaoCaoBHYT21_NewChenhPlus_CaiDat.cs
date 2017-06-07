using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MedicalLink.ClassCommon;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucBaoCaoBHYT21_NewChenh : UserControl
    {
        string worksheetName = "DVKTTuongDuong";      // Tên Sheet  //Tools_DMDVChenh_New
        //internal DevExpress.XtraGrid.GridControl gridControlDichVu_Import;
        //internal DevExpress.XtraGrid.Views.Grid.GridView gridViewDichVu_Import;
        internal DataView dvDichVu_Import;

        //Them tu excel
        private void btnThemTuFileExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogSelect.FileName;
                    ReadExcelFile _excel = new ReadExcelFile(openFileDialogSelect.FileName);
                    var data = _excel.GetDataTable("SELECT DVKT_CODE, TEN_DVKT, DVKT_GIA, MA_DV_BHYT_CU, GIA_DV_CU_1, MA_DV_BHYT_MOI, GIA_DV_MOI FROM [" + worksheetName + "$]");
                    if (data != null)
                    {
                        int dem_update = 0;
                        int dem_insert = 0;
                        //gridViewDichVu_Import.DataSource = data;
                        dvDichVu_Import = new DataView(data);

                        for (int i = 0; i < dvDichVu_Import.Count; i++)
                        {
                            if (dvDichVu_Import[i]["DVKT_CODE"].ToString() != "")
                            {
                                condb.Connect();
                                string sql_kt = "SELECT servicecode FROM tools_dvktbhytchenh_new WHERE servicecode='" + dvDichVu_Import[i]["DVKT_CODE"] + "';";
                                DataView dv_kt = new DataView(condb.GetDataTable(sql_kt));
                                if (dv_kt.Count > 0) //update
                                {
                                    string sql_updateDVKT = "UPDATE tools_dvktbhytchenh_new SET dvkt_ten='" + dvDichVu_Import[i]["TEN_DVKT"] + "', dongia_hientai='" + (dvDichVu_Import[i]["DVKT_GIA"].ToString() == "" ? "0" : dvDichVu_Import[i]["DVKT_GIA"]) + "', dvkt_code_cu='" + dvDichVu_Import[i]["MA_DV_BHYT_CU"] + "', dongia_cu_1='" + (dvDichVu_Import[i]["GIA_DV_CU_1"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_CU_1"]) + "', dvkt_code_moi='" + dvDichVu_Import[i]["MA_DV_BHYT_MOI"] + "', dongia_moi_2='" + (dvDichVu_Import[i]["GIA_DV_MOI"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_MOI"]) + "' WHERE servicecode='" + dvDichVu_Import[i]["DVKT_CODE"] + "';";
                                    try
                                    {
                                        condb.ExecuteNonQuery(sql_updateDVKT);
                                        dem_update += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }

                                }
                                else
                                {
                                    string sql_insertDVKT = "INSERT INTO tools_dvktbhytchenh_new(servicecode, dvkt_ten, dongia_hientai, dvkt_code_cu, dongia_cu_1, dvkt_code_moi, dongia_moi_2) VALUES ('" + dvDichVu_Import[i]["DVKT_CODE"] + "', '" + dvDichVu_Import[i]["TEN_DVKT"] + "', '" + (dvDichVu_Import[i]["DVKT_GIA"].ToString() == "" ? "0" : dvDichVu_Import[i]["DVKT_GIA"]) + "', '" + dvDichVu_Import[i]["MA_DV_BHYT_CU"] + "', '" + (dvDichVu_Import[i]["GIA_DV_CU_1"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_CU_1"]) + "', '" + dvDichVu_Import[i]["MA_DV_BHYT_MOI"] + "', '" + (dvDichVu_Import[i]["GIA_DV_MOI"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_MOI"]) + "');";
                                    try
                                    {
                                        condb.ExecuteNonQuery(sql_insertDVKT);
                                        dem_insert += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Thêm mới [ " + dem_insert + " ] & cập nhật [ " + dem_update + " ] dịch vụ thành công.");
                        LoadDanhMucDichVuGanMaTuongDuong();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Xuat exxcel
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSGanMa.RowCount > 0)
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
                                        gridControlDSGanMa.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridControlDSGanMa.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridControlDSGanMa.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridControlDSGanMa.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridControlDSGanMa.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridControlDSGanMa.ExportToMht(exportFilePath);
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
            catch (Exception)
            {
            }
        }
        private void gridViewDSGanMa_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        private void LoadData_DichVu_TheoMa(string dv_ma_timkiem, DevExpress.XtraEditors.SearchLookUpEdit cbbTenDV, DevExpress.XtraEditors.LabelControl lblThongTinDVKT)
        {
            try
            {
                string sql_dv = "SELECT servicepricecode as dv_ma, ServicePriceCodeUser as dv_matuongduong, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn,servicepricename as dv_tenyc, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicepricecode ='" + dv_ma_timkiem + "'";
                DataView data_tk = new DataView(condb.GetDataTable(sql_dv));
                if (data_tk.Count > 0)
                {
                    cbbTenDV.EditValue = data_tk[0]["dv_ma"].ToString();
                    lblThongTinDVKT.Text = "Mã tương đương: " + data_tk[0]["dv_matuongduong"].ToString() + " - Giá BHYT: " + data_tk[0]["gia_bhyt"].ToString();
                }
                else
                {
                    cbbTenDV.EditValue = null;
                    lblThongTinDVKT.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadDanhMucDichVuGanMaTuongDuong()
        {
            try
            {
                lstDVKTBHYTChenh = new List<ClassCommon.classDMDVKTBHYTChenhNew>();
                string sql_laydanhsach = "SELECT servicecode, dvkt_ten, dongia_hientai, dvkt_code_cu, dongia_cu_1, dvkt_code_moi, dongia_moi_2 FROM tools_dvktbhytchenh_new;";
                DataView dv_dmdvkt = new DataView(condb.GetDataTable(sql_laydanhsach));
                if (dv_dmdvkt != null && dv_dmdvkt.Count > 0)
                {
                    for (int i = 0; i < dv_dmdvkt.Count; i++)
                    {
                        classDMDVKTBHYTChenhNew dmdvktbhyt = new classDMDVKTBHYTChenhNew();
                        dmdvktbhyt.stt = (i + 1);
                        dmdvktbhyt.servicecode = dv_dmdvkt[i]["servicecode"].ToString();
                        dmdvktbhyt.dvkt_code_cu = dv_dmdvkt[i]["dvkt_code_cu"].ToString();
                        dmdvktbhyt.dvkt_code_moi = dv_dmdvkt[i]["dvkt_code_moi"].ToString();
                        dmdvktbhyt.dvkt_ten = dv_dmdvkt[i]["dvkt_ten"].ToString();
                        dmdvktbhyt.dongia_cu_1 = Convert.ToDecimal(dv_dmdvkt[i]["dongia_cu_1"].ToString());
                        dmdvktbhyt.dongia_hientai = Convert.ToDecimal(dv_dmdvkt[i]["dongia_hientai"].ToString());
                        dmdvktbhyt.dongia_moi_2 = Convert.ToDecimal(dv_dmdvkt[i]["dongia_moi_2"].ToString());

                        lstDVKTBHYTChenh.Add(dmdvktbhyt);
                    }
                    gridControlDSGanMa.DataSource = null;
                    gridControlDSGanMa.DataSource = lstDVKTBHYTChenh;
                }

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Khoa
        private void LoadDanhSachKhoa()
        {
            try
            {
                string sql_lstKhoa = "SELECT departmentgroupid,departmentgroupcode,departmentgroupname,departmentgrouptype FROM departmentgroup WHERE departmentgrouptype in(4,11,1) ORDER BY departmentgroupname;";
                DataView lstDSKhoa = new DataView(condb.GetDataTable(sql_lstKhoa));
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    //chkListKhoa.DataSource = lstDSKhoa;
                    //chkListKhoa.DisplayMember = "departmentgroupname";
                    //chkListKhoa.ValueMember = "departmentgroupcode";
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count; i++)
                {
                    chkcomboListDSKhoa.Properties.Items[i].CheckState = CheckState.Checked;
                }
            }
            catch (Exception)
            {
            }
        }

        private void CheckDSKhoa()
        {
            try
            {
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count; i++)
                {
                    chkcomboListDSKhoa.Properties.Items[i].CheckState = CheckState.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

    }
}
