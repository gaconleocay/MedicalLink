--Báo cáo CHI THƯỞNG DỊCH VỤ VIỆN PHÍ														
--ucBC103_ChiThuongDichVuVienPhi

--ngay 16/8/2018: sua filter XN = bỏ các dv có phân loại PTTT đi

--Cấu hình tại: ml_chiathuongdvvp
--Cấu hình DM dùng chung:REPORT_103_KHOA;REPORT_103_DV_KB;REPORT_103_DV_SADT

--Kham benh - khoa khac
SELECT 
	ser.departmentid as departmentgroupid,
	(ser.departmentid || 'KB') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where bhyt_groupcode='01KB' and departmentid not in (398,"+lstdsphongkham_kbyc+") and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_kb+") ser
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentid,ser.dongia

UNION ALL
--Kham benh - khoa dieu tri Yeu cau
SELECT 
	ser.departmentid as departmentgroupid,
	(ser.departmentid || 'KB') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where bhyt_groupcode='01KB' and departmentid=398 and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_kbdtyc+") ser
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentid,ser.dongia

UNION ALL
--Kham benh - khoa kham benh Yeu cau-thuong
SELECT 
	ser.departmentid as departmentgroupid,
	(ser.departmentid || 'KB') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien,
				(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where bhyt_groupcode='01KB' and departmentid in ("+lstdsphongkham_kbyc+") and EXTRACT(DOW FROM servicepricedate) not in (6,0) and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_kbyc+") ser
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid	
GROUP BY ser.departmentid,ser.dongia

UNION ALL
--Kham benh - khoa kham benh Yeu cau-th7,cn
SELECT 
	ser.departmentid as departmentgroupid,
	(ser.departmentid || 'KBTH7CN') as keymapping,
	sum(ser.soluong) as soluong_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then ser.soluong else 0 end) as soluong_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then ser.soluong else 0 end) as soluong_th7cn,
	ser.dongia,
	sum(ser.soluong*ser.dongia) as thanhtien_tong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) not in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_ngaythuong,
	sum(case when EXTRACT(DOW FROM ser.servicepricedate) in (6,0) then (ser.soluong*ser.dongia) else 0 end) as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where bhyt_groupcode='01KB' and departmentid in ("+lstdsphongkham_kbyc+") and EXTRACT(DOW FROM servicepricedate) in (6,0) and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_kbycth7cn+") ser
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid	
GROUP BY ser.departmentid,ser.dongia
		
UNION ALL
--Sieu am-dien tim
SELECT 
	ser.departmentgroupid,
	(ser.departmentgroupid || 'SADT') as keymapping,
	sum(ser.soluong) as soluong_tong,
	0 as soluong_ngaythuong,
	0 as soluong_th7cn,
	0 as dongia,
	0 as thanhtien_tong,
	sum(ser.soluong*ser.dongia) as thanhtien_ngaythuong,	
	0 as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where bhyt_groupcode in ('04CDHA','05TDCN') and departmentgroupid=46 and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_sadt+") ser
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentgroupid

UNION ALL
--Xet nghiem Yeu cau
SELECT 
	ser.departmentgroupid,
	(ser.departmentgroupid || 'XN') as keymapping,
	sum(ser.soluong) as soluong_tong,
	0 as soluong_ngaythuong,
	0 as soluong_th7cn,
	0 as dongia,
	0 as thanhtien_tong,
	sum(ser.soluong*ser.dongia) as thanhtien_ngaythuong,	
	0 as thanhtien_th7cn
FROM (select vienphiid,departmentgroupid,departmentid,soluong,servicepricecode,servicepricename,servicepricedate,billid_thutien,billid_clbh_thutien,
			(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai
					else (case when loaidoituong=0 then servicepricemoney_bhyt
								  when loaidoituong=1 then servicepricemoney_nhandan
								  else servicepricemoney
						  end)
				end) as dongia
		from serviceprice 
		where bhyt_groupcode in ('03XN','07KTC') and departmentgroupid=46 and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_xn+") ser
	inner join (select servicepricecode from servicepriceref where servicegrouptype=2 and pttt_loaiid=0) serf on serf.servicepricecode=ser.servicepricecode	
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentgroupid;
	
	
	
	
	
	
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
  soluong_ngaythuong double precision DEFAULT 0,
  soluong_th7cn double precision DEFAULT 0,
  dongia double precision DEFAULT 0,
  thanhtien_tong double precision DEFAULT 0,
  thanhtien_ngaythuong double precision DEFAULT 0,
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
	coalesce(TMP1.soluong_ngaythuong,0) as soluong_ngaythuong,
	coalesce(TMP1.soluong_th7cn,0) as soluong_th7cn,
	TMP1.dongia,
	coalesce(TMP1.thanhtien_tong) as thanhtien_tong,
	coalesce(TMP1.thanhtien_ngaythuong) as thanhtien_ngaythuong,
	CT1.tylehuong,
	((TMP1.thanhtien_ngaythuong*(CT1.tylehuong/100.0))) as tienhuong,
	(TMP1.thanhtien_th7cn) as thanhtien_th7cn,
	0 as chiphi,
	(TMP1.thanhtien_th7cn*0.15) as tienthuong_th7cn,
	((TMP1.soluong_th7cn*CT1.tienbsi_th7cn)) as tienbsi_th7cn,
	((TMP1.thanhtien_ngaythuong*(CT1.tylehuong/100.0))+(TMP1.thanhtien_th7cn*0.15)+(TMP1.soluong_th7cn*CT1.tienbsi_th7cn)) as tonghuong,
	'' as kynhan
FROM (select stt,departmentgroupname,quyetdinh_so,quyetdinh_ngay,tylehuong,tienbsi_th7cn from ml_chiathuongdvvp group by stt,departmentgroupname,quyetdinh_so,quyetdinh_ngay,tylehuong,tienbsi_th7cn) CT1
LEFT JOIN
	(select ct.stt as stt1,
		sum(tmp.soluong_tong) as soluong_tong,
		sum(tmp.soluong_ngaythuong) as soluong_ngaythuong,
		sum(tmp.soluong_th7cn) as soluong_th7cn,
		tmp.dongia,
		sum(tmp.thanhtien_tong) as thanhtien_tong,
		sum(tmp.thanhtien_ngaythuong) as thanhtien_ngaythuong,
		sum((tmp.thanhtien_ngaythuong*(ct.tylehuong/100.0))) as tienhuong,
		sum(tmp.thanhtien_th7cn) as thanhtien_th7cn,
		sum((tmp.soluong_th7cn*ct.tienbsi_th7cn)) as tienbsi_th7cn
	from ml_chiathuongdvvp ct 
		inner join (select * from ml_datachithuongdvvp_tmp where createusercode='"+_createusercode+"' and createdate='"+_createdate+"') tmp on tmp.keymapping=ct.keymapping
	group by ct.stt,ct.departmentgroupname,ct.quyetdinh_so,ct.quyetdinh_ngay,tmp.dongia,ct.tylehuong
	) TMP1 on TMP1.stt1=CT1.stt;
	

	






		
		

		
		
		
		
		
		