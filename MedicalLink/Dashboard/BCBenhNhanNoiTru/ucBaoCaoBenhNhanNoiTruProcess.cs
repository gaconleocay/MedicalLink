using DevExpress.XtraSplashScreen;
using MedicalLink.TimerService;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoBenhNhanNoiTru : UserControl
    {
        //internal void LayDuLieuBaoCaoKhiClick(string thoiGianTu, string thoiGianDen)
        //{
        //    try
        //    {
        //        lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

        //        string sqlBaoCao = "SELECT A.departmentgroupid, A.departmentgroupcode, A.departmentgroupname, A.dangdt_slbn_bhyt, A.dangdt_slbn_nhandan, (A.dangdt_slbn_bhyt + A.dangdt_slbn_nhandan) as dangdt_slbn_tong, A.dangdt_tamung, A.ravienchuatt_slbn, A.ravienchuatt_tongtien, A.ravienchuatt_tamung, A.dathanhtoan_sotien_bhyt, A.dathanhtoan_sotien_nhandan, A.dathanhtoan_sotien_tong, A.dathanhtoan_thuoc_bhyt, A.dathanhtoan_thuoc_nhandan, A.dathanhtoan_thuoc_tong, case when A.dathanhtoan_sotien_bhyt<>0 then round((A.dathanhtoan_thuoc_bhyt/dathanhtoan_sotien_bhyt) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_bhyt, case when A.dathanhtoan_sotien_nhandan<>0 then round((A.dathanhtoan_thuoc_nhandan/dathanhtoan_sotien_nhandan) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_nhandan, case when A.dathanhtoan_sotien_tong<>0 then round((A.dathanhtoan_thuoc_tong/dathanhtoan_sotien_tong) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_tong FROM (SELECT degr.departmentgroupid, degr.departmentgroupcode, degr.departmentgroupname, sum(case when vp.doituongbenhnhanid =1 and vp.vienphistatus=0 then 1 else 0 end) as dangdt_slbn_bhyt, sum(case when vp.doituongbenhnhanid <>1 and vp.vienphistatus=0 then 1 else 0 end) as dangdt_slbn_nhandan, sum(case when vp.vienphistatus=0 then (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0 ) else 0 end) as dangdt_tamung, sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then 1 else 0 end) as ravienchuatt_slbn, round(cast(sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) +  sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravienchuatt_tongtien, sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0 ) else 0 end) as ravienchuatt_tamung, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_bhyt) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) + sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_bhyt) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid)  else 0 end) as numeric),0) as dathanhtoan_sotien_bhyt, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_nhandan) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) +  sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_nhandan) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_sotien_nhandan, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) + sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_sotien_tong, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_bhyt) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_bhyt, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_nhandan) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_nhandan, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_tong FROM departmentgroup degr inner join vienphi vp on degr.departmentgroupid=vp.departmentgroupid WHERE degr.departmentgrouptype in (4) and vp.loaivienphiid=0 and vp.vienphidate >='" + thoiGianTu + "' and vp.vienphidate <='" + thoiGianDen + "' GROUP BY degr.departmentgroupid) A ORDER BY A.departmentgroupname ;";
        //        DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
        //        HienThiDuLieuBaoCao(dataBCTongTheKhoa);
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}

        //internal void LayDuLieuBaoCaoTuDongCapNhat(decimal thoiGianCapNhat)
        //{
        //    try
        //    {
        //        thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //        thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
        //        lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        //        string sqlBaoCao = "SELECT A.departmentgroupid, A.departmentgroupcode, A.departmentgroupname, A.dangdt_slbn_bhyt, A.dangdt_slbn_nhandan, (A.dangdt_slbn_bhyt + A.dangdt_slbn_nhandan) as dangdt_slbn_tong, A.dangdt_tamung, A.ravienchuatt_slbn, A.ravienchuatt_tongtien, A.ravienchuatt_tamung, A.dathanhtoan_sotien_bhyt, A.dathanhtoan_sotien_nhandan, A.dathanhtoan_sotien_tong, A.dathanhtoan_thuoc_bhyt, A.dathanhtoan_thuoc_nhandan, A.dathanhtoan_thuoc_tong, case when A.dathanhtoan_sotien_bhyt<>0 then round((A.dathanhtoan_thuoc_bhyt/dathanhtoan_sotien_bhyt) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_bhyt, case when A.dathanhtoan_sotien_nhandan<>0 then round((A.dathanhtoan_thuoc_nhandan/dathanhtoan_sotien_nhandan) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_nhandan, case when A.dathanhtoan_sotien_tong<>0 then round((A.dathanhtoan_thuoc_tong/dathanhtoan_sotien_tong) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_tong FROM (SELECT degr.departmentgroupid, degr.departmentgroupcode, degr.departmentgroupname, sum(case when vp.doituongbenhnhanid =1 and vp.vienphistatus=0 then 1 else 0 end) as dangdt_slbn_bhyt, sum(case when vp.doituongbenhnhanid <>1 and vp.vienphistatus=0 then 1 else 0 end) as dangdt_slbn_nhandan, sum(case when vp.vienphistatus=0 then (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0 ) else 0 end) as dangdt_tamung, sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then 1 else 0 end) as ravienchuatt_slbn, round(cast(sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) +  sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravienchuatt_tongtien, sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0 ) else 0 end) as ravienchuatt_tamung, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_bhyt) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) + sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_bhyt) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid)  else 0 end) as numeric),0) as dathanhtoan_sotien_bhyt, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_nhandan) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) +  sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_nhandan) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_sotien_nhandan, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) + sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_sotien_tong, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_bhyt) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_bhyt, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_nhandan) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_nhandan, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_tong FROM departmentgroup degr inner join vienphi vp on degr.departmentgroupid=vp.departmentgroupid WHERE degr.departmentgrouptype in (4) and vp.loaivienphiid=0 and vp.vienphidate >='" + thoiGianTu + "' and vp.vienphidate <='" + thoiGianDen + "' GROUP BY degr.departmentgroupid) A ORDER BY A.departmentgroupname ;";
        //        DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
        //        HienThiDuLieuBaoCao(dataBCTongTheKhoa);
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}

        internal void LayDuLieuBaoCao()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                // Lấy từ ngày, đến ngày
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                TimerServiceProcess.SQLKiemTraVaUpdateTableTmp();
                SQLLayDuLieuBaoCao(thoiGianTu, thoiGianDen);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void SQLLayDuLieuBaoCao(string thoigiantu, string thoigianden)
        {
            try
            {

                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao = "SELECT A.departmentgroupid, A.departmentgroupcode, A.departmentgroupname, A.dangdt_slbn_bhyt, A.dangdt_slbn_nhandan, (A.dangdt_slbn_bhyt + A.dangdt_slbn_nhandan) as dangdt_slbn_tong, A.dangdt_tamung, A.ravienchuatt_slbn, A.ravienchuatt_tongtien, A.ravienchuatt_tamung, A.dathanhtoan_sotien_bhyt, A.dathanhtoan_sotien_nhandan, A.dathanhtoan_sotien_tong, A.dathanhtoan_thuoc_bhyt, A.dathanhtoan_thuoc_nhandan, A.dathanhtoan_thuoc_tong, case when A.dathanhtoan_sotien_bhyt<>0 then round((A.dathanhtoan_thuoc_bhyt/dathanhtoan_sotien_bhyt) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_bhyt, case when A.dathanhtoan_sotien_nhandan<>0 then round((A.dathanhtoan_thuoc_nhandan/dathanhtoan_sotien_nhandan) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_nhandan, case when A.dathanhtoan_sotien_tong<>0 then round((A.dathanhtoan_thuoc_tong/dathanhtoan_sotien_tong) * 100,2) else 0 end as dathanhtoan_tyle_thuoc_tong FROM (SELECT degr.departmentgroupid, degr.departmentgroupcode, degr.departmentgroupname, (select count(*) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=degr.departmentgroupid and tmp.doituongbenhnhanid=1) as dangdt_slbn_bhyt, (select count(*) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=degr.departmentgroupid and tmp.doituongbenhnhanid<>1)  as dangdt_slbn_nhandan, round(cast((select sum(tmp.tam_ung) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=degr.departmentgroupid) as numeric),0) as dangdt_tamung, sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then 1 else 0 end) as ravienchuatt_slbn, round(cast(sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) +  sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravienchuatt_tongtien, sum(case when vp.vienphistatus<>0 and vp.vienphistatus_vp=0 or vp.vienphistatus_vp is null then (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0 ) else 0 end) as ravienchuatt_tamung, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_bhyt) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) + sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_bhyt) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid)  else 0 end) as numeric),0) as dathanhtoan_sotien_bhyt, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_nhandan) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) +  sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_nhandan) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_sotien_nhandan, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) + sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_sotien_tong, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_bhyt) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_bhyt, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_nhandan) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_nhandan, round(cast(sum(case when vp.vienphistatus_vp=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as dathanhtoan_thuoc_tong FROM departmentgroup degr inner join vienphi vp on degr.departmentgroupid=vp.departmentgroupid WHERE degr.departmentgrouptype in (4) and vp.loaivienphiid=0 and vp.vienphidate >='" + thoiGianTu + "' and vp.vienphidate <='" + thoiGianDen + "' GROUP BY degr.departmentgroupid) A ORDER BY A.departmentgroupname ;";
                DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
                HienThiDuLieuBaoCao(dataBCTongTheKhoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void HienThiDuLieuBaoCao(DataView dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Count > 0)
                {
                    dataBCTongTheKhoa = new DataView();
                    dataBCTongTheKhoa = dataBC;
                    gridControlDataBNNT.DataSource = dataBCTongTheKhoa;
                }
                else
                {
                    dataBCTongTheKhoa = null;
                    gridControlDataBNNT.DataSource = null;
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
