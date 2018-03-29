--Báo cáo Tai nạn thương tích
--Ngày 22/3/2018

--1.So nguoi bi TNTT
SELECT
	'Số người bị TNTT' as noi_dung,
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
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c
FROM (select * from tainanthuongtich where 1=1 ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
 
UNION ALL
--2.nghe nghiep
SELECT
	(case when hsba.nghenghiepcode in ('07','08','09','15','19') then '1CBCC'
				when hsba.nghenghiepcode in ('05') then '2ND'
				when hsba.nghenghiepcode in ('06','18') then '3BDCA'
				when hsba.nghenghiepcode in ('02') then '4HSSV'
				when hsba.nghenghiepcode in ('04','13') then '5CNTTC'
				when hsba.nghenghiepcode in ('10') then '6LDTD'
				when hsba.nghenghiepcode in ('99','01','03','12') then '7KHAC'
			end) as noi_dung,
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
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c
FROM (select * from tainanthuongtich where 1=1 ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when hsba.nghenghiepcode in ('07','08','09','15','19') then '1CBCC'
				when hsba.nghenghiepcode in ('05') then '2ND'
				when hsba.nghenghiepcode in ('06','18') then '3BDCA'
				when hsba.nghenghiepcode in ('02') then '4HSSV'
				when hsba.nghenghiepcode in ('04','13') then '5CNTTC'
				when hsba.nghenghiepcode in ('10') then '6LDTD'
				when hsba.nghenghiepcode in ('99','01','03','12') then '7KHAC'
			end)
ORDER BY noi_dung			

UNION ALL
--3.Dia diem xay ra
SELECT
	(case when tntt.tainan_diadiemid=1 then '1TDD'
				when tntt.tainan_diadiemid=2 then '2TN'
				when tntt.tainan_diadiemid=3 then '3TH'
				when tntt.tainan_diadiemid=4 then '4NLV'
				when tntt.tainan_diadiemid=5 then '5NCC'
				when tntt.tainan_diadiemid=6 then '6HA'
				when tntt.tainan_diadiemid in (0,99) then '7KHAC'
			end) as noi_dung,
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
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c
FROM (select * from tainanthuongtich where 1=1 ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_diadiemid=1 then '1TDD'
				when tntt.tainan_diadiemid=2 then '2TN'
				when tntt.tainan_diadiemid=3 then '3TH'
				when tntt.tainan_diadiemid=4 then '4NLV'
				when tntt.tainan_diadiemid=5 then '5NCC'
				when tntt.tainan_diadiemid=6 then '6HA'
				when tntt.tainan_diadiemid in (0,99) then '7KHAC'
			end)
ORDER BY noi_dung	 
 
UNION ALL
--4.Bộ phận bị thương- theo ICD10 
SELECT
	(case when tntt.tainan_bophanbithuongid=1 then '1DMC'
				when tntt.tainan_bophanbithuongid=2 then '2TM'
				when tntt.tainan_bophanbithuongid=3 then '3C'
				when tntt.tainan_bophanbithuongid=4 then '4DCT'
				when tntt.tainan_bophanbithuongid in (0,5,99) then '5KHAC'
			end) as noi_dung,
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
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c
FROM (select * from tainanthuongtich where 1=1 ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_bophanbithuongid=1 then '1DMC'
				when tntt.tainan_bophanbithuongid=2 then '2TM'
				when tntt.tainan_bophanbithuongid=3 then '3C'
				when tntt.tainan_bophanbithuongid=4 then '4DCT'
				when tntt.tainan_bophanbithuongid in (0,5,99) then '5KHAC'
			end)
ORDER BY noi_dung 
 
UNION ALL
--5.Nguyên  nhân TNTT - theo ICD10
SELECT
	(case when tntt.tainan_bophanbithuongid=1 then '1DMC'
				when tntt.tainan_bophanbithuongid=2 then '2TM'
				when tntt.tainan_bophanbithuongid=3 then '3C'
				when tntt.tainan_bophanbithuongid=4 then '4DCT'
				when tntt.tainan_bophanbithuongid in (0,5,99) then '5KHAC'
			end) as noi_dung,
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
	sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c
FROM (select * from tainanthuongtich where 1=1 ) tntt
	inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid
GROUP BY (case when tntt.tainan_nguyennhanid=1 then '1TNGT'
				when tntt.tainan_nguyennhanid= then '2TNLD'
				when tntt.tainan_nguyennhanid= then '3SVDV'
				when tntt.tainan_nguyennhanid=2 then '4N'
				when tntt.tainan_nguyennhanid= then '5DN'
				when tntt.tainan_nguyennhanid= then '6B'
				when tntt.tainan_nguyennhanid= then '7ND'
				when tntt.tainan_nguyennhanid= then '8TT'
				when tntt.tainan_nguyennhanid= then '9BL'
				when tntt.tainan_nguyennhanid in (0,5,99) then '10KHAC'
			end)
ORDER BY noi_dung  
 
 
 
 
 
 
 
 
 
 

CREATE TABLE tainanthuongtich
(
  tainanthuongtichid serial NOT NULL,
  medicalrecordid integer DEFAULT 0,
  patientid integer DEFAULT 0,
  tainanthuongtichdate timestamp without time zone,
  hc_thon text,
  hc_xacode text,
  hc_huyencode text,
  hc_tinhcode text,
  tainan_diadiemid integer DEFAULT 0,
  tainan_nguyennhanid integer DEFAULT 0,
  tainan_dienbienid=1: tu vong
  tainan_bophanbithuongid integer DEFAULT 0,
  tainan_ngodocid integer DEFAULT 0,
  tainan_giaothongid integer DEFAULT 0,
  tainan_xutriid integer DEFAULT 0,
  version timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  tainan_ngodoc text,
  tainan_giaothongmuid integer DEFAULT 0,
  hosobenhanid integer DEFAULT 0,
  userid integer DEFAULT 0,
  tainan_doconid integer DEFAULT 0,
  CONSTRAINT tainanthuongtich_pkey PRIMARY KEY (tainanthuongtichid)
)