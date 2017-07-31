--Bao cao thu tien tu BN - ngay 28/7

SELECT spt.vienphiid,
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau,
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi,
		sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan,
		sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc,		
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 		
		sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_tong		
		FROM tools_serviceprice_pttt spt 
		WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp>='2017-03-01 00:00:00' and spt.duyet_ngayduyet_vp<='2017-03-01 23:59:59' GROUP BY spt.vienphiid


		
ĐT Viện phí
ĐT BHYT
ĐT BHYT+DV Viện phí
ĐT BHYT+DV BHYT
Tất cả		
		
		
		
		
		
		
