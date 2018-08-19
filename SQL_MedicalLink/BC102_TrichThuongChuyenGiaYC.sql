--Báo cáo trích thưởng chuyên gia 
--ucBC102_TrichThuongChuyenGiaYC

--them nhom 

/*
ALter table nhompersonnel add nhom_bcid integer; --1=Nhân viên hợp đồng; 2=Nhân viên bệnh viện
ALter table nhompersonnel add nhom_bcten text;

*/

--ngay 16/8/2018
--lay BS kham benh = bs nhấn bắt đầu đầu tiên

SELECT 
	(row_number() OVER (PARTITION BY ncd.nhom_bcid ORDER BY ncd.username)) as stt,
	mbp.userthuchien as userhisid,
	ncd.usercode,
	ncd.username,
	coalesce(ncd.nhom_bcid,0) as nhom_bcid,
	ncd.nhom_bcten,
	sum(ser.soluong) as soluong,
	sum(ser.soluong*ser.dongia) as thanhtien,
	50 as tylehuong,
	sum(ser.soluong*ser.dongia*0.5) as tongtien,
	0 as tienthue,
	0 as thuclinh,
	'' as kynhan,
	0 as isgroup
FROM (select maubenhphamid,userid,maubenhphamstatus,(select pk.userid from sothutuphongkham pk where isremoved=0 and pk.medicalrecordid=m.medicalrecordid order by sothutuid limit 1) as userthuchien from maubenhpham m where maubenhphamgrouptype=2 "+tieuchi_mbp+_khoaChiDinh+") mbp 
	inner join (select maubenhphamid,vienphiid,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
				from serviceprice where bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+lstdichvu_ser+tieuchi_ser+_khoaChiDinh+") ser on ser.maubenhphamid=mbp.maubenhphamid
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select userhisid,usercode,username,nhom_bcid,nhom_bcten from nhompersonnel group by userhisid,usercode,username,nhom_bcid,nhom_bcten) ncd on ncd.userhisid=mbp.userthuchien
WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0	
GROUP BY mbp.userthuchien,ncd.usercode,ncd.username,ncd.nhom_bcid,ncd.nhom_bcten;




--Chi tiet - 17/8/2018

SELECT row_number() over (order by vp.patientid,mbp.maubenhphamdate) as stt, 
	ser.servicepriceid,
	vp.patientid, 
	vp.vienphiid, 
	hsba.patientname,
	hsba.bhytcode,
	kcd.departmentgroupname as khoachidinh, 
	pcd.departmentname as phongchidinh, 
	mbp.maubenhphamid,
	mbp.maubenhphamdate,
	mbp.userthuchien as userhisid,
	ncd.usercode,
	ncd.username,
	coalesce(ncd.nhom_bcid,0) as nhom_bcid,
	ncd.nhom_bcten,
	ser.servicepricecode,
	ser.servicepricename,
	ser.soluong, 
	ser.dongia,
	(ser.soluong*ser.dongia) as thanhtien,
	50 as tylehuong,
	(ser.soluong*ser.dongia*0.5) as tongtien,
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC'
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		when 20 then 'Thanh toán riêng'
		end) as loaidoituong,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,	
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus,
	(case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' end) trangthaithutien
FROM 
	(select maubenhphamid,userid,maubenhphamstatus,maubenhphamdate,departmentid_des,(select pk.userid from sothutuphongkham pk where isremoved=0 and pk.medicalrecordid=m.medicalrecordid order by sothutuid limit 1) as userthuchien from maubenhpham m where maubenhphamgrouptype=2 "+tieuchi_mbp+_khoaChiDinh+") mbp 
	INNER JOIN (select servicepriceid,vienphiid,maubenhphamid,departmentgroupid,departmentid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,servicepricedate,maubenhphamphieutype,bhyt_groupcode,loaidoituong 
				from serviceprice where bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+lstdichvu_ser+tieuchi_ser+_khoaChiDinh+") ser on ser.maubenhphamid=mbp.maubenhphamid
	INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	INNER JOIN (select userhisid,usercode,username,nhom_bcid,nhom_bcten from nhompersonnel group by userhisid,usercode,username,nhom_bcid,nhom_bcten) ncd on ncd.userhisid=mbp.userthuchien
	INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 "+_tieuchi_hsba+_hosobenhanstatus+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid
WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0	

























