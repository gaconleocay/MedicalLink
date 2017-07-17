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
