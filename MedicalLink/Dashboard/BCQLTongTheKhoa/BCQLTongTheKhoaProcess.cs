﻿using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess.FilterDTO;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucBCQLTongTheKhoa : UserControl
    {
        private void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                SQLLayDuLieuBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void SQLLayDuLieuBaoCao()
        {
            try
            {
                string lstdepartmentid = " and vpm.departmentid in (" + this.lstPhongChonLayBC + ")";
                string lstdepartmentid_mrd = " and mrd.departmentid in (" + this.lstPhongChonLayBC + ")";
                string lstdepartmentid_tt = " and spt.departmentid in (" + this.lstPhongChonLayBC + ")";
                string lstdepartmentid_bill = " and b.departmentid in (" + this.lstPhongChonLayBC + ")";
                string lstphongravien = " and spt.phongravien in (" + this.lstPhongChonLayBC + ")";

                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string _thutienstatus = "";

                if (chkThuTienStatus.Checked)
                {
                    _thutienstatus = " and spt.thutienstatus=1 ";
                }

                //25/3/2018
                string sqlBaoCao_DangDT = " SELECT count(*) as dangdt_slbn, sum(vpm.money_khambenh) as dangdt_tienkb, sum(vpm.money_xetnghiem) as dangdt_tienxn, sum(coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0)) as dangdt_tiencdhatdcn, sum(vpm.money_pttt) as dangdt_tienpttt, sum(vpm.money_ptttyeucau) as dangdt_tienptttyeucau, sum(vpm.money_dvktc) as dangdt_tiendvktc, sum(vpm.money_giuongthuong) as dangdt_tiengiuongthuong, sum(vpm.money_giuongyeucau) as dangdt_tiengiuongyeucau, sum(coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0)) as dangdt_tienkhac, sum(vpm.money_vattu) as dangdt_tienvattu, sum(vpm.money_vattu_ttrieng) as dangdt_tienvattu_ttrieng, sum(vpm.money_mau) as dangdt_tienmau, sum(vpm.money_thuoc) as dangdt_tienthuoc, sum(coalesce(vpm.money_khambenh,0) + coalesce(vpm.money_xetnghiem,0) + coalesce(vpm.money_cdha,0) + coalesce(vpm.money_tdcn,0) + coalesce(vpm.money_pttt,0) + coalesce(vpm.money_dvktc,0) + coalesce(vpm.money_giuongthuong,0) + coalesce(vpm.money_giuongyeucau,0) + coalesce(vpm.money_khac,0) + coalesce(vpm.money_phuthu,0) + coalesce(vpm.money_vanchuyen,0) + coalesce(vpm.money_vattu,0) + coalesce(vpm.money_mau,0) + coalesce(vpm.money_thuoc,0) + coalesce(vpm.money_ptttyeucau,0) + coalesce(vpm.money_vattu_ttrieng,0)) as dangdt_tongtien, sum(vpm.tam_ung) as dangdt_tamung FROM ( SELECT mrd.vienphiid, mrd.patientid, mrd.hosobenhanid, mrd.loaibenhanid, mrd.departmentgroupid, mrd.departmentid, mrd.doituongbenhnhanid, sum(case when ser.bhyt_groupcode='01KB' and ser.loaidoituong=0 then (case when mrd.loaibenhanid=24 and (ser.lankhambenh = 0 or ser.lankhambenh is null) then ser.servicepricemoney_bhyt*ser.soluong when mrd.loaibenhanid=1 then ser.servicepricemoney_bhyt*ser.soluong else 0 end) when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='01KB' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='01KB' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_khambenh, sum(case when ser.bhyt_groupcode='03XN' and ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='03XN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='03XN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_xetnghiem, sum(case when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='04CDHA' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_cdha, sum(case when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='05TDCN' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_tdcn, sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')<>'PTYC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_pttt, sum(case when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='06PTTT' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='06PTTT')='PTYC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_ptttyeucau, sum(case when ser.bhyt_groupcode='08MA' and ser.loaidoituong=0 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode='08MA' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end) when ser.bhyt_groupcode='08MA' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) as money_mau, sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')<>'G303YC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_giuongthuong, sum(case when ser.bhyt_groupcode='12NG' and ser.loaidoituong=0 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (1,8) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='12NG' and ser.loaidoituong in (4,6) and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='12NG' and ser.loaidoituong=3 and ((select serf.servicepricegroupcode from servicepriceref serf where serf.servicepricecode=ser.servicepricecode and serf.bhyt_groupcode='12NG')='G303YC') then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_giuongyeucau, sum(case when ser.bhyt_groupcode='11VC' and ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='11VC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='11VC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_vanchuyen, sum(case when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='999DVKHAC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_khac, sum(case when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney else ser.servicepricemoney_bhyt end)*ser.soluong end) when ser.bhyt_groupcode='1000PhuThu' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as money_phuthu, (sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (0,2,4,6) then ser.servicepricemoney_bhyt*ser.soluong else 0 end) + sum(case when ser.loaidoituong=2 and ser.servicepriceid_master in (select ser_ktc.servicepriceid from serviceprice ser_ktc where ser_ktc.vienphiid=mrd.vienphiid and ser_ktc.bhyt_groupcode='07KTC') and ((select seref.tinhtoanlaigiadvktc from servicepriceref seref where seref.servicepricecode=(select ser_ktc.servicepricecode from serviceprice ser_ktc where ser_ktc.servicepriceid=ser.servicepriceid_master))=1) then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan * ser.soluong) end) else 0 end) + sum(case when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney_nhandan*ser.soluong end) when ser.bhyt_groupcode='07KTC' and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (ser.servicepricemoney_nuocngoai-servicepricemoney_bhyt)*ser.soluong else (case when ser.servicepricemoney>ser.servicepricemoney_bhyt then ser.servicepricemoney-servicepricemoney_bhyt else 0 end)*ser.soluong end) when ser.bhyt_groupcode='07KTC' and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then ser.servicepricemoney_nuocngoai*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end)) as money_dvktc, sum(case when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=0 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end) when ser.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) as money_thuoc, sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=0 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (1,8) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nuocngoai*ser.soluong else 0-(ser.servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_nhandan*ser.soluong else 0-(ser.servicepricemoney_nhandan*ser.soluong) end) end) when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong in (4,6) then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney_nuocngoai)*ser.soluong else 0-((ser.servicepricemoney_nuocngoai)*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then (ser.servicepricemoney)*ser.soluong else 0-((ser.servicepricemoney)*ser.soluong) end) end) when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=3 then (case when ser.doituongbenhnhanid=4 then (case when ser.maubenhphamphieutype=0 then servicepricemoney_nuocngoai*ser.soluong else 0-(servicepricemoney_nuocngoai*ser.soluong) end) else (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney*ser.soluong else 0-(ser.servicepricemoney*ser.soluong) end) end) else 0 end) as money_vattu, sum(case when ser.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle') and ser.loaidoituong=20 and ser.servicepriceid_thanhtoanrieng>0 then (case when ser.maubenhphamphieutype=0 then ser.servicepricemoney_bhyt*ser.soluong else 0-(ser.servicepricemoney_bhyt * ser.soluong) end) else 0 end) as money_vattu_ttrieng, (select sum(bill.datra) from bill where bill.vienphiid=mrd.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung FROM medicalrecord mrd left join serviceprice ser on mrd.vienphiid=ser.vienphiid and ser.thuockhobanle=0 WHERE mrd.thoigianvaovien >='" + this.KhoangThoiGianLayDuLieu + "' " + lstdepartmentid_mrd + " and mrd.medicalrecordstatus=2 GROUP BY mrd.vienphiid, mrd.patientid, mrd.hosobenhanid, mrd.loaibenhanid, mrd.departmentgroupid, mrd.departmentid, mrd.doituongbenhnhanid ) VPM;";

                //25/5/2018
                string sqlBaoCao_RaVienChuaTT = @"SELECT count(A.*) as ravienchuatt_slbn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0)) as numeric),0),0) as ravienchuatt_tienkb,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienxn,0)) as numeric),0),0) as ravienchuatt_tienxn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiencdhatdcn,0)) as numeric),0),0) as ravienchuatt_tiencdhatdcn,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienpttt,0)) as numeric),0),0) as ravienchuatt_tienpttt,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienptttyeucau,0)) as numeric),0),0) as ravienchuatt_tienptttyeucau,
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiendvktc,0)) as numeric),0),0) as ravienchuatt_tiendvktc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongthuong,0)) as numeric),0),0) as ravienchuatt_tiengiuongthuong,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tiengiuongyeucau,0)) as numeric),0),0) as ravienchuatt_tiengiuongyeucau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkhac,0)) as numeric),0),0) as ravienchuatt_tienkhac,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu,0)) as numeric),0),0) as ravienchuatt_tienvattu, 
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienvattu_ttrieng,0)) as numeric),0),0) as ravienchuatt_tienvattu_ttrieng, 
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienmau,0)) as numeric),0),0) as ravienchuatt_tienmau,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienthuoc,0)) as numeric),0),0) as ravienchuatt_tienthuoc,  
COALESCE(round(cast(sum(COALESCE(A.ravienchuatt_tienkb,0) + COALESCE(A.ravienchuatt_tienxn,0) + COALESCE(A.ravienchuatt_tiencdhatdcn,0) + COALESCE(A.ravienchuatt_tienpttt,0) + COALESCE(A.ravienchuatt_tiendvktc,0) + COALESCE(A.ravienchuatt_tiengiuongthuong,0) + COALESCE(A.ravienchuatt_tiengiuongyeucau,0) + COALESCE(A.ravienchuatt_tienkhac,0) + COALESCE(A.ravienchuatt_tienvattu,0) + COALESCE(A.ravienchuatt_tienmau,0) + COALESCE(A.ravienchuatt_tienthuoc,0) + COALESCE(A.ravienchuatt_tienvattu_ttrieng,0) + COALESCE(A.ravienchuatt_tienptttyeucau,0)) as numeric),0),0) as ravienchuatt_tongtien
FROM (select spt.vienphiid,  
		sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as ravienchuatt_tienkb,  
		sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as ravienchuatt_tienxn,  
		sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as ravienchuatt_tiencdhatdcn,  
		sum(spt.money_pttt_bh + spt.money_pttt_vp) as ravienchuatt_tienpttt,  
		sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as ravienchuatt_tienptttyeucau, 
		sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as ravienchuatt_tiendvktc,  
		sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as ravienchuatt_tiengiuongthuong,  
		sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as ravienchuatt_tiengiuongyeucau,  
		sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as ravienchuatt_tienkhac,  
		sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as ravienchuatt_tienvattu,  
		sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as ravienchuatt_tienvattu_ttrieng,  
		sum(spt.money_mau_bh + spt.money_mau_vp) as ravienchuatt_tienmau,  
		sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as ravienchuatt_tienthuoc 
		from ihs_servicespttt spt 
		where COALESCE(spt.vienphistatus_vp,0)=0 and spt.vienphistatus<>0 and spt.vienphidate_ravien>='" + this.KhoangThoiGianLayDuLieu + "' " + lstphongravien + _thutienstatus + " group by spt.vienphiid) A ;";

                //30/5/2018
                string sqlBaoCao_RaVienDaTT = @" SELECT (select count(vp.*) from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) as raviendatt_slbn, round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0) as raviendatt_tienkb, round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0) as raviendatt_tienxn, round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0) as raviendatt_tiencdhatdcn, round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0) as raviendatt_tienpttt, round(cast(sum(COALESCE(A.raviendatt_tienptttyeucau,0)) as numeric),0) as raviendatt_tienptttyeucau, round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0) as raviendatt_tiendvktc, round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0) as raviendatt_tiengiuongthuong, round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0) as raviendatt_tiengiuongyeucau, round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0) as raviendatt_tienkhac, round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0) as raviendatt_tienvattu, round(cast(sum(COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0) as raviendatt_tienvattu_ttrieng, round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0) as raviendatt_tienmau, round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0) as raviendatt_tienthuoc, round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0) + COALESCE(A.raviendatt_tienptttyeucau,0) + COALESCE(A.raviendatt_tienvattu_ttrieng,0)) as numeric),0) as raviendatt_tongtien, round(cast(sum(COALESCE(CP_XN.chiphixn,0)) as numeric),0) as raviendatt_chiphixn, round(cast(sum(COALESCE(CP_CDHA.chiphicdha,0)) as numeric),0) as raviendatt_chiphicdha, round(cast(sum(COALESCE(CP_KHOA.chiphikhoa,0)) as numeric),0) as raviendatt_chiphikhoa FROM (select spt.khoaravien, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as raviendatt_tienkb, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as raviendatt_tienxn, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as raviendatt_tiencdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as raviendatt_tienpttt, sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as raviendatt_tienptttyeucau, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as raviendatt_tiendvktc, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as raviendatt_tiengiuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as raviendatt_tiengiuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as raviendatt_tienkhac, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as raviendatt_tienvattu, sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as raviendatt_tienvattu_ttrieng, sum(spt.money_mau_bh + spt.money_mau_vp) as raviendatt_tienmau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as raviendatt_tienthuoc from ihs_servicespttt spt where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstphongravien + _thutienstatus + " group by spt.khoaravien) A LEFT JOIN (SELECT XN.departmentgroupid, sum(XN.chiphixn) as chiphixn FROM (SELECT SERV.departmentgroupid, sum(SERV.soluong*chiphi.chiphixn) as chiphixn FROM (select ser.servicepriceid, ser.servicepricecode, ser.soluong, se.idmayxn, vp.departmentgroupid from (select vp.vienphiid,vp.departmentgroupid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) vp inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,vp.departmentgroupid) SERV LEFT JOIN (SELECT * FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp') AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn GROUP BY SERV.departmentgroupid UNION ALL SELECT SERV.departmentgroupid, sum(SERV.soluong*chiphi.chiphixn) as chiphixn FROM (select ser.servicepriceid, ser.servicepricecode, ser.soluong, vp.departmentgroupid from (select vp.vienphiid,vp.departmentgroupid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) vp inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid,maubenhphamid from serviceprice where bhyt_groupcode='03XN') ser on ser.vienphiid=vp.vienphiid inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 and departmentid_des=253 and maubenhphamdate>'2018-01-01 00:00:00') mbp on mbp.maubenhphamid=ser.maubenhphamid group by ser.servicepriceid,ser.servicepricecode,ser.soluong,vp.departmentgroupid) SERV LEFT JOIN (SELECT * FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp') AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode GROUP BY SERV.departmentgroupid) XN GROUP BY XN.departmentgroupid) CP_XN on CP_XN.departmentgroupid=A.khoaravien LEFT JOIN (SELECT A.departmentgroupid, sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha FROM (SELECT vp.departmentgroupid,vp.vienphiid,ser.servicepriceid, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as thuoc_tronggoi, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as vattu_tronggoi, (((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as chiphikhac, ((case serf.pttt_loaiid when 1 then 1201000 when 2 then 572000 when 3 then 243000 when 4 then 149000 when 5 then 185000 when 6 then 70500 when 7 then 35500 when 8 then 27500 else 0 end) * ser.soluong) as chiphibs FROM (select vp.vienphiid,vp.departmentgroupid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ")) vp inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN')) ser on ser.vienphiid=vp.vienphiid inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=A.khoaravien LEFT JOIN (select departmentgroupid,sum(medicinestorebilltotalmoney) as chiphikhoa from medicine_store_bill where medicinestorebilltype=202 and medicinestorebillstatus=2 and isremove=0 and bill_mode=4 and ngaysudungthuoc between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ") group by departmentgroupid ) CP_KHOA on CP_KHOA.departmentgroupid=A.khoaravien;  ";

                string sqlBNRaVienChuyenDiChuyenDen = "SELECT * FROM ((SELECT '3-BN ra vien' as name, count(vp.vienphiid) as ravien_slbn FROM vienphi vp WHERE vp.vienphistatus>0 and vp.vienphidate_ravien>='" + this.thoiGianTu + "' and vp.vienphidate_ravien<='" + this.thoiGianDen + "' and vp.departmentid in (" + this.lstPhongChonLayBC + ")) Union (SELECT '1-BN chuyen di' as name, count(A1.vienphiid) as bn_chuyendi FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd WHERE mrd.departmentid in (" + this.lstPhongChonLayBC + ") and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + this.thoiGianTu + "' and mrd.thoigianravien<='" + this.thoiGianDen + "') A1) Union (SELECT '2-BN chuyen den' as name, count(A2.vienphiid) as bn_chuyenden FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd WHERE mrd.departmentid in (" + this.lstPhongChonLayBC + ") and mrd.hinhthucvaovienid=3 and mrd.thoigianravien>='" + this.thoiGianTu + "' and mrd.thoigianravien<='" + this.thoiGianDen + "') A2)) O ORDER BY O.name;";

                //29/3/2018
                string sqlDoanhThu = @"SELECT sum(A.doanhthu_slbn) as doanhthu_slbn, sum(B.doanhthuGM_slbn) as doanhthuGM_slbn, (select count(sl.*) from (select vienphiid from ihs_servicespttt spt where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + _thutienstatus + " and (spt.departmentid in (" + this.lstPhongChonLayBC + ") or (spt.departmentid in (34,335,269,285) and spt.departmentgroup_huong in (" + this.lstKhoaChonLayBC + "))) group by spt.vienphiid) sl) as doanhthuTongGM_slbn, round(cast(sum(COALESCE(A.doanhthu_tienkb,0)) as numeric),0) as doanhthu_tienkb, round(cast(sum(COALESCE(A.doanhthu_tienxn,0)) as numeric),0) as doanhthu_tienxn, round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthu_tiencdhatdcn, round(cast(sum(COALESCE(A.doanhthu_tienpttt,0)) as numeric),0) as doanhthu_tienpttt, round(cast(sum(COALESCE(A.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthu_tienptttyeucau, round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0)) as numeric),0) as doanhthu_tiendvktc, round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthu_tiengiuongthuong, round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthu_tiengiuongyeucau, round(cast(sum(COALESCE(A.doanhthu_tienkhac,0)) as numeric),0) as doanhthu_tienkhac, round(cast(sum(COALESCE(A.doanhthu_tienvattu,0)) as numeric),0) as doanhthu_tienvattu, round(cast(sum(COALESCE(A.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthu_tienvattu_ttrieng, round(cast(sum(COALESCE(A.doanhthu_tienmau,0)) as numeric),0) as doanhthu_tienmau, round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tienthuoc, round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0)) as numeric),0) as doanhthu_tongtien, round(cast(sum(COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuGM_tienkb, round(cast(sum(COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuGM_tienxn, round(cast(sum(COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuGM_tiencdhatdcn, round(cast(sum(COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuGM_tienpttt, round(cast(sum(COALESCE(B.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthuGM_tienptttyeucau, round(cast(sum(COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuGM_tiendvktc, round(cast(sum(COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuGM_tiengiuongthuong, round(cast(sum(COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuGM_tiengiuongyeucau, round(cast(sum(COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuGM_tienkhac, round(cast(sum(COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuGM_tienvattu, round(cast(sum(COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuGM_tienvattu_ttrieng, round(cast(sum(COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuGM_tienmau, round(cast(sum(COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuGM_tienthuoc, round(cast(sum(COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc ,0)) as numeric),0) as doanhthuGM_tongtien, round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienkb,0)) as numeric),0) as doanhthuTongGM_tienkb, round(cast(sum(COALESCE(A.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tienxn,0)) as numeric),0) as doanhthuTongGM_tienxn, round(cast(sum(COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0)) as numeric),0) as doanhthuTongGM_tiencdhatdcn, round(cast(sum(COALESCE(A.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienpttt,0)) as numeric),0) as doanhthuTongGM_tienpttt, round(cast(sum(COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tienptttyeucau,0)) as numeric),0) as doanhthuTongGM_tienptttyeucau, round(cast(sum(COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiendvktc,0)) as numeric),0) as doanhthuTongGM_tiendvktc, round(cast(sum(COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongthuong,0)) as numeric),0) as doanhthuTongGM_tiengiuongthuong, round(cast(sum(COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0)) as numeric),0) as doanhthuTongGM_tiengiuongyeucau, round(cast(sum(COALESCE(A.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienkhac,0)) as numeric),0) as doanhthuTongGM_tienkhac, round(cast(sum(COALESCE(A.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienvattu,0)) as numeric),0) as doanhthuTongGM_tienvattu, round(cast(sum(COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuTongGM_tienvattu_ttrieng, round(cast(sum(COALESCE(A.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienmau,0)) as numeric),0) as doanhthuTongGM_tienmau, round(cast(sum(COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienthuoc,0)) as numeric),0) as doanhthuTongGM_tienthuoc, round(cast(sum(COALESCE(A.doanhthu_tienkb,0) + COALESCE(A.doanhthu_tienxn,0) + COALESCE(A.doanhthu_tiencdhatdcn,0) + COALESCE(A.doanhthu_tienpttt,0) + COALESCE(A.doanhthu_tienptttyeucau,0) + COALESCE(A.doanhthu_tiendvktc,0) + COALESCE(A.doanhthu_tiengiuongthuong,0) + COALESCE(A.doanhthu_tiengiuongyeucau,0) + COALESCE(A.doanhthu_tienkhac,0) + COALESCE(A.doanhthu_tienvattu,0) + COALESCE(A.doanhthu_tienmau,0) + COALESCE(A.doanhthu_tienthuoc,0) + COALESCE(B.doanhthu_tienkb,0) + COALESCE(B.doanhthu_tienxn,0) + COALESCE(B.doanhthu_tiencdhatdcn,0) + COALESCE(B.doanhthu_tienpttt,0) + COALESCE(B.doanhthu_tienptttyeucau,0) + COALESCE(B.doanhthu_tiendvktc,0) + COALESCE(B.doanhthu_tiengiuongthuong,0) + COALESCE(B.doanhthu_tiengiuongyeucau,0) + COALESCE(B.doanhthu_tienkhac,0) + COALESCE(B.doanhthu_tienvattu,0) + COALESCE(B.doanhthu_tienmau,0) + COALESCE(B.doanhthu_tienthuoc,0) + COALESCE(A.doanhthu_tienvattu_ttrieng,0) + COALESCE(B.doanhthu_tienvattu_ttrieng,0)) as numeric),0) as doanhthuTongGM_tongtien, round(cast(sum(COALESCE(CP_XN.chiphixn,0)) as numeric),0) as doanhthu_chiphixn, round(cast(sum(COALESCE(CP_CDHA.chiphicdha,0)) as numeric),0) as doanhthu_chiphicdha, round(cast(sum(COALESCE(CP_KHOA.chiphikhoa,0)) as numeric),0) as doanhthu_chiphikhoa FROM (select spt1.departmentgroupid, count(spt1.*) as doanhthu_slbn, sum(spt1.doanhthu_tienkb) as doanhthu_tienkb, sum(spt1.doanhthu_tienxn) as doanhthu_tienxn, sum(spt1.doanhthu_tiencdhatdcn) as doanhthu_tiencdhatdcn, sum(spt1.doanhthu_tienpttt) as doanhthu_tienpttt, sum(spt1.doanhthu_tienptttyeucau) as doanhthu_tienptttyeucau, sum(spt1.doanhthu_tiendvktc) as doanhthu_tiendvktc, sum(spt1.doanhthu_tiengiuongthuong) as doanhthu_tiengiuongthuong, sum(spt1.doanhthu_tiengiuongyeucau) as doanhthu_tiengiuongyeucau, sum(spt1.doanhthu_tienkhac) as doanhthu_tienkhac, sum(spt1.doanhthu_tienvattu) as doanhthu_tienvattu, sum(spt1.doanhthu_tienvattu_ttrieng) as doanhthu_tienvattu_ttrieng, sum(spt1.doanhthu_tienmau) as doanhthu_tienmau, sum(spt1.doanhthu_tienthuoc) as doanhthu_tienthuoc from (select spt.departmentgroupid,spt.vienphiid, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt, sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as doanhthu_tienptttyeucau, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as doanhthu_tienvattu_ttrieng, sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc from ihs_servicespttt spt where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstdepartmentid_tt + _thutienstatus + " group by spt.departmentgroupid,spt.vienphiid) spt1 group by spt1.departmentgroupid) A LEFT JOIN (select spt1.departmentgroup_huong, count(spt1.*) as doanhthuGM_slbn, sum(spt1.doanhthu_tienkb) as doanhthu_tienkb, sum(spt1.doanhthu_tienxn) as doanhthu_tienxn, sum(spt1.doanhthu_tiencdhatdcn) as doanhthu_tiencdhatdcn, sum(spt1.doanhthu_tienpttt) as doanhthu_tienpttt, sum(spt1.doanhthu_tienptttyeucau) as doanhthu_tienptttyeucau, sum(spt1.doanhthu_tiendvktc) as doanhthu_tiendvktc, sum(spt1.doanhthu_tiengiuongthuong) as doanhthu_tiengiuongthuong, sum(spt1.doanhthu_tiengiuongyeucau) as doanhthu_tiengiuongyeucau, sum(spt1.doanhthu_tienkhac) as doanhthu_tienkhac, sum(spt1.doanhthu_tienvattu) as doanhthu_tienvattu, sum(spt1.doanhthu_tienvattu_ttrieng) as doanhthu_tienvattu_ttrieng, sum(spt1.doanhthu_tienmau) as doanhthu_tienmau, sum(spt1.doanhthu_tienthuoc) as doanhthu_tienthuoc from (select spt.departmentgroup_huong, 1 as doanhthuGM_slbn, sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as doanhthu_tienkb, sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as doanhthu_tienxn, sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as doanhthu_tiencdhatdcn, sum(spt.money_pttt_bh + spt.money_pttt_vp) as doanhthu_tienpttt, sum(spt.money_ptttyeucau_bh + spt.money_ptttyeucau_vp) as doanhthu_tienptttyeucau, sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as doanhthu_tiendvktc, sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as doanhthu_tiengiuongthuong, sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as doanhthu_tiengiuongyeucau, sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as doanhthu_tienkhac, sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as doanhthu_tienvattu, sum(spt.money_vattu_ttrieng_bh + spt.money_vattu_ttrieng_vp) as doanhthu_tienvattu_ttrieng, sum(spt.money_mau_bh + spt.money_mau_vp) as doanhthu_tienmau, sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as doanhthu_tienthuoc from ihs_servicespttt spt where spt.vienphistatus_vp=1 and spt.departmentid in (34,335,269,285) and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + _thutienstatus + " group by spt.departmentgroup_huong,spt.vienphiid) spt1 group by spt1.departmentgroup_huong) B ON A.departmentgroupid=B.departmentgroup_huong LEFT JOIN (SELECT XN.departmentgroupid, sum(XN.chiphixn) as chiphixn FROM (SELECT SERV.departmentgroupid, sum(SERV.soluong*chiphi.chiphixn) as chiphixn FROM (select ser.servicepriceid, ser.servicepricecode, ser.soluong, se.idmayxn, ser.departmentgroupid from (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "') vp inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid from serviceprice where bhyt_groupcode='03XN' and departmentid in (" + this.lstPhongChonLayBC + ")) ser on ser.vienphiid=vp.vienphiid left join (select s.idmayxn,servicepriceid from service s where coalesce(s.idmayxn,0)>0 and s.servicedate>'2017-05-01 00:00:00') se on se.servicepriceid=ser.servicepriceid group by ser.servicepriceid,ser.servicepricecode,ser.soluong,se.idmayxn,ser.departmentgroupid) SERV LEFT JOIN (SELECT * FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp') AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode and chiphi.mayxn_ma=SERV.idmayxn GROUP BY SERV.departmentgroupid UNION ALL SELECT SERV.departmentgroupid, sum(SERV.soluong*chiphi.chiphixn) as chiphixn FROM (select ser.servicepriceid, ser.servicepricecode, ser.soluong, ser.departmentgroupid from (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp inner join (select servicepriceid,vienphiid,servicepricecode,soluong,departmentgroupid,maubenhphamid from serviceprice where bhyt_groupcode='03XN' and departmentid in (" + this.lstPhongChonLayBC + ")) ser on ser.vienphiid=vp.vienphiid inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 and departmentid_des=253 and maubenhphamdate>'2018-01-01 00:00:00' and departmentid in (" + this.lstPhongChonLayBC + ")) mbp on mbp.maubenhphamid=ser.maubenhphamid group by ser.servicepriceid,ser.servicepricecode,ser.soluong,ser.departmentgroupid) SERV LEFT JOIN (SELECT * FROM dblink('myconn_mel','select cp.mayxn_ma,cp.servicepricecode,(cp.cp_hoachat+cp.cp_haophixn+cp.cp_luong+cp.cp_diennuoc+cp.cp_khmaymoc+cp.cp_khxaydung) as chiphixn from ml_mayxnchiphi cp') AS ml_mayxn(mayxn_ma integer,servicepricecode text,chiphixn double precision)) chiphi on chiphi.servicepricecode=SERV.servicepricecode GROUP BY SERV.departmentgroupid) XN GROUP BY XN.departmentgroupid) CP_XN on CP_XN.departmentgroupid=A.departmentgroupid LEFT JOIN (SELECT A.departmentgroupid, sum(coalesce(A.thuoc_tronggoi,0)+coalesce(A.vattu_tronggoi,0)+coalesce(A.chiphikhac,0)+(A.chiphibs * (A.tyle/100))) as chiphicdha FROM (SELECT ser.departmentgroupid,vp.vienphiid,ser.servicepriceid, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as thuoc_tronggoi, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) as vattu_tronggoi, (((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong) as chiphikhac, ((case serf.pttt_loaiid when 1 then 1201000 when 2 then 572000 when 3 then 243000 when 4 then 149000 when 5 then 185000 when 6 then 70500 when 7 then 35500 when 8 then 27500 else 0 end) * ser.soluong) as chiphibs FROM (select vp.vienphiid from vienphi vp where vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + thoiGianTu + "' and '" + thoiGianDen + "') vp inner join (select vienphiid,departmentgroupid,servicepriceid,servicepricecode,loaidoituong,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN') and departmentid in (" + this.lstPhongChonLayBC + ")) ser on ser.vienphiid=vp.vienphiid inner join (select servicepricecode,pttt_loaiid from servicepriceref where bhyt_groupcode in ('04CDHA','05TDCN')) serf on serf.servicepricecode=ser.servicepricecode) A GROUP BY A.departmentgroupid) CP_CDHA on CP_CDHA.departmentgroupid=A.departmentgroupid LEFT JOIN (select departmentgroupid,sum(medicinestorebilltotalmoney) as chiphikhoa from medicine_store_bill where medicinestorebilltype=202 and medicinestorebillstatus=2 and isremove=0 and bill_mode=4 and ngaysudungthuoc between '" + thoiGianTu + "' and '" + thoiGianDen + "' and departmentid in (" + this.lstPhongChonLayBC + ") group by departmentgroupid ) CP_KHOA on CP_KHOA.departmentgroupid=A.departmentgroupid; ";

                DataView dataBCTongTheKhoa_DangDT = new DataView(condb.GetDataTable_HIS(sqlBaoCao_DangDT));
                DataView dataBCTongTheKhoa_RaVienChuaTT = new DataView(condb.GetDataTable_HIS(sqlBaoCao_RaVienChuaTT));
                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.GetDataTable_HISToMeL(sqlBaoCao_RaVienDaTT));
                DataView dataBNRaVienChuyenDiChuyenDen = new DataView(condb.GetDataTable_HIS(sqlBNRaVienChuyenDiChuyenDen));
                DataView dataDoanhThu = new DataView(condb.GetDataTable_HISToMeL(sqlDoanhThu));

                if (dataBCTongTheKhoa_DangDT == null || dataBCTongTheKhoa_DangDT.Count <= 0)
                {
                    string sqlBaoCao_DangDT_null = "SELECT 0 as dangdt_slbn, 0 as dangdt_tienkb, 0 as dangdt_tienxn, 0 as dangdt_tiencdhatdcn, 0 as dangdt_tienpttt, 0 as dangdt_tienptttyeucau, 0 as dangdt_tiendvktc, 0 as dangdt_tiengiuongthuong, 0 as dangdt_tiengiuongyeucau, 0 as dangdt_tienkhac, 0 as dangdt_tienvattu, 0 as dangdt_tienvattu_ttrieng, 0 as dangdt_tienmau, 0 as dangdt_tienthuoc, 0 as dangdt_tongtien, 0 as dangdt_tamung;";
                    dataBCTongTheKhoa_DangDT = new DataView(condb.GetDataTable_HIS(sqlBaoCao_DangDT_null));
                }
                if (dataBCTongTheKhoa_RaVienChuaTT == null || dataBCTongTheKhoa_RaVienChuaTT.Count <= 0)
                {
                    string sqlBaoCao_RaVienChuaTT_null = "SELECT 0 as ravienchuatt_slbn, 0 as ravienchuatt_tienkb, 0 as ravienchuatt_tienxn, 0 as ravienchuatt_tiencdhatdcn, 0 as ravienchuatt_tienpttt, 0 as ravienchuatt_tienptttyeucau, 0 as ravienchuatt_tiendvktc, 0 as ravienchuatt_tiengiuongthuong, 0 as ravienchuatt_tiengiuongyeucau, 0 as ravienchuatt_tienkhac, 0 as ravienchuatt_tienvattu, 0 as ravienchuatt_tienvattu_ttrieng, 0 as ravienchuatt_tienmau, 0 as ravienchuatt_tienthuoc, 0 as ravienchuatt_tongtien, 0 as ravienchuatt_tamung;";
                    dataBCTongTheKhoa_RaVienChuaTT = new DataView(condb.GetDataTable_HIS(sqlBaoCao_RaVienChuaTT_null));
                }
                if (dataBCTongTheKhoa_RaVienDaTT == null || dataBCTongTheKhoa_RaVienDaTT.Count <= 0)
                {
                    string sqlBaoCao_RaVienDaTT_null = "SELECT 0 as raviendatt_slbn, 0 as raviendatt_tienkb, 0 as raviendatt_tienxn, 0 as raviendatt_tiencdhatdcn, 0 as raviendatt_tienpttt, 0 as raviendatt_tienptttyeucau, 0 as raviendatt_tiendvktc, 0 as raviendatt_tiengiuongthuong, 0 as raviendatt_tiengiuongyeucau, 0 as raviendatt_tienkhac, 0 as raviendatt_tienvattu, 0 as raviendatt_tienvattu_ttrieng, 0 as raviendatt_tienmau, 0 as raviendatt_tienthuoc, 0 as raviendatt_tongtien, 0 as raviendatt_chiphixn, 0 as raviendatt_chiphicdha, 0 as raviendatt_chiphikhoa; ";
                    dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.GetDataTable_HIS(sqlBaoCao_RaVienDaTT_null));
                }
                if (dataBNRaVienChuyenDiChuyenDen == null || dataBNRaVienChuyenDiChuyenDen.Count <= 0)
                {
                    string sqlBNRaVienChuyenDiChuyenDen_null = "select '3-BN ra vien' as name, 0 as ravien_slbn Union select '1-BN chuyen di' as name, 0 as bn_chuyendi UNION select '2-BN chuyen den' as name, 0 as bn_chuyenden;";
                    dataBNRaVienChuyenDiChuyenDen = new DataView(condb.GetDataTable_HIS(sqlBNRaVienChuyenDiChuyenDen_null));
                }
                if (dataDoanhThu == null || dataDoanhThu.Count <= 0 || dataDoanhThu[0]["doanhthu_slbn"].ToString() == "")
                {
                    string sqlDoanhThu_null = "SELECT 0 as doanhthu_slbn, 0 as doanhthu_tienkb, 0 as doanhthu_tienxn, 0 as doanhthu_tiencdhatdcn, 0 as doanhthu_tienpttt, 0 as doanhthu_tienptttyeucau, 0 as doanhthu_tiendvktc, 0 as doanhthu_tiengiuongthuong, 0 as doanhthu_tiengiuongyeucau, 0 as doanhthu_tienkhac, 0 as doanhthu_tienvattu, 0 as doanhthu_tienvattu_ttrieng, 0 as doanhthu_tienmau, 0 as doanhthu_tienthuoc, 0 as doanhthu_tongtien, 0 as doanhthuGM_slbn, 0 as doanhthuGM_tienkb, 0 as doanhthuGM_tienxn, 0 as doanhthuGM_tiencdhatdcn, 0 as doanhthuGM_tienpttt, 0 as doanhthuGM_tienptttyeucau, 0 as doanhthuGM_tiendvktc, 0 as doanhthuGM_tiengiuongthuong, 0 as doanhthuGM_tiengiuongyeucau, 0 as doanhthuGM_tienkhac, 0 as doanhthuGM_tienvattu, 0 as doanhthuGM_tienvattu_ttrieng, 0 as doanhthuGM_tienmau, 0 as doanhthuGM_tienthuoc, 0 as doanhthuGM_tongtien, 0 as doanhthuTongGM_slbn, 0 as doanhthuTongGM_tienkb, 0 as doanhthuTongGM_tienxn, 0 as doanhthuTongGM_tiencdhatdcn, 0 as doanhthuTongGM_tienpttt, 0 as doanhthuTongGM_tienptttyeucau, 0 as doanhthuTongGM_tiendvktc, 0 as doanhthuTongGM_tiengiuongthuong, 0 as doanhthuTongGM_tiengiuongyeucau, 0 as doanhthuTongGM_tienkhac, 0 as doanhthuTongGM_tienvattu, 0 as doanhthuTongGM_tienvattu_ttrieng, 0 as doanhthuTongGM_tienmau, 0 as doanhthuTongGM_tienthuoc, 0 as doanhthuTongGM_tongtien, 0 as doanhthu_chiphixn, 0 as doanhthu_chiphicdha, 0 as doanhthu_chiphikhoa;";
                    dataDoanhThu = new DataView(condb.GetDataTable_HIS(sqlDoanhThu_null));
                }

                List<DataView> dataBC = new List<DataView>();
                dataBC.Add(dataBNRaVienChuyenDiChuyenDen);
                dataBC.Add(dataBCTongTheKhoa_DangDT);
                dataBC.Add(dataBCTongTheKhoa_RaVienChuaTT);
                dataBC.Add(dataBCTongTheKhoa_RaVienDaTT);
                dataBC.Add(dataDoanhThu);
                HienThiDuLieu(dataBC);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #region Hien thi du lieu
        private void HienThiDuLieu(List<DataView> dataBC)
        {
            try
            {
                dataBCQLTongTheKhoaSDO = new List<BCDashboardQLTongTheKhoa>();
                BCDashboardQLTongTheKhoa dataRow_1 = new BCDashboardQLTongTheKhoa();
                dataRow_1.BNDangDT_stt = 1;
                dataRow_1.BNDangDT_name = "SL bệnh nhân hiện diện";
                dataRow_1.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_slbn"].ToString()), 0);

                BCDashboardQLTongTheKhoa dataRow_2 = new BCDashboardQLTongTheKhoa();
                dataRow_2.BNDangDT_stt = 2;
                dataRow_2.BNDangDT_name = "SL bệnh nhân chuyển đi";
                dataRow_2.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[0][0]["ravien_slbn"].ToString()), 0);

                BCDashboardQLTongTheKhoa dataRow_3 = new BCDashboardQLTongTheKhoa();
                dataRow_3.BNDangDT_stt = 3;
                dataRow_3.BNDangDT_name = "SL bệnh nhân chuyển đến";
                dataRow_3.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[0][1]["ravien_slbn"].ToString()), 0);

                BCDashboardQLTongTheKhoa dataRow_4 = new BCDashboardQLTongTheKhoa();
                dataRow_4.BNDangDT_stt = 4;
                dataRow_4.BNDangDT_name = "SL bệnh nhân ra viện";
                dataRow_4.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[0][2]["ravien_slbn"].ToString()), 0);
                dataRow_4.RaVienChuaTT_name = "Số lượng";
                dataRow_4.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_slbn"].ToString()), 0);
                dataRow_4.DaTT_name = "Số lượng";
                dataRow_4.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_slbn"].ToString()), 0);
                dataRow_4.DoanhThu_name = "Số lượng";
                dataRow_4.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_slbn"].ToString()), 0);
                dataRow_4.DoanhThuGM_name = "Số lượng";
                dataRow_4.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_slbn"].ToString()), 0);
                dataRow_4.DoanhThuTongGM_name = "Số lượng";
                dataRow_4.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_slbn"].ToString()), 0);

                BCDashboardQLTongTheKhoa dataRow_5 = new BCDashboardQLTongTheKhoa();
                dataRow_5.BNDangDT_stt = 5;
                dataRow_5.BNDangDT_name = "Tổng tiền";
                dataRow_5.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tongtien"].ToString()), 0) + " đ";
                dataRow_5.RaVienChuaTT_name = "Tổng tiền";
                dataRow_5.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"].ToString()), 0) + " đ";
                dataRow_5.DaTT_name = "Tổng tiền";
                dataRow_5.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tongtien"].ToString()), 0) + " đ";
                dataRow_5.DoanhThu_name = "Tổng tiền";
                dataRow_5.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tongtien"].ToString()), 0) + " đ";
                dataRow_5.DoanhThuGM_name = "Tổng tiền";
                dataRow_5.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tongtien"].ToString()), 0) + " đ";
                dataRow_5.DoanhThuTongGM_name = "Tổng tiền";
                dataRow_5.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tongtien"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_6 = new BCDashboardQLTongTheKhoa();
                dataRow_6.BNDangDT_stt = 6;
                dataRow_6.BNDangDT_name = "Khám bệnh";
                dataRow_6.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienkb"].ToString()), 0) + " đ";
                dataRow_6.RaVienChuaTT_name = "Khám bệnh";
                dataRow_6.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienkb"].ToString()), 0) + " đ";
                dataRow_6.DaTT_name = "Khám bệnh";
                dataRow_6.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienkb"].ToString()), 0) + " đ";
                dataRow_6.DoanhThu_name = "Khám bệnh";
                dataRow_6.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienkb"].ToString()), 0) + " đ";
                dataRow_6.DoanhThuGM_name = "Khám bệnh";
                dataRow_6.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienkb"].ToString()), 0) + " đ";
                dataRow_6.DoanhThuTongGM_name = "Khám bệnh";
                dataRow_6.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienkb"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_7 = new BCDashboardQLTongTheKhoa();
                dataRow_7.BNDangDT_stt = 7;
                dataRow_7.BNDangDT_name = "Xét nghiệm";
                dataRow_7.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienxn"].ToString()), 0) + " đ";
                dataRow_7.RaVienChuaTT_name = "Xét nghiệm";
                dataRow_7.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienxn"].ToString()), 0) + " đ";
                dataRow_7.DaTT_name = "Xét nghiệm";
                dataRow_7.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienxn"].ToString()), 0) + " đ";
                dataRow_7.DoanhThu_name = "Xét nghiệm";
                dataRow_7.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienxn"].ToString()), 0) + " đ";
                dataRow_7.DoanhThuGM_name = "Xét nghiệm";
                dataRow_7.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienxn"].ToString()), 0) + " đ";
                dataRow_7.DoanhThuTongGM_name = "Xét nghiệm";
                dataRow_7.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienxn"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_8 = new BCDashboardQLTongTheKhoa();
                dataRow_8.BNDangDT_stt = 8;
                dataRow_8.BNDangDT_name = "CĐHA-TDCN";
                dataRow_8.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tiencdhatdcn"].ToString()), 0) + " đ";
                dataRow_8.RaVienChuaTT_name = "CĐHA-TDCN";
                dataRow_8.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tiencdhatdcn"].ToString()), 0) + " đ";
                dataRow_8.DaTT_name = "CĐHA-TDCN";
                dataRow_8.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tiencdhatdcn"].ToString()), 0) + " đ";
                dataRow_8.DoanhThu_name = "CĐHA-TDCN";
                dataRow_8.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tiencdhatdcn"].ToString()), 0) + " đ";
                dataRow_8.DoanhThuGM_name = "CĐHA-TDCN";
                dataRow_8.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tiencdhatdcn"].ToString()), 0) + " đ";
                dataRow_8.DoanhThuTongGM_name = "CĐHA-TDCN";
                dataRow_8.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tiencdhatdcn"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_9 = new BCDashboardQLTongTheKhoa();
                dataRow_9.BNDangDT_stt = 9;
                dataRow_9.BNDangDT_name = "PTTT";
                dataRow_9.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienpttt"].ToString()), 0) + " đ";
                dataRow_9.RaVienChuaTT_name = "PTTT";
                dataRow_9.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienpttt"].ToString()), 0) + " đ";
                dataRow_9.DaTT_name = "PTTT";
                dataRow_9.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienpttt"].ToString()), 0) + " đ";
                dataRow_9.DoanhThu_name = "PTTT";
                dataRow_9.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienpttt"].ToString()), 0) + " đ";
                dataRow_9.DoanhThuGM_name = "PTTT";
                dataRow_9.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienpttt"].ToString()), 0) + " đ";
                dataRow_9.DoanhThuTongGM_name = "PTTT";
                dataRow_9.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienpttt"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_10 = new BCDashboardQLTongTheKhoa();
                dataRow_10.BNDangDT_stt = 10;
                dataRow_10.BNDangDT_name = "DV KTC";
                dataRow_10.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tiendvktc"].ToString()), 0) + " đ";
                dataRow_10.RaVienChuaTT_name = "DV KTC";
                dataRow_10.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tiendvktc"].ToString()), 0) + " đ";
                dataRow_10.DaTT_name = "DV KTC";
                dataRow_10.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tiendvktc"].ToString()), 0) + " đ";
                dataRow_10.DoanhThu_name = "DV KTC";
                dataRow_10.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tiendvktc"].ToString()), 0) + " đ";
                dataRow_10.DoanhThuGM_name = "DV KTC";
                dataRow_10.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tiendvktc"].ToString()), 0) + " đ";
                dataRow_10.DoanhThuTongGM_name = "DV KTC";
                dataRow_10.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tiendvktc"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_11 = new BCDashboardQLTongTheKhoa();
                dataRow_11.BNDangDT_stt = 11;
                dataRow_11.BNDangDT_name = "Giường thường";
                dataRow_11.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tiengiuongthuong"].ToString()), 0) + " đ";
                dataRow_11.RaVienChuaTT_name = "Giường thường";
                dataRow_11.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tiengiuongthuong"].ToString()), 0) + " đ";
                dataRow_11.DaTT_name = "Giường thường";
                dataRow_11.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tiengiuongthuong"].ToString()), 0) + " đ";
                dataRow_11.DoanhThu_name = "Giường thường";
                dataRow_11.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tiengiuongthuong"].ToString()), 0) + " đ";
                dataRow_11.DoanhThuGM_name = "Giường thường";
                dataRow_11.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tiengiuongthuong"].ToString()), 0) + " đ";
                dataRow_11.DoanhThuTongGM_name = "Giường thường";
                dataRow_11.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tiengiuongthuong"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_12 = new BCDashboardQLTongTheKhoa();
                dataRow_12.BNDangDT_stt = 12;
                dataRow_12.BNDangDT_name = "Giường yêu cầu";
                dataRow_12.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tiengiuongyeucau"].ToString()), 0) + " đ";
                dataRow_12.RaVienChuaTT_name = "Giường yêu cầu";
                dataRow_12.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tiengiuongyeucau"].ToString()), 0) + " đ";
                dataRow_12.DaTT_name = "Giường yêu cầu";
                dataRow_12.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tiengiuongyeucau"].ToString()), 0) + " đ";
                dataRow_12.DoanhThu_name = "Giường yêu cầu";
                dataRow_12.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tiengiuongyeucau"].ToString()), 0) + " đ";
                dataRow_12.DoanhThuGM_name = "Giường yêu cầu";
                dataRow_12.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tiengiuongyeucau"].ToString()), 0) + " đ";
                dataRow_12.DoanhThuTongGM_name = "Giường yêu cầu";
                dataRow_12.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tiengiuongyeucau"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_13 = new BCDashboardQLTongTheKhoa();
                dataRow_13.BNDangDT_stt = 13;
                dataRow_13.BNDangDT_name = "DV khác";
                dataRow_13.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienkhac"].ToString()), 0) + " đ";
                dataRow_13.RaVienChuaTT_name = "DV khác";
                dataRow_13.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienkhac"].ToString()), 0) + " đ";
                dataRow_13.DaTT_name = "DV khác";
                dataRow_13.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienkhac"].ToString()), 0) + " đ";
                dataRow_13.DoanhThu_name = "DV khác";
                dataRow_13.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienkhac"].ToString()), 0) + " đ";
                dataRow_13.DoanhThuGM_name = "DV khác";
                dataRow_13.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienkhac"].ToString()), 0) + " đ";
                dataRow_13.DoanhThuTongGM_name = "DV khác";
                dataRow_13.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienkhac"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_14 = new BCDashboardQLTongTheKhoa();
                dataRow_14.BNDangDT_stt = 14;
                dataRow_14.BNDangDT_name = "Máu, chế phẩm";
                dataRow_14.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienmau"].ToString()), 0) + " đ";
                dataRow_14.RaVienChuaTT_name = "Máu, chế phẩm";
                dataRow_14.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienmau"].ToString()), 0) + " đ";
                dataRow_14.DaTT_name = "Máu, chế phẩm";
                dataRow_14.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienmau"].ToString()), 0) + " đ";
                dataRow_14.DoanhThu_name = "Máu, chế phẩm";
                dataRow_14.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienmau"].ToString()), 0) + " đ";
                dataRow_14.DoanhThuGM_name = "Máu, chế phẩm";
                dataRow_14.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienmau"].ToString()), 0) + " đ";
                dataRow_14.DoanhThuTongGM_name = "Máu, chế phẩm";
                dataRow_14.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienmau"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_15 = new BCDashboardQLTongTheKhoa();
                dataRow_15.BNDangDT_stt = 15;
                dataRow_15.BNDangDT_name = "Vật tư";
                dataRow_15.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienvattu"].ToString()), 0) + " đ";
                dataRow_15.RaVienChuaTT_name = "Vật tư";
                dataRow_15.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienvattu"].ToString()), 0) + " đ";
                dataRow_15.DaTT_name = "Vật tư";
                dataRow_15.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienvattu"].ToString()), 0) + " đ";
                dataRow_15.DoanhThu_name = "Vật tư";
                dataRow_15.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienvattu"].ToString()), 0) + " đ";
                dataRow_15.DoanhThuGM_name = "Vật tư";
                dataRow_15.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienvattu"].ToString()), 0) + " đ";
                dataRow_15.DoanhThuTongGM_name = "Vật tư";
                dataRow_15.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienvattu"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_16 = new BCDashboardQLTongTheKhoa();
                dataRow_16.BNDangDT_stt = 16;
                dataRow_16.BNDangDT_name = "Thuốc";
                dataRow_16.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienthuoc"].ToString()), 0) + " đ";
                dataRow_16.RaVienChuaTT_name = "Thuốc";
                dataRow_16.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienthuoc"].ToString()), 0) + " đ";
                dataRow_16.DaTT_name = "Thuốc";
                dataRow_16.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienthuoc"].ToString()), 0) + " đ";
                dataRow_16.DoanhThu_name = "Thuốc";
                dataRow_16.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienthuoc"].ToString()), 0) + " đ";
                dataRow_16.DoanhThuGM_name = "Thuốc";
                dataRow_16.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienthuoc"].ToString()), 0) + " đ";
                dataRow_16.DoanhThuTongGM_name = "Thuốc";
                dataRow_16.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienthuoc"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_17 = new BCDashboardQLTongTheKhoa();
                dataRow_17.BNDangDT_stt = 17;
                dataRow_17.BNDangDT_name = "Tỷ lệ thuốc";
                dataRow_17.BNDangDT_value = "0 %";
                if (Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tongtien"].ToString()) != 0)
                {
                    dataRow_17.BNDangDT_value = Math.Round(((Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienthuoc"].ToString()) / Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tongtien"].ToString())) * 100), 1).ToString() + " %";
                }
                dataRow_17.RaVienChuaTT_name = "Tỷ lệ thuốc";
                dataRow_17.RaVienChuaTT_value = "0 %";
                if (Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"].ToString()) != 0)
                {
                    dataRow_17.RaVienChuaTT_value = Math.Round(((Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienthuoc"].ToString()) / Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"].ToString())) * 100), 1).ToString() + " %";
                }
                dataRow_17.DaTT_name = "Tỷ lệ thuốc";
                dataRow_17.DaTT_value = "0 %";
                if (Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tongtien"].ToString()) != 0)
                {
                    dataRow_17.DaTT_value = Math.Round(((Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienthuoc"].ToString()) / Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tongtien"].ToString())) * 100), 1).ToString() + " %";
                }
                dataRow_17.DoanhThu_name = "Tỷ lệ thuốc";
                dataRow_17.DoanhThu_value = "0 %";
                if (Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tongtien"].ToString()) != 0)
                {
                    dataRow_17.DoanhThu_value = Math.Round(((Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienthuoc"].ToString()) / Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tongtien"].ToString())) * 100), 1).ToString() + " %";
                }
                dataRow_17.DoanhThuGM_name = "Tỷ lệ thuốc";
                dataRow_17.DoanhThuGM_value = "0 %";
                if (Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tongtien"].ToString()) != 0)
                {
                    dataRow_17.DoanhThuGM_value = Math.Round(((Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienthuoc"].ToString()) / Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tongtien"].ToString())) * 100), 1).ToString() + " %";
                }
                dataRow_17.DoanhThuTongGM_name = "Tỷ lệ thuốc";
                dataRow_17.DoanhThuTongGM_value = "0 %";
                if (Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tongtien"].ToString()) != 0)
                {
                    dataRow_17.DoanhThuTongGM_value = Math.Round(((Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienthuoc"].ToString()) / Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tongtien"].ToString())) * 100), 1).ToString() + " %";
                }

                BCDashboardQLTongTheKhoa dataRow_18 = new BCDashboardQLTongTheKhoa();
                dataRow_18.BNDangDT_stt = 18;
                dataRow_18.BNDangDT_name = "Tạm ứng";
                dataRow_18.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tamung"].ToString()), 0) + " đ";
                //dataRow_18.RaVienChuaTT_name = "Tạm ứng";
                //dataRow_18.RaVienChuaTT_value = "0";
                //dataRow_18.DaTT_name = "Tạm ứng";
                //dataRow_18.DaTT_value = "0";
                //dataRow_18.DoanhThu_name = "Tạm ứng";
                //dataRow_18.DoanhThu_value = "0";
                //dataRow_18.DoanhThuGM_name = "Tạm ứng";
                //dataRow_18.DoanhThuGM_value = "0";
                //dataRow_18.DoanhThuTongGM_name = "Tạm ứng";
                //dataRow_18.DoanhThuTongGM_value = "0";

                BCDashboardQLTongTheKhoa dataRow_19 = new BCDashboardQLTongTheKhoa();
                dataRow_19.BNDangDT_stt = 19;
                dataRow_19.BNDangDT_name = "PTTT yêu cầu";
                dataRow_19.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienptttyeucau"].ToString()), 0) + " đ";
                dataRow_19.RaVienChuaTT_name = "PTTT yêu cầu";
                dataRow_19.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienptttyeucau"].ToString()), 0) + " đ";
                dataRow_19.DaTT_name = "PTTT yêu cầu";
                dataRow_19.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienptttyeucau"].ToString()), 0) + " đ";
                dataRow_19.DoanhThu_name = "PTTT yêu cầu";
                dataRow_19.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienptttyeucau"].ToString()), 0) + " đ";
                dataRow_19.DoanhThuGM_name = "PTTT yêu cầu";
                dataRow_19.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienptttyeucau"].ToString()), 0) + " đ";
                dataRow_19.DoanhThuTongGM_name = "PTTT yêu cầu";
                dataRow_19.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienptttyeucau"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_20 = new BCDashboardQLTongTheKhoa();
                dataRow_20.BNDangDT_stt = 20;
                dataRow_20.BNDangDT_name = "VT TT riêng";
                dataRow_20.BNDangDT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[1][0]["dangdt_tienvattu_ttrieng"].ToString()), 0) + " đ";
                dataRow_20.RaVienChuaTT_name = "VT TT riêng";
                dataRow_20.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[2][0]["ravienchuatt_tienvattu_ttrieng"].ToString()), 0) + " đ";
                dataRow_20.DaTT_name = "VT TT riêng";
                dataRow_20.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_tienvattu_ttrieng"].ToString()), 0) + " đ";
                dataRow_20.DoanhThu_name = "VT TT riêng";
                dataRow_20.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_tienvattu_ttrieng"].ToString()), 0) + " đ";
                dataRow_20.DoanhThuGM_name = "VT TT riêng";
                dataRow_20.DoanhThuGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthugm_tienvattu_ttrieng"].ToString()), 0) + " đ";
                dataRow_20.DoanhThuTongGM_name = "VT TT riêng";
                dataRow_20.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthutonggm_tienvattu_ttrieng"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_21 = new BCDashboardQLTongTheKhoa();
                dataRow_21.BNDangDT_stt = 21;
                //dataRow_21.BNDangDT_name = "Chi phí Xét nghiệm";
                //dataRow_21.BNDangDT_value = "0";
                //dataRow_21.RaVienChuaTT_name = "Chi phí Xét nghiệm";
                //dataRow_21.RaVienChuaTT_value = "0";
                dataRow_21.DaTT_name = "Chi phí Xét nghiệm";
                dataRow_21.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_chiphixn"].ToString()), 0) + " đ";
                dataRow_21.DoanhThu_name = "Chi phí Xét nghiệm";
                dataRow_21.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_chiphixn"].ToString()), 0) + " đ";
                //dataRow_21.DoanhThuGM_name = "Chi phí Xét nghiệm";
                //dataRow_21.DoanhThuGM_value = "0";
                dataRow_21.DoanhThuTongGM_name = "Chi phí Xét nghiệm";
                dataRow_21.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_chiphixn"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_22 = new BCDashboardQLTongTheKhoa();
                dataRow_22.BNDangDT_stt = 22;
                //dataRow_22.BNDangDT_name = "Chi phí CĐHA";
                //dataRow_22.BNDangDT_value = "0";
                //dataRow_22.RaVienChuaTT_name = "Chi phí CĐHA";
                //dataRow_22.RaVienChuaTT_value = "0";
                dataRow_22.DaTT_name = "Chi phí CĐHA";
                dataRow_22.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_chiphicdha"].ToString()), 0) + " đ";
                dataRow_22.DoanhThu_name = "Chi phí CĐHA";
                dataRow_22.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_chiphicdha"].ToString()), 0) + " đ";
                //dataRow_22.DoanhThuGM_name = "Chi phí CĐHA";
                //dataRow_22.DoanhThuGM_value = "0";
                dataRow_22.DoanhThuTongGM_name = "Chi phí CĐHA";
                dataRow_22.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_chiphicdha"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_23 = new BCDashboardQLTongTheKhoa();
                dataRow_23.BNDangDT_stt = 23;
                //dataRow_23.BNDangDT_name = "Chi phí khoa/phòng";
                //dataRow_23.BNDangDT_value = "0";
                //dataRow_23.RaVienChuaTT_name = "Chi phí khoa/phòng";
                //dataRow_23.RaVienChuaTT_value = "0";
                dataRow_23.DaTT_name = "Chi phí khoa/phòng";
                dataRow_23.DaTT_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[3][0]["raviendatt_chiphikhoa"].ToString()), 0) + " đ";
                dataRow_23.DoanhThu_name = "Chi phí khoa/phòng";
                dataRow_23.DoanhThu_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_chiphikhoa"].ToString()), 0) + " đ";
                //dataRow_23.DoanhThuGM_name = "Chi phí khoa/phòng";
                //dataRow_23.DoanhThuGM_value = "0";
                dataRow_23.DoanhThuTongGM_name = "Chi phí khoa/phòng";
                dataRow_23.DoanhThuTongGM_value = Util_NumberConvert.NumberToString(Utilities.Util_TypeConvertParse.ToDecimal(dataBC[4][0]["doanhthu_chiphikhoa"].ToString()), 0) + " đ";


                dataBCQLTongTheKhoaSDO.Add(dataRow_1);
                dataBCQLTongTheKhoaSDO.Add(dataRow_2);
                dataBCQLTongTheKhoaSDO.Add(dataRow_3);
                dataBCQLTongTheKhoaSDO.Add(dataRow_4);
                dataBCQLTongTheKhoaSDO.Add(dataRow_5);
                dataBCQLTongTheKhoaSDO.Add(dataRow_6);
                dataBCQLTongTheKhoaSDO.Add(dataRow_7);
                dataBCQLTongTheKhoaSDO.Add(dataRow_8);
                dataBCQLTongTheKhoaSDO.Add(dataRow_9);
                dataBCQLTongTheKhoaSDO.Add(dataRow_10);
                dataBCQLTongTheKhoaSDO.Add(dataRow_11);
                dataBCQLTongTheKhoaSDO.Add(dataRow_12);
                dataBCQLTongTheKhoaSDO.Add(dataRow_13);
                dataBCQLTongTheKhoaSDO.Add(dataRow_14);
                dataBCQLTongTheKhoaSDO.Add(dataRow_15);
                dataBCQLTongTheKhoaSDO.Add(dataRow_16);
                dataBCQLTongTheKhoaSDO.Add(dataRow_17);
                dataBCQLTongTheKhoaSDO.Add(dataRow_18);
                dataBCQLTongTheKhoaSDO.Add(dataRow_19);
                dataBCQLTongTheKhoaSDO.Add(dataRow_20);
                dataBCQLTongTheKhoaSDO.Add(dataRow_21);
                dataBCQLTongTheKhoaSDO.Add(dataRow_22);
                dataBCQLTongTheKhoaSDO.Add(dataRow_23);

                gridControlDataQLTTKhoa.DataSource = null;
                gridControlDataQLTTKhoa.DataSource = dataBCQLTongTheKhoaSDO;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

    }
}
