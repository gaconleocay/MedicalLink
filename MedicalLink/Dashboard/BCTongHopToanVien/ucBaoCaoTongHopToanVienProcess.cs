using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess;
using MedicalLink.DatabaseProcess.FilterDTO;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoTongHopToanVien : UserControl
    {
        /// <summary>
        /// tieuchi">=0: theo khoa ra vien; =1: theo khoa chi dinh
        /// kieuxem">=0: xem tong hop; =1: xem chi tiet
        /// </summary>
        /// <param name="tieuchi"></param>
        /// <param name="kieuxem"></param>
        internal void LayDuLieuBaoCao_ChayMoi(int tieuchi, int kieuxem)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string dateTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string dateDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string sqlLayBaoCao = "";

                //=0: theo khoa ra vien; =1: theo khoa chi dinh
                //=0: xem tong hop; =1: xem chi tiet theo khoa
                if (tieuchi == 0 && kieuxem == 0)//theo khoa ra vien + xem tong hop
                {
                    sqlLayBaoCao = "SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, O.*, money_khambenh+money_xetnghiem+money_cdhatdcn+money_pttt+money_dvktc+money_giuongthuong+money_giuongyeucau+money_mau+money_thuoc+money_vattu+money_phuthu+money_vanchuyen+money_khac as tien_tong FROM (SELECT (case vpm.loaivienphiid when 1 then 'Ngoại trú' when 0 then 'Nội trú' else '' end) as loaivienphi, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, count(vpm.*) as slbn_tong, sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as money_khambenh, sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as money_xetnghiem, sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as money_cdhatdcn, sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as money_pttt, sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as money_dvktc, sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as money_giuongthuong,sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as money_giuongyeucau,sum(vpm.money_mau_bh + vpm.money_mau_vp) as money_mau, sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as money_thuoc, sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as money_vattu, sum(vpm.money_phuthu_bh + vpm.money_phuthu_vp) as money_phuthu, sum(vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as money_vanchuyen, sum(vpm.money_khac_bh + vpm.money_khac_vp) as money_khac, sum(vpm.tam_ung) as tam_ung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 and vpm.duyet_ngayduyet_vp>='" + dateTu + "' and vpm.duyet_ngayduyet_vp<='" + dateDen + "' GROUP BY vpm.loaivienphiid) O;";
                }
                else if (tieuchi == 0 && kieuxem == 1) //Theo khoa ra vien + xem chi tiet tung khoa
                {
                    sqlLayBaoCao = "SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, O.*, money_khambenh+money_xetnghiem+money_cdhatdcn+money_pttt+money_dvktc+money_giuongthuong+money_giuongyeucau+money_mau+money_thuoc+money_vattu+money_phuthu+money_vanchuyen+money_khac as tien_tong FROM (SELECT depg.departmentgroupname as loaivienphi, A.*  FROM departmentgroup depg  LEFT JOIN (SELECT vpm.departmentgroupid as departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, count(vpm.*) as slbn_tong, sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as money_khambenh, sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as money_xetnghiem, sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as money_cdhatdcn, sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as money_pttt, sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as money_dvktc, sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as money_giuongthuong,sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as money_giuongyeucau, sum(vpm.money_mau_bh + vpm.money_mau_vp) as money_mau, sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as money_thuoc, sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as money_vattu, sum(vpm.money_phuthu_bh + vpm.money_phuthu_vp) as money_phuthu, sum(vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as money_vanchuyen, sum(vpm.money_khac_bh + vpm.money_khac_vp) as money_khac, sum(vpm.tam_ung) as tam_ung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 and vpm.duyet_ngayduyet_vp>='" + dateTu + "' and vpm.duyet_ngayduyet_vp<='" + dateDen + "'  GROUP BY vpm.departmentgroupid) A ON depg.departmentgroupid=A.departmentgroupid  WHERE depg.departmentgrouptype in (1,4,11,10,100)  ORDER BY depg.departmentgroupname) O;";
                }
                else if (tieuchi == 1 && kieuxem == 0)//theo khoa chi dinh + xem tong hop
                {
                }
                else if (tieuchi == 1 && kieuxem == 1)//theo khoa chi dinh + xem chi tiet tung khoa
                {
                }
                DataTable DataBaoCao = condb.getDataTable(sqlLayBaoCao);
                HienThiDuLieuBaoCao(DataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void HienThiDuLieuBaoCao(DataTable dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Rows.Count > 0)
                {
                    this.dataBCBTongHopToanVien = dataBC;
                    gridControlDataBNNT.DataSource = this.dataBCBTongHopToanVien;
                }
                else
                {
                    gridControlDataBNNT.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
