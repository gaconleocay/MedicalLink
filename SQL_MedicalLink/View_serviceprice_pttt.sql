---------------View Serviceprice_pttt v 1.13 ngay 05/04/2017
----su dung rieng cho bv VietTiep chia khoa GMHT
--1.10:Fix bug doi tuong VP co tien BHYT
--1.11:tách thuốc,vật tư tính tiền trong gói từ nhóm pttt.
CREATE OR REPLACE VIEW serviceprice_pttt AS 
SELECT 
	vp.vienphiid, 
	vp.patientid, 
	vp.bhytid, 
	vp.hosobenhanid, 
	vp.loaivienphiid, 
	vp.vienphistatus, 
	vp.departmentgroupid as khoaravien, 
	vp.departmentid as phongravien, 
	vp.doituongbenhnhanid, 
	vp.vienphidate, 
	vp.vienphidate_ravien, 
	vp.duyet_ngayduyet, 
	vp.vienphistatus_vp, 
	vp.duyet_ngayduyet_vp, 
	vp.vienphistatus_bh,
	vp.duyet_ngayduyet_bh,
	vp.bhyt_tuyenbenhvien,
	ser.departmentid,
	ser.departmentgroupid,
	(case when ser.departmentid in (34,335,269,285) 
				then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid)
		  else ser.departmentgroupid end) as departmentgroup_huong,		  
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (0,4,6) 
			then (case when vp.loaivienphiid=0 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
							then ser.servicepricemoney_bhyt*ser.soluong
						when vp.loaivienphiid=1 
							then ser.servicepricemoney_bhyt*ser.soluong
						else 0 end)
		else 0 end) as money_khambenh_bh,		
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong 
						else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-ser.servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_khambenh_vp,
sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_xetnghiem_bh,
sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		 when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		 when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		 else 0 end) as money_xetnghiem_vp,
sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_cdha_bh,
sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_cdha_vp,	 
sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_tdcn_bh,
sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_tdcn_vp,	 	 
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_pttt_bh,
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 
							then ser.servicepricemoney_nuocngoai*ser.soluong 
						else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt else 0 end)*ser.soluong
						else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 
							then ser.servicepricemoney_nuocngoai*ser.soluong 
						else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_pttt_vp,	 
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_mau_bh,
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
		when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end) as money_mau_vp, 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongthuong_bh,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_giuongthuong_vp,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongyeucau_bh,	
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_giuongyeucau_vp,		 	 
sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_vanchuyen_bh,
sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_vanchuyen_vp,	 	 
sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_khac_bh,
sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_khac_vp,	 	 
sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_phuthu_bh,
sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_phuthu_vp,	
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		else 0 end) as money_thuoc_bh,
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
		when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end) as money_thuoc_vp,
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_vattu_bh,
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) then 
		(case when ser.doituongbenhnhanid=4 
				then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
			  else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
	when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) 
		then (case when ser.doituongbenhnhanid=4 
					then (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
								else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
					else (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)		
	 when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
	 else 0 end) as money_vattu_vp,
-------================= 
(sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		 else 0 end) 
		 ) as money_vtthaythe_bh, 
(sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)						
		when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end)
+
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2
and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end)		) as money_vtthaythe_vp,
---------=========					
sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_dvktc_bh,
(sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) 
						else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end)
		when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end)
+ 
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end)	) as money_dvktc_vp,			 
sum(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong)  as money_chiphikhac,		 
sum(case when ser.loaidoituong=5 then ser.servicepricemoney * ser.soluong else 0 end) as money_hpngaygiuong,	 
sum(case when ser.loaidoituong=7 then ser.servicepricemoney * ser.soluong else 0 end) as money_hppttt,
sum(case when ser.departmentid in (34,335,269,285) 
		then (case when ser.loaidoituong=2 and ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM') and ser.servicepriceid_master<>0
						then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode in ('07KTC','06PTTT')) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0) 
									then ser.servicepricemoney * ser.soluong else 0 end)			
					else 0 end)
	  else 0 end) as money_hpdkpttt_gm_thuoc,  
sum(case when ser.departmentid in (34,335,269,285) 
		then (case when ser.loaidoituong=2 and ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.servicepriceid_master<>0
						then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode in ('07KTC','06PTTT')) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0) 
									then ser.servicepricemoney * ser.soluong else 0 end)			
					else 0 end)
	  else 0 end) as money_hpdkpttt_gm_vattu,
-----chi phí trong gói có tính tiền
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=2
and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
			else 0 end) as money_dkpttt_thuoc_bh,	
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.loaidoituong in (1,3)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end) as money_dkpttt_thuoc_vp,
			
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2
and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
			else 0 end) as money_dkpttt_vattu_bh,
(sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end)
+
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2
and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (1,3)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
			else 0 end) ) as money_dkpttt_vattu_vp		
FROM vienphi vp left join serviceprice ser on vp.vienphiid=ser.vienphiid
WHERE vp.vienphidate >'2014-01-04 00:00:00' 
	and ser.thuockhobanle=0 
GROUP BY vp.vienphiid,vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, 
vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, 
vp.vienphistatus_bh, vp.duyet_ngayduyet_bh, vp.bhyt_tuyenbenhvien, ser.departmentid, ser.departmentgroupid,
(case when ser.departmentid in (34,335,269,285) then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid)
		  else ser.departmentgroupid end);
--ORDER BY vp.vienphiid DESC;






---------------View Serviceprice_pttt v 1.10 ngay 31/3/2017
--- su dung rieng cho bv VietTiep chia khoa GMHT
--Fix bug doi tuong VP co tien BHYT
CREATE OR REPLACE VIEW serviceprice_pttt AS 
SELECT 
	vp.vienphiid, 
	vp.patientid, 
	vp.bhytid, 
	vp.hosobenhanid, 
	vp.loaivienphiid, 
	vp.vienphistatus, 
	vp.departmentgroupid as khoaravien, 
	vp.departmentid as phongravien, 
	vp.doituongbenhnhanid, 
	vp.vienphidate, 
	vp.vienphidate_ravien, 
	vp.duyet_ngayduyet, 
	vp.vienphistatus_vp, 
	vp.duyet_ngayduyet_vp, 
	vp.vienphistatus_bh,
	vp.duyet_ngayduyet_bh,
	vp.bhyt_tuyenbenhvien,
	ser.departmentid,
	ser.departmentgroupid,
	(case when ser.departmentid in (34,335,269,285) 
				then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid)
		  else ser.departmentgroupid end) as departmentgroup_huong,		  
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (0,4,6) 
			then (case when vp.loaivienphiid=0 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
							then ser.servicepricemoney_bhyt*ser.soluong
						when vp.loaivienphiid=1 
							then ser.servicepricemoney_bhyt*ser.soluong
						else 0 end)
		else 0 end) as money_khambenh_bh,		
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong 
						else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-ser.servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_khambenh_vp,
sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_xetnghiem_bh,
sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		 when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		 when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		 else 0 end) as money_xetnghiem_vp,
sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_cdha_bh,
sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_cdha_vp,	 
sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_tdcn_bh,
sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_tdcn_vp,	 	 
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_pttt_bh,
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 
							then ser.servicepricemoney_nuocngoai*ser.soluong 
						else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt else 0 end)*ser.soluong
						else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 
							then ser.servicepricemoney_nuocngoai*ser.soluong 
						else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_pttt_vp,	 
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_mau_bh,
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
		when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end) as money_mau_vp, 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongthuong_bh,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_giuongthuong_vp,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongyeucau_bh,	
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_giuongyeucau_vp,		 	 
sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_vanchuyen_bh,
sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_vanchuyen_vp,	 	 
sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_khac_bh,
sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_khac_vp,	 	 
sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_phuthu_bh,
sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_phuthu_vp,	
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		else 0 end) as money_thuoc_bh,
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
		when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end) as money_thuoc_vp,

sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_vattu_bh,
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) then 
		(case when ser.doituongbenhnhanid=4 
				then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
			  else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
	when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) 
		then (case when ser.doituongbenhnhanid=4 
					then (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
								else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
					else (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)		
	 when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
	 else 0 end) as money_vattu_vp,
-------=================	
(sum(case when ser.bhyt_groupcode in ('103VTtyle') and ser.loaidoituong in (0,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		 else 0 end)
 +
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		 else 0 end)
+
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2
and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
			else 0 end)		 
		 ) as money_vtthaythe_bh,
-----------------	 
(sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)						
		when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end)
+
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end)
+
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2
and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end)
+
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2
and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (1,3)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
			else 0 end)			
			) as money_vtthaythe_vp,
---------					
sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,4,6) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_dvktc_bh,
(sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) 
						else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end)
		when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end)
+ 
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
			else 0 end)	) as money_dvktc_vp,		
	 
sum(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong)  as money_chiphikhac,		 
sum(case when ser.loaidoituong=5 then ser.servicepricemoney * ser.soluong else 0 end) as money_hpngaygiuong,	 
sum(case when ser.loaidoituong=7 then ser.servicepricemoney * ser.soluong else 0 end) as money_hppttt,

sum(case when ser.departmentid in (34,335,269,285) 
		then (case when ser.loaidoituong=2 and ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM') and ser.servicepriceid_master<>0
						then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode in ('07KTC','06PTTT')) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0) 
									then ser.servicepricemoney * ser.soluong else 0 end)			
					else 0 end)
	  else 0 end) as money_hpdkpttt_gm_thuoc,
sum(case when ser.departmentid in (34,335,269,285) 
		then (case when ser.loaidoituong=2 and ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.servicepriceid_master<>0
						then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode in ('07KTC','06PTTT')) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0) 
									then ser.servicepricemoney * ser.soluong else 0 end)			
					else 0 end)
	  else 0 end) as money_hpdkpttt_gm_vattu
							
FROM vienphi vp left join serviceprice ser on vp.vienphiid=ser.vienphiid
WHERE vp.vienphidate >'2014-01-04 00:00:00' 
	and ser.thuockhobanle=0 
GROUP BY vp.vienphiid,vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, 
vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, 
vp.vienphistatus_bh, vp.duyet_ngayduyet_bh, vp.bhyt_tuyenbenhvien, ser.departmentid, ser.departmentgroupid,
(case when ser.departmentid in (34,335,269,285) then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid)
		  else ser.departmentgroupid end);
--ORDER BY vp.vienphiid DESC;













