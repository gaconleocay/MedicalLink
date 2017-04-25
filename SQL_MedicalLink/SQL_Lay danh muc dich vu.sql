SELECT CASE serf.servicegrouptype 
			WHEN 1 THEN 'Khám bệnh' 
			WHEN 2 THEN 'Xét nghiệm' 
			WHEN 3 THEN 'Chẩn đoán hình ảnh' 
			WHEN 4 THEN 'Chuyên khoa'  
			ELSE '' END AS SERVICEGROUPTYPE_NAME, 
	serf.servicepricecode AS SERVICEPRICECODE, 
	serf.servicepricegroupcode AS SERVICEPRICEGROUPCODE, 
	serf.servicepricecodeuser AS SERVICEPRICECODEUSER, 
	serf.servicepricesttuser AS SERVICEPRICESTTUSER,
	serf.servicepricename AS SERVICEPRICENAME,	
	serf.servicepricenamebhyt AS SERVICEPRICENAMEBHYT,
	serf.servicepriceunit AS SERVICEPRICEUNIT, 
	serf.servicepricefeebhyt AS SERVICEPRICEFEEBHYT, 
	serf.servicepricefeenhandan AS SERVICEPRICEFEENHANDAN, 
	serf.servicepricefee AS SERVICEPRICEFEE, 
	serf.servicepricefeenuocngoai AS SERVICEPRICEFEENUOCNGOAI, 
	serf.servicepricefee_old_date AS SERVICEPRICEFEE_OLD_DATE, 
	case serf.servicepricefee_old_type
		when 1 then 'Ngày chỉ định'
		else 'Ngày vào viện' end AS SERVICEPRICEFEE_OLD_TYPE_NAME, 
	case serf.pttt_hangid
		when 1 then 'Đặc biệt'
		when 2 then 'Loại 1A'
		when 3 then 'Loại 1B'
		when 4 then 'Loại 1C'
		when 5 then 'Loại 2A'
		when 6 then 'Loại 2B'
		when 7 then 'Loại 2C'
		when 8 then 'Loại 3'
		when 9 then 'Loại 1'
		when 10 then 'Loại 2' end AS PTTT_HANGID_NAME,
	case serf.pttt_loaiid 
		when 1 then 'Phẫu thuật đặc biệt'
		when 2 then 'Phẫu thuật loại 1'
		when 3 then 'Phẫu thuật loại 2'
		when 4 then 'Phẫu thuật loại 3'
		when 5 then 'Thủ thuật đặc biệt'
		when 6 then 'Thủ thuật loại 1'
		when 7 then 'Thủ thuật loại 2'
		when 8 then 'Thủ thuật loại 3' end AS PTTT_LOAIID_NAME,
	serf.servicelock AS SERVICELOCK,
	case serf.bhyt_groupcode
		when '01KB' then 'Khám bệnh'
		when '03XN' then 'Xét nghiệm'
		when '04CDHA' then 'CĐHA'
		when '05TDCN' then 'Thăm dò CN'
		when '06PTTT' then 'PTTT'
		when '07KTC' then 'DV kỹ thuật cao'
		when '11VC' then 'Vận chuyển'
		when '12NG' then 'Ngày giường'
		when '999DVKHAC' then 'DV khác'
		when '1000PhuThu' then 'Phụ thu' end AS BHYT_GROUPCODE_NAME,
	serf.report_groupcode AS REPORT_GROUPCODE,
	serf.report_tkcode AS REPORT_TKCODE,
	serf.servicepricetype AS SERVICEPRICETYPE,
	case when serf.servicegrouptype in (1,3,4) 
		then ser.servicecode
		end AS SERVICECODE
FROM servicepriceref serf
	inner join serviceref4price ser on ser.servicepricecode=serf.servicepricecode
WHERE isremove is null and serf.servicepricecode <>'' and serf.servicegrouptype in (1,2,3,4) 
ORDER BY serf.servicegrouptype, serf.servicepricegroupcode, serf.servicepricename;













