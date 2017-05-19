--BC BN Noi tru

SELECT
FROM tools_serviceprice_pttt spt
WHERE spt


SELECT
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
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc,
			sum(b.datra) as tam_ung
	from tools_serviceprice_pttt spt 
		left join (select b.vienphiid, b.departmentid, b.departmentgroupid, b.datra
				   from bill b where b.loaiphieuthuid=2 and b.dahuyphieu=0) BILL on BILL.vienphiid=spt.vienphiid and BILL.departmentid=spt.departmentid
	where spt.vienphistatus_vp=1 
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstdepartmentid_tt + " 
	group by spt.departmentgroupid) TT
	
left join 












