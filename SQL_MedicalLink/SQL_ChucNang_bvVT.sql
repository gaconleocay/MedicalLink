---Bao cao BHYT

SELECT hsba.patientid, 
	log.vienphiid, 
	hsba.sovaovien, 
	'' as so_dkbhyt,
	hsba.patientname,
	(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam,
	(case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nu,
	bh.bhytcode,
	bh.macskcbbd,
	bh.bhytfromdate,
	bh.bhytutildate,
	log.logeventcontent,
	bh.noisinhsong,
	bh.du5nam6thangluongcoban,
	'' as khoa_dangky,
	replace(log.logform, 'TAB:', '') as noi_dangky,
	log.loguser as nguoi_dangky,
	nv.username,
	(case bh.bhyt_loaiid
			when 1 then 'Đúng tuyến'
			when 2 then 'Đúng tuyến (giới thiệu)'
			when 3 then 'Đúng tuyến (cấp cứu)'
			when 4 then 'Trái tuyến'
			else '' end) as bhyt_loaiid,
	'' as huy_dangky,
	log.logeventtype,
	log.logtime
	
FROM hosobenhan hsba inner join bhyt bh on hsba.hosobenhanid=bh.hosobenhanid
	left join logevent log on log.hosobenhanid=hsba.hosobenhanid 
	and log.logtime>='2017-03-01 00:00:00' and log.logtime<='2017-03-15 23:59:59'
	and log.logeventcontent like '%' || bh.bhytcode || '%'
	left join tools_tblnhanvien nv on nv.usercode=log.loguser
WHERE bh.bhytdate>='2017-03-01 00:00:00' and bh.bhytdate<='2017-03-15 23:59:59' 
	and bh.bhytcode<>''
	and log.logeventtype in (1,8)
GROUP BY hsba.patientid,log.vienphiid,hsba.sovaovien,hsba.patientname,hsba.gioitinhcode,hsba.birthday,
bh.bhytcode,bh.macskcbbd,bh.bhytfromdate,bh.bhytutildate,log.logeventcontent,bh.noisinhsong,
bh.du5nam6thangluongcoban,log.logform,log.loguser,nv.username,bh.bhyt_loaiid,log.logeventtype,log.logtime
ORDER BY log.logtime;
-----------
select patientid,vienphiid, duyet_ngayduyet_vp,
sum (money_vtthaythe_vp + money_vtthaythe_bh) 
from serviceprice_pttt 
where --departmentgroupid=21
--patientid in (558082)
vienphiid=751283
and duyet_ngayduyet_vp>='2016-12-01:00:00' and duyet_ngayduyet_vp<='2016-12-06:00:00'
group by patientid,vienphiid,duyet_ngayduyet_vp
order by patientid;
-----------------
PATIENTID	VIENPHIID	SOVAOVIEN	SO_DKBHYT	PATIENTNAME	YEAR_NAM	YEAR_NU	BHYTCODE	MACSKCBBD	BHYTFROMDATE	BHYTUTILDATE	NOISINHSONG	DU5NAM6THANGLUONGCOBAN	KHOA_DANGKY	BHYTDATE	NGUOI_DANGKY	BHYT_LOAIID	HUY_DANGKY


---------- bao cao BHYT
SELECT hsba.patientid, 
	log.vienphiid, 
	hsba.sovaovien, 
	'' as so_dkbhyt,
	hsba.patientname,
	(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam,
	(case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nu,
	bh.bhytcode,
	bh.macskcbbd,
	bh.bhytfromdate,
	bh.bhytutildate,
	log.logeventcontent,
	bh.noisinhsong,
	bh.du5nam6thangluongcoban,
	'' as khoa_dangky,
	replace(log.logform, 'TAB:', '') as noi_dangky,
	log.loguser as nguoi_dangky,
	nv.username,
	(case bh.bhyt_loaiid
			when 1 then 'Đúng tuyến'
			when 2 then 'Đúng tuyến (giới thiệu)'
			when 3 then 'Đúng tuyến (cấp cứu)'
			when 4 then 'Trái tuyến'
			else '' end) as bhyt_loaiid,
	'' as huy_dangky,
	log.logeventtype,
	log.logtime
	
FROM hosobenhan hsba inner join bhyt bh on hsba.hosobenhanid=bh.hosobenhanid
	left join logevent log on log.hosobenhanid=hsba.hosobenhanid 
	and log.logtime>='2017-03-01 00:00:00' and log.logtime<='2017-03-15 23:59:59'
	and log.logeventcontent like '%' || bh.bhytcode || '%'
	left join tools_tblnhanvien nv on nv.usercode=log.loguser
WHERE bh.bhytdate>='2017-03-01 00:00:00' and bh.bhytdate<='2017-03-15 23:59:59' 
	and bh.bhytcode<>''
	and log.logeventtype in (1,8)
GROUP BY hsba.patientid,log.vienphiid,hsba.sovaovien,hsba.patientname,hsba.gioitinhcode,hsba.birthday,
bh.bhytcode,bh.macskcbbd,bh.bhytfromdate,bh.bhytutildate,log.logeventcontent,bh.noisinhsong,
bh.du5nam6thangluongcoban,log.logform,log.loguser,nv.username,bh.bhyt_loaiid,log.logeventtype,log.logtime
ORDER BY log.logtime;
---------











