--So CDHA ser 1.1 ngay 26/7/2017
--them thuoc/vt di kem

SELECT
		 ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt,
		hsba.patientcode,
		hsba.patientname,
		A.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) ||
		hc_quocgianame) as diachi,
		(case when hsba.bhytcode <>'' then 'x' else '' end) as COBHYT,
		A.CHANDOAN,
		(KYC.DEPARTMENTGROUPNAME || ' - ' || PYC.DEPARTMENTNAME) AS NOIGUI,
		A.YEUCAU,
		A.KETQUA,
		A.MAUBENHPHAMDATE,
		A.maubenhphamfinishdate,
		NTKQ.USERNAME AS NGUOIDOC,
		(CASE WHEN A.DEPARTMENTID_DES=244 THEN 'X' ELSE '' END) AS PHIM_20X25,
		(CASE WHEN A.DEPARTMENTID_DES IN (245,246) THEN 'X' ELSE '' END) AS PHIM_35X43,
		A.isthuocdikem,
		A.isvattudikem
FROM
	(SELECT mbp.maubenhphamid,
			mbp.hosobenhanid,
			mbp.departmentgroupid,
			mbp.departmentid,
			mbp.departmentid_des,
			mbp.chandoan,
			mbp.usertrakq, 
			mbp.userthuchien,
			mbp.maubenhphamdate,
			mbp.maubenhphamfinishdate,
			se.servicename as yeucau,
			se.servicevalue as ketqua,
			(case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle'))>0 then 'X' end) as isthuocdikem,
		(case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle'))>0 then 'X' end) as isvattudikem
	FROM (select maubenhphamid,hosobenhanid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq, userthuchien,maubenhphamdate,maubenhphamfinishdate from maubenhpham where maubenhphamgrouptype=1 and
	departmentid_des='" + cboPhongThucHien.EditValue.ToString() + "' and " + tieuchi + " between '" + datetungay + "' and '" + datedenngay + "') mbp
		LEFT JOIN (select servicepriceid,servicename,servicevalue,maubenhphamid from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)) se ON se.maubenhphamid=mbp.maubenhphamid
		) A	
	INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=A.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pyc ON pyc.departmentid=A.departmentid
	LEFT JOIN (select userhisid,username from nhompersonnel) ntkq ON ntkq.userhisid=COALESCE(A.usertrakq, A.userthuchien);
	

	










	
	