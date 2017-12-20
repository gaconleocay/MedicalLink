-- Table: bhyt

-- DROP TABLE bhyt;

CREATE TABLE bhyt
(
  bhytid serial NOT NULL,
  patientid integer,
  hosobenhanid integer,
  medicalrecordid integer,
  vienphiid integer,
  bhytcode text,
  macskcbbd text,
  bhytdate timestamp without time zone,
  bhytfromdate timestamp without time zone,
  bhytutildate timestamp without time zone,
  bhyt_loaiid integer,
  thuongbinhdieutritaiphat integer,
  thamgiabhytdu36thang integer,
  imagedata bytea,
  imagesize integer DEFAULT 0,
  bhytremark text,
  lastaccessdate timestamp without time zone,
  version timestamp without time zone,
  dongbaohiemlientuc150ngay integer,
  sync_flag integer,
  update_flag integer,
  isencript integer,
  noisinhsong text,
  du5nam6thangluongcoban integer,
  dtcbh_luyke6thang integer,
  stt_dkbhyt text,
  CONSTRAINT bhyt_pkey PRIMARY KEY (bhytid)
)


CREATE TABLE hosobenhan	doituongbenhnhanid
(
  hosobenhanid serial NOT NULL,
  soluutru text,
  soluutru_remark text,
  soluutru_vitri text,
  soluutru_nguoiluu integer,
  hosobenhancode text,
  isuploaded integer,
  isdownloaded integer,
  loaibenhanid integer,
  userid integer,
  departmentgroupid integer,
  departmentid integer,
  hinhthucvaovienid integer,
  ketquadieutriid integer,
  xutrikhambenhid integer,
  hinhthucravienid integer,
  hosobenhanstatus integer,
  patientid integer,
  hosobenhandate timestamp without time zone,
  hosobenhandate_ravien timestamp without time zone,
  chandoanvaovien_code text,
  chandoanvaovien text,
  chandoanravien_code text,
  chandoanravien text,
  chandoanravien_kemtheo_code text,
  chandoanravien_kemtheo text,
  lastaccessdate timestamp without time zone,
  hosobenhanremark text,
  version timestamp without time zone,
  sovaovien text DEFAULT ''::text,
  patientname text,
  birthday timestamp without time zone,
  birthday_year integer,
  gioitinhcode text,
  nghenghiepcode text,
  hc_dantoccode text,
  hc_quocgiacode text,
  hc_sonha text,
  hc_thon text,
  hc_xacode text,
  hc_huyencode text,
  hc_tinhcode text,
  noilamviec text,
  nguoithan text,
  nguoithan_name text,
  nguoithan_phone text,
  nguoithan_address text,
  gioitinhname text,
  nghenghiepname text,
  hc_dantocname text,
  hc_quocgianame text,
  hc_xaname text,
  hc_huyenname text,
  hc_tinhname text,
  sync_flag integer,
  update_flag integer,
  patient_id text,
  imagedata bytea,
  imagesize integer DEFAULT 0,
  patientcode text,
  soluutru_thoigianluu timestamp without time zone,
  isencript integer,
  ismocapcuu integer,
  bhytcode text,
  malifegap text,
  CONSTRAINT hosobenhan_pkey PRIMARY KEY (hosobenhanid)
)
  
CREATE TABLE medicalrecord bhytid
(
  medicalrecordid serial NOT NULL,
  MEDICALRECORDCODE text,
  sothutuid integer,
  sothutunumber integer,
  sothutuphongkhamid integer,
  sothutuphongkhamnumber integer,
  vienphiid integer,
  hosobenhanid integer DEFAULT 0,
  medicalrecordid_next integer DEFAULT 0,
  medicalrecordid_master integer,
  medicalrecordstatus integer,
  departmentgroupid integer,
  departmentid integer,
  giuong text,
  loaibenhanid integer,
  userid integer,
  patientid integer,
  doituongbenhnhanid integer,
  bhytid integer,
  lydodenkham text,
  yeucaukham text,
  thoigianvaovien timestamp without time zone,
  chandoanvaovien text,
  chandoanvaovien_code text,
  chandoanvaovien_kemtheo text,
  chandoanvaovien_kemtheo_code text,
  chandoankkb text,
  chandoankkb_code text,
  chandoanvaokhoa text,
  chandoanvaokhoa_code text,
  chandoanvaokhoa_kemtheo text,
  chandoanvaokhoa_kemtheo_code text,
  isthuthuat integer,
  isphauthuat integer,
  hinhthucvaovienid integer,
  backdepartmentid integer,
  uutienkhamid integer,
  noigioithieuid integer,
  vaoviencungbenhlanthu integer,
  thoigianravien timestamp without time zone,
  chandoanravien text,
  chandoanravien_code text,
  chandoanravien_kemtheo text,
  chandoanravien_kemtheo_code text,
  chandoanravien_kemtheo1 text,
  chandoanravien_kemtheo_code1 text,
  chandoanravien_kemtheo2 text,
  chandoanravien_kemtheo_code2 text,
  xutrikhambenhid integer,
  hinhthucravienid integer,
  ketquadieutriid integer,
  nextdepartmentid integer,
  nexthospitalid integer,
  istaibien integer,
  isbienchung integer,
  giaiphaubenhid integer DEFAULT 0,
  lydovaovien text,
  vaongaythucuabenh integer,
  quatrinhbenhly text,
  tiensubenh_banthan text,
  tiensubenh_giadinh text,
  khambenh_toanthan text,
  khambenh_mach text,
  khambenh_nhietdo text,
  khambenh_huyetap_low text,
  khambenh_huyetap_high text,
  khambenh_nhiptho text,
  khambenh_cannang text,
  khambenh_chieucao text,
  khambenh_vongnguc text,
  khambenh_vongdau text,
  khambenh_bophan text,
  tomtatkqcanlamsang text,
  chandoanbandau text,
  daxuly text,
  tomtatbenhan text,
  chandoankhoakhambenh text,
  daxulyotuyenduoi text,
  medicalrecordremark text,
  lastaccessdate timestamp without time zone,
  version timestamp without time zone,
  chandoantuyenduoi text,
  chandoantuyenduoi_code text,
  noigioithieucode text,
  canlamsangstatus integer,
  sync_flag integer,
  update_flag integer,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  keylock integer,
  cv_chuyenvien_hinhthucid integer,
  cv_chuyenvien_lydoid integer,
  cv_chuyendungtuyen integer,
  cv_chuyenvuottuyen integer,
  xetnghiemcanthuchienlai text,
  loidanbacsi text,
  chandoanbandau_code text,
  nextbedrefid integer,
  nextbedrefid_nguoinha text,
  thoigianchuyenden timestamp without time zone,
  khambenh_thilucmatphai text,
  khambenh_thilucmattrai text,
  khambenh_klthilucmatphai text,
  khambenh_klthilucmattrai text,
  khambenh_nhanapmatphai text,
  khambenh_nhanapmattrai text,
  CONSTRAINT medicalrecord_pkey PRIMARY KEY (medicalrecordid)
)
CREATE TABLE vienphi
(
  vienphiid serial NOT NULL,
  vienphicode text,
  loaivienphiid integer,
  vienphistatus integer,
  departmentgroupid integer,
  departmentid integer,
  hosobenhanid integer,
  doituongbenhnhanid integer,
  dathutienkham integer,
  dagiuthebhyt integer,
  bhytid integer,
  patientid integer,
  vienphidate timestamp without time zone,
  vienphidate_ravien timestamp without time zone,
  chandoanvaovien text,
  chandoanvaovien_code text,
  chandoanravien text,
  chandoanravien_code text,
  chandoanravien_kemtheo text,
  chandoanravien_kemtheo_code text,
  bhyt_traituyen double precision,
  bhyt_thangluongtoithieu double precision,
  bhyt_gioihanbhyttrahoantoan double precision,
  duyet_ngayduyet timestamp without time zone,
  duyet_nguoiduyet integer,
  duyet_sothutuduyet integer,
  vienphistatus_bh integer,
  duyet_ngayduyet_bh timestamp without time zone,
  duyet_nguoiduyet_bh integer,
  duyet_sothutuduyet_bh integer,
  vienphistatus_vp integer,
  duyet_ngayduyet_vp timestamp without time zone,
  duyet_nguoiduyet_vp integer,
  duyet_sothutuduyet_vp integer,
  vienphistatus_tk integer,
  duyet_ngayduyet_tk timestamp without time zone,
  duyet_nguoiduyet_tk integer,
  duyet_sothutuduyet_tk integer,
  tongtiendichvu double precision,
  tongtiendichvu_bh double precision,
  tongtiendichvu_dv double precision,
  tongtiendichvu_bhyt double precision,
  tongtienmiengiam double precision,
  tongtiendatra double precision,
  tongtiendatra_bh double precision,
  tongtiendatra_dv double precision,
  tongtiendatra_tk double precision,
  isneedupdatemoney integer DEFAULT 0,
  isneedupdateinfo integer DEFAULT 0,
  money_khambenh double precision,
  money_xetnghiem double precision,
  money_chandoanhinhanh double precision,
  money_thamdochucnang double precision,
  money_thuoctrongdanhmuc double precision,
  money_thuocngoaidanhmuc double precision,
  money_mauchephammau double precision,
  money_phauthuatthuthuat double precision,
  money_vattutrongdanhmuc double precision,
  money_vattutrongdanhmuctt double precision,
  money_vattungoaidanhmuc double precision,
  money_dichvukythuatcao double precision,
  money_thuocungthungoaidanhmuc double precision,
  money_ngaygiuongchuyenkhoa double precision,
  money_vanchuyen double precision,
  money_phuthu double precision,
  money_dv_khambenh double precision,
  money_dv_xetnghiem double precision,
  money_dv_chandoanhinhanh double precision,
  money_dv_thamdochucnang double precision,
  money_dv_thuoctrongdanhmuc double precision,
  money_dv_thuocngoaidanhmuc double precision,
  money_dv_mauchephammau double precision,
  money_dv_phauthuatthuthuat double precision,
  money_dv_vattutrongdanhmuc double precision,
  money_dv_vattutrongdanhmuctt double precision,
  money_dv_vattungoaidanhmuc double precision,
  money_dv_dichvukythuatcao double precision,
  money_dv_thuocungthungoaidanhmuc double precision,
  money_dv_ngaygiuongchuyenkhoa double precision,
  money_dv_vanchuyen double precision,
  money_dv_phuthu double precision,
  lastaccessdate timestamp without time zone,
  vienphiremark text,
  version timestamp without time zone,
  duyet_quyduyet text,
  vienphidate_noitru timestamp without time zone,
  medicalrecordid_start integer,
  medicalrecordid_end integer,
  sync_flag integer,
  update_flag integer,
  datronvien integer,
  departmentgroupid_start integer,
  departmentid_start integer,
  bhyt_tuyenbenhvien integer,
  masothue text,
  sotaikhoan text,
  CONSTRAINT vienphi_pkey PRIMARY KEY (vienphiid)
)
bhyt_tuyenbenhvien

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
WITH (

CREATE TABLE servicepriceref
(
  servicepricerefid serial NOT NULL,
  servicepricegroupcode text,
  servicepricetype integer,
  servicegrouptype integer,
  SERVICEPRICECODE text,
  bhyt_groupcode text,
  report_groupcode text,
  report_tkcode text,
  SERVICEPRICENAME text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  listdepartmentphongthuchien text,
  servicepricefee_old_date timestamp without time zone,
  servicepricefee_old text,
  servicepricefeenhandan_old text,
  servicepricefeebhyt_old text,
  servicepricefeenuocngoai_old text,
  servicepriceprintorder integer DEFAULT 0,
  servicepricerefid_master integer DEFAULT 0,
  servicelock integer DEFAULT 0,
  pttt_hangid integer DEFAULT 0,
  laymauphongthuchien integer DEFAULT 0,
  khongchuyendoituonghaophi integer DEFAULT 0,
  cdha_soluongthuoc double precision DEFAULT 0,
  cdha_soluongvattu double precision DEFAULT 0,
  tylelaichidinh double precision DEFAULT 0,
  tylelaithuchien double precision DEFAULT 0,
  version timestamp without time zone,
  luonchuyendoituonghaophi integer DEFAULT 0,
  tinhtoanlaigiadvktc integer DEFAULT 0,
  servicepricecodeuser text,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  servicepricebhytdinhmuc text,
  listdepartmentphongthuchienkhamgoi text,
  ck_groupcode text,
  servicepricecode_ng text,
  pttt_dinhmucvtth double precision DEFAULT 0,
  pttt_dinhmucthuoc double precision DEFAULT 0,
  isremove integer,
  servicepricefee_old_type integer DEFAULT 0,
  servicepricesttuser text,
  pttt_loaiid integer DEFAULT 0,
  CONSTRAINT servicepriceref_pkey PRIMARY KEY (servicepricerefid)
)

CREATE TABLE medicine_ref
(
  medicinerefid serial NOT NULL,
  medicinecode text,
  medicinegroupcode text,
  medicinereftype integer,
  datatype integer,
  medicinetype integer,
  medicinename text,
  tenkhoahoc text,
  donvitinh text,
  donggoi text,
  hangsanxuat text,
  nuocsanxuat text,
  nongdo text,
  lieuluong text,
  hoatchat text,
  dangdung text,
  huongdansudung text,
  soluongngay double precision,
  chuy text,
  medicineprintorder integer,
  gianhap double precision DEFAULT 0,
  giaban double precision DEFAULT 0,
  vatnhap double precision DEFAULT 0,
  vatban double precision DEFAULT 0,
  canhbaosoluong integer DEFAULT 0,
  canhbaohsd integer DEFAULT 0,
  medicinerefid_org integer DEFAULT 0,
  solo text,
  sodangky text,
  hansudung timestamp without time zone,
  version timestamp without time zone NOT NULL DEFAULT now(),
  bietduoc text,
  atc text,
  tylehuhao double precision,
  bhyt_groupcode text,
  report_tkcode text,
  report_groupcode text,
  khongchuyendoituonghaophi integer,
  luonchuyendoituonghaophi integer,
  sync_flag integer,
  update_flag integer,
  stt_thuoc integer,
  servicepricefee double precision DEFAULT 0,
  servicepricefeenhandan double precision DEFAULT 0,
  servicepricefeebhyt double precision DEFAULT 0,
  servicepricefeenuocngoai double precision DEFAULT 0,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  servicepricefee_loinhuan double precision DEFAULT 0,
  servicepricefeenhandan_loinhuan double precision DEFAULT 0,
  servicepricefeebhyt_loinhuan double precision DEFAULT 0,
  servicepricefeenuocngoai_loinhuan double precision DEFAULT 0,
  servicepricebhytdinhmuc double precision DEFAULT 0,
  listdepartmentphongthuchien text,
  isthuockhangsinh integer,
  mahoatchat text,
  tuongtacthuoc text,
  medicinecodeuser text,
  benhvienapthau text,
  nhacungcapid integer,
  hansudung_year integer DEFAULT 0,
  hansudung_month integer DEFAULT 0,
  iskhongnhapmoi integer,
  nhomquyche text,
  nhombaocao text,
  nhomduocly text,
  nhomtieuduocly text,
  nhomquanly text,
  nhomnghiencuu text,
  nhomabcven text,
  stt_thuoc_chuyeu integer,
  isremove integer,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  goithau text,
  namcungung text,
  thuhoivolo text,
  isthuhoivolo integer,
  ischephamyhoccotruyen integer,
  isvithuocyhoccotruyen integer,
  isthuoctanduoc integer,
  stt_dauthau text,
  nguonchuongtrinhid integer,
  medicinename_byt text,
  ischolanhdaoduyet integer,
  medicinestorebillid integer DEFAULT 0,
  sochungtu text,
  ngaychungtu timestamp without time zone,
  ngaychungtu2 text,
  danhsttdungthuoc integer,
  stt_thuoc_tt40 integer,
  stt_thuoc_tt40text text,
)
CREATE TABLE medicine_store_bill
(
  medicinestorebillid serial NOT NULL,
  medicineperiodid integer DEFAULT 0,
  medicinestoreid integer,
  medicinestorebillid_yc integer,
  medicinestorebillid_im integer,
  medicinestorebillid_ex integer,
  sochungtu text,
  linkcode text,
  isxuattutruc integer,
  partnerid integer,
  partnername text,
  customername text,
  medicinestorebillcode text,
  medicinestorebilldate timestamp without time zone,
  medicinestorebilldoer text,
  medicinestorebilltype integer,
  medicinestorebillstatus integer,
  medicinestorebillfinisheddate timestamp without time zone,
  datatype integer,
  loaithuoc integer,
  isdaduyetphieu integer,
  medicinestorebillapprover text,
  medicinestorebillapprovemessage text,
  medicinestorebillapprovedate timestamp without time zone,
  isdayeucau integer,
  medicinestorebillprocessinger text,
  medicinestorebillprocessingmessage text,
  medicinestorebillprocessingdate timestamp without time zone,
  isremove integer,
  nguoihuyphieu text,
  lydohuyphieu text,
  ngayhuyphieu timestamp without time zone,
  isdathutien integer,
  nguoithutien text,
  lydothutien text,
  ngaythutien timestamp without time zone,
  sothutien double precision,
  medicinestorebillremark text,
  medicinestorebilltotalmoney double precision,
  medicinestorebillphaitramoney double precision,
  medicinestorebilldatramoney double precision,
  medicinestorebillconnomoney double precision,
  medicinestorebilldatemoney timestamp without time zone,
  medicinestorebillchietkhau double precision,
  medicinestorebillthuegtgt double precision,
  ketoanduocid integer,
  listdonthuoc text,
  bill_mode integer,
  lanin integer,
  maubenhphamid integer,
  departmentgroupid integer,
  departmentid integer,
  ngaysudungthuoc timestamp without time zone,
  lanlinhthuoctrongngay integer,
  ylenhlinhthuoc text,
  version timestamp without time zone,
  isphieubuthieu integer,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  sothutunumber integer,
  soquaynumber integer,
  iscangoiloa integer,
  ngaychungtu timestamp without time zone,
  keylock integer,
  medicinekiemkeid integer,
  ngayylenh text,
  isbusy integer DEFAULT 0,
  sothuthuphongluu text,
  bacsi text,
  chandoan text,
  medicinekiemkeid_parter integer,
  bacsi_id integer,
  khoa_id integer,
  phong_id integer,
  CONSTRAINT medicine_store_bill_pkey PRIMARY KEY (medicinestorebillid)
)
CREATE TABLE medicine
(
  medicineid serial NOT NULL,
  medicineperiodid integer DEFAULT 0,
  medicinestorerefid integer,
  medicinerefid integer,
  medicinestoreid integer,
  medicinestorebillid integer,
  medicinestorebillcode text,
  medicinestorebilltype integer,
  medicinestorebillstatus integer,
  bill_mode integer,
  ngaysudungthuoc timestamp without time zone,
  partnerid integer,
  medicinestorebillremark text,
  departmentgroupid integer,
  departmentid integer,
  solo text,
  sodangky text,
  hansudung timestamp without time zone,
  xuat_vat double precision,
  xuat_giaban double precision,
  medicinedate timestamp without time zone,
  soluong double precision,
  sodukhadungupdated double precision,
  money double precision,
  vat double precision,
  approve_medicinedate timestamp without time zone,
  approve_soluong double precision,
  approve_money double precision,
  approve_vat double precision,
  accept_medicinedate timestamp without time zone,
  accept_soluong double precision,
  accept_money double precision,
  accept_vat double precision,
  finish_medicinedate timestamp without time zone,
  finish_soluong double precision,
  finish_money double precision,
  finish_vat double precision,
  isremove integer,
  servicepriceid integer,
  listservicepriceid text,
  medicineremark text,
  medicinelinhthuocremark text,
  version timestamp without time zone NOT NULL DEFAULT now(),
  isphieubuthieu integer,
  sync_flag integer,
  update_flag integer,
  medicinekiemkeid integer,
  hansudung_year integer,
  hansudung_month integer,
  goithau text,
  nguonchuongtrinhid integer,
  userid integer,
  medicinekiemkeid_parter integer,
  stt_dauthau text,
  CONSTRAINT medicine_pkey PRIMARY KEY (medicineid)
)
-----------
-- Table: bill

-- DROP TABLE bill;

CREATE TABLE bill
(
  billid serial NOT NULL,
  billcode text,
  billgroupcode text,
  medicalrecordid integer,
  patientid integer DEFAULT 0,
  vienphiid integer DEFAULT 0,
  listmaubenhphamid text,
  hosobenhanid integer DEFAULT 0,
  loaiphieuthuid integer,
  billmode integer,
  userid integer DEFAULT 0,
  billdate timestamp without time zone,
  departmentgroupid integer,
  departmentid integer,
  tongtien double precision,
  chietkhau double precision,
  datra double precision,
  conno double precision,
  conno_henthanhtoan timestamp without time zone,
  patientname text,
  dahuyphieu integer,
  billid_ketchuyenam integer,
  billid_ketchuyenduong integer,
  isyeucau integer,
  thoigianhuyphieu timestamp without time zone,
  lydohuyphieu text,
  billremark text,
  printremark text,
  version timestamp without time zone,
  miengiam text,
  lydomiengiam text,
  useridhuyphieu integer,
  sync_flag integer,
  update_flag integer,
  hinhthucthanhtoan integer,
  tracenumber integer,
  pos_billcode text,
  imei text,
  hinhthucthanhtoanhuy integer,
  isencript integer,
  nguonhachtoan integer,
  khoachuyenden integer,
  phonghachtoan integer,
  danhsachdoituongdichvu text,
  iskhac integer,
  CONSTRAINT bill_pkey PRIMARY KEY (billid)
)



CREATE TABLE maubenhpham
(
  maubenhphamid serial NOT NULL,
  medicalrecordid integer DEFAULT 0,
  patientid integer DEFAULT 0,
  vienphiid integer DEFAULT 0,
  hosobenhanid integer DEFAULT 0,
  dathutien integer DEFAULT 0,
  datamung integer DEFAULT 0,
  servicepriceid_master integer DEFAULT 0,
  phieudieutriid integer DEFAULT 0,
  sothutuid integer DEFAULT 0,
  sothutunumber integer DEFAULT 0,
  sothutunumberdagoi integer DEFAULT 0,
  sothutuid_laymau integer DEFAULT 0,
  sothutunumber_laymau integer DEFAULT 0,
  sothutunumberdagoi_laymau integer DEFAULT 0,
  medicinestoreid integer DEFAULT 0,
  departmentgroupid integer DEFAULT 0,
  departmentid integer DEFAULT 0,
  buonggiuong text,
  userid integer DEFAULT 0,
  departmentgroupid_des integer DEFAULT 0,
  departmentid_des integer DEFAULT 0,
  departmentid_des_org integer DEFAULT 0,
  departmentid_laymau integer DEFAULT 0,
  medicinestorebillid integer DEFAULT 0,
  sophieu text,
  barcode text,
  maubenhphamtype integer DEFAULT 0,
  maubenhphamhaophi integer DEFAULT 0,
  maubenhphamphieutype integer DEFAULT 0,
  maubenhphamgrouptype integer DEFAULT 0,
  maubenhphamdatastatus integer DEFAULT 0,
  maubenhphamstatus_laymau integer DEFAULT 0,
  maubenhphamprintstatus integer DEFAULT 0,
  maubenhphamstatus integer DEFAULT 0,
  maubenhphamdate timestamp without time zone,
  maubenhphamdate_sudung timestamp without time zone,
  maubenhphamdate_thuchien timestamp without time zone,
  maubenhphamdate_laymau timestamp without time zone,
  maubenhphamfinishdate timestamp without time zone,
  unmapedhisservice text,
  isdeleted integer DEFAULT 0,
  maubenhphamdeletedate timestamp without time zone,
  userdelete text,
  usercreate text,
  userupdatebarcode text,
  userlastupdate text,
  userupdatebarcodedate timestamp without time zone,
  userlastupdatedate timestamp without time zone,
  patientpid text,
  doituong text,
  patientname text,
  patientaddress text,
  patientphone text,
  patientbirthday timestamp without time zone,
  patientsex integer,
  chandoan text,
  remark text,
  version timestamp without time zone,
  userduyetall integer DEFAULT 0,
  usertrakq integer DEFAULT 0,
  ismaubenhphamtemp integer DEFAULT 0,
  sothutuphongkhamid integer DEFAULT 0,
  userthuchien integer,
  thoigiandukiencoketqua timestamp without time zone,
  sodienthoaibaotinkhicoketqua text,
  sync_flag integer,
  update_flag integer,
  dacodichvuthutien integer,
  dacodichvuduyetcanlamsang integer,
  sessionid integer DEFAULT 0,
  isautongaygiuong integer,
  sothutuchidinhcanlamsang integer,
  isencript integer,
  maubenhphamdate_org timestamp without time zone,
  departmentid_traketqua integer,
  numbertraketqua integer,
  isdatraketqua integer,
  usertraketqua integer,
  maubenhphamtraketquadate timestamp without time zone,
  remarkdetail text,
  chandoan_code text,
  hl7_is integer,
  hl7_isdatraketqua integer,
  hl7_maubenhphamtraketquadate timestamp without time zone,
  loidanbacsi text,
  phieutonghopsuatanid integer,
  medicinekiemkeid integer,
  isloaidonthuoc integer,
  servicepriceid_thanhtoanrieng integer,
  sothutunumber_h_n integer,
  userlaymau integer,
  CONSTRAINT maubenhpham_pkey PRIMARY KEY (maubenhphamid)
)




Hiện giờ có những nhánh sau:

1. Sử dụng x/y dịch vụ: 
vd: A, B, C, D thì tìm BN sử dụng 2/4 dv bất kỳ: BN có dv: A, B hoặc B, C hoặc B, D hoặc A, D 

(a & b) || (a & c) || (a & d) || (b & c) || (b & d) || (c & d)

===>
#2#(a || b || c || d || ...)
#2#((a & b) || (c & d) || (e & f) || (g & h) || ....)	


Là tổ hợp x của y:
vd 2/4:
4!			4*3*2*1		24
-------  = --------- = --- = 6
2!(4-2)!	2*1(2*1)	4

chỉnh hợp: = n(n−1)(n−2)(n−3)...(n−k+1) = 4*3 = 12


2. Sử dụng DV chính A và kèm 1 trong những dịch vụ phụ thuộc (B,C,D...)
===>
(A & B & ...) & (a || b || c || ...)
(A & B & ...) & ((a & b) || (c & d) || ....)

3. Sử dụng n dịch vụ chính: BN sử dụng đồng thời n dịch vụ là dịch vụ PTTT chính (theo nhánh tìm kiếm hiện tại)
====>
A & B & ...

---------
1. Tổ hợp x/(n dịch vụ)

vd: 2/(a,c,c,d)
- Tính tổ hợp 2/4 dịch vụ = (a & b) || (a & c) || (a & d) || (b & c) || (b & d) || (c & d)

chuyen vien :BN000582679

-------=--------------
Khoa danh mục thuốc/vật tư không sử dụng

-----=========

CREATE TABLE tools_benhvien
(
  benhvienid serial NOT NULL,
  benhvienkcbbd text,
  benhviencode text,
  benhvienname text,
  benhvienaddress text,
  benhvienhang text,
  benhvienloai text,
  benhvientuyen text,
  ghichu text,
  matinh text,
  mahuyen text,
  maxa text,
  version timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  CONSTRAINT tools_benhvien_pkey PRIMARY KEY (benhvienid)
)

CREATE INDEX tools_benhvien_benhviencode_idx
  ON tools_benhvien
  USING btree
  (benhviencode);


select benhvienid, benhvienkcbbd, benhviencode, benhvienname from tools_benhvien;

654398718



ALTER TABLE thuchiencls ADD tools_userid integer;
ALTER TABLE thuchiencls ADD tools_username text;

-----
CREATE TABLE tools_serviceprice_ttrieng
(
  serttriengpid serial NOT NULL,
  servicepriceid integer,
  updatetype integer, --1: update di kem - hao phi; 2: update id dv di kem,
  dateupdate timestamp without time zone,
  CONSTRAINT tools_serviceprice_dkhp_pkey PRIMARY KEY (serttriengpid)
)
CREATE INDEX serdkhp_servicepriceid_idx
  ON tools_serviceprice_ttrieng
  USING btree
  (servicepriceid);
CREATE INDEX serdkhp_updatetype_idx
  ON tools_serviceprice_ttrieng
  USING btree
  (updatetype);

CREATE TABLE tools_vienphi_tltt
(
  vienphitlttid serial NOT NULL,
  vienphiid integer,
  thangluong_old double precision,
  dateupdate timestamp without time zone,
  CONSTRAINT tools_vienphi_tltt_pkey PRIMARY KEY (vienphitlttid)
)
CREATE INDEX vienphitltt_vienphiid_idx
  ON tools_vienphi_tltt
  USING btree
  (vienphiid);
 
  
  
servicepriceid in (28252929,
28252930,
28252757,
28252758
)




XUAT_AN
NUOC_SOI

G303TH
G303YC
G350

===========
select ot.tools_othertypelistid, ot.tools_othertypelistcode, ot.tools_othertypelistname, ot.tools_othertypeliststatus, ot.tools_othertypelistnote, o.tools_otherlistid, o.tools_otherlistcode, o.tools_otherlistname, o.tools_otherlistvalue, o.tools_otherliststatus from tools_othertypelist ot inner join tools_otherlist o on o.tools_othertypelistid=ot.tools_othertypelistid;

CREATE TABLE IF NOT EXISTS tools_serviceref
(
  toolsservicerefid serial NOT NULL,
  his_servicepricerefid integer,
  servicegrouptype integer,
  servicepricetype integer,
  bhyt_groupcode text,
  servicepricegroupcode text,
  servicepricecode text,
  servicepricename text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  servicelock integer DEFAULT 0,
  servicepricecodeuser text,
  servicepricesttuser text,
  pttt_hangid integer DEFAULT 0,
  pttt_loaiid integer DEFAULT 0,
  tools_otherlistid integer,
  CONSTRAINT tools_serviceref_pkey PRIMARY KEY (toolsservicerefid)
)
  CREATE INDEX tools_serviceref_serrefid_idx
  ON tools_serviceref
  USING btree
  (his_servicepricerefid);  
   CREATE INDEX tools_serviceref_servicepricegroupcode_idx
  ON tools_serviceref
  USING btree
  (servicepricegroupcode); 
  CREATE INDEX tools_serviceref_serrecode_idx
  ON tools_serviceref
  USING btree
  (servicepricecode);  
  CREATE INDEX tools_serviceref_bhytcode_idx
  ON tools_servicefref
  USING btree
  (bhyt_groupcode);  
  CREATE INDEX tools_serviceref_tools_otherlistid_idx
  ON tools_serviceref
  USING btree
  (tools_otherlistid);    


  select se.servicecomment, 
		mbp.usertrakq, 
		mbp.userthuchien, 
		mbp.usertraketqua,
		nv.usercode,
		nv.username 
		
  from service se
	inner join maubenhpham mbp on mbp.maubenhphamid=se.maubenhphamid and maubenhphamgrouptype=0
	left join tools_tblnhanvien nv on nv.userhisid=mbp.userthuchien
  where se.servicecomment<>'' 
  order by mbp.maubenhphamid desc limit 100



-----
CREATE INDEX serviceprice_bhyt_groupcode_idx
  ON serviceprice
  USING btree
  (bhyt_groupcode);
  
  
 INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (12, 'REPORT_28_NHOMDV', 'REPORT_28_Danh sách nhóm dịch vụ', 0, 'Nhóm Chi phí khác - bv Thanh Hóa');
ALTER SEQUENCE tools_othertypelist_tools_othertypelistid_seq RESTART WITH 13;
 
 INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (12, 'U2918-0644', 'Nhóm chi phí khác - BV Thanh Hóa', 'U2918-0644', '0', ''); 
  
  select ser.* from serviceprice ser
	inner join vienphi vp on vp.vienphiid=ser.vienphiid and vp.vienphistatus>0
	
where ser.loaidoituong=2 and ser.servicepriceid_master=0 and ser.servicepriceid_thanhtoanrieng=0 
and vp.duyet_ngayduyet_vp between '2017-06-01 00:00:00' and '2017-07-31 23:59:59'
and ser.maubenhphamphieutype=0
  
  
  

CREATE TABLE client_machine
(
  clientmachineid serial NOT NULL,
  clientmachinecode text,
  machineid text,
  machineserial text,
  appversion text,
  apptype integer,
  usenfc integer,
  reportstatus integer,
  softstatus integer,
  servicestatus integer,
  installedtime timestamp without time zone,
  lastping timestamp without time zone,
  currentusercode text,
  processorid text,
  baseboardproduct text,
  baseboardmanufacturer text,
  diskdrivesignature text,
  videocontrollercaption text,
  physicalmediaserialnumber text,
  biosversion text,
  operatingsystemserialnumber text,
  ipaddress text,
  macadddress text,
  computername text,
  username text,
  domain text,
  version timestamp without time zone,
  alertdata text,
  alertstatus integer,
  sync_flag integer,
  update_flag integer,
  CONSTRAINT client_machine_pkey PRIMARY KEY (clientmachineid)
)
  
  
----Thuật toán yêu cầu tắt phần mềm

- Client khi đăng nhập lấy thông tin về phần cứng máy tính => insert/update thông tin vào bảng ds client
Thông tin bao gồm:
	+ ID máy tính
	+ ID CPU
	+ ID Main
	+ ID OS
	+ Thời gian tạo
	+ Thời gian update 
	+ Thời gian ping cuối?
	+ Tên người đăng nhập
	
	+ Trạng thái hoạt động
	+ Yêu cầu thực hiện chức năng: khởi động lại PM, tắt phần mềm, tắt máy tính, khởi động lại máy tính

- Khi yêu cầu thực hiện chức năng: update yêu cầu thực hiện chức năng: tất cả các máy, từng máy
- Client thiết lập timer để truy vấn vào DB kiểm tra yêu cầu thực hiện, khi đó thực hiện yêu cầu

* Timer truy vấn thường xuyên -> ảnh hưởng server, 
* Dùng chung timer thời gian của phần mềm, sau mỗi 10s thì truy vấn CSDL
* Thiết lập thời gian truy vấn CSDL trên server

 	
 F1_3
F1_4

Kiểm tra với phiếu chỉ định dịch vụ, nếu phiếu chỉ định là Viện phí thì không check
Kiểm tra hạn thẻ trước và sau thời gian điều trị.
Kiểm tra xem có tồn tại phiếu chỉ định trước/hoặc sau hạn thẻ. Nếu có thì cảnh báo Lỗi này


 
  Allow Internet access	GUANGDONG OPPO MOBILE TELECOMMUNICATIONS CORP.LTD	GUANGDONG OPPO MOBILE TELECOMMUN	192.168.2.215	Automatic IP	A8:1B:5A:FA:1A:A7	2.4 GHz	-	-	0:00:00	

  
  
---Thống kê số lượng hóa đơn phát hành trong tháng 10- bv Việt Tiệp
select
	count(*) as soluong_hd,
	sum(datra) as thanhtien
from bill 
where 
	loaiphieuthuid=0 
	and dahuyphieu=0
	and billdate between '2017-10-01 00:00:00' and '2017-10-31 23:59:59'


21935;
216697046508.055
  
22231;217171864444.154  
  
 
  
A. DUy

F3_32: Natri clorid 0,9% [500ml] cùng số Đăng kí và tên thuốc, nhưng cái giám định chỉ nhận lô thuốc có giá thấp hơn ở trên, trong khi lô dưới mới là lô đúng giá nên pmem báo vượt giá thầu  
  
=> Chạy lại giám định là ok

F4_40
F4_41: thừa 1 dấu " " so với danh mục
Danh mục: Vasofix Safety[Kim luồn mạch máu an toàn có cánh số 20G]
XML: 	  Vasofix Safety[ Kim luồn mạch máu an toàn có cánh số 20G]
  
  
  
Kim chọc dò tủy sống(Spinocan G18 đến G27)
  
  
  
  Vít khóa 4,5 mm, các cỡ[IEC]
  Vít khóa 4,5 mm, các cỡ[IEC]
  
  Selection	STT	MA_NHOM_VTYT	MA_NHOM_AX	TEN_NHOM_VTYT	TEN_NHOM_AX	MA_HIEU	MA_VTYT_BV	TEN_VTYT_BV	QUY_CACH	NUOC_SX	HANG_SX	DON_VI_TINH	DON_GIA	DON_GIA_TT	NHA_THAU	QUYET_DINH	KETQUA	HIEULUC	MANHOM_9324	LOAI_THAU	MA_CSKCB	SO_LUONG	DINH_MUC	CONG_BO	LYDOTUCHOI	Người cập nhật	Thời gian cập nhật
Đã đánh dấu	1657	N07.06.040	N07.06.040	Đinh, nẹp, ghim, kim, khóa, ốc, vít, lồng dùng trong phẫu thuật các loại, các cỡ	Đinh, nẹp, ghim, kim, khóa, ốc, vít, lồng dùng trong phẫu thuật các loại, các cỡ		N07.06.040	Vít khóa 4,5 mm, các cỡ[IEC]	25 cái/ túi		Intercus/ CHLB Đức - G7	cái	450000	450000	Công ty TNHH Hà Nội IEC	756/QĐ-BVVT	Đã phê duyệt	Có	10	2	31153	5	1	20170503		tdduy-it	2017-11-15 08:17
  
  
  
  
  
 -- kiem tra 1 the xe chi duoc cap nhat cho 1 BN tai 1 thoi diem
 
  
 alter table cp_benhnhanthexe add cardeventid text;

CREATE INDEX cp_benhnhanthexe_cardeventid_idx
  ON cp_benhnhanthexe
  USING btree
  (cardeventid);

------
alter table cp_benhnhanthexe add sothexe text;

CREATE INDEX cp_benhnhanthexe_sothexe_idx
  ON cp_benhnhanthexe
  USING btree
  (sothexe);


 
alter table cp_bill add benhnhanthexeid integer;

CREATE INDEX cp_bill_benhnhanthexeid_idx
  ON cp_bill
  USING btree
  (benhnhanthexeid);
  
  
alter table cp_checkxera add mathexe text;
alter table cp_checkxera add sothexe text;
alter table cp_checkxera add biensoxe text;
alter table cp_checkxera add loaixe text;
alter table cp_checkxera add thoigianvao timestamp without time zone;
alter table cp_checkxera add cardeventid text;

CREATE INDEX cp_checkxera_mathexe_idx ON cp_checkxera USING btree (mathexe);
CREATE INDEX cp_checkxera_sothexe_idx ON cp_checkxera USING btree (sothexe);
CREATE INDEX cp_checkxera_thoigianvao_idx ON cp_checkxera USING btree (thoigianvao);
CREATE INDEX cp_checkxera_cardeventid_idx ON cp_checkxera USING btree (cardeventid);
  
  
  
SELECT row_number () over (order by chk.checkdate) as stt, chk.patientid, chk.patientcode, chk.vienphiid, chk.hosobenhanid, chk.patientname, (case when chk.checkstatus=1 then 'Miễn phí' else 'Trả phí' end) as checkstatus_name, chk.checkdate, chk.checknote, (chk.checkusercode || ' - ' || chk.checkusername) as checkuser, chk.vienphidate, (case when chk.vienphidate_ravien<>'0001-01-01 00:00:00' then to_char(chk.vienphidate_ravien,'HH24:MI dd/MM/yyyy') end) as vienphidate_ravien, chk.bhytcode, chk.gioitinhname, to_char(chk.namsinh,'dd/MM/yyyy') as namsinh, chk.diachi, chk.vienphistatus, (case when chk.vienphistatus=0 then 'Đang điều trị' else 'Đã ra viện' end) as vienphistatus_name, chk.loaivienphiid, (case chk.loaivienphiid when 0 then 'Nội trú' when 1 then 'Ngoại trú' when 2 then 'Điều trị ngoại trú' end) as loaivienphi_name, (chk.departmentgroupname || ' - ' || chk.departmentname) as khoaphong, (case when chk.thuhoithe=1 then 'Thu lại thẻ gửi xe' end) as thuhoithe,
chk.mathexe as mathexe,
chk.sothexe as sothexe,
chk.biensoxe as biensoxe,
chk.loaixe as loaixe,
to_char(chk.thoigianvao,'HH24:MI dd/MM/yyyy') as thoigianvao,
chk.cardeventid as cardeventid
FROM (select * from cp_checkxera where " + _tieuchi + _trangthai + _loaibenhan + ") chk;   
  
  
  
  
-------
2817298624 - 1989
2817465920 - 1987
2816790128 - 1988
2815600416 - 1995
2817462400 - 1986
2815600992 - 1991
2815601008 - 1992
01638660011
01229229266




alter table tools_tbllog add vienphiid integer; alter table tools_tbllog add patientid integer;



  
  
  
  
  
  
  
