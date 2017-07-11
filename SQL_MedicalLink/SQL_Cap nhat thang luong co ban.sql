----Cap nhat thang luong co ban 

select ROW_NUMBER () OVER (ORDER BY (case when vp.vienphistatus=0 then '1' else (case when COALESCE(vp.vienphistatus_vp,0)=0 then '2' else '3' end) end)) as stt,
	vp.vienphiid,
	vp.patientid, 
	hsba.patientname,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	vp.bhyt_thangluongtoithieu,		
	(case when vp.vienphistatus=0 then 'Đang điều trị'
			else (case when COALESCE(vp.vienphistatus_vp,0)=0 then 'Ra viện chưa thanh toán'
						else 'Đã thanh toán' end)
			end) as trangthaivienphi,
	degp.departmentgroupname,
	de.departmentname			
from (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_thangluongtoithieu,departmentgroupid,departmentid from vienphi where bhyt_thangluongtoithieu='1210000' and vienphidate between '2017-01-01 00:00:00' and '2017-01-02 00:00:00') vp
	inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid
	left join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=vp.departmentid;

--------backup
insert into tools_vienphi_tltt(vienphiid,thangluong_old,dateupdate) SELECT VP.vienphiid, vp.bhyt_thangluongtoithieu as thangluong_old, '" + dateupdate + "' as dateupdate FROM dblink('myconn','select vp.vienphiid,vp.bhyt_thangluongtoithieu from (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_thangluongtoithieu,departmentgroupid,departmentid from vienphi where bhyt_thangluongtoithieu=''" + txtLuongCoBan.Text.Trim() + "'' " + tieuchi_dlink + ") vp inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid') AS VP(vienphiid integer, bhyt_thangluongtoithieu double precision);;	
	
-------	update
Update vienphi set bhyt_thangluongtoithieu='"++"', bhyt_gioihanbhyttrahoantoan='"+_15phantram_tlcb+"' where vienphiid in (select vp.vienphiid from (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_thangluongtoithieu,departmentgroupid,departmentid from vienphi where bhyt_thangluongtoithieu='" + txtLuongCoBan.Text.Trim() + "' " + tieuchi + ") vp inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid )
	
	
	
	
	
	
	
	
	