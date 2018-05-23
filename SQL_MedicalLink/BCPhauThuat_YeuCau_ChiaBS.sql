--Bao cao Phau thuat yeu cau chia tien cho bac si -
--BCPhauThuat_YeuCau

U11970-3701	Phẫu thuật theo yêu cầu (sử dụng kính hiển vi)
U11620-4506	Phẫu thuật mời bác sĩ chuyên gia tuyến trên
U11621-4524	Phẫu thuật yêu cầu chọn bác sĩ, chọn thời gian (PT loại ĐB+loại I)
U11622-4536	Phẫu thuật yêu cầu chọn bác sĩ, chọn thời gian (PT loại II)
U11623-4610	Phẫu thuật yêu cầu chọn bác sĩ, chọn thời gian (PT loại III)+chuyên khoa Mắt

 
 
--ngay 23/5/2018: fix loi x2 ket qua PTTT

 
SELECT row_number () over (order by A.ngay_thuchien) as stt, 
A.patientid, 
A.vienphiid, 
hsba.patientname, 
(case when hsbA.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam, 
(case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, 
hsba.bhytcode,
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
round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) - (A.mochinh_tien * (A.tyle/100)) - (A.gayme_tien * (A.tyle/100)) - (A.phu1_tien * (A.tyle/100)) - (A.phu2_tien * (A.tyle/100)) - (A.ktvphume_tien * (A.tyle/100)) - (A.dddungcu_tien * (A.tyle/100)) - (A.ddhoitinh_tien * (A.tyle/100)) - (A.ddhanhchinh_tien * (A.tyle/100)) - (A.holy_tien * (A.tyle/100)) - (A.khoachuyenden_tien * (A.tyle/100)) - (A.khoachuyenve_tien * (A.tyle/100))) as numeric),0) as lai, 
mc.username as mochinh_tenbs, 
(A.mochinh_tien * (A.tyle/100)) as mochinh_tien,  
gm.username as gayme_tenbs, 
(A.gayme_tien * (A.tyle/100)) as gayme_tien,  
p1.username as phu1_tenbs, 
(A.phu1_tien * (A.tyle/100)) as phu1_tien, 
p2.username as phu2_tenbs, 
(A.phu2_tien * (A.tyle/100)) as phu2_tien, 
ktvpm.username as ktvphume_tenbs, 
(A.ktvphume_tien * (A.tyle/100)) as ktvphume_tien, 
dddc.username as dddungcu_tenbs, 
(A.dddungcu_tien * (A.tyle/100)) as dddungcu_tien, 
ddht.username as ddhoitinh_tenbs, 
(A.ddhoitinh_tien * (A.tyle/100)) as ddhoitinh_tien, 
ktvht.username as ktvhoitinh_tenbs, 
(A.ktvhoitinh_tien * (A.tyle/100)) as ktvhoitinh_tien,
ddhc.username as ddhanhchinh_tenbs, 
(A.ddhanhchinh_tien * (A.tyle/100)) as ddhanhchinh_tien, 
hl.username as holy_tenbs, 
(A.holy_tien * (A.tyle/100)) as holy_tien, 
(A.khoachuyenden_tien * (A.tyle/100)) as khoachuyenden_tien,
kcv.departmentgroupname as khoachuyenve,
(A.khoachuyenve_tien * (A.tyle/100)) as khoachuyenve_tien,
A.ngay_vaovien, 
A.ngay_ravien, 
A.ngay_thanhtoan,
nnth.username as nguoinhapthuchien
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
		(case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename,
		(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, 
		(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, 
		(case when serf.tinhtoanlaigiadvktc=1 then 
			(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) 
		else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) 
		end) as thuoc_tronggoi, 
		(case when serf.tinhtoanlaigiadvktc=1 then 
			(select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) 
		else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0)
		end) as vattu_tronggoi, 
		(case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, 
		(case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, 
		(case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, 
		(case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, 
		(case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, 
		ser.soluong as soluong, 
		((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, 
		pttt.phauthuatvien as mochinh_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 800000 
				when 'U11620-4506' then 5000000 
				when 'U11621-4524' then 1500000 
				when 'U11622-4536' then 1300000 
				when 'U11623-4610' then 1200000
				else 0 end) * ser.soluong) as mochinh_tien,
		pttt.bacsigayme as gayme_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 350000 
				when 'U11620-4506' then 500000 
				when 'U11621-4524' then 400000 
				when 'U11622-4536' then 350000 
				when 'U11623-4610' then 325000
				else 0 end) * ser.soluong) as gayme_tien,
		pttt.phumo1 as phu1_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 250000 
				when 'U11620-4506' then 750000 
				when 'U11621-4524' then 375000 
				when 'U11622-4536' then 300000 
				when 'U11623-4610' then 250000
				else 0 end) * ser.soluong) as phu1_tien,
		pttt.phumo2 as phu2_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 250000 
				when 'U11620-4506' then 750000 
				when 'U11621-4524' then 375000 
				when 'U11622-4536' then 300000 
				when 'U11623-4610' then 250000
				else 0 end) * ser.soluong) as phu2_tien,				
		pttt.phume as ktvphume_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 175000 
				when 'U11620-4506' then 200000 
				when 'U11621-4524' then 160000 
				when 'U11622-4536' then 140000 
				when 'U11623-4610' then 130000
				else 0 end) * ser.soluong) as ktvphume_tien,		
		pttt.dungcuvien as dddungcu_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 175000 
				when 'U11620-4506' then 200000 
				when 'U11621-4524' then 160000 
				when 'U11622-4536' then 140000 
				when 'U11623-4610' then 130000
				else 0 end) * ser.soluong) as dddungcu_tien,		
		pttt.phume2 as ddhoitinh_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 0 
				when 'U11620-4506' then 30000 
				when 'U11621-4524' then 24000 
				when 'U11622-4536' then 21000 
				when 'U11623-4610' then 19500
				else 0 end) * ser.soluong) as ddhoitinh_tien,
		pttt.phumo3 as ktvhoitinh_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 0 
				when 'U11620-4506' then 30000 
				when 'U11621-4524' then 24000 
				when 'U11622-4536' then 21000 
				when 'U11623-4610' then 19500
				else 0 end) * ser.soluong) as ktvhoitinh_tien,				
		pttt.dieuduong as ddhanhchinh_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 0 
				when 'U11620-4506' then 30000 
				when 'U11621-4524' then 24000 
				when 'U11622-4536' then 21000 
				when 'U11623-4610' then 19500
				else 0 end) * ser.soluong) as ddhanhchinh_tien,
		pttt.phumo4 as holy_tenbs, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 0 
				when 'U11620-4506' then 10000 
				when 'U11621-4524' then 8000 
				when 'U11622-4536' then 7000 
				when 'U11623-4610' then 6500
				else 0 end) * ser.soluong) as holy_tien,
		((case serf.servicepricecode 
				when 'U11970-3701' then 400000 
				when 'U11620-4506' then 225000 
				when 'U11621-4524' then 200000 
				when 'U11622-4536' then 150000 
				when 'U11623-4610' then 100000
				else 0 end) * ser.soluong) as khoachuyenden_tien,
		(select mrd.nextdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenve, 
		((case serf.servicepricecode 
				when 'U11970-3701' then 0 
				when 'U11620-4506' then 225000 
				when 'U11621-4524' then 200000 
				when 'U11622-4536' then 150000 
				when 'U11623-4610' then 100000
				else 0 end) * ser.soluong) as khoachuyenve_tien,		
		vp.vienphidate as ngay_vaovien, 
		(case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, 
		(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan,
		pttt.userid as nguoinhapthuchien
	FROM (select servicepricecode, vienphiid, departmentgroupid, departmentid, servicepricedate, medicalrecordid, servicepricename,servicepricename_bhyt,servicepricename_nhandan, servicepricemoney_bhyt, servicepricemoney, loaipttt, servicepriceid, soluong, chiphidauvao, chiphimaymoc, chiphipttt, mayytedbid, loaidoituong, servicepricemoney_nhandan from serviceprice where servicepricecode in (" + lstServicecheck + ") "+_tieuchi_ser+") ser 
		left join (select (row_number() OVER (PARTITION BY servicepriceid ORDER BY servicepriceid desc)) as stt,servicepriceid, phauthuatthuthuatdate, phauthuatvien, bacsigayme, phumo1, phumo2, phume, dungcuvien, phume2, phumo3, dieuduong, phumo4, userid from phauthuatthuthuat) pttt on pttt.servicepriceid=ser.servicepriceid 
		inner join (select patientid, vienphiid, hosobenhanid, bhytid, vienphistatus, departmentgroupid, vienphidate, vienphistatus_vp, vienphidate_ravien, duyet_ngayduyet_vp from vienphi where 1=1 "+_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid 
		inner join (select tinhtoanlaigiadvktc, pttt_loaiid, servicepricecode  from servicepriceref where servicepricecode in (" + lstServicecheck + ")) serf on serf.servicepricecode=ser.servicepricecode
	WHERE coalesce(pttt.stt,1)=1 " + _tieuchi_pttt + ") A 
INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame, bhytcode from hosobenhan) hsba on hsbA.hosobenhanid=A.hosobenhanid 
LEFT JOIN departmentgroup KCHD ON KCHD.departmentgroupid=A.khoachidinh 
LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh 
LEFT JOIN departmentgroup KCD ON KCD.departmentgroupid=A.khoachuyenden 
LEFT JOIN departmentgroup krv ON krv.departmentgroupid=A.khoaravien 
LEFT JOIN nhompersonnel mc ON mc.userhisid=A.mochinh_tenbs 
LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_tenbs 
LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_tenbs 
LEFT JOIN nhompersonnel p2 ON p2.userhisid=A.phu2_tenbs 
LEFT JOIN nhompersonnel ktvpm ON ktvpm.userhisid=A.ktvphume_tenbs 
LEFT JOIN nhompersonnel dddc ON dddc.userhisid=A.dddungcu_tenbs
LEFT JOIN nhompersonnel ddht ON ddht.userhisid=A.ddhoitinh_tenbs
LEFT JOIN nhompersonnel ktvht ON ktvht.userhisid=A.ktvhoitinh_tenbs 
LEFT JOIN nhompersonnel ddhc ON ddhc.userhisid=A.ddhanhchinh_tenbs 
LEFT JOIN nhompersonnel hl ON hl.userhisid=A.holy_tenbs 
LEFT JOIN departmentgroup kcv ON kcv.departmentgroupid=A.khoachuyenve
LEFT JOIN nhompersonnel nnth ON nnth.userhisid=A.nguoinhapthuchien;


