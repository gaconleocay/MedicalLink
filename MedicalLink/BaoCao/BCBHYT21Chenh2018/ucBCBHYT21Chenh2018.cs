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
using MedicalLink.Utilities.GridControl;
using MedicalLink.Utilities;
using DevExpress.Utils.Menu;
using MedicalLink.ClassCommon;
using Aspose.Cells;

namespace MedicalLink.BaoCao
{
    public partial class ucBCBHYT21Chenh2018 : UserControl
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        private List<ServicePriceRef_ChenhBHYT21> lstServicePriceRef_Chenh21 { get; set; }
        private DataView DMDV_Import { get; set; }
        private string maTinhThanh { get; set; }
        #endregion

        public ucBCBHYT21Chenh2018()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCBHYT21Chenh2018_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucDichVuChenh2018();
            LoadMaTinhThanhPho();
            btnLuuLai.Enabled = false;
        }

        private void LoadDanhMucDichVuChenh2018()
        {
            try
            {
                string _sqlGetDVKT = "select row_number () over (order by servicepricenamebhyt) as stt, * from tools_servicerefchenh2018 ORDER BY servicepricenamebhyt;";
                DataTable _dataDVKT = condb.GetDataTable_MeL(_sqlGetDVKT);
                if (_dataDVKT != null && _dataDVKT.Rows.Count > 0)
                {
                    gridControlDSGanMa.DataSource = _dataDVKT;
                }
                else
                {
                    gridControlDSGanMa.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadMaTinhThanhPho()
        {
            try
            {
                string _sqltinh = "select substring(mayte,1,2) as ma_tinh from hospital;";
                DataTable _dataMaTinh = condb.GetDataTable_HIS(_sqltinh);
                if (_dataMaTinh != null && _dataMaTinh.Rows.Count > 0)
                {
                    this.maTinhThanh = _dataMaTinh.Rows[0]["ma_tinh"].ToString();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _trangthai_vp = "";
                string _doituongBN = "";
                string _loaivienphi = "";
                string _loaidoituong = "";
                string _createdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string _sql_timkiem = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")
                {
                    _tieuchi_vp = " and vienphistatus=2 and duyet_ngayduyet between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //Trang thai vien phi
                if (cbbTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    _trangthai_vp = " and vienphistatus=0 ";
                }
                else if (cbbTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    _trangthai_vp = " and vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 ";
                }
                else if (cbbTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    _trangthai_vp = " and vienphistatus_vp=1 ";
                }
                //Doi tuong BN
                if (cbbDoiTuongBN.Text == "BHYT")
                {
                    _doituongBN = " and doituongbenhnhanid=1 ";
                    _loaidoituong = "and loaidoituong in (0,4,6,20) ";
                }
                else if (cbbDoiTuongBN.Text == "Viện phí")
                {
                    _doituongBN = " and doituongbenhnhanid<>1 ";
                    //_loaidoituong = "and loaidoituong not in (0,4,6,20) ";
                }
                //Loai benh an
                if (cbbLoaiBenhAn.Text == "Ngoại trú")
                {
                    _loaivienphi = " and loaivienphiid<>0 ";
                }
                else if (cbbLoaiBenhAn.Text == "Nội trú")
                {
                    _loaivienphi = " and loaivienphiid=0 ";
                }


                //SQL tim kiem
                if (cbbDoiTuongBN.Text == "BHYT")
                {
                    _sql_timkiem = @"SELECT 'A.Nội tỉnh' as ngoaitinh, vp.loaivienphiid, vp.doituongbenhnhanid, serf.servicepricecodeuser, ser.bhyt_groupcode, serf.servicepricecode, serf.servicepricenamebhyt, ser.servicepricemoney_bhyt, sum((case when vp.loaivienphiid=0 and ser.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong, (case when ser.bhyt_groupcode='01KB' then (case when ser.lankhambenh in (2,3) then 30 when coalesce(ser.lankhambenh,0)=0 then 100 else 0 end) when ser.bhyt_groupcode='12NG' then (case when ser.loaingaygiuong=1 then 50 when ser.loaingaygiuong=2 then 30 else 100 end) when ser.bhyt_groupcode in ('06PTTT','07KTC') then (case when ser.loaipttt=2 then 80 when ser.loaipttt=1 then 50 else 100 end) else 100 end) as tyle, sum(ser.servicepricemoney_bhyt*(case when vp.loaivienphiid=0 and ser.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien FROM (select * from vienphi where 1=1 " + _tieuchi_vp + _doituongBN + _trangthai_vp + _loaivienphi + ") vp inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') " + _tieuchi_ser + _loaidoituong + ") ser on ser.vienphiid=vp.vienphiid inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode inner join (select bhytid from bhyt where substring(bhytcode,4,2)='" + this.maTinhThanh + "') bh on bh.bhytid=vp.bhytid GROUP BY vp.loaivienphiid, vp.doituongbenhnhanid, serf.servicepricecodeuser, ser.bhyt_groupcode, serf.servicepricecode, serf.servicepricenamebhyt, ser.servicepricemoney_bhyt, ser.lankhambenh, ser.loaingaygiuong, ser.loaipttt UNION ALL SELECT 'B.Ngoại tỉnh' as ngoaitinh, vp.loaivienphiid, vp.doituongbenhnhanid, serf.servicepricecodeuser, ser.bhyt_groupcode, serf.servicepricecode, serf.servicepricenamebhyt, ser.servicepricemoney_bhyt, sum((case when vp.loaivienphiid=0 and ser.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong, (case when ser.bhyt_groupcode='01KB' then (case when ser.lankhambenh in (2,3) then 30 when coalesce(ser.lankhambenh,0)=0 then 100 else 0 end) when ser.bhyt_groupcode='12NG' then (case when ser.loaingaygiuong=1 then 50 when ser.loaingaygiuong=2 then 30 else 100 end) when ser.bhyt_groupcode in ('06PTTT','07KTC') then (case when ser.loaipttt=2 then 80 when ser.loaipttt=1 then 50 else 100 end) else 100 end) as tyle, sum(ser.servicepricemoney_bhyt*(case when vp.loaivienphiid=0 and ser.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien FROM (select * from vienphi where 1=1 " + _tieuchi_vp + _doituongBN + _trangthai_vp + _loaivienphi + ") vp inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') " + _tieuchi_ser + _loaidoituong + ") ser on ser.vienphiid=vp.vienphiid inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode inner join (select bhytid from bhyt where substring(bhytcode,4,2)<>'" + this.maTinhThanh + "') bh on bh.bhytid=vp.bhytid GROUP BY vp.loaivienphiid, vp.doituongbenhnhanid, serf.servicepricecodeuser, ser.bhyt_groupcode, serf.servicepricecode, serf.servicepricenamebhyt, ser.servicepricemoney_bhyt, ser.lankhambenh, ser.loaingaygiuong, ser.loaipttt;";
                }
                else //<>BHYT
                {
                    _sql_timkiem = @"SELECT '' as ngoaitinh, vp.loaivienphiid, vp.doituongbenhnhanid, serf.servicepricecodeuser, ser.bhyt_groupcode, serf.servicepricecode, serf.servicepricenamebhyt, ser.servicepricemoney_bhyt, sum((case when vp.loaivienphiid=0 and ser.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong, (case when ser.bhyt_groupcode='01KB' then (case when ser.lankhambenh in (2,3) then 30 when coalesce(ser.lankhambenh,0)=0 then 100 else 0 end) when ser.bhyt_groupcode='12NG' then (case when ser.loaingaygiuong=1 then 50 when ser.loaingaygiuong=2 then 30 else 100 end) when ser.bhyt_groupcode in ('06PTTT','07KTC') then (case when ser.loaipttt=2 then 80 when ser.loaipttt=1 then 50 else 100 end) else 100 end) as tyle, sum(ser.servicepricemoney_bhyt*(case when vp.loaivienphiid=0 and ser.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien FROM (select * from vienphi where 1=1 " + _tieuchi_vp + _doituongBN + _trangthai_vp + _loaivienphi + ") vp inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') " + _tieuchi_ser + _loaidoituong + ") ser on ser.vienphiid=vp.vienphiid inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode GROUP BY vp.loaivienphiid, vp.doituongbenhnhanid, serf.servicepricecodeuser, ser.bhyt_groupcode, serf.servicepricecode, serf.servicepricenamebhyt, ser.servicepricemoney_bhyt, ser.lankhambenh, ser.loaingaygiuong, ser.loaipttt;";
                }
                //Lay du lieu bao cao + insert vào DB
                DataTable _dataBHYT21TMP = condb.GetDataTable_HIS(_sql_timkiem);
                if (_dataBHYT21TMP != null && _dataBHYT21TMP.Rows.Count > 0)
                {
                    for (int i = 0; i < _dataBHYT21TMP.Rows.Count; i++)
                    {
                        string _sqlInsert = @"insert into tools_datachenh2018tmp(loaivienphiid, doituongbenhnhanid, servicepricecodeuser, bhyt_groupcode, servicepricecode, servicepricenamebhyt, servicepricemoney_bhyt, soluong, tyle, thanhtien, ngoaitinh, createusercode, createdate) values ('" + _dataBHYT21TMP.Rows[i]["loaivienphiid"].ToString() + "', '" + _dataBHYT21TMP.Rows[i]["doituongbenhnhanid"].ToString() + "', '" + _dataBHYT21TMP.Rows[i]["servicepricecodeuser"].ToString() + "', '" + _dataBHYT21TMP.Rows[i]["bhyt_groupcode"].ToString() + "', '" + _dataBHYT21TMP.Rows[i]["servicepricecode"].ToString() + "', '" + _dataBHYT21TMP.Rows[i]["servicepricenamebhyt"].ToString().Replace("'", "''") + "', '" + _dataBHYT21TMP.Rows[i]["servicepricemoney_bhyt"].ToString().Replace(",", ".") + "', '" + _dataBHYT21TMP.Rows[i]["soluong"].ToString().Replace(",", ".") + "', '" + _dataBHYT21TMP.Rows[i]["tyle"].ToString() + "', '" + _dataBHYT21TMP.Rows[i]["thanhtien"].ToString().Replace(",", ".") + "', '" + _dataBHYT21TMP.Rows[i]["ngoaitinh"].ToString() + "', '" + Base.SessionLogin.SessionUsercode + "', '" + _createdate + "');";
                        condb.ExecuteNonQuery_MeL(_sqlInsert);
                    }

                    //Lay bao cao chenh --ngay 28/2/2018
                    string _sql_dataChenh = @"SELECT (row_number() OVER (PARTITION BY O.tennhom_bhyt ORDER BY O.ngoaitinh,O.servicepricenamebhyt)) as stt, O.*
                    FROM
	                    (SELECT distinct chenh.datachenh2018tmpid,
		                    chenh.ngoaitinh,
		                    chenh.servicepricecode,
		                    chenh.bhyt_groupcode,
		                    (case when chenh.bhyt_groupcode='01KB' then 'I - Công khám'
				                    when chenh.bhyt_groupcode='12NG' then 'II - Ngày giường'
				                    when chenh.bhyt_groupcode='03XN' then 'III - Xét nghiệm'
				                    when chenh.bhyt_groupcode in ('04CDHA','05TDCN') then 'IV - Chẩn đoán hình ảnh'
				                    when chenh.bhyt_groupcode='06PTTT' then 'V - Phẫu thuật thủ thuật'
				                    when chenh.bhyt_groupcode='07KTC' then 'VI - Dịch vụ KTC'
				                    else 'VII - Khác' end
			                    ) as tennhom_bhyt,
		                    chenh.servicepricecodeuser,
		                    chenh.servicepricenamebhyt,
		                    chenh.tyle,
		                    chenh.soluong,
		                    sef.servicepricemoney_bhyt_tr13 as giabhyt_truoc13,
		                    (chenh.soluong*sef.servicepricemoney_bhyt_tr13*(chenh.tyle/100.0)) as thanhtien_truoc13,
		                    sef.servicepricemoney_bhyt_13 as giabhyt_13,
		                    (chenh.soluong*sef.servicepricemoney_bhyt_13*(chenh.tyle/100.0)) as thanhtien_13,
		                    chenh.servicepricemoney_bhyt as giabhyt_21,
		                    chenh.thanhtien as thanhtien_21,
		                    sef.servicepricemoney_bhyt_17 as giabhyt_17,
		                    (chenh.soluong*sef.servicepricemoney_bhyt_17*(chenh.tyle/100.0)) as thanhtien_17,
		                    ((coalesce(sef.servicepricemoney_bhyt_17,0)-coalesce(sef.servicepricemoney_bhyt_13,0))*chenh.soluong*(chenh.tyle/100.0)) as chenh_17_13,
		                    ((coalesce(sef.servicepricemoney_bhyt_17,0)-coalesce(sef.servicepricemoney_bhyt_tr13,0))*chenh.soluong*(chenh.tyle/100.0)) as chenh_17_truoc13,
		                    '0' as isgroup
	                    FROM tools_datachenh2018tmp chenh
		                    left join tools_servicerefchenh2018 sef on sef.servicepricecode=chenh.servicepricecode
	                    WHERE chenh.createdate='" + _createdate + "' and chenh.createusercode='" + Base.SessionLogin.SessionUsercode + "') O WHERE O.soluong > 0; ";
                    this.dataBaoCao = condb.GetDataTable_MeL(_sql_dataChenh);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBaoCao.DataSource = this.dataBaoCao;
                        string _sqldeleteTmp = "DELETE FROM tools_datachenh2018tmp WHERE createdate='" + _createdate + "' and createusercode='" + Base.SessionLogin.SessionUsercode + "';";
                        condb.ExecuteNonQuery_MeL(_sqldeleteTmp);
                    }
                    else
                    {
                        gridControlDataBaoCao.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Export and Print
        private void tbnExport_Click(object sender, EventArgs e)
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
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_40_BHYT21ChenhLech2018.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
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
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_40_BHYT21ChenhLech2018.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.BCBHYT21Chenh2018DTO> lstData_XuatBaoCao = new List<ClassCommon.BCBHYT21Chenh2018DTO>();
                List<ClassCommon.BCBHYT21Chenh2018DTO> lstDataTimKiem = Util_DataTable.DataTableToList<ClassCommon.BCBHYT21Chenh2018DTO>(this.dataBaoCao);

                List<ClassCommon.BCBHYT21Chenh2018DTO> lstGroup_NgoaiTinh = lstDataTimKiem.GroupBy(o => o.ngoaitinh).Select(n => n.First()).ToList();
                foreach (var item_ngoaitinh in lstGroup_NgoaiTinh)
                {
                    List<ClassCommon.BCBHYT21Chenh2018DTO> lstData_NgoaiTinh = lstDataTimKiem.Where(o => o.ngoaitinh == item_ngoaitinh.ngoaitinh).ToList();
                    decimal NT_sum_soluong = 0;
                    decimal NT_sum_thanhtien_tr13 = 0;
                    decimal NT_sum_thanhtien_13 = 0;
                    decimal NT_sum_thanhtien_17 = 0;
                    decimal NT_sum_thanhtien_21 = 0;
                    decimal NT_sum_chenh1713 = 0;
                    decimal NT_sum_chenh17tr13 = 0;

                    for (int i = 0; i < lstData_NgoaiTinh.Count; i++)
                    {
                        //lstData_doanhthu[i].stt = (i + 1).ToString();
                        NT_sum_soluong += lstData_NgoaiTinh[i].soluong;
                        NT_sum_thanhtien_tr13 += lstData_NgoaiTinh[i].thanhtien_truoc13;
                        NT_sum_thanhtien_13 += lstData_NgoaiTinh[i].thanhtien_13;
                        NT_sum_thanhtien_17 += lstData_NgoaiTinh[i].thanhtien_17;
                        NT_sum_thanhtien_21 += lstData_NgoaiTinh[i].thanhtien_21;
                        NT_sum_chenh1713 += lstData_NgoaiTinh[i].chenh_17_13;
                        NT_sum_chenh17tr13 += lstData_NgoaiTinh[i].chenh_17_truoc13;
                    }
                    ClassCommon.BCBHYT21Chenh2018DTO data_ngoaitinh = new ClassCommon.BCBHYT21Chenh2018DTO();
                    data_ngoaitinh.stt = item_ngoaitinh.ngoaitinh;
                    data_ngoaitinh.soluong = NT_sum_soluong;
                    data_ngoaitinh.thanhtien_truoc13 = NT_sum_thanhtien_tr13;
                    data_ngoaitinh.thanhtien_13 = NT_sum_thanhtien_13;
                    data_ngoaitinh.thanhtien_17 = NT_sum_thanhtien_17;
                    data_ngoaitinh.thanhtien_21 = NT_sum_thanhtien_21;
                    data_ngoaitinh.chenh_17_13 = NT_sum_chenh1713;
                    data_ngoaitinh.chenh_17_truoc13 = NT_sum_chenh17tr13;
                    data_ngoaitinh.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_ngoaitinh);//add ngoai tinh

                    List<ClassCommon.BCBHYT21Chenh2018DTO> lstGroup_NhomBHYT = lstData_NgoaiTinh.GroupBy(o => o.tennhom_bhyt).Select(n => n.First()).ToList();
                    foreach (var item_group in lstGroup_NhomBHYT)
                    {
                        List<ClassCommon.BCBHYT21Chenh2018DTO> lstData_NhomBHYT = lstData_NgoaiTinh.Where(o => o.tennhom_bhyt == item_group.tennhom_bhyt).ToList();

                        decimal sum_soluong = 0;
                        decimal sum_thanhtien_tr13 = 0;
                        decimal sum_thanhtien_13 = 0;
                        decimal sum_thanhtien_17 = 0;
                        decimal sum_thanhtien_21 = 0;
                        decimal sum_chenh1713 = 0;
                        decimal sum_chenh17tr13 = 0;

                        for (int i = 0; i < lstData_NhomBHYT.Count; i++)
                        {
                            //lstData_doanhthu[i].stt = (i + 1).ToString();
                            sum_soluong += lstData_NhomBHYT[i].soluong;
                            sum_thanhtien_tr13 += lstData_NhomBHYT[i].thanhtien_truoc13;
                            sum_thanhtien_13 += lstData_NhomBHYT[i].thanhtien_13;
                            sum_thanhtien_17 += lstData_NhomBHYT[i].thanhtien_17;
                            sum_thanhtien_21 += lstData_NhomBHYT[i].thanhtien_21;
                            sum_chenh1713 += lstData_NhomBHYT[i].chenh_17_13;
                            sum_chenh17tr13 += lstData_NhomBHYT[i].chenh_17_truoc13;
                        }

                        ClassCommon.BCBHYT21Chenh2018DTO data_groupname = new ClassCommon.BCBHYT21Chenh2018DTO();
                        data_groupname.stt = item_group.tennhom_bhyt;
                        data_groupname.soluong = sum_soluong;
                        data_groupname.thanhtien_truoc13 = sum_thanhtien_tr13;
                        data_groupname.thanhtien_13 = sum_thanhtien_13;
                        data_groupname.thanhtien_17 = sum_thanhtien_17;
                        data_groupname.thanhtien_21 = sum_thanhtien_21;
                        data_groupname.chenh_17_13 = sum_chenh1713;
                        data_groupname.chenh_17_truoc13 = sum_chenh17tr13;
                        data_groupname.isgroup = 2;

                        lstData_XuatBaoCao.Add(data_groupname);
                        lstData_XuatBaoCao.AddRange(lstData_NhomBHYT);
                    }
                }
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }


        #endregion

        #region Custom
        private void gridViewDataBaoCao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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





        #endregion

        #region Cau hinh 
        private void btnThemTuFileExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    this.DMDV_Import = new DataView();
                    gridControlDSGanMa.DataSource = null;
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets[0];
                    //row chay tu 0
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(3, 0, worksheet.Cells.MaxDataRow, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null)
                    {
                        this.DMDV_Import = new DataView(data_Excel);
                        gridControlDSGanMa.DataSource = this.DMDV_Import;
                        btnLuuLai.Enabled = true;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                        btnLuuLai.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(Base.ThongBaoLable.CO_LOI_XAY_RA);
                frmthongbao.Show();
                btnLuuLai.Enabled = false;
            }
        }
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                int dem_update = 0;
                int dem_insert = 0;
                for (int i = 0; i < this.DMDV_Import.Count; i++)
                {
                    if (this.DMDV_Import[i]["SERVICEPRICECODE"].ToString() != "")
                    {
                        string sql_kt = "SELECT servicerefchenh2018id FROM tools_servicerefchenh2018 WHERE servicepricecode='" + this.DMDV_Import[i]["SERVICEPRICECODE"].ToString() + "';";
                        DataTable _data_KT = condb.GetDataTable_MeL(sql_kt);
                        string _servicepricemoney_bhyt = this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT"].ToString() == "" ? "0" : this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT"].ToString().Replace(",",".");
                        string _servicepricemoney_bhyt_tr13 = this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT_TR13"].ToString() == "" ? "0" : this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT_TR13"].ToString().Replace(",",".");
                        string _servicepricemoney_bhyt_13 = this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT_13"].ToString() == "" ? "0" : this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT_13"].ToString().Replace(",",".");
                        string _servicepricemoney_bhyt_17 = this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT_17"].ToString() == "" ? "0" : this.DMDV_Import[i]["SERVICEPRICEMONEY_BHYT_17"].ToString().Replace(",",".");
                        string _servicepricemoney_vp_new = this.DMDV_Import[i]["SERVICEPRICEMONEY_VP_NEW"].ToString() == "" ? "0" : this.DMDV_Import[i]["SERVICEPRICEMONEY_VP_NEW"].ToString().Replace(",",".");
                        string _servicepricemoney_vp_old = this.DMDV_Import[i]["SERVICEPRICEMONEY_VP_OLD"].ToString() == "" ? "0" : this.DMDV_Import[i]["SERVICEPRICEMONEY_VP_OLD"].ToString().Replace(",",".");

                        if (_data_KT != null && _data_KT.Rows.Count > 0) //update
                        {
                            string sql_updateDVKT = "UPDATE tools_servicerefchenh2018 SET servicepricecodeuser='" + this.DMDV_Import[i]["SERVICEPRICECODEUSER"].ToString() + "', servicepricecodeuser_old='" + this.DMDV_Import[i]["SERVICEPRICECODEUSER_OLD"].ToString() + "', servicepricecodeuser_new='" + this.DMDV_Import[i]["SERVICEPRICECODEUSER_NEW"].ToString() + "', servicepricenamebhyt='" + this.DMDV_Import[i]["SERVICEPRICENAMEBHYT"].ToString().Replace("'", "''") + "', servicepricenamebhyt_old='" + this.DMDV_Import[i]["SERVICEPRICENAMEBHYT_OLD"].ToString().Replace("'", "''") + "', servicepricenamebhyt_new='" + this.DMDV_Import[i]["SERVICEPRICENAMEBHYT_NEW"].ToString().Replace("'", "''") + "', servicepricemoney_bhyt='" + _servicepricemoney_bhyt + "', servicepricemoney_bhyt_tr13='" + _servicepricemoney_bhyt_tr13 + "', servicepricemoney_bhyt_13='" + _servicepricemoney_bhyt_13 + "', servicepricemoney_bhyt_17='" + _servicepricemoney_bhyt_17 + "', servicepricemoney_vp_new='" + _servicepricemoney_vp_new + "', servicepricemoney_vp_old='" + _servicepricemoney_vp_old + "', createusercode='" + Base.SessionLogin.SessionUsercode + "', createusername='" + Base.SessionLogin.SessionUsername + "', createdate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE servicepricecode='" + this.DMDV_Import[i]["SERVICEPRICECODE"].ToString() + "';";
                            try
                            {
                                if (condb.ExecuteNonQuery_MeL(sql_updateDVKT))
                                {
                                    dem_update += 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            string sql_insertDVKT = @"INSERT INTO tools_servicerefchenh2018(servicepricecode, servicepricecodeuser, servicepricecodeuser_old, servicepricecodeuser_new, servicepricenamebhyt, servicepricenamebhyt_old, servicepricenamebhyt_new, servicepricemoney_bhyt, servicepricemoney_bhyt_tr13, servicepricemoney_bhyt_13, servicepricemoney_bhyt_17, servicepricemoney_vp_new, servicepricemoney_vp_old, createusercode, createusername, createdate) VALUES('" + this.DMDV_Import[i]["SERVICEPRICECODE"].ToString() + "', '" + this.DMDV_Import[i]["SERVICEPRICECODEUSER"].ToString() + "', '" + this.DMDV_Import[i]["SERVICEPRICECODEUSER_OLD"].ToString() + "', '" + this.DMDV_Import[i]["SERVICEPRICECODEUSER_NEW"].ToString() + "', '" + this.DMDV_Import[i]["SERVICEPRICENAMEBHYT"].ToString().Replace("'", "''") + "', '" + this.DMDV_Import[i]["SERVICEPRICENAMEBHYT_OLD"].ToString().Replace("'", "''") + "', '" + this.DMDV_Import[i]["SERVICEPRICENAMEBHYT_NEW"].ToString().Replace("'", "''") + "', '" + _servicepricemoney_bhyt + "', '" + _servicepricemoney_bhyt_tr13 + "', '" + _servicepricemoney_bhyt_13 + "', '" + _servicepricemoney_bhyt_17 + "', '" + _servicepricemoney_vp_new + "', '" + _servicepricemoney_vp_old + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            try
                            {
                                if (condb.ExecuteNonQuery_MeL(sql_insertDVKT))
                                {
                                    dem_insert += 1;
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
                MessageBox.Show("Thêm mới [ " + dem_insert + " ] & cập nhật [ " + dem_update + " ] danh mục dịch vụ thành công.");
                LoadDanhMucDichVuChenh2018();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void gridViewDSGanMa_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (!btnLuuLai.Enabled)
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        e.Menu.Items.Clear();
                        DXMenuItem itemKiemTraDaChon = new DXMenuItem("Xóa dịch vụ đã chọn");
                        itemKiemTraDaChon.Image = imageCollectionDSBN.Images[0];
                        itemKiemTraDaChon.Click += new EventHandler(XoaDichVuDaChon_Click);
                        e.Menu.Items.Add(itemKiemTraDaChon);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void XoaDichVuDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSGanMa.RowCount > 0)
                {
                    string sql_deleteDV = "";
                    foreach (var item_index in gridViewDSGanMa.GetSelectedRows())
                    {
                        string _servicepricecodeuser = gridViewDSGanMa.GetRowCellValue(item_index, "servicepricecodeuser").ToString();
                        sql_deleteDV += "DELETE FROM tools_servicerefchenh2018 where servicepricecodeuser='" + _servicepricecodeuser + "'; ";
                    }
                    condb.ExecuteNonQuery_MeL(sql_deleteDV);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.XOA_THANH_CONG);
                    frmthongbao.Show();
                    LoadDanhMucDichVuChenh2018();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDanhMucDichVuChenh2018();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion


    }
}
