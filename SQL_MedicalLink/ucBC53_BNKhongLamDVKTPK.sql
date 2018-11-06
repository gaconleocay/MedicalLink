--ucBC53_BNKhongLamDVKTPK

--ngay 1/11/2018

SELECT row_number () over (order by de.departmentgroupid,de.departmentname) as stt,
	de.departmentname,
	count(mrd.*) as tong_sl,
	sum(case when vp.doituongbenhnhanid=1 then 1 else 0 end) as bh_sl,
	sum(case when vp.doituongbenhnhanid=1 and mrd.xutrikhambenhid=4 then 1 else 0 end) as bh_nhapvien,
	sum(case when vp.doituongbenhnhanid=1 and mrd.hinhthucravienid=5 then 1 else 0 end) as bh_chuyenvien,
	sum(case when vp.doituongbenhnhanid=1 and serv.isxn=0 then 1 else 0 end) as bh_koxn,
	sum(case when vp.doituongbenhnhanid=1 and serv.iscdha=0 then 1 else 0 end) as bh_kocdha,
	sum(case when vp.doituongbenhnhanid=1 and serv.isxn=0 and serv.iscdha=0 then 1 else 0 end) as bh_koxncdha,
	sum(case when vp.doituongbenhnhanid=1 and serv.ispttt=0 then 1 else 0 end) as bh_kopttt,
	sum(case when vp.doituongbenhnhanid=1 and serv.isxn=0 and serv.iscdha=0 and serv.ispttt=0 then 1 else 0 end) as bh_koptttcls,
	sum(case when vp.doituongbenhnhanid<>1 then 1 else 0 end) as vp_sl,
	sum(case when vp.doituongbenhnhanid<>1 and mrd.xutrikhambenhid=4 then 1 else 0 end) as vp_nhapvien,
	sum(case when vp.doituongbenhnhanid<>1 and mrd.hinhthucravienid=5 then 1 else 0 end) as vp_chuyenvien,
	sum(case when vp.doituongbenhnhanid<>1 and serv.isxn=0 then 1 else 0 end) as vp_koxn,
	sum(case when vp.doituongbenhnhanid<>1 and serv.iscdha=0 then 1 else 0 end) as vp_kocdha,
	sum(case when vp.doituongbenhnhanid<>1 and serv.isxn=0 and serv.iscdha=0 then 1 else 0 end) as vp_koxncdha,
	sum(case when vp.doituongbenhnhanid<>1 and serv.ispttt=0 then 1 else 0 end) as vp_kopttt,
	sum(case when vp.doituongbenhnhanid<>1 and serv.isxn=0 and serv.iscdha=0 and serv.ispttt=0 then 1 else 0 end) as vp_koptttcls	
FROM (select vienphiid,doituongbenhnhanid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp	
	inner join (select vienphiid,departmentid,xutrikhambenhid,hinhthucravienid from medicalrecord where loaibenhanid=24 {_tieuchi_mrd}) mrd on mrd.vienphiid=vp.vienphiid
	inner join (select ser.vienphiid,
				max(case when ser.bhyt_groupcode='01KB' then 1 else 0 end) as iskb,
				max(case when ser.bhyt_groupcode='03XN' then 1  else 0 end) as isxn,
				max(case when ser.bhyt_groupcode in ('04CDHA','07KTC','05TDCN') then 1  else 0 end) as iscdha,
				max(case when ser.bhyt_groupcode='06PTTT' then 1  else 0 end) as ispttt
			from	
				(select vienphiid,bhyt_groupcode from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','07KTC','06PTTT') {_tieuchi_ser} group by vienphiid,bhyt_groupcode) ser group by ser.vienphiid) serv on serv.vienphiid=vp.vienphiid 
	left join (select departmentid,departmentname,departmentgroupid from department where departmenttype in (2,3,9)) de on de.departmentid=mrd.departmentid
GROUP BY de.departmentgroupid,de.departmentname;



