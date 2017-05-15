--Chi tiet khoa 
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung,
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 	
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast((spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast((spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast((spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc,
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
		FROM tools_serviceprice_pttt spt 
			inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
			inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
			inner join department prv ON spt.departmentid=prv.departmentid and prv.departmenttype in (2,3,9) 
		WHERE spt.vienphistatus_vp=1 
			and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
			and spt.departmentid in (92)) A 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
			b.vienphiid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid and b.departmentid in (92)
	where vp.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
		and b.loaiphieuthuid=2 
		and b.dahuyphieu=0 
		and vp.vienphistatus_vp=1 
	group by b.vienphiid) BILL ON BILL.vienphiid=A.vienphiid			
ORDER BY A.duyet_ngayduyet_vp ;


---chi tiet gay me

SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung,
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 	
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast((spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast((spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast((spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc,
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
		FROM tools_serviceprice_pttt spt 
			inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
			inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
			inner join department prv ON spt.departmentid=prv.departmentid and prv.departmenttype in (2,3,9) 
		WHERE spt.vienphistatus_vp=1 
			and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
			and spt.departmentid in (34,335,269,285) and spt.departmentgroup_huong in (27)

			) A 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
			b.vienphiid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid and b.departmentid in (34,335,269,285)
	where vp.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
		and b.loaiphieuthuid=2 
		and b.dahuyphieu=0 
		and vp.vienphistatus_vp=1 
	group by b.vienphiid) BILL ON BILL.vienphiid=A.vienphiid			
ORDER BY A.duyet_ngayduyet_vp ;


--Tong khoa + Gay me

SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung,
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.departmentgroupid, 
		spt.departmentid,
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 	
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast((spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast((spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast((spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc,
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
		FROM tools_serviceprice_pttt spt 
			inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
			inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
			inner join department prv ON spt.departmentid=prv.departmentid and prv.departmenttype in (2,3,9) 
		WHERE spt.vienphistatus_vp=1 
			and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
			and (spt.departmentid in (92) 
			or (spt.departmentid in (34,335,269,285) and spt.departmentgroup_huong in (27)))
		
			) A 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
			b.vienphiid,
			b.departmentid
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid and b.departmentid in (92,34,335,269,285)
	where vp.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
		and b.loaiphieuthuid=2 
		and b.dahuyphieu=0 
		and vp.vienphistatus_vp=1 
	group by b.vienphiid, b.departmentid) BILL ON BILL.vienphiid=A.vienphiid and BILL.departmentid = A.departmentid 			
ORDER BY A.duyet_ngayduyet_vp ;




