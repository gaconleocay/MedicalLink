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

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string sql_getdata = "";
                string _tieuchi_ser = " and servicepricedate>'2017-05-01 00:00:00' ";
                string _tieuchi_mbp = " and maubenhphamdate>'2017-05-01 00:00:00' ";
                string _tieuchi_vp = " and vienphidate>'2017-05-01 00:00:00' ";
                string _trangthaibenhan = "";
                string _doituong_vp = "";
                string _doituong_ser = "";
                string _loaibaocao = " and departmentid_des<>253 "; //253: phong xn "Khoa Vi Sinh"
                string _dieukien_khacvisinh = " and chiphi.mayxn_ma=SERV.idmayxn ";
                string _tenmayxn_khacvisinh_select = " , SERV.idmayxn as idmay_xn, SERV.tenmayxn as tenmay_xn ";
                string _tenmayxn_khacvisinh_groupby = ",SERV.idmayxn,SERV.tenmayxn";

                string dsmayxn = " WHERE SERV.idmayxn in (";
                List<Object> lstMayXNCheck = chkcomboListMayXN.Properties.Items.GetCheckedValues();
                if (lstMayXNCheck.Count > 0)
                {
                    for (int i = 0; i < lstMayXNCheck.Count - 1; i++)
                    {
                        dsmayxn += "'" + lstMayXNCheck[i] + "',";
                    }
                    dsmayxn += "'" + lstMayXNCheck[lstMayXNCheck.Count - 1] + "') ";
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
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    _tieuchi_mbp = " and maubenhphamfinishdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphistatus>0 and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                //Doi tuong 
                if (cboDoiTuong.Text == "Đối tượng BHYT")
                {
                    _doituong_vp = " and doituongbenhnhanid=1 ";
                }
                else if (cboDoiTuong.Text == "Đối tượng Viện phí")
                {
                    _doituong_vp = " and doituongbenhnhanid<>1 ";
                }
                else if (cboDoiTuong.Text == "ĐT BHYT + DV BHYT")
                {
                    _doituong_vp = " and doituongbenhnhanid=1 ";
                    _doituong_ser = " and loaidoituong in (0,4,6) ";
                }


                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    _trangthaibenhan = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaibenhan = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    _trangthaibenhan = " and vienphistatus<>0 and vienphistatus_vp=1 ";
                }
                //loai bao cao 
                if (cboLoaiBaoCao.Text == "Xét nghiệm Vi sinh")
                {
                    _loaibaocao = " and departmentid_des=253 ";
                    _dieukien_khacvisinh = " ";
                    _tenmayxn_khacvisinh_select = " , chiphi.mayxn_ma as idmay_xn, chiphi.mayxn_ten as tenmay_xn ";
                    _tenmayxn_khacvisinh_groupby = ",chiphi.mayxn_ma,chiphi.mayxn_ten";
                    dsmayxn = "";
                }

                sql_getdata = @"SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt, SERV.ma_xn, SERV.ten_xn " + _tenmayxn_khacvisinh_select + ", sum(SERV.sl_bhyt) as sl_bhyt, sum(SERV.sl_vp) as sl_vp, sum(SERV.sl_yc) as sl_yc, sum(SERV.sl_nnn) as sl_nnn, sum(coalesce(SERV.sl_bhyt,0) + coalesce(SERV.sl_vp,0) + coalesce(SERV.sl_yc,0) + coalesce(SERV.sl_nnn,0)) as sl_tong, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, sum(coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) as tien_bhyt, sum(coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) as tien_vp, sum(coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) as tien_yc, sum(coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0)) as tien_nnn, sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) as tien_tong, sum(SERV.cp_tructiep) as cp_tructiep, sum(SERV.cp_pttt) as cp_pttt, sum(SERV.cp_maymoc) as cp_maymoc, sum(SERV.cp_ldlk) as cp_ldlk, (sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0)) + sum((coalesce(chiphi.cp_hoachat,0)+coalesce(chiphi.cp_haophixn,0)+coalesce(chiphi.cp_luong,0)+coalesce(chiphi.cp_diennuoc,0)+coalesce(chiphi.cp_khmaymoc,0)+coalesce(chiphi.cp_khxaydung,0))*SERV.soluong)) as cp_tong, (sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) - sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0)) - sum((coalesce(chiphi.cp_hoachat,0)+coalesce(chiphi.cp_haophixn,0)+coalesce(chiphi.cp_luong,0)+coalesce(chiphi.cp_diennuoc,0)+coalesce(chiphi.cp_khmaymoc,0)+coalesce(chiphi.cp_khxaydung,0))*SERV.soluong)) as lai, SERV.khoatra_kq, chiphi.khuvuc_ten, sum(coalesce(chiphi.cp_hoachat,0)*SERV.soluong) as cp_hoachat_tong, sum(coalesce(chiphi.cp_haophixn,0)*SERV.soluong) as cp_haophixn_tong, sum(coalesce(chiphi.cp_luong,0)*SERV.soluong) as cp_luong_tong, sum(coalesce(chiphi.cp_diennuoc,0)*SERV.soluong) as cp_diennuoc_tong, sum(coalesce(chiphi.cp_khmaymoc,0)*SERV.soluong) as cp_khmaymoc_tong, sum(coalesce(chiphi.cp_khxaydung,0)*SERV.soluong) as cp_khxaydung_tong FROM (select ser.vienphiid, ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode as ma_xn, ser.servicepricename as ten_xn, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=0 then ser.soluong end) as sl_bhyt, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=1 then ser.soluong end) as sl_vp, (case when ser.doituongbenhnhanid<>4 and ser.loaidoituong in (3,4) then ser.soluong end) as sl_yc, (case when ser.doituongbenhnhanid=4 then ser.soluong end) as sl_nnn, ser.soluong, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_vp, ser.servicepricemoney as gia_yc, ser.servicepricemoney_nuocngoai as gia_nnn, (ser.chiphidauvao) as cp_tructiep, (ser.chiphipttt) as cp_pttt, (ser.chiphimaymoc) as cp_maymoc, (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) as cp_ldlk, (case when tkq.usercode like '%-sh' then 'Khoa sinh hóa' when tkq.usercode like '%-hh' then 'Khoa huyết học' when tkq.usercode like '%-vs' then 'Khoa vi sinh' when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh' when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa' else '' end) as khoatra_kq, (select s.idmayxn from service s where s.servicepriceid=ser.servicepriceid and s.servicedate>'2017-05-01 00:00:00' order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn, (select s.tenmayxn from service s where s.servicepriceid=ser.servicepriceid and s.servicedate>'2017-05-01 00:00:00' order by coalesce(s.idmayxn,0) desc limit 1) as tenmayxn from (select servicepriceid,vienphiid,maubenhphamid,servicepricecode,servicepricename,doituongbenhnhanid,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,servicepricemoney_nuocngoai,chiphidauvao,chiphipttt,chiphimaymoc,mayytedbid from serviceprice where bhyt_groupcode='03XN' " + _tieuchi_ser + _doituong_ser + ") ser INNER JOIN (select maubenhphamid,usertrakq from maubenhpham where maubenhphamgrouptype=0 " + _tieuchi_mbp + _loaibaocao + ") MBP ON MBP.maubenhphamid=ser.maubenhphamid LEFT JOIN nhompersonnel tkq on tkq.userhisid=MBP.usertrakq ) SERV INNER JOIN (select vienphiid from vienphi where 1=1 " + _tieuchi_vp + _trangthaibenhan + _doituong_vp + ") VP ON VP.vienphiid=SERV.vienphiid LEFT JOIN (SELECT * FROM dblink('myconn_mel','select kv.mayxn_ma,kv.mayxn_ten,kv.khuvuc_ma,kv.khuvuc_ten,cp.servicepricecode,cp.cp_hoachat,cp.cp_haophixn,cp.cp_luong,cp.cp_diennuoc,cp.cp_khmaymoc,cp.cp_khxaydung from ml_mayxnkhuvuc kv left join ml_mayxnchiphi cp on cp.mayxn_ma=kv.mayxn_ma') AS ml_mayxn(mayxn_ma integer,mayxn_ten text,khuvuc_ma text,khuvuc_ten text,servicepricecode text,cp_hoachat double precision,cp_haophixn double precision,cp_luong double precision,cp_diennuoc double precision,cp_khmaymoc double precision,cp_khxaydung double precision)) chiphi on chiphi.servicepricecode=SERV.ma_xn " + _dieukien_khacvisinh + dsmayxn + " GROUP BY SERV.ma_xn,SERV.ten_xn" + _tenmayxn_khacvisinh_groupby + ",SERV.gia_bhyt,SERV.gia_vp,SERV.gia_yc,SERV.gia_nnn,SERV.khoatra_kq,chiphi.khuvuc_ten; ";

                this.dataBaoCao = condb.GetDataTable_Dblink_MeL(sql_getdata);
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

        #endregion

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
                string sql_dsmayxn = @"select '-1' as idmayxn, 'Tất cả' as tenmayxn union all (select mayxn_ma as idmayxn, mayxn_ten as tenmayxn from ml_mayxnkhuvuc order by mayxn_ten); ";
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

        #region Xuat excel va in
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
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

                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, this.dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
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
        private void cboLoaiBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboLoaiBaoCao.Text == "Xét nghiệm Vi sinh")
                {
                    chkcomboListMayXN.Enabled = false;
                }
                else
                {
                    chkcomboListMayXN.Enabled = true;
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
