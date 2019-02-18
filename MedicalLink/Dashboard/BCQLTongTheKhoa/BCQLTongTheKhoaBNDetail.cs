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
        #region Khai bao
        private QLTongTheKhoaDetailFilterDTO filter { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        #endregion

        public BCQLTongTheKhoaBNDetail()
        {
            InitializeComponent();
        }

        #region Load
        public BCQLTongTheKhoaBNDetail(QLTongTheKhoaDetailFilterDTO _filter)
        {
            //loai:
            //=1: BN hien dien
            //=2: BN chuyen di
            //=3: BN chuyen den
            //=4: BN ra vien
            //=5: SL BN da ra vien chua thanh toan
            //=6: SL BN da thanh toan trong ngay
            //=7: SL BN thanh toan trong ngay tinh theo doanh thu
            InitializeComponent();
            this.filter = _filter;
        }

        private void BCTongTheKhoaBNDetail_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                LoadDataToGrid();

                if (this.filter.loaiLayDuLieu == 5)
                {
                    btnRVChuaTT_1_6.Visible = true;
                    btnRVChuaTT_7.Visible = true;
                }
                else if (this.filter.loaiLayDuLieu == 6 || this.filter.loaiLayDuLieu == 7 || this.filter.loaiLayDuLieu == 8 || this.filter.loaiLayDuLieu == 9)
                {
                    btnRVChuaTT_1_6.Visible = false;
                    btnRVChuaTT_7.Visible = true;
                    btnRVChuaTT_7.Text = "(ngày thanh toán - ngày ra viện)>=7 ngày";
                }
                else
                {
                    btnRVChuaTT_1_6.Visible = false;
                    btnRVChuaTT_7.Visible = false;
                }

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void LoadDataToGrid()
        {
            try
            {
                string sqlGetData = "";
                string _tieuchi_ravien_vp = "";
                string _tieuchi_ravien_spt = "";
                string _lstbn_chuyenden_di = "";
                string _tieuchi_songay = " -1 as songay, ";
                string _tieuchi_songaythanhtoan = " -1 as songaythanhtoan, ";
                string _thutienstatus = "";
                string _doituongbenhnhanid_spt = "";
                string _doituongbenhnhanid_vp = "";
                string _doituongbenhnhanid_mrd = "";
                string _doituongbenhnhanid_ser = "";
                string _tieuchi_hsba = " and hosobenhandate>'2017-01-01 00:00:00' ";
                string _tieuchi_bh = " and bhytdate>'2017-01-01 00:00:00' ";

                if (this.filter.thutienstatus)
                {
                    _thutienstatus = " and thutienstatus=1 ";
                }
                if (this.filter.cboDoiTuongText == "Đối tượng BHYT")
                {
                    _doituongbenhnhanid_spt = " and doituongbenhnhanid=1 ";
                    _doituongbenhnhanid_vp = " and doituongbenhnhanid=1 ";
                    _doituongbenhnhanid_mrd = " and doituongbenhnhanid=1 ";
                }
                else if (this.filter.cboDoiTuongText == "Đối tượng Viện phí")
                {
                    _doituongbenhnhanid_spt = " and doituongbenhnhanid<>1 ";
                    _doituongbenhnhanid_vp = " and doituongbenhnhanid<>1 ";
                    _doituongbenhnhanid_mrd = " and doituongbenhnhanid=1 ";
                }
                else if (this.filter.cboDoiTuongText == "ĐT BHYT + DV BHYT")
                {
                    _doituongbenhnhanid_spt = " and doituongbenhnhanid=1 ";
                    _doituongbenhnhanid_vp = " and doituongbenhnhanid=1 ";
                    _doituongbenhnhanid_mrd = " and doituongbenhnhanid=1 ";
                    _doituongbenhnhanid_ser = " and loaidoituong in (0,2,4,6,20) ";
                }

                switch (this.filter.loaiLayDuLieu)
                {
                    case 1: //BN hien dien 22/9/2018
                        {
                            sqlGetData = $@"SELECT ROW_NUMBER() OVER (ORDER BY hsbn.patientname) as stt, 
	VPP.*, 
	vp.vienphiid, 
	vp.patientid, 
	hsbn.patientname, 
	bhyt.bhytcode, 
	bhyt.bhyt_loaiid, 
	vp.loaivienphiid, 
	bhyt.du5nam6thangluongcoban, 
	vp.bhyt_tuyenbenhvien, 
	vp.departmentgroupid, 
	'' as departmentname, 
	vp.vienphidate, 
	vp.vienphidate_ravien, 
	-1 as songay,
	-1 as songaythanhtoan,
	vp.duyet_ngayduyet_vp, 
	case when VPP.money_tong<>0 then round(cast((VPP.money_thuoc/VPP.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM 
	(SELECT count(*) as dangdt_slbn, 
		 vpm.hosobenhanid, 
		 vpm.departmentid, 
		 sum(vpm.money_khambenh) as money_khambenh,  
		 sum(vpm.money_xetnghiem) as money_xetnghiem,  
		 sum(coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0)) as money_cdhatdcn,  
		 sum(vpm.money_pttt) as money_pttt, 
		 sum(vpm.money_ptttyeucau) as money_ptttyeucau, 
		 sum(vpm.money_dvktc) as money_dvktc,  
		 sum(vpm.money_giuongthuong) as money_giuongthuong,  
		 sum(vpm.money_giuongyeucau) as money_giuongyeucau,  
		 sum(coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0)) as money_khac,   
		 sum(vpm.money_vattu) as money_vattu,  
		sum(vpm.money_vattu_ttrieng) as money_vattu_ttrieng,
		 sum(vpm.money_mau) as money_mau,  
		 sum(vpm.money_thuoc) as money_thuoc,  
		 sum(coalesce(vpm.money_khambenh,0) + coalesce(vpm.money_xetnghiem,0) + coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0) + coalesce(vpm.money_pttt,0) + coalesce(vpm.money_dvktc,0) + coalesce(vpm.money_giuongthuong,0) + coalesce(vpm.money_giuongyeucau,0) + coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0) + coalesce(vpm.money_vattu,0) + coalesce(vpm.money_mau,0) + coalesce(vpm.money_thuoc,0) + coalesce(vpm.money_ptttyeucau,0) + coalesce(vpm.money_vattu_ttrieng,0)) as money_tong, 
		 sum(vpm.tam_ung) as tam_ung  
	FROM
	(
		SELECT mrd.vienphiid,
		mrd.hosobenhanid, 
		mrd.loaibenhanid,  
		mrd.departmentid, 
		mrd.doituongbenhnhanid, 
		sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong=0
					then (case when mrd.loaibenhanid=24 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
									then ser.servicepricemoney_bhyt*ser.soluong
								when mrd.loaibenhanid=1 
									then ser.servicepricemoney_bhyt*ser.soluong
								else 0 end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
							   else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
							   else ser.servicepricemoney*ser.soluong end)			   
				else 0 end) as money_khambenh,		
		sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong=0
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney*ser.soluong end)	
				else 0 end) as money_xetnghiem,
		sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_cdha,	 
		sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)		
				else 0 end) as money_tdcn,	 	 
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 
								then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney 
								else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_pttt,
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 
								then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney 
								else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_ptttyeucau, 
		sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)	
				else 0 end) as money_mau,
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)	
				else 0 end) as money_giuongthuong,	
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)			
				else 0 end) as money_giuongyeucau,	
		sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_vanchuyen, 	 
		sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_khac, 	 
		sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_phuthu,	 
		(sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) 
				then ser.servicepricemoney_bhyt*ser.soluong else 0 end) 
		+ sum(case when ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=mrd.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
				then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan * ser.soluong) end)
				else 0 end)		
		+ sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end)) as money_dvktc, 	
		sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)		
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)	
				else 0 end) as money_thuoc,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)		
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)		
				else 0 end) as money_vattu,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
				else 0 end) as money_vattu_ttrieng,
		(select sum(bill.datra) from bill where bill.vienphiid=mrd.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung		
		FROM (select vienphiid,hosobenhanid,loaibenhanid,departmentid,doituongbenhnhanid from medicalrecord where thoigianvaovien >'{this.filter.dateKhoangDLTu}' and medicalrecordstatus=2 and departmentid in ({this.filter.lstPhongChonLayBC}) {_doituongbenhnhanid_mrd}) mrd 
			left join (select vienphiid,bhyt_groupcode,maubenhphamphieutype,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,loaidoituong,doituongbenhnhanid,lankhambenh,servicepricecode,servicepriceid_master,servicepriceid_thanhtoanrieng from serviceprice where servicepricedate >'{this.filter.dateKhoangDLTu}' and thuockhobanle=0 {_doituongbenhnhanid_ser}) ser on mrd.vienphiid=ser.vienphiid
		GROUP BY mrd.vienphiid,mrd.hosobenhanid,mrd.loaibenhanid,mrd.departmentid,mrd.doituongbenhnhanid
	) vpm
	GROUP BY vpm.hosobenhanid,vpm.departmentid) VPP
inner join (select vienphiid,hosobenhanid,patientid,loaivienphiid,bhyt_tuyenbenhvien,departmentgroupid,vienphidate,bhytid,TO_CHAR(vienphidate_ravien,'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien,TO_CHAR(duyet_ngayduyet_vp,'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp from vienphi where vienphistatus=0 and vienphidate>'{this.filter.dateKhoangDLTu}' {_doituongbenhnhanid_vp}) vp on vpp.hosobenhanid=vp.hosobenhanid
inner join (select hosobenhanid,patientname from hosobenhan where 1=1 and hosobenhandate>'{this.filter.dateKhoangDLTu}') hsbn on vp.hosobenhanid=hsbn.hosobenhanid 	
inner join (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt where bhytdate>'{this.filter.dateKhoangDLTu}') bhyt on bhyt.bhytid=vp.bhytid;";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN HIỆN DIỆN";
                            break;
                        }
                    case 2: //bn chuyen di
                        {
                            _lstbn_chuyenden_di = "0";
                            string sqlBNChuyenDi = "SELECT DISTINCT (mrd.vienphiid) as vienphiid FROM medicalrecord mrd  WHERE mrd.departmentid in (" + this.filter.lstPhongChonLayBC + ") and mrd.hinhthucravienid=8 and mrd.thoigianravien between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' ;";
                            DataView dataBnChuyenDi = new DataView(condb.GetDataTable_HIS(sqlBNChuyenDi));
                            if (dataBnChuyenDi != null && dataBnChuyenDi.Count > 0)
                            {
                                for (int i = 0; i < dataBnChuyenDi.Count; i++)
                                {
                                    _lstbn_chuyenden_di += "," + dataBnChuyenDi[i]["vienphiid"].ToString();
                                }
                            }
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN CHUYỂN ĐI";
                            break;
                        }
                    case 3: //=3: BN chuyen den
                        {
                            _lstbn_chuyenden_di = "0";
                            string sqlBNChuyenDen = "SELECT DISTINCT (mrd.vienphiid) as vienphiid FROM medicalrecord mrd  WHERE mrd.departmentid in (" + this.filter.lstPhongChonLayBC + ") and mrd.hinhthucvaovienid=3 and mrd.thoigianravien between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' ;";
                            DataView dataBnChuyenDen = new DataView(condb.GetDataTable_HIS(sqlBNChuyenDen));
                            if (dataBnChuyenDen != null && dataBnChuyenDen.Count > 0)
                            {
                                for (int i = 0; i < dataBnChuyenDen.Count; i++)
                                {
                                    _lstbn_chuyenden_di += "," + dataBnChuyenDen[i]["vienphiid"].ToString();
                                }
                            }
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN CHUYỂN ĐẾN";
                            break;
                        }
                    case 4: //=4: BN ra vien
                        {
                            _tieuchi_ravien_spt = " and vienphistatus<>0 and vienphidate_ravien between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and phongravien in (" + this.filter.lstPhongChonLayBC + ") ";
                            _tieuchi_ravien_vp = " and vp.departmentid in (" + this.filter.lstPhongChonLayBC + ") and vp.vienphidate_ravien between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and vp.vienphistatus<>0 ";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN";
                            break;
                        }
                    case 5: // SL BN da ra vien chua thanh toan
                        {
                            _tieuchi_songay = " DATE_PART('day', now() - spt.vienphidate_ravien) as songay, ";

                            _tieuchi_ravien_spt = " and COALESCE(vienphistatus_vp,0)=0 and vienphistatus<>0 and vienphidate_ravien>='" + this.filter.dateKhoangDLTu + "' and phongravien in (" + this.filter.lstPhongChonLayBC + ") ";
                            _tieuchi_ravien_vp = " and vp.departmentid in (" + this.filter.lstPhongChonLayBC + ") and vp.vienphidate_ravien>='" + this.filter.dateKhoangDLTu + "' and COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 ";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN CHƯA THANH TOÁN";
                            break;
                        }
                    case 6:  //SL BN da thanh toan trong ngay
                        {
                            _tieuchi_songaythanhtoan = " ((spt.duyet_ngayduyet_vp::date)-(spt.vienphidate_ravien::date)) as songaythanhtoan, ";
                            _tieuchi_ravien_spt = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and phongravien in (" + this.filter.lstPhongChonLayBC + ") ";
                            _tieuchi_ravien_vp = " and vp.departmentid in (" + this.filter.lstPhongChonLayBC + ") and vp.duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and vp.vienphistatus_vp=1 ";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN ĐÃ THANH TOÁN";
                            break;
                        }
                    case 7:  //SL BN da thanh toan tinh theo doanh thu khoa
                        {
                            _tieuchi_songaythanhtoan = " ((spt.duyet_ngayduyet_vp::date)-(spt.vienphidate_ravien::date)) as songaythanhtoan, ";
                            _tieuchi_ravien_spt = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and departmentid in (" + this.filter.lstPhongChonLayBC + ") ";
                            _tieuchi_ravien_vp = " and b.departmentid in (" + this.filter.lstPhongChonLayBC + ") and vp.duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and vp.vienphistatus_vp=1 ";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN ĐÃ THANH TOÁN THEO DOANH THU KHOA";
                            break;
                        }
                    case 8:  //SL BN da thanh toan tinh theo doanh thu khoa Gay me
                        {
                            _tieuchi_songaythanhtoan = " ((spt.duyet_ngayduyet_vp::date)-(spt.vienphidate_ravien::date)) as songaythanhtoan, ";
                            _tieuchi_ravien_spt = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and departmentid in (34,335,269,285) and departmentgroup_huong in (" + this.filter.lstKhoaChonLayBC + ") ";
                            _tieuchi_ravien_vp = " and b.departmentid in (" + this.filter.lstPhongChonLayBC + ") and vp.duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and vp.vienphistatus_vp=1 ";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN ĐÃ THANH TOÁN THEO DOANH THU KHOA TẠI GÂY MÊ HỒI TỈNH";
                            break;
                        }
                    case 9:  //SL BN da thanh toan tinh theo doanh thu tổng
                        {
                            _tieuchi_songaythanhtoan = " ((spt.duyet_ngayduyet_vp::date)-(spt.vienphidate_ravien::date)) as songaythanhtoan, ";
                            _tieuchi_ravien_spt = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and (departmentid in (" + this.filter.lstPhongChonLayBC + ") or (departmentid in (34,335,269,285) and departmentgroup_huong in (" + this.filter.lstKhoaChonLayBC + "))) ";
                            _tieuchi_ravien_vp = " and b.departmentid in (" + this.filter.lstPhongChonLayBC + ",34,335,269,285) and vp.duyet_ngayduyet_vp between '" + this.filter.dateTuNgay + "' and '" + this.filter.dateDenNgay + "' and vp.vienphistatus_vp=1 ";
                            lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN RA VIỆN ĐÃ THANH TOÁN THEO DOANH THU KHOA + GÂY MÊ HỒI TỈNH";
                            break;
                        }
                    default:
                        break;
                }

                //SQL BN chuyen di va BN chuyen den
                if (this.filter.loaiLayDuLieu == 2 || this.filter.loaiLayDuLieu == 3)
                {
                    sqlGetData = $@"SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
	A.*, 
	case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM 
(SELECT vpm.vienphiid, 
		vpm.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		vpm.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, 
		prv.departmentname, 
		vpm.vienphidate, 
		TO_CHAR(vpm.vienphidate_ravien,'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		-1 as songay, 
		-1 as songaythanhtoan,
		TO_CHAR(vpm.duyet_ngayduyet_vp,'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
		round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt,
		round(cast((vpm.money_ptttyeucau_bh + vpm.money_ptttyeucau_vp) as numeric),0) as money_ptttyeucau,
		round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, 
		round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu,
		round(cast((vpm.money_vattu_ttrieng_bh + vpm.money_vattu_ttrieng_vp) as numeric),0) as money_vattu_ttrieng, 
		round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, 
		round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp + vpm.money_ptttyeucau_bh + vpm.money_ptttyeucau_vp + vpm.money_vattu_ttrieng_bh + vpm.money_vattu_ttrieng_vp) as numeric),0) as money_tong, 
		round(cast((vpm.tam_ung) as numeric),0) as tam_ung 
FROM (SELECT vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, vp.vienphistatus_bh,vp.duyet_ngayduyet_bh,vp.bhyt_tuyenbenhvien,
		sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (0,4,6) 
					then (case when vp.loaivienphiid=0 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
									then ser.servicepricemoney_bhyt*ser.soluong
								when vp.loaivienphiid=1 
									then ser.servicepricemoney_bhyt*ser.soluong
								else 0 end)
				else 0 end) as money_khambenh_bh,		
		sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-ser.servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_khambenh_vp,
		sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_xetnghiem_bh,
		sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_xetnghiem_vp,
		sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_cdha_bh,
		sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_cdha_vp,	 
		sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_tdcn_bh,
		sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_tdcn_vp,	 	 
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				else 0 end) as money_pttt_bh,	 
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_pttt_vp,
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				else 0 end) as money_ptttyeucau_bh,	
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_ptttyeucau_vp,	 
		sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_mau_bh,
		sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
			when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
			 when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
			 else 0 end) as money_mau_vp,
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				else 0 end) as money_giuongthuong_bh,	 
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_giuongthuong_vp,	 
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				else 0 end) as money_giuongyeucau_bh,	
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_giuongyeucau_vp,	
		sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_vanchuyen_bh,
		sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_vanchuyen_vp,	 	 
		sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_khac_bh,
		sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_khac_vp,	 	 
		sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_phuthu_bh,
		sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
			 when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
			 when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
			 else 0 end) as money_phuthu_vp,	 
		sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) 
				then ser.servicepricemoney_bhyt*ser.soluong else 0 end) 
		+ sum(case when ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
					then (case when ser.maubenhphamphieutype=0 
								then ser.servicepricemoney_nhandan*ser.soluong 
							  else 0-(ser.servicepricemoney_nhandan * ser.soluong) end)
				else 0 end) as money_dvktc_bh,	
		sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_dvktc_vp, 
		sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_thuoc_bh,
		sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
			when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
			 when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
			 else 0 end) as money_thuoc_vp,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_vattu_bh,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
			when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
			 when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 then 
				(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
			 else 0 end) as money_vattu_vp,
		sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1
					then (case when ser.maubenhphamphieutype=0 
									then servicepricemoney_bhyt*ser.soluong
								else 0-(servicepricemoney_bhyt*ser.soluong) end)
				when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
			else 0 end) as money_vattu_ttrieng_bh,
		sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 
				then (case when vp.doituongbenhnhanid<>1
								then (case when ser.maubenhphamphieutype=0 
												then servicepricemoney_bhyt*ser.soluong
											else 0-(servicepricemoney_bhyt*ser.soluong) end)
						else 0 end)
			else 0 end) as money_vattu_ttrieng_vp,
		(select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung
	FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,departmentgroupid,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,vienphistatus_vp,duyet_ngayduyet_vp,vienphistatus_bh,duyet_ngayduyet_bh,bhyt_tuyenbenhvien 
			from vienphi where vienphiid in ({_lstbn_chuyenden_di}) {_doituongbenhnhanid_vp}) vp 
		left join (select * from serviceprice where thuockhobanle=0 and vienphiid in ({_lstbn_chuyenden_di}) {_doituongbenhnhanid_ser}) ser on vp.vienphiid=ser.vienphiid
	GROUP BY vp.vienphiid,vp.patientid,vp.bhytid,vp.hosobenhanid,vp.loaivienphiid,vp.vienphistatus,vp.departmentgroupid,vp.departmentid,vp.doituongbenhnhanid,vp.vienphidate,vp.vienphidate_ravien,vp.duyet_ngayduyet,vp.vienphistatus_vp,vp.duyet_ngayduyet_vp,vienphistatus_bh,duyet_ngayduyet_bh,bhyt_tuyenbenhvien) vpm 
inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsbn on vpm.hosobenhanid=hsbn.hosobenhanid inner join (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt where 1=1 {_tieuchi_bh}) bhyt on bhyt.bhytid=vpm.bhytid 
INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON vpm.departmentid=prv.departmentid) A;";
                }
                else if (this.filter.loaiLayDuLieu == 4 || this.filter.loaiLayDuLieu == 5 || this.filter.loaiLayDuLieu == 6 || this.filter.loaiLayDuLieu == 7 || this.filter.loaiLayDuLieu == 8 || this.filter.loaiLayDuLieu == 9)
                {
                    if (this.filter.cboDoiTuongText == "ĐT BHYT + DV BHYT")
                    {
                        sqlGetData = $@"SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
	A.*, 
	BILL.tam_ung, 
	case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM 
(SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.khoaravien as departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 
		'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		{_tieuchi_songay}
		{_tieuchi_songaythanhtoan}
		TO_CHAR(spt.duyet_ngayduyet_vp,'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
		round(cast(sum(spt.money_khambenh_bh) as numeric),0) as money_khambenh, 
		round(cast(sum(spt.money_xetnghiem_bh) as numeric),0) as money_xetnghiem, 
		round(cast(sum(spt.money_cdha_bh + spt.money_tdcn_bh) as numeric),0) as money_cdhatdcn, 
		round(cast(sum(spt.money_pttt_bh) as numeric),0) as money_pttt, 
		round(cast(sum(spt.money_ptttyeucau_bh) as numeric),0) as money_ptttyeucau,
		round(cast(sum(spt.money_dvktc_bh) as numeric),0) as money_dvktc, 
		round(cast(sum(spt.money_giuongthuong_bh) as numeric),0) as money_giuongthuong, 
		round(cast(sum(spt.money_giuongyeucau_bh) as numeric),0) as money_giuongyeucau, 
		round(cast(sum(spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_phuthu_bh) as numeric),0) as money_khac, 
		round(cast(sum(spt.money_vattu_bh + spt.money_vtthaythe_bh) as numeric),0) as money_vattu, 
		round(cast(sum(spt.money_vattu_ttrieng_bh) as numeric),0) as money_vattu_ttrieng, 
		round(cast(sum(spt.money_mau_bh) as numeric),0) as money_mau, 
		round(cast(sum(spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh) as numeric),0) as money_thuoc, 
		round(cast(sum(spt.money_khambenh_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_phuthu_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_mau_bh + spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_ptttyeucau_bh + spt.money_vattu_ttrieng_bh) as numeric),0) as money_tong 
	FROM (select * from ihs_servicespttt where 1=1 {_tieuchi_ravien_spt} {_thutienstatus} {_doituongbenhnhanid_spt}) spt 
		inner join (select * from hosobenhan where 1=1 {_tieuchi_hsba}) hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
		inner join (select * from bhyt where 1=1 {_tieuchi_bh}) bhyt on bhyt.bhytid=spt.bhytid 
		left join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
	GROUP BY spt.vienphiid,spt.patientid,hsbn.patientname,bhyt.bhytcode,bhyt.bhyt_loaiid,spt.loaivienphiid,bhyt.du5nam6thangluongcoban,spt.bhyt_tuyenbenhvien,spt.khoaravien,prv.departmentname,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp) A 
LEFT JOIN (select sum(b.datra) as tam_ung,vp.vienphiid from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
			where 1=1 {_tieuchi_ravien_vp}
			group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid;";
                    }
                    else
                    {
                        sqlGetData = $@"SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
	A.*, 
	BILL.tam_ung, 
	case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM 
(SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.khoaravien as departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 
		'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		{_tieuchi_songay}
		{_tieuchi_songaythanhtoan}
		TO_CHAR(spt.duyet_ngayduyet_vp,'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
		round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast(sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast(sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast(sum(spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast(sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as numeric),0) as money_ptttyeucau,
		round(cast(sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast(sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast(sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast(sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast(sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast(sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as numeric),0) as money_vattu_ttrieng, 
		round(cast(sum(spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast(sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc, 
		round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp + spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp + spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as numeric),0) as money_tong 
	FROM (select * from ihs_servicespttt where 1=1 {_tieuchi_ravien_spt} {_thutienstatus} {_doituongbenhnhanid_spt}) spt 
		inner join (select * from hosobenhan where 1=1 {_tieuchi_hsba}) hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
		inner join (select * from bhyt where 1=1 {_tieuchi_bh}) bhyt on bhyt.bhytid=spt.bhytid 
		left join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
	GROUP BY spt.vienphiid,spt.patientid,hsbn.patientname,bhyt.bhytcode,bhyt.bhyt_loaiid,spt.loaivienphiid,bhyt.du5nam6thangluongcoban,spt.bhyt_tuyenbenhvien,spt.khoaravien,prv.departmentname,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp) A 
LEFT JOIN (select sum(b.datra) as tam_ung,vp.vienphiid from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
			where 1=1 {_tieuchi_ravien_vp}
			group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid;";
                    }
                }

                DataView dataBNDetail = new DataView(condb.GetDataTable_HIS(sqlGetData));
                HienThiDuLieu(dataBNDetail);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                        databaocao.stt = Utilities.TypeConvertParse.ToInt64(DataBaoCao[i]["stt"].ToString());
                        databaocao.vienphiid = Utilities.TypeConvertParse.ToInt64(DataBaoCao[i]["vienphiid"].ToString());
                        databaocao.patientid = Utilities.TypeConvertParse.ToInt64(DataBaoCao[i]["patientid"].ToString());
                        databaocao.patientname = DataBaoCao[i]["patientname"].ToString();
                        databaocao.bhytcode = DataBaoCao[i]["bhytcode"].ToString();
                        databaocao.departmentgroupid = Utilities.TypeConvertParse.ToInt64(DataBaoCao[i]["departmentgroupid"].ToString());
                        databaocao.departmentname = DataBaoCao[i]["departmentname"].ToString();
                        databaocao.vienphidate = Utilities.TypeConvertParse.ToDateTime(DataBaoCao[i]["vienphidate"].ToString());
                        if (DataBaoCao[i]["vienphidate_ravien"].ToString() != null && DataBaoCao[i]["vienphidate_ravien"].ToString() != "" && DataBaoCao[i]["vienphidate_ravien"].ToString() != "0001-01-01 12:01:00")
                        {
                            databaocao.vienphidate_ravien = DataBaoCao[i]["vienphidate_ravien"].ToString();
                        }
                        databaocao.songay = Utilities.TypeConvertParse.ToInt32(DataBaoCao[i]["songay"].ToString());
                        databaocao.songaythanhtoan = Utilities.TypeConvertParse.ToInt32(DataBaoCao[i]["songaythanhtoan"].ToString());
                        if (DataBaoCao[i]["duyet_ngayduyet_vp"].ToString() != null && DataBaoCao[i]["duyet_ngayduyet_vp"].ToString() != "" && DataBaoCao[i]["duyet_ngayduyet_vp"].ToString() != "0001-01-01 12:01:00")
                        {
                            databaocao.duyet_ngayduyet_vp = DataBaoCao[i]["duyet_ngayduyet_vp"].ToString();
                        }

                        databaocao.money_khambenh = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_khambenh"].ToString());
                        databaocao.money_xetnghiem = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_xetnghiem"].ToString());
                        databaocao.money_cdhatdcn = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_cdhatdcn"].ToString());
                        databaocao.money_pttt = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_pttt"].ToString());
                        databaocao.money_ptttyeucau = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_ptttyeucau"].ToString());
                        databaocao.money_dvktc = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_dvktc"].ToString());
                        databaocao.money_giuongthuong = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_giuongthuong"].ToString());
                        databaocao.money_giuongyeucau = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_giuongyeucau"].ToString());
                        databaocao.money_khac = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_khac"].ToString());
                        databaocao.money_vattu = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_vattu"].ToString());
                        databaocao.money_vattu_ttrieng = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_vattu_ttrieng"].ToString());
                        databaocao.money_mau = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_mau"].ToString());
                        databaocao.money_thuoc = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_thuoc"].ToString());
                        databaocao.money_tong = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["money_tong"].ToString());
                        databaocao.tam_ung = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["tam_ung"].ToString());
                        databaocao.ty_le_thuoc = Utilities.TypeConvertParse.ToDecimal(DataBaoCao[i]["ty_le_thuoc"].ToString());

                        databaocao.bhyt_loaiid = Utilities.TypeConvertParse.ToInt16(DataBaoCao[i]["bhyt_loaiid"].ToString());
                        databaocao.loaivienphiid = Utilities.TypeConvertParse.ToInt16(DataBaoCao[i]["loaivienphiid"].ToString());
                        databaocao.du5nam6thangluongcoban = Utilities.TypeConvertParse.ToInt16(DataBaoCao[i]["du5nam6thangluongcoban"].ToString());
                        databaocao.bhyt_tuyenbenhvien = Utilities.TypeConvertParse.ToInt16(DataBaoCao[i]["bhyt_tuyenbenhvien"].ToString());

                        ClassCommon.TinhBHYTThanhToanDTO tinhBHYT = new ClassCommon.TinhBHYTThanhToanDTO();
                        tinhBHYT.bhytcode = databaocao.bhytcode;
                        tinhBHYT.bhyt_loaiid = databaocao.bhyt_loaiid;
                        tinhBHYT.bhyt_tuyenbenhvien = databaocao.bhyt_tuyenbenhvien;
                        tinhBHYT.du5nam6thangluongcoban = databaocao.du5nam6thangluongcoban;
                        tinhBHYT.loaivienphiid = databaocao.loaivienphiid;

                        databaocao.muchuong = DatabaseProcess.TinhMucHuongBHYT.TinhMucHuongTheoTheBHYT(tinhBHYT);
                        lstBCQL.Add(databaocao);
                    }
                }
                gridControlBNDetail.DataSource = lstBCQL;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewBNDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void gridViewBNDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    int tongsongay = Utilities.TypeConvertParse.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["songay"]));
                    if (tongsongay >= 1 && tongsongay <= 6)
                    {
                        e.Appearance.BackColor = Color.PeachPuff;
                        //e.Appearance.BackColor2 = Color.LightCyan;
                        e.HighPriority = true;
                    }
                    else if (tongsongay > 6)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        //e.Appearance.BackColor2 = Color.LightCyan; 
                        e.HighPriority = true;
                    }
                    //else if (tongsongay > 10)
                    //{
                    //    e.Appearance.BackColor = Color.Red;
                    //    //e.Appearance.BackColor2 = Color.LightCyan;
                    //    e.HighPriority = true;
                    //}

                    int tongsongaythanhtoan = Utilities.TypeConvertParse.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["songaythanhtoan"]));
                    if (tongsongaythanhtoan >= 7)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        //e.Appearance.BackColor2 = Color.LightCyan; 
                        e.HighPriority = true;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Events
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
                                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
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
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

    }
}
