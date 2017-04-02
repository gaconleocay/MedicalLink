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
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BCPhauThuatThuThuat : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        DataTable dataBCPTTT { get; set; }

        #endregion

        #region Load
        public BCPhauThuatThuThuat()
        {
            InitializeComponent();
        }

        private void BCPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucKhoa();
            LoadDanhSachBaoCao();
        }

        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    cboKhoa.Properties.DataSource = lstDSKhoa;
                    cboKhoa.Properties.DisplayMember = "departmentgroupname";
                    cboKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    cboKhoa.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachBaoCao()
        {
            try
            {
                //List<ClassCommon.classPermission> lstReportCurrent = new List<classPermission>();
                //List<ClassCommon.classPermission> lstReport = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 10).ToList();
                //if (Base.SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                //{
                //    lstReportCurrent = lstReport;
                //}
                //else
                //{
                //    if (Base.SessionLogin.SessionLstPhanQuyenNguoiDung != null && Base.SessionLogin.SessionLstPhanQuyenNguoiDung.Count > 0)
                //    {
                //        lstReportCurrent = (from lstKho in lstReport
                //                            from lstKhoa in Base.SessionLogin.SessionLstPhanQuyenNguoiDung
                //                            where lstKho.permissioncode == lstKhoa.permissioncode
                //                            select new ClassCommon.classPermission
                //                                   {
                //                                       permissionid = lstKho.permissionid,
                //                                       permissioncode = lstKho.permissioncode,
                //                                       en_permissioncode = lstKho.en_permissioncode,
                //                                       permissionname = lstKho.permissionname,
                //                                       en_permissionname = lstKho.en_permissionname,
                //                                       permissiontype = lstKho.permissiontype,
                //                                       permissionnote = lstKho.permissionnote
                //                                   }).ToList();
                //    }
                //}

                cboLoaiBaoCao.Properties.DataSource = Base.SessionLogin.SessionLstPhanQuyen_BaoCao;
                cboLoaiBaoCao.Properties.DisplayMember = "permissionname";
                cboLoaiBaoCao.Properties.ValueMember = "permissioncode";
                if (Base.SessionLogin.SessionLstPhanQuyen_BaoCao.Count > 0)
                {
                    cboLoaiBaoCao.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiBaoCao.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_LOAI_BAO_CAO);
                    frmthongbao.Show();
                    return;
                }
                if (cboLoaiBaoCao.EditValue == "BAOCAO_005" || cboLoaiBaoCao.EditValue == "BAOCAO_008")
                {
                    if (cboKhoa.EditValue == null)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                    if (chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count==0)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                        frmthongbao.Show();
                        return;
                    }
                }
                gridControlDataBCPTTT.DataSource = null;
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void bandedGridViewDataBNNT_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        private void cboLoaiBaoCao_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                cboKhoa.EditValue = null;

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001") //gay me
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002") //tai mui hong
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003")//rang ham mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004")//mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = true;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005")//khoa khac    ------
                {
                    cboKhoa.Enabled = true;
                    chkcomboListDSPhong.Enabled = true;
                    bandedGridColumn_tyle.Visible = true;
                    gridBand_gayme.Visible = true;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = true;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = true;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = true;
                    gridBand_MoiGayMe.Visible = true;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")//thu thuat - mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = false;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//thua thuat - tru mat
                {
                    cboKhoa.Enabled = false;
                    chkcomboListDSPhong.Enabled = false;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//thu thuat khac    ---------
                {
                    cboKhoa.Enabled = true;
                    chkcomboListDSPhong.Enabled = true;
                    bandedGridColumn_tyle.Visible = false;
                    gridBand_gayme.Visible = false;
                    gridBand_phumo1.Visible = true;
                    gridBand_phumo2.Visible = false;
                    gridBand_giupviec1.Visible = true;
                    gridBand_giupviec2.Visible = false;
                    gridBand_PhuMe.Visible = false;
                    gridBand_MoiMoChinh.Visible = false;
                    gridBand_MoiGayMe.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = cboKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                if (cboLoaiBaoCao.EditValue == "BAOCAO_001") //gay me
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_GMHT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_002") //tai mui hong
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TMH.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_003")//rang ham mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_RHM.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_004")//mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_005")//khoa khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_CHUNG.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_006")//thu thuat - mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_MAT.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_007")//thua thuat - tru mat
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_KHOAKHAC.xlsx";
                }
                else if (cboLoaiBaoCao.EditValue == "BAOCAO_008")//thu thuat khac
                {
                    fileTemplatePath = "BC_PhauThuatThuThuat_TT_CHUNG.xlsx";
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void cboKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListDSPhong.Properties.Items.Clear();
                if (cboKhoa.EditValue != null)
                {
                    //Load danh muc phong thuoc khoa
                    var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt64(cboKhoa.EditValue.ToString())).OrderBy(o=>o.departmentname).ToList();
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
               
                    chkcomboListDSPhong.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


    }
}
