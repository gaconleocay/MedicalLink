--Khoa chuẩn bị bệnh nhân											
--BC115_KhoaChuanBiBNQD151

--8/8/2018
select row_number () over (order by degp.departmentgroupname) as stt,
		mrd.backdepartmentid,
		degp.departmentgroupname,
		sum(case when ser.servicepricecode='U11620-4506' then ser.soluong else 0 end) as soluong_tt,
		sum(case when ser.servicepricecode='U11620-4506' then ser.soluong*450000 else 0 end) as thanhtien_tt,
		sum(case when ser.servicepricecode='U11621-4524' then ser.soluong else 0 end) as soluong_db,
		sum(case when ser.servicepricecode='U11621-4524' then ser.soluong*400000 else 0 end) as thanhtien_db,
		sum(case when ser.servicepricecode='U11622-4536' then ser.soluong else 0 end) as soluong_l2,
		sum(case when ser.servicepricecode='U11622-4536' then ser.soluong*300000 else 0 end) as thanhtien_l2,
		sum(case when ser.servicepricecode='U11623-4610' then ser.soluong else 0 end) as soluong_l3,
		sum(case when ser.servicepricecode='U11623-4610' then ser.soluong*200000 else 0 end) as thanhtien_l3,	
		0 as thanhtien_tong,
		'' as kynhan,
		'' as ghichu
from 
		(select vienphiid,soluong,medicalrecordid,servicepricecode,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
		inner join (select backdepartmentid,medicalrecordid,medicalrecordid_next from medicalrecord where 1=1 "+tieuchi_mrd+") mrd on mrd.medicalrecordid=ser.medicalrecordid
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
		inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=mrd.backdepartmentid	
group by mrd.backdepartmentid,degp.departmentgroupname;



	

---Chi tiet - 19/8


SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate) as stt, 
	ser.servicepriceid,
	vp.patientid, 
	vp.vienphiid, 
	hsba.patientname,
	hsba.bhytcode,
	kcb.departmentgroupname as khoachuyenden,
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	ngcd.username as nguoichidinh,
	mbp.maubenhphamid,
	mbp.maubenhphamdate,
	ser.servicepricecode,
	ser.servicepricename,
	ser.soluong, 
	ser.dongia,
	(ser.soluong*ser.dongia) as thanhtien,
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC'
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		when 20 then 'Thanh toán riêng'
		end) as loaidoituong,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,	
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus,
	(case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' end) trangthaithutien
FROM 
	(select servicepriceid,vienphiid,maubenhphamid,departmentgroupid,departmentid,medicalrecordid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,servicepricedate,maubenhphamphieutype,bhyt_groupcode,loaidoituong 
		from serviceprice 
		where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
	inner join (select backdepartmentid,medicalrecordid,medicalrecordid_next from medicalrecord where 1=1 "+tieuchi_mrd+") mrd on mrd.medicalrecordid=ser.medicalrecordid	
	INNER JOIN (select maubenhphamid,maubenhphamstatus,maubenhphamdate,userid,departmentid_des from maubenhpham where maubenhphamgrouptype=4 "+tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid		
	INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 "+_tieuchi_hsba+_hosobenhanstatus+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	LEFT JOIN (select userhisid,username from nhompersonnel) ngcd ON ngcd.userhisid=mbp.userid	
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcb ON kcb.departmentgroupid=mrd.backdepartmentid;
	
	
	
	
		
	
	
	
	
	
	
	
	



