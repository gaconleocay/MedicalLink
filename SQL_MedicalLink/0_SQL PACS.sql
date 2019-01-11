SELECT rs.accessnumber ,json_agg(row_to_json((  SELECT t FROM (  SELECT rs.patientid, rs.accessnumber as orderId, rs.epatientname, rs.dateofbirth as birthDate,de.departmentcode,  
		(case when de.departmentcode='KCDHA_CT' then 'CT'  		
			else (case when de.departmentcode='KCDHA_XQ' then 'CR'  			
				else (case when de.departmentcode='KCDHA_CHT' then 'MR'  				
					else (case when de.departmentcode='KCDHA_XQCT' then 'XA' end)  			      end)  		      
			end)   
		end) as modality, rs.patientsex as gender, rs.patientclass, rs.reqphysicianid as doctorOrderId, rs.reqphysicianname as doctorOrderName, 
		rs.orderdate,  rs.examcode as procedureOrderId, rs.examdescription as procedureOrderName) t ))) as orderlist  
FROM resordertab rs   
inner join (select servicepricerefid,servicepricecode,listdepartmentphongthuchien from servicepriceref) serf on serf.servicepricecode=rs.examcode   
inner join (select servicepricerefid,servicepricecode, cast(regexp_split_to_table(listdepartmentphongthuchien, ';') as text) as departmentphongthuchien from servicepriceref) serf2 on serf2.servicepricerefid=serf.servicepricerefid  
inner join (select departmentid,departmentcode from department where (departmentcode = 'KCDHA_CT' OR departmentcode = 'KCDHA_XQ' OR departmentcode = 'KCDHA_CHT' OR departmentcode = 'KCDHA_XQCT')) de on cast(de.departmentid as text)=serf2.departmentphongthuchien  WHERE rs.ResStatus ='U'  GROUP BY rs.accessnumber


----
SELECT re.examcode, vp.vienphiid as ma_lien_ket,bh.bhytcode as ma_the_bhyt, 
	(case when vp.loaivienphiid=0 then 1  		
		else (case when dept.departmenttype <> 2 then 1 				
				else (case when mbp.dacodichvuthutien=1 or mbp.dacodichvuduyetcanlamsang=1 then 1  							
						else 0 end)  				end)					
	end) as duocthuchiencls,
	hsba.birthday,
	hsba.birthday_year
FROM (select accessnumber,examcode from Resordertab where accessnumber='19882215') re 	
inner join (select vienphiid,maubenhphamid,dacodichvuthutien,dacodichvuduyetcanlamsang,departmentid,hosobenhanid from maubenhpham) mbp on mbp.maubenhphamid=cast(re.accessnumber as numeric) and mbp.maubenhphamid=19882215	
inner join (select vienphiid,bhytid,loaivienphiid from vienphi) vp on vp.vienphiid=mbp.vienphiid 	
inner join (select bhytcode,bhytid from bhyt) bh on bh.bhytid=vp.bhytid	
inner join (select departmentid,departmenttype,departmentcode from department) dept on dept.departmentid=mbp.departmentid
inner join (select hosobenhanid,birthday,birthday_year from hosobenhan) hsba on hsba.hosobenhanid=mbp.hosobenhanid






