----BC Quan ly tong the khoa - DOANH THU KHOA + GÂY MÊ ngay 11/5


SELECT sum(A.doanhthu_slbn) as doanhthu_slbn, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0)) as numeric),0) as doanhthu_tienkb, 
	round(cast(sum(COALESCE(A.doanhthu_tienxn,0)) as numeric),0) as doanhthu_tienxn, 
	round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthu_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.doanhthu_tienpttt,0)) as numeric),0) as doanhthu_tienpttt, 
	round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0)) as numeric),0) as doanhthu_tiendvktc, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthu_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthu_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tienkhac,0)) as numeric),0) as doanhthu_tienkhac, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu,0)) as numeric),0) as doanhthu_tienvattu, 
	round(cast(sum(COALESCE(A.doanhthu_tienmau,0)) as numeric),0) as doanhthu_tienmau, 
	round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tienthuoc, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tongtien,
	sum(B.doanhthuGM_slbn) as doanhthuGM_slbn, 
	round(cast(sum(COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuGM_tienkb, 
	round(cast(sum(COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuGM_tienxn, 
	round(cast(sum(COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuGM_tiencdhatdcn, 
	round(cast(sum(COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuGM_tienpttt, 
	round(cast(sum(COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuGM_tiendvktc, 
	round(cast(sum(COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuGM_tiengiuongthuong, 
	round(cast(sum(COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuGM_tiengiuongyeucau, 
	round(cast(sum(COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuGM_tienkhac, 
	round(cast(sum(COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuGM_tienvattu, 
	round(cast(sum(COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuGM_tienmau, 
	round(cast(sum(COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuGM_tienthuoc, 
	round(cast(sum(COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc ,0)) as numeric),0) as doanhthuGM_tongtien,
	sum(COALESCE(A.doanhthu_slbn,0) + COALESCE(B.doanhthuGM_slbn,0)) as doanhthuTongGM_slbn, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuTongGM_tienkb, 
	round(cast(sum(COALESCE(A.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuTongGM_tienxn, 
	round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuTongGM_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuTongGM_tienpttt, 
	round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuTongGM_tiendvktc, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuTongGM_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuTongGM_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuTongGM_tienkhac, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuTongGM_tienvattu, 
	round(cast(sum(COALESCE(A.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuTongGM_tienmau, 
	round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuTongGM_tienthuoc, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuTongGM_tongtien
FROM 
	(select 
			spt.departmentgroupid,
			count(spt.*) as doanhthu_slbn,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt, 
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, 
			sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc 
	from tools_serviceprice_pttt spt 
	where spt.vienphistatus_vp=1 
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstdepartmentid_tt + " 
	group by spt.departmentgroupid) A 
LEFT JOIN 	
	(select 
			spt.departmentgroup_huong,
			count(spt.*) as doanhthuGM_slbn,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt, 
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, 
			sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc 
	from tools_serviceprice_pttt spt 
	where spt.vienphistatus_vp=1 
		and spt.departmentid in (34,335,269,285)
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "'
	group by spt.departmentgroup_huong) B ON A.departmentgroupid=B.departmentgroup_huong
; 
	
-------RA VIEN DA THANH TOAN ngay 11/5/2017
SELECT count(A.*) as raviendatt_slbn, 
	round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0) as raviendatt_tienkb, 
	round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0) as raviendatt_tienxn, 
	round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0) as raviendatt_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0) as raviendatt_tienpttt, 
	round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0) as raviendatt_tiendvktc, 
	round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0) as raviendatt_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0) as raviendatt_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0) as raviendatt_tienkhac, 
	round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0) as raviendatt_tienvattu, 
	round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0) as raviendatt_tienmau, 
	round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0) as raviendatt_tienthuoc, 
	round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0) as raviendatt_tongtien
FROM 	
	(select 
			spt.vienphiid,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as raviendatt_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as raviendatt_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as raviendatt_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as raviendatt_tienpttt, 
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as raviendatt_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as raviendatt_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as raviendatt_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as raviendatt_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as raviendatt_tienvattu, 
			sum(spt.money_mau_bh + spt.money_mau_vp) as raviendatt_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as raviendatt_tienthuoc 
	from tools_serviceprice_pttt spt 
	where spt.vienphistatus_vp=1 	
		and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 23:59:59' 
		and spt.phongravien in (92)
	group by spt.vienphiid) A



----------------===========QL tong the khoa - DANG DIEU TRI
SELECT count(vpm.*) as dangdt_slbn, 
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




---====== BN ra vien nhung chua thanh toan  ngay 8/6
SELECT count(A.*) as ravienchuatt_slbn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0)) as numeric),0),0) as ravienchuatt_tienkb,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienxn,0)) as numeric),0),0) as ravienchuatt_tienxn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiencdhatdcn,0)) as numeric),0),0) as ravienchuatt_tiencdhatdcn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienpttt,0)) as numeric),0),0) as ravienchuatt_tienpttt,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiendvktc,0)) as numeric),0),0) as ravienchuatt_tiendvktc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongthuong,0)) as numeric),0),0) as ravienchuatt_tiengiuongthuong,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongyeucau,0)) as numeric),0),0) as ravienchuatt_tiengiuongyeucau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkhac,0)) as numeric),0),0) as ravienchuatt_tienkhac,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu,0)) as numeric),0),0) as ravienchuatt_tienvattu,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienmau,0)) as numeric),0),0) as ravienchuatt_tienmau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienthuoc,0)) as numeric),0),0) as ravienchuatt_tienthuoc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0) + COALESCE(A.ravienchuatt_tienxn,0) + COALESCE(A.ravienchuatt_tiencdhatdcn,0) + COALESCE(A.ravienchuatt_tienpttt,0) + COALESCE(A.ravienchuatt_tiendvktc,0) + COALESCE(A.ravienchuatt_tiengiuongthuong,0) + COALESCE(A.ravienchuatt_tiengiuongyeucau,0) + COALESCE(A.ravienchuatt_tienkhac,0) + COALESCE(A.ravienchuatt_tienvattu,0) + COALESCE(A.ravienchuatt_tienmau,0) + COALESCE(A.ravienchuatt_tienthuoc,0)) as numeric),0),0) as ravienchuatt_tongtien
FROM (select spt.vienphiid,  
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as ravienchuatt_tienkb,  
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as ravienchuatt_tienxn,  
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as ravienchuatt_tiencdhatdcn,  
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as ravienchuatt_tienpttt,  
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as ravienchuatt_tiendvktc,  
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as ravienchuatt_tiengiuongthuong,  
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as ravienchuatt_tiengiuongyeucau,  
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as ravienchuatt_tienkhac,  
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as ravienchuatt_tienvattu,  
		sum(spt.money_mau_bh + spt.money_mau_vp) as ravienchuatt_tienmau,  
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as ravienchuatt_tienthuoc 
		from tools_serviceprice_pttt spt 
		where COALESCE(spt.vienphistatus_vp,0)=0 
				and spt.vienphistatus<>0
				and spt.vienphidate_ravien >='2016-01-01 00:00:00' and spt.phongravien in (229)
		group by spt.vienphiid) A








