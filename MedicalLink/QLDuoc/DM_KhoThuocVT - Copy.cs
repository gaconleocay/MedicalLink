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
using Aspose.Cells;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;

namespace MedicalLink.QLDuoc
{
    public partial class DM_KhoThuocVT1 : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private List<Model.Models.DanhMucDichVu_ThuocDTO> lstDichVuImport { get; set; }
        public DM_KhoThuocVT1()
        {
            InitializeComponent();
        }

        #region Load
        private void DM_KhoThuocVT1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhMucDichVu();
                EnableAndDisableControl();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhMucDichVu()
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                gridControlDichVu.DataSource = null;
                string sql_getdmdv = "SELECT danhmucthuocid, row_number () over (order by stt) as stt, ma_hoat_chat, ma_ax, hoat_chat, hoatchat_ax, ma_duong_dung, ma_duongdung_ax, duong_dung, duongdung_ax, ham_luong, hamluong_ax, ten_thuoc, tenthuoc_ax, so_dang_ky, sodangky_ax, dong_goi, don_vi_tinh, don_gia, don_gia_tt, so_luong, ma_cskcb, hang_sx, nuoc_sx, nha_thau, quyet_dinh, cong_bo, ma_thuoc_bv, loai_thuoc, loai_thau, nhom_thau, manhom_9324, hieuluc_id, hieuluc, ketqua_id, ketqua, lydotuchoi, is_look, lastuserupdated, lasttimeupdated FROM ie_danhmuc_thuoc; ";
                DataTable dataDMDV = condb.GetDataTable_IE(sql_getdmdv);
                this.lstDichVuImport = new List<Model.Models.DanhMucDichVu_ThuocDTO>();
                if (dataDMDV != null && dataDMDV.Rows.Count > 0)
                {
                    foreach (DataRow row in dataDMDV.Rows)
                    {
                        Model.Models.DanhMucDichVu_ThuocDTO dichVu = new Model.Models.DanhMucDichVu_ThuocDTO();
                        dichVu.danhmucthuocid = O2S_Common.TypeConvert.Parse.ToInt64(row["danhmucthuocid"].ToString());
                        dichVu.STT = O2S_Common.TypeConvert.Parse.ToInt64(row["STT"].ToString());
                        dichVu.MA_HOAT_CHAT = row["MA_HOAT_CHAT"].ToString();
                        dichVu.MA_AX = row["MA_AX"].ToString();
                        dichVu.HOAT_CHAT = row["HOAT_CHAT"].ToString();
                        dichVu.HOATCHAT_AX = row["HOATCHAT_AX"].ToString();
                        dichVu.MA_DUONG_DUNG = row["MA_DUONG_DUNG"].ToString();
                        dichVu.MA_DUONGDUNG_AX = row["MA_DUONGDUNG_AX"].ToString();
                        dichVu.DUONG_DUNG = row["DUONG_DUNG"].ToString();
                        dichVu.DUONGDUNG_AX = row["DUONGDUNG_AX"].ToString();
                        dichVu.HAM_LUONG = row["HAM_LUONG"].ToString();
                        dichVu.HAMLUONG_AX = row["HAMLUONG_AX"].ToString();
                        dichVu.TEN_THUOC = row["TEN_THUOC"].ToString();
                        dichVu.TENTHUOC_AX = row["TENTHUOC_AX"].ToString();
                        dichVu.SO_DANG_KY = row["SO_DANG_KY"].ToString();
                        dichVu.SODANGKY_AX = row["SODANGKY_AX"].ToString();
                        dichVu.DONG_GOI = row["DONG_GOI"].ToString();
                        dichVu.DON_VI_TINH = row["DON_VI_TINH"].ToString();
                        dichVu.DON_GIA = O2S_Common.TypeConvert.Parse.ToDecimal(row["DON_GIA"].ToString());
                        dichVu.DON_GIA_TT = O2S_Common.TypeConvert.Parse.ToDecimal(row["DON_GIA_TT"].ToString());
                        dichVu.SO_LUONG = O2S_Common.TypeConvert.Parse.ToDecimal(row["SO_LUONG"].ToString());
                        dichVu.MA_CSKCB = O2S_Common.TypeConvert.Parse.ToInt64(row["MA_CSKCB"].ToString());
                        dichVu.HANG_SX = row["HANG_SX"].ToString();
                        dichVu.NUOC_SX = row["NUOC_SX"].ToString();
                        dichVu.NHA_THAU = row["NHA_THAU"].ToString();
                        dichVu.QUYET_DINH = row["QUYET_DINH"].ToString();
                        dichVu.CONG_BO = row["CONG_BO"].ToString();
                        dichVu.MA_THUOC_BV = row["MA_THUOC_BV"].ToString();
                        dichVu.LOAI_THUOC = O2S_Common.TypeConvert.Parse.ToInt64(row["LOAI_THUOC"].ToString());
                        dichVu.LOAI_THAU = O2S_Common.TypeConvert.Parse.ToInt64(row["LOAI_THAU"].ToString());
                        dichVu.NHOM_THAU = row["NHOM_THAU"].ToString();
                        dichVu.MANHOM_9324 = O2S_Common.TypeConvert.Parse.ToInt64(row["MANHOM_9324"].ToString());
                        dichVu.HIEULUC = row["HIEULUC"].ToString();
                        dichVu.KETQUA = row["KETQUA"].ToString();
                        dichVu.LYDOTUCHOI = row["LYDOTUCHOI"].ToString();

                        if (dichVu.HIEULUC.ToLower() == "có")
                        {
                            dichVu.HIEULUC_ID = 1;
                        }
                        if (dichVu.KETQUA.ToLower() == "đã phê duyệt")
                        {
                            dichVu.KETQUA_ID = 1;
                        }
                        dichVu.LASTUSERUPDATED = row["LASTUSERUPDATED"].ToString();
                        dichVu.LASTTIMEUPDATED = row["LASTTIMEUPDATED"];

                        this.lstDichVuImport.Add(dichVu);
                    }
                }
                gridControlDichVu.DataSource = this.lstDichVuImport;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void EnableAndDisableControl()
        {
            try
            {
                btnLuuLai.Enabled = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        private void btnNhapTuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    //SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                    this.lstDichVuImport = new List<Model.Models.DanhMucDichVu_ThuocDTO>();
                    gridControlDichVu.DataSource = null;
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets[0];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(0, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null)
                    {
                        foreach (DataRow row in data_Excel.Rows)
                        {
                            Model.Models.DanhMucDichVu_ThuocDTO dichVu = new Model.Models.DanhMucDichVu_ThuocDTO();
                            dichVu.STT = O2S_Common.TypeConvert.Parse.ToInt64(row["STT"].ToString());
                            dichVu.MA_HOAT_CHAT = row["MA_HOAT_CHAT"].ToString().Trim();
                            dichVu.MA_AX = row["MA_AX"].ToString().Trim();
                            dichVu.HOAT_CHAT = row["HOAT_CHAT"].ToString().Trim();
                            dichVu.HOATCHAT_AX = row["HOATCHAT_AX"].ToString().Trim();
                            dichVu.MA_DUONG_DUNG = row["MA_DUONG_DUNG"].ToString().Trim();
                            dichVu.MA_DUONGDUNG_AX = row["MA_DUONGDUNG_AX"].ToString().Trim();
                            dichVu.DUONG_DUNG = row["DUONG_DUNG"].ToString().Trim();
                            dichVu.DUONGDUNG_AX = row["DUONGDUNG_AX"].ToString().Trim();
                            dichVu.HAM_LUONG = row["HAM_LUONG"].ToString().Trim();
                            dichVu.HAMLUONG_AX = row["HAMLUONG_AX"].ToString().Trim();
                            dichVu.TEN_THUOC = row["TEN_THUOC"].ToString().Trim();
                            dichVu.TENTHUOC_AX = row["TENTHUOC_AX"].ToString().Trim();
                            dichVu.SO_DANG_KY = row["SO_DANG_KY"].ToString().Trim();
                            dichVu.SODANGKY_AX = row["SODANGKY_AX"].ToString().Trim();
                            dichVu.DONG_GOI = row["DONG_GOI"].ToString().Trim();
                            dichVu.DON_VI_TINH = row["DON_VI_TINH"].ToString().Trim();
                            dichVu.DON_GIA = O2S_Common.TypeConvert.Parse.ToDecimal(row["DON_GIA"].ToString());
                            dichVu.DON_GIA_TT = O2S_Common.TypeConvert.Parse.ToDecimal(row["DON_GIA_TT"].ToString());
                            dichVu.SO_LUONG = O2S_Common.TypeConvert.Parse.ToDecimal(row["SO_LUONG"].ToString());
                            dichVu.MA_CSKCB = O2S_Common.TypeConvert.Parse.ToInt64(row["MA_CSKCB"].ToString());
                            dichVu.HANG_SX = row["HANG_SX"].ToString().Trim();
                            dichVu.NUOC_SX = row["NUOC_SX"].ToString().Trim();
                            dichVu.NHA_THAU = row["NHA_THAU"].ToString().Trim();
                            dichVu.QUYET_DINH = row["QUYET_DINH"].ToString().Trim();
                            dichVu.CONG_BO = row["CONG_BO"].ToString();
                            dichVu.MA_THUOC_BV = row["MA_THUOC_BV"].ToString();
                            dichVu.LOAI_THUOC = O2S_Common.TypeConvert.Parse.ToInt64(row["LOAI_THUOC"].ToString());
                            dichVu.LOAI_THAU = O2S_Common.TypeConvert.Parse.ToInt64(row["LOAI_THAU"].ToString());
                            dichVu.NHOM_THAU = row["NHOM_THAU"].ToString();
                            dichVu.MANHOM_9324 = O2S_Common.TypeConvert.Parse.ToInt64(row["MANHOM_9324"].ToString());
                            dichVu.HIEULUC = row["HIEULUC"].ToString();
                            dichVu.KETQUA = row["KETQUA"].ToString();
                            dichVu.LYDOTUCHOI = row["LYDOTUCHOI"].ToString();

                            if (dichVu.HIEULUC.ToLower() == "có")
                            {
                                dichVu.HIEULUC_ID = 1;
                            }
                            if (dichVu.KETQUA.ToLower() == "đã phê duyệt")
                            {
                                dichVu.KETQUA_ID = 1;
                            }

                            this.lstDichVuImport.Add(dichVu);
                        }
                        gridControlDichVu.DataSource = this.lstDichVuImport;
                        btnLuuLai.Enabled = true;
                    }
                    else
                    {
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                        btnLuuLai.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                frmthongbao.Show();
                btnLuuLai.Enabled = false;
                //SplashScreenManager.CloseForm();
            }
            //SplashScreenManager.CloseForm();
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                int insert_count = 0;
                int _update_count = 0;
                foreach (var item_dv in this.lstDichVuImport)
                {
                    try
                    {
                        string _sqlkiemtratrung = "SELECT ma_hoat_chat FROM ie_danhmuc_thuoc WHERE ma_hoat_chat='" + item_dv.MA_HOAT_CHAT + "' and ma_ax='" + item_dv.MA_AX + "' and hoat_chat='" + item_dv.HOAT_CHAT.Replace("'", "''") + "' and hoatchat_ax='" + item_dv.HOATCHAT_AX.Replace("'", "''") + "' and ma_duong_dung='" + item_dv.MA_DUONG_DUNG + "' and ma_duongdung_ax='" + item_dv.MA_DUONGDUNG_AX + "' and ham_luong='" + item_dv.HAM_LUONG + "' and hamluong_ax='" + item_dv.HAMLUONG_AX + "' and ten_thuoc='" + item_dv.TEN_THUOC.Replace("'", "''") + "' and tenthuoc_ax='" + item_dv.TENTHUOC_AX.Replace("'", "''") + "' and so_dang_ky='" + item_dv.SO_DANG_KY + "' and dong_goi='" + item_dv.DONG_GOI + "' and don_vi_tinh='" + item_dv.DON_VI_TINH + "' and don_gia='" + item_dv.DON_GIA.ToString().Replace(",", ".") + "' and don_gia_tt='" + item_dv.DON_GIA_TT.ToString().Replace(",", ".") + "' and manhom_9324='" + item_dv.MANHOM_9324 + "' and SODANGKY_AX='" + item_dv.SODANGKY_AX.Replace("'", "''") + "' and HANG_SX='" + item_dv.HANG_SX.Replace("'", "''") + "' and NUOC_SX='" + item_dv.NUOC_SX.Replace("'", "''") + "' and NHA_THAU='" + item_dv.NHA_THAU.Replace("'", "''") + "' and QUYET_DINH='" + item_dv.QUYET_DINH.Replace("'", "''") + "' and CONG_BO='" + item_dv.CONG_BO + "' and MA_THUOC_BV='" + item_dv.MA_THUOC_BV + "' and LOAI_THUOC='" + item_dv.LOAI_THUOC + "' and LOAI_THAU='" + item_dv.LOAI_THAU + "' and NHOM_THAU='" + item_dv.NHOM_THAU + "' ;";
                        DataTable _dataKiemTra = condb.GetDataTable_IE(_sqlkiemtratrung);
                        if (_dataKiemTra != null && _dataKiemTra.Rows.Count > 0)
                        {
                            _update_count += _dataKiemTra.Rows.Count;
                            //Xoa
                            string _sql_delete = "DELETE FROM ie_danhmuc_thuoc WHERE ma_hoat_chat='" + item_dv.MA_HOAT_CHAT + "' and ma_ax='" + item_dv.MA_AX + "' and hoat_chat='" + item_dv.HOAT_CHAT.Replace("'", "''") + "' and hoatchat_ax='" + item_dv.HOATCHAT_AX.Replace("'", "''") + "' and ma_duong_dung='" + item_dv.MA_DUONG_DUNG + "' and ma_duongdung_ax='" + item_dv.MA_DUONGDUNG_AX + "' and ham_luong='" + item_dv.HAM_LUONG + "' and hamluong_ax='" + item_dv.HAMLUONG_AX + "' and ten_thuoc='" + item_dv.TEN_THUOC + "' and tenthuoc_ax='" + item_dv.TENTHUOC_AX + "' and so_dang_ky='" + item_dv.SO_DANG_KY + "' and dong_goi='" + item_dv.DONG_GOI + "' and don_vi_tinh='" + item_dv.DON_VI_TINH + "' and don_gia='" + item_dv.DON_GIA.ToString().Replace(",", ".") + "' and don_gia_tt='" + item_dv.DON_GIA_TT.ToString().Replace(",", ".") + "' and manhom_9324='" + item_dv.MANHOM_9324 + "' and SODANGKY_AX='" + item_dv.SODANGKY_AX.Replace("'", "''") + "' and HANG_SX='" + item_dv.HANG_SX.Replace("'", "''") + "' and NUOC_SX='" + item_dv.NUOC_SX.Replace("'", "''") + "' and NHA_THAU='" + item_dv.NHA_THAU.Replace("'", "''") + "' and QUYET_DINH='" + item_dv.QUYET_DINH.Replace("'", "''") + "' and CONG_BO='" + item_dv.CONG_BO + "' and MA_THUOC_BV='" + item_dv.MA_THUOC_BV + "' and LOAI_THUOC='" + item_dv.LOAI_THUOC + "' and LOAI_THAU='" + item_dv.LOAI_THAU + "' and NHOM_THAU='" + item_dv.NHOM_THAU + "' ; ";
                            condb.ExecuteNonQuery_IE(_sql_delete);
                        }

                        string sql_insert = "INSERT INTO ie_danhmuc_thuoc(stt, ma_hoat_chat, ma_ax, hoat_chat, hoatchat_ax, ma_duong_dung, ma_duongdung_ax, duong_dung, duongdung_ax, ham_luong, hamluong_ax, ten_thuoc, tenthuoc_ax, so_dang_ky, sodangky_ax, dong_goi, don_vi_tinh, don_gia, don_gia_tt, so_luong, ma_cskcb, hang_sx, nuoc_sx, nha_thau, quyet_dinh, cong_bo, ma_thuoc_bv, loai_thuoc, loai_thau, nhom_thau, manhom_9324, hieuluc_id, hieuluc, ketqua_id, ketqua, lydotuchoi, is_look, lastuserupdated, lasttimeupdated) VALUES ('" + item_dv.STT + "', '" + item_dv.MA_HOAT_CHAT + "', '" + item_dv.MA_AX + "', '" + item_dv.HOAT_CHAT.Replace("'", "''") + "', '" + item_dv.HOATCHAT_AX.Replace("'", "''") + "', '" + item_dv.MA_DUONG_DUNG + "', '" + item_dv.MA_DUONGDUNG_AX + "', '" + item_dv.DUONG_DUNG + "', '" + item_dv.DUONGDUNG_AX + "', '" + item_dv.HAM_LUONG + "', '" + item_dv.HAMLUONG_AX + "', '" + item_dv.TEN_THUOC.Replace("'", "''") + "', '" + item_dv.TENTHUOC_AX.Replace("'", "''") + "', '" + item_dv.SO_DANG_KY + "', '" + item_dv.SODANGKY_AX + "', '" + item_dv.DONG_GOI + "', '" + item_dv.DON_VI_TINH + "', '" + item_dv.DON_GIA.ToString().Replace(",", ".") + "', '" + item_dv.DON_GIA_TT.ToString().Replace(",", ".") + "', '" + item_dv.SO_LUONG + "', '" + item_dv.MA_CSKCB + "', '" + item_dv.HANG_SX.Replace("'", "''") + "', '" + item_dv.NUOC_SX.Replace("'", "''") + "', '" + item_dv.NHA_THAU.Replace("'", "''") + "', '" + item_dv.QUYET_DINH + "', '" + item_dv.CONG_BO + "', '" + item_dv.MA_THUOC_BV + "', '" + item_dv.LOAI_THUOC + "', '" + item_dv.LOAI_THAU + "', '" + item_dv.NHOM_THAU + "', '" + item_dv.MANHOM_9324 + "', '" + item_dv.HIEULUC_ID + "', '" + item_dv.HIEULUC + "', '" + item_dv.KETQUA_ID + "', '" + item_dv.KETQUA + "', '" + item_dv.LYDOTUCHOI + "','0', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ); ";
                        if (condb.ExecuteNonQuery_IE(sql_insert))
                        {
                            insert_count += 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        O2S_Common.Logging.LogSystem.Error("Loi insert ie_danhmuc_thuoc " + ex.ToString());
                        continue;
                    }
                }
                MessageBox.Show("Thêm mới [" + (insert_count - _update_count) + "/" + this.lstDichVuImport.Count + "]; cập nhật [" + _update_count + "] thuốc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BUS.CallAPIServerYeuCauCapNhat.LayDuLieuTuCSDL("ie_danhmuc_thuoc");
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
            DM_KhoThuocVT1_Load(null, null);
        }

        #region Custom
        private void gridViewDichVu_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Delete Row
        private void gridViewDichVu_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (!btnLuuLai.Enabled)
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        //GridView view = sender as GridView;
                        e.Menu.Items.Clear();
                        DXMenuItem itemKiemTraDaChon = new DXMenuItem("Xóa thuốc đã chọn"); // caption menu
                        itemKiemTraDaChon.Image = imageCollectionDSBN.Images[0]; // icon cho menu
                        itemKiemTraDaChon.Click += new EventHandler(XoaDichVuDaChon_Click);// thêm sự kiện click
                        e.Menu.Items.Add(itemKiemTraDaChon);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void XoaDichVuDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDichVu.RowCount > 0)
                {
                    string sql_deleteDV = "";
                    foreach (var item_index in gridViewDichVu.GetSelectedRows())
                    {
                        string danhmucthuocid = gridViewDichVu.GetRowCellValue(item_index, "danhmucthuocid").ToString();
                        sql_deleteDV += "DELETE FROM ie_danhmuc_thuoc where danhmucthuocid='" + danhmucthuocid + "'; ";
                    }
                    condb.ExecuteNonQuery_IE(sql_deleteDV);
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(Base.ThongBaoLable.XOA_THANH_CONG);
                    frmthongbao.Show();
                    DM_KhoThuocVT1_Load(null, null);
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
