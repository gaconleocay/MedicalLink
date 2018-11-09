using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using MedicalLink.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao.BC57_KeThuocVatTuTruocNgay
{
    public partial class frmXemChiTietDonThuocVatTu : Form
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion

        public frmXemChiTietDonThuocVatTu()
        {
            InitializeComponent();
        }

        public frmXemChiTietDonThuocVatTu(string _lstKhoaChonLayBC, string _maubenhphamgrouptype)
        {
            InitializeComponent();
            LoadDanhSachThuocVatTuChiTiet(_lstKhoaChonLayBC, _maubenhphamgrouptype);
        }

        private void LoadDanhSachThuocVatTuChiTiet(string _lstKhoaChonLayBC, string _maubenhphamgrouptype)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_hsba = "";// and hosobenhandate>='2014-01-01 00:00:00' ";
                string _tieuchi_vp = ""; //" and vienphidate>='2017-01-01 00:00:00' ";

                string _sqlBC = $@"SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate_sudung) as stt, 
	vp.patientid, 
	vp.vienphiid, 
	hsba.patientname,
	hsba.bhytcode,
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	mbp.maubenhphamid,
	mbp.maubenhphamdate,
	mbp.maubenhphamdate_sudung,
	((mbp.maubenhphamdate_sudung::date)-(now()::date)) as songay,
	(case when mbp.maubenhphamstatus in (4,5,9) then 'Đã THYL' end) as dathyl,
	mes.medicinestorename,
	ncd.usercode,
	ncd.username as nguoichidinh,
	(case when mbp.maubenhphamphieutype=1 then 'Phiếu trả' end) as maubenhphamphieutype,
	(case vp.doituongbenhnhanid 
			when 1 then 'BHYT'
			when 2 then 'Viện phí'
			when 3 then 'Dịch vụ'
			when 4 then 'Người nước ngoài'
			when 5 then 'Miễn phí'
			when 6 then 'Hợp đồng'
			end) as doituongbenhnhan,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,	
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus
FROM 
	(select maubenhphamid,vienphiid,userid,maubenhphamstatus,maubenhphamdate,medicinestoreid,maubenhphamdate_sudung,departmentgroupid,departmentid,maubenhphamphieutype from maubenhpham m where maubenhphamdate_sudung>now() and medicinestoreid not in (144,145,146,147,148,158,164,165,181) {_maubenhphamgrouptype} {_lstKhoaChonLayBC}) mbp 
	INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_vp}) vp on vp.vienphiid=mbp.vienphiid
	LEFT JOIN (select userhisid,usercode,username from nhompersonnel) ncd on ncd.userhisid=mbp.userid
	INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=mbp.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=mbp.departmentid
	LEFT JOIN (select medicinestoreid,medicinestorename from medicine_store) mes on mes.medicinestoreid=mbp.medicinestoreid;";
                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlBC);
                if (_dataBaoCao.Rows.Count > 0)
                {
                    gridControlBNDetail.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBNDetail.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

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
        private void gridViewBNDetail_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                GridView View = sender as GridView;
                if (e.RowHandle >= 0)
                {
                    int tongsongay = Utilities.TypeConvertParse.ToInt32(View.GetRowCellDisplayText(e.RowHandle, View.Columns["songay"]));
                    if (tongsongay >= 4 && tongsongay <= 7)
                    {
                        e.Appearance.BackColor = Color.PeachPuff;
                        e.HighPriority = true;
                    }
                    else if (tongsongay > 7)
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.HighPriority = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBNDetail);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("DANH SÁCH PHIẾU CHỈ ĐỊNH", data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
    }
}
