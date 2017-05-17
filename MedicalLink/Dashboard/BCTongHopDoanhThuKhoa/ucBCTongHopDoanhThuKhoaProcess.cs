﻿using DevExpress.XtraSplashScreen;
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
                string doituongbenhnhanid_spt = "";
                string doituongbenhnhanid_vp = "";

                if (chkBHYT.Checked == true && chkVP.Checked == false)
                {
                    doituongbenhnhanid_spt = " and spt.doituongbenhnhanid=1 ";
                    doituongbenhnhanid_vp = " and vp.doituongbenhnhanid=1 ";
                }
                else if (chkBHYT.Checked == false && chkVP.Checked == true)
                {
                    doituongbenhnhanid_spt = " and spt.doituongbenhnhanid<>1 ";
                    doituongbenhnhanid_vp = " and vp.doituongbenhnhanid<>1 ";
                }
                else if (chkBHYT.Checked == true && chkVP.Checked == true)
                {
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    return;
                }

                string sqllaybaocao = "SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, O.*, bill.tam_ung as tam_ung FROM (SELECT dep.departmentgroupid, dep.departmentgroupname, sum(A.soluot) as soluot, sum(C.count_bh) as soluong_bh, sum(C.count_vp) as soluong_vp, sum(C.count) as soluong, sum(A.money_khambenh) as money_khambenh, sum(A.money_xetnghiem) as money_xetnghiem, sum(A.money_cdhatdcn) as money_cdhatdcn, sum(A.money_pttt) as money_pttt, sum(A.money_dvktc) as money_dvktc, sum(A.money_giuongthuong) as money_giuongthuong, sum(A.money_giuongyeucau) as money_giuongyeucau, sum(A.money_nuocsoi) as money_nuocsoi, sum(A.money_xuatan) as money_xuatan, sum(A.money_diennuoc) as money_diennuoc, sum(A.money_mau) as money_mau, sum(A.money_thuoc) as money_thuoc, sum(A.money_vattu) as money_vattu, sum(A.money_vtthaythe) as money_vtthaythe, sum(A.money_phuthu) as money_phuthu, sum(A.money_vanchuyen) as money_vanchuyen, sum(A.money_khac) as money_khac, COALESCE(sum(A.money_hpngaygiuong),0)+COALESCE(sum(B.money_hpngaygiuong),0) as money_hpngaygiuong, COALESCE(sum(A.money_hppttt),0)+COALESCE(sum(B.money_hppttt),0) as money_hppttt, COALESCE(sum(A.money_chiphikhac),0)+COALESCE(sum(B.money_chiphikhac),0) as money_chiphikhac, sum(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, sum(A.money_dkpttt_vattu) as money_dkpttt_vattu, sum(B.money_mau) as gmht_money_mau, sum(B.money_pttt) as gmht_money_pttt, sum(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, sum(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu, sum(B.money_vtthaythe) as gmht_money_vtthaythe, sum(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls, sum(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, sum(B.money_hpdkpttt_gm_vattu) as gmht_money_hpdkpttt_vattu, COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) as tong_tien_bh, COALESCE(sum(A.money_tong_vp),0) + COALESCE(sum(B.money_tong_vp),0) as tong_tien_vp, COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) + COALESCE(sum(A.money_tong_vp),0) + COALESCE(sum(B.money_tong_vp),0) as tong_tien FROM departmentgroup dep LEFT JOIN (SELECT spt.departmentgroupid, count(spt.*) as soluot, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, sum(spt.money_hpngaygiuong) as money_hpngaygiuong, sum(spt.money_hppttt + spt.money_hppttt_goi_thuoc + money_hppttt_goi_vattu) as money_hppttt, sum(spt.money_chiphikhac) as money_chiphikhac, sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp FROM tools_serviceprice_pttt spt WHERE spt.departmentid not in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid LEFT JOIN (SELECT spt.departmentgroup_huong, sum(spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp) as money_pttt, sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cls, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, sum(spt.money_hpngaygiuong) as money_hpngaygiuong, sum(spt.money_hppttt + spt.money_hppttt_goi_thuoc + money_hppttt_goi_vattu) as money_hppttt, sum(spt.money_chiphikhac) as money_chiphikhac, sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, sum(spt.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_gm_thuoc, sum(spt.money_hpdkpttt_gm_vattu) as money_hpdkpttt_gm_vattu, sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp FROM tools_serviceprice_pttt spt WHERE spt.departmentid in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong LEFT JOIN (SELECT count(*) as count, sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, vp.departmentgroupid FROM vienphi vp WHERE vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid WHERE dep.departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100) GROUP BY dep.departmentgroupid, dep.departmentgroupname) O LEFT JOIN (select sum(b.datra) as tam_ung, b.departmentgroupid from vienphi vp inner join bill b on vp.vienphiid=b.vienphiid where vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " group by b.departmentgroupid) BILL ON BILL.departmentgroupid=O.departmentgroupid;  ";
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
