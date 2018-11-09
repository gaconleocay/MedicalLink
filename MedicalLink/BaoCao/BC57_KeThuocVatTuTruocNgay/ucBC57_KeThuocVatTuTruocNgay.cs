using System;
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
using MedicalLink.Base;
using DevExpress.Utils.Menu;
using MedicalLink.BaoCao.BC57_KeThuocVatTuTruocNgay;

namespace MedicalLink.BaoCao
{
    public partial class ucBC57_KeThuocVatTuTruocNgay : UserControl
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion

        public ucBC57_KeThuocVatTuTruocNgay()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC53_BNKhongLamDVKTPK_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhoa();
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
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
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _lstKhoaChonLayBC = " and departmentgroupid in (0";
                string _maubenhphamgrouptype = " and maubenhphamgrouptype in (5,6) ";

                //Khoa
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        _lstKhoaChonLayBC += "," + lstKhoaCheck[i];
                    }
                    _lstKhoaChonLayBC += ")";
                    _lstKhoaChonLayBC = _lstKhoaChonLayBC.Replace("(0,", "(");
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }
                //Loai thuoc/VT
                if (cboLoaiThuocVT.Text == "Thuốc")
                {
                    _maubenhphamgrouptype = " and maubenhphamgrouptype in (5) ";
                }
                else if (cboLoaiThuocVT.Text == "Vật tư")
                {
                    _maubenhphamgrouptype = " and maubenhphamgrouptype in (6) ";
                }
                string _sqlTimKiem = $@"SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=1 then 1 else 0 end) as sl_qua1,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=1 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua1,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=2 then 1 else 0 end) as sl_qua2,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=2 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua2,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=3 then 1 else 0 end) as sl_qua3,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=3 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua3,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=4 then 1 else 0 end) as sl_qua4,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=4 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua4,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=5 then 1 else 0 end) as sl_qua5,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=5 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua5,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=6 then 1 else 0 end) as sl_qua6,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=6 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua6,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=7 then 1 else 0 end) as sl_qua7,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=7 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua7,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))>7 then 1 else 0 end) as sl_quahon7,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))>7 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_quahon7
FROM (select departmentgroupid,maubenhphamdate_sudung,maubenhphamstatus from maubenhpham where maubenhphamdate_sudung>now() and medicinestoreid not in (144,145,146,147,148,158,164,165,181) {_maubenhphamgrouptype} {_lstKhoaChonLayBC}) mbp
	inner join (select departmentgroupid,departmentgroupname from departmentgroup where 1=1 {_lstKhoaChonLayBC}) degp on degp.departmentgroupid=mbp.departmentgroupid
GROUP BY degp.departmentgroupid,degp.departmentgroupname;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlTimKiem);
                if (_dataBaoCao.Rows.Count > 0)
                {
                    gridControlBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region In va xuat excel
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "Thời gian lấy báo cáo " + DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_57_KeThuocVatTuTruocNgay.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "Thời gian lấy báo cáo " + DateTime.Now.ToString("HH:mm dd/MM/yyyy");
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_57_KeThuocVatTuTruocNgay.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Custom
        private void bandedGridViewBaoCao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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





        #endregion

        #region Events
        private void gridViewBaoCao_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem item_DuyetPTTTChon = new DXMenuItem("Xem chi tiết đơn thuốc/vật tư");
                    item_DuyetPTTTChon.Image = imMenu.Images[0];
                    item_DuyetPTTTChon.Click += new EventHandler(XemChiTietDonThuocVatTu_Click);
                    e.Menu.Items.Add(item_DuyetPTTTChon);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void XemChiTietDonThuocVatTu_Click(object sender, EventArgs e)
        {
            HienThiXemChiTietThuocVatTu();
        }
        private void repositoryItemButton_ChiTiet_Click(object sender, EventArgs e)
        {
            HienThiXemChiTietThuocVatTu();
        }
        private void HienThiXemChiTietThuocVatTu()
        {
            try
            {
                string _maubenhphamgrouptype = " and maubenhphamgrouptype in (5,6) ";
                if (cboLoaiThuocVT.Text == "Thuốc")
                {
                    _maubenhphamgrouptype = " and maubenhphamgrouptype in (5) ";
                }
                else if (cboLoaiThuocVT.Text == "Vật tư")
                {
                    _maubenhphamgrouptype = " and maubenhphamgrouptype in (6) ";
                }

                var rowHandle = gridViewBaoCao.FocusedRowHandle;
               string _lstKhoaChonLayBC = " and departmentgroupid=" + gridViewBaoCao.GetRowCellValue(rowHandle, "departmentgroupid").ToString(); frmXemChiTietDonThuocVatTu _frm = new frmXemChiTietDonThuocVatTu(_lstKhoaChonLayBC, _maubenhphamgrouptype);
                _frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion


    }
}
