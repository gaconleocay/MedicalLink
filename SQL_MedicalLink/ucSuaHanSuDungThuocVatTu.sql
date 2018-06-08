--Sua han su dung thuoc/vat tu
--ucSuaHanSuDungThuocVatTu


--ngay 5/6/2018

SELECT row_number() OVER (order by me.medicinegroupcode,me.medicinename,me.medicinecode) as stt,  
	me.medicinerefid,
	me.medicinerefid_org, 
	me.medicinegroupcode, 
	me.medicinecode, 
	me.medicinename, 
	me.donvitinh, 
	me.servicepricefeebhyt,
	msref.soluongtonkho, 
	msref.soluongkhadung, 
	me.hansudung,
	me.solo,
	me.hoatchat,
	me.sodangky
FROM 
(select medicinerefid,medicinerefid_org,medicinegroupcode,medicinecode,medicinename,donvitinh,hansudung,solo,servicepricefeebhyt,hoatchat,sodangky from medicine_ref) me 
inner join (select medicinerefid,soluongtonkho,soluongkhadung from medicine_store_ref where medicinestoreid=" + cboKhoThuocVT.EditValue + " and (soluongtonkho>0 or soluongkhadung>0) and medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + ") msref on me.medicinerefid=msref.medicinerefid;



UPDATE medicine_ref SET hansudung='"+ item_dv .hansudung+ "' WHERE medicinecode='"+item_dv.medicinecode+"'; 
UPDATE medicine_store_ref SET hansudung = '"+ item_dv.hansudung + "' WHERE medicinerefid = '"+item_dv.medicinerefid+"'; 



        public long? stt { get; set; }
        public long? medicinerefid { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public string donvitinh { get; set; }
        public decimal? servicepricefeebhyt { get; set; }
        public decimal? soluongtonkho { get; set; }
        public decimal? soluongkhadung { get; set; }
        public string MEDICINEGROUPCODE { get; set; }
        public string hansudung { get; set; }
        public string solo { get; set; }
        public string hoatchat { get; set; }
        public string sodangky { get; set; }
