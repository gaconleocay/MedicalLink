
alter table departmentgroup add cumthutien text;
CREATE INDEX departmentgroup_cumthutien_idx ON departmentgroup USING btree (cumthutien);


-- Table: tools_vienphi_tltt
--drop table tools_billedit

CREATE TABLE tools_billedit
(
  billeditid serial NOT NULL,
  billid integer,
  billcode text,
  billgroupcode text,
  patientid integer DEFAULT 0,
  vienphiid integer DEFAULT 0,
  hosobenhanid integer DEFAULT 0,
  loaiphieuthuid integer,
  userid integer DEFAULT 0,
  username text,
  billdate timestamp without time zone,
  cumthutien text,
  departmentgroupid integer,
  departmentgroupname text,
  departmentid integer,  
  departmentname text,  
  patientname text,  
  userid_nhan integer DEFAULT 0,
  username_nhan text,  	
  departmentgroupid_nhan integer,
  departmentgroupname_nhan text,
  departmentid_nhan integer,    
  departmentname_nhan text,    
  sotien double precision,
  createusercode text,
  createdate timestamp without time zone,
  CONSTRAINT tools_billedit_pkey PRIMARY KEY (billeditid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX billedit_billid_idx ON tools_billedit USING btree (billid);
CREATE INDEX billedit_vienphiid_idx ON tools_billedit USING btree (vienphiid);
CREATE INDEX billedit_departmentgroupid_idx ON tools_billedit USING btree (departmentgroupid);
CREATE INDEX billedit_departmentgroupid_nhan_idx ON tools_billedit USING btree (departmentgroupid_nhan);
CREATE INDEX billedit_createdate_idx ON tools_billedit USING btree (createdate);


---=========Table: tools_duyet_pttt
--drop table tools_duyet_pttt

CREATE TABLE tools_duyet_pttt
(
  duyetptttid serial NOT NULL,
  servicepriceid integer,
  vienphiid integer,
  maubenhphamid integer,
  bhyt_groupcode text,
  duyetpttt_stt integer,
  gui_usercode text,
  gui_username text,
  gui_date timestamp without time zone,
  tiepnhan_usercode text,
  tiepnhan_username text,
  tiepnhan_date timestamp without time zone,
  duyet_usercode text,
  duyet_username text,
  duyet_date timestamp without time zone,  
  khoa_usercode text,
  khoa_username text,
  khoa_date timestamp without time zone,
  CONSTRAINT tools_duyet_pttt_pkey PRIMARY KEY (duyetptttid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX duyet_pttt_servicepriceid_idx ON tools_duyet_pttt USING btree (servicepriceid);
CREATE INDEX duyet_pttt_vienphiid_idx ON tools_duyet_pttt USING btree (vienphiid);
CREATE INDEX duyet_pttt_bhyt_groupcode_idx ON tools_duyet_pttt USING btree (bhyt_groupcode);
CREATE INDEX duyet_pttt_maubenhphamid_idx ON tools_duyet_pttt USING btree (maubenhphamid);
CREATE INDEX duyet_pttt_duyetpttt_stt_idx ON tools_duyet_pttt USING btree (duyetpttt_stt);
CREATE INDEX duyet_pttt_gui_usercode_idx ON tools_duyet_pttt USING btree (gui_usercode);
CREATE INDEX duyet_pttt_tiepnhan_usercode_idx ON tools_duyet_pttt USING btree (tiepnhan_usercode);
CREATE INDEX duyet_pttt_duyet_usercode_idx ON tools_duyet_pttt USING btree (duyet_usercode);
CREATE INDEX duyet_pttt_khoa_usercode_idx ON tools_duyet_pttt USING btree (khoa_usercode);




---=========Table: tools_bc_tntt
--drop table tools_bc_tntt

CREATE TABLE tools_bc_tntt
(
  bctnttid serial NOT NULL,
  stt text,
  noi_dung_code text,
  noi_dung_name text,
  isgroup integer,
  CONSTRAINT tools_bc_tntt_pkey PRIMARY KEY (bctnttid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX bc_tntt_noi_dung_code_idx ON tools_bc_tntt USING btree (noi_dung_code);





---=========Table: ml_mayxnkhuvuc
--drop table ml_mayxnkhuvuc

CREATE TABLE ml_mayxnkhuvuc
(
  MAYXNKHUVUCID serial not null,
  mayxn_ma integer,
  mayxn_ten text,
  khuvuc_ma text,
  khuvuc_ten text,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone,
  CONSTRAINT ml_mayxnkhuvuc_pkey PRIMARY KEY (mayxnkhuvucid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX mayxnkhuvuc_mayxn_ma_idx ON ml_mayxnkhuvuc USING btree (mayxn_ma);
CREATE INDEX mayxnkhuvuc_khuvuc_ma_idx ON ml_mayxnkhuvuc USING btree (khuvuc_ma);




---=========Table: ml_mayxnchiphi
--drop table ml_mayxnchiphi

CREATE TABLE ml_mayxnchiphi
(
  mayxndmxncpid serial NOT NULL,
  mayxn_ma integer,
  mayxn_ten text,
  servicepricecode text,
  servicepricename text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepriceunit text,
  servicepricefee double precision default 0,
  servicepricefeenhandan double precision default 0,
  servicepricefeebhyt double precision default 0,
  servicepricefeenuocngoai double precision default 0,
  cp_hoachat double precision default 0,
  cp_haophixn double precision default 0,
  cp_luong double precision default 0,
  cp_diennuoc double precision default 0,
  cp_khmaymoc double precision default 0,
  cp_khxaydung double precision default 0,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone,
  nhombc_ma text,
  CONSTRAINT ml_mayxnchiphi_pkey PRIMARY KEY (mayxndmxncpid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX mayxnchiphi_mayxn_ma_idx ON ml_mayxnchiphi USING btree (mayxn_ma);
CREATE INDEX mayxnchiphi_servicepricecode_idx ON ml_mayxnchiphi USING btree (servicepricecode);
CREATE INDEX mayxnchiphi_nhombc_ma_idx ON ml_mayxnchiphi USING btree (nhombc_ma);

alter table ml_mayxnchiphi add nhombc_ma text


---=========Table: ml_mayxnnhombc
--drop table ml_mayxnnhombc

CREATE TABLE ml_mayxnnhombc
(
  mayxnnhombcid serial not null,
  nhombc_ma text,
  nhombc_ten text,
  istrakq integer default 1,
   ghichu text,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone,
  CONSTRAINT ml_mayxnnhombc_pkey PRIMARY KEY (mayxnnhombcid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX mayxnnhombc_nhombc_ma_idx ON ml_mayxnnhombc USING btree (nhombc_ma);
CREATE INDEX mayxnnhombc_istrakq_idx ON ml_mayxnnhombc USING btree (istrakq);




---=========Table: ml_thuchienpttt
--drop table ml_thuchienpttt

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
  nguoinhap text,
  thuchienttdate timestamp without time zone,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone,
  CONSTRAINT ml_thuchienpttt_pkey PRIMARY KEY (thuchienptttid)
)

CREATE INDEX thuchienpttt_servicepriceid_idx ON ml_thuchienpttt USING btree (servicepriceid);
CREATE INDEX thuchienpttt_vienphiid_idx ON ml_thuchienpttt USING btree (vienphiid);
CREATE INDEX thuchienpttt_medicalrecordid_idx ON ml_thuchienpttt USING btree (medicalrecordid);
CREATE INDEX thuchienpttt_hosobenhanid_idx ON ml_thuchienpttt USING btree (hosobenhanid);
CREATE INDEX thuchienpttt_maubenhphamid_idx ON ml_thuchienpttt USING btree (maubenhphamid);
CREATE INDEX thuchienpttt_servicepricecode_idx ON ml_thuchienpttt USING btree (servicepricecode);
CREATE INDEX thuchienpttt_pttt_loaiid_idx ON ml_thuchienpttt USING btree (pttt_loaiid);
CREATE INDEX thuchienpttt_departmentgroupid_idx ON ml_thuchienpttt USING btree (departmentgroupid);
CREATE INDEX thuchienpttt_departmentid_idx ON ml_thuchienpttt USING btree (departmentid);
CREATE INDEX thuchienpttt_patientid_idx ON ml_thuchienpttt USING btree (patientid);
CREATE INDEX thuchienpttt_servicepricedate_idx ON ml_thuchienpttt USING btree (servicepricedate);
CREATE INDEX thuchienpttt_bhytid_idx ON ml_thuchienpttt USING btree (bhytid);





-- DROP TABLE tools_tbluser_medicinestore;

CREATE TABLE tools_tbluser_rpt13
(
  rpt13id serial NOT NULL,
  usercode text,
  username text,
  departmentgroupid integer,
  departmentgroupcode text,
  departmentgroupname text,
  iskhoagui integer,
  iskhoatra integer,
  CONSTRAINT tools_tbluser_rpt13_pkey PRIMARY KEY (rpt13id)
);

CREATE INDEX tbluser_rpt13_usercode_idx ON tools_tbluser_rpt13 USING btree (usercode);
CREATE INDEX tbluser_rpt13_departmentgroupid_idx ON tools_tbluser_rpt13 USING btree (departmentgroupid);



select departmentgroupid,departmentgroupcode,departmentgroupname from tools_tbluser_rpt13 where iskhoagui=1 and usercode='"++"';










