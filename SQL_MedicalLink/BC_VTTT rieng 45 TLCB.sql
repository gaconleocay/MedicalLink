---Bao cao VT thanh toan rieng > 45 TLCB ngay 30/8
--Chinh sua lay tat cả vtyt có id là tt riêng, không cần so sánh vs 45 tlcb
--ngay 11/10: them stent1,2
--ngay 3/7/2018: them XN, CDHA duoc vt tt rieng di kem


SELECT row_number () over (" + _orderby + ") as stt,
	VT.servicepriceid_thanhtoanrieng as servicepriceid,
	VP.patientid,
	VP.vienphiid,
	HSBA.patientname,
	HSBA.bhytcode,
	VP.vienphidate,
	(case when VP.vienphidate_ravien<>'0001-01-01 00:00:00' then VP.vienphidate_ravien end) as vienphidate_ravien,
	(case when VP.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then VP.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	KRV.departmentgroupname as khoaravien,
	SER.servicepricecode,
	SER.servicepricename_bhyt as servicepricename,
	SER.servicepricemoney,
	SER.soluong,
	(SER.servicepricemoney*SER.soluong) as thanhtien,
	SER.servicepricedate,
	KCD.departmentgroupname as khoachidinh,
	PCD.departmentname as phongchidinh,
	VT.thanhtien_vtyt,
	VT.thanhtien_vtyttran,
	(VT.thanhtien_vtyttran - (case when VT.stent2>36000000 then 18000000 else VT.stent2/2 end)) as stent1,
	(case when VT.stent2>36000000 then 18000000 else VT.stent2/2 end) as stent2,
	0 as bhyt_tt,
	VT.ghichu,
	VP.bhyt_thangluongtoithieu
FROM
	(select servicepriceid_thanhtoanrieng,vienphiid,
		sum(case when maubenhphamphieutype=0 
				then (soluong*servicepricemoney_bhyt) 
			  else 0-(soluong*servicepricemoney_bhyt)	
			  end) as thanhtien_vtyt,
	/*sum(case when stenphuthuoc=1 
				then (case when maubenhphamphieutype=0
								then (soluong*(case when cast(servicepricebhytdinhmuc as numeric)>0 then cast(servicepricebhytdinhmuc as numeric) else servicepricemoney_bhyt end)) 
						  else 0-(soluong*(case when cast(servicepricebhytdinhmuc as numeric)>0 then cast(servicepricebhytdinhmuc as numeric) else servicepricemoney_bhyt end))	
						  end)
				else 0 end) as stent1, */
	sum(case when stenphuthuoc=2
				then (case when maubenhphamphieutype=0 
								then (soluong*servicepricemoney_bhyt) 
							else 0-(soluong*servicepricemoney_bhyt)	
					 end)
				else 0
				end) as stent2,			  
	sum(case when maubenhphamphieutype=0 
				then (soluong*(case when cast(servicepricebhytdinhmuc as numeric)>0 then cast(servicepricebhytdinhmuc as numeric) else servicepricemoney_bhyt end)) 
			  else 0-(soluong*(case when cast(servicepricebhytdinhmuc as numeric)>0 then cast(servicepricebhytdinhmuc as numeric) else servicepricemoney_bhyt end))	
			  end) as thanhtien_vtyttran,  
		string_agg(servicepricename_bhyt, '; ') as ghichu
	from serviceprice
	where servicepriceid_thanhtoanrieng>0 and loaidoituong=20 and bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') "+_tieuchi_ser+"
	group by servicepriceid_thanhtoanrieng,vienphiid) VT
INNER JOIN 
	(select vienphiid,patientid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet,duyet_ngayduyet_vp,departmentgroupid,bhyt_thangluongtoithieu from vienphi where "+_trangthaivienphi+_tieuchi_vp+") VP ON VP.vienphiid=VT.vienphiid
INNER JOIN 
	(select hosobenhanid,patientid,patientname,bhytcode from hosobenhan) HSBA ON HSBA.hosobenhanid=VP.hosobenhanid 
LEFT JOIN
	(select servicepriceid,vienphiid,servicepricecode,servicepricename_bhyt,servicepricedate,soluong,
			(case loaidoituong when 0 then servicepricemoney_bhyt
							   when 1 then servicepricemoney_nhandan
							   else servicepricemoney
				end) as servicepricemoney,
			departmentgroupid,departmentid
		from serviceprice 
		where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC') and loaidoituong in (0,1,3,4,6)
			"+_tieuchi_ser+"
		) SER ON SER.servicepriceid=VT.servicepriceid_thanhtoanrieng
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=SER.departmentgroupid
LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=SER.departmentid		
LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) Krv ON Krv.departmentgroupid=VP.departmentgroupid;
		
		
		
		
		
		
		
