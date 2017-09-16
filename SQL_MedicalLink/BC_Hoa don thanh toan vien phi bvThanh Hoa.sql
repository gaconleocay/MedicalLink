----Báo cáo hóa đơn thanh toán viện phí - bv Thanh Hóa
--ngày 13/9

--the
alter table nhompersonnel add usergnhom_name text
update nhompersonnel set usergnhom=99
--update nhompersonnel set usergnhom=99 where usercode like '%-tc' 

SELECT row_number () over (order by b.billgroupcode,cast(b.billcode as numeric)) as stt,
	b.billgroupcode,
	b.billcode,
	'' as billgrouptu_den,
	b.billdate,
	(b.datra-b.miengiam) as sotien,
	b.miengiam as miengiam,
	b.patientid,
	b.vienphiid,
	b.patientname,
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi,
	hsba.bhytcode,
	b.userid,
	ngthu.username as nguoithu

FROM (select hosobenhanid,patientid,vienphiid,patientname,billgroupcode,dahuyphieu,datra,(case when miengiam<>'' then cast(replace(miengiam,',','') as numeric) else 0 end) as miengiam,billcode,billdate,userid from bill where loaiphieuthuid='"+loaiphieuthuid+"' and dahuyphieu=0 and billdate between '" + tungay + "' and '" + denngay + "' and userid in ("+_listuserid+")) b
INNER JOIN (select vienphiid,hosobenhanid from vienphi) vp on vp.vienphiid=b.vienphiid
INNER JOIN (select hosobenhanid,patientcode,patientname,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
LEFT JOIN nhompersonnel ngthu ON ngthu.userhisid=b.userid;


	
--

	
	
	
