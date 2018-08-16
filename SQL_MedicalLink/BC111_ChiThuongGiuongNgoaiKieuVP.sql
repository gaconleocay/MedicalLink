--Bao cao chi thuong giuong ngoai kieu vien phi
--BC111_ChiThuongGiuongNgoaiKieuVP


--ngay 8/8/2018

SELECT 1 as stt,
	'Ngoại kiều' as departmentgroupname,
	'' as quyetdinh_so,
	'' as quyetdinh_ngaythang,
	sum(ser.soluong) as soluong,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as tongtienthu,
	10 as tyle,
	sum(ser.soluong*ser.dongia*0.1) as thuong,	
	'' as kynhan,
	'' as ghichu
FROM (select vienphiid,soluong,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
		from serviceprice 
		where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.dongia




