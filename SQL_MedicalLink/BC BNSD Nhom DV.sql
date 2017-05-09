SELECT ROW_NUMBER() OVER (ORDER BY vp.vienphiid, 
	vp.duyet_ngayduyet_vp) as stt, 
	vp.patientid, 
	vp.vienphiid, 
	hsba.patientname, 
	(case hsba.gioitinhcode  
		when '01' then 'Nam' 
		when '02' then 'Nữ' 
		end) as gioitinh, 
	to_char(hsba.birthday, 'yyyy') as namsinh, 
	(case vp.vienphistatus  
		when 2 then 'Đã duyệt VP'  
		when 1 then (case vp.vienphistatus_vp  
						when 1 then 'Đã duyệt VP'  
						else 'Đã đóng BA'  
						end)  
				else 'Đang điều trị' end) as trangthai, 	
	vp.vienphidate as thoigianvaovien, 
	vp.vienphidate_ravien as thoigianravien, 
	vp.duyet_ngayduyet_vp as thoigianduyetvp, 
	vp.duyet_ngayduyet as thoigianduyetbh, 
	depg.departmentgroupname as khoaravien,  
	de.departmentname as phongravien, 
	bhyt.bhytcode 
FROM vienphi vp 
	INNER JOIN hosobenhan hsba ON vp.hosobenhanid=hsba.hosobenhanid  
	INNER JOIN departmentgroup depg ON vp.departmentgroupid=depg.departmentgroupid  
	INNER JOIN department de ON vp.departmentid=de.departmentid  
	INNER JOIN bhyt ON bhyt.bhytid=vp.bhytid 
	INNER JOIN serviceprice ser ON ser.vienphiid=vp.vienphiid				 
WHERE ser.servicepricecode in (" + dsdv + ") " + tieuchi + " >= '" + datetungay + "' " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + " ; 
GROUP BY vp.patientid, vp.vienphiid, hsba.patientname, hsba.gioitinhcode, hsba.birthday, vp.vienphistatus, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet_vp, vp.duyet_ngayduyet, depg.departmentgroupname, de.departmentname, bhyt.bhytcode 
 
 
---------
SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepriceid) as stt, 
	ser.vienphiid,
	(case ser.bhyt_groupcode 
		when '01KB' then 'Khám bệnh' 
		when '03XN' then 'Xét nghiệm' 
		when '04CDHA' then 'CĐHA' 
		when '05TDCN' then 'CĐHA' 
		when '06PTTT' then 'PTTT' 
		when '07KTC' then 'DV KTC' 
		when '12NG' then 'Ngày giường' 
		when '08MA' then 'Máu'
		when '09TDT' then 'Thuốc'
		when '091TDTtrongDM' then 'Thuốc'
		when '092TDTngoaiDM' then 'Thuốc'
		when '093TDTUngthu' then 'Thuốc'
		when '094TDTTyle' then 'Thuốc TT tỷ lệ'
		when '10VT' then 'Vật tư'
		when '101VTtrongDM' then 'Vật tư'
		when '101VTtrongDMTT' then 'Vật tư'
		when '102VTngoaiDM' then 'Vật tư'
		when '103VTtyle' then 'Vật tư TT tỷ lệ'
		when '11VC' then 'Vận chuyển'
		when '999DVKHAC' then 'Khác'
		when '1000PhuThu' then 'Phụ thu'
		else '' end) as bhyt_groupcode,	
	ser.maubenhphamid,
	ser.servicepricecode,
	ser.servicepricename,
	ser.soluong,
	ser.servicepricemoney,
	ser.servicepricemoney_nhandan,
	ser.servicepricemoney_bhyt,
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC '
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		end) as loaidoituong,
	ser.servicepricedate,
	case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end as loaiphieu,
	degp.departmentgroupname,
	de.departmentname
FROM serviceprice ser
	inner join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid
	inner join department de on de.departmentid=ser.departmentid
WHERE ser.vienphiid="+vienphiid+";











 
 
 
