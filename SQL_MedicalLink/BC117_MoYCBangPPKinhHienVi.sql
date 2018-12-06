--Bao cao Danh sách mổ Yêu cầu bằng phương pháp kính hiển vi														
--BC117_MoYCBangPPKinhHienVi


--ngay 5/12/2018 - Server HIS
SELECT
	vp.vienphiid,
	pttt.phauthuatvien,
	pttt.phumo1,
	pttt.phumo2,
	pttt.bacsigayme,
	pttt.phume,
	pttt.dungcuvien,
	ser.soluong,
	(case when (pttt.phumo1>0 and pttt.phumo2>0) then (ser.soluong/2)
			else ser.soluong end) as soluong_phu
FROM 
	(select vienphiid,servicepriceid,phauthuatvien,phumo1,phumo2,bacsigayme,phume,dungcuvien from phauthuatthuthuat where 1=1 {tieuchi_pttt}) pttt
	inner join (select vienphiid,servicepriceid,soluong from serviceprice where servicepricecode='U11970-3701' {tieuchi_ser}) ser on ser.servicepriceid=pttt.servicepriceid
	inner join (select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}) vp on vp.vienphiid=ser.vienphiid;


	
	
	
	
	
	
/*	
	
--ngay 16/7/2018 - Server nhap PTTT = O2S

SELECT	row_number () over (order by nv.username) as stt,
	nv.username,nv.usercode,
	'' as departmentgroupname,
	sum(case when PT.user_loai='mochinhid' then PT.soluong else 0 end) as sl_mochinh,
	sum(case when PT.user_loai='mochinhid' then PT.soluong*800000 else 0 end) as tien_mochinh,
	sum(case when PT.user_loai='phuid' then PT.soluong else 0 end) as sl_phu,
	sum(case when PT.user_loai='phuid' then PT.soluong*500000 else 0 end) as tien_phu,
	sum(case when PT.user_loai='bacsigaymeid' then PT.soluong else 0 end) as sl_bacsigayme,
	sum(case when PT.user_loai='bacsigaymeid' then PT.soluong*350000 else 0 end) as tien_bacsigayme,
	sum(case when PT.user_loai='ktvphumeid' then PT.soluong else 0 end) as sl_ktvphume,
	sum(case when PT.user_loai='ktvphumeid' then PT.soluong*175000 else 0 end) as tien_ktvphume,
	sum(case when PT.user_loai='dungcuvienid' then PT.soluong else 0 end) as sl_dungcuvien,
	sum(case when PT.user_loai='dungcuvienid' then PT.soluong*175000 else 0 end) as tien_dungcuvien,
	sum(case PT.user_loai
			when 'mochinhid' then PT.soluong*800000
			when 'phuid' then PT.soluong*500000
			when 'bacsigaymeid' then PT.soluong*350000
			when 'ktvphumeid' then PT.soluong*175000
			when 'dungcuvienid' then PT.soluong*175000
		else 0 end) as thuclinh,
	'' as kynhan,
	'' as ghichu
FROM
	(select pttt.mochinhid as userid,
			'mochinhid' as user_loai,
			sum(pttt.soluong) as soluong
	from (select vienphiid,soluong,mochinhid from ml_thuchienpttt where servicepricecode='U11970-3701' and mochinhid>0 {tieuchi_ser}) pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.mochinhid
	union all
	select pttt.phu1id as userid,
			'phuid' as user_loai,
			sum(pttt.soluong) as soluong
	from (select vienphiid,(case when phu2id>0 then soluong/2 else soluong end) as soluong,phu1id from ml_thuchienpttt where servicepricecode='U11970-3701' and phu1id>0 {tieuchi_ser}) pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.phu1id			
	union all
	select pttt.phu2id as userid,
			'phuid' as user_loai,
			sum(pttt.soluong) as soluong
	from (select vienphiid,(case when phu1id>0 then soluong/2 else soluong end) as soluong,phu2id from ml_thuchienpttt where servicepricecode='U11970-3701' and phu2id>0 {tieuchi_ser}) pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.phu2id			
	union all
	select pttt.bacsigaymeid as userid,
			'bacsigaymeid' as user_loai,
			sum(pttt.soluong) as soluong
	from (select vienphiid,soluong,bacsigaymeid from ml_thuchienpttt where servicepricecode='U11970-3701' and bacsigaymeid>0 {tieuchi_ser}) pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.bacsigaymeid
	union all
	select pttt.ktvphumeid as userid,
			'ktvphumeid' as user_loai,
			sum(pttt.soluong) as soluong
	from (select vienphiid,soluong,ktvphumeid from ml_thuchienpttt where servicepricecode='U11970-3701' and ktvphumeid>0 {tieuchi_ser}) pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.ktvphumeid			
	union all
	select pttt.dungcuvienid as userid,
			'dungcuvienid' as user_loai,
			sum(pttt.soluong) as soluong
	from (select vienphiid,soluong,dungcuvienid from ml_thuchienpttt where servicepricecode='U11970-3701' and dungcuvienid>0 {tieuchi_ser}) pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.dungcuvienid) PT
INNER JOIN ml_nhanvien nv ON nv.userhisid=PT.userid
GROUP BY nv.usercode,nv.username;

*/

