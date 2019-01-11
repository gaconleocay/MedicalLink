---Trigger Function 2019
--Lấy dữ liệu sang server báo cáo 246


-----================================================================ TRIGGER - HIS
-- HIS: vienphidate_ravien_change on vienphi: lấy mã viện phí sang SV_BaoCao

CREATE TRIGGER vienphidate_ravien_change
  AFTER UPDATE OF vienphidate_ravien OR DELETE ON vienphi
  FOR EACH ROW EXECUTE PROCEDURE takevienphiid_rv();
ALTER TABLE vienphi DISABLE TRIGGER vienphidate_ravien_change;

-- HIS: vienphistatus_vp_change ON vienphi: lấy mã viện phí sang SV_BaoCao

CREATE TRIGGER vienphistatus_vp_change
  AFTER UPDATE OF vienphistatus_vp ON vienphi 
  FOR EACH ROW EXECUTE PROCEDURE takevienphiid_sttvp();
ALTER TABLE vienphi DISABLE TRIGGER vienphistatus_vp_change;

-- HIS: duyet_ngayduyet_change ON vienphi: lấy mã viện phí sang SV_BaoCao

CREATE TRIGGER duyet_ngayduyet_change
  AFTER UPDATE OF duyet_ngayduyet ON vienphi
  FOR EACH ROW EXECUTE PROCEDURE takevienphiid_sttbh();
ALTER TABLE vienphi DISABLE TRIGGER duyet_ngayduyet_change;

-----================================================================ FUNCTION - HIS

--HIS: takevienphiid_rv();
--ra viện status=1; hủy ra viện+xóa thì status=0

CREATE OR REPLACE FUNCTION takevienphiid_rv() RETURNS trigger AS
$BODY$
DECLARE
	query text;
    id integer;
BEGIN
 IF TG_OP = 'DELETE' THEN
	id = NEW.vienphiid;
    query = 'INSERT INTO his_vienphi(vienphiid,status) VALUES (''' || id || ''',0)';
    PERFORM dblink_exec('dbname=O2SMedicalLink port=5432 host=localhost user=takevp password=abc123',query);
	RETURN OLD;
 END IF;
 IF NEW.vienphidate_ravien<>'0001-01-01 00:00:00' THEN
	id = NEW.vienphiid;
    query = 'INSERT INTO his_vienphi(vienphiid,status) VALUES (''' || id || ''',1)';
    PERFORM dblink_exec('dbname=O2SMedicalLink port=5432 host=localhost user=takevp password=abc123',query);
	RETURN NEW;
 ELSIF NEW.vienphidate_ravien='0001-01-01 00:00:00' THEN
	id = NEW.vienphiid;
    query = 'INSERT INTO his_vienphi(vienphiid,status) VALUES (''' || id || ''',0)';
    PERFORM dblink_exec('dbname=O2SMedicalLink port=5432 host=localhost user=takevp password=abc123',query);
  RETURN NEW;
 END IF;
 
END;
$BODY$
  LANGUAGE plpgsql;
ALTER FUNCTION takevienphiid_rv()
  OWNER TO postgres;
  
  

  
--HIS: takevienphiid_sttvp();
--duyệt VP/gỡ duyệt status=2

CREATE OR REPLACE FUNCTION takevienphiid_sttvp() RETURNS trigger AS
$BODY$
DECLARE
	query text;
    id integer;
BEGIN
 IF COALESCE(OLD.vienphistatus_vp,0) <> COALESCE(NEW.vienphistatus_vp,0) THEN
	id = NEW.vienphiid;
    query = 'INSERT INTO his_vienphi(vienphiid,status) VALUES (''' || id || ''',2)';
    PERFORM dblink_exec('dbname=O2SMedicalLink port=5432 host=localhost user=takevp password=abc123',query);
 END IF;
 
 RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql;
ALTER FUNCTION takevienphiid_sttvp() OWNER TO postgres;  
  
--HIS: takevienphiid_sttbh();
--duyệt VP/gỡ duyệt status=3
  
CREATE OR REPLACE FUNCTION takevienphiid_sttbh() RETURNS trigger AS
$BODY$
DECLARE
	query text;
    id integer;
BEGIN
 IF OLD.duyet_ngayduyet <> NEW.duyet_ngayduyet THEN
	id = NEW.vienphiid;
    query = 'INSERT INTO his_vienphi(vienphiid,status) VALUES (''' || id || ''',3)';
    PERFORM dblink_exec('dbname=O2SMedicalLink port=5432 host=localhost user=takevp password=abc123',query);
 END IF;
 
 RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql;
ALTER FUNCTION takevienphiid_sttbh() OWNER TO postgres; 
  

  
-----================================================================ TRIGGER - SV_BaoCao
-- SV_BaoCao: on his_vienphi: Lấy Data từ HIS về

CREATE TRIGGER getdatahis
  AFTER INSERT ON his_vienphi
  FOR EACH ROW EXECUTE PROCEDURE getdatahis_func();
ALTER TABLE his_vienphi DISABLE TRIGGER getdatahis;
  

-----================================================================ FUNCTION - SV_BaoCao

--SV_BaoCao: getdatahis_func();

CREATE OR REPLACE FUNCTION getdatahis_func()
  RETURNS trigger AS
$BODY$BEGIN
 IF NEW.status='0' THEN
	--;
	RETURN NEW;
 ELSIF NEW.status='1' THEN
	--;
  RETURN NEW;
 ELSIF NEW.status='2' THEN
	--;
  RETURN NEW;
 ELSIF NEW.status='3' THEN
	--;
  RETURN NEW;  
 END IF;
 
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION getdatahis_func()
  OWNER TO postgres;  
  
  
  
  
  
  
  
  
  
  
  
  
  