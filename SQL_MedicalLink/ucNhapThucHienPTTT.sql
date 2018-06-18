

--ngay 18/6/2018

select	row_number () over (order by ser.servicepricedate) as stt,
	coalesce(thuchien.thuchienptttid,0) as thuchienptttid,
	ser.servicepriceid,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	ser.medicalrecordid,
    vp.hosobenhanid,
	vp.doituongbenhnhanid,
	vp.loaivienphiid,
	vp.vienphistatus,
    mbp.maubenhphamid,
    bh.bhytid,
	bh.bhytcode,
	ser.servicepricecode,
	ser.servicepricename,
	ser.servicepricedate,
	ser.loaidoituong,
	ser.dongia,
	ser.soluong,
	serf.pttt_loaiid,
	(case serf.pttt_loaiid 
				when 1 then 'Phẫu thuật đặc biệt' 
				when 2 then 'Phẫu thuật loại 1' 
				when 3 then 'Phẫu thuật loại 2' 
				when 4 then 'Phẫu thuật loại 3' 
				when 5 then 'Thủ thuật đặc biệt' 
				when 6 then 'Thủ thuật loại 1' 
				when 7 then 'Thủ thuật loại 2' 
				when 8 then 'Thủ thuật loại 3' 
				else '' end) as pttt_loaiten,
	ser.departmentgroupid,
	degp.departmentgroupname,
	ser.departmentid,
	de.departmentname,
	mbp.userid as ngchidinhid,
	ngcd.username as ngchidinhname,
	thuchien.mochinhid,
	thuchien.moimochinhid,
	thuchien.bacsigaymeid,
	thuchien.moigaymeid,
	thuchien.ktvphumeid,
	thuchien.phu1id,
	thuchien.phu2id,
	thuchien.ktvhoitinhid,
	thuchien.ddhoitinhid,
	thuchien.ddhanhchinhid,
	thuchien.holyid,
	thuchien.dungcuvienid,
	thuchien.mota,
	thuchien.thuchienttdate,
	thuchien.nguoinhap,
	thuchien.nguoinhapname,
	thuchien.lastuserupdated,
	thuchien.lasttimeupdated
from (select servicepriceid,vienphiid,medicalrecordid,maubenhphamid,servicepricecode,servicepricename,servicepricedate,loaidoituong,soluong,departmentgroupid,departmentid, from serviceprice where 1=1 and bhyt_groupcode in ('06PTTT','07KTC') "+_tieuchi_ser+_lstPhongChonLayBC+") ser
	inner join (select maubenhphamid,userid from maubenhpham where maubenhphamgrouptype=4 "+_tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid
	inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4) serf on serf.servicepricecode=ser.servicepricecode
	inner join (select vienphiid,patientid,hosobenhanid,doituongbenhnhanid,loaivienphiid,vienphistatus,bhytid,vienphidate,vienphidate_ravien from vienphi where 1=1 "+_tieuchi_vp+_trangthai_vp+_doituongbenhnhanid+") vp on vp.vienphiid=ser.vienphiid
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 "+_tieuchi_hsba+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where 1=1 "+_tieuchi_bhyt+") bhyt on bhyt.bhytid=vp.bhytid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	inner join (select departmentid,departmentname from department where departmenttype in(2,3,9) "+_lstPhongChonLayBC+") de on de.departmentid=ser.departmentid
	left join nhompersonnel ngcd on ngcd.userhisid=mbp.userid 
	LEFT JOIN (select *
				from dblink('myconn_mel','select thuchienptttid,servicepriceid,mochinhid,moimochinhid,bacsigaymeid,moigaymeid,ktvphumeid,phu1id,phu2id,ktvhoitinhid,ddhoitinhid,ddhanhchinhid,holyid,dungcuvienid,mota,nguoinhap,thuchienttdate,lastuserupdated,lasttimeupdated from ml_thuchienpttt where 1=1 "+_tieuchi_thuchienpttt+"')
				as thuchien(thuchienptttid integer,servicepriceid integer,mochinhid integer,moimochinhid integer,bacsigaymeid integer,moigaymeid integer,ktvphumeid integer,phu1id integer,phu2id integer,ktvhoitinhid integer,ddhoitinhid integer,ddhanhchinhid integer,holyid integer,dungcuvienid integer,mota text,nguoinhap text,thuchienttdate timestamp without time zone,lastuserupdated text,lasttimeupdated timestamp without time zone)) thuchien on thuchien.servicepriceid=ser.servicepriceid;

	
	

	
---INSERT INTO
insert into ml_thuchienpttt(servicepriceid,vienphiid,patientid,patientname,medicalrecordid,hosobenhanid,maubenhphamid,bhytid,bhytcode,servicepricecode,servicepricename,servicepricedate,loaidoituong,dongia,soluong,pttt_loaiid,departmentgroupid,departmentid,ngchidinhid,mochinhid,moimochinhid,bacsigaymeid,moigaymeid,ktvphumeid,phu1id,phu2id,ktvhoitinhid,ddhoitinhid,ddhanhchinhid,holyid,dungcuvienid,mota,thuchienttdate,nguoinhap,lastuserupdated,lasttimeupdated) 
values()



CREATE TABLE ml_thuchienpttt
(
  thuchienptttid serial NOT NULL,
  servicepriceid integer,
  vienphiid integer,
  patientid integer,
  patientname text,
  medicalrecordid integer,
  hosobenhanid integer,
  --doituongbenhnhanid integer,
  --loaivienphiid integer,
  --vienphistatus integer,
  maubenhphamid integer,
  bhytid integer,
  bhytcode text,
  servicepricecode text,
  servicepricename text,
  servicepricedate timestamp without time zone,
  loaidoituong integer,
  dongia double precision default 0,
  soluong double precision default 0,
  pttt_loaiid integer,
  departmentgroupid integer DEFAULT 0,
  departmentid integer DEFAULT 0,
  ngchidinhid integer,
  mochinhid integer,
  moimochinhid integer,
  bacsigaymeid integer,
  moigaymeid integer,
  ktvphumeid integer,
  phu1id integer,
  phu2id integer,
  ktvhoitinhid integer,
  ddhoitinhid integer,
  ddhanhchinhid integer,
  holyid integer, 
  dungcuvienid integer,
  mota text,
  thuchienttdate timestamp without time zone,
  nguoinhap text,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone,
  CONSTRAINT ml_thuchienpttt_pkey PRIMARY KEY (thuchienptttid)
)