--Bao cao thu tien tu BN - ngay 28/7


	SELECT --row_number () over (order by hsba.patientname) as stt,
		(row_number() OVER (PARTITION BY krv.departmentgroupname order by hsba.patientname)) as stt,
		spt.vienphiid,
		spt.patientid,
		hsba.patientcode,
		hsba.patientname,
		to_char(hsba.birthday, 'yyyy') as namsinh,
		hsba.gioitinhname as gioitinh,
		bh.bhytcode,
		bh.bhyt_loaiid,
		bh.du5nam6thangluongcoban,
		spt.vienphidate,
		spt.vienphidate_ravien,
		spt.duyet_ngayduyet_vp,
		spt.duyet_ngayduyet,
		spt.bhyt_tuyenbenhvien,
		spt.doituongbenhnhanid,
		spt.bhyt_thangluongtoithieu,
		krv.departmentgroupid,
		krv.departmentgroupname,
		prv.departmentname,	
		sum(spt.money_khambenh_bh) as money_khambenh_bh, 
		sum(spt.money_khambenh_vp) as money_khambenh_vp, 
		sum(spt.money_xetnghiem_bh) as money_xetnghiem_bh, 
		sum(spt.money_xetnghiem_vp) as money_xetnghiem_vp,
		sum(spt.money_cdha_bh + spt.money_tdcn_bh) as money_cdhatdcn_bh, 
		sum(spt.money_cdha_vp + spt.money_tdcn_vp) as money_cdhatdcn_vp, 
		sum(spt.money_pttt_bh) as money_pttt_bh,
		sum(spt.money_pttt_vp) as money_pttt_vp, 		
		sum(spt.money_dvktc_bh) as money_dvktc_bh, 
		sum(spt.money_dvktc_vp) as money_dvktc_vp, 
		sum(spt.money_mau_bh) as money_mau_bh, 
		sum(spt.money_mau_vp) as money_mau_vp, 
		sum(spt.money_giuong_bh) as money_giuong_bh, 
		sum(spt.money_giuong_vp) as money_giuong_vp, 
		sum(spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh) as money_thuoc_bh,
		sum(spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp) as money_thuoc_vp, 
		sum(spt.money_vattu_bh + spt.money_dkpttt_vattu_bh) as money_vattu_bh, 
		sum(spt.money_vattu_vp + spt.money_dkpttt_vattu_vp) as money_vattu_vp,
		sum(spt.money_phuthu_bh) as money_phuthu_bh,
		sum(spt.money_phuthu_vp) as money_phuthu_vp,
		sum(spt.money_vtthaythe_bh) as money_vtthaythe_bh,
		sum(spt.money_vtthaythe_vp) as money_vtthaythe_vp,
		sum(spt.money_vanchuyen_bh) as money_vanchuyen_bh,
		sum(spt.money_vanchuyen_vp) as money_vanchuyen_vp,
		sum(spt.money_khac_bh) as money_khac_bh, 		
		sum(spt.money_khac_vp) as money_khac_vp,
		sum(spt.money_khambenh_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_mau_bh + spt.money_giuong_bh + spt.money_thuoc_bh + spt.money_dkpttt_thuoc_bh + spt.money_vattu_bh + spt.money_dkpttt_vattu_bh + spt.money_phuthu_bh + spt.money_vtthaythe_bh + spt.money_vanchuyen_bh + spt.money_khac_bh) as money_tong_bh,
		sum(spt.money_khambenh_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_mau_vp + spt.money_giuong_vp + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_vp + spt.money_vattu_vp + spt.money_dkpttt_vattu_vp + spt.money_phuthu_vp + spt.money_vtthaythe_vp + spt.money_vanchuyen_vp + spt.money_khac_vp) as money_tong_vp
		FROM 
			(select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoaravien,phongravien,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,duyet_ngayduyet_vp,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu,money_khambenh_bh,money_xetnghiem_bh,money_cdha_bh,money_tdcn_bh,money_pttt_bh,money_dvktc_bh,money_mau_bh,money_giuong_bh,money_thuoc_bh,money_dkpttt_thuoc_bh,money_vattu_bh,money_dkpttt_vattu_bh,money_phuthu_bh,money_vtthaythe_bh,money_vanchuyen_bh,money_khac_bh,money_khambenh_vp,money_xetnghiem_vp,money_cdha_vp,money_tdcn_vp,money_pttt_vp,money_dvktc_vp,money_mau_vp,money_giuong_vp,money_thuoc_vp,money_dkpttt_thuoc_vp,money_vattu_vp,money_dkpttt_vattu_vp,money_phuthu_vp,money_vtthaythe_vp,money_vanchuyen_vp,money_khac_vp		
			from vienphi_money_tm where vienphistatus_vp=1 and "+tieuchi_vp+" between '" + datetungay + "' and '" + datedenngay + "' "+doituongbenhnhan_vp+") spt 
		INNER JOIN (select hosobenhanid,patientcode,patientname,birthday,gioitinhname from hosobenhan) hsba on hsba.hosobenhanid=spt.hosobenhanid
		INNER JOIN (select bhytid,bhytcode,bhyt_loaiid,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=spt.bhytid	
		LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=spt.khoaravien
		LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) prv ON prv.departmentid=spt.phongravien
		GROUP BY spt.vienphiid,spt.patientid,hsba.patientcode,hsba.patientname,hsba.birthday,hsba.gioitinhname,bh.bhytcode,bh.bhyt_loaiid,bh.du5nam6thangluongcoban,spt.vienphidate,spt.vienphidate_ravien,spt.duyet_ngayduyet_vp,spt.duyet_ngayduyet,spt.bhyt_tuyenbenhvien,spt.doituongbenhnhanid,spt.bhyt_thangluongtoithieu,krv.departmentgroupid,krv.departmentgroupname,prv.departmentname;
		


		money_tong_bh
		
		
		
		
		
		
/*		
ĐT Viện phí
ĐT BHYT
ĐT BHYT+DV Viện phí
ĐT BHYT+DV BHYT
Tất cả		

doituongbenhnhan_vp<>1
doituongbenhnhan_vp=1
doituongbenhnhan_vp=1 	

doituongbenhnhanid
		
*/		
	
Theo ngày vào viện
Theo ngày ra viện
Theo ngày duyệt VP














	
		
		
