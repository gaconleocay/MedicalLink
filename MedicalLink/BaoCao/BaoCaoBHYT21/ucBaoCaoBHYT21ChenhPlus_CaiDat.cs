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
    public partial class ucBCBHYT21Chenh : UserControl
    {
        string worksheetName = "DVKTTuongDuong";      // Tên Sheet 
        internal DevExpress.XtraGrid.GridControl gridControlDichVu_Import;
        internal DevExpress.XtraGrid.Views.Grid.GridView gridViewDichVu_Import;
        internal DataView dvDichVu_Import;

        private void btnThemTuFileExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogSelect.FileName;
                    ReadExcelFile _excel = new ReadExcelFile(openFileDialogSelect.FileName);
                    var data = _excel.GetDataTable("SELECT DVKT_CODE, MA_DV_DCG, TEN_DV_DCG, MA_DV_BHYT_DCG, GIA_DV_DCG, MA_DV_TUONGDUONG, MA_DV_MOI, TEN_DV_MOI, MA_DV_BHYT_MOI, GIA_DV_MOI FROM [" + worksheetName + "$]");
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
                                string sql_kt = "SELECT MaDDVKT_CODE FROM tools_dvktbhytchenh WHERE MaDDVKT_CODE='" + dvDichVu_Import[i]["DVKT_CODE"] + "';";
                                DataView dv_kt = new DataView(condb.GetDataTable_MeL(sql_kt));
                                if (dv_kt.Count > 0) //update
                                {
                                    string sql_updateDVKT = "UPDATE tools_dvktbhytchenh SET MaDVKT_Cu='" + dvDichVu_Import[i]["MA_DV_DCG"] + "', TenDVKT_Cu='" + dvDichVu_Import[i]["TEN_DV_DCG"] + "', MaDVKTBHYT_Cu='" + dvDichVu_Import[i]["MA_DV_BHYT_DCG"] + "', DonGia_Cu='" + (dvDichVu_Import[i]["GIA_DV_DCG"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_DCG"]) + "', MaDVKT_TuongDuong='" + dvDichVu_Import[i]["MA_DV_TUONGDUONG"] + "', MaDVKT_Moi='" + dvDichVu_Import[i]["MA_DV_MOI"] + "', TenDVKT_Moi='" + dvDichVu_Import[i]["TEN_DV_MOI"] + "', MaDVKTBHYT_Moi='" + dvDichVu_Import[i]["MA_DV_BHYT_MOI"] + "', DonGia_Moi='" + (dvDichVu_Import[i]["GIA_DV_MOI"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_MOI"]) + "' WHERE MaDDVKT_CODE='" + dvDichVu_Import[i]["DVKT_CODE"] + "';";
                                    try
                                    {
                                        condb.ExecuteNonQuery_MeL(sql_updateDVKT);
                                        dem_update += 1;
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }

                                }
                                else
                                {
                                    string sql_insertDVKT = "INSERT INTO tools_dvktbhytchenh(MaDDVKT_CODE, MaDVKT_Cu, TenDVKT_Cu, MaDVKTBHYT_Cu, DonGia_Cu, MaDVKT_TuongDuong, MaDVKT_Moi, TenDVKT_Moi, MaDVKTBHYT_Moi, DonGia_Moi, is_lock) VALUES ('" + dvDichVu_Import[i]["DVKT_CODE"] + "', '" + dvDichVu_Import[i]["MA_DV_DCG"] + "','" + dvDichVu_Import[i]["TEN_DV_DCG"] + "','" + dvDichVu_Import[i]["MA_DV_BHYT_DCG"] + "','" + (dvDichVu_Import[i]["GIA_DV_DCG"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_DCG"]) + "','" + dvDichVu_Import[i]["MA_DV_TUONGDUONG"] + "','" + dvDichVu_Import[i]["MA_DV_MOI"] + "','" + dvDichVu_Import[i]["TEN_DV_MOI"] + "','" + dvDichVu_Import[i]["MA_DV_BHYT_MOI"] + "','" + (dvDichVu_Import[i]["GIA_DV_MOI"].ToString() == "" ? "0" : dvDichVu_Import[i]["GIA_DV_MOI"]) + "','0');";
                                    try
                                    {
                                        condb.ExecuteNonQuery_MeL(sql_insertDVKT);
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
            catch (Exception)
            {
            }
        }
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
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaDV_Moi37.Text.Trim() != "" && txtMaDV_Cu.Text.Trim() != "")
                {
                    string sql_dv_cu = "SELECT servicepricecode as dv_ma, ServicePriceCodeUser as dv_matuongduong, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn,servicepricename as dv_tenyc, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicepricecode ='" + txtMaDV_Moi37.Text.Trim() + "'";
                    DataView data_tk_cu = new DataView(condb.GetDataTable_HIS(sql_dv_cu));
                    string sql_dv_moi = "SELECT servicepricecode as dv_ma, ServicePriceCodeUser as dv_matuongduong, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn,servicepricename as dv_tenyc, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicepricecode ='" + txtMaDV_Cu.Text.Trim() + "'";
                    DataView data_tk_moi = new DataView(condb.GetDataTable_HIS(sql_dv_moi));

                    //Kiem tra ma dịch vụ đã tồn tại trong DB hay chưa?
                    condb.Connect();
                    string sql_kt = "SELECT MaDDVKT_CODE FROM tools_dvktbhytchenh WHERE MaDVKT_Cu='" + txtMaDV_Moi37.Text.Trim() + "';";
                    DataView dv_kt = new DataView(condb.GetDataTable_MeL(sql_kt));
                    if (dv_kt.Count > 0) //update
                    {
                        try
                        {
                            if (data_tk_cu.Count > 0 && data_tk_moi.Count > 0)
                            {
                                string sql_updateDVKT = "UPDATE tools_dvktbhytchenh SET TenDVKT_Cu='" + data_tk_cu[0]["dv_tenbhyt"].ToString() + "', MaDVKTBHYT_Cu='" + data_tk_cu[0]["dv_matuongduong"].ToString() + "', DonGia_Cu='" + data_tk_cu[0]["gia_bhyt"].ToString() + "', MaDVKT_TuongDuong='" + data_tk_cu[0]["dv_matuongduong"].ToString() + "', MaDVKT_Moi='" + data_tk_moi[0]["dv_ma"].ToString() + "', TenDVKT_Moi='" + data_tk_moi[0]["dv_tenbhyt"].ToString() + "', MaDVKTBHYT_Moi='" + data_tk_moi[0]["dv_matuongduong"].ToString() + "', DonGia_Moi='" + data_tk_moi[0]["gia_bhyt"].ToString() + "' WHERE MaDVKT_Cu='" + txtMaDV_Moi37.Text.Trim() + "';";
                                condb.ExecuteNonQuery_MeL(sql_updateDVKT);
                            }

                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                            frmthongbao.Show();
                            LoadDanhMucDichVuGanMaTuongDuong();
                        }
                        catch (Exception)
                        { }

                    }
                    else//insert
                    {
                        try
                        {
                            if (data_tk_cu.Count > 0 && data_tk_moi.Count > 0)
                            {                               
                                string dv_dv_insert_code = "";
                                dv_dv_insert_code = DateTime.Now.ToString("yyyyMMddHHmmss");

                                string sql_insertDVKT = "INSERT INTO tools_dvktbhytchenh(MaDDVKT_CODE, MaDVKT_Cu, TenDVKT_Cu, MaDVKTBHYT_Cu, DonGia_Cu, MaDVKT_TuongDuong, MaDVKT_Moi, TenDVKT_Moi, MaDVKTBHYT_Moi, DonGia_Moi, is_lock) VALUES ('" + dv_dv_insert_code + "', '" + data_tk_cu[0]["dv_ma"].ToString() + "','" + data_tk_cu[0]["dv_tenbhyt"].ToString() + "','" + data_tk_cu[0]["dv_matuongduong"].ToString() + "','" + data_tk_cu[0]["gia_bhyt"].ToString() + "','" + data_tk_cu[0]["dv_matuongduong"].ToString() + "','" + data_tk_moi[0]["dv_ma"].ToString() + "','" + data_tk_moi[0]["dv_tenbhyt"].ToString() + "','" + data_tk_moi[0]["dv_matuongduong"].ToString() + "','" + data_tk_moi[0]["gia_bhyt"].ToString() + "','0');";
                                condb.ExecuteNonQuery_MeL(sql_insertDVKT);

                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                                frmthongbao.Show();
                                LoadDanhMucDichVuGanMaTuongDuong();
                            }
                        }
                        catch (Exception)
                        { }
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSGanMa_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        #region Dich vu TT37
        private void txtMaDV_Moi37_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string strValue = (sender as DevExpress.XtraEditors.TextEdit).Text.ToUpper();
                    LoadData_DichVu_TheoMa(txtMaDV_Moi37.Text.Trim(), cbbTenDV_Moi37, lblThongTinDVKT_TT37);
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbbTenDV_Moi37_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    cbbTenDV_Moi37.Properties.Buttons[1].Visible = false;
                    cbbTenDV_Moi37.EditValue = null;
                    txtMaDV_Moi37.Text = "";
                    lblThongTinDVKT_TT37.Text = "";
                }
            }
            catch (Exception)
            {

            }
        }

        private void cbbTenDV_Moi37_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cbbTenDV_Moi37.EditValue != null)
                    {
                        txtMaDV_Moi37.Text = cbbTenDV_Moi37.EditValue.ToString();
                        cbbTenDV_Moi37.Properties.Buttons[1].Visible = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbbTenDV_Moi37_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == PopupCloseMode.Normal)
                {
                    if (cbbTenDV_Moi37.EditValue != null)
                    {
                        txtMaDV_Moi37.Text = cbbTenDV_Moi37.EditValue.ToString();
                        cbbTenDV_Moi37.Properties.Buttons[1].Visible = true;
                        LoadData_DichVu_TheoMa(txtMaDV_Moi37.Text.Trim(), cbbTenDV_Moi37, lblThongTinDVKT_TT37);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Dich vu cu
        private void txtMaDV_Cu_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string strValue = (sender as DevExpress.XtraEditors.TextEdit).Text.ToUpper();
                    LoadData_DichVu_TheoMa(txtMaDV_Cu.Text.Trim(), cbbTenDV_Cu, lblThongTinDVKT_Cu);
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbbTenDV_Cu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    cbbTenDV_Cu.Properties.Buttons[1].Visible = false;
                    cbbTenDV_Cu.EditValue = null;
                    txtMaDV_Cu.Text = "";
                    lblThongTinDVKT_Cu.Text = "";
                }
            }
            catch (Exception)
            {

            }
        }

        private void cbbTenDV_Cu_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (e.CloseMode == PopupCloseMode.Normal)
                {
                    if (cbbTenDV_Cu.EditValue != null)
                    {
                        txtMaDV_Cu.Text = cbbTenDV_Cu.EditValue.ToString();
                        cbbTenDV_Cu.Properties.Buttons[1].Visible = true;
                        LoadData_DichVu_TheoMa(txtMaDV_Cu.Text.Trim(), cbbTenDV_Cu, lblThongTinDVKT_Cu);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbbTenDV_Cu_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cbbTenDV_Cu.EditValue != null)
                    {
                        txtMaDV_Cu.Text = cbbTenDV_Cu.EditValue.ToString();
                        cbbTenDV_Cu.Properties.Buttons[1].Visible = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void LoadData_DichVu_TheoMa(string dv_ma_timkiem, DevExpress.XtraEditors.SearchLookUpEdit cbbTenDV, DevExpress.XtraEditors.LabelControl lblThongTinDVKT)
        {
            try
            {
                string sql_dv = "SELECT servicepricecode as dv_ma, ServicePriceCodeUser as dv_matuongduong, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn,servicepricename as dv_tenyc, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicepricecode ='" + dv_ma_timkiem + "'";
                DataView data_tk = new DataView(condb.GetDataTable_HIS(sql_dv));
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
            catch (Exception)
            {
            }
        }
        private void LoadDanhMucDichVu()
        {
            try
            {
                //Load danh muc dich vu TT37
                string sqldsdv = "SELECT servicepricecode as dv_ma, ServicePriceCodeUser as dv_matuongduong, servicepricenamebhyt as dv_tenbhyt, servicepricenamenhandan as dv_tenvp, servicepricenamenuocngoai as dv_tennnn, servicepricefeebhyt as gia_bhyt, servicepricefeenhandan as gia_vp, servicepricefee as gia_yc, servicepricefeenuocngoai as gia_nnn FROM servicepriceref WHERE servicelock=0 and isremove is null and servicepricegroupcode <> '' ORDER BY servicepricegroupcode;";
                DataView dv_dmdv = new DataView(condb.GetDataTable_HIS(sqldsdv));

                cbbTenDV_Moi37.Properties.DataSource = dv_dmdv;
                cbbTenDV_Moi37.Properties.DisplayMember = "dv_tenbhyt";
                cbbTenDV_Moi37.Properties.ValueMember = "dv_ma";

                cbbTenDV_Cu.Properties.DataSource = dv_dmdv;
                cbbTenDV_Cu.Properties.DisplayMember = "dv_tenbhyt";
                cbbTenDV_Cu.Properties.ValueMember = "dv_ma";
            }
            catch (Exception)
            {
            }
        }
        private void LoadDanhMucDichVuGanMaTuongDuong()
        {
            try
            {
                string sql_laydanhsach = "SELECT maddvkt_code as maddvkt_code, madvkt_cu as madvkt_dcg, tendvkt_cu as tendvkt_dcg, madvktbhyt_cu as madvktbhyt_dcg, dongia_cu as dongia_dcg, madvkt_tuongduong as madvkt_tuongduong, madvkt_moi as madvkt_moi, tendvkt_moi as tendvkt_moi, madvktbhyt_moi as madvktbhyt_moi, dongia_moi as dongia_moi FROM tools_dvktbhytchenh;";
                DataView dv_dmdvkt = new DataView(condb.GetDataTable_MeL(sql_laydanhsach));
                if (dv_dmdvkt != null && dv_dmdvkt.Count > 0)
                {
                    for (int i = 0; i < dv_dmdvkt.Count; i++)
                    {
                        classDMDVKTBHYTChenh dmdvktbhyt = new classDMDVKTBHYTChenh();
                        dmdvktbhyt.MaDVKT_Code = dv_dmdvkt[i]["maddvkt_code"].ToString();
                        dmdvktbhyt.MaDVKT_Cu = dv_dmdvkt[i]["madvkt_dcg"].ToString();
                        dmdvktbhyt.TenDVKT_Cu = dv_dmdvkt[i]["tendvkt_dcg"].ToString();
                        dmdvktbhyt.MaDVKTBHYT_Cu = dv_dmdvkt[i]["madvktbhyt_dcg"].ToString();
                        dmdvktbhyt.DonGia_Cu = Convert.ToDecimal(dv_dmdvkt[i]["dongia_dcg"].ToString());
                        dmdvktbhyt.MaDVKT_TuongDuong = dv_dmdvkt[i]["madvkt_tuongduong"].ToString();
                        dmdvktbhyt.MaDVKT_Moi = dv_dmdvkt[i]["madvkt_moi"].ToString();
                        dmdvktbhyt.TenDVKT_Moi = dv_dmdvkt[i]["tendvkt_moi"].ToString();
                        dmdvktbhyt.MaDVKTBHYT_Moi = dv_dmdvkt[i]["madvktbhyt_moi"].ToString();
                        dmdvktbhyt.DonGia_Moi = Convert.ToDecimal(dv_dmdvkt[i]["dongia_moi"].ToString());

                        lstDVKTBHYTChenh.Add(dmdvktbhyt);
                    }
                    gridControlDSGanMa.DataSource = null;
                    gridControlDSGanMa.DataSource = dv_dmdvkt;
                }

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadDanhSachKhoa()
        {
            try
            {
                string sql_lstKhoa = "SELECT departmentgroupid,departmentgroupcode,departmentgroupname,departmentgrouptype FROM departmentgroup WHERE departmentgrouptype in(4,11,1) ORDER BY departmentgroupname;";
                DataView lstDSKhoa = new DataView(condb.GetDataTable_HIS(sql_lstKhoa));
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
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
