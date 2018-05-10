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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using System.Globalization;
using MedicalLink.Utilities;

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoXNTTuTruc : UserControl
    {
        #region Khai bao
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ClassCommon.classMedicineRef> lstMedicineStore { get; set; }
        private List<ClassCommon.classMedicineRef> lstMedicine_ThuocCurrent { get; set; }
        #endregion
        public ucBaoCaoXNTTuTruc()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBaoCaoXNTTuTruc_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachTuTruc();
                LoadDanhMucThuocVatTu();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachTuTruc()
        {
            try
            {
                List<ClassCommon.classMedicineStore> lstMedicineStoreCurrent = new List<ClassCommon.classMedicineStore>();

                string sql_getmedistore = "SELECT medicinestoreid, departmentgroupid, medicinestoretype,medicinestorecode,medicinestorename FROM medicine_store WHERE  medicinestoretype in (8,9) ORDER BY medicinestorename, departmentgroupid, medicinestoretype;";
                DataView dataStore = new DataView(condb.GetDataTable_HIS(sql_getmedistore));
                List<ClassCommon.classMedicineStore> lstMedicineStore = new List<ClassCommon.classMedicineStore>();

                if (dataStore != null && dataStore.Count > 0)
                {
                    for (int i = 0; i < dataStore.Count; i++)
                    {
                        ClassCommon.classMedicineStore medicinestore = new ClassCommon.classMedicineStore();
                        medicinestore.medicinestoreid = Utilities.Util_TypeConvertParse.ToInt32(dataStore[i]["medicinestoreid"].ToString());
                        medicinestore.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt32(dataStore[i]["departmentgroupid"].ToString());
                        medicinestore.medicinestoretype = Utilities.Util_TypeConvertParse.ToInt32(dataStore[i]["medicinestoretype"].ToString());
                        medicinestore.medicinestorecode = dataStore[i]["medicinestorecode"].ToString();
                        medicinestore.medicinestorename = dataStore[i]["medicinestorename"].ToString();
                        lstMedicineStore.Add(medicinestore);
                    }
                }

                if (Base.SessionLogin.SessionUsercode == Base.KeyTrongPhanMem.AdminUser_key)
                {
                    lstMedicineStoreCurrent = lstMedicineStore;
                }
                else
                {
                    var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                    if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                    {
                        lstMedicineStoreCurrent = (from lstKho in lstMedicineStore
                                                   from lstKhoa in lstDSKhoa
                                                   where lstKho.departmentgroupid == lstKhoa.departmentgroupid
                                                   select new ClassCommon.classMedicineStore
                                                   {
                                                       departmentgroupid = lstKho.departmentgroupid,
                                                       medicinestoreid = lstKho.medicinestoreid,
                                                       medicinestoretype = lstKho.medicinestoretype,
                                                       medicinestorecode = lstKho.medicinestorecode,
                                                       medicinestorename = lstKho.medicinestorename
                                                   }).ToList();
                    }
                }

                cboTuTruc.Properties.DataSource = lstMedicineStoreCurrent;
                cboTuTruc.Properties.DisplayMember = "medicinestorename";
                cboTuTruc.Properties.ValueMember = "medicinestoreid";
                if (lstMedicineStoreCurrent.Count == 1)
                {
                    cboTuTruc.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhMucThuocVatTu()
        {
            try
            {
                string sql_getmeref = "SELECT mef.medicinerefid, mef.medicinerefid_org, mef.medicinecode, mef.medicinename FROM medicine_ref mef WHERE mef.isremove=0;";
                DataView dataStore = new DataView(condb.GetDataTable_HIS(sql_getmeref));
                lstMedicineStore = new List<ClassCommon.classMedicineRef>();

                if (dataStore != null && dataStore.Count > 0)
                {
                    for (int i = 0; i < dataStore.Count; i++)
                    {
                        ClassCommon.classMedicineRef medicinestore = new ClassCommon.classMedicineRef();
                        medicinestore.medicinerefid = Utilities.Util_TypeConvertParse.ToInt64(dataStore[i]["medicinerefid"].ToString());
                        medicinestore.medicinerefid_org = Utilities.Util_TypeConvertParse.ToInt64(dataStore[i]["medicinerefid_org"].ToString());
                        medicinestore.medicinecode = dataStore[i]["medicinecode"].ToString();
                        medicinestore.medicinename = dataStore[i]["medicinename"].ToString();
                        lstMedicineStore.Add(medicinestore);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tim Kiem va HIen Thi
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTuTruc.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHO_TU_TRUC);
                    frmthongbao.Show();
                }
                else
                {
                    gridControlThuocTuTruc.DataSource = null;
                    LayDuLieuThuocTrongKho();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LayDuLieuThuocTrongKho()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string medicinekiemkeid = " ";
                string sql_getThuoc = "";
                string sql_getkiemke = "select COALESCE(max(medicinekiemkeid),0) as medicinekiemkeid from medicinekiemke where medicinestoreid=" + cboTuTruc.EditValue + " and medicinekiemkestatus=0;";
                DataView dataKiemKe = new DataView(condb.GetDataTable_HIS(sql_getkiemke));
                if (dataKiemKe != null && dataKiemKe.Count > 0 && Utilities.Util_TypeConvertParse.ToInt64(dataKiemKe[0]["medicinekiemkeid"].ToString()) > 0)
                {
                    medicinekiemkeid = " and medicinekiemkeid=" + dataKiemKe[0]["medicinekiemkeid"] + " ";
                }
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                if (chkXemChiTiet.Checked) // xem chi tiet
                {
                    sql_getThuoc = "SELECT '' as stt, me.medicinerefid_org, me.medicinegroupcode, me.medicinecode, me.medicinename, me.donvitinh, msref.soluongtonkho as soluongtonkho, msref.soluongkhadung as soluongkhadung, msref.soluongtutruc, me.hansudung, DATE_PART('day', me.hansudung - now()) as songay, me.solo FROM (select medicinerefid,medicinerefid_org,medicinegroupcode,medicinecode,medicinename,donvitinh,hansudung,solo from medicine_ref) me inner join (select medicinerefid,soluongtonkho,soluongkhadung,soluongtutruc from medicine_store_ref where medicinestoreid=" + cboTuTruc.EditValue + " and (soluongtonkho>0 or soluongkhadung>0) and medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + ") msref on me.medicinerefid=msref.medicinerefid order by me.medicinegroupcode,me.medicinename,me.medicinecode;";
                }
                else
                {
                    sql_getThuoc = "SELECT row_number() OVER () as stt, me.medicinerefid_org, me.medicinegroupcode, (select medi.medicinecode from medicine_ref medi where medi.medicinerefid=me.medicinerefid_org) as medicinecode, me.medicinename, me.donvitinh, sum(msref.soluongtonkho) as soluongtonkho, sum(msref.soluongkhadung) as soluongkhadung, msref.soluongtutruc,'' as hansudung, 100 as songay, '' as solo FROM (select medicinerefid,medicinerefid_org,medicinegroupcode,medicinename,donvitinh from medicine_ref) me inner join (select medicinerefid,soluongtonkho,soluongkhadung,soluongtutruc from medicine_store_ref where medicinestoreid=" + cboTuTruc.EditValue + " and (soluongtutruc>0 or soluongtonkho>0 or soluongkhadung>0) and medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + ") msref on me.medicinerefid=msref.medicinerefid GROUP BY me.medicinerefid_org,me.medicinegroupcode,me.medicinename,me.donvitinh,msref.soluongtutruc ORDER BY me.medicinegroupcode,me.medicinename; ";
                }
                DataTable dataDanhMucThuoc = condb.GetDataTable_HIS(sql_getThuoc);

                List<ClassCommon.classMedicineRef> lstMedicine_ThuocHienThi = new List<ClassCommon.classMedicineRef>();

                if (dataDanhMucThuoc != null && dataDanhMucThuoc.Rows.Count > 0)
                {

                    // List<ClassCommon.classMedicineRef> lstMedicine_Thuoc = Util_DataTable.DataTableToList<ClassCommon.classMedicineRef>(dataDanhMucThuoc);
                    this.lstMedicine_ThuocCurrent = new List<ClassCommon.classMedicineRef>();
                    for (int i = 0; i < dataDanhMucThuoc.Rows.Count; i++)
                    {
                        ClassCommon.classMedicineRef _datathuoc = new ClassCommon.classMedicineRef();
                        _datathuoc.stt = dataDanhMucThuoc.Rows[i]["stt"];
                        _datathuoc.medicinerefid_org = Utilities.Util_TypeConvertParse.ToInt64(dataDanhMucThuoc.Rows[i]["medicinerefid_org"].ToString());
                        _datathuoc.medicinecode = dataDanhMucThuoc.Rows[i]["medicinecode"].ToString();
                        _datathuoc.medicinename = dataDanhMucThuoc.Rows[i]["medicinename"].ToString();
                        _datathuoc.medicinegroupcode = dataDanhMucThuoc.Rows[i]["medicinegroupcode"].ToString();
                        _datathuoc.donvitinh = dataDanhMucThuoc.Rows[i]["donvitinh"].ToString();
                        _datathuoc.soluongtonkho = Util_TypeConvertParse.ToDecimal(dataDanhMucThuoc.Rows[i]["soluongtonkho"].ToString());
                        _datathuoc.soluongkhadung = Util_TypeConvertParse.ToDecimal(dataDanhMucThuoc.Rows[i]["soluongkhadung"].ToString());
                        _datathuoc.soluongtutruc = Util_TypeConvertParse.ToDecimal(dataDanhMucThuoc.Rows[i]["soluongtutruc"].ToString());
                        _datathuoc.hansudung = dataDanhMucThuoc.Rows[i]["hansudung"];
                        _datathuoc.songay= Utilities.Util_TypeConvertParse.ToInt64(dataDanhMucThuoc.Rows[i]["songay"].ToString());
                        _datathuoc.solo = dataDanhMucThuoc.Rows[i]["solo"].ToString();
                        this.lstMedicine_ThuocCurrent.Add(_datathuoc);
                    }



                    if (chkXemChiTiet.Checked == false)
                    {
                        lstMedicine_ThuocHienThi = this.lstMedicine_ThuocCurrent;
                    }
                    else
                    {
                        //Lo thuoc Goc
                        List<ClassCommon.classMedicineRef> lstMedicine_LoGoc = this.lstMedicine_ThuocCurrent.GroupBy(o => o.medicinerefid_org).Select(n => n.First()).ToList();
                        for (int i = 0; i < lstMedicine_LoGoc.Count; i++)
                        {
                            ClassCommon.classMedicineRef medicineitem_logoc = new ClassCommon.classMedicineRef();

                            string[] malothuocgoc = lstMedicine_LoGoc[i].medicinecode.Split('.');
                            medicineitem_logoc.stt = i + 1;
                            medicineitem_logoc.medicinecode = malothuocgoc[0];
                            medicineitem_logoc.medicinerefid = lstMedicine_LoGoc[i].medicinerefid;
                            medicineitem_logoc.medicinerefid_org = lstMedicine_LoGoc[i].medicinerefid_org;
                            medicineitem_logoc.medicinename = lstMedicine_LoGoc[i].medicinename;
                            medicineitem_logoc.medicinerefid_orgcode = lstMedicine_LoGoc[i].medicinerefid_orgcode;
                            medicineitem_logoc.medicinegroupcode = lstMedicine_LoGoc[i].medicinegroupcode;
                            medicineitem_logoc.donvitinh = lstMedicine_LoGoc[i].donvitinh;
                            medicineitem_logoc.soluongtutruc = lstMedicine_LoGoc[i].soluongtutruc;
                            medicineitem_logoc.islogoc = 1;
                            medicineitem_logoc.songay = 100;
                            decimal sum_soluongtonkho = 0;
                            decimal sum_soluongkhadung = 0;
                            //medicineitem_logoc.
                            List<ClassCommon.classMedicineRef> lst_thuoccon = this.lstMedicine_ThuocCurrent.Where(o => o.medicinerefid_org == lstMedicine_LoGoc[i].medicinerefid_org && o.medicinecode != medicineitem_logoc.medicinecode).ToList();
                            foreach (var item_locon in lst_thuoccon)
                            {
                                sum_soluongtonkho += item_locon.soluongtonkho;
                                sum_soluongkhadung += item_locon.soluongkhadung;
                            }

                            medicineitem_logoc.soluongtonkho = sum_soluongtonkho;
                            medicineitem_logoc.soluongkhadung = sum_soluongkhadung;

                            lstMedicine_ThuocHienThi.Add(medicineitem_logoc);
                            lstMedicine_ThuocHienThi.AddRange(lst_thuoccon);
                        }
                    }

                    ////=======
                    ////Nhom thuoc
                    //List<ClassCommon.classMedicineRef> lstMedicine_ThuocGroup = lstMedicine_Thuoc.GroupBy(o => o.medicinegroupcode).Select(n => n.First()).ToList();
                    //foreach (var item_group in lstMedicine_ThuocGroup)
                    //{
                    //    ClassCommon.classMedicineRef medicineitem_group = new ClassCommon.classMedicineRef();
                    //    medicineitem_group.medicinegroupcode = item_group.medicinegroupcode;
                    //    medicineitem_group.isgroup = 1;
                    //    lstMedicine_ThuocHienThi.Add(medicineitem_group);
                    //    //Lo thuoc Goc
                    //    List<ClassCommon.classMedicineRef> lstMedicine_LoGoc = lstMedicine_Thuoc.Where(o => o.medicinegroupcode == item_group.medicinegroupcode).ToList().GroupBy(o => o.medicinerefid_org).Select(n => n.First()).ToList();

                    //    if (chkXemChiTiet.Checked == false)
                    //    {
                    //        lstMedicine_ThuocHienThi.AddRange(lstMedicine_LoGoc);
                    //    }
                    //    else
                    //    {
                    //        foreach (var item_logoc in lstMedicine_LoGoc)
                    //        {
                    //            ClassCommon.classMedicineRef medicineitem_logoc = new ClassCommon.classMedicineRef();

                    //            string[] malothuocgoc = item_logoc.medicinecode.Split('.');
                    //            medicineitem_logoc.medicinecode = malothuocgoc[0];
                    //            medicineitem_logoc.medicinerefid = item_logoc.medicinerefid;
                    //            medicineitem_logoc.medicinerefid_org = item_logoc.medicinerefid_org;
                    //            medicineitem_logoc.medicinename = item_logoc.medicinename;
                    //            medicineitem_logoc.medicinerefid_orgcode = item_logoc.medicinerefid_orgcode;
                    //            medicineitem_logoc.medicinegroupcode = item_logoc.medicinegroupcode;
                    //            medicineitem_logoc.donvitinh = item_logoc.donvitinh;

                    //            decimal sum_soluongtonkho = 0;
                    //            decimal sum_soluongkhadung = 0;
                    //            //medicineitem_logoc.
                    //            List<ClassCommon.classMedicineRef> lst_thuoccon = lstMedicine_Thuoc.Where(o => o.medicinerefid_org == item_logoc.medicinerefid_org && o.medicinecode != medicineitem_logoc.medicinecode).ToList();
                    //            foreach (var item_locon in lst_thuoccon)
                    //            {
                    //                sum_soluongtonkho += item_locon.soluongtonkho;
                    //                sum_soluongkhadung += item_locon.soluongkhadung;
                    //            }

                    //            medicineitem_logoc.soluongtonkho = sum_soluongtonkho;
                    //            medicineitem_logoc.soluongkhadung = sum_soluongkhadung;

                    //            lstMedicine_ThuocHienThi.Add(medicineitem_logoc);
                    //            lstMedicine_ThuocHienThi.AddRange(lst_thuoccon);
                    //        }
                    //    }
                    //}
                }
                gridControlThuocTuTruc.DataSource = lstMedicine_ThuocHienThi;
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                itemXemLichSuNhapXuat_Click(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridViewThuocTuTruc_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Xem lịch sử nhập xuất thuốc/vật tư");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(itemXemLichSuNhapXuat_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        void itemXemLichSuNhapXuat_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewThuocTuTruc.FocusedRowHandle;
                long medicinerefid_org = Utilities.Util_TypeConvertParse.ToInt64(gridViewThuocTuTruc.GetRowCellValue(rowHandle, "medicinerefid_org").ToString());
                if (medicinerefid_org != 0)
                {
                    BCXNTTuTruc.BCXNTTuTrucLichSu frmLichSu = new BCXNTTuTruc.BCXNTTuTrucLichSu(Utilities.Util_TypeConvertParse.ToInt64(cboTuTruc.EditValue.ToString()), medicinerefid_org, lstMedicineStore);
                    frmLichSu.ShowDialog();
                }
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
                try
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    string tungaydenngay = "Thời gian " + lblThoiGianLayBaoCao.Text;

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                    reportitem_khoa.name = Base.bienTrongBaoCao.MEDICINESTORENAME;
                    reportitem_khoa.value = cboTuTruc.Text;
                    thongTinThem.Add(reportitem_khoa);

                    string fileTemplatePath = "BC_XuatNhapTonTuTruc.xlsx";
                    DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
                catch (Exception ex)
                {
                    Base.Logging.Warn(ex);
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungaydenngay = "Thời gian " + lblThoiGianLayBaoCao.Text;

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                reportitem_khoa.name = Base.bienTrongBaoCao.MEDICINESTORENAME;
                reportitem_khoa.value = cboTuTruc.Text;
                thongTinThem.Add(reportitem_khoa);

                string fileTemplatePath = "BC_XuatNhapTonTuTruc.xlsx";
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
                List<ClassCommon.BCXuatNhapTonTuTruc> lstData_XuatBaoCao = new List<ClassCommon.BCXuatNhapTonTuTruc>();
                List<ClassCommon.BCXuatNhapTonTuTruc> lstDataDoanhThu = new List<ClassCommon.BCXuatNhapTonTuTruc>();
                lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.BCXuatNhapTonTuTruc>(Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewThuocTuTruc));

                List<ClassCommon.BCXuatNhapTonTuTruc> lstData_Group = lstDataDoanhThu.GroupBy(o => o.medicinegroupcode).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BCXuatNhapTonTuTruc data_groupname = new ClassCommon.BCXuatNhapTonTuTruc();
                    List<ClassCommon.BCXuatNhapTonTuTruc> lstData_doanhthu = lstDataDoanhThu.Where(o => o.medicinegroupcode == item_group.medicinegroupcode).ToList();

                    data_groupname.stt = item_group.medicinegroupcode;
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

        private void chkXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnTimKiem_Click(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Custom
        private void gridViewThuocTuTruc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
            if (view.GetRowCellValue(e.RowHandle,
              view.Columns["islogoc"]).ToString() == "1")
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void gridViewThuocTuTruc_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    int tongsongay = Utilities.Util_TypeConvertParse.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["songay"]));
                    if (tongsongay >= 1 && tongsongay <= 30)
                    {
                        e.Appearance.BackColor = Color.LightSalmon;
                        //e.Appearance.BackColor2 = Color.LightCyan;
                        e.HighPriority = true;
                    }
                    else if (tongsongay <= 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                        //e.Appearance.BackColor2 = Color.LightCyan;
                        e.HighPriority = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }





        #endregion
    }
}
