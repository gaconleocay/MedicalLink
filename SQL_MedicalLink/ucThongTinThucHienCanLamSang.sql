--ucThongTinThucHienCanLamSang

--ngay 14/12/18


select row_number () over (order by  mbp.maubenhphamstatus,mbp.maubenhphamdate) as stt,
	mbp.hosobenhanid, 
	mbp.maubenhphamid, 
	mbp.maubenhphamstatus, 
	mbp.sothutunumber, 
	mbp.sophieu as maubenhphamcode, 
	mbp.patientpid as patientcode, 
	mbp.patientid, 
	mbp.vienphiid, 
	hsba.patientname, 
	'' as khan, 
	degp.departmentgroupname, 
	de.departmentname, 
	mbp.maubenhphamdate, 
	ncd.username as bacsichidinh, 
	mbp.chandoan, 
	(case when mbp.maubenhphamdate_thuchien<>'0001-01-01 00:00:00' then mbp.maubenhphamdate_thuchien end) as thoigiannhanphieu, 
	(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as thoigiantraphieu 
FROM (select * from maubenhpham where 1=1 {_maubenhphamgrouptype} {_maubenhphamstatus} {_tieuchi_mbp} {_departmentid_des}) mbp 
	inner join (select hosobenhanid,patientname from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid 
	left join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=mbp.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9,7)) de on de.departmentid=mbp.departmentid 
	left join nhompersonnel ncd on ncd.userhisid=mbp.userid;