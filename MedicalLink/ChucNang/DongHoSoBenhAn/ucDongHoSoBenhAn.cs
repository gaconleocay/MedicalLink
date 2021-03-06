﻿using System;
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
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                string _sqlSelectKhoa = "SELECT departmentgroupid,departmentgroupname FROM departmentgroup WHERE departmentgrouptype in (1,4,11,100) ORDER BY departmentgroupname;";
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _sql_timkiem = "";
                gridControlHSBA.DataSource = null;
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _tieuchi_vp = "";
                string _tieuchi_hsba = "";
                string _doituongbenhnhanid = "";
                string _loaivienphiid = "";
                string _lstPhongcheck = "";
                string _vienphistatus = "";
                //Loai benh an
                if (cboBenhAn.Text == "Ngoại trú")
                {
                    _loaivienphiid = " and loaivienphiid=1 ";
                }
                else if (cboBenhAn.Text == "Nội trú")
                {
                    _loaivienphiid = " and loaivienphiid=0 ";
                }

                //doi tuong BN
                if (cboDoiTuongBN.Text == "Viện phí")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid<>1 ";
                }
                //trang thai vp
                if (cboTrangThaiVP.Text == "Đang điều trị")
                {
                    _vienphistatus = " and vienphistatus=0 ";
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTrangThaiVP.Text == "Ra viện chưa thanh toán")
                {
                    _vienphistatus = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                _lstPhongcheck = " and departmentid in (";
                List<Object> _lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (_lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < _lstPhongCheck.Count - 1; i++)
                    {
                        _lstPhongcheck += _lstPhongCheck[i] + ",";
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
             (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	        degp.departmentgroupname,
	        de.departmentname
        FROM (select vienphiid,patientid,hosobenhanid,departmentgroupid,departmentid,vienphidate,vienphidate_ravien,vienphistatus from vienphi where 1=1 " + _tieuchi_vp + _loaivienphiid + _doituongbenhnhanid + _lstPhongcheck + _vienphistatus + ") vp inner join (select hosobenhanid,patientname,birthday from hosobenhan where 1=1 " + _tieuchi_hsba + ") hsba on hsba.hosobenhanid = vp.hosobenhanid inner join departmentgroup degp on degp.departmentgroupid = vp.departmentgroupid inner join department de on de.departmentid = vp.departmentid; ";

                    DataTable _dataHSBA = condb.GetDataTable_HIS(_sql_timkiem);
                    if (_dataHSBA != null && _dataHSBA.Rows.Count > 0)
                    {
                        gridControlHSBA.DataSource = _dataHSBA;
                    }
                    else
                    {
                        gridControlHSBA.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                    if (cboTrangThaiVP.Text == "Đang điều trị")
                    {
                        DXMenuItem itemMoBenhAn = new DXMenuItem("Đóng bệnh án và duyệt viện phí HS đã chọn");
                        itemMoBenhAn.Image = imageCollectionMBA.Images[0];
                        itemMoBenhAn.Click += new EventHandler(DongBenhAnVaDuyetVP_Click);
                        e.Menu.Items.Add(itemMoBenhAn);
                    }
                    else if (cboTrangThaiVP.Text == "Ra viện chưa thanh toán")
                    {
                        DXMenuItem _itemDuyetVP = new DXMenuItem("Duyệt viện phí HS đã chọn");
                        _itemDuyetVP.Image = imageCollectionMBA.Images[0];
                        _itemDuyetVP.Click += new EventHandler(DuyetVP_Click);
                        e.Menu.Items.Add(_itemDuyetVP);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void gridViewHSBA_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        #region Process
        private void DongBenhAnVaDuyetVP_Click(object sender, EventArgs e)
        {
            if (Base.SessionLogin.SessionUserHISID == null || Base.SessionLogin.SessionUserHISID == "0")
            {
                MessageBox.Show("Tài khoản chưa được thiết lập HIS ID, vui lòng kiểm tra lại tài khoản để bổ sung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (gridViewHSBA.GetSelectedRows().Count() > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đóng bệnh án và duyệt viện phí của BN đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    int _demupdate = 0;
                    SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                    try
                    {
                        if (gridViewHSBA.GetSelectedRows().Count() > 0)
                        {
                            foreach (var item_index in gridViewHSBA.GetSelectedRows())
                            {
                                string _vienphiid = gridViewHSBA.GetRowCellValue(item_index, "vienphiid").ToString();
                                string _hosobenhanid = gridViewHSBA.GetRowCellValue(item_index, "hosobenhanid").ToString();

                                //Lay ngay chi dinh cuoi cung cua Ho so benh an
                                string _dongHSTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                string _dongHSTime_EndDay = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                                string _dongHSTime_long = DateTime.Now.ToString("yyyyMMdd");

                                string _sqlChiDinhCuoi = "SELECT (TO_CHAR(maubenhphamdate,'yyyy-MM-dd') || ' 23:59:59') as donghstime_endday,TO_CHAR(maubenhphamdate,'yyyyMMdd') as donghstime_long FROM maubenhpham WHERE vienphiid='" + _vienphiid + "' ORDER BY maubenhphamdate desc limit 1;";
                                DataTable _dataChiDinhCuoi = condb.GetDataTable_HIS(_sqlChiDinhCuoi);
                                if (_dataChiDinhCuoi.Rows.Count > 0)
                                {
                                    _dongHSTime_EndDay = _dataChiDinhCuoi.Rows[0]["donghstime_endday"].ToString();
                                    _dongHSTime_long = _dataChiDinhCuoi.Rows[0]["donghstime_long"].ToString();
                                }

                                //update HSBA
                                string _sqlDongHSBA = @" UPDATE vienphi SET vienphistatus=1, vienphidate_ravien='" + _dongHSTime_EndDay + "', chandoanravien='NONE', chandoanravien_code='NONE', vienphistatus_bh=1, duyet_ngayduyet_bh='" + _dongHSTime_EndDay + "', duyet_nguoiduyet_bh='" + Base.SessionLogin.SessionUserHISID + "', duyet_sothutuduyet_bh=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='" + Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='" + _dongHSTime_long + "'), vienphistatus_vp=1, duyet_ngayduyet_vp='" + _dongHSTime_EndDay + "', duyet_nguoiduyet_vp='" + Base.SessionLogin.SessionUserHISID + "', duyet_sothutuduyet_vp=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='" + Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='" + _dongHSTime_long + "'), medicalrecordid_end=(select medicalrecordid from medicalrecord where vienphiid='" + _vienphiid + "' order by medicalrecordid desc limit 1), vienphidate_ravien_update='" + _dongHSTime + "' WHERE vienphiid='" + _vienphiid + "'; UPDATE hosobenhan SET isdownloaded=1, hosobenhanstatus=1, xutrikhambenhid=7, hosobenhandate_ravien='" + _dongHSTime_EndDay + "', chandoanravien_code='NONE', chandoanravien='NONE' WHERE hosobenhanid='" + _hosobenhanid + "'; UPDATE medicalrecord SET medicalrecordstatus=99, thoigianravien='" + _dongHSTime_EndDay + "', chandoanravien='NONE', chandoanravien_code='NONE', xutrikhambenhid=7, medicalrecordremark='Đóng bệnh án tự động' WHERE medicalrecordid=(select medicalrecordid from medicalrecord where vienphiid='" + _vienphiid + "' order by medicalrecordid desc limit 1); UPDATE sothutuphongkham SET sothutustatus=4 WHERE medicalrecordid=(select medicalrecordid from medicalrecord where vienphiid='" + _vienphiid + "' order by medicalrecordid desc limit 1);";
                                if (condb.ExecuteNonQuery_HIS(_sqlDongHSBA))
                                {
                                    _demupdate += 1;
                                    //Log vào DB
                                    string _sqlluulog = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + Base.SessionLogin.SessionUsercode + "', 'Đóng và duyệt VP tự động VP=" + _vienphiid + "' ,'" + Base.SessionLogin.SessionMyIP + "', '" + Base.SessionLogin.SessionMachineName + "', '" + Base.SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + _vienphiid + "', 'TOOL_25');";
                                    condb.ExecuteNonQuery_MeL(_sqlluulog);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        O2S_Common.Logging.LogSystem.Error(ex);
                    }
                    SplashScreenManager.CloseForm();

                    if (_demupdate > 0)
                    {
                        MessageBox.Show("Đóng bệnh án và duyệt viện phí thành công SL=" + _demupdate, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnTimKiem_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Đóng bệnh án và duyệt viện phí thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DuyetVP_Click(object sender, EventArgs e)
        {
            if (Base.SessionLogin.SessionUserHISID == null || Base.SessionLogin.SessionUserHISID == "0")
            {
                MessageBox.Show("Tài khoản chưa được thiết lập HIS ID, vui lòng kiểm tra lại tài khoản để bổ sung", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (gridViewHSBA.GetSelectedRows().Count() > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn duyệt viện phí của BN đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    int _demupdate = 0;
                    SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                    try
                    {
                        if (gridViewHSBA.GetSelectedRows().Count() > 0)
                        {
                            foreach (var item_index in gridViewHSBA.GetSelectedRows())
                            {
                                string _vienphiid = gridViewHSBA.GetRowCellValue(item_index, "vienphiid").ToString();

                                //lay ngay ra vien
                                DateTime _vienphidate_ravien = Utilities.TypeConvertParse.ToDateTime(gridViewHSBA.GetRowCellValue(item_index, "vienphidate_ravien").ToString());
                                string _dongHSTime_EndDay = _vienphidate_ravien.ToString("yyyy-MM-dd") + " 23:59:59";
                                string _dongHSTime_long = _vienphidate_ravien.ToString("yyyyMMdd");

                                //update HSBA
                                string _sqlDongHSBA = @"UPDATE vienphi SET vienphistatus=1, vienphistatus_bh=1, duyet_ngayduyet_bh='" + _dongHSTime_EndDay + "', duyet_nguoiduyet_bh='" + Base.SessionLogin.SessionUserHISID + "', duyet_sothutuduyet_bh=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='" + Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='" + _dongHSTime_long + "'), vienphistatus_vp=1, duyet_ngayduyet_vp='" + _dongHSTime_EndDay + "', duyet_nguoiduyet_vp='" + Base.SessionLogin.SessionUserHISID + "', duyet_sothutuduyet_vp=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='" + Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='" + _dongHSTime_long + "') WHERE vienphiid='" + _vienphiid + "';";
                                if (condb.ExecuteNonQuery_HIS(_sqlDongHSBA))
                                {
                                    _demupdate += 1;
                                    //Log vào DB
                                    string _sqlluulog = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, vienphiid, logtype) VALUES ('" + Base.SessionLogin.SessionUsercode + "', 'Duyệt VP tự động VP=" + _vienphiid + "' ,'" + Base.SessionLogin.SessionMyIP + "', '" + Base.SessionLogin.SessionMachineName + "', '" + Base.SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + _vienphiid + "', 'TOOL_25');";
                                    condb.ExecuteNonQuery_MeL(_sqlluulog);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        O2S_Common.Logging.LogSystem.Error(ex);
                    }
                    SplashScreenManager.CloseForm();

                    if (_demupdate > 0)
                    {
                        MessageBox.Show("Duyệt viện phí thành công SL=" + _demupdate, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnTimKiem_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Duyệt viện phí thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        #region Export
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion



    }
}
