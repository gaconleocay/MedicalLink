--Báo cáo Tai nạn thương tích
--Ngày 22/3/2018

--1.So nguoi bi TNTT
SELECT '1' as stt, 
	'1' as nhom,
	'1' as noi_dung_code,
	'' as noi_dung_name,
	count(*) as tong_m,
	sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c,
	sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m,
	sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c,
	sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m,
	sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c,	
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c,
	sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m,
	sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c,
	'1' as isgroup
FROM (select * from tainanthuongtich where 1=1 "+_tieuchi+" ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
 
UNION ALL
--2.nghe nghiep
SELECT '2' as stt, 
	'2' as nhom,
	(case when hsba.nghenghiepcode in ('07','08','09','15','19') then '3'
				when hsba.nghenghiepcode in ('05') then '4'
				when hsba.nghenghiepcode in ('06','18') then '5'
				when hsba.nghenghiepcode in ('02') then '6'
				when hsba.nghenghiepcode in ('04','13') then '7'
				when hsba.nghenghiepcode in ('10') then '8'
				when hsba.nghenghiepcode in ('99','01','03','12') then '9'
			end) as noi_dung_code,
	'' as noi_dung_name,
	count(*) as tong_m,
	sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c,
	sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m,
	sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c,
	sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m,
	sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c,	
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c,
	sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m,
	sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c,
	'1' as isgroup
FROM (select * from tainanthuongtich where 1=1 "+_tieuchi+" ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when hsba.nghenghiepcode in ('07','08','09','15','19') then '3'
				when hsba.nghenghiepcode in ('05') then '4'
				when hsba.nghenghiepcode in ('06','18') then '5'
				when hsba.nghenghiepcode in ('02') then '6'
				when hsba.nghenghiepcode in ('04','13') then '7'
				when hsba.nghenghiepcode in ('10') then '8'
				when hsba.nghenghiepcode in ('99','01','03','12') then '9'
			end)		

UNION ALL
--3.Dia diem xay ra
SELECT '3' as stt, 
	'3' as nhom,
	(case when tntt.tainan_diadiemid=1 then '11'
				when tntt.tainan_diadiemid=2 then '12'
				when tntt.tainan_diadiemid=3 then '13'
				when tntt.tainan_diadiemid=4 then '14'
				when tntt.tainan_diadiemid=5 then '15'
				when tntt.tainan_diadiemid=6 then '16'
				when tntt.tainan_diadiemid in (0,99) then '161'
			end) as noi_dung_code,
	'' as noi_dung_name,
	count(*) as tong_m,
	sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c,
	sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m,
	sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c,
	sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m,
	sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c,	
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c,
	sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m,
	sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c,
	'1' as isgroup
FROM (select * from tainanthuongtich where 1=1 "+_tieuchi+" ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_diadiemid=1 then '11'
				when tntt.tainan_diadiemid=2 then '12'
				when tntt.tainan_diadiemid=3 then '13'
				when tntt.tainan_diadiemid=4 then '14'
				when tntt.tainan_diadiemid=5 then '15'
				when tntt.tainan_diadiemid=6 then '16'
				when tntt.tainan_diadiemid in (0,99) then '161'
			end)
 
UNION ALL
--4.Bộ phận bị thương- theo ICD10 
SELECT '4' as stt, 
	'4' as nhom,
	(case when tntt.tainan_bophanbithuongid=1 then '18'
				when tntt.tainan_bophanbithuongid=2 then '19'
				when tntt.tainan_bophanbithuongid=3 then '20'
				when tntt.tainan_bophanbithuongid=4 then '21'
				when tntt.tainan_bophanbithuongid in (0,5,99) then '22'
			end) as noi_dung_code,
	'' as noi_dung_name,
	count(*) as tong_m,
	sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c,
	sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m,
	sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c,
	sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m,
	sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c,	
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c,
	sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m,
	sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c,
	'1' as isgroup
FROM (select * from tainanthuongtich where 1=1 "+_tieuchi+" ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_bophanbithuongid=1 then '18'
				when tntt.tainan_bophanbithuongid=2 then '19'
				when tntt.tainan_bophanbithuongid=3 then '20'
				when tntt.tainan_bophanbithuongid=4 then '21'
				when tntt.tainan_bophanbithuongid in (0,5,99) then '22'
			end)
 
UNION ALL
--5.Nguyên  nhân TNTT - theo ICD10
SELECT '5' as stt, 
	'5' as nhom,
	(case when tntt.tainan_nguyennhanid=1 then '24'
				when tntt.tainan_nguyennhanid=3 then '25'
				when tntt.tainan_nguyennhanid=4 then '26'
				when tntt.tainan_nguyennhanid=2 then '27'
				when tntt.tainan_nguyennhanid=5 then '28'
				when tntt.tainan_nguyennhanid=6 then '29'
				when tntt.tainan_nguyennhanid=7 then '30'
				when tntt.tainan_nguyennhanid=8 then '31'
				when tntt.tainan_nguyennhanid=9 then '32'
				when tntt.tainan_nguyennhanid in (10,11,99) then '33'
			end) as noi_dung_code,
	'' as noi_dung_name,
	count(*) as tong_m,
	sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c,
	sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m,
	sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c,
	sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m,
	sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c,	
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c,
	sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m,
	sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c,
	'1' as isgroup
FROM (select * from tainanthuongtich where 1=1 "+_tieuchi+" ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_nguyennhanid=1 then '24'
				when tntt.tainan_nguyennhanid=3 then '25'
				when tntt.tainan_nguyennhanid=4 then '26'
				when tntt.tainan_nguyennhanid=2 then '27'
				when tntt.tainan_nguyennhanid=5 then '28'
				when tntt.tainan_nguyennhanid=6 then '29'
				when tntt.tainan_nguyennhanid=7 then '30'
				when tntt.tainan_nguyennhanid=8 then '31'
				when tntt.tainan_nguyennhanid=9 then '32'
				when tntt.tainan_nguyennhanid in (10,11,99) then '33'
			end)
 
UNION ALL
--6. Điều trị ban đầu sau TNTT
SELECT '6' as stt, 
	'6' as nhom,
	(case when tntt.tainan_xutriid=1 then '35'
				when tntt.tainan_xutriid=2 then '36'
				when tntt.tainan_xutriid=3 then '37'
				when tntt.tainan_xutriid=4 then '38'
				when tntt.tainan_xutriid=5 then '39'
				when tntt.tainan_xutriid=6 then '40'
				when tntt.tainan_xutriid=7 then '41'
				when tntt.tainan_xutriid=99 then '42'
			end) as noi_dung_code,
	'' as noi_dung_name,
	count(*) as tong_m,
	sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c,
	sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m,
	sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c,
	sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m,
	sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m,
	sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m,
	sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c,	
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m,
	sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m,
	sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c,
	sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m,
	sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m,
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c,
	'1' as isgroup
FROM (select * from tainanthuongtich where 1=1 "+_tieuchi+") tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_xutriid=1 then '35'
				when tntt.tainan_xutriid=2 then '36'
				when tntt.tainan_xutriid=3 then '37'
				when tntt.tainan_xutriid=4 then '38'
				when tntt.tainan_xutriid=5 then '39'
				when tntt.tainan_xutriid=6 then '40'
				when tntt.tainan_xutriid=7 then '41'
				when tntt.tainan_xutriid=99 then '42'
			end);
 
 
 
 
 
 
 
 