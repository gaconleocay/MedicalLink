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
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        // internal string string_loctimkiem="";
        public ucSuaMaTenGiaDV()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

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

        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string[] dsdv_temp;
                string dsdv = "";
                string trangthaiVP = "";
                string loaivienphiid = "";
                string doituongbenhnhanid = "";
                string datetungay = "";
                string datedenngay = "";

                if ((mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)") || (cbbTrangThaiVP.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    gridControlDSDV.DataSource = null;
                    // Lấy dữ liệu danh sách dịch vụ nhập vào
                    dsdv_temp = mmeMaDV.Text.Split(',');
                    for (int i = 0; i < dsdv_temp.Length - 1; i++)
                    {
                        dsdv += "'" + dsdv_temp[i].ToString() + "',";
                    }
                    dsdv += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString() + "'";

                    // Lấy Tiêu chí trạng thai vien phi: vienphistatus
                    if (cbbTrangThaiVP.Text.Trim() == "Đang điều trị")
                    {
                        trangthaiVP = "and vp.vienphistatus=0";
                    }
                    else if (cbbTrangThaiVP.Text.Trim() == "Đóng BA nhưng chưa duyệt VP")
                    {
                        trangthaiVP = " and vp.vienphidate_ravien != '0001-01-01 00:00:00' and (vp.vienphistatus_vp IS NULL or vp.vienphistatus_vp=0) and vp.vienphistatus<>2 ";
                    }
                    else if (cbbTrangThaiVP.Text.Trim() == "Đã duyệt viện phí")
                    {
                        trangthaiVP = " and vp.vienphistatus_vp=1 ";
                    }

                    // Lấy loaivienphiid
                    if (cbbLoaiBA.Text == "Ngoại trú")
                    {
                        loaivienphiid = "and vp.loaivienphiid=1 ";
                    }
                    else if (cbbLoaiBA.Text == "Nội trú")
                    {
                        loaivienphiid = "and vp.loaivienphiid=0 ";
                    }
                    else
                    {
                        loaivienphiid = " ";
                    }

                    // Lấy trường đối tượng BN: loaidoituong
                    if (chkBHYT.Checked == true && chkVP.Checked == false)
                    {
                        doituongbenhnhanid = "and vp.doituongbenhnhanid=1 ";
                    }
                    else if (chkBHYT.Checked == false && chkVP.Checked == true)
                    {
                        doituongbenhnhanid = "and vp.doituongbenhnhanid<>1 ";
                    }
                    else if (chkBHYT.Checked == true && chkVP.Checked == true)
                    {
                        doituongbenhnhanid = " ";
                    }

                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    string sqlquerry = "SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepriceid) as stt, ser.servicepriceid as servicepriceid, ser.maubenhphamid as maphieu, vp.patientid as mabn,vp.vienphiid as mavp, hsba.patientname as tenbn, degp.departmentgroupname as tenkhoachidinh, de.departmentname as tenphongchidinh, ser.servicepricecode as madv, ser.servicepricename as tendv_yc, ser.servicepricename_bhyt as tendv_bhyt, ser.servicepricename_nhandan as tendv_vp, ser.servicepricename_nuocngoai as tendv_nnn, ser.servicepricemoney as dongia, ser.servicepricemoney_nhandan as dongiavienphi, ser.servicepricemoney_bhyt as dongiabhyt, ser.servicepricemoney_nuocngoai as dongiannn, ser.servicepricedate as thoigianchidinh, ser.soluong as soluong, vp.vienphidate as thoigianvaovien, (case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, ser.huongdansudung FROM serviceprice ser inner join vienphi vp on ser.vienphiid=vp.vienphiid inner join hosobenhan hsba on vp.hosobenhanid=hsba.hosobenhanid inner join departmentgroup degp on ser.departmentgroupid=degp.departmentgroupid inner join department de on ser.departmentid=de.departmentid and de.departmenttype in (0,2,3,9) WHERE ser.servicepricecode in (" + dsdv + ") and ser.servicepricedate >= '" + datetungay + "' and ser.servicepricedate <= '" + datedenngay + "' " + trangthaiVP + loaivienphiid + doituongbenhnhanid + " ;";

                    // string_loctimkiem= "serviceprice.servicepricedate > '" + datetungay + "' and serviceprice.servicepricedate < '" + datedenngay + "' " + trangthaiVP + loaivienphiid + doituongbenhnhanid + " ";

                    DataView dv = new DataView(condb.GetDataTable(sqlquerry));
                    gridControlDSDV.DataSource = dv;

                    if (gridViewDSDV.RowCount == 0)
                    {
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

        private void ucBCDSBNSDdv_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        //xuat ra excel
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

        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
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
        void itemSuaGiaMotDV_Click(object sender, EventArgs e)
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
        void itemSuaGiaNhieuDV_Click(object sender, EventArgs e)
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
        void itemSuaTenMotDV_Click(object sender, EventArgs e)
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
        void itemSuaTenNhieuDV_Click(object sender, EventArgs e)
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




    }
}
