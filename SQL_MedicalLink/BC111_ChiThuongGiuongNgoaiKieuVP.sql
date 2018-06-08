--Bao cao chi thuong giuong ngoai kieu vien phi
--BC111_ChiThuongGiuongNgoaiKieuVP

--ngay 7/6/2018

select 1 as stt,
	'Ngoại kiều' as departmentgroupname,
	sum(ser.soluong) as soluong,
	sum(ser.dongia*ser.soluong) as tongtien,
	sum(ser.dongia*ser.soluong*0.02) as thuetndn,
	sum(ser.dongia*ser.soluong*0.1) as tiendien,
	sum(ser.soluong*80000) as tienxuatan,
	(sum(ser.dongia*ser.soluong*0.88)-sum(ser.soluong*80000)) as tongthusauthue,
	(sum(ser.dongia*ser.soluong*0.88)-sum(ser.soluong*80000))*0.25 as tienhuong,
	'' as ghichu
from (select departmentgroupid,departmentgroupname from departmentgroup where 1=1  "+lstkhoa_ser+") degp
	left join (select vienphiid,soluong,departmentgroupid,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser on ser.departmentgroupid=degp.departmentgroupid
	left join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY degp.departmentgroupid,degp.departmentgroupname




