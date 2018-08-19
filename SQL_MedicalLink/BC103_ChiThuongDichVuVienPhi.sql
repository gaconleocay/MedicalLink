--Báo cáo CHI THƯỞNG DỊCH VỤ VIỆN PHÍ														
--ucBC103_ChiThuongDichVuVienPhi

--ngay 17/8/2018: sua filter XN = bỏ các dv có phân loại PTTT đi

--Cấu hình tại: ml_chiathuongdvvp
--Cấu hình DM dùng chung:REPORT_103_KHOA;REPORT_103_DV_KB;REPORT_103_DV_SADT

--Kham benh
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
		where (case when departmentid in (201,202) then servicepricecode='15'
				when departmentid in (212) then servicepricecode='15'
				when departmentid in (234,216,227,356,232,226,229,203,357,231,235,307,237,236,228,225,233,423,460) then servicepricecode='15'
				when departmentid in (222) then servicepricecode='15'
				when departmentid in (220) then servicepricecode='15'
				when departmentid in (408) then servicepricecode='15'
				when departmentid in (278) then servicepricecode='15'
				when departmentid in (393,394) then servicepricecode='15'
				when departmentid in (398) then servicepricecode='14'
				when departmentid in (205,206,207,208,209,211,354) then servicepricecode in ('14')
				when departmentid in (14) then servicepricecode='U18843-5947'
				when departmentid in (379) then servicepricecode='14'
			end)
			and bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+") ser
	inner join (select vienphiid,doituongbenhnhanid from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
GROUP BY ser.departmentid,ser.dongia
UNION ALL
--Kham benh th7,cn
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
		where bhyt_groupcode='01KB' and departmentid in (205,206,207,208,209,211,354) and EXTRACT(DOW FROM servicepricedate) in (6,0) and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) "+tieuchi_ser+lstdichvu_ser_kbycth7cn+") ser
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



	
	
	
	
------------------------================================
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
	

	


	
-----Chi tiet - 16/8/2018

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
	ser.servicepricecode,
	ser.servicepricename,
	ser.soluong, 
	ser.dongia,
	(ser.soluong*ser.dongia) as thanhtien,
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
	(case when ser.bhyt_groupcode='01KB' then 'Khám bệnh'
			when ser.bhyt_groupcode in ('04CDHA','05TDCN') then 'Siêu âm, điện tim'
			else 'Xét nghiệm'
		end) as bhyt_groupname,
	vp.vienphidate,
	(case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,	
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus,
	(case when ser.billid_thutien>0 or ser.billid_clbh_thutien>0 then 'Đã thu tiền' end) trangthaithutien
FROM 
	(select servicepriceid,vienphiid,maubenhphamid,departmentgroupid,departmentid,servicepricecode,servicepricename,billid_thutien,billid_clbh_thutien,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,servicepricedate,maubenhphamphieutype,bhyt_groupcode,loaidoituong 
		from serviceprice 
		where (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) 
			and ((case when departmentid in (201,202) then servicepricecode='15'
				when departmentid in (212) then servicepricecode='15'
				when departmentid in (234,216,227,356,232,226,229,203,357,231,235,307,237,236,228,225,233,423,460) then servicepricecode='15'
				when departmentid in (222) then servicepricecode='15'
				when departmentid in (220) then servicepricecode='15'
				when departmentid in (408) then servicepricecode='15'
				when departmentid in (278) then servicepricecode='15'
				when departmentid in (393,394) then servicepricecode='15'
				when departmentid in (398) then servicepricecode='14'
				when departmentid in (205,206,207,208,209,211,354) then servicepricecode in ('14','U18843-5947')
				when departmentid in (14) then servicepricecode='U18843-5947'
				when departmentid in (379) then servicepricecode='14'
			end) 
		or 
		(case when departmentgroupid=46 then (bhyt_groupcode in ('04CDHA','05TDCN') and servicepricecode in ("+this.DanhMucDichVu_SADT_String+")) end)
		or 
		(case when departmentgroupid=46 then (bhyt_groupcode in ('03XN','07KTC') and servicepricecode in (select servicepricecode from servicepriceref where servicegrouptype=2 and pttt_loaiid=0)) end))
		"+tieuchi_ser+") ser
	INNER JOIN (select maubenhphamid,maubenhphamstatus,maubenhphamdate,userid,departmentid_des from maubenhpham where maubenhphamgrouptype in (0,1,2) "+_tieuchi_mbp+") mbp on mbp.maubenhphamid=ser.maubenhphamid		
	INNER JOIN (select vienphiid,patientid,vienphistatus,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus_vp,duyet_ngayduyet_vp from vienphi where 1=1 "+tieuchi_vp+trangthai_vp+") vp on vp.vienphiid=ser.vienphiid
	INNER JOIN (select hosobenhanid,patientname,bhytcode from hosobenhan where 1=1 "+_tieuchi_hsba+_hosobenhanstatus+") hsba on hsba.hosobenhanid=vp.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid 
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=ser.departmentid




		
		
		
		
		
		