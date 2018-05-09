--Dashboard Doanh thu BN Noi tru
--ucDashboardBenhNhanNoiTru


--ngay 9/5/2018


SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, 
O.* FROM (SELECT depg.departmentgroupname, 
COALESCE(B.money_tong_nhandan,0) as money_tong_nhandan, 
COALESCE(B.money_tong_bhyt,0) as money_tong_bhyt, 
A.tam_ung 
FROM departmentgroup depg 
	LEFT JOIN (SELECT sum(b.datra) as tam_ung,b.departmentgroupid 
				FROM vienphi vp 
				LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
			WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' 
			GROUP BY b.departmentgroupid) A ON A.departmentgroupid=depg.departmentgroupid 
	LEFT JOIN (SELECT spt.departmentgroupid,
				sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_nhandan,
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + + spt.money_vattu_ttrieng_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bhyt 
			FROM ihs_servicespttt spt 
			WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' 
			GROUP BY spt.departmentgroupid) B ON B.departmentgroupid=depg.departmentgroupid 
WHERE depg.departmentgrouptype in (1,4,11,10,100)) O;