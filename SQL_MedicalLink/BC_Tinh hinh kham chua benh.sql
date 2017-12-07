--bao cao tinh hình khám chữa bệnh tổng hợp.
--ngay 3/12/2017
-- chi tinh BN da nhấn nút bắt đầu
CREATE extension tablefunc;


SELECT
	vp.thang,
	sum(case when vp.loaivienphiid<>0 then 1 else 0 end) as kb_tong,
	sum(case when vp.loaivienphiid<>0 and hsba.gioitinhcode='02' then 1 else 0 end) as kb_nu,
	sum(case when vp.loaivienphiid<>0 and vp.doituongbenhnhanid=1 then 1 else 0 end) as kb_bhyt,
	sum(case when vp.loaivienphiid<>0 and vp.doituongbenhnhanid<>1 then 1 else 0 end) as kb_vp,
	sum(case when vp.loaivienphiid<>0 and hsba.tuoi>=60 then 1 else 0 end) as kb_60,
	sum(case when vp.loaivienphiid<>0 and hsba.tuoi<15 then 1 else 0 end) as kb_15,
	sum(case when vp.loaivienphiid<>0 and hsba.tuoi<5 then 1 else 0 end) as kb_5,
	sum(case when vp.loaivienphiid<>0 and hsba.hc_tinhcode<>'31' then 1 else 0 end) as kb_ngtinh,
	sum(case when vp.loaivienphiid=0 then 1 else 0 end) as nt_tong,
	sum(case when vp.loaivienphiid=0 and hsba.gioitinhcode='02' then 1 else 0 end) as nt_nu,
	sum(case when vp.loaivienphiid=0 and vp.doituongbenhnhanid=1 then 1 else 0 end) as nt_bhyt,
	sum(case when vp.loaivienphiid=0 and vp.doituongbenhnhanid<>1 then 1 else 0 end) as nt_vp,
	sum(case when vp.loaivienphiid=0 and hsba.tuoi>=60 then 1 else 0 end) as nt_60,
	sum(case when vp.loaivienphiid=0 and hsba.tuoi<15 then 1 else 0 end) as nt_15,
	sum(case when vp.loaivienphiid=0 and hsba.tuoi<5 then 1 else 0 end) as nt_5,
	sum(case when vp.loaivienphiid=0 and hsba.hc_tinhcode<>'31' then 1 else 0 end) as nt_ngtinh
FROM (select loaivienphiid,doituongbenhnhanid,hosobenhanid, to_char(vienphidate,'MM') as thang from vienphi where vienphidate between '2016-01-01 00:00:00' and '2016-05-11 00:00:00') vp
	inner join (select hosobenhanid,hc_tinhcode,gioitinhcode,(cast(to_char(now(),'yyyy') as numeric)-cast(to_char(birthday,'yyyy') as numeric)) as tuoi from hosobenhan where hosobenhandate between '2016-01-01 00:00:00' and '2016-05-11 00:00:00') hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select hosobenhanid from medicalrecord where hinhthucvaovienid=0 and medicalrecordstatus<>0 and thoigianvaovien between '2016-01-01 00:00:00' and '2016-05-11 00:00:00') mrd on mrd.hosobenhanid=vp.hosobenhanid 

GROUP BY vp.thang;
	

----

SELECT *
FROM 
	crosstab('SELECT
	vp.thang,
	sum(case when vp.loaivienphiid<>0 then 1 else 0 end) as kb_tong,
	sum(case when vp.loaivienphiid<>0 and hsba.gioitinhcode=''02'' then 1 else 0 end) as kb_nu,
	sum(case when vp.loaivienphiid<>0 and vp.doituongbenhnhanid=1 then 1 else 0 end) as kb_bhyt,
	sum(case when vp.loaivienphiid<>0 and vp.doituongbenhnhanid<>1 then 1 else 0 end) as kb_vp,
	sum(case when vp.loaivienphiid<>0 and hsba.tuoi>=60 then 1 else 0 end) as kb_60,
	sum(case when vp.loaivienphiid<>0 and hsba.tuoi<15 then 1 else 0 end) as kb_15,
	sum(case when vp.loaivienphiid<>0 and hsba.tuoi<5 then 1 else 0 end) as kb_5,
	sum(case when vp.loaivienphiid<>0 and hsba.hc_tinhcode<>''31'' then 1 else 0 end) as kb_ngtinh,
	sum(case when vp.loaivienphiid=0 then 1 else 0 end) as nt_tong,
	sum(case when vp.loaivienphiid=0 and hsba.gioitinhcode=''02'' then 1 else 0 end) as nt_nu,
	sum(case when vp.loaivienphiid=0 and vp.doituongbenhnhanid=1 then 1 else 0 end) as nt_bhyt,
	sum(case when vp.loaivienphiid=0 and vp.doituongbenhnhanid<>1 then 1 else 0 end) as nt_vp,
	sum(case when vp.loaivienphiid=0 and hsba.tuoi>=60 then 1 else 0 end) as nt_60,
	sum(case when vp.loaivienphiid=0 and hsba.tuoi<15 then 1 else 0 end) as nt_15,
	sum(case when vp.loaivienphiid=0 and hsba.tuoi<5 then 1 else 0 end) as nt_5,
	sum(case when vp.loaivienphiid=0 and hsba.hc_tinhcode<>''31'' then 1 else 0 end) as nt_ngtinh
FROM (select loaivienphiid,doituongbenhnhanid,hosobenhanid, to_char(vienphidate,''MM'') as thang 
	from vienphi where vienphidate between ''2016-01-01 00:00:00'' and ''2016-05-11 00:00:00'') vp
	inner join (select hosobenhanid,hc_tinhcode,gioitinhcode,(cast(to_char(now(),''yyyy'') as numeric)-cast(to_char(birthday,''yyyy'') as numeric)) as tuoi 
		from hosobenhan where hosobenhandate between ''2016-01-01 00:00:00'' and ''2016-05-11 00:00:00'') hsba on hsba.hosobenhanid=vp.hosobenhanid
GROUP BY vp.thang
ORDER BY 1'
	'SELECT * FROM (select ''01'',''02'',''03'',''04'',''05'',''06'',''07'',''08'',''09'',''10'',''11'',''12'') as AA'
)
	AS ct(noidung text, thang_1 integer, thang_2 integer, thang_3 integer, thang_4 integer, thang_5 integer, thang_6 integer, thang_7 integer, thang_8 integer, thang_9 integer, thang_10 integer, thang_11 integer, thang_12 integer)
	
	
	






