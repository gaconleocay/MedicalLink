--Bao cao Bom tiem Hao phi
--ucBC47_BomTiemHPTrongXN

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	count(serxn.*) as soluong_luot,
	sum(serhp.soluong_hpng) as soluong_hpng,
	sum(serhp.soluong_hppttt) as soluong_hppttt
FROM
(select departmentgroupid,departmentgroupname from departmentgroup) degp
	left join (select ser.vienphiid,ser.departmentgroupid from (select vienphiid,departmentgroupid,servicepricedate,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','07KTC') {_tieuchi_ser} group by vienphiid,departmentgroupid,servicepricedate) ser inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 {_tieuchi_mbp}) mbp on mbp.maubenhphamid=ser.maubenhphamid) serxn on serxn.departmentgroupid=degp.departmentgroupid
	left join ((select ser.vienphiid,ser.departmentgroupid,sum(case when ser.loaidoituong=5 then ser.soluong else 0 end) as soluong_hpng,sum(case when ser.loaidoituong=7 then ser.soluong else 0 end) as soluong_hppttt from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,soluong from serviceprice where loaidoituong in (5,7) {_tieuchi_ser}) ser inner join (select * from servicepriceref where servicepricegroupcode='VT02') serf on serf.servicepricecode=ser.servicepricecode group by ser.vienphiid,ser.departmentgroupid) serhp) serhp on serhp.vienphiid=serxn.vienphiid
	inner join (select vienphiid from vienphi where 1=1 {_tieuchi_vp} {_vienphi_stt}) vp on vp.vienphiid=serxn.vienphiid
GROUP BY degp.departmentgroupname;





vienphiid
patientid
patientname
soluong_luot
soluong_hpng
soluong_hppttt
vienphidate
vienphidate_ravien
duyet_ngayduyet_vp	
vienphistatus