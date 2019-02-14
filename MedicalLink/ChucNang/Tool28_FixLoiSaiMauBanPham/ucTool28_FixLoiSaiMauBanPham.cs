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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.ChucNang.TOOL27_KiemTraKetNoiPostgre;
using System.Globalization;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucTool28_FixLoiSaiMauBanPham : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #endregion

        public ucTool28_FixLoiSaiMauBanPham()
        {
            InitializeComponent();
        }

        #region Load
        private void ucTool28_FixLoiSaiMauBanPham_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
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
                string _trangthaivp = "";
                string _loaivienphiid = "";
                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbbTrangThaiVP.Text == "Đang điều trị")
                {
                    _trangthaivp = " and vp.vienphistatus=0 ";
                }
                else if (cbbTrangThaiVP.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaivp = " and vp.vienphistatus>0 and vp.vienphistatus_vp=0 ";
                }
                else if (cbbTrangThaiVP.Text == "Đã thanh toán")
                {
                    _trangthaivp = " and vp.vienphistatus>0 and vp.vienphistatus_vp=1 ";
                }
                //

                if (cboBenhAn.Text == "Ngoại trú")
                {
                    _loaivienphiid = " and vp.loaivienphiid=1 ";
                }
                else if (cboBenhAn.Text == "Nội trú")
                {
                    _loaivienphiid = " and vp.loaivienphiid=0 ";
                }

                //string _sqlTimKiem = $@"select mbp1.* from maubenhpham_copy1 mbp1 where mbp1.maubenhphamdate between '{datetungay}' and '{datedenngay}';";

                string _sqlTimKiem = $@"select row_number () over (order by mbp1.maubenhphamid) as stt, (case mbp1.maubenhphamgrouptype	when 0 then 'Xét nghiệm'	when 1 then 'CĐHA'	when 2 then 'Khám bệnh'	when 3 then 'Phiếu điều trị'	when 4 then 'Chuyên khoa '	when 5 then 'Thuốc'	when 6 then 'Vật tư '	end) as maubenhphamgrouptype_name, mbp1.*, hsba.patientname as patientname_hsba
from maubenhpham_copy1 mbp1
    inner join vienphi vp on vp.vienphiid=mbp1.vienphiid
	inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join serviceprice ser on ser.maubenhphamid=mbp1.maubenhphamid and ser.vienphiid=mbp1.vienphiid and ser.medicalrecordid=mbp1.medicalrecordid and ser.departmentid=mbp1.departmentid
where mbp1.maubenhphamdate between '{datetungay}' and '{datedenngay}' {_trangthaivp} {_loaivienphiid}
	and ser.vienphiid<>(select vienphiid from maubenhpham where maubenhphamid=mbp1.maubenhphamid);";
                DataTable _dataMPB = condb.GetDataTable_HIS(_sqlTimKiem);
                if (_dataMPB.Rows.Count > 0)
                {
                    gridControlMBP.DataSource = _dataMPB;
                }
                else
                {
                    gridControlMBP.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void gridViewMBP_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemmenu1 = new DXMenuItem("Cập nhật sang bảng Maubenhpham");
                    itemmenu1.Image = imMenu_HSBA.Images[0];
                    itemmenu1.Click += new EventHandler(CapNhatMauBenhPham_Click);
                    e.Menu.Items.Add(itemmenu1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnUpdateIDMBP1_Click(object sender, EventArgs e)
        {
            try
            {
                //string _sqlupdate = "UPDATE maubenhpham_copy1 SET maubenhphamID = nextval('maubenhpham_maubenhphamid_seq'::regclass);";
                //if (condb.ExecuteNonQuery_HIS(_sqlupdate))
                //{
                //    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                //    frmthongbao.Show();
                //}
                //else
                //{
                //    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THAT_BAI);
                //    frmthongbao.Show();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void gridViewMBP_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewMBP.FocusedRowHandle;
                string _maubenhphamid = gridViewMBP.GetRowCellValue(rowHandle, "maubenhphamid").ToString();
                string _vienphiid = gridViewMBP.GetRowCellValue(rowHandle, "vienphiid").ToString();
                string _medicalrecordid = gridViewMBP.GetRowCellValue(rowHandle, "medicalrecordid").ToString();
                string _departmentid = gridViewMBP.GetRowCellValue(rowHandle, "departmentid").ToString();

                string _sqlTimKiem = $@"select * from servicepricewhere maubenhphamid={_maubenhphamid} and vienphiid={_vienphiid} and medicalrecordid={_medicalrecordid} and departmentid={_departmentid}and vienphiid<>(select vienphiid from maubenhpham where maubenhphamid={_maubenhphamid});";
                DataTable _dataDVKT = condb.GetDataTable_HIS(_sqlTimKiem);
                if (_dataDVKT.Rows.Count > 0)
                {
                    gridControlDVKT.DataSource = _dataDVKT;
                }
                else
                {
                    gridControlDVKT.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        //Xu ly BN da chon
        private void CapNhatMauBenhPham_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item_index in gridViewMBP.GetSelectedRows())
                {
                    string _maubenhphamid = gridViewMBP.GetRowCellValue(item_index, "maubenhphamid").ToString();
                    string _vienphiid = gridViewMBP.GetRowCellValue(item_index, "vienphiid").ToString();
                    string _medicalrecordid = gridViewMBP.GetRowCellValue(item_index, "medicalrecordid").ToString();
                    string _departmentid = gridViewMBP.GetRowCellValue(item_index, "departmentid").ToString();

                    string _maubenhphamID_new = "0";
                    //-InsertMaubenhpham
                    string _sqlInsertMBP = "INSERT INTO maubenhpham(medicalrecordid, patientid, vienphiid, hosobenhanid, dathutien, datamung, servicepriceid_master, phieudieutriid, sothutuid, sothutunumber, sothutunumberdagoi, sothutuid_laymau, sothutunumber_laymau, sothutunumberdagoi_laymau, medicinestoreid, departmentgroupid, departmentid, buonggiuong, userid, departmentgroupid_des, departmentid_des, departmentid_des_org, departmentid_laymau, medicinestorebillid, sophieu, barcode, maubenhphamtype, maubenhphamhaophi, maubenhphamphieutype, maubenhphamgrouptype, maubenhphamdatastatus, maubenhphamstatus_laymau, maubenhphamprintstatus, maubenhphamstatus, maubenhphamdate, maubenhphamdate_sudung, maubenhphamdate_thuchien, maubenhphamdate_laymau, maubenhphamfinishdate, unmapedhisservice, isdeleted, maubenhphamdeletedate, userdelete, usercreate, userupdatebarcode, userlastupdate, userupdatebarcodedate, userlastupdatedate, patientpid, doituong, patientname, patientaddress, patientphone, patientbirthday, patientsex, chandoan, remark, version, userduyetall, usertrakq, ismaubenhphamtemp, sothutuphongkhamid, userthuchien, thoigiandukiencoketqua, sodienthoaibaotinkhicoketqua, sync_flag, update_flag, dacodichvuthutien, dacodichvuduyetcanlamsang, sessionid, isautongaygiuong, sothutuchidinhcanlamsang, isencript, maubenhphamdate_org, departmentid_traketqua, numbertraketqua, isdatraketqua, usertraketqua, maubenhphamtraketquadate, remarkdetail, chandoan_code, hl7_is, hl7_isdatraketqua, hl7_maubenhphamtraketquadate, loidanbacsi, phieutonghopsuatanid, medicinekiemkeid, isloaidonthuoc, servicepriceid_thanhtoanrieng, sothutunumber_h_n, userlaymau, dm_loaibenhphamcode, maubenhphamtype_capcuu, chandoan_code_2, chandoan_2, chandoan_code_3, chandoan_3, dotdungthuoc, dotdung_tungay, dotdung_denngay) SELECT medicalrecordid, patientid, vienphiid, hosobenhanid, dathutien, datamung, servicepriceid_master, phieudieutriid, sothutuid, sothutunumber, sothutunumberdagoi, sothutuid_laymau, sothutunumber_laymau, sothutunumberdagoi_laymau, medicinestoreid, departmentgroupid, departmentid, buonggiuong, userid, departmentgroupid_des, departmentid_des, departmentid_des_org, departmentid_laymau, medicinestorebillid, sophieu, barcode, maubenhphamtype, maubenhphamhaophi, maubenhphamphieutype, maubenhphamgrouptype, maubenhphamdatastatus, maubenhphamstatus_laymau, maubenhphamprintstatus, maubenhphamstatus, maubenhphamdate, maubenhphamdate_sudung, maubenhphamdate_thuchien, maubenhphamdate_laymau, maubenhphamfinishdate, unmapedhisservice, isdeleted, maubenhphamdeletedate, userdelete, usercreate, userupdatebarcode, userlastupdate, userupdatebarcodedate, userlastupdatedate, patientpid, doituong, patientname, patientaddress, patientphone, patientbirthday, patientsex, chandoan, remark, version, userduyetall, usertrakq, ismaubenhphamtemp, sothutuphongkhamid, userthuchien, thoigiandukiencoketqua, sodienthoaibaotinkhicoketqua, sync_flag, update_flag, dacodichvuthutien, dacodichvuduyetcanlamsang, sessionid, isautongaygiuong, sothutuchidinhcanlamsang, isencript, maubenhphamdate_org, departmentid_traketqua, numbertraketqua, isdatraketqua, usertraketqua, maubenhphamtraketquadate, remarkdetail, chandoan_code, hl7_is, hl7_isdatraketqua, hl7_maubenhphamtraketquadate, loidanbacsi, phieutonghopsuatanid, medicinekiemkeid, isloaidonthuoc, servicepriceid_thanhtoanrieng, sothutunumber_h_n, userlaymau, dm_loaibenhphamcode, maubenhphamtype_capcuu, chandoan_code_2, chandoan_2, chandoan_code_3, chandoan_3, dotdungthuoc, dotdung_tungay, dotdung_denngay FROM maubenhpham_copy1 WHERE maubenhphamid=" + _maubenhphamid + " RETURNING maubenhphamid;";

                    DataTable _dataInsert = condb.GetDataTable_HIS(_sqlInsertMBP);
                    if (_dataInsert.Rows.Count > 0)
                    {
                        _maubenhphamID_new = _dataInsert.Rows[0]["maubenhphamid"].ToString();
                    }

                    //-Update ID vào Ser, Se
                    string _updateSe = $@"UPDATE service SET maubenhphamid={_maubenhphamID_new}
WHERE servicepriceid in (select servicepriceid from serviceprice
where maubenhphamid={_maubenhphamid} 
and vienphiid={_vienphiid} 
and medicalrecordid={_medicalrecordid} 
and departmentid={_departmentid}
and vienphiid<>(select vienphiid from maubenhpham where maubenhphamid={_maubenhphamid}));";

                    string _updateSER = $@"UPDATE serviceprice ser SET maubenhphamid={_maubenhphamID_new} 
WHERE servicepriceid in (select servicepriceid from serviceprice
where maubenhphamid={_maubenhphamid} 
and vienphiid={_vienphiid} 
and medicalrecordid={_medicalrecordid} 
and departmentid={_departmentid}
and vienphiid<>(select vienphiid from maubenhpham where maubenhphamid={_maubenhphamid}));
UPDATE maubenhpham_copy1 SET remark='1' WHERE maubenhphamid={_maubenhphamid};";

                    if (condb.ExecuteNonQuery_HIS(_updateSe) && condb.ExecuteNonQuery_HIS(_updateSER))
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                        frmthongbao.Show();
                        //Luu lai Log
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype, vienphiid) VALUES ('" + SessionLogin.SessionUsercode + "', 'Cập nhật MBP_ID=" + _maubenhphamid + " sang MBP_ID_New=" + _maubenhphamID_new + " ', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_28', '" + _vienphiid + "');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THAT_BAI);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Xuat excel
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dataBaoCao = _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewMBP);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("DANH SÁCH PHIẾU", _dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewChiTiet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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


    }
}
