using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess.FilterDTO;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucBCTongHopDoanhThuKhoa : UserControl
    {
        private DangDTRaVienChuaDaTTFilterDTO filter { get; set; }
        private void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string sqllaybaocao = "SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, O.*, bill.tam_ung as tam_ung FROM (SELECT dep.departmentgroupid, dep.departmentgroupname, sum(A.soluot) as soluot, COALESCE(sum(A.money_khambenh),0)+COALESCE(sum(B.money_khambenh),0) as money_khambenh, sum(A.money_xetnghiem) as money_xetnghiem, sum(A.money_cdhatdcn) as money_cdhatdcn, sum(A.money_pttt) as money_pttt, sum(A.money_dvktc) as money_dvktc, COALESCE(sum(A.money_giuongthuong),0)+COALESCE(sum(B.money_giuongthuong),0) as money_giuongthuong, COALESCE(sum(A.money_giuongyeucau),0)+COALESCE(sum(B.money_giuongyeucau),0) as money_giuongyeucau, COALESCE(sum(A.money_mau),0)+COALESCE(sum(B.money_mau),0) as money_mau, sum(A.money_thuoc) as money_thuoc, sum(A.money_vattu) as money_vattu, sum(A.money_vtthaythe) as money_vtthaythe, COALESCE(sum(A.money_phuthu),0)+COALESCE(sum(B.money_phuthu),0) as money_phuthu, COALESCE(sum(A.money_vanchuyen),0)+COALESCE(sum(B.money_vanchuyen),0) as money_vanchuyen, COALESCE(sum(A.money_khac),0)+COALESCE(sum(B.money_khac),0) as money_khac, COALESCE(sum(A.money_hpngaygiuong),0)+COALESCE(sum(B.money_hpngaygiuong),0) as money_hpngaygiuong, COALESCE(sum(A.money_hppttt),0)+COALESCE(sum(B.money_hppttt),0) as money_hppttt, COALESCE(sum(A.money_chiphikhac),0)+COALESCE(sum(B.money_chiphikhac),0) as money_chiphikhac, sum(B.money_pttt) as gmht_money_pttt, sum(B.money_thuoc) as gmht_money_thuoc, sum(B.money_vattu) as gmht_money_vattu, sum(B.money_vtthaythe) as gmht_money_vtthaythe, sum(B.money_cls) as gmht_money_cls, COALESCE(sum(A.money_khambenh),0) + COALESCE(sum(B.money_khambenh),0) + COALESCE(sum(A.money_xetnghiem),0) + COALESCE(sum(A.money_cdhatdcn),0) + COALESCE(sum(A.money_pttt),0) + COALESCE(sum(A.money_dvktc),0) + COALESCE(sum(A.money_giuongthuong),0) + COALESCE(sum(B.money_giuongthuong),0) + COALESCE(sum(A.money_giuongyeucau),0) + COALESCE(sum(B.money_giuongyeucau),0) + COALESCE(sum(A.money_mau),0) + COALESCE(sum(B.money_mau),0) + COALESCE(sum(A.money_thuoc),0) + COALESCE(sum(A.money_vattu),0) + COALESCE(sum(A.money_vtthaythe),0) + COALESCE(sum(A.money_phuthu),0) + COALESCE(sum(B.money_phuthu),0) + COALESCE(sum(A.money_vanchuyen),0) + COALESCE(sum(B.money_vanchuyen),0) + COALESCE(sum(A.money_khac),0) + COALESCE(sum(B.money_khac),0) + COALESCE(sum(B.money_pttt),0) + COALESCE(sum(B.money_thuoc),0) + COALESCE(sum(B.money_vattu),0) + COALESCE(sum(B.money_vtthaythe),0) + COALESCE(sum(B.money_cls),0) as tong_tien FROM tools_depatment dep FULL JOIN (SELECT spt.departmentid, count(spt.*) as soluot, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, sum(spt.money_hpngaygiuong) as money_hpngaygiuong, sum(spt.money_hppttt) as money_hppttt, sum(spt.money_chiphikhac) as money_chiphikhac FROM serviceprice_pttt spt WHERE spt.departmentid not in (34,335,269,285) and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp >='" + thoiGianTu + "' and spt.duyet_ngayduyet_vp <='" + thoiGianDen + "' GROUP BY spt.departmentid) A ON dep.departmentid=A.departmentid FULL JOIN (SELECT spt.departmentid_huong, sum(spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp) as money_pttt, sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cls, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, sum(spt.money_hpngaygiuong) as money_hpngaygiuong, sum(spt.money_hppttt) as money_hppttt, sum(spt.money_chiphikhac) as money_chiphikhac FROM serviceprice_pttt spt WHERE spt.departmentid in (34,335,269,285) and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp >='" + thoiGianTu + "' and spt.duyet_ngayduyet_vp <='" + thoiGianDen + "' GROUP BY spt.departmentid_huong) B ON dep.departmentid=B.departmentid_huong WHERE dep.departmentgroupid<>21 and dep.departmentgrouptype in (1,4,11,10,100) GROUP BY dep.departmentgroupid, dep.departmentgroupname) O LEFT JOIN (select sum(b.datra) as tam_ung, b.departmentgroupid from vienphi vp inner join bill b on vp.vienphiid=b.vienphiid where vp.duyet_ngayduyet_vp >='" + thoiGianTu + "' and vp.duyet_ngayduyet_vp <='" + thoiGianDen + "'  and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphistatus_vp=1 group by b.departmentgroupid) BILL ON BILL.departmentgroupid=O.departmentgroupid;   ";
                DataTable dataBC = condb.getDataTable(sqllaybaocao);
                HienThiDuLieu(dataBC);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Hien thi du lieu
        private void HienThiDuLieu(DataTable dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Rows.Count > 0)
                {
                    this.dataBaoCao = new DataTable();
                    this.dataBaoCao = dataBC;
                    gridControlTTDTKhoa.DataSource = this.dataBaoCao;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

    }
}
