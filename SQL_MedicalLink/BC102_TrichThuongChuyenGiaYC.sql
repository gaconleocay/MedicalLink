--Báo cáo trích thưởng chuyên gia 
--ucBC102_TrichThuongChuyenGiaYC

--them nhom 
ALter table nhompersonnel add nhom_bcid integer; --1=Nhân viên hợp đồng; 2=Nhân viên bệnh viện
ALter table nhompersonnel add nhom_bcten text;



SELECT 
	(row_number() OVER (PARTITION BY ncd.nhom_bcid ORDER BY ncd.username)) as stt,
	mbp.userid as userhisid,
	ncd.usercode,
	ncd.username,
	ncd.nhom_bcid,
	ncd.nhom_bcten,
	sum(ser.soluong) as soluong,
	sum(ser.soluong*ser.dongia) as thanhtien,
	50 as tylehuong,
	sum(ser.soluong*ser.dongia*0.5) as tongtien,
	0 as tienthue,
	0 as thuclinh,
	'' as kynhan,
	0 as isgroup
FROM (select maubenhphamid,userid from maubenhpham where maubenhphamgrouptype=2 "+tieuchi_mbp+") mbp 
	inner join (select maubenhphamid,vienphiid,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
				from serviceprice where bhyt_groupcode='01KB' "+lstdichvu_ser+tieuchi_ser+") ser on ser.maubenhphamid=mbp.maubenhphamid
	inner join (select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	left join (select * from nhompersonnel where nhom_bcid>0) ncd on ncd.userhisid=mbp.userid
GROUP BY mbp.userid,ncd.usercode,ncd.username,ncd.nhom_bcid,ncd.nhom_bcten
ORDER BY ncd.username;










ALter table nhompersonnel add nhom_bcid integer; --1=Nhân viên hợp đồng; 2=Nhân viên bệnh viện
ALter table nhompersonnel add nhom_bcten text;


CREATE TABLE nhompersonnel
(
  NHANVIENID SERIAL NOT NULL,
  usercode text not null,
  username text,
  userpassword text,
  userstatus integer,
  usergnhom integer,
  usernote text,
  userhisid integer,
  usergnhom_name text,
  nhom_bcid integer,
  nhom_bcten text,
  CONSTRAINT nhompersonnel_pkey PRIMARY KEY (nhanvienid)
)


CREATE TABLE serviceprice
(
  servicepriceid serial NOT NULL,
  medicalrecordid integer,
  vienphiid integer DEFAULT 0,
  hosobenhanid integer DEFAULT 0,
  maubenhphamid integer,
  maubenhphamphieutype integer DEFAULT 0,
  servicepriceid_master integer DEFAULT 0,
  thuockhobanle integer DEFAULT 0,
  doituongbenhnhanid integer DEFAULT 0,
  loaidoituong_org integer DEFAULT 0,
  loaidoituong_org_remark text,
  loaidoituong integer DEFAULT 0,
  loaiduyetbhyt integer DEFAULT 0,
  loaidoituong_remark text,
  loaidoituong_userid integer DEFAULT 0,
  departmentid integer DEFAULT 0,
  departmentgroupid integer DEFAULT 0,
  servicepricecode text,
  servicepricename text,
  servicepricename_nhandan text,
  servicepricename_bhyt text,
  servicepricename_nuocngoai text,
  servicepricedate timestamp without time zone,
  servicepricestatus integer,
  servicepricedoer text,
  servicepricecomment text,
  servicepricemoney double precision,
  servicepricemoney_nhandan double precision,
  servicepricemoney_bhyt double precision,
  servicepricemoney_nuocngoai double precision,
  servicepricemoney_bhyt_tra double precision,
  servicepricemoney_miengiam double precision,
  servicepricemoney_danop double precision,
  servicepricemoney_miengiam_type integer default 0,
  billid_thutien integer DEFAULT 0,
  billid_hoantien integer DEFAULT 0,
  billid_clbh_thutien integer DEFAULT 0,
  billid_clbh_hoantien integer DEFAULT 0,
  billaccountid integer DEFAULT 0,
  soluong double precision,
  soluongbacsi double precision,
  huongdansudung text,
  version timestamp without time zone,
  loaipttt integer DEFAULT 0,
  soluongquyettoan double precision DEFAULT 0,
  servicepriceid_xuattoan double precision DEFAULT 0,
  daduyetthuchiencanlamsang integer DEFAULT 0,
  sync_flag integer,
  update_flag integer,
  servicepricemoney_bhyt_danop double precision,
  servicepricemoney_damiengiam double precision,
  loaidoituong_xuat integer,
  servicepricemoney_tranbhyt double precision,
  servicepricebhytdinhmuc text,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  bhyt_groupcode text,
  huongdanphathuoc text,
  servicepriceid_org integer,
  lankhambenh integer,
  vitrisinhthiet text,
  somanhsinhthiet text,
  stt_theodoithuoc integer,
  chiphidauvao double precision,
  chiphimaymoc double precision,
  chiphipttt double precision,
  mayytedbid integer,
  servicepriceid_thanhtoanrieng
  CONSTRAINT serviceprice_pkey PRIMARY KEY (servicepriceid)
)




