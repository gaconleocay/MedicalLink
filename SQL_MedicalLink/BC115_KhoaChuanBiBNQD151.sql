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



	
	
	
	
	
	
	
	
	
	



