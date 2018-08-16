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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using MedicalLink.Utilities.GUIGridView;
using MedicalLink.ClassCommon.BCQLTaiChinh;
using MedicalLink.Base;

namespace MedicalLink.BCQLTaiChinh.NhapThongTinPTTT
{
    public partial class ucNhapThucHienPTTT : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new Base.ConnectDatabase();
        private DataTable DataNguoiThucHien { get; set; }
        private List<NhapThucHienPTTTDTO> lstBaoCao { get; set; }

        #endregion
        public ucNhapThucHienPTTT()
        {
            InitializeComponent();
        }

        #region Load
        private void ucNhapThucHienPTTT_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhMucKhoa();
                LoadDataNguoiThucHien();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    chkcomboListDSKhoa.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDataNguoiThucHien()
        {
            try
            {
                if (this.DataNguoiThucHien == null || this.DataNguoiThucHien.Rows.Count <= 0)
                {
                    string getnguoithuchien = "select 0 as userhisid, '' as usercode, '' as username, '' as usercodename union all select nv.userhisid, nv.usercode, nv.username, (nv.usercode || ' - ' || nv.username) as usercodename from nhompersonnel nv;";
                    this.DataNguoiThucHien = condb.GetDataTable_HIS(getnguoithuchien);
                }

                repositoryItemGridLookUp_MoChinh.DataSource = DataNguoiThucHien;
                repositoryItemGridLookUp_MoChinh.DisplayMember = "usercodename";
                repositoryItemGridLookUp_MoChinh.ValueMember = "userhisid";

                //repositoryItemGridLookUp_MoiMoChinh.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_MoiMoChinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_MoiMoChinh.ValueMember = "userhisid";

                //repositoryItemGridLookUp_BSGayMe.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_BSGayMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_BSGayMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_MoiGayMe.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_MoiGayMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_MoiGayMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_KTVPhuMe.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_KTVPhuMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_KTVPhuMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_Phu1.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_Phu1.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_Phu1.ValueMember = "usercode";

                //repositoryItemGridLookUp_Phu2.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_Phu2.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_Phu2.ValueMember = "usercode";

                //repositoryItemGridLookUp_KTVHoiTinh.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_KTVHoiTinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_KTVHoiTinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_DDHoiTinh.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_DDHoiTinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_DDHoiTinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_DDHanhChinh.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_DDHanhChinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_DDHanhChinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_HoLy.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_HoLy.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_HoLy.ValueMember = "usercode";

                //repositoryItemGridLookUp_DungCuVien.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_DungCuVien.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_DungCuVien.ValueMember = "usercode";

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _tieuchi_mbp = "";
                string _tieuchi_bhyt = "";
                string _trangthainhappttt = "";
                string _trangthainhappttt_2 = "";
                string _trangthai_vp = "";
                string _doituongbenhnhanid = "";
                //string _loaivienphiid = "";
                string _tieuchi_hsba = "";
                string _tieuchi_thuchienpttt = "";
                string _lstPhongChonLayBC = " and departmentid in (";
                //string _listuserid = "";

                string _datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                //Lay phong
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        _lstPhongChonLayBC += lstPhongCheck[i] + ",";
                    }
                    _lstPhongChonLayBC += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";

                    if (cboTieuChi.Text == "Theo ngày chỉ định")
                    {
                        _tieuchi_ser = " and servicepricedate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_mbp = " and maubenhphamdate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_thuchienpttt = " and servicepricedate between ''" + _datetungay + "'' and ''" + _datedenngay + "'' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày thực hiện")
                    {
                        _tieuchi_mbp = " and maubenhphamdate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày vào viện")
                    {
                        _tieuchi_ser = " and servicepricedate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_hsba = " and hosobenhandate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_vp = " and vienphidate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_mbp = " and maubenhphamdate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_bhyt = " and bhytdate between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày ra viện")
                    {
                        _tieuchi_vp = " and vienphidate_ravien between '" + _datetungay + "' and '" + _datedenngay + "' ";
                        _tieuchi_hsba = " and hosobenhandate_ravien between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                    {
                        _tieuchi_vp = " and duyet_ngayduyet_vp between '" + _datetungay + "' and '" + _datedenngay + "' ";
                    }
                    //trang thai
                    if (cboTrangThaiBA.Text == "Đang điều trị")
                    {
                        _trangthai_vp = " and vienphistatus=0 ";
                    }
                    else if (cboTrangThaiBA.Text == "Ra viện chưa thanh toán")
                    {
                        _trangthai_vp = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                    }
                    else if (cboTrangThaiBA.Text == "Đã thanh toán")
                    {
                        _trangthai_vp = " and vienphistatus<>0 and vienphistatus_vp=1 ";
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
                    //Trang thai nhap PTTT
                    if (cboTrangThaiNhapPTTT.Text == "Chưa nhập PTTT")
                    {
                        _trangthainhappttt = " LEFT JOIN ";
                        _trangthainhappttt_2 = " WHERE thuchien.thuchienptttid is null ";
                    }
                    else if (cboTrangThaiNhapPTTT.Text == "Đã nhập PTTT")
                    {
                        _trangthainhappttt = " INNER JOIN ";
                    }
                    else //if (cboTrangThaiNhapPTTT.Text == "Tất cả")
                    {
                        _trangthainhappttt = " LEFT JOIN ";
                    }

                    this.lstBaoCao = new List<NhapThucHienPTTTDTO>();

                    string _sqlLayData = @" select row_number () over (order by ser.servicepricedate) as stt, coalesce(thuchien.thuchienptttid,0) as thuchienptttid, ser.servicepriceid, vp.vienphiid, vp.patientid, hsba.patientname, ser.medicalrecordid, vp.hosobenhanid, vp.doituongbenhnhanid, vp.loaivienphiid, vp.vienphistatus, mbp.maubenhphamid, bh.bhytid, bh.bhytcode, ser.servicepricecode, ser.servicepricename, ser.servicepricedate, ser.loaidoituong, ser.dongia, ser.soluong, serf.pttt_loaiid, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as pttt_loaiten, ser.departmentgroupid, degp.departmentgroupname, ser.departmentid, de.departmentname, mbp.userid as ngchidinhid, ngcd.username as ngchidinhname, thuchien.mochinhid, thuchien.moimochinhid, thuchien.bacsigaymeid, thuchien.moigaymeid, thuchien.ktvphumeid, thuchien.phu1id, thuchien.phu2id, thuchien.ktvhoitinhid, thuchien.ddhoitinhid, thuchien.ddhanhchinhid, thuchien.holyid, thuchien.dungcuvienid, thuchien.mota, thuchien.thuchienttdate, thuchien.nguoinhap, thuchien.lastuserupdated, thuchien.lasttimeupdated from (select servicepriceid,vienphiid,medicalrecordid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,loaidoituong,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,departmentgroupid,departmentid from serviceprice where 1=1 and bhyt_groupcode in ('06PTTT','07KTC') " + _tieuchi_ser + _lstPhongChonLayBC + ") ser inner join (select maubenhphamid,userid from maubenhpham where maubenhphamgrouptype=4 " + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,patientid,hosobenhanid,doituongbenhnhanid,loaivienphiid,vienphistatus,bhytid,vienphidate,vienphidate_ravien from vienphi where 1=1 " + _tieuchi_vp + _trangthai_vp + _doituongbenhnhanid + ") vp on vp.vienphiid=ser.vienphiid inner join (select hosobenhanid,patientname from hosobenhan where 1=1 " + _tieuchi_hsba + ") hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select bhytid,bhytcode from bhyt where 1=1 " + _tieuchi_bhyt + ") bh on bh.bhytid=vp.bhytid inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in(2,3,9) " + _lstPhongChonLayBC + ") de on de.departmentid=ser.departmentid left join nhompersonnel ngcd on ngcd.userhisid=mbp.userid " + _trangthainhappttt + " (select * from dblink('myconn_mel','select thuchienptttid,servicepriceid,mochinhid,moimochinhid,bacsigaymeid,moigaymeid,ktvphumeid,phu1id,phu2id,ktvhoitinhid,ddhoitinhid,ddhanhchinhid,holyid,dungcuvienid,mota,nguoinhap,thuchienttdate,lastuserupdated,lasttimeupdated from ml_thuchienpttt where 1=1 " + _tieuchi_thuchienpttt + "') as thuchien(thuchienptttid integer,servicepriceid integer,mochinhid integer,moimochinhid integer,bacsigaymeid integer,moigaymeid integer,ktvphumeid integer,phu1id integer,phu2id integer,ktvhoitinhid integer,ddhoitinhid integer,ddhanhchinhid integer,holyid integer,dungcuvienid integer,mota text,nguoinhap text,thuchienttdate timestamp without time zone,lastuserupdated text,lasttimeupdated timestamp without time zone)) thuchien on thuchien.servicepriceid=ser.servicepriceid "+ _trangthainhappttt_2 + ";";
                    DataTable _dataBaoCao = condb.GetDataTable_HISToMeL(_sqlLayData);
                    if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                    {
                        this.lstBaoCao = Utilities.DataTables.DataTableToList<NhapThucHienPTTTDTO>(_dataBaoCao);
                        gridControlDataDV.DataSource = this.lstBaoCao;
                    }
                    else
                    {
                        gridControlDataDV.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }

                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void gridViewDataDV_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                string _sqlexcute = "";
                var rowHandle = gridViewDataDV.FocusedRowHandle;
                NhapThucHienPTTTDTO _ptttDTO = gridViewDataDV.GetRow(rowHandle) as NhapThucHienPTTTDTO;

                string _mochinhid = _ptttDTO.mochinhid != null ? _ptttDTO.mochinhid.ToString() : "0";
                string _moimochinhid = _ptttDTO.moimochinhid != null ? _ptttDTO.moimochinhid.ToString() : "0";
                string _bacsigaymeid = _ptttDTO.bacsigaymeid != null ? _ptttDTO.bacsigaymeid.ToString() : "0";
                string _moigaymeid = _ptttDTO.moigaymeid != null ? _ptttDTO.moigaymeid.ToString() : "0";
                string _ktvphumeid = _ptttDTO.ktvphumeid != null ? _ptttDTO.ktvphumeid.ToString() : "0";
                string _phu1id = _ptttDTO.phu1id != null ? _ptttDTO.phu1id.ToString() : "0";
                string _phu2id = _ptttDTO.phu2id != null ? _ptttDTO.phu2id.ToString() : "0";
                string _ktvhoitinhid = _ptttDTO.ktvhoitinhid != null ? _ptttDTO.ktvhoitinhid.ToString() : "0";
                string _ddhoitinhid = _ptttDTO.ddhoitinhid != null ? _ptttDTO.ddhoitinhid.ToString() : "0";
                string _ddhanhchinhid = _ptttDTO.ddhanhchinhid != null ? _ptttDTO.ddhanhchinhid.ToString() : "0";
                string _holyid = _ptttDTO.holyid != null ? _ptttDTO.holyid.ToString() : "0";
                string _dungcuvienid = _ptttDTO.dungcuvienid != null ? _ptttDTO.dungcuvienid.ToString() : "0";


                //Kiem tra ton tai trong CSDL
                string _sqlKiemtra = "SELECT servicepriceid FROM ml_thuchienpttt where servicepriceid='" + _ptttDTO.servicepriceid + "';";
                DataTable _dataKiemTra = condb.GetDataTable_MeL(_sqlKiemtra);

                if (_mochinhid != "0" || _moimochinhid != "0" || _bacsigaymeid != "0" || _moigaymeid != "0" || _ktvphumeid != "0" || _phu1id != "0" || _phu2id != "0" || _ktvhoitinhid != "0" || _ddhoitinhid != "0" || _ddhanhchinhid != "0" || _holyid != "0" || _dungcuvienid != "0")
                {
                    if (_dataKiemTra.Rows.Count>0) //update
                    {
                        _sqlexcute = @"UPDATE ml_thuchienpttt SET vienphiid='" + _ptttDTO.vienphiid + "',patientid='" + _ptttDTO.patientid + "',patientname = '" + _ptttDTO.patientname + "', medicalrecordid='" + _ptttDTO.medicalrecordid + "', hosobenhanid='" + _ptttDTO.hosobenhanid + "', maubenhphamid='" + _ptttDTO.maubenhphamid + "', bhytid='" + _ptttDTO.bhytid + "', bhytcode='" + _ptttDTO.bhytcode + "', servicepricecode='" + _ptttDTO.servicepricecode + "', servicepricename='" + _ptttDTO.servicepricename + "', servicepricedate='" + _ptttDTO.servicepricedate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', loaidoituong='" + _ptttDTO.loaidoituong + "', dongia='" + _ptttDTO.dongia + "', soluong='" + _ptttDTO.soluong + "', pttt_loaiid='" + _ptttDTO.pttt_loaiid + "', departmentgroupid='" + _ptttDTO.departmentgroupid + "', departmentid='" + _ptttDTO.departmentid + "', ngchidinhid='" + _ptttDTO.ngchidinhid + "', mochinhid='" + _mochinhid + "', moimochinhid='" + _moimochinhid + "', bacsigaymeid='" + _bacsigaymeid + "', moigaymeid='" + _moigaymeid + "', ktvphumeid='" + _ktvphumeid + "', phu1id='" + _phu1id + "', phu2id='" + _phu2id + "', ktvhoitinhid='" + _ktvhoitinhid + "', ddhoitinhid='" + _ddhoitinhid + "', ddhanhchinhid='" + _ddhanhchinhid + "', holyid='" + _holyid + "', dungcuvienid='" + _dungcuvienid + "', mota='" + _ptttDTO.mota + "', lastuserupdated='" + SessionLogin.SessionUsercode + " - " + SessionLogin.SessionUsername + "', lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE servicepriceid='" + _ptttDTO.servicepriceid + "';";
                    }
                    else//insert
                    {
                        _sqlexcute = @"INSERT INTO ml_thuchienpttt(servicepriceid,vienphiid,patientid,patientname,medicalrecordid,hosobenhanid,maubenhphamid,bhytid,bhytcode, servicepricecode,servicepricename,servicepricedate,loaidoituong,dongia,soluong,pttt_loaiid,departmentgroupid,departmentid,ngchidinhid,mochinhid,moimochinhid,bacsigaymeid,moigaymeid,ktvphumeid,phu1id,phu2id,ktvhoitinhid,ddhoitinhid,ddhanhchinhid,holyid,dungcuvienid,mota,nguoinhap,thuchienttdate) 
VALUES ('" + _ptttDTO.servicepriceid + "','" + _ptttDTO.vienphiid + "','" + _ptttDTO.patientid + "','" + _ptttDTO.patientname + "','" + _ptttDTO.medicalrecordid + "','" + _ptttDTO.hosobenhanid + "','" + _ptttDTO.maubenhphamid + "','" + _ptttDTO.bhytid + "','" + _ptttDTO.bhytcode + "','" + _ptttDTO.servicepricecode + "','" + _ptttDTO.servicepricename + "','" + _ptttDTO.servicepricedate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + _ptttDTO.loaidoituong + "','" + _ptttDTO.dongia + "','" + _ptttDTO.soluong + "','" + _ptttDTO.pttt_loaiid + "','" + _ptttDTO.departmentgroupid + "','" + _ptttDTO.departmentid + "','" + _ptttDTO.ngchidinhid + "','" + _mochinhid + "','" + _moimochinhid + "','" + _bacsigaymeid + "','" + _moigaymeid + "','" + _ktvphumeid + "','" + _phu1id + "','" + _phu2id + "','" + _ktvhoitinhid + "','" + _ddhoitinhid + "','" + _ddhanhchinhid + "','" + _holyid + "','" + _dungcuvienid + "','" + _ptttDTO.mota + "','" + SessionLogin.SessionUsercode + " - " + SessionLogin.SessionUsername + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
                    }
                }
                else//delete
                {
                    if (_dataKiemTra.Rows.Count > 0)
                    {
                        _sqlexcute = "delete from ml_thuchienpttt where thuchienptttid='" + _ptttDTO.thuchienptttid + "';";
                    }
                }

                if (condb.ExecuteNonQuery_MeL(_sqlexcute))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                    frmthongbao.Show();
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THAT_BAI);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void chkcomboListDSKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListDSPhong.Properties.Items.Clear();
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    //Load danh muc phong theo khoa da chon
                    List<ClassCommon.classUserDepartment> lstDSPhong = new List<classUserDepartment>();
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        List<ClassCommon.classUserDepartment> lstdsphongthuockhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
                        lstDSPhong.AddRange(lstdsphongthuockhoa);
                    }
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
                    chkcomboListDSPhong.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        #region Custom
        private void gridViewDataDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        private void gridViewDataDV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "img_nhapptttstt")
                {
                    string val = gridViewDataDV.GetRowCellValue(e.RowHandle, "thuchienptttid").ToString();
                    if (val != "0")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[4]);
                        e.Graphics.DrawImage(imMenu.Images[4], pos);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Process

        #endregion

        #region In va xuat file
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                //string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                //string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                //List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                //ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                //reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                //reportitem.value = tungaydenngay;
                //thongTinThem.Add(reportitem);
                //ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                //reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                //reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                //thongTinThem.Add(reportitem_khoa);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataDV);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("", _dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }




        #endregion





    }
}
