--Danh sách hưởng tiền dịch vụ mổ yêu cầu											
--BC119_DSHuongTienDVMoYC

--ngay 20/6/2018

SELECT	row_number () over (order by nv.username) as stt,
	nv.username,nv.usercode,
	'' as departmentgroupname,
	'' as sotaikhoan,
	sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as mochinh_sltt,
	sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as mochinh_sll1,
	sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as mochinh_sll2,
	sum(case when (PT.user_loai='mochinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as mochinh_sll3,	
	sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as phu_sltt,
	sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as phu_sll1,
	sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as phu_sll2,
	sum(case when (PT.user_loai='phuid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as phu_sll3,	
	sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as bacsigayme_sltt,
	sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as bacsigayme_sll1,
	sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as bacsigayme_sll2,
	sum(case when (PT.user_loai='bacsigaymeid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as bacsigayme_sll3,
	sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ktvphume_sltt,
	sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ktvphume_sll1,
	sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ktvphume_sll2,
	sum(case when (PT.user_loai='ktvphumeid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ktvphume_sll3,
	sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as dungcuvien_sltt,
	sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as dungcuvien_sll1,
	sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as dungcuvien_sll2,
	sum(case when (PT.user_loai='dungcuvienid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as dungcuvien_sll3,
	sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ddhoitinh_sltt,
	sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ddhoitinh_sll1,
	sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ddhoitinh_sll2,
	sum(case when (PT.user_loai='ddhoitinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ddhoitinh_sll3,
	sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ktvhoitinh_sltt,
	sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ktvhoitinh_sll1,
	sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ktvhoitinh_sll2,
	sum(case when (PT.user_loai='ktvhoitinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ktvhoitinh_sll3,
	sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as ddhanhchinh_sltt,
	sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as ddhanhchinh_sll1,
	sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as ddhanhchinh_sll2,
	sum(case when (PT.user_loai='ddhanhchinhid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as ddhanhchinh_sll3,
	sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11620-4506') then PT.soluong else 0 end) as holy_sltt,
	sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11621-4524') then PT.soluong else 0 end) as holy_sll1,
	sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11622-4536') then PT.soluong else 0 end) as holy_sll2,
	sum(case when (PT.user_loai='holyid' and PT.servicepricecode='U11623-4610') then PT.soluong else 0 end) as holy_sll3,	
	sum(case PT.user_loai
			when 'mochinhid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*5000000
										when 'U11621-4524' then PT.soluong*1500000
										when 'U11622-4536' then PT.soluong*1300000
										when 'U11622-4536' then PT.soluong*1200000
										else 0 end)
			when 'phuid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*1300000
										when 'U11621-4524' then PT.soluong*750000
										when 'U11622-4536' then PT.soluong*600000
										when 'U11622-4536' then PT.soluong*500000
										else 0 end)
			when 'bacsigaymeid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*500000
										when 'U11621-4524' then PT.soluong*400000
										when 'U11622-4536' then PT.soluong*350000
										when 'U11622-4536' then PT.soluong*325000
										else 0 end)
			when 'ktvphumeid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*200000
										when 'U11621-4524' then PT.soluong*160000
										when 'U11622-4536' then PT.soluong*140000
										when 'U11622-4536' then PT.soluong*130000
										else 0 end)							
			when 'dungcuvienid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*200000
										when 'U11621-4524' then PT.soluong*160000
										when 'U11622-4536' then PT.soluong*140000
										when 'U11622-4536' then PT.soluong*130000
										else 0 end)
			when 'ddhoitinhid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*30000
										when 'U11621-4524' then PT.soluong*24000
										when 'U11622-4536' then PT.soluong*21000
										when 'U11622-4536' then PT.soluong*19500
										else 0 end)
			when 'ktvhoitinhid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*30000
										when 'U11621-4524' then PT.soluong*24000
										when 'U11622-4536' then PT.soluong*21000
										when 'U11622-4536' then PT.soluong*19500
										else 0 end)
			when 'ddhanhchinhid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*30000
										when 'U11621-4524' then PT.soluong*24000
										when 'U11622-4536' then PT.soluong*21000
										when 'U11622-4536' then PT.soluong*19500
										else 0 end)
			when 'holyid' then (case PT.servicepricecode
										when 'U11620-4506' then PT.soluong*10000
										when 'U11621-4524' then PT.soluong*8000
										when 'U11622-4536' then PT.soluong*7000
										when 'U11622-4536' then PT.soluong*6500
										else 0 end)
		else 0 end) as thuclinh,
	'' as kynhan
FROM
	(select pttt.mochinhid as userid,
			'mochinhid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,mochinhid,servicepricecode from ml_thuchienpttt where mochinhid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.mochinhid,pttt.servicepricecode
	union all
	select pttt.phu1id as userid,
			'phuid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,phu1id,servicepricecode from ml_thuchienpttt where phu1id>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.phu1id,pttt.servicepricecode
	union all
	select pttt.phu2id as userid,
			'phuid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,phu2id,servicepricecode from ml_thuchienpttt where phu2id>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.phu2id,pttt.servicepricecode
	union all
	select pttt.bacsigaymeid as userid,
			'bacsigaymeid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,bacsigaymeid,servicepricecode from ml_thuchienpttt where bacsigaymeid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.bacsigaymeid,pttt.servicepricecode		
	union all
	select pttt.ktvphumeid as userid,
			'ktvphumeid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,ktvphumeid,servicepricecode from ml_thuchienpttt where ktvphumeid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.ktvphumeid,pttt.servicepricecode		
	union all
	select pttt.dungcuvienid as userid,
			'dungcuvienid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,dungcuvienid,servicepricecode from ml_thuchienpttt where dungcuvienid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.dungcuvienid,pttt.servicepricecode		
	union all
	select pttt.ddhoitinhid as userid,
			'ddhoitinhid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,ddhoitinhid,servicepricecode from ml_thuchienpttt where ddhoitinhid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.ddhoitinhid,pttt.servicepricecode		
	union all
	select pttt.ktvhoitinhid as userid,
			'ktvhoitinhid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,ktvhoitinhid,servicepricecode from ml_thuchienpttt where ktvhoitinhid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.ktvhoitinhid,pttt.servicepricecode
	union all
	select pttt.ddhanhchinhid as userid,
			'ddhanhchinhid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,ddhanhchinhid,servicepricecode from ml_thuchienpttt where ddhanhchinhid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.ddhanhchinhid,pttt.servicepricecode		
	union all
	select pttt.holyid as userid,
			'holyid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.servicepricecode
	from (select vienphiid,soluong,holyid,servicepricecode from ml_thuchienpttt where holyid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.holyid,pttt.servicepricecode		
		) PT
INNER JOIN ml_nhanvien nv ON nv.userhisid=PT.userid
GROUP BY nv.usercode,nv.username;


