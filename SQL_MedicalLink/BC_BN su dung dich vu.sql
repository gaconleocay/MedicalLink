 --Bao cao benh nhan su dung dich vu + nhom dich vu ngay 24/7/2017
 

SELECT 
ROW_NUMBER() OVER (ORDER BY ser.servicepricecode,vp.duyet_ngayduyet_vp) as stt, 
vp.patientid as mabn,
vp.vienphiid as mavp, 
hsba.patientname as tenbn, 
degp.departmentgroupname as tenkhoa, 
de.departmentname as tenphong, 
ser.servicepricecode as madv, 
ser.servicepricename as tendv, 
ser.servicepricemoney as dongia, 
ser.servicepricedate as thoigianchidinh, 
(case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong, 
kcd.departmentgroupname as khoachidinh, 
pcd.departmentname as phongchidinh, 
(case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as loaiphieu, 
vp.vienphidate as thoigianvaovien, 
(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as thoigianravien, 
(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as thoigianduyetvp, 
vp.duyet_ngayduyet as thoigianduyetbh, 
(case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai, 
vp.chandoanravien_code as benhchinh_code, 
vp.chandoanravien as benhchinh_name, 
vp.chandoanravien_kemtheo_code as benhkemtheo_code, 
vp.chandoanravien_kemtheo as benhkemtheo_name, 
bhyt.bhytcode as bhytcode, 
(case ser.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end) as bhyt_groupcode, 
(case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, 
CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, 
(case ser.thuockhobanle when 0 then '' else 'Đơn nhà thuốc' end) as thuockhobanle 
FROM (select vienphiid,departmentgroupid,departmentid,servicepricecode,servicepricename,servicepricemoney,servicepricedate,maubenhphamphieutype,soluong,bhyt_groupcode,loaidoituong,thuockhobanle
		from serviceprice where servicepricecode in ("+danhsachDichVu+")) ser 
	--INNER JOIN (select serff.servicepricecode from (select servicepricecode,servicepricegroupcode,servicepricename from servicepriceref where ServiceGroupType not in (5,6) union all select medicinecode as servicepricecode,medicinegroupcode as servicepricegroupcode,medicinename as servicepricename from medicine_ref) serff where serff.servicepricegroupcode in ("+danhsachDichVu+")) serf on serf.servicepricecode=ser.servicepricecode
	INNER JOIN (select patientid,vienphiid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,duyet_ngayduyet,vienphistatus,vienphistatus_vp,chandoanravien_code,chandoanravien,chandoanravien_kemtheo_code,chandoanravien_kemtheo,departmentgroupid,departmentid,bhytid,doituongbenhnhanid,loaivienphiid from vienphi) vp ON ser.vienphiid=vp.vienphiid 
	INNER JOIN (select hosobenhanid,patientname from hosobenhan) hsba ON vp.hosobenhanid=hsba.hosobenhanid 
	INNER JOIN (select departmentgroupid, departmentgroupname from departmentgroup) degp ON vp.departmentgroupid=degp.departmentgroupid 
	INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de ON vp.departmentid=de.departmentid 
	INNER JOIN (select departmentgroupid, departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid 
	INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid 
	INNER JOIN (select bhytid,bhytcode from bhyt) bhyt ON bhyt.bhytid=vp.bhytid 
	WHERE "+tieuchi + " between '" + datetungay + "' and '" + datedenngay + "' "+loaivienphiid+doituongbenhnhanid +" ; 













