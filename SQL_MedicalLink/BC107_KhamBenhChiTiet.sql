--Bao cao chi tiet su dung dich vu Kham benh
--ucBC107_KhamBenhChiTiet

--ngay 23/5/2018: sua trang thai kham

SELECT
	row_number () over (order by ser.servicepricedate) as stt,
	ser.servicepriceid,
	ser.maubenhphamid,
	ser.servicepricecode,
	ser.servicepricename,
	ser.dongia,
	ser.soluong,
	(ser.soluong*ser.dongia) as thanhtien,
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC'
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		when 20 then 'Thanh toán riêng'
		end) as loaidoituong,
	ser.servicepricedate,
	degp.departmentgroupname as khoachidinh,
	de.departmentname as phongchidinh,
	mbp.userthuchien as userhisid,
	ncd.usercode,
	ncd.username,
	(case when vp.vienphistatus<>0 then 'Đã khám' 
			else (case when mbp.maubenhphamstatus=16 then 'Đã khám' end) 
	end) as trangthaikham,
	vp.patientid,
	vp.vienphiid,
	hsba.patientname,
	(case vp.doituongbenhnhanid 
			when 1 then 'BHYT'
			when 2 then 'Viện phí'
			when 3 then 'Dịch vụ'
			when 4 then 'Người nước ngoài'
			when 5 then 'Miễn phí'
			when 6 then 'Hợp đồng'
			end) as doituongbenhnhanid,
	vp.vienphidate,
	vp.vienphidate_ravien,
	vp.duyet_ngayduyet_vp,
	(case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as vienphistatus
	
FROM (select servicepriceid,servicepricecode,servicepricename,maubenhphamid,vienphiid,soluong,loaidoituong,departmentgroupid,departmentid,servicepricedate,
		(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai 
			else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end)
		end) as dongia 
		from serviceprice where bhyt_groupcode='01KB' "+lstdichvu_ser+tieuchi_ser+") ser
	inner join (select maubenhphamid,userid,maubenhphamstatus,(case when userthuchien=0 then userid else userthuchien end) as  userthuchien from maubenhpham where maubenhphamgrouptype=2 "+tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid
	inner join (select vienphiid,hosobenhanid,patientid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 "+tieuchi_hsba+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	left join (select userhisid,usercode,username from nhompersonnel) ncd on ncd.userhisid=mbp.userthuchien
	inner join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid
	inner join department de on de.departmentid=ser.departmentid;

	
	
	
	
	
	
	
	
	