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

namespace MedicalLink.BaoCao
{
    public partial class ucBCXuatThuocNhaThuoc : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        DataTable dataBCBXuatThuoc { get; set; }

        public ucBCXuatThuocNhaThuoc()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCXuatThuocNhaThuoc_Load(object sender, EventArgs e)
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
                string sql_timkiem = "";
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

                this.dataBCBXuatThuoc = new DataTable();
                gridControlDonThuoc.DataSource = null;
                //Trang thai
                if (cbbTieuChi.Text == "Đã xuất")
                {
                    sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY msb_cd.medicinestorebilldate) as stt, msb_cd.medicinestorebillid, msb_cd.medicinestorebillcode, mbp.patientid, mbp.vienphiid, (case when msb_cd.maubenhphamid=0 then msb_cd.partnername else hsba.patientname end) as patientname, (case when hsba.gioitinhcode='01' then to_char(hsba.birthday,'yyyy') else '' end) as year_nam, (case hsba.gioitinhcode when '02' then to_char(hsba.birthday,'yyyy') else '' end) as year_nu, hsba.bhytcode, kcd.departmentgroupname, (case when msb_cd.maubenhphamid=0 then mes.medicinestorename else pcd.departmentname end) as departmentname, msb_cd.medicinestorebilldate as ngaychidinh, msb_cd.bacsi as nguoichidinh, msb_cd.medicinestorebilldate as ngayxuat, nx.username as nguoixuat, mes.medicinestorename, (case when msb_cd.medicinestorebilltype=8 then (case when msb_cd.maubenhphamid=0 then (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) else (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) end) else (case when msb_cd.maubenhphamid=0 then (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) else (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) end) end) as thanhtien, (case when msb_cd.medicinestorebilltype=8 then 'Nhập trả' else '' end) as medicinestorebilltypename, msb_cd.medicinestorebillstatus, msb_cd.medicinestorebillprocessingdate as ngayhethong, (case when msb_cd.maubenhphamid=0 then 'Khách lẻ' else '' end) as ghichu FROM medicine_store_bill msb_cd LEFT JOIN (select medicinestorebillid,medicinestorebilldoer from medicine_store_bill) msb on msb.medicinestorebillid=msb_cd.medicinestorebillid_ex LEFT JOIN (select maubenhphamid,hosobenhanid,patientid,vienphiid,isloaidonthuoc,medicinestoreid from maubenhpham) mbp on mbp.maubenhphamid=msb_cd.maubenhphamid and mbp.isloaidonthuoc=1 LEFT JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=(case when msb_cd.departmentgroupid=0 then msb_cd.khoa_id else msb_cd.departmentgroupid end) LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd on pcd.departmentid=msb_cd.departmentid LEFT JOIN nhompersonnel nx on nx.usercode=COALESCE(msb.medicinestorebilldoer,msb_cd.medicinestorebilldoer) LEFT JOIN medicine_store mes on mes.medicinestoreid=msb_cd.partnerid or mes.medicinestoreid=msb_cd.medicinestoreid and mes.medicinestoretype=4 WHERE msb_cd.isremove=0 and msb_cd.medicinestorebilltype in (200,6,8) and msb_cd.medicinestorebilldate>='" + datetungay + "' and msb_cd.medicinestorebilldate<='" + datedenngay + "' and mes.medicinestoreid in (" + lstKhoThuocChonLayBC + ") and msb_cd.medicinestorebillstatus<>9;  ";
                }
                else if (cbbTieuChi.Text == "Chưa xuất")
                {

                    sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY msb_cd.medicinestorebilldate) as stt, msb_cd.medicinestorebillid, msb_cd.medicinestorebillcode, mbp.patientid, mbp.vienphiid, (case when msb_cd.maubenhphamid=0 then msb_cd.partnername else hsba.patientname end) as patientname, (case when hsba.gioitinhcode='01' then to_char(hsba.birthday,'yyyy') else '' end) as year_nam, (case hsba.gioitinhcode when '02' then to_char(hsba.birthday,'yyyy') else '' end) as year_nu, hsba.bhytcode, kcd.departmentgroupname, (case when msb_cd.maubenhphamid=0 then mes.medicinestorename else pcd.departmentname end) as departmentname, msb_cd.medicinestorebilldate as ngaychidinh, msb_cd.bacsi as nguoichidinh, '' as ngayxuat, '' as nguoixuat, mes.medicinestorename, (case when msb_cd.medicinestorebilltype=8 then (case when msb_cd.maubenhphamid=0 then (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) else (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) end) else (case when msb_cd.maubenhphamid=0 then (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) else (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) end) end) as thanhtien, (case when msb_cd.medicinestorebilltype=8 then 'Nhập trả' else '' end) as medicinestorebilltypename, msb_cd.medicinestorebillstatus, '' as ngayhethong, (case when msb_cd.maubenhphamid=0 then 'Khách lẻ' else '' end) as ghichu FROM medicine_store_bill msb_cd LEFT JOIN (select maubenhphamid,hosobenhanid,patientid,vienphiid,medicinestoreid from maubenhpham where isloaidonthuoc=1) mbp on mbp.maubenhphamid=msb_cd.maubenhphamid LEFT JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=(case when msb_cd.departmentgroupid=0 then msb_cd.khoa_id else msb_cd.departmentgroupid end) LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd on pcd.departmentid=msb_cd.departmentid LEFT JOIN nhompersonnel nx on nx.usercode=msb_cd.medicinestorebilldoer LEFT JOIN medicine_store mes on mes.medicinestoreid=mbp.medicinestoreid or mes.medicinestoreid=msb_cd.medicinestoreid and mes.medicinestoretype=4 WHERE msb_cd.isremove=0 and msb_cd.medicinestorebilltype in (200,6,8) and msb_cd.medicinestorebilldate>='" + datetungay + "' and msb_cd.medicinestorebilldate<='" + datedenngay + "' and mes.medicinestoreid in (" + lstKhoThuocChonLayBC + ") and msb_cd.medicinestorebillstatus not in (2,11);  ";
                }

                this.dataBCBXuatThuoc = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBCBXuatThuoc != null && this.dataBCBXuatThuoc.Rows.Count > 0)
                {
                    gridControlDonThuoc.DataSource = this.dataBCBXuatThuoc;
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

        private void gridViewDonThuoc_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Xem chi tiết hóa đơn xuất thuốc");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(itemXemChiTietDonThuoc_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        void itemXemChiTietDonThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDonThuoc.FocusedRowHandle;
                long medicinestorebillid = Utilities.TypeConvertParse.ToInt64(gridViewDonThuoc.GetRowCellValue(rowHandle, "medicinestorebillid").ToString());
                if (medicinestorebillid != 0)
                {
                    BCXuatThuocNhaThuoc.BCXuatThuocNhaThuocDetail frmDetail = new BCXuatThuocNhaThuoc.BCXuatThuocNhaThuocDetail(medicinestorebillid);
                    frmDetail.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void repositoryItemButtonEdit_detail_Click(object sender, EventArgs e)
        {
            try
            {
                itemXemChiTietDonThuoc_Click(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
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

                string fileTemplatePath = "BC_ThuTienNhaThuocHangNgay.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCBXuatThuoc);
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
                string fileTemplatePath = "BC_ThuTienNhaThuocHangNgay.xlsx";
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
