--Bao cao trich thuong tien dich vu giuong yeu cau chuyen khoan
--BC110_TrichThuongGiuongYCCK

--ngay 8/6/2018

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	SER.*,
	'' as ghichu
FROM (select departmentgroupid,departmentgroupname from departmentgroup where 1=1  "+lstkhoa_ser+") degp
LEFT JOIN 
	(select ser.departmentgroupid,
		sum(ser.soluong) as soluong,
		sum(ser.dongia*ser.soluong) as tongtien,
		sum(ser.dongia*ser.soluong*0.02) as thuetndn,
		sum(ser.dongia*ser.soluong*0.1) as tiendien,
		sum(ser.soluong*80000) as tienxuatan,
		(sum(ser.dongia*ser.soluong*0.88)-sum(ser.soluong*80000)) as tongthusauthue,
		(sum(ser.dongia*ser.soluong*0.88)-sum(ser.soluong*80000))*0.25 as tienhuong
	from 
		(select vienphiid,soluong,departmentgroupid,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	group by ser.departmentgroupid) SER on SER.departmentgroupid=degp.departmentgroupid;




