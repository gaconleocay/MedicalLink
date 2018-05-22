--Báo cáo trích thưởng chuyên gia 
--ucBC102_TrichThuongChuyenGiaYC

--them nhom 

/*
ALter table nhompersonnel add nhom_bcid integer; --1=Nhân viên hợp đồng; 2=Nhân viên bệnh viện
ALter table nhompersonnel add nhom_bcten text;

*/

--ngay 18/5/2018
SELECT 
	(row_number() OVER (PARTITION BY ncd.nhom_bcid ORDER BY ncd.username)) as stt,
	mbp.userid as userhisid,
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
FROM (select maubenhphamid,userid from maubenhpham where maubenhphamgrouptype=2 and maubenhphamstatus=16 "+tieuchi_mbp+") mbp 
	inner join (select maubenhphamid,vienphiid,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
				from serviceprice where bhyt_groupcode='01KB' "+lstdichvu_ser+tieuchi_ser+") ser on ser.maubenhphamid=mbp.maubenhphamid
	inner join (select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select userhisid,usercode,username,nhom_bcid,nhom_bcten from nhompersonnel group by userhisid,usercode,username,nhom_bcid,nhom_bcten) ncd on ncd.userhisid=mbp.userid
GROUP BY mbp.userid,ncd.usercode,ncd.username,ncd.nhom_bcid,ncd.nhom_bcten
ORDER BY ncd.username;













