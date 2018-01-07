---SQL gop benh an

update serviceprice
set vienphiid=1057777,hosobenhanid=1051831,medicalrecordid=1552975
where vienphiid=1057964;

update maubenhpham
set vienphiid=1057777,hosobenhanid=1051831,medicalrecordid=1552975,patientid=760747
where vienphiid=1057964;

update bill 
set vienphiid=1057777,hosobenhanid=1051831,medicalrecordid=1552975,patientid=760747
where vienphiid=1057964;

------

SELECT mrd.medicalrecordid,
		vp.vienphiid,
		vp.patientid,
		vp.bhytid,
		hsba.patientname,
		coalesce(vp.vienphistatus_vp,0) as vienphistatus_vp,
		(case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as vienphistatus,
		mrd.thoigianvaovien,
		(case when thoigianravien<>'0001-01-01 00:00:00' then thoigianravien end) as thoigianravien,
		degp.departmentgroupname,
		mrd.departmentid,
		de.departmentname,
		hsba.hosobenhanid
FROM medicalrecord mrd 
	inner join vienphi vp on vp.vienphiid=mrd.vienphiid
	inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid
	left join department de on de.departmentid=mrd.departmentid
where mrd.vienphiid='"+txtVienPhiId_A.Text+"' and vp.vienphiid='"+txtVienPhiId_A.Text+"'
order by mrd.medicalrecordid;

-------
SELECT row_number () over (order by mbp.maubenhphamgrouptype,mbp.maubenhphamdate) as stt,
	mbp.maubenhphamid,
	mbp.medicalrecordid,
	mbp.patientid,
	mbp.vienphiid,
	(case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 3 then 'Phiếu điều trị' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype,
	(case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus,
	degp.departmentgroupname,
	de.departmentname,
	(case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien,
	mbp.maubenhphamdate,
	(case when mbp.maubenhphamdate_sudung<>'0001-01-01 00:00:00' then mbp.maubenhphamdate_sudung end) as maubenhphamdate_sudung,
	(case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien,
	(case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype
FROM maubenhpham mbp
	inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid
	left join department de on de.departmentid=mbp.departmentid
WHERE maubenhphamgrouptype<>3 and medicalrecordid='"+_medicalrecordid+"';


-----------
SELECT bhytcode, macskcbbd, to_char(bhytfromdate,'yyyy-MM-dd HH24:MI:ss') as bhytfromdate, to_char(bhytutildate,'yyyy-MM-dd HH24:MI:ss') as bhytutildate, du5nam6thangluongcoban, dtcbh_luyke6thang, noisinhsong FROM bhyt WHERE hosobenhanid='"++"';
  
UPDATE bhyt 
SET theghep_bhytcode='"+_dataBHYT_B.Rows[0]["bhytcode"].ToString()+"',
theghep_bhytfromdate='"+_dataBHYT_B.Rows[0]["bhytfromdate"].ToString()+"',
theghep_bhytutildate='"+_dataBHYT_B.Rows[0]["bhytutildate"].ToString()+"',
theghep_macskcbbd='"+_dataBHYT_B.Rows[0]["macskcbbd"].ToString()+"',
theghep_du5nam6thangluongcoban='"+_dataBHYT_B.Rows[0]["du5nam6thangluongcoban"].ToString()+"',
theghep_dtcbh_luyke6thang='"+_dataBHYT_B.Rows[0]["dtcbh_luyke6thang"].ToString()+"',
theghep_noisinhsong='"+_dataBHYT_B.Rows[0]["noisinhsong"].ToString()+"'
WHERE bhytid='"++"'


--- neu co department
update maubenhpham
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+",medicalrecordid="+_lst_Phong[0].medicalrecordid+",patientid="+_lst_Phong[0].patientid+"
where vienphiid="+item_B.vienphiid+";

update serviceprice
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+",medicalrecordid="+_lst_Phong[0].medicalrecordid+"
where vienphiid="+item_B.vienphiid+";

update bill 
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+",medicalrecordid="+_lst_Phong[0].medicalrecordid+",patientid="+_lst_Phong[0].patientid+"
where vienphiid="+item_B.vienphiid+";

--Neu chua co department
update maubenhpham
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+",patientid="+_lst_Phong[0].patientid+"
where vienphiid="+item_B.vienphiid+";

update serviceprice
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+"
where vienphiid="+item_B.vienphiid+";

update bill 
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+",patientid="+_lst_Phong[0].patientid+"
where vienphiid="+item_B.vienphiid+";

update medicalrecord 
set vienphiid="+_lst_Phong[0].vienphiid+",hosobenhanid="+_lst_Phong[0].hosobenhanid+",patientid="+_lst_Phong[0].patientid+"
where medicalrecordid="+item_B.medicalrecordid+";










