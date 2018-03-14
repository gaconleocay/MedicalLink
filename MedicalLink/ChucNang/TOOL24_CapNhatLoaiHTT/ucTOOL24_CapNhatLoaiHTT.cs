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
using MedicalLink.ClassCommon;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucTOOL24_CapNhatLoaiHTT : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }

        #endregion
        public ucTOOL24_CapNhatLoaiHTT()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        #region Load
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaDV.Text.Length == 0)
            {
                mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*";
                mmeMaDV.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*")
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
                string _danhsachDichVu = "";
                string danhsachDichVu_In = "";
                string danhsachDichVu_Like = "";
                string _vienphistatus = "";
                string _tieuchi_vp = "";
                string _tieuchi_ser = "";
                string _doituongbenhnhanid = "";
                string _loaidoituong = "";

                if (mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    this.dataBaoCao = new DataTable();
                    string _datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string _datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    // Lấy dữ liệu danh sách dịch vụ nhập vào
                    string danhsachtimkiem = mmeMaDV.Text.Replace("*", "%");
                    string[] dsdv_temp = danhsachtimkiem.Split(',');
                    for (int i = 0; i < dsdv_temp.Length; i++)
                    {
                        if (dsdv_temp[i].Contains("%"))
                        {
                            danhsachDichVu_Like += "'" + dsdv_temp[i].ToString().Trim() + "',";
                        }
                        else
                        {
                            danhsachDichVu_In += "'" + dsdv_temp[i].ToString().Trim() + "',";
                        }
                    }
                    //
                    if (danhsachDichVu_Like != "")
                    {
                        string kytucuoicung = danhsachDichVu_Like.Substring(danhsachDichVu_Like.Length - 1, 1);
                        if (kytucuoicung == ",")
                        {
                            danhsachDichVu_Like = danhsachDichVu_Like.Substring(0, danhsachDichVu_Like.Length - 1);
                        }
                        danhsachDichVu_Like = " and servicepricecode LIKE ANY(ARRAY[" + danhsachDichVu_Like + "]) ";

                    }
                    if (danhsachDichVu_In != "")
                    {
                        string kytucuoicung = danhsachDichVu_In.Substring(danhsachDichVu_In.Length - 1, 1);
                        if (kytucuoicung == ",")
                        {
                            danhsachDichVu_In = danhsachDichVu_In.Substring(0, danhsachDichVu_In.Length - 1);
                        }

                        danhsachDichVu_In = " and servicepricecode in (" + danhsachDichVu_In + ") ";
                    }

                    if (danhsachDichVu_Like != "" && danhsachDichVu_In != "")
                    {
                        _danhsachDichVu = danhsachDichVu_Like + " or " + danhsachDichVu_In;
                    }
                    else
                    {
                        _danhsachDichVu = danhsachDichVu_Like + danhsachDichVu_In;
                    }

                    //trạng thai vien phi: vienphistatus
                    if (cboTrangThaiVP.Text.Trim() == "Đang điều trị")
                    {
                        _vienphistatus = "and vienphistatus=0";
                    }
                    else if (cboTrangThaiVP.Text.Trim() == "Đóng BA nhưng chưa duyệt VP")
                    {
                        _vienphistatus = " and vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 ";
                    }
                    else if (cboTrangThaiVP.Text.Trim() == "Đã duyệt viện phí")
                    {
                        _vienphistatus = " and vienphistatus_vp=1 ";
                    }
                    //Tieu chi cboTieuChi
                    if (cboTieuChi.Text == "Theo thời gian chỉ định")
                    {
                        _tieuchi_ser = " and servicepricedate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo thời gian vào viện")
                    {
                        _tieuchi_vp = " and vienphidate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo thời gian ra viện")
                    {
                        _tieuchi_vp = " and vienphidate_ravien between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo thời gian duyệt VP")
                    {
                        _tieuchi_vp = " and duyet_ngayduyet_vp between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    //Doi tuong thanh toan
                    if (cboDoiTuongThanhToan.Text == "BHYT")
                    {
                        _loaidoituong = " and loaidoituong=0 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Viện phí")
                    {
                        _loaidoituong = " and loaidoituong=1 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Đi kèm")
                    {
                        _loaidoituong = " and loaidoituong=2 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Yêu cầu")
                    {
                        _loaidoituong = " and loaidoituong=3 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "BHYT+YC")
                    {
                        _loaidoituong = " and loaidoituong=4 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Hao phí giường, CK")
                    {
                        _loaidoituong = " and loaidoituong=5 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "BHYT+Phụ thu")
                    {
                        _loaidoituong = " and loaidoituong=6 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Hao phí PTTT")
                    {
                        _loaidoituong = " and loaidoituong=7 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Thanh toán riêng")
                    {
                        _loaidoituong = " and loaidoituong=20 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Đối tượng khác")
                    {
                        _loaidoituong = " and loaidoituong=8 ";
                    }
                    else if (cboDoiTuongThanhToan.Text == "Hao phí khác")
                    {
                        _loaidoituong = " and loaidoituong=9 ";
                    }
                    //Doi tuogn benh nhan
                    if (cboDoiTuongBenhNhan.Text == "Viện phí")
                    {
                        _doituongbenhnhanid = " and doituongbenhnhanid<>1 ";
                    }
                    else if (cboDoiTuongBenhNhan.Text == "BHYT")
                    {
                        _doituongbenhnhanid = " and doituongbenhnhanid=1 ";
                    }

                    //
                    string _sqlquerry = @"SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepricename) as stt,                        ser.servicepriceid,                        ser.maubenhphamid,
                        vp.patientid, 
                        vp.vienphiid, 
                        hsba.patientname,                        (case when vp.vienphistatus=0 then 'Đang điều trị'		                          else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán'		                          else 'Chưa thanh toán' end) end) as vienphistatus,                        kcd.departmentgroupname as khoachidinh,                         pcd.departmentname as phongchidinh, 		  
                        ser.servicepricecode, 
                        ser.servicepricename,                        ser.servicepricename_bhyt,                        ser.servicepricename_nhandan,                        (case ser.loaidoituong		                        when 0 then 'BHYT'		                        when 1 then 'Viện phí'		                        when 2 then 'Đi kèm'		                        when 3 then 'Yêu cầu'		                        when 4 then 'BHYT+YC'		                        when 5 then 'Hao phí giường, CK'		                        when 6 then 'BHYT+phụ thu'		                        when 7 then 'Hao phí PTTT'		                        when 8 then 'Đối tượng khác'		                        when 9 then 'Hao phí khác'		                        when 20 then 'Thanh toán riêng'		                        end) as loaidoituong,
                        ser.servicepricemoney,                         ser.servicepricemoney_bhyt,                        ser.servicepricemoney_nhandan,                        ser.servicepricemoney_nuocngoai,		
                        ser.servicepricedate, 
                        (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong, 
                        (case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as loaiphieu, 
                        vp.vienphidate, 
                        (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, 
                        (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp                        FROM (select * from serviceprice where 1=1 " + _danhsachDichVu + _tieuchi_ser + _loaidoituong + ") ser  inner join (select vienphiid, hosobenhanid, patientid, vienphistatus, vienphistatus_vp,vienphidate, vienphidate_ravien, duyet_ngayduyet_vp from vienphi where 1 = 1 " + _vienphistatus + _tieuchi_vp + _doituongbenhnhanid + ") vp on vp.vienphiid = ser.vienphiid inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid = vp.hosobenhanid inner join(select departmentgroupid, departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid = ser.departmentgroupid inner join(select departmentid, departmentname from department where departmenttype in (2, 3, 6, 7, 9)) pcd on pcd.departmentid = ser.departmentid; ";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sqlquerry);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDSDV.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDSDV.DataSource = null;
                        this.dataBaoCao = new DataTable();
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

        #region Xuat Exxcel
        //xuat ra excel
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);

                    string fileTemplatePath = "TOOLS_24_CapNhatLoaiHinhThanhToan.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataBaoCao);
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Cusstom
        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        #endregion

        #region Events
        private void gridViewDSDV_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                e.Menu.Items.Clear();

                DXSubMenuItem menuChuyenTT = new DXSubMenuItem("Chuyển loại hình thanh toán"); // caption menu
                menuChuyenTT.Image = imMenu.Images[1]; // icon cho menu
                e.Menu.Items.Add(menuChuyenTT);

                DXMenuItem itemBHYT = new DXMenuItem("BHYT");
                itemBHYT.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemBHYT);
                itemBHYT.Click += new EventHandler(itemBHYT_Click);

                DXMenuItem itemVP = new DXMenuItem("Viện phí");
                itemVP.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemVP);
                itemVP.Click += new EventHandler(itemVP_Click);

                DXMenuItem itemDKPTTT = new DXMenuItem("Đi kèm");
                itemDKPTTT.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemDKPTTT);
                itemDKPTTT.Click += new EventHandler(itemDKPTTT_Click);

                DXMenuItem itemYC = new DXMenuItem("Yêu cầu");
                itemYC.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemYC);
                itemYC.Click += new EventHandler(itemYC_Click);

                DXMenuItem itemBHYTYC = new DXMenuItem("BHYT+YC");
                itemBHYTYC.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemBHYTYC);
                itemBHYTYC.Click += new EventHandler(itemBHYTYC_Click);

                DXMenuItem itemHaoPhi = new DXMenuItem("Hao phí giường, CK");
                itemHaoPhi.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemHaoPhi);
                itemHaoPhi.Click += new EventHandler(itemHaoPhi_Click);

                DXMenuItem itemBHYTPhuThu = new DXMenuItem("BHYT+phụ thu");
                itemBHYTPhuThu.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemBHYTPhuThu);
                itemBHYTPhuThu.Click += new EventHandler(itemBHYTPhuThu_Click);

                DXMenuItem itemHPPTTT = new DXMenuItem("Hao phí PTTT");
                itemHPPTTT.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemHPPTTT);
                itemHPPTTT.Click += new EventHandler(itemHPPTTT_Click);

                DXMenuItem itemDTKhac = new DXMenuItem("Đối tượng khác");
                itemDTKhac.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemDTKhac);
                itemDTKhac.Click += new EventHandler(itemDTKhac_Click);

                DXMenuItem itemHPKhac = new DXMenuItem("Hao phí khác");
                itemHPKhac.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemHPKhac);
                itemHPKhac.Click += new EventHandler(itemHPKhac_Click);

                DXMenuItem itemTTRieng = new DXMenuItem("Thanh toán riêng");
                itemTTRieng.Image = imMenu.Images[0];
                menuChuyenTT.Items.Add(itemTTRieng);
                itemTTRieng.Click += new EventHandler(itemTTRieng_Click);
            }
        }
        private void itemBHYT_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(0);
        }
        private void itemVP_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(1);
        }
        private void itemDKPTTT_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(2);
        }
        private void itemYC_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(3);
        }
        private void itemBHYTYC_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(4);
        }
        private void itemHaoPhi_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(5);
        }
        private void itemBHYTPhuThu_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(6);
        }
        private void itemHPPTTT_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(7);
        }
        private void itemDTKhac_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(8);
        }
        private void itemHPKhac_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(9);
        }
        private void itemTTRieng_Click(object sender, EventArgs e)
        {
            SuaDoiTuongThanhToan_Process(20);
        }

        #endregion

        #region Process
        private void SuaDoiTuongThanhToan_Process(int _loaidoituong)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn sửa đối tượng thanh toán?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    List<ServicepriceIDDTO> lstServicepriceids = GetIdCollection();
                    if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET loaidoituong='" + _loaidoituong + "' WHERE servicepriceid in (" + ConvertListObjToListString(lstServicepriceids) + ");";

                        //Log vào DB
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Cập nhật đối tượng thanh toán sang loaidoituong=" + _loaidoituong + " WHERE servicepriceid in (" + ConvertListObjToListString(lstServicepriceids).Replace("'", "") + ")' ,'" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_24');";

                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                            MessageBox.Show("Cập nhật đối tượng thanh toán thành công SL=" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnTimKiem_Click(null, null);
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                catch (Exception ex)
                {
                    MedicalLink.Base.Logging.Error(ex);
                }
            }
        }

        private List<ServicepriceIDDTO> GetIdCollection()
        {
            List<ServicepriceIDDTO> IDs = new List<ServicepriceIDDTO>();
            foreach (var item_index in gridViewDSDV.GetSelectedRows())
            {
                ServicepriceIDDTO _serviceID = new ServicepriceIDDTO();
                _serviceID.servicepriceid = gridViewDSDV.GetRowCellValue(item_index, "servicepriceid").ToString();
                IDs.Add(_serviceID);
            }
            return IDs;
        }

        private string ConvertListObjToListString(List<ServicepriceIDDTO> IDs)
        {
            string query = "";
            try
            {
                int number = 0;
                foreach (var item in IDs)
                {
                    if (number > 0)
                    {
                        query = query + "," + "'" + item.servicepriceid + "'";
                    }
                    else
                    {
                        query = "'" + item.servicepriceid + "'";
                    }
                    number++;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return query;
        }

        #endregion

    }
}
