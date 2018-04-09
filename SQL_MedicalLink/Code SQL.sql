﻿vào postgresql.conf tìm đến và chỉnh các dòng :  
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
coalesce(A.servicepricecode,B.servicepricecode,C.servicepricecode,D.servicepricecode) 
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
-- 20: Thanh toán riêng
	(case ser.loaidoituong
		when 0 then 'BHYT'
		when 1 then 'Viện phí'
		when 2 then 'Đi kèm'
		when 3 then 'Yêu cầu'
		when 4 then 'BHYT+YC'
		when 5 then 'Hao phí giường, CK'
		when 6 then 'BHYT+phụ thu'
		when 7 then 'Hao phí PTTT'
		when 8 then 'Đối tượng khác'
		when 9 then 'Hao phí khác'
		when 20 then 'Thanh toán riêng'
		end) as loaidoituong,
		
loaingaygiuong
	=1 then 'Nam ghep 2'
	=2 then 'Nam ghep 3 tro len'
	
(case when ser.loaipttt=2 then 'Có thay kíp mổ'
		when ser.loaipttt=1 then 'Không thay kíp mổ'		
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
Thuốc thanh toán theo tỷ lệ: 094TDTTyle 
--''09TDT'',''091TDTtrongDM'',''092TDTngoaiDM'',''093TDTUngthu'',''094TDTTyle''
Vật tư: 10VT 
Vật tư trong DM: 101VTtrongDM 
Vật tư thay thế: 101VTtrongDMTT 
Vật tư ngoài DM: 102VTngoaiDM 
Vật tư thanh toán theo tỷ lệ: 103VTtyle 

--bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')
Vận chuyển: 11VC 
Ngày giường: 12NG 
DV khác: 999DVKHAC 
Phụ thu: 1000PhuThu 
 
 '01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC'
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
 
BHYT thanh toán định mức trần:
ser.servicepricebhytdinhmuc : định mức trần cho dịch vụ mà nằm trong khoảng thanh toán của BHYT.

 
 
========================ServicePriceRef================= 
ServiceGroupType: 
- 1: Kham benh 
- 2: Xet nghiem 
- 3: CDHA 
- 4: Chuyen khoa 
- 5: Thuoc 
- 6: Vat tu 
- 11: Khac 
 
 
where servicegrouptype in (1,2,3,4,11)
 
 
 
 
 
 
 
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
- loaiphieuthuid=0 : thu tien
- loaiphieuthuid=1 : hoan ung

=====================billgroup==========================
- billgrouptype = 2:thu tien; 0:tong hop ; 1:tam ung
- bullgroupmode = 0:tong hop; 
 
===============================medicalrecord==================== 
 
medicalrecord :  
- loaibenhanid = 24 ngoại trú; loaibenhanid=1 nội trú; 
- sothutuphongkhamid = sothutuphongkham.sothutuid ???;  
- doituongbenhnhanid=1 có thẻ bhyt 
- xutrikhambenhid=4 nhập viện; =7 
- hinhthucravienid: 8=chuyển khoa; 1=ra viện; 2=xin về; 3=bỏ về; 4=đưa về; 5=chuyển viện; 
- hinhthucvaovienid: 3=chuyển khoa ;=2: nhap vien; 0=
 
medicalrecordstatus: 
0: chưa tiếp nhận 
2: đã tiếp nhận 
3: gửi mổ 
5,6: 
99: đã kết thúc 
 
===============================vienphi==================== 
 
(case vp.doituongbenhnhanid 
			when 1 then 'BHYT'
			when 2 then 'Viện phí'
			when 3 then 'Dịch vụ'
			when 4 then 'Người nước ngoài'
			when 5 then 'Miễn phí'
			when 6 then 'Hợp đồng'
			end) as doituongbenhnhanid,
 
=4:nguoi nuoc ngoai ;=0: 
- dagiuthebhyt=1 có thẻ bhyt 
- loaivienphiid=0 nội trú; =1 ngoại trú 
 
 duyet_ngayduyet: duyet BHYT (khi duyet bhyt thi vienphistatus=2 & duyet_ngayduyet)
 duyet_ngayduyet_vp: duyet Vien phi
 (case when vp.vienphistatus=0 then 'Đang điều trị'
		  else (case when vienphistatus_vp=1 then 'Đã thanh toán'
					else 'Chưa thanh toán' end) end) as trangthai
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
8 : don thuoc ban le tra lai
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
 
 
===============================hosobenhan
(case mrd.xutrikhambenhid -- Xutrikhambenh ngoại trú
		when 1 then 'Cấp toa cho về'
		when 2 then 'Điều trị ngoại trú'
		when 4 then 'Nhập viện'
		when 5 then 'Chuyển viện'
		when 6 then 'Tử vong'
		when 7 then 'Trốn viện'
		when 8 then 'Khác'
	end) as xutrikhambenh, 
 
--Nghề nghiệp
01: Trẻ dưới 6 tuổi đi học, dưới 15 tuổi không đi học
02: Sinh viên, học sinh
03: Hưu và trên 60 tuổi
04: Công nhân
05: Nông dân
06: Lực lượng vũ trang
07: Trí thức
08: Hành chính, sự nghiệp
09: Y tế
10: Dịch vụ
12: Ngoại kiều
13: Nhân dân
15: Giáo viên
18: Thương binh
19: Kế toán
99: Loại khác
 
 
 
 
 
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
 
======================== 
--Hầu hết các hệ quản trị CSDL đều cho phép chúng ta nối các xâu lại với nhau bằng cách này hay cách khác. 
Ví dụ: để nối 2 xâu lại với nhau 
· MS SQL Server sử dụng toán tử + 
· Oracle sử dụng toán tử || hoặc hàm concat 
· MySQL sử dụng hàm concat  
==========================  
---------- 
 
select pg_backend_pid()	; 
3872 
SELECT * FROM pg_stat_activity; 
DataID; dataname; pid 
12029;"postgres";2568 
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
HH:mm dd/MM/yyyy

#,##0.00
#,##0
{0:#,##0}
 

 
textEdit trong devexpress để hiển thị dạng  ###,###,###
thì đặt thuộc tính: - EditValie=Null,
- Properties Display = Custom #,##0
- Properties MaskType=Nummeric; Editmask=n0
 
 
 
 
 
 
SELECT TO_CHAR(vienphidate_ravien, 'yyyy-MM-dd HH24:MI:ss') from vienphi vp where vp.vienphiid=800543 
	 TO_CHAR(vienphidate_ravien, 'HH24:MI dd/MM/yyyy')


 
row_number() over() as stt, 
row_number () over (order by a.ngay_thuchien) as stt 
(row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY vms.vienphidate)) as stt,

--------- 
9.16.3. NULLIF 
NULLIF(value1, value2) 
Hàm NULLIF trả về một giá trị null nếu value1 bằng value2; nếu không thì nó trả về value1. Điều này 
có thể được sử dụng để thực hiện hành động ngược lại của ví dụ COALESCE được đưa ra ở trên: SELECT NULLIF(value, ’(none)’) ... 
Trong ví dụ này, nếu giá trị value là (none), thì null sẽ được trả về, nếu không thì giá trị của value sẽ được trả về. 
 -------- 
(case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as year_nam 
-------------- Concat trong postgres
string_agg(case when b.dahuyphieu=1 then b.billcode end, '; ') as billcode_huy,
partition
------------
 //string lastupdatedate_bhyt = DateTime.ParseExact(datalichsuKCB.lastupdatedate_bhyt.ToString(), "d/M/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

cast(A.VATTU_TRONGGOI as numeric)
 LIKE ANY(ARRAY[" + this.lstservicepricecode + "])
cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric)

//-------Gan vao DTO
Mapper.Initialize(cfg => cfg.CreateMap<XML1_TagDTO, XML_HOSODTO>());
_xmlHoSo = AutoMapper.Mapper.Map<XML1_TagDTO, XML_HOSODTO>(_xml1_TagDto);

-----
if (SplashScreenManager.Default == null || SplashScreenManager.Default.IsSplashFormVisible == false)
{
SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
}



1. cổng của bv gửi 
https://gdbhyt.baohiemxahoi.gov.vn/
acc : 31153_BV
pass : viettiep31153
2. cổng giám định của bh 
giamdinh.baohiemxahoi.gov.vn
acc: 31_HALT
pass: Hadung6@

 &=&=IF(P{r}=1,M{r},0)
 
------=====================
--SQL Server
SELECT FORMAT(SYSDATETIME(),'yyyyMMddHHmmss') as 'yyyyMMddHHmmss'

--hay

SELECT CONVERT(VARCHAR(19), SYSDATETIME(), 120) AS 'YYYY-MM-DD HH:MI:SS(24h)'

SELECT REPLACE(REPLACE(REPLACE(CAST(CONVERT(VARCHAR(19),SYSDATETIME(),120) AS VARCHAR(20)),'-',''),':',''),' ','') AS 'YYYYMMDDHHMISS'

SELECT (CONVERT(VARCHAR(8), SYSDATETIME(), 24) + ' ' + CONVERT(VARCHAR(10), SYSDATETIME(), 101)) AS 'HH:MI:SS DD/MM/YYYY'


-----
select service_broker_guid from sys.databases where name='O2S_STUDENTMANAGEMENT'


   using (var db = new DAL.O2S_STUDENTMANAGEMENTEntities())
                {
                    List<SM_VERSION> lstVersion = db.SM_VERSION.Where(o => o.app_type == 0).ToList();
                    if (lstVersion == null || lstVersion.Count > 0)
                    {
                        txtUpdateVersionLink.Text = lstVersion[0].app_link;
                        txtUrlFullServer.Text = lstVersion[0].urlfullserver;
                    }
                }




------
https://svn.3asoft.vn:444/svn/Invoice3A 
Data Source=119.17.215.98,1433\SQL2008;Initial Catalog=QLBL;Persist Security Info=True;User ID=ktx;Password=ktxa2oi6

Tóm lại, cấu trúc của lệnh IF rút gọn là:
( biểu-thức-điều-kiện ) ? giá-trị-khi-BTDK-đúng : giá-trị-khi-BTĐK-sai ;


//DateTime NgayBD = DateTime.ParseExact(dateNgayBD.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
 dateTuNgay.MaxDate = dateDenNgay.MaxDate = 
 DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);



--trung đội 2


 
 
 
 
