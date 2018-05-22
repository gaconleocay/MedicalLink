--Báo cáo CHI THƯỞNG DỊCH VỤ VIỆN PHÍ														
--ucBC103_ChiThuongDichVuVienPhi


--Kham benh
SELECT 
	ser.departmentid as departmentgroupid,
	(ser.departmentid || 'KB') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where bhyt_groupcode='01KB' "+tieuchi_ser+lstdichvu_ser_kb+") ser
	inner join (select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentid,ser.dongia
	
UNION ALL
--Sieu am-dien tim
SELECT 
	ser.departmentgroupid,
	(ser.departmentgroupid || 'SADT') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	0 as dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where bhyt_groupcode in ('04CDHA','05TDCN') and departmentgroupid=46 "+tieuchi_ser+lstdichvu_ser_sadt+") ser
	inner join (select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentgroupid

UNION ALL
--Xet nghiem Yeu cau
SELECT 
	ser.departmentgroupid,
	(ser.departmentgroupid || 'XN') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	0 as dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else servicepricemoney_nhandan end) as dongia
		from serviceprice 
		where bhyt_groupcode='03XN' and departmentgroupid=46 "+tieuchi_ser+lstdichvu_ser_xn+") ser
	inner join (select vienphiid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentgroupid
	
------------------------
--Bảng cấu hình
CREATE TABLE ml_chiathuongdvvp
(
  chithuongdvvpid serial NOT NULL,
  stt integer,
  keymapping text,
  departmentgroupname text,
  departmentname text,
  quyetdinh_so text,
  quyetdinh_ngay timestamp without time zone,
  tylehuong double precision DEFAULT 0,
  tienbsi_th7cn double precision DEFAULT 0,
  CONSTRAINT ml_chiathuongdvvp_pkey PRIMARY KEY (chithuongdvvpid)
);
CREATE INDEX ml_chiathuongdvvp_keymapping_idx ON ml_chiathuongdvvp USING btree (keymapping);

--Bảng tạm - Lưu dữ liệu querry trên vào bảng này
CREATE TABLE ml_datachithuongdvvp_tmp
(
  datachithuongtmpid serial NOT NULL,
  keymapping text,
  departmentgroupid integer,
  soluong_tong double precision DEFAULT 0,
  soluong_th7cn double precision DEFAULT 0,
  dongia double precision DEFAULT 0,
  thanhtien_tong double precision DEFAULT 0,
  thanhtien_th7cn double precision DEFAULT 0,
  createusercode text,
  createdate timestamp without time zone,
  CONSTRAINT ml_datachithuongdvvp_tmp_pkey PRIMARY KEY (datachithuongtmpid)
);
CREATE INDEX ml_datachithuongdvvp_tmp_keymapping_idx ON ml_datachithuongdvvp_tmp USING btree (keymapping);
CREATE INDEX ml_datachithuongdvvp_tmp_createusercode_idx ON ml_datachithuongdvvp_tmp USING btree (createusercode);
CREATE INDEX ml_datachithuongdvvp_tmp_createdate_idx ON ml_datachithuongdvvp_tmp USING btree (createdate);

---------------------------------------
--SQL lay data cuoi cung

SELECT row_number () over (order by CT1.stt) as stt,
	CT1.departmentgroupname,
	CT1.quyetdinh_so,
	TO_CHAR(CT1.quyetdinh_ngay,'dd/MM/yyyy') as quyetdinh_ngay,
	coalesce(TMP1.soluong_tong,0) as soluong_tong,
	coalesce(TMP1.soluong_th7cn,0) as soluong_th7cn,
	TMP1.dongia,
	coalesce(TMP1.thanhtien_tong) as thanhtien_tong,
	CT1.tylehuong,
	((TMP1.thanhtien_tong*(CT1.tylehuong/100.0))) as tienhuong,
	(TMP1.thanhtien_th7cn) as thanhtien_th7cn,
	0 as chiphi,
	(TMP1.thanhtien_th7cn*0.15) as tienthuong_th7cn,
	((TMP1.soluong_th7cn*CT1.tienbsi_th7cn)) as tienbsi_th7cn,
	((TMP1.thanhtien_tong*(CT1.tylehuong/100.0))+(TMP1.thanhtien_th7cn*0.15)+(TMP1.soluong_th7cn*CT1.tienbsi_th7cn)) as tonghuong,
	'' as kynhan
FROM (select stt,departmentgroupname,quyetdinh_so,quyetdinh_ngay,tylehuong,tienbsi_th7cn from ml_chiathuongdvvp group by stt,departmentgroupname,quyetdinh_so,quyetdinh_ngay,tylehuong,tienbsi_th7cn) CT1
LEFT JOIN
	(select ct.stt as stt1,
		sum(tmp.soluong_tong) as soluong_tong,
		sum(tmp.soluong_th7cn) as soluong_th7cn,
		tmp.dongia,
		sum(tmp.thanhtien_tong) as thanhtien_tong,
		sum((tmp.thanhtien_tong*(ct.tylehuong/100.0))) as tienhuong,
		sum(tmp.thanhtien_th7cn) as thanhtien_th7cn,
		sum((tmp.soluong_th7cn*ct.tienbsi_th7cn)) as tienbsi_th7cn
	from ml_chiathuongdvvp ct 
		inner join (select * from ml_datachithuongdvvp_tmp where createusercode='"+_createusercode+"' and createdate='"+_createdate+"') tmp on tmp.keymapping=ct.keymapping
	group by ct.stt,ct.departmentgroupname,ct.quyetdinh_so,ct.quyetdinh_ngay,tmp.dongia,ct.tylehuong
	--having sum(tmp.soluong_tong)>0;
	) TMP1 on TMP1.stt1=CT1.stt;
	

	






		
		

		
		
		
		
		
		