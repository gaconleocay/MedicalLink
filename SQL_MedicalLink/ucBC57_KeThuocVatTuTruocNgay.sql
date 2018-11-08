--Báo cáo kê thuốc/vật tư trước ngày
--ucBC57_KeThuocVatTuTruocNgay

--ngay 8/11/2018

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=1 then 1 else 0 end) as sl_qua1,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=1 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua1,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=2 then 1 else 0 end) as sl_qua2,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=2 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua2,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=3 then 1 else 0 end) as sl_qua3,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=3 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua3,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=4 then 1 else 0 end) as sl_qua4,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=4 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua4,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=5 then 1 else 0 end) as sl_qua5,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=5 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua5,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=6 then 1 else 0 end) as sl_qua6,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=6 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua6,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=7 then 1 else 0 end) as sl_qua7,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))=7 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_qua7,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))>7 then 1 else 0 end) as sl_quahon7,
	sum(case when ((mbp.maubenhphamdate_sudung::date)-(now()::date))>7 and mbp.maubenhphamstatus in (4,5,9) then 1 else 0 end) as slthyl_quahon7
FROM (select departmentgroupid,maubenhphamdate_sudung,maubenhphamstatus from maubenhpham where maubenhphamdate_sudung>now() {_maubenhphamgrouptype} {_lstKhoaChonLayBC}) mbp
	inner join (select departmentgroupid,departmentgroupname from departmentgroup where 1=1 {_lstKhoaChonLayBC}) degp on degp.departmentgroupid=mbp.departmentgroupid
GROUP BY degp.departmentgroupid,degp.departmentgroupname;
	

	
	
---Chi tiet

SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate_sudung) as stt, 
	vp.patientid, 
	vp.vienphiid, 
	hsba.patientname,
	hsba.bhytcode,
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	mbp.maubenhphamid,
	mbp.maubenhphamdate,
	mbp.maubenhphamdate_sudung,
	((mbp.maubenhphamdate_sudung::date)-(now()::date)) as songay,
	(case when mbp.maubenhphamstatus in (4,5,9) then 'Đã THYL' end) as dathyl,
	mes.medicinestorename,
	ncd.usercode,
	ncd.username as nguoichidinh,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,	
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus
FROM 
	(select maubenhphamid,vienphiid,userid,maubenhphamstatus,maubenhphamdate,medicinestoreid,maubenhphamdate_sudung,departmentgroupid,departmentid from maubenhpham m where maubenhphamdate_sudung>now() {_maubenhphamgrouptype} {_lstKhoaChonLayBC}) mbp 
	INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 {_tieuchi_vp}) vp on vp.vienphiid=mbp.vienphiid
	LEFT JOIN (select userhisid,usercode,username from nhompersonnel) ncd on ncd.userhisid=mbp.userid
	INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=mbp.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=mbp.departmentid
	LEFT JOIN (select medicinestoreid,medicinestorename from medicine_store) mes on mes.medicinestoreid=mbp.medicinestoreid;












	
	