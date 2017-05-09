--tools_serviceprice_pttt

CREATE TABLE IF NOT EXISTS tools_serviceprice_pttt ( 
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
CONSTRAINT tools_serviceprice_pttt_pkey PRIMARY KEY (servicepriceptttid) )

CREATE INDEX t_serviceprice_pttt_vienphiid_idx ON tools_serviceprice_pttt USING btree (vienphiid)
CREATE INDEX t_serviceprice_pttt_patientid_idx ON tools_serviceprice_pttt USING btree (patientid)
CREATE INDEX t_serviceprice_pttt_bhytid_idx ON tools_serviceprice_pttt USING btree (bhytid)
CREATE INDEX t_serviceprice_pttt_hosobenhanid_idx ON tools_serviceprice_pttt USING btree (hosobenhanid)
CREATE INDEX t_serviceprice_pttt_loaivienphiid_idx ON tools_serviceprice_pttt USING btree (loaivienphiid)
CREATE INDEX t_serviceprice_pttt_khoaravien_idx ON tools_serviceprice_pttt USING btree (khoaravien)
CREATE INDEX t_serviceprice_pttt_phongravien_idx ON tools_serviceprice_pttt USING btree (phongravien)
CREATE INDEX t_serviceprice_pttt_doituongbenhnhanid_idx ON tools_serviceprice_pttt USING btree (doituongbenhnhanid)
CREATE INDEX t_serviceprice_pttt_vienphidate_idx ON tools_serviceprice_pttt USING btree (vienphidate)
CREATE INDEX t_serviceprice_pttt_vienphidate_ravien_idx ON tools_serviceprice_pttt USING btree (vienphidate_ravien)
CREATE INDEX t_serviceprice_pttt_duyet_ngayduyet_vp_idx ON tools_serviceprice_pttt USING btree (duyet_ngayduyet_vp)
CREATE INDEX t_serviceprice_pttt_departmentid_idx ON tools_serviceprice_pttt USING btree (departmentid)
CREATE INDEX t_serviceprice_pttt_departmentgroupid_idx ON tools_serviceprice_pttt USING btree (departmentgroupid)
CREATE INDEX t_serviceprice_pttt_departmentgroup_huong_idx ON tools_serviceprice_pttt USING btree (departmentgroup_huong)
