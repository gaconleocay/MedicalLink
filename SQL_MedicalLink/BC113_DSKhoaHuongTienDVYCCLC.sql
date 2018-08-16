--Danh sách Khoa hưởng tiền dịch vụ yêu cầu chất lượng cao													
--BC113_DSKhoaHuongTienDVYCCLC

--8/8/2018

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname as khoachuyenden,
	SER.soluong_2,
	SER.thanhtien_2,
	SER.soluong_3,
	SER.thanhtien_3,
	SER.soluong_5,
	SER.thanhtien_5,
	SER.thanhtien,
	(SER.thanhtien*0.02) as tienthue2,
	(SER.thanhtien-(SER.thanhtien*0.02)) as tiensauthue,
	((SER.thanhtien-(SER.thanhtien*0.02))*0.05) as tientrich5,
	'' as kynhan
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp
INNER JOIN 
	(select mrd.backdepartmentid,
		sum(case when ser.dongia=2000000 then ser.soluong else 0 end) as soluong_2,
		sum(case when ser.dongia=2000000 then ser.soluong*ser.dongia else 0 end) as thanhtien_2,
		sum(case when ser.dongia=3000000 then ser.soluong else 0 end) as soluong_3,
		sum(case when ser.dongia=3000000 then ser.soluong*ser.dongia else 0 end) as thanhtien_3,
		sum(case when ser.dongia=5000000 then ser.soluong else 0 end) as soluong_5,
		sum(case when ser.dongia=5000000 then ser.soluong*ser.dongia else 0 end) as thanhtien_5,
		sum(ser.soluong*ser.dongia) as thanhtien	
	from 
		(select vienphiid,soluong,medicalrecordid,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser
		inner join (select backdepartmentid,medicalrecordid,medicalrecordid_next from medicalrecord where 1=1 "+tieuchi_mrd+") mrd on mrd.medicalrecordid=ser.medicalrecordid
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	group by mrd.backdepartmentid) SER on SER.backdepartmentid=degp.departmentgroupid;




	
	
	
	
	
	
	
	
	
	



