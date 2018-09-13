--Bao cao Bom tiem Hao phi
--ucBC47_BomTiemHPTrongXN

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	count(serxn.*) as soluong_luot,
	sum(serhp.soluong_hpng) as soluong_hpng,
	sum(serhp.soluong_hppttt) as soluong_hppttt
FROM
	(select ser.vienphiid,ser.departmentgroupid from (select vienphiid,departmentgroupid,servicepricedate,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','07KTC') {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 {_tieuchi_mbp}) mbp on mbp.maubenhphamid=ser.maubenhphamid group by ser.vienphiid,ser.departmentgroupid,ser.servicepricedate) serxn
	left join (select ser.vienphiid,ser.departmentgroupid,sum(case when ser.loaidoituong=5 then ser.soluong else 0 end) as soluong_hpng,sum(case when ser.loaidoituong=7 then ser.soluong else 0 end) as soluong_hppttt from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,soluong from serviceprice where bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and loaidoituong in (5,7) {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select medicinecode from medicine_ref where medicinegroupcode='VT02') serf on serf.medicinecode=ser.servicepricecode group by ser.vienphiid,ser.departmentgroupid) serhp on serhp.vienphiid=serxn.vienphiid
	inner join (select vienphiid,patientid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 {_tieuchi_vp} {_vienphi_stt}) vp on vp.vienphiid=serxn.vienphiid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=serxn.departmentgroupid
GROUP BY degp.departmentgroupname;






----Chi tiet: ngay 11/9/2018

SELECT row_number () over (order by vp.vienphidate) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	count(serxn.*) as soluong_luot,
	sum(serhp.soluong_hpng) as soluong_hpng,
	sum(serhp.soluong_hppttt) as soluong_hppttt,
	vp.vienphidate,
	(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus
FROM 
	(select ser.vienphiid,ser.departmentgroupid from (select vienphiid,departmentgroupid,servicepricedate,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','07KTC') {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 {_tieuchi_mbp}) mbp on mbp.maubenhphamid=ser.maubenhphamid group by ser.vienphiid,ser.departmentgroupid,ser.servicepricedate) serxn
	left join (select ser.vienphiid,ser.departmentgroupid,sum(case when ser.loaidoituong=5 then ser.soluong else 0 end) as soluong_hpng,sum(case when ser.loaidoituong=7 then ser.soluong else 0 end) as soluong_hppttt from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,soluong from serviceprice where bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and loaidoituong in (5,7) {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select medicinecode from medicine_ref where medicinegroupcode='VT02') serf on serf.medicinecode=ser.servicepricecode group by ser.vienphiid,ser.departmentgroupid) serhp on serhp.vienphiid=serxn.vienphiid
	inner join (select vienphiid,patientid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 {_tieuchi_vp} {_vienphi_stt}) vp on vp.vienphiid=serxn.vienphiid
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
GROUP BY vp.vienphiid,vp.patientid,hsba.patientname,vp.vienphidate,vp.vienphidate_ravien,vp.vienphistatus_vp,vp.duyet_ngayduyet_vp,vp.vienphistatus

















