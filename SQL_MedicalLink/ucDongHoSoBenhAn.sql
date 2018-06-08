---Dong ho so benh an
--ucDongHoSoBenhAn

alter table tools_tbluser add userhisid integer;

--ngay 31/5/2018

SELECT row_number () over (order by vp.vienphidate) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.hosobenhanid,
	hsba.patientname,
	TO_CHAR(hsba.birthday,'dd/MM/yyyy') as birthday,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	degp.departmentgroupname,
	de.departmentname
FROM (select vienphiid,patientid,hosobenhanid,departmentgroupid,departmentid,vienphidate,vienphidate_ravien,vienphistatus from vienphi where 1=1 "+_tieuchi_vp+_loaivienphiid+_doituongbenhnhanid+_lstPhongcheck+_vienphistatus+") vp
	inner join (select hosobenhanid,patientname,birthday from hosobenhan where 1=1 "+_tieuchi_hsba+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join departmentgroup degp on degp.departmentgroupid=vp.departmentgroupid
	inner join department de on de.departmentid=vp.departmentid;
	
	
	
---Chi tiet
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
	mbp.userid as userhisid,
	ncd.usercode,
	ncd.username,
	(case when billid_thutien<>0 or billid_clbh_thutien<>0 then 'Đã thu tiền' end) as trangthaithutien
FROM (select servicepriceid,servicepricecode,servicepricename,maubenhphamid,vienphiid,soluong,loaidoituong,departmentgroupid,departmentid,servicepricedate,billid_thutien,billid_clbh_thutien,
		(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai 
			else (case when loaidoituong=0 then servicepricemoney_bhyt 
						when loaidoituong=1 then servicepricemoney_nhandan
						else servicepricemoney end)
		end) as dongia 
		from serviceprice where vienphiid="+_vienphiid+") ser
	inner join (select maubenhphamid,userid,maubenhphamstatus from maubenhpham where vienphiid="+_vienphiid+") mbp on mbp.maubenhphamid=ser.maubenhphamid
	left join (select userhisid,usercode,username from nhompersonnel) ncd on ncd.userhisid=mbp.userid
	inner join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid
	inner join department de on de.departmentid=ser.departmentid;

--========== UPDATE HSBA + Duyet VP
--ngay 24/5/2018

UPDATE vienphi SET
	vienphistatus=1,
	vienphidate_ravien='"+_dongHSTime_EndDay+"',
	chandoanravien='NONE',
	chandoanravien_code='NONE',
	vienphistatus_bh=1,
	duyet_ngayduyet_bh='"+_dongHSTime_EndDay+"',
	duyet_nguoiduyet_bh='"+Base.SessionLogin.SessionUserHISID+"',
	duyet_sothutuduyet_bh=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='"+Base.SessionLogin.SessionUserHISID+"' and TO_CHAR(sothutudate,'yyyyMMdd')='"+_dongHSTime_long+"'),
	vienphistatus_vp=1,
	duyet_ngayduyet_vp='"+_dongHSTime_EndDay+"',
	duyet_nguoiduyet_vp='"+Base.SessionLogin.SessionUserHISID+"',
	duyet_sothutuduyet_vp=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='"+ Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='"+_dongHSTime_long+"'),
	medicalrecordid_end=(select medicalrecordid from medicalrecord where vienphiid='"+_vienphiid+"' order by medicalrecordid desc limit 1),
	vienphidate_ravien_update='"+_dongHSTime+"'
WHERE vienphiid='"+_vienphiid+"';

UPDATE hosobenhan SET
	isdownloaded=1,
	hosobenhanstatus=1,
	xutrikhambenhid=7,
	hosobenhandate_ravien='"+_dongHSTime_EndDay+"',
	chandoanravien_code='NONE',
	chandoanravien='NONE'
WHERE hosobenhanid='"+_hosobenhanid+"';

UPDATE medicalrecord SET
	medicalrecordstatus=99,
	thoigianravien='"+_dongHSTime_EndDay+"',
	chandoanravien='NONE',
	chandoanravien_code='NONE',
	xutrikhambenhid=7,
	medicalrecordremark='Đóng bệnh án tự động'
WHERE medicalrecordid=(select medicalrecordid from medicalrecord where vienphiid='"+_vienphiid+"' order by medicalrecordid desc limit 1);

UPDATE sothutuphongkham SET 
sothutustatus=4 
WHERE medicalrecordid=(select medicalrecordid from medicalrecord where vienphiid='"+_vienphiid+"' order by medicalrecordid desc limit 1);



--==========Duyet VP
--ngay 31/5/2018

UPDATE vienphi SET
	vienphistatus=1,
	vienphistatus_bh=1,
	duyet_ngayduyet_bh='"+_dongHSTime_EndDay+"',
	duyet_nguoiduyet_bh='"+Base.SessionLogin.SessionUserHISID+"',
	duyet_sothutuduyet_bh=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='"+Base.SessionLogin.SessionUserHISID+"' and TO_CHAR(sothutudate,'yyyyMMdd')='"+_dongHSTime_long+"'),
	vienphistatus_vp=1,
	duyet_ngayduyet_vp='"+_dongHSTime_EndDay+"',
	duyet_nguoiduyet_vp='"+Base.SessionLogin.SessionUserHISID+"',
	duyet_sothutuduyet_vp=(select coalesce(max(sothutunumber),1) as stt_duyetvp from sothutuduyetvienphi where userid='"+ Base.SessionLogin.SessionUserHISID + "' and TO_CHAR(sothutudate,'yyyyMMdd')='"+_dongHSTime_long+"')
WHERE vienphiid='"+_vienphiid+"';















------====== TEST
select * from vienphi where vienphiid=1162969;
select * from hosobenhan where hosobenhanid=1157063;
select * from medicalrecord where vienphiid=1162969;


- BN Viện phí Ngoại trú lấy tiêu chí = đã duyệt VP hoặc chưa duyệt thì chỉ lấy dv đã thu tiền

-----



 

