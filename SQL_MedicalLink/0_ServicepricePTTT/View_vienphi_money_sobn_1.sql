-- View: vienphi_money_sobn

-- DROP VIEW vienphi_money_sobn;

CREATE OR REPLACE VIEW vienphi_money_sobn AS 
 SELECT vp.vienphiid,
    vp.patientid,
    vp.bhytid,
    vp.hosobenhanid,
    vp.loaivienphiid,
    vp.vienphistatus,
    vp.departmentgroupid,
    vp.departmentid,
    vp.doituongbenhnhanid,
    vp.vienphidate,
    vp.vienphidate_ravien,
    vp.duyet_ngayduyet,
    vp.vienphistatus_vp,
    vp.duyet_ngayduyet_vp,
    vp.vienphistatus_bh,
    vp.duyet_ngayduyet_bh,
    vp.bhyt_tuyenbenhvien,
    vp.chandoanravien_code,
    vp.duyet_sothutuduyet_vp,
        CASE
            WHEN vp.loaivienphiid = 0 THEN ( SELECT medicalrecord.departmentgroupid
               FROM medicalrecord
              WHERE medicalrecord.hosobenhanid = vp.hosobenhanid AND medicalrecord.loaibenhanid = 1
              ORDER BY medicalrecord.medicalrecordid
             LIMIT 1)
            ELSE vp.departmentgroupid
        END AS khoanoitrudautien,
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
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
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
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) AS money_giuong_bh,
    sum(
        CASE
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney_nhandan * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '12NG'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_giuong_vp,
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
            WHEN ser.bhyt_groupcode = '07KTC'::text AND (ser.loaidoituong = ANY (ARRAY[0, 2, 4, 6])) THEN ser.servicepricemoney_bhyt * ser.soluong
            ELSE 0::double precision
        END) + sum(
        CASE
            WHEN ser.loaidoituong = 2 AND (ser.servicepriceid_master IN ( SELECT ser_ktc.servicepriceid
               FROM serviceprice ser_ktc
              WHERE ser_ktc.vienphiid = vp.vienphiid AND ser_ktc.bhyt_groupcode = '07KTC'::text)) AND (( SELECT seref.tinhtoanlaigiadvktc
               FROM servicepriceref seref
              WHERE seref.servicepricecode = (( SELECT ser_ktc.servicepricecode
                       FROM serviceprice ser_ktc
                      WHERE ser_ktc.servicepriceid = ser.servicepriceid_master)))) = 1 THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_nhandan * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_nhandan * ser.soluong
            END
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
                WHEN ser.doituongbenhnhanid = 4 THEN (ser.servicepricemoney_nuocngoai - ser.servicepricemoney_bhyt) * ser.soluong
                ELSE
                CASE
                    WHEN ser.servicepricemoney > ser.servicepricemoney_bhyt THEN ser.servicepricemoney - ser.servicepricemoney_bhyt
                    ELSE 0::double precision
                END * ser.soluong
            END
            WHEN ser.bhyt_groupcode = '07KTC'::text AND ser.loaidoituong = 3 THEN
            CASE
                WHEN ser.doituongbenhnhanid = 4 THEN ser.servicepricemoney_nuocngoai * ser.soluong
                ELSE ser.servicepricemoney * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_dvktc_vp,
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
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '101VTtrongDMTT'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[0, 4, 6])) THEN
            CASE
                WHEN ser.maubenhphamphieutype = 0 THEN ser.servicepricemoney_bhyt * ser.soluong
                ELSE 0::double precision - ser.servicepricemoney_bhyt * ser.soluong
            END
            ELSE 0::double precision
        END) AS money_vattu_bh,
    sum(
        CASE
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '101VTtrongDMTT'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[1, 8])) THEN
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
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '101VTtrongDMTT'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND (ser.loaidoituong = ANY (ARRAY[4, 6])) THEN
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
            WHEN (ser.bhyt_groupcode = ANY (ARRAY['10VT'::text, '101VTtrongDM'::text, '101VTtrongDMTT'::text, '102VTngoaiDM'::text, '103VTtyle'::text])) AND ser.loaidoituong = 3 THEN
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
            WHEN (ser.loaidoituong = ANY (ARRAY[5, 7])) AND ser.servicepriceid_master = 0 THEN ser.servicepricemoney * ser.soluong
            ELSE 0::double precision
        END) AS money_haophi
   FROM ( SELECT vienphi.vienphiid,
            vienphi.patientid,
            vienphi.bhytid,
            vienphi.hosobenhanid,
            vienphi.loaivienphiid,
            vienphi.vienphistatus,
            vienphi.departmentgroupid,
            vienphi.departmentid,
            vienphi.doituongbenhnhanid,
            vienphi.vienphidate,
            vienphi.vienphidate_ravien,
            vienphi.duyet_ngayduyet,
            vienphi.vienphistatus_vp,
            vienphi.duyet_ngayduyet_vp,
            vienphi.vienphistatus_bh,
            vienphi.duyet_ngayduyet_bh,
            vienphi.bhyt_tuyenbenhvien,
            vienphi.chandoanravien_code,
            vienphi.duyet_sothutuduyet_vp
           FROM vienphi
          WHERE vienphi.vienphidate > '2016-01-01 00:00:00'::timestamp without time zone) vp
     LEFT JOIN ( SELECT serviceprice.vienphiid,
            serviceprice.bhyt_groupcode,
            serviceprice.loaidoituong,
            serviceprice.maubenhphamphieutype,
            serviceprice.doituongbenhnhanid,
            serviceprice.soluong,
            serviceprice.servicepricemoney,
            serviceprice.servicepricemoney_bhyt,
            serviceprice.servicepricemoney_nhandan,
            serviceprice.servicepricemoney_nuocngoai,
            serviceprice.lankhambenh,
            serviceprice.servicepriceid_master
           FROM serviceprice
          WHERE serviceprice.thuockhobanle = 0) ser ON vp.vienphiid = ser.vienphiid
  GROUP BY vp.vienphiid, vp.patientid, vp.bhytid, vp.hosobenhanid, vp.loaivienphiid, vp.vienphistatus, vp.departmentgroupid, vp.departmentid, vp.doituongbenhnhanid, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet, vp.vienphistatus_vp, vp.duyet_ngayduyet_vp, vp.vienphistatus_bh, vp.duyet_ngayduyet_bh, vp.bhyt_tuyenbenhvien, vp.chandoanravien_code, vp.duyet_sothutuduyet_vp
  ORDER BY vp.vienphiid DESC;

ALTER TABLE vienphi_money_sobn
  OWNER TO postgres;