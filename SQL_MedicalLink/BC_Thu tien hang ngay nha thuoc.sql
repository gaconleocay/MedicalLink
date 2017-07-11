---Bao cao thu tien hang ngay nha thuoc ngay 7/7
--Da xuat
SELECT ROW_NUMBER () OVER (ORDER BY msb_cd.medicinestorebilldate) as stt, 
msb_cd.medicinestorebillid, 
msb_cd.medicinestorebillcode, 
mbp.patientid, 
mbp.vienphiid, 
(case when msb_cd.maubenhphamid=0 then msb_cd.partnername else hsba.patientname end) as patientname, 
(case when hsba.gioitinhcode='01' then to_char(hsba.birthday,'yyyy') else '' end) as year_nam, 
(case hsba.gioitinhcode when '02' then to_char(hsba.birthday,'yyyy') else '' end) as year_nu, 
hsba.bhytcode, 
kcd.departmentgroupname, 
(case when msb_cd.maubenhphamid=0 then mes.medicinestorename else pcd.departmentname end) as departmentname, 
msb_cd.medicinestorebilldate as ngaychidinh, 
msb_cd.bacsi as nguoichidinh, 
msb_cd.medicinestorebilldate as ngayxuat, 
nx.username as nguoixuat, 
mes.medicinestorename, 
(case when msb_cd.medicinestorebilltype=8
		then (case when msb_cd.maubenhphamid=0 
					then (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) 
				else (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) 
			end)
		else (case when msb_cd.maubenhphamid=0 
					then (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) 
				else (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) 
			end)	
		end) as thanhtien, 
(case when msb_cd.medicinestorebilltype=8 then 'Nhập trả' else '' end) as medicinestorebilltypename, 
msb_cd.medicinestorebillstatus, 
msb_cd.medicinestorebillprocessingdate as ngayhethong, 
(case when msb_cd.maubenhphamid=0 then 'Khách lẻ' else '' end) as ghichu 
FROM medicine_store_bill msb_cd 
LEFT JOIN (select medicinestorebillid,medicinestorebillprocessinger from medicine_store_bill) msb on msb.medicinestorebillid=msb_cd.medicinestorebillid_ex 
LEFT JOIN (select maubenhphamid,hosobenhanid,patientid,vienphiid from maubenhpham where isloaidonthuoc=1) mbp on mbp.maubenhphamid=msb_cd.maubenhphamid
LEFT JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=(case when msb_cd.departmentgroupid=0 then msb_cd.khoa_id else msb_cd.departmentgroupid end)
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd on pcd.departmentid=msb_cd.departmentid
LEFT JOIN tools_tblnhanvien nx on nx.usercode=COALESCE(msb.medicinestorebillprocessinger,msb_cd.medicinestorebillprocessinger) 
LEFT JOIN medicine_store mes on mes.medicinestoreid=msb_cd.partnerid or mes.medicinestoreid=msb_cd.medicinestoreid and mes.medicinestoretype=4
WHERE msb_cd.isremove=0 and msb_cd.medicinestorebilltype in (200,6,8) and msb_cd.medicinestorebilldate>='" + datetungay + "' and msb_cd.medicinestorebilldate<='" + datedenngay + "' and mes.medicinestoreid in (" + lstKhoThuocChonLayBC + ") and msb_cd.medicinestorebillstatus<>9;


----chua xuat
SELECT ROW_NUMBER () OVER (ORDER BY msb_cd.medicinestorebilldate) as stt, 
msb_cd.medicinestorebillid, 
msb_cd.medicinestorebillcode, 
mbp.patientid, 
mbp.vienphiid, 
(case when msb_cd.maubenhphamid=0 then msb_cd.partnername else hsba.patientname end) as patientname, 
(case when hsba.gioitinhcode='01' then to_char(hsba.birthday,'yyyy') else '' end) as year_nam, 
(case hsba.gioitinhcode when '02' then to_char(hsba.birthday,'yyyy') else '' end) as year_nu, 
hsba.bhytcode, 
kcd.departmentgroupname, 
(case when msb_cd.maubenhphamid=0 then mes.medicinestorename else pcd.departmentname end) as departmentname, 
msb_cd.medicinestorebilldate as ngaychidinh, 
msb_cd.bacsi as nguoichidinh, 
'' as ngayxuat, 
'' as nguoixuat, 
mes.medicinestorename, 
(case when msb_cd.medicinestorebilltype=8
		then (case when msb_cd.maubenhphamid=0 
					then (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) 
				else (select 0-sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) 
			end)
		else (case when msb_cd.maubenhphamid=0 
					then (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid) 
				else (select sum(me.accept_soluong * me.accept_money) from medicine me where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) 
			end)	
		end) as thanhtien,
(case when msb_cd.medicinestorebilltype=8 then 'Nhập trả' else '' end) as medicinestorebilltypename, 
msb_cd.medicinestorebillstatus, 
'' as ngayhethong, 
(case when msb_cd.maubenhphamid=0 then 'Khách lẻ' else '' end) as ghichu 
FROM medicine_store_bill msb_cd 
LEFT JOIN (select maubenhphamid,hosobenhanid,patientid,vienphiid,medicinestoreid from maubenhpham where isloaidonthuoc=1) mbp on mbp.maubenhphamid=msb_cd.maubenhphamid
LEFT JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=(case when msb_cd.departmentgroupid=0 then msb_cd.khoa_id else msb_cd.departmentgroupid end) 
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd on pcd.departmentid=msb_cd.departmentid 
LEFT JOIN tools_tblnhanvien nx on nx.usercode=msb_cd.medicinestorebillprocessinger 
LEFT JOIN medicine_store mes on mes.medicinestoreid=mbp.medicinestoreid or mes.medicinestoreid=msb_cd.medicinestoreid and mes.medicinestoretype=4
WHERE msb_cd.isremove=0 and msb_cd.medicinestorebilltype in (200,6,8) and msb_cd.medicinestorebilldate>='" + datetungay + "' and msb_cd.medicinestorebilldate<='" + datedenngay + "' and mes.medicinestoreid in (" + lstKhoThuocChonLayBC + ") and msb_cd.medicinestorebillstatus not in (2,11);











