--BN dang dieu tri

SELECT count(vpm.*) as dangdt_slbn, 
COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0),0) as dangdt_tienkb, 
COALESCE(round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0),0) as dangdt_tienxn, 
COALESCE(round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0),0) as dangdt_tiencdhatdcn, 
COALESCE(round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0),0) as dangdt_tienpttt, 
COALESCE(round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0),0) as dangdt_tiendvktc, 
COALESCE(round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0),0) as dangdt_tiengiuongthuong, 
COALESCE(round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0),0) as dangdt_tiengiuongyeucau, 
COALESCE(round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0),0) as dangdt_tienkhac, 
COALESCE(round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0),0) as dangdt_tienvattu, 
COALESCE(round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0),0) as dangdt_tienmau, 
COALESCE(round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0),0) as dangdt_tienthuoc, 
COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0),0) as dangdt_tongtien, 
COALESCE(round(cast(sum(vpm.tam_ung) as numeric),0),0) as dangdt_tamung 
FROM vienphi_money vpm 
WHERE vpm.vienphistatus=0 and vpm.vienphidate>='2016-01-01 00:00:00'
	and vpm.vienphiid in (select mrd.vienphiid from medicalrecord mrd 	
		where mrd.departmentid=50 and mrd.medicalrecordstatus=2
		and mrd.thoigianvaovien>='2016-01-01 00:00:00')


	
	
vpm.vienphistatus=0 " + lstdepartmentid + " and vpm.vienphidate>='" + this.KhoangThoiGianLayDuLieu + "';	
	
	
	
	
	
	
	
	
	
	
	









