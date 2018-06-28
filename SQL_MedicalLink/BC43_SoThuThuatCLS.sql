--Sổ Phẫu thuật thủ thuật CLS
--ucBC43_SoThuThuatCLS

--Chan doan hinh anh - ngay 15/5

SELECT
		 ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		ser.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) ||
		hc_quocgianame) as diachi,
		(case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT,
		mbp.chandoan as chandoantruocphauthuat,
		mbp.chandoan as chandoansauphauthuat,
		ser.servicepricecode,
		ser.servicepricename,
		ser.servicepricename as phuongphappttt,
		'' as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		ser.servicepricedate,
		(case when se.servicetimetrakq<>'0001-01-01 00:00:00' then se.servicetimetrakq else ((case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end)) end) as phauthuatthuthuatdate,
		(case serf.pttt_loaiid 
			when 1 then 'Phẫu thuật đặc biệt' 
			when 2 then 'Phẫu thuật loại 1' 
			when 3 then 'Phẫu thuật loại 2' 
			when 4 then 'Phẫu thuật loại 3' 
			when 5 then 'Thủ thuật đặc biệt' 
			when 6 then 'Thủ thuật loại 1' 
			when 7 then 'Thủ thuật loại 2' 
			when 8 then 'Thủ thuật loại 3' 
			else '' end) as loaipttt,
		COALESCE(ntkq.username,ntkq_cc.username) as phauthuatvien,
		bsgm.username as bacsigayme
FROM
	(select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') "+_tieuchi_tgchidinh_ser+ " ) ser
inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 "+_tieuchi_tgchidinh_mbp+_tieuchi_tgthuchien_mbp+_phongthuchien+") mbp on mbp.maubenhphamid=ser.maubenhphamid
left join 
	(select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3 from thuchiencls) cls on cls.servicepriceid=ser.servicepriceid
inner join 
	(select servicepriceid,servicetimetrakq,serviceusertrakq from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)) se on se.servicepriceid=ser.servicepriceid	
inner join 
	(select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode
inner join 
	(select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid
inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.vienphiid=ser.vienphiid
left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid 
left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq
left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme
left join nhompersonnel ntkq ON ntkq.usercode=se.serviceusertrakq;
	
	


	
--Xet nghiem ngay 11/4

SELECT
		 ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		ser.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) ||
		hc_quocgianame) as diachi,
		(case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT,
		mbp.chandoan as chandoantruocphauthuat,
		mbp.chandoan as chandoansauphauthuat,
		ser.servicepricecode,
		ser.servicepricename,
		ser.servicepricename as phuongphappttt,
		'' as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		ser.servicepricedate,
		mbp.maubenhphamfinishdate as phauthuatthuthuatdate,
		(case serf.pttt_loaiid 
			when 1 then 'Phẫu thuật đặc biệt' 
			when 2 then 'Phẫu thuật loại 1' 
			when 3 then 'Phẫu thuật loại 2' 
			when 4 then 'Phẫu thuật loại 3' 
			when 5 then 'Thủ thuật đặc biệt' 
			when 6 then 'Thủ thuật loại 1' 
			when 7 then 'Thủ thuật loại 2' 
			when 8 then 'Thủ thuật loại 3' 
			else '' end) as loaipttt,
		ntkq_cc.username as phauthuatvien,
		bsgm.username as bacsigayme
FROM
	(select servicepriceid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') "+_tieuchi_tgchidinh_ser+ " ) ser
inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 "+_tieuchi_tgchidinh_mbp+_tieuchi_tgthuchien_mbp+_phongthuchien+") mbp on mbp.maubenhphamid=ser.maubenhphamid
left join 
	(select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3 from thuchiencls) cls on cls.servicepriceid=ser.servicepriceid
inner join 
	(select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode
inner join 
	(select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid
inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.vienphiid=ser.vienphiid
left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid 
left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq
left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme;
	
	
	
	

