--Bảng dữ liệu SV_BaoCao 2019
--Lưu trữ dữ liệu viện phí 




---=========Table: his_vienphiid

CREATE TABLE his_vienphi
(
  hisvienphiid serial NOT NULL,
  vienphiid integer,
  status integer,
  isexecute integer,
  CONSTRAINT his_vienphi_pkey PRIMARY KEY (hisvienphiid)
)
WITH (
  OIDS=FALSE
);

CREATE INDEX his_vienphi_vienphiid_idx ON his_vienphi USING btree (vienphiid);




GRANT ALL ON TABLE his_vienphi TO takevp;
GRANT ALL ON TABLE his_vienphi_hisvienphiid_seq TO takevp;