----Báo cáo thống kê sử dụng dịch vụ - yeu cau



--Chi tiết
--ngay 13/3

select row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname,ser.servicepricename) as stt,
	hsba.patientid,
	ser.vienphiid,
	hsba.patientname,
	bh.bhytcode,
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi,
	ser.servicepricecode,
	ser.servicepricename,
	ncd.username as nguoichidinh,
	ser.servicepricedate,
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC '
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		when 20 then 'Thanh toán riêng'
		end) as loaidoituong,
	ser.soluong,
	ser.servicepricemoney,
	(ser.servicepricemoney*ser.soluong) as thanhtien,
	ser.departmentid,		
	de.departmentname,
	ser.departmentgroupid,
	degp.departmentgroupname,
	'0' as isgroup
from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,servicepricedate,maubenhphamid from serviceprice where loaidoituong in (3,4) and  departmentid in ("+_lstPhongChonLayBC+") "+_tieuchi_ser+") ser
	inner join (select hosobenhanid,patientid,patientname,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid
	inner join (select vienphiid,hosobenhanid,bhytid from vienphi where 1=1 "+_tieuchi_vp+_listuserid+") vp on vp.hosobenhanid=hsba.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	left join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid
	"+_tieuchi_bill+"
	inner join (select maubenhphamid,userid from maubenhpham) mbp on mbp.maubenhphamid=ser.maubenhphamid
	LEFT JOIN (select userhisid,username from nhompersonnel) ncd ON ncd.userhisid=mbp.userid;
	
	
	
----Tổng hợp
--ngay 13/3
select row_number () over (order by ser.bhyt_groupcode,ser.servicepricename) as stt,
	ser.servicepricecode,
	ser.servicepricename,
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC '
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		when 20 then 'Thanh toán riêng'
		end) as loaidoituong,
	(case ser.bhyt_groupcode 
			when '01KB' then '01-Khám bệnh'
			when '03XN' then '02-Xét nghiệm'
			when '04CDHA' then '04-CĐHA'
			when '05TDCN' then '05-Thăm dò chức năng'
			when '06PTTT' then '06-PTTT'
			when '07KTC' then '07-DV KTC'
			else '99-Khác'
			end) as bhyt_groupcode,
	sum(ser.soluong) as soluong,
	ser.servicepricemoney,
	sum(ser.servicepricemoney*ser.soluong) as thanhtien,
	'0' as isgroup		
from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,bhyt_groupcode from serviceprice where loaidoituong in (3,4) and departmentid in ("+_lstPhongChonLayBC+") "+_tieuchi_ser+") ser
	inner join (select vienphiid from vienphi where 1=1 "+_tieuchi_vp+_listuserid+") vp on vp.vienphiid=ser.vienphiid
	"+_tieuchi_bill+"
group by ser.servicepricecode,ser.servicepricename,ser.loaidoituong,ser.bhyt_groupcode,ser.servicepricemoney;


---Tong hop theo danh sach benh nhan 
-- ngay 13/3
SELECT row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname) as stt,
	hsba.patientid,
	ser.vienphiid,
	hsba.patientname,
	bh.bhytcode,
	to_char(hsba.birthday,'yyyy') as year,
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi,
	sum(ser.servicepricemoney*ser.soluong) as thanhtien,
	ser.departmentid,		
	de.departmentname,
	ser.departmentgroupid,
	degp.departmentgroupname,
	'0' as isgroup 
FROM (select hosobenhanid,vienphiid,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid from serviceprice where loaidoituong in (3,4) and departmentid in ("+_lstPhongChonLayBC+") "+_tieuchi_ser+") ser 
	inner join (select hosobenhanid,patientid,patientname,bhytcode,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname from hosobenhan) hsba on hsba.hosobenhanid=ser.hosobenhanid 
	inner join (select vienphiid,hosobenhanid,bhytid from vienphi where 1=1 and "+_tieuchi_vp+_listuserid+") vp on vp.hosobenhanid=hsba.hosobenhanid 
	"+_tieuchi_bill+"
	inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid 
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid 
	left join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid
GROUP BY hsba.patientid,ser.vienphiid,hsba.patientname,bh.bhytcode,hsba.birthday,hsba.hc_sonha,hsba.hc_thon,hsba.hc_xacode,hsba.hc_xaname,hsba.hc_huyencode,hsba.hc_huyenname,ser.departmentid,de.departmentname,ser.departmentgroupid,degp.departmentgroupname;




   








