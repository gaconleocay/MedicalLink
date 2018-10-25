---BÁO CÁO CHI TIẾT SỬ DỤNG THUỐC THEO KHOA 
--ucBC50_SuDungThuocTheoKhoaChiTiet
--ngay 11/10/2018

--Lấy danh mục thuốc/vật tư
select medicinerefid,medicinerefid_org,medicinecode,medicinename,dangdung,hoatchat,nhomthau from medicine_ref where isremove=0;

--Lay DS nhom thuoc/vat tu
select medicinerefid,medicinereftype,medicinegroupcode,medicinecode,medicinename from medicine_ref
where medicinecode in (select medicinegroupcode from medicine_ref where isremove=0 and medicinegroupcode not in ('','NHATHUOC','T46603-4518','VT39826-0633','T37527-3420') group by medicinegroupcode)
and medicinegroupcode not in ('','NHATHUOC','T46603-4518','VT39826-0633','T37527-3420')
order by medicinereftype
	,medicinerefid
	,medicinegroupcode;



--Lấy DS bệnh nhân
--string _sqlDSBN

SELECT row_number () over (order by vp.vienphiid) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	bh.bhytcode
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where 1=1 {_tieuchi_bh}) bh on bh.bhytid=vp.bhytid
	inner join (select vienphiid from serviceprice where thuockhobanle=0 and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_lstKhoaChonLayBC} {_lstDSThuocVT_Ser} group by vienphiid) ser on ser.vienphiid=vp.vienphiid;
	
	
--Lấy thuốc/vật tư chi tiết	
--string _sqlDSThuocVT
SELECT
	vp.vienphiid,	
	mef.medicinerefid_org,
	ser.servicepricecode,
	TO_CHAR(ser.servicepricedate,'HH24:MI dd/MM/yyyy') as servicepricedate,
	TO_CHAR(ser.servicepricedate,'yyyyMMddHH24MI') as servicepricedatelong,
	ser.soluong,
	(ser.soluong*ser.dongia) as thanhtien
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
	inner join (select vienphiid,servicepricecode,servicepricedate,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong from serviceprice where thuockhobanle=0 and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_lstKhoaChonLayBC} {_lstDSThuocVT_Ser}) ser on ser.vienphiid=vp.vienphiid
	inner join (select medicinerefid_org,medicinecode,medicinename from medicine_ref where 1=1 {_lstDSThuocVT_Mef}) mef on mef.medicinecode=ser.servicepricecode;
	
	
























