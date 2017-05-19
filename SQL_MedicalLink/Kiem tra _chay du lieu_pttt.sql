--Kiem tra chay du lieu tools_serviceprice_pttt

select spt.vienphiid
from vienphi spt 
where 
	spt.duyet_ngayduyet_vp>='2017-01-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and spt.vienphistatus_vp=1
	and spt.vienphiid not in (select vp.vienphiid from tools_serviceprice_pttt vp where vp.duyet_ngayduyet_vp>='2017-01-01 00:00:00' 
	and vp.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and vp.vienphistatus_vp=1)
	
--------------	
select spt.vienphiid
from tools_serviceprice_pttt spt 
where 
	spt.duyet_ngayduyet_vp>='2017-01-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and spt.vienphistatus_vp=1
	and spt.vienphiid not in (select vp.vienphiid from vienphi vp where vp.duyet_ngayduyet_vp>='2017-01-01 00:00:00' 
	and vp.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and vp.vienphistatus_vp=1)	
	
---------
delete from tools_serviceprice_pttt spt where spt.duyet_ngayduyet_vp>='2017-03-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-03-31 23:59:59' and spt.vienphistatus_vp=1

-------
select * 
from serviceprice ser
where ser.vienphiid in (select spt.vienphiid
from vienphi spt 
where 
	spt.duyet_ngayduyet_vp>='2017-01-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and spt.vienphistatus_vp=1
	and spt.vienphiid not in (select vp.vienphiid from tools_serviceprice_pttt vp where vp.duyet_ngayduyet_vp>='2017-01-01 00:00:00' 
	and vp.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and vp.vienphistatus_vp=1))

---------
---da ra vien nhung chua duyet vien phi

select --vienphiid
	count(*)
from vienphi vp 
where COALESCE(vp.vienphistatus_vp,0)=0
	and vp.vienphistatus<>0
	and vp.vienphidate_ravien>='2016-01-01 00:00:00'
	and vp.vienphiid not in (select vienphiid from tools_serviceprice_pttt )

-----------
select spt.vienphiid
from vienphi spt 
where 
	spt.vienphidate_ravien>='2016-01-01 00:00:00' 
	and spt.vienphistatus<>0 and COALESCE(spt.vienphistatus_vp,0)=0
	and spt.vienphiid not in (select vp.vienphiid 
							from tools_serviceprice_pttt vp 
							where vp.vienphidate_ravien>='2016-01-01 00:00:00' 
									and vp.vienphistatus<>0 
									and COALESCE(vp.vienphistatus_vp,0)=0)


	
	
	
	