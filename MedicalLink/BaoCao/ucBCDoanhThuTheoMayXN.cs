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

namespace MedicalLink.BaoCao
{
    public partial class ucBCDoanhThuTheoMayXN : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBCDoanhThuTheoMayXN()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string sql_getdata = "";
                string tieuchi = "";
                string dsmayxn = " WHERE SERV.idmayxn in (";
                List<Object> lstMayXNCheck = chkcomboListMayXN.Properties.Items.GetCheckedValues();
                if (lstMayXNCheck.Count > 0)
                {
                    for (int i = 0; i < lstMayXNCheck.Count - 1; i++)
                    {
                        dsmayxn += lstMayXNCheck[i] + ",";
                    }
                    dsmayxn += lstMayXNCheck[lstMayXNCheck.Count - 1] + ") ";
                    if (dsmayxn.Contains("-1"))
                    {
                        dsmayxn = "";
                    }
                }
                else
                {
                    gridControlSoCDHA.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chưa chọn máy xét nghiệm!");
                    frmthongbao.Show();
                    SplashScreenManager.CloseForm();
                    return;
                }
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                //tieu chi
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi = " maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi = " maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi = " vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi = " vienphistatus<>1 and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi = " vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                //SQL chay du lieu
                if (cboTieuChi.Text == "Theo ngày chỉ định" || cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    sql_getdata = "SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt, SERV.ma_xn, SERV.ten_xn, SERV.idmayxn as idmay_xn, SERV.tenmayxn as tenmay_xn, sum(SERV.sl_bhyt) as sl_bhyt, sum(SERV.sl_vp) as sl_vp, sum(SERV.sl_yc) as sl_yc, sum(SERV.sl_nnn) as sl_nnn, sum(coalesce(SERV.sl_bhyt,0) + coalesce(SERV.sl_vp,0) + coalesce(SERV.sl_yc,0) + coalesce(SERV.sl_nnn,0)) as sl_tong, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, sum(coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) as tien_bhyt, sum(coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) as tien_vp, sum(coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) as tien_yc, sum(coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0)) as tien_nnn, sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) as tien_tong, sum(SERV.cp_tructiep) as cp_tructiep, sum(SERV.cp_pttt) as cp_pttt, sum(SERV.cp_maymoc) as cp_maymoc, sum(SERV.cp_ldlk) as cp_ldlk, sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0)) as cp_tong, (sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) - sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0))) as lai, SERV.khoatra_kq FROM (select ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode as ma_xn, ser.servicepricename as ten_xn, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=0 then ser.soluong end) as sl_bhyt, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=1 then ser.soluong end) as sl_vp, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong in (3,4) then ser.soluong end) as sl_yc, (case when ser.doituongbenhnhanid=4 then ser.soluong end) as sl_nnn, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_vp, ser.servicepricemoney as gia_yc, ser.servicepricemoney_nuocngoai as gia_nnn, (ser.chiphidauvao) as cp_tructiep, (ser.chiphipttt) as cp_pttt, (ser.chiphimaymoc) as cp_maymoc, (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) as cp_ldlk, (case when tkq.usercode like '%-sh' then 'Khoa sinh hóa' when tkq.usercode like '%-hh' then 'Khoa huyết học' when tkq.usercode like '%-vs' then 'Khoa vi sinh' when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh' when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa' else '' end) as khoatra_kq, (select s.idmayxn from service s where s.servicepriceid=ser.servicepriceid order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn, (select s.tenmayxn from service s where s.servicepriceid=ser.servicepriceid order by coalesce(s.idmayxn,0) desc limit 1) as tenmayxn from serviceprice ser INNER JOIN (select maubenhphamid, usertrakq from maubenhpham where maubenhphamgrouptype=0 and " + tieuchi + ") MBP ON MBP.maubenhphamid=ser.maubenhphamid LEFT JOIN nhompersonnel tkq on tkq.userhisid=MBP.usertrakq where ser.bhyt_groupcode='03XN') SERV " + dsmayxn + " GROUP BY SERV.ma_xn, SERV.ten_xn, SERV.idmayxn, SERV.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq; ";
                }

                if (cboTieuChi.Text == "Theo ngày vào viện" || cboTieuChi.Text == "Theo ngày ra viện" || cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    sql_getdata = " SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt, SERV.ma_xn, SERV.ten_xn, SERV.idmayxn as idmay_xn, SERV.tenmayxn as tenmay_xn, sum(SERV.sl_bhyt) as sl_bhyt, sum(SERV.sl_vp) as sl_vp, sum(SERV.sl_yc) as sl_yc, sum(SERV.sl_nnn) as sl_nnn, sum(coalesce(SERV.sl_bhyt,0) + coalesce(SERV.sl_vp,0) + coalesce(SERV.sl_yc,0) + coalesce(SERV.sl_nnn,0)) as sl_tong, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, sum(coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) as tien_bhyt, sum(coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) as tien_vp, sum(coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) as tien_yc, sum(coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0)) as tien_nnn, sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) as tien_tong, sum(SERV.cp_tructiep) as cp_tructiep, sum(SERV.cp_pttt) as cp_pttt, sum(SERV.cp_maymoc) as cp_maymoc, sum(SERV.cp_ldlk) as cp_ldlk, sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0)) as cp_tong, (sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) - sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0))) as lai, SERV.khoatra_kq FROM (select ser.vienphiid, ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode as ma_xn, ser.servicepricename as ten_xn, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=0 then ser.soluong end) as sl_bhyt, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=1 then ser.soluong end) as sl_vp, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong in (3,4) then ser.soluong end) as sl_yc, (case when ser.doituongbenhnhanid=4 then ser.soluong end) as sl_nnn, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_vp, ser.servicepricemoney as gia_yc, ser.servicepricemoney_nuocngoai as gia_nnn, (ser.chiphidauvao) as cp_tructiep, (ser.chiphipttt) as cp_pttt, (ser.chiphimaymoc) as cp_maymoc, (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) as cp_ldlk, (case when tkq.usercode like '%-sh' then 'Khoa sinh hóa' when tkq.usercode like '%-hh' then 'Khoa huyết học' when tkq.usercode like '%-vs' then 'Khoa vi sinh' when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh' when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa' else '' end) as khoatra_kq, (select s.idmayxn from service s where s.servicepriceid=ser.servicepriceid order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn, (select s.tenmayxn from service s where s.servicepriceid=ser.servicepriceid order by coalesce(s.idmayxn,0) desc limit 1) as tenmayxn from serviceprice ser INNER JOIN (select maubenhphamid, usertrakq from maubenhpham where maubenhphamgrouptype=0) MBP ON MBP.maubenhphamid=ser.maubenhphamid LEFT JOIN nhompersonnel tkq on tkq.userhisid=MBP.usertrakq where ser.bhyt_groupcode='03XN') SERV INNER JOIN (select vienphiid from vienphi where " + tieuchi + " ) VP ON VP.vienphiid=SERV.vienphiid " + dsmayxn + " GROUP BY SERV.ma_xn, SERV.ten_xn, SERV.idmayxn, SERV.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq;  ";
                }

                dataBaoCao = condb.GetDataTable_HIS(sql_getdata);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = this.dataBaoCao;
                }
                else
                {
                    gridControlSoCDHA.DataSource = null;
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

        #region Load
        private void ucBCDoanhThuTheoMayXN_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadDataDSMayXN();
        }
        private void LoadDataDSMayXN()
        {
            try
            {
                string sql_dsmayxn = "select '-1' as idmayxn, 'Tất cả' as tenmayxn union all (select idmayxn, tenmayxn from (select o.tools_otherlistcode as idmayxn, o.tools_otherlistname as tenmayxn from tools_otherlist o inner join tools_othertypelist ot on ot.tools_othertypelistid=o.tools_othertypelistid where ot.tools_othertypelistcode='DSMAYXN' order by o.tools_otherlistname) O);  ";
                DataTable lstDanhSachXN = condb.GetDataTable_MeL(sql_dsmayxn);
                chkcomboListMayXN.Properties.DataSource = lstDanhSachXN;
                chkcomboListMayXN.Properties.DisplayMember = "tenmayxn";
                chkcomboListMayXN.Properties.ValueMember = "idmayxn";
                chkcomboListMayXN.CheckAll();
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
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";
                string fileTemplatePath = "BC_DoanhThuTheoMayXetNghiem.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_tc = new ClassCommon.reportExcelDTO();
                reportitem_tc.name = Base.bienTrongBaoCao.TIEUCHI;
                reportitem_tc.value = cboTieuChi.Text;
                thongTinThem.Add(reportitem_tc);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
