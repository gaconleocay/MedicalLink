--bao cao doanh thu theo may xet nghiem  ngay 14/6/2017 
--ucBCDoanhThuTheoMayXN

--su dung tat ca
--ngay 11/5/2018 them chi phi + tach vi sinh
SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt,
		SERV.ma_xn, SERV.ten_xn
		"+_tenmayxn_khacvisinh_select+",
		sum(SERV.sl_bhyt) as sl_bhyt, 
		sum(SERV.sl_vp) as sl_vp, 
		sum(SERV.sl_yc) as sl_yc, 
		sum(SERV.sl_nnn) as sl_nnn,
		sum(coalesce(SERV.sl_bhyt,0) + coalesce(SERV.sl_vp,0) + coalesce(SERV.sl_yc,0) + coalesce(SERV.sl_nnn,0)) as sl_tong,	
		SERV.gia_bhyt,
		SERV.gia_vp,
		SERV.gia_yc,
		SERV.gia_nnn,
		sum(coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) as tien_bhyt,
		sum(coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) as tien_vp,
		sum(coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) as tien_yc,
		sum(coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0)) as tien_nnn,	
		sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) as tien_tong,
		coalesce(SERV.cp_tructiep,0) as cp_tructiep, 
		coalesce(SERV.cp_maymoc,0) as cp_maymoc, 
		coalesce(SERV.cp_ldlk,0) as cp_ldlk,
		coalesce(SERV.cp_pttt,0) as cp_pttt, 	
		coalesce(chiphi.cp_hoachat,0) as cp_hoachat,
		coalesce(chiphi.cp_haophixn,0) as cp_haophixn,
		coalesce(chiphi.cp_luong,0) as cp_luong,
		coalesce(chiphi.cp_diennuoc,0) as cp_diennuoc,
		coalesce(chiphi.cp_khmaymoc,0) as cp_khmaymoc,
		coalesce(chiphi.cp_khxaydung,0) as cp_khxaydung,
		sum(coalesce(SERV.cp_tructiep,0)+coalesce(SERV.cp_maymoc,0)+coalesce(SERV.cp_ldlk,0)+coalesce(SERV.cp_pttt,0)+coalesce(chiphi.cp_hoachat,0)+coalesce(chiphi.cp_haophixn,0)+coalesce(chiphi.cp_luong,0)+coalesce(chiphi.cp_diennuoc,0)+coalesce(chiphi.cp_khmaymoc,0)+coalesce(chiphi.cp_khxaydung,0)) as cp_tong,
		(sum((coalesce(SERV.sl_bhyt,0)*coalesce(SERV.gia_bhyt,0))+(coalesce(SERV.sl_vp,0)*coalesce(SERV.gia_vp,0))+(coalesce(SERV.sl_yc,0)*coalesce(SERV.gia_yc,0))+(coalesce(SERV.sl_nnn,0)*coalesce(SERV.gia_nnn,0)))-sum(coalesce(SERV.cp_tructiep,0)+coalesce(SERV.cp_pttt,0)+coalesce(SERV.cp_maymoc,0)+coalesce(SERV.cp_ldlk,0))-sum((coalesce(chiphi.cp_hoachat,0)+coalesce(chiphi.cp_haophixn,0)+coalesce(chiphi.cp_luong,0)+coalesce(chiphi.cp_diennuoc,0)+coalesce(chiphi.cp_khmaymoc,0)+coalesce(chiphi.cp_khxaydung,0))*SERV.soluong)) as lai,
		SERV.khoatra_kq,
		chiphi.khuvuc_ten
FROM
	(select ser.vienphiid, ser.servicepriceid, ser.maubenhphamid, 
		ser.servicepricecode as ma_xn, 
		ser.servicepricename as ten_xn,  
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=0 then ser.soluong end) as sl_bhyt,
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=1 then ser.soluong end) as sl_vp,
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong in (3,4) then ser.soluong end) as sl_yc,
		(case when ser.doituongbenhnhanid=4 then ser.soluong end) as sl_nnn,
		ser.soluong,
		ser.servicepricemoney_bhyt as gia_bhyt,
		ser.servicepricemoney_nhandan as gia_vp,
		ser.servicepricemoney as gia_yc,
		ser.servicepricemoney_nuocngoai as gia_nnn,
		(ser.chiphidauvao) as cp_tructiep,
		(ser.chiphipttt) as cp_pttt,
		(ser.chiphimaymoc) as cp_maymoc,
		(select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) as cp_ldlk,
		(case when tkq.usercode like '%-sh' then 'Khoa sinh hóa'
			 when tkq.usercode like '%-hh' then 'Khoa huyết học'
			 when tkq.usercode like '%-vs' then 'Khoa vi sinh'
			 when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh'
			 when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa'
			 else '' end) as khoatra_kq,
		(select s.idmayxn from service s where s.servicepriceid=ser.servicepriceid and s.servicedate>'2017-05-01 00:00:00'
			order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn,
		(select s.tenmayxn from service s where s.servicepriceid=ser.servicepriceid and s.servicedate>'2017-05-01 00:00:00'
			order by coalesce(s.idmayxn,0) desc limit 1) as tenmayxn	
	from (select servicepriceid,vienphiid,maubenhphamid,servicepricecode,servicepricename,doituongbenhnhanid,loaidoituong,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,servicepricemoney_nuocngoai,chiphidauvao,chiphipttt,chiphimaymoc,mayytedbid from serviceprice where bhyt_groupcode='03XN' "+_tieuchi_ser+_doituong_ser+") ser 
		 INNER JOIN (select maubenhphamid,usertrakq from maubenhpham where maubenhphamgrouptype=0 "+_tieuchi_mbp+_loaibaocao+") MBP ON MBP.maubenhphamid=ser.maubenhphamid
		 LEFT JOIN nhompersonnel tkq on tkq.userhisid=MBP.usertrakq
		) SERV
 INNER JOIN (select vienphiid from vienphi where 1=1 "+_tieuchi_vp+_trangthaibenhan+_doituong_vp+") VP ON VP.vienphiid=SERV.vienphiid	
LEFT JOIN (SELECT *
					FROM dblink('myconn_mel','select cp.mayxn_ma,kv.mayxn_ten,kv.khuvuc_ma,kv.khuvuc_ten,cp.servicepricecode,cp.cp_hoachat,cp.cp_haophixn,cp.cp_luong,cp.cp_diennuoc,cp.cp_khmaymoc,cp.cp_khxaydung from ml_mayxnchiphi cp left join ml_mayxnkhuvuc kv on cp.mayxn_ma=kv.mayxn_ma')
					AS ml_mayxn(mayxn_ma integer,mayxn_ten text,khuvuc_ma text,khuvuc_ten text,servicepricecode text,cp_hoachat double precision,cp_haophixn double precision,cp_luong double precision,cp_diennuoc double precision,cp_khmaymoc double precision,cp_khxaydung double precision)) chiphi on chiphi.servicepricecode=SERV.ma_xn "+_dieukien_khacvisinh+"
" + dsmayxn + "					
GROUP BY SERV.ma_xn,SERV.ten_xn"+_tenmayxn_khacvisinh_groupby+",SERV.gia_bhyt,SERV.gia_vp,SERV.gia_yc,SERV.gia_nnn,SERV.khoatra_kq,chiphi.khuvuc_ten,SERV.cp_tructiep,SERV.cp_maymoc,SERV.cp_ldlk,SERV.cp_pttt,chiphi.cp_hoachat,chiphi.cp_haophixn,chiphi.cp_luong,chiphi.cp_diennuoc,chiphi.cp_khmaymoc,chiphi.cp_khxaydung;






---------===========Chi tiet - ngay 26/6/2018

SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepricedate) as stt,
	vp.vienphiid,
	vp.patientid,
	mbp.maubenhphamid,
	mbp.maubenhphamdate,
	hsba.patientname,
	bh.bhytcode,
	degp.departmentgroupname,
	de.departmentname,
	ngd.username as nguoichidinh,
	ser.servicepricecode,
	ser.servicepricename,
	ser.soluong,
	ser.servicepricemoney_bhyt,
	ser.servicepricemoney_nhandan,
	ser.servicepricemoney,
	ser.servicepricemoney_nuocngoai,
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
	(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	(case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as vienphistatus,
	(case when tkq.usercode like '%-sh' then 'Khoa sinh hóa'
			 when tkq.usercode like '%-hh' then 'Khoa huyết học'
			 when tkq.usercode like '%-vs' then 'Khoa vi sinh'
			 when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh'
			 when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa'
			 else '' end) as khoatra_kq			
FROM 
	(select se.servicepriceid,se.vienphiid,se.maubenhphamid,se.servicepricecode,se.servicepricename,se.soluong,se.loaidoituong,se.departmentgroupid,se.departmentid,se.servicepricedate,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,servicepricemoney_nuocngoai,
		(select s.idmayxn from service s where s.servicepriceid=se.servicepriceid and s.servicedate>'2017-05-01 00:00:00'
			order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn
		from serviceprice se where se.bhyt_groupcode='03XN' and servicepricemoney_bhyt="+_gia_bhyt+" and servicepricemoney_nhandan="+_gia_vp+" and servicepricemoney="+_gia_yc+" and servicepricemoney_nuocngoai="+_gia_nnn+" "+_servicepricecode+_tieuchi_ser+_doituong_ser+") ser
	inner join (select maubenhphamid,maubenhphamdate,userid,usertrakq from maubenhpham where maubenhphamgrouptype=0 "+_tieuchi_mbp+_loaibaocao+") mbp on mbp.maubenhphamid=ser.maubenhphamid
	inner join (select vienphiid,patientid,hosobenhanid,bhytid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 "+_tieuchi_vp+_doituong_vp+_trangthaibenhan+") vp on vp.vienphiid=ser.vienphiid
	inner join (select hosobenhanid,patientname from hosobenhan where hosobenhandate>'2017-05-01 00:00:00') hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where bhytdate>'2017-05-01 00:00:00') bh on bh.bhytid=vp.bhytid
	left join (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userid 
	left join nhompersonnel tkq on tkq.userhisid=mbp.usertrakq
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid
	left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=ser.departmentid
	"+_idmay_xn+";



	and servicepricemoney_bhyt="+_gia_bhyt+" and servicepricemoney_nhandan="+_gia_vp+" and servicepricemoney="+_gia_yc+" and servicepricemoney_nuocngoai="+_gia_nnn+"







/*

-- ngay 14/6 bo sung them cot acc tra ket qua va khoa tra kq (ap dung XN đã có KQ)
--ngay 5/4/2018: them tieu chi: trang thai; doi tuong BN
-----------------========su dung tat ca
SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt,
		SERV.ma_xn, SERV.ten_xn, 
		SERV.idmayxn as idmay_xn,
		SERV.tenmayxn as tenmay_xn,
		sum(SERV.sl_bhyt) as sl_bhyt, 
		sum(SERV.sl_vp) as sl_vp, 
		sum(SERV.sl_yc) as sl_yc, 
		sum(SERV.sl_nnn) as sl_nnn,
		sum(coalesce(SERV.sl_bhyt,0) + coalesce(SERV.sl_vp,0) + coalesce(SERV.sl_yc,0) + coalesce(SERV.sl_nnn,0)) as sl_tong,	
		SERV.gia_bhyt,
		SERV.gia_vp,
		SERV.gia_yc,
		SERV.gia_nnn,
		sum(coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) as tien_bhyt,
		sum(coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) as tien_vp,
		sum(coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) as tien_yc,
		sum(coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0)) as tien_nnn,	
		sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) as tien_tong,
		sum(SERV.cp_tructiep) as cp_tructiep, 
		sum(SERV.cp_pttt) as cp_pttt, 
		sum(SERV.cp_maymoc) as cp_maymoc, 
		sum(SERV.cp_ldlk) as cp_ldlk,
		sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0)) as cp_tong,
		(sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) - sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0))) as lai,
		SERV.khoatra_kq
FROM
	(select ser.vienphiid, ser.servicepriceid, ser.maubenhphamid, 
		ser.servicepricecode as ma_xn, 
		ser.servicepricename as ten_xn,  
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=0 then ser.soluong end) as sl_bhyt,
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=1 then ser.soluong end) as sl_vp,
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong in (3,4) then ser.soluong end) as sl_yc,
		(case when ser.doituongbenhnhanid=4 then ser.soluong end) as sl_nnn,
		ser.servicepricemoney_bhyt as gia_bhyt,
		ser.servicepricemoney_nhandan as gia_vp,
		ser.servicepricemoney as gia_yc,
		ser.servicepricemoney_nuocngoai as gia_nnn,
		(ser.chiphidauvao) as cp_tructiep,
		(ser.chiphipttt) as cp_pttt,
		(ser.chiphimaymoc) as cp_maymoc,
		(select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) as cp_ldlk,
		(case when tkq.usercode like '%-sh' then 'Khoa sinh hóa'
			 when tkq.usercode like '%-hh' then 'Khoa huyết học'
			 when tkq.usercode like '%-vs' then 'Khoa vi sinh'
			 when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh'
			 when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa'
			 else '' end) as khoatra_kq,
		(select s.idmayxn from service s where s.servicepriceid=ser.servicepriceid 
			order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn,
		(select s.tenmayxn from service s where s.servicepriceid=ser.servicepriceid 
			order by coalesce(s.idmayxn,0) desc limit 1) as tenmayxn	
	from (select * from serviceprice where bhyt_groupcode='03XN' "+_tieuchi_ser+_doituong_ser+") ser 
		 INNER JOIN (select maubenhphamid, usertrakq from maubenhpham where maubenhphamgrouptype=0 "+_tieuchi_mbp+") MBP ON MBP.maubenhphamid=ser.maubenhphamid
		 LEFT JOIN nhompersonnel tkq on tkq.userhisid=MBP.usertrakq
		) SERV
 INNER JOIN (select vienphiid from vienphi where 1=1 "+_tieuchi_vp+_trangthaibenhan+_doituong+") VP ON VP.vienphiid=SERV.vienphiid	
" + dsmayxn + "
GROUP BY SERV.ma_xn, SERV.ten_xn, SERV.idmayxn, SERV.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq;




---lay ds may xet nghiem

select idmayxn,tenmayxn from service where idmayxn is not null
group by idmayxn,tenmayxn
order by tenmayxn


---===theo ngay chi dinh va ngay tra ket qua  ngay 15/6/2017
--fix nhan doi so luong do group by
SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt,
		SERV.ma_xn, SERV.ten_xn, 
		SERV.idmayxn as idmay_xn,
		SERV.tenmayxn as tenmay_xn,
		sum(SERV.sl_bhyt) as sl_bhyt, 
		sum(SERV.sl_vp) as sl_vp, 
		sum(SERV.sl_yc) as sl_yc, 
		sum(SERV.sl_nnn) as sl_nnn,
		sum(coalesce(SERV.sl_bhyt,0) + coalesce(SERV.sl_vp,0) + coalesce(SERV.sl_yc,0) + coalesce(SERV.sl_nnn,0)) as sl_tong,	
		SERV.gia_bhyt,
		SERV.gia_vp,
		SERV.gia_yc,
		SERV.gia_nnn,
		sum(coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) as tien_bhyt,
		sum(coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) as tien_vp,
		sum(coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) as tien_yc,
		sum(coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0)) as tien_nnn,	
		sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) as tien_tong,
		sum(SERV.cp_tructiep) as cp_tructiep, 
		sum(SERV.cp_pttt) as cp_pttt, 
		sum(SERV.cp_maymoc) as cp_maymoc, 
		sum(SERV.cp_ldlk) as cp_ldlk,
		sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0)) as cp_tong,
		(sum((coalesce(SERV.sl_bhyt,0) * coalesce(SERV.gia_bhyt,0)) + (coalesce(SERV.sl_vp,0) * coalesce(SERV.gia_vp,0)) + (coalesce(SERV.sl_yc,0) * coalesce(SERV.gia_yc,0)) + (coalesce(SERV.sl_nnn,0) * coalesce(SERV.gia_nnn,0))) - sum(coalesce(SERV.cp_tructiep,0) + coalesce(SERV.cp_pttt,0) + coalesce(SERV.cp_maymoc,0) + coalesce(SERV.cp_ldlk,0))) as lai,
		SERV.khoatra_kq
FROM
	(select ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode as ma_xn, ser.servicepricename as ten_xn,  
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=0 then ser.soluong end) as sl_bhyt,
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong=1 then ser.soluong end) as sl_vp,
		(case when ser.doituongbenhnhanid<>4 and ser.loaidoituong in (3,4) then ser.soluong end) as sl_yc,
		(case when ser.doituongbenhnhanid=4 then ser.soluong end) as sl_nnn,
		ser.servicepricemoney_bhyt as gia_bhyt,
		ser.servicepricemoney_nhandan as gia_vp,
		ser.servicepricemoney as gia_yc,
		ser.servicepricemoney_nuocngoai as gia_nnn,
		(ser.chiphidauvao) as cp_tructiep,
		(ser.chiphipttt) as cp_pttt,
		(ser.chiphimaymoc) as cp_maymoc,
		(select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) as cp_ldlk,
		(case when tkq.usercode like '%-sh' then 'Khoa sinh hóa'
			 when tkq.usercode like '%-hh' then 'Khoa huyết học'
			 when tkq.usercode like '%-vs' then 'Khoa vi sinh'
			 when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh'
			 when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa'
			 else '' end) as khoatra_kq,
		(select s.idmayxn from service s where s.servicepriceid=ser.servicepriceid 
			order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn,
		(select s.tenmayxn from service s where s.servicepriceid=ser.servicepriceid 
			order by coalesce(s.idmayxn,0) desc limit 1) as tenmayxn	
	from serviceprice ser 
		 INNER JOIN (select maubenhphamid, usertrakq from maubenhpham where maubenhphamgrouptype=0 		 
		 and maubenhphamfinishdate between '2017-01-01 00:00:00' and '2017-01-05 23:59:59')	MBP ON MBP.maubenhphamid=ser.maubenhphamid
		 LEFT JOIN nhompersonnel tkq on tkq.userhisid=MBP.usertrakq
	where ser.bhyt_groupcode='03XN') SERV
--WHERE SERV.idmayxn in (2)
GROUP BY SERV.ma_xn, SERV.ten_xn, SERV.idmayxn, SERV.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq


*/


























