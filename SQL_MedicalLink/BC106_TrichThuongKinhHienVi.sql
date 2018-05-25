--KHOA CHUẨN BỊ BỆNH NHÂN BẰNG PHƯƠNG PHÁP KÍNH HIỂN VI						
				
		
--ucBC106_TrichThuongKinhHienVi



--ngay 25/5/2018

SELECT row_number() over() as stt,
	'Ngoại sọ não (F12)' as departmentgroupname,
	sum(ser.soluong) as soluong,
	sum(ser.soluong*400000) as sotien,
	0 as truthue,
	sum(ser.soluong*400000) as tongtien,
	'' as ghichu,
	'' as kynhan
FROM (select vienphiid,departmentid,soluong,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where bhyt_groupcode='06PTTT' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid;
	
