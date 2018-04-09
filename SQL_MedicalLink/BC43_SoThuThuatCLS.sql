--Sổ Phẫu thuật thủ thuật CLS
--ucBC43_SoThuThuatCLS

-- ngay 9/4


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
		cls.phuongphappttt,
		'' as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		ser.servicepricedate,
		mbp.maubenhphamfinishdate,--thoi gian tra kq
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
		bspt.username as phauthuatvien,
		bsgm.username as bacsigayme
		--phu.username as phumo1,
		--giupviec.username as giupviec,
		--nnhap.username as ghichu
FROM
	(select servicepriceid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') "+_tieuchi_tgchidinh_ser+ " ) ser
inner join (select maubenhphamid,chandoan,maubenhphamfinishdate from maubenhpham where 1=1 "+_tieuchi_tgchidinh_mbp+_tieuchi_tgthuchien_mbp+_phongthuchien+") mbp on mbp.maubenhphamid=ser.maubenhphamid
left join 
	(select servicepriceid,phuongphappttt,phauthuatvien,bacsigayme,phumo1,phumo3 from thuchiencls) cls on cls.servicepriceid=ser.servicepriceid
inner join 
	(select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode
inner join 
	(select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid
inner join (select hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.hosobenhanid=hsba.hosobenhanid
left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid 
left join nhompersonnel bspt on bspt.userhisid=cls.phauthuatvien 
left join nhompersonnel bsgm on bsgm.userhisid=cls.bacsigayme
--left join nhompersonnel phu on phu.userhisid=cls.phumo1
--left join nhompersonnel giupviec on giupviec.userhisid=cls.phumo3 		
--left join nhompersonnel nnhap on nnhap.userhisid=cls.userid_gmhs;
	
	


	
	
	
	
	

