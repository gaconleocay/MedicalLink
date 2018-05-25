--TRÍCH THƯỞNG DỊCH VỤ ĐO MẬT ĐỘ LOÃNG XƯƠNG, ĐO ĐỘ SƠ HÓA CỦA GAN				
--ucBC104_TrichThuongFIBRO



--ngay 25/5/2018

SELECT row_number () over (order by ser.servicepricename) as stt,
	serf.servicepricecode,
	serf.servicepricename,
	sum(ser.soluong) as soluong,
	sum(ser.soluong*50000) as tientrich,
	'' as kynhan
FROM 
	(select servicepricecode,servicepricename from servicepriceref where 1=1 "+lstdichvu_ser+") serf
	inner join (select vienphiid,departmentid,soluong,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where bhyt_groupcode in ('04CDHA','05TDCN') and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser+") ser on ser.servicepricecode=serf.servicepricecode
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid	
GROUP BY serf.servicepricecode,serf.servicepricename;

