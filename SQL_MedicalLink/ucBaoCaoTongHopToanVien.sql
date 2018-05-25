--ucBaoCaoTongHopToanVien


--ngay 23/3/2018: tach nhom PTTT yeu cau, VTTT rieng
--ngay 25/5/2018: them tieu chi dv da thu tien

--theo khoa ra vien + xem tong hop
SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, 
	O.* 
FROM 
(SELECT (case A.loaivienphiid when 1 then 'Ngoại trú' when 0 then 'Nội trú' else '' end) as loaivienphi, 
		(A.tam_ung) as tam_ung, 
		(A.slbn_bh) as slbn_bh, 
		(A.slbn_vp) as slbn_vp, 
		(A.slbn_bh + A.slbn_vp) as slbn_tong, 
		(B.money_khambenh) as money_khambenh, 
		(B.money_xetnghiem) as money_xetnghiem, 
		(B.money_cdhatdcn) as money_cdhatdcn, 
		(B.money_pttt) as money_pttt, 
		(B.money_ptttyeucau) as money_ptttyeucau,
		(B.money_dvktc) as money_dvktc, 
		(B.money_giuongthuong) as money_giuongthuong, 
		(B.money_giuongyeucau) as money_giuongyeucau, 
		(B.money_nuocsoi) as money_nuocsoi, 
		(B.money_xuatan) as money_xuatan, 
		(B.money_diennuoc) as money_diennuoc, 
		(B.money_mau) as money_mau, 
		(B.money_thuoc) as money_thuoc, 
		(B.money_vattu) as money_vattu, 
		(B.money_vattu_ttrieng) as money_vattu_ttrieng,
		(B.money_vtthaythe) as money_vtthaythe, 
		(B.money_phuthu) as money_phuthu, 
		(B.money_vanchuyen) as money_vanchuyen, 
		(B.money_khac) as money_khac, 
		(B.money_tong) as money_tong 
FROM (SELECT V.loaivienphiid, 
				sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
				sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
				count(V.*) as slbn, 
				SUM(V.tam_ung) as tam_ung 
		FROM (SELECT sum(b.datra) as tam_ung, 
					vp.loaivienphiid, 
					vp.vienphiid, 
					vp.doituongbenhnhanid 
				FROM vienphi vp 
					LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
				WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + dateTu + "' and vp.duyet_ngayduyet_vp<='" + dateDen + "' 
				GROUP BY vp.vienphiid) V 
		GROUP BY V.loaivienphiid) A 
LEFT JOIN (SELECT spt.loaivienphiid, 
				sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
				sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
				sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
				sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
				sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau, 
				sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
				sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
				sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
				sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
				sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, 
				sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, 
				sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, 
				sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
				sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
				sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
				sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
				sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
				sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
				sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh +  + spt.money_vattu_ttrieng_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong 
		FROM ihs_servicespttt spt 
		WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp>='" + dateTu + "' and spt.duyet_ngayduyet_vp<='" + dateDen + "' "+_thutienstatus+"
		GROUP BY spt.loaivienphiid) B ON A.loaivienphiid=B.loaivienphiid 
) O;


--Theo khoa ra vien + xem chi tiet tung khoa
SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, 
O.* 
FROM 
(SELECT depg.departmentgroupname as loaivienphi, 
		(A.tam_ung) as tam_ung, 
		(A.slbn_bh) as slbn_bh, 
		(A.slbn_vp) as slbn_vp, 
		(A.slbn_bh + A.slbn_vp) as slbn_tong, 
		(B.money_khambenh) as money_khambenh, 
		(B.money_xetnghiem) as money_xetnghiem, 
		(B.money_cdhatdcn) as money_cdhatdcn, 
		(B.money_pttt) as money_pttt,
		(B.money_ptttyeucau) as money_ptttyeucau,
		(B.money_dvktc) as money_dvktc, 
		(B.money_giuongthuong) as money_giuongthuong, 
		(B.money_giuongyeucau) as money_giuongyeucau, 
		(B.money_nuocsoi) as money_nuocsoi, 
		(B.money_xuatan) as money_xuatan, 
		(B.money_diennuoc) as money_diennuoc, 
		(B.money_mau) as money_mau, 
		(B.money_thuoc) as money_thuoc, 
		(B.money_vattu) as money_vattu,
		(B.money_vattu_ttrieng) as money_vattu_ttrieng,
		(B.money_vtthaythe) as money_vtthaythe, 
		(B.money_phuthu) as money_phuthu, 
		(B.money_vanchuyen) as money_vanchuyen, 
		(B.money_khac) as money_khac, 
		(B.money_tong) as money_tong 
FROM departmentgroup depg 
LEFT JOIN (SELECT V.departmentgroupid, 
					sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
					sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
					count(V.*) as slbn, 
					SUM(V.tam_ung) as tam_ung 
			FROM (SELECT sum(b.datra) as tam_ung, 
						vp.departmentgroupid, 
						vp.vienphiid, 
						vp.doituongbenhnhanid 
					FROM vienphi vp 
						LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
					WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + dateTu + "' and vp.duyet_ngayduyet_vp<='" + dateDen + "' 
					GROUP BY vp.vienphiid, 
							vp.departmentgroupid) V 
			GROUP BY V.departmentgroupid) A ON A.departmentgroupid=depg.departmentgroupid 
LEFT JOIN (SELECT spt.khoaravien, 
				sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
				sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
				sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
				sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt,
				sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,
				sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
				sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
				sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
				sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
				sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, 
				sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, 
				sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, 
				sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
				sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu,
				sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
				sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
				sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
				sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
				sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh +  + spt.money_vattu_ttrieng_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong 
			FROM ihs_servicespttt spt 
			WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp>='" + dateTu + "' and spt.duyet_ngayduyet_vp<='" + dateDen + "' "+_thutienstatus+" 
			GROUP BY spt.khoaravien) B ON B.khoaravien=depg.departmentgroupid 
WHERE depg.departmentgrouptype in (1,4,11,10,100)) O; 



--theo khoa ra vien + Xem chi tiết bệnh nhân
SELECT ROW_NUMBER () OVER (ORDER BY O.vienphiid) as stt, 
O.* 
FROM 
(SELECT A.vienphiid, 
		hsba.patientid, 
		hsba.patientname, 
		(A.tam_ung) as tam_ung, 
		(A.slbn_bh) as slbn_bh, 
		(A.slbn_vp) as slbn_vp, 
		(A.slbn_bh + A.slbn_vp) as slbn_tong, 
		(B.money_khambenh) as money_khambenh, 
		(B.money_xetnghiem) as money_xetnghiem, 
		(B.money_cdhatdcn) as money_cdhatdcn, 
		(B.money_pttt) as money_pttt,
		(B.money_ptttyeucau) as money_ptttyeucau,
		(B.money_dvktc) as money_dvktc, 
		(B.money_giuongthuong) as money_giuongthuong, 
		(B.money_giuongyeucau) as money_giuongyeucau, 
		(B.money_nuocsoi) as money_nuocsoi, 
		(B.money_xuatan) as money_xuatan, 
		(B.money_diennuoc) as money_diennuoc, 
		(B.money_mau) as money_mau, 
		(B.money_thuoc) as money_thuoc, 
		(B.money_vattu) as money_vattu,
		(B.money_vattu_ttrieng) as money_vattu_ttrieng,
		(B.money_vtthaythe) as money_vtthaythe, 
		(B.money_phuthu) as money_phuthu, 
		(B.money_vanchuyen) as money_vanchuyen, 
		(B.money_khac) as money_khac, 
		(B.money_tong) as money_tong 
FROM (SELECT V.vienphiid, 
			V.hosobenhanid, 
			sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
			sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
			count(V.*) as slbn, 
			sum(V.tam_ung) as tam_ung 
		FROM (SELECT sum(b.datra) as tam_ung, 
					vp.hosobenhanid, 
					vp.vienphiid, 
					vp.doituongbenhnhanid 
		FROM vienphi vp 
		LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
		WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + dateTu + "' and vp.duyet_ngayduyet_vp<='" + dateDen + "' 
		GROUP BY vp.vienphiid) V 
	GROUP BY V.vienphiid,V.hosobenhanid,V.doituongbenhnhanid) A 
LEFT JOIN (SELECT spt.vienphiid, 
				sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
				sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
				sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
				sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt,
				sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,
				sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
				sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
				sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
				sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
				sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, 
				sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, 
				sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, 
				sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
				sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
				sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
				sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
				sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
				sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
				sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh +  + spt.money_vattu_ttrieng_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong 
			FROM ihs_servicespttt spt 
			WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp>='" + dateTu + "' and spt.duyet_ngayduyet_vp<='" + dateDen + "' "+_thutienstatus+"
			GROUP BY spt.vienphiid) B ON A.vienphiid=B.vienphiid 
INNER JOIN hosobenhan hsba on hsba.hosobenhanid=A.hosobenhanid ) O; 


--theo khoa chi dinh + xem tong hop
SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, 
O.* 
FROM 
(SELECT (case de.departmenttype when 2 then 'Ngoại trú' when 3 then 'Nội trú' when 9 then 'Điều trị ngoại trú' else '' end) as loaivienphi, 
		sum(A.tam_ung) as tam_ung, 
		sum(A.slbn_bh) as slbn_bh, 
		sum(A.slbn_vp) as slbn_vp, 
		sum(A.slbn_bh + A.slbn_vp) as slbn_tong, 
		sum(B.money_khambenh) as money_khambenh, 
		sum(B.money_xetnghiem) as money_xetnghiem, 
		sum(B.money_cdhatdcn) as money_cdhatdcn, 
		sum(B.money_pttt) as money_pttt, 
		sum(B.money_ptttyeucau) as money_ptttyeucau,
		sum(B.money_dvktc) as money_dvktc, 
		sum(B.money_giuongthuong) as money_giuongthuong, 
		sum(B.money_giuongyeucau) as money_giuongyeucau, 
		sum(B.money_nuocsoi) as money_nuocsoi, 
		sum(B.money_xuatan) as money_xuatan, 
		sum(B.money_diennuoc) as money_diennuoc, 
		sum(B.money_mau) as money_mau, 
		sum(B.money_thuoc) as money_thuoc, 
		sum(B.money_vattu) as money_vattu, 
		sum(B.money_vattu_ttrieng) as money_vattu_ttrieng,
		sum(B.money_vtthaythe) as money_vtthaythe, 
		sum(B.money_phuthu) as money_phuthu, 
		sum(B.money_vanchuyen) as money_vanchuyen, 
		sum(B.money_khac) as money_khac, 
		sum(B.money_tong) as money_tong 
FROM department de 
LEFT JOIN (SELECT V.departmentid, 
				sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
				sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
				count(V.*) as slbn, 
				SUM(V.tam_ung) as tam_ung 
			FROM (SELECT sum(b.datra) as tam_ung, 
						(case when b.departmentid=0 then 8 else b.departmentid end) as departmentid, 
						vp.vienphiid, 
						vp.doituongbenhnhanid 
					FROM vienphi vp 
						LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
					WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + dateTu + "' and '" + dateDen + "' GROUP BY vp.vienphiid,b.departmentid) V 
			GROUP BY V.departmentid) A ON A.departmentid=de.departmentid 
LEFT JOIN (SELECT spt.departmentid, 
				sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
				sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
				sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
				sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt,
				sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,				
				sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
				sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
				sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
				sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
				sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, 
				sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, 
				sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, 
				sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
				sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
				sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
				sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
				sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
				sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
				sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh +  + spt.money_vattu_ttrieng_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong 
			FROM ihs_servicespttt spt 
			WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + dateTu + "' and '" + dateDen + "' "+_thutienstatus+"
			GROUP BY spt.departmentid) B ON B.departmentid=de.departmentid 
WHERE de.departmenttype in (2,3,9) 
GROUP BY departmenttype) O; 


--theo khoa chi dinh + xem chi tiet tung khoa 
SELECT ROW_NUMBER () OVER (ORDER BY O.loaivienphi) as stt, 
O.* 
FROM 
(SELECT depg.departmentgroupname as loaivienphi, 
			(A.tam_ung) as tam_ung, 
			(A.slbn_bh) as slbn_bh, 
			(A.slbn_vp) as slbn_vp, 
			(A.slbn_bh + A.slbn_vp) as slbn_tong, 
			(B.money_khambenh) as money_khambenh, 
			(B.money_xetnghiem) as money_xetnghiem, 
			(B.money_cdhatdcn) as money_cdhatdcn, 
			(B.money_pttt) as money_pttt,
			(B.money_ptttyeucau) as money_ptttyeucau,
			(B.money_dvktc) as money_dvktc, 
			(B.money_giuongthuong) as money_giuongthuong, 
			(B.money_giuongyeucau) as money_giuongyeucau, 
			(B.money_nuocsoi) as money_nuocsoi, 
			(B.money_xuatan) as money_xuatan, 
			(B.money_diennuoc) as money_diennuoc, 
			(B.money_mau) as money_mau, 
			(B.money_thuoc) as money_thuoc, 
			(B.money_vattu) as money_vattu, 
			(B.money_vattu_ttrieng) as money_vattu_ttrieng,
			(B.money_vtthaythe) as money_vtthaythe, 
			(B.money_phuthu) as money_phuthu, 
			(B.money_vanchuyen) as money_vanchuyen, 
			(B.money_khac) as money_khac, 
			(B.money_tong) as money_tong 
	FROM departmentgroup depg 
		LEFT JOIN (SELECT V.departmentgroupid, 
							sum(case when V.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, 
							sum(case when V.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, 
							count(V.*) as slbn, 
							SUM(V.tam_ung) as tam_ung 
					FROM (SELECT sum(b.datra) as tam_ung, 
								b.departmentgroupid, 
								vp.vienphiid, 
								vp.doituongbenhnhanid 
							FROM vienphi vp 
								LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0
							WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + dateTu + "' and '" + dateDen + "' 
							GROUP BY vp.vienphiid, 
						b.departmentgroupid) V 
					GROUP BY V.departmentgroupid) A ON A.departmentgroupid=depg.departmentgroupid 
LEFT JOIN (SELECT spt.departmentgroupid, 
				sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
				sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
				sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
				sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
				sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,
				sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
				sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
				sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
				sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
				sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi, 
				sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan, 
				sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc, 
				sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_thuoc, 
				sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_vattu, 
				sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
				sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
				sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
				sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
				sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh +  + spt.money_vattu_ttrieng_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh + spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong 
			FROM ihs_servicespttt spt 
			WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + dateTu + "' and '" + dateDen + "' "+_thutienstatus+"
			GROUP BY spt.departmentgroupid) B ON B.departmentgroupid=depg.departmentgroupid 
WHERE depg.departmentgrouptype in (1,4,11,10,100)) O; 












