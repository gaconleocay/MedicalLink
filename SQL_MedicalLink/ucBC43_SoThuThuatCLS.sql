--Sổ Phẫu thuật thủ thuật CLS
--ucBC43_SoThuThuatCLS

--Chan doan hinh anh - ngay 30/12/18

SELECT ROW_NUMBER () OVER (ORDER BY A.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		A.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
		(case when A.doituongbenhnhanid=1 then 'x' end) as cobhyt,
		A.chandoan as chandoantruocphauthuat,
		A.chandoan as chandoansauphauthuat,
		A.servicepricecode,
		A.servicepricename,
		A.servicepricename as phuongphappttt,
		(case A.ppvocamid
			when 1 then 'Gây mê tĩnh mạch'
			when 2 then 'Gây mê nội khí quản'
			when 3 then 'Gây tê tại chỗ'
			when 4 then 'Tiền mê + gây tê tại chỗ'
			when 5 then 'Gây tê tủy sống'
			when 6 then 'Gây tê'
			when 7 then 'Gây tê màng ngoài cứng'
			when 8 then 'Gây tê đám rối thần kinh'
			when 9 then 'Gây tê Codan'
			when 10 then 'Gây tê nhãn cầu'
			when 11 then 'Gây tê cạnh sống'
			when 12 then 'Gây tê hậu nhãn cầu'
			when 99 then 'Khác'
			end) as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		A.servicepricedate,
		(case when A.servicetimetrakq is not null then A.servicetimetrakq else ((case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end)) end) as phauthuatthuthuatdate,
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
	(select * from 
		(select vp.vienphiid,
		vp.doituongbenhnhanid,
		ser.hosobenhanid,
		ser.maubenhphamid,
		ser.servicepricedate,
		ser.servicepricecode,
		ser.servicepricename,
		ser.departmentgroupid,
		ser.departmentid,
		mbp.maubenhphamfinishdate,
		mbp.chandoan,
		mbp.usertrakq as usertrakq_cc,
		cls.phauthuatvien,
		cls.bacsigayme,
		cls.ppvocamid,
		(case when pacs.readingdate is not null then pacs.readingdate else se.servicetimetrakq end) as servicetimetrakq,
		(case when pacs.readingdoctor1 is not null then pacs.readingdoctor1 else se.serviceusertrakq end) as serviceusertrakq
	from 
		(select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') {_tieuchi_date_ser}) ser
		inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 {_tieuchi_date_mbp} {_phongthuchien}) mbp on mbp.maubenhphamid=ser.maubenhphamid
		{_join_thuchiencls} (select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3,ppvocamid from thuchiencls where 1=1 {_tieuchi_date_thuchien}) cls on cls.servicepriceid=ser.servicepriceid
		inner join (select servicepriceid,servicename,servicevalue,maubenhphamid,servicetimetrakq,serviceusertrakq,servicecode from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) {_tieuchi_se}) se on se.maubenhphamid=mbp.maubenhphamid
		left join (select accessnumber,service_code,to_timestamp(readingdate,'yyyyMMddHH24MIss') at time zone 'utc' as readingdate,readingdoctor1,readingdr1name from resresulttab where {_tieuchi_pacs}) pacs ON pacs.accessnumber=mbp.maubenhphamid::text and pacs.service_code=se.servicecode
		inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_date_vp}) vp on vp.vienphiid=ser.vienphiid) tmp where 1=1 {_tieuchi_trakqtp}) A
	inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') and pttt_loaiid>0) serf on serf.servicepricecode=A.servicepricecode
	inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=A.hosobenhanid	
	left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=A.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=A.departmentid 
	left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=A.usertrakq_cc
	left join nhompersonnel bsgm on bsgm.userhisid=A.bacsigayme
	left join nhompersonnel ntkq ON ntkq.usercode=A.serviceusertrakq;
		
		
		
-----------OLD 		
/*
SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		ser.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
		(case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT,
		mbp.chandoan as chandoantruocphauthuat,
		mbp.chandoan as chandoansauphauthuat,
		ser.servicepricecode,
		ser.servicepricename,
		ser.servicepricename as phuongphappttt,
		(case cls.ppvocamid
			when 1 then 'Gây mê tĩnh mạch'
			when 2 then 'Gây mê nội khí quản'
			when 3 then 'Gây tê tại chỗ'
			when 4 then 'Tiền mê + gây tê tại chỗ'
			when 5 then 'Gây tê tủy sống'
			when 6 then 'Gây tê'
			when 7 then 'Gây tê màng ngoài cứng'
			when 8 then 'Gây tê đám rối thần kinh'
			when 9 then 'Gây tê Codan'
			when 10 then 'Gây tê nhãn cầu'
			when 11 then 'Gây tê cạnh sống'
			when 12 then 'Gây tê hậu nhãn cầu'
			when 99 then 'Khác'
			end) as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		ser.servicepricedate,
		--(case when se.servicetimetrakq<>'0001-01-01 00:00:00' then se.servicetimetrakq else ((case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end)) end) as phauthuatthuthuatdate,
		(case when pacs.readingdate is not null then pacs.readingdate else se.servicetimetrakq end)
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
	(select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') {_tieuchi_date_ser}) ser
	inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 {_tieuchi_date_mbp} {_phongthuchien}) mbp on mbp.maubenhphamid=ser.maubenhphamid
	{_join_thuchiencls}
	(select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3,ppvocamid from thuchiencls where 1=1 {_tieuchi_date_thuchien}) cls on cls.servicepriceid=ser.servicepriceid
	--inner join (select servicepriceid,servicetimetrakq,serviceusertrakq from service where servicedate>'2017-01-01 00:00:00' and servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) {_tieuchi_se}) se on se.servicepriceid=ser.servicepriceid
	inner join (select servicepriceid,servicename,servicevalue,maubenhphamid,servicetimetrakq,serviceusertrakq,servicecode from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) {_tieuchi_se}) se on se.maubenhphamid=mbp.maubenhphamid
	left join (select accessnumber,service_code,to_timestamp(readingdate,'yyyyMMddHH24MIss') as readingdate,readingdoctor1,readingdr1name from resresulttab where 1=1 {_tieuchi_pacs}) pacs ON pacs.accessnumber=mbp.maubenhphamid::text and pacs.service_code=se.servicecode	
	inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode
	inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=ser.hosobenhanid
	inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_date_vp} ) vp on vp.vienphiid=ser.vienphiid
	left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid 
	left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq
	left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme
	left join nhompersonnel ntkq ON ntkq.usercode=se.serviceusertrakq
WHERE 1=1 {_tieuchi_trakqtp}
		
*/		


	
--Xet nghiem  - ngay 12/12/18

SELECT ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		ser.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
		(case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT,
		mbp.chandoan as chandoantruocphauthuat,
		mbp.chandoan as chandoansauphauthuat,
		ser.servicepricecode,
		ser.servicepricename,
		ser.servicepricename as phuongphappttt,
		(case cls.ppvocamid
			when 1 then 'Gây mê tĩnh mạch'
			when 2 then 'Gây mê nội khí quản'
			when 3 then 'Gây tê tại chỗ'
			when 4 then 'Tiền mê + gây tê tại chỗ'
			when 5 then 'Gây tê tủy sống'
			when 6 then 'Gây tê'
			when 7 then 'Gây tê màng ngoài cứng'
			when 8 then 'Gây tê đám rối thần kinh'
			when 9 then 'Gây tê Codan'
			when 10 then 'Gây tê nhãn cầu'
			when 11 then 'Gây tê cạnh sống'
			when 12 then 'Gây tê hậu nhãn cầu'
			when 99 then 'Khác'
			end) as pttt_phuongphapvocam,
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
	(select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') {_tieuchi_date_ser}) ser
	inner join (select maubenhphamid,chandoan,maubenhphamfinishdate,usertrakq from maubenhpham where 1=1 {_tieuchi_date_mbp} {_phongthuchien}) mbp on mbp.maubenhphamid=ser.maubenhphamid
	{_join_thuchiencls} (select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3,ppvocamid from thuchiencls where 1=1 {_tieuchi_date_thuchien}) cls on cls.servicepriceid=ser.servicepriceid
	inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode
	inner join (select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=ser.hosobenhanid
	inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_date_vp}) vp on vp.vienphiid=ser.vienphiid
	left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid 
	left join nhompersonnel ntkq_cc ON ntkq_cc.userhisid=mbp.usertrakq
	left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme;
	
	
	
	

