select spt.vienphiid
from vienphi spt 
where 
	spt.duyet_ngayduyet_vp>='2017-01-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and spt.vienphistatus_vp=1
	and spt.vienphiid not in (select vp.vienphiid from tools_serviceprice_pttt vp where vp.duyet_ngayduyet_vp>='2017-01-01 00:00:00' 
	and vp.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and vp.vienphistatus_vp=1)
	
------	
select spt.vienphiid
from tools_serviceprice_pttt spt 
where 
	spt.duyet_ngayduyet_vp>='2017-01-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and spt.vienphistatus_vp=1
	and spt.vienphiid not in (select vp.vienphiid from vienphi vp where vp.duyet_ngayduyet_vp>='2017-01-01 00:00:00' 
	and vp.duyet_ngayduyet_vp<='2017-04-30 23:59:59' and vp.vienphistatus_vp=1)	
	
---------
select vp.vienphiid, 
			from vienphi vp 
			where vp.vienphiid not in (select spt.vienphiid from tools_serviceprice_pttt spt)
				and vp.vienphistatus<>0 and COALESCE(vp.vienphistatus_vp,0)=0
				and vienphidate_ravien>='2016-01-01 00:00:00' 
				and vienphidate_ravien<='2017-05-07 23:59:59' 



delete from tools_serviceprice_pttt where 


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





	
	
	
	