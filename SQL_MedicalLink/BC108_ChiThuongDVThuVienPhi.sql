--BC chi thuong dich vu thu vien phi
--BC108_ChiThuongDVThuVienPhi


--ngay 8/8/2018

SELECT 1 as stt,
	'Thần kinh (ĐNĐ) 03/18' as departmentgroupname,
	'' as quyetdinh_so,
	'21/6/2012' as quyetdinh_ngaythang,
	sum(ser.soluong) as soluong,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as tongtienthu,
	30 as tyle,
	sum(ser.soluong*ser.dongia*0.3) as thuong,	
	'' as kynhan,
	'' as ghichu
FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where servicepricecode in ('TD37018','TD37019') "+tieuchi_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.dongia
	
UNION ALL

SELECT 2 as stt,
	'Thận Nhân Tạo' as departmentgroupname,
	'' as quyetdinh_so,
	'' as quyetdinh_ngaythang,
	sum(ser.soluong) as soluong,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as tongtienthu,
	39.6 as tyle,
	sum(ser.soluong*ser.dongia*0.396) as thuong,	
	'' as kynhan,
	'' as ghichu
FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where servicepricecode='PT11437030' and departmentgroupid=14 "+tieuchi_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.dongia

UNION ALL

SELECT 3 as stt,
	'Nội soi TMH (KBYC)' as departmentgroupname,
	'1557' as quyetdinh_so,
	'26/9/2016' as quyetdinh_ngaythang,
	sum(ser.soluong) as soluong,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as tongtienthu,
	0 as tyle,
	sum(ser.soluong*18000) as thuong,	
	'' as kynhan,
	'' as ghichu
FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,maubenhphamid,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where servicepricecode='U30001-3222' "+tieuchi_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select maubenhphamid from maubenhpham where userthuchien in (504,508) and departmentid_des=279 "+_tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid
GROUP BY ser.dongia
	
UNION ALL

SELECT 4 as stt,
	'Nội soi TMH khoa TMH' as departmentgroupname,
	'' as quyetdinh_so,
	'' as quyetdinh_ngaythang,
	sum(ser.soluong) as soluong,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as tongtienthu,
	0 as tyle,
	sum(ser.soluong*10000) as thuong,	
	'' as kynhan,
	'' as ghichu
FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,maubenhphamid,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where servicepricecode='U30001-3222' "+tieuchi_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select maubenhphamid from maubenhpham where userthuchien not in (504,508) and departmentid_des=279 "+_tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid
GROUP BY ser.dongia

	


	
---Chi tiet - 17/8/2018

SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate) as stt, 
	ser.servicepriceid,
	vp.patientid, 
	vp.vienphiid, 
	hsba.patientname,
	hsba.bhytcode,
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	ngcd.username as nguoichidinh,
	mbp.maubenhphamid,
	mbp.maubenhphamdate,
	pth.departmentname as phongthuchien,
	ngth.username as nguoithuchien,
	(case when ser.servicepricecode in ('TD37018','TD37019') then 'Thần kinh (ĐNĐ) 03/18'
			when (ser.servicepricecode='PT11437030' and ser.departmentgroupid=14) then 'Thận Nhân Tạo'
			when (ser.servicepricecode='U30001-3222' and mbp.userthuchien in (504,508)) then 'Nội soi TMH (KBYC)'
			when (ser.servicepricecode='U30001-3222' and mbp.userthuchien not in (504,508)) then 'Nội soi TMH khoa TMH'
		end) as nhombaocao,
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
	(select servicepriceid,vienphiid,maubenhphamid,departmentgroupid,departmentid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,servicepricedate,maubenhphamphieutype,bhyt_groupcode,loaidoituong 
		from serviceprice 
		where 
			(servicepricecode in ('TD37018','TD37019','U30001-3222') or (servicepricecode='PT11437030' and departmentgroupid=14))
			"+tieuchi_ser+") ser
	INNER JOIN (select maubenhphamid,maubenhphamstatus,maubenhphamdate,userid,departmentid_des,userthuchien from maubenhpham where maubenhphamgrouptype in (1,4) "+_tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid		
	INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 "+_tieuchi_hsba+_hosobenhanstatus+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	LEFT JOIN (select userhisid,username from nhompersonnel) ngcd ON ngcd.userhisid=mbp.userid	
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pth ON pth.departmentid=mbp.departmentid_des
	LEFT JOIN (select userhisid,username from nhompersonnel) ngth ON ngth.userhisid=mbp.userthuchien
WHERE (case when ser.servicepricecode='U30001-3222' then mbp.departmentid_des=279 end) or ser.servicepricecode<>'U30001-3222';
	
	
	
	
	
	
	
	
	
	
	
	

