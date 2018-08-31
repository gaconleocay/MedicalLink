---Bao cao doanh thu theo nhom dich vu - Ngay 18/7/2017
--ucDoanhThuTheoNhomDichVu

--TOng hop
SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY dv.servicepricegroupcode,dv.servicepricename)) as stt,
	degp.departmentgroupname,
	dv.servicepricecode,
	dv.servicepricename,
	dv.servicepricegroupcode,
	dv.loaidoituong,
	sum(dv.soluong) as soluong,
	dv.servicepricemoney,
	sum(dv.soluong * dv.servicepricemoney) as thanhtien
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp 
LEFT JOIN 	
	(select 
		ser.departmentgroupid,
		ser.servicepricecode,
		ser.servicepricename,
		serf.servicepricegroupcode,
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
		sum(ser.soluong) as soluong,
		(case when vp.doituongbenhnhanid=4 
					then ser.servicepricemoney_nuocngoai
			else 
				(case ser.loaidoituong
					when 0 then ser.servicepricemoney_bhyt
					when 1 then ser.servicepricemoney_nhandan
					when 2 then ser.servicepricemoney_bhyt
					when 3 then ser.servicepricemoney
					when 4 then ser.servicepricemoney
					when 5 then ser.servicepricemoney
					when 6 then ser.servicepricemoney
					when 7 then ser.servicepricemoney_nhandan
					when 8 then ser.servicepricemoney_nhandan
					when 9 then ser.servicepricemoney_nhandan
					end)
			end) as servicepricemoney
	from (select servicepricecode,servicepricegroupcode from servicepriceref where "+lstservicegroupcode+") serf 
		inner join (select vienphiid,servicepricecode,servicepricename,loaidoituong,departmentgroupid,departmentid,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong from serviceprice where bhyt_groupcode in ('06PTTT','07KTC','12NG') "+khoachidinh+") ser on ser.servicepricecode=serf.servicepricecode
		inner join (select vienphiid,doituongbenhnhanid 
					from vienphi 
					where "+trangthaibenhan+"
					--vienphistatus=0
					-- vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 
					--vienphistatus>0 and vienphistatus_vp=1 and duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-04 23:59:59'
					) vp on vp.vienphiid=ser.vienphiid 
	group by ser.departmentgroupid,ser.servicepricecode,ser.servicepricename,serf.servicepricegroupcode,ser.loaidoituong,ser.servicepricemoney,ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney_nuocngoai,vp.doituongbenhnhanid
	order by serf.servicepricegroupcode,ser.servicepricename) dv on dv.departmentgroupid=degp.departmentgroupid
GROUP BY degp.departmentgroupname,dv.servicepricecode,dv.servicepricename,dv.servicepricegroupcode,dv.loaidoituong,dv.servicepricemoney;




---Chi tiet

select (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY serf.servicepricegroupcode,ser.servicepricename)) as stt,
		vp.vienphiid,
		vp.patientid,
		hsba.patientname,
		to_char(hsba.birthday, 'yyyy') as namsinh,
		hsba.gioitinhname as gioitinh,
		hsba.bhytcode,
		degp.departmentgroupname,
		de.departmentname,
		ser.servicepricedate,
		ser.departmentgroupid,
		ser.servicepricecode,
		ser.servicepricename,
		serf.servicepricegroupcode,
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
		ser.soluong as soluong,
		(case when vp.doituongbenhnhanid=4 
					then ser.servicepricemoney_nuocngoai
			else 
				(case ser.loaidoituong
					when 0 then ser.servicepricemoney_bhyt
					when 1 then ser.servicepricemoney_nhandan
					when 2 then ser.servicepricemoney_bhyt
					when 3 then ser.servicepricemoney
					when 4 then ser.servicepricemoney
					when 5 then ser.servicepricemoney
					when 6 then ser.servicepricemoney
					when 7 then ser.servicepricemoney_nhandan
					when 8 then ser.servicepricemoney_nhandan
					when 9 then ser.servicepricemoney_nhandan
					end)
			end) as servicepricemoney,
		(case when vp.doituongbenhnhanid=4 
					then (ser.servicepricemoney_nuocngoai * ser.soluong)
			else 
				(case ser.loaidoituong
					when 0 then (ser.servicepricemoney_bhyt * ser.soluong)
					when 1 then (ser.servicepricemoney_nhandan * ser.soluong)
					when 2 then (ser.servicepricemoney_bhyt * ser.soluong)
					when 3 then (ser.servicepricemoney * ser.soluong)
					when 4 then (ser.servicepricemoney * ser.soluong)
					when 5 then (ser.servicepricemoney * ser.soluong)
					when 6 then (ser.servicepricemoney * ser.soluong)
					when 7 then (ser.servicepricemoney_nhandan * ser.soluong)
					when 8 then (ser.servicepricemoney_nhandan * ser.soluong)
					when 9 then (ser.servicepricemoney_nhandan * ser.soluong)
					end)
			end) as thanhtien	
	from (select servicepricecode,servicepricegroupcode from servicepriceref where "+lstservicegroupcode+") serf 
		inner join (select vienphiid,servicepricecode,servicepricename,servicepricedate,loaidoituong,departmentgroupid,departmentid,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong from serviceprice where bhyt_groupcode in ('06PTTT','07KTC','12NG') "+khoachidinh+") ser on ser.servicepricecode=serf.servicepricecode
		inner join (select vienphiid,doituongbenhnhanid,hosobenhanid,patientid
					from vienphi 
					where "+trangthaibenhan+"
					) vp on vp.vienphiid=ser.vienphiid 
		inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid	
		left join (select departmentid,departmentname from department where departmenttype in (2,3,9,6,7)) de on de.departmentid=ser.departmentid
		inner join (select hosobenhanid,patientname,birthday,gioitinhname,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid;

	
