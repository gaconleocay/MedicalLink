--BC Thời gian khám chữa bệnh
--Ngày 17/10/2018


SELECT TMP.sl_bn,
	(TMP.tg_chokham/TMP.sl_bn) as tg_chokham,
	(TMP.tg_khamls_qlcl/TMP.sl_bn) as tg_khamls_qlcl,
	(TMP.tg_khamls_it/TMP.sl_bn) as tg_khamls_it,
	(TMP.tg_khamlsxn_qlcl/TMP.sl_bn) as tg_khamlsxn_qlcl,
	(TMP.tg_khamlsxn_it/TMP.sl_bn) as tg_khamlsxn_it,
	(TMP.tg_khamlsxncdha_qlcl/TMP.sl_bn) as tg_khamlsxncdha_qlcl,
	(TMP.tg_khamlsxncdha_it/TMP.sl_bn) as tg_khamlsxncdha_it,
	(TMP.tg_khamlsxncdhatdcn_qlcl/TMP.sl_bn) as tg_khamlsxncdhatdcn_qlcl,
	(TMP.tg_khamlsxncdhatdcn_it/TMP.sl_bn) as tg_khamlsxncdhatdcn_it
FROM 
(SELECT
	count(vp.*) as sl_bn,
	sum(case when stt.sothutustatus>=1 then ((DATE_PART('day',stt.sothutudate_start-mrd.thoigianvaovien)*24+DATE_PART('hour',stt.sothutudate_start-mrd.thoigianvaovien))*60+DATE_PART('minute',stt.sothutudate_start-mrd.thoigianvaovien)) end) as tg_chokham,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamls_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.iskb=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamls_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxn_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdha_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdha_it,
	sum(case when mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 and serv.istdcn=1 then ((DATE_PART('day',mrd.thoigianravien-mrd.thoigianvaovien)*24+DATE_PART('hour',mrd.thoigianravien-mrd.thoigianvaovien))*60+DATE_PART('minute',mrd.thoigianravien-mrd.thoigianvaovien)) end) as tg_khamlsxncdhatdcn_qlcl,
	sum(case when stt.sothutustatus>=1 and mrd.thoigianravien<>'0001-01-01 00:00:00' and serv.isxn=1 and serv.iscdha=1 and serv.istdcn=1 then ((DATE_PART('day',mrd.thoigianravien-stt.sothutudate_start)*24+DATE_PART('hour',mrd.thoigianravien-stt.sothutudate_start))*60+DATE_PART('minute',mrd.thoigianravien-stt.sothutudate_start)) end) as tg_khamlsxncdhatdcn_it	
FROM 
	(select vienphiid from vienphi where 1=1 {_doituongbenhnhanid} {_tieuchi_vp}) vp
	inner join (select vienphiid,medicalrecordid,thoigianvaovien,thoigianravien,medicalrecordstatus from medicalrecord where loaibenhanid=24 {_doituongbenhnhanid} {_tieuchi_mrd}) mrd on mrd.vienphiid=vp.vienphiid
	left join (select medicalrecordid,sothutudate,sothutudate_start,sothutudate_end,sothutustatus from sothutuphongkham where 1=1 {_tieuchi_stt}) stt on stt.medicalrecordid=mrd.medicalrecordid
	left join (select ser.vienphiid,
				max(case when ser.bhyt_groupcode='01KB' then 1 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','05TDCN','07KTC') then 1 end) as iscdha,
				max(case when ser.bhyt_groupcode='05TDCN' then 1 end) as istdcn
			from	
				(select vienphiid,bhyt_groupcode from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC') {_tieuchi_ser} group by vienphiid,bhyt_groupcode) ser group by ser.vienphiid) serv on serv.vienphiid=vp.vienphiid) TMP;


				
				
				
				
				
	


