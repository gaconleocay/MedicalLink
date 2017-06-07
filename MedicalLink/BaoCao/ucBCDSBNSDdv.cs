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

namespace MedicalLink.ChucNang
{
    public partial class ucBCDSBNSDdv : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBCBXuatThuoc { get; set; }

        public ucBCDSBNSDdv()
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
                string tieuchi="";
                string loaivienphiid = "";
                string doituongbenhnhanid="";
                string datetungay = "";
                string datedenngay = "";

                if ((mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)") || (cbbTieuChi.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
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
                        dsdv += "'" + dsdv_temp[i].ToString().Trim() + "',";
                    }
                    dsdv += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString().Trim() + "'";

                    // Lấy Tiêu chí thời gian: tieuchi
                    if (cbbTieuChi.Text == "Theo ngày chỉ định")
                    {
                        tieuchi = "and ser.servicepricedate";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày vào viện")
                    {
                        tieuchi = "and vp.vienphidate";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày ra viện")
                    {
                        tieuchi = "and vp.vienphidate_ravien";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                    {
                        tieuchi = "and vp.duyet_ngayduyet_vp ";
                    }
                    else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")//theo ngay duyet BHYT
                    {
                        tieuchi = "and vp.duyet_ngayduyet ";
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

                    // Lấy trường đối tượng BN loaidoituong
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
                    else
                        doituongbenhnhanid = " ";

                    // Lấy từ ngày, đến ngày
                    datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    // Thực thi câu lệnh SQL
                    string sqlquerry = "SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepricecode, vp.duyet_ngayduyet_vp) as stt, vp.patientid as mabn,vp.vienphiid as mavp, hsba.patientname as tenbn, degp.departmentgroupname as tenkhoa, de.departmentname as tenphong, ser.servicepricecode as madv, ser.servicepricename as tendv, ser.servicepricemoney as dongia, ser.servicepricedate as thoigianchidinh, (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end as loaiphieu, vp.vienphidate as thoigianvaovien, vp.vienphidate_ravien as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, vp.chandoanravien_code as benhchinh_code, vp.chandoanravien as benhchinh_name, vp.chandoanravien_kemtheo_code as benhkemtheo_code, vp.chandoanravien_kemtheo as benhkemtheo_name, bhyt.bhytcode as bhytcode, case ser.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end as bhyt_groupcode, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, (case ser.thuockhobanle when 0 then '' else 'Đơn nhà thuốc' end) as thuockhobanle FROM serviceprice ser INNER JOIN vienphi vp ON ser.vienphiid=vp.vienphiid INNER JOIN (select hs.hosobenhanid, hs.patientname from hosobenhan hs) hsba ON vp.hosobenhanid=hsba.hosobenhanid INNER JOIN departmentgroup degp ON vp.departmentgroupid=degp.departmentgroupid INNER JOIN department de ON vp.departmentid=de.departmentid INNER JOIN departmentgroup kcd ON kcd.departmentgroupid=ser.departmentgroupid INNER JOIN department pcd ON pcd.departmentid=ser.departmentid INNER JOIN bhyt ON bhyt.bhytid=vp.bhytid WHERE ser.servicepricecode in (" + dsdv + ") " + tieuchi + " between '" + datetungay + "' and '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + " ;   ";

                    dataBCBXuatThuoc = condb.GetDataTable(sqlquerry);
                    gridControlDSDV.DataSource = dataBCBXuatThuoc;

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

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataBCBXuatThuoc != null && dataBCBXuatThuoc.Rows.Count > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);

                    string fileTemplatePath = "BC_BenhNhanSuDungDichVu.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBCBXuatThuoc);
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

        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
