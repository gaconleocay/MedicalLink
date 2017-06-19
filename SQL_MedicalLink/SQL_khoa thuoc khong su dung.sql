--Khoa danh muc thuoc/vat tu khong su dung - ngay 19/6/2017
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
-------v2
UPDATE medicine_ref SET islock_sql=1, servicelock=1, isremove=1
WHERE medicinerefid IN (
						select msr.medicinerefid
						from medicine_store_ref msr 
							left join medicinekiemke kk on kk.medicinekiemkeid=msr.medicinekiemkeid and kk.medicinekiemkestatus=0
							inner join medicine_ref mf on mf.medicinerefid=msr.medicinerefid
						where msr.soluongtonkho = 0 and msr.soluongkhadung=0 
							and msr.version<'2017-01-01 00:00:00'
							and msr.islock=0
							and mf.medicinecode like '%.%'
						)		
and coalesce(servicelock,0)=0
and coalesce(isremove,0)=0	
	
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


--------
-------v3
UPDATE medicine_ref SET islock_sql=1, servicelock=1, isremove=1
WHERE medicinerefid IN (
						select msr.*
						from medicine_store_ref msr 
							--left join medicinekiemke kk on kk.medicinekiemkeid=msr.medicinekiemkeid and kk.medicinekiemkestatus=0
							inner join medicine_ref mf on mf.medicinerefid=msr.medicinerefid
						where msr.soluongtonkho = 0 and msr.soluongkhadung=0 
							and msr.version<'2017-01-01 00:00:00'
							and msr.islock=0
							and mf.medicinecode like '%.%'
						)		
and coalesce(servicelock,0)=0
and coalesce(isremove,0)=0	

select * from medicine_store_ref where islock=1 and soluongtonkho<>0







