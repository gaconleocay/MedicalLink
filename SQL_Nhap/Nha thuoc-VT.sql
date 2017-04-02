--Bao cao nha thuoc/
--da xuat
SELECT  ROW_NUMBER () OVER (ORDER BY msb_cd.medicinestorebilldate) as stt,
		msb_cd.medicinestorebillid,
		msb_cd.medicinestorebillcode,		
		mbp.patientid,
		mbp.vienphiid,
		(case when msb_cd.maubenhphamid=0 
					then msb_cd.partnername
			  else hsba.patientname end) as patientname,
		(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam,
		(case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nu,
		hsba.bhytcode,
		kcd.departmentgroupname,
		(case when msb_cd.maubenhphamid=0 
					then mes.medicinestorename
			  else pcd.departmentname end) as departmentname,
		msb_cd.medicinestorebilldate as ngaychidinh,
		msb_cd.bacsi as nguoichidinh,
		msb_cd.medicinestorebilldate as ngayxuat,
		nx.username as nguoixuat,
		mes.medicinestorename,
		(case when msb_cd.maubenhphamid=0 
					then (select sum(me.accept_soluong * me.accept_money) 
						  from medicine me 
						  where me.medicinestorebillid=msb_cd.medicinestorebillid)
			  else (select sum(me.accept_soluong * me.accept_money) 
						  from medicine me 
						  where me.medicinestorebillid=msb_cd.medicinestorebillid_ex) end) as thanhtien,
		msb_cd.medicinestorebilltype,
		msb_cd.medicinestorebillstatus,
		msb_cd.medicinestorebillprocessingdate as ngayhethong,
		(case when msb_cd.maubenhphamid=0 
					then 'Khách lẻ'
			  else '' end) as ghichu
			
FROM medicine_store_bill msb_cd
	LEFT JOIN medicine_store_bill msb on msb.medicinestorebillid=msb_cd.medicinestorebillid_ex
	LEFT JOIN maubenhpham mbp on mbp.maubenhphamid=msb_cd.maubenhphamid and mbp.isloaidonthuoc=1
	LEFT JOIN hosobenhan hsba on hsba.hosobenhanid=mbp.hosobenhanid
	LEFT JOIN departmentgroup kcd on kcd.departmentgroupid=msb_cd.departmentgroupid
	LEFT JOIN department pcd on pcd.departmentid=msb_cd.departmentid and pcd.departmenttype in (2,3,9)
	LEFT JOIN tools_tblnhanvien nx on nx.usercode=COALESCE(msb.medicinestorebillprocessinger,msb_cd.medicinestorebillprocessinger)
	LEFT JOIN medicine_store mes on mes.medicinestoreid=msb_cd.partnerid or mes.medicinestoreid=msb_cd.medicinestoreid
WHERE mes.medicinestoretype=4 and msb_cd.isremove=0 and msb_cd.medicinestorebilltype in (200,6)
	and msb_cd.medicinestorebilldate>='2017-01-01 00:00:00' and msb_cd.medicinestorebilldate<='2017-01-05 00:00:00'
	--and msb_cd.medicinestorebillid_ex=0
	--and msb_cd.medicinestorebillstatus<>11;



---chua xuat
SELECT  ROW_NUMBER () OVER (ORDER BY msb_cd.medicinestorebilldate) as stt,
		msb_cd.medicinestorebillid,
		msb_cd.medicinestorebillcode,		
		mbp.patientid,
		mbp.vienphiid,
		(case when msb_cd.maubenhphamid=0 
					then msb_cd.partnername
			  else hsba.patientname end) as patientname,
		(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam,
		(case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nu,
		hsba.bhytcode,
		kcd.departmentgroupname,
		(case when msb_cd.maubenhphamid=0 
					then mes.medicinestorename
			  else pcd.departmentname end) as departmentname,
		msb_cd.medicinestorebilldate as ngaychidinh,
		msb_cd.bacsi as nguoichidinh,
		'' as ngayxuat,
		'' as nguoixuat,
		mes.medicinestorename,
		(case when msb_cd.maubenhphamid=0 
					then (select sum(me.accept_soluong * me.accept_money) 
						  from medicine me 
						  where me.medicinestorebillid=msb_cd.medicinestorebillid)
			  else (select sum(me.accept_soluong * me.accept_money) 
						  from medicine me 
						  where me.medicinestorebillid=msb_cd.medicinestorebillid) end) as thanhtien,
		msb_cd.medicinestorebilltype,
		msb_cd.medicinestorebillstatus,
		'' as ngayhethong,
		(case when msb_cd.maubenhphamid=0 
					then 'Khách lẻ'
			  else '' end) as ghichu
			
FROM medicine_store_bill msb_cd
	LEFT JOIN maubenhpham mbp on mbp.maubenhphamid=msb_cd.maubenhphamid and mbp.isloaidonthuoc=1
	LEFT JOIN hosobenhan hsba on hsba.hosobenhanid=mbp.hosobenhanid
	LEFT JOIN departmentgroup kcd on kcd.departmentgroupid=msb_cd.departmentgroupid
	LEFT JOIN department pcd on pcd.departmentid=msb_cd.departmentid and pcd.departmenttype in (2,3,9)
	LEFT JOIN tools_tblnhanvien nx on nx.usercode=msb_cd.medicinestorebillprocessinger
	LEFT JOIN medicine_store mes on mes.medicinestoreid=mbp.medicinestoreid or mes.medicinestoreid=msb_cd.medicinestoreid
WHERE mes.medicinestoretype=4 and msb_cd.isremove=0 and msb_cd.medicinestorebilltype in (200,6)
	and msb_cd.medicinestorebilldate>='2017-01-01 00:00:00' and msb_cd.medicinestorebilldate<='2017-01-05 00:00:00'
	and msb_cd.medicinestorebillstatus<>2

	
	
	
	
	
	
	
	
	

	
	
-----------	thuoc cho bac si
SELECT ROW_NUMBER () OVER (ORDER BY msb.bacsi, (case when msb.departmentid=0 
					then mes.medicinestorename
			  else pcd.departmentname end), mef.medicinerefid_org) as stt,
		mef.medicinerefid_org,
		(select medicinecode from medicine_ref where medicinerefid=mef.medicinerefid_org) as medicinecode,
		mef.medicinename,
		sum(me.accept_soluong) as soluong,
		me.accept_money as don gia,
		sum(me.accept_soluong * me.accept_money) as thanhtien,
		kcd.departmentgroupname as khoachidinh,
		(case when msb.departmentid=0 
					then mes.medicinestorename
			  else pcd.departmentname end) as phongchidinh,
		msb.bacsi as nguoichidinh
		
FROM medicine_ref mef inner join medicine me on mef.medicinerefid=me.medicinerefid								
	inner join medicine_store_bill msb on msb.medicinestorebillid=me.medicinestorebillid						
	left join departmentgroup kcd on kcd.departmentgroupid=msb.departmentgroupid
	left join department pcd on pcd.departmentid=msb.departmentid and pcd.departmenttype in (2,3,9)
	left join medicine_store mes on mes.medicinestoreid=msb.partnerid or mes.medicinestoreid=msb.medicinestoreid
WHERE msb.medicinestorebillstatus=2 and  mes.medicinestoretype=4 
	and msb.isremove=0 and msb.medicinestorebilltype in (204,6)
	and msb.medicinestorebilldate>='2017-01-01 00:00:00' and msb.medicinestorebilldate<='2017-01-05 00:00:00'
GROUP BY mef.medicinerefid_org, mef.medicinename, me.accept_money, 
		kcd.departmentgroupname, msb.bacsi, 
		(case when msb.departmentid=0 
					then mes.medicinestorename
			  else pcd.departmentname end);







medicinestorebillcode='BILL4769632'






	
	
	
	







