--Bao cao BN Su dung dich vu theo nhom ngay 27/7
-- nhom Thuoc han che su dung

--Theo khoa
SELECT ROW_NUMBER () OVER (ORDER BY mefo.medicinename) as stt,
		mefo.medicinecode as servicepricecode,
		mefo.medicinename as servicepricename,
		O.money_dangdt_khoa,
		O.money_dangdt_tv,
		O.money_rvchuatt_khoa,
		O.money_rvchuatt_tv,
		O.money_datt_khoa,
		O.money_datt_tv,
		(coalesce(O.money_dangdt_khoa,0)+coalesce(O.money_rvchuatt_khoa,0)+coalesce(O.money_datt_khoa,0)) as money_tong_khoa,
		(coalesce(O.money_dangdt_tv,0)+coalesce(O.money_rvchuatt_tv,0)+coalesce(O.money_datt_tv,0)) as money_tong_tv	
FROM 
	(select
		mef.medicinerefid_org,
		sum(A.money_dangdt_khoa) as money_dangdt_khoa,
		sum(A.money_dangdt_tv) as money_dangdt_tv,
		sum(A.money_rvchuatt_khoa) as money_rvchuatt_khoa,
		sum(A.money_rvchuatt_tv) as money_rvchuatt_tv,
		sum(A.money_datt_khoa) as money_datt_khoa,
		sum(A.money_datt_tv) as money_datt_tv	
FROM 
	(select 
			ser.servicepricecode,
			sum(case when vp.doituongbenhnhanid=1 and vp.vienphistatus=0 and ser.departmentgroupid="+cboKhoa.EditValue+" then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as money_dangdt_khoa,
			sum(case when vp.doituongbenhnhanid=1 and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as money_dangdt_tv,
			sum(case when vp.doituongbenhnhanid=1 and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 and ser.departmentgroupid="+cboKhoa.EditValue+" then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as money_rvchuatt_khoa,
			sum(case when vp.doituongbenhnhanid=1 and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as money_rvchuatt_tv,
			sum(case when vp.doituongbenhnhanid=1 and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' and ser.departmentgroupid="+cboKhoa.EditValue+" then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as money_datt_khoa,
			sum(case when vp.doituongbenhnhanid=1 and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as money_datt_tv
	from (select vienphiid,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp,doituongbenhnhanid from vienphi where vienphidate>='"+GlobalStore.KhoangThoiGianLayDuLieu+"') vp
		inner join (select vienphiid,servicepricecode,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype,servicepricemoney
				from serviceprice where servicepricedate>='"+GlobalStore.KhoangThoiGianLayDuLieu+"' and thuockhobanle=0 and soluong>0 "+lstservicepricecode+") ser on ser.vienphiid=vp.vienphiid	
	group by ser.servicepricecode) A
INNER JOIN (select medicinecode,medicinename,medicinerefid,medicinerefid_org from medicine_ref where medicinegroupcode='"+this.medicinegroupcode_thuoc+"') mef on mef.medicinecode=A.servicepricecode
group by mef.medicinerefid_org) O
INNER JOIN (select medicinecode,medicinename,medicinerefid from medicine_ref where medicinegroupcode='"+this.medicinegroupcode_thuoc+"') mefo on mefo.medicinerefid=O.medicinerefid_org;






--------Tong hop 
SELECT ROW_NUMBER () OVER (ORDER BY degp.departmentgroupname) as stt,
		degp.departmentgroupname,
		A.dangdt_1127,
		A.rvchuatt_1127,
		A.datt_1127,
		(coalesce(A.dangdt_1127)+coalesce(A.rvchuatt_1127)+coalesce(A.datt_1127)) as tong_1127,
		A.dangdt_5411,
		A.rvchuatt_5411,
		A.datt_5411,
		(coalesce(A.dangdt_5411)+coalesce(A.rvchuatt_5411)+coalesce(A.datt_5411)) as tong_5411,
		A.dangdt_4257,
		A.rvchuatt_4257,
		A.datt_4257,
		(coalesce(A.dangdt_4257)+coalesce(A.rvchuatt_4257)+coalesce(A.datt_4257)) as tong_4257,
		A.dangdt_1517,
		A.rvchuatt_1517,
		A.datt_1517,
		(coalesce(A.dangdt_1517)+coalesce(A.rvchuatt_1517)+coalesce(A.datt_1517)) as tong_1517,
		A.dangdt_3552,
		A.rvchuatt_3552,
		A.datt_3552,
		(coalesce(A.dangdt_3552)+coalesce(A.rvchuatt_3552)+coalesce(A.datt_3552)) as tong_3552,
		A.dangdt_0553,
		A.rvchuatt_0553,
		A.datt_0553,
		(coalesce(A.dangdt_0553)+coalesce(A.rvchuatt_0553)+coalesce(A.datt_0553)) as tong_0553,
		A.dangdt_4011,
		A.rvchuatt_4011,
		A.datt_4011,
		(coalesce(A.dangdt_4011)+coalesce(A.rvchuatt_4011)+coalesce(A.datt_4011)) as tong_4011,
		A.dangdt_2835,
		A.rvchuatt_2835,
		A.datt_2835,
		(coalesce(A.dangdt_2835)+coalesce(A.rvchuatt_2835)+coalesce(A.datt_2835)) as tong_2835,
		A.dangdt_5006,
		A.rvchuatt_5006,
		A.datt_5006,
		(coalesce(A.dangdt_5006)+coalesce(A.rvchuatt_5006)+coalesce(A.datt_5006)) as tong_5006,
		A.dangdt_0338,
		A.rvchuatt_0338,
		A.datt_0338,
		(coalesce(A.dangdt_0338)+coalesce(A.rvchuatt_0338)+coalesce(A.datt_0338)) as tong_0338	
FROM 
	(select
		ser.departmentgroupid,
		sum(case when ser.servicepricecode like 'T34869-1127%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_1127,
		sum(case when ser.servicepricecode like 'T34869-1127%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_1127,
		sum(case when ser.servicepricecode like 'T34869-1127%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_1127,
		--tong_1127,
		sum(case when ser.servicepricecode like 'T33286-5411%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_5411,
		sum(case when ser.servicepricecode like 'T33286-5411%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_5411,
		sum(case when ser.servicepricecode like 'T33286-5411%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_5411,
		--tong_5411,
		sum(case when ser.servicepricecode like 'T36589-4257%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_4257,
		sum(case when ser.servicepricecode like 'T36589-4257%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_4257,
		sum(case when ser.servicepricecode like 'T36589-4257%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_4257,
		--tong_4257,
		sum(case when ser.servicepricecode like 'T32767-1517%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_1517,
		sum(case when ser.servicepricecode like 'T32767-1517%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_1517,
		sum(case when ser.servicepricecode like 'T32767-1517%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_1517,
		--tong_1517,
		sum(case when ser.servicepricecode like 'T39104-3552%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_3552,
		sum(case when ser.servicepricecode like 'T39104-3552%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_3552,
		sum(case when ser.servicepricecode like 'T39104-3552%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_3552,
		--tong_3552,
		sum(case when ser.servicepricecode like 'T33369-0553%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_0553,
		sum(case when ser.servicepricecode like 'T33369-0553%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_0553,
		sum(case when ser.servicepricecode like 'T33369-0553%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_0553,
		--tong_0553,
		sum(case when ser.servicepricecode like 'T33281-4011%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_4011,
		sum(case when ser.servicepricecode like 'T33281-4011%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_4011,
		sum(case when ser.servicepricecode like 'T33281-4011%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_4011,
		--tong_4011,
		sum(case when ser.servicepricecode like 'T36835-2835%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_2835,
		sum(case when ser.servicepricecode like 'T36835-2835%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_2835,
		sum(case when ser.servicepricecode like 'T36835-2835%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_2835,
		--tong_2835,
		sum(case when ser.servicepricecode like 'T34221-5006%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_5006,
		sum(case when ser.servicepricecode like 'T34221-5006%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_5006,
		sum(case when ser.servicepricecode like 'T34221-5006%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_5006,
		--tong_5006,
		sum(case when ser.servicepricecode like 'T33403-0338%' and vp.vienphistatus=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as dangdt_0338,
		sum(case when ser.servicepricecode like 'T33403-0338%' and vp.vienphistatus>0 and coalesce(vp.vienphistatus_vp,0)=0 then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as rvchuatt_0338,
		sum(case when ser.servicepricecode like 'T33403-0338%' and vp.vienphistatus>0 and vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' then (case when ser.maubenhphamphieutype=0 then (ser.soluong*ser.servicepricemoney) else 0-(ser.soluong*ser.servicepricemoney) end) else 0 end) as datt_0338
		--tong_0338 	
	from (select vienphiid,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp,doituongbenhnhanid from vienphi where vienphidate>='"+GlobalStore.KhoangThoiGianLayDuLieu+"' and doituongbenhnhanid=1) vp
		 inner join (select vienphiid,servicepricecode,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype,servicepricemoney
					from serviceprice where servicepricedate>='"+GlobalStore.KhoangThoiGianLayDuLieu+"' and thuockhobanle=0 and soluong>0 "+lstservicepricecode+") ser on ser.vienphiid=vp.vienphiid	
	group by ser.departmentgroupid) A
INNER JOIN (select departmentgroupid,departmentgroupname from departmentgroup) degp ON degp.departmentgroupid=A.departmentgroupid;


-----------===========
select * from medicine_ref where medicinegroupcode='T41446-1948' and medicinecode not like '%.%'
order by medicinecode

"T34869-1127";"Basultam [Cefoperazon+Sulbactam 1g+1g]"
"T33286-5411";"Cefe injection "Swiss" 1g [Cefmetazole]"
"T36589-4257";"Colistin TZF [Colistin 1.000.000IU]"
"T32767-1517";"Mikrobiel [Moxifloxacin 400mg/250ml]"
"T39104-3552";"Minata Inj. 1g [Cefpirom]"
"T33369-0553";"Phillebicel 500mg [Ceftizoxim]"
"T33281-4011";"Proxacin 1% [Ciprofloxacin 200mg/20ml]"
"T36835-2835";"Tienam [Imipenem+Cilastatin 500+500mg]"
"T34221-5006";"Verapime [Cefepim 2g]"
"T33403-0338";"Vitazovilin [Piperacilin + Tazobactam 2.25g]"








