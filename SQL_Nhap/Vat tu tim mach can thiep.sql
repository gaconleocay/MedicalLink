--bao cao vật tư TMCT
--vật tư xuất từ 1/1/2017 - 31/1/2017 và BN thanh toán sau ngày đó

--select * from maubenhpham where maubenhphamid=11822089	


---------
select ser.vienphiid,
	mbp.maubenhphamid,
	ser.servicepricecode,
	ser.servicepricename,
	ser.soluong,
	ser.soluongbacsi,
	ser.loaidoituong,
	ser.servicepricedate as thoigianchidinh,
	msb.medicinestorebilldate as thoigianxuat,
	ser.servicepricemoney,
	ser.servicepricemoney_nhandan,
	ser.servicepricemoney_bhyt,
	ser.servicepricemoney_nuocngoai,
	ser.soluong * ser.servicepricemoney AS THANHTIEN_yc,
	(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt
						when 1 then ser.servicepricemoney_nhandan
						when 2 then ser.servicepricemoney
						when 3 then ser.servicepricemoney
						when 4 then ser.servicepricemoney
						when 5 then 0
						when 6 then ser.servicepricemoney
						when 7 then 0 
						when 8 then ser.servicepricemoney_nhandan
						when 9 then 0
						else 0 end) * ser.soluong as THANHTIEN
	
from serviceprice ser
	inner join maubenhpham mbp on ser.maubenhphamid=mbp.maubenhphamid
	inner join vienphi vp on vp.vienphiid=ser.vienphiid
	inner join medicine_store_bill msb on mbp.medicinestorebillid=msb.medicinestorebillid
where mbp.medicinestoreid=81 
	and msb.medicinestorebilldate>='2017-01-01 00:00:00'
	and msb.medicinestorebilldate<='2017-03-31 23:59:59'
	and COALESCE(vp.duyet_ngayduyet_vp,'0001-01-01 00:00:00') not BETWEEN  '2017-01-01 00:00:00' and '2017-03-31 23:59:59'	
order by ser.vienphiid

		
				
				
				
				
				
				
				