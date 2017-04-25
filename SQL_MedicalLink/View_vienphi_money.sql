---------------View v4.9 ngay 25/4/2017 tach giuong Yeu cau-bv VietTiep

SELECT 
row_number () over (order by A.ngay_thuchien desc) as stt, 
A.patientid, 
A.vienphiid, 
hsbA.patientname, 
(case when hsbA.gioitinhcode='01' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nam, 
(case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, 
bh.bhytcode, 
kchd.departmentgroupname as khoachidinh, 
pcd.departmentname as phongchidinh, 
A.ngay_chidinh, 
A.ngay_thuchien, 
kcd.departmentgroupname as khoachuyenden, 
krv.departmentgroupname as khoaravien, 
A.servicepricecode, 
A.servicepricename, 
A.loaipttt_db, 
A.loaipttt_l1, 
A.loaipttt_l2, 
A.loaipttt_l3, 
A.loaipttt, 
A.soluong, 
A.servicepricefee, 
A.tyle, 
round(cast(A.thuoc_tronggoi as numeric),0) as thuoc_tronggoi, 
round(cast(A.vattu_tronggoi as numeric),0) as vattu_tronggoi, 
round(cast(A.chiphikhac as numeric),0) AS chiphikhac, 
(A.servicepricefee * A.soluong) as thanhtien, 
round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, 
mc.username as mochinh_tenbs, 
(A.mochinh_tien * (A.tyle/100)) as mochinh_tien, 
mmc.username as moimochinh_tenbs, 
(A.moimochinh_tien * (A.tyle/100)) as moimochinh_tien, 
gm.username as gayme_tenbs, 
(A.gayme_tien * (A.tyle/100)) as gayme_tien, 
mgm.username as moigayme_tenbs, 
(A.moigayme_tien * (A.tyle/100)) as moigayme_tien, 
pm.username as phume_tenbs, 
(A.phume_tien * (A.tyle/100)) as phume_tien, 
p1.username as phu1_tenbs, 
(A.phu1_tien * (A.tyle/100)) as phu1_tien, 
p2.username as phu2_tenbs, 
(A.phu2_tien * (A.tyle/100)) as phu2_tien, 
gv1.username as giupviec1_tenbs, 
(A.giupviec1_tien * (A.tyle/100)) as giupviec1_tien, 
gv2.username as giupviec2_tenbs, 
(A.giupviec2_tien * (A.tyle/100)) as giupviec2_tien, 
A.ngay_vaovien, 
A.ngay_ravien, 
A.ngay_thanhtoan 
FROM 
	(SELECT vp.patientid, 
	vp.vienphiid, 
	vp.hosobenhanid, 
	vp.bhytid, 
	ser.departmentgroupid as khoachidinh, 
	ser.departmentid as phongchidinh, 
	ser.servicepricedate as ngay_chidinh, 
	pttt.phauthuatthuthuatdate as ngay_thuchien, 
	(select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, 
	(case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, 
	ser.servicepricecode, 
	ser.servicepricename, 
	(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, 
	(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, 
	(case when serf.tinhtoanlaigiadvktc=1 
				then (select sum(case when ser_dikem.maubenhphamphieutype=0 
										then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong 
									  else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end)
					from serviceprice ser_dikem 
					where ser_dikem.servicepriceid_master=ser.servicepriceid 
							and ser_dikem.loaidoituong<>2
							and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle'))
		 else (select sum(case when ser_dikem.maubenhphamphieutype=0 
						then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong 
					else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end)
				from serviceprice ser_dikem 
				where ser_dikem.servicepriceid_master=ser.servicepriceid 
					and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) end) as thuoc_tronggoi,
	(case when serf.tinhtoanlaigiadvktc=1 
				then (select sum(case when ser_dikem.maubenhphamphieutype=0 
										then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong 
									  else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end)
					from serviceprice ser_dikem 
					where ser_dikem.servicepriceid_master=ser.servicepriceid 
							and ser_dikem.loaidoituong<>2
							and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle'))
		 else (select sum(case when ser_dikem.maubenhphamphieutype=0 
						then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong 
					else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end)
				from serviceprice ser_dikem 
				where ser_dikem.servicepriceid_master=ser.servicepriceid 
					and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) end) as vattu_tronggoi,
	(case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, 
	(case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, 
	(case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, 
	(case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, 
	(case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, 
	ser.soluong as soluong, 
	((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, 
	pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 
	(case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 else 0 end) as moimochinh_tenbs, 
	(case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 
	pttt.bacsigayme as gayme_tenbs, 
	((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as gayme_tien, 
	pttt.phume2 as moigayme_tenbs, 
	(case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, 
	pttt.phume as phume_tenbs, 
	((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phume_tien, 
	pttt.phumo1 as phu1_tenbs, 
	((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as phu1_tien, 
	pttt.phumo2 as phu2_tenbs, 
	((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 0 when 4 then 0 else 0 end) * ser.soluong) as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 when 5 then 36000 when 6 then 21000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 
	pttt.phumo4 as giupviec2_tenbs, 
	((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 0 else 0 end) * ser.soluong) as giupviec2_tien, 
	vp.vienphidate as ngay_vaovien, 
	(case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, 
	(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan 
	FROM serviceprice ser 
		left join phauthuatthuthuat pttt on pttt.servicepriceid=ser.servicepriceid 
		inner join vienphi vp on vp.vienphiid=ser.vienphiid 
		inner join servicepriceref serf on serf.servicepricecode=ser.servicepricecode 
	WHERE serf.servicegrouptype=4 
		and serf.bhyt_groupcode in ('06PTTT','07KTC') 
		and ser.bhyt_groupcode in ('06PTTT','07KTC') 
		and " + serf_pttt_loaiid + " 
		and " + ser_departmentid + " 
		and " + tieuchi_date + ") A 
INNER JOIN hosobenhan hsba on hsbA.hosobenhanid=A.hosobenhanid 
INNER JOIN bhyt bh on bh.bhytid=A.bhytid 
LEFT JOIN departmentgroup KCHD ON KCHD.departmentgroupid=A.khoachidinh 
LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh 
LEFT JOIN departmentgroup KCD ON KCD.departmentgroupid=A.khoachuyenden 
LEFT JOIN departmentgroup krv ON krv.departmentgroupid=A.khoaravien 
LEFT JOIN tools_tblnhanvien mc ON mc.userhisid=A.mochinh_tenbs 
LEFT JOIN tools_tblnhanvien mmc ON mmc.userhisid=A.moimochinh_tenbs 
LEFT JOIN tools_tblnhanvien gm ON gm.userhisid=A.gayme_tenbs 
LEFT JOIN tools_tblnhanvien mgm ON mgm.userhisid=A.moigayme_tenbs 
LEFT JOIN tools_tblnhanvien pm ON pm.userhisid=A.phume_tenbs 
LEFT JOIN tools_tblnhanvien p1 ON p1.userhisid=A.phu1_tenbs 
LEFT JOIN tools_tblnhanvien p2 ON p2.userhisid=A.PHU2_TENBS 
LEFT JOIN tools_tblnhanvien gv1 ON gv1.userhisid=A.giupviec1_tenbs 
LEFT JOIN tools_tblnhanvien gv2 ON gv2.userhisid=A.giupviec2_tenbs; 







---------------View v4.8 ngay 15/3/2017 tach giuong Yeu cau-bv VietTiep
CREATE OR REPLACE VIEW vienphi_money AS 
SELECT vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, vp.vienphistatus_bh,vp.duyet_ngayduyet_bh,vp.bhyt_tuyenbenhvien,
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (0,4,6) 
			then (case when vp.loaivienphiid=0 and (ser.lankhambenh = 0 or ser.lankhambenh is null)
							then ser.servicepricemoney_bhyt*ser.soluong
						when vp.loaivienphiid=1 
							then ser.servicepricemoney_bhyt*ser.soluong
						else 0 end)
		else 0 end) as money_khambenh_bh,		
sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-ser.servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_khambenh_vp,
sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_xetnghiem_bh,
sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_xetnghiem_vp,
sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_cdha_bh,
sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_cdha_vp,	 
sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_tdcn_bh,
sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_tdcn_vp,	 	 
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_pttt_bh,
sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_pttt_vp,	 
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_mau_bh,
sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
	when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
	 when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
	 else 0 end) as money_mau_vp,
-----------	 
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

---------	 
sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_vanchuyen_bh,
sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_vanchuyen_vp,	 	 
sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_khac_bh,
sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_khac_vp,	 	 
sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) as money_phuthu_bh,
sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
	 when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
	 when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
	 else 0 end) as money_phuthu_vp,	 
sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) 
		then ser.servicepricemoney_bhyt*ser.soluong else 0 end) 
+ sum(case when ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=vp.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1)
		then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan * ser.soluong) end)
		else 0 end) as money_dvktc_bh,	
sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end)
		when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) 
			then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end)
		when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 
			then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end)
		else 0 end) as money_dvktc_vp, 
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_thuoc_bh,
sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
	when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
	 when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
	 else 0 end) as money_thuoc_vp,
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (0,4,6) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_vattu_bh,
sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end)
	when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong else 0-((ser.servicepricemoney-servicepricemoney_bhyt)*ser.soluong) end) end)		
	 when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 then 
		(case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end)
	 else 0 end) as money_vattu_vp,
(select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung					
FROM vienphi vp left join serviceprice ser on vp.vienphiid=ser.vienphiid
WHERE vp.vienphidate >'2014-01-01 00:00:00' 
and ser.thuockhobanle=0 
--and (ser.lankhambenh = 0 or ser.lankhambenh is null)
GROUP BY vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp
ORDER BY vp.vienphiid DESC;
