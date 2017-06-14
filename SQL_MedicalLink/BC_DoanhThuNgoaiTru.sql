----------------BC doanh thu phong kham version 2.0
SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupid, O.departmentname) as stt,
		O.*
FROM		
(SELECT 
		de.departmentgroupid,
		de.departmentid, 
		de.departmentcode,
		(degp.departmentgroupcode || '.' || de.departmentname) as departmentname,
		(B.slbn_bh) as slbn_bh,
		(B.slbn_vp) as slbn_vp,
		(COALESCE(B.slbn_bh,0) + COALESCE(B.slbn_vp,0)) as slbn,
		0 as slbn_nhapvien,
		(B.money_khambenh) as money_khambenh,
		(B.money_xetnghiem) as money_xetnghiem,
		(B.money_cdhatdcn) as money_cdhatdcn,
		(B.money_pttt) as money_pttt,
		(B.money_dvktc) as money_dvktc,
		(B.money_mau) as money_mau,
		(B.money_thuoc) as money_thuoc,
		(COALESCE(B.money_vattu,0) + COALESCE(B.money_vtthaythe,0)) as money_vattu,
		(COALESCE(B.money_khac,0) + COALESCE(B.money_giuongthuong,0) + COALESCE(B.money_giuongyeucau,0) + COALESCE(B.money_nuocsoi,0) + COALESCE(B.money_xuatan,0) + COALESCE(B.money_diennuoc,0) + COALESCE(B.money_phuthu,0) + COALESCE(B.money_vanchuyen,0)) as money_khac,
		B.tien_bh as tien_bh,
		B.tien_vp as tien_vp,
		(COALESCE(B.tien_bh,0) + COALESCE(B.tien_vp,0)) as tien_tong
			
FROM department de
	LEFT JOIN
	(SELECT spt.departmentid,
		sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
		sum(case when spt.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi,
		sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan,
		sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc,
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 		
		sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as tien_bh,
		sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as tien_vp		
	FROM tools_serviceprice_pttt spt 
	WHERE spt.vienphistatus_vp=1 
		and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "'
		and spt.departmentid in (select d.departmentid from department d where d.departmenttype=2)
	GROUP BY spt.departmentid) B ON B.departmentid=de.departmentid 
	LEFT JOIN departmentgroup degp ON degp.departmentgroupid=de.departmentgroupid
WHERE de.departmenttype=2) O;
		
		
		


---====theo thoi gian vao vien ngay 8/6

SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupid, 
O.departmentname) as stt, 
O.* FROM 
(SELECT de.departmentgroupid, 
de.departmentid, 
de.departmentcode, 
(degp.departmentgroupcode || '.' || de.departmentname) as departmentname, 
(B.slbn_bh) as slbn_bh, 
(B.slbn_vp) as slbn_vp, 
(COALESCE(B.slbn_bh,0) + COALESCE(B.slbn_vp,0)) as slbn, 
0 as slbn_nhapvien, 
(B.money_khambenh) as money_khambenh, 
(B.money_xetnghiem) as money_xetnghiem, 
(B.money_cdhatdcn) as money_cdhatdcn, 
(B.money_pttt) as money_pttt, 
(B.money_dvktc) as money_dvktc, 
(B.money_mau) as money_mau, 
(B.money_thuoc) as money_thuoc, 
(COALESCE(B.money_vattu,0) + COALESCE(B.money_vtthaythe,0)) as money_vattu, 
(COALESCE(B.money_khac,0) + COALESCE(B.money_giuongthuong,0) + COALESCE(B.money_giuongyeucau,0) + COALESCE(B.money_nuocsoi,0) + COALESCE(B.money_xuatan,0) + COALESCE(B.money_diennuoc,0) + COALESCE(B.money_phuthu,0) + COALESCE(B.money_vanchuyen,0)) as money_khac, 
B.tien_bh as tien_bh, 
B.tien_vp as tien_vp, 
(COALESCE(B.tien_bh,0) + COALESCE(B.tien_vp,0)) as tien_tong 
FROM department de 
LEFT JOIN 
	(SELECT spt.departmentid, 
	sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
	sum(case when spt.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
	sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
	sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
	sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
	sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
	sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
	sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
	sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
	sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
	sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, 
	sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, 
	sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, 
	sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
	sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
	sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
	sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
	sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
	sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
	sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as tien_bh, 
	sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as tien_vp 
	FROM tools_serviceprice_pttt spt 
	WHERE spt.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
		and spt.departmentid in (select d.departmentid from department d where d.departmenttype=2) 
	GROUP BY spt.departmentid) B ON B.departmentid=de.departmentid 
LEFT JOIN departmentgroup degp ON degp.departmentgroupid=de.departmentgroupid
LEFT JOIN 
WHERE de.departmenttype=2) O;

		
		
		
		
		
		
		
		
		
		

	
	
	

-------------------theo thoi gian vao vien ngay 13/5
SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupid, O.departmentname) as stt,
	O.departmentid, 
	O.departmentcode,
	(degp.departmentgroupcode || '.' || O.departmentname) as departmentname,
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
	GROUP BY de.departmentid, de.departmentcode, de.departmentname) O
LEFT JOIN departmentgroup degp ON degp.departmentgroupid=O.departmentgroupid;
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 















		 
		 
		 
		 
		 

		 
		 
		 








