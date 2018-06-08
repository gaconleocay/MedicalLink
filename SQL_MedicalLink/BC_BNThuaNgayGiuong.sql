--BAO CAO BN THUA NGAY GIUONG
--Công văn mới Ngày ra-ngày vào:
--Tìm kiếm BN thừa ngày giường và lấy ngày giường BY, YC của khoa cuối cùng

select row_number () over (order by vp.vienphiid) as stt,
	vp.patientid,
	vp.vienphiid,
	hsba.patientname,
	TO_CHAR(hsba.birthday, 'dd/MM/yyyy') as birthday,
	degp.departmentgroupname,
	vp.vienphidate,
	vp.vienphidate_ravien,
	date_part('day',vp.vienphidate_ravien-vp.vienphidate) as songay,
	sum(ser.soluong) as ngaygiuongBN,
	vp.duyet_ngayduyet,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong=0 then ser.servicepriceid end) as servicepriceid_bh,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong=0 then ser.servicepricecode end) as servicepricecode_bh,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong=0 then ser.servicepricename end) as servicepricename_bh,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong=0 then ser.servicepricemoney_bhyt end) as servicepricemoney_bhyt,	
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong>0 then ser.servicepriceid end) as servicepriceid_yc,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong>0 then ser.servicepricecode end) as servicepricecode_yc,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong>0 then ser.servicepricename end) as servicepricename_yc,
	max(case when ser.departmentgroupid=vp.departmentgroupid and ser.loaidoituong>0 then ser.servicepricemoney end) as servicepricemoney_yc
	
from (select patientid,vienphiid,hosobenhanid,departmentgroupid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,duyet_ngayduyet from vienphi where loaivienphiid=0 and doituongbenhnhanid=1 and vienphistatus>0 
		and duyet_ngayduyet between '2018-05-01 00:00:00' and '2018-05-31 23:59:59'
		--and vienphiid=1165650
		) vp
	inner join (select hosobenhanid,patientname,birthday from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select servicepriceid,vienphiid,servicepricecode,servicepricename,servicepricemoney_bhyt,servicepricemoney,loaidoituong,departmentgroupid,soluong from serviceprice where bhyt_groupcode='12NG' and servicepricedate>'2018-01-01 00:00:00') ser on ser.vienphiid=vp.vienphiid
	inner join (select servicepricecode from servicepriceref where bhyt_groupcode ='12NG' and report_groupcode in ('NGIUONG','NGGIUONGT')) serf on serf.servicepricecode=ser.servicepricecode
	inner join departmentgroup degp on degp.departmentgroupid=vp.departmentgroupid
GROUP BY vp.patientid,vp.vienphiid,hsba.patientname,hsba.birthday,
	degp.departmentgroupname,vp.vienphidate,vp.vienphidate_ravien,vp.duyet_ngayduyet




---
--select servicepricecode from servicepriceref where bhyt_groupcode ='12NG' and report_groupcode in ('NGIUONG','NGGIUONGT')





