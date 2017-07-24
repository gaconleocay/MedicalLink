--SQL update table OTHERS
Alter table tools_othertypelist add tools_othertypelistnote text; Alter table tools_otherlist add tools_otherlistnote text;


---
ALTER SEQUENCE tools_othertypelist_tools_othertypelistid_seq RESTART WITH 1;
ALTER SEQUENCE tools_otherlist_tools_otherlistid_seq RESTART WITH 1;

----
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (1, 'DSMAYXN', 'Danh sách máy Xét nghiệm', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (2, 'REPORT_23_NHOM_DV', 'REPORT_23_Danh sách nhóm dịch vụ', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (3, 'REPORT_08_LOAIBC', 'REPORT_08_Danh sách loại báo cáo', 0, 'Thiết lập departmentID để lấy dữ liệu theo từng loại báo cáo');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (4, 'REPORT_09_NHOMDV', 'REPORT_09_Danh sách nhóm dịch vụ', 0, 'Danh sách nhóm dịch vụ lấy báo cáo (cách nhau bởi dấu , )');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (5, 'REPORT_12_NSDD', 'REPORT_12_Nhóm thủ thuật nội soi dạ dày', 0, 'Mã nhóm trên danh mục');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (6, 'REPORT_17_DV', 'REPORT_17_Danh sách dịch vụ', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (7, 'REPORT_19_TNT', 'REPORT_19_ID khoa Thận nhân tạo', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (8, 'REPORT_20_DV', 'REPORT_20_Danh sách dịch vụ', 0, '');
INSERT INTO tools_othertypelist(tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote) VALUES (9, 'DS_SOXETNGHIEM', 'Danh sách Sổ xét nghiệm', 0, '');


