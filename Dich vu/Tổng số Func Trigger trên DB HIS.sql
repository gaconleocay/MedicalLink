----Tổng số trigger; Function; bảng dữ liệu trên DB HIS

--=================== Giám định BHYT
ALTER TABLE bhyt DISABLE TRIGGER bhytlastaccessdate_change;
ALTER TABLE hosobenhan DISABLE TRIGGER hsbalastaccessdate_change;
-- DROP FUNCTION trgbhyt_lastaccessdate();
-- DROP FUNCTION trghosobenhan_lastaccessdate();



---=====================Quản lý bệnh viện MediLink
ALTER TABLE vienphi DISABLE TRIGGER vienphidate_ravien_change;
ALTER TABLE vienphi DISABLE TRIGGER vienphistatus_vp_change;
ALTER TABLE vienphi DISABLE TRIGGER duyet_ngayduyet_change;
-- DROP FUNCTION servicespttt_cal();
-- DROP FUNCTION vienphi_vienphistatus_vp();
-- DROP FUNCTION vienphi_duyet_ngayduyet();






--===============================Bảng dữ liệu
-- DROP TABLE ihs_servicespttt;
-- DROP TABLE nhompersonnel;



--===============================View
-- DROP VIEW view_tools_serviceprice_pttt;
-- DROP VIEW vienphi_money_tm;
-- DROP VIEW vienphi_money_sobn;
-- DROP VIEW vienphi_money;
-- DROP VIEW serviceprice_department;












