------Bao cao CHI TIỀN GIƯỜNG YÊU CẦU CHO CÁC KHOA PHÒNG CHUYỂN KHOẢN					
--BC112_ChiTienGiuongYCChoCacKhoaCK

--ngay 26/11/2018

CREATE TABLE ml_bc112giuongyc
(
  bc112id serial NOT NULL,
  code integer,
  stt integer,
  departmentgroupname text,
  tylehuong double precision,
  CONSTRAINT ml_bc112giuongyc_pkey PRIMARY KEY (bc112id)
);

CREATE INDEX ml_bc112giuongyc_code_idx
  ON ml_bc112giuongyc
  USING btree
  (code);

--=(((Tổng giường yêu cầu toàn viện - Tổng thu( của BC giuong ngoaikieu))*0.88)) - Tiền xuất ăn (của BC GYC 12%)




SELECT
	degp.stt,
	degp.departmentgroupname,
	sum(T.thanhtien) as thanhtien,
	degp.tylehuong,
	sum(T.thanhtien)*(degp.tylehuong/100.0) as tienthuong,
	'' as kynhan
FROM (SELECT degp.*
		FROM dblink('myconn_mel','SELECT code,stt,departmentgroupname,tylehuong FROM ml_bc112giuongyc')
		AS degp(code integer,stt integer,departmentgroupname text,tylehuong double precision)) degp
LEFT JOIN
	(select 112 as code,
		sum(ser.soluong*ser.dongia*0.88) as thanhtien
	from (select vienphiid,soluong,servicepricecode,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
			from serviceprice 
			where bhyt_groupcode='12NG' "+tieuchi_ser+") ser
		inner join (select servicepricecode from servicepriceref where servicepricegroupcode='G303YC') serf on serf.servicepricecode=ser.servicepricecode	
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+_bntronvien+") vp on vp.vienphiid=ser.vienphiid
		
	UNION ALL

	select 112 as code,
		0-sum(ser.soluong*ser.dongia*0.88) as thanhtien
	from (select vienphiid,soluong,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_serBC111+") ser
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+_bntronvien+") vp on vp.vienphiid=ser.vienphiid

	UNION ALL
	select 112 as code,
		0-sum(ser.soluong*80000) as thanhtien
	from 
		(select vienphiid,soluong,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia
			from serviceprice 
			where 1=1 "+tieuchi_ser+lstdichvu_serBC110+lstkhoa_serBC110+") ser
		inner join (select vienphiid,vienphistatus from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+_bntronvien+") vp on vp.vienphiid=ser.vienphiid) T on T.code=degp.code
GROUP BY degp.stt,degp.departmentgroupname,degp.tylehuong
ORDER BY degp.stt;





