---Bao cao PTTT chia tien theo bac si chi tiet thuoc/vt trong goi
--ucBC48_PhauThuatThuThuatChiTiet

--ngay 12/9/2018: fix bhytcode; them tieu chi + them chi tiet thuoc/vat tu trong goi



SELECT row_number () over (order by A.ngay_chidinh) as stt, 
	A.servicepriceid,
	coalesce(A.duyetpttt_stt,0) as duyetpttt_stt,
	(case A.duyetpttt_stt
			when 1 then 'Đã gửi YC' 
			when 2 then 'Đã tiếp nhận YC' 
			when 3 then 'Đã duyệt PTTT' 
			when 99 then 'Đã khóa' 
			else 'Chưa gửi YC' end) as duyetpttt_sttname,
	(case when A.duyetpttt_stt in (3,99) then A.duyetpttt_date end) as duyetpttt_date,
	(case when A.duyetpttt_stt in (3,99) then A.duyetpttt_usercode end) as duyetpttt_usercode,
	(case when A.duyetpttt_stt in (3,99) then A.duyetpttt_username end) as duyetpttt_username,
	A.patientid, 
	A.vienphiid, 
	A.maubenhphamid,
	A.bhyt_groupcode,
	hsbA.patientname, 
	(case when hsbA.gioitinhcode='01' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nam, 
	(case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, 
	bh.bhytcode, 
	((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, 
	kchd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	TO_CHAR(A.ngay_chidinh,'HH24:MI dd/MM/yyyy') as ngay_chidinh, 
	A.servicepricecode, 
	A.servicepricename, 
	A.loaipttt_db, 
	A.loaipttt_l1, 
	A.loaipttt_l2, 
	A.loaipttt_l3, 
	A.loaipttt, 
	A.tinhtoanlaigiadvktc,
	A.soluong, 
	A.servicepricefee, 
	A.tyle, 
	round(cast(A.thuoc_tronggoi as numeric),0) as thuoc_tronggoi, 
	round(cast(A.vattu_tronggoi as numeric),0) as vattu_tronggoi, 
	(A.servicepricefee * A.soluong) as thanhtien, 
	TO_CHAR(A.ngay_vaovien, 'HH24:MI dd/MM/yyyy') as ngay_vaovien, 
	TO_CHAR(A.ngay_ravien, 'HH24:MI dd/MM/yyyy') as ngay_ravien, 
	TO_CHAR(A.ngay_thanhtoan, 'HH24:MI dd/MM/yyyy') as ngay_thanhtoan,
	'' as thuoc_servicepricecode,
	'' as thuoc_servicepricename,
	'' as thuoc_soluong,
	'' as thuoc_servicepricefee,
	'' as thuoc_thanhtien,
	'' as vattu_servicepricecode,
	'' as vattu_servicepricename,
	'' as vattu_soluong,
	'' as vattu_servicepricefee,
	'' as vattu_thanhtien
FROM 
	(SELECT vp.patientid, 
		vp.vienphiid, 
		vp.hosobenhanid, 
		vp.bhytid, 
		ser.servicepriceid,
		ser.duyetpttt_stt,
		ser.duyetpttt_date,
		ser.duyetpttt_usercode,
		ser.duyetpttt_username,
		ser.maubenhphamid,
		ser.bhyt_groupcode,
		ser.departmentgroupid as khoachidinh, 
		ser.departmentid as phongchidinh, 
		ser.servicepricedate as ngay_chidinh, 
		(case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, 
		ser.servicepricecode, 
		(case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, 
		(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, 
		(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, 
		0 as thuoc_tronggoi,
		0 as vattu_tronggoi,
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
		serf.tinhtoanlaigiadvktc,
		ser.soluong as soluong,
		vp.vienphidate as ngay_vaovien, 
		(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as ngay_ravien, 
		(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan
	FROM (select servicepriceid,vienphiid,duyetpttt_stt,duyetpttt_date,duyetpttt_usercode,duyetpttt_username,maubenhphamid,bhyt_groupcode,departmentgroupid,departmentid,servicepricedate,medicalrecordid,servicepricecode,loaidoituong,servicepricename,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') {_trangthaipttt} {_tieuchi_ser} {_departmentid_ser}) ser
	inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1{_tieuchi_vp} {_doituongbenhnhanid}) vp on vp.vienphiid=ser.vienphiid 
	inner join (select servicepricecode,tinhtoanlaigiadvktc,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') {_pttt_loaiid_serf}) serf on serf.servicepricecode=ser.servicepricecode 
	) A 
INNER JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsbA.hosobenhanid=A.hosobenhanid 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kchd ON kchd.departmentgroupid=A.khoachidinh 
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=A.phongchidinh
INNER JOIN (select bhytid,bhytcode from bhyt where 1=1 {_tieuchi_bhyt}) bh on bh.bhytid=A.bhytid;



------Thuoc/vat tu trong goi ngay 12/9/2018
--ver_2
SELECT goi.servicepriceid_master,
	goi.servicepriceid as goi_servicepriceid,
	goi.servicepriceid_thanhtoanrieng,
	goi.loaithuocvt,
	goi.servicepricecode as goi_servicepricecode,
	goi.servicepricename as goi_servicepricename,
	goi.soluong as goi_soluong,
	goi.servicepricefee as goi_servicepricefee,
	(goi.soluong*goi.servicepricefee) as goi_thanhtien
FROM (select vienphiid from vienphi where 1=1{_tieuchi_vp} {_doituongbenhnhanid}) vp 
	inner join (select vienphiid,servicepriceid,(case when bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') then 1 else 2 end) as loaithuocvt,servicepricecode,servicepricename,servicepricename_bhyt,servicepricename_nhandan,(case loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan else servicepricemoney end) as servicepricefee,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,coalesce(servicepriceid_thanhtoanrieng,0) as servicepriceid_thanhtoanrieng,servicepriceid_master,loaidoituong from serviceprice where loaidoituong in (2,5,7,9) and bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_departmentid_ser}) goi on goi.vienphiid=vp.vienphiid


	/*
--ver_1 cham - bỏ
SELECT ser.servicepriceid,
	goi.servicepriceid as goi_servicepriceid,
	goi.loaithuocvt,
	goi.servicepricecode as goi_servicepricecode,
	goi.servicepricename as goi_servicepricename,
	goi.soluong as goi_soluong,
	goi.servicepricefee as goi_servicepricefee,
	(goi.soluong*goi.servicepricefee) as goi_thanhtien
FROM (select se.vienphiid,se.servicepriceid,serf.tinhtoanlaigiadvktc
from ((select servicepriceid,servicepricecode,vienphiid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') {_trangthaipttt} {_tieuchi_ser} {_departmentid_ser}) se inner join (select servicepricecode,tinhtoanlaigiadvktc,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') {_pttt_loaiid_serf}) serf on serf.servicepricecode=se.servicepricecode)) ser
inner join (select vienphiid from vienphi where 1=1{_tieuchi_vp} {_doituongbenhnhanid}) vp on vp.vienphiid=ser.vienphiid 
inner join (select vienphiid,servicepriceid,(case when bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') then 1 else 2 end) as loaithuocvt,servicepricecode,servicepricename,servicepricename_bhyt,servicepricename_nhandan,(case loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan else servicepricemoney end) as servicepricefee,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,coalesce(servicepriceid_thanhtoanrieng,0) as servicepriceid_thanhtoanrieng,servicepriceid_master,loaidoituong from serviceprice where loaidoituong in (2,5,7,9) and bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser}) goi on (case when ser.tinhtoanlaigiadvktc=1 then goi.servicepriceid_master=ser.servicepriceid else (goi.loaidoituong=2 and goi.servicepriceid_master=ser.servicepriceid and goi.servicepriceid_thanhtoanrieng=0) end);

*/













