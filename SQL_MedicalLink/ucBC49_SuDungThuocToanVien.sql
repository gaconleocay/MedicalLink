--Báo cáo sử dụng thuốc chi tiết Theo khoa
--ngay 25/10/2018



SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY mef.medicinename)) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	mef.medicinerefid_org,
	mef.medicinename,
	O.dongia,
	sum(O.noitru_sl) as noitru_sl,
	sum(O.noitru_thanhtien) as noitru_thanhtien,
	sum(O.tutruc_sl) as tutruc_sl,
	sum(O.tutruc_thanhtien) as tutruc_thanhtien,
	sum(O.ton_sl) as ton_sl,
	sum(O.ton_thanhtien) as ton_thanhtien
FROM 
	(select ser.departmentgroupid,ser.dongia,ser.servicepricecode,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong end) as noitru_sl,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia end) as noitru_thanhtien,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong end) as tutruc_sl,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia end) as tutruc_thanhtien,
		0 as ton_sl,
		0 as ton_thanhtien
	from 
		(select vienphiid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp} {_doituongbenhnhanid}) vp
		  inner join (select vienphiid,departmentgroupid,servicepricecode,maubenhphamid,
						(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong
					from serviceprice where thuockhobanle=0 and soluong>0 {_bhyt_groupcode} {_tieuchi_ser} {_lstKhoaChonLayBC} {_lstDSThuocVT_Ser}) ser on ser.vienphiid=vp.vienphiid
		  inner join (select maubenhphamid,medicinestoreid from maubenhpham where maubenhphamgrouptype in (5,6) {_tieuchi_mbp} {_lstKhoaChonLayBC} {_lstKhoTTChonLayBC}) mbp on mbp.maubenhphamid=ser.maubenhphamid
	group by ser.departmentgroupid,ser.dongia,ser.servicepricecode) O
	inner join (select medicinerefid_org,medicinecode,medicinename from medicine_ref where 1=1 {_lstDSThuocVT_Mef}) mef on mef.medicinecode=O.servicepricecode
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=O.departmentgroupid
WHERE O.noitru_sl<>0 or O.tutruc_sl<>0 or O.ton_sl<>0
GROUP BY degp.departmentgroupid,degp.departmentgroupname,mef.medicinerefid_org,mef.medicinename,O.dongia;











