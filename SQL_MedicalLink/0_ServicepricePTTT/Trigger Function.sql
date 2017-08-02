--Ngay 2/8

-- Function: servicespttt_cal()

-- DROP FUNCTION servicespttt_cal();

CREATE OR REPLACE FUNCTION servicespttt_cal()
  RETURNS trigger AS
$BODY$BEGIN
 IF TG_OP = 'DELETE' THEN
  DELETE FROM ihs_servicespttt tvp WHERE tvp.vienphiid=OLD.vienphiid;
   RETURN OLD;
 END IF;
 IF NEW.vienphidate_ravien<>'0001-01-01 00:00:00' THEN
	DELETE FROM ihs_servicespttt tvp WHERE tvp.vienphiid=OLD.vienphiid;
	INSERT INTO ihs_servicespttt
	SELECT nextval('ihs_servicespttt_servicepriceptttid_seq'),
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
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG') in ('G303TH','G350','G303') ) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongthuong_bh,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG') in ('G303TH','G350','G303') ) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG') in ('G303TH','G350','G303') ) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG') in ('G303TH','G350','G303') ) 
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
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='NS') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_nuocsoi_bh,	
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='NS') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='NS') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='NS') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_nuocsoi_vp,
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='XA') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_xuatan_bh,	
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='XA') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='XA') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='XA') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_xuatan_vp,
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='VSDN') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_diennuoc_bh,	
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='VSDN') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='VSDN') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.report_groupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='VSDN') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_diennuoc_vp,
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
--thuoc		
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
---vat tu (co vat tu vuot tran)
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong in (0,4,6)
			then (case when cast(ser.servicepricebhytdinhmuc as numeric)>0	
							then (case when ser.maubenhphamphieutype=0 then cast(ser.servicepricebhytdinhmuc as numeric)*ser.soluong else 0-(cast(ser.servicepricebhytdinhmuc as numeric)*ser.soluong) end)
						else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) end)
		else 0 end) as money_vattu_bh,
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8)
			then (case when ser.doituongbenhnhanid=4 
						  then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
					   else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong in (4,6)
			then (case when ser.doituongbenhnhanid=4 
						then (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
								else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
					else (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
		when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) 
						else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong in (0,4,6)
			then (case when cast(ser.servicepricebhytdinhmuc as numeric)>0
						  then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_bhyt-cast(ser.servicepricebhytdinhmuc as numeric))*ser.soluong else 0-((ser.servicepricemoney_bhyt-cast(ser.servicepricebhytdinhmuc as numeric))*ser.soluong) end)
					else 0 end)			
		else 0 end) as money_vattu_vp,		
--Vat tu thay the	 
sum(case when ser.bhyt_groupcode='101VTtrongDMTT' and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		 else 0 end) as money_vtthaythe_bh, 
sum(case when ser.bhyt_groupcode='101VTtrongDMTT' and ser.loaidoituong in (1,8)
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode='101VTtrongDMTT' and ser.loaidoituong in (4,6)
			then (case when ser.doituongbenhnhanid=4 
							then (case when ser.maubenhphamphieutype=0 
											then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
										else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
						else (case when ser.maubenhphamphieutype=0 
										then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
									else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
		when ser.bhyt_groupcode='101VTtrongDMTT' and ser.loaidoituong=3
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle') and ser.loaidoituong=2
			then (case when ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0)
							then (case when ser.doituongbenhnhanid=4 
											then (case when ser.maubenhphamphieutype=0 
															then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
														else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
										else (case when ser.maubenhphamphieutype=0 
														then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
													else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
						else 0 end)
		else 0 end) as money_vtthaythe_vp,
---dich vu KTC		
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
			else 0 end)) as money_dvktc_vp,		
--Chi phi, hao phi			
sum(((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as money_chiphikhac,		 
sum(case when ser.loaidoituong=5 
			then ser.servicepricemoney * ser.soluong 
		else 0 end) as money_hpngaygiuong,	 
sum(case when ser.loaidoituong=7 and ser.servicepriceid_master=0 
			then ser.servicepricemoney * ser.soluong 
		else 0 end) as money_hppttt,
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
--Thuoc di kem PTTT	  
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
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
---Vat tu di kem PTTT (co vat tu vuot tran)			
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0 and vp.doituongbenhnhanid=1
			then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)	
							then (case when ser.maubenhphamphieutype=0 
											then servicepricemoney_bhyt*ser.soluong
										else 0-(servicepricemoney_bhyt*ser.soluong) end)
						else 0 end)		
		when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
		when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong=2 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1 
			then (case when cast(ser.servicepricebhytdinhmuc as numeric)>0
							then (case when ser.maubenhphamphieutype=0 then cast(ser.servicepricebhytdinhmuc as numeric)*ser.soluong else 0-(cast(ser.servicepricebhytdinhmuc as numeric)*ser.soluong) end)
						else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) end)
			else 0 end) as money_dkpttt_vattu_bh,	
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0
		then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
						then (case when ser.doituongbenhnhanid=4 
										then (case when ser.maubenhphamphieutype=0 
														then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
													else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) 
												end) 
									else (case when ser.maubenhphamphieutype=0 
													then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
												else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 
										   end) 
								end)
				 when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (1,3)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
								then (case when ser.maubenhphamphieutype=0 
												then servicepricemoney_bhyt*ser.soluong
											else 0-(servicepricemoney_bhyt*ser.soluong) end)					
				else 0 end)
	when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT', '101VTtrongDM', '102VTngoaiDM') and ser.loaidoituong=2 and ser.servicepriceid_thanhtoanrieng>0 
		then (case when vp.doituongbenhnhanid<>1
						then (case when ser.maubenhphamphieutype=0 
										then servicepricemoney_bhyt*ser.soluong
									else 0-(servicepricemoney_bhyt*ser.soluong) end)
				else 0 end)
				---------?????
	when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong in (0,2,4,6) and cast(ser.servicepricebhytdinhmuc as numeric)>0
			then (case when ser.maubenhphamphieutype=0 
							then (ser.servicepricemoney_bhyt-cast(ser.servicepricebhytdinhmuc as numeric))*ser.soluong 
						else 0-((ser.servicepricemoney_bhyt-cast(ser.servicepricebhytdinhmuc as numeric))*ser.soluong)
				  end)
	 else 0 end) as money_dkpttt_vattu_vp,		
-----hao phi trong goi khong tinh tien
sum(case when ser.servicepriceid_master<>0 and ser.loaidoituong in (5,7,9) and ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_nhandan*ser.soluong
					    else 0-(servicepricemoney_nhandan*ser.soluong) end)
	 when ser.servicepriceid_master<>0 and ser.loaidoituong=2 and ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')
			then (case when ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0)
							then (case when ser.maubenhphamphieutype=0 
											then servicepricemoney_nhandan*ser.soluong
										else 0-(servicepricemoney_nhandan*ser.soluong) end)
						else 0 end)
	else 0 end) as money_hppttt_goi_thuoc,
sum(case when ser.servicepriceid_master<>0 and ser.loaidoituong in (5,7,9) and ser.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_nhandan*ser.soluong
					    else 0-(servicepricemoney_nhandan*ser.soluong) end)
	  when ser.servicepriceid_master<>0 and ser.loaidoituong=2 and ser.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')
			then (case when ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0)
							then (case when ser.maubenhphamphieutype=0 
										then servicepricemoney_nhandan*ser.soluong
									else 0-(servicepricemoney_nhandan*ser.soluong) end)
						else 0 end)
	else 0 end) as money_hppttt_goi_vattu	
							
FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,departmentgroupid,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,vienphistatus_vp,duyet_ngayduyet_vp,vienphistatus_bh,duyet_ngayduyet_bh,bhyt_tuyenbenhvien from vienphi where vienphiid=OLD.vienphiid) vp 
	left join serviceprice ser on vp.vienphiid=ser.vienphiid and ser.thuockhobanle=0
--WHERE vp.vienphiid=OLD.vienphiid
	 
GROUP BY vp.vienphiid,vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, 
vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, 
vp.vienphistatus_bh, vp.duyet_ngayduyet_bh, vp.bhyt_tuyenbenhvien, ser.departmentid, ser.departmentgroupid,
(case when ser.departmentid in (34,335,269,285) then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid)
		  else ser.departmentgroupid end);	
 RETURN NEW;
 ELSIF NEW.vienphidate_ravien='0001-01-01 00:00:00' THEN
 DELETE FROM ihs_servicespttt tvp WHERE tvp.vienphiid=OLD.vienphiid;
  RETURN NEW;
 END IF;
 
 
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION servicespttt_cal()
  OWNER TO postgres;





-------bắt gỡ duyệt Thu ngân
-- Function: vienphi_vienphistatus_vp()

-- DROP FUNCTION vienphi_vienphistatus_vp();

CREATE OR REPLACE FUNCTION vienphi_vienphistatus_vp()
  RETURNS trigger AS
$BODY$
BEGIN
 IF COALESCE(OLD.vienphistatus_vp,0) <> COALESCE(NEW.vienphistatus_vp,0) THEN
 UPDATE ihs_servicespttt SET vienphistatus=NEW.vienphistatus, vienphistatus_vp=NEW.vienphistatus_vp, duyet_ngayduyet_vp=NEW.duyet_ngayduyet_vp, vienphistatus_bh=NEW.vienphistatus_bh, duyet_ngayduyet_bh=NEW.duyet_ngayduyet_bh, duyet_ngayduyet=NEW.duyet_ngayduyet WHERE vienphiid=OLD.vienphiid;
 END IF;
 
 RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION vienphi_vienphistatus_vp()
  OWNER TO postgres;

  
-------bắt gỡ duyệt BHYT
-- Function: vienphi_duyet_ngayduyet()

-- DROP FUNCTION vienphi_duyet_ngayduyet();

CREATE OR REPLACE FUNCTION vienphi_duyet_ngayduyet()
  RETURNS trigger AS
$BODY$
BEGIN
 IF OLD.duyet_ngayduyet <> NEW.duyet_ngayduyet THEN
 UPDATE ihs_servicespttt SET vienphistatus=NEW.vienphistatus, duyet_ngayduyet=NEW.duyet_ngayduyet WHERE vienphiid=OLD.vienphiid;
 END IF;
 
 RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION vienphi_duyet_ngayduyet()
  OWNER TO postgres;






  
  
  
  
  