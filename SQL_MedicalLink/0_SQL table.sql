
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
  CONSTRAINT ml_mayxnchiphi_pkey PRIMARY KEY (mayxndmxncpid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX mayxnchiphi_mayxn_ma_idx ON ml_mayxnchiphi USING btree (mayxn_ma);
CREATE INDEX mayxnchiphi_servicepricecode_idx ON ml_mayxnchiphi USING btree (servicepricecode);







