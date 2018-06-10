--Hậu cần và quản lý mổ Yêu cầu												
												
--BC114_HauCanVaQuanLyMoYeuCau

--9/6/2018
select 0 as stt,
		'' as departmentgroupname,
		sum(case when ser.servicepricecode='U11620-4506' then ser.soluong end) as soluong_tt,
		0 as thanhtien_tt,
		sum(case when ser.servicepricecode='U11621-4524' then ser.soluong end) as soluong_db,
		0 as thanhtien_db,
		sum(case when ser.servicepricecode='U11622-4536' then ser.soluong end) as soluong_l2,
		0 as thanhtien_l2,
		sum(case when ser.servicepricecode='U11623-4610' then ser.soluong end) as soluong_l3
		0 as thanhtien_l3,	
		0 as thanhtien_tong,
		'' as kynhan,
		'' as ghichu
from 
		(select vienphiid,soluong,departmentgroupid,servicepricecode,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid;	
	
	
	
	
	