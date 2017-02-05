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
    public partial class ucBaoCaoTongTheKhoa : UserControl
    {
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

                string sqlBaoCao = "SELECT vp.departmentgroupid, (select count(*) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=vp.departmentgroupid) as dang_dt_slbn, sum(case when vp.vienphistatus=0 then (select count(mrd.*) from medicalrecord mrd where mrd.vienphiid=vp.vienphiid and mrd.departmentgroupid=vp.departmentgroupid and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + thoigiantu + "' and mrd.thoigianravien<='" + thoigianden + "') else 0 end) as bn_chuyendi, sum(case when vp.vienphistatus=0 then (select count(mrd.*) from medicalrecord mrd where mrd.vienphiid=vp.vienphiid and mrd.departmentgroupid=vp.departmentgroupid and mrd.hinhthucvaovienid=3 and mrd.thoigianvaovien>='" + thoigiantu + "' and mrd.thoigianvaovien<='" + thoigianden + "') else 0 end) as bn_chuyenden, sum(case when vp.vienphidate_ravien>='" + thoigiantu + "' and vp.vienphidate_ravien<='" + thoigianden + "' then 1 else 0 end) as ravien_slbn, round(cast((select sum(tmp.serviceprice_dichvu) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=vp.departmentgroupid) as numeric),0) as dang_dt_tiendv, round(cast((select sum(tmp.serviceprice_thuoc) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=vp.departmentgroupid) as numeric),0) as dang_dt_tienthuoc, round(cast((select sum(tmp.tam_ung) from tools_bndangdt_tmp tmp where tmp.departmentgroupid=vp.departmentgroupid) as numeric),0) as dang_dt_tamung,	sum(case when COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 then 1 else 0 end) as ravien_chuatt_slbn, round(cast(sum(case when COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravien_chuatt_tiendv,	round(cast(sum(case when COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravien_chuatt_tienthuoc, sum(case COALESCE(vp.vienphistatus_vp,0) when 1 then 1 else 0 end) as ravien_datt_slbn, round(cast(sum(case when COALESCE(vp.vienphistatus_vp,0)=1 then (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravien_datt_tiendv, round(cast(sum(case when COALESCE(vp.vienphistatus_vp,0)=1 then (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) else 0 end) as numeric),0) as ravien_datt_tienthuoc, TIMESTAMP 'now' as bctongthekhoaid_date FROM vienphi vp WHERE vp.loaivienphiid=0 and vp.vienphidate >= '" + thoigiantu + "' and vp.vienphidate <= '" + thoigianden + "' and vp.departmentgroupid = '" + Convert.ToInt16(cboKhoa.EditValue) + "' GROUP BY vp.departmentgroupid;";
                DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
                HienThiDuLieuBaoCao(dataBCTongTheKhoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void HienThiDuLieuBaoCao(DataView dataBCTongTheKhoa)
        {
            try
            {
                if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
                {
                    lblBNHienDien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_slbn"]), 0);
                    lblBNChuyenDi.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["bn_chuyendi"]), 0);
                    lblBNChuyenDen.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["bn_chuyenden"]), 0);
                    lblBNRaVien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_slbn"]), 0);
                    lblDangDTSoTien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tienthuoc"]), 0);
                    lblDangDTThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tienthuoc"]), 0);
                    if ((Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tienthuoc"])) != 0)
                    {
                        lblDangDTTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tienthuoc"]) * 100 / (Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tienthuoc"]))), 2));
                    }
                    else
                    {
                        lblDangDTTyLeThuoc.Text = "0";
                    }
                    lblDangDTTamUng.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dang_dt_tamung"]), 0);

                    lblDaRVSoLuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_slbn"]), 0);
                    lblDaRVDoanhThu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tienthuoc"])), 0);
                    lblDaRVThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tienthuoc"]), 0);
                    if ((Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tienthuoc"])) != 0)
                    {
                        lblDaRVTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tienthuoc"]) * 100 / (Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_chuatt_tienthuoc"]))), 2));
                    }
                    else
                    {
                        lblDaRVTyLeThuoc.Text = "0";
                    }

                    lblDaTTSoLuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_slbn"]), 0);
                    lblDaTTDoanhThu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tienthuoc"]), 0);
                    lblDaTTThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tienthuoc"]), 0);
                    if ((Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tienthuoc"])) != 0)
                    {
                        lblDaTTTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tienthuoc"]) * 100 / (Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tiendv"]) + Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_datt_tienthuoc"]))), 2));
                    }
                    else
                    {
                        lblDaTTTyLeThuoc.Text = "0";
                    }
                }
                else
                {
                    lblBNHienDien.Text = "0";
                    lblBNChuyenDi.Text = "0";
                    lblBNChuyenDen.Text = "0";
                    lblBNRaVien.Text = "0";
                    lblDangDTSoTien.Text = "0";
                    lblDangDTThuoc.Text = "0";
                    lblDangDTTyLeThuoc.Text = "0";
                    lblDangDTTamUng.Text = "0";

                    lblDaRVSoLuong.Text = "0";
                    lblDaRVDoanhThu.Text = "0";
                    lblDaRVThuoc.Text = "0";
                    lblDaRVTyLeThuoc.Text = "0";

                    lblDaTTSoLuong.Text = "0";
                    lblDaTTDoanhThu.Text = "0";
                    lblDaTTThuoc.Text = "0";
                    lblDaTTTyLeThuoc.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



    }
}
