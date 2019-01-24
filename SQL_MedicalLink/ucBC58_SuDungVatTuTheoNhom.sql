ucBC58_SuDungVatTuTheoNhom
--Báo cáo sử dụng vật tư theo nhóm
--ngay 24/1/2019



SELECT row_number () over (order by mefg.medicinename) as stt,
	mefg.medicinerefid,
	mefg.medicinecode,
	mefg.medicinename,
	tmp.noitru_sl,
	tmp.noitru_thanhtien,
	tmp.tutruc_sl,
	tmp.tutruc_thanhtien,
	tmp.tong_sl,
	tmp.tong_thanhtien
FROM
	(SELECT	
		mef.medicinegroupcode,
		sum(O.noitru_sl) as noitru_sl,
		sum(O.noitru_thanhtien) as noitru_thanhtien,
		sum(O.tutruc_sl) as tutruc_sl,
		sum(O.tutruc_thanhtien) as tutruc_thanhtien,
		sum(O.noitru_sl)+sum(O.tutruc_sl) as tong_sl,
		sum(O.noitru_thanhtien)+sum(O.tutruc_thanhtien) as tong_thanhtien
	FROM 
		(select ser.servicepricecode,
			sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong else 0 end) as noitru_sl,
			sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia else 0 end) as noitru_thanhtien,
			sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong else 0 end) as tutruc_sl,
			sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia else 0 end) as tutruc_thanhtien
		from 
			(select vienphiid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp} {_doituongbenhnhanid}) vp
			  inner join (select vienphiid,departmentgroupid,servicepricecode,maubenhphamid,
							(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong
						from serviceprice where thuockhobanle=0 and soluong>0 {_bhyt_groupcode} {_tieuchi_ser} {_lstKhoaChonLayBC}) ser on ser.vienphiid=vp.vienphiid
			  inner join (select maubenhphamid,medicinestoreid from maubenhpham where maubenhphamgrouptype in (5,6) {_tieuchi_mbp} {_lstKhoaChonLayBC} {_lstKhoTTChonLayBC}) mbp on mbp.maubenhphamid=ser.maubenhphamid
		group by ser.servicepricecode) O
		inner join (select medicinerefid_org,medicinecode,medicinename,medicinegroupcode from medicine_ref where 1=1 {_datatype}) mef on mef.medicinecode=O.servicepricecode
	WHERE O.noitru_sl<>0 or O.tutruc_sl<>0
	GROUP BY mef.medicinegroupcode) TMP
INNER JOIN (select medicinerefid,medicinecode,medicinename from medicine_ref where 1=1 {_datatype}) mefg on mefg.medicinecode=TMP.medicinegroupcode;






---Chi tiet - 24/1/2019

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	sum(O.noitru_sl) as noitru_sl,
	sum(O.noitru_thanhtien) as noitru_thanhtien,
	sum(O.tutruc_sl) as tutruc_sl,
	sum(O.tutruc_thanhtien) as tutruc_thanhtien,
	sum(O.noitru_sl)+sum(O.tutruc_sl) as tong_sl,
	sum(O.noitru_thanhtien)+sum(O.tutruc_thanhtien) as tong_thanhtien
FROM 
	(select ser.departmentgroupid,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong else 0 end) as noitru_sl,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia else 0 end) as noitru_thanhtien,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong else 0 end) as tutruc_sl,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia else 0 end) as tutruc_thanhtien
	from 
		(select vienphiid from vienphi where 1=1 {_filter.tieuchi_vp} {_filter.trangthai_vp} {_filter.doituongbenhnhanid}) vp
		  inner join (select vienphiid,departmentgroupid,servicepricecode,maubenhphamid,
						(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong
					from serviceprice where thuockhobanle=0 and soluong>0 {_lstservicepricecode} {_filter.tieuchi_ser} {_filter.lstKhoaChonLayBC}) ser on ser.vienphiid=vp.vienphiid
		  inner join (select maubenhphamid,medicinestoreid from maubenhpham where maubenhphamgrouptype in (5,6) {_filter.tieuchi_mbp} {_filter.lstKhoaChonLayBC} {_filter.lstKhoTTChonLayBC}) mbp on mbp.maubenhphamid=ser.maubenhphamid
	group by ser.departmentgroupid,ser.dongia,ser.servicepricecode) O
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=O.departmentgroupid
WHERE O.noitru_sl<>0 or O.tutruc_sl<>0
GROUP BY degp.departmentgroupid,degp.departmentgroupname;








