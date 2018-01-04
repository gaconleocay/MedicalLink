select distinct  
 hsba.hosobenhanid,
 mrd.medicalrecordid as madieutri, 
 mrd.patientid as mabenhnhan, 
 mrd.vienphiid as mavienphi, 
 hsba.patientname as tenbenhnhan, 
 (case when vp.vienphistatus_vp=1 then 'Đã duyệt VP' 
		else (case when mrd.medicalrecordstatus=99 then 'Đã đóng BA' else 'Đang điều trị' end) end) as trangthai,
 mrd.thoigianvaovien as thoigianvaovien, 
 (case when mrd.thoigianravien<>'0001-01-01 00:00:00' then mrd.thoigianravien end) as thoigianravien, 
 degp.departmentgroupname as tenkhoa, 
 de.departmentname as tenphong, 
 case mrd.nextdepartmentid when 0 then '1' else '0' end as lakhoacuoi, 
 case mrd.hinhthucvaovienid when 0 then '1' else '0' end as lakhoadau 
FROM medicalrecord mrd 
 inner join hosobenhan hsba on hsba.hosobenhanid=mrd.hosobenhanid 
 inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid 
 left join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid 
 left join department de on de.departmentid=mrd.departmentid 
WHERE " + _timkiemtheo + "  
ORDER BY madieutri;
------

UPDATE medicalrecord SET thoigianvaovien='" + dateThoiGianVao.Text + "', thoigianravien='" + dateThoiGianRa.Text + "' WHERE medicalrecordid= '" + _medirecordId + "'; 
UPDATE vienphi SET vienphidate='" + dateThoiGianVao.Text + "', vienphidate_ravien='" + dateThoiGianRa.Text + "' WHERE vienphiid=" + _mavienphi + "; 
UPDATE hosobenhan SET hosobenhandate='" + dateThoiGianVao.Text + "', hosobenhandate_ravien='" + dateThoiGianRa.Text + "' WHERE hosobenhanid=" + _hosobenhanid + ";












