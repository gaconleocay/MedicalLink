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

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoXNTTuTruc : UserControl
    {
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ClassCommon.classMedicineRef> lstMedicineStore { get; set; }

        public ucBaoCaoXNTTuTruc()
        {
            InitializeComponent();
        }

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

                string sql_getmedistore = "SELECT medicinestoreid, departmentgroupid, medicinestoretype,medicinestorecode,medicinestorename FROM medicine_store WHERE  medicinestoretype in (8,9) ORDER BY departmentgroupid, medicinestoretype, medicinestorename;";
                DataView dataStore = new DataView(condb.getDataTable(sql_getmedistore));
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
                    var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyenKhoaPhong.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
                DataView dataStore = new DataView(condb.getDataTable(sql_getmeref));
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
                string sql_getkiemke = "select COALESCE(max(medicinekiemkeid),0) as medicinekiemkeid from medicinekiemke where medicinestoreid=" + cboTuTruc.EditValue + " and medicinekiemkestatus=0;";
                DataView dataKiemKe = new DataView(condb.getDataTable(sql_getkiemke));
                if (dataKiemKe != null && dataKiemKe.Count > 0 && Utilities.Util_TypeConvertParse.ToInt64(dataKiemKe[0]["medicinekiemkeid"].ToString()) > 0)
                {
                    medicinekiemkeid = " and msref.medicinekiemkeid=" + dataKiemKe[0]["medicinekiemkeid"] + " ";
                }
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sql_getThuoc = "SELECT row_number() OVER () as stt, me.medicinerefid_org, me.medicinegroupcode, (select medi.medicinecode from medicine_ref medi where medi.medicinerefid=me.medicinerefid_org) as medicinecode, me.medicinename, me.donvitinh, sum(msref.soluongtonkho) as soluongtonkho, sum(msref.soluongkhadung) as soluongkhadung, msref.soluongtutruc FROM medicine_ref me inner join medicine_store_ref msref on me.medicinerefid=msref.medicinerefid WHERE msref.medicinestoreid=" + cboTuTruc.EditValue + " and msref.soluongtutruc>0  and msref.medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + " GROUP BY me.medicinerefid_org,me.medicinegroupcode,me.medicinename,me.donvitinh,msref.soluongtutruc ORDER BY me.medicinegroupcode,me.medicinename;";
                DataView dataDanhMucThuoc = new DataView(condb.getDataTable(sql_getThuoc));
                gridControlThuocTuTruc.DataSource = dataDanhMucThuoc;
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportDataGridViewToFile(gridControlThuocTuTruc, gridViewThuocTuTruc);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridViewThuocTuTruc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
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




    }
}
