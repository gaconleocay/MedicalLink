using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MedicalLink.Utilities.GUIGridView;
using DevExpress.XtraSplashScreen;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;

namespace MedicalLink.ChucNang.ThongTinThucHienCLS
{
    public partial class ucThongTinThucHienCanLamSang : UserControl
    {
        #region Khai bao
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private long patientid, hosobenhanid, departmentgroupid;
        private string chandoancls_name, departmentname;
        #endregion
        public ucThongTinThucHienCanLamSang()
        {
            InitializeComponent();
        }

        #region Load
        private void ucThongTinThucHienCanLamSang_Load(object sender, EventArgs e)
        {
            try
            {
                dtThoiGianTu.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dtThoiGianDen.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                Load_DanhSachPhongThucHien();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void Load_DanhSachPhongThucHien()
        {
            try
            {
                var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 7).OrderBy(o => o.departmentname).ToList();
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    cboPhongThucHien.Properties.DataSource = lstDSPhong;
                    cboPhongThucHien.Properties.DisplayMember = "departmentname";
                    cboPhongThucHien.Properties.ValueMember = "departmentid";
                }
                if (lstDSPhong.Count == 1)
                {
                    cboPhongThucHien.EditValue = lstDSPhong[0].departmentid;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string maubenhphamstatus = "mbp.maubenhphamstatus<>0";
                if (cboPhongThucHien.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }
                classUserDepartment varselect = cboPhongThucHien.GetSelectedDataRow() as classUserDepartment;
                this.departmentgroupid = varselect.departmentgroupid;

                if (cboTrangThai.Text == "Chưa thực hiện")
                {
                    maubenhphamstatus = "mbp.maubenhphamstatus=1";
                }
                else if (cboTrangThai.Text == "Đang thực hiện")
                {
                    maubenhphamstatus = "mbp.maubenhphamstatus=16";
                }
                else if (cboTrangThai.Text == "Đã trả kết quả")
                {
                    maubenhphamstatus = "mbp.maubenhphamstatus=2";
                }

                gridControlDS_PhieuDichVu.DataSource = null;
                gridControlChiTiet.DataSource = null;

                string thoiGianTu = DateTime.ParseExact(dtThoiGianTu.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string thoiGianDen = DateTime.ParseExact(dtThoiGianDen.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string getDSMauBenPham = "select mbp.hosobenhanid, mbp.maubenhphamid, mbp.maubenhphamstatus, mbp.sothutunumber, mbp.sophieu as maubenhphamcode, mbp.patientpid as patientcode, mbp.patientid, mbp.vienphiid, hsba.patientname, '' as khan, degp.departmentgroupname, de.departmentname, mbp.maubenhphamdate, ncd.username as bacsichidinh, mbp.chandoan, (case when mbp.maubenhphamdate_thuchien<>'0001-01-01 00:00:00' then mbp.maubenhphamdate_thuchien end) as thoigiannhanphieu, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as thoigiantraphieu from maubenhpham mbp inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid left join (select departmentgroupid, departmentgroupname from departmentgroup) degp on degp.departmentgroupid=mbp.departmentgroupid left join (select departmentid, departmentname from department where departmenttype in (2,3,9,7)) de on de.departmentid=mbp.departmentid left join tools_tblnhanvien ncd on ncd.userhisid=mbp.userid where mbp.maubenhphamgrouptype=1 and " + maubenhphamstatus + " and mbp.maubenhphamdate between '" + thoiGianTu + "' and '" + thoiGianDen + "' and mbp.departmentid_des = " + cboPhongThucHien.EditValue.ToString() + " order by mbp.maubenhphamstatus, mbp.maubenhphamdate; ";

                DataTable dataMauBenhPham = condb.GetDataTable_HIS(getDSMauBenPham);
                gridControlDS_PhieuDichVu.DataSource = dataMauBenhPham;
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void gridViewDS_PhieuDichVu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "status_img")
                {
                    string val = Convert.ToString(gridViewDS_PhieuDichVu.GetRowCellValue(e.RowHandle, "maubenhphamstatus"));
                    if (val == "16") //da tiep nhan benh pham
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[0]);
                        e.Graphics.DrawImage(imageListstatus.Images[0], pos);
                    }
                    else if (val == "2") //da tra kq
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                        e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                    }

                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridControlDS_PhieuDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDS_PhieuDichVu.RowCount > 0)
                {
                    gridControlChiTiet.DataSource = null;

                    var rowHandle = gridViewDS_PhieuDichVu.FocusedRowHandle;
                    string maubenhphamid = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "maubenhphamid").ToString();
                    this.patientid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "patientid").ToString());
                    this.departmentname = gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "departmentname").ToString();
                    this.chandoancls_name= gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "chandoan").ToString();
                    this.hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDS_PhieuDichVu.GetRowCellValue(rowHandle, "hosobenhanid").ToString());
                    string sqlGetDichVuChiTiet = "select servicepriceid, medicalrecordid, servicestatus, maubenhphamid, servicepricecode, servicename, servicevalue, serviceremark1, serviceremark2 from service se inner join (select servicecode from service_ref where servicetype=0) sef on sef.servicecode=se.servicecode where se.maubenhphamid=" + maubenhphamid + "; ";
                    DataTable dataDVChiTiet = condb.GetDataTable_HIS(sqlGetDichVuChiTiet);
                    gridControlChiTiet.DataSource = dataDVChiTiet;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #region Click Nhap thong tin
        private void gridViewChiTiet_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Nhập thông tin thực hiện Cận lâm sàng");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(NhapThucHienCanLamSang_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        void NhapThucHienCanLamSang_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChiTiet.FocusedRowHandle;
                classNhapThongTinPTTT thongtinPTTT = new classNhapThongTinPTTT();

                thongtinPTTT.hosobenhanid = this.hosobenhanid;
                thongtinPTTT.departmentgroupid = this.departmentgroupid;
                thongtinPTTT.medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTiet.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                thongtinPTTT.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTiet.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                thongtinPTTT.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTiet.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                thongtinPTTT.patientid = this.patientid;
                thongtinPTTT.departmentname = this.departmentname;
                thongtinPTTT.chandoancls_name = this.chandoancls_name;

                frmNhapThongTinPTTT frmNhap = new frmNhapThongTinPTTT(thongtinPTTT);
                frmNhap.ShowDialog();
                //gridControlDS_PhieuDichVu_Click(null,null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void repositoryItemButton_NhapThucHien_Click(object sender, EventArgs e)
        {
            try
            {
                NhapThucHienCanLamSang_Click(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        private void gridViewDS_PhieuDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
    }
}
