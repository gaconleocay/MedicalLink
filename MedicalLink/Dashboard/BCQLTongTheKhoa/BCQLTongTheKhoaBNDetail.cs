using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.BCQLTongTheKhoa
{
    public partial class BCQLTongTheKhoaBNDetail : Form
    {
        private int loaiLayDuLieu { get; set; }
        private string dateTuNgay { get; set; }
        private string dateDenNgay { get; set; }
        private long departmentgroupid { get; set; }
        private string dateKhoangDLTu { get; set; }
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public BCQLTongTheKhoaBNDetail()
        {
            InitializeComponent();
        }
        public BCQLTongTheKhoaBNDetail(int loai, string dateTuNgay, string dateDenNgay, long departmentgroupid, string dateKhoangDLTu)
        {
            //loai:
            //=1: BN hien dien
            //=2: BN chuyen di
            //=3: BN chuyen den
            //=4: BN ra vien
            //=9: SL BN da ra vien chua thanh toan
            //=13: SL BN da thanh toan trong ngay
            //=14: SL BN thanh toan trong ngay tinh theo doanh thu
            InitializeComponent();
            loaiLayDuLieu = loai;
            this.dateTuNgay = dateTuNgay;
            this.dateDenNgay = dateDenNgay;
            this.departmentgroupid = departmentgroupid;
            this.dateKhoangDLTu = dateKhoangDLTu;
        }

        private void BCTongTheKhoaBNDetail_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                LoadDataToGrid();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void LoadDataToGrid()
        {
            try
            {
                DataView dataBNDetail = new DataView();
                string sqlGetData = "";
                switch (loaiLayDuLieu)
                {
                    case 1: //BN hien dien
                        sqlGetData = "SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode, bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien, vpm.departmentgroupid, vpm.vienphidate, vpm.vienphidate_ravien, vpm.duyet_ngayduyet_vp, round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as money_giuong, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong,  round(cast((vpm.tam_ung) as numeric),0) as tam_ung FROM vienphi_money vpm inner join hosobenhan hsbn on vpm.hosobenhanid=hsbn.hosobenhanid left join bhyt bhyt on bhyt.bhytid=vpm.bhytid WHERE vpm.loaivienphiid=0 and vpm.vienphistatus=0 and vpm.vienphidate>='" + dateKhoangDLTu + "'  and vpm.departmentgroupid='" + departmentgroupid + "' ) A ORDER BY A.duyet_ngayduyet_vp;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN HIỆN DIỆN";
                        break;
                    case 2: //bn chuyen di
                        string lstbn_chuyendi = "0";
                        string sqlBNChuyenDi = "SELECT DISTINCT (mrd.vienphiid) as vienphiid FROM medicalrecord mrd  WHERE mrd.departmentgroupid='" + departmentgroupid + "' and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + dateTuNgay + "' and mrd.thoigianravien<='" + dateDenNgay + "' ;";
                        DataView dataBnChuyenDi = new DataView(condb.getDataTable(sqlBNChuyenDi));
                        if (dataBnChuyenDi != null && dataBnChuyenDi.Count > 0)
                        {
                            for (int i = 0; i < dataBnChuyenDi.Count; i++)
                            {
                                lstbn_chuyendi += "," + dataBnChuyenDi[i]["vienphiid"].ToString();
                            }
                        }

                        sqlGetData = "SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode,bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, vpm.vienphidate, vpm.vienphidate_ravien, vpm.duyet_ngayduyet_vp, round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as money_giuong, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong,  round(cast((vpm.tam_ung) as numeric),0) as tam_ung FROM vienphi_money vpm inner join hosobenhan hsbn on vpm.hosobenhanid=hsbn.hosobenhanid left join bhyt bhyt on bhyt.bhytid=vpm.bhytid WHERE vpm.vienphiid in (" + lstbn_chuyendi + ")) A ORDER BY A.vienphidate_ravien;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN CHUYỂN ĐI";
                        break;

                    case 3: //=3: BN chuyen den
                        string lstbn_chuyenden = "0";
                        string sqlBNChuyenDen = "SELECT DISTINCT (mrd.vienphiid) as vienphiid FROM medicalrecord mrd  WHERE mrd.departmentgroupid='" + departmentgroupid + "' and mrd.hinhthucvaovienid=3 and mrd.thoigianravien>='" + dateTuNgay + "' and mrd.thoigianravien<='" + dateDenNgay + "' ;";
                        DataView dataBnChuyenDen = new DataView(condb.getDataTable(sqlBNChuyenDen));
                        if (dataBnChuyenDen != null && dataBnChuyenDen.Count > 0)
                        {
                            for (int i = 0; i < dataBnChuyenDen.Count; i++)
                            {
                                lstbn_chuyenden += "," + dataBnChuyenDen[i]["vienphiid"].ToString();
                            }
                        }

                        sqlGetData = "SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode,bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, vpm.vienphidate, vpm.vienphidate_ravien, vpm.duyet_ngayduyet_vp, round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as money_giuong, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong,  round(cast((vpm.tam_ung) as numeric),0) as tam_ung FROM vienphi_money vpm inner join hosobenhan hsbn on vpm.hosobenhanid=hsbn.hosobenhanid left join bhyt bhyt on bhyt.bhytid=vpm.bhytid WHERE vpm.vienphiid in (" + lstbn_chuyenden + ")) A ORDER BY A.vienphidate_ravien;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN CHUYỂN ĐẾN";
                        break;

                    case 4: //=4: BN ra vien
                        sqlGetData = "SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode,bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, vpm.vienphidate, vpm.vienphidate_ravien, vpm.duyet_ngayduyet_vp, round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as money_giuong, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong,  round(cast((vpm.tam_ung) as numeric),0) as tam_ung FROM vienphi_money vpm inner join hosobenhan hsbn on vpm.hosobenhanid=hsbn.hosobenhanid left join bhyt bhyt on bhyt.bhytid=vpm.bhytid WHERE vpm.vienphistatus=1 and vpm.vienphidate_ravien>='" + dateTuNgay + "' and vpm.vienphidate_ravien<='" + dateDenNgay + "' and vpm.departmentgroupid='" + departmentgroupid + "' ) A ORDER BY A.vienphidate_ravien;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN";
                        break;

                    case 9: //=9: SL BN da ra vien chua thanh toan
                        sqlGetData = "SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode,bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, vpm.vienphidate, vpm.vienphidate_ravien, vpm.duyet_ngayduyet_vp, round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as money_giuong, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong,  round(cast((vpm.tam_ung) as numeric),0) as tam_ung FROM vienphi_money vpm inner join hosobenhan hsbn on vpm.hosobenhanid=hsbn.hosobenhanid left join bhyt bhyt on bhyt.bhytid=vpm.bhytid WHERE vpm.loaivienphiid=0 and COALESCE(vpm.vienphistatus_vp,0)=0 and vpm.vienphistatus<>0 and vpm.departmentgroupid='" + departmentgroupid + "'  and vpm.vienphidate>='" + dateKhoangDLTu + "') A ORDER BY A.duyet_ngayduyet_vp;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN CHƯA THANH TOÁN";
                        break;

                    case 13:  //SL BN da thanh toan trong ngay
                        sqlGetData = "SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode,bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, vpm.vienphidate, vpm.vienphidate_ravien, vpm.duyet_ngayduyet_vp, round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as money_giuong, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong,  round(cast((vpm.tam_ung) as numeric),0) as tam_ung FROM vienphi_money vpm inner join hosobenhan hsbn on vpm.hosobenhanid=hsbn.hosobenhanid left join bhyt bhyt on bhyt.bhytid=vpm.bhytid WHERE vpm.loaivienphiid=0 and COALESCE(vpm.vienphistatus_vp,0)=1 and vpm.duyet_ngayduyet_vp >= '" + this.dateTuNgay + "' and vpm.duyet_ngayduyet_vp <= '" + this.dateDenNgay + "' and vpm.departmentgroupid='" + departmentgroupid + "' ) A ORDER BY A.duyet_ngayduyet_vp ;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN ĐÃ THANH TOÁN";

                        break;
                    case 14:  //SL BN da thanh toan tinh theo doanh thu khoa
                        sqlGetData = "";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN ĐÃ THANH TOÁN THEO DOANH THU KHOA";

                        break;
                    default:
                        break;
                }
                dataBNDetail = new DataView(condb.getDataTable(sqlGetData));
                HienThiDuLieu(dataBNDetail);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void HienThiDuLieu(DataView DataBaoCao)
        {
            try
            {
                List<ClassCommon.classBCQLTongTheKhoaBNDetail> lstBCQL = new List<ClassCommon.classBCQLTongTheKhoaBNDetail>();
                if (DataBaoCao != null && DataBaoCao.Count > 0)
                {
                    for (int i = 0; i < DataBaoCao.Count; i++)
                    {
                        ClassCommon.classBCQLTongTheKhoaBNDetail databaocao = new ClassCommon.classBCQLTongTheKhoaBNDetail();
                        databaocao.stt = Utilities.Util_TypeConvertParse.ToInt64(DataBaoCao[i]["stt"].ToString());
                        databaocao.vienphiid = Utilities.Util_TypeConvertParse.ToInt64(DataBaoCao[i]["vienphiid"].ToString());
                        databaocao.patientid = Utilities.Util_TypeConvertParse.ToInt64(DataBaoCao[i]["patientid"].ToString());
                        databaocao.patientname = DataBaoCao[i]["patientname"].ToString();
                        databaocao.bhytcode = DataBaoCao[i]["bhytcode"].ToString();
                        databaocao.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt64(DataBaoCao[i]["departmentgroupid"].ToString());
                        databaocao.vienphidate = Utilities.Util_TypeConvertParse.ToDateTime(DataBaoCao[i]["vienphidate"].ToString());
                        databaocao.vienphidate_ravien = Utilities.Util_TypeConvertParse.ToDateTime(DataBaoCao[i]["vienphidate_ravien"].ToString());
                        databaocao.duyet_ngayduyet_vp = Utilities.Util_TypeConvertParse.ToDateTime(DataBaoCao[i]["duyet_ngayduyet_vp"].ToString());
                        databaocao.money_khambenh = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_khambenh"].ToString());
                        databaocao.money_xetnghiem = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_xetnghiem"].ToString());
                        databaocao.money_cdhatdcn = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_cdhatdcn"].ToString());
                        databaocao.money_pttt = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_pttt"].ToString());
                        databaocao.money_dvktc = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_dvktc"].ToString());
                        databaocao.money_giuong = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_giuong"].ToString());
                        databaocao.money_khac = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_khac"].ToString());
                        databaocao.money_vattu = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_vattu"].ToString());
                        databaocao.money_mau = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_mau"].ToString());
                        databaocao.money_thuoc = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_thuoc"].ToString());
                        databaocao.money_tong = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["money_tong"].ToString());
                        databaocao.tam_ung = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["tam_ung"].ToString());
                        databaocao.ty_le_thuoc = Utilities.Util_TypeConvertParse.ToDecimal(DataBaoCao[i]["ty_le_thuoc"].ToString());

                        databaocao.bhyt_loaiid = Utilities.Util_TypeConvertParse.ToInt16(DataBaoCao[i]["bhyt_loaiid"].ToString());
                        databaocao.loaivienphiid = Utilities.Util_TypeConvertParse.ToInt16(DataBaoCao[i]["loaivienphiid"].ToString());
                        databaocao.du5nam6thangluongcoban = Utilities.Util_TypeConvertParse.ToInt16(DataBaoCao[i]["du5nam6thangluongcoban"].ToString());
                        databaocao.bhyt_tuyenbenhvien = Utilities.Util_TypeConvertParse.ToInt16(DataBaoCao[i]["bhyt_tuyenbenhvien"].ToString());

                        databaocao.muchuong = DatabaseProcess.TinhMuaHuongBHYT.TinhMucHuongTheoTheBHYT(databaocao.bhytcode, databaocao.bhyt_loaiid, databaocao.loaivienphiid, databaocao.du5nam6thangluongcoban, databaocao.bhyt_tuyenbenhvien);
                        lstBCQL.Add(databaocao);
                    }
                }
                gridControlBNDetail.DataSource = lstBCQL;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }



        private void gridViewBNDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
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
                if (gridViewBNDetail.RowCount > 0)
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
                                        gridViewBNDetail.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewBNDetail.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewBNDetail.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewBNDetail.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewBNDetail.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewBNDetail.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                timerThongBao.Start();
                                lblThongBao.Visible = true;
                                lblThongBao.Text = "Export dữ liệu thành công!";
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
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Không có dữ liệu!";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void gridViewBNDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }



    }
}
