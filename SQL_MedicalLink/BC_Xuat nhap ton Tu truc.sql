---BN xuat nhap ton tu truc ngay 4/8

---Xuat nhap ton - Tong hop
SELECT row_number() OVER () as stt, 
me.medicinerefid_org, 
me.medicinegroupcode, 
(select medi.medicinecode from medicine_ref medi where medi.medicinerefid=me.medicinerefid_org) as medicinecode, 
me.medicinename, 
me.donvitinh, 
sum(msref.soluongtonkho) as soluongtonkho, 
sum(msref.soluongkhadung) as soluongkhadung, 
msref.soluongtutruc,
'' as hansudung,
'' as solo
FROM 
(select medicinerefid,medicinerefid_org,medicinegroupcode,medicinename,donvitinh from medicine_ref) me 
inner join (select medicinerefid,soluongtonkho,soluongkhadung,soluongtutruc from medicine_store_ref where medicinestoreid=" + cboTuTruc.EditValue + " and (soluongtutruc>0 or soluongtonkho>0 or soluongkhadung>0) and medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + ") msref on me.medicinerefid=msref.medicinerefid 
GROUP BY me.medicinerefid_org,me.medicinegroupcode,me.medicinename,me.donvitinh,msref.soluongtutruc 
ORDER BY me.medicinegroupcode,me.medicinename; 


---- Xuất nhập tồn chi tiết từng lô -Tổng hợp ngày 7/8
SELECT row_number() OVER (order by me.medicinegroupcode,me.medicinename,me.medicinecode) as stt,  
me.medicinerefid,
me.medicinerefid_org, 
me.medicinegroupcode, 
me.medicinecode, 
me.medicinename, 
me.donvitinh, 
msref.soluongtonkho as soluongtonkho, 
msref.soluongkhadung as soluongkhadung, 
msref.soluongtutruc,
me.hansudung,
me.solo
FROM 
(select medicinerefid,medicinerefid_org,medicinegroupcode,medicinecode,medicinename,donvitinh,hansudung,solo from medicine_ref) me 
inner join (select medicinerefid,soluongtonkho,soluongkhadung,soluongtutruc from medicine_store_ref where medicinestoreid=" + cboTuTruc.EditValue + " and (soluongtutruc>0 or soluongtonkho>0 or soluongkhadung>0) and medicineperiodid=(select max(medicineperiodid) from medicine_period) " + medicinekiemkeid + ") msref on me.medicinerefid=msref.medicinerefid;
--GROUP BY me.medicinecode,me.medicinegroupcode,me.medicinename,me.donvitinh,msref.soluongtutruc,me.medicinerefid_org



medicinerefid_org






----lay danh sach sang ben XNT chi tiet
select medicinestorerefid,medicinerefid
from medicine_store_ref
where medicinestoreid=60

SELECT msf.medicinestorerefid,mef.medicinerefid, mef.medicinerefid_org, mef.medicinecode, mef.medicinename
FROM medicine_ref mef
	inner join medicine_store_ref msf on msf.medicinerefid=mef.medicinerefid
WHERE mef.isremove=0 and mef.medicinecode like 'T33252-1717%' and msf.medicinestoreid=60



---Xuat nhap ton - Chi Tiet
SELECT me.medicinerefid, 
me.medicinedate, 
me.medicinestorebillid, 
me.medicinestorebillcode, 
(case me.medicinestorebilltype when 204 then 'Xuất đơn' when 217 then 'Nhập trả (BS)' when 2 then 'Nhập (kho)' else '' end) as medicinestorebilltype, 
me.medicinestorebillstatus, 
me.medicinestorebillremark, 
me.medicinestoreid, 
me.accept_soluong, 
me.accept_money + ((me.accept_money*me.accept_vat)/100) as accept_money, 
me.solo, 
me.sodangky, 
me.medicinekiemkeid,
kyc.departmentgroupname,
pyc.departmentname
FROM (select medicinerefid,medicinedate,medicinestorebillid,medicinestorebillcode,medicinestorebilltype,medicinestorebillstatus,medicinestorebillremark,medicinestoreid,accept_soluong,accept_money,accept_vat,solo,sodangky,medicinekiemkeid from medicine where isremove=0 and medicinestoreid='" + this.medicinestoreid + "' and medicinerefid in (" + this.lstmedicineref_string + ") and medicinestorebilltype in (2,204,217) and medicinedate between '" + thoiGianTu + "' and '" + thoiGianDen + "') me 
inner join (select medicinestorebillid,departmentgroupid,departmentid from medicine_store_bill where coalesce(isremove,0)=0 and medicinestoreid='" + this.medicinestoreid + "') ser on ser.medicinestorebillid=me.medicinestorebillid 
left join (select departmentgroupid,departmentgroupname from departmentgroup) kyc on kyc.departmentgroupid=ser.departmentgroupid
left join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pyc on pyc.departmentid=ser.departmentid
ORDER BY me.medicinedate DESC;
-----------------

medicinestorerefid
medicinestorerefid 










