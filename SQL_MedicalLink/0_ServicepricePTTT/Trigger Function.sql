--3 function trigger trên bảng vienphi
-1. Đóng bệnh án servicespttt_cal(): tính toán số tiền
-2. Duyệt/gỡ duyệt viện phí vienphi_vienphistatus_vp()
-3. Duyệt/gỡ duyệt BHYT vienphi_duyet_ngayduyet()

-----================================================================ TRIGGER
-- Trigger: vienphidate_ravien_change on vienphi
-- DROP TRIGGER vienphidate_ravien_change ON vienphi;

CREATE TRIGGER vienphidate_ravien_change
  AFTER UPDATE OF vienphidate_ravien OR DELETE
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE servicespttt_cal();
ALTER TABLE vienphi DISABLE TRIGGER vienphidate_ravien_change;

-- Trigger: vienphistatus_vp_change on vienphi
-- DROP TRIGGER vienphistatus_vp_change ON vienphi;

CREATE TRIGGER vienphistatus_vp_change
  AFTER UPDATE OF vienphistatus_vp
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE vienphi_vienphistatus_vp();
ALTER TABLE vienphi DISABLE TRIGGER vienphistatus_vp_change;

-- Trigger: duyet_ngayduyet_change on vienphi
-- DROP TRIGGER duyet_ngayduyet_change ON vienphi;

CREATE TRIGGER duyet_ngayduyet_change
  AFTER UPDATE OF duyet_ngayduyet
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE vienphi_duyet_ngayduyet();
ALTER TABLE vienphi DISABLE TRIGGER duyet_ngayduyet_change;

-----================================================================TRIGGER FUNCTION


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
  
-----=====
--Ngay 25/5/2018 function
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
	
	INSERT INTO ihs_servicespttt(servicepriceptttid,vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoaravien,phongravien,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,vienphistatus_vp,duyet_ngayduyet_vp,vienphistatus_bh,duyet_ngayduyet_bh,bhyt_tuyenbenhvien,departmentid,departmentgroupid,departmentgroup_huong,money_khambenh_bh,money_khambenh_vp,money_xetnghiem_bh,money_xetnghiem_vp,money_cdha_bh,money_cdha_vp,money_tdcn_bh,money_tdcn_vp,money_pttt_bh,money_pttt_vp,money_ptttyeucau_bh,money_ptttyeucau_vp,money_mau_bh,money_mau_vp,money_giuongthuong_bh,money_giuongthuong_vp,money_giuongyeucau_bh,money_giuongyeucau_vp,money_nuocsoi_bh,money_nuocsoi_vp,money_xuatan_bh,money_xuatan_vp,money_diennuoc_bh,money_diennuoc_vp,money_vanchuyen_bh,money_vanchuyen_vp,money_khac_bh,money_khac_vp,money_phuthu_bh,money_phuthu_vp,money_thuoc_bh,money_thuoc_vp,money_vattu_bh,money_vattu_vp,money_vtthaythe_bh,money_vtthaythe_vp,money_dvktc_bh,money_dvktc_vp,money_chiphikhac,money_hpngaygiuong,money_hppttt,money_hpdkpttt_gm_thuoc,money_hpdkpttt_gm_vattu,money_dkpttt_thuoc_bh,money_dkpttt_thuoc_vp,money_dkpttt_vattu_bh,money_dkpttt_vattu_vp,money_vattu_ttrieng_bh,money_vattu_ttrieng_vp,money_hppttt_goi_thuoc,money_hppttt_goi_vattu,thutienstatus)
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
--PTTT Thuong
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))<>'PTYC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_pttt_bh,	 
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))<>'PTYC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))<>'PTYC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))<>'PTYC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_pttt_vp,
--PTTT yeu cau	 	
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='PTYC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_ptttyeucau_bh,	
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='PTYC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='PTYC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='PTYC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_ptttyeucau_vp,		
/*		
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
*/		
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_mau_bh,
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
		when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
		else 0 end) as money_mau_vp, 
--ngay giuong		
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG')) in ('G303TH','G350','G303') ) 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongthuong_bh,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG')) in ('G303TH','G350','G303') ) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG')) in ('G303TH','G350','G303') ) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG')) in ('G303TH','G350','G303') ) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_giuongthuong_vp,	 
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (0,4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='G303YC') 
			then ser.servicepricemoney_bhyt*ser.soluong 
		else 0 end) as money_giuongyeucau_bh,	
sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='G303YC') 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode in ('06PTTT','12NG'))='G303YC') 
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
sum(case when ser.bhyt_groupcode in ('10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong in (0,4,6) 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) 
		when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong in (0,4,6)
			then (case when cast(ser.servicepricebhytdinhmuc as numeric)>0	
							then (case when ser.maubenhphamphieutype=0 then cast(ser.servicepricebhytdinhmuc as numeric)*ser.soluong else 0-(cast(ser.servicepricebhytdinhmuc as numeric)*ser.soluong) end)
						else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) end)
		else 0 end) as money_vattu_bh,
sum(case when ser.bhyt_groupcode in ('10VT','101VTtrongDM','102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8)
			then (case when ser.doituongbenhnhanid=4 
						  then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) 
					   else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
		when ser.bhyt_groupcode in ('10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong in (4,6)
			then (case when ser.doituongbenhnhanid=4 
						then (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end)
								else (case when ser.servicepricemoney_nuocngoai>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0 end) end) 
					else (case when ser.maubenhphamphieutype=0 
									then (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) 		
								else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then 0-(ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0 end) end) end)
		when ser.bhyt_groupcode in ('10VT','101VTtrongDM','102VTngoaiDM','103VTtyle') and ser.loaidoituong=3
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
		then (case when ser.loaidoituong=2 and ser.bhyt_groupcode in ('10VT','101VTtrongDM','102VTngoaiDM') and ser.servicepriceid_master<>0
						then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode in ('07KTC','06PTTT')) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=0) 
									then ser.servicepricemoney * ser.soluong else 0 end)			
					else 0 end)
	  else 0 end) as money_hpdkpttt_gm_vattu,
--Thuoc di kem PTTT	+ Thuoc TT rieng
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
			when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)			
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
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','103VTtyle','10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong=2 and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0 and vp.doituongbenhnhanid=1
			then (case when ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC' and ser_ktc.loaidoituong in (0,4,6)) and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)	
							then (case when ser.maubenhphamphieutype=0 
											then servicepricemoney_bhyt*ser.soluong
										else 0-(servicepricemoney_bhyt*ser.soluong) end)
						else 0 end)	
/*--tach VT TT rieng						
		when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
		when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
*/
			else 0 end) as money_dkpttt_vattu_bh,	
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong=2 and coalesce(ser.servicepriceid_thanhtoanrieng,0)=0
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

/*	when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM','102VTngoaiDM') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 
		then (case when vp.doituongbenhnhanid<>1
						then (case when ser.maubenhphamphieutype=0 
										then servicepricemoney_bhyt*ser.soluong
									else 0-(servicepricemoney_bhyt*ser.soluong) end)
				else 0 end)
*/				
				---------?????
	when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong in (0,2,4,6) and cast(ser.servicepricebhytdinhmuc as numeric)>0
			then (case when ser.maubenhphamphieutype=0 
							then (ser.servicepricemoney_bhyt-cast(ser.servicepricebhytdinhmuc as numeric))*ser.soluong 
						else 0-((ser.servicepricemoney_bhyt-cast(ser.servicepricebhytdinhmuc as numeric))*ser.soluong)
				  end)
	 else 0 end) as money_dkpttt_vattu_vp,
	 
--VT TT rieng
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1
			then (case when ser.maubenhphamphieutype=0 
							then servicepricemoney_bhyt*ser.soluong
					    else 0-(servicepricemoney_bhyt*ser.soluong) end)
		when ser.bhyt_groupcode='103VTtyle' and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 and vp.doituongbenhnhanid=1 
			then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end)
	else 0 end) as money_vattu_ttrieng_bh,
sum(case when ser.bhyt_groupcode in ('101VTtrongDMTT','10VT','101VTtrongDM','102VTngoaiDM','103VTtyle') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 
		then (case when vp.doituongbenhnhanid<>1
						then (case when ser.maubenhphamphieutype=0 
										then servicepricemoney_bhyt*ser.soluong
									else 0-(servicepricemoney_bhyt*ser.soluong) end)
				else 0 end)
	else 0 end) as money_vattu_ttrieng_vp,	

	
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
	else 0 end) as money_hppttt_goi_vattu,
(case when vp.vienphistatus_vp=1 and vp.doituongbenhnhanid>1 and vp.loaivienphiid=1 and ser.billid_thutien=0 and ser.billid_clbh_thutien=0 
		then 0
		else 1 end) as thutienstatus
FROM (select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,departmentgroupid,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,coalesce(vienphistatus_vp,0) as vienphistatus_vp,duyet_ngayduyet_vp,vienphistatus_bh,duyet_ngayduyet_bh,bhyt_tuyenbenhvien 
		from vienphi 
		where vienphiid=OLD.vienphiid
		) vp 
	left join (select * from serviceprice where vienphiid=OLD.vienphiid and thuockhobanle=0) ser on vp.vienphiid=ser.vienphiid
GROUP BY vp.vienphiid,vp.patientid,vp.bhytid,vp.hosobenhanid,vp.loaivienphiid,vp.vienphistatus,vp.departmentgroupid, vp.departmentid,
vp.doituongbenhnhanid,vp.vienphidate,vp.vienphidate_ravien,vp.duyet_ngayduyet,vp.vienphistatus_vp,vp.duyet_ngayduyet_vp,
vp.vienphistatus_bh,vp.duyet_ngayduyet_bh,vp.bhyt_tuyenbenhvien,ser.departmentid,ser.departmentgroupid,
(case when ser.departmentid in (34,335,269,285) then (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid)
		  else ser.departmentgroupid end),
(case when vp.vienphistatus_vp=1 and vp.doituongbenhnhanid>1 and vp.loaivienphiid=1 and ser.billid_thutien=0 and ser.billid_clbh_thutien=0 
		then 0
		else 1 end)	 ;
		  
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











  
  
  
  
  