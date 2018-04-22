----BC Thống kê tình hình BN ra, vào viện (nội trú) TT 4388 BYT 
--ngay 17/4

select row_number () over (order by "+orderby+") as stt,
	vp.patientid,
	vp.vienphiid,
	hsba.patientname,
	hsba.namsinh,
	hsba.gioitinhname,
	bh.bhytcode,
	kvv.departmentgroupname as departmentgroupname_noitru,
	mrd.thoigianvaovien as vienphidate_noitru,
	(case when cast(to_char(mrd.thoigianvaovien, 'HH24MI') as numeric)>0 and cast(to_char(mrd.thoigianvaovien, 'HH24MI') as numeric)<1201 then 'Buổi sáng'
			else 'Buổi chiều' end) as buoi_noitru,
	krv.departmentgroupname as departmentgroupname_ravien,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when cast(to_char(vp.vienphidate_ravien, 'HH24MI') as numeric)>0 and cast(to_char(vp.vienphidate_ravien, 'HH24MI') as numeric)<1201 then 'Buổi sáng'
			else (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then 'Buổi chiều' end) end) as buoi_ravien,
	(case vp.vienphistatus 
		when 2 then 'Đã duyệt VP' 
		when 1 then 
			case vp.vienphistatus_vp 
				when 1 then 'Đã duyệt VP' 
				else 'Đã đóng BA' end 
		else 'Đang điều trị' end) as vienphistatus
from (select patientid,vienphiid,hosobenhanid,vienphidate_ravien,departmentgroupid,vienphistatus,vienphistatus_vp,bhytid from vienphi where loaivienphiid=0 "+tieuchi_ravien+") vp 
	inner join (select hosobenhanid,patientname,to_char(birthday, 'yyyy') as namsinh,gioitinhname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid
	inner join (select hosobenhanid,departmentgroupid,thoigianvaovien from medicalrecord where hinhthucvaovienid=2 "+tieuchi_vaovien+") mrd on mrd.hosobenhanid=vp.hosobenhanid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) kvv on kvv.departmentgroupid=mrd.departmentgroupid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) krv on krv.departmentgroupid=vp.departmentgroupid;






	