-- them danh muc ket qua
INSERT INTO Service_Ref ( ServiceGroupCode, ServiceCode, ServiceName, ServiceNameBHYT, ServiceType, ServiceGroupType, ServiceFee, ServiceFeeBHYT, ServicePrintOrder, HisServiceCode, ServiceMin, ServiceMax, ServiceValueRange, ServiceUnit, ServiceFormula, SnippetCodeDisplay, SnippetCodeInsert)  VALUES( 'G305', 'U4362-3007', 'dv themgfgfgfg', 'bhyt', '0', '4', '33', '11', '0', '', '', '', '', '', '', '', '')

INSERT INTO ServicePriceRef ( ServicePriceRefID_Master, ServicePriceGroupCode, ServicePriceCode, ServicePriceCodeUser, ServicePriceSTTUser, ServicePriceCode_NG, BHYT_GroupCode, Report_GroupCode, CK_GroupCode, Report_TKCode, ServicePriceName, ServicePriceNameNhanDan, ServicePriceNameBHYT, ServicePriceNameNuocNgoai, ServicePriceFee, ServicePriceFeeNhanDan, ServicePriceFeeBHYT, ServicePriceFeeNuocNgoai, ListDepartmentPhongThucHien, ListDepartmentPhongThucHienKhamGoi, ServicePriceFee_OLD_DATE, ServicePriceFee_OLD, ServicePriceFeeNhanDan_OLD, ServicePriceFeeBHYT_OLD, ServicePriceFeeNuocNgoai_OLD, ServicePriceFee_OLD_Type, KhongChuyenDoiTuongHaoPhi, LuonChuyenDoiTuongHaoPhi, CDHA_SoLuongThuoc, CDHA_SoLuongVatTu, PTTT_DinhMucVTTH, PTTT_DinhMucThuoc, TyLeLaiChiDinh, TyLeLaiThucHien, TinhToanLaiGiaDVKTC, LastUserUpdated, LastTimeUpdated, ServicePriceUnit, LayMauPhongThucHien, ServicePriceType, ServiceGroupType, ServicePricePrintOrder, ServiceLock, ServicePriceBHYTQuyDoi, ServicePriceBHYTQuyDoi_TT, ServicePriceBHYTDinhMuc, PTTT_HangID)  VALUES( '0', 'G304', 'U11961-3132', 'ma dmbyt', '', '', '06PTTT', 'XA', '', 'DVK', 'ten dich vụ', 'ten dich vụ', 'ten BHYT', 'ten pttt', '3333', '2222', '1111', '4444', '', '', '2016-03-07 00:00:00', '', '', '', '', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1246', '2016-03-13 17:33:02', 'dvt', '0', '0', '4', '0', '0', '', '', '', '0')  
INSERT INTO ServiceRef4Price ( ServicePriceCode, ServiceCode)  VALUES( 'U11961-3132', 'U4362-3007')



UPDATE ServicePriceRef   SET sync_flag = '0',update_flag = '0',  ServicePriceRefID_Master = '0', ServicePriceGroupCode = 'G304', ServicePriceCode = 'U11961-3132', ServicePriceCodeUser = 'ma dmbyt', ServicePriceSTTUser = '', ServicePriceCode_NG = '', BHYT_GroupCode = '06PTTT', Report_GroupCode = 'XA', CK_GroupCode = '', Report_TKCode = 'DVK', ServicePriceName = 'ten dich vụ', ServicePriceNameNhanDan = 'ten dich vụ', ServicePriceNameBHYT = 'ten BHYT', ServicePriceNameNuocNgoai = 'ten pttt', ServicePriceFee = '3333', ServicePriceFeeNhanDan = '2222', ServicePriceFeeBHYT = '1111', ServicePriceFeeNuocNgoai = '4444', ListDepartmentPhongThucHien = '', ListDepartmentPhongThucHienKhamGoi = '', ServicePriceFee_OLD_DATE = '2016-03-01 00:00:00', ServicePriceFee_OLD = '', ServicePriceFeeNhanDan_OLD = '', ServicePriceFeeBHYT_OLD = '', ServicePriceFeeNuocNgoai_OLD = '', ServicePriceFee_OLD_Type = '1', KhongChuyenDoiTuongHaoPhi = '0', LuonChuyenDoiTuongHaoPhi = '0', CDHA_SoLuongThuoc = '0', CDHA_SoLuongVatTu = '0', PTTT_DinhMucVTTH = '0', PTTT_DinhMucThuoc = '0', TyLeLaiChiDinh = '0', TyLeLaiThucHien = '0', TinhToanLaiGiaDVKTC = '0', LastUserUpdated = '1246', LastTimeUpdated = '2016-03-13 17:34:09', ServicePriceUnit = 'dvt', ServicePriceType = '0', LayMauPhongThucHien = '0', ServiceGroupType = '4', ServicePricePrintOrder = '0', ServiceLock = '0', ServicePriceBHYTQuyDoi = '', ServicePriceBHYTQuyDoi_TT = '', ServicePriceBHYTDinhMuc = '', PTTT_HangID = '0'   WHERE ServicePriceRefID = 12603

INSERT INTO ServiceRef4Price ( ServicePriceCode, ServiceCode)  VALUES( 'U11961-3132', 'G305')
==================
CREATE TABLE servicepriceref
(
  servicepricerefid serial NOT NULL,
  servicepricegroupcode text,
  servicepricetype integer,
  servicegrouptype integer,
  servicepricecode text,
  bhyt_groupcode text,
  report_groupcode text,
  report_tkcode text,
  servicepricename text,
  servicepricenamenhandan text,
  servicepricenamebhyt text,
  servicepricenamenuocngoai text,
  servicepricebhytquydoi text,
  servicepricebhytquydoi_tt text,
  servicepriceunit text,
  servicepricefee text,
  servicepricefeenhandan text,
  servicepricefeebhyt text,
  servicepricefeenuocngoai text,
  listdepartmentphongthuchien text,
  servicepricefee_old_date timestamp without time zone,
  servicepricefee_old text,
  servicepricefeenhandan_old text,
  servicepricefeebhyt_old text,
  servicepricefeenuocngoai_old text,
  servicepriceprintorder integer DEFAULT 0,
  servicepricerefid_master integer DEFAULT 0,
  servicelock integer DEFAULT 0,
  pttt_hangid integer DEFAULT 0,
  laymauphongthuchien integer DEFAULT 0,
  khongchuyendoituonghaophi integer DEFAULT 0,
  cdha_soluongthuoc double precision DEFAULT 0,
  cdha_soluongvattu double precision DEFAULT 0,
  tylelaichidinh double precision DEFAULT 0,
  tylelaithuchien double precision DEFAULT 0,
  version timestamp without time zone,
  luonchuyendoituonghaophi integer DEFAULT 0,
  tinhtoanlaigiadvktc integer DEFAULT 0,
  servicepricecodeuser text,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  servicepricebhytdinhmuc text,
  listdepartmentphongthuchienkhamgoi text,
  ck_groupcode text,
  servicepricecode_ng text,
  pttt_dinhmucvtth double precision DEFAULT 0,
  pttt_dinhmucthuoc double precision DEFAULT 0,
  isremove integer,
  servicepricefee_old_type integer DEFAULT 0,
  servicepricesttuser text,
  tools_tgapdung_bak_1 timestamp without time zone,
  tools_gia_bak_1 text,
  tools_gianhandan_bak_1 text,
  tools_giabhyt_bak_1 text,
  tools_gianuocngoai_bak_1 text,
  tools_kieuapdung_bak_1 integer DEFAULT 0,
  CONSTRAINT servicepriceref_pkey PRIMARY KEY (servicepricerefid)
)
==========
" + gridViewDichVu.GetRowCellValue(i, "xxxxx") + "
MA_DV	MA_NHOM	MA_DV_USER	MA_DV_STTTHAU	TEN_VP	TEN_BH	DVT	GIA_BH	GIA_VP	GIA_YC	GIA_NNN	THOIGIAN_APDUNG	THEO_NGAY_CHI_DINH	LOAI_PTTT	KHOA	NHOM_BHYT	MA_CLS	TEN_CLS	LA_NHOM
CASE servicepriceref.servicegrouptype WHEN 1 THEN 'KHÁM BỆNH' WHEN 2 THEN 'XÉT NGHIỆM' WHEN 3 THEN 'CHẨN ĐOÁN HÌNH ẢNH' WHEN 4 THEN 'CHUYÊN KHOA'  WHEN 5 THEN 'THUỐC TRONG DANH MỤC' WHEN 6 THEN 'THUỐC NGOÀI DANH MỤC' ELSE 'KHÁC' END AS LOAI_DV,
servicepriceref.servicepricecode AS MA_DV, 
servicepriceref.servicepricegroupcode AS MA_NHOM, 
servicepriceref.servicepricecodeuser AS MA_DV_USER, 
servicepriceref.servicepricesttuser AS MA_DV_STTTHAU, 
servicepriceref.servicepricenamenhandan AS TEN_VP, 
servicepriceref.servicepricenamebhyt AS TEN_BH, 
servicepriceref.servicepriceunit AS DVT, 
servicepriceref.servicepricefeebhyt AS GIA_BH, 
servicepriceref.servicepricefeenhandan AS GIA_VP, 
servicepriceref.servicepricefee AS GIA_YC, 
servicepriceref.servicepricefeenuocngoai AS GIA_NNN, 
servicepriceref.servicepricefee_old_date AS THOIGIAN_APDUNG, 
servicepriceref.servicepricefee_old_type AS THEO_NGAY_CHI_DINH, 
servicepriceref.pttt_hangid AS LOAI_PTTT, 
servicepriceref.servicelock AS KHOA, 
servicepriceref.bhyt_groupcode AS NHOM_BHYT
servicepriceref.ServicePriceType AS LA_NHOM
" + gridViewDichVu.GetRowCellValue(i, "xxxxx") + "
==============
INSERT INTO ServicePriceRef ( ServicePriceRefID_Master, ServicePriceGroupCode, ServicePriceCode, ServicePriceCodeUser, ServicePriceSTTUser, ServicePriceCode_NG, BHYT_GroupCode, Report_GroupCode, CK_GroupCode, Report_TKCode, ServicePriceName, ServicePriceNameNhanDan, ServicePriceNameBHYT, ServicePriceNameNuocNgoai, ServicePriceFee, ServicePriceFeeNhanDan, ServicePriceFeeBHYT, ServicePriceFeeNuocNgoai, ListDepartmentPhongThucHien, ListDepartmentPhongThucHienKhamGoi, ServicePriceFee_OLD_DATE, ServicePriceFee_OLD, ServicePriceFeeNhanDan_OLD, ServicePriceFeeBHYT_OLD, ServicePriceFeeNuocNgoai_OLD, ServicePriceFee_OLD_Type, KhongChuyenDoiTuongHaoPhi, LuonChuyenDoiTuongHaoPhi, CDHA_SoLuongThuoc, CDHA_SoLuongVatTu, PTTT_DinhMucVTTH, PTTT_DinhMucThuoc, TyLeLaiChiDinh, TyLeLaiThucHien, TinhToanLaiGiaDVKTC, LastUserUpdated, LastTimeUpdated, ServicePriceUnit, LayMauPhongThucHien, ServicePriceType, ServiceGroupType, ServicePricePrintOrder, ServiceLock, ServicePriceBHYTQuyDoi, ServicePriceBHYTQuyDoi_TT, ServicePriceBHYTDinhMuc, PTTT_HangID)  VALUES( '0', '" + gridViewDichVu.GetRowCellValue(i, "MA_NHOM") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_DV") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_DV_USER") + "', '" + gridViewDichVu.GetRowCellValue(i, "MA_DV_STTTHAU") + "', '', '" + gridViewDichVu.GetRowCellValue(i, "NHOM_BHYT") + "', '', '', '', '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "servicepricenamebhyt") + "', '" + gridViewDichVu.GetRowCellValue(i, "TEN_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_YC") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_VP") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_BH") + "', '" + gridViewDichVu.GetRowCellValue(i, "GIA_NNN") + "', '', '', '" + gridViewDichVu.GetRowCellValue(i, "THOIGIAN_APDUNG") + "', '', '', '', '', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0001-01-01 00:00:00', '" + gridViewDichVu.GetRowCellValue(i, "DVT") + "', '0', '" + gridViewDichVu.GetRowCellValue(i, "LA_NHOM") + "', '" + gridViewDichVu.GetRowCellValue(i, "LOAI_DV") + "', '0', '0', '', '', '', '" + gridViewDichVu.GetRowCellValue(i, "LOAI_PTTT") + "')  


ServiceGroupType
ServicePriceFee_OLD_DATE

servicepricerefid serial NOT NULL,
  servicepricegroupcode text,
  servicepricetype integer,
  servicegrouptype integer,
  servicepricefee_old_date timestamp without time zone,
  servicepriceprintorder integer DEFAULT 0,
  servicepricerefid_master integer DEFAULT 0,
  servicelock integer DEFAULT 0,
  pttt_hangid integer DEFAULT 0,
  laymauphongthuchien integer DEFAULT 0,
  khongchuyendoituonghaophi integer DEFAULT 0,
  cdha_soluongthuoc double precision DEFAULT 0,
  cdha_soluongvattu double precision DEFAULT 0,
  tylelaichidinh double precision DEFAULT 0,
  tylelaithuchien double precision DEFAULT 0,
  version timestamp without time zone,
  luonchuyendoituonghaophi integer DEFAULT 0,
  tinhtoanlaigiadvktc integer DEFAULT 0,
  lastuserupdated integer,
  lasttimeupdated timestamp without time zone,
  sync_flag integer,
  update_flag integer,
  pttt_dinhmucvtth double precision DEFAULT 0,
  pttt_dinhmucthuoc double precision DEFAULT 0,
  isremove integer,
  servicepricefee_old_type integer DEFAULT 0,
  tools_tgapdung_bak_1 timestamp without time zone,
  tools_kieuapdung_bak_1 integer DEFAULT 0,
  CONSTRAINT servicepriceref_pkey PRIMARY KEY (servicepricerefid)
  
  

