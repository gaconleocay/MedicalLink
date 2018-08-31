--Lấy chi tiết BN trong BC ucBCTongHopDoanhThuKhoa
--BN BHYT + DT BHYT: ngày 31/8/2018


SELECT spt.vienphiid, 
 (spt.patientid || '_' || TO_CHAR(spt.vienphidate,'yyyyMMddHH24MI')) as mabntt,
 --count(spt.*) as soluot, 
 --sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
 --sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
 sum(spt.money_khambenh_bh) as money_khambenh, 
 sum(spt.money_xetnghiem_bh) as money_xetnghiem, 
 sum(spt.money_cdha_bh + spt.money_tdcn_bh) as money_cdhatdcn, 
 sum(spt.money_pttt_bh) as money_pttt, 
 sum(spt.money_ptttyeucau_bh) as money_ptttyeucau,
 sum(spt.money_dvktc_bh) as money_dvktc, 
 sum(spt.money_mau_bh) as money_mau, 
 sum(spt.money_giuongthuong_bh) as money_giuongthuong, 
 sum(spt.money_giuongyeucau_bh) as money_giuongyeucau, 
 sum(spt.money_nuocsoi_bh) as money_nuocsoi, 
 sum(spt.money_xuatan_bh) as money_xuatan, 
 sum(spt.money_diennuoc_bh) as money_diennuoc, 
 sum(spt.money_thuoc_bh) as money_thuoc, 
 sum(spt.money_vattu_bh) as money_vattu, 
 sum(spt.money_vattu_ttrieng_bh) as money_vattu_ttrieng,
 sum(spt.money_phuthu_bh) as money_phuthu, 
 sum(spt.money_vtthaythe_bh) as money_vtthaythe, 
 sum(spt.money_vanchuyen_bh) as money_vanchuyen, 
 sum(spt.money_khac_bh) as money_khac, 
 sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
 sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
 sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu, 
 sum(spt.money_hppttt) as money_hppttt, 
 sum(spt.money_chiphikhac) as money_chiphikhac, 
 sum(spt.money_dkpttt_thuoc_bh) as money_dkpttt_thuoc, 
 sum(spt.money_dkpttt_vattu_bh) as money_dkpttt_vattu, 
 sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh 
FROM ihs_servicespttt spt 
WHERE 1=1 and spt.doituongbenhnhanid=1 and spt.vienphistatus=2 and spt.duyet_ngayduyet between '2018-08-01 00:00:00' and '2018-08-31 23:59:59'
GROUP BY vienphiid,patientid,vienphidate
				
				
				
				
				