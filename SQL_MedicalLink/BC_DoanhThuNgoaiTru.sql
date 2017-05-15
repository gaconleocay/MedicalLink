--Bao cao Doanh thu ngoai tru ngay 14/5
SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupid, O.departmentname) as stt,
	O.departmentid, 
	O.departmentcode,
	O.departmentname,
	O.slbn_bh,
	O.slbn_vp,
	(COALESCE(O.slbn_bh,0) + COALESCE(O.slbn_vp,0)) AS slbn,
	O.slbn_nhapvien,
	O.money_khambenh,
	O.money_xetnghiem,
	O.money_cdhatdcn,
	O.money_pttt,
	O.money_dvktc,
	O.money_mau,
	O.money_thuoc,
	O.money_vattu,
	O.money_khac,
	O.tien_bh,
	O.tien_vp,
	(COALESCE(O.tien_bh,0) + COALESCE(O.tien_vp,0)) as tien_tong
FROM
	(SELECT 
		de.departmentgroupid,
		de.departmentid, 
		de.departmentcode,
		de.departmentname,
		sum(case when serdep.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
		sum(case when serdep.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
		sum(case when serdep.xutrikhambenhid=4 then 1 else 0 end) as slbn_nhapvien, 
		sum(serdep.money_khambenh_bh+serdep.money_khambenh_vp) as money_khambenh,
		sum(serdep.money_xetnghiem_bh+serdep.money_xetnghiem_vp) as money_xetnghiem,
		sum(serdep.money_cdha_bh+serdep.money_tdcn_bh + serdep.money_cdha_vp+serdep.money_tdcn_vp) as money_cdhatdcn,
		sum(serdep.money_pttt_bh+serdep.money_pttt_vp) as money_pttt,
		sum(serdep.money_dvktc_bh+serdep.money_dvktc_vp) as money_dvktc,
		sum(serdep.money_mau_bh+serdep.money_mau_vp) as money_mau,
		sum(serdep.money_thuoc_bh+serdep.money_thuoc_vp) as money_thuoc,
		sum(serdep.money_vattu_bh+serdep.money_vattu_vp) as money_vattu,
		sum(serdep.money_giuong_bh + serdep.money_giuong_vp + serdep.money_phuthu_bh + serdep.money_phuthu_vp + serdep.money_vanchuyen_bh + serdep.money_vanchuyen_vp + serdep.money_khac_bh + serdep.money_khac_vp ) as money_khac,
		sum(serdep.money_khambenh_bh + serdep.money_xetnghiem_bh + serdep.money_cdha_bh + serdep.money_tdcn_bh + serdep.money_pttt_bh + serdep.money_dvktc_bh + serdep.money_mau_bh + serdep.money_thuoc_bh + serdep.money_vattu_bh + serdep.money_giuong_bh + serdep.money_phuthu_bh + serdep.money_vanchuyen_bh + serdep.money_khac_bh) as tien_bh, 
		sum(serdep.money_khambenh_vp + serdep.money_xetnghiem_vp + serdep.money_cdha_vp + serdep.money_tdcn_vp + serdep.money_pttt_vp + serdep.money_dvktc_vp + serdep.money_mau_vp + serdep.money_thuoc_vp + serdep.money_vattu_vp + serdep.money_giuong_vp + serdep.money_phuthu_vp + serdep.money_vanchuyen_vp + serdep.money_khac_vp) as tien_vp	
	FROM department de
	LEFT JOIN serviceprice_department serdep ON serdep.departmentid=de.departmentid
	INNER JOIN (select vp.vienphiid, vp.duyet_ngayduyet_vp from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') V ON V.vienphiid=serdep.vienphiid
	WHERE de.departmenttype in (2,24)
		  and serdep.loaibenhanid in (24,20) 
		  and serdep.medicalrecordstatus<>0
	GROUP BY de.departmentid, de.departmentcode, de.departmentname) O;


	
	
	
	

-------------------theo thoi gian vao vien ngay 13/5
SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupid, O.departmentname) as stt,
	O.departmentid, 
	O.departmentcode,
	O.departmentname,
	O.slbn_bh,
	O.slbn_vp,
	(COALESCE(O.slbn_bh,0) + COALESCE(O.slbn_vp,0)) AS slbn,
	O.slbn_nhapvien,
	O.money_khambenh,
	O.money_xetnghiem,
	O.money_cdhatdcn,
	O.money_pttt,
	O.money_dvktc,
	O.money_mau,
	O.money_thuoc,
	O.money_vattu,
	O.money_khac,
	O.tien_bh,
	O.tien_vp,
	(COALESCE(O.tien_bh,0) + COALESCE(O.tien_vp,0)) as tien_tong
FROM
	(SELECT 
		de.departmentgroupid,
		de.departmentid, 
		de.departmentcode,
		de.departmentname,
		sum(case when serdep.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
		sum(case when serdep.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
		sum(case when serdep.xutrikhambenhid=4 then 1 else 0 end) as slbn_nhapvien, 
		sum(serdep.money_khambenh_bh+serdep.money_khambenh_vp) as money_khambenh,
		sum(serdep.money_xetnghiem_bh+serdep.money_xetnghiem_vp) as money_xetnghiem,
		sum(serdep.money_cdha_bh+serdep.money_tdcn_bh + serdep.money_cdha_vp+serdep.money_tdcn_vp) as money_cdhatdcn,
		sum(serdep.money_pttt_bh+serdep.money_pttt_vp) as money_pttt,
		sum(serdep.money_dvktc_bh+serdep.money_dvktc_vp) as money_dvktc,
		sum(serdep.money_mau_bh+serdep.money_mau_vp) as money_mau,
		sum(serdep.money_thuoc_bh+serdep.money_thuoc_vp) as money_thuoc,
		sum(serdep.money_vattu_bh+serdep.money_vattu_vp) as money_vattu,
		sum(serdep.money_giuong_bh + serdep.money_giuong_vp + serdep.money_phuthu_bh + serdep.money_phuthu_vp + serdep.money_vanchuyen_bh + serdep.money_vanchuyen_vp + serdep.money_khac_bh + serdep.money_khac_vp ) as money_khac,
		sum(serdep.money_khambenh_bh + serdep.money_xetnghiem_bh + serdep.money_cdha_bh + serdep.money_tdcn_bh + serdep.money_pttt_bh + serdep.money_dvktc_bh + serdep.money_mau_bh + serdep.money_thuoc_bh + serdep.money_vattu_bh + serdep.money_giuong_bh + serdep.money_phuthu_bh + serdep.money_vanchuyen_bh + serdep.money_khac_bh) as tien_bh, 
		sum(serdep.money_khambenh_vp + serdep.money_xetnghiem_vp + serdep.money_cdha_vp + serdep.money_tdcn_vp + serdep.money_pttt_vp + serdep.money_dvktc_vp + serdep.money_mau_vp + serdep.money_thuoc_vp + serdep.money_vattu_vp + serdep.money_giuong_vp + serdep.money_phuthu_vp + serdep.money_vanchuyen_vp + serdep.money_khac_vp) as tien_vp	
	FROM department de
	LEFT JOIN serviceprice_department serdep ON serdep.departmentid=de.departmentid
	WHERE de.departmenttype in (2,24)
		  and serdep.loaibenhanid in (24,20) 
		  and serdep.medicalrecordstatus<>0
		  and serdep.thoigianvaovien between '" + thoiGianTu + "' and '" + thoiGianDen + "'
	GROUP BY de.departmentid, de.departmentcode, de.departmentname) O;	
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 















		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 








