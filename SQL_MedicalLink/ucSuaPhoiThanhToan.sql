--ucSuaPhoiThanhToan
--ngay 26/6/2018

SELECT ser.servicepriceid as servicepriceid, 
	ser.maubenhphamid as maubenhpham_ma, 
	case ser.maubenhphamphieutype when 0 then '' else 'Phiếu trả' end as loaiphieu,ser.servicepricecode as dichvu_ma, 
	ser.servicepricename as dichvu_ten, 
	ser.soluong as soluong,ser.loaidoituong as loaihinhthanhtoan,ser.servicepricemoney as gia_yc, 
	ser.servicepricemoney_nhandan as gia_vp, 
	ser.servicepricemoney_bhyt as gia_bhyt, 
	ser.servicepricemoney_nuocngoai as gia_nnn, 
	ser.servicepricedate as thoigianchidinh, 
	vp.patientid as mabn,vp.vienphiid as mavp, 
	hsba.patientname as tenbn, 
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh,kcc.departmentgroupname as khoacuoicung,pcc.departmentname as phongcuoicung,vp.vienphidate as thoigianvaovien, 
	vp.vienphidate_ravien as thoigianravien, 
	vp.duyet_ngayduyet_vp as thoigianduyetvp, 
	vp.duyet_ngayduyet as thoigianduyetbh, 
	case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai 
FROM serviceprice ser 
	inner join vienphi vp on ser.vienphiid=vp.vienphiid 
	inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcd on ser.departmentgroupid=kcd.departmentgroupid 
	inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd on ser.departmentid=pcd.departmentid 
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcc on vp.departmentgroupid=kcc.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcc on vp.departmentid=pcc.departmentid 
WHERE " + timkiemtheo + " 
ORDER BY ser.servicepriceid;