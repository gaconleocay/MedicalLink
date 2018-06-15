----------------===========QL tong the khoa - DANG DIEU TRI - chi tiet
--DANH SÁCH CHI TIẾT BỆNH NHÂN HIỆN DIỆN
--ngay 25/3/2018

SELECT ROW_NUMBER() OVER (ORDER BY hsbn.patientname) as stt, 
VPP.*, 
vp.vienphiid, 
vp.patientid, 
hsbn.patientname, 
bhyt.bhytcode, 
bhyt.bhyt_loaiid, 
vp.loaivienphiid, 
bhyt.du5nam6thangluongcoban, 
vp.bhyt_tuyenbenhvien, 
vp.departmentgroupid, 
'' as departmentname, 
vp.vienphidate, 
vp.vienphidate_ravien, 
-1 as songay,
-1 as songaythanhtoan,
vp.duyet_ngayduyet_vp, 
case when VPP.money_tong<>0 then round(cast((VPP.money_thuoc/VPP.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (	
	SELECT count(*) as dangdt_slbn, 
		 vpm.hosobenhanid, 
		 vpm.departmentid, 
		 sum(vpm.money_khambenh) as money_khambenh,  
		 sum(vpm.money_xetnghiem) as money_xetnghiem,  
		 sum(coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0)) as money_cdhatdcn,  
		 sum(vpm.money_pttt) as money_pttt, 
		 sum(vpm.money_ptttyeucau) as money_ptttyeucau, 
		 sum(vpm.money_dvktc) as money_dvktc,  
		 sum(vpm.money_giuongthuong) as money_giuongthuong,  
		 sum(vpm.money_giuongyeucau) as money_giuongyeucau,  
		 sum(coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0)) as money_khac,   
		 sum(vpm.money_vattu) as money_vattu,  
		sum(vpm.money_vattu_ttrieng) as money_vattu_ttrieng,
		 sum(vpm.money_mau) as money_mau,  
		 sum(vpm.money_thuoc) as money_thuoc,  
		 sum(coalesce(vpm.money_khambenh,0) + coalesce(vpm.money_xetnghiem,0) + coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0) + coalesce(vpm.money_pttt,0) + coalesce(vpm.money_dvktc,0) + coalesce(vpm.money_giuongthuong,0) + coalesce(vpm.money_giuongyeucau,0) + coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0) + coalesce(vpm.money_vattu,0) + coalesce(vpm.money_mau,0) + coalesce(vpm.money_thuoc,0) + coalesce(vpm.money_ptttyeucau,0) + coalesce(vpm.money_vattu_ttrieng,0)) as money_tong, 
		 sum(vpm.tam_ung) as tam_ung  
	FROM
	(
		SELECT mrd.vienphiid,
		mrd.hosobenhanid, 
		mrd.loaibenhanid,  
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
		FROM (select vienphiid,hosobenhanid,loaibenhanid,departmentid,doituongbenhnhanid from medicalrecord where thoigianvaovien >'" + dateKhoangDLTu + "' and medicalrecordstatus=2 and departmentid in (" + this.lstPhongChonLayBC + ")) mrd 
			left join (select vienphiid,bhyt_groupcode,maubenhphamphieutype,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,loaidoituong,doituongbenhnhanid,lankhambenh,servicepricecode,servicepriceid_master,servicepriceid_thanhtoanrieng from serviceprice where servicepricedate >'" + dateKhoangDLTu + "' and thuockhobanle=0) ser on mrd.vienphiid=ser.vienphiid
		GROUP BY mrd.vienphiid,
		mrd.hosobenhanid, 
		mrd.loaibenhanid,  
		mrd.departmentid, 
		mrd.doituongbenhnhanid
	) vpm
	GROUP BY vpm.hosobenhanid, vpm.departmentid
	) VPP
inner join (select vienphiid, hosobenhanid, patientid, loaivienphiid, bhyt_tuyenbenhvien, departmentgroupid, 
vienphidate, bhytid, TO_CHAR(vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, TO_CHAR(duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp
			from vienphi where vienphistatus=0 and vienphidate >'" + dateKhoangDLTu + "') vp on vpp.hosobenhanid=vp.hosobenhanid
inner join (select hosobenhanid, patientname from hosobenhan) hsbn on vp.hosobenhanid=hsbn.hosobenhanid 	
inner join (select bhytid, bhytcode, bhyt_loaiid, du5nam6thangluongcoban from bhyt where bhytdate>'" + dateKhoangDLTu + "') bhyt on bhyt.bhytid=vp.bhytid;
--inner join (select departmentid, departmentname from department where departmenttype in (2,3,9)) prv on prv.departmentid=vpp.departmentid



--BN chuyển đi + BN chuyển đến
--ngay 25/3/2018
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM 
(SELECT vpm.vienphiid, 
		vpm.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		vpm.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		vpm.bhyt_tuyenbenhvien,vpm.departmentgroupid, 
		prv.departmentname, 
		vpm.vienphidate, 
		TO_CHAR(vpm.vienphidate_ravien, 
		'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		-1 as songay, 
		-1 as songaythanhtoan,
		TO_CHAR(vpm.duyet_ngayduyet_vp, 
		'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
		round(cast((vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as money_pttt,
		round(cast((vpm.money_ptttyeucau_bh + vpm.money_ptttyeucau_vp) as numeric),0) as money_ptttyeucau,
		round(cast((vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as money_khac, 
		round(cast((vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as money_vattu,
		round(cast((vpm.money_vattu_ttrieng_bh + vpm.money_vattu_ttrieng_vp) as numeric),0) as money_vattu_ttrieng, 
		round(cast((vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as money_thuoc, 
		round(cast((vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp + vpm.money_ptttyeucau_bh + vpm.money_ptttyeucau_vp + vpm.money_vattu_ttrieng_bh + vpm.money_vattu_ttrieng_vp) as numeric),0) as money_tong, 
		round(cast((vpm.tam_ung) as numeric),0) as tam_ung 
FROM vienphi_money vpm 
inner join (select hosobenhanid,patientname from hosobenhan) hsbn on vpm.hosobenhanid=hsbn.hosobenhanid inner join (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bhyt on bhyt.bhytid=vpm.bhytid 
INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON vpm.departmentid=prv.departmentid 
WHERE vpm.vienphiid in (" + lstbn_chuyendi + ")) A; 
--WHERE vpm.vienphiid in (" + lstbn_chuyenden + ")) A; 




--Ra viện chưa TT; ra viện đã TT : case = 4,5,6,7,8,9
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung, 
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM 
(SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.khoaravien as departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 
		'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		"+_tieuchi_songay+"
		---1 as songay, 
		---1 as songaythanhtoan,
		TO_CHAR(spt.duyet_ngayduyet_vp, 
		'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
		round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast(sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast(sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast(sum(spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast(sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as numeric),0) as money_ptttyeucau,
		round(cast(sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast(sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast(sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast(sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast(sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast(sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as numeric),0) as money_vattu_ttrieng, 
		round(cast(sum(spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast(sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc, 
		round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp + spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp + spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as numeric),0) as money_tong 
FROM ihs_servicespttt spt 
	inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
	inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
	left join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
WHERE 1=1 "+_tieuchi_ravien_spt+"
	--spt.vienphistatus<>0 and spt.vienphidate_ravien between '" + dateTuNgay + "' and '" + dateDenNgay + "' and spt.phongravien in (" + this.lstPhongChonLayBC + ") 
GROUP BY spt.vienphiid,spt.patientid,hsbn.patientname,bhyt.bhytcode,bhyt.bhyt_loaiid,spt.loaivienphiid,bhyt.du5nam6thangluongcoban,spt.bhyt_tuyenbenhvien,spt.khoaravien,prv.departmentname,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp) A 
LEFT JOIN (select sum(b.datra) as tam_ung,vp.vienphiid 
		from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 
		where 1=1 "+_tieuchi_ravien_vp+"
--vp.departmentid in (" + this.lstPhongChonLayBC + ") and vp.vienphidate_ravien between '" + dateTuNgay + "' and '" + dateDenNgay + "' and vp.vienphistatus<>0 
		group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid;


























----BO


/*
--Chi tiet khoa 
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung,
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 	
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast((spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast((spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast((spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc,
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
		FROM tools_serviceprice_pttt spt 
			inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
			inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
			inner join department prv ON spt.departmentid=prv.departmentid and prv.departmenttype in (2,3,9) 
		WHERE spt.vienphistatus_vp=1 
			and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
			and spt.departmentid in (92)) A 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
			b.vienphiid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid and b.departmentid in (92)
	where vp.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
		and b.loaiphieuthuid=2 
		and b.dahuyphieu=0 
		and vp.vienphistatus_vp=1 
	group by b.vienphiid) BILL ON BILL.vienphiid=A.vienphiid			
ORDER BY A.duyet_ngayduyet_vp ;


---chi tiet gay me

SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung,
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.departmentgroupid, 
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 	
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast((spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast((spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast((spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc,
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
		FROM tools_serviceprice_pttt spt 
			inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
			inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
			inner join department prv ON spt.departmentid=prv.departmentid and prv.departmenttype in (2,3,9) 
		WHERE spt.vienphistatus_vp=1 
			and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
			and spt.departmentid in (34,335,269,285) and spt.departmentgroup_huong in (27)

			) A 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
			b.vienphiid 
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid and b.departmentid in (34,335,269,285)
	where vp.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
		and b.loaiphieuthuid=2 
		and b.dahuyphieu=0 
		and vp.vienphistatus_vp=1 
	group by b.vienphiid) BILL ON BILL.vienphiid=A.vienphiid			
ORDER BY A.duyet_ngayduyet_vp ;


--Tong khoa + Gay me

SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung,
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
		spt.patientid, 
		hsbn.patientname, 
		bhyt.bhytcode, 
		bhyt.bhyt_loaiid, 
		spt.loaivienphiid, 
		bhyt.du5nam6thangluongcoban, 
		spt.bhyt_tuyenbenhvien, 
		spt.departmentgroupid, 
		spt.departmentid,
		prv.departmentname, 
		spt.vienphidate, 
		TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
		TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 	
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
		round(cast((spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
		round(cast((spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
		round(cast((spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
		round(cast((spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
		round(cast((spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
		round(cast((spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
		round(cast((spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
		round(cast((spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
		round(cast((spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
		round(cast((spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc,
		round(cast((spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
		FROM tools_serviceprice_pttt spt 
			inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
			inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
			inner join department prv ON spt.departmentid=prv.departmentid and prv.departmenttype in (2,3,9) 
		WHERE spt.vienphistatus_vp=1 
			and spt.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
			and (spt.departmentid in (92) 
			or (spt.departmentid in (34,335,269,285) and spt.departmentgroup_huong in (27)))
		
			) A 
LEFT JOIN 
	(select sum(b.datra) as tam_ung, 
			b.vienphiid,
			b.departmentid
	from vienphi vp 
		inner join bill b on vp.vienphiid=b.vienphiid and b.departmentid in (92,34,335,269,285)
	where vp.duyet_ngayduyet_vp between '2017-01-01 00:00:00' and '2017-01-05 00:00:00' 
		and b.loaiphieuthuid=2 
		and b.dahuyphieu=0 
		and vp.vienphistatus_vp=1 
	group by b.vienphiid, b.departmentid) BILL ON BILL.vienphiid=A.vienphiid and BILL.departmentid = A.departmentid 			
ORDER BY A.duyet_ngayduyet_vp ;


-----------Benh nhan da thanh toan - daonh thu tinh theo khoa ra vien
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung, 
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
spt.patientid, 
hsbn.patientname, 
bhyt.bhytcode, 
bhyt.bhyt_loaiid, 
spt.loaivienphiid, 
bhyt.du5nam6thangluongcoban, 
spt.bhyt_tuyenbenhvien, 
spt.khoaravien as departmentgroupid, 
prv.departmentname, 
spt.vienphidate, 
TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
round(cast(sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
round(cast(sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
round(cast(sum(spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
round(cast(sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
round(cast(sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
round(cast(sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
round(cast(sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
round(cast(sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
round(cast(sum(spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
round(cast(sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
FROM tools_serviceprice_pttt spt 
inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
inner join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.dateTuNgay + "' and '" + this.dateDenNgay + "' and spt.phongravien in (" + this.lstPhongChonLayBC + ")
GROUP BY spt.vienphiid, spt.patientid, hsbn.patientname, bhyt.bhytcode, bhyt.bhyt_loaiid, spt.loaivienphiid, bhyt.du5nam6thangluongcoban, spt.bhyt_tuyenbenhvien, spt.khoaravien, prv.departmentname, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp) A 

LEFT JOIN (select sum(b.datra) as tam_ung, vp.vienphiid 
			from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0
			where vp.departmentid in (" + this.lstPhongChonLayBC + ") and vp.duyet_ngayduyet_vp between '" + this.dateTuNgay + "' and '" + this.dateDenNgay + "' and vp.vienphistatus_vp=1 group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid 
ORDER BY A.duyet_ngayduyet_vp ;



--------Da ra vien nhung chua thanh toan
SELECT ROW_NUMBER() OVER (ORDER BY A.duyet_ngayduyet_vp) as stt, 
A.*, 
BILL.tam_ung, 
case when A.money_tong<>0 then round(cast((A.money_thuoc/A.money_tong) * 100 as numeric) ,2) else 0 end as ty_le_thuoc 
FROM (SELECT spt.vienphiid, 
spt.patientid, 
hsbn.patientname, 
bhyt.bhytcode, 
bhyt.bhyt_loaiid, 
spt.loaivienphiid, 
bhyt.du5nam6thangluongcoban, 
spt.bhyt_tuyenbenhvien, 
spt.khoaravien as departmentgroupid, 
prv.departmentname, 
spt.vienphidate, 
TO_CHAR(spt.vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien, 
DATE_PART('day', spt.vienphidate_ravien - spt.vienphidate) as songay,
TO_CHAR(spt.duyet_ngayduyet_vp, 'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as numeric),0) as money_khambenh, 
round(cast(sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as numeric),0) as money_xetnghiem, 
round(cast(sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as numeric),0) as money_cdhatdcn, 
round(cast(sum(spt.money_pttt_bh + spt.money_pttt_vp) as numeric),0) as money_pttt, 
round(cast(sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as numeric),0) as money_dvktc, 
round(cast(sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as numeric),0) as money_giuongthuong, 
round(cast(sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as numeric),0) as money_giuongyeucau, 
round(cast(sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as numeric),0) as money_khac, 
round(cast(sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as numeric),0) as money_vattu, 
round(cast(sum(spt.money_mau_bh + spt.money_mau_vp) as numeric),0) as money_mau, 
round(cast(sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_thuoc, 
round(cast(sum(spt.money_khambenh_bh + spt.money_khambenh_vp + spt.money_xetnghiem_bh + spt.money_xetnghiem_vp + spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp + spt.money_pttt_bh + spt.money_pttt_vp + spt.money_dvktc_bh + spt.money_dvktc_vp + spt.money_giuongthuong_bh + spt.money_giuongthuong_vp + spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp + spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp + spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp + spt.money_mau_bh + spt.money_mau_vp + spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as numeric),0) as money_tong 
FROM ihs_servicespttt spt 
inner join hosobenhan hsbn on spt.hosobenhanid=hsbn.hosobenhanid 
inner join bhyt bhyt on bhyt.bhytid=spt.bhytid 
left join department prv ON spt.phongravien=prv.departmentid and prv.departmenttype in (2,3,9) 
WHERE COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphistatus<>0 and spt.vienphidate>='" + dateKhoangDLTu + "' and spt.phongravien in (" + this.lstPhongChonLayBC + ")
GROUP BY spt.vienphiid, spt.patientid, hsbn.patientname, bhyt.bhytcode, bhyt.bhyt_loaiid, spt.loaivienphiid, bhyt.du5nam6thangluongcoban, spt.bhyt_tuyenbenhvien, spt.khoaravien, prv.departmentname, spt.vienphidate, spt.vienphidate_ravien, spt.duyet_ngayduyet_vp) A 

LEFT JOIN (select sum(b.datra) as tam_ung, vp.vienphiid 
			from vienphi vp 
			inner join bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0
			where vp.departmentid in (" + this.lstPhongChonLayBC + ") and vp.vienphidate>='" + dateKhoangDLTu + "' and COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 group by vp.vienphiid) BILL ON BILL.vienphiid=A.vienphiid 
ORDER BY A.duyet_ngayduyet_vp ;


*/


















