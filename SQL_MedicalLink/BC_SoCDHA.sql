--So CDHA ser 1.1 ngay 13/4/2017

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
		(CASE WHEN A.DEPARTMENTID_DES IN (245,246) THEN 'X' ELSE '' END) AS PHIM_35X43

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
			se.servicevalue as ketqua
	FROM maubenhpham mbp
		LEFT JOIN service se ON se.maubenhphamid=mbp.maubenhphamid
	WHERE se.servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) and mbp.maubenhphamgrouptype=1 and
	mbp.departmentid_des='" + cboPhongThucHien.EditValue.ToString() + "' and " + tieuchi + ">='" + datetungay + "' and " + tieuchi + "<='" + datedenngay + "' 
		) A	
	INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=A.hosobenhanid
	LEFT JOIN departmentgroup kyc ON kyc.departmentgroupid=A.departmentgroupid
	LEFT JOIN department pyc ON pyc.departmentid=A.departmentid
	LEFT JOIN tools_tblnhanvien ntkq ON ntkq.userhisid=COALESCE(A.usertrakq, A.userthuchien);
	
-----
and se.servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)	
	
	
	