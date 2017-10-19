--BC hoa don, chung tu thuoc/VTYT su dung - bv Thanh Hoa
--ngay 16/10

--loai kho=kho tong =1; kho VTHC=7
SELECT medicinestoreid, medicinestorename
FROM medicine_store
WHERE medicinestoretype in (1,3,7)
ORDER BY medicinestorename

-----

SELECT --row_number () over (order by msb.medicinestorebilldate) as stt,
	(row_number() over (partition by msb.medicinestoreid order by msb.medicinestorebilldate)) as stt,	
	msb.medicinestorebillcode,
	msb.medicinestorebilldate,
	msb.sochungtu,
	msb.medicinestorebilltotalmoney as tongtien,
	(select count(*) from medicine where medicinestorebillid=msb.medicinestorebillid) as soluong,
	ncc.nhacungcapname as nhacungcap,
	msb.customername as nguoigiaohang,
	(case when msb.ngaychungtu<>'0001-01-01 00:00:00' then msb.ngaychungtu end) as ngaynhanduhang,
	'' as nguoinhanhang,
	msb.medicinestoreid,
	kho.medicinestorename as khonhanhang,
	msb.medicinestorebillremark as ghichu,
	'0' as isgroup
FROM (select medicinestorebillid,medicinestorebillcode,medicinestorebilldate,sochungtu,medicinestorebilltotalmoney,partnerid,customername,ngaychungtu,medicinestoreid,medicinestorebillremark from medicine_store_bill 
		where medicinestorebilltype=1 
			and medicineperiodid=3
			and medicinestorebilldate between '"+_tungay+"' and '"+_denngay+"'
			"+_listKhoThuoc+"
	  ) msb 
	LEFT JOIN (select nhacungcapid,nhacungcapname from nhacungcap_medicine) ncc on ncc.nhacungcapid=msb.partnerid
	INNER JOIN (select medicinestoreid,medicinestorename from medicine_store) kho on kho.medicinestoreid=msb.medicinestoreid;
 



INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (15, 'REPORT_35_KHO', 'REPORT_35_Danh sách kho thuốc', 0, 'ID kho thuốc cách nhau bởi dấu phẩy (,)');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (15, 'REPORT35_ID_KHO', 'Danh sách Kho', '1,2,5', '0', '')
