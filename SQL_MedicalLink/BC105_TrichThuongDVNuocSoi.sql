--TRÍCH THƯỞNG DỊCH VỤ NƯỚC SÔI						
		
--ucBC105_TrichThuongDVNuocSoi



--ngay 25/5/2018

SELECT row_number() over() as stt,
	'' as departmentgroupname,
	sum(ser.soluong*ser.dongia) as tongthu,
	0 as tongchi,
	0 as phantramhuong,
	0 as thuclinh,
	'' as kynhan
FROM (select vienphiid,departmentid,soluong,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where bhyt_groupcode='12NG' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser+") ser
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid;