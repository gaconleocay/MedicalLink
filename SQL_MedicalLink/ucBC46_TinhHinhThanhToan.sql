---Báo cáo tình hình thanh toán bênh nhân ra viện																					
--ucBC46_TinhHinhThanhToan

--ngay 22/8/2018

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	sum(case when vp.vienphistatus_vp=0 and doituongbenhnhanid=1 then 1 else 0 end) as sl_rvchuatt_bh,
	sum(case when vp.vienphistatus_vp=0 and doituongbenhnhanid<>1 then 1 else 0 end) as sl_rvchuatt_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=0 then 1 else 0 end) as sl_ttdung_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=0 then 1 else 0 end) as sl_ttdung_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=1 then 1 else 0 end) as sl_ttsau1_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=1 then 1 else 0 end) as sl_ttsau1_vp,	
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=2 then 1 else 0 end) as sl_ttsau2_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=2 then 1 else 0 end) as sl_ttsau2_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=3 then 1 else 0 end) as sl_ttsau3_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=3 then 1 else 0 end) as sl_ttsau3_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=4 then 1 else 0 end) as sl_ttsau4_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=4 then 1 else 0 end) as sl_ttsau4_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=5 then 1 else 0 end) as sl_ttsau5_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=5 then 1 else 0 end) as sl_ttsau5_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=6 then 1 else 0 end) as sl_ttsau6_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=6 then 1 else 0 end) as sl_ttsau6_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt=7 then 1 else 0 end) as sl_ttsau7_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt=7 then 1 else 0 end) as sl_ttsau7_vp,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid=1 and songaytt>7 then 1 else 0 end) as sl_tthon7_bh,
	sum(case when vp.vienphistatus_vp=1 and doituongbenhnhanid<>1 and songaytt>7 then 1 else 0 end) as sl_tthon7_vp
	
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp
	INNER JOIN (select vienphiid,departmentgroupid,vienphistatus,doituongbenhnhanid,vienphidate_ravien,duyet_ngayduyet_vp,coalesce(vienphistatus_vp,0) as vienphistatus_vp,
	(case when duyet_ngayduyet_vp is not null then ((duyet_ngayduyet_vp::date) - (vienphidate_ravien::date)) else -1 end) as songaytt
	 from vienphi where vienphistatus>0 "+++") vp on vp.departmentgroupid=degp.departmentgroupid
GROUP BY degp.departmentgroupname;

















