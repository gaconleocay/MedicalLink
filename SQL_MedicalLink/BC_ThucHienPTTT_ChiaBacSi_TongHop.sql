--Bao cao Tong hop thanh toan PTTT theo bac si - noi tru 
---ngay 23/9
--ngay 9/11 bo gia tien Thu thuat loai 1 _ giup viec 1
//ps: bo sung them ty le thanh toan.

SELECT O.*,
	  (O.ptdb_moi+O.ptdb_mc+O.ptdb_phu+O.ptdb_gv) as ptdb_tong,
	  (O.ptl1_moi+O.ptl1_mc+O.ptl1_phu+O.ptl1_gv) as ptl1_tong,
	  (O.ptl2_moi+O.ptl2_mc+O.ptl2_phu+O.ptl2_gv) as ptl2_tong,
	  (O.ptl3_moi+O.ptl3_mc+O.ptl3_phu+O.ptl3_gv) as ptl3_tong,
	  (O.ptdb_moi+O.ptdb_mc+O.ptdb_phu+O.ptdb_gv+O.ptl1_moi+O.ptl1_mc+O.ptl1_phu+O.ptl1_gv+O.ptl2_moi+O.ptl2_mc+O.ptl2_phu+O.ptl2_gv+O.ptl3_moi+O.ptl3_mc+O.ptl3_phu+O.ptl3_gv) as pt_tongsl,
	  (O.ptdb_moi*280000+O.ptdb_mc*280000+O.ptdb_phu*200000+O.ptdb_gv*120000+O.ptl1_moi*125000+O.ptl1_mc*125000+O.ptl1_phu*90000+O.ptl1_gv*70000+O.ptl2_moi*65000+O.ptl2_mc*65000+O.ptl2_phu*50000+O.ptl2_gv*30000+O.ptl3_moi*50000+O.ptl3_mc*50000+O.ptl3_phu*30000+O.ptl3_gv*15000) as pt_tongtien,  
	  (O.ttdb_mc+O.ttdb_phu+O.ttdb_gv) as ttdb_tong,
	  (O.ttl1_mc+O.ttl1_phu+O.ttl1_gv) as ttl1_tong,		
	  (O.ttl2_mc+O.ttl2_phu+O.ttl2_gv) as ttl2_tong,
	  (O.ttl3_mc+O.ttl3_phu+O.ttl3_gv) as ttl3_tong,
	  (O.ttdb_mc+O.ttdb_phu+O.ttdb_gv+O.ttl1_mc+O.ttl1_phu+O.ttl1_gv+O.ttl2_mc+O.ttl2_phu+O.ttl2_gv+O.ttl3_mc+O.ttl3_phu+O.ttl3_gv) as tt_tongsl,
	  (O.ttdb_mc*84000+O.ttdb_phu*60000+O.ttdb_gv*36000+O.ttl1_mc*37500+O.ttl1_phu*27000+O.ttl1_gv*0+O.ttl2_mc*19500+O.ttl2_phu*0+O.ttl2_gv*9000+O.ttl3_mc*15000+O.ttl3_phu*0+O.ttl3_gv*4500) as tt_tongtien,
	  (O.ptdb_moi*280000+O.ptdb_mc*280000+O.ptdb_phu*200000+O.ptdb_gv*120000+O.ptl1_moi*125000+O.ptl1_mc*125000+O.ptl1_phu*90000+O.ptl1_gv*70000+O.ptl2_moi*65000+O.ptl2_mc*65000+O.ptl2_phu*50000+O.ptl2_gv*30000+O.ptl3_moi*50000+O.ptl3_mc*50000+O.ptl3_phu*30000+O.ptl3_gv*15000+O.ttdb_mc*84000+O.ttdb_phu*60000+O.ttdb_gv*36000+O.ttl1_mc*37500+O.ttl1_phu*27000+O.ttl1_gv*0+O.ttl2_mc*19500+O.ttl2_phu*0+O.ttl2_gv*9000+O.ttl3_mc*15000+O.ttl3_phu*0+O.ttl3_gv*4500) as tongtien
FROM
	(SELECT row_number () over (order by degp.departmentgroupname,nv.username) as stt,
		U.userid,
		nv.username,	
		U.departmentgroupid,
		degp.departmentgroupname,
		sum(case when U.pttt_loaiid=1 and U.user_loai='phauthuatvien2' then U.soluong else 0 end) as ptdb_moi,
		sum(case when U.pttt_loaiid=1 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptdb_mc,
		sum(case when U.pttt_loaiid=1 and U.user_loai='phumo1' then U.soluong else 0 end) as ptdb_phu,
		sum(case when U.pttt_loaiid=1 and U.user_loai='phumo3' then U.soluong else 0 end) as ptdb_gv,
		sum(case when U.pttt_loaiid=2 and U.user_loai='phauthuatvien2' then U.soluong else 0 end) as ptl1_moi,
		sum(case when U.pttt_loaiid=2 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptl1_mc,
		sum(case when U.pttt_loaiid=2 and U.user_loai='phumo1' then U.soluong else 0 end) as ptl1_phu,
		sum(case when U.pttt_loaiid=2 and U.user_loai='phumo3' then U.soluong else 0 end) as ptl1_gv,		
		sum(case when U.pttt_loaiid=3 and U.user_loai='phauthuatvien2' then U.soluong else 0 end) as ptl2_moi,
		sum(case when U.pttt_loaiid=3 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptl2_mc,
		sum(case when U.pttt_loaiid=3 and U.user_loai='phumo1' then U.soluong else 0 end) as ptl2_phu,
		sum(case when U.pttt_loaiid=3 and U.user_loai='phumo3' then U.soluong else 0 end) as ptl2_gv,		
		sum(case when U.pttt_loaiid=4 and U.user_loai='phauthuatvien2' then U.soluong else 0 end) as ptl3_moi,
		sum(case when U.pttt_loaiid=4 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ptl3_mc,
		sum(case when U.pttt_loaiid=4 and U.user_loai='phumo1' then U.soluong else 0 end) as ptl3_phu,
		sum(case when U.pttt_loaiid=4 and U.user_loai='phumo3' then U.soluong else 0 end) as ptl3_gv,	
		sum(case when U.pttt_loaiid=5 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttdb_mc,
		sum(case when U.pttt_loaiid=5 and U.user_loai='phumo1' then U.soluong else 0 end) as ttdb_phu,
		sum(case when U.pttt_loaiid=5 and U.user_loai='phumo3' then U.soluong else 0 end) as ttdb_gv,		
		sum(case when U.pttt_loaiid=6 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttl1_mc,
		sum(case when U.pttt_loaiid=6 and U.user_loai='phumo1' then U.soluong else 0 end) as ttl1_phu,
		sum(case when U.pttt_loaiid=6 and U.user_loai='phumo3' then U.soluong else 0 end) as ttl1_gv,		
		sum(case when U.pttt_loaiid=7 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttl2_mc,
		sum(case when U.pttt_loaiid=7 and U.user_loai='phumo1' then U.soluong else 0 end) as ttl2_phu,
		sum(case when U.pttt_loaiid=7 and U.user_loai='phumo3' then U.soluong else 0 end) as ttl2_gv,		
		sum(case when U.pttt_loaiid=8 and U.user_loai='phauthuatvien' then U.soluong else 0 end) as ttl3_mc,
		sum(case when U.pttt_loaiid=8 and U.user_loai='phumo1' then U.soluong else 0 end) as ttl3_phu,
		sum(case when U.pttt_loaiid=8 and U.user_loai='phumo3' then U.soluong else 0 end) as ttl3_gv			
	FROM 
		(select pttt.phauthuatvien2 as userid,
			   'phauthuatvien2' as user_loai,
			   sum(ser.soluong) as soluong,
			   serf.pttt_loaiid,
			   ser.departmentgroupid
		from (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') "+_tieuchi_ser+lstKhoacheck+" ) ser
			inner join (select servicepriceid,phauthuatvien2,phauthuatvien,phumo1,phumo3 from phauthuatthuthuat where phauthuatvien2>0 "+_tieuchi_pttt+") pttt on pttt.servicepriceid=ser.servicepriceid
			inner join (select vienphiid from vienphi  "+_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
			inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode 
		group by pttt.phauthuatvien2,serf.pttt_loaiid,ser.departmentgroupid
		union all
		select pttt.phauthuatvien as userid,
			   'phauthuatvien' as user_loai,
			   sum(ser.soluong) as soluong,
			   serf.pttt_loaiid,
			   ser.departmentgroupid
		from (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') "+_tieuchi_ser+lstKhoacheck+" ) ser
			inner join (select servicepriceid,phauthuatvien2,phauthuatvien,phumo1,phumo3 from phauthuatthuthuat where phauthuatvien>0 "+_tieuchi_pttt+") pttt on pttt.servicepriceid=ser.servicepriceid
			inner join (select vienphiid from vienphi  "+_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
			inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode 
		group by pttt.phauthuatvien,serf.pttt_loaiid,ser.departmentgroupid
		union all
		select pttt.phumo1 as userid,
			   'phumo1' as user_loai,
			   sum(ser.soluong) as soluong,
			   serf.pttt_loaiid,
			   ser.departmentgroupid
		from (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') "+_tieuchi_ser+lstKhoacheck+" ) ser
			inner join (select servicepriceid,phauthuatvien2,phauthuatvien,phumo1,phumo3 from phauthuatthuthuat where phumo1>0 "+_tieuchi_pttt+") pttt on pttt.servicepriceid=ser.servicepriceid
			inner join (select vienphiid from vienphi  "+_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
			inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode 
		group by pttt.phumo1,serf.pttt_loaiid,ser.departmentgroupid
		union all
		select pttt.phumo3 as userid,
			   'phumo3' as user_loai,
			   sum(ser.soluong) as soluong,
			   serf.pttt_loaiid,
			   ser.departmentgroupid
		from (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') "+_tieuchi_ser+lstKhoacheck+" ) ser
			inner join (select servicepriceid,phauthuatvien2,phauthuatvien,phumo1,phumo3 from phauthuatthuthuat where phumo3>0 "+_tieuchi_pttt+") pttt on pttt.servicepriceid=ser.servicepriceid
			inner join (select vienphiid from vienphi "+_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid
			inner join (select servicepricecode,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid>0) serf on serf.servicepricecode=ser.servicepricecode 
		group by pttt.phumo3,serf.pttt_loaiid,ser.departmentgroupid) U
	LEFT JOIN nhompersonnel nv ON nv.userhisid=U.userid	
	INNER JOIN departmentgroup degp on degp.departmentgroupid=U.departmentgroupid
	GROUP BY U.userid,nv.username,U.departmentgroupid,degp.departmentgroupname) O;










	
	

