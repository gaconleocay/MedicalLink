--ngay 22/4/2018

SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepriceid) as stt, 
	ser.servicepriceid, 
	ser.maubenhphamid as maphieu, 
	vp.patientid as mabn,
	vp.vienphiid as mavp, 
	hsba.patientname as tenbn, 
	degp.departmentgroupname as tenkhoachidinh, 
	de.departmentname as tenphongchidinh, 
	ser.servicepricecode as madv, 
	ser.servicepricename as tendv_yc, 
	ser.servicepricename_bhyt as tendv_bhyt, 
	ser.servicepricename_nhandan as tendv_vp, 
	ser.servicepricename_nuocngoai as tendv_nnn, 
	ser.servicepricemoney as dongia, 
	ser.servicepricemoney_nhandan as dongiavienphi, 
	ser.servicepricemoney_bhyt as dongiabhyt, 
	ser.servicepricemoney_nuocngoai as dongiannn, 
	ser.servicepricedate as thoigianchidinh, 
	ser.soluong as soluong, 
	vp.vienphidate as thoigianvaovien, 
	(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as thoigianravien, 
	vp.duyet_ngayduyet_vp as thoigianduyetvp, 
	vp.duyet_ngayduyet as thoigianduyetbh, 
	(case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as trangthai,
	ser.huongdansudung 
FROM (select * from serviceprice where 1=1 "+_tieuchi_ser+_dsdichvu_ser+") ser 
inner join (select * from vienphi where 1=1 "+_tieuchi_vp+_trangthaiVP+_loaivienphiid+_doituongbenhnhanid+") vp on ser.vienphiid=vp.vienphiid 
inner join hosobenhan hsba on vp.hosobenhanid=hsba.hosobenhanid inner join departmentgroup degp on ser.departmentgroupid=degp.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (0,2,3,9)) de on ser.departmentid=de.departmentid;










