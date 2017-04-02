CREATE OR REPLACE FUNCTION vienphi_vienphistatus_vp()
  RETURNS trigger AS
$BODY$
BEGIN
 IF OLD.vienphistatus_vp=1 and NEW.vienphistatus_vp=0 THEN
 UPDATE tools_serviceprice_pttt SET vienphistatus=NEW.vienphistatus, vienphistatus_vp=NEW.vienphistatus_vp, duyet_ngayduyet_vp=NEW.duyet_ngayduyet_vp, vienphistatus_bh=NEW.vienphistatus_bh, duyet_ngayduyet_bh=NEW.duyet_ngayduyet_bh WHERE vienphiid=OLD.vienphiid;
 ELSIF OLD.vienphistatus_vp=0 and NEW.vienphistatus_vp=1 THEN
 UPDATE tools_serviceprice_pttt SET vienphistatus=NEW.vienphistatus, vienphistatus_vp=NEW.vienphistatus_vp, duyet_ngayduyet_vp=NEW.duyet_ngayduyet_vp, vienphistatus_bh=NEW.vienphistatus_bh, duyet_ngayduyet_bh=NEW.duyet_ngayduyet_bh WHERE vienphiid=OLD.vienphiid;
 END IF;
 
 RETURN NEW;
END;
$BODY$

LANGUAGE plpgsql;


---------
-- DROP TRIGGER vienphistatus_vp_change ON vienphi;
CREATE TRIGGER vienphistatus_vp_change
  AFTER UPDATE OF vienphistatus_vp
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE vienphi_vienphistatus_vp();








  