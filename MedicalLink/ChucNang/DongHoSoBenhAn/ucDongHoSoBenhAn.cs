using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using System.Globalization;

namespace MedicalLink.ChucNang
{
    public partial class ucDongHoSoBenhAn : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #endregion
        public ucDongHoSoBenhAn()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDongHoSoBenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhMucKhoa();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                string _sqlSelectKhoa = "SELECT departmentgroupid,departmentgroupname FROM departmentgroup WHERE departmentgrouptype in (1,4,11) ORDER BY departmentgroupname;";
                DataTable _dataKhoa = condb.GetDataTable_HIS(_sqlSelectKhoa);
                if (_dataKhoa != null && _dataKhoa.Rows.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = _dataKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (_dataKhoa.Rows.Count == 1)
                {
                    chkcomboListDSKhoa.CheckAll();
                }
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
                string _sql_timkiem = "";
                gridControlHSBA.DataSource = null;
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                string _tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                string _doituongbenhnhanid = " and doituongbenhnhanid in (2,3) ";
                string _loaivienphiid = " and loaivienphiid=1 ";
                string _lstPhongcheck = "";
                string _vienphistatus = " and vienphistatus=0 ";
                //Loai benh an
                if (cboBenhAn.Text == "Ngoại trú")
                {
                    _loaivienphiid = " and loaivienphiid=1 ";
                }
                //doi tuong BN
                if (cboDoiTuongBN.Text == "Viện phí")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid in (2,3) ";
                }
                //trang thai vp
                if (cboTrangThaiVP.Text == "Đang điều trị")
                {
                    _vienphistatus = " and vienphistatus=0 ";
                }
                _lstPhongcheck = " and departmentid in (";
                List<Object> _lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (_lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < _lstPhongCheck.Count - 1; i++)
                    {
                        _lstPhongcheck += _lstPhongCheck[i] + ", ";
                    }
                    _lstPhongcheck += _lstPhongCheck[_lstPhongCheck.Count - 1] + ")";

                    //
                    _sql_timkiem = @"SELECT row_number () over (order by vp.vienphidate) as stt,
	        vp.vienphiid,
	        vp.patientid,
	        hsba.hosobenhanid,
	        hsba.patientname,
	        TO_CHAR(hsba.birthday,'dd/MM/yyyy') as birthday,
	        vp.vienphidate,
	        degp.departmentgroupname,
	        de.departmentname
        FROM (select vienphiid,patientid,hosobenhanid,departmentgroupid,departmentid,vienphidate,vienphistatus from vienphi where 1=1 " + _tieuchi_vp + _loaivienphiid + _doituongbenhnhanid + _lstPhongcheck + _vienphistatus + ") vp inner join (select hosobenhanid,patientname,birthday from hosobenhan where 1=1 " + _tieuchi_hsba + ") hsba on hsba.hosobenhanid = vp.hosobenhanid inner join departmentgroup degp on degp.departmentgroupid = vp.departmentgroupid inner join department de on de.departmentid = vp.departmentid; ";

                    DataTable _dataHSBA = condb.GetDataTable_HIS(_sql_timkiem);
                    if (_dataHSBA != null && _dataHSBA.Rows.Count > 0)
                    {
                        gridControlHSBA.DataSource = _dataHSBA;
                    }
                    else
                    {
                        gridControlHSBA.DataSource = null;
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
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void gridViewHSBA_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    //GridView view = sender as GridView;
                    e.Menu.Items.Clear();
                    DXMenuItem itemMoBenhAn = new DXMenuItem("Đóng bệnh án và duyệt viện phí HS đã chọn");
                    itemMoBenhAn.Image = imageCollectionMBA.Images[0];
                    itemMoBenhAn.Click += new EventHandler(DongBenhAnVaDuyetVP_Click);
                    e.Menu.Items.Add(itemMoBenhAn);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        //process dong benh an
        private void DongBenhAnVaDuyetVP_Click(object sender, EventArgs e)
        {
            if (gridViewHSBA.GetSelectedRows().Count() > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đóng bệnh án của BN đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    try
                    {
                        int _demupdate = 0;
                        if (gridViewHSBA.GetSelectedRows().Count() > 0)
                        {
                            DateTime _datetime = DateTime.Now;
                            string _dongHSTime = _datetime.ToString("yyyy-MM-dd HH:mm:ss");
                            string _dongHSTime_long = _datetime.ToString("yyyMMdd");
                            string _duyetVPTime = _datetime.AddMinutes(1).ToString("yyyy-MM-dd HH:mm:ss");


                            foreach (var item_index in gridViewHSBA.GetSelectedRows())
                            {
                                string _vienphiid = gridViewHSBA.GetRowCellValue(item_index, "vienphiid").ToString();
                                string _hosobenhanid = gridViewHSBA.GetRowCellValue(item_index, "hosobenhanid").ToString();

                                //update HSBA
                                string _sqlDongHSBA = @" UPDATE vienphi SET vienphistatus=1, vienphidate_ravien='" + _dongHSTime + "', chandoanravien='NONE', chandoanravien_code='NONE', vienphistatus_bh=1, duyet_ngayduyet_bh='" + _duyetVPTime + "', duyet_nguoiduyet_bh='" + Base.SessionLogin.SessionUserHISID + "', duyet_sothutuduyet_bh=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='" + Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='" + _dongHSTime_long + "'), vienphistatus_vp=1, duyet_ngayduyet_vp='" + _duyetVPTime + "', duyet_nguoiduyet_vp='" + Base.SessionLogin.SessionUserHISID + "', duyet_sothutuduyet_vp=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='" + Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='" + _dongHSTime_long + "'), medicalrecordid_end=(select medicalrecordid from medicalrecord where vienphiid='" + _vienphiid + "' order by medicalrecordid desc limit 1) WHERE vienphiid='" + _vienphiid + "'; UPDATE hosobenhan SET hosobenhanstatus=1, xutrikhambenhid=7, hosobenhandate_ravien='" + _dongHSTime + "', chandoanravien_code='NONE', chandoanravien='NONE' WHERE hosobenhanid='" + _hosobenhanid + "'; UPDATE medicalrecord SET medicalrecordstatus=99, thoigianravien='" + _dongHSTime + "', chandoanravien='NONE', chandoanravien_code='NONE', xutrikhambenhid=7, medicalrecordremark='Đóng bệnh án tự động' WHERE medicalrecordid=(select medicalrecordid from medicalrecord where vienphiid='" + _vienphiid + "' order by medicalrecordid desc limit 1); ";
                                if (condb.ExecuteNonQuery_HIS(_sqlDongHSBA))
                                {
                                    _demupdate += 1;
                                    //Log vào DB
                                    string _sqlluulog = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + Base.SessionLogin.SessionUsercode + "', 'Đóng và duyệt VP tự động VP=" + _vienphiid + "' ,'" + Base.SessionLogin.SessionMyIP + "', '" + Base.SessionLogin.SessionMachineName + "', '" + Base.SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + _vienphiid + "', 'TOOL_25');";
                                    condb.ExecuteNonQuery_MeL(_sqlluulog);
                                }
                            }
                            MessageBox.Show("Đóng bệnh án và duyệt viện phí thành công SL=" + _demupdate, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnTimKiem_Click(null,null);
                        }
                    }
                    catch (Exception ex)
                    {
                        MedicalLink.Base.Logging.Error(ex);
                    }
                    SplashScreenManager.CloseForm();
                }
            }
        }


        private void chkcomboListDSKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string _lstServicecheck = " and departmentgroupid in (";
                List<Object> _lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (_lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < _lstKhoaCheck.Count - 1; i++)
                    {
                        _lstServicecheck += _lstKhoaCheck[i] + ", ";
                    }
                    _lstServicecheck += _lstKhoaCheck[_lstKhoaCheck.Count - 1] + ")";
                    string _sqlSelectPhong = "SELECT departmentid,departmentname FROM department WHERE departmenttype=2 " + _lstServicecheck + ";";
                    DataTable _dataPhong = condb.GetDataTable_HIS(_sqlSelectPhong);
                    if (_dataPhong != null && _dataPhong.Rows.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = _dataPhong;
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

        private void gridViewHSBA_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewHSBA.RowCount > 0)
                {
                    var rowHandle = gridViewHSBA.FocusedRowHandle;
                    string _vienphiid = gridViewHSBA.GetRowCellValue(rowHandle, "vienphiid").ToString();

                    string _sqlDichVu = @"SELECT row_number () over (order by ser.servicepricedate) as stt, ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode, ser.servicepricename, ser.dongia, ser.soluong, (ser.soluong*ser.dongia) as thanhtien, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, ser.servicepricedate, degp.departmentgroupname as khoachidinh, de.departmentname as phongchidinh, mbp.userid as userhisid, ncd.usercode, ncd.username, (case when billid_thutien<>0 or billid_clbh_thutien<>0 then 'Đã thu tiền' end) as trangthaithutien FROM (select servicepriceid,servicepricecode,servicepricename,maubenhphamid,vienphiid,soluong,loaidoituong,departmentgroupid,departmentid,servicepricedate,billid_thutien,billid_clbh_thutien, (case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where vienphiid=" + _vienphiid + ") ser inner join (select maubenhphamid,userid,maubenhphamstatus from maubenhpham where vienphiid=" + _vienphiid + ") mbp on mbp.maubenhphamid=ser.maubenhphamid left join (select userhisid,usercode,username from nhompersonnel) ncd on ncd.userhisid=mbp.userid inner join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid inner join department de on de.departmentid=ser.departmentid;";
                    DataTable _dataDSDichVu = condb.GetDataTable_HIS(_sqlDichVu);
                    if (_dataDSDichVu.Rows.Count > 0)
                    {
                        gridControlDichVuChiTiet.DataSource = _dataDSDichVu;
                    }
                    else
                    {
                        gridControlDichVuChiTiet.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void gridViewHSBA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #endregion

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

                DataTable _dataBaocao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewHSBA);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("DANH SÁCH HỒ SƠ BỆNH ÁN", _dataBaocao);
                /// export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaocao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
    }
}
