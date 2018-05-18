--TRÍCH THƯỞNG DỊCH VỤ ĐO MẬT ĐỘ LOÃNG XƯƠNG, ĐO ĐỘ SƠ HÓA CỦA GAN				
--ucBC104_TrichThuongFIBRO



--ngay 18/5/2018

SELECT row_number () over (order by ser.servicepricename) as stt,
	serf.servicepricecode,
	serf.servicepricename,
	sum(ser.soluong) as soluong,
	sum(ser.soluong*50000) as tientrich,
	'' as kynhan
FROM 
	(select servicepricecode,servicepricename from servicepriceref where 1=1 "+lstdichvu_ser+") serf
	left join (select vienphiid,departmentid,soluong,servicepricecode,servicepricename,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where 1=1 "+tieuchi_ser+lstdichvu_ser+") ser on ser.servicepricecode=serf.servicepricecode
	left join (select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY serf.servicepricecode,serf.servicepricename;

