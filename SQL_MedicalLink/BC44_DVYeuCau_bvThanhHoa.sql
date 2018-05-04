--ucBC44_DVYeuCau_2: BC dịch vụ yêu cầu: giống BC41


	
----Xem chi tiet
--ngay 4/5:
SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname,de.departmentname,serf.report_tkcode order by ser.servicepricename))  as stt,
	ser.departmentgroupid,
	ser.departmentid,
	degp.departmentgroupname,
	de.departmentname,
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
	serf.report_tkcode,
	tkgroup.billaccountrefname,
	sum(ser.soluong) as soluong,
	ser.servicepricemoney,
	sum(ser.servicepricemoney*ser.soluong) as thanhtien,
	ser.servicepricemoney_bhyt,
	sum(ser.servicepricemoney_bhyt*ser.soluong) as thanhtien_bhyt,
	sum(case when ser.servicepricemoney_bhyt<>0 then ((ser.servicepricemoney-ser.servicepricemoney_bhyt)*ser.soluong) else 0 end) as chenhlech,
	'0' as isgroup
FROM (select hosobenhanid,vienphiid,servicepricecode,servicepricename,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,departmentgroupid,departmentid,bhyt_groupcode,billid_clbh_thutien from serviceprice where loaidoituong in (3,4) and bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') and departmentid in ("+_lstPhongChonLayBC+") "+_tieuchi_ser+") ser
	inner join (select vienphiid from vienphi where 1=1 "+_tieuchi_vp+_listuserid+") vp on vp.vienphiid=ser.vienphiid
	"+_tieuchi_bill+"
	inner join (select servicepricecode,servicepricegroupcode,servicepricename,report_tkcode from servicepriceref where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode
	left join billaccountref tkgroup on tkgroup.billaccountrefcode=serf.report_tkcode
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	inner join (select departmentid,departmentname from department) de on de.departmentid=ser.departmentid
GROUP BY ser.servicepricecode,ser.servicepricename,ser.loaidoituong,serf.report_tkcode,tkgroup.billaccountrefname,ser.servicepricemoney,ser.servicepricemoney_bhyt,ser.departmentgroupid,ser.departmentid,degp.departmentgroupname,de.departmentname;

















