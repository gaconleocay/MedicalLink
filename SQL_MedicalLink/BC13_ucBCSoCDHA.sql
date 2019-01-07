--So CDHA ser 1.1 ngay 31/8/2017
--them thuoc/vt di kem
--7/8 bo sung thoi gian tra kq tung phan
--31/8 bo sung ng tra ket qua neu nhu nguoi tra kq tung phan = null
--ngay 1/9/2018: loc theo khoa chi dinh
--ngay 27/12/2018: lấy trả KQ từng phần trả bên PACS: X-quang; CT; Cộng hưởng từ

SELECT
		ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt,
		hsba.patientcode,
		hsba.patientname,
		A.maubenhphamid,
		(case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, 
		(case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) 
		- cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu,
		((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) ||
		(case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) ||
		(case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) ||
		(case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) ||
		(case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname else '' end)) as diachi,
		(case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt,
		a.chandoan,
		(kyc.departmentgroupname || ' - ' || pyc.departmentname) as noigui,
		a.yeucau,
		a.ketqua,
		a.maubenhphamdate,
		a.maubenhphamfinishdate, --tra ket qua cuoi cung
		a.vienphidate_ravien,
		a.duyet_ngayduyet_vp,
		(case when A.servicetimetrakq is not null then A.servicetimetrakq end) as servicetimetrakq,
		(case when ntkq.username is not null then ntkq.username else ntkqcc.username end) as nguoidoc,
		(case when a.departmentid_des=244 then 'x' else '' end) as phim_20x25,
		(case when a.departmentid_des in (245,246) then 'x' else '' end) as phim_35x43,
		A.isthuocdikem,
		A.isvattudikem
FROM
	(select * from (SELECT mbp.maubenhphamid,
			mbp.hosobenhanid,
			mbp.departmentgroupid,
			mbp.departmentid,
			mbp.departmentid_des,
			mbp.chandoan,
			mbp.usertrakq, 
			mbp.userthuchien,
			mbp.maubenhphamdate,
			(case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate,
			(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien,
			(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
			se.servicename as yeucau,
			se.servicevalue as ketqua,
			(case when pacs.readingdate is not null then pacs.readingdate else se.servicetimetrakq end) as servicetimetrakq,
			(case when pacs.readingdoctor1 is not null then pacs.readingdoctor1 else se.serviceusertrakq end) as serviceusertrakq,
			(case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle'))>0 then 'X' end) as isthuocdikem,
			(case when (select count(*) from serviceprice where servicepriceid_master=se.servicepriceid and bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle'))>0 then 'X' end) as isvattudikem
	FROM (select maubenhphamid,hosobenhanid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate from maubenhpham where maubenhphamgrouptype=1 and
	departmentid_des='{cboPhongThucHien.EditValue.ToString()}' {tieuchi_mbp} {_theokhoagui}) mbp
		INNER JOIN (select servicepriceid,servicename,servicevalue,maubenhphamid,servicetimetrakq,serviceusertrakq,servicecode from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) {_tieuchi_se}) se ON se.maubenhphamid=mbp.maubenhphamid
		left join (select accessnumber,service_code,to_timestamp(readingdate,'yyyyMMddHH24MIss') at time zone 'utc' as readingdate,readingdoctor1,readingdr1name from resresulttab where {_tieuchi_pacs}) pacs ON pacs.accessnumber=mbp.maubenhphamid::text and pacs.service_code=se.servicecode
		INNER JOIN (select vienphiid,hosobenhanid,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 {tieuchi_vp}) vp on vp.hosobenhanid=mbp.hosobenhanid
		) tmp where 1=1 {_tieuchi_trakqtp}) A
	INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan where 1=1 {tieuchi_hsba}) hsba ON hsba.hosobenhanid=A.hosobenhanid
	LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid
	LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pyc ON pyc.departmentid=A.departmentid
	LEFT JOIN (select userhisid,usercode,username,departmentgroupid from nhompersonnel) ntkq ON ntkq.usercode=A.serviceusertrakq
	LEFT JOIN (select userhisid,username,departmentgroupid from nhompersonnel) ntkqcc ON ntkqcc.userhisid=COALESCE(A.usertrakq,A.userthuchien)
WHERE 1=1 {_theokhoatrakq};
 
 
--theo 
 
 
 
-- _theokhoatrakq = " WHERE (case when ntkq.departmentgroupid is not null then ntkq.departmentgroupid else ntkqcc.departmentgroupid end) in (" + _lstdepartmentgroup + ") ";










 
 
 
 
 
 
 
 