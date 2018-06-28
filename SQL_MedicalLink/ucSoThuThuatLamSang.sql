--Sổ Phẫu thuật thủ thuật.
--ucSoPhauThuatThuThuat




--ngay 26/6/2018: fix bug nhan doi do tach benh an

SELECT
		 ROW_NUMBER () OVER (ORDER BY ser.servicepricedate) as stt,
		hsba.patientcode,
		hsba.patientname,
		ser.maubenhphamid,
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
		(case when vp.doituongbenhnhanid=1 then 'x' end) as COBHYT,
		(pttt.chandoantruocphauthuat || (case when pttt.chandoantruocphauthuat_code<>'' then ' (' || pttt.chandoantruocphauthuat_code || ')' end)) as chandoantruocphauthuat,
		(pttt.chandoansauphauthuat || (case when pttt.chandoantruocphauthuat_code<>'' then ' (' || pttt.chandoansauphauthuat_code || ')' end)) as chandoansauphauthuat,
		ser.servicepricecode,
		ser.servicepricename,
		pttt.phuongphappttt,
		(case pttt.pttt_phuongphapvocamid
			when 1 then 'Gây mê tĩnh mạch'
			when 2 then 'Gây mê nội khí quản'
			when 3 then 'Gây mê tại chỗ'
			when 4 then 'Tiền mê + gây tê tại chỗ'
			when 5 then 'Gây tê tủy sống'
			when 6 then 'Gây tê'
			when 7 then 'Gây tê màng ngoài cứng'
			when 8 then 'Gây tê đám rối thần kinh'
			when 9 then 'Gây tê Codan'
			when 10 then 'Gây tê nhãn cầu'
			when 11 then 'Gây tê cạnh sống'
			when 99 then 'Khác'
			end) as pttt_phuongphapvocam,
		kchd.departmentgroupname as khoachidinh, 
		pcd.departmentname as phongchidinh, 
		ser.servicepricedate,
		pttt.phauthuatthuthuatdate,
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
		bsgm.username as bacsigayme,
		phu.username as phumo1,
		giupviec.username as giupviec,
		nnhap.username as ghichu
FROM
	(select servicepriceid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,departmentid,departmentgroupid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') "+tieuchi_thoigianchidinh+ khoachidinh +" ) ser
left join 
	(select (row_number() OVER (PARTITION BY servicepriceid ORDER BY phauthuatthuthuatid desc)) as stt,servicepriceid,chandoantruocphauthuat_code,chandoantruocphauthuat,chandoansauphauthuat_code,chandoansauphauthuat,phauthuatthuthuatdate,phuongphappttt,pttt_phuongphapvocamid,pttt_hangid,phauthuatvien,bacsigayme,phumo1,phumo3,userid_gmhs from phauthuatthuthuat pttt) pttt on pttt.servicepriceid=ser.servicepriceid
inner join 
	(select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4 and pttt_loaiid not in (1,2,3,4)) serf on serf.servicepricecode=ser.servicepricecode
inner join 
	(select hosobenhanid,patientcode,patientname,gioitinhcode,birthday,hosobenhandate,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid
inner join (select vienphiid,hosobenhanid,doituongbenhnhanid from vienphi) vp on vp.vienphiid=ser.vienphiid
left join (select departmentgroupid,departmentgroupname from departmentgroup) kchd on kchd.departmentgroupid=ser.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) pcd on pcd.departmentid=ser.departmentid 
left join nhompersonnel bspt on bspt.userhisid=pttt.phauthuatvien 
left join nhompersonnel bsgm on bsgm.userhisid=pttt.bacsigayme
left join nhompersonnel phu on phu.userhisid=pttt.phumo1
left join nhompersonnel giupviec on giupviec.userhisid=pttt.phumo3 		
left join nhompersonnel nnhap on nnhap.userhisid=pttt.userid_gmhs
WHERE coalesce(pttt.stt,1)=1 "+tieuchi_thoigianthuchien+";
	
	
	

	


	
	
	
	
	

