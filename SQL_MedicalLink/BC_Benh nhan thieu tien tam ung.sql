--bao cao Benh nhan thieu thien tam ung - bc Thanh Hoa ngay 24/7

select 
	--row_number () over (order by vms.vienphidate) as stt,
	(row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY vms.vienphidate)) as stt,
	vms.patientid,
	hsba.patientcode,
	vms.vienphiid,
	hsba.patientname,
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', 
	' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi,
	bh.bhytcode,
	bh.macskcbbd,
	vms.bhyt_tuyenbenhvien,
	bh.bhyt_loaiid,
	vms.loaivienphiid,
	bh.du5nam6thangluongcoban,
	vms.vienphidate,
	(case when vms.vienphistatus>0 then vms.vienphidate_ravien end) as vienphidate_ravien,
	degp.departmentgroupid,
	degp.departmentgroupname,
	de.departmentname,
	b.money_tamung,
	b.money_datra,
	b.money_hoanung,
	vms.money_tong_bh,
	vms.money_tong_vp,
	0 as money_bhyttt,
	0 as money_bntt,
	0 as money_thieu,
	0 as isgroup
from (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,departmentgroupid,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,bhyt_tuyenbenhvien,
				(money_khambenh_bh+money_xetnghiem_bh+money_cdha_bh+money_tdcn_bh+money_thuoc_bh+money_mau_bh+money_pttt_bh+money_vattu_bh+money_dvktc_bh+money_giuong_bh+money_vanchuyen_bh+money_khac_bh+money_phuthu_bh) as money_tong_bh,
				(money_khambenh_vp+money_xetnghiem_vp+money_cdha_vp+money_tdcn_vp+money_thuoc_vp+money_mau_vp+money_pttt_vp+money_vattu_vp+money_dvktc_vp+money_giuong_vp+money_vanchuyen_vp+money_khac_vp+money_phuthu_vp) as money_tong_vp				
			from vienphi_money_sobn 
			where vienphidate between '"+datetungay+"' and '"+datedenngay+"' "+departmentgroupid+ doituongbenhnhan+" ) vms
	inner join (select hosobenhanid,patientcode,patientname,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vms.hosobenhanid
	inner join (select bhytid,bhytcode,macskcbbd,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=vms.bhytid
	left join (select vienphiid,
					  sum(case when loaiphieuthuid=2 then datra else 0 end) as money_tamung,
					  sum(case when loaiphieuthuid=0 then datra else 0 end) as money_datra,
					  sum(case when loaiphieuthuid=1 then datra else 0 end) as money_hoanung
				from bill 
				where billdate>='"+datetungay+"' and dahuyphieu=0
				group by vienphiid
				) b on b.vienphiid=vms.vienphiid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vms.departmentgroupid 
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=vms.departmentid;
	
--group by vms.patientid,hsba.patientcode,vms.vienphiid,hsba.patientname,hsba.bhytcode,vms.vienphidate,vms.vienphidate_ravien,vms.vienphistatus,degp.departmentgroupid,degp.departmentgroupname,de.departmentname,hsba.hc_sonha,hsba.hc_thon,hsba.hc_xacode,hsba.hc_xaname,hsba.hc_huyencode,hsba.hc_huyenname,hsba.hc_tinhcode,hsba.hc_tinhname,hsba.hc_quocgianame,bh.bhytcode,bh.macskcbbd,bh.bhyt_loaiid,bh.du5nam6thangluongcoban,vms.bhyt_tuyenbenhvien,vms.loaivienphiid;




