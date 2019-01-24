--BC Thời gian khám chữa bệnh
--Tong hop - Ngày 26/11/2018

--Lấy Data 2 khoa: KKB và KKBYC:
--Bỏ Phòng khám sau:
--219:Khám Phục Hồi Chức Năng"
--221:Khám Sức Khỏe.
--224:Phòng Khám Khoa Quốc Tế
--239:TT Lọc Máu và Thận Nhân Tạo


SELECT TMP.sl_bn,
	TMP.sl_chokham,
	TMP.tg_chokham,
	(TMP.tg_chokham/TMP.sl_bn) as tg_chokhamtb,
	TMP.sl_khamls,
	TMP.tg_khamls_qlcl,
	(TMP.tg_khamls_qlcl/TMP.sl_khamls) as tg_khamls_qlcltb,
	TMP.tg_khamls_it,
	(TMP.tg_khamls_it/TMP.sl_khamls) as tg_khamls_ittb,
	TMP.sl_khamlsxn,
	TMP.tg_khamlsxn_qlcl,
	(TMP.tg_khamlsxn_qlcl/TMP.sl_khamlsxn) as tg_khamlsxn_qlcltb,
	TMP.tg_khamlsxn_it,
	(TMP.tg_khamlsxn_it/TMP.sl_khamlsxn) as tg_khamlsxn_ittb,
	TMP.sl_khamlsxncdha,
	TMP.tg_khamlsxncdha_qlcl,
	(TMP.tg_khamlsxncdha_qlcl/TMP.sl_khamlsxncdha) as tg_khamlsxncdha_qlcltb,
	TMP.tg_khamlsxncdha_it,
	(TMP.tg_khamlsxncdha_it/TMP.sl_khamlsxncdha) as tg_khamlsxncdha_ittb,
	TMP.sl_khamlsxncdhatdcn,
	TMP.tg_khamlsxncdhatdcn_qlcl,
	(TMP.tg_khamlsxncdhatdcn_qlcl/TMP.sl_khamlsxncdhatdcn) as tg_khamlsxncdhatdcn_qlcltb,
	TMP.tg_khamlsxncdhatdcn_it,
	(TMP.tg_khamlsxncdhatdcn_it/TMP.sl_khamlsxncdhatdcn) as tg_khamlsxncdhatdcn_ittb
FROM 
(SELECT
	count(vp.*) as sl_bn,
	sum(case when stt.sothutustatus>=1 then 1 else 0 end) as sl_chokham,
	sum(case when stt.sothutustatus>=1 then ((DATE_PART('day',stt.sothutudate_start-mrd.thoigianvaovien)*24+DATE_PART('hour',stt.sothutudate_start-mrd.thoigianvaovien))*60+DATE_PART('minute',stt.sothutudate_start-mrd.thoigianvaovien)) end) as tg_chokham,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then 1 else 0 end) as sl_khamls,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamls_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamls_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then 1 else 0 end) as sl_khamlsxn,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxn_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then 1 else 0 end) as sl_khamlsxncdha,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdha_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdha_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then 1 else 0 end) as sl_khamlsxncdhatdcn,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdhatdcn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdhatdcn_it	
FROM 
	(select vienphiid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp} {_lstvienphi_loaitru}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus from medicalrecord where loaibenhanid=24 and departmentgroupid in (33,46) and departmentid not in (219,224,221,239) {_doituongbenhnhanid} {_tieuchi_mrd} {_lstvienphi_loaitru}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select medicalrecordid,min(sothutudate_start) as sothutudate_start,max(sothutudate_end) as sothutudate_end,max(sothutustatus) as sothutustatus from sothutuphongkham where 1=1 and sothutudate_start<>'0001-01-01 00:00:00' {_tieuchi_stt} group by medicalrecordid) stt on stt.medicalrecordid=mrd.medicalrecordid
	inner join (select ser.vienphiid,ser.medicalrecordid,
				max(case when ser.bhyt_groupcode='01KB' then 1 else 0 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1 else 0 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','07KTC') then 1 else 0 end) as iscdha,
				max(case when ser.bhyt_groupcode='05TDCN' then 1 else 0 end) as istdcn
			from	
				(select vienphiid,bhyt_groupcode,medicalrecordid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} {_lstvienphi_loaitru} group by vienphiid,bhyt_groupcode,medicalrecordid) ser group by ser.vienphiid,ser.medicalrecordid) serv on serv.medicalrecordid=mrd.medicalrecordid) TMP;

				
				
				

				
---Chi tiet - 27/11/2018
SELECT row_number () over (order by vp.vienphiid) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	de.departmentname,
	mrd.thoigianvaovien as tgdangky,
	stt.sothutudate_start as tgbatdaukham,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' then mrd.thoigianravien end) as tgketthuckham,
	(case when stt.sothutustatus>=1 then ((DATE_PART('day',stt.sothutudate_start-mrd.thoigianvaovien)*24+DATE_PART('hour',stt.sothutudate_start-mrd.thoigianvaovien))*60+DATE_PART('minute',stt.sothutudate_start-mrd.thoigianvaovien)) end) as tg_chokham,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamls_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 and serv.isxn=0 and serv.iscdha=0 and serv.istdcn=0 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamls_it,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxn_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=2 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxn_it,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdha_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=3 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdha_it,
	(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdhatdcn_qlcl,
	(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and (serv.iskb+serv.isxn+serv.iscdha+serv.istdcn)=4 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdhatdcn_it,
	serv.iskb,
	serv.isxn,
	serv.iscdha,
	serv.istdcn
FROM
	(select vienphiid,hosobenhanid,patientid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp} {_lstvienphi_loaitru}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus,departmentid from medicalrecord where loaibenhanid=24 and departmentgroupid in (33,46) and departmentid not in (219,224,221,239) {_doituongbenhnhanid} {_tieuchi_mrd} {_lstvienphi_loaitru}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select medicalrecordid,min(sothutudate_start) as sothutudate_start,max(sothutudate_end) as sothutudate_end,max(sothutustatus) as sothutustatus from sothutuphongkham where 1=1 and sothutudate_start<>'0001-01-01 00:00:00' {_tieuchi_stt} group by medicalrecordid) stt on stt.medicalrecordid=mrd.medicalrecordid
	inner join (select ser.vienphiid,ser.medicalrecordid,
				max(case when ser.bhyt_groupcode='01KB' then 1 else 0 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1  else 0 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','07KTC') then 1  else 0 end) as iscdha,
				max(case when ser.bhyt_groupcode='05TDCN' then 1  else 0 end) as istdcn
			from	
				(select vienphiid,bhyt_groupcode,medicalrecordid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} {_lstvienphi_loaitru} group by vienphiid,bhyt_groupcode,medicalrecordid) ser group by ser.vienphiid,ser.medicalrecordid) serv on serv.medicalrecordid=mrd.medicalrecordid 
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=mrd.departmentid;
				
				
				
--ngay 27/11/2018

-- Loại trừ những bệnh nhân sử dụng dịch vụ theo tiêu chuẩn loại trừ

SELECT vp.vienphiid
FROM
	(select vienphiid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus from medicalrecord where loaibenhanid=24 and departmentgroupid in (33,46) and departmentid not in (219,224,221,239) {_doituongbenhnhanid} {_tieuchi_mrd}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select ser.vienphiid,ser.medicalrecordid
				from (select vienphiid,medicalrecordid from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} {_lstDSDVKT_Ser} group by vienphiid,medicalrecordid) ser group by ser.vienphiid,ser.medicalrecordid) serv on serv.medicalrecordid=mrd.medicalrecordid;

















	



