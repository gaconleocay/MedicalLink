--báo cáo THỐNG KÊ SỐ TIỀN KHÁM YÊU CẦU THỨ 7, CHỦ NHẬT 
--ucBC101_TKTienKhamYCT7CN

--ngay 12/6/2018

SELECT row_number () over (order by ser.yyyymmdd) as stt,
	ser.ngaythangnam,
	sum(case when ser.departmentid in (209,210,211,354,355,205,409,206,207,208) then ser.soluong else 0 end) as kyeucau_sl,
	sum(case when ser.departmentid in (209,210,211,354,355,205,409,206,207,208) then (ser.soluong*ser.dongia) else 0 end) as kyeucau_thanhtien,
	sum(case when ser.departmentid in (201,202) then ser.soluong else 0 end) as kdalieu_sl,
	sum(case when ser.departmentid in (201,202) then (ser.soluong*ser.dongia) else 0 end) as kdalieu_thanhtien,
	sum(case when ser.departmentid=212 then ser.soluong else 0 end) as kmat_sl,
	sum(case when ser.departmentid=212 then (ser.soluong*ser.dongia) else 0 end) as kmat_thanhtien,
	sum(case when ser.departmentid=220 then ser.soluong else 0 end) as krhm_sl,
	sum(case when ser.departmentid=220 then (ser.soluong*ser.dongia) else 0 end) as krhm_thanhtien,
	sum(case when ser.departmentid=222 then ser.soluong else 0 end) as ktmh_sl,
	sum(case when ser.departmentid=222 then (ser.soluong*ser.dongia) else 0 end) as ktmh_thanhtien
FROM (select vienphiid,maubenhphamid,departmentid,TO_CHAR(servicepricedate, 'dd/MM/yyyy') as ngaythangnam,TO_CHAR(servicepricedate, 'yyyymmdd') as yyyymmdd,soluong,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where departmentid in (209,210,211,354,355,205,409,206,207,208,201,202,212,220,222)
			and EXTRACT(DOW FROM servicepricedate) in (6,0) and bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser+") ser
	inner join (select maubenhphamid,maubenhphamstatus from maubenhpham where maubenhphamgrouptype=2) mbp on mbp.maubenhphamid=ser.maubenhphamid		
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0
GROUP BY ser.ngaythangnam,ser.yyyymmdd;
	
	
-----

	
	
	
	
	
	
	
	
