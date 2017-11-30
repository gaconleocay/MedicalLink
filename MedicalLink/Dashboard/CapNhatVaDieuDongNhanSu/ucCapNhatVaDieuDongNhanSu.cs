﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using DevExpress.XtraSplashScreen;
using MedicalLink.Utilities.GridControl;

namespace MedicalLink.Dashboard
{
    public partial class ucCapNhatVaDieuDongNhanSu : UserControl
    {
        #region Declaration
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion

        public ucCapNhatVaDieuDongNhanSu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucCapNhatVaDieuDongNhanSu_Load(object sender, EventArgs e)
        {
            try
            {
                dateNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                LoadDanhSachKhoa();
                LoadDanhSachKhoaDen();
                btnTimKiem.PerformClick();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                //var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11 && o.departmentgroupid != 14).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void LoadDanhSachKhoaDen()
        {
            try
            {
                string _sqlGetDSKhoa = "select departmentgroupid as ddktv_khoadenid,departmentgroupname as ddktv_khoadenname from departmentgroup order by departmentgroupname;";
                DataTable _dataDSKhoa = condb.GetDataTable_HIS(_sqlGetDSKhoa);
                repositoryItemGridLookUp_KhoaDen.DataSource = _dataDSKhoa;
                repositoryItemGridLookUp_KhoaDen.DisplayMember = "ddktv_khoadenname";
                repositoryItemGridLookUp_KhoaDen.ValueMember = "ddktv_khoadenid";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        #region Tim Kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string _thoigian = DateTime.ParseExact(dateNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");

                if (_thoigian == DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_04"))
                    {
                        LoadPhanQuyenChinhSua(true);
                    }
                    else
                    {
                        LoadPhanQuyenChinhSua(false);
                    }
                    if (MedicalLink.Base.CheckPermission.ChkPerModule("SYS_05"))//quan tri he thong
                    {
                        LoadChinhSua(true);
                    }
                }
                else
                {
                    LoadChinhSua(false);
                }


                if (chkcomboListDSKhoa.Properties.Items.GetCheckedValues().Count == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }
                string _dskhoa = " and degp.departmentgroupid in (";
                List<Object> lstPhongCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                {
                    _dskhoa += lstPhongCheck[i] + ",";
                }
                _dskhoa += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";

                string _sqlTimKiem = "SELECT row_number () over (order by degp.loaikhoiid,degp.departmentgroupname) as stt, ns.updatenhansuid, ns.departmentgroupid, ns.giuongthucke, ns.nb_hientai, ns.ns_bienche, ns.ns_hopdong, ns.ns_hocviec, ns.ns_hienco, ns.ns_vang, ns.lydo_om, ns.lydo_phep, ns.lydo_de, ns.lydo_khac, ns.hvsv_hocvien, ns.hvsv_sinhvien, ns.ddktv_soluong, ns.ddktv_khoadenid, ns.ddktv_khoadenname, ns.ddktv_songay, ns.ghichu_khoa, ns.ghichu_dieuduong, ns.nhansudate, degp.loaikhoiid, degp.loaikhoiten, degp.departmentgroupid, degp.departmentgroupname, '0'  as isgroup FROM tools_updatenhansu ns inner join tools_departmentgroup degp on degp.departmentgroupid=ns.departmentgroupid WHERE to_char(ns.nhansudate,'yyyyMMdd')='" + _thoigian + "' and degp.loaikhoiid>0 " + _dskhoa + ";";
                DataTable _dataTimKiem = condb.GetDataTable_MeL(_sqlTimKiem);
                if (_dataTimKiem != null && _dataTimKiem.Rows.Count > 0)
                {
                    gridControlData.DataSource = _dataTimKiem;
                }
                else//tao moi va lay du lieu hien thi
                {
                    gridControlData.DataSource = null;
                    if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_04"))
                    {
                        List<Object> lstKhoaCheck_Insert = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                        for (int i = 0; i < lstKhoaCheck_Insert.Count; i++)
                        {
                            string _sqlInsertNew = "";
                            //tim kiem ban ghi cua ngay truoc do
                            string _nhansu_old = "select departmentgroupid, ns_bienche, ns_hopdong, ns_hocviec, hvsv_hocvien, hvsv_sinhvien from tools_updatenhansu where departmentgroupid='" + lstKhoaCheck_Insert[i] + "' order by nhansudate desc limit 1;";
                            DataTable _dataNhanSu_Old = condb.GetDataTable_MeL(_nhansu_old);
                            if (_dataNhanSu_Old != null && _dataNhanSu_Old.Rows.Count > 0)
                            {
                                _sqlInsertNew = "INSERT INTO tools_updatenhansu(departmentgroupid, ns_bienche, ns_hopdong, ns_hocviec, hvsv_hocvien, hvsv_sinhvien, nhansudate, createdate, createusercode, createusername) VALUES ('" + lstKhoaCheck_Insert[i] + "', '" + _dataNhanSu_Old.Rows[0]["ns_bienche"].ToString() + "', '" + _dataNhanSu_Old.Rows[0]["ns_hopdong"].ToString() + "', '" + _dataNhanSu_Old.Rows[0]["ns_hocviec"].ToString() + "', '" + _dataNhanSu_Old.Rows[0]["hvsv_hocvien"].ToString() + "', '" + _dataNhanSu_Old.Rows[0]["hvsv_sinhvien"].ToString() + "', '" + DateTime.ParseExact(dateNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00" + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "'); ";
                            }
                            else
                            {
                                _sqlInsertNew = "INSERT INTO tools_updatenhansu(departmentgroupid, ns_bienche, ns_hopdong, ns_hocviec, hvsv_hocvien, hvsv_sinhvien, nhansudate, createdate, createusercode, createusername) VALUES ('" + lstKhoaCheck_Insert[i] + "', '0', '0', '0', '0', '0', '" + DateTime.ParseExact(dateNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + " 00:00:00" + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "'); ";
                            }
                            condb.ExecuteNonQuery_MeL(_sqlInsertNew);
                        }
                        btnTimKiem.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        #region Events
        private void advBandedGridViewData_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var rowHandle = advBandedGridViewData.FocusedRowHandle;
                long _updatenhansuid = Utilities.Util_TypeConvertParse.ToInt64(advBandedGridViewData.GetRowCellValue(rowHandle, "updatenhansuid").ToString());

                int giuongthucke = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "giuongthucke").ToString());
                int nb_hientai = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "nb_hientai").ToString());
                int ns_bienche = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ns_bienche").ToString());
                int ns_hopdong = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ns_hopdong").ToString());
                int ns_hocviec = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ns_hocviec").ToString());
                int ns_hienco = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ns_hienco").ToString());
                int ns_vang = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ns_vang").ToString());
                int lydo_om = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "lydo_om").ToString());
                int lydo_phep = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "lydo_phep").ToString());
                int lydo_de = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "lydo_de").ToString());
                int lydo_khac = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "lydo_khac").ToString());
                int hvsv_hocvien = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "hvsv_hocvien").ToString());
                int hvsv_sinhvien = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "hvsv_sinhvien").ToString());
                int ddktv_soluong = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ddktv_soluong").ToString());
                int ddktv_khoadenid = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ddktv_khoadenid").ToString());
                string ddktv_khoadenname = advBandedGridViewData.GetRowCellValue(rowHandle, "ddktv_khoadenname").ToString();
                int ddktv_songay = Utilities.Util_TypeConvertParse.ToInt32(advBandedGridViewData.GetRowCellValue(rowHandle, "ddktv_songay").ToString());
                string ghichu_khoa = advBandedGridViewData.GetRowCellValue(rowHandle, "ghichu_khoa").ToString();
                string ghichu_dieuduong = advBandedGridViewData.GetRowCellValue(rowHandle, "ghichu_dieuduong").ToString();

                string _sqlUpdate = "UPDATE tools_updatenhansu SET giuongthucke ='" + giuongthucke + "', nb_hientai ='" + nb_hientai + "', ns_bienche ='" + ns_bienche + "', ns_hopdong ='" + ns_hopdong + "', ns_hocviec ='" + ns_hocviec + "', ns_hienco ='" + ns_hienco + "', ns_vang ='" + ns_vang + "', lydo_om ='" + lydo_om + "', lydo_phep ='" + lydo_phep + "', lydo_de ='" + lydo_de + "', lydo_khac ='" + lydo_khac + "', hvsv_hocvien ='" + hvsv_hocvien + "', hvsv_sinhvien ='" + hvsv_sinhvien + "', ddktv_soluong ='" + ddktv_soluong + "', ddktv_khoadenid ='" + ddktv_khoadenid + "', ddktv_khoadenname ='" + ddktv_khoadenname + "', ddktv_songay ='" + ddktv_songay + "', ghichu_khoa ='" + ghichu_khoa + "', ghichu_dieuduong ='" + ghichu_dieuduong + "' WHERE updatenhansuid='" + _updatenhansuid + "';";

                condb.ExecuteNonQuery_MeL(_sqlUpdate);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Custom
        private void advBandedGridViewData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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


        #endregion

        #region Print and Export
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                string tungaydenngay = "(Ngày " + tungay + ")";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "DB_13_CapNhatVaDieuDongNhanLuc.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = DateTime.ParseExact(dateNgay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                string tungaydenngay = "(Ngày " + tungay + ")";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTGROUPNAME;
                reportitem_khoa.value = chkcomboListDSKhoa.Text.ToUpper();
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "DB_13_CapNhatVaDieuDongNhanLuc.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.CapNhatVaDieuDongNhanSuDTO> lstData_XuatBaoCao = new List<ClassCommon.CapNhatVaDieuDongNhanSuDTO>();
                List<ClassCommon.CapNhatVaDieuDongNhanSuDTO> lstData_Khoi = new List<ClassCommon.CapNhatVaDieuDongNhanSuDTO>();

                lstData_Khoi = Utilities.Util_DataTable.DataTableToList<ClassCommon.CapNhatVaDieuDongNhanSuDTO>(Util_GridcontrolConvert.ConvertGridControlToDataTable(advBandedGridViewData));

                List<ClassCommon.CapNhatVaDieuDongNhanSuDTO> lstData_Group = lstData_Khoi.GroupBy(o => o.loaikhoiid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.CapNhatVaDieuDongNhanSuDTO data_groupname = new ClassCommon.CapNhatVaDieuDongNhanSuDTO();

                    List<ClassCommon.CapNhatVaDieuDongNhanSuDTO> lstData_doanhthu = lstData_Khoi.Where(o => o.loaikhoiid == item_group.loaikhoiid).ToList();

                    data_groupname.stt = item_group.loaikhoiten;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }

        #endregion

        #region Process
        private void LoadChinhSua(bool _result)
        {
            try
            {
                gridColumn_giuongthucke.OptionsColumn.AllowEdit = _result;
                gridColumn_nb_hientai.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_bienche.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_hopdong.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_hocviec.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_hienco.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_vang.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_om.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_phep.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_de.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_khac.OptionsColumn.AllowEdit = _result;
                gridColumn_hvsv_hocvien.OptionsColumn.AllowEdit = _result;
                gridColumn_hvsv_sinhvien.OptionsColumn.AllowEdit = _result;
                gridColumn_ddktv_soluong.OptionsColumn.AllowEdit = _result;
                gridColumn_ddktv_khoadenid.OptionsColumn.AllowEdit = _result;
                gridColumn_ddktv_songay.OptionsColumn.AllowEdit = _result;
                gridColumn_ghichu_khoa.OptionsColumn.AllowEdit = _result;
                gridColumn_ghichu_dieuduong.OptionsColumn.AllowEdit = _result;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void LoadPhanQuyenChinhSua(bool _result)
        {
            try
            {
                gridColumn_giuongthucke.OptionsColumn.AllowEdit = _result;
                gridColumn_nb_hientai.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_bienche.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_hopdong.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_hocviec.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_hienco.OptionsColumn.AllowEdit = _result;
                gridColumn_ns_vang.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_om.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_phep.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_de.OptionsColumn.AllowEdit = _result;
                gridColumn_lydo_khac.OptionsColumn.AllowEdit = _result;
                gridColumn_hvsv_hocvien.OptionsColumn.AllowEdit = _result;
                gridColumn_hvsv_sinhvien.OptionsColumn.AllowEdit = _result;
                gridColumn_ddktv_soluong.OptionsColumn.AllowEdit = _result;
                gridColumn_ddktv_khoadenid.OptionsColumn.AllowEdit = _result;
                gridColumn_ddktv_songay.OptionsColumn.AllowEdit = _result;
                gridColumn_ghichu_khoa.OptionsColumn.AllowEdit = _result;

                gridColumn_ghichu_dieuduong.OptionsColumn.AllowEdit = !_result;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion


    }
}