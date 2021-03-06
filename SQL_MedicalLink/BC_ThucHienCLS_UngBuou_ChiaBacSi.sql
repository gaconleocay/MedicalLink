--Bao cao thuc hien Can lam sang - K.Ung buou - ngay 28/8
-- Dich vu "UB3025094"


SELECT ROW_NUMBER () OVER ("+sapxeptheo+") as stt, 
	A.patientid, 
	A.vienphiid, 
	A.medicalrecordid,
	hsba.patientname, 
	(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NAM, 
	(case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NU, 
	bh.bhytcode,
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
	KCHD.departmentgroupname AS khoachidinh, 
	pcd.departmentname as phongchidinh, 
	pth.departmentname as phongthuchien, 
	A.ngay_chidinh, 
	(case when A.maubenhphamdate_thuchien<>'0001-01-01 00:00:00' then A.maubenhphamdate_thuchien end) as ngay_tiepnhan,
	(case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as ngay_thuchien,
	(case when A.servicetimetrakq<>'0001-01-01 00:00:00' then A.servicetimetrakq else ((case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end)) end) as ngay_thuchien_tp,
	KCD.departmentgroupname AS khoachuyenden, 
	KRV.departmentgroupname AS khoaravien,
	A.chandoan as cd_chidinh,
	A.maubenhphamid,
	A.sophieu,	
	A.thuchienclsid,
	A.servicepriceid,
	A.servicepricecode, 
	A.servicepricename, 
	A.LOAIPTTT_DB, 
	A.LOAIPTTT_L1, 
	A.LOAIPTTT_L2, 
	A.LOAIPTTT_L3, 
	A.LOAIPTTT, 
	A.SOLUONG, 
	A.SERVICEPRICEFEE, 
	A.TYLE, 
	round(cast(A.THUOC_TRONGGOI as numeric),0) AS THUOC_TRONGGOI, 
	round(cast(A.VATTU_TRONGGOI as numeric),0) AS VATTU_TRONGGOI, 
	round(cast(A.chiphikhac as numeric),0) AS chiphikhac, 
	(A.servicepricefee * A.soluong) as thanhtien, 
	round(cast(((A.servicepricefee * A.soluong) - COALESCE(A.THUOC_TRONGGOI,0) - COALESCE(A.VATTU_TRONGGOI,0) - COALESCE(A.chiphikhac,0) -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100))) as numeric),0) as lai, 
	A.mochinh_idbs,
	mc.username AS mochinh_tenbs, 
	(A.MOCHINH_TIEN * (A.TYLE/100)) AS MOCHINH_TIEN, 
	A.gayme_idbs,
	GM.username AS GAYME_TENBS, 
	(A.GAYME_TIEN * (A.TYLE/100)) AS GAYME_TIEN, 
	A.phu1_idbs,
	P1.username AS PHU1_TENBS, 
	(A.PHU1_TIEN * (A.TYLE/100)) AS PHU1_TIEN, 
	A.phu2_idbs,
	P2.username AS PHU2_TENBS, 
	(A.PHU2_TIEN * (A.TYLE/100)) AS PHU2_TIEN, 
	A.giupviec1_idbs,
	GV1.username AS GIUPVIEC1_TENBS, 
	(A.GIUPVIEC1_TIEN * (A.TYLE/100)) AS GIUPVIEC1_TIEN,
	(A.giupviec1nsdd_tien * (A.TYLE/100)) AS giupviec1nsdd_tien, 
	A.giupviec2_idbs,
	GV2.username AS GIUPVIEC2_TENBS, 
	(A.GIUPVIEC2_TIEN * (A.TYLE/100)) AS GIUPVIEC2_TIEN, 
	A.NGAY_VAOVIEN, 
	A.NGAY_RAVIEN, 
	A.NGAY_THANHTOAN,
	COALESCE(ntkq.username,ntkq_cc.username) as nguoitraketqua,
	A.nguoinhapthuchien
FROM (
	SELECT vp.patientid, 
			vp.vienphiid, 
			ser.medicalrecordid,
			vp.hosobenhanid, 
			vp.bhytid, 
			ser.departmentgroupid as khoachidinh, 
			ser.departmentid as phongchidinh, 
			ser.servicepricedate as NGAY_CHIDINH, 
			ser.maubenhphamid, 
			mbp.sophieu,
			mbp.departmentid_des,
			mbp.maubenhphamfinishdate,
			mbp.maubenhphamdate_thuchien,
			se.servicetimetrakq,
			se.serviceusertrakq,
			mbp.usertrakq,
			mbp.chandoan,		
			(select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, 
			(case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, 
			cls.thuchienclsid,
			ser.servicepriceid,
			ser.servicepricecode,
			(case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename,			
			(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as SERVICEPRICEFEE, 
			(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as TYLE, 
			(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as THUOC_TRONGGOI, 
			(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as VATTU_TRONGGOI, 
			(case serf.pttt_loaiid 
				when 1 then 'Phẫu thuật đặc biệt' 
				when 2 then 'Phẫu thuật loại 1' 
				when 3 then 'Phẫu thuật loại 2' 
				when 4 then 'Phẫu thuật loại 3' 
				when 5 then 'Thủ thuật đặc biệt' 
				when 6 then 'Thủ thuật loại 1' 
				when 7 then 'Thủ thuật loại 2' 
				when 8 then 'Thủ thuật loại 3' 
				else '' end) as LOAIPTTT, 
			(case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as LOAIPTTT_DB, 
			(case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as LOAIPTTT_L1, 
			(case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as LOAIPTTT_L2, 
			(case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as LOAIPTTT_L3, 
			ser.soluong as SOLUONG, 
			((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, 
			cls.phauthuatvien as mochinh_idbs, 
			((case serf.pttt_loaiid 
					when 1 then 280000 
					when 2 then 125000 
					when 3 then 65000 
					when 4 then 50000 
					when 5 then 84000 
					when 6 then 37500 
					when 7 then 19500 
					when 8 then 15000 
					else 0 end) * ser.soluong) as MOCHINH_TIEN, 
			cls.bacsigayme as gayme_idbs, 
			((case serf.pttt_loaiid 
				when 1 then 280000 
				when 2 then 125000 
				when 3 then 65000 
				when 4 then 50000 
				else 0 end) * ser.soluong) as GAYME_TIEN, 
			cls.phumo1 as phu1_idbs, 
			((case serf.pttt_loaiid 
				when 1 then 200000 
				when 2 then 90000 
				when 3 then 50000 
				when 4 then 30000 
				when 5 then 60000 
				when 6 then 27000 
				else 0 end) * ser.soluong) as PHU1_TIEN, 
			cls.phumo2 as phu2_idbs, 
			((case serf.pttt_loaiid 
				when 1 then 200000 
				when 2 then 90000 
				when 3 then 0 
				when 4 then 0 
				else 0 end) * ser.soluong) as PHU2_TIEN, 
			cls.phumo3 as giupviec1_idbs, 
			((case serf.pttt_loaiid 
				when 1 then 120000 
				when 2 then 70000 
				when 3 then 30000 
				when 4 then 15000 
				when 5 then 36000
				when 6 then 0	
				when 7 then 9000 
				when 8 then 4500 
				else 0 end) * ser.soluong) as GIUPVIEC1_TIEN, 
		  ((case serf.pttt_loaiid 
				when 6 then 21000
				else 0 end) * ser.soluong) as giupviec1nsdd_tien, 
			cls.phumo4 as giupviec2_idbs, 
			((case serf.pttt_loaiid 
				when 1 then 120000 
				when 2 then 70000 
				when 3 then 30000 
				when 4 then 0 
				else 0 end) * ser.soluong) as GIUPVIEC2_TIEN, 
			vp.vienphidate as ngay_vaovien, 
			(case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, 
			(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan,
		cls.tools_username as nguoinhapthuchien
	FROM (select vienphiid,servicepriceid,departmentgroupid,departmentid,servicepricedate,maubenhphamid,servicepricecode,servicepricename,loaidoituong,medicalrecordid,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where "+serf_servicecode + tieuchi_date_ser+") ser 
	left join (select servicepriceid,thuchienclsid,phauthuatvien,bacsigayme,phumo1,phumo2,phumo3,phumo4,tools_username from thuchiencls) cls on cls.servicepriceid=ser.servicepriceid 
	inner join (select servicepricecode, pttt_loaiid from servicepriceref where "+serf_servicecode +") serf on serf.servicepricecode=ser.servicepricecode 
	inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphistatus_vp,vienphidate_ravien,duyet_ngayduyet_vp from vienphi "+tieuchi_date_vp+") vp on vp.vienphiid=ser.vienphiid 
	INNER JOIN (select maubenhphamid,sophieu,departmentid_des,maubenhphamfinishdate,maubenhphamdate_thuchien,usertrakq,chandoan from maubenhpham where maubenhphamgrouptype in (0,1) "+ tieuchi_date_thuchien + mbp_departmentid+ ") mbp on mbp.maubenhphamid=ser.maubenhphamid
	INNER JOIN (select servicepriceid,servicetimetrakq,serviceusertrakq from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)) se on se.servicepriceid=ser.servicepriceid
	) A
INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=A.hosobenhanid 
INNER JOIN (select bhytid,bhytcode from bhyt) bh on bh.bhytid=A.bhytid 
LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh 
LEFT JOIN (select departmentid, departmentname from department where departmenttype in (2,3,9,6,7)) pcd ON pcd.departmentid=A.phongchidinh 
LEFT JOIN (select departmentid, departmentname from department where departmenttype in (6,7)) pth ON pth.departmentid=A.departmentid_des 
LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden 
LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien 
LEFT JOIN nhompersonnel mc ON mc.userhisid=A.mochinh_idbs 
LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_idbs 
LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_idbs 
LEFT JOIN nhompersonnel p2 ON p2.userhisid=A.phu2_idbs 
LEFT JOIN nhompersonnel gv1 ON gv1.userhisid=A.giupviec1_idbs 
LEFT JOIN nhompersonnel gv2 ON gv2.userhisid=A.giupviec2_idbs
LEFT JOIN nhompersonnel ntkq ON ntkq.usercode=A.serviceusertrakq
LEFT JOIN nhompersonnel ntkq_cc ON ntkq_cc.userhisid=A.usertrakq;