CREATE OR REPLACE FUNCTION vienphiprice_tinh()
  RETURNS trigger AS
$BODY$
DECLARE
  patientname text	:= '';
  tongtienvienphi double precision	:= 0;
BEGIN
 IF NEW.vienphistatus_vp=1 THEN
select hsba.patientname into patientname from hosobenhan hsba where hsba.hosobenhanid=OLD.hosobenhanid;
select sum(bill.datra) into tongtienvienphi from bill where bill.vienphiid=OLD.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0;
 
 INSERT INTO tools_vienphiprice(vienphiid, patientid, patientname, loaivienphiid, vienphistatus, departmentgroupid, departmentid, tongtienvienphi)
 VALUES(OLD.vienphiid,OLD.patientid,patientname,OLD.loaivienphiid, OLD.vienphistatus,OLD.departmentgroupid, OLD.departmentid, tongtienvienphi);
 ELSIF NEW.vienphistatus_vp=0 OR NEW.vienphistatus_vp is null THEN
 DELETE FROM tools_vienphiprice tvp WHERE tvp.vienphiid=OLD.vienphiid;
 END IF;
 
 RETURN NEW;
END;
$BODY$

LANGUAGE plpgsql;


---------
-- DROP TRIGGER vienphistatus_vp_change ON vienphi;
CREATE TRIGGER vienphistatus_vp_change
  AFTER UPDATE OF vienphistatus_vp OR DELETE
  ON vienphi
  FOR EACH ROW
  EXECUTE PROCEDURE vienphiprice_tinh();








  