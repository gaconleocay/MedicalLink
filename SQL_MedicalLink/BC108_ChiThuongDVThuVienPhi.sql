--BC chi thuong dich vu thu vien phi
--BC108_ChiThuongDVThuVienPhi


--ngay 7/6/2018

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
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where servicepricecode in ('TD37018','TD37019') and departmentgroupid=11 "+tieuchi_ser+") ser
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
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
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
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where servicepricecode='U30001-3222' "+tieuchi_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select maubenhphamid from maubenhpham where userthuchien in (504,508) and departmentid_des=279 "+tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid
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
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where servicepricecode='U30001-3222' "+tieuchi_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select maubenhphamid from maubenhpham where userthuchien not in (504,508) and departmentid_des=279 "+tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid
GROUP BY ser.dongia

	


	
	
	
	
	
	
	
	
	
	
	
	
	

