---BÁO CÁO CHI TIẾT SỬ DỤNG THUỐC THEO KHOA 
--ucBC50_SuDungThuocTheoKhoaChiTiet
--ngay 11/10/2018

--Lấy danh mục thuốc/vật tư




--Lấy DS bệnh nhân

SELECT
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	bh.bhytcode
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where 1=1 {_tieuchi_bh}) bh on bh.bhytid=vp.bhytid
	
	
--Lấy thuốc/vật tư chi tiết	
SELECT
	mef.medicinerefid_org,
	ser.servicepricecode,
	TO_CHAR(ser.servicepricedate,'HH24:MI dd/MM/yyyy') as servicepricedate,
	ser.soluong,
	ser.soluong*ser.dongia
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
	inner join (select vienphiid,servicepricecode,servicepricedate,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong from serviceprice where thuockhobanle=0 and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_lstKhoaChonLayBC}) ser on ser.vienphiid=vp.vienphiid
	inner join (select medicinerefid_org,medicinecode,medicinename from medicine_ref) mef on mef.medicinecode=ser.servicepricecode
	
	

/*
--Lay DS benh nhan v1
SELECT
	O.vienphiid,
	O.patientid,
	hsba.patientname,
	bh.bhytcode
FROM 
	(select vp.vienphiid,vp.hosobenhanid,vp.bhytid,vp.patientid from 
		(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
		inner join (select vienphiid from serviceprice where thuockhobanle=0 and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_lstKhoaChonLayBC}) ser on ser.vienphiid=vp.vienphiid
	   group by vp.vienphiid,vp.hosobenhanid,vp.bhytid,vp.patientid) O
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=O.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where 1=1 {_tieuchi_bh}) bh on bh.bhytid=O.bhytid

*/




SELECT
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	bh.bhytcode
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 and vienphidate between '2018-01-01 00:00:00' and '2018-02-01 00:00:00') vp
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 and hosobenhandate>='2017-01-01 00:00:00') hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where 1=1 and bhytdate>='2017-01-01 00:00:00') bh on bh.bhytid=vp.bhytid




--Lấy Chi tiết thuốc/vật tư

























