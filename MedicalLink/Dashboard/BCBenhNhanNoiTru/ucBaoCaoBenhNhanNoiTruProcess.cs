﻿using DevExpress.XtraSplashScreen;
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
    public partial class ucBaoCaoBenhNhanNoiTru : UserControl
    {
        private void LayDuLieuBaoCao()
        {
			 SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                //  LayDuLieuBaoCao_ChayMoi(); //todo
                lblThoiGianLayBaoCao.Text = System.DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                thoiGianTu = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string sqlBaoCao = "SELECT ROW_NUMBER () OVER (ORDER BY degp.departmentgroupname) as stt, degp.departmentgroupname, DDT.dangdt_slbn_bh, DDT.dangdt_slbn_vp, DDT.dangdt_slbn, DDT.dangdt_tienthuoc, DDT.dangdt_tienvattu, (coalesce(DDT.dangdt_tienkb,0) + coalesce(DDT.dangdt_tienxn,0) + coalesce(DDT.dangdt_tiencdhatdcn,0) + coalesce(DDT.dangdt_tienpttt,0) + coalesce(DDT.dangdt_tiendvktc,0) + coalesce(DDT.dangdt_tiengiuongthuong,0) + coalesce(DDT.dangdt_tiengiuongyeucau,0) + coalesce(DDT.dangdt_tienkhac,0) + coalesce(DDT.dangdt_tienvattu,0) + coalesce(DDT.dangdt_tienmau,0) + coalesce(DDT.dangdt_tienthuoc,0)) as dangdt_tongtien, DDT.tam_ung as dangdt_tamung, RV.chuatt_slbn_bh, RV.chuatt_slbn_vp, RV.chuatt_slbn, RV.chuatt_tienthuoc, RV.chuatt_tienvattu, (coalesce(RV.chuatt_tienkb,0) + coalesce(RV.chuatt_tienxn,0) + coalesce(RV.chuatt_tiencdhatdcn,0) + coalesce(RV.chuatt_tienpttt,0) + coalesce(RV.chuatt_tiendvktc,0) + coalesce(RV.chuatt_tiengiuongthuong,0) + coalesce(RV.chuatt_tiengiuongyeucau,0) + coalesce(RV.chuatt_tienkhac,0) + coalesce(RV.chuatt_tienvattu,0) + coalesce(RV.chuatt_tienmau,0) + coalesce(RV.chuatt_tienthuoc,0)) as chuatt_tongtien, RV.tam_ung as chuatt_tamung, TT.datt_slbn_bh, TT.datt_slbn_vp, TT.datt_slbn, TT.datt_tienthuoc_bh, TT.datt_tienthuoc_vp, (coalesce(TT.datt_tienthuoc_bh,0) + coalesce(TT.datt_tienthuoc_vp,0)) as datt_tienthuoc, TT.datt_tienvattu, TT.datt_tongtien_bh, TT.datt_tongtien_vp, (coalesce(TT.datt_tongtien_bh,0) + coalesce(TT.datt_tongtien_vp,0)) as datt_tongtien, TT.tam_ung as datt_tamung, case when TT.datt_tongtien_bh<>0 then round(cast((TT.datt_tienthuoc_bh/TT.datt_tongtien_bh) * 100 as numeric) ,2) else 0 end as datt_tyle_thuoc_bh, case when TT.datt_tongtien_vp<>0 then round(cast((TT.datt_tienthuoc_vp/TT.datt_tongtien_vp) * 100 as numeric) ,2) else 0 end as datt_tyle_thuoc_vp, case when (coalesce(TT.datt_tongtien_bh,0) + coalesce(TT.datt_tongtien_vp,0))<>0 then round(cast(((coalesce(TT.datt_tienthuoc_bh,0) + coalesce(TT.datt_tienthuoc_vp,0))/(coalesce(TT.datt_tongtien_bh,0) + coalesce(TT.datt_tongtien_vp,0))) * 100 as numeric) ,2) else 0 end as datt_tyle_thuoc FROM departmentgroup degp left join (SELECT vi.departmentgroupid, vi.dangdt_slbn_bh as dangdt_slbn_bh, vi.dangdt_slbn_vp as dangdt_slbn_vp, vi.dangdt_slbn as dangdt_slbn, VP.dangdt_tienkb as dangdt_tienkb, vp.dangdt_tienxn as dangdt_tienxn, vp.dangdt_tiencdhatdcn as dangdt_tiencdhatdcn, vp.dangdt_tienpttt as dangdt_tienpttt, vp.dangdt_tiendvktc as dangdt_tiendvktc, vp.dangdt_tiengiuongthuong as dangdt_tiengiuongthuong, vp.dangdt_tiengiuongyeucau as dangdt_tiengiuongyeucau, vp.dangdt_tienkhac as dangdt_tienkhac, vp.dangdt_tienvattu as dangdt_tienvattu, vp.dangdt_tienmau as dangdt_tienmau, vp.dangdt_tienthuoc as dangdt_tienthuoc, vi.tam_ung as tam_ung FROM (SELECT V.departmentgroupid, sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as dangdt_slbn_bh, sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as dangdt_slbn_vp, count(V.*) as dangdt_slbn, SUM(V.tam_ung) as tam_ung FROM (SELECT sum(b.datra) as tam_ung, vp.departmentgroupid, vp.vienphiid, vp.doituongbenhnhanid FROM vienphi vp LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 WHERE vp.vienphistatus=0 and vp.vienphidate>='" + GlobalStore.KhoangThoiGianLayDuLieu + "' GROUP BY vp.vienphiid, vp.departmentgroupid) V GROUP BY V.departmentgroupid) VI left join (select spt.khoaravien, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as dangdt_tienkb, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as dangdt_tienxn, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as dangdt_tiencdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as dangdt_tienpttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as dangdt_tiendvktc, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as dangdt_tiengiuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as dangdt_tiengiuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as dangdt_tienkhac, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as dangdt_tienvattu, sum(spt.money_mau_bh + spt.money_mau_vp) as dangdt_tienmau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as dangdt_tienthuoc from view_tools_serviceprice_pttt spt where spt.vienphistatus=0 and spt.vienphidate>='" + GlobalStore.KhoangThoiGianLayDuLieu + "' group by spt.khoaravien) VP on vp.khoaravien=vi.departmentgroupid ) DDT on DDT.departmentgroupid=degp.departmentgroupid left join (SELECT vi.departmentgroupid, vi.chuatt_slbn_bh as chuatt_slbn_bh, vi.chuatt_slbn_vp as chuatt_slbn_vp, vi.chuatt_slbn as chuatt_slbn, VP.chuatt_tienkb as chuatt_tienkb, vp.chuatt_tienxn as chuatt_tienxn, vp.chuatt_tiencdhatdcn as chuatt_tiencdhatdcn, vp.chuatt_tienpttt as chuatt_tienpttt, vp.chuatt_tiendvktc as chuatt_tiendvktc, vp.chuatt_tiengiuongthuong as chuatt_tiengiuongthuong, vp.chuatt_tiengiuongyeucau as chuatt_tiengiuongyeucau, vp.chuatt_tienkhac as chuatt_tienkhac, vp.chuatt_tienvattu as chuatt_tienvattu, vp.chuatt_tienmau as chuatt_tienmau, vp.chuatt_tienthuoc as chuatt_tienthuoc, vi.tam_ung as tam_ung FROM (SELECT V.departmentgroupid, sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as chuatt_slbn_bh, sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as chuatt_slbn_vp, count(V.*) as chuatt_slbn, SUM(V.tam_ung) as tam_ung FROM (SELECT sum(b.datra) as tam_ung, vp.departmentgroupid, vp.vienphiid, vp.doituongbenhnhanid FROM vienphi vp LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 WHERE coalesce(vp.vienphistatus_vp,0)=0 and vp.vienphistatus=1 and vp.vienphidate_ravien>='" + GlobalStore.KhoangThoiGianLayDuLieu + "' GROUP BY vp.vienphiid, vp.departmentgroupid) V GROUP BY V.departmentgroupid) VI left join (select spt.khoaravien, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as chuatt_tienkb, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as chuatt_tienxn, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as chuatt_tiencdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as chuatt_tienpttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as chuatt_tiendvktc, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as chuatt_tiengiuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as chuatt_tiengiuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as chuatt_tienkhac, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as chuatt_tienvattu, sum(spt.money_mau_bh + spt.money_mau_vp) as chuatt_tienmau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as chuatt_tienthuoc from ihs_servicespttt spt where coalesce(spt.vienphistatus_vp,0)=0 and spt.vienphistatus=1 and spt.vienphidate_ravien>='" + GlobalStore.KhoangThoiGianLayDuLieu + "' group by spt.khoaravien) VP on vp.khoaravien=vi.departmentgroupid ) RV on RV.departmentgroupid=degp.departmentgroupid left join (SELECT VI.departmentgroupid, VI.datt_slbn_bh as datt_slbn_bh, VI.datt_slbn_vp as datt_slbn_vp, VI.datt_slbn as datt_slbn, vp.datt_tienkb as datt_tienkb, vp.datt_tienxn as datt_tienxn, vp.datt_tiencdhatdcn as datt_tiencdhatdcn, vp.datt_tienpttt as datt_tienpttt, vp.datt_tiendvktc as datt_tiendvktc, vp.datt_tiengiuongthuong as datt_tiengiuongthuong, vp.datt_tiengiuongyeucau as datt_tiengiuongyeucau, vp.datt_tienkhac as datt_tienkhac, vp.datt_tienvattu as datt_tienvattu, vp.datt_tienmau as datt_tienmau, vp.datt_tienthuoc_bh as datt_tienthuoc_bh, vp.datt_tienthuoc_vp as datt_tienthuoc_vp, vp.datt_tongtien_bh as datt_tongtien_bh, vp.datt_tongtien_vp as datt_tongtien_vp, bill.tam_ung as tam_ung FROM (select v.departmentgroupid, sum(case when v.doituongbenhnhanid=1 then 1 else 0 end) as datt_slbn_bh, sum(case when v.doituongbenhnhanid<>1 then 1 else 0 end) as datt_slbn_vp, count(v.*) as datt_slbn from vienphi v where v.vienphistatus_vp=1 and v.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' group by v.departmentgroupid) VI left join (select spt.khoaravien, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as datt_tienkb, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as datt_tienxn, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as datt_tiencdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as datt_tienpttt, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as datt_tiendvktc, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as datt_tiengiuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as datt_tiengiuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as datt_tienkhac, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as datt_tienvattu, sum(spt.money_mau_bh + spt.money_mau_vp) as datt_tienmau, sum(spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh) as datt_tienthuoc_bh, sum(spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp) as datt_tienthuoc_vp, sum(spt.money_khambenh_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_phuthu_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_dkpttt_vattu_bh + spt.money_mau_bh + spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh) as datt_tongtien_bh, sum(spt.money_khambenh_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_phuthu_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_dkpttt_vattu_vp + spt.money_mau_vp + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp) as datt_tongtien_vp from ihs_servicespttt spt where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' group by spt.khoaravien) VP on vp.khoaravien=vi.departmentgroupid left join (select vp.departmentgroupid, sum(b.datra) as tam_ung from bill b inner join (select vienphiid, departmentgroupid from vienphi where vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "') vp on vp.vienphiid=b.vienphiid where b.loaiphieuthuid=2 and b.dahuyphieu=0 group by vp.departmentgroupid ) BILL on BILL.departmentgroupid=vi.departmentgroupid ) TT on TT.departmentgroupid=degp.departmentgroupid WHERE degp.departmentgrouptype in (1,4,11,10,100); ";
                DataView dataBCTongTheKhoa = new DataView(condb.GetDataTable_HIS(sqlBaoCao));
                HienThiDuLieuBaoCao(dataBCTongTheKhoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
			SplashScreenManager.CloseForm();
        }
        private void HienThiDuLieuBaoCao(DataView dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Count > 0)
                {
                    dataBCTongTheKhoa = new DataView();
                    dataBCTongTheKhoa = dataBC;
                    gridControlDataBNNT.DataSource = dataBCTongTheKhoa;
                }
                else
                {
                    dataBCTongTheKhoa = null;
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
