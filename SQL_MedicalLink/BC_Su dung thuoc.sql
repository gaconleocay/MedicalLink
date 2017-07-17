---bao cao Su dung thuoc ngay 16/7/2017

---dang dieu tri
SELECT 
	row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	sum(case when data.datatype=1 then data.sl_1 else 0 end) as sl_dangdt_1,
	sum(case when data.datatype=2 then data.sl_1 else 0 end) as sl_chuatt_1,
	sum(case when data.datatype=3 then data.sl_1 else 0 end) as sl_datt_1,
	sum(data.sl_1) as sl_tong_1,
	sum(data.tongtien_1) as tongtien_1,
	sum(case when data.datatype=1 then data.sl_2 else 0 end) as sl_dangdt_2,
	sum(case when data.datatype=2 then data.sl_2 else 0 end) as sl_chuatt_2,
	sum(case when data.datatype=3 then data.sl_2 else 0 end) as sl_datt_2,
	sum(data.sl_2) as sl_tong_2,
	sum(data.tongtien_2) as tongtien_2,
	sum(case when data.datatype=1 then data.sl_3 else 0 end) as sl_dangdt_3,
	sum(case when data.datatype=2 then data.sl_3 else 0 end) as sl_chuatt_3,
	sum(case when data.datatype=3 then data.sl_3 else 0 end) as sl_datt_3,
	sum(data.sl_3) as sl_tong_3,
	sum(data.tongtien_3) as tongtien_3,
	sum(case when data.datatype=1 then data.sl_4 else 0 end) as sl_dangdt_4,
	sum(case when data.datatype=2 then data.sl_4 else 0 end) as sl_chuatt_4,
	sum(case when data.datatype=3 then data.sl_4 else 0 end) as sl_datt_4,
	sum(data.sl_4) as sl_tong_4,
	sum(data.tongtien_4) as tongtien_4,
	sum(case when data.datatype=1 then data.sl_5 else 0 end) as sl_dangdt_5,
	sum(case when data.datatype=2 then data.sl_5 else 0 end) as sl_chuatt_5,
	sum(case when data.datatype=3 then data.sl_5 else 0 end) as sl_datt_5,
	sum(data.sl_5) as sl_tong_5,
	sum(data.tongtien_5) as tongtien_5,
	sum(case when data.datatype=1 then data.sl_6 else 0 end) as sl_dangdt_6,
	sum(case when data.datatype=2 then data.sl_6 else 0 end) as sl_chuatt_6,
	sum(case when data.datatype=3 then data.sl_6 else 0 end) as sl_datt_6,
	sum(data.sl_6) as sl_tong_6,
	sum(data.tongtien_6) as tongtien_6,
	sum(case when data.datatype=1 then data.sl_7 else 0 end) as sl_dangdt_7,
	sum(case when data.datatype=2 then data.sl_7 else 0 end) as sl_chuatt_7,
	sum(case when data.datatype=3 then data.sl_7 else 0 end) as sl_datt_7,
	sum(data.sl_7) as sl_tong_7,
	sum(data.tongtien_7) as tongtien_7,
	sum(case when data.datatype=1 then data.sl_8 else 0 end) as sl_dangdt_8,
	sum(case when data.datatype=2 then data.sl_8 else 0 end) as sl_chuatt_8,
	sum(case when data.datatype=3 then data.sl_8 else 0 end) as sl_datt_8,
	sum(data.sl_8) as sl_tong_8,
	sum(data.tongtien_8) as tongtien_8,
	sum(case when data.datatype=1 then data.sl_9 else 0 end) as sl_dangdt_9,
	sum(case when data.datatype=2 then data.sl_9 else 0 end) as sl_chuatt_9,
	sum(case when data.datatype=3 then data.sl_9 else 0 end) as sl_datt_9,
	sum(data.sl_9) as sl_tong_9,
	sum(data.tongtien_9) as tongtien_9,
	sum(case when data.datatype=1 then data.sl_10 else 0 end) as sl_dangdt_10,
	sum(case when data.datatype=2 then data.sl_10 else 0 end) as sl_chuatt_10,
	sum(case when data.datatype=3 then data.sl_10 else 0 end) as sl_datt_10,
	sum(data.sl_10) as sl_tong_10,
	sum(data.tongtien_10) as tongtien_10,
	sum(case when data.datatype=1 then data.sl_11 else 0 end) as sl_dangdt_11,
	sum(case when data.datatype=2 then data.sl_11 else 0 end) as sl_chuatt_11,
	sum(case when data.datatype=3 then data.sl_11 else 0 end) as sl_datt_11,
	sum(data.sl_11) as sl_tong_11,
	sum(data.tongtien_11) as tongtien_11,
	sum(case when data.datatype=1 then data.sl_12 else 0 end) as sl_dangdt_12,
	sum(case when data.datatype=2 then data.sl_12 else 0 end) as sl_chuatt_12,
	sum(case when data.datatype=3 then data.sl_12 else 0 end) as sl_datt_12,
	sum(data.sl_12) as sl_tong_12,
	sum(data.tongtien_12) as tongtien_12,
	sum(data.tongtien_1+data.tongtien_2+data.tongtien_3+data.tongtien_4+data.tongtien_5+data.tongtien_6+data.tongtien_7+data.tongtien_8+data.tongtien_9+data.tongtien_10+data.tongtien_11+data.tongtien_12) as tongtien
	
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp
	left join
	(
	select 1 as datatype,
		ser.departmentgroupid,
		sum(case when to_char(ser.servicepricedate, 'mm')='01' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_1,
		sum(case when to_char(ser.servicepricedate, 'mm')='01' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_1,		
		sum(case when to_char(ser.servicepricedate, 'mm')='02' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_2,
		sum(case when to_char(ser.servicepricedate, 'mm')='02' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_2,		
		sum(case when to_char(ser.servicepricedate, 'mm')='03' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_3,
		sum(case when to_char(ser.servicepricedate, 'mm')='03' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_3,		
		sum(case when to_char(ser.servicepricedate, 'mm')='04' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_4,
		sum(case when to_char(ser.servicepricedate, 'mm')='04' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_4,
		sum(case when to_char(ser.servicepricedate, 'mm')='05' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_5,
		sum(case when to_char(ser.servicepricedate, 'mm')='05' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_5,		
		sum(case when to_char(ser.servicepricedate, 'mm')='06' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_6,
		sum(case when to_char(ser.servicepricedate, 'mm')='06' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_6,		
		sum(case when to_char(ser.servicepricedate, 'mm')='07' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_7,
		sum(case when to_char(ser.servicepricedate, 'mm')='07' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_7,		
		sum(case when to_char(ser.servicepricedate, 'mm')='08' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_8,
		sum(case when to_char(ser.servicepricedate, 'mm')='08' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_8,		
		sum(case when to_char(ser.servicepricedate, 'mm')='09' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_9,
		sum(case when to_char(ser.servicepricedate, 'mm')='09' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_9,		
		sum(case when to_char(ser.servicepricedate, 'mm')='10' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_10,
		sum(case when to_char(ser.servicepricedate, 'mm')='10' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_10,		
		sum(case when to_char(ser.servicepricedate, 'mm')='11' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_11,
		sum(case when to_char(ser.servicepricedate, 'mm')='11' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_11,		
		sum(case when to_char(ser.servicepricedate, 'mm')='12' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_12,
		sum(case when to_char(ser.servicepricedate, 'mm')='12' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_12		
	from (select vienphiid from vienphi where vienphistatus=0 and vienphidate>'2016-01-01 00:00:00') vp
		inner join (select vienphiid,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype,servicepricemoney
			from serviceprice where servicepricecode in ("+lstservicepricecode+") and thuockhobanle=0 and servicepricedate>'2016-01-01 00:00:00' and soluong>0) ser on ser.vienphiid=vp.vienphiid
	group by ser.departmentgroupid

	union all ---ra vien chua thanh toan
	select 2 as datatype,
		ser.departmentgroupid,
		sum(case when to_char(ser.servicepricedate, 'mm')='01' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_1,
		sum(case when to_char(ser.servicepricedate, 'mm')='01' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_1,		
		sum(case when to_char(ser.servicepricedate, 'mm')='02' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_2,
		sum(case when to_char(ser.servicepricedate, 'mm')='02' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_2,		
		sum(case when to_char(ser.servicepricedate, 'mm')='03' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_3,
		sum(case when to_char(ser.servicepricedate, 'mm')='03' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_3,		
		sum(case when to_char(ser.servicepricedate, 'mm')='04' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_4,
		sum(case when to_char(ser.servicepricedate, 'mm')='04' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_4,
		sum(case when to_char(ser.servicepricedate, 'mm')='05' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_5,
		sum(case when to_char(ser.servicepricedate, 'mm')='05' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_5,		
		sum(case when to_char(ser.servicepricedate, 'mm')='06' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_6,
		sum(case when to_char(ser.servicepricedate, 'mm')='06' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_6,		
		sum(case when to_char(ser.servicepricedate, 'mm')='07' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_7,
		sum(case when to_char(ser.servicepricedate, 'mm')='07' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_7,		
		sum(case when to_char(ser.servicepricedate, 'mm')='08' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_8,
		sum(case when to_char(ser.servicepricedate, 'mm')='08' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_8,		
		sum(case when to_char(ser.servicepricedate, 'mm')='09' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_9,
		sum(case when to_char(ser.servicepricedate, 'mm')='09' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_9,		
		sum(case when to_char(ser.servicepricedate, 'mm')='10' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_10,
		sum(case when to_char(ser.servicepricedate, 'mm')='10' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_10,		
		sum(case when to_char(ser.servicepricedate, 'mm')='11' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_11,
		sum(case when to_char(ser.servicepricedate, 'mm')='11' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_11,		
		sum(case when to_char(ser.servicepricedate, 'mm')='12' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_12,
		sum(case when to_char(ser.servicepricedate, 'mm')='12' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_12	
	from (select vienphiid from vienphi where vienphistatus>0 and coalesce(vienphistatus_vp,0)=0) vp
		inner join (select vienphiid,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype,servicepricemoney
			from serviceprice where servicepricecode in ("+lstservicepricecode+") and thuockhobanle=0 and soluong>0) ser on ser.vienphiid=vp.vienphiid
	group by ser.departmentgroupid

	union all ---da thanh toan
	select 3 as datatype,
		ser.departmentgroupid,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='01' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_1,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='01' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_1,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='02' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_2,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='02' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_2,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='03' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_3,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='03' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_3,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='04' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_4,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='04' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_4,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='05' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_5,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='05' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_5,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='06' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_6,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='06' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_6,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='07' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_7,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='07' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_7,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='08' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_8,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='08' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_8,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='09' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_9,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='09' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_9,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='10' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_10,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='10' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_10,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='11' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_11,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='11' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_11,		
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='12' 
					then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end)
				else 0 end) as sl_12,
		sum(case when to_char(vp.duyet_ngayduyet_vp, 'mm')='12' 
					then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end)
				else 0 end) as tongtien_12	
	from (select vienphiid,duyet_ngayduyet_vp from vienphi where vienphistatus>0 and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "') vp
		inner join (select vienphiid,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype,servicepricemoney
			from serviceprice where servicepricecode in ("+lstservicepricecode+") and thuockhobanle=0 and soluong>0) ser on ser.vienphiid=vp.vienphiid
	group by ser.departmentgroupid
	) data ON degp.departmentgroupid=data.departmentgroupid
GROUP BY degp.departmentgroupname;









	
	
	
	
	
	
	
	

