----------------===========QL tong the khoa - DANG DIEU TRI
/*SELECT count(vpm.*) as dangdt_slbn, 
COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0),0) as dangdt_tienkb, 
COALESCE(round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0),0) as dangdt_tienxn, 
COALESCE(round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0),0) as dangdt_tiencdhatdcn, 
COALESCE(round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0),0) as dangdt_tienpttt, 
COALESCE(round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0),0) as dangdt_tiendvktc, 
COALESCE(round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0),0) as dangdt_tiengiuongthuong, 
COALESCE(round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0),0) as dangdt_tiengiuongyeucau, 
COALESCE(round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0),0) as dangdt_tienkhac, 
COALESCE(round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0),0) as dangdt_tienvattu, 
COALESCE(round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0),0) as dangdt_tienmau, 
COALESCE(round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0),0) as dangdt_tienthuoc, 
COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0),0) as dangdt_tongtien, 
COALESCE(round(cast(sum(vpm.tam_ung) as numeric),0),0) as dangdt_tamung 
FROM 
	(select hosobenhanid, departmentid from medicalrecord where medicalrecordstatus in (0,2) 
		and thoigianvaovien>='" + this.KhoangThoiGianLayDuLieu + "' " + lstdepartmentid_mrd + " group by hosobenhanid, departmentid) med
	left join vienphi_money vpm on vpm.hosobenhanid=med.hosobenhanid
WHERE vpm.vienphistatus=0 and vpm.vienphidate>='" + this.KhoangThoiGianLayDuLieu + "'
*/




----BC Quan ly tong the khoa - DOANH THU KHOA + GÂY MÊ
-- string sqlDoanhThu
--ngay 25/5/2018
--
SELECT sum(A.doanhthu_slbn) as doanhthu_slbn, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0)) as numeric),0) as doanhthu_tienkb, 
	round(cast(sum(COALESCE(A.doanhthu_tienxn,0)) as numeric),0) as doanhthu_tienxn, 
	round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthu_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.doanhthu_tienpttt,0)) as numeric),0) as doanhthu_tienpttt,
	round(cast(sum(COALESCE(A.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthu_tienptttyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0)) as numeric),0) as doanhthu_tiendvktc, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthu_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthu_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tienkhac,0)) as numeric),0) as doanhthu_tienkhac, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu,0)) as numeric),0) as doanhthu_tienvattu, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthu_tienvattu_ttrieng,
	round(cast(sum(COALESCE(A.doanhthu_tienmau,0)) as numeric),0) as doanhthu_tienmau, 
	round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tienthuoc, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tongtien,
	sum(B.doanhthuGM_slbn) as doanhthuGM_slbn, 
	round(cast(sum(COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuGM_tienkb, 
	round(cast(sum(COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuGM_tienxn, 
	round(cast(sum(COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuGM_tiencdhatdcn, 
	round(cast(sum(COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuGM_tienpttt, 
	round(cast(sum(COALESCE(B.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthuGM_tienptttyeucau,
	round(cast(sum(COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuGM_tiendvktc, 
	round(cast(sum(COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuGM_tiengiuongthuong, 
	round(cast(sum(COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuGM_tiengiuongyeucau, 
	round(cast(sum(COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuGM_tienkhac, 
	round(cast(sum(COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuGM_tienvattu, 
	round(cast(sum(COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuGM_tienvattu_ttrieng,
	round(cast(sum(COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuGM_tienmau, 
	round(cast(sum(COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuGM_tienthuoc, 
	round(cast(sum(COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc ,0)) as numeric),0) as doanhthuGM_tongtien,
	sum(COALESCE(A.doanhthu_slbn,0) + COALESCE(B.doanhthuGM_slbn,0)) as doanhthuTongGM_slbn, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuTongGM_tienkb, 
	round(cast(sum(COALESCE(A.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuTongGM_tienxn, 
	round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuTongGM_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuTongGM_tienpttt,
	round(cast(sum(COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthuTongGM_tienptttyeucau,
	round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuTongGM_tiendvktc, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuTongGM_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuTongGM_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuTongGM_tienkhac, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuTongGM_tienvattu, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuTongGM_tienvattu_ttrieng,
	round(cast(sum(COALESCE(A.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuTongGM_tienmau, 
	round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuTongGM_tienthuoc, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc,0) + COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuTongGM_tongtien
FROM 
	(select 
			spt.departmentgroupid,
			count(spt.*) as doanhthu_slbn,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt,
			sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as doanhthu_tienptttyeucau, 
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, 
			sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as doanhthu_tienvattu_ttrieng,
			sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc 
	from ihs_servicespttt spt 
	where spt.vienphistatus_vp=1 
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstdepartmentid_tt + _thutienstatus+" 
	group by spt.departmentgroupid) A 
LEFT JOIN 	
	(select 
			spt.departmentgroup_huong,
			count(spt.*) as doanhthuGM_slbn,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt, 
			sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as doanhthu_tienptttyeucau,
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, 
			sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as doanhthu_tienvattu_ttrieng,
			sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc 
	from ihs_servicespttt spt 
	where spt.vienphistatus_vp=1 
		and spt.departmentid in (34,335,269,285)
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' "+_thutienstatus+"
	group by spt.departmentgroup_huong) B ON A.departmentgroupid=B.departmentgroup_huong
; 
	
-------RA VIEN DA THANH TOAN ngay 25/5/2018
--string sqlBaoCao_RaVienDaTT =

SELECT count(A.*) as raviendatt_slbn, 
	round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0) as raviendatt_tienkb, 
	round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0) as raviendatt_tienxn, 
	round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0) as raviendatt_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0) as raviendatt_tienpttt,
	round(cast(sum(COALESCE(A.raviendatt_tienptttyeucau,0)) as numeric),0) as raviendatt_tienptttyeucau, 
	round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0) as raviendatt_tiendvktc, 
	round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0) as raviendatt_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0) as raviendatt_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0) as raviendatt_tienkhac, 
	round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0) as raviendatt_tienvattu, 
	round(cast(sum(COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0) as raviendatt_tienvattu_ttrieng, 
	round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0) as raviendatt_tienmau, 
	round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0) as raviendatt_tienthuoc, 
	round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0) + COALESCE(A.raviendatt_tienptttyeucau,0) + COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0) as raviendatt_tongtien
FROM 	
	(select 
			spt.vienphiid,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as raviendatt_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as raviendatt_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as raviendatt_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as raviendatt_tienpttt, 
			sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as raviendatt_tienptttyeucau, 
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as raviendatt_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as raviendatt_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as raviendatt_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as raviendatt_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as raviendatt_tienvattu, 
			sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as raviendatt_tienvattu_ttrieng, 
			sum(spt.money_mau_bh + spt.money_mau_vp) as raviendatt_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as raviendatt_tienthuoc 
	from ihs_servicespttt spt 
	where spt.vienphistatus_vp=1 	
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstphongravien + _thutienstatus+" 
	group by spt.vienphiid) A;






---====== BN ra vien nhung chua thanh toan 
--string sqlBaoCao_RaVienChuaTT 
--ngay 25/5/2018

SELECT count(A.*) as ravienchuatt_slbn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0)) as numeric),0),0) as ravienchuatt_tienkb,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienxn,0)) as numeric),0),0) as ravienchuatt_tienxn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiencdhatdcn,0)) as numeric),0),0) as ravienchuatt_tiencdhatdcn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienpttt,0)) as numeric),0),0) as ravienchuatt_tienpttt,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienptttyeucau,0)) as numeric),0),0) as ravienchuatt_tienptttyeucau,
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiendvktc,0)) as numeric),0),0) as ravienchuatt_tiendvktc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongthuong,0)) as numeric),0),0) as ravienchuatt_tiengiuongthuong,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongyeucau,0)) as numeric),0),0) as ravienchuatt_tiengiuongyeucau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkhac,0)) as numeric),0),0) as ravienchuatt_tienkhac,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu,0)) as numeric),0),0) as ravienchuatt_tienvattu, 
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu_ttrieng,0)) as numeric),0),0) as ravienchuatt_tienvattu_ttrieng, 
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienmau,0)) as numeric),0),0) as ravienchuatt_tienmau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienthuoc,0)) as numeric),0),0) as ravienchuatt_tienthuoc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0) + COALESCE(A.ravienchuatt_tienxn,0) + COALESCE(A.ravienchuatt_tiencdhatdcn,0) + COALESCE(A.ravienchuatt_tienpttt,0) + COALESCE(A.ravienchuatt_tiendvktc,0) + COALESCE(A.ravienchuatt_tiengiuongthuong,0) + COALESCE(A.ravienchuatt_tiengiuongyeucau,0) + COALESCE(A.ravienchuatt_tienkhac,0) + COALESCE(A.ravienchuatt_tienvattu,0) + COALESCE(A.ravienchuatt_tienmau,0) + COALESCE(A.ravienchuatt_tienthuoc,0) + COALESCE(A.ravienchuatt_tienvattu_ttrieng,0) + COALESCE(A.ravienchuatt_tienptttyeucau,0)) as numeric),0),0) as ravienchuatt_tongtien
FROM (select spt.vienphiid,  
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as ravienchuatt_tienkb,  
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as ravienchuatt_tienxn,  
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as ravienchuatt_tiencdhatdcn,  
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as ravienchuatt_tienpttt,  
		sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as ravienchuatt_tienptttyeucau, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as ravienchuatt_tiendvktc,  
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as ravienchuatt_tiengiuongthuong,  
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as ravienchuatt_tiengiuongyeucau,  
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as ravienchuatt_tienkhac,  
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as ravienchuatt_tienvattu,  
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as ravienchuatt_tienvattu_ttrieng,  
		sum(spt.money_mau_bh + spt.money_mau_vp) as ravienchuatt_tienmau,  
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as ravienchuatt_tienthuoc 
		from ihs_servicespttt spt 
		where COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphistatus<>0 and spt.vienphidate_ravien between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstphongravien + _thutienstatus+" group by spt.vienphiid) A ;
		-->='" + this.KhoangThoiGianLayDuLieu + "' 


---====== BN dang dieu tri
--string sqlBaoCao_DangDT 
--ngay 25/3/2018


SELECT count(*) as dangdt_slbn, 
sum(vpm.money_khambenh) as dangdt_tienkb, 
sum(vpm.money_xetnghiem) as dangdt_tienxn, 
sum(coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0)) as dangdt_tiencdhatdcn, 
sum(vpm.money_pttt) as dangdt_tienpttt, 
sum(vpm.money_ptttyeucau) as dangdt_tienptttyeucau,
sum(vpm.money_dvktc) as dangdt_tiendvktc, 
sum(vpm.money_giuongthuong) as dangdt_tiengiuongthuong, 
sum(vpm.money_giuongyeucau) as dangdt_tiengiuongyeucau, 
sum(coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0)) as dangdt_tienkhac,  
sum(vpm.money_vattu) as dangdt_tienvattu, 
sum(vpm.money_vattu_ttrieng) as dangdt_tienvattu_ttrieng, 
sum(vpm.money_mau) as dangdt_tienmau, 
sum(vpm.money_thuoc) as dangdt_tienthuoc, 
sum(coalesce(vpm.money_khambenh,0) + coalesce(vpm.money_xetnghiem,0) + coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0) + coalesce(vpm.money_pttt,0) + coalesce(vpm.money_dvktc,0) + coalesce(vpm.money_giuongthuong,0) + coalesce(vpm.money_giuongyeucau,0) + coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0) + coalesce(vpm.money_vattu,0) + coalesce(vpm.money_mau,0) + coalesce(vpm.money_thuoc,0) + coalesce(vpm.money_ptttyeucau,0) + coalesce(vpm.money_vattu_ttrieng,0)) as dangdt_tongtien,
sum(vpm.tam_ung) as dangdt_tamung 
FROM
(
SELECT mrd.vienphiid, 
mrd.patientid, 
mrd.hosobenhanid, 
mrd.loaibenhanid,  
mrd.departmentgroupid, 
mrd.departmentid, 
mrd.doituongbenhnhanid, 
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong=0
					then (case when mrd.loaibenhanid=24 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
									then ser.servicepricemoney_bhyt*ser.soluong
								when mrd.loaibenhanid=1 
									then ser.servicepricemoney_bhyt*ser.soluong
								else 0 end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
							   else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
							   else ser.servicepricemoney*ser.soluong end)			   
				else 0 end) as money_khambenh,		
		sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong=0
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney*ser.soluong end)	
				else 0 end) as money_xetnghiem,
		sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_cdha,	 
		sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)		
				else 0 end) as money_tdcn,	 	 
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 
								then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney 
								else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_pttt,
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 
								then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney 
								else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_ptttyeucau, 
		sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)	
				else 0 end) as money_mau,
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)	
				else 0 end) as money_giuongthuong,	
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)			
				else 0 end) as money_giuongyeucau,	
		sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_vanchuyen, 	 
		sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_khac, 	 
		sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_phuthu,	 
		(sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) 
				then ser.servicepricemoney_bhyt*ser.soluong else 0 end) 
		+ sum(case when ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=mrd.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
				then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan * ser.soluong) end)
				else 0 end)		
		+ sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end)) as money_dvktc, 	
		sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)		
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)	
				else 0 end) as money_thuoc,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)		
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)		
				else 0 end) as money_vattu,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
				else 0 end) as money_vattu_ttrieng,
(select sum(bill.datra) from bill where bill.vienphiid=mrd.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung		
FROM medicalrecord mrd 
	left join serviceprice ser on mrd.vienphiid=ser.vienphiid and ser.thuockhobanle=0 
WHERE mrd.thoigianvaovien >='" + this.KhoangThoiGianLayDuLieu + "' " + lstdepartmentid_mrd + " and mrd.medicalrecordstatus=2
GROUP BY mrd.vienphiid,mrd.patientid,mrd.hosobenhanid,mrd.loaibenhanid,mrd.departmentgroupid,mrd.departmentid,mrd.doituongbenhnhanid
) VPM;












