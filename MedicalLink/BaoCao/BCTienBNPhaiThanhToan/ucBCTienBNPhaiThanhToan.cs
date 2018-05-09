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
using MedicalLink.DatabaseProcess;

namespace MedicalLink.BaoCao
{
    public partial class ucBCTienBNPhaiThanhToan : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ClassCommon.TienBNPhaiThanhToanDTO> lstDataBaoCaoCurrent { get; set; }
        public ucBCTienBNPhaiThanhToan()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCTienBNPhaiThanhToan_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlDataBaoCao.DataSource = null;
        }
        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi_vp = "";
                string doituongbenhnhan_vp = " duyet_ngayduyet_vp ";
                string sql_timkiem = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");


                 if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " vienphidate ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " vienphidate_ravien ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi_vp = " duyet_ngayduyet_vp ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")
                {
                    tieuchi_vp = " duyet_ngayduyet ";
                }

                if (cboDoiTuongBN.Text == "ĐT BHYT+DV Viện phí")
                {
                    doituongbenhnhan_vp = " and doituongbenhnhanid=1 ";
                    sql_timkiem = " SELECT (row_number() OVER (PARTITION BY krv.departmentgroupname order by hsba.patientname)) as stt, spt.vienphiid, spt.patientid, hsba.patientcode, hsba.patientname, to_char(hsba.birthday, 'yyyy') as namsinh, hsba.gioitinhname as gioitinh, bh.bhytcode, bh.bhyt_loaiid, bh.du5nam6thangluongcoban, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp, spt.duyet_ngayduyet, spt.bhyt_tuyenbenhvien, spt.doituongbenhnhanid, spt.bhyt_thangluongtoithieu, krv.departmentgroupid, krv.departmentgroupname, prv.departmentname, 0 as money_khambenh_bh, sum(spt.money_khambenh_vp) as money_khambenh_vp, 0 as money_xetnghiem_bh, sum(spt.money_xetnghiem_vp) as money_xetnghiem_vp, 0 as money_cdhatdcn_bh, sum(spt.money_cdha_vp + spt.money_tdcn_vp) as money_cdhatdcn_vp, 0 as money_pttt_bh, sum(spt.money_pttt_vp) as money_pttt_vp, 0 as money_dvktc_bh, sum(spt.money_dvktc_vp) as money_dvktc_vp, 0 as money_mau_bh, sum(spt.money_mau_vp) as money_mau_vp, 0 as money_giuong_bh, sum(spt.money_giuong_vp) as money_giuong_vp, 0 as money_thuoc_bh, sum(spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp) as money_thuoc_vp, 0 as money_vattu_bh, sum(spt.money_vattu_vp + spt.money_dkpttt_vattu_vp) as money_vattu_vp, 0 as money_phuthu_bh, sum(spt.money_phuthu_vp) as money_phuthu_vp, 0 as money_vtthaythe_bh, sum(spt.money_vtthaythe_vp) as money_vtthaythe_vp, 0 as money_vanchuyen_bh, sum(spt.money_vanchuyen_vp) as money_vanchuyen_vp, 0 as money_khac_bh, sum(spt.money_khac_vp) as money_khac_vp, 0 as money_tong_bh, sum(spt.money_khambenh_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_mau_vp + spt.money_giuong_vp + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp + spt.money_vattu_vp + spt.money_dkpttt_vattu_vp + spt.money_phuthu_vp + spt.money_vtthaythe_vp + spt.money_vanchuyen_vp + spt.money_khac_vp) as money_tong_vp FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoaravien,phongravien,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,duyet_ngayduyet_vp,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu,money_khambenh_bh,money_xetnghiem_bh,money_cdha_bh,money_tdcn_bh,money_pttt_bh,money_dvktc_bh,money_mau_bh,money_giuong_bh,money_thuoc_bh,money_dkpttt_thuoc_bh,money_vattu_bh,money_dkpttt_vattu_bh,money_phuthu_bh,money_vtthaythe_bh,money_vanchuyen_bh,money_khac_bh,money_khambenh_vp,money_xetnghiem_vp,money_cdha_vp,money_tdcn_vp,money_pttt_vp,money_dvktc_vp,money_mau_vp,money_giuong_vp,money_thuoc_vp,money_dkpttt_thuoc_vp,money_vattu_vp,money_dkpttt_vattu_vp,money_phuthu_vp,money_vtthaythe_vp,money_vanchuyen_vp,money_khac_vp from vienphi_money_tm where vienphistatus_vp=1 and "+tieuchi_vp+"  between '" + datetungay + "' and '" + datedenngay + "' " + doituongbenhnhan_vp + ") spt INNER JOIN (select hosobenhanid,patientcode,patientname,birthday,gioitinhname from hosobenhan) hsba on hsba.hosobenhanid=spt.hosobenhanid INNER JOIN (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=spt.bhytid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=spt.khoaravien LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) prv ON prv.departmentid=spt.phongravien GROUP BY spt.vienphiid,spt.patientid,hsba.patientcode,hsba.patientname,hsba.birthday,hsba.gioitinhname,bh.bhytcode,bh.bhyt_loaiid,bh.du5nam6thangluongcoban,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp,spt.duyet_ngayduyet,spt.bhyt_tuyenbenhvien,spt.doituongbenhnhanid,spt.bhyt_thangluongtoithieu,krv.departmentgroupid,krv.departmentgroupname,prv.departmentname;  ";
                }
                else if (cboDoiTuongBN.Text == "ĐT BHYT+DV BHYT")
                {
                    doituongbenhnhan_vp = " and doituongbenhnhanid=1 ";
                    sql_timkiem = " SELECT (row_number() OVER (PARTITION BY krv.departmentgroupname order by hsba.patientname)) as stt, spt.vienphiid, spt.patientid, hsba.patientcode, hsba.patientname, to_char(hsba.birthday, 'yyyy') as namsinh, hsba.gioitinhname as gioitinh, bh.bhytcode, bh.bhyt_loaiid, bh.du5nam6thangluongcoban, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp, spt.duyet_ngayduyet, spt.bhyt_tuyenbenhvien, spt.doituongbenhnhanid, spt.bhyt_thangluongtoithieu, krv.departmentgroupid, krv.departmentgroupname, prv.departmentname, sum(spt.money_khambenh_bh) as money_khambenh_bh, 0 as money_khambenh_vp, sum(spt.money_xetnghiem_bh) as money_xetnghiem_bh, 0 as money_xetnghiem_vp, sum(spt.money_cdha_bh + spt.money_tdcn_bh) as money_cdhatdcn_bh, 0 as money_cdhatdcn_vp, sum(spt.money_pttt_bh) as money_pttt_bh, 0 as money_pttt_vp, sum(spt.money_dvktc_bh) as money_dvktc_bh, 0 as money_dvktc_vp, sum(spt.money_mau_bh) as money_mau_bh, 0 as money_mau_vp, sum(spt.money_giuong_bh) as money_giuong_bh, 0 as money_giuong_vp, sum(spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh) as money_thuoc_bh, 0 as money_thuoc_vp, sum(spt.money_vattu_bh + spt.money_dkpttt_vattu_bh) as money_vattu_bh, 0 as money_vattu_vp, sum(spt.money_phuthu_bh) as money_phuthu_bh, 0 as money_phuthu_vp, sum(spt.money_vtthaythe_bh) as money_vtthaythe_bh, 0 as money_vtthaythe_vp, sum(spt.money_vanchuyen_bh) as money_vanchuyen_bh, 0 as money_vanchuyen_vp, sum(spt.money_khac_bh) as money_khac_bh, 0 as money_khac_vp, sum(spt.money_khambenh_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_mau_bh + spt.money_giuong_bh + spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh + spt.money_vattu_bh + spt.money_dkpttt_vattu_bh + spt.money_phuthu_bh + spt.money_vtthaythe_bh + spt.money_vanchuyen_bh + spt.money_khac_bh) as money_tong_bh, 0 as money_tong_vp FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoaravien,phongravien,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,duyet_ngayduyet_vp,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu,money_khambenh_bh,money_xetnghiem_bh,money_cdha_bh,money_tdcn_bh,money_pttt_bh,money_dvktc_bh,money_mau_bh,money_giuong_bh,money_thuoc_bh,money_dkpttt_thuoc_bh,money_vattu_bh,money_dkpttt_vattu_bh,money_phuthu_bh,money_vtthaythe_bh,money_vanchuyen_bh,money_khac_bh,money_khambenh_vp,money_xetnghiem_vp,money_cdha_vp,money_tdcn_vp,money_pttt_vp,money_dvktc_vp,money_mau_vp,money_giuong_vp,money_thuoc_vp,money_dkpttt_thuoc_vp,money_vattu_vp,money_dkpttt_vattu_vp,money_phuthu_vp,money_vtthaythe_vp,money_vanchuyen_vp,money_khac_vp from vienphi_money_tm where vienphistatus_vp=1 and " + tieuchi_vp + "  between '" + datetungay + "' and '" + datedenngay + "' " + doituongbenhnhan_vp + ") spt INNER JOIN (select hosobenhanid,patientcode,patientname,birthday,gioitinhname from hosobenhan) hsba on hsba.hosobenhanid=spt.hosobenhanid INNER JOIN (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=spt.bhytid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=spt.khoaravien LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) prv ON prv.departmentid=spt.phongravien GROUP BY spt.vienphiid,spt.patientid,hsba.patientcode,hsba.patientname,hsba.birthday,hsba.gioitinhname,bh.bhytcode,bh.bhyt_loaiid,bh.du5nam6thangluongcoban,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp,spt.duyet_ngayduyet,spt.bhyt_tuyenbenhvien,spt.doituongbenhnhanid,spt.bhyt_thangluongtoithieu,krv.departmentgroupid,krv.departmentgroupname,prv.departmentname; ";
                }
                else
                {
                    if (cboDoiTuongBN.Text == "ĐT Viện phí")
                    {
                        doituongbenhnhan_vp = " and doituongbenhnhanid<>1 ";
                    }
                    else if (cboDoiTuongBN.Text == "ĐT BHYT")
                    {
                        doituongbenhnhan_vp = " and doituongbenhnhanid=1 ";
                    }
                    else if (cboDoiTuongBN.Text == "Tất cả")
                    {
                        doituongbenhnhan_vp = "";
                    }
                    sql_timkiem = " SELECT (row_number() OVER (PARTITION BY krv.departmentgroupname order by hsba.patientname)) as stt, spt.vienphiid, spt.patientid, hsba.patientcode, hsba.patientname, to_char(hsba.birthday, 'yyyy') as namsinh, hsba.gioitinhname as gioitinh, bh.bhytcode, bh.bhyt_loaiid, bh.du5nam6thangluongcoban, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp, spt.duyet_ngayduyet, spt.bhyt_tuyenbenhvien, spt.doituongbenhnhanid, spt.bhyt_thangluongtoithieu, krv.departmentgroupid, krv.departmentgroupname, prv.departmentname, sum(spt.money_khambenh_bh) as money_khambenh_bh, sum(spt.money_khambenh_vp) as money_khambenh_vp, sum(spt.money_xetnghiem_bh) as money_xetnghiem_bh, sum(spt.money_xetnghiem_vp) as money_xetnghiem_vp, sum(spt.money_cdha_bh + spt.money_tdcn_bh) as money_cdhatdcn_bh, sum(spt.money_cdha_vp + spt.money_tdcn_vp) as money_cdhatdcn_vp, sum(spt.money_pttt_bh) as money_pttt_bh, sum(spt.money_pttt_vp) as money_pttt_vp, sum(spt.money_dvktc_bh) as money_dvktc_bh, sum(spt.money_dvktc_vp) as money_dvktc_vp, sum(spt.money_mau_bh) as money_mau_bh, sum(spt.money_mau_vp) as money_mau_vp, sum(spt.money_giuong_bh) as money_giuong_bh, sum(spt.money_giuong_vp) as money_giuong_vp, sum(spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh) as money_thuoc_bh, sum(spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp) as money_thuoc_vp, sum(spt.money_vattu_bh + spt.money_dkpttt_vattu_bh) as money_vattu_bh, sum(spt.money_vattu_vp + spt.money_dkpttt_vattu_vp) as money_vattu_vp, sum(spt.money_phuthu_bh) as money_phuthu_bh, sum(spt.money_phuthu_vp) as money_phuthu_vp, sum(spt.money_vtthaythe_bh) as money_vtthaythe_bh, sum(spt.money_vtthaythe_vp) as money_vtthaythe_vp, sum(spt.money_vanchuyen_bh) as money_vanchuyen_bh, sum(spt.money_vanchuyen_vp) as money_vanchuyen_vp, sum(spt.money_khac_bh) as money_khac_bh, sum(spt.money_khac_vp) as money_khac_vp, sum(spt.money_khambenh_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_mau_bh + spt.money_giuong_bh + spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh + spt.money_vattu_bh + spt.money_dkpttt_vattu_bh + spt.money_phuthu_bh + spt.money_vtthaythe_bh + spt.money_vanchuyen_bh + spt.money_khac_bh) as money_tong_bh, sum(spt.money_khambenh_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_mau_vp + spt.money_giuong_vp + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp + spt.money_vattu_vp + spt.money_dkpttt_vattu_vp + spt.money_phuthu_vp + spt.money_vtthaythe_vp + spt.money_vanchuyen_vp + spt.money_khac_vp) as money_tong_vp FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoaravien,phongravien,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,duyet_ngayduyet_vp,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu,money_khambenh_bh,money_xetnghiem_bh,money_cdha_bh,money_tdcn_bh,money_pttt_bh,money_dvktc_bh,money_mau_bh,money_giuong_bh,money_thuoc_bh,money_dkpttt_thuoc_bh,money_vattu_bh,money_dkpttt_vattu_bh,money_phuthu_bh,money_vtthaythe_bh,money_vanchuyen_bh,money_khac_bh,money_khambenh_vp,money_xetnghiem_vp,money_cdha_vp,money_tdcn_vp,money_pttt_vp,money_dvktc_vp,money_mau_vp,money_giuong_vp,money_thuoc_vp,money_dkpttt_thuoc_vp,money_vattu_vp,money_dkpttt_vattu_vp,money_phuthu_vp,money_vtthaythe_vp,money_vanchuyen_vp,money_khac_vp from vienphi_money_tm where vienphistatus_vp=1 and " + tieuchi_vp + "  between '" + datetungay + "' and '" + datedenngay + "' " + doituongbenhnhan_vp + ") spt INNER JOIN (select hosobenhanid,patientcode,patientname,birthday,gioitinhname from hosobenhan) hsba on hsba.hosobenhanid=spt.hosobenhanid INNER JOIN (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=spt.bhytid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=spt.khoaravien LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) prv ON prv.departmentid=spt.phongravien GROUP BY spt.vienphiid,spt.patientid,hsba.patientcode,hsba.patientname,hsba.birthday,hsba.gioitinhname,bh.bhytcode,bh.bhyt_loaiid,bh.du5nam6thangluongcoban,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp,spt.duyet_ngayduyet,spt.bhyt_tuyenbenhvien,spt.doituongbenhnhanid,spt.bhyt_thangluongtoithieu,krv.departmentgroupid,krv.departmentgroupname,prv.departmentname;  ";
                }
                DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);

                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    this.lstDataBaoCaoCurrent = new List<ClassCommon.TienBNPhaiThanhToanDTO>();
                    //Chạy chậm nên thay đổi hàm xử lý
                    //this.lstDataBaoCaoCurrent = Util_DataTable.DataTableToList<ClassCommon.TienBNPhaiThanhToanDTO>(dataBaoCao);
                    for (int i = 0; i < dataBaoCao.Rows.Count; i++)
                    {
                        ClassCommon.TienBNPhaiThanhToanDTO bnphaithanhtoan = new ClassCommon.TienBNPhaiThanhToanDTO();
                        bnphaithanhtoan.stt = dataBaoCao.Rows[i]["stt"].ToString();
                        bnphaithanhtoan.vienphiid = dataBaoCao.Rows[i]["vienphiid"].ToString();
                        bnphaithanhtoan.patientid = dataBaoCao.Rows[i]["patientid"].ToString();
                        bnphaithanhtoan.patientcode = dataBaoCao.Rows[i]["patientcode"].ToString();
                        bnphaithanhtoan.patientname = dataBaoCao.Rows[i]["patientname"].ToString();
                        bnphaithanhtoan.namsinh = dataBaoCao.Rows[i]["namsinh"].ToString();
                        bnphaithanhtoan.gioitinh = dataBaoCao.Rows[i]["gioitinh"].ToString();
                        bnphaithanhtoan.bhytcode = dataBaoCao.Rows[i]["bhytcode"].ToString();
                        bnphaithanhtoan.bhyt_loaiid = Utilities.Util_TypeConvertParse.ToInt16(dataBaoCao.Rows[i]["bhyt_loaiid"].ToString());
                        bnphaithanhtoan.du5nam6thangluongcoban = Utilities.Util_TypeConvertParse.ToInt16(dataBaoCao.Rows[i]["du5nam6thangluongcoban"].ToString());
                        bnphaithanhtoan.vienphidate = dataBaoCao.Rows[i]["vienphidate"];
                        bnphaithanhtoan.vienphidate_ravien = dataBaoCao.Rows[i]["vienphidate_ravien"];
                        bnphaithanhtoan.duyet_ngayduyet_vp = dataBaoCao.Rows[i]["duyet_ngayduyet_vp"];
                        bnphaithanhtoan.duyet_ngayduyet = dataBaoCao.Rows[i]["duyet_ngayduyet"];
                        bnphaithanhtoan.bhyt_tuyenbenhvien = Utilities.Util_TypeConvertParse.ToInt16(dataBaoCao.Rows[i]["bhyt_tuyenbenhvien"].ToString());
                        bnphaithanhtoan.doituongbenhnhanid = Utilities.Util_TypeConvertParse.ToInt16(dataBaoCao.Rows[i]["doituongbenhnhanid"].ToString());
                        bnphaithanhtoan.bhyt_thangluongtoithieu = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["bhyt_thangluongtoithieu"].ToString());
                        bnphaithanhtoan.departmentgroupid = Util_TypeConvertParse.ToInt16(dataBaoCao.Rows[i]["departmentgroupid"].ToString());
                        bnphaithanhtoan.departmentgroupname = dataBaoCao.Rows[i]["departmentgroupname"].ToString();
                        bnphaithanhtoan.departmentname = dataBaoCao.Rows[i]["departmentname"].ToString();
                        bnphaithanhtoan.money_khambenh_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_khambenh_bh"].ToString());
                        bnphaithanhtoan.money_khambenh_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_khambenh_vp"].ToString());
                        bnphaithanhtoan.money_xetnghiem_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_xetnghiem_bh"].ToString());
                        bnphaithanhtoan.money_xetnghiem_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_xetnghiem_vp"].ToString());
                        bnphaithanhtoan.money_cdhatdcn_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_cdhatdcn_bh"].ToString());
                        bnphaithanhtoan.money_cdhatdcn_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_cdhatdcn_vp"].ToString());
                        bnphaithanhtoan.money_pttt_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_pttt_bh"].ToString());
                        bnphaithanhtoan.money_pttt_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_pttt_vp"].ToString());
                        bnphaithanhtoan.money_dvktc_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_dvktc_bh"].ToString());
                        bnphaithanhtoan.money_dvktc_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_dvktc_vp"].ToString());
                        bnphaithanhtoan.money_mau_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_mau_bh"].ToString());
                        bnphaithanhtoan.money_mau_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_mau_vp"].ToString());
                        bnphaithanhtoan.money_giuong_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_giuong_bh"].ToString());
                        bnphaithanhtoan.money_giuong_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_giuong_vp"].ToString());
                        bnphaithanhtoan.money_thuoc_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_thuoc_bh"].ToString());
                        bnphaithanhtoan.money_thuoc_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_thuoc_vp"].ToString());
                        bnphaithanhtoan.money_vattu_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_vattu_bh"].ToString());
                        bnphaithanhtoan.money_vattu_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_vattu_vp"].ToString());
                        bnphaithanhtoan.money_phuthu_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_phuthu_bh"].ToString());
                        bnphaithanhtoan.money_phuthu_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_phuthu_vp"].ToString());
                        bnphaithanhtoan.money_vtthaythe_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_vtthaythe_bh"].ToString());
                        bnphaithanhtoan.money_vtthaythe_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_vtthaythe_vp"].ToString());
                        bnphaithanhtoan.money_vanchuyen_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_vanchuyen_bh"].ToString());
                        bnphaithanhtoan.money_vanchuyen_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_vanchuyen_vp"].ToString());
                        bnphaithanhtoan.money_khac_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_khac_bh"].ToString());
                        bnphaithanhtoan.money_khac_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_khac_vp"].ToString());
                        bnphaithanhtoan.money_tong_bh = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_tong_bh"].ToString());
                        bnphaithanhtoan.money_tong_vp = Util_TypeConvertParse.ToDecimal(dataBaoCao.Rows[i]["money_tong_vp"].ToString());
                        bnphaithanhtoan.money_tong = bnphaithanhtoan.money_tong_bh + bnphaithanhtoan.money_tong_vp;
                        this.lstDataBaoCaoCurrent.Add(bnphaithanhtoan);
                    }

                    foreach (var item_data in this.lstDataBaoCaoCurrent)
                    {
                        ClassCommon.TinhBHYTThanhToanDTO tinhBHYT = new ClassCommon.TinhBHYTThanhToanDTO();
                        tinhBHYT.bhytcode = item_data.bhytcode;
                        tinhBHYT.bhyt_loaiid = item_data.bhyt_loaiid;
                        tinhBHYT.bhyt_tuyenbenhvien = item_data.bhyt_tuyenbenhvien;
                        tinhBHYT.chiphi_goidvktc = 0;
                        tinhBHYT.chiphi_trongpvql = item_data.money_tong_bh;
                        tinhBHYT.du5nam6thangluongcoban = item_data.du5nam6thangluongcoban;
                        tinhBHYT.loaivienphiid = item_data.loaivienphiid;
                        tinhBHYT.bhyt_thangluongtoithieu = item_data.bhyt_thangluongtoithieu;

                        //
                        decimal tyle_bntt = ((decimal)1 - TinhMucHuongBHYT.TinhSoTienBHYTThanhToan_TyLeTrungBinh(tinhBHYT));

                        item_data.money_tong_bntt = item_data.money_tong_vp + (item_data.money_tong_bh * tyle_bntt);
                        item_data.money_khambenh_bntt = item_data.money_khambenh_vp + (item_data.money_khambenh_bh * tyle_bntt);
                        item_data.money_xetnghiem_bntt = item_data.money_xetnghiem_vp + (item_data.money_xetnghiem_bh * tyle_bntt);
                        item_data.money_cdhatdcn_bntt = item_data.money_cdhatdcn_vp + (item_data.money_cdhatdcn_bh * tyle_bntt);
                        item_data.money_pttt_bntt = item_data.money_pttt_vp + (item_data.money_pttt_bh * tyle_bntt);
                        item_data.money_dvktc_bntt = item_data.money_dvktc_vp + (item_data.money_dvktc_bh * tyle_bntt);
                        item_data.money_mau_bntt = item_data.money_mau_vp + (item_data.money_mau_bh * tyle_bntt);
                        item_data.money_thuoc_bntt = item_data.money_thuoc_vp + (item_data.money_thuoc_bh * tyle_bntt);
                        item_data.money_vattu_bntt = item_data.money_vattu_vp + (item_data.money_vattu_bh * tyle_bntt);
                        item_data.money_vtthaythe_bntt = item_data.money_vtthaythe_vp + (item_data.money_vtthaythe_bh * tyle_bntt);
                        item_data.money_giuong_bntt = item_data.money_giuong_vp + (item_data.money_giuong_bh * tyle_bntt);
                        item_data.money_phuthu_bntt = item_data.money_phuthu_vp + (item_data.money_phuthu_bh * tyle_bntt);
                        item_data.money_vanchuyen_bntt = item_data.money_vanchuyen_vp + (item_data.money_vanchuyen_bh * tyle_bntt);
                        item_data.money_khac_bntt = item_data.money_khac_vp + (item_data.money_khac_bh * tyle_bntt);
                    }

                    gridControlDataBaoCao.DataSource = this.lstDataBaoCaoCurrent;
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

        #region Export
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
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_TienBenhNhanPhaiThanhToan_ChiTiet.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
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
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_TienBenhNhanPhaiThanhToan_ChiTiet.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.TienBNPhaiThanhToanDTO> lstData_XuatBaoCao = new List<ClassCommon.TienBNPhaiThanhToanDTO>();

                List<ClassCommon.TienBNPhaiThanhToanDTO> lstData_Group = this.lstDataBaoCaoCurrent.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.TienBNPhaiThanhToanDTO data_groupname = new ClassCommon.TienBNPhaiThanhToanDTO();
                    List<ClassCommon.TienBNPhaiThanhToanDTO> lstData_doanhthu = this.lstDataBaoCaoCurrent.Where(o => o.departmentgroupid == item_group.departmentgroupid).ToList();
                    decimal money_tong_bntt = 0;
                    decimal money_khambenh_bntt = 0;
                    decimal money_xetnghiem_bntt = 0;
                    decimal money_cdhatdcn_bntt = 0;
                    decimal money_pttt_bntt = 0;
                    decimal money_dvktc_bntt = 0;
                    decimal money_mau_bntt = 0;
                    decimal money_thuoc_bntt = 0;
                    decimal money_vattu_bntt = 0;
                    decimal money_vtthaythe_bntt = 0;
                    decimal money_giuong_bntt = 0;
                    decimal money_phuthu_bntt = 0;
                    decimal money_vanchuyen_bntt = 0;
                    decimal money_khac_bntt = 0;

                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        money_tong_bntt += item_tinhsum.money_tong_bntt;
                        money_khambenh_bntt += item_tinhsum.money_khambenh_bntt;
                        money_xetnghiem_bntt += item_tinhsum.money_xetnghiem_bntt;
                        money_cdhatdcn_bntt += item_tinhsum.money_cdhatdcn_bntt;
                        money_pttt_bntt += item_tinhsum.money_pttt_bntt;
                        money_dvktc_bntt += item_tinhsum.money_dvktc_bntt;
                        money_mau_bntt += item_tinhsum.money_mau_bntt;
                        money_thuoc_bntt += item_tinhsum.money_thuoc_bntt;
                        money_vattu_bntt += item_tinhsum.money_vattu_bntt;
                        money_vtthaythe_bntt += item_tinhsum.money_vtthaythe_bntt;
                        money_giuong_bntt += item_tinhsum.money_giuong_bntt;
                        money_phuthu_bntt += item_tinhsum.money_phuthu_bntt;
                        money_vanchuyen_bntt += item_tinhsum.money_vanchuyen_bntt;
                        money_khac_bntt += item_tinhsum.money_khac_bntt;

                    }

                    data_groupname.departmentgroupid = item_group.departmentgroupid;
                    data_groupname.stt = item_group.departmentgroupname;
                    data_groupname.money_tong_bntt = money_tong_bntt;
                    data_groupname.money_khambenh_bntt = money_khambenh_bntt;
                    data_groupname.money_xetnghiem_bntt = money_xetnghiem_bntt;
                    data_groupname.money_cdhatdcn_bntt = money_cdhatdcn_bntt;
                    data_groupname.money_pttt_bntt = money_pttt_bntt;
                    data_groupname.money_dvktc_bntt = money_dvktc_bntt;
                    data_groupname.money_mau_bntt = money_mau_bntt;
                    data_groupname.money_thuoc_bntt = money_thuoc_bntt;
                    data_groupname.money_vattu_bntt = money_vattu_bntt;
                    data_groupname.money_vtthaythe_bntt = money_vtthaythe_bntt;
                    data_groupname.money_giuong_bntt = money_giuong_bntt;
                    data_groupname.money_phuthu_bntt = money_phuthu_bntt;
                    data_groupname.money_vanchuyen_bntt = money_vanchuyen_bntt;
                    data_groupname.money_khac_bntt = money_khac_bntt;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
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
        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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



    }
}
