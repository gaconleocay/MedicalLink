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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.ClassCommon;

namespace MedicalLink.BaoCao
{
    public partial class ucBCThuocTheoNguoiKe : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        DataTable dataBCBXuatThuoc { get; set; }

        public ucBCThuocTheoNguoiKe()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCThuocTheoNguoiKe_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhSachNhaThuoc();
        }
        private void LoadDanhSachNhaThuoc()
        {
            try
            {
                List<ClassCommon.classUserMedicineStore> lstdsphongthuockhoa = Base.SessionLogin.LstPhanQuyen_KhoThuoc.Where(o => o.MedicineStoreType == 4).ToList();
                chkcomboListDSKho.Properties.DataSource = lstdsphongthuockhoa;
                chkcomboListDSKho.Properties.DisplayMember = "MedicineStoreName";
                chkcomboListDSKho.Properties.ValueMember = "MedicineStoreId";

                chkcomboListDSKho.CheckAll();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string lstKhoThuocChonLayBC = "";
                string lstPhongLuuChonLayBC = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                //kho thuoc
                List<Object> lstPhongCheck = chkcomboListDSKho.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        lstKhoThuocChonLayBC += lstPhongCheck[i] + ",";
                    }
                    lstKhoThuocChonLayBC += lstPhongCheck[lstPhongCheck.Count - 1];
                }
                if (lstKhoThuocChonLayBC == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHO_THUOC);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }
                //Phong luu
                List<Object> lstNhomCheck = chkcomboListPhongLuu.Properties.Items.GetCheckedValues();
                if (lstNhomCheck.Count > 0)
                {
                    lstPhongLuuChonLayBC = " where msr.medicinephongluuid in (";
                    for (int i = 0; i < lstNhomCheck.Count - 1; i++)
                    {
                        lstPhongLuuChonLayBC += "'" + lstNhomCheck[i] + "',";
                    }
                    lstPhongLuuChonLayBC += "'" + lstNhomCheck[lstNhomCheck.Count - 1] + "') ";
                    lstPhongLuuChonLayBC = lstPhongLuuChonLayBC.Replace("'", "");
                }
                if (chkNhom_TatCa.Checked == false && lstPhongLuuChonLayBC == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_LUU);
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }

                //Lay du lieu
                this.dataBCBXuatThuoc = new DataTable();
                gridControlTheoNguoiKe.DataSource = null;
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY msb.bacsi, (case when msb.departmentid=0 then mes.medicinestorename else pcd.departmentname end), mef.medicinerefid_org) as stt, mef.medicinerefid_org, (select medicinecode from medicine_ref where medicinerefid=mef.medicinerefid_org) as medicinecode, mef.medicinename, sum(me.accept_soluong) as soluong, me.accept_money as dongia, sum(me.accept_soluong * me.accept_money) as thanhtien, kcd.departmentgroupname as khoachidinh, (case when msb.departmentid=0 then mes.medicinestorename else pcd.departmentname end) as phongchidinh, msb.bacsi as nguoichidinh FROM medicine_ref mef inner join (select msr.medicinerefid, msr.medicinestorerefid from medicine_store_ref msr " + lstPhongLuuChonLayBC + ") msrid on msrid.medicinerefid=mef.medicinerefid inner join medicine me on me.medicinestorerefid=msrid.medicinestorerefid inner join medicine_store_bill msb on msb.medicinestorebillid=me.medicinestorebillid left join departmentgroup kcd on kcd.departmentgroupid=msb.departmentgroupid left join department pcd on pcd.departmentid=msb.departmentid and pcd.departmenttype in (2,3,9) left join medicine_store mes on mes.medicinestoreid=msb.partnerid or mes.medicinestoreid=msb.medicinestoreid WHERE msb.medicinestorebillstatus=2 and mes.medicinestoretype=4 and msb.isremove=0 and msb.medicinestorebilltype in (204,6) and msb.medicinestorebilldate>='" + datetungay + "' and msb.medicinestorebilldate<='" + datedenngay + "' and mes.medicinestoreid in (" + lstKhoThuocChonLayBC + ") GROUP BY mef.medicinerefid_org, mef.medicinename, me.accept_money, kcd.departmentgroupname, msb.bacsi, (case when msb.departmentid=0 then mes.medicinestorename else pcd.departmentname end); ";
                this.dataBCBXuatThuoc = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBCBXuatThuoc != null && this.dataBCBXuatThuoc.Rows.Count > 0)
                {
                    gridControlTheoNguoiKe.DataSource = this.dataBCBXuatThuoc;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void gridViewDonThuoc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void chkNhom_TatCa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkNhom_TatCa.Checked)
                {
                    chkcomboListPhongLuu.ReadOnly = true;
                    chkcomboListPhongLuu.SetEditValue(null);
                    chkcomboListPhongLuu.DeselectAll();
                }
                else
                {
                    chkcomboListPhongLuu.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void chkcomboListDSKho_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListPhongLuu.Properties.Items.Clear();
                List<Object> lstKhoaCheck = chkcomboListDSKho.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    List<ClassCommon.classUserMedicinePhongLuu> lstDSPhongLuu = new List<classUserMedicinePhongLuu>();
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        List<ClassCommon.classUserMedicinePhongLuu> lstdsphongthuockhoa = Base.SessionLogin.LstPhanQuyen_PhongLuu.Where(o => o.MedicineStoreId == Utilities.TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
                        lstDSPhongLuu.AddRange(lstdsphongthuockhoa);
                    }
                    if (lstDSPhongLuu != null && lstDSPhongLuu.Count > 0)
                    {
                        //foreach (var item in lstDSPhongLuu)
                        //{
                        //    item.MedicinePhongLuuName = item.MedicineStoreName + "-" + item.MedicinePhongLuuName;
                        //}
                        chkcomboListPhongLuu.Properties.DataSource = lstDSPhongLuu;
                        chkcomboListPhongLuu.Properties.DisplayMember = "MedicinePhongLuuName";
                        chkcomboListPhongLuu.Properties.ValueMember = "MedicinePhongLuuId";
                    }
                    chkcomboListPhongLuu.CheckAll();
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
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.bienTrongBaoCao.LST_MECICINESTORENAME;
                reportitem_kho.value = chkcomboListDSKho.Text.ToUpper();

                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);

                string fileTemplatePath = "BC_ThuocTheoNguoiKeNhaThuoc.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataBCBXuatThuoc);
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
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.bienTrongBaoCao.LST_MECICINESTORENAME;
                reportitem_kho.value = chkcomboListDSKho.Text.ToUpper();

                thongTinThem.Add(reportitem);
                thongTinThem.Add(reportitem_kho);
                string fileTemplatePath = "BC_ThuocTheoNguoiKeNhaThuoc.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBCBXuatThuoc);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
    }
}
