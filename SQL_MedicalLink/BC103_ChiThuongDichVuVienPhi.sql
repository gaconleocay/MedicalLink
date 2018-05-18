--Báo cáo CHI THƯỞNG DỊCH VỤ VIỆN PHÍ														
--ucBC103_ChiThuongDichVuVienPhi

select 
from (select from serviceprice where bhyt_groupcode='01KB' )




CREATE TABLE ml_chithuongdvvp
(
  chithuongdvvpid serial NOT NULL,
  departmentgroupid integer,
  departmentgroupname text,
  quyetdinh_so text,
  quyetdinh_ngay timestamp without time zone,
  tylehuong integer,
  chibsth7cn double precision,
  CONSTRAINT ml_chithuongdvvp_pkey PRIMARY KEY (chithuongdvvpid)
)

CREATE INDEX ml_chithuongdvvp_departmentgroupid_idx ON ml_chithuongdvvp USING btree (departmentgroupid);


        public int? stt { get; set; }
        public int? departmentgroupid { get; set; }
        public string departmentgroupname { get; set; }
        public string quyetdinh_so { get; set; }
        public string quyetdinh_ngay { get; set; }
        public int? soluong_tong { get; set; }
        public int? soluong_th7cn { get; set; }
        public decimal? dongia { get; set; }
        public decimal? thanhtien { get; set; }
        public int? tylehuong { get; set; }
        public decimal? tienhuong { get; set; }
        public decimal? tongtien { get; set; }
        public decimal? chiphi { get; set; }
        public decimal? tienthuong_th7cn { get; set; }
        public decimal? tienbsi_th7cn { get; set; }
        public decimal? tonghuong { get; set; }
        public string kynhan { get; set; }

		
		

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
		
	