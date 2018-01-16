
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

