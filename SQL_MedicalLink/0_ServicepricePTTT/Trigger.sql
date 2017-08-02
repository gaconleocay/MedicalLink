--Ngay 2/8

-- Trigger: vienphidate_ravien_change on vienphi

-- DROP TRIGGER vienphidate_ravien_change ON vienphi;

CREATE TRIGGER vienphidate_ravien_change
  AFTER UPDATE OF vienphidate_ravien OR DELETE
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE servicespttt_cal();
ALTER TABLE vienphi DISABLE TRIGGER vienphidate_ravien_change;



-----
-- Trigger: vienphistatus_vp_change on vienphi

-- DROP TRIGGER vienphistatus_vp_change ON vienphi;

CREATE TRIGGER vienphistatus_vp_change
  AFTER UPDATE OF vienphistatus_vp
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE vienphi_vienphistatus_vp();
ALTER TABLE vienphi DISABLE TRIGGER vienphistatus_vp_change;


-- Trigger: duyet_ngayduyet_change on vienphi

-- DROP TRIGGER duyet_ngayduyet_change ON vienphi;

CREATE TRIGGER duyet_ngayduyet_change
  AFTER UPDATE OF duyet_ngayduyet
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE vienphi_duyet_ngayduyet();
ALTER TABLE vienphi DISABLE TRIGGER duyet_ngayduyet_change;




