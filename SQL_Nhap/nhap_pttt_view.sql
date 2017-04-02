---------------View Serviceprice_pttt v 1.9 ngay 20/3/2017
--- su dung rieng cho bv VietTiep chia khoa GMHT
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
----------
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
-----==================	 
(sum(case when ser.bhyt_groupcode in ('103VTtyle') and ser.loaidoituong in (0,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		 else 0 end)
 +
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		 else 0 end)
+
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2
and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
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
			) as money_vtthaythe_vp,
			
----------==============		
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
------
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
---------

--bao cao PTTT co tinh cho GMHT: ngay 20/3
--bao cao PTTT co tinh cho GMHT: ngay 20/3
SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, 
O.*, 
bill.tam_ung as tam_ung 
FROM 
	(SELECT dep.departmentgroupid, 
	dep.departmentgroupname, 
	sum(A.soluot) as soluot,
	sum(C.count_bh) as soluong_bh,
	sum(C.count_vp) as soluong_vp,
	sum(C.count) as soluong,
	COALESCE(sum(A.money_khambenh),0)+COALESCE(sum(B.money_khambenh),0) as money_khambenh, 
	sum(A.money_xetnghiem) as money_xetnghiem, 
	sum(A.money_cdhatdcn) as money_cdhatdcn, 
	sum(A.money_pttt) as money_pttt, 
	sum(A.money_dvktc) as money_dvktc, 
	COALESCE(sum(A.money_giuongthuong),0)+COALESCE(sum(B.money_giuongthuong),0) as money_giuongthuong, 
	COALESCE(sum(A.money_giuongyeucau),0)+COALESCE(sum(B.money_giuongyeucau),0) as money_giuongyeucau, 
	COALESCE(sum(A.money_mau),0)+COALESCE(sum(B.money_mau),0) as money_mau, 
	COALESCE(sum(A.money_thuoc),0)+COALESCE(sum(B.money_thuoc),0) as money_thuoc,
	COALESCE(sum(A.money_vattu),0)+COALESCE(sum(B.money_vattu),0) as money_vattu,
	sum(A.money_vtthaythe) as money_vtthaythe, 
	COALESCE(sum(A.money_phuthu),0)+COALESCE(sum(B.money_phuthu),0) as money_phuthu, 
	COALESCE(sum(A.money_vanchuyen),0)+COALESCE(sum(B.money_vanchuyen),0) as money_vanchuyen, 
	COALESCE(sum(A.money_khac),0)+COALESCE(sum(B.money_khac),0) as money_khac, 
	COALESCE(sum(A.money_hpngaygiuong),0)+COALESCE(sum(B.money_hpngaygiuong),0) as money_hpngaygiuong, 
	COALESCE(sum(A.money_hppttt),0)+COALESCE(sum(B.money_hppttt),0) as money_hppttt, 
	COALESCE(sum(A.money_chiphikhac),0)+COALESCE(sum(B.money_chiphikhac),0) as money_chiphikhac, 
	sum(B.money_pttt) as gmht_money_pttt, 
	sum(B.money_hpdkpttt_gm_thuoc) as gmht_money_thuoc, 
	sum(B.money_hpdkpttt_gm_vattu) as gmht_money_vattu, 
	sum(B.money_vtthaythe) as gmht_money_vtthaythe, 
	sum(B.money_cls) as gmht_money_cls,
	COALESCE(sum(A.money_tong_bh),0) + COALESCE(sum(B.money_tong_bh),0) as tong_tien_bh,
	COALESCE(sum(A.money_tong_vp),0) + COALESCE(sum(B.money_tong_vp),0) as tong_tien_vp,
	COALESCE(sum(A.money_khambenh),0) + COALESCE(sum(B.money_khambenh),0) + COALESCE(sum(A.money_xetnghiem),0) + COALESCE(sum(A.money_cdhatdcn),0) + COALESCE(sum(A.money_pttt),0) + COALESCE(sum(A.money_dvktc),0) + COALESCE(sum(A.money_giuongthuong),0) + COALESCE(sum(B.money_giuongthuong),0) + COALESCE(sum(A.money_giuongyeucau),0) + COALESCE(sum(B.money_giuongyeucau),0) + COALESCE(sum(A.money_mau),0) + COALESCE(sum(B.money_mau),0) + COALESCE(sum(A.money_thuoc),0) + COALESCE(sum(A.money_vattu),0) + COALESCE(sum(A.money_vtthaythe),0) + COALESCE(sum(A.money_phuthu),0) + COALESCE(sum(B.money_phuthu),0) + COALESCE(sum(A.money_vanchuyen),0) + COALESCE(sum(B.money_vanchuyen),0) + COALESCE(sum(A.money_khac),0) + COALESCE(sum(B.money_khac),0) + COALESCE(sum(B.money_pttt),0) + COALESCE(sum(B.money_thuoc),0) + COALESCE(sum(B.money_vattu),0) + COALESCE(sum(B.money_vtthaythe),0) + COALESCE(sum(B.money_cls),0) as tong_tien 
	FROM departmentgroup dep 
	LEFT JOIN 
		(SELECT spt.departmentgroupid, 
		count(spt.*) as soluot, 
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as money_xetnghiem, 
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cdhatdcn, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as money_pttt, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as money_dvktc, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_chiphikhac) as money_chiphikhac,
		sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh) as money_tong_bh,
		sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp) as money_tong_vp		
		FROM serviceprice_pttt spt 
		WHERE spt.departmentid not in (34,335,269,285) and spt.vienphistatus_vp=1 
		and spt.duyet_ngayduyet_vp >='" + thoiGianTu + "' and spt.duyet_ngayduyet_vp <='" + thoiGianDen + "' 
		GROUP BY spt.departmentgroupid) A ON dep.departmentgroupid=A.departmentgroupid 
	LEFT JOIN 
		(SELECT spt.departmentgroup_huong, 
		sum(spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp) as money_pttt, 
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp) as money_thuoc, 
		sum(spt.money_vattu_bh + spt.money_vattu_vp) as money_vattu, 
		sum(spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as money_vtthaythe, 
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as money_cls, 
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as money_khambenh, 
		sum(spt.money_mau_bh + spt.money_mau_vp) as money_mau, 
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as money_giuongthuong, 
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as money_giuongyeucau, 
		sum(spt.money_phuthu_bh + spt.money_phuthu_vp) as money_phuthu, 
		sum(spt.money_vanchuyen_bh + spt.money_vanchuyen_vp) as money_vanchuyen, 
		sum(spt.money_khac_bh + spt.money_khac_vp) as money_khac, 
		sum(spt.money_hpngaygiuong) as money_hpngaygiuong, 
		sum(spt.money_hppttt) as money_hppttt, 
		sum(spt.money_chiphikhac) as money_chiphikhac,
		sum(spt.money_hpdkpttt_gm_thuoc) as money_hpdkpttt_gm_thuoc,
		sum(spt.money_hpdkpttt_gm_vattu) as money_hpdkpttt_gm_vattu,
		sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh) as money_tong_bh,
		sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp) as money_tong_vp
		FROM serviceprice_pttt spt 
		WHERE spt.departmentid in (34,335,269,285) and spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp >='" + thoiGianTu + "' and spt.duyet_ngayduyet_vp <='" + thoiGianDen + "' 
		GROUP BY spt.departmentgroup_huong) B ON dep.departmentgroupid=B.departmentgroup_huong
	LEFT JOIN 
	(SELECT count(*) as count, 
			sum(case when doituongbenhnhanid=1 then 1 else 0 end) as count_bh,
			sum(case when doituongbenhnhanid=1 then 0 else 1 end) as count_vp,
			vp.departmentgroupid FROM vienphi vp 
	 WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp >='" + thoiGianTu + "' and vp.duyet_ngayduyet_vp <='" + thoiGianDen + "'
	 GROUP BY vp.departmentgroupid) C ON C.departmentgroupid=dep.departmentgroupid	
	WHERE dep.departmentgroupid<>21 and departmentgrouptype in (1,4,11,10,100)	
	GROUP BY dep.departmentgroupid, dep.departmentgroupname) O 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, b.departmentgroupid 
	from vienphi vp inner join bill b on vp.vienphiid=b.vienphiid 
	where vp.duyet_ngayduyet_vp >='" + thoiGianTu + "' and vp.duyet_ngayduyet_vp <='" + thoiGianDen + "'  and b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphistatus_vp=1 
	group by b.departmentgroupid) BILL ON BILL.departmentgroupid=O.departmentgroupid;
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	









