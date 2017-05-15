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













--ngay 10/5 co tam ung
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
	b.vienphiid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid " + lstdepartmentid_bill + " 
	where vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphistatus_vp=1 
	group by b.vienphiid) BILL ON BILL.vienphiid=A.vienphiid ; 









