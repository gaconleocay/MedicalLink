-- Table: ihs_servicespttt
--ngay 23/3/2018: them cot 
alter table ihs_servicespttt add money_ptttyeucau_bh double precision DEFAULT 0;
alter table ihs_servicespttt add money_ptttyeucau_vp double precision DEFAULT 0;
alter table ihs_servicespttt add money_vattu_ttrieng_bh double precision DEFAULT 0;
alter table ihs_servicespttt add money_vattu_ttrieng_vp double precision DEFAULT 0;

-- DROP TABLE ihs_servicespttt;

CREATE TABLE ihs_servicespttt
(
  servicepriceptttid serial NOT NULL,
  vienphiid integer,
  patientid integer,
  bhytid integer,
  hosobenhanid integer,
  loaivienphiid integer,
  vienphistatus integer,
  khoaravien integer,
  phongravien integer,
  doituongbenhnhanid integer,
  vienphidate timestamp without time zone,
  vienphidate_ravien timestamp without time zone,
  duyet_ngayduyet timestamp without time zone,
  vienphistatus_vp integer,
  duyet_ngayduyet_vp timestamp without time zone,
  vienphistatus_bh integer,
  duyet_ngayduyet_bh timestamp without time zone,
  bhyt_tuyenbenhvien integer,
  departmentid integer,
  departmentgroupid integer,
  departmentgroup_huong integer,
  money_khambenh_bh double precision,
  money_khambenh_vp double precision,
  money_xetnghiem_bh double precision,
  money_xetnghiem_vp double precision,
  money_cdha_bh double precision,
  money_cdha_vp double precision,
  money_tdcn_bh double precision,
  money_tdcn_vp double precision,
  money_pttt_bh double precision,
  money_pttt_vp double precision,
  money_mau_bh double precision,
  money_mau_vp double precision,
  money_giuongthuong_bh double precision,
  money_giuongthuong_vp double precision,
  money_giuongyeucau_bh double precision,
  money_giuongyeucau_vp double precision,
  money_nuocsoi_bh double precision,
  money_nuocsoi_vp double precision,
  money_xuatan_bh double precision,
  money_xuatan_vp double precision,
  money_diennuoc_bh double precision,
  money_diennuoc_vp double precision,
  money_vanchuyen_bh double precision,
  money_vanchuyen_vp double precision,
  money_khac_bh double precision,
  money_khac_vp double precision,
  money_phuthu_bh double precision,
  money_phuthu_vp double precision,
  money_thuoc_bh double precision,
  money_thuoc_vp double precision,
  money_vattu_bh double precision,
  money_vattu_vp double precision,
  money_vtthaythe_bh double precision,
  money_vtthaythe_vp double precision,
  money_dvktc_bh double precision,
  money_dvktc_vp double precision,
  money_chiphikhac double precision,
  money_hpngaygiuong double precision,
  money_hppttt double precision,
  money_hpdkpttt_gm_thuoc double precision,
  money_hpdkpttt_gm_vattu double precision,
  money_dkpttt_thuoc_bh double precision,
  money_dkpttt_thuoc_vp double precision,
  money_dkpttt_vattu_bh double precision,
  money_dkpttt_vattu_vp double precision,
  money_hppttt_goi_thuoc double precision,
  money_hppttt_goi_vattu double precision,
  CONSTRAINT ihs_servicespttt_pkey PRIMARY KEY (servicepriceptttid)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ihs_servicespttt
  OWNER TO postgres;

 CREATE INDEX ihs_servicespttt_servicepriceptttid_idx
  ON ihs_servicespttt
  USING btree
  (servicepriceptttid); 
  
-- Index: ihs_servicespttt_bhytid_idx

-- DROP INDEX ihs_servicespttt_bhytid_idx;

CREATE INDEX ihs_servicespttt_bhytid_idx
  ON ihs_servicespttt
  USING btree
  (bhytid);

-- Index: ihs_servicespttt_departmentgroup_huong_idx

-- DROP INDEX ihs_servicespttt_departmentgroup_huong_idx;

CREATE INDEX ihs_servicespttt_departmentgroup_huong_idx
  ON ihs_servicespttt
  USING btree
  (departmentgroup_huong);

-- Index: ihs_servicespttt_departmentgroupid_idx

-- DROP INDEX ihs_servicespttt_departmentgroupid_idx;

CREATE INDEX ihs_servicespttt_departmentgroupid_idx
  ON ihs_servicespttt
  USING btree
  (departmentgroupid);

-- Index: ihs_servicespttt_departmentid_idx

-- DROP INDEX ihs_servicespttt_departmentid_idx;

CREATE INDEX ihs_servicespttt_departmentid_idx
  ON ihs_servicespttt
  USING btree
  (departmentid);

-- Index: ihs_servicespttt_doituongbenhnhanid_idx

-- DROP INDEX ihs_servicespttt_doituongbenhnhanid_idx;

CREATE INDEX ihs_servicespttt_doituongbenhnhanid_idx
  ON ihs_servicespttt
  USING btree
  (doituongbenhnhanid);

-- Index: ihs_servicespttt_duyet_ngayduyet_vp_idx

-- DROP INDEX ihs_servicespttt_duyet_ngayduyet_vp_idx;

CREATE INDEX ihs_servicespttt_duyet_ngayduyet_vp_idx
  ON ihs_servicespttt
  USING btree
  (duyet_ngayduyet_vp);

-- Index: ihs_servicespttt_hosobenhanid_idx

-- DROP INDEX ihs_servicespttt_hosobenhanid_idx;

CREATE INDEX ihs_servicespttt_hosobenhanid_idx
  ON ihs_servicespttt
  USING btree
  (hosobenhanid);

-- Index: ihs_servicespttt_khoaravien_idx

-- DROP INDEX ihs_servicespttt_khoaravien_idx;

CREATE INDEX ihs_servicespttt_khoaravien_idx
  ON ihs_servicespttt
  USING btree
  (khoaravien);

-- Index: ihs_servicespttt_loaivienphiid_idx

-- DROP INDEX ihs_servicespttt_loaivienphiid_idx;

CREATE INDEX ihs_servicespttt_loaivienphiid_idx
  ON ihs_servicespttt
  USING btree
  (loaivienphiid);

-- Index: ihs_servicespttt_patientid_idx

-- DROP INDEX ihs_servicespttt_patientid_idx;

CREATE INDEX ihs_servicespttt_patientid_idx
  ON ihs_servicespttt
  USING btree
  (patientid);

-- Index: ihs_servicespttt_phongravien_idx

-- DROP INDEX ihs_servicespttt_phongravien_idx;

CREATE INDEX ihs_servicespttt_phongravien_idx
  ON ihs_servicespttt
  USING btree
  (phongravien);

-- Index: ihs_servicespttt_vienphidate_idx

-- DROP INDEX ihs_servicespttt_vienphidate_idx;

CREATE INDEX ihs_servicespttt_vienphidate_idx
  ON ihs_servicespttt
  USING btree
  (vienphidate);

-- Index: ihs_servicespttt_vienphidate_ravien_idx

-- DROP INDEX ihs_servicespttt_vienphidate_ravien_idx;

CREATE INDEX ihs_servicespttt_vienphidate_ravien_idx
  ON ihs_servicespttt
  USING btree
  (vienphidate_ravien);

-- Index: ihs_servicespttt_vienphiid_idx

-- DROP INDEX ihs_servicespttt_vienphiid_idx;

CREATE INDEX ihs_servicespttt_vienphiid_idx
  ON ihs_servicespttt
  USING btree
  (vienphiid);

-- Index: ihs_servicespttt_vienphistatus_vp_idx

-- DROP INDEX ihs_servicespttt_vienphistatus_vp_idx;

CREATE INDEX ihs_servicespttt_vienphistatus_vp_idx
  ON ihs_servicespttt
  USING btree
  (vienphistatus_vp);

