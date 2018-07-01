----------------BC doanh thu phong kham version 2.0
--ucBaoCaoBenhNhanNgoaiTru

---Theo thoi gian thanh toan
--ngay 7/1/2018
	
SELECT ROW_NUMBER () OVER (ORDER BY de.departmentgroupid,de.departmentname) as stt,
		de.departmentgroupid,
		de.departmentid,
		de.departmentcode,
		(degp.departmentgroupcode || '.' || de.departmentname) as departmentname,
		sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
		sum(case when spt.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
		count(spt.*) as slbn,
		0 as slbn_nhapvien,
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vattu, 
		sum(spt.money_khac_bh + spt.money_khac_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_khac,
		sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as tien_bh,
		sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as tien_vp,
		sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as tien_tong	
FROM (select departmentid,departmentgroupid,departmentcode,departmentname from department where departmenttype in (2,9)) de
		LEFT JOIN (select * from ihs_servicespttt where vienphistatus_vp=1 "+_thutienstatus+" and duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') spt on spt.departmentid=de.departmentid
		LEFT JOIN (select departmentgroupid,departmentgroupcode from departmentgroup) degp ON degp.departmentgroupid=de.departmentgroupid
GROUP BY de.departmentgroupid,de.departmentid,de.departmentcode,de.departmentname,degp.departmentgroupcode;






-------------------theo thoi gian vao vien ngay 1/7

SELECT ROW_NUMBER () OVER (ORDER BY de.departmentgroupid,de.departmentname) as stt,
		de.departmentgroupid,
		de.departmentid, 
		de.departmentcode,
		(degp.departmentgroupcode || '.' || de.departmentname) as departmentname,
		de.departmentname,
		sum(case when serdep.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
		sum(case when serdep.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp,
		count(serdep.*) as slbn,
		sum(case when serdep.xutrikhambenhid=4 then 1 else 0 end) as slbn_nhapvien, 
		sum(serdep.money_khambenh_bh+serdep.money_khambenh_vp) as money_khambenh,
		sum(serdep.money_xetnghiem_bh+serdep.money_xetnghiem_vp) as money_xetnghiem,
		sum(serdep.money_cdha_bh+serdep.money_tdcn_bh + serdep.money_cdha_vp+serdep.money_tdcn_vp) as money_cdhatdcn,
		sum(serdep.money_pttt_bh+serdep.money_pttt_vp) as money_pttt,
		sum(serdep.money_dvktc_bh+serdep.money_dvktc_vp) as money_dvktc,
		sum(serdep.money_mau_bh+serdep.money_mau_vp) as money_mau,
		sum(serdep.money_thuoc_bh+serdep.money_thuoc_vp) as money_thuoc,
		sum(serdep.money_vattu_bh+serdep.money_vattu_vp) as money_vattu,
		sum(serdep.money_giuong_bh + serdep.money_giuong_vp + serdep.money_phuthu_bh + serdep.money_phuthu_vp + serdep.money_vanchuyen_bh + serdep.money_vanchuyen_vp + serdep.money_khac_bh + serdep.money_khac_vp) as money_khac,
		sum(serdep.money_khambenh_bh + serdep.money_xetnghiem_bh + serdep.money_cdha_bh + serdep.money_tdcn_bh + serdep.money_pttt_bh + serdep.money_dvktc_bh + serdep.money_mau_bh + serdep.money_thuoc_bh + serdep.money_vattu_bh + serdep.money_giuong_bh + serdep.money_phuthu_bh + serdep.money_vanchuyen_bh + serdep.money_khac_bh) as tien_bh, 
		sum(serdep.money_khambenh_vp + serdep.money_xetnghiem_vp + serdep.money_cdha_vp + serdep.money_tdcn_vp + serdep.money_pttt_vp + serdep.money_dvktc_vp + serdep.money_mau_vp + serdep.money_thuoc_vp + serdep.money_vattu_vp + serdep.money_giuong_vp + serdep.money_phuthu_vp + serdep.money_vanchuyen_vp + serdep.money_khac_vp) as tien_vp,
		sum(serdep.money_khambenh_bh + serdep.money_xetnghiem_bh + serdep.money_cdha_bh + serdep.money_tdcn_bh + serdep.money_pttt_bh + serdep.money_dvktc_bh + serdep.money_mau_bh + serdep.money_thuoc_bh + serdep.money_vattu_bh + serdep.money_giuong_bh + serdep.money_phuthu_bh + serdep.money_vanchuyen_bh + serdep.money_khac_bh + serdep.money_khambenh_vp + serdep.money_xetnghiem_vp + serdep.money_cdha_vp + serdep.money_tdcn_vp + serdep.money_pttt_vp + serdep.money_dvktc_vp + serdep.money_mau_vp + serdep.money_thuoc_vp + serdep.money_vattu_vp + serdep.money_giuong_vp + serdep.money_phuthu_vp + serdep.money_vanchuyen_vp + serdep.money_khac_vp) as tien_tong
		
FROM (select departmentid,departmentgroupid,departmentcode,departmentname from department where departmenttype in (2,9)) de
	LEFT JOIN (select * from serviceprice_department where loaibenhanid in (24,20) and thoigianvaovien between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+") serdep ON serdep.departmentid=de.departmentid
	LEFT JOIN (select departmentgroupid,departmentgroupcode from departmentgroup) degp ON degp.departmentgroupid=de.departmentgroupid
GROUP BY de.departmentgroupid,de.departmentid,de.departmentcode,de.departmentname,degp.departmentgroupcode;
	


		 
		 
		 
		 

/*
---====theo thoi gian vao vien ngay 8/6

SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupid,O.departmentname) as stt, 
O.* 
FROM 
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
	FROM ihs_servicespttt spt 
	WHERE spt.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
		and spt.departmentid in (select d.departmentid from department d where d.departmenttype in (2,9)) 
	GROUP BY spt.departmentid) B ON B.departmentid=de.departmentid 
LEFT JOIN departmentgroup degp ON degp.departmentgroupid=de.departmentgroupid
LEFT JOIN 
WHERE de.departmenttype in (2,9)) O;

		
*/		
		
		
		
		
		
		
		
		

	
	
	

		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 
		 















		 
		 
		 
		 
		 

		 
		 
		 








