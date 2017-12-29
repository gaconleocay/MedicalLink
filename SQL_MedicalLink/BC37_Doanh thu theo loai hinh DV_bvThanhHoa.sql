--BÁO CÁO DOANH THU THEO LOẠI HÌNH DỊCH VỤ CHI TIẾT bv Thanh Hoa
-- ngay 22/12
--lọc theo đối tượng thanh toán dịch vụ: 
--tieu chi theo ngay thu tien



SELECT row_number () over (order by serf.servicepricename) as stt,
		serf.servicepricegroupcode,
		serf.bhyt_groupcode,
		serf.servicegrouptype,
		(case serf.servicegrouptype  
				when 1 then 'Khám bệnh'  
				when 2 then 'Xét nghiệm'  
				when 3 then 'CĐHA'  
				when 4 then 'Chuyên khoa'     
				end) as servicegrouptype_name,
		ser.departmentgroupid,		
		degp.departmentgroupname,		
		serf.servicepricecode,
		serf.servicepricename,
		serf.servicepricenamebhyt,
		serf.servicepriceunit,
		ser.soluong,
		ser.servicepricemoney,
		ser.thanhtien_dv,
		ser.servicepricemoney_bhyt,
		ser.thanhtien_bh,
		ser.thanhtien_chenh,
		ser.thanhtien_chenh as tienthucthu,
		'0' as isgroup
		
FROM (select servicepricegroupcode,bhyt_groupcode,servicegrouptype,servicepricetype,servicepricecode,servicepricename,servicepricenamebhyt,servicepriceunit,servicepricefee,servicepricefeenhandan,servicepricefeebhyt,servicepricefeenuocngoai from servicepriceref where servicegrouptype in (1,2,3,4)) serf
	inner join (select se.servicepricecode,
					se.departmentgroupid,
					sum(se.soluong) as soluong,
					se.servicepricemoney,
					(se.servicepricemoney*sum(se.soluong)) as thanhtien_dv,
					se.servicepricemoney_bhyt,
					(se.servicepricemoney_bhyt*sum(se.soluong)) as thanhtien_bh,
					((se.servicepricemoney-se.servicepricemoney_bhyt)*sum(se.soluong)) as thanhtien_chenh,				
					se.bhyt_groupcode					
				from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,bhyt_groupcode,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,billid_clbh_thutien
					from serviceprice 
					where " + _loaihinhthanhtoan + " and bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG') "+_tieuchi_ser+" ) se
					inner join (select vienphiid from vienphi where "+_trangthaivienphi+_tieuchi_vp+" ) vp on vp.vienphiid=se.vienphiid
					"+_tieuchi_bill+"
				group by se.servicepricecode,se.departmentgroupid,se.bhyt_groupcode,se.servicepricemoney_bhyt,se.servicepricemoney) ser on ser.servicepricecode=serf.servicepricecode
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
WHERE ser.soluong>0
ORDER BY serf.servicegrouptype,serf.servicepricegroupcode,serf.servicepricename;
	



----lay ten nhom dich vu
SELECT servicepricecode,servicepricename
FROM servicepriceref
WHERE servicepricecode in (select servicepricegroupcode 
							from servicepriceref 
							where servicegrouptype in (1,2,3,4)
							group by servicepricegroupcode)


--_tieuchi_bill
inner join (select billid from bill where dahuyphieu=0 and billdate between '" + datetungay + "' and '" + datedenngay + "') bi on bi.billid=se.billid_clbh_thutien













