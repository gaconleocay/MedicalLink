--Khoa danh muc thuoc/vat tu khong su dung 
--------------------------------ngay 20/6/2017
------------------------medicine_store_ref

UPDATE medicine_store_ref SET islock_sql=1, islock=1
--SELECT count(*) 
	--sum(soluongtonkho), sum (soluongkhadung)
--FROM medicine_store_ref 
WHERE medicinestorerefid IN (
	SELECT ME.medicinestorerefid /*,
		ME.count_m as count_me,
		mf.* */
	FROM 
		(
		select 
			msr.medicinestorerefid,
			msr.medicinerefid,
			count(m.*) as count_m
		from (select msf.medicinestorerefid, msf.medicinerefid, msf.soluongtonkho, msf.soluongkhadung
			  from (select medicinestorerefid, medicinerefid, soluongtonkho, soluongkhadung, version, islock  	  from medicine_store_ref) msf 
			   inner join (select medicinerefid, medicinecode from medicine_ref) mf on mf.medicinerefid=msf.medicinerefid
			  where mf.medicinecode like '%.%'
					and msf.soluongtonkho=0 
					and msf.soluongkhadung=0
					and msf.version<'2017-01-01 00:00:00'
					and msf.islock=0			
			 ) msr		
			left join (select medicinestorerefid, medicinedate from medicine) m on msr.medicinestorerefid=m.medicinestorerefid and m.medicinedate>='2017-01-01 00:00:00'
		group by msr.medicinestorerefid, medicinerefid
		) ME
	--INNER JOIN medicine_ref mf ON mf.medicinerefid=ME.medicinerefid
	WHERE ME.count_m=0 	
	)
and coalesce(islock,0)=0





------===================

-- ngay 19/6/2017
ALTER TABLE medicine_store_ref
ADD islock_sql integer;
ALTER TABLE medicine_ref
ADD islock_sql integer;
------------------ - - - -medicine_ref

UPDATE medicine_ref SET islock_sql=1, servicelock=1, isremove=1
WHERE medicinerefid IN (
		SELECT mf.medicinerefid
		FROM medicine_ref mf
			 INNER JOIN 
				(SELECT M.medicinerefid
				FROM 
					(select msr.medicinerefid,
							sum (msr.soluongtonkho) as soluongtonkho,
							sum (msr.soluongkhadung) as soluongkhadung
					from medicine_store_ref msr 
						left join medicinekiemke kk on kk.medicinekiemkeid=msr.medicinekiemkeid and kk.medicinekiemkestatus=0 and kk.medicinestoreid=msr.medicinestoreid	
					where msr.soluongtonkho = 0 and msr.soluongkhadung=0 
						and msr.version<'2017-01-01 00:00:00'
						and msr.islock=0
					group by msr.medicinerefid) M
				WHERE M.soluongtonkho=0 and M.soluongkhadung=0) MI ON MI.medicinerefid=MF.medicinerefid
		WHERE mf.medicinecode like '%.%'	
			and mf.lasttimeupdated <'2017-01-01 00:00:00'	
				)		
and coalesce(servicelock,0)=0
and coalesce(isremove,0)=0	
-- Undo
update medicine_ref set servicelock=0, isremove=0 where islock_sql=1		

	
------------------------medicine_store_ref
UPDATE medicine_store_ref SET islock_sql=1, islock=1
WHERE medicinestorerefid IN (select msr.medicinestorerefid
						from medicine_store_ref msr 
							left join medicinekiemke kk on kk.medicinekiemkeid=msr.medicinekiemkeid and kk.medicinekiemkestatus=0
							inner join medicine_ref mf on mf.medicinerefid=msr.medicinerefid
						where msr.soluongtonkho = 0 and msr.soluongkhadung=0 
							and msr.version<'2017-01-01 00:00:00'
							and msr.islock=0
							and mf.medicinecode like '%.%'
							)
and coalesce(islock,0)=0	

-- Undo
update medicine_store_ref set islock=0 where islock_sql=1


---- kiem tra lai
select * from medicine_ref 
where medicinerefid in (select medicinerefid from medicine_store_ref msr where islock_sql=1 and (soluongtonkho<>0 or soluongkhadung<>0))
	and islock_sql=1
select * from medicine_store_ref where islock=1 and soluongtonkho<>0







311421





	
	
	
	
	
	
	



