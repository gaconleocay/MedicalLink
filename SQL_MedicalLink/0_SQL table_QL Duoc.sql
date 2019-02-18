--Table Module QL Dược
--O2SPharma

CREATE TABLE pm_medicinestore
(
  medicinestoreid serial NOT NULL,
  medicinestoretype integer,
  medicinestorecode text,
  medicinestorename text,
  thukhocode text,
  remark text,
  loaidoituongbenhnhan integer DEFAULT 0,
  islock integer,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone NOT NULL DEFAULT now(),
  CONSTRAINT pm_medicinestore_pkey PRIMARY KEY (medicinestoreid)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE pm_medicinestore OWNER TO postgres;

CREATE INDEX pm_medicinestore_medicinestorecode_idx ON pm_medicinestore USING btree (medicinestorecode);
CREATE INDEX pm_medicinestore_medicinestoretype_idx ON pm_medicinestore USING btree (medicinestoretype);

-------------

CREATE TABLE pm_medicineref
(
  medicinerefid serial NOT NULL,
  medicinecode text,
  medicinegroupcode text,
  --medicinereftype integer,
  datatype integer,
  medicinetype integer, --loaihanghoa
  medicinename text,
    medicinename_byt text,
  medicinerefid_org integer DEFAULT 0,
  --tenkhoahoc text,
  donvitinh text,
  quycachdonggoi text,
  hangsanxuatid integer,
  nuocsanxuatid integer,
  nhacungcapid integer,
  nongdo text,
  hamluong text,--lieuluong
  hoatchat text,
  mahoatchat text,
  dangdung text,
  huongdansudung text,
  soluongngay double precision,
  gianhap double precision DEFAULT 0,
  giaban double precision DEFAULT 0,
  vatnhap double precision DEFAULT 0,
  vatban double precision DEFAULT 0,
  servicepricefee double precision DEFAULT 0,
  canhbaosoluong integer DEFAULT 0,
  canhbaohsd integer DEFAULT 0,
  solo text,
  sodangky text,
  hansudung timestamp without time zone,
  bietduoc text,
  --atc text,
  --tylehuhao double precision,
  bhyt_groupcode text,
  --isthuockhangsinh integer,
  --tuongtacthuoc text,
  medicinecodeuser text,
  --benhvienapthau text,
  --iskhongnhapmoi integer,
  nhomquyche text,
  nhombaocao text,
  nhomduocly text,
  nhomtieuduocly text,
  nhomquanly text,
  nhomnghiencuu text,
  nhomabcven text,
  --stt_thuoc_chuyeu integer,
  --servicepricebhytquydoi text,
  --servicepricebhytquydoi_tt text,
  goithau text,
  namcungung text,
  --thuhoivolo text,
  --isthuhoivolo integer,
  --ischephamyhoccotruyen integer,
  --isvithuocyhoccotruyen integer,
  --isthuoctanduoc integer,
  stt_dauthau text,
  --nguonchuongtrinhid integer,
  --ischolanhdaoduyet integer,
  --medicinestorebillid integer DEFAULT 0,
  --sochungtu text,
  --ngaychungtu timestamp without time zone,
  --ngaychungtu2 text,
  --danhsttdungthuoc integer,
  stt_thuoc_tt40 integer,
  stt_thuoc_tt40text text,
  --servicelock integer,
  --islock_sql integer,
  quyetdinhtrungthau text,
  nhomthau text,
  --medicinename_khoahoc text,
  isremove integer,
  lastuserupdated text,
  lasttimeupdated timestamp without time zone NOT NULL DEFAULT now(),
  CONSTRAINT pm_medicineref_pkey PRIMARY KEY (medicinerefid)
)
WITH (
  OIDS=TRUE
);
ALTER TABLE pm_medicineref OWNER TO postgres;

CREATE INDEX pm_medicineref_medicinecode_idx ON pm_medicineref USING btree (medicinecode);
CREATE INDEX pm_medicineref_medicinegroupcode_idx ON pm_medicineref USING btree (medicinegroupcode);
CREATE INDEX pm_medicineref_medicinetype_idx ON pm_medicineref USING btree (medicinetype);
CREATE INDEX pm_medicineref_datatype_idx ON pm_medicineref USING btree (datatype);
CREATE INDEX pm_medicineref_bhyt_groupcode_idx ON pm_medicineref USING btree (bhyt_groupcode);
CREATE INDEX pm_medicineref_isremove_idx ON pm_medicineref USING btree (isremove);
CREATE INDEX pm_medicineref_medicinerefid_org_idx ON pm_medicineref USING btree (medicinerefid_org);
CREATE INDEX pm_medicineref_medicinecodeuser_idx ON pm_medicineref USING btree (medicinecodeuser);






