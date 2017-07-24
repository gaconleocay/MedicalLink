--bao cao so chi tiet benh nhan ngay 23/7
--chinh sua % BN thanh toan va BHYT thahn toan


SELECT (rank() OVER (PARTITION BY VMS.khoanoitrudautien ORDER BY VMS.vienphidate)) as stt,
		VMS.khoanoitrudautien,
		DEGP.departmentgroupname,
		HSBA.patientcode,
		HSBA.patientname,
		HSBA.namsinh,
		HSBA.gioitinhname,
		BH.bhytcode,
		BH.macskcbbd,
		VMS.vienphidate,
		TO_CHAR(VMS.vienphidate_ravien,'yyyy-MM-dd HH:mm:ss') as vienphidate_ravien,
		VMS.chandoanravien_code,
		VMS.vienphiid,
		VMS.duyet_sothutuduyet_vp,
		TO_CHAR(VMS.duyet_ngayduyet_vp,'yyyy-MM-dd HH:mm:ss') as duyet_ngayduyet_vp,
		VMS.bhyt_tuyenbenhvien,
		BH.bhyt_loaiid,
		VMS.loaivienphiid,
		BH.du5nam6thangluongcoban,
		VMS.money_xetnghiem,
		VMS.money_cdhatdcn,
		VMS.money_thuoc,
		VMS.money_mau,
		VMS.money_pttt,
		VMS.money_vattu,
		VMS.money_dvktc,
		VMS.money_khambenh,
		VMS.money_giuong,
		VMS.money_vanchuyen,
		VMS.money_khac,
		(VMS.money_tong_bh+VMS.money_tong_vp) as money_tongcong,
		VMS.money_tong_bh,
		VMS.money_tong_vp,
		(case when VMS.doituongbenhnhanid=5 then (VMS.money_xetnghiem + VMS.money_cdhatdcn + VMS.money_thuoc + VMS.money_mau + VMS.money_pttt + VMS.money_vattu + VMS.money_dvktc + VMS.money_khambenh + VMS.money_giuong + VMS.money_vanchuyen + VMS.money_khac) else 0 end) as money_mienphi,
		0 as money_bnthanhtoan,
		0 as money_bhytthanhtoan,
		VMS.money_haophi,
		BIL.hoadon_thutien,
		BIL.thutien_kytruoc,
		BIL.tamung_kytruoc,
		BIL.tamung_trongky,
		BIL.thutien_trongky,
		BIL.hoanung_trongky,
		0 as tylenop,
		(BIL.thutien_kytruoc + BIL.thutien_trongky + BIL.tamung_kytruoc + BIL.tamung_trongky - BIL.hoanung_trongky) as soducuoiky
FROM 	(select vienphiid,patientid,bhytid,hosobenhanid,loaivienphiid,vienphistatus,khoanoitrudautien,departmentid,doituongbenhnhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,chandoanravien_code,duyet_sothutuduyet_vp,bhyt_tuyenbenhvien,
				(money_xetnghiem_bh + money_xetnghiem_vp) as money_xetnghiem,
				(money_cdha_bh + money_cdha_vp + money_tdcn_bh + money_tdcn_vp) as money_cdhatdcn,
				(money_thuoc_bh + money_thuoc_vp) as money_thuoc,
				(money_mau_bh + money_mau_vp) as money_mau,
				(money_pttt_bh + money_pttt_vp) as money_pttt,
				(money_vattu_bh + money_vattu_vp) as money_vattu,
				(money_dvktc_bh + money_dvktc_vp) as money_dvktc,
				(money_khambenh_bh + money_khambenh_vp) as money_khambenh,
				(money_giuong_bh + money_giuong_vp) as money_giuong,
				(money_vanchuyen_bh + money_vanchuyen_vp) as money_vanchuyen,
				(money_khac_bh + money_khac_vp + money_phuthu_bh + money_phuthu_vp) as money_khac,
				money_haophi,
				(money_khambenh_bh+money_xetnghiem_bh+money_cdha_bh+money_tdcn_bh+money_thuoc_bh+money_mau_bh+money_pttt_bh+money_vattu_bh+money_dvktc_bh+money_giuong_bh+money_vanchuyen_bh+money_khac_bh+money_phuthu_bh) as money_tong_bh,
				(money_khambenh_vp+money_xetnghiem_vp+money_cdha_vp+money_tdcn_vp+money_thuoc_vp+money_mau_vp+money_pttt_vp+money_vattu_vp+money_dvktc_vp+money_giuong_vp+money_vanchuyen_vp+money_khac_vp+money_phuthu_vp) as money_tong_vp
			from vienphi_money_sobn 
			where " + tieuchi_date + trangthaibenhan + " ) VMS
		LEFT JOIN 
			(select b.vienphiid,
				STRING_AGG(case when b.loaiphieuthuid=0
							then (b.billgroupcode || '/' || b.billcode || '/' || round(cast(b.datra as numeric),0))
						 end, '; ') as hoadon_thutien, 
				sum(case when b.loaiphieuthuid=0 and b.billdate<'" + tungay + "'
							then b.datra
						else 0 end) as thutien_kytruoc,
				sum(case when b.loaiphieuthuid=2 and b.billdate<'" + tungay + "'
							then b.datra
						else 0 end) as tamung_kytruoc,
				sum(case when b.loaiphieuthuid=2 and b.billdate between '" + tungay + "' and '" + denngay + "'
							then b.datra
						else 0 end) as tamung_trongky,
				sum(case when b.loaiphieuthuid=0 and b.billdate between '" + tungay + "' and '" + denngay + "'
							then b.datra
						else 0 end) as thutien_trongky,
				sum(case when b.loaiphieuthuid=1 and b.billdate between '" + tungay + "' and '" + denngay + "'
							then b.datra
						else 0 end) as hoanung_trongky		
			from bill b
			where b.dahuyphieu=0 
			group by b.vienphiid) BIL on BIL.vienphiid=VMS.vienphiid
		LEFT JOIN 	
			(select hosobenhanid,patientcode,patientname,to_char(birthday, 'yyyy') as namsinh,gioitinhname from hosobenhan) HSBA on HSBA.hosobenhanid=VMS.hosobenhanid
		INNER JOIN (select bhytid,bhytcode,macskcbbd,bhyt_loaiid,du5nam6thangluongcoban   from bhyt) BH on BH.bhytid=VMS.bhytid	
		INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) DEGP on DEGP.departmentgroupid=VMS.khoanoitrudautien;

		
		
				
		
		
		
		
		
		










/*
Số tiền bệnh nhân phải thanh toán
Số tiền sẽ được BHXH thanh toán
Tổng Hao Phí
Hóa đơn	Số tiền thu kỳ trước
Số tiền tạm ứng dư đầu kỳ
Số tiền tam ứng trong kỳ
Số tiền thu trong kỳ
Số tiền chi trong kỳ
Tỷ lệ nộp
Số tiền dư cuối kỳ											
TinhMucHuongTheoTheBHYT(string bhytcode, int bhyt_loaiid, int loaivienphiid, int du5nam6thangluongcoban, int bhyt_tuyenbenhvien)												
	*/											

											
												
												