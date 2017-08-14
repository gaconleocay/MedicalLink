SELECT ser.servicepriceid as servicepriceid, 
ser.medicalrecordid as madieutri, 
ser.vienphiid as mavienphi, 
ser.hosobenhanid as hosobenhan, 
ser.maubenhphamid as maubenhpham, 
ser.servicepricecode as madichvu, 
ser.servicepricename as tendichvu, 
ser.servicepricemoney as gia, 
ser.servicepricemoney_bhyt as gia_bhyt, 
ser.servicepricemoney_nhandan as gia_nhandan, 
ser.servicepricemoney_nuocngoai as gia_nnn, 
ser.soluong as soluong, 
ser.bhyt_groupcode as bhyt_groupcode, 
ser.servicepricedate as ngaychidinh 
FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where (bhyt_groupcode is null or bhyt_groupcode='') "+tieuchi_ser+") ser
	inner join (select medicinecode from medicine_ref) serf on serf.medicinecode=ser.servicepricecode
	inner join (select vienphiid from vienphi where "+trangthaiVP+tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
ORDER BY ser.servicepriceid;


" + tieuchi + " >= '" + datetungay + "' and " + tieuchi + " <= '" + datedenngay + "' and ser. " + trangthaiVP + " 
 ;



