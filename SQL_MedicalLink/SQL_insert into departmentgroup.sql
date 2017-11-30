SELECT dblink_connect('myconn', 'dbname=test_viettiep port=5432 host=localhost user=postgres password=1234');

CREATE TABLE tools_departmentgroup
(
  departmentgroupid serial NOT NULL,
  departmentgroupcode text,
  departmentgroupname text,
  departmentgrouptype integer,
  loaikhoiid integer,
  loaikhoiten text,
  truongkhoacode text,
  departmentgroupremark text,
  departmentgroupcode_byt text,
  CONSTRAINT tools_departmentgroup_pkey PRIMARY KEY (departmentgroupid)
);


insert into tools_departmentgroup
SELECT degp.departmentgroupid,
	degp.departmentgroupcode,
	degp.departmentgroupname,
	degp.departmentgrouptype,
	'0' as loaikhoiid,
	'' as loaikhoiten,
	degp.truongkhoacode,
	degp.departmentgroupremark,
	degp.departmentgroupcode_byt
FROM dblink('myconn','select departmentgroupid,departmentgroupcode,departmentgroupname,departmentgrouptype,truongkhoacode,departmentgroupremark,departmentgroupcode_byt from departmentgroup')
    AS degp(departmentgroupid integer,
  departmentgroupcode text,
  departmentgroupname text,
  departmentgrouptype integer,
  loaikhoiid integer,
  loaikhoiten text,
  truongkhoacode text,
  departmentgroupremark text,
  departmentgroupcode_byt text);

---------
update tools_departmentgroup set loaikhoiten='I. Khối Nội' where loaikhoiid=1;
update tools_departmentgroup set loaikhoiten='II. Khối Ngoại' where loaikhoiid=2;
update tools_departmentgroup set loaikhoiten='III. Khối chuyên khoa lẻ và phòng khám' where loaikhoiid=3;
update tools_departmentgroup set loaikhoiten='IV. Cận lâm sàng' where loaikhoiid=4;


---------
CREATE TABLE tools_updatenhansu
(
  updatenhansuid serial NOT NULL,
  departmentgroupid integer,
  giuongthucke integer,
  nb_hientai integer,
  ns_bienche integer,
  ns_hopdong integer,
  ns_hocviec integer,
  ns_hienco integer,
  ns_vang integer,
  lydo_om integer,
  lydo_phep integer,
  lydo_de integer,
  lydo_khac integer,
  hvsv_hocvien integer,
  hvsv_sinhvien integer,
  ddktv_soluong integer,
  ddktv_khoadenid integer,
  ddktv_khoadenname text,
  ddktv_songay integer,
  ghichu_khoa text,
  ghichu_dieuduong text, 
  nhansudate timestamp without time zone,
  createdate timestamp without time zone,
  createusercode text,
  createusername text,
  updatedate timestamp without time zone,
  updateusercode text,
  updateusername text,
  CONSTRAINT tools_updatenhansu_pkey PRIMARY KEY (updatenhansuid)
);


CREATE INDEX tools_updatenhansu_departmentgroupid_idx ON tools_updatenhansu USING btree(departmentgroupid);
CREATE INDEX tools_updatenhansu_nhansudate_idx ON tools_updatenhansu USING btree(nhansudate);




----------
SELECT row_number () over (order by degp.loaikhoiid,degp.departmentgroupname) as stt,
	  ns.updatenhansuid,
	  ns.departmentgroupid,
	  ns.giuongthucke,
	  ns.nb_hientai,
	  ns.ns_bienche,
	  ns.ns_hopdong,
	  ns.ns_hocviec,
	  ns.ns_hienco,
	  ns.ns_vang,
	  ns.lydo_om,
	  ns.lydo_phep,
	  ns.lydo_de,
	  ns.lydo_khac,
	  ns.hvsv_hocvien,
	  ns.hvsv_sinhvien,
	  ns.ddktv_soluong,
	  ns.ddktv_khoadenid,
	  ns.ddktv_khoadenname,
	  ns.ddktv_songay,
	  ns.ghichu_khoa,
	  ns.ghichu_dieuduong,
	  ns.nhansudate,
	  degp.loaikhoiid,
	  degp.loaikhoiten,
	  degp.departmentgroupid,
	  degp.departmentgroupname,
	  '0'  as isgroup
FROM tools_updatenhansu ns
	inner join tools_departmentgroup degp on degp.departmentgroupid=ns.departmentgroupid
WHERE to_char(ns.nhansudate,'yyyyMMdd')='"+_thoigian+"' and degp.loaikhoiid>0 "+_dskhoa+";
	

---//thuật toán cập nhật tình hình nhân sự

- Select theo thời gian lựa chọn: 
		+ nếu có bản ghi thì lấy ra; 
		+ nếu không tìm thấy thì tạo bản ghi mới ứng với khoa đã chọn
	- Tạo mới: Lấy dữ liệu của ngày gần nhất hiển thị lên
	

----------------------
select departmentgroupid,
		ns_bienche,
		ns_hopdong,
		ns_hocviec,
		hvsv_hocvien,
		hvsv_sinhvien
from tools_updatenhansu
where departmentgroupid='"++"'
order by nhansudate desc
limit 1;





INSERT INTO tools_updatenhansu(departmentgroupid, giuongthucke, nb_hientai, ns_bienche, ns_hopdong, ns_hocviec, ns_hienco, ns_vang, lydo_om, lydo_phep, lydo_de, lydo_khac, hvsv_hocvien, hvsv_sinhvien, ddktv_soluong, ddktv_khoadenid, ddktv_khoadenname, ddktv_songay, ghichu_khoa, ghichu_dieuduong, nhansudate, createdate, createusercode, createusername) VALUES ();

























