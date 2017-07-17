-- Table: nhompersonnel

-- DROP TABLE nhompersonnel;

CREATE TABLE nhompersonnel
(
  nhanvienid serial NOT NULL,
  usercode text NOT NULL,
  username text,
  userpassword text,
  userstatus integer,
  usergnhom integer,
  usernote text,
  userhisid integer,
  CONSTRAINT nhompersonnel_pkey PRIMARY KEY (nhanvienid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE nhompersonnel
  OWNER TO postgres;

-- Index: nhompersonnel_usercode_idx

-- DROP INDEX nhompersonnel_usercode_idx;

CREATE INDEX nhompersonnel_usercode_idx
  ON nhompersonnel
  USING btree
  (usercode COLLATE pg_catalog."default");

-- Index: nhompersonnel_userhisid_idx

-- DROP INDEX nhompersonnel_userhisid_idx;

CREATE INDEX nhompersonnel_userhisid_idx
  ON nhompersonnel
  USING btree
  (userhisid);

/*
insert into   nhompersonnel
select * from tools_tblnhanvien
  
  update nhompersonnel set usernote=''
  
  */
  
  
  