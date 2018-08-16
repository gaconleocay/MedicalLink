--Bao cao DS cac khoa duoc huong K3
--BC109_DSCacKhoaDuocHuongK3

--ngay 8/8/2018

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	SER.*,
	'' as ghichu
FROM (select departmentgroupid,departmentgroupname from departmentgroup where 1=1  "+lstkhoa_ser+") degp
LEFT JOIN 
	(select ser.departmentgroupid,
		sum(ser.dongia*ser.soluong) as tongtien,
		sum(ser.dongia*ser.soluong*0.02) as thuetndn,
		sum(ser.dongia*ser.soluong*0.1) as tiendien,
		sum(ser.dongia*ser.soluong*0.88) as tongthu,
		0 as truhoan,
		sum(ser.dongia*ser.soluong*0.396) as tienhuong
	from 
		(select vienphiid,soluong,departmentgroupid,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	group by ser.departmentgroupid) SER on SER.departmentgroupid=degp.departmentgroupid;



