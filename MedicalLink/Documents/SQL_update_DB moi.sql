--SQL update table OTHERS
--Alter table tools_othertypelist add tools_othertypelistnote text; Alter table tools_otherlist add tools_otherlistnote text;
---
DELETE FROM tools_othertypelist;
DELETE FROM tools_otherlist;
------------
ALTER SEQUENCE tools_othertypelist_tools_othertypelistid_seq RESTART WITH 1;
ALTER SEQUENCE tools_otherlist_tools_otherlistid_seq RESTART WITH 1;

---------------tools_othertypelist
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (1, 'DSMAYXN', 'Danh sách máy Xét nghiệm', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (2, 'REPORT_23_NHOM_DV', 'REPORT_23_Danh sách nhóm dịch vụ', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (3, 'REPORT_08_LOAIBC', 'REPORT_08_Danh sách loại báo cáo', 0, 'Thiết lập departmentID để lấy dữ liệu theo từng loại báo cáo');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (4, 'REPORT_09_NHOMDV', 'REPORT_09_Danh sách nhóm dịch vụ', 0, 'Danh sách nhóm dịch vụ lấy báo cáo (cách nhau bởi dấu , )');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (5, 'REPORT_12_NSDD', 'REPORT_12_Nhóm thủ thuật nội soi dạ dày', 0, 'Mã nhóm trên danh mục');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (6, 'REPORT_17_DV', 'REPORT_17_Danh sách dịch vụ', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (7, 'REPORT_19_TNT', 'REPORT_19_ID khoa Thận nhân tạo', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (8, 'REPORT_20_DV', 'REPORT_20_Danh sách dịch vụ', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (9, 'DS_SOXETNGHIEM', 'Danh sách Sổ xét nghiệm', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (10, 'DASHBOARD_12_DSHCSD', 'Nhóm thuốc hạn chế sử dụng', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (11, 'REPORT_26_NHOMDV', 'REPORT_26_Danh sách nhóm dịch vụ', 0, 'Danh sách nhóm dịch vụ lấy báo cáo (cách nhau bởi dấu , )');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (12, 'REPORT_28_NHOMDV', 'REPORT_28_Danh sách nhóm dịch vụ', 0, 'Nhóm Chi phí khác - bv Thanh Hóa');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (13, 'REPORT_29_NHOMDV', 'REPORT_29_Danh sách dịch vụ', 0, 'Danh sách từng dịch vụ');

----
ALTER SEQUENCE tools_othertypelist_tools_othertypelistid_seq RESTART WITH 13;



-------------------tools_otherlist
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (2, 'GIUONG', 'Ngày giường', 'G303TH,G350,G303YC', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (2, 'NUOC_SOI', 'Nước sôi', 'NUOC_SOI', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (2, 'XUAT_AN', 'Xuất ăn', 'XUAT_AN', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (3, 'BAOCAO_001', 'Báo cáo Phẫu thuật - Khoa Gây mê hồi tỉnh (Phòng mổ phiên; Phòng mổ CC)', '285,34', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (3, 'BAOCAO_002', 'Báo cáo Phẫu thuật - Khoa Tai mũi họng (BĐT K.Tai mũi họng)', '122', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (3, 'BAOCAO_003', 'Báo cáo Phẫu thuật - Khoa Răng hàm mặt (BĐT K.Răng hàm mặt)', '116', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (3, 'BAOCAO_004', 'Báo cáo Phẫu thuật - Khoa Mắt (Mổ mắt BĐT; PK mổ mắt; BĐT K.Mắt; Khám mắt)', '269,335,80,212', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (3, 'BAOCAO_006', 'Báo cáo Thủ thuật - Khoa Mắt (BĐT K.Mắt; PK Mắt)', '80,212', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (4, 'REPORT_09_XA', 'Báo cáo bệnh nhân sử dụng nhóm dịch vụ - Xuất ăn', 'G304,XUAT_AN', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (5, 'BAOCAO_009', 'Báo cáo Thủ thuật Nội soi dạ dày', 'NSDDTT37_TT1', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (6, 'REPORT_17_DV', 'Danh sách dịch vụ Phẫu thuật yêu cầu (có thêm dấu nháy '')', '''U11970-3701'', ''U11620-4506'', ''U11621-4524'', ''U11622-4536'', ''U11623-4610''', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (7, 'REPORT_19_TNT', 'ID khoa Thận nhân tạo', '14', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (8, 'REPORT_20_DV', 'Danh sách dịch vụ Phẫu thuật yêu cầu (có thêm dấu nháy '')', '''U18851-4427'',''U19154-1951'',''U18765-1454'',''U17261-2902'',''U17265-3936'',''U30001-5346'',''U30001-1207'',''U30001-2954''', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (9, 'SO_VS', 'Sổ Vi sinh', 'SO_VS', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (9, 'SO_SHTQ', 'Sổ Sinh hóa thường quy', 'SO_SHTQ', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (9, 'SO_NTVD', 'Sổ Nước tiểu và dịch khác', 'SO_NTVD', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (9, 'SO_MD', 'Sổ Miễn dịch', 'SO_MD', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (9, 'SO_KM', 'Sổ Khí máu', 'SO_KM', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (10, 'T41446-1948', 'Nhóm thuốc hạn chế sử dụng', 'T41446-1948', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (11, '38TH10', 'Nhóm dịch vụ ngày giường khoa Quốc tế', '38TH10', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (12, 'U2918-0644', 'Nhóm chi phí khác - BV Thanh Hóa', 'U2918-0644', '0', '');
INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (13, 'UB3025094', 'Xạ trị sử dụng CT mô phỏng', 'UB3025094', '0', '');

--INSERT INTO tools_otherlist(tools_othertypelistid,tools_otherlistcode,tools_otherlistname,tools_otherlistvalue,tools_otherliststatus,tools_otherlistnote) VALUES (2222, '222222', '222222', '222222', '0', '');






















