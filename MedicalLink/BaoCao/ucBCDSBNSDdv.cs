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
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;

namespace MedicalLink.ChucNang
{
    public partial class ucBCDSBNSDdv : UserControl
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //private DataTable dataBCBXuatThuoc { get; set; }
        private string DanhSachDichVu { get; set; }
        private List<ServicepriceDTO> lstDanhMucDichVu { get; set; }
        #endregion
        public ucBCDSBNSDdv()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            txtDSMaDichVu.ForeColor = SystemColors.GrayText;
            txtDSMaDichVu.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*";
            this.txtDSMaDichVu.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.txtDSMaDichVu.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        #region Load
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (txtDSMaDichVu.Text.Length == 0)
            {
                txtDSMaDichVu.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*";
                txtDSMaDichVu.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (txtDSMaDichVu.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*")
            {
                txtDSMaDichVu.Text = "";
                txtDSMaDichVu.ForeColor = SystemColors.WindowText;
            }
        }

        private void ucBCDSBNSDdv_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhMucDichVu();
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void LoadDanhMucDichVu()
        {
            try
            {
                this.lstDanhMucDichVu = new List<ServicepriceDTO>();
                string _sqlDMDichVu = @"select servicepricegroupcode,servicepricecode,1 as loaidichvu
                        from servicepriceref
                        where servicegrouptype in (1,2,3,4,11)
                        UNION ALL
                        select medicinegroupcode as servicepricegroupcode,medicinecode as servicepricecode,2 as loaidichvu
                        from medicine_ref
                        where isremove=0;";
                DataTable _dataDMDV = condb.GetDataTable_HIS(_sqlDMDichVu);
                if (_dataDMDV.Rows.Count > 0)
                {
                    this.lstDanhMucDichVu = Utilities.Util_DataTable.DataTableToList<ServicepriceDTO>(_dataDMDV);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi_ser = "";
                string tieuchi_vp = "";
                string tieuchi_hsba = "";
                string loaivienphiid = "";
                string doituongbenhnhanid = "";
                string _bhyt_groupcode = "";

                if ((txtDSMaDichVu.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*"))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    // Lấy từ ngày, đến ngày
                    string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    //Lay danh sach dich vu
                    LayDanhSachDichVu();

                    // Lấy Tiêu chí thời gian: tieuchi
                    if (cbbTieuChi.Text == "Theo ngày chỉ định")
                    {
                        tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày vào viện")
                    {
                        tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                        tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày ra viện")
                    {
                        tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                        tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                    {
                        tieuchi_vp = " and vienphistatus<>0 and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")//theo ngay duyet BHYT
                    {
                        tieuchi_vp = " and vienphistatus_bh=1 and duyet_ngayduyet between '" + datetungay + "' and '" + datedenngay + "' ";
                    }

                    // Lấy loaivienphiid
                    if (cbbLoaiBA.Text == "Ngoại trú")
                    {
                        loaivienphiid = " and loaivienphiid=1 ";
                    }
                    else if (cbbLoaiBA.Text == "Nội trú")
                    {
                        loaivienphiid = " and loaivienphiid=0 ";
                    }

                    // Lấy trường đối tượng BN loaidoituong
                    if (cboDoiTuongBN.Text == "Viện phí")
                    {
                        doituongbenhnhanid = "and doituongbenhnhanid<>1 ";
                    }
                    else if (cboDoiTuongBN.Text == "BHYT")
                    {
                        doituongbenhnhanid = "and doituongbenhnhanid=1 ";
                    }

                    //nhom dich vu
                    if (cboNhomDichVu.Text == "Dịch vụ kỹ thuật")
                    {
                        _bhyt_groupcode = " and bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') ";
                    }
                    else if (cboNhomDichVu.Text == "Thuốc và vật tư")
                    {
                        _bhyt_groupcode = " and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') ";
                    }

                    // Thực thi câu lệnh SQL

                    string _sqlquerry = @"SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepricecode,vp.duyet_ngayduyet_vp) as stt, vp.patientid as mabn, vp.vienphiid as mavp, hsba.patientname as tenbn, degp.departmentgroupname as tenkhoa, de.departmentname as tenphong, ser.servicepricecode as madv, ser.servicepricename as tendv, ser.dongia, ser.servicepricedate as thoigianchidinh, (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong, ((case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)*ser.dongia) as thanhtien, (case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' end) trangthaithutien, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, (case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as loaiphieu, vp.vienphidate as thoigianvaovien, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as thoigianravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai, vp.chandoanravien_code as benhchinh_code, vp.chandoanravien as benhchinh_name, vp.chandoanravien_kemtheo_code as benhkemtheo_code, vp.chandoanravien_kemtheo as benhkemtheo_name, bhyt.bhytcode as bhytcode, (case ser.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end) as bhyt_groupcode, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, (case ser.thuockhobanle when 0 then '' else 'Đơn nhà thuốc' end) as thuockhobanle FROM (select vienphiid,departmentgroupid,departmentid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia, servicepricedate,maubenhphamphieutype,soluong,bhyt_groupcode,loaidoituong,thuockhobanle from serviceprice where " + this.DanhSachDichVu + tieuchi_ser + _bhyt_groupcode + ") ser INNER JOIN (select patientid,vienphiid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,duyet_ngayduyet,vienphistatus,vienphistatus_vp,vienphistatus_bh,chandoanravien_code,chandoanravien,chandoanravien_kemtheo_code,chandoanravien_kemtheo,departmentgroupid,departmentid,bhytid,doituongbenhnhanid,loaivienphiid from vienphi where 1=1 " + tieuchi_vp + loaivienphiid + doituongbenhnhanid + ") vp ON ser.vienphiid=vp.vienphiid INNER JOIN (select hosobenhanid,patientname from hosobenhan where 1=1 " + tieuchi_hsba + ") hsba ON vp.hosobenhanid=hsba.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) degp ON vp.departmentgroupid=degp.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de ON vp.departmentid=de.departmentid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid INNER JOIN (select bhytid,bhytcode from bhyt) bhyt ON bhyt.bhytid=vp.bhytid;";

                    DataTable _dataBCBXuatThuoc = condb.GetDataTable_HIS(_sqlquerry);

                    if (_dataBCBXuatThuoc.Rows.Count > 0)
                    {
                        gridControlDSDV.DataSource = _dataBCBXuatThuoc;
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
            try
            {
                if (gridViewDSDV.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);

                    string fileTemplatePath = "BC_01_BenhNhanSuDungDichVu.xlsx";

                    DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDSDV);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
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

        #region Custom
        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Process
        private void LayDanhSachDichVu()
        {
            try
            {
                this.DanhSachDichVu = "";
                string danhsachDichVu_In = "";
                string danhsachDichVu_Like = "";

                // Lấy dữ liệu danh sách dịch vụ nhập vào

                if (chkTheoNhomDV.Checked)
                {
                    string danhsachtimkiem = txtDSMaDichVu.Text.Replace("*", "");
                    string[] dsdv_temp = danhsachtimkiem.Split(',');
                    for (int i = 0; i < dsdv_temp.Length; i++)
                    {
                        LayDanhSachDichVuTheoNhom_Process(dsdv_temp[i].ToString().Trim());
                        if (this.DanhSachDichVu != "")
                        {
                            string kytucuoicung = this.DanhSachDichVu.Substring(this.DanhSachDichVu.Length - 1, 1);
                            if (kytucuoicung == ",")
                            {
                                this.DanhSachDichVu = this.DanhSachDichVu.Substring(0, this.DanhSachDichVu.Length - 1);
                            }
                            this.DanhSachDichVu = " servicepricecode in (" + this.DanhSachDichVu + ") ";
                        }
                    }
                }
                else
                {
                    string danhsachtimkiem = txtDSMaDichVu.Text.Replace("*", "%");
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
                    //xoa ky tu cuoi cung
                    if (danhsachDichVu_Like != "")
                    {
                        string kytucuoicung = danhsachDichVu_Like.Substring(danhsachDichVu_Like.Length - 1, 1);
                        if (kytucuoicung == ",")
                        {
                            danhsachDichVu_Like = danhsachDichVu_Like.Substring(0, danhsachDichVu_Like.Length - 1);
                        }
                        danhsachDichVu_Like = " servicepricecode LIKE ANY(ARRAY[" + danhsachDichVu_Like + "]) ";

                    }
                    if (danhsachDichVu_In != "")
                    {
                        string kytucuoicung = danhsachDichVu_In.Substring(danhsachDichVu_In.Length - 1, 1);
                        if (kytucuoicung == ",")
                        {
                            danhsachDichVu_In = danhsachDichVu_In.Substring(0, danhsachDichVu_In.Length - 1);
                        }

                        danhsachDichVu_In = " servicepricecode in (" + danhsachDichVu_In + ") ";
                    }

                    //
                    if (danhsachDichVu_Like != "" && danhsachDichVu_In != "")
                    {
                        this.DanhSachDichVu = danhsachDichVu_Like + " or " + danhsachDichVu_In;
                    }
                    else
                    {
                        this.DanhSachDichVu = danhsachDichVu_Like + danhsachDichVu_In;
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void LayDanhSachDichVuTheoNhom_Process(string _manhomDV)
        {
            try
            {
                List<ServicepriceDTO> _lstDichVu = this.lstDanhMucDichVu.Where(o => o.servicepricegroupcode == _manhomDV).ToList();
                if (_lstDichVu != null && _lstDichVu.Count > 0)
                {
                    foreach (var item in _lstDichVu)
                    {
                        //this.DanhSachDichVu += "'" + item.servicepricecode + "',";
                        LayDanhSachDichVuTheoNhom_Process(item.servicepricecode);
                    }
                }
                else
                {
                    this.DanhSachDichVu += "'" + _manhomDV + "',";
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        #endregion
    }
}
