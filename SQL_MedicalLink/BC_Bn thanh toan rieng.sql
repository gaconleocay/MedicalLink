--bv viet tiep ngay 6/7
---dv kt ko có dấu tích "tính lại pttt" có đi kèm mà bây h chuyển thành vật tư thanh toán riêng. 

1. cập nhật đi kèm sang hao phí với tất cả dvkt ko tích vào tính lại pttt
2. quét bn ra viện áp dụng thahgs luong cơ bản
3. cập nhật tất cả bn có vat tu thanh toán riêng nhung ko có id đi kèm

làm hộ a cái tool quét toàn bộ dvkt mag ko tính lại pttt dó có vật tư đi kèm bây h là ttrieng sau đó để a chuyển sang bên hao phí pttt
1 cái nữa là cập nhật lại tiền lương co bản với bệnh án đã đóng rồi


select ROW_NUMBER () OVER (ORDER BY vp.vienphiid,maubenhphamid desc) as stt, 
	ser.servicepriceid,
	ser.maubenhphamid,
	vp.patientid, 
	vp.vienphiid,
	hsba.patientname,
	vp.vienphidate as thoigianvaovien,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	ser.servicepricecode,
	ser.servicepricename,
	ser.servicepricedate,
	degp.departmentgroupname,
	de.departmentname,
	ser.soluong,
	ser.loaidoituong,
	ser.servicepriceid_master,
	serm.servicepricecode as servicepricecode_master,
	serm.servicepricename as servicepricename_master,
	ser.servicepriceid_thanhtoanrieng,
	(case when vp.vienphistatus=0 then 'Đang điều trị'
			else (case when COALESCE(vp.vienphistatus_vp,0)=0 then 'Ra viện chưa thanh toán'
						else 'Đã thanh toán' end)
			end) as trangthaivienphi
from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and coalesce(servicepriceid_thanhtoanrieng,0)=0 and servicepricedate>='2017-01-01 00:00:00') ser
	inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi 
		where vienphidate between '2017-01-01 00:00:00' and '2017-01-02 00:00:00') vp on vp.vienphiid=ser.vienphiid
	inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='2017-01-01 00:00:00') serm on serm.servicepriceid=ser.servicepriceid_master
	inner join (select servicepricecode from servicepriceref where tinhtoanlaigiadvktc=0) serf on serf.servicepricecode=serm.servicepricecode
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid;


---------UPDATE Di kem - hao phi (loaidoituong=2 sang Hao phi pttt loaidoituong=7)
update serviceprice set loaidoituong=7 where loaidoituong=2 and servicepriceid in (select ser.servicepriceid
from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and coalesce(servicepriceid_thanhtoanrieng,0)=0 and servicepricedate>='2017-01-01 00:00:00') ser
	inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi 
		where vienphidate between '2017-01-01 00:00:00' and '2017-01-02 00:00:00') vp on vp.vienphiid=ser.vienphiid
	inner join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='2017-01-01 00:00:00') serm on serm.servicepriceid=ser.servicepriceid_master
	inner join (select servicepricecode from servicepriceref where tinhtoanlaigiadvktc=0) serf on serf.servicepricecode=serm.servicepricecode
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid);
	
----Luu log
insert into tools_serviceprice_ttrieng(servicepriceid,updatetype,dateupdate) 
SELECT Dser.servicepriceid, '1' as updatetype, '" + dateupdate + "' as dateupdate
FROM 
dblink('myconn','select ser.servicepriceid from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and coalesce(servicepriceid_thanhtoanrieng,0)=0 and servicepricedate>=''2017-01-01 00:00:00'') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between ''2017-01-01 00:00:00'' and ''2017-01-02 00:00:00'') vp on vp.vienphiid=ser.vienphiid inner join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>=''2017-01-01 00:00:00'') serm on serm.servicepriceid=ser.servicepriceid_master inner join (select servicepricecode from servicepriceref where tinhtoanlaigiadvktc=0) serf on serf.servicepricecode=serm.servicepricecode inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid')
 AS Dser(servicepriceid integer);



	
	
	
-----===================Khong co ID dich vu di kem ma la thanh toan rieng
select ROW_NUMBER () OVER (ORDER BY vp.vienphiid,maubenhphamid desc) as stt, 
	ser.servicepriceid,
	ser.maubenhphamid,
	vp.patientid, 
	vp.vienphiid,
	hsba.patientname,
	vp.vienphidate as thoigianvaovien,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	ser.servicepricecode,
	ser.servicepricename,
	ser.servicepricedate,
	degp.departmentgroupname,
	de.departmentname,
	ser.soluong,
	ser.loaidoituong,
	ser.servicepriceid_master,
	serttr.servicepricecode as servicepricecode_master,
	serttr.servicepricename as servicepricename_master,
	ser.servicepriceid_thanhtoanrieng,
	(case when vp.vienphistatus=0 then 'Đang điều trị'
			else (case when COALESCE(vp.vienphistatus_vp,0)=0 then 'Ra viện chưa thanh toán'
						else 'Đã thanh toán' end)
			end) as trangthaivienphi
from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and servicepriceid_thanhtoanrieng>0 and servicepriceid_master=0 and servicepricedate>='2017-01-01 00:00:00') ser
	inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi 
		where vienphidate between '2017-01-01 00:00:00' and '2017-01-02 00:00:00') vp on vp.vienphiid=ser.vienphiid
	inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
left join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='2017-01-01 00:00:00') serttr on serttr.servicepriceid=ser.servicepriceid_thanhtoanrieng
inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid;	

----update ID dich vu di kem = ID dich vu thanh toan rieng
update serviceprice set servicepriceid_master=servicepriceid_thanhtoanrieng where  servicepriceid_master=0 and servicepriceid in (select ser.servicepriceid
from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and servicepriceid_thanhtoanrieng>0 and servicepriceid_master=0 and servicepricedate>='2017-01-01 00:00:00') ser
 inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi 
  where vienphidate between '2017-01-01 00:00:00' and '2017-01-02 00:00:00') vp on vp.vienphiid=ser.vienphiid
left join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='2017-01-01 00:00:00') serttr on serttr.servicepriceid=ser.servicepriceid_thanhtoanrieng
inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid);

----luu log
insert into tools_serviceprice_ttrieng(servicepriceid,updatetype,dateupdate) SELECT Dser.servicepriceid, '2' as updatetype, '" + dateupdate + "' as dateupdate FROM dblink('myconn','select ser.servicepriceid
from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and servicepriceid_thanhtoanrieng>0 and servicepriceid_master=0 and servicepricedate>=''2017-01-01 00:00:00'') ser
	inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi 
		where vienphidate between ''2017-01-01 00:00:00'' and ''2017-01-02 00:00:00'') vp on vp.vienphiid=ser.vienphiid
left join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>=''2017-01-01 00:00:00'') serttr on serttr.servicepriceid=ser.servicepriceid_thanhtoanrieng
inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid') AS Dser(servicepriceid integer);


select * from serviceprice where servicepriceid_thanhtoanrieng>0 and loaidoituong<>2






	
	
	
	
	
	
	

