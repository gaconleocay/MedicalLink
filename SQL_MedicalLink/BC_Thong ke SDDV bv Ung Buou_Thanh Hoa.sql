----Báo cáo thống kê sử dụng dịch vụ - bv Ung bướu
--ngay 29/9
--Chi tiết


select row_number () over (order by degp.departmentgroupname,de.departmentname,hsba.patientname,ser.servicepricename) as stt,
	hsba.patientid,
	ser.vienphiid,
	hsba.patientname,
	bh.bhytcode,
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi,
	ser.servicepricecode,
	ser.servicepricename,
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
	(case ser.loaidoituong 
			when 0 then servicepricemoney_bhyt
			when 1 then servicepricemoney_nhandan
			when 3 then servicepricemoney
			else servicepricemoney end) as servicepricemoney,
	((case ser.loaidoituong 
			when 0 then servicepricemoney_bhyt
			when 1 then servicepricemoney_nhandan
			when 3 then servicepricemoney
			else servicepricemoney end)*ser.soluong) as thanhtien,
	ser.departmentid,		
	de.departmentname,
	ser.departmentgroupid,
	degp.departmentgroupname,
	'0' as isgroup
from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,servicepricedate from serviceprice where departmentid in ("+_lstPhongChonLayBC+") "+_tieuchi_ser+_doituong_ser+") ser
	inner join (select hosobenhanid,patientid,patientname,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname from hosobenhan "+_tieuchi_hsba+") hsba on hsba.hosobenhanid=ser.hosobenhanid
	inner join (select vienphiid,hosobenhanid,bhytid from vienphi) vp on vp.hosobenhanid=hsba.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	left join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid;
	
	
	
	
----Tổng hợp

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
	(case ser.loaidoituong 
			when 0 then servicepricemoney_bhyt
			when 1 then servicepricemoney_nhandan
			when 3 then servicepricemoney
			else servicepricemoney end) as servicepricemoney,
	sum((case ser.loaidoituong 
			when 0 then servicepricemoney_bhyt
			when 1 then servicepricemoney_nhandan
			when 3 then servicepricemoney
			else servicepricemoney end)*ser.soluong) as thanhtien,
	'0' as isgroup		
from (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,bhyt_groupcode from serviceprice where departmentid in ("+_lstPhongChonLayBC+") "+_tieuchi_ser+_doituong_ser+") ser
	inner join (select hosobenhanid from hosobenhan "+_tieuchi_hsba+") hsba on hsba.hosobenhanid=ser.hosobenhanid
group by ser.servicepricecode,ser.servicepricename,ser.loaidoituong,ser.bhyt_groupcode,ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney;








