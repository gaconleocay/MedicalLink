vào postgresql.conf tìm đến và chỉnh các dòng :  
1. log_line_prefix : %t - %a -%u - %h 
2. log_statement : mod 
trạng thái sửa thành enable 
========= 
ASC nếu tăng dần (mặc định)
DESC nếu giảm dần 
-- Ky tu ' o doan dau:  
billaccountrefcode in ('KB''','KBYC'''); 
 
Chú ý khi dùng SQL postgres: Khi chia lấy double thì phải viết sau dấu phẩy. 
VD: 80.0/100.0 
NVLS trong orracle 
COALESCE(A.servicepricecode,B.servicepricecode,C.servicepricecode,D.servicepricecode) 
---- 
==========================maubenhpham============= 
KT là đơn thuốc/VT hay phiếu DV: 
maubenhphamgrouptype in (5,6) 
 
"P006014990" 0: Xét nghiệm 
"P006014997" 1: CĐHA 
"P006014543" 2: Khám bệnh 
"P006014971" 3: Phiếu điều trị 
"P006014974" 4: Chuyên khoa 
"P006015888" 5: Thuốc 
"P006016484" 6: Vật tư 
 
Trạng thái:maubenhphamstatus 
0: chua gui yc 
1: da gui YC 
2: Đã trả kết quả 
4: đang tổng hợp y lệnh 
5: đã linh thuoc 
7: trả thuốc ok 
8: thuốc ở kho ngoại trú chưa duyệt 
9: đã lĩnh thuốc o tu truc 
16: Đã tiếp nhận BP 
isloaidonthuoc=0 don thuoc; =1 nha thuoc 
 
 
 
==========================serviceprice============= 
maubenhphamphieutype=0 : chi dinh  =1 tra thuoc 
 
loaipttt=1: PTTT phụ không thay kíp mổ (50%) ; =2 PTTT phụ có thay kíp mổ (=80%) ; =0: PTT chính : HIS đã tính giá * với tỷ lệ rồi. 
 
serviceprice.loaidoituong as loaihinhthanhtoan, ser.loaidoituong in (0,1,3,4,6,8,9) 
-- 0: BHYT 
-- 1: Vien Phi 
-- 2: Di kem 
-- 3: yeu cau 
-- 4: BHYT+YC 
-- 5: Hao phi giuong ,ck 
-- 6: BHYT+phu thu 
-- 7: Hao phi PTTT 
-- 8: Doi tuong khac 
-- 9: Hao phi khac 

	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC '
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		end) as loaidoituong,
-------------- 
CASE servicepriceref.servicegrouptype  
		WHEN 1 THEN 'KHÁM BỆNH'  
		WHEN 2 THEN 'XÉT NGHIỆM'  
		WHEN 3 THEN 'CHẨN ĐOÁN HÌNH ẢNH'  
		WHEN 4 THEN 'CHUYÊN KHOA'   
		WHEN 5 THEN 'THUỐC TRONG DANH MỤC'  
		WHEN 6 THEN 'THUỐC NGOÀI DANH MỤC'  
		ELSE 'KHÁC' END AS LOAI_DV 
------------ 
Mã nhóm BHYT: bhyt_groupcode 
Khám bệnh: 01KB 
Xét nghiệm: 03XN 
CĐHA: 04CDHA 
Thăm dò CN: 05TDCN 
PTTT: 06PTTT 
DV kỹ thuật cao: 07KTC 
Máu: 08MA 
Thuốc: 09TDT 
Thuốc trong DM: 091TDTtrongDM 
Thuốc ngoài DM: 092TDTngoaiDM 
Thuốc ưng thư: 093TDTUngthu 
Thuốc thanh toán theo tỷ lệ: 094TDTTyle ''09TDT'',''091TDTtrongDM'',''092TDTngoaiDM'',''093TDTUngthu'',''094TDTTyle''
Vật tư: 10VT 
Vật tư trong DM: 101VTtrongDM 
Vật tư thay thế: 101VTtrongDMTT 
Vật tư ngoài DM: 102VTngoaiDM 
Vật tư thanh toán theo tỷ lệ: 103VTtyle 
Vận chuyển: 11VC 
Ngày giường: 12NG 
DV khác: 999DVKHAC 
Phụ thu: 1000PhuThu 
 
 
--pttt_loaiid 
Phẫu thuật đặc biệt: 1 
Phẫu thuật loại 1: 2 
Phẫu thuật loại 2: 3 
Phẫu thuật loại 3: 4 
Thủ thuật đặc biệt: 5 
Thủ thuật loại 1: 6 
Thủ thuật loại 2: 7 
Thủ thuật loại 3: 8 
 
--pttt_hangid 
Đặc biệt: 1 
Loại 1A: 2 
Loại 1B: 3 
Loại 1C: 4 
Loại 2A: 5 
Loại 2B: 6 
Loại 2C: 7 
Loại 1: 9 
Loại 2: 10 
Loại 3: 8 
 
ser.thuockhobanle=0 : don thuoc thuong 
 
========================ServicePriceRef================= 
ServiceGroupType: 
- 1: Kham benh 
- 2: Xet nghiem 
- 3: CDHA 
- 4: Chuyen khoa 
- 5: Thuoc 
- 6: Vat tu 
- 11: Khac 
 
 
 servicegrouptype
 
 
 
 
 
 
 
===============================department==================== 
 
departmenttype=2 and loaibenhanid=24 and iskhonghoatdong=0 phòng khám 
departmenttype=3 and loaibenhanid=1 and iskhonghoatdong=0 buồng điều trị 
departmenttype=9 and loaibenhanid=20 and iskhonghoatdong=0 buồng điều trị ngoại trú 
departmenttype=6 phòng xét nghiệm 
departmenttype=7 phòng CĐHA 
departmenttype=8 quản lý thuốc 
departmenttype=10 quản lý vật tư 
departmenttype=24 tủ trực thuốc 
departmenttype=25 tủ trực vật tư 
 
===============================departmentgroup==================== 
 
- departmentgrouptype in (4,11) khoa nội trú + đt ngoại trú 
1: ngoại trú; 4: nội trú; 11: đt ngoại trú; 10: cđha 
 
===============================bill==================== 
 
- loaiphieuthuid=2 and dahuyphieu=0 tạm ứng 
 
===============================medicalrecord==================== 
 
medicalrecord :  
- loaibenhanid = 24 ngoại trú; loaibenhanid=1 nội trú; 
- sothutuphongkhamid = sothutuphongkham.sothutuid ???;  
- doituongbenhnhanid=1 có thẻ bhyt 
- xutrikhambenhid=4 nhập viện; =7 
- hinhthucravienid: 8=chuyển khoa; 1=ra viện; 2=xin về; 3=bỏ về; 4=đưa về; 5=chuyển viện; 
- hinhthucvaovienid: 3=chuyển khoa 
 
medicalrecordstatus: 
0: chưa tiếp nhận 
2: đã tiếp nhận 
3: gửi mổ 
5,6: 
99: đã kết thúc 
 
===============================vienphi==================== 
 
- doituongbenhnhanid=1: co the bhyt;   
		=2: vien phi 
		=3: dich vu 
		=4: nguoi nuoc ngoai 
		=5: mien phi 
		=6: hop dong 
 
=4:nguoi nuoc ngoai ;=0: 
- dagiuthebhyt=1 có thẻ bhyt 
- loaivienphiid=0 nội trú; =1 ngoại trú 
 
 
===============================bhyt==================== 
bhyt_loaiid:  
-1: đúng tuyến 
-2: đúng tuyến (giấy giới thiệu) 
-3: đúng tuyến (cấp cứu) 
-4: trái tuyến 
 
bhyt_tuyenbenhvien: 
=1: huyen 
=2: tinh 
=3: TW 
 
===============================medicine==================== 
 
medicinestorebilltype 
211 
8 
204: xuất đơn 
1 
213 
4 
200: phiếu yêu cầu từ BN tủ trực 
217: phiếu nhập trả từ BN - nhập trả bs 
102: phiếu yêu cầu nhập bù tủ trực 
6 
98 
212 
2: nhập kho 
214 
3 
97 
218: phiếu yêu cầu trả từ BN  
202 
5: xuất kho 
9 
203 
--- 
partnerid: kho gửi yêu cầu (phiếu yêu cầu) 
 
 
 
 
me.medicinestorebillstatus 
status=2: 2,5,204,217 
status=12: 102,218 
status=11: 200 
status=9: phieu yeu cau: 200 
 
===============================medicine_ref==================== 
 
datatype=0: thuốc; =1:vật tư 
 
 
 
 
 
 
 
 
 
================================================================ 
----=============== 
Đóng gói phần mềm - Đặt tất cả thư viện liên kết động (DLL) vào 1 thư mục 
 
Sau đây mình sẽ hướng dẫn bạn làm việc đó chỉ trong 1 dòng Code. Tại phương thức Main trong file Program.cs bạn thêm dòng code sau :  
AppDomain.CurrentDomain.AppendPrivatePath(AppDomain.CurrentDomain.BaseDirectory + @"\Library"); 
(select date '2001-09-28 00:00:00' - time '00:00:01') 
 
-------- 
Gọi 1 file .exe khác để chạy: 
System.Diagnostics.Process.Start("E:\ChuongtrinhA\ ctA.exe"); 
 
Bạn kiểm tra xem có file đó trong đường dẫn không? 
Nếu có thì phải Hoặc thêm @ vào trước chuỗi đường dẫn vd :@"C:\test.exe" 
hoặc là thêm 1 cái \ nữa vd: "C:\\test.exe" 
 
--------template baos caos: 
List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>(); 
ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO(); 
reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO; 
reportitem.value = "Từ xxxx đến yyyy"; 
thongTinThem.Add(reportitem); 
 
string fileTemplatePath = "BC_PhauThuatThuThuat_01.xlsx"; 
Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport(); 
export.ExportExcelTemplate("", fileTemplatePath,thongTinThem, dataExport); 
 
 
======================== 
--Hầu hết các hệ quản trị CSDL đều cho phép chúng ta nối các xâu lại với nhau bằng cách này hay cách khác. 
Ví dụ: để nối 2 xâu lại với nhau 
· MS SQL Server sử dụng toán tử + 
· Oracle sử dụng toán tử || hoặc hàm concat 
· MySQL sử dụng hàm concat 
 
 
================= 
-- Fuction trong Postgres 
CREATE FUNCTION sum(int[]) RETURNS int8 AS $$ 
DECLARE 
  s int8 := 0; 
  x int; 
BEGIN 
  FOREACH x IN ARRAY $1 
  LOOP 
    s := s + x; 
  END LOOP; 
  RETURN s; 
END; 
$$ LANGUAGE plpgsql; 
-- thực thi 
select sum(tongchiphi) from tb_servicedata where patientrecordid=4  
========================== 
//How to export data from database tables to an XML file using SSIS 
http://www.codeproject.com/Articles/635956/How-to-export-data-from-database-tables-to-an-XML 
SELECT ( 
  SELECT TOP 1 NULL as N, 
    (SELECT Employee.EmployeeId as Id, Employee.FirstName as Name, 
      (SELECT TOP 1 NULL as N, 
        (SELECT Customer.CustomerID as Id, CustomerName as Name 
         FROM EmployeesCustomers INNER JOIN Customers Customer 
         ON EmployeesCustomers.CustomerID = Customer.CustomerID 
         WHERE EmployeesCustomers.EmployeeId = Employee.EmployeeId 
         ORDER BY Customer.CustomerID 
         FOR XML AUTO, TYPE) 
       FROM Customers 
       FOR XML AUTO, TYPE) 
     FROM EmployeesCustomers INNER JOIN Employees Employee 
     ON EmployeesCustomers.EmployeeID = Employee.EmployeeID 
     GROUP BY Employee.EmployeeId, Employee.FirstName 
     ORDER BY Employee.EmployeeId 
     FOR XML AUTO, TYPE) 
  FROM Employees 
  FOR XML AUTO, ROOT('Company') 
) AS COL_XML 
 
================== 
CREATE PROCEDURE prc_readxmldata 
( 
@XMLdata XML 
) 
AS 
BEGIN 
SELECT 
t.value('(FirstName/text())[1]','nvarchar(120)')AS FirstName , 
t.value('(LastName/text())[1]','nvarchar(120)')AS LastName, 
t.value('(UserName/text())[1]','nvarchar(120)')AS UserName, 
t.value('(Job/text())[1]','nvarchar(120)')AS Job 
FROM 
@XMLdata.nodes('/users/user')AS TempTable(t) 
END 
============================ 
UPDATE ServicePriceRef   SET sync_flag = '0',update_flag = '0',        ServicePriceRefID_Master = '0',       ServicePriceGroupCode = 'XNHH',       ServicePriceCode = 'XNHH003',       ServicePriceCodeUser = '2259.1.354',       ServicePriceSTTUser = 'STT tháº§u',       ServicePriceCode_NG = '',       BHYT_GroupCode = '03XN',       Report_GroupCode = 'CT32',       CK_GroupCode = '',       Report_TKCode = 'VATTU',       ServicePriceName = 'Cáº·n Addis-ten dv',       ServicePriceNameNhanDan = 'Cáº·n Addis-ten dv',       ServicePriceNameBHYT = 'Cáº·n Addis-ten bhyt',       ServicePriceNameNuocNgoai = 'Cáº·n Addis-ten pttt',       ServicePriceFee = '300000',       ServicePriceFeeNhanDan = '300000',       ServicePriceFeeBHYT = '300000',       ServicePriceFeeNuocNgoai = '1000000',       ListDepartmentPhongThucHien = '254',       ListDepartmentPhongThucHienKhamGoi = '',       ServicePriceFee_OLD_DATE = '0001-01-01 00:00:00',       ServicePriceFee_OLD = '',       ServicePriceFeeNhanDan_OLD = '',       ServicePriceFeeBHYT_OLD = '',       ServicePriceFeeNuocNgoai_OLD = '',       ServicePriceFee_OLD_Type = '0',       KhongChuyenDoiTuongHaoPhi = '0',       LuonChuyenDoiTuongHaoPhi = '0',       CDHA_SoLuongThuoc = '0',       CDHA_SoLuongVatTu = '0',       PTTT_DinhMucVTTH = '0',       PTTT_DinhMucThuoc = '0',       TyLeLaiChiDinh = '0',       TyLeLaiThucHien = '0',       TinhToanLaiGiaDVKTC = '0',       LastUserUpdated = '1246',       LastTimeUpdated = '2016-07-03 12:24:15',       ServicePriceUnit = 'dvt',       ServicePriceType = '0',       LayMauPhongThucHien = '0',       ServiceGroupType = '2',       ServicePricePrintOrder = '6',       ServiceLock = '0',       ServicePriceBHYTQuyDoi = '',       ServicePriceBHYTQuyDoi_TT = '',       ServicePriceBHYTDinhMuc = '',       PTTT_HangID = '0'   WHERE ServicePriceRefID = 235 
 UPDATE ServicePriceRef   SET sync_flag = '0',update_flag = '0',        LastUserUpdated = '1246',       LastTimeUpdated = '2016-07-03 12:31:01',       ListDepartmentPhongThucHien = '255;248'   WHERE ServicePriceRefID = 235 
========================= 
 
"C:\Program Files\PostgreSQL\9.3\bin\pg_ctl.exe" runservice -N "postgresql-x64-9.3" -D "E:/Program Files/PostgreSQL/9.3/data" -w 
"C:\Program Files\PostgreSQL\9.3\bin\pg_ctl.exe" runservice -N "postgresql-x64-9.3" -D "\\Mac\AllFiles\Volumes\My Passport\Program Files\PostgreSQL\9.3\data" -w 
 
---------- 
 
select pg_backend_pid()	; 
3872 
SELECT * FROM pg_stat_activity; 
DataID; dataname; pid 
12029;"postgres";2568 
135173122;"test_viettiep";3856 
135173122;"test_viettiep";3872 
--- 
SELECT * FROM pg_stat_activity where pid=(select pg_backend_pid()); 
 
135173122 
----------- 
 INSERT INTO table (image) VALUES (pg_escape_bytea(image.jpg)) 
----------- 
Use pg_read_file('location_of file')::bytea. 
 
For example, 
 
create table test(id int, image bytea); 
insert into test values (1, pg_read_file('/home/xyz')::bytea); 
 
 
connection(); 
Stream fs = FileUpload1.PostedFile.InputStream; 
BinaryReader br = new BinaryReader(fs);             //reads the   binary files 
Byte[] bytes = br.ReadBytes((Int32)fs.Length);       //counting the file length into bytes 
query = "insert into wordFiles (Name,type,data)" + " values (@Name, @type, @Data)";   //insert query 
com = new SqlCommand(query, con); 
com.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename1; 
com.Parameters.Add("@type", SqlDbType.VarChar).Value = type; 
com.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes; 
com.ExecuteNonQuery(); 
Label2.ForeColor = System.Drawing.Color.Green; 
Label2.Text = " Word File Uploaded Successfully";  
---- 
stream inputstream = File.OpenRead(@"C;\test\source\folder3.cs "); 
 
stream outputstream = File.Openwrite(@"C:test\source\folder3.bak"); 
 
// tạo một tập tin mới trên thư mục làm việc FileStream myFStream = new FileStream("test.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite); 
 
-------- 
table:  
- version 
- data 
- typeupdate: 0=file exe; 1: file zip giải nén 
 
----- 
yyyy-MM-dd HH:mm:ss 
 
#,##0.00 
#,##0 
{0:#,##0} 
yyyy-MM-dd HH:mm:ss 
 
SELECT TO_CHAR(vienphidate_ravien, 'yyyy-MM-dd HH:mm:ss') from vienphi vp where vp.vienphiid=800543 
 
 
permissioncode in ('KkZTgDoK3hgIm+81AVXkyA==','KkZTgDoK3hhrZwf8J63W4Q==') 
 
 
row_number() over() as stt, 
ROW_NUMBER () OVER (ORDER BY A.NGAY_THUCHIEN desc) as stt 
 
        private void gridViewDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e) 
        { 
            try 
            { 
                GridView view = sender as GridView; 
                if (e.RowHandle == view.FocusedRowHandle) 
                { 
                    e.Appearance.BackColor = Color.LightGreen; 
                    e.Appearance.ForeColor = Color.Black; 
                } 
            } 
            catch (Exception ex) 
            { 
                MedicalLink.Base.Logging.Warn(ex); 
            } 
        } 
------------ 
10.211.55.5 
10.211.55.6 
----------------- 
POSTGRE SQL 
 
WITH regional_sales AS ( 
SELECT region, SUM(amount) AS total_sales FROM orders 
GROUP BY region 
), top_regions AS ( 
SELECT region 
FROM regional_sales 
WHERE total_sales > (SELECT SUM(total_sales)/10 FROM regional_sales) 
) 
SELECT region, 
product, 
SUM(quantity) AS product_units, SUM(amount) AS product_sales 
FROM orders 
WHERE region IN (SELECT region FROM top_regions) GROUP BY region, product; 
--------- 
9.16.3. NULLIF 
NULLIF(value1, value2) 
Hàm NULLIF trả về một giá trị null nếu value1 bằng value2; nếu không thì nó trả về value1. Điều này 
có thể được sử dụng để thực hiện hành động ngược lại của ví dụ COALESCE được đưa ra ở trên: SELECT NULLIF(value, ’(none)’) ... 
Trong ví dụ này, nếu giá trị value là (none), thì null sẽ được trả về, nếu không thì giá trị của value sẽ được trả về. 
 
vp.duyet_ngayduyet_vp >='2017-01-01 00:00:00' and vp.duyet_ngayduyet_vp <='2017-01-05 23:59:59'  
-------- 
(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam 
-------------- 
SELECT pl.medicinephongluuname,  
    STRING_AGG(trim(to_char(pl.medicinephongluuid, '99')), ',' ORDER BY pl.medicinephongluuid) as lstmedicinephongluuid 
from medicinephongluu pl 
where pl.medicinephongluuname <>'' 
group by pl.medicinephongluuname 
 
 kho tbyt tổng.
---------------Chay update may xet nghiem
INSERT INTO tools_otherlist(tools_othertypelistid, tools_otherlistcode, 
            tools_otherlistname, tools_otherlistvalue)
SELECT 1 as tools_othertypelistid, idmayxn as tools_otherlistcode, tenmayxn as tools_otherlistname, idmayxn as tools_otherlistvalue from service where idmayxn is not null group by idmayxn, tenmayxn;







 
