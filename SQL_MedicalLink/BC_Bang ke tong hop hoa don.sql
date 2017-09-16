--Báo cáo bảng kê tổng hợp hóa đơn - bv Thanh Hóa ngày 16/9/2017
--16/9 them loc theo nguoi thu tien

select ROW_NUMBER () OVER (ORDER BY big.billgroupdate) as stt, 
		big.billgroupcode, 
		big.billgroupdate, 
		big.sophieusudung, 
		(big.sophieufrom || '-' || big.sophieuto) sophieutu_den, 
		string_agg(case when b.dahuyphieu=1 then b.billcode end, 
		'; ') as billcode_huy, 
		sum(case when b.dahuyphieu=0 then (b.datra-b.miengiam) else 0 end) as tongtien_thu,
		sum(case when b.dahuyphieu=0 then b.miengiam else 0 end) as miengiam,
		b.userid,
		ngthu.username as nguoithu
from (select billgroupcode,dahuyphieu,datra,billcode,(case when miengiam<>'' then cast(replace(miengiam,',','') as numeric) else 0 end) as miengiam,userid from bill where billdate between '" + tungay + "' and '" + denngay + "' and userid in ("+_listuserid+") ) b 
	inner join billgroup big on big.billgroupcode=b.billgroupcode and billgrouptype=" + billgrouptype + " 
	LEFT JOIN nhompersonnel ngthu ON ngthu.userhisid=b.userid
group by big.billgroupcode,big.sophieufrom,big.sophieuto,big.billgroupdate,big.sophieusudung,b.userid,ngthu.username;



