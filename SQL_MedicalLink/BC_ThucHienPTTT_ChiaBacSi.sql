---Bao cao PTTT chia tien theo bac si ngay 19/7
--Chia ra tung loai bao cao rieng biet (dung bien)
--them chan doan chi dinh
--Chinh sua ten theo ten cua doituongthanhtoan

SELECT row_number () over (order by A.ngay_thuchien) as stt, 
A.patientid, 
A.vienphiid, 
hsbA.patientname, 
(case when hsbA.gioitinhcode='01' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nam, 
(case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, hsba.bhytcode, 
((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
kchd.departmentgroupname as khoachidinh, 
pcd.departmentname as phongchidinh, 
A.ngay_chidinh, 
A.ngay_thuchien, 
kcd.departmentgroupname as khoachuyenden, 
krv.departmentgroupname as khoaravien, 
mbp.chandoan as cd_chidinh,
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
A.ngay_thanhtoan,
nnth.username as nguoinhapthuchien
FROM 
	(SELECT vp.patientid, 
	vp.vienphiid, 
	vp.hosobenhanid, 
	vp.bhytid, 
	ser.maubenhphamid,
	ser.departmentgroupid as khoachidinh, 
	ser.departmentid as phongchidinh, 
	ser.servicepricedate as ngay_chidinh, 
	pttt.phauthuatthuthuatdate as ngay_thuchien, 
	(select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, 
	(case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, 
	ser.servicepricecode, 
	(case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, 
			--ser.servicepricename, 
	(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, 
	(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, 
	(case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) end) as thuoc_tronggoi, 
	(case when serf.tinhtoanlaigiadvktc=1 
			then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) 
		else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) 
	    end) as vattu_tronggoi, 
	(case serf.pttt_loaiid 
			when 1 then 'Phẫu thuật đặc biệt' 
			when 2 then 'Phẫu thuật loại 1' 
			when 3 then 'Phẫu thuật loại 2' 
			when 4 then 'Phẫu thuật loại 3' 
			when 5 then 'Thủ thuật đặc biệt' 
			when 6 then 'Thủ thuật loại 1' 
			when 7 then 'Thủ thuật loại 2' 
			when 8 then 'Thủ thuật loại 3' 
			else '' end) as loaipttt, 
	(case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, 
	(case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, 
	(case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, 
	(case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, 
	ser.soluong as soluong, 
	((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, 
			" + baocaotungloai + "
	vp.vienphidate as ngay_vaovien, 
	(case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, 
	(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan,
	pttt.userid as nguoinhapthuchien
	FROM serviceprice ser 
	left join phauthuatthuthuat pttt on pttt.servicepriceid=ser.servicepriceid 
	inner join (select patientid, vienphiid, hosobenhanid, bhytid, vienphistatus, departmentgroupid, vienphidate, vienphidate_ravien, vienphistatus_vp, duyet_ngayduyet_vp from vienphi) vp on vp.vienphiid=ser.vienphiid 
	inner join (select servicepricecode, tinhtoanlaigiadvktc, pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') and " + serf_pttt_loaiid + ") serf on serf.servicepricecode=ser.servicepricecode 
	WHERE ser.bhyt_groupcode in ('06PTTT','07KTC') 
		and " + ser_departmentid + " and " + tieuchi_date + ") A 
INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsbA.hosobenhanid=A.hosobenhanid 
INNER JOIN (select maubenhphamid, chandoan from maubenhpham) mbp on mbp.maubenhphamid=A.maubenhphamid 
LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh 
LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh 
LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden 
LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien 
LEFT JOIN tools_tblnhanvien mc ON mc.userhisid=A.mochinh_tenbs 
LEFT JOIN tools_tblnhanvien mmc ON mmc.userhisid=A.moimochinh_tenbs 
LEFT JOIN tools_tblnhanvien gm ON gm.userhisid=A.gayme_tenbs 
LEFT JOIN tools_tblnhanvien mgm ON mgm.userhisid=A.moigayme_tenbs 
LEFT JOIN tools_tblnhanvien p1 ON p1.userhisid=A.phu1_tenbs 
LEFT JOIN tools_tblnhanvien p2 ON p2.userhisid=A.PHU2_TENBS 
LEFT JOIN tools_tblnhanvien gv1 ON gv1.userhisid=A.giupviec1_tenbs 
LEFT JOIN tools_tblnhanvien gv2 ON gv2.userhisid=A.giupviec2_tenbs
LEFT JOIN tools_tblnhanvien nnth ON nnth.userhisid=A.nguoinhapthuchien;






-------Tung loai
-----===========Phau thuat khoa Gay me
pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 280000 
			when 2 then 125000 
			when 3 then 65000 
			when 4 then 50000 
			else 0 end) * ser.soluong) as mochinh_tien, 
	(case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, 
	(case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid 
												when 1 then 280000 
												when 2 then 125000 
												when 3 then 65000 
												else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 
	pttt.bacsigayme as gayme_tenbs, 
	((case serf.pttt_loaiid 
					when 1 then 280000 
					when 2 then 125000 
					when 3 then 65000 
					when 4 then 50000 
					else 0 end) * ser.soluong) as gayme_tien, 
	pttt.phume2 as moigayme_tenbs, 
	(case when pttt.phume2>0 then ((case serf.pttt_loaiid 
										when 1 then 280000 
										when 2 then 125000 
										when 3 then 65000 
										else 0 end) * ser.soluong) else 0 end) as moigayme_tien, 
	pttt.phumo1 as phu1_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 200000 
			when 2 then 90000 
			when 3 then 50000 
			when 4 then 30000 
			else 0 end) * ser.soluong) as phu1_tien, 
	pttt.phumo2 as phu2_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 200000 
			when 2 then 90000 
			else 0 end) * ser.soluong) as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 120000 
			when 2 then 70000 
			when 3 then 30000 
			when 4 then 15000 
			else 0 end) * ser.soluong) as giupviec1_tien, 
	pttt.phumo4 as giupviec2_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 120000 
			when 2 then 70000 
			when 3 then 30000 
			else 0 end) * ser.soluong) as giupviec2_tien, 
			 
-----PHau thuat khoa Tai Mui Hong 
pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid 
			when 2 then 125000 
			when 3 then 65000 
			when 4 then 50000 
			else 0 end) * ser.soluong) as mochinh_tien, 
	(case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, 
	(case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid 
												when 1 then 280000 
												when 2 then 125000 
												when 3 then 65000 
												else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 
	0 as gayme_tenbs, 
	0 as gayme_tien, 
	0 as moigayme_tenbs, 
	0 as moigayme_tien, 
	pttt.phumo1 as phu1_tenbs, 
	((case serf.pttt_loaiid 
			when 2 then 90000 
			when 3 then 50000 
			when 4 then 30000 
			else 0 end) * ser.soluong) as phu1_tien, 
	pttt.phumo2 as phu2_tenbs, 
	((case serf.pttt_loaiid 
			when 2 then 90000 
			else 0 end) * ser.soluong) as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid 
			when 2 then 70000 
			when 3 then 30000 
			when 4 then 15000 
			else 0 end) * ser.soluong) as giupviec1_tien, 
	pttt.phumo4 as giupviec2_tenbs, 
	((case serf.pttt_loaiid 
			when 2 then 70000 
			when 3 then 30000 
			else 0 end) * ser.soluong) as giupviec2_tien, 
	 
-----PHau thuat khoa Rang Ham Mat
pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 280000 
			when 2 then 125000 
			when 3 then 65000 
			when 4 then 50000 
			else 0 end) * ser.soluong) as mochinh_tien, 
	(case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, 
	(case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid 
												when 1 then 280000 
												when 2 then 125000 
												when 3 then 65000 
												else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 
	0 as gayme_tenbs, 
	0 as gayme_tien, 
	0 as moigayme_tenbs, 
	0 as moigayme_tien, 
	pttt.phumo1 as phu1_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 200000 
			when 2 then 90000 
			when 3 then 50000 
			when 4 then 30000 
			else 0 end) * ser.soluong) as phu1_tien, 
	0 as phu2_tenbs, 
	0 as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 120000 
			when 2 then 70000 
			when 3 then 30000 
			when 4 then 15000 
			else 0 end) * ser.soluong) as giupviec1_tien, 
	0 as giupviec2_tenbs, 
	0 as giupviec2_tien, 
		
 
-----===========Phau thuat khoa Mat
pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 280000 
			when 2 then 125000 
			when 3 then 65000 
			when 4 then 50000 
			else 0 end) * ser.soluong) as mochinh_tien, 
	(case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, 
	(case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid 
												when 1 then 280000 
												when 2 then 125000 
												when 3 then 65000 
												else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 
	pttt.bacsigayme as gayme_tenbs, 
	((case serf.pttt_loaiid 
					when 1 then 200000 
					when 2 then 90000 
					when 3 then 50000 
					when 4 then 30000 
					else 0 end) * ser.soluong) as gayme_tien, 
	pttt.phume2 as moigayme_tenbs, 
	(case when pttt.phume2>0 then ((case serf.pttt_loaiid 
										when 1 then 200000 
										when 2 then 90000 
										when 3 then 50000 
										else 0 end) * ser.soluong) else 0 end) as moigayme_tien, 
	pttt.phumo1 as phu1_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 200000 
			when 2 then 90000 
			when 3 then 50000 
			when 4 then 30000 
			else 0 end) * ser.soluong) as phu1_tien, 
	0 as phu2_tenbs, 
	0 as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid 
			when 1 then 120000 
			when 2 then 70000 
			when 3 then 30000 
			when 4 then 15000 
			else 0 end) * ser.soluong) as giupviec1_tien, 
	0 as giupviec2_tenbs, 
	0 as giupviec2_tien, 
 
 
 
--------------================================Thu thuat Khoa mat

	pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid 
			when 5 then 84000 
			when 6 then 37500 
			when 7 then 19500 
			when 8 then 15000 
			else 0 end) * ser.soluong) as mochinh_tien, 
	0 as moimochinh_tenbs, 
	0 as moimochinh_tien, 
	0 as gayme_tenbs, 
	0 as gayme_tien, 
	0 as moigayme_tenbs, 
	0 as moigayme_tien, 
	0 as phu1_tenbs, 
	0 as phu1_tien, 
	0 as phu2_tenbs, 
	0 as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid 
			when 5 then 36000 
			when 6 then 21000 
			when 7 then 9000 
			when 8 then 4500 
			else 0 end) * ser.soluong) as giupviec1_tien, 
	0 as giupviec2_tenbs, 
	0 as giupviec2_tien, 
 
 --------------================================Thu thuat cac khoa khac
 
 	pttt.phauthuatvien as mochinh_tenbs, 
	((case serf.pttt_loaiid 
			when 5 then 84000 
			when 6 then 37500 
			when 7 then 19500 
			when 8 then 15000 
			else 0 end) * ser.soluong) as mochinh_tien, 
	0 as moimochinh_tenbs, 
	0 as moimochinh_tien, 
	0 as gayme_tenbs, 
	0 as gayme_tien, 
	0 as moigayme_tenbs, 
	0 as moigayme_tien, 
	pttt.phumo1 as phu1_tenbs, 
	((case serf.pttt_loaiid 
			when 5 then 60000 
			when 6 then 27000 
			else 0 end) * ser.soluong) as phu1_tien, 
	0 as phu2_tenbs, 
	0 as phu2_tien, 
	pttt.phumo3 as giupviec1_tenbs, 
	((case serf.pttt_loaiid 
			when 5 then 36000 
			when 7 then 9000 
			when 8 then 4500 
			else 0 end) * ser.soluong) as giupviec1_tien, 
	0 as giupviec2_tenbs, 
	0 as giupviec2_tien, 
	
	
	
	








---Báo cáo Thực hiện PTTT Chuyên khoa_ngày 17/5/2017: 
--thêm địa chỉ
--sửa lại thuốc/vật tư trong gói không tính tiền

SELECT row_number () over (order by A.ngay_thuchien desc) as stt, 
A.patientid, 
A.vienphiid, 
hsbA.patientname, 
(case when hsbA.gioitinhcode='01' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nam, 
(case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, 
bh.bhytcode,
((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) ||
		hc_quocgianame) as diachi, 
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
		(case when serf.tinhtoanlaigiadvktc=1 then 
			(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) 
		else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) 
		end) as thuoc_tronggoi, 
		(case when serf.tinhtoanlaigiadvktc=1 then 
			(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) 
		else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle'))
		end) as vattu_tronggoi, 
		(case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, 
		(case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, 
		(case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, 
		(case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, 
		(case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, 
		ser.soluong as soluong, 
		((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, 
		pttt.phauthuatvien as mochinh_tenbs, 
		((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 
		(case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, 
		(case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 	
		pttt.bacsigayme as gayme_tenbs, 
		(case when ser.departmentid in (212,335) 
				then ((case serf.pttt_loaiid 
							when 1 then 200000 
							when 2 then 90000 
							when 3 then 50000 
							when 4 then 30000 
							else 0 end) * ser.soluong)
			  else ((case serf.pttt_loaiid 
							when 1 then 280000 
							when 2 then 125000 
							when 3 then 65000 
							when 4 then 50000 
							else 0 end) * ser.soluong) end) as gayme_tien, 			
		pttt.phume2 as moigayme_tenbs, 
		(case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, 
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
LEFT JOIN tools_tblnhanvien p1 ON p1.userhisid=A.phu1_tenbs 
LEFT JOIN tools_tblnhanvien p2 ON p2.userhisid=A.PHU2_TENBS 
LEFT JOIN tools_tblnhanvien gv1 ON gv1.userhisid=A.giupviec1_tenbs 
LEFT JOIN tools_tblnhanvien gv2 ON gv2.userhisid=A.giupviec2_tenbs;  
















