--So xet nghiem - ngay 20/7/2017


---------------SINH HOA THUONG QUY
SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,A.maubenhphamid) as stt, 
	hsba.patientcode, 
	hsba.patientname, 
	A.   , 
	A.vienphiid,
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
			mbp.vienphiid,
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
	from (select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 "+tieuchi_mbp+") mbp 
		inner join (select maubenhphamid,servicepricecode,
							string_agg((case when upper(servicename)='NA+' then servicevalue end),'') as ketqua_na1,
							string_agg((case when upper(servicename)='CL-' then servicevalue end),'') as ketqua_cl1,
							string_agg((case when upper(servicename)='K+' then servicevalue end),'') as ketqua_k1,
							string_agg((case when upper(servicename)='NA' then servicevalue end),'') as ketqua_na2,
							string_agg((case when upper(servicename)='CL' then servicevalue end),'') as ketqua_cl2,
							string_agg((case when upper(servicename)='K' then servicevalue end),'') as ketqua_k2,
							string_agg((case when upper(servicename) not in ('NA+','CL-','K+','NA','CL','K') then servicevalue end),'') as ketqua
					from service 
					where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)
					group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid
		--inner join (select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid="+_tools_otherlistid+") tsef on tsef.servicepricecode=se.servicepricecode
		inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename
					FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid="+_tools_otherlistid+"')
					AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode
		) A
INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=A.hosobenhanid 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid 
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=A.departmentid 
LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=A.nguoidoc 
LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=A.nguoigui;




---------------NUOC TIEU VA DICH KHAC
SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,mbp.maubenhphamid) as stt, 
	hsba.patientcode, 
	hsba.patientname, 
	mbp.maubenhphamid, 
	mbp.vienphiid,
	(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, 
	(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, 
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
	(case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, 
	mbp.chandoan, 
	(KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, 
	tsef.servicepricename as yeucau, 
	se.ketqua_ubg,
	se.ketqua_bil,
	se.ketqua_ket,
	se.ketqua_pro,
	se.ketqua_nit,
	se.ketqua_leu,
	se.ketqua_glu,
	se.ketqua_sg,
	se.ketqua_ph,
	se.ketqua_blo,
	se.ketqua,
	mbp.maubenhphamdate, 
	(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate,  
	ngd.username AS nguoidoc, 
	ngg.username as nguoigui 
FROM 
	(select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 "+tieuchi_mbp+") mbp 
	inner join (select maubenhphamid,servicepricecode,
							string_agg((case when upper(servicename)='UBG (UROBILINOGEN)' then servicevalue end),'') as ketqua_ubg,
							string_agg((case when upper(servicename)='BIL (BILIRUBIN)' then servicevalue end),'') as ketqua_bil,
							string_agg((case when upper(servicename)='KET (KETONE)' then servicevalue end),'') as ketqua_ket,
							string_agg((case when upper(servicename)='PRO (PROTEIN)' then servicevalue end),'') as ketqua_pro,
							string_agg((case when upper(servicename)='NIT (NITRIT)' then servicevalue end),'') as ketqua_nit,
							string_agg((case when upper(servicename)='LEU (BẠCH CẦU)' then servicevalue end),'') as ketqua_leu,
							string_agg((case when upper(servicename)='GLU (GLUCOSE)' then servicevalue end),'') as ketqua_glu,
							string_agg((case when upper(servicename)='SG (TỶ TRỌNG)' then servicevalue end),'') as ketqua_sg,
							string_agg((case when upper(servicename)='PH' then servicevalue end),'') as ketqua_ph,
							string_agg((case when upper(servicename)='BLO (HỒNG CẦU)' then servicevalue end),'') as ketqua_blo,
							string_agg((case when upper(servicename) not in ('UBG (UROBILINOGEN)','BIL (BILIRUBIN)','KET (KETONE)','PRO (PROTEIN)','NIT (NITRIT)','LEU (BẠCH CẦU)','GLU (GLUCOSE)','SG (TỶ TRỌNG)','PH','BLO (HỒNG CẦU)') then servicevalue end),'') as ketqua
					from service 
					where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)
					group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid
		inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename
					FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid="+_tools_otherlistid+"')
					AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode
	INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=mbp.hosobenhanid 
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=mbp.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=mbp.departmentid 
	LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userthuchien 
	LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=mbp.userid;
 



---------------MIEN DICH
SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,mbp.maubenhphamid) as stt, 
	hsba.patientcode, 
	hsba.patientname, 
	mbp.maubenhphamid, 
	mbp.vienphiid,
	(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, 
	(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, 
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
	(case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, 
	mbp.chandoan, 
	(KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, 
	tsef.servicepricename as yeucau, 
	se.ketqua,
	mbp.maubenhphamdate, 
	(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate,  
	ngd.username AS nguoidoc, 
	ngg.username as nguoigui 
FROM 
	(select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 "+tieuchi_mbp+") mbp 
	inner join (select maubenhphamid,servicepricecode,
					servicevalue as ketqua
				from service 
				where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)
				) se on se.maubenhphamid=mbp.maubenhphamid
		inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename
					FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid="+_tools_otherlistid+"')
					AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode
	INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=mbp.hosobenhanid 
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=mbp.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=mbp.departmentid 
	LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userthuchien 
	LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=mbp.userid;
 

 
 
 
 
 
 
 
---------------KHI MAU
SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,mbp.maubenhphamid) as stt, 
	hsba.patientcode, 
	hsba.patientname, 
	mbp.maubenhphamid, 
	mbp.vienphiid,
	(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, 
	(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, 
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
	(case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, 
	mbp.chandoan, 
	(KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, 
	tsef.servicepricename as yeucau,
	se.ketqua_fio2,
	se.ketqua_ptem,
	se.ketqua_ag,
	se.ketqua_ao2,
	se.ketqua_hco,
	se.ketqua_ctc_b,
	se.ketqua_cto2,
	se.ketqua_bb,
	se.ketqua_bee,
	se.ketqua_be,
	se.ketqua_ctc_p,
	se.ketqua_chc,
	se.ketqua_baro,
	se.ketqua_bili,
	se.ketqua_hhb,
	se.ketqua_met,
	se.ketqua_coh,
	se.ketqua_o2h,
	se.ketqua_so2,
	se.ketqua_thb,
	se.ketqua_cl,
	se.ketqua_ca2,
	se.ketqua_k,
	se.ketqua_na,
	se.ketqua_hct,
	se.ketqua_pco,
	se.ketqua_po2,
	se.ketqua_ph,
	mbp.maubenhphamdate, 
	(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate,  
	ngd.username AS nguoidoc, 
	ngg.username as nguoigui 
FROM 
	(select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 "+tieuchi_mbp+") mbp 
	inner join (select maubenhphamid,servicepricecode,
							string_agg((case when upper(servicename)='FIO2' then servicevalue end),'') as ketqua_fio2,
							string_agg((case when upper(servicename)='PAT.TEMP' then servicevalue end),'') as ketqua_ptem,
							string_agg((case when upper(servicename)='AG' then servicevalue end),'') as ketqua_ag,
							string_agg((case when upper(servicename)='A/AO2' then servicevalue end),'') as ketqua_ao2,
							string_agg((case when upper(servicename)='HCO3 - ST' then servicevalue end),'') as ketqua_hco,
							string_agg((case when upper(servicename)='CTCO2 (B)' then servicevalue end),'') as ketqua_ctc_b,
							string_agg((case when upper(servicename)='CTO2' then servicevalue end),'') as ketqua_cto2,
							string_agg((case when upper(servicename)='BB' then servicevalue end),'') as ketqua_bb,
							string_agg((case when upper(servicename)='BE (ECF)' then servicevalue end),'') as ketqua_bee,
							string_agg((case when upper(servicename)='BE' then servicevalue end),'') as ketqua_be,
							string_agg((case when upper(servicename)='CTCO2 (P)' then servicevalue end),'') as ketqua_ctc_p,
							string_agg((case when upper(servicename)='CHCO3' then servicevalue end),'') as ketqua_chc,
							string_agg((case when upper(servicename)='BARO' then servicevalue end),'') as ketqua_baro,
							string_agg((case when upper(servicename)='BILI' then servicevalue end),'') as ketqua_bili,
							string_agg((case when upper(servicename)='HHB' then servicevalue end),'') as ketqua_hhb,
							string_agg((case when upper(servicename)='METHB' then servicevalue end),'') as ketqua_met,
							string_agg((case when upper(servicename)='COHB' then servicevalue end),'') as ketqua_coh,
							string_agg((case when upper(servicename)='O2HB' then servicevalue end),'') as ketqua_o2h,
							string_agg((case when upper(servicename)='SO2' then servicevalue end),'') as ketqua_so2,
							string_agg((case when upper(servicename)='THB' then servicevalue end),'') as ketqua_thb,
							string_agg((case when upper(servicename)='CL-' then servicevalue end),'') as ketqua_cl,
						    string_agg((case when upper(servicename)='CA2++' then servicevalue end),'') as ketqua_ca2,
							string_agg((case when upper(servicename)='K+' then servicevalue end),'') as ketqua_k,
							string_agg((case when upper(servicename)='NA+' then servicevalue end),'') as ketqua_na,
							string_agg((case when upper(servicename)='HCT' then servicevalue end),'') as ketqua_hct,
							string_agg((case when upper(servicename)='PCO2 (T)' then servicevalue end),'') as ketqua_pco,
							string_agg((case when upper(servicename)='PO2 (T)' then servicevalue end),'') as ketqua_po2,
							string_agg((case when upper(servicename)='PH (T)' then servicevalue end),'') as ketqua_ph
					from service 
					where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)
					group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid
		inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename
					FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid="+_tools_otherlistid+"')
					AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode
		INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=mbp.hosobenhanid 
		LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=mbp.departmentgroupid 
		LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=mbp.departmentid 
		LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userthuchien 
		LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=mbp.userid;

 


 
 
 


	
	
	
	
	
	
	





  
 
  