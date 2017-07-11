--Chi tiet bn dang dieu tri - ngay 7/6/2017
--QL Tong the khoa Chi tiet - BN dang dieu tri
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, A.*, case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc FROM (SELECT vpm.vienphiid, vpm.patientid, hsbn.patientname, bhyt.bhytcode, bhyt.bhyt_loaiid, vpm.loaivienphiid, bhyt.du5nam6thangluongcoban, vpm.bhyt_tuyenbenhvien, vpm.departmentgroupid, prv.departmentname, vpm.vienphidate,  
(case when vpm.vienphidate_ravien<>'0001-01-01 00:00:00' then vpm.vienphidate_ravien end) as vienphidate_ravien, 
(case when vpm.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vpm.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,  
round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt, round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, round(cast((vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, round(cast((vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu, round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as money_tong, round(cast((vpm.tam_ung) as numeric),0) as tam_ung  
FROM  
 (select hosobenhanid, departmentid from medicalrecord where medicalrecordstatus in (0,2)  
  and thoigianvaovien>='" + dateKhoangDLTu + "' and departmentid in (" + this.lstPhongChonLayBC + ") group by hosobenhanid, departmentid) med 
 left join (select hosobenhanid, patientname from hosobenhan) hsbn on med.hosobenhanid=hsbn.hosobenhanid  
 left join vienphi_money vpm on vpm.hosobenhanid=med.hosobenhanid 
 left join (select bhytid, bhytcode, bhyt_loaiid, du5nam6thangluongcoban from bhyt) bhyt on bhyt.bhytid=vpm.bhytid  
 left join (select departmentid, departmentname from department where departmenttype in (2,3,9)) prv ON med.departmentid=prv.departmentid 
WHERE vpm.vienphistatus=0 and vpm.vienphidate>='" + dateKhoangDLTu + "' 
) A ORDER BY A.vienphiid;  



TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss')
(case when spt.vienphidate_ravien<>'0001-01-01 00:00:00' then spt.vienphidate_ravien end)

TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss')
(case when spt.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then spt.duyet_ngayduyet_vp end)








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


-----------Benh nhan da thanh toan - daonh thu tinh theo khoa ra vien
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
spt.khoaravien as departmentgroupid, 
prv.departmentname, 
spt.vienphidate, 
TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
round(cast(sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
round(cast(sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
round(cast(sum(spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
round(cast(sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
round(cast(sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
round(cast(sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
round(cast(sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
round(cast(sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
round(cast(sum(spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
round(cast(sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
FROM tools_serviceprice_pttt spt 
inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
inner join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.dateTuNgay + "' and '" + this.dateDenNgay + "' and spt.phongravien in (" + this.lstPhongChonLayBC + ")
GROUP BY spt.vienphiid, spt.patientid, hsbn.patientname, bhyt.bhytcode, bhyt.bhyt_loaiid, spt.loaivienphiid, bhyt.du5nam6thangluongcoban, spt.bhyt_tuyenbenhvien, spt.khoaravien, prv.departmentname, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp) A 

LEFT JOIN (select sum(b.datra) as tam_ung, vp.vienphiid 
			from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0
			where vp.departmentid in (" + this.lstPhongChonLayBC + ") and vp.duyet_ngayduyet_vp between '" + this.dateTuNgay + "' and '" + this.dateDenNgay + "' and vp.vienphistatus_vp=1 group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid 
ORDER BY A.duyet_ngayduyet_vp ;



--------Da ra vien nhung chua thanh toan
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
spt.khoaravien as departmentgroupid, 
prv.departmentname, 
spt.vienphidate, 
TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
DATE_PART('day', spt.vienphidate_ravien - spt.vienphidate) as songay,
TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
round(cast(sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
round(cast(sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
round(cast(sum(spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
round(cast(sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
round(cast(sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
round(cast(sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
round(cast(sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
round(cast(sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
round(cast(sum(spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
round(cast(sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
FROM tools_serviceprice_pttt spt 
inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
inner join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
WHERE COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphistatus<>0 and spt.vienphidate>='" + dateKhoangDLTu + "' and spt.phongravien in (" + this.lstPhongChonLayBC + ")
GROUP BY spt.vienphiid, spt.patientid, hsbn.patientname, bhyt.bhytcode, bhyt.bhyt_loaiid, spt.loaivienphiid, bhyt.du5nam6thangluongcoban, spt.bhyt_tuyenbenhvien, spt.khoaravien, prv.departmentname, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp) A 

LEFT JOIN (select sum(b.datra) as tam_ung, vp.vienphiid 
			from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0
			where vp.departmentid in (" + this.lstPhongChonLayBC + ") and vp.vienphidate>='" + dateKhoangDLTu + "' and COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid 
ORDER BY A.duyet_ngayduyet_vp ;





















