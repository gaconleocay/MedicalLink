--Mo benh an ngay 27/11


SELECT 
DISTINCT vp.vienphiid as mavienphi, 
vp.patientid as mabenhnhan,
hsba.patientname as tenbenhnhan, 
'' as tenbenhnhan_khongdau,
hsba.gioitinhname,
(case when hsba.birthday_year<>0 then cast(hsba.birthday_year as text) else to_char(hsba.birthday,'dd/MM/yyyy') end) as namsinh,
((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
to_char(vp.vienphidate, 'yyyy-MM-dd HH24:MI:ss') as ngayvaovien, 
(case when vp.vienphistatus<>0 then to_char(vp.vienphidate_ravien, 'yyyy-MM-dd HH24:MI:ss') end) as ngayravien,
(case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai, 
degp.departmentgroupname as khoa, 
(CASE vp.departmentid WHEN '0' THEN 'Hành chính' ELSE de.departmentname END) as phong, 
bh.bhytcode,
(vp.chandoanravien_code || ' - ' || vp.chandoanravien) as tenbenh
 
FROM vienphi vp
	inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join bhyt bh on bh.bhytid=vp.bhytid
	inner join departmentgroup degp on degp.departmentgroupid=vp.departmentgroupid
	left join department de on de.departmentid=vp.departmentid
WHERE " + _tieuchi + _tukhoatimkiem +" 
ORDER BY mavienphi desc;







