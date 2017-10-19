--Báo cáo BN sử dụng dịch vụ theo nhóm  - Chi phí khác bv Thanh Hóa
-- ngày 18/8
  
-----Xem chi tiet
SELECT 
	(row_number() over (partition by degp.departmentgroupid order by hsba.patientname)) as stt,
	vp.patientid,
	vp.vienphiid,
	hsba.patientname,
	to_char(hsba.birthday, 'yyyy') as namsinh,
	hsba.gioitinhname as gioitinh,
	hsba.bhytcode,
	degp.departmentgroupid,
	degp.departmentgroupname,
	(case vp.doituongbenhnhanid 
			when 1 then 'BHYT'
			when 2 then 'Viện phí'
			when 3 then 'Dịch vụ'
			when 4 then 'Người nước ngoài'
			when 5 then 'Miễn phí'
			when 6 then 'Hợp đồng'
			end) as doituongbenhnhanid,
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
		end) as loaidoituong,
	(case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt
		  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan
		  else ser.servicepricemoney
		  end) as servicepricemoney,
	ser.soluong,
	((case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt
		  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan
		  else ser.servicepricemoney
		  end)*ser.soluong) as thanhtien,
	(case when (ser.billid_thutien<>0 or ser.billid_clbh_thutien<>0)
			then (case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong
						  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan*ser.soluong
						  else ser.servicepricemoney*ser.soluong
				   end)
			else 0 end)	as dathu,
	(case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as trangthai,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp
FROM (select servicepricecode from servicepriceref where servicepricegroupcode in ("+dsnhomdv+")) serf
	inner join (select vienphiid,servicepricecode,servicepricename,loaidoituong,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,soluong,departmentgroupid,servicepricedate,billid_thutien,billid_clbh_thutien from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') "+_tieuchi_ser+") ser on ser.servicepricecode=serf.servicepricecode
	inner join (select vienphiid,patientid,hosobenhanid,doituongbenhnhanid,departmentgroupid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi "+_trangthaivienphi + _tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select hosobenhanid,patientname,birthday,gioitinhname,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname) degp on degp.departmentgroupid="+_partitiongroup+";






	
---Xem tong hop
SELECT 
	(row_number() over (partition by degp.departmentgroupid order by ser.servicepricename)) as stt,
	'' as vienphiid, '' as patientid, '' as patientname, '' as namsinh, '' as gioitinh, '' as bhytcode, '' as doituongbenhnhanid, '' as servicepricedate, '' as loaidoituong, '' as trangthai, '' as vienphidate, '' as vienphidate_ravien, '' as duyet_ngayduyet_vp,
	degp.departmentgroupid,
	degp.departmentgroupname,
	ser.servicepricecode,
	ser.servicepricename,
	(case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt
		  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan
		  else ser.servicepricemoney
		  end) as servicepricemoney,
	sum(ser.soluong) as soluong,
	sum((case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt
		  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan
		  else ser.servicepricemoney
		  end)*ser.soluong) as thanhtien,
	sum(case when (ser.billid_thutien<>0 or ser.billid_clbh_thutien<>0)
			then (case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong
						  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan*ser.soluong
						  else ser.servicepricemoney*ser.soluong
				   end)
			else 0 end) as dathu
FROM (select servicepricecode from servicepriceref where servicepricegroupcode in ("+_dsnhomdv+")) serf
	inner join (select vienphiid,servicepricecode,servicepricename,loaidoituong,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,soluong,departmentgroupid,billid_thutien,billid_clbh_thutien from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') "+_tieuchi_ser+") ser on ser.servicepricecode=serf.servicepricecode
	inner join (select vienphiid,doituongbenhnhanid,departmentgroupid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi "+_trangthaivienphi +_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname) degp on degp.departmentgroupid="+_partitiongroup+"
GROUP BY degp.departmentgroupid,degp.departmentgroupname,ser.servicepricecode,ser.servicepricename,(case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt
		  when ser.loaidoituong=1 then ser.servicepricemoney_nhandan
		  else ser.servicepricemoney
		  end);
	



