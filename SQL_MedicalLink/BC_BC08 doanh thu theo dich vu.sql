---Báo cáo Doanh thu theo dịch vụ - BC08
--ngay 8/11/2017


SELECT serf.servicepricegroupcode,
		serf.bhyt_groupcode,
		serf.servicegrouptype,
		serf.servicepricetype,
		(case serf.servicegrouptype  
				when 1 then 'Khám bệnh'  
				when 2 then 'Xét nghiệm'  
				when 3 then 'CĐHA'  
				when 4 then 'Chuyên khoa'     
				end) as servicegrouptype_name,
		serf.servicepricecode,
		serf.servicepricename,
		serf.servicepricenamebhyt,
		serf.servicepriceunit,
		ser.loaidoituong,
		ser.loaidoituong_name,
		ser.soluong,
		ser.servicepricemoney,
		ser.thanhtien,
		'0' as isgroup
		
FROM (select servicepricegroupcode,bhyt_groupcode,servicegrouptype,servicepricetype,servicepricecode,servicepricename,servicepricenamebhyt,servicepriceunit,servicepricefee,servicepricefeenhandan,servicepricefeebhyt,servicepricefeenuocngoai from servicepriceref where "+_servicegrouptype+") serf
	left join (select se.servicepricecode,
					sum(se.soluong) as soluong,
					se.loaidoituong,
					(case se.loaidoituong 
							when 0 then 'BHYT'
							when 1 then 'VP'
							when 2 then 'Đi kèm'
							when 3 then 'YC'
							when 4 then 'BHYT+YC'
							when 6 then 'BHYT+YC'
							when 20 then 'TT riêng'
							end) as loaidoituong_name,
					(case se.loaidoituong 
							when 0 then se.servicepricemoney_bhyt
							when 1 then se.servicepricemoney_nhandan
							when 2 then se.servicepricemoney_bhyt
							when 3 then se.servicepricemoney
							when 4 then se.servicepricemoney
							when 6 then se.servicepricemoney
							when 20 then se.servicepricemoney_bhyt
							else 0 end) as servicepricemoney,
					sum((case se.loaidoituong 
							when 0 then se.servicepricemoney_bhyt
							when 1 then se.servicepricemoney_nhandan
							when 2 then se.servicepricemoney_bhyt
							when 3 then se.servicepricemoney
							when 4 then se.servicepricemoney
							when 6 then se.servicepricemoney
							when 20 then se.servicepricemoney_bhyt
							else 0 end)*se.soluong) as thanhtien,
					se.bhyt_groupcode					
				from (select vienphiid,servicepricecode,loaidoituong,bhyt_groupcode,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney from serviceprice where "+_bhyt_groupcode +_servicepricedate+") se
					inner join (select vienphiid from vienphi where "+_trangthaibenhan+_vienphidate+") vp on vp.vienphiid=se.vienphiid
				group by se.servicepricecode,se.loaidoituong,se.bhyt_groupcode,se.servicepricemoney_bhyt,se.servicepricemoney_nhandan,se.servicepricemoney) ser on ser.servicepricecode=serf.servicepricecode
WHERE ser.soluong>0 or serf.servicepricetype=1
ORDER BY serf.servicegrouptype,serf.servicepricegroupcode,serf.servicepricename;
	






  Đang điều trị
Ra viện chưa thanh toán
Đã thanh toán


Khám bệnh
Xét nghiệm
Chẩn đoán hình ảnh
Chuyên khoa
Tất cả


servicepricecode='U5582-1609'


 public string servicepricegroupcode { get; set; }
        public string bhyt_groupcode { get; set; }
        public int servicegrouptype { get; set; }
        public string servicegrouptype_name { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public decimal servicepricenamebhyt { get; set; }
        public string servicepriceunit { get; set; }
        public int loaidoituong { get; set; }
        public string loaidoituong_name { get; set; }
        public decimal soluong { get; set; }
        public decimal servicepricemoney { get; set; }
        public decimal thanhtien { get; set; }







