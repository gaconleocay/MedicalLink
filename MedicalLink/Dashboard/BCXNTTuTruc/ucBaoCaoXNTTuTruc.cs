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

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoXNTTuTruc : UserControl
    {
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        List<ClassCommon.classMedicineStore> lstMedicineStoreCurrent = new List<ClassCommon.classMedicineStore>();


        public ucBaoCaoXNTTuTruc()
        {
            InitializeComponent();
        }

        private void ucBaoCaoXNTTuTruc_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachTuTruc();
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
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTuTruc.EditValue == null)
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHO_TU_TRUC;
                }
                else
                {
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
                Base.ExportDataToFile.ExportDataGridViewToFile(gridControlThuocTuTruc, gridViewThuocTuTruc);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
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
    }
}
