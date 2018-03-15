--ucSuaThongTinBenhAn
--ngay 15/3/2018


SELECT vp.vienphiid as vienphiid, 
hsba.patientid as patientid, 
bh.bhytid as bhytid, 
hsba.hosobenhanid as hosobenhanid, 
hsba.patientname as patientname, 
case vp.vienphistatus when 2 then 'Đã duyệt BHYT' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, 
hsba.gioitinhcode as gioitinhcode, 
hsba.gioitinhname as gioitinh, 
vp.vienphidate as thoigianvaovien, 
vp.vienphidate_ravien as thoigianravien, 
vp.duyet_ngayduyet_bh as thoigianduyetbhyt, 
krv.departmentgroupname as khoaravien, 
prv.departmentname as phongravien, 
bh.bhytcode as sothebhyt, 
bh.macskcbbd as noidkkcbbd, 
bh.bhytfromdate as hanthetu, 
bh.bhytutildate as hantheden, 
vp.theghep_bhytcode,
vp.theghep_bhytfromdate,
vp.theghep_bhytutildate,
vp.theghep_macskcbbd,
hsba.noilamviec as noilamviec, 
hsba.birthday as ngaysinh, 
hsba.hc_xacode as hc_xacode, 
hsba.hc_huyencode as hc_huyencode, 
hsba.hc_tinhcode as hc_tinhcode, 
hsba.hc_xaname as hc_xaname, 
hsba.hc_huyenname as hc_huyenname, 
hsba.hc_tinhname as hc_tinhname, 
hsba.hc_sonha as hc_sonha, 
hc_thon as hc_thon 
FROM vienphi vp 
inner join hosobenhan hsba on vp.hosobenhanid= hsba.hosobenhanid 
inner join (select departmentgroupid,departmentgroupname from departmentgroup) krv on vp.departmentgroupid = krv.departmentgroupid 
left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv on vp.departmentid=prv.departmentid 
inner join bhyt bh on vp.bhytid = bh.bhytid 
WHERE " + timkiemtheo + " ORDER BY vp.vienphiid;








