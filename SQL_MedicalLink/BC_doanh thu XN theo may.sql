--bao cao doanh thu theo may xet nghiem  ngay 14/6/2017 
-- ngay 14/6 bo sung them cot acc tra ket qua va khoa tra kq (ap dung XN đã có KQ)

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
		 LEFT JOIN tools_tblnhanvien tkq on tkq.userhisid=MBP.usertrakq
	where ser.bhyt_groupcode='03XN') SERV
--WHERE SERV.idmayxn in (2)
GROUP BY SERV.ma_xn, SERV.ten_xn, SERV.idmayxn, SERV.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq






-----------------========
---===theo ngay vao vien, ra vien, thanh toan  ngay 15/6/2017
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
	(select ser.vienphiid, ser.servicepriceid, ser.maubenhphamid, ser.servicepricecode as ma_xn, ser.servicepricename as ten_xn,  
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
		 INNER JOIN (select maubenhphamid, usertrakq from maubenhpham where maubenhphamgrouptype=0)	MBP ON MBP.maubenhphamid=ser.maubenhphamid
		 LEFT JOIN tools_tblnhanvien tkq on tkq.userhisid=MBP.usertrakq
	where ser.bhyt_groupcode='03XN') SERV
 INNER JOIN (select vienphiid from vienphi where 
			 vienphidate between '2017-01-01 00:00:00' and '2017-01-05 23:59:59'
			 --" + tieuchi + "
			) VP ON VP.vienphiid=SERV.vienphiid	
" + dsmayxn + "
GROUP BY SERV.ma_xn, SERV.ten_xn, SERV.idmayxn, SERV.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq;
















---===theo ngay chi dinh va ngay tra ket qua  ngay 14/6/2017
SELECT ROW_NUMBER() OVER (ORDER BY SERV.ten_xn) as stt,
		SERV.ma_xn, SERV.ten_xn, 
		SE.idmayxn as idmay_xn,
		SE.tenmayxn as tenmay_xn,
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
			 else '' end) as khoatra_kq
	from serviceprice ser 
		 INNER JOIN (select maubenhphamid, usertrakq from maubenhpham where maubenhphamgrouptype=0 and maubenhphamfinishdate between '2017-01-01 00:00:00' and '2017-01-05 23:59:59')	MBP ON MBP.maubenhphamid=ser.maubenhphamid
		 LEFT JOIN tools_tblnhanvien tkq on tkq.userhisid=MBP.usertrakq
	where ser.bhyt_groupcode='03XN') SERV
 INNER JOIN
	(select s.servicepriceid, s.servicepricecode, s.servicedate, s.idmayxn, s.tenmayxn
	from service s
	--" + dsmayxn + "
	group by s.servicepriceid, s.servicepricecode, s.servicedate, s.idmayxn, s.tenmayxn) SE ON SE.servicepriceid=SERV.servicepriceid
GROUP BY SERV.ma_xn, SERV.ten_xn, SE.idmayxn, SE.tenmayxn, SERV.gia_bhyt, SERV.gia_vp, SERV.gia_yc, SERV.gia_nnn, SERV.khoatra_kq





























