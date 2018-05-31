
----BC Quan ly tong the khoa - DOANH THU KHOA + GÂY MÊ
-- string sqlDoanhThu
--ngay 29/5/2018: them hao phi XN, CDHA, khoa phong
SELECT sum(A.doanhthu_slbn) as doanhthu_slbn, 
	sum(B.doanhthuGM_slbn) as doanhthuGM_slbn, 
	(select count(sl.*)
		from (select vienphiid from ihs_servicespttt spt where spt.vienphistatus_vp=1 
					and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + _thutienstatus+" 
					and (spt.departmentid in ("+this.lstPhongChonLayBC+") or (spt.departmentid in (34,335,269,285) 
					and spt.departmentgroup_huong in ("+this.lstKhoaChonLayBC+")))
			group by spt.vienphiid) sl) as doanhthuTongGM_slbn,
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0)) as numeric),0) as doanhthu_tienkb, 
	round(cast(sum(COALESCE(A.doanhthu_tienxn,0)) as numeric),0) as doanhthu_tienxn, 
	round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthu_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.doanhthu_tienpttt,0)) as numeric),0) as doanhthu_tienpttt,
	round(cast(sum(COALESCE(A.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthu_tienptttyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0)) as numeric),0) as doanhthu_tiendvktc, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthu_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthu_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tienkhac,0)) as numeric),0) as doanhthu_tienkhac, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu,0)) as numeric),0) as doanhthu_tienvattu, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthu_tienvattu_ttrieng,
	round(cast(sum(COALESCE(A.doanhthu_tienmau,0)) as numeric),0) as doanhthu_tienmau, 
	round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tienthuoc, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tongtien,
	round(cast(sum(COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuGM_tienkb, 
	round(cast(sum(COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuGM_tienxn, 
	round(cast(sum(COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuGM_tiencdhatdcn, 
	round(cast(sum(COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuGM_tienpttt, 
	round(cast(sum(COALESCE(B.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthuGM_tienptttyeucau,
	round(cast(sum(COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuGM_tiendvktc, 
	round(cast(sum(COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuGM_tiengiuongthuong, 
	round(cast(sum(COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuGM_tiengiuongyeucau, 
	round(cast(sum(COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuGM_tienkhac, 
	round(cast(sum(COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuGM_tienvattu, 
	round(cast(sum(COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuGM_tienvattu_ttrieng,
	round(cast(sum(COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuGM_tienmau, 
	round(cast(sum(COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuGM_tienthuoc, 
	round(cast(sum(COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc ,0)) as numeric),0) as doanhthuGM_tongtien,
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuTongGM_tienkb, 
	round(cast(sum(COALESCE(A.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuTongGM_tienxn, 
	round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuTongGM_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuTongGM_tienpttt,
	round(cast(sum(COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthuTongGM_tienptttyeucau,
	round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuTongGM_tiendvktc, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuTongGM_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuTongGM_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuTongGM_tienkhac, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuTongGM_tienvattu, 
	round(cast(sum(COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuTongGM_tienvattu_ttrieng,
	round(cast(sum(COALESCE(A.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuTongGM_tienmau, 
	round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuTongGM_tienthuoc, 
	round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc,0) + COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuTongGM_tongtien,
	round(cast(sum(COALESCE(CP_XN.chiphixn,0)) as numeric),0) as doanhthu_chiphixn, 
	round(cast(sum(COALESCE(CP_CDHA.chiphicdha,0)) as numeric),0) as doanhthu_chiphicdha, 
	round(cast(sum(COALESCE(CP_KHOA.chiphikhoa,0)) as numeric),0) as doanhthu_chiphikhoa
FROM 
	(select 
			spt1.departmentgroupid,
			count(spt1.*) as doanhthu_slbn,
			sum(spt1.doanhthu_tienkb) as doanhthu_tienkb, 
			sum(spt1.doanhthu_tienxn) as doanhthu_tienxn, 
			sum(spt1.doanhthu_tiencdhatdcn) as doanhthu_tiencdhatdcn,
			sum(spt1.doanhthu_tienpttt) as doanhthu_tienpttt,
			sum(spt1.doanhthu_tienptttyeucau) as doanhthu_tienptttyeucau, 
			sum(spt1.doanhthu_tiendvktc) as doanhthu_tiendvktc, 
			sum(spt1.doanhthu_tiengiuongthuong) as doanhthu_tiengiuongthuong, 
			sum(spt1.doanhthu_tiengiuongyeucau) as doanhthu_tiengiuongyeucau, 
			sum(spt1.doanhthu_tienkhac) as doanhthu_tienkhac, 
			sum(spt1.doanhthu_tienvattu) as doanhthu_tienvattu, 
			sum(spt1.doanhthu_tienvattu_ttrieng) as doanhthu_tienvattu_ttrieng,
			sum(spt1.doanhthu_tienmau) as doanhthu_tienmau, 
			sum(spt1.doanhthu_tienthuoc) as doanhthu_tienthuoc 
	from (select 
				spt.departmentgroupid,spt.vienphiid,
				sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, 
				sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, 
				sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, 
				sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt,
				sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as doanhthu_tienptttyeucau, 
				sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, 
				sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, 
				sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, 
				sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, 
				sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, 
				sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as doanhthu_tienvattu_ttrieng,
				sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, 
				sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc 
			from ihs_servicespttt spt 
			where spt.vienphistatus_vp=1 
				and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstdepartmentid_tt + _thutienstatus+" 
			group by spt.departmentgroupid,spt.vienphiid) spt1
	group by spt1.departmentgroupid) A 
LEFT JOIN 	
	(select spt1.departmentgroup_huong,
			count(spt1.*) as doanhthuGM_slbn,
			sum(spt1.doanhthu_tienkb) as doanhthu_tienkb, 
			sum(spt1.doanhthu_tienxn) as doanhthu_tienxn, 
			sum(spt1.doanhthu_tiencdhatdcn) as doanhthu_tiencdhatdcn, 
			sum(spt1.doanhthu_tienpttt) as doanhthu_tienpttt, 
			sum(spt1.doanhthu_tienptttyeucau) as doanhthu_tienptttyeucau,
			sum(spt1.doanhthu_tiendvktc) as doanhthu_tiendvktc, 
			sum(spt1.doanhthu_tiengiuongthuong) as doanhthu_tiengiuongthuong, 
			sum(spt1.doanhthu_tiengiuongyeucau) as doanhthu_tiengiuongyeucau, 
			sum(spt1.doanhthu_tienkhac) as doanhthu_tienkhac, 
			sum(spt1.doanhthu_tienvattu) as doanhthu_tienvattu, 
			sum(spt1.doanhthu_tienvattu_ttrieng) as doanhthu_tienvattu_ttrieng,
			sum(spt1.doanhthu_tienmau) as doanhthu_tienmau, 
			sum(spt1.doanhthu_tienthuoc) as doanhthu_tienthuoc
	from (select 
					spt.departmentgroup_huong,
					1 as doanhthuGM_slbn,
					sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, 
					sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, 
					sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, 
					sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt, 
					sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as doanhthu_tienptttyeucau,
					sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, 
					sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, 
					sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, 
					sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, 
					sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, 
					sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as doanhthu_tienvattu_ttrieng,
					sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, 
					sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc 
			from ihs_servicespttt spt 
			where spt.vienphistatus_vp=1 
				and spt.departmentid in (34,335,269,285)
				and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' "+_thutienstatus+"
			group by spt.departmentgroup_huong,spt.vienphiid) spt1	
	group by spt1.departmentgroup_huong) B ON A.departmentgroupid=B.departmentgroup_huong
--chi phi Xet nghiem
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
					from (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "') vp
						inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN' and departmentid in (" + this.lstPhongChonLayBC + ")) ser on ser.vienphiid=vp.vienphiid
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
					from (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp
						inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid,maubenhphamid from serviceprice where bhyt_groupcode='03XN' and departmentid in (" + this.lstPhongChonLayBC + ")) ser on ser.vienphiid=vp.vienphiid
						inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 and departmentid_des=253 and maubenhphamdate>'2018-01-01 00:00:00' and departmentid in (" + this.lstPhongChonLayBC + ")) mbp on mbp.maubenhphamid=ser.maubenhphamid
					group by ser.servicepriceid,ser.servicepricecode,ser.soluong,ser.departmentgroupid) SERV
				LEFT JOIN (SELECT *
							FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
							AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode
				GROUP BY SERV.departmentgroupid) XN
		GROUP BY XN.departmentgroupid) CP_XN on CP_XN.departmentgroupid=A.departmentgroupid
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
			FROM (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp		
				inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN') and departmentid in (" + this.lstPhongChonLayBC + ")) ser on ser.vienphiid=vp.vienphiid
				inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A
		GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=A.departmentgroupid
--Chi phi khoa/phong
LEFT JOIN 
	(select departmentgroupid,sum(medicinestorebilltotalmoney) as chiphikhoa
		from medicine_store_bill
		where medicinestorebilltype=202 and medicinestorebillstatus=2 and isremove=0 and bill_mode=4
		and ngaysudungthuoc between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")
		group by departmentgroupid
		) CP_KHOA on CP_KHOA.departmentgroupid=A.departmentgroupid;
	
	
	
	
	
	
-------RA VIEN DA THANH TOAN ngay 25/5/2018
--string sqlBaoCao_RaVienDaTT =
--ngay 30/5/2018: them hao phi XN, CDHA, khoa


SELECT 
	(select count(vp.*) from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) as raviendatt_slbn,
	round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0) as raviendatt_tienkb, 
	round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0) as raviendatt_tienxn, 
	round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0) as raviendatt_tiencdhatdcn, 
	round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0) as raviendatt_tienpttt,
	round(cast(sum(COALESCE(A.raviendatt_tienptttyeucau,0)) as numeric),0) as raviendatt_tienptttyeucau, 
	round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0) as raviendatt_tiendvktc, 
	round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0) as raviendatt_tiengiuongthuong, 
	round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0) as raviendatt_tiengiuongyeucau, 
	round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0) as raviendatt_tienkhac, 
	round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0) as raviendatt_tienvattu, 
	round(cast(sum(COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0) as raviendatt_tienvattu_ttrieng, 
	round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0) as raviendatt_tienmau, 
	round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0) as raviendatt_tienthuoc, 
	round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0) + COALESCE(A.raviendatt_tienptttyeucau,0) + COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0) as raviendatt_tongtien,
	round(cast(sum(COALESCE(CP_XN.chiphixn,0)) as numeric),0) as raviendatt_chiphixn, 
	round(cast(sum(COALESCE(CP_CDHA.chiphicdha,0)) as numeric),0) as raviendatt_chiphicdha, 
	round(cast(sum(COALESCE(CP_KHOA.chiphikhoa,0)) as numeric),0) as raviendatt_chiphikhoa
FROM 	
	(select 
			spt.khoaravien,
			sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as raviendatt_tienkb, 
			sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as raviendatt_tienxn, 
			sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as raviendatt_tiencdhatdcn, 
			sum(spt.money_pttt_bh + spt.money_pttt_vp) as raviendatt_tienpttt, 
			sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as raviendatt_tienptttyeucau, 
			sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as raviendatt_tiendvktc, 
			sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as raviendatt_tiengiuongthuong, 
			sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as raviendatt_tiengiuongyeucau, 
			sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as raviendatt_tienkhac, 
			sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as raviendatt_tienvattu, 
			sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as raviendatt_tienvattu_ttrieng, 
			sum(spt.money_mau_bh + spt.money_mau_vp) as raviendatt_tienmau, 
			sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as raviendatt_tienthuoc 
	from ihs_servicespttt spt 
	where spt.vienphistatus_vp=1 	
		and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstphongravien + _thutienstatus+" 
	group by spt.khoaravien) A
--chi phi Xet nghiem
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
						vp.departmentgroupid
					from (select vp.vienphiid,vp.departmentgroupid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) vp
						inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid
						left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid
					group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,vp.departmentgroupid) SERV
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
						vp.departmentgroupid
					from (select vp.vienphiid,vp.departmentgroupid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) vp
						inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid,maubenhphamid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid
						inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 and departmentid_des=253 and maubenhphamdate>'2018-01-01 00:00:00') mbp on mbp.maubenhphamid=ser.maubenhphamid
					group by ser.servicepriceid,ser.servicepricecode,ser.soluong,vp.departmentgroupid) SERV
				LEFT JOIN (SELECT *
							FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp')
							AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode
				GROUP BY SERV.departmentgroupid) XN
		GROUP BY XN.departmentgroupid) CP_XN on CP_XN.departmentgroupid=A.khoaravien
--Chi phi CDHA 
	LEFT JOIN
		(SELECT A.departmentgroupid,
				sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha
		FROM 
			(SELECT vp.departmentgroupid,vp.vienphiid,ser.servicepriceid,
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
			FROM (select vp.vienphiid,vp.departmentgroupid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) vp		
				inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN')) ser on ser.vienphiid=vp.vienphiid
				inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A
		GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=A.khoaravien
--Chi phi khoa/phong
LEFT JOIN 
	(select departmentgroupid,sum(medicinestorebilltotalmoney) as chiphikhoa
		from medicine_store_bill
		where medicinestorebilltype=202 and medicinestorebillstatus=2 and isremove=0 and bill_mode=4
		and ngaysudungthuoc between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")
	group by departmentgroupid
		) CP_KHOA on CP_KHOA.departmentgroupid=A.khoaravien;








---====== BN ra vien nhung chua thanh toan 
--string sqlBaoCao_RaVienChuaTT 
--ngay 25/5/2018

SELECT count(A.*) as ravienchuatt_slbn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0)) as numeric),0),0) as ravienchuatt_tienkb,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienxn,0)) as numeric),0),0) as ravienchuatt_tienxn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiencdhatdcn,0)) as numeric),0),0) as ravienchuatt_tiencdhatdcn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienpttt,0)) as numeric),0),0) as ravienchuatt_tienpttt,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienptttyeucau,0)) as numeric),0),0) as ravienchuatt_tienptttyeucau,
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiendvktc,0)) as numeric),0),0) as ravienchuatt_tiendvktc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongthuong,0)) as numeric),0),0) as ravienchuatt_tiengiuongthuong,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongyeucau,0)) as numeric),0),0) as ravienchuatt_tiengiuongyeucau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkhac,0)) as numeric),0),0) as ravienchuatt_tienkhac,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu,0)) as numeric),0),0) as ravienchuatt_tienvattu, 
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu_ttrieng,0)) as numeric),0),0) as ravienchuatt_tienvattu_ttrieng, 
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienmau,0)) as numeric),0),0) as ravienchuatt_tienmau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienthuoc,0)) as numeric),0),0) as ravienchuatt_tienthuoc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0) + COALESCE(A.ravienchuatt_tienxn,0) + COALESCE(A.ravienchuatt_tiencdhatdcn,0) + COALESCE(A.ravienchuatt_tienpttt,0) + COALESCE(A.ravienchuatt_tiendvktc,0) + COALESCE(A.ravienchuatt_tiengiuongthuong,0) + COALESCE(A.ravienchuatt_tiengiuongyeucau,0) + COALESCE(A.ravienchuatt_tienkhac,0) + COALESCE(A.ravienchuatt_tienvattu,0) + COALESCE(A.ravienchuatt_tienmau,0) + COALESCE(A.ravienchuatt_tienthuoc,0) + COALESCE(A.ravienchuatt_tienvattu_ttrieng,0) + COALESCE(A.ravienchuatt_tienptttyeucau,0)) as numeric),0),0) as ravienchuatt_tongtien
FROM (select spt.vienphiid,  
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as ravienchuatt_tienkb,  
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as ravienchuatt_tienxn,  
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as ravienchuatt_tiencdhatdcn,  
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as ravienchuatt_tienpttt,  
		sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as ravienchuatt_tienptttyeucau, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as ravienchuatt_tiendvktc,  
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as ravienchuatt_tiengiuongthuong,  
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as ravienchuatt_tiengiuongyeucau,  
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as ravienchuatt_tienkhac,  
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as ravienchuatt_tienvattu,  
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as ravienchuatt_tienvattu_ttrieng,  
		sum(spt.money_mau_bh + spt.money_mau_vp) as ravienchuatt_tienmau,  
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as ravienchuatt_tienthuoc 
		from ihs_servicespttt spt 
		where COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphistatus<>0 and spt.vienphidate_ravien between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstphongravien + _thutienstatus+" group by spt.vienphiid) A ;
		-->='" + this.KhoangThoiGianLayDuLieu + "' 


---====== BN dang dieu tri
--string sqlBaoCao_DangDT 
--ngay 25/3/2018


SELECT count(*) as dangdt_slbn, 
sum(vpm.money_khambenh) as dangdt_tienkb, 
sum(vpm.money_xetnghiem) as dangdt_tienxn, 
sum(coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0)) as dangdt_tiencdhatdcn, 
sum(vpm.money_pttt) as dangdt_tienpttt, 
sum(vpm.money_ptttyeucau) as dangdt_tienptttyeucau,
sum(vpm.money_dvktc) as dangdt_tiendvktc, 
sum(vpm.money_giuongthuong) as dangdt_tiengiuongthuong, 
sum(vpm.money_giuongyeucau) as dangdt_tiengiuongyeucau, 
sum(coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0)) as dangdt_tienkhac,  
sum(vpm.money_vattu) as dangdt_tienvattu, 
sum(vpm.money_vattu_ttrieng) as dangdt_tienvattu_ttrieng, 
sum(vpm.money_mau) as dangdt_tienmau, 
sum(vpm.money_thuoc) as dangdt_tienthuoc, 
sum(coalesce(vpm.money_khambenh,0) + coalesce(vpm.money_xetnghiem,0) + coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0) + coalesce(vpm.money_pttt,0) + coalesce(vpm.money_dvktc,0) + coalesce(vpm.money_giuongthuong,0) + coalesce(vpm.money_giuongyeucau,0) + coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0) + coalesce(vpm.money_vattu,0) + coalesce(vpm.money_mau,0) + coalesce(vpm.money_thuoc,0) + coalesce(vpm.money_ptttyeucau,0) + coalesce(vpm.money_vattu_ttrieng,0)) as dangdt_tongtien,
sum(vpm.tam_ung) as dangdt_tamung 
FROM
(
SELECT mrd.vienphiid, 
mrd.patientid, 
mrd.hosobenhanid, 
mrd.loaibenhanid,  
mrd.departmentgroupid, 
mrd.departmentid, 
mrd.doituongbenhnhanid, 
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong=0
					then (case when mrd.loaibenhanid=24 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
									then ser.servicepricemoney_bhyt*ser.soluong
								when mrd.loaibenhanid=1 
									then ser.servicepricemoney_bhyt*ser.soluong
								else 0 end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
							   else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
							   else ser.servicepricemoney*ser.soluong end)			   
				else 0 end) as money_khambenh,		
		sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong=0
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney*ser.soluong end)	
				else 0 end) as money_xetnghiem,
		sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then ser.servicepricemoney_nuocngoai*ser.soluong 
								else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_cdha,	 
		sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)		
				else 0 end) as money_tdcn,	 	 
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 
								then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney 
								else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_pttt,
		sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)	
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 
								then (ser.servicepricemoney_nuocngoai)*ser.soluong 
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney 
								else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC')
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end) as money_ptttyeucau, 
		sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)
				when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 
									then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) 
								else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)	
				else 0 end) as money_mau,
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)	
				else 0 end) as money_giuongthuong,	
		sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)			
				else 0 end) as money_giuongyeucau,	
		sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_vanchuyen, 	 
		sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_khac, 	 
		sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=0 
					then ser.servicepricemoney_bhyt*ser.soluong 
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end)
				when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)				
				else 0 end) as money_phuthu,	 
		(sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) 
				then ser.servicepricemoney_bhyt*ser.soluong else 0 end) 
		+ sum(case when ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=mrd.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
				then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan * ser.soluong) end)
				else 0 end)		
		+ sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
				when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
				else 0 end)) as money_dvktc, 	
		sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)		
				when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)	
				else 0 end) as money_thuoc,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=0 
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end)		
				when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 
					then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)		
				else 0 end) as money_vattu,
		sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0
					then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
				else 0 end) as money_vattu_ttrieng,
(select sum(bill.datra) from bill where bill.vienphiid=mrd.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung		
FROM medicalrecord mrd 
	left join serviceprice ser on mrd.vienphiid=ser.vienphiid and ser.thuockhobanle=0 
WHERE mrd.thoigianvaovien >='" + this.KhoangThoiGianLayDuLieu + "' " + lstdepartmentid_mrd + " and mrd.medicalrecordstatus=2
GROUP BY mrd.vienphiid,mrd.patientid,mrd.hosobenhanid,mrd.loaibenhanid,mrd.departmentgroupid,mrd.departmentid,mrd.doituongbenhnhanid
) VPM;












