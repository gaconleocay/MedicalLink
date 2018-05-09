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
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;

namespace MedicalLink.BaoCao
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucBCThucHienCLS_TongHop : UserControl
    {
        #region Declaration
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBCPTTT { get; set; }
        #endregion

        #region Load
        public ucBCThucHienCLS_TongHop()
        {
            InitializeComponent();
        }

        private void ucBCThucHienCLS_TongHop_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDanhMucPhongThucHien();
        }
        private void LoadDanhMucPhongThucHien()
        {
            try
            {
                var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmenttype == 6 || o.departmenttype == 7).OrderBy(o => o.departmentname).ToList();
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                    chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                    chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                }

                chkcomboListDSPhong.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                if (chkcomboListDSPhong.Properties.Items.GetCheckedValues().Count == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_PHONG_THUC_HIEN);
                    frmthongbao.Show();
                    return;
                }
                LayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void LayDuLieuBaoCao_ChayMoi()
        {
            try
            {
                EnableAndDisableNutIn();
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _tieuchi_pttt = "";
                string _tieuchi_mbp = "";
                string lstKhoacheck = " where departmentid_des in (";
                string _trangthaipttt = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    _tieuchi_vp = " where vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    _tieuchi_pttt = " and thuchienclsdate between '" + tungay + "' and '" + denngay + "' ";
                }
                else
                {
                    _tieuchi_ser = " and servicepricedate between '" + tungay + "' and '" + denngay + "' ";
                    _tieuchi_mbp= " and maubenhphamdate between '" + tungay + "' and '" + denngay + "' ";
                }

                if (cboTrangThai.Text == "Chưa gửi YC")
                {
                    _trangthaipttt = " and coalesce(duyetpttt_stt,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã gửi YC")
                {
                    _trangthaipttt = " and duyetpttt_stt=1 ";
                }
                else if (cboTrangThai.Text == "Đã tiếp nhận YC")
                {
                    _trangthaipttt = " and duyetpttt_stt=2 ";
                }
                else if (cboTrangThai.Text == "Đã duyệt PTTT")
                {
                    _trangthaipttt = " and duyetpttt_stt=3 ";
                }
                //else if (cboTrangThai.Text == "Đã khóa")
                //{
                //    _trangthaipttt = " and duyetpttt_stt=99 ";
                //}

                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                {
                    lstKhoacheck += "'" + lstPhongCheck[i] + "',";
                }
                lstKhoacheck += "'" + lstPhongCheck[lstPhongCheck.Count - 1] + "') ";

                string sql_laydulieu = " SELECT O.*, (O.ptdb_moi+O.ptdb_mc+O.ptdb_phu+O.ptdb_gv) as ptdb_tong, (O.ptl1_moi+O.ptl1_mc+O.ptl1_phu+O.ptl1_gv) as ptl1_tong, (O.ptl2_moi+O.ptl2_mc+O.ptl2_phu+O.ptl2_gv) as ptl2_tong, (O.ptl3_moi+O.ptl3_mc+O.ptl3_phu+O.ptl3_gv) as ptl3_tong, (O.ptdb_moi+O.ptdb_mc+O.ptdb_phu+O.ptdb_gv+O.ptl1_moi+O.ptl1_mc+O.ptl1_phu+O.ptl1_gv+O.ptl2_moi+O.ptl2_mc+O.ptl2_phu+O.ptl2_gv+O.ptl3_moi+O.ptl3_mc+O.ptl3_phu+O.ptl3_gv) as pt_tongsl, (O.ptdb_moi*280000+O.ptdb_mc*280000+O.ptdb_phu*200000+O.ptdb_gv*120000+O.ptl1_moi*125000+O.ptl1_mc*125000+O.ptl1_phu*90000+O.ptl1_gv*70000+O.ptl2_moi*65000+O.ptl2_mc*65000+O.ptl2_phu*50000+O.ptl2_gv*30000+O.ptl3_moi*50000+O.ptl3_mc*50000+O.ptl3_phu*30000+O.ptl3_gv*15000) as pt_tongtien, (O.ttdb_mc+O.ttdb_phu+O.ttdb_gv) as ttdb_tong, (O.ttl1_mc+O.ttl1_phu+O.ttl1_gv) as ttl1_tong, (O.ttl2_mc+O.ttl2_phu+O.ttl2_gv) as ttl2_tong, (O.ttl3_mc+O.ttl3_phu+O.ttl3_gv) as ttl3_tong, (O.ttdb_mc+O.ttdb_phu+O.ttdb_gv+O.ttl1_mc+O.ttl1_phu+O.ttl1_gv+O.ttl2_mc+O.ttl2_phu+O.ttl2_gv+O.ttl3_mc+O.ttl3_phu+O.ttl3_gv) as tt_tongsl, (O.ttdb_mc*84000+O.ttdb_phu*60000+O.ttdb_gv*36000+O.ttl1_mc*37500+O.ttl1_phu*27000+O.ttl1_gv*21000+O.ttl2_mc*19500+O.ttl2_phu*0+O.ttl2_gv*9000+O.ttl3_mc*15000+O.ttl3_phu*0+O.ttl3_gv*4500) as tt_tongtien, (O.ptdb_moi*280000+O.ptdb_mc*280000+O.ptdb_phu*200000+O.ptdb_gv*120000+O.ptl1_moi*125000+O.ptl1_mc*125000+O.ptl1_phu*90000+O.ptl1_gv*70000+O.ptl2_moi*65000+O.ptl2_mc*65000+O.ptl2_phu*50000+O.ptl2_gv*30000+O.ptl3_moi*50000+O.ptl3_mc*50000+O.ptl3_phu*30000+O.ptl3_gv*15000+O.ttdb_mc*84000+O.ttdb_phu*60000+O.ttdb_gv*36000+O.ttl1_mc*37500+O.ttl1_phu*27000+O.ttl1_gv*21000+O.ttl2_mc*19500+O.ttl2_phu*0+O.ttl2_gv*9000+O.ttl3_mc*15000+O.ttl3_phu*0+O.ttl3_gv*4500) as tongtien FROM (SELECT row_number () over (order by de.departmentname,nv.username) as stt, U.userid, nv.username, U.departmentid_des, de.departmentname, sum(case when U.pttt_loaiid=1 and U.user_loai='bacsigayme' then U.soluong else 0 end) as ptdb_moi, sum(case when U.pttt_loaiid=1 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptdb_mc, sum(case when U.pttt_loaiid=1 and U.user_loai='phumo1' then U.soluong else 0 end) as ptdb_phu, sum(case when U.pttt_loaiid=1 and U.user_loai='phumo3' then U.soluong else 0 end) as ptdb_gv, sum(case when U.pttt_loaiid=2 and U.user_loai='bacsigayme' then U.soluong else 0 end) as ptl1_moi, sum(case when U.pttt_loaiid=2 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptl1_mc, sum(case when U.pttt_loaiid=2 and U.user_loai='phumo1' then U.soluong else 0 end) as ptl1_phu, sum(case when U.pttt_loaiid=2 and U.user_loai='phumo3' then U.soluong else 0 end) as ptl1_gv, sum(case when U.pttt_loaiid=3 and U.user_loai='bacsigayme' then U.soluong else 0 end) as ptl2_moi, sum(case when U.pttt_loaiid=3 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptl2_mc, sum(case when U.pttt_loaiid=3 and U.user_loai='phumo1' then U.soluong else 0 end) as ptl2_phu, sum(case when U.pttt_loaiid=3 and U.user_loai='phumo3' then U.soluong else 0 end) as ptl2_gv, sum(case when U.pttt_loaiid=4 and U.user_loai='bacsigayme' then U.soluong else 0 end) as ptl3_moi, sum(case when U.pttt_loaiid=4 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptl3_mc, sum(case when U.pttt_loaiid=4 and U.user_loai='phumo1' then U.soluong else 0 end) as ptl3_phu, sum(case when U.pttt_loaiid=4 and U.user_loai='phumo3' then U.soluong else 0 end) as ptl3_gv, sum(case when U.pttt_loaiid=5 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttdb_mc, sum(case when U.pttt_loaiid=5 and U.user_loai='phumo1' then U.soluong else 0 end) as ttdb_phu, sum(case when U.pttt_loaiid=5 and U.user_loai='phumo3' then U.soluong else 0 end) as ttdb_gv, sum(case when U.pttt_loaiid=6 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttl1_mc, sum(case when U.pttt_loaiid=6 and U.user_loai='phumo1' then U.soluong else 0 end) as ttl1_phu, sum(case when U.pttt_loaiid=6 and U.user_loai='phumo3' then U.soluong else 0 end) as ttl1_gv, sum(case when U.pttt_loaiid=7 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttl2_mc, sum(case when U.pttt_loaiid=7 and U.user_loai='phumo1' then U.soluong else 0 end) as ttl2_phu, sum(case when U.pttt_loaiid=7 and U.user_loai='phumo3' then U.soluong else 0 end) as ttl2_gv, sum(case when U.pttt_loaiid=8 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttl3_mc, sum(case when U.pttt_loaiid=8 and U.user_loai='phumo1' then U.soluong else 0 end) as ttl3_phu, sum(case when U.pttt_loaiid=8 and U.user_loai='phumo3' then U.soluong else 0 end) as ttl3_gv FROM (select pttt.bacsigayme as userid, 'bacsigayme' as user_loai, sum(ser.soluong) as soluong, serf.pttt_loaiid, mbp.departmentid_des from (select servicepriceid,vienphiid,servicepricecode,soluong,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') " + _tieuchi_ser + _trangthaipttt + " ) ser inner join (select maubenhphamid,departmentid_des from maubenhpham " + lstKhoacheck + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select servicepriceid,bacsigayme,phauthuatvien,phumo1,phumo3 from thuchiencls where bacsigayme>0 " + _tieuchi_pttt + ") pttt on pttt.servicepriceid=ser.servicepriceid inner join (select vienphiid from vienphi " + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode group by pttt.bacsigayme,serf.pttt_loaiid,mbp.departmentid_des union all select pttt.phauthuatvien as userid, 'phauthuatvien' as user_loai, sum(ser.soluong) as soluong, serf.pttt_loaiid, mbp.departmentid_des from (select servicepriceid,vienphiid,servicepricecode,soluong,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') " + _tieuchi_ser + _trangthaipttt + " ) ser inner join (select maubenhphamid,departmentid_des from maubenhpham " + lstKhoacheck + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select servicepriceid,bacsigayme,phauthuatvien,phumo1,phumo3 from thuchiencls where phauthuatvien>0 " + _tieuchi_pttt + ") pttt on pttt.servicepriceid=ser.servicepriceid inner join (select vienphiid from vienphi " + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode group by pttt.phauthuatvien,serf.pttt_loaiid,mbp.departmentid_des union all select pttt.phumo1 as userid, 'phumo1' as user_loai, sum(ser.soluong) as soluong, serf.pttt_loaiid, mbp.departmentid_des from (select servicepriceid,vienphiid,servicepricecode,soluong,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') " + _tieuchi_ser + _trangthaipttt + " ) ser inner join (select maubenhphamid,departmentid_des from maubenhpham " + lstKhoacheck + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select servicepriceid,bacsigayme,phauthuatvien,phumo1,phumo3 from thuchiencls where phumo1>0 " + _tieuchi_pttt + ") pttt on pttt.servicepriceid=ser.servicepriceid inner join (select vienphiid from vienphi " + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode group by pttt.phumo1,serf.pttt_loaiid,mbp.departmentid_des union all select pttt.phumo3 as userid, 'phumo3' as user_loai, sum(ser.soluong) as soluong, serf.pttt_loaiid, mbp.departmentid_des from (select servicepriceid,vienphiid,servicepricecode,soluong,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') " + _tieuchi_ser + _trangthaipttt + " ) ser inner join (select maubenhphamid,departmentid_des from maubenhpham " + lstKhoacheck + _tieuchi_mbp + ") mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select servicepriceid,bacsigayme,phauthuatvien,phumo1,phumo3 from thuchiencls where phumo3>0 " + _tieuchi_pttt + ") pttt on pttt.servicepriceid=ser.servicepriceid inner join (select vienphiid from vienphi " + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('03XN','04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode group by pttt.phumo3,serf.pttt_loaiid,mbp.departmentid_des) U LEFT JOIN nhompersonnel nv ON nv.userhisid=U.userid INNER JOIN department de on de.departmentid=U.departmentid_des GROUP BY U.userid,nv.username,U.departmentid_des,de.departmentname) O;";

                this.dataBCPTTT = condb.GetDataTable_HIS(sql_laydulieu);
                if (dataBCPTTT != null && dataBCPTTT.Rows.Count > 0)
                {
                    gridControlDataBCPTTT.DataSource = this.dataBCPTTT;
                }
                else
                {
                    gridControlDataBCPTTT.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Custom
        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Xuat bao cao
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_ThanhToanPTTTCLS_TongHop.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataBCPTTT);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = "( Từ " + tungay + " - " + denngay + " )";
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_ThanhToanPTTTCLS_TongHop.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBCPTTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Hien Thi nut In xuat Excel
        private void EnableAndDisableNutIn()
        {
            try
            {
                if (cboTrangThai.Text == "Đã duyệt PTTT" || CheckPermission.ChkPerModule("SYS_05") || CheckPermission.ChkPerModule("THAOTAC_06"))
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }

                if (CheckPermission.ChkPerModule("SYS_05") || (CheckPermission.ChkPerModule("THAOTAC_07") && cboTrangThai.Text == "Đã duyệt PTTT") || CheckPermission.ChkPerModule("THAOTAC_06"))
                {
                    tbnExport.Enabled = true;
                }
                else
                {
                    tbnExport.Enabled = false;
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
