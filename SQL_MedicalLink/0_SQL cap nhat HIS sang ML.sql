--SQL Cập nhật từ DB HIS sang DB MediLink

--------Cập nhật bảng DS nhân viên bệnh viện
 update nhompersonnel nv set departmentgroupname=(select tmp.departmentgroupname from (select degp.departmentgroupname from departmentgroup degp 
 where degp.departmentgroupid=nv.departmentgroupid) tmp limit 1) 
 where nv.departmentgroupid<>0 and nv.departmentgroupname=''
 
 --cập nhật sang MeL
delete from ml_nhanvien;
--lay DS nhanvien ben HIS-Medilink
SELECT dblink_connect('myconn', 'dbname=bvhaiphong port=5432 host=179.84.31.5 user=postgres password=haiphong1234');

INSERT INTO ml_nhanvien(nhanvienid,usercode,username,userpassword,userhisid,usergnhom_name,nhom_bcid,nhom_bcten,departmentgroupid,departmentgroupname) 
SELECT tbluser.*
FROM dblink('myconn','select nhanvienid,usercode,username,userpassword,userhisid,usergnhom_name,nhom_bcid,nhom_bcten,departmentgroupid,departmentgroupname FROM nhompersonnel')
    AS tbluser(nhanvienid integer,usercode text,username text,userpassword text,userhisid integer,usergnhom_name text,nhom_bcid integer,nhom_bcten text,departmentgroupid integer,departmentgroupname text)
 
 
 
 
 
 
 
 
 
 
 
 
 