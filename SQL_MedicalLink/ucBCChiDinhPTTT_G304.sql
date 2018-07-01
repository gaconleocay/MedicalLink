--ucBCChiDinhPTTT_G304

SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricename) as stt, 
	ser.servicepriceid, 
	vp.patientid, 
	vp.vienphiid, 
	mbp.maubenhphamid, 
	hsba.patientname, 
	degp.departmentgroupname as tenkhoaravien, 
	dep.departmentname as tenphongravien, 
	ser.servicepricecode, 
	ser.servicepricename, 
	ser.servicepricemoney, 
	ser.servicepricemoney_bhyt, 
	ser.soluong, 
	ser.soluong * ser.servicepricemoney as thanhtien, 
	ser.soluong * ser.servicepricemoney_bhyt as thanhtienbhyt, 
	ser.servicepricedate, 
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	vp.vienphidate, 
	vp.vienphidate_ravien, 
	vp.duyet_ngayduyet_vp, 
	vp.duyet_ngayduyet, 
	nv.usercode as mauserchidinh, 
	mbp.userid, 
	nv.username as tenuserchidinh, 
	serf.servicepricegroupcode, 
	(case when ser.billid_thutien<>0 or ser.billid_clbh_thutien<>0 then 'Đã thu tiền' else '' end) as trangthaithutien, 
	bh.bhytcode, 
	serf.bhyt_groupcode, 
	serf.servicegrouptype, 
	ser.chiphidauvao, 
	ser.chiphimaymoc, 
	ser.chiphipttt 
FROM serviceprice ser 
	INNER JOIN vienphi vp ON ser.vienphiid=vp.vienphiid 
	INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=vp.hosobenhanid 
	INNER JOIN departmentgroup degp ON vp.departmentgroupid=degp.departmentgroupid 
	INNER JOIN department dep ON vp.departmentid=dep.departmentid and dep.departmenttype in (2,3,9) 
	INNER JOIN departmentgroup kcd ON ser.departmentgroupid=kcd.departmentgroupid 
	INNER JOIN department pcd ON ser.departmentid=pcd.departmentid and pcd.departmenttype in (2,3,9) 
	INNER JOIN maubenhpham mbp ON mbp.maubenhphamid=ser.maubenhphamid and mbp.maubenhphamgrouptype=4 
	INNER JOIN servicepriceref serf ON ser.servicepricecode=serf.servicepricecode 
	LEFT JOIN bhyt bh ON vp.bhytid=bh.bhytid 
	LEFT JOIN nhompersonnel nv ON nv.userhisid=mbp.userid 
WHERE serf.servicepricegroupcode in (" + dsnhomdv + ") " + tieuchi + " >= '" + datetungay + "' " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + ";










