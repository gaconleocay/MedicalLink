--ucDashboardDoanhThuTungKhoa

--ngay 15/5/2018
--Tat ca doi tuong

SELECT COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0),0) as raviendatt_tienkb, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0),0) as raviendatt_tienxn, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0),0) as raviendatt_tiencdhatdcn, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0),0) as raviendatt_tienpttt, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienptttyeucau,0)) as numeric),0),0) as raviendatt_tienptttyeucau, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0),0) as raviendatt_tiendvktc, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0),0) as raviendatt_tiengiuongthuong, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0),0) as raviendatt_tiengiuongyeucau, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0),0) as raviendatt_tienkhac, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0),0) as raviendatt_tienvattu, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0),0) as raviendatt_tienvattu_ttrieng,
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0),0) as raviendatt_tienmau, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0),0) as raviendatt_tienthuoc, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tienptttyeucau,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienvattu_ttrieng,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0),0) as raviendatt_tongtien 
FROM 
(select spt.vienphiid, 
sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as raviendatt_tienkb, 
sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as raviendatt_tienxn, 
sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as raviendatt_tiencdhatdcn, 
sum(spt.money_pttt_bh + spt.money_pttt_vp) as raviendatt_tienpttt, 
sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as raviendatt_tienptttyeucau, 
sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as raviendatt_tiendvktc, 
sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as raviendatt_tiengiuongthuong, 
sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as raviendatt_tiengiuongyeucau, 
sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as raviendatt_tienkhac, 
sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as raviendatt_tienvattu, 
sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as raviendatt_tienvattu_ttrieng, 
sum(spt.money_mau_bh + spt.money_mau_vp) as raviendatt_tienmau, 
sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as raviendatt_tienthuoc 
from ihs_servicespttt spt 
where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' " + lstdepartmentid + _doituongbenhnhanid_spt+_thutienstatus+" group by spt.vienphiid) A;



---ngay 15/6 - - ĐT BHYT + DV BHYT

SELECT COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0),0) as raviendatt_tienkb, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0),0) as raviendatt_tienxn, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0),0) as raviendatt_tiencdhatdcn, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0),0) as raviendatt_tienpttt, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienptttyeucau,0)) as numeric),0),0) as raviendatt_tienptttyeucau, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0),0) as raviendatt_tiendvktc, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0),0) as raviendatt_tiengiuongthuong, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0),0) as raviendatt_tiengiuongyeucau, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0),0) as raviendatt_tienkhac, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0),0) as raviendatt_tienvattu, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0),0) as raviendatt_tienvattu_ttrieng,
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0),0) as raviendatt_tienmau, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0),0) as raviendatt_tienthuoc, 
COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tienptttyeucau,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienvattu_ttrieng,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0),0) as raviendatt_tongtien 
FROM 
(select spt.vienphiid, 
sum(spt.money_khambenh_bh) as raviendatt_tienkb, 
sum(spt.money_xetnghiem_bh) as raviendatt_tienxn, 
sum(spt.money_cdha_bh + spt.money_tdcn_bh) as raviendatt_tiencdhatdcn, 
sum(spt.money_pttt_bh) as raviendatt_tienpttt, 
sum(spt.money_ptttyeucau_bh) as raviendatt_tienptttyeucau, 
sum(spt.money_dvktc_bh) as raviendatt_tiendvktc, 
sum(spt.money_giuongthuong_bh) as raviendatt_tiengiuongthuong, 
sum(spt.money_giuongyeucau_bh) as raviendatt_tiengiuongyeucau, 
sum(spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_phuthu_bh) as raviendatt_tienkhac, 
sum(spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_dkpttt_vattu_bh) as raviendatt_tienvattu, 
sum(spt.money_vattu_ttrieng_bh) as raviendatt_tienvattu_ttrieng, 
sum(spt.money_mau_bh) as raviendatt_tienmau, 
sum(spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh) as raviendatt_tienthuoc 
from ihs_servicespttt spt 
where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' " + lstdepartmentid +_doituongbenhnhanid_spt+_thutienstatus+" group by spt.vienphiid) A;