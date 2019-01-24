using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;

namespace MedicalLink.BaoCao
{
    public partial class ucBC52_TGKhamBenhTrungBinh : UserControl
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion

        public ucBC52_TGKhamBenhTrungBinh()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC52_TGKhamBenhTrungBinh_Load(object sender, EventArgs e)
        {
            LoadDuLieuMacDinh();
            LoadDanhSachNhomDVKT();
        }
        private void LoadDuLieuMacDinh()
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

            radioXemTongHop.Checked = true;
            radioXemChiTiet.Checked = false;
            gridControlBCTongHop.Visible = true;
            gridControlBCTongHop.DataSource = null;
            gridControlBCTongHop.Dock = DockStyle.Fill;
            gridControlBCChiTiet.Visible = false;
        }

        private void LoadDanhSachNhomDVKT()
        {
            try
            {
                string _sqlDSNhom = @"select servicepricerefid,servicepricegroupcode,servicepricecode,servicepricename
from servicepriceref
where servicegrouptype in (2,3) and coalesce(isremove,0)=0 and servicepricegroupcode<>''
order by servicegrouptype,servicepricegroupcode,servicepricename;";
                DataTable _dataDSNhom = condb.GetDataTable_HIS(_sqlDSNhom);

                treeListLookUpEdit1TreeList.KeyFieldName = "servicepricecode";
                treeListLookUpEdit1TreeList.ParentFieldName = "servicepricegroupcode";
                treeListNhomThuoc.Properties.DisplayMember = "servicepricename";
                treeListNhomThuoc.Properties.ValueMember = "servicepricecode";

                treeListLookUpEdit1TreeList.DataSource = _dataDSNhom;
                treeListLookUpEdit1TreeList.ExpandAll();

                treeListNhomThuoc.EditValue = _dataDSNhom.Rows[2];
                treeListLookUpEdit1TreeList.SelectNode(treeListLookUpEdit1TreeList.FindNodeByFieldValue("servicepricecode", _dataDSNhom.Rows[2]["servicepricecode"]));
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Events   
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlBCTongHop.Visible = true;
                    gridControlBCTongHop.DataSource = null;
                    gridControlBCTongHop.Dock = DockStyle.Fill;
                    gridControlBCChiTiet.Visible = false;
                    btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void radioXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemChiTiet.Checked)
                {
                    radioXemTongHop.Checked = false;
                    gridControlBCChiTiet.Visible = true;
                    gridControlBCChiTiet.DataSource = null;
                    gridControlBCChiTiet.Dock = DockStyle.Fill;
                    gridControlBCTongHop.Visible = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_vp = " and vienphidate>='2018-01-01 00:00:00' ";
                string _tieuchi_mrd = " and thoigianvaovien>='2018-01-01 00:00:00' ";
                string _tieuchi_stt = " and sothutudate>='2018-01-01 00:00:00' ";
                string _tieuchi_ser = " and servicepricedate>='2018-01-01 00:00:00' ";
                string _tieuchi_hsba = " and hosobenhandate>='2018-01-01 00:00:00' ";
                string _doituongbenhnhanid = "";
                string _lstvienphi_loaitru = " and vienphiid not in (0";

                string _tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //tieu chi
                if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianvaovien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_stt = " and sothutudate>='" + _tungay + "' ";
                    _tieuchi_ser = " and servicepricedate>='" + _tungay + "' ";
                    _tieuchi_hsba = " and hosobenhandate between '" + _tungay + "' and '" + _denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianravien between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_stt = " and sothutudate between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate_ravien between '" + _tungay + "' and '" + _denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and vienphistatus>0 and duyet_ngayduyet_vp between '" + _tungay + "' and '" + _denngay + "' ";
                    _tieuchi_mrd = " and thoigianravien between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                    _tieuchi_stt = " and sothutudate between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                    _tieuchi_ser = " and servicepricedate between '2018-01-01 00:00:00' and '" + _denngay + "' ";
                }
                //doi tuong Bn
                if (cboDoiTuong.Text == "BHYT")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid=1 ";
                }
                else if (cboDoiTuong.Text == "Viện phí")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid<>1 ";
                }

                //Lay DS Vienphi loai tru
                string _lstDSDVKT_Ser = " and servicepricecode in ('0'";
                //DS DVKT
                var _lstCheckNode = treeListLookUpEdit1TreeList.GetAllCheckedNodes();
                foreach (var _item in _lstCheckNode)
                {
                    _lstDSDVKT_Ser += ",'" + _item.GetValue(treeListColumn_medicinecode).ToString() + "'";
                }
                _lstDSDVKT_Ser += ")";
                _lstDSDVKT_Ser = _lstDSDVKT_Ser.Replace("'0',", "");

                string _sqlLayVP = $@"SELECT vp.vienphiid
FROM
	(select vienphiid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus from medicalrecord where loaibenhanid=24 and departmentgroupid in (33,46) and departmentid not in (219,224,221,239) {_doituongbenhnhanid} {_tieuchi_mrd}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select ser.vienphiid,ser.medicalrecordid
				from (select vienphiid,medicalrecordid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} {_lstDSDVKT_Ser} group by vienphiid,medicalrecordid) ser group by ser.vienphiid,ser.medicalrecordid) serv on serv.medicalrecordid=mrd.medicalrecordid;";
                DataTable _dataLayVP = condb.GetDataTable_HIS(_sqlLayVP);
                if (_dataLayVP.Rows.Count > 0)
                {
                    for (int i = 0; i < _dataLayVP.Rows.Count; i++)
                    {
                        _lstvienphi_loaitru += "," + _dataLayVP.Rows[i]["vienphiid"].ToString();
                    }
                }
                _lstvienphi_loaitru = _lstvienphi_loaitru.Replace("0,", "") + ") ";

                //
                if (radioXemTongHop.Checked)
                {
                    string _sqlTimKiem = $@"SELECT TMP.sl_bn,
	TMP.sl_chokham,
	TMP.tg_chokham,
	(TMP.tg_chokham/TMP.sl_bn) as tg_chokhamtb,
	TMP.sl_khamls,
	TMP.tg_khamls_qlcl,
	(TMP.tg_khamls_qlcl/TMP.sl_khamls) as tg_khamls_qlcltb,
	TMP.tg_khamls_it,
	(TMP.tg_khamls_it/TMP.sl_khamls) as tg_khamls_ittb,
	TMP.sl_khamlsxn,
	TMP.tg_khamlsxn_qlcl,
	(TMP.tg_khamlsxn_qlcl/TMP.sl_khamlsxn) as tg_khamlsxn_qlcltb,
	TMP.tg_khamlsxn_it,
	(TMP.tg_khamlsxn_it/TMP.sl_khamlsxn) as tg_khamlsxn_ittb,
	TMP.sl_khamlsxncdha,
	TMP.tg_khamlsxncdha_qlcl,
	(TMP.tg_khamlsxncdha_qlcl/TMP.sl_khamlsxncdha) as tg_khamlsxncdha_qlcltb,
	TMP.tg_khamlsxncdha_it,
	(TMP.tg_khamlsxncdha_it/TMP.sl_khamlsxncdha) as tg_khamlsxncdha_ittb,
	TMP.sl_khamlsxncdhatdcn,
	TMP.tg_khamlsxncdhatdcn_qlcl,
	(TMP.tg_khamlsxncdhatdcn_qlcl/TMP.sl_khamlsxncdhatdcn) as tg_khamlsxncdhatdcn_qlcltb,
	TMP.tg_khamlsxncdhatdcn_it,
	(TMP.tg_khamlsxncdhatdcn_it/TMP.sl_khamlsxncdhatdcn) as tg_khamlsxncdhatdcn_ittb
FROM 
(SELECT
	count(vp.*) as sl_bn,
	sum(case when stt.sothutustatus>=1 then 1 else 0 end) as sl_chokham,
	sum(case when stt.sothutustatus>=1 then ((DATE_PART('day',stt.sothutudate_start-mrd.thoigianvaovien)*24+DATE_PART('hour',stt.sothutudate_start-mrd.thoigianvaovien))*60+DATE_PART('minute',stt.sothutudate_start-mrd.thoigianvaovien)) end) as tg_chokham,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then 1 else 0 end) as sl_khamls,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamls_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamls_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then 1 else 0 end) as sl_khamlsxn,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxn_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then 1 else 0 end) as sl_khamlsxncdha,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdha_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdha_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then 1 else 0 end) as sl_khamlsxncdhatdcn,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdhatdcn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdhatdcn_it	
FROM 
	(select vienphiid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp} {_lstvienphi_loaitru}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus from medicalrecord where loaibenhanid=24 and departmentgroupid in (33,46) and departmentid not in (219,224,221,239) {_doituongbenhnhanid} {_tieuchi_mrd} {_lstvienphi_loaitru}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select medicalrecordid,min(sothutudate_start) as sothutudate_start,max(sothutudate_end) as sothutudate_end,max(sothutustatus) as sothutustatus from sothutuphongkham where 1=1 and sothutudate_start<>'0001-01-01 00:00:00' {_tieuchi_stt} group by medicalrecordid) stt on stt.medicalrecordid=mrd.medicalrecordid
	inner join (select ser.vienphiid,ser.medicalrecordid,
				max(case when ser.bhyt_groupcode='01KB' then 1 else 0 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1 else 0 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','07KTC') then 1 else 0 end) as iscdha,
				max(case when ser.bhyt_groupcode='05TDCN' then 1 else 0 end) as istdcn
			from	
				(select vienphiid,bhyt_groupcode,medicalrecordid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} {_lstvienphi_loaitru} group by vienphiid,bhyt_groupcode,medicalrecordid) ser group by ser.vienphiid,ser.medicalrecordid) serv on serv.medicalrecordid=mrd.medicalrecordid) TMP;";
                    DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlTimKiem);
                    if (_dataBaoCao.Rows.Count > 0)
                    {
                        gridControlBCTongHop.DataSource = _dataBaoCao;
                    }
                    else
                    {
                        gridControlBCTongHop.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else//xem chi tiet
                {
                    string _sqlTimKiem = $@"SELECT row_number () over (order by vp.vienphiid) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	de.departmentname,
	mrd.thoigianvaovien as tgdangky,
	stt.sothutudate_start as tgbatdaukham,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' then mrd.thoigianravien end) as tgketthuckham,
	(case when stt.sothutustatus>=1 then ((DATE_PART('day',stt.sothutudate_start-mrd.thoigianvaovien)*24+DATE_PART('hour',stt.sothutudate_start-mrd.thoigianvaovien))*60+DATE_PART('minute',stt.sothutudate_start-mrd.thoigianvaovien)) end) as tg_chokham,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamls_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamls_it,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxn_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxn_it,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdha_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdha_it,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdhatdcn_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdhatdcn_it,
	serv.iskb,
	serv.isxn,
	serv.iscdha,
	serv.istdcn
FROM
	(select vienphiid,hosobenhanid,patientid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp} {_lstvienphi_loaitru}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus,departmentid from medicalrecord where loaibenhanid=24 and departmentgroupid in (33,46) and departmentid not in (219,224,221,239) {_doituongbenhnhanid} {_tieuchi_mrd} {_lstvienphi_loaitru}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select medicalrecordid,min(sothutudate_start) as sothutudate_start,max(sothutudate_end) as sothutudate_end,max(sothutustatus) as sothutustatus from sothutuphongkham where 1=1 and sothutudate_start<>'0001-01-01 00:00:00' {_tieuchi_stt} group by medicalrecordid) stt on stt.medicalrecordid=mrd.medicalrecordid
	inner join (select ser.vienphiid,ser.medicalrecordid,
				max(case when ser.bhyt_groupcode='01KB' then 1 else 0 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1  else 0 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','07KTC') then 1  else 0 end) as iscdha,
				max(case when ser.bhyt_groupcode='05TDCN' then 1  else 0 end) as istdcn
			from	
				(select vienphiid,bhyt_groupcode,medicalrecordid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} {_lstvienphi_loaitru} group by vienphiid,bhyt_groupcode,medicalrecordid) ser group by ser.vienphiid,ser.medicalrecordid) serv on serv.medicalrecordid=mrd.medicalrecordid 
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=mrd.departmentid;";
                    DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlTimKiem);
                    if (_dataBaoCao.Rows.Count > 0)
                    {
                        gridControlBCChiTiet.DataSource = _dataBaoCao;
                    }
                    else
                    {
                        gridControlBCChiTiet.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Process
        #endregion

        #region Xuat bao cao and print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                    string fileTemplatePath = "BC_52_TGKhamBenhTrungBinh.xlsx";
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBCTongHop);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
                else
                {
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBCChiTiet);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("BÁO CÁO THỜI GIAN KHÁM CHỮA BỆNH CHI TIẾT", data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
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

                string fileTemplatePath = "BC_52_TGKhamBenhTrungBinh.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBCTongHop);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }


        #endregion

        #region Custom
        private void bandedGridViewBaoCao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Tree List
        private void treeListLookUpEdit1TreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                if (e.Node.ParentNode != null)
                {
                    e.Node.ParentNode.Checked = IsAllChecked(e.Node.ParentNode.Nodes);
                    SetCheckedChildNodes(e.Node.Nodes);
                }
                else
                {
                    SetCheckedChildNodes(e.Node.Nodes);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void SetCheckedChildNodes(TreeListNodes nodes)
        {
            try
            {
                foreach (TreeListNode node in nodes)
                {
                    if (node.Nodes.Count > 0)
                    {
                        node.Checked = node.ParentNode.Checked;
                        SetCheckedChildNodes(node.Nodes);
                    }
                    else
                    {
                        node.Checked = node.ParentNode.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private bool IsAllChecked(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes)
        {
            bool value = true;
            try
            {
                foreach (TreeListNode node in nodes)
                {
                    if (!node.Checked)
                    {
                        value = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return value;
        }


        #endregion
    }
}
