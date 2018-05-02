--Bao cao bieu chenh 21_ 2018
--ngay 26/2/2018

CREATE TABLE tools_servicerefchenh2018
(
  servicerefchenh2018id serial NOT NULL,
  servicepricecode text,
  servicepricecodeuser text,
  servicepricecodeuser_old text,
  servicepricecodeuser_new text,
  servicepricenamebhyt text,
  servicepricenamebhyt_old text,
  servicepricenamebhyt_new text,
  servicepricemoney_bhyt double precision DEFAULT 0,
  servicepricemoney_bhyt_tr13 double precision DEFAULT 0,
  servicepricemoney_bhyt_13 double precision DEFAULT 0,
  servicepricemoney_bhyt_17 double precision DEFAULT 0,
  servicepricemoney_vp_new double precision DEFAULT 0,
  servicepricemoney_vp_old double precision DEFAULT 0, 
  createusercode text,
  createusername text,
  createdate timestamp without time zone,
  CONSTRAINT tools_servicerefchenh2018_pkey PRIMARY KEY (servicerefchenh2018id)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX servicerefchenh2018_servicepricecode_idx ON tools_servicerefchenh2018 USING btree (servicepricecode);
CREATE INDEX servicerefchenh2018_servicepricecodeuser_idx ON tools_servicerefchenh2018 USING btree (servicepricecodeuser);
CREATE INDEX servicerefchenh2018_servicepricecodeuser_old_idx ON tools_servicerefchenh2018 USING btree (servicepricecodeuser_old);
CREATE INDEX servicerefchenh2018_servicepricecodeuser_new_idx ON tools_servicerefchenh2018 USING btree (servicepricecodeuser_new);

----
CREATE TABLE tools_datachenh2018tmp
(
  datachenh2018tmpid serial NOT NULL,
  loaivienphiid integer,
  doituongbenhnhanid integer,
  servicepricecodeuser text,
  bhyt_groupcode text,
  servicepricecode text,
  servicepricenamebhyt text,
  servicepricemoney double precision DEFAULT 0,
  soluong double precision DEFAULT 0,
  tyle double precision,
  thanhtien double precision DEFAULT 0,
  ngoaitinh text,
  createusercode text, 
  createdate timestamp without time zone,
  CONSTRAINT tools_datachenh2018tmp_pkey PRIMARY KEY (datachenh2018tmpid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX tools_datachenh2018tmp_servicepricecode_idx ON tools_datachenh2018tmp USING btree (servicepricecode);
CREATE INDEX tools_datachenh2018tmp_servicepricecodeuser_idx ON tools_datachenh2018tmp USING btree (servicepricecodeuser);
CREATE INDEX tools_datachenh2018tmp_createdate_idx ON tools_datachenh2018tmp USING btree (createdate);
CREATE INDEX tools_datachenh2018tmp_createusercode_idx ON tools_datachenh2018tmp USING btree (createusercode);


-----SQL bao cao Tmp
--khong tach ngoai tinh - noi tinh (ap dung cho doi tuong<>BHYT)
--ngay 19/4 sua gia tien 1/7, tự tính ra tỷ lệ thanh toán theo đơn giá trên DM và đơn giá trong Ser
SELECT
	'' as ngoaitinh
	vp.loaivienphiid, 
	vp.doituongbenhnhanid,
	serf.servicepricecodeuser,
	serf.bhyt_groupcode,
	serf.servicepricecode,
	serf.servicepricenamebhyt,
	cast(coalesce(serf.servicepricefee,0) as numeric) as servicepricefee,
	ser.servicepricemoney,
	sum((case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong,
	((ser.servicepricemoney/cast(coalesce(serf.servicepricefee,0) as numeric)) * 100) as tyle,
	sum(ser.servicepricemoney*(case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien
FROM (select * from vienphi where 1=1 "+_tieuchi_vp+_doituongBN+_trangthai_vp+_loaivienphi+") vp
	inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') "+_tieuchi_ser+_loaidoituong+") ser on ser.vienphiid=vp.vienphiid
	inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode
GROUP BY vp.loaivienphiid,
	vp.doituongbenhnhanid,
	serf.servicepricecodeuser,
	serf.bhyt_groupcode,
	serf.servicepricecode,
	serf.servicepricenamebhyt,
	serf.servicepricefee,
	ser.servicepricemoney,
	ser.lankhambenh,
	ser.loaingaygiuong,
	ser.loaipttt;
	

------==========
--Ap dung cho doi tuong BHYT (tach ngoai tinh, noi tinh)
--ngay 19/4 sua gia tien 1/7, tự tính ra tỷ lệ thanh toán theo đơn giá trên DM và đơn giá trong Ser
SELECT
	'A.Nội tỉnh' as ngoaitinh,
	vp.loaivienphiid, 
	vp.doituongbenhnhanid,
	serf.servicepricecodeuser,
	serf.bhyt_groupcode,
	serf.servicepricecode,
	serf.servicepricenamebhyt,
	cast(coalesce(serf.servicepricefeebhyt,0) as numeric) as servicepricefee,
	ser.servicepricemoney_bhyt as servicepricemoney,
	sum((case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong,
	((ser.servicepricemoney_bhyt/cast(coalesce(serf.servicepricefeebhyt,0) as numeric)) * 100) as tyle,
	sum(ser.servicepricemoney_bhyt*(case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien
FROM (select * from vienphi where 1=1 "+_tieuchi_vp+_doituongBN+_trangthai_vp+_loaivienphi+") vp
	inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') "+_tieuchi_ser+_loaidoituong+") ser on ser.vienphiid=vp.vienphiid
	inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode
	inner join (select bhytid from bhyt where substring(bhytcode,4,2)='"+this.maTinhThanh+"') bh on bh.bhytid=vp.bhytid
GROUP BY vp.loaivienphiid,
	vp.doituongbenhnhanid,
	serf.servicepricecodeuser,
	serf.bhyt_groupcode,
	serf.servicepricecode,
	serf.servicepricenamebhyt,
	serf.servicepricefeebhyt,
	ser.servicepricemoney_bhyt,
	ser.lankhambenh,
	ser.loaingaygiuong,
	ser.loaipttt
UNION ALL
SELECT
	'B.Ngoại tỉnh' as ngoaitinh,
	vp.loaivienphiid, 
	vp.doituongbenhnhanid,
	serf.servicepricecodeuser,
	serf.bhyt_groupcode,
	serf.servicepricecode,
	serf.servicepricenamebhyt,
	cast(coalesce(serf.servicepricefeebhyt,0) as numeric) as servicepricefee,
	ser.servicepricemoney_bhyt as servicepricemoney,
	sum((case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong,
	((ser.servicepricemoney_bhyt/cast(coalesce(serf.servicepricefeebhyt,0) as numeric)) * 100) as tyle,
	sum(ser.servicepricemoney_bhyt*(case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien
FROM (select * from vienphi where 1=1 "+_tieuchi_vp+_doituongBN+_trangthai_vp+_loaivienphi+") vp
	inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') "+_tieuchi_ser+_loaidoituong+") ser on ser.vienphiid=vp.vienphiid
	inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode
	inner join (select bhytid from bhyt where substring(bhytcode,4,2)<>'"+this.maTinhThanh+"') bh on bh.bhytid=vp.bhytid
GROUP BY vp.loaivienphiid,
	vp.doituongbenhnhanid,
	serf.servicepricecodeuser,
	serf.bhyt_groupcode,
	serf.servicepricecode,
	serf.servicepricenamebhyt,
	serf.servicepricefeebhyt,
	ser.servicepricemoney_bhyt,
	ser.lankhambenh,
	ser.loaingaygiuong,
	ser.loaipttt;




	
	
--Bao cao lay du lieu chenh
--ngay 14/3/2018: thay doi khoa chinh=servicepricecode; them ngoai tinh
--ngay 19/4 thay doi ty le


SELECT (row_number() OVER (PARTITION BY O.tennhom_bhyt ORDER BY O.ngoaitinh,O.servicepricenamebhyt)) as stt, O.*
FROM
	(SELECT distinct chenh.datachenh2018tmpid,
		chenh.ngoaitinh,
		chenh.servicepricecode,
		chenh.bhyt_groupcode,
		(case when chenh.bhyt_groupcode='01KB' then 'I - Công khám'
				when chenh.bhyt_groupcode='12NG' then 'II - Ngày giường'
				when chenh.bhyt_groupcode='03XN' then 'III - Xét nghiệm'
				when chenh.bhyt_groupcode in ('04CDHA','05TDCN') then 'IV - Chẩn đoán hình ảnh'
				when chenh.bhyt_groupcode='06PTTT' then 'V - Phẫu thuật thủ thuật'
				when chenh.bhyt_groupcode='07KTC' then 'VI - Dịch vụ KTC'
				else 'VII - Khác' end
			) as tennhom_bhyt,
		chenh.servicepricecodeuser,
		chenh.servicepricenamebhyt,
		chenh.tyle,
		chenh.soluong,
		sef.servicepricemoney_bhyt_tr13 as giabhyt_truoc13,
		(chenh.soluong*sef.servicepricemoney_bhyt_tr13*(chenh.tyle/100.0)) as thanhtien_truoc13,
		sef.servicepricemoney_bhyt_13 as giabhyt_13,
		(chenh.soluong*sef.servicepricemoney_bhyt_13*(chenh.tyle/100.0)) as thanhtien_13,
		chenh.servicepricemoney as dongia,
		chenh.thanhtien as thanhtien,
		sef.servicepricemoney_bhyt_17 as giabhyt_17,
		(chenh.soluong*sef.servicepricemoney_bhyt_17*(chenh.tyle/100.0)) as thanhtien_17,
		((coalesce(sef.servicepricemoney_bhyt_17,0)-coalesce(sef.servicepricemoney_bhyt_13,0))*chenh.soluong*(chenh.tyle/100.0)) as chenh_17_13,
		((coalesce(sef.servicepricemoney_bhyt_17,0)-coalesce(sef.servicepricemoney_bhyt_tr13,0))*chenh.soluong*(chenh.tyle/100.0)) as chenh_17_truoc13,
		--(chenh.thanhtien-(chenh.soluong*coalesce(sef.servicepricemoney_bhyt_13,0)*(chenh.tyle/100.0))) as chenh_17_13,
		--(chenh.thanhtien-(chenh.soluong*coalesce(sef.servicepricemoney_bhyt_tr13,0)*(chenh.tyle/100.0))) as chenh_17_truoc13,
		'0' as isgroup
	FROM tools_datachenh2018tmp chenh
		left join tools_servicerefchenh2018 sef on sef.servicepricecode=chenh.servicepricecode
	WHERE chenh.createdate='"+_createdate+"' and chenh.createusercode='"+Base.SessionLogin.SessionUsercode+"') O
WHERE O.soluong>0;	
	




-----==============nháp]
SELECT O.*, 
	(case when servicepricefee>0 then ((O.servicepricemoney/O.servicepricefee) * 100) else 100 end) as tyle
FROM
(SELECT '' as ngoaitinh, 
vp.loaivienphiid, 
vp.doituongbenhnhanid, 
serf.servicepricecodeuser, 
serf.bhyt_groupcode, 
serf.servicepricecode, 
serf.servicepricenamebhyt, 
(case when ser.servicepricemoney>ser.servicepricemoney_nhandan then (case when serf.servicepricefee='' then 0 else cast(coalesce(serf.servicepricefee,'0') as double precision) end) else (case when serf.servicepricefeenhandan='' then 0 else cast(coalesce(serf.servicepricefeenhandan,'0') as double precision) end) end) as servicepricefee, 
(case when ser.servicepricemoney>ser.servicepricemoney_nhandan then ser.servicepricemoney else ser.servicepricemoney_nhandan end) as servicepricemoney,
sum((case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as soluong, 
sum(ser.servicepricemoney*(case when vp.loaivienphiid=0 and serf.bhyt_groupcode='01KB' and ser.lankhambenh>0 then 0 else ser.soluong end)) as thanhtien 
FROM (select * from vienphi where 1=1  and doituongbenhnhanid<>1  and vienphistatus_vp=1 ) vp 
inner join (select * from serviceprice where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')  and servicepricedate between '2017-01-01 00:00:00' and '2017-01-04 23:59:59' ) ser on ser.vienphiid=vp.vienphiid 
inner join (select * from servicepriceref where bhyt_groupcode in ( '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC')) serf on serf.servicepricecode=ser.servicepricecode 
GROUP BY vp.loaivienphiid, 
vp.doituongbenhnhanid, 
serf.servicepricecodeuser, 
serf.bhyt_groupcode, 
serf.servicepricecode, 
serf.servicepricenamebhyt, 
serf.servicepricefee,
serf.servicepricefeenhandan,
ser.servicepricemoney, 
ser.servicepricemoney_nhandan,
ser.lankhambenh, 
ser.loaingaygiuong, 
ser.loaipttt) O;







