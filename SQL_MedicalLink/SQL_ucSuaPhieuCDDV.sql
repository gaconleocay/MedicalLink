--ngay 8/2/2018: lay danh sach phieu chi dinh

SELECT mbp.maubenhphamid, 
	mbp.medicalrecordid, 
	hsba.patientid, 
	vp.vienphiid, 
	hsba.patientname, 
	mbp.maubenhphamtype, 
	mbp.phieudieutriid, 
	mbp.maubenhphamgrouptype as maubenhphamgrouptypeid,
	(case mbp.maubenhphamgrouptype when 0 then 'Xét nghiệm' when 1 then 'CĐHA' when 2 then 'Khám bệnh' when 3 then 'Phiếu điều trị' when 4 then 'Chuyên khoa' when 5 then 'Thuốc' when 6 then 'Vật tư' else '' end) as maubenhphamgrouptype, 
	(case mbp.maubenhphamstatus when 0 then 'Chưa gửi YC' when 1 then 'Đã gửi YC' when 2 then 'Đã trả kết quả' when 4 then 'Tổng hợp y lệnh' when 5 then 'Đã xuất thuốc/VT' when 7 then 'Đã trả thuốc' when 8 then 'Chưa duyệt thuốc' when 9 then 'Đã xuất tủ trực' when 16 then 'Đã tiếp nhận bệnh phẩm' else '' end) as maubenhphamstatus, 
	mbp.maubenhphamdate, 
	mbp.maubenhphamdate_sudung,
	mbp.maubenhphamfinishdate,
	(case mbp.dathutien when 1 then 'Đã thu tiền' else '' end) as dathutien, 
	mbp.dathutien as dathutienid, 
	kcd.departmentgroupname, 
	pcd.departmentname, 
	mbp.isdeleted, 
	(case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end) as trangthai,
	(case when maubenhphamgrouptype in (5,6) then (select msto.medicinestorename from medicine_store msto where mbp.medicinestoreid=msto.medicinestoreid) when maubenhphamgrouptype in (0,1,2) then (select dep.departmentname from department dep where mbp.departmentid_des=dep.departmentid) else '' end) as phongthuchien, 
	COALESCE(vp.vienphistatus_vp,0) as vienphistatus_vp,
	mbp.medicinestorebillid,
	(case mbp.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end) as maubenhphamphieutype, 
	mbp.maubenhphamphieutype as maubenhphamphieutypeid 
FROM maubenhpham mbp 
INNER JOIN hosobenhan hsba on mbp.hosobenhanid=hsba.hosobenhanid 
INNER JOIN vienphi vp on vp.hosobenhanid=hsba.hosobenhanid 
INNER JOIN (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) pcd ON pcd.departmentid=mbp.departmentid 
INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=mbp.departmentgroupid
WHERE " + timkiemtheo + " 
ORDER BY mbp.maubenhphamgrouptype,mbp.maubenhphamid;  






