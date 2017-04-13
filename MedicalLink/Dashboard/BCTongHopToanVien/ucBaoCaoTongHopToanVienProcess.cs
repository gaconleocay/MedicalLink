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
                    sqlLayBaoCao = "SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, O.* FROM (SELECT (case A.loaivienphiid when 1 then 'Ngoại trú' when 0 then 'Nội trú' else '' end) as loaivienphi, (A.tam_ung) as tam_ung, (A.slbn_bh) as slbn_bh, (A.slbn_vp) as slbn_vp, (A.slbn_bh + A.slbn_vp) as slbn_tong, (B.money_khambenh) as money_khambenh, (B.money_xetnghiem) as money_xetnghiem, (B.money_cdhatdcn) as money_cdhatdcn, (B.money_pttt) as money_pttt, (B.money_dvktc) as money_dvktc, (B.money_giuongthuong) as money_giuongthuong, (B.money_giuongyeucau) as money_giuongyeucau, (B.money_mau) as money_mau, (B.money_thuoc) as money_thuoc, (B.money_vattu) as money_vattu, (B.money_vtthaythe) as money_vtthaythe, (B.money_phuthu) as money_phuthu, (B.money_vanchuyen) as money_vanchuyen, (B.money_khac) as money_khac, (B.money_tong) as money_tong FROM (SELECT V.loaivienphiid, sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, count(V.*) as slbn, SUM(V.tam_ung) as tam_ung FROM (SELECT sum(b.datra) as tam_ung, vp.loaivienphiid, vp.vienphiid, vp.doituongbenhnhanid FROM vienphi vp LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + dateTu + "' and vp.duyet_ngayduyet_vp<='" + dateDen + "' GROUP BY vp.vienphiid) V GROUP BY V.loaivienphiid) A LEFT JOIN (SELECT spt.loaivienphiid, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp) as money_tong FROM tools_serviceprice_pttt spt WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp>='" + dateTu + "' and spt.duyet_ngayduyet_vp<='" + dateDen + "' GROUP BY spt.loaivienphiid) B ON A.loaivienphiid=B.loaivienphiid ) O;   ";
                }
                else if (tieuchi == 0 && kieuxem == 1) //Theo khoa ra vien + xem chi tiet tung khoa
                {
                    sqlLayBaoCao = "SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, O.* FROM (SELECT depg.departmentgroupname as loaivienphi, (A.tam_ung) as tam_ung, (A.slbn_bh) as slbn_bh, (A.slbn_vp) as slbn_vp, (A.slbn_bh + A.slbn_vp) as slbn_tong, (B.money_khambenh) as money_khambenh, (B.money_xetnghiem) as money_xetnghiem, (B.money_cdhatdcn) as money_cdhatdcn, (B.money_pttt) as money_pttt, (B.money_dvktc) as money_dvktc, (B.money_giuongthuong) as money_giuongthuong, (B.money_giuongyeucau) as money_giuongyeucau, (B.money_mau) as money_mau, (B.money_thuoc) as money_thuoc, (B.money_vattu) as money_vattu, (B.money_vtthaythe) as money_vtthaythe, (B.money_phuthu) as money_phuthu, (B.money_vanchuyen) as money_vanchuyen, (B.money_khac) as money_khac, (B.money_tong) as money_tong FROM departmentgroup depg LEFT JOIN (SELECT V.departmentgroupid, sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, count(V.*) as slbn, SUM(V.tam_ung) as tam_ung FROM (SELECT sum(b.datra) as tam_ung, vp.departmentgroupid, vp.vienphiid, vp.doituongbenhnhanid FROM vienphi vp LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + dateTu + "' and vp.duyet_ngayduyet_vp<='" + dateDen + "' GROUP BY vp.vienphiid, vp.departmentgroupid) V GROUP BY V.departmentgroupid) A ON A.departmentgroupid=depg.departmentgroupid LEFT JOIN (SELECT spt.khoaravien, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp) as money_tong FROM tools_serviceprice_pttt spt WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp>='" + dateTu + "' and spt.duyet_ngayduyet_vp<='" + dateDen + "' GROUP BY spt.khoaravien) B ON B.khoaravien=depg.departmentgroupid WHERE depg.departmentgrouptype in (1,4,11,10,100)) O; ";
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
