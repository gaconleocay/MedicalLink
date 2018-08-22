---Báo cáo tình hình thanh toán bênh nhân ra viện																					
--ucBC46_TinhHinhThanhToan

--ngay 22/8/2018

SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	rv.sl_rvchuatt_bh,
	rv.sl_rvchuatt_vp,
	datt.sl_ttdung_bh,
	datt.sl_ttdung_vp,
	datt.sl_ttsau1_bh,
	datt.sl_ttsau1_vp,	
	datt.sl_ttsau2_bh,
	datt.sl_ttsau2_vp,
	datt.sl_ttsau3_bh,
	datt.sl_ttsau3_vp,
	datt.sl_ttsau4_bh,
	datt.sl_ttsau4_vp,
	datt.sl_ttsau5_bh,
	datt.sl_ttsau5_vp,
	datt.sl_ttsau6_bh,
	datt.sl_ttsau6_vp,
	datt.sl_ttsau7_bh,
	datt.sl_ttsau7_vp,
	datt.sl_tthon7_bh,
	datt.sl_tthon7_vp
FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp
	INNER JOIN (select vp.departmentgroupid,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=0 then 1 else 0 end) as sl_ttdung_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=0 then 1 else 0 end) as sl_ttdung_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=1 then 1 else 0 end) as sl_ttsau1_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=1 then 1 else 0 end) as sl_ttsau1_vp,	
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=2 then 1 else 0 end) as sl_ttsau2_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=2 then 1 else 0 end) as sl_ttsau2_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=3 then 1 else 0 end) as sl_ttsau3_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=3 then 1 else 0 end) as sl_ttsau3_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=4 then 1 else 0 end) as sl_ttsau4_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=4 then 1 else 0 end) as sl_ttsau4_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=5 then 1 else 0 end) as sl_ttsau5_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=5 then 1 else 0 end) as sl_ttsau5_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=6 then 1 else 0 end) as sl_ttsau6_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=6 then 1 else 0 end) as sl_ttsau6_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt=7 then 1 else 0 end) as sl_ttsau7_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt=7 then 1 else 0 end) as sl_ttsau7_vp,
					sum(case when vp.doituongbenhnhanid=1 and vp.songaytt>7 then 1 else 0 end) as sl_tthon7_bh,
					sum(case when vp.doituongbenhnhanid<>1 and vp.songaytt>7 then 1 else 0 end) as sl_tthon7_vp
				from (select vienphiid,departmentgroupid,doituongbenhnhanid,
					(case when duyet_ngayduyet_vp is not null then ((duyet_ngayduyet_vp::date)-(vienphidate_ravien::date)) else -1 end) as songaytt
					 from vienphi where vienphistatus>0 and vienphistatus_vp=1 " + _tieuchi_vp + _lstKhoaChonLayBC + ") vp group by vp.departmentgroupid) datt on datt.departmentgroupid=degp.departmentgroupid 
	LEFT JOIN (select departmentgroupid,sum(case when doituongbenhnhanid=1 then 1 else 0 end) as sl_rvchuatt_bh,sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as sl_rvchuatt_vp from vienphi where vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 "+_tieuchi_vp_rv+" group by departmentgroupid) rv on rv.departmentgroupid=degp.departmentgroupid;
	 
