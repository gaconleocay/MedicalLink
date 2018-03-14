SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepricename) as stt,ser.servicepriceid,ser.maubenhphamid,
vp.patientid, 
vp.vienphiid, 
hsba.patientname,(case when vp.vienphistatus=0 then 'Đang điều trị'		  else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán'		  else 'Chưa thanh toán' end) end) as vienphistatus,kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, 		  
ser.servicepricecode, 
ser.servicepricename,ser.servicepricename_bhyt,ser.servicepricename_nhandan,(case ser.loaidoituong		when 0 then 'BHYT'		when 1 then 'Viện phí'		when 2 then 'Đi kèm'		when 3 then 'Yêu cầu'		when 4 then 'BHYT+YC'		when 5 then 'Hao phí giường, CK'		when 6 then 'BHYT+phụ thu'		when 7 then 'Hao phí PTTT'		when 8 then 'Đối tượng khác'		when 9 then 'Hao phí khác'		when 20 then 'Thanh toán riêng'		end) as loaidoituong,
ser.servicepricemoney, ser.servicepricemoney_bhyt,ser.servicepricemoney_nhandan,ser.servicepricemoney_nuocngoai,		
ser.servicepricedate, 
(case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong, 
(case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as loaiphieu, 
vp.vienphidate, 
(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, 
(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vpFROM (select * from serviceprice where 1=1 "+_danhsachDichVu+_tieuchi_ser+_loaidoituong+") ser	inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where 1=1 "+_vienphistatus+_tieuchi_vp+_doituongbenhnhanid+") vp on vp.vienphiid=ser.vienphiid	inner join (select hosobenhanid,patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid	inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=ser.departmentgroupid 	inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd on pcd.departmentid=ser.departmentid;	