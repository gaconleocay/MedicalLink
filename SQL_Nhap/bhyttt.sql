select * from 
(select patientid, hosobenhanid,  count(*)
from bhyt 
where bhytdate>'2017-01-01 00:00:00' and bhytcode <>''
group by patientid, hosobenhanid ) A
where a.count>1

select * from bhyt where patientid=522674 and bhytdate>'2017-01-01 00:00:00'