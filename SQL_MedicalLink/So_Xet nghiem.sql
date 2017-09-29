--So xet nghiem - ngay 20/7/2017


---------------SINH HOA THUONG QUY
SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,A.maubenhphamid) as stt, 
	hsba.patientcode, 
	hsba.patientname, 
	A.maubenhphamid, 
	(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, 
	(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, 
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
	(case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, 
	A.chandoan, 
	(KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, 
	A.yeucau,
	A.ketqua_na1,
	A.ketqua_cl1,
	A.ketqua_k1,
	A.ketqua_na2,
	A.ketqua_cl2,
	A.ketqua_k2,
	A.ketqua,
	A.maubenhphamdate, 
	A.maubenhphamfinishdate, 
	ngd.username AS nguoidoc, 
	ngg.username as nguoigui 
FROM (
	select 
			mbp.maubenhphamid, 
			mbp.hosobenhanid, 
			mbp.departmentgroupid, 
			mbp.departmentid, 
			mbp.departmentid_des, 
			mbp.chandoan, 
			--mbp.usertrakq, 		
			mbp.maubenhphamdate, 
			(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, 
			tsef.servicepricename as yeucau, 
			se.ketqua_na1,
			se.ketqua_cl1,
			se.ketqua_k1,
			se.ketqua_na2,
			se.ketqua_cl2,
			se.ketqua_k2,
			se.ketqua,
			mbp.userthuchien as nguoidoc, 
			mbp.userid as nguoigui
	from (select maubenhphamid,hosobenhanid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 and maubenhphamdate between '2017-01-01 00:00:00' and '2017-01-04 00:00:00') mbp 
		inner join (select maubenhphamid,servicepricecode,
							(case when upper(servicename)='NA+' then servicevalue end) as ketqua_na1,
							(case when upper(servicename)='CL+' then servicevalue end) as ketqua_cl1,
							(case when upper(servicename)='K+' then servicevalue end) as ketqua_k1,
							(case when upper(servicename)='NA' then servicevalue end) as ketqua_na2,
							(case when upper(servicename)='CL' then servicevalue end) as ketqua_cl2,
							(case when upper(servicename)='K' then servicevalue end) as ketqua_k2,
							(case when upper(servicename) not in ('NA+','CL+','K+','NA','CL','K') then servicevalue end) as ketqua
					from service 
					where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)
					group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid
		inner join (select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid="+tools_otherlistid+") tsef on tsef.servicepricecode=se.servicepricecode) A
INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=A.hosobenhanid 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid 
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=A.departmentid 
LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=A.nguoidoc 
LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=A.nguoigui



 
 
 
 
 
 


	
	
	
	
	
	
	





  
 
  