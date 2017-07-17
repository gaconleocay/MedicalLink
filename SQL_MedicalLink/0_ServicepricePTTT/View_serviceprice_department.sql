SELECT ROW_NUMBER () OVER (ORDER BY sef.servicepricegroupcode,ser.servicepricecode) as stt, ser.servicepriceid as servicepriceid, vp.patientid as mabn, vp.vienphiid as mavp, mbp.maubenhphamid as maubenhphamid, hsba.patientname as tenbn, krv.departmentgroupname as tenkhoaravien, prv.departmentname as tenphongravien, ser.servicepricecode as madv, ser.servicepricename as tendv, ser.servicepricemoney as dongia, ser.servicepricemoney_bhyt as dongiabhyt, ser.soluong as soluong, ser.soluong * ser.servicepricemoney as thanhtien, ser.soluong * ser.servicepricemoney_bhyt as thanhtienbhyt, ser.servicepricedate as thoigianchidinh, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, vp.vienphidate as thoigianvaovien, vp.vienphidate_ravien as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, mbp.userid as iduserchidinh, ncd.usercode as mauserchidinh, ncd.username as tenuserchidinh, sef.servicepricegroupcode as manhomdichvu, (case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' else '' end) as trangthaithutien, hsba.bhytcode as sothebhyt, case sef.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end as bhyt_groupcode, sef.servicegrouptype as servicegrouptype, (select sum(ser_dk.servicepricemoney * ser_dk.soluong) from serviceprice ser_dk where ser_dk.servicepriceid_master=ser.servicepriceid and ser_dk.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle','093TDTUngthu') and ser_dk.loaidoituong=2 and ser_dk.vienphiid=ser.vienphiid) as thuocdikem, (select sum(ser_dk.servicepricemoney * ser_dk.soluong) from serviceprice ser_dk where ser_dk.servicepriceid_master=ser.servicepriceid and ser_dk.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser_dk.loaidoituong=2 and ser_dk.vienphiid=ser.vienphiid) as vattudikem, ser.chiphidauvao as chiphidauvao, ser.chiphimaymoc as chiphimaymoc, ser.chiphipttt as chiphipttt, mayyte.mayytename as mayytename, mayyte.chiphiliendoanh as chiphiliendoanh  
FROM serviceprice ser  
INNER JOIN vienphi vp ON ser.vienphiid=vp.vienphiid  
INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan) hsba ON vp.hosobenhanid=hsba.hosobenhanid  
INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON vp.departmentid=prv.departmentid   
INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON vp.departmentgroupid=krv.departmentgroupid 
INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd ON ser.departmentid=pcd.departmentid 
INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON vp.departmentgroupid=kcd.departmentgroupid 
INNER JOIN maubenhpham mbp ON mbp.maubenhphamid=ser.maubenhphamid INNER JOIN servicepriceref sef ON ser.servicepricecode=sef.servicepricecode LEFT JOIN mayyte ON ser.mayytedbid=mayyte.mayytedbid LEFT JOIN tools_tblnhanvien ncd on ncd.userhisid=mbp.userid WHERE sef.bhyt_groupcode in ('06PTTT','07KTC') " + tieuchi + " >= '" + datetungay + "' " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + " ORDER BY sef.servicepricegroupcode; 
 
 
INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON vp.departmentid=prv.departmentid   
INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON vp.departmentgroupid=krv.departmentgroupid 









 
