--Bao cao Phau thuat yeu cau chia tien cho bac si - QD1055
--BCPhauThuat_YeuCauQD1055

U18851-4427	Phẫu thuật yêu cầu Tán sỏi ngoài cơ thể
U19154-1951	Phẫu thuật, thủ thuật sử dụng dịch vụ chất lượng cao
U18765-1454	Phẫu thuật yêu cầu kỹ thuật cao trong mổ tiêu hóa
U17261-2902	Phẫu thuật yêu cầu kỹ thuật cao trong mổ cấp cứu
U17265-3936	Phẫu thuật yêu cầu sử dụng dao siêu âm trong mổ tiêu hóa (cắt dạ dày, khối tá tụy, đại tràng, trực tràng)
U30001-5346	Phẫu thuật yêu cầu mổ dao siêu âm (Loại 1)
U30001-1207	Phẫu thuật yêu cầu mổ dao siêu âm (Loại 2)
U30001-2954	Phẫu thuật yêu cầu dao siêu âm (Loại 3)

 -----
 --ngay 27/11/2018
 
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
round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) - (A.mochinh_tien * (A.tyle/100)) - ((A.gayme_tien * (A.tyle/100))*4)) as numeric),0) as lai, 
mc.username as mochinh_tenbs, 
(A.mochinh_tien * (A.tyle/100)) as mochinh_tien,  
gm.username as gayme_tenbs, 
(A.gayme_tien * (A.tyle/100)) as gayme_tien,  
p1.username as phu1_tenbs, 
(A.gayme_tien * (A.tyle/100)) as phu1_tien, 
(A.gayme_tien * (A.tyle/100)) as khoachuyenden_tien,
(A.gayme_tien * (A.tyle/100)) as banlanhdao_tien,
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
		((case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee * ser.soluong * 0.25) as mochinh_tien,
		pttt.bacsigayme as gayme_tenbs, 
		((case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee * ser.soluong * 0.05) as gayme_tien,			
		pttt.phumo1 as phu1_tenbs,  		
		vp.vienphidate as ngay_vaovien, 
		(case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, 
		(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan,
		pttt.userid as nguoinhapthuchien
	FROM (select servicepricecode,vienphiid,departmentgroupid,departmentid,servicepricedate,medicalrecordid,servicepricename,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney,loaipttt,servicepriceid,soluong, chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid,loaidoituong,servicepricemoney_nhandan from serviceprice where servicepricecode in (" + lstServicecheck + ") "+_tieuchi_ser+") ser 
		left join (select (row_number() OVER (PARTITION BY servicepriceid ORDER BY phauthuatthuthuatid desc)) as stt,servicepriceid, phauthuatthuthuatdate,phauthuatvien,bacsigayme,phumo1,phumo2,phume,dungcuvien,phume2,phumo3,dieuduong,phumo4,userid from phauthuatthuthuat) pttt on pttt.servicepriceid=ser.servicepriceid 
		inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphistatus_vp, vienphidate_ravien,duyet_ngayduyet_vp from vienphi where 1=1 "+_tieuchi_vp+") vp on vp.vienphiid=ser.vienphiid 
		inner join (select tinhtoanlaigiadvktc,pttt_loaiid,servicepricecode from servicepriceref where servicepricecode in (" + lstServicecheck + ")) serf on serf.servicepricecode=ser.servicepricecode
	WHERE coalesce(pttt.stt,1)=1 " + _tieuchi_pttt + ") A 
INNER JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname, hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba on hsbA.hosobenhanid=A.hosobenhanid 
LEFT JOIN departmentgroup KCHD ON KCHD.departmentgroupid=A.khoachidinh 
LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh 
LEFT JOIN departmentgroup KCD ON KCD.departmentgroupid=A.khoachuyenden 
LEFT JOIN departmentgroup krv ON krv.departmentgroupid=A.khoaravien 
LEFT JOIN nhompersonnel mc ON mc.userhisid=A.mochinh_tenbs 
LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_tenbs 
LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_tenbs 
LEFT JOIN nhompersonnel nnth ON nnth.userhisid=A.nguoinhapthuchien;


