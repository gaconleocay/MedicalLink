---Bao cao Chenh Lech ngay giuong - Khoa quoc Te - bv Thanh Hoa. ngay 31/7/2017

select (row_number() OVER (PARTITION BY kcden.departmentgroupname ORDER BY ser.servicepricecode)) as stt,
	vp.patientid,
	vp.vienphiid,
	hsba.patientname,
	bh.bhytcode,
	mrd.backdepartmentid as khoachuyendenid,
	kcden.departmentgroupname as khoachuyenden,
	krv.departmentgroupname as khoaravien,
	prv.departmentname as phongravien,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, 
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,	
	ser.servicepricecode,
	ser.servicepricename,
	ser.servicepricedate,
	kcd.departmentgroupname as khoachidinh,
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
	ser.soluong,
	(case vp.doituongbenhnhanid 
			when 4 then ser.servicepricemoney_nuocngoai
			else (case ser.loaidoituong 
					when 0 then ser.servicepricemoney_bhyt
					when 1 then ser.servicepricemoney_nhandan
					else ser.servicepricemoney end)
		     end) as servicepricemoney,
	ser.servicepricemoney_bhyt,
	0 as thu_chenhlech,
	0 as bn_thanhtoan,
	0 as bhyt_thanhtoan,
	0 as tongthu,
	vp.loaivienphiid,
	vp.bhyt_tuyenbenhvien,
	vp.bhyt_thangluongtoithieu as thangluongcoban,
	bh.du5nam6thangluongcoban,
	bh.bhyt_loaiid,
	0 as isgroup
from (select patientid,vienphiid,hosobenhanid,bhytid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,doituongbenhnhanid,departmentgroupid,departmentid,loaivienphiid,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu from vienphi "+tieuchi_vp+") vp 
	inner join (select vienphiid,medicalrecordid,departmentgroupid,servicepricecode,servicepricename,servicepricedate,loaidoituong,soluong,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai from serviceprice "+tieuchi_ser+") ser on ser.vienphiid=vp.vienphiid
	inner join (select servicepricecode from servicepriceref where servicepricegroupcode='"+mmeMaNhomDV.Text+"') serf on serf.servicepricecode=ser.servicepricecode
	inner join (select medicalrecordid,backdepartmentid from medicalrecord) mrd on mrd.medicalrecordid=ser.medicalrecordid
	inner join (select hosobenhanid,patientname,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhyt_loaiid,bhytcode,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=vp.bhytid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname) kcden ON kcden.departmentgroupid=mrd.backdepartmentid 
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=vp.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON prv.departmentid=vp.departmentid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid;

 

--select servicepricecode from servicepriceref where servicepricegroupcode='38TH10'
 


