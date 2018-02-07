SELECT row_number () over (order by ser.servicepricedate) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	bh.bhytcode,
	degp.departmentgroupname,
	de.departmentname,
	 (case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as vienphistatus,
	vp.vienphidate,
	(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.vienphistatus>0 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	ser.servicepriceid,
	ser.maubenhphamid,
	ser.servicepricedate,
	ser.servicepricecode,
	ser.servicepricename,
	ser.servicepricemoney_bhyt,
	ser.servicepricemoney,
	ser.soluong,
	(ser.servicepricemoney*ser.soluong) as thanhtien,
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
	(case ser.loaidoituong_org
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
		end) as loaidoituong_org,
	kcd.departmentgroupname as khoachidinh,
	pcd.departmentname as phongchidinh,
	ser.loaidoituong_remark
	
FROM (select * from serviceprice where bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and loaidoituong=9 
and loaidoituong_remark='[AUTO] Kho thuốc miễn phí' "+_tieuchi_ser+") ser
inner join (select vienphiid,patientid,bhytid,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus,vienphistatus_vp,duyet_ngayduyet_vp,departmentgroupid,departmentid from vienphi where 1=1 "+_trangthaibenhan + _tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
inner join (select hosobenhanid,patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid
left join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=vp.departmentid 
left join (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=ser.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd on pcd.departmentid=ser.departmentid;






select * from tbllogdata where logvalue like '%IsKhoCapPhatMienPhi = ''1''%' and logtime >'2017-11-01 00:00:00'




