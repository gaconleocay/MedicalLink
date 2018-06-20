--Danh sách hưởng tiền dịch vụ yêu cầu chất lượng cao												
--BC118_DSHuongTienDVYCCLC

--ngay 20/6/2018

SELECT	row_number () over (order by nv.username) as stt,
	nv.username,nv.usercode,
	'' as departmentgroupname,
	sum(case when (PT.user_loai='mochinhid' and PT.dongia=2000000) then PT.soluong else 0 end) as mochinh_sl2,
	sum(case when (PT.user_loai='mochinhid' and PT.dongia=3000000) then PT.soluong else 0 end) as mochinh_sl3,
	sum(case when (PT.user_loai='mochinhid' and PT.dongia=5000000) then PT.soluong else 0 end) as mochinh_sl5,
	sum(case when (PT.user_loai='phuid' and PT.dongia=2000000) then PT.soluong else 0 end) as phu_sl2,
	sum(case when (PT.user_loai='phuid' and PT.dongia=3000000) then PT.soluong else 0 end) as phu_sl3,
	sum(case when (PT.user_loai='phuid' and PT.dongia=5000000) then PT.soluong else 0 end) as phu_sl5,
	sum(case PT.user_loai
			when 'mochinhid' then PT.soluong*PT.dongia
		else 0 end) as mochinh_tien,
	sum(case PT.user_loai
			when 'mochinhid' then PT.soluong*PT.dongia*0.02
		else 0 end) as mochinh_thue2,
	sum(case PT.user_loai
			when 'mochinhid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.25
		else 0 end) as mochinh_sauthue,
	sum(case PT.user_loai
			when 'phuid' then PT.soluong*PT.dongia
		else 0 end) as phu_tien,
	sum(case PT.user_loai
			when 'phuid' then PT.soluong*PT.dongia*0.02
		else 0 end) as phu_thue2,
	sum(case PT.user_loai
			when 'phuid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.05
		else 0 end) as phu_sauthue,	
	(sum(case PT.user_loai
			when 'mochinhid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.25
		else 0 end)
	+ sum(case PT.user_loai
			when 'phuid' then (PT.soluong*PT.dongia-PT.soluong*PT.dongia*0.02)*0.05
		else 0 end)) as thuclinh,	
	'' as kynhan
FROM
	(select pttt.mochinhid as userid,
			'mochinhid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.dongia
	from (select vienphiid,soluong,mochinhid,dongia from ml_thuchienpttt where mochinhid>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.mochinhid,pttt.dongia
	union all
	select pttt.phu1id as userid,
			'phuid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.dongia
	from (select vienphiid,soluong,phu1id,dongia from ml_thuchienpttt where phu1id>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.phu1id,pttt.dongia
	union all
	select pttt.phu2id as userid,
			'phuid' as user_loai,
			sum(pttt.soluong) as soluong,
			pttt.dongia
	from (select vienphiid,soluong,phu2id,dongia from ml_thuchienpttt where phu2id>0 "+tieuchi_ser+lstdichvu_ser+") pttt
		inner join (select *
					from dblink('myconn','select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+"')
					as vienphi(vienphiid integer)) vienphi on vienphi.vienphiid=pttt.vienphiid
		group by pttt.phu2id,pttt.dongia) PT
INNER JOIN ml_nhanvien nv ON nv.userhisid=PT.userid
GROUP BY nv.usercode,nv.username;



