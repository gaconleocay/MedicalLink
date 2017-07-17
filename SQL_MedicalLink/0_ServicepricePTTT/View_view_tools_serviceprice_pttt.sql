-- View: view_tools_serviceprice_pttt

-- DROP VIEW view_tools_serviceprice_pttt;

CREATE OR REPLACE VIEW view_tools_serviceprice_pttt AS 
 SELECT vp.vienphiid,
    vp.patientid,
    vp.bhytid,
    vp.hosobenhanid,
    vp.loaivienphiid,
    vp.vienphistatus,
    vp.departmentgroupid AS khoaravien,
    vp.departmentid AS phongravien,
    vp.doituongbenhnhanid,
    vp.vienphidate,
    vp.vienphidate_ravien,
    vp.duyet_ngayduyet,
    vp.vienphistatus_vp,
    vp.duyet_ngayduyet_vp,
    vp.vienphistatus_bh,
    vp.duyet_ngayduyet_bh,
    vp.bhyt_tuyenbenhvien,
    ser.departmentid,
    ser.departmentgroupid,
        CASE
            WHEN ser.departmentid = ANY (ARRAY[34, 335, 269, 285]) THEN ( SELECT mrd.backdepartmentid
               FROM medicalrecord mrd
              WHERE mrd.medicalrecordid = ser.medicalrecordid)
            ELSE ser.departmentgroupid
        END AS departmentgroup_huong,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '01KB'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN
            CASE
                WHEN vp.loaivienphiid = 0 AND (ser.lankhambenh = 0 OR ser.lankhambenh IS NULL) THEN ser.servicepricemoney_bhyt * ser.soluong
                WHEN vp.loaivienphiid = 1 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision
            END
            ELSE 0::double precision
        END) AS money_khambenh_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '01KB'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '01KB'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '01KB'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_khambenh_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '03XN'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_xetnghiem_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '03XN'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '03XN'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '03XN'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_xetnghiem_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '04CDHA'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_cdha_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '04CDHA'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '04CDHA'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '04CDHA'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_cdha_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '05TDCN'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_tdcn_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '05TDCN'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '05TDCN'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '05TDCN'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_tdcn_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '06PTTT'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_pttt_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '06PTTT'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '06PTTT'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '06PTTT'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_pttt_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '08MA'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_mau_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '08MA'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
                END
            END
            WHEN ser.bhyt_groupcode = '08MA'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                    ELSE 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                    ELSE 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                END
            END
            WHEN ser.bhyt_groupcode = '08MA'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney * ser.soluong
                END
            END
            ELSE 0::double precision
        END) AS money_mau_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) AND ((( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = ANY (ARRAY['G303TH'::text, 'G350'::text, 'G303'::text])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_giuongthuong_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) AND ((( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = ANY (ARRAY['G303TH'::text, 'G350'::text, 'G303'::text])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) AND ((( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = ANY (ARRAY['G303TH'::text, 'G350'::text, 'G303'::text])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND ser.loaidoituong = 3 AND ((( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = ANY (ARRAY['G303TH'::text, 'G350'::text, 'G303'::text])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_giuongthuong_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) AND (( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'G303YC'::text THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_giuongyeucau_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) AND (( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'G303YC'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) AND (( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'G303YC'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND ser.loaidoituong = 3 AND (( SELECT serf.servicepricegroupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'G303YC'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_giuongyeucau_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'NS'::text THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_nuocsoi_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'NS'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'NS'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND ser.loaidoituong = 3 AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'NS'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_nuocsoi_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'XA'::text THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_xuatan_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'XA'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'XA'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND ser.loaidoituong = 3 AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'XA'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_xuatan_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'VSDN'::text THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_diennuoc_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'VSDN'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'VSDN'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND ser.loaidoituong = 3 AND (( SELECT serf.report_groupcode
               FROM servicepriceref serf
              WHERE serf.servicepricecode = ser.servicepricecode AND serf.bhyt_groupcode = '12NG'::text)) = 'VSDN'::text THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_diennuoc_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '11VC'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_vanchuyen_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '11VC'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '11VC'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '11VC'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_vanchuyen_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '999DVKHAC'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_khac_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '999DVKHAC'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '999DVKHAC'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '999DVKHAC'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_khac_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '1000PhuThu'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_phuthu_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '1000PhuThu'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '1000PhuThu'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '1000PhuThu'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_phuthu_vp,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_thuoc_bh,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
                END
            END
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                    ELSE 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                    ELSE 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                END
            END
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney * ser.soluong
                END
            END
            ELSE 0::double precision
        END) AS money_thuoc_vp,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_vattu_bh,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
                END
            END
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
            END
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney * ser.soluong
                END
            END
            ELSE 0::double precision
        END) AS money_vattu_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '101VTtrongDMTT'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_vtthaythe_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '101VTtrongDMTT'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
                END
            END
            WHEN ser.bhyt_groupcode = '101VTtrongDMTT'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
            END
            WHEN ser.bhyt_groupcode = '101VTtrongDMTT'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney_nuocngoai * ser.soluong
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney * ser.soluong
                    ELSE 0::double precision - ser.servicepricemoney * ser.soluong
                END
            END
            ELSE 0::double precision
        END) + sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['101VTtrongDMTT'::text, '103VTtyle'::text])) AND ser.loaidoituong = 2 AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 0 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
            END
            ELSE 0::double precision
        END) AS money_vtthaythe_vp,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '07KTC'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_dvktc_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '07KTC'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '07KTC'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                    ELSE 0::double precision
                END
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                    ELSE 0::double precision
                END
            END
            WHEN ser.bhyt_groupcode = '07KTC'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) + sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['101VTtrongDMTT'::text, '103VTtyle'::text])) AND ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND ser_ktc.bhyt_groupcode = '07KTC'::text)) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
            END
            ELSE 0::double precision
        END) AS money_dvktc_vp,
    sum((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt + COALESCE(
        CASE
            WHEN ser.mayytedbid <> 0 THEN ( SELECT myt.chiphiliendoanh
               FROM mayyte myt
              WHERE myt.mayytedbid = ser.mayytedbid)
            ELSE 0::double precision
        END, 0::double precision)) * ser.soluong) AS money_chiphikhac,
    sum(
        CASE
            WHEN ser.loaidoituong = 5 THEN ser.servicepricemoney * ser.soluong
            ELSE 0::double precision
        END) AS money_hpngaygiuong,
    sum(
        CASE
            WHEN ser.loaidoituong = 7 AND ser.servicepriceid_master = 0 THEN ser.servicepricemoney * ser.soluong
            ELSE 0::double precision
        END) AS money_hppttt,
    sum(
        CASE
            WHEN ser.departmentid = ANY (ARRAY[34, 335, 269, 285]) THEN
            CASE
                WHEN ser.loaidoituong = 2 AND (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text])) AND ser.servicepriceid_master <> 0 THEN
                CASE
                    WHEN (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.vienphiid = vp.vienphiid AND (ser_ktc.bhyt_groupcode = ANY (ARRAY['07KTC'::text, '06PTTT'::text])))) AND (( SELECT seref.tinhtoanlaigiadvktc
                       FROM servicepriceref seref
                      WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                               FROM serviceprice ser_ktc
                              WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 0 THEN ser.servicepricemoney * ser.soluong
                    ELSE 0::double precision
                END
                ELSE 0::double precision
            END
            ELSE 0::double precision
        END) AS money_hpdkpttt_gm_thuoc,
    sum(
        CASE
            WHEN ser.departmentid = ANY (ARRAY[34, 335, 269, 285]) THEN
            CASE
                WHEN ser.loaidoituong = 2 AND (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text])) AND ser.servicepriceid_master <> 0 THEN
                CASE
                    WHEN (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.vienphiid = vp.vienphiid AND (ser_ktc.bhyt_groupcode = ANY (ARRAY['07KTC'::text, '06PTTT'::text])))) AND (( SELECT seref.tinhtoanlaigiadvktc
                       FROM servicepriceref seref
                      WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                               FROM serviceprice ser_ktc
                              WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 0 THEN ser.servicepricemoney * ser.soluong
                    ELSE 0::double precision
                END
                ELSE 0::double precision
            END
            ELSE 0::double precision
        END) AS money_hpdkpttt_gm_vattu,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND (ser_ktc.loaidoituong = ANY (ARRAY[0, 4, 6])))) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_dkpttt_thuoc_bh,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND (ser_ktc.loaidoituong = ANY (ARRAY[1, 3])))) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
            END
            ELSE 0::double precision
        END) AS money_dkpttt_thuoc_vp,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['101VTtrongDMTT'::text, '103VTtyle'::text, '10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text])) AND ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND ser_ktc.bhyt_groupcode = '07KTC'::text AND (ser_ktc.loaidoituong = ANY (ARRAY[0, 4, 6])))) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_dkpttt_vattu_bh,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text])) AND ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND ser_ktc.bhyt_groupcode = '07KTC'::text)) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney_nuocngoai > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
                ELSE
                CASE
                    WHEN ser.maubenhphamphieutype = 0 THEN
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                    ELSE
                    CASE
                        WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN 0::double precision - (ser.servicepricemoney - ser.servicepricemoney_bhyt) * ser.soluong
                        ELSE 0::double precision
                    END
                END
            END
            ELSE 0::double precision
        END) + sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['101VTtrongDMTT'::text, '103VTtyle'::text, '10VT'::text, '101VTtrongDM'::text, '102VTngoaiDM'::text])) AND ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND ser_ktc.bhyt_groupcode = '07KTC'::text AND (ser_ktc.loaidoituong = ANY (ARRAY[1, 3])))) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_dkpttt_vattu_vp,
    sum(
        CASE
            WHEN ser.servicepriceid_master <> 0 AND (ser.loaidoituong = ANY (ARRAY[5, 7, 9])) AND (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
            END
            ELSE 0::double precision
        END) + sum(
        CASE
            WHEN ser.servicepriceid_master <> 0 AND ser.loaidoituong = 2 AND (ser.bhyt_groupcode = ANY (ARRAY['09TDT'::text, '091TDTtrongDM'::text, '093TDTUngthu'::text, '092TDTngoaiDM'::text, '094TDTTyle'::text])) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 0 THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_hppttt_goi_thuoc,
    sum(
        CASE
            WHEN ser.servicepriceid_master <> 0 AND (ser.loaidoituong = ANY (ARRAY[5, 7, 9])) AND (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '101VTtrongDMTT'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
            END
            ELSE 0::double precision
        END) + sum(
        CASE
            WHEN ser.servicepriceid_master <> 0 AND ser.loaidoituong = 2 AND (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '101VTtrongDMTT'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 0 THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_hppttt_goi_vattu
   FROM vienphi vp
     LEFT JOIN serviceprice ser ON vp.vienphiid = ser.vienphiid
  WHERE vp.vienphidate >= '2016-01-01 00:00:00'::timestamp without time zone AND ser.thuockhobanle = 0 AND vp.vienphistatus = 0
  GROUP BY vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, vp.vienphistatus_bh, vp.duyet_ngayduyet_bh, vp.bhyt_tuyenbenhvien, ser.departmentid, ser.departmentgroupid,
        CASE
            WHEN ser.departmentid = ANY (ARRAY[34, 335, 269, 285]) THEN ( SELECT mrd.backdepartmentid
               FROM medicalrecord mrd
              WHERE mrd.medicalrecordid = ser.medicalrecordid)
            ELSE ser.departmentgroupid
        END;

ALTER TABLE view_tools_serviceprice_pttt
  OWNER TO postgres;
