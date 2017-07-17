--Báo cáo bảng kê tổng hợp hóa đơn - bv Thanh Hóa ngày 17/7/2017

select ROW_NUMBER () OVER (ORDER BY big.billgroupdate) as stt,
	big.billgroupcode,
	big.billgroupdate,
	big.sophieusudung,
	(big.sophieufrom || '-' || big.sophieuto) sophieutu_den,
	string_agg(case when b.dahuyphieu=1 then b.billcode end, '; ') as billcode_huy,	
	sum(case when b.dahuyphieu=0 then b.datra else 0 end) as tongtien_thu
from (select billgroupcode,dahuyphieu,datra,billcode from bill where billdate between '" + tungay + "' and '" + denngay + "') b
	inner join billgroup big on big.billgroupcode=b.billgroupcode and billgrouptype=" + billgrouptype + "
group by big.billgroupcode,big.sophieufrom,big.sophieuto,big.billgroupdate,big.sophieusudung;





