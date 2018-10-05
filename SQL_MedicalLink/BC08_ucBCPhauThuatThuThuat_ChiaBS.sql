---Bao cao PTTT chia tien theo bac si
--ucBCPhauThuatThuThuat

--Chia ra tung loai bao cao rieng biet (dung bien)

--Duyet PTTT:
/*
Alter table serviceprice add duyetpttt_stt integer;
Alter table serviceprice add duyetpttt_date timestamp without time zone;
Alter table serviceprice add duyetpttt_usercode text;
Alter table serviceprice add duyetpttt_username text;

CREATE INDEX serviceprice_duyetpttt_stt_idx ON serviceprice USING btree (duyetpttt_stt);
CREATE INDEX serviceprice_duyetpttt_date_idx ON serviceprice USING btree (duyetpttt_date);
CREATE INDEX serviceprice_duyetpttt_user_idx ON serviceprice USING btree (duyetpttt_usercode);
*/

---------
/*
- Trạng thái
+ 0=Chưa gửi
+ 1=Đã gửi (từ trạng thái null,0,3)
+ 0=Khoa hủy gửi (từ trạng thái 1)
+ 2=Đã tiếp nhận (từ trạng thái 1)
+ 1=KHTH hủy tiếp nhận : hủy cả khoa, từng dv (từ trạng thái 2)
+ 3=Đã duyệt PTTT (từ trạng thái 2)
+ 2=KHTH hủy duyệt PTTT (từ trạng thái 3)
+ 99=KHTH khóa dịch vụ (từ trạng thái 3)
*/


--ngay 23/5: fix loi x2 ket qua
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
	--TO_CHAR(A.ngay_chidinh,'HH24:MI dd/MM/yyyy') as ngay_chidinh, 
	--TO_CHAR(A.ngay_thuchien,'HH24:MI dd/MM/yyyy') as ngay_thuchien, 
	--(case when A.ngay_ketthuc<>'0001-01-01 00:00:00' then TO_CHAR(A.ngay_ketthuc,'HH24:MI dd/MM/yyyy') end) as ngay_ketthuc,
	A.ngay_chidinh,
	A.ngay_thuchien,
	(case when A.ngay_ketthuc<>'0001-01-01 00:00:00' then A.ngay_ketthuc end) as ngay_ketthuc,
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
	A.tinhtoanlaigiadvktc,
	A.soluong, 
	A.servicepricefee, 
	A.tyle, 
	round(cast(A.thuoc_tronggoi as numeric),0) as thuoc_tronggoi, 
	round(cast(A.vattu_tronggoi as numeric),0) as vattu_tronggoi, 
	round(cast(A.chiphikhac as numeric),0) AS chiphikhac, 
	(A.servicepricefee * A.soluong) as thanhtien, 
	round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) {chiachobacsi} ) as numeric),0) as lai, 
	mc.username as mochinh_tenbs, 
	(case when coalesce(mc.username,'CX')<>'CX' then (A.mochinh_tien * (A.tyle/100)) else 0 end) as mochinh_tien, 
	mmc.username as moimochinh_tenbs, 
	(case when coalesce(mmc.username,'CX')<>'CX' then (A.moimochinh_tien * (A.tyle/100)) else 0 end) as moimochinh_tien, 
	gm.username as gayme_tenbs, 
	(case when coalesce(gm.username,'CX')<>'CX' then (A.gayme_tien * (A.tyle/100)) else 0 end) as gayme_tien, 
	mgm.username as moigayme_tenbs, 
	(case when coalesce(mgm.username,'CX')<>'CX' then (A.moigayme_tien * (A.tyle/100)) else 0 end) as moigayme_tien, 
	p1.username as phu1_tenbs, 
	(case when coalesce(p1.username,'CX')<>'CX' then (A.phu1_tien * (A.tyle/100)) else 0 end) as phu1_tien, 
	p2.username as phu2_tenbs, 
	(case when coalesce(p2.username,'CX')<>'CX' then (A.phu2_tien * (A.tyle/100)) else 0 end) as phu2_tien, 
	gv1.username as giupviec1_tenbs, 
	(case when coalesce(gv1.username,'CX')<>'CX' then (A.giupviec1_tien * (A.tyle/100)) else 0 end) as giupviec1_tien, 
	gv2.username as giupviec2_tenbs, 
	(case when coalesce(gv2.username,'CX')<>'CX' then (A.giupviec2_tien * (A.tyle/100)) else 0 end) as giupviec2_tien, 
	--TO_CHAR(A.ngay_vaovien,'HH24:MI dd/MM/yyyy') as ngay_vaovien, 
	--TO_CHAR(A.ngay_ravien,'HH24:MI dd/MM/yyyy') as ngay_ravien, 
	--TO_CHAR(A.ngay_thanhtoan,'HH24:MI dd/MM/yyyy') as ngay_thanhtoan,
	A.ngay_vaovien,
	A.ngay_ravien,
	A.ngay_thanhtoan,
	nnth.username as nguoinhapthuchien,
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
		pttt.phauthuatthuthuatdate as ngay_thuchien, 
		pttt.phauthuatthuthuatdate_ketthuc as ngay_ketthuc,
		(select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, 
		(case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, 
		ser.servicepricecode, 
		(case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, 
		(case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, 
		(case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, 
		{_sqlhaophi}
		/*0 as thuoc_tronggoi,
		0 as vattu_tronggoi,
		(case when serf.tinhtoanlaigiadvktc=1 
				then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) 
			else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) end) as thuoc_tronggoi, 
		(case when serf.tinhtoanlaigiadvktc=1 
				then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) 
			else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) 
			end) as vattu_tronggoi, */
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
		((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, 
		{baocaotungloai}
		vp.vienphidate as ngay_vaovien, 
		(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as ngay_ravien, 
		(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan,
		pttt.userid_gmhs as nguoinhapthuchien
	FROM (select servicepriceid,vienphiid,duyetpttt_stt,duyetpttt_date,duyetpttt_usercode,duyetpttt_username,maubenhphamid,bhyt_groupcode,departmentgroupid,departmentid,servicepricedate,medicalrecordid,servicepricecode,loaidoituong,servicepricename,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') {_trangthaipttt} {_tieuchi_ser} {_departmentid_ser}) ser
	left join (select (row_number() OVER (PARTITION BY servicepriceid ORDER BY phauthuatthuthuatid desc)) as stt,* from phauthuatthuthuat) pttt on pttt.servicepriceid=ser.servicepriceid 
	inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1{_tieuchi_vp} {_doituongbenhnhanid}) vp on vp.vienphiid=ser.vienphiid 
	inner join (select servicepricecode,tinhtoanlaigiadvktc,pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') {_pttt_loaiid_serf}) serf on serf.servicepricecode=ser.servicepricecode 
	WHERE coalesce(pttt.stt,1)=1 {_tieuchi_pttt}) A 
INNER JOIN (select hosobenhanid,patientname,gioitinhcode,birthday,bhytcode,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsbA.hosobenhanid=A.hosobenhanid 
INNER JOIN (select maubenhphamid,chandoan from maubenhpham where 1=1 and maubenhphamgrouptype=4 {_tieuchi_mbp}) mbp on mbp.maubenhphamid=A.maubenhphamid 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kchd ON kchd.departmentgroupid=A.khoachidinh 
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=A.phongchidinh
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=A.khoachuyenden 
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien 
LEFT JOIN (select userhisid,username from nhompersonnel) mc ON mc.userhisid=A.mochinh_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) mmc ON mmc.userhisid=A.moimochinh_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) gm ON gm.userhisid=A.gayme_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) mgm ON mgm.userhisid=A.moigayme_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) p1 ON p1.userhisid=A.phu1_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) p2 ON p2.userhisid=A.phu2_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) gv1 ON gv1.userhisid=A.giupviec1_tenbs 
LEFT JOIN (select userhisid,username from nhompersonnel) gv2 ON gv2.userhisid=A.giupviec2_tenbs
LEFT JOIN (select userhisid,username from nhompersonnel) nnth ON nnth.userhisid=A.nguoinhapthuchien
INNER JOIN (select bhytid,bhytcode from bhyt where 1=1 {_tieuchi_bhyt}) bh on bh.bhytid=A.bhytid;


------Thuoc/vat tu trong goi ngay 12/9/2018
--ver_2
SELECT goi.servicepriceid_master,
	goi.servicepriceid as goi_servicepriceid,
	goi.servicepriceid_thanhtoanrieng,
	goi.loaidoituong,
	goi.loaithuocvt,
	goi.servicepricecode as goi_servicepricecode,
	goi.servicepricename as goi_servicepricename,
	goi.soluong as goi_soluong,
	goi.servicepricefee as goi_servicepricefee,
	(goi.soluong*goi.servicepricefee) as goi_thanhtien
FROM (select vienphiid from vienphi where 1=1{_tieuchi_vp} {_doituongbenhnhanid}) vp 
	inner join (select vienphiid,servicepriceid,(case when bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') then 1 else 2 end) as loaithuocvt,servicepricecode,servicepricename,servicepricename_bhyt,servicepricename_nhandan,(case loaidoituong when 0 then servicepricemoney_bhyt when 1 then servicepricemoney_nhandan else servicepricemoney end) as servicepricefee,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,coalesce(servicepriceid_thanhtoanrieng,0) as servicepriceid_thanhtoanrieng,servicepriceid_master,loaidoituong from serviceprice where loaidoituong in (2,5,7,9) and bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_departmentid_ser}) goi on goi.vienphiid=vp.vienphiid

	
	
	
/*
------Thuoc/vat tu trong goi ngay 12/9/2018 -chay cham - bỏ
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
	
	
	
	















