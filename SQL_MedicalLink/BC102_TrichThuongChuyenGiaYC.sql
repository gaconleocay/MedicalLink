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
FROM (select maubenhphamid,userid,maubenhphamstatus,(select pk.userid from sothutuphongkham pk where pk.medicalrecordid=m.medicalrecordid order by sothutuid limit 1) as userthuchien from maubenhpham m where maubenhphamgrouptype=2 "+tieuchi_mbp+_khoaChiDinh+") mbp 
	inner join (select maubenhphamid,vienphiid,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
				from serviceprice where bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+lstdichvu_ser+tieuchi_ser+_khoaChiDinh+") ser on ser.maubenhphamid=mbp.maubenhphamid
	inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	inner join (select userhisid,usercode,username,nhom_bcid,nhom_bcten from nhompersonnel group by userhisid,usercode,username,nhom_bcid,nhom_bcten) ncd on ncd.userhisid=mbp.userthuchien
WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0	
GROUP BY mbp.userthuchien,ncd.usercode,ncd.username,ncd.nhom_bcid,ncd.nhom_bcten;













