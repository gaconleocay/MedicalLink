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
using System.IO;
using DevExpress.XtraSplashScreen;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaMaTenGiaDV : UserControl
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion
        public ucSuaMaTenGiaDV()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        #region Load
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaDV.Text.Length == 0)
            {
                mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)";
                mmeMaDV.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)")
            {
                mmeMaDV.Text = "";
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
        }
        private void ucBCDSBNSDdv_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string[] dsdv_temp;
                string _dsdichvu_ser = " and servicepricecode in (";
                string _trangthaiVP = "";
                string _loaivienphiid = "";
                string _doituongbenhnhanid = "";
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";

                if ((mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)"))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    // Lấy dữ liệu danh sách dịch vụ nhập vào
                    dsdv_temp = mmeMaDV.Text.Split(',');
                    for (int i = 0; i < dsdv_temp.Length - 1; i++)
                    {
                        _dsdichvu_ser += "'" + dsdv_temp[i].ToString() + "',";
                    }
                    _dsdichvu_ser += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString() + "') ";

                    // Lấy Tiêu chí trạng thai vien phi: vienphistatus
                    if (cbbTrangThaiVP.Text.Trim() == "Đang điều trị")
                    {
                        _trangthaiVP = " and vienphistatus=0 ";
                    }
                    else if (cbbTrangThaiVP.Text.Trim() == "Đóng BA nhưng chưa duyệt VP")
                    {
                        _trangthaiVP = " and vienphidate_ravien != '0001-01-01 00:00:00' and (vienphistatus_vp IS NULL or vienphistatus_vp=0) and vienphistatus<>2 ";
                    }
                    else if (cbbTrangThaiVP.Text.Trim() == "Đã duyệt viện phí")
                    {
                        _trangthaiVP = " and vienphistatus>0 and vienphistatus_vp=1 ";
                    }
                    // Lấy loaivienphiid
                    if (cbbLoaiBA.Text == "Ngoại trú")
                    {
                        _loaivienphiid = " and loaivienphiid=1 ";
                    }
                    else if (cbbLoaiBA.Text == "Nội trú")
                    {
                        _loaivienphiid = " and loaivienphiid=0 ";
                    }
                    //doi tuong BN
                    if (cboDoiTuongBN.Text == "BHYT")
                    {
                        _doituongbenhnhanid = " and doituongbenhnhanid=1 ";
                    }
                    else if (cboDoiTuongBN.Text == "Viện phí")
                    {
                        _doituongbenhnhanid = " and doituongbenhnhanid<>1 ";
                    }
                    //tieu chi
                    string _datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string _datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    if (cboTieuChi.Text == "Theo ngày chỉ định")
                    {
                        _tieuchi_ser = " and servicepricedate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày vào viện")
                    {
                        _tieuchi_vp = " and vienphidate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày ra viện")
                    {
                        _tieuchi_vp = " and vienphidate_ravien between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                    {
                        _tieuchi_vp = " and duyet_ngayduyet_vp between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }

                    string sqlquerry = "SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepriceid) as stt, ser.servicepriceid, ser.maubenhphamid as maphieu, vp.patientid as mabn, vp.vienphiid as mavp, hsba.patientname as tenbn, degp.departmentgroupname as tenkhoachidinh, de.departmentname as tenphongchidinh, ser.servicepricecode as madv, ser.servicepricename as tendv_yc, ser.servicepricename_bhyt as tendv_bhyt, ser.servicepricename_nhandan as tendv_vp, ser.servicepricename_nuocngoai as tendv_nnn, ser.servicepricemoney as dongia, ser.servicepricemoney_nhandan as dongiavienphi, ser.servicepricemoney_bhyt as dongiabhyt, ser.servicepricemoney_nuocngoai as dongiannn, ser.servicepricedate as thoigianchidinh, ser.soluong as soluong, vp.vienphidate as thoigianvaovien, (case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as trangthai, ser.huongdansudung FROM (select * from serviceprice where 1=1 " + _tieuchi_ser + _dsdichvu_ser + ") ser inner join (select * from vienphi where 1=1 " + _tieuchi_vp + _trangthaiVP + _loaivienphiid + _doituongbenhnhanid + ") vp on ser.vienphiid=vp.vienphiid inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join departmentgroup degp on ser.departmentgroupid=degp.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (0,2,3,9)) de on ser.departmentid=de.departmentid;";

                    DataTable _dataBaoCao = condb.GetDataTable_HIS(sqlquerry);
                    if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDSDV.DataSource = _dataBaoCao;
                    }
                    else
                    {
                        gridControlDSDV.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Events
        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDSDV.RowCount > 0)
            {
                try
                {
                    using (SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                        if (saveDialog.ShowDialog() != DialogResult.Cancel)
                        {
                            string exportFilePath = saveDialog.FileName;
                            string fileExtenstion = new FileInfo(exportFilePath).Extension;

                            switch (fileExtenstion)
                            {
                                case ".xls":
                                    gridControlDSDV.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDSDV.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDSDV.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDSDV.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDSDV.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDSDV.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                            frmthongbao.Show();

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                }
            }
            else
            {
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                frmthongbao.Show();
            }

        }
        private void gridViewDSDV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                e.Menu.Items.Clear();

                DXMenuItem itemSuaGiaMotDV = new DXMenuItem("Sửa giá 1 row đang chọn"); // caption menu
                itemSuaGiaMotDV.Image = imMenu.Images[0]; // icon cho menu
                //itemChuyenHanhChinh.Shortcut = Shortcut.F6; // phím tắt
                itemSuaGiaMotDV.Click += new EventHandler(itemSuaGiaMotDV_Click);// thêm sự kiện click
                e.Menu.Items.Add(itemSuaGiaMotDV);

                //DXMenuItem itemSuaGiaNhieuDV = new DXMenuItem("Sửa giá tất cả dịch vụ có mã đang chọn");
                //itemSuaGiaNhieuDV.Image = imMenu.Images[1];
                ////itemXoaToanBA.Shortcut = Shortcut.F6;
                //itemSuaGiaNhieuDV.Click += new EventHandler(itemSuaGiaNhieuDV_Click);
                //e.Menu.Items.Add(itemSuaGiaNhieuDV);
                ////itemSuaGiaNhieuDV.BeginGroup = true;

                DXMenuItem itemSuaTenMotDV = new DXMenuItem("Sửa tên 1 row đang chọn");
                itemSuaTenMotDV.Image = imMenu.Images[2];
                //itemXoaToanBA.Shortcut = Shortcut.F6;
                itemSuaTenMotDV.Click += new EventHandler(itemSuaTenMotDV_Click);
                e.Menu.Items.Add(itemSuaTenMotDV);
                itemSuaTenMotDV.BeginGroup = true;

                DXMenuItem itemSuaTenNhieuDV = new DXMenuItem("Sửa tên tất cả dịch vụ có mã đang chọn");
                itemSuaTenNhieuDV.Image = imMenu.Images[3];
                //itemXoaToanBA.Shortcut = Shortcut.F6;
                itemSuaTenNhieuDV.Click += new EventHandler(itemSuaTenNhieuDV_Click);
                e.Menu.Items.Add(itemSuaTenNhieuDV);
            }
        }
        //Sửa giá 1 row đang chọn
        private void itemSuaGiaMotDV_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDSDV.FocusedRowHandle;
                string trangthai_kt = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "trangthai").ToString());
                int servicepriceid = Convert.ToInt32(gridViewDSDV.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                string madv = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "madv").ToString());
                string tendv = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "tendv_bhyt").ToString());
                int mabn = Convert.ToInt32(gridViewDSDV.GetRowCellValue(rowHandle, "mabn").ToString());
                int mavp = Convert.ToInt32(gridViewDSDV.GetRowCellValue(rowHandle, "mavp").ToString());
                string tenbn = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "tenbn").ToString());
                int maphieu = Convert.ToInt32(gridViewDSDV.GetRowCellValue(rowHandle, "maphieu").ToString());
                string dongia = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "dongia").ToString());
                string dongiavienphi = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "dongiavienphi").ToString());
                string dongiabhyt = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "dongiabhyt").ToString());
                string dongiannn = Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "dongiannn").ToString());

                if (trangthai_kt != "Đã duyệt VP")
                {
                    //truyền biến sang bên form thực hiện
                    MedicalLink.ChucNang.frmSuaGiaDV_TH frmsuagiadv_th = new MedicalLink.ChucNang.frmSuaGiaDV_TH(servicepriceid, tendv, madv, mabn, mavp, tenbn, maphieu, dongia, dongiavienphi, dongiabhyt, dongiannn);
                    frmsuagiadv_th.ShowDialog();
                    gridControlDSDV.DataSource = null;
                    btnTimKiem_Click(null, null);
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_NHAN_DA_DUYET_VIEN_PHI);
                    frmthongbao.Show();
                }

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        //Sửa giá tất cả dịch vụ có mã đang chọn
        private void itemSuaGiaNhieuDV_Click(object sender, EventArgs e)
        {

            try
            {
                MessageBox.Show("Chức năng đang phát triển !", "Thông báo");
                //timerThongBao.Start();
                //lblThongBao.Visible = true;
                //lblThongBao.Text = "Chức năng nguy hiểm, nghiêm cấm trẻ em dưới 18 tuổi :D !";
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        //Sửa tên 1 row đang chọn
        private void itemSuaTenMotDV_Click(object sender, EventArgs e)
        {
            try
            {
                int kieusua = 1;
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDSDV.FocusedRowHandle;
                int trangthai_kt = 0;
                if (Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "trangthai").ToString()) == "Đã duyệt VP")
                {
                    trangthai_kt = 1;
                }
                //truyền biến sang bên form thực hiện
                MedicalLink.ChucNang.frmSuaTenDV_TH frmsuatendv_th = new MedicalLink.ChucNang.frmSuaTenDV_TH(this, kieusua, trangthai_kt);
                frmsuatendv_th.ShowDialog();
                gridControlDSDV.DataSource = null;
                btnTimKiem_Click(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        //Sửa tên tất cả dịch vụ có mã đang chọn
        private void itemSuaTenNhieuDV_Click(object sender, EventArgs e)
        {
            try
            {
                int kieusua = 2;
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDSDV.FocusedRowHandle;
                int trangthai_kt = 0;
                if (Convert.ToString(gridViewDSDV.GetRowCellValue(rowHandle, "trangthai").ToString()) == "Đã duyệt VP")
                {
                    trangthai_kt = 1;
                }

                //truyền biến sang bên form thực hiện
                MedicalLink.ChucNang.frmSuaTenDV_TH frmsuatendv_th = new MedicalLink.ChucNang.frmSuaTenDV_TH(this, kieusua, trangthai_kt);
                frmsuatendv_th.ShowDialog();
                gridControlDSDV.DataSource = null;
                btnTimKiem_Click(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }


        #endregion





    }
}
