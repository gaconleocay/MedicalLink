--bao cao PTTT co tinh cho GMHT: ucBCTongHopDoanhThuKhoa
 
 --Chuyển Thuốc, vật tư, DV tại Gây mê vào nhóm của gây mê
 --bo sung them thuoc/vat tu hao phi khong tinh tien trong goi PTTT
 -- hao phi gay me - va hao phi khoa con lai

 
 
--1.1=====================================I.if (cboDoiTuong.Text == "ĐT BHYT + DV BHYT") 
--if (cboTieuChi.Text == "Đã thanh toán")  DT BHYT+DV BHYT
--ngay 27/8/2018
 
SELECT ROW_NUMBER () OVER (ORDER BY dep.departmentgroupname) as stt, 
		dep.departmentgroupid, 
		dep.departmentgroupname, 
		(A.soluot) as soluot, 
		(A.soluot_bh) as soluot_bh, 
		(A.soluot_vp) as soluot_vp, 
		(C.count_bh) as soluong_bh, 
		(C.count_vp) as soluong_vp, 
		(C.count) as soluong, 
		(A.money_khambenh) as money_khambenh, 
		(A.money_xetnghiem) as money_xetnghiem, 
		(A.money_cdhatdcn) as money_cdhatdcn, 
		(A.money_pttt) as money_pttt, 
		(A.money_ptttyeucau) as money_ptttyeucau,
		(A.money_dvktc) as money_dvktc, 
		(A.money_giuongthuong) as money_giuongthuong, 
		(A.money_giuongyeucau) as money_giuongyeucau, 
		(A.money_nuocsoi) as money_nuocsoi, 
		(A.money_xuatan) as money_xuatan, 
		(A.money_diennuoc) as money_diennuoc, 
		(A.money_mau) as money_mau, 
		(A.money_thuoc) as money_thuoc, 
		(A.money_vattu) as money_vattu, 
		(A.money_vattu_ttrieng) as money_vattu_ttrieng,
		(A.money_vtthaythe) as money_vtthaythe, 
		(A.money_phuthu) as money_phuthu, 
		(A.money_vanchuyen) as money_vanchuyen, 
		(A.money_khac) as money_khac, 
		COALESCE((A.money_hpngaygiuong),0)+COALESCE((B.money_hpngaygiuong),0) as money_hpngaygiuong, 
		COALESCE((A.money_chiphikhac),0)+COALESCE((B.money_chiphikhac),0) as money_chiphikhac, 
		(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, 
		(A.money_dkpttt_vattu) as money_dkpttt_vattu, 
		(B.money_mau) as gmht_money_mau, 
		(B.money_pttt) as gmht_money_pttt,
		(B.money_ptttyeucau) as gmht_money_ptttyeucau,
		(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, 
		(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu,
		(B.money_vattu_ttrieng) as gmht_money_vattu_ttrieng,		
		(B.money_vtthaythe) as gmht_money_vtthaythe, 
		(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls, 
		(A.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_thuoc, 
		COALESCE((A.money_hpdkpttt_gm_vattu),0) as money_hpdkpttt_vattu, 
		COALESCE((A.money_hppttt),0) as money_hppttt, 
		(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, 
		COALESCE((B.money_hpdkpttt_gm_vattu),0) as gmht_money_hpdkpttt_vattu, 
		COALESCE((B.money_hppttt),0) as gmht_money_hppttt, 
		COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) as tong_tien_bh, 
		0 as tong_tien_vp, 
		COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) as tong_tien,
		bill.tam_ung,
		CP_XN.chiphixn,
		CP_CDHA.chiphicdha
FROM (select departmentgroupid,departmentgroupname from departmentgroup where departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100)) dep
	LEFT JOIN (SELECT spt.departmentgroupid, 
					count(spt.*) as soluot, 
					sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
					sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
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
				WHERE spt.departmentid not in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt." + _loaitrangthai + " and spt." + loaithoigian + " between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+"
				GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 
	LEFT JOIN (SELECT spt.departmentgroup_huong, 
						sum(spt.money_pttt_bh + spt.money_dvktc_bh) as money_pttt,
						sum(spt.money_ptttyeucau_bh) as money_ptttyeucau,
						sum(spt.money_thuoc_bh) as money_thuoc, 
						sum(spt.money_vattu_bh) as money_vattu, 
						sum(spt.money_vattu_ttrieng_bh) as money_vattu_ttrieng,
						sum(spt.money_vtthaythe_bh) as money_vtthaythe, 
						sum(spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh) as money_cls, 
						sum(spt.money_khambenh_bh) as money_khambenh, 
						sum(spt.money_mau_bh) as money_mau, 
						sum(spt.money_giuongthuong_bh) as money_giuongthuong, 
						sum(spt.money_giuongyeucau_bh) as money_giuongyeucau, 
						sum(spt.money_nuocsoi_bh) as money_nuocsoi, 
						sum(spt.money_xuatan_bh) as money_xuatan, 
						sum(spt.money_diennuoc_bh) as money_diennuoc, 
						sum(spt.money_phuthu_bh) as money_phuthu, 
						sum(spt.money_vanchuyen_bh) as money_vanchuyen, 
						sum(spt.money_khac_bh) as money_khac, 
						sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
						sum(spt.money_chiphikhac) as money_chiphikhac, 
						sum(spt.money_dkpttt_thuoc_bh) as money_dkpttt_thuoc, 
						sum(spt.money_dkpttt_vattu_bh) as money_dkpttt_vattu, 
						sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
						sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu, 
						sum(spt.money_hppttt) as money_hppttt, 
						sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh 
				FROM ihs_servicespttt spt 
				WHERE spt.departmentid in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt." + _loaitrangthai + " and spt." + loaithoigian + " between '" + thoiGianTu + "' and '" + thoiGianDen + "'  "+_thutienstatus+"
				GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong 
	LEFT JOIN (SELECT count(*) as count, 
						sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, 
						sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, 
						vp.departmentgroupid 
				FROM vienphi vp 
				WHERE vp." + _loaitrangthai + " " + doituongbenhnhanid_vp + " and vp." + loaithoigian + " between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
				GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid 

	LEFT JOIN (select sum(b.datra) as tam_ung, 
				b.departmentgroupid 
				from vienphi vp inner join bill b on vp.vienphiid=b.vienphiid 
				where vp." + loaithoigian + " between '" + thoiGianTu + "' and '" + thoiGianDen + "' and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp." + _loaitrangthai + " " + doituongbenhnhanid_vp + " 
				group by b.departmentgroupid) BILL ON BILL.departmentgroupid=dep.departmentgroupid

	LEFT JOIN	
			(SELECT 
				SERV.departmentgroupid,
				sum(SERV.soluong*chiphi.chiphixn) as chiphixn
			FROM
				(select  
					ser.servicepriceid,
					ser.servicepricecode, 
					ser.soluong,		
					se.idmayxn,
					ser.departmentgroupid
				from (select vp.vienphiid from vienphi vp where vp." + _loaitrangthai + " " + doituongbenhnhanid_vp + " and vp." + loaithoigian + " between '" + thoiGianTu + "' and '" + thoiGianDen + "' ) vp
					inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN' and loaidoituong in (0,4,6)) ser on ser.vienphiid=vp.vienphiid
					left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
				group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV
			LEFT JOIN (SELECT *
						FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
						AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn
			GROUP BY SERV.departmentgroupid) CP_XN on CP_XN.departmentgroupid=dep.departmentgroupid
	--Chi phi CDHA 
	LEFT JOIN
		(SELECT A.departmentgroupid,
				sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha
		FROM 
			(SELECT ser.departmentgroupid,vp.vienphiid,ser.servicepriceid,
				(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle,
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as thuoc_tronggoi, 
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as vattu_tronggoi, 
				(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as chiphikhac,
				((case serf.pttt_loaiid 
								when 1 then 1201000 
								when 2 then 572000 
								when 3 then 243000 
								when 4 then 149000 
								when 5 then 185000 
								when 6 then 70500 
								when 7 then 35500 
								when 8 then 27500 
								else 0 end) * ser.soluong) as chiphibs
			FROM (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp		
				inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN') and loaidoituong in (0,4,6)) ser on ser.vienphiid=vp.vienphiid
				inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A
		GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=dep.departmentgroupid;		
	


			
--1.2=====================================I.if (cboDoiTuong.Text == "ĐT BHYT + DV BHYT") 			
-- else if (cboTieuChi.Text == "Ra viện chưa thanh toán")
--ngay 1/7/2018

SELECT ROW_NUMBER () OVER (ORDER BY dep.departmentgroupname) as stt, 
	dep.departmentgroupid, 
	dep.departmentgroupname, 
	(A.soluot) as soluot, 
	(A.soluot_bh) as soluot_bh, 
	(A.soluot_vp) as soluot_vp,
	(C.count_bh) as soluong_bh, 
	(C.count_vp) as soluong_vp, 
	(C.count) as soluong, 
	(A.money_khambenh) as money_khambenh, 
	(A.money_xetnghiem) as money_xetnghiem, 
	(A.money_cdhatdcn) as money_cdhatdcn, 
	(A.money_pttt) as money_pttt,
	(A.money_ptttyeucau) as money_ptttyeucau,
	(A.money_dvktc) as money_dvktc, 
	(A.money_giuongthuong) as money_giuongthuong, 
	(A.money_giuongyeucau) as money_giuongyeucau, 
	(A.money_nuocsoi) as money_nuocsoi, 
	(A.money_xuatan) as money_xuatan, 
	(A.money_diennuoc) as money_diennuoc, 
	(A.money_mau) as money_mau, 
	(A.money_thuoc) as money_thuoc, 
	(A.money_vattu) as money_vattu,
	(A.money_vattu_ttrieng) as money_vattu_ttrieng,
	(A.money_vtthaythe) as money_vtthaythe, 
	(A.money_phuthu) as money_phuthu, 
	(A.money_vanchuyen) as money_vanchuyen, 
	(A.money_khac) as money_khac, 
	COALESCE((A.money_hpngaygiuong),0)+COALESCE((B.money_hpngaygiuong),0) as money_hpngaygiuong, 
	COALESCE((A.money_chiphikhac),0)+COALESCE((B.money_chiphikhac),0) as money_chiphikhac, 
	(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, 
	(A.money_dkpttt_vattu) as money_dkpttt_vattu, 
	(B.money_mau) as gmht_money_mau, 
	(B.money_pttt) as gmht_money_pttt,
	(B.money_ptttyeucau) as gmht_money_ptttyeucau,
	(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, 
	(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu,
	(B.money_vattu_ttrieng) as gmht_money_vattu_ttrieng,	
	(B.money_vtthaythe) as gmht_money_vtthaythe, 
	(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls, 
	(A.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_thuoc, 
	COALESCE((A.money_hpdkpttt_gm_vattu),0) as money_hpdkpttt_vattu, 
	COALESCE((A.money_hppttt),0) as money_hppttt, 
	(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, 
	COALESCE((B.money_hpdkpttt_gm_vattu),0) as gmht_money_hpdkpttt_vattu, 
	COALESCE((B.money_hppttt),0) as gmht_money_hppttt, 
	COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) as tong_tien_bh, 
	0 as tong_tien_vp, 
	COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) as tong_tien,
	bill.tam_ung,
	CP_XN.chiphixn,
	CP_CDHA.chiphicdha
FROM (select departmentgroupid,departmentgroupname from departmentgroup where departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100)) dep 
LEFT JOIN (SELECT spt.departmentgroupid, 
			count(spt.*) as soluot,
			sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
			sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
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
		WHERE spt.departmentid not in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt.vienphistatus=1 and COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "'  "+_thutienstatus+" 
		GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 
LEFT JOIN (SELECT spt.departmentgroup_huong, 
				sum(spt.money_pttt_bh + spt.money_dvktc_bh) as money_pttt, 
				sum(spt.money_ptttyeucau_bh) as money_ptttyeucau,
				sum(spt.money_thuoc_bh) as money_thuoc, 
				sum(spt.money_vattu_bh) as money_vattu, 
				sum(spt.money_vattu_ttrieng_bh) as money_vattu_ttrieng,
				sum(spt.money_vtthaythe_bh) as money_vtthaythe, 
				sum(spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh) as money_cls, 
				sum(spt.money_khambenh_bh) as money_khambenh, 
				sum(spt.money_mau_bh) as money_mau, 
				sum(spt.money_giuongthuong_bh) as money_giuongthuong, 
				sum(spt.money_giuongyeucau_bh) as money_giuongyeucau, 
				sum(spt.money_nuocsoi_bh) as money_nuocsoi, 
				sum(spt.money_xuatan_bh) as money_xuatan, 
				sum(spt.money_diennuoc_bh) as money_diennuoc, 
				sum(spt.money_phuthu_bh) as money_phuthu, 
				sum(spt.money_vanchuyen_bh) as money_vanchuyen, 
				sum(spt.money_khac_bh) as money_khac, 
				sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
				sum(spt.money_chiphikhac) as money_chiphikhac, 
				sum(spt.money_dkpttt_thuoc_bh) as money_dkpttt_thuoc, 
				sum(spt.money_dkpttt_vattu_bh) as money_dkpttt_vattu, 
				sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
				sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu, 
				sum(spt.money_hppttt) as money_hppttt, 
				sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh 	
			FROM ihs_servicespttt spt 
			WHERE spt.departmentid in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt.vienphistatus=1 and COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+" GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong 
LEFT JOIN (SELECT count(*) as count, 
				sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, 
				sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, 
				vp.departmentgroupid 
			FROM vienphi vp 
			WHERE vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0 " + doituongbenhnhanid_vp + " and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
			GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid 
LEFT JOIN (select sum(b.datra) as tam_ung, 
				b.departmentgroupid 
			from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid where vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "' and b.loaiphieuthuid=2 and b.dahuyphieu=0 " + doituongbenhnhanid_vp + " 
			group by b.departmentgroupid) BILL ON BILL.departmentgroupid=dep.departmentgroupid
LEFT JOIN	
		(SELECT 
			SERV.departmentgroupid,
			sum(SERV.soluong*chiphi.chiphixn) as chiphixn
		FROM
			(select  
				ser.servicepriceid,
				ser.servicepricecode, 
				ser.soluong,		
				se.idmayxn,
				ser.departmentgroupid
			from (select vp.vienphiid from vienphi vp where vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0 " + doituongbenhnhanid_vp + " and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "' ) vp
				inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN' and loaidoituong in (0,4,6)) ser on ser.vienphiid=vp.vienphiid
				left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
			group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV
		LEFT JOIN (SELECT *
					FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
					AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn
		GROUP BY SERV.departmentgroupid) CP_XN on CP_XN.departmentgroupid=dep.departmentgroupid

LEFT JOIN
		(SELECT A.departmentgroupid,
				sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha
		FROM 
			(SELECT ser.departmentgroupid,vp.vienphiid,ser.servicepriceid,
				(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle,
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as thuoc_tronggoi, 
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as vattu_tronggoi, 
				(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as chiphikhac,
				((case serf.pttt_loaiid 
								when 1 then 1201000 
								when 2 then 572000 
								when 3 then 243000 
								when 4 then 149000 
								when 5 then 185000 
								when 6 then 70500 
								when 7 then 35500 
								when 8 then 27500 
								else 0 end) * ser.soluong) as chiphibs
			FROM (select vp.vienphiid from vienphi vp where vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0 " + doituongbenhnhanid_vp + " and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp		
				inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN') and loaidoituong in (0,4,6)) ser on ser.vienphiid=vp.vienphiid
				inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A
		GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=dep.departmentgroupid;	
	
	
	
	
	
	
		
--1.3=====================================I.if (cboDoiTuong.Text == "ĐT BHYT + DV BHYT")  
--else if (cboTieuChi.Text == "Chưa ra viện")
--ngay 27/5/2018

SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, 
	O.*, 
	bill.tam_ung,
	CP_XN.chiphixn		
FROM (SELECT dep.departmentgroupid, 
		dep.departmentgroupname, 
		sum(A.soluot) as soluot,
		sum(A.soluot_bh) as soluot_bh, 
		sum(A.soluot_vp) as soluot_vp,
		sum(C.count_bh) as soluong_bh, 
		sum(C.count_vp) as soluong_vp, 
		sum(C.count) as soluong, 
		sum(A.money_khambenh) as money_khambenh, 
		sum(A.money_xetnghiem) as money_xetnghiem, 
		sum(A.money_cdhatdcn) as money_cdhatdcn, 
		sum(A.money_pttt) as money_pttt,
		sum(A.money_ptttyeucau) as money_ptttyeucau,
		sum(A.money_dvktc) as money_dvktc, 
		sum(A.money_giuongthuong) as money_giuongthuong, 
		sum(A.money_giuongyeucau) as money_giuongyeucau, 
		sum(A.money_nuocsoi) as money_nuocsoi, 
		sum(A.money_xuatan) as money_xuatan, 
		sum(A.money_diennuoc) as money_diennuoc, 
		sum(A.money_mau) as money_mau, 
		sum(A.money_thuoc) as money_thuoc, 
		sum(A.money_vattu) as money_vattu, 
		sum(A.money_vattu_ttrieng) as money_vattu_ttrieng,
		sum(A.money_vtthaythe) as money_vtthaythe, 
		sum(A.money_phuthu) as money_phuthu, 
		sum(A.money_vanchuyen) as money_vanchuyen, 
		sum(A.money_khac) as money_khac, 
		COALESCE(sum(A.money_hpngaygiuong),0)+COALESCE(sum(B.money_hpngaygiuong),0) as money_hpngaygiuong, 
		COALESCE(sum(A.money_chiphikhac),0)+COALESCE(sum(B.money_chiphikhac),0) as money_chiphikhac, 
		sum(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, 
		sum(A.money_dkpttt_vattu) as money_dkpttt_vattu, 
		sum(B.money_mau) as gmht_money_mau, 
		sum(B.money_pttt) as gmht_money_pttt, 
		sum(B.money_ptttyeucau) as gmht_money_ptttyeucau,
		sum(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, 
		sum(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu, 
		sum(B.money_vattu_ttrieng) as gmht_money_vattu_ttrieng,	
		sum(B.money_vtthaythe) as gmht_money_vtthaythe, 
		sum(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls, 
		sum(A.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_thuoc, 
		COALESCE(sum(A.money_hpdkpttt_gm_vattu),0) as money_hpdkpttt_vattu, 
		COALESCE(sum(A.money_hppttt),0) as money_hppttt, 
		sum(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, 
		COALESCE(sum(B.money_hpdkpttt_gm_vattu),0) as gmht_money_hpdkpttt_vattu, 
		COALESCE(sum(B.money_hppttt),0) as gmht_money_hppttt, 
		COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) as tong_tien_bh, 
		0 as tong_tien_vp, 
		COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) as tong_tien 
FROM departmentgroup dep 
LEFT JOIN (SELECT spt.departmentgroupid, 
			count(spt.*) as soluot, 
			sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
			sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
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
		FROM view_tools_serviceprice_pttt spt 
		WHERE spt.departmentid not in (34,335,269,285) and spt.vienphistatus=0 and spt.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "' " + doituongbenhnhanid_spt + " 
		GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 
LEFT JOIN (SELECT spt.departmentgroup_huong, 
			sum(spt.money_pttt_bh + spt.money_dvktc_bh) as money_pttt, 
			sum(spt.money_ptttyeucau_bh) as money_ptttyeucau,
			sum(spt.money_thuoc_bh) as money_thuoc, 
			sum(spt.money_vattu_bh) as money_vattu, 
			sum(spt.money_vattu_ttrieng_bh) as money_vattu_ttrieng,
			sum(spt.money_vtthaythe_bh) as money_vtthaythe, 
			sum(spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh) as money_cls, 
			sum(spt.money_khambenh_bh) as money_khambenh, 
			sum(spt.money_mau_bh) as money_mau, 
			sum(spt.money_giuongthuong_bh) as money_giuongthuong, 
			sum(spt.money_giuongyeucau_bh) as money_giuongyeucau, 
			sum(spt.money_nuocsoi_bh) as money_nuocsoi, 
			sum(spt.money_xuatan_bh) as money_xuatan, 
			sum(spt.money_diennuoc_bh) as money_diennuoc, 
			sum(spt.money_phuthu_bh) as money_phuthu, 
			sum(spt.money_vanchuyen_bh) as money_vanchuyen, 
			sum(spt.money_khac_bh) as money_khac, 
			sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
			sum(spt.money_chiphikhac) as money_chiphikhac, 
			sum(spt.money_dkpttt_thuoc_bh) as money_dkpttt_thuoc, 
			sum(spt.money_dkpttt_vattu_bh) as money_dkpttt_vattu, 
			sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
			sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu, 
			sum(spt.money_hppttt) as money_hppttt, 
			sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh 
		FROM view_tools_serviceprice_pttt spt 
		WHERE spt.departmentid in (34,335,269,285) and spt.vienphistatus=0 and spt.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "' " + doituongbenhnhanid_spt + " 
		GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong 
LEFT JOIN (SELECT count(*) as count, 
				sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, 
				sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, 
				vp.departmentgroupid 
			FROM vienphi vp 
			WHERE vp.vienphistatus=0 " + doituongbenhnhanid_vp + " and vp.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
			GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid 
WHERE dep.departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100) 
GROUP BY dep.departmentgroupid,dep.departmentgroupname) O 
LEFT JOIN (select sum(b.datra) as tam_ung,b.departmentgroupid 
			from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid 
			where vp.vienphistatus=0 and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "' " + doituongbenhnhanid_vp + " 
			group by b.departmentgroupid) BILL ON BILL.departmentgroupid=O.departmentgroupid
LEFT JOIN	
		(SELECT 
			SERV.departmentgroupid,
			sum(SERV.soluong*chiphi.chiphixn) as chiphixn
		FROM
			(select  
				ser.servicepriceid,
				ser.servicepricecode, 
				ser.soluong,		
				se.idmayxn,
				ser.departmentgroupid
			from (select vp.vienphiid from vienphi vp where vp.vienphistatus=0 " + doituongbenhnhanid_vp + " and vp.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp
				inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN' and loaidoituong in (0,4,6)) ser on ser.vienphiid=vp.vienphiid
				left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
			group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV
		LEFT JOIN (SELECT *
					FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
					AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn
		GROUP BY SERV.departmentgroupid) CP_XN on CP_XN.departmentgroupid=O.departmentgroupid;
 
 
 
 
 
 
 
 
--1.4=====================================II. Cho doi tuong Khac (trừ DTBHYT+dvBHYT)
 --if (cboTrangThai.Text == "Đã thanh toán")
--ngay 29/5/2018: them hao phi XN; CDHA, hao phi Khoa
 
SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, 
	O.*, 
	BILL.tam_ung,
	CP_XN.chiphixn,
	CP_CDHA.chiphicdha,
	CP_KHOA.chiphikhoa
FROM 
(SELECT dep.departmentgroupid, 
	dep.departmentgroupname, 
	sum(A.soluot) as soluot,
	sum(A.soluot_bh) as soluot_bh, 
	sum(A.soluot_vp) as soluot_vp,
	sum(C.count_bh) as soluong_bh, 
	sum(C.count_vp) as soluong_vp, 
	sum(C.count) as soluong, 
	sum(A.money_khambenh) as money_khambenh, 
	sum(A.money_xetnghiem) as money_xetnghiem, 
	sum(A.money_cdhatdcn) as money_cdhatdcn, 
	sum(A.money_pttt) as money_pttt, 
	sum(A.money_ptttyeucau) as money_ptttyeucau,
	sum(A.money_dvktc) as money_dvktc, 
	sum(A.money_giuongthuong) as money_giuongthuong, 
	sum(A.money_giuongyeucau) as money_giuongyeucau,
	sum(A.money_nuocsoi) as money_nuocsoi,
	sum(A.money_xuatan) as money_xuatan,
	sum(A.money_diennuoc) as money_diennuoc,
	sum(A.money_mau) as money_mau, 
	sum(A.money_thuoc) as money_thuoc, 
	sum(A.money_vattu) as money_vattu, 
	sum(A.money_vattu_ttrieng) as money_vattu_ttrieng,
	sum(A.money_vtthaythe) as money_vtthaythe, 
	sum(A.money_phuthu) as money_phuthu, 
	sum(A.money_vanchuyen) as money_vanchuyen, 
	sum(A.money_khac) as money_khac, 
	COALESCE(sum(A.money_hpngaygiuong),0)+COALESCE(sum(B.money_hpngaygiuong),0) as money_hpngaygiuong, 
	COALESCE(sum(A.money_chiphikhac),0)+COALESCE(sum(B.money_chiphikhac),0) as money_chiphikhac,
	sum(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, 
	sum(A.money_dkpttt_vattu) as money_dkpttt_vattu, 
	sum(B.money_mau) as gmht_money_mau,
	sum(B.money_pttt) as gmht_money_pttt, 
	sum(B.money_ptttyeucau) as gmht_money_ptttyeucau,	
	sum(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, 
	sum(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu, 
	sum(B.money_vattu_ttrieng) as gmht_money_vattu_ttrieng,
	sum(B.money_vtthaythe) as gmht_money_vtthaythe, 
	sum(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls,	
	sum(A.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_thuoc, 
	COALESCE(sum(A.money_hpdkpttt_gm_vattu),0) as money_hpdkpttt_vattu, 
	COALESCE(sum(A.money_hppttt),0) as money_hppttt, 	
	sum(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, 
	COALESCE(sum(B.money_hpdkpttt_gm_vattu),0) as gmht_money_hpdkpttt_vattu, 
	COALESCE(sum(B.money_hppttt),0) as gmht_money_hppttt,
	COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) as tong_tien_bh, 
	COALESCE(sum(A.money_tong_vp),0) + COALESCE(sum(B.money_tong_vp),0) as tong_tien_vp, 
	COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) + COALESCE(sum(A.money_tong_vp),0) + COALESCE(sum(B.money_tong_vp),0) as tong_tien 
FROM departmentgroup dep 
	LEFT JOIN 
	(SELECT spt.departmentgroupid, 
		count(spt.*) as soluot, 
		sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
		sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
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
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
		sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
		sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu,	
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_chiphikhac) as money_chiphikhac, 
		sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, 
		sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, 
		sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, 
		sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp 
	FROM ihs_servicespttt spt 
	WHERE spt.departmentid not in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+"
	GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 

	LEFT JOIN 
	(SELECT spt.departmentgroup_huong, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp) as money_pttt,
		sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cls, 
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau,
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi,
		sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan,
		sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc,
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 		
		sum(spt.money_chiphikhac) as money_chiphikhac, 
		sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, 
		sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, 
		sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
		sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu,	
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, 
		sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp 
	FROM ihs_servicespttt spt 
	WHERE spt.departmentid in (34,335,269,285) " + doituongbenhnhanid_spt + " and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+" GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong
	LEFT JOIN 
	(SELECT count(*) as count, 
		sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, 
		sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, 
		vp.departmentgroupid 
	FROM vienphi vp 
	WHERE vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
	GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid 
WHERE dep.departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100) 
GROUP BY dep.departmentgroupid, dep.departmentgroupname) O 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
		b.departmentgroupid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid 
	where vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " 
	group by b.departmentgroupid) BILL ON BILL.departmentgroupid=O.departmentgroupid

	LEFT JOIN	
		(SELECT XN.departmentgroupid,
			sum(XN.chiphixn) as chiphixn
		FROM 
		(SELECT 
					SERV.departmentgroupid,
					sum(SERV.soluong*chiphi.chiphixn) as chiphixn
				FROM
					(select  
						ser.servicepriceid,
						ser.servicepricecode, 
						ser.soluong,		
						se.idmayxn,
						ser.departmentgroupid
					from (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp
						inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid
						left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
					group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV
				LEFT JOIN (SELECT *
							FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
							AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn
				GROUP BY SERV.departmentgroupid
		UNION ALL
		SELECT 
					SERV.departmentgroupid,
					sum(SERV.soluong*chiphi.chiphixn) as chiphixn
				FROM
				(select  
						ser.servicepriceid,
						ser.servicepricecode, 
						ser.soluong,		
						ser.departmentgroupid
					from (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp
						inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid,maubenhphamid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid
						inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 and departmentid_des=253 and maubenhphamdate>'2018-01-01 00:00:00') mbp on mbp.maubenhphamid=ser.maubenhphamid
					group by ser.servicepriceid,ser.servicepricecode,ser.soluong,ser.departmentgroupid) SERV
				LEFT JOIN (SELECT *
							FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
							AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode
				GROUP BY SERV.departmentgroupid) XN
		GROUP BY XN.departmentgroupid) CP_XN on CP_XN.departmentgroupid=O.departmentgroupid

	LEFT JOIN
		(SELECT A.departmentgroupid,
				sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha
		FROM 
			(SELECT ser.departmentgroupid,vp.vienphiid,ser.servicepriceid,
				(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle,
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as thuoc_tronggoi, 
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as vattu_tronggoi, 
				(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as chiphikhac,
				((case serf.pttt_loaiid 
								when 1 then 1201000 
								when 2 then 572000 
								when 3 then 243000 
								when 4 then 149000 
								when 5 then 185000 
								when 6 then 70500 
								when 7 then 35500 
								when 8 then 27500 
								else 0 end) * ser.soluong) as chiphibs
			FROM (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 " + doituongbenhnhanid_vp + " and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp		
				inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN')) ser on ser.vienphiid=vp.vienphiid
				inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A
		GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=O.departmentgroupid

LEFT JOIN 
	(select departmentgroupid,sum(medicinestorebilltotalmoney) as chiphikhoa
		from medicine_store_bill
		where medicinestorebilltype=202 and medicinestorebillstatus=2 and isremove=0 and bill_mode=4
		and ngaysudungthuoc between '" + thoiGianTu + "' and '" + thoiGianDen + "' 
		group by departmentgroupid
		) CP_KHOA on CP_KHOA.departmentgroupid=O.departmentgroupid;





--1.5=====================================II. Cho doi tuong Khac (trừ DTBHYT+dvBHYT)
--else if (cboTrangThai.Text == "Ra viện chưa thanh toán") 
--ngay 1/7/2018
 
SELECT ROW_NUMBER () OVER (ORDER BY dep.departmentgroupname) as stt, 
	dep.departmentgroupid, 
	dep.departmentgroupname, 
	(A.soluot) as soluot, 
	(A.soluot_bh) as soluot_bh, 
	(A.soluot_vp) as soluot_vp,
	(C.count_bh) as soluong_bh, 
	(C.count_vp) as soluong_vp, 
	(C.count) as soluong, 
	(A.money_khambenh) as money_khambenh, 
	(A.money_xetnghiem) as money_xetnghiem, 
	(A.money_cdhatdcn) as money_cdhatdcn, 
	(A.money_pttt) as money_pttt, 
	(A.money_ptttyeucau) as money_ptttyeucau,
	(A.money_dvktc) as money_dvktc, 
	(A.money_giuongthuong) as money_giuongthuong, 
	(A.money_giuongyeucau) as money_giuongyeucau,
	(A.money_nuocsoi) as money_nuocsoi,
	(A.money_xuatan) as money_xuatan,
	(A.money_diennuoc) as money_diennuoc,
	(A.money_mau) as money_mau, 
	(A.money_thuoc) as money_thuoc, 
	(A.money_vattu) as money_vattu, 
	(A.money_vattu_ttrieng) as money_vattu_ttrieng,
	(A.money_vtthaythe) as money_vtthaythe, 
	(A.money_phuthu) as money_phuthu, 
	(A.money_vanchuyen) as money_vanchuyen, 
	(A.money_khac) as money_khac, 
	COALESCE((A.money_hpngaygiuong),0)+COALESCE((B.money_hpngaygiuong),0) as money_hpngaygiuong, 
	COALESCE((A.money_chiphikhac),0)+COALESCE((B.money_chiphikhac),0) as money_chiphikhac,
	(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, 
	(A.money_dkpttt_vattu) as money_dkpttt_vattu, 
	(B.money_mau) as gmht_money_mau,
	(B.money_pttt) as gmht_money_pttt,
	(B.money_ptttyeucau) as gmht_money_ptttyeucau,	
	(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, 
	(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu, 
	(B.money_vattu_ttrieng) as gmht_money_vattu_ttrieng,
	(B.money_vtthaythe) as gmht_money_vtthaythe, 
	(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls,	
	(A.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_thuoc, 
	COALESCE((A.money_hpdkpttt_gm_vattu),0) as money_hpdkpttt_vattu, 
	COALESCE((A.money_hppttt),0) as money_hppttt, 	
	(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, 
	COALESCE((B.money_hpdkpttt_gm_vattu),0) as gmht_money_hpdkpttt_vattu, 
	COALESCE((B.money_hppttt),0) as gmht_money_hppttt,
	COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) as tong_tien_bh, 
	COALESCE((A.money_tong_vp),0) + COALESCE((B.money_tong_vp),0) as tong_tien_vp, 
	COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) + COALESCE((A.money_tong_vp),0) + COALESCE((B.money_tong_vp),0) as tong_tien,
	bill.tam_ung,
	CP_XN.chiphixn,
	CP_CDHA.chiphicdha
FROM (select departmentgroupid,departmentgroupname from departmentgroup where departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100)) dep
	LEFT JOIN 
	(SELECT spt.departmentgroupid, 
		count(spt.*) as soluot, 
		sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
		sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
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
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
		sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
		sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu,	
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_chiphikhac) as money_chiphikhac, 
		sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, 
		sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, 
		sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, 
		sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp 
	FROM ihs_servicespttt spt 
	WHERE spt.departmentid not in (34,335,269,285) " + doituongbenhnhanid_spt + " 
		and spt.vienphistatus=1 and COALESCE(spt.vienphistatus_vp,0)=0
		and spt.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+"
	GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 

	LEFT JOIN 
	(SELECT spt.departmentgroup_huong, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp) as money_pttt, 
		sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cls, 
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau,
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi,
		sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan,
		sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc,
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 		
		sum(spt.money_chiphikhac) as money_chiphikhac, 
		sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, 
		sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, 
		sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
		sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu,	
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, 
		sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp 
	FROM ihs_servicespttt spt 
	WHERE spt.departmentid in (34,335,269,285) " + doituongbenhnhanid_spt + "
		and spt.vienphistatus=1 and COALESCE(spt.vienphistatus_vp,0)=0
		and spt.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "' "+_thutienstatus+"
	GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong
	LEFT JOIN 
	(SELECT count(*) as count, 
		sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, 
		sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, 
		vp.departmentgroupid 
	FROM vienphi vp 
	WHERE vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0
		" + doituongbenhnhanid_vp + "
		and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "'
	GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
		b.departmentgroupid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid 
	where 
		vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0
		and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "'
		and b.loaiphieuthuid=2 and b.dahuyphieu=0 " + doituongbenhnhanid_vp + " 
	group by b.departmentgroupid) BILL ON BILL.departmentgroupid=dep.departmentgroupid
LEFT JOIN	
		(SELECT 
			SERV.departmentgroupid,
			sum(SERV.soluong*chiphi.chiphixn) as chiphixn
		FROM
			(select  
				ser.servicepriceid,
				ser.servicepricecode, 
				ser.soluong,		
				se.idmayxn,
				ser.departmentgroupid
			from (select vp.vienphiid from vienphi vp where vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0
		" + doituongbenhnhanid_vp + "
		and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp
				inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid
				left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
			group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV
		LEFT JOIN (SELECT *
					FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
					AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn
		GROUP BY SERV.departmentgroupid) CP_XN on CP_XN.departmentgroupid=dep.departmentgroupid

	LEFT JOIN
		(SELECT A.departmentgroupid,
				sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha
		FROM 
			(SELECT ser.departmentgroupid,vp.vienphiid,ser.servicepriceid,
				(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle,
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as thuoc_tronggoi, 
				(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as vattu_tronggoi, 
				(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as chiphikhac,
				((case serf.pttt_loaiid 
								when 1 then 1201000 
								when 2 then 572000 
								when 3 then 243000 
								when 4 then 149000 
								when 5 then 185000 
								when 6 then 70500 
								when 7 then 35500 
								when 8 then 27500 
								else 0 end) * ser.soluong) as chiphibs
			FROM (select vp.vienphiid from vienphi vp where vp.vienphistatus=1 and COALESCE(vp.vienphistatus_vp,0)=0 " + doituongbenhnhanid_vp + " and vp.vienphidate_ravien between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp		
				inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN')) ser on ser.vienphiid=vp.vienphiid
				inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A
		GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=dep.departmentgroupid;
	




 
 
 
 
 
 
--1.6=====================================II. Cho doi tuong Khac (trừ DTBHYT+dvBHYT)
-- else if (cboTieuChi.Text == "Chưa ra viện")
--ngay 1/7/2018


SELECT ROW_NUMBER () OVER (ORDER BY dep.departmentgroupname) as stt, 
	dep.departmentgroupid, 
	dep.departmentgroupname, 
	(A.soluot) as soluot, 
	(A.soluot_bh) as soluot_bh, 
	(A.soluot_vp) as soluot_vp,
	(C.count_bh) as soluong_bh, 
	(C.count_vp) as soluong_vp, 
	(C.count) as soluong, 
	(A.money_khambenh) as money_khambenh, 
	(A.money_xetnghiem) as money_xetnghiem, 
	(A.money_cdhatdcn) as money_cdhatdcn, 
	(A.money_pttt) as money_pttt, 
	(A.money_ptttyeucau) as money_ptttyeucau,
	(A.money_dvktc) as money_dvktc, 
	(A.money_giuongthuong) as money_giuongthuong, 
	(A.money_giuongyeucau) as money_giuongyeucau,
	(A.money_nuocsoi) as money_nuocsoi,
	(A.money_xuatan) as money_xuatan,
	(A.money_diennuoc) as money_diennuoc,
	(A.money_mau) as money_mau, 
	(A.money_thuoc) as money_thuoc, 
	(A.money_vattu) as money_vattu, 
	(A.money_vattu_ttrieng) as money_vattu_ttrieng,
	(A.money_vtthaythe) as money_vtthaythe, 
	(A.money_phuthu) as money_phuthu, 
	(A.money_vanchuyen) as money_vanchuyen, 
	(A.money_khac) as money_khac, 
	COALESCE((A.money_hpngaygiuong),0)+COALESCE((B.money_hpngaygiuong),0) as money_hpngaygiuong, 
	COALESCE((A.money_chiphikhac),0)+COALESCE((B.money_chiphikhac),0) as money_chiphikhac,
	(A.money_dkpttt_thuoc) as money_dkpttt_thuoc, 
	(A.money_dkpttt_vattu) as money_dkpttt_vattu, 
	(B.money_mau) as gmht_money_mau,
	(B.money_pttt) as gmht_money_pttt,
	(B.money_ptttyeucau) as gmht_money_ptttyeucau,
	(B.money_dkpttt_thuoc + B.money_thuoc) as gmht_money_dkpttt_thuoc, 
	(B.money_dkpttt_vattu + B.money_vattu) as gmht_money_dkpttt_vattu,
	(B.money_vattu_ttrieng) as gmht_money_vattu_ttrieng,	
	(B.money_vtthaythe) as gmht_money_vtthaythe, 
	(B.money_cls + B.money_giuongthuong + B.money_giuongyeucau + B.money_nuocsoi + B.money_xuatan + B.money_diennuoc + B.money_khambenh + B.money_phuthu + B.money_vanchuyen + B.money_khac) as gmht_money_cls,	
	(A.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_thuoc, 
	COALESCE((A.money_hpdkpttt_gm_vattu),0) as money_hpdkpttt_vattu, 
	COALESCE((A.money_hppttt),0) as money_hppttt, 	
	(B.money_hpdkpttt_gm_thuoc) as gmht_money_hpdkpttt_thuoc, 
	COALESCE((B.money_hpdkpttt_gm_vattu),0) as gmht_money_hpdkpttt_vattu, 
	COALESCE((B.money_hppttt),0) as gmht_money_hppttt,
	COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) as tong_tien_bh, 
	COALESCE((A.money_tong_vp),0) + COALESCE((B.money_tong_vp),0) as tong_tien_vp, 
	COALESCE((A.money_tong_bh),0) + COALESCE((B.money_tong_bh),0) + COALESCE((A.money_tong_vp),0) + COALESCE((B.money_tong_vp),0) as tong_tien,
	bill.tam_ung,
	CP_XN.chiphixn
FROM (select departmentgroupid,departmentgroupname from departmentgroup where departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100)) dep 
	LEFT JOIN 
	(SELECT spt.departmentgroupid, 
		count(spt.*) as soluot, --sai
		sum(case when spt.doituongbenhnhanid=1 then 1 else 0 end) as soluot_bh,
		sum(case when spt.doituongbenhnhanid>1 then 1 else 0 end) as soluot_vp,
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
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
		sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
		sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu,	
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_chiphikhac) as money_chiphikhac, 
		sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, 
		sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, 
		sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, 
		sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp 
	FROM View_tools_serviceprice_pttt spt 
	WHERE spt.departmentid not in (34,335,269,285) and spt.vienphistatus=0
		and spt.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "'	
		" + doituongbenhnhanid_spt + "
	GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 

	LEFT JOIN 
	(SELECT spt.departmentgroup_huong, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp) as money_pttt, 
		sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as money_ptttyeucau,
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as money_vattu_ttrieng,
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cls, 
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau,
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp) as money_nuocsoi,
		sum(spt.money_xuatan_bh + spt.money_xuatan_vp) as money_xuatan,
		sum(spt.money_diennuoc_bh + spt.money_diennuoc_vp) as money_diennuoc,
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 		
		sum(spt.money_chiphikhac) as money_chiphikhac, 
		sum(spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp) as money_dkpttt_thuoc, 
		sum(spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as money_dkpttt_vattu, 
		sum(spt.money_hppttt_goi_thuoc) as money_hpdkpttt_gm_thuoc, 
		sum(spt.money_hppttt_goi_vattu) as money_hpdkpttt_gm_vattu,	
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_pttt_bh + spt.money_ptttyeucau_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vattu_ttrieng_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bh, 
		sum(spt.money_pttt_vp + spt.money_ptttyeucau_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vattu_ttrieng_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_vp 
	FROM View_tools_serviceprice_pttt spt 
	WHERE spt.departmentid in (34,335,269,285) and spt.vienphistatus=0
		and spt.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "'
		" + doituongbenhnhanid_spt + "
	GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong
	LEFT JOIN 
	(SELECT count(*) as count, 
		sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh, 
		sum(case when doituongbenhnhanid<>1 then 1 else 0 end) as count_vp, 
		vp.departmentgroupid 
	FROM vienphi vp 
	WHERE vp.vienphistatus=0 " + doituongbenhnhanid_vp + " 
	and vp.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "'	
	GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid 

	LEFT JOIN 
		(select sum(b.datra) as tam_ung, 
			b.departmentgroupid 
		from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid 
		where vp.vienphistatus=0 and b.loaiphieuthuid=2 and b.dahuyphieu=0 
		and vp.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "'
		" + doituongbenhnhanid_vp + " 
		group by b.departmentgroupid) BILL ON BILL.departmentgroupid=dep.departmentgroupid
	LEFT JOIN	
			(SELECT 
				SERV.departmentgroupid,
				sum(SERV.soluong*chiphi.chiphixn) as chiphixn
			FROM
				(select  
					ser.servicepriceid,
					ser.servicepricecode, 
					ser.soluong,		
					se.idmayxn,
					ser.departmentgroupid
				from (select vp.vienphiid from vienphi vp where vp.vienphistatus=0 " + doituongbenhnhanid_vp + " 
		and vp.vienphidate between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp
					inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid
					left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
				group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV
			LEFT JOIN (SELECT *
						FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
						AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn
			GROUP BY SERV.departmentgroupid) CP_XN on CP_XN.departmentgroupid=dep.departmentgroupid;	









	
	