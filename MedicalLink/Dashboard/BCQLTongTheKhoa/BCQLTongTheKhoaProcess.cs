using DevExpress.XtraSplashScreen;
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
                string lstdepartmentid = " and vpm.departmentid in (" + this.lstPhongChonLayBC + ")";
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                SQLLayDuLieuBaoCao(lstdepartmentid);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void SQLLayDuLieuBaoCao(string lstdepartmentid)
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao_DangDT = "SELECT count(vpm.*) as dangdt_slbn, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0),0) as dangdt_tienkb, COALESCE(round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0),0) as dangdt_tienxn, COALESCE(round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0),0) as dangdt_tiencdhatdcn, COALESCE(round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0),0) as dangdt_tienpttt, COALESCE(round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0),0) as dangdt_tiendvktc, COALESCE(round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0),0) as dangdt_tiengiuongthuong, COALESCE(round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0),0) as dangdt_tiengiuongyeucau, COALESCE(round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0),0) as dangdt_tienkhac, COALESCE(round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0),0) as dangdt_tienvattu, COALESCE(round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0),0) as dangdt_tienmau, COALESCE(round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0),0) as dangdt_tienthuoc, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0),0) as dangdt_tongtien, COALESCE(round(cast(sum(vpm.tam_ung) as numeric),0),0) as dangdt_tamung FROM vienphi_money vpm WHERE vpm.vienphistatus=0 " + lstdepartmentid + " and vpm.vienphidate>='" + this.KhoangThoiGianLayDuLieu + "';";

                string sqlBaoCao_RaVienChuaTT = "SELECT count(vpm.*) as ravienchuatt_slbn, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0),0) as ravienchuatt_tienkb,  COALESCE(round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0),0) as ravienchuatt_tienxn,  COALESCE(round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0),0) as ravienchuatt_tiencdhatdcn,  COALESCE(round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0),0) as ravienchuatt_tienpttt,  COALESCE(round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0),0) as ravienchuatt_tiendvktc,  COALESCE(round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0),0) as ravienchuatt_tiengiuongthuong,  COALESCE(round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0),0) as ravienchuatt_tiengiuongyeucau,  COALESCE(round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0),0) as ravienchuatt_tienkhac,  COALESCE(round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0),0) as ravienchuatt_tienvattu,  COALESCE(round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0),0) as ravienchuatt_tienmau,  COALESCE(round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0),0) as ravienchuatt_tienthuoc,  COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0),0) as ravienchuatt_tongtien,  COALESCE(round(cast(sum(vpm.tam_ung) as numeric),0),0) as ravienchuatt_tamung FROM vienphi_money vpm WHERE COALESCE(vpm.vienphistatus_vp,0)=0 " + lstdepartmentid + " and vpm.vienphistatus<>0 and vpm.vienphidate>='" + this.KhoangThoiGianLayDuLieu + "'; ";

                string sqlBaoCao_RaVienDaTT = "SELECT count(vpm.*) as raviendatt_slbn, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0),0) as raviendatt_tienkb, COALESCE(round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0),0) as raviendatt_tienxn, COALESCE(round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0),0) as raviendatt_tiencdhatdcn, COALESCE(round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0),0) as raviendatt_tienpttt, COALESCE(round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0),0) as raviendatt_tiendvktc, COALESCE(round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0),0) as raviendatt_tiengiuongthuong, COALESCE(round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0),0) as raviendatt_tiengiuongyeucau, COALESCE(round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0),0) as raviendatt_tienkhac, COALESCE(round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0),0) as raviendatt_tienvattu, COALESCE(round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0),0) as raviendatt_tienmau, COALESCE(round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0),0) as raviendatt_tienthuoc, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0),0) as raviendatt_tongtien, COALESCE(round(cast(sum(vpm.tam_ung) as numeric),0),0) as raviendatt_tamung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 " + lstdepartmentid + " and vpm.duyet_ngayduyet_vp >= '" + this.thoiGianTu + "' and vpm.duyet_ngayduyet_vp <= '" + this.thoiGianDen + "';";

                string sqlBNRaVienChuyenDiChuyenDen = "SELECT * FROM ((SELECT '3-BN ra vien' as name, count(vp.vienphiid) as ravien_slbn FROM vienphi vp WHERE vp.vienphistatus>0 and vp.vienphidate_ravien>='" + this.thoiGianTu + "' and vp.vienphidate_ravien<='" + this.thoiGianDen + "' and vp.departmentid in (" + this.lstPhongChonLayBC + ")) Union (SELECT '1-BN chuyen di' as name, count(A1.vienphiid) as bn_chuyendi FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd WHERE mrd.departmentid in (" + this.lstPhongChonLayBC + ") and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + this.thoiGianTu + "' and mrd.thoigianravien<='" + this.thoiGianDen + "') A1) Union (SELECT '2-BN chuyen den' as name, count(A2.vienphiid) as bn_chuyenden FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd WHERE mrd.departmentid in (" + this.lstPhongChonLayBC + ") and mrd.hinhthucvaovienid=3 and mrd.thoigianravien>='" + this.thoiGianTu + "' and mrd.thoigianravien<='" + this.thoiGianDen + "') A2)) O ORDER BY O.name;";

                string sqlDoanhThu = "SELECT 0 as departmentgroupid,0 as vienphiid, 0 as doanhthu_slbn_bh, 0 as doanhthu_slbn_vp, 0 as doanhthu_slbn, 0 as doanhthu_tienkb, 0 as doanhthu_tienxn, 0 as doanhthu_tiencdhatdcn, 0 as doanhthu_tienpttt, 0 as doanhthu_tiendvktc, 0 as doanhthu_tiengiuongthuong,0 as doanhthu_tiengiuongyeucau, 0 as doanhthu_tienkhac, 0 as doanhthu_tienvattu, 0 as doanhthu_tienmau, 0 as doanhthu_tienthuoc_bhyt,0 as doanhthu_tienthuoc_vp,0 as doanhthu_tienthuoc,0 as doanhthu_tongtien_bhyt,0 as doanhthu_tongtien_vp, 0 as doanhthu_tongtien from hospital limit 1;";

                DataView dataBCTongTheKhoa_DangDT = new DataView(condb.getDataTable(sqlBaoCao_DangDT));
                DataView dataBCTongTheKhoa_RaVienChuaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienChuaTT));
                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienDaTT));
                DataView dataBNRaVienChuyenDiChuyenDen = new DataView(condb.getDataTable(sqlBNRaVienChuyenDiChuyenDen));
                DataView dataDoanhThu = new DataView(condb.getDataTable(sqlDoanhThu));

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
                dataRow_1.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0);
                dataRow_1.BNDangDT_unit = "";
                dataRow_1.RaVienChuaTT_stt = 1;
                dataRow_1.RaVienChuaTT_name = "Số lượng";
                dataRow_1.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_slbn"]), 0);
                dataRow_1.RaVienChuaTT_unit = "";
                dataRow_1.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_slbn"]), 0);
                dataRow_1.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_slbn"]), 0);

                BCDashboardQLTongTheKhoa dataRow_2 = new BCDashboardQLTongTheKhoa();
                dataRow_2.BNDangDT_stt = 2;
                dataRow_2.BNDangDT_name = "SL bệnh nhân chuyển đi";
                dataRow_2.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[0][0]["ravien_slbn"]), 0);
                dataRow_2.BNDangDT_unit = "";
                dataRow_2.RaVienChuaTT_stt = 2;
                dataRow_2.RaVienChuaTT_name = "Tổng doanh thu";
                dataRow_2.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"]), 0) + " đ";
                dataRow_2.RaVienChuaTT_unit = "";
                dataRow_2.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tongtien"]), 0) + " đ";
                dataRow_2.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tongtien"].ToString()), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_3 = new BCDashboardQLTongTheKhoa();
                dataRow_3.BNDangDT_stt = 3;
                dataRow_3.BNDangDT_name = "SL bệnh nhân chuyển đến";
                dataRow_3.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[0][1]["ravien_slbn"]), 0);
                dataRow_3.BNDangDT_unit = "";
                dataRow_3.RaVienChuaTT_stt = 3;
                dataRow_3.RaVienChuaTT_name = "Khám bệnh";
                dataRow_3.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienkb"]), 0) + " đ";
                dataRow_3.RaVienChuaTT_unit = "";
                dataRow_3.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienkb"]), 0) + " đ";
                dataRow_3.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienkb"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_4 = new BCDashboardQLTongTheKhoa();
                dataRow_4.BNDangDT_stt = 4;
                dataRow_4.BNDangDT_name = "SL bệnh nhân ra viện";
                dataRow_4.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[0][2]["ravien_slbn"]), 0);
                dataRow_4.BNDangDT_unit = "";
                dataRow_4.RaVienChuaTT_stt = 4;
                dataRow_4.RaVienChuaTT_name = "Xét nghiệm";
                dataRow_4.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienxn"]), 0) + " đ";
                dataRow_4.RaVienChuaTT_unit = "";
                dataRow_4.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienxn"]), 0) + " đ";
                dataRow_4.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienxn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_5 = new BCDashboardQLTongTheKhoa();
                dataRow_5.BNDangDT_stt = 5;
                dataRow_5.BNDangDT_name = "Tổng tiền";
                dataRow_5.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tongtien"]), 0) + " đ";
                dataRow_5.BNDangDT_unit = "";
                dataRow_5.RaVienChuaTT_stt = 5;
                dataRow_5.RaVienChuaTT_name = "CĐHA-TDCN";
                dataRow_5.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tiencdhatdcn"]), 0) + " đ";
                dataRow_5.RaVienChuaTT_unit = "";
                dataRow_5.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tiencdhatdcn"]), 0) + " đ";
                dataRow_5.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_6 = new BCDashboardQLTongTheKhoa();
                dataRow_6.BNDangDT_stt = 6;
                dataRow_6.BNDangDT_name = "Khám bệnh";
                dataRow_6.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienkb"]), 0) + " đ";
                dataRow_6.BNDangDT_unit = "";
                dataRow_6.RaVienChuaTT_stt = 6;
                dataRow_6.RaVienChuaTT_name = "PTTT";
                dataRow_6.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienpttt"]), 0) + " đ";
                dataRow_6.RaVienChuaTT_unit = "";
                dataRow_6.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienpttt"]), 0) + " đ";
                dataRow_6.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienpttt"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_7 = new BCDashboardQLTongTheKhoa();
                dataRow_7.BNDangDT_stt = 7;
                dataRow_7.BNDangDT_name = "Xét nghiệm";
                dataRow_7.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienxn"]), 0) + " đ";
                dataRow_7.BNDangDT_unit = "";
                dataRow_7.RaVienChuaTT_stt = 7;
                dataRow_7.RaVienChuaTT_name = "DV KTC";
                dataRow_7.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tiendvktc"]), 0) + " đ";
                dataRow_7.RaVienChuaTT_unit = "";
                dataRow_7.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tiendvktc"]), 0) + " đ";
                dataRow_7.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tiendvktc"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_8 = new BCDashboardQLTongTheKhoa();
                dataRow_8.BNDangDT_stt = 8;
                dataRow_8.BNDangDT_name = "CĐHA-TDCN";
                dataRow_8.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiencdhatdcn"]), 0) + " đ";
                dataRow_8.BNDangDT_unit = "";
                dataRow_8.RaVienChuaTT_stt = 8;
                dataRow_8.RaVienChuaTT_name = "Giường thường";
                dataRow_8.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tiengiuongthuong"]), 0) + " đ";
                dataRow_8.RaVienChuaTT_unit = "";
                dataRow_8.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tiengiuongthuong"]), 0) + " đ";
                dataRow_8.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tiengiuongthuong"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_9 = new BCDashboardQLTongTheKhoa();
                dataRow_9.BNDangDT_stt = 9;
                dataRow_9.BNDangDT_name = "PTTT";
                dataRow_9.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienpttt"]), 0) + " đ";
                dataRow_9.BNDangDT_unit = "";
                dataRow_9.RaVienChuaTT_stt = 9;
                dataRow_9.RaVienChuaTT_name = "Giường yêu cầu";
                dataRow_9.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tiengiuongyeucau"]), 0) + " đ";
                dataRow_9.RaVienChuaTT_unit = "";
                dataRow_9.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tiengiuongyeucau"]), 0) + " đ";
                dataRow_9.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tiengiuongyeucau"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_10 = new BCDashboardQLTongTheKhoa();
                dataRow_10.BNDangDT_stt = 10;
                dataRow_10.BNDangDT_name = "DV KTC";
                dataRow_10.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiendvktc"]), 0) + " đ";
                dataRow_10.BNDangDT_unit = "";
                dataRow_10.RaVienChuaTT_stt = 10;
                dataRow_10.RaVienChuaTT_name = "DV khác";
                dataRow_10.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienkhac"]), 0) + " đ";
                dataRow_10.RaVienChuaTT_unit = "";
                dataRow_10.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienkhac"]), 0) + " đ";
                dataRow_10.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienkhac"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_11 = new BCDashboardQLTongTheKhoa();
                dataRow_11.BNDangDT_stt = 11;
                dataRow_11.BNDangDT_name = "Giường thường";
                dataRow_11.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiengiuongthuong"]), 0) + " đ";
                dataRow_11.BNDangDT_unit = "";
                dataRow_11.RaVienChuaTT_stt = 11;
                dataRow_11.RaVienChuaTT_name = "Máu, chế phẩm";
                dataRow_11.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienmau"]), 0) + " đ";
                dataRow_11.RaVienChuaTT_unit = "";
                dataRow_11.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienmau"]), 0) + " đ";
                dataRow_11.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienmau"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_12 = new BCDashboardQLTongTheKhoa();
                dataRow_12.BNDangDT_stt = 12;
                dataRow_12.BNDangDT_name = "Giường yêu cầu";
                dataRow_12.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiengiuongyeucau"]), 0) + " đ";
                dataRow_12.BNDangDT_unit = "";
                dataRow_12.RaVienChuaTT_stt = 12;
                dataRow_12.RaVienChuaTT_name = "Vật tư";
                dataRow_12.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienvattu"]), 0) + " đ";
                dataRow_12.RaVienChuaTT_unit = "";
                dataRow_12.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienvattu"]), 0) + " đ";
                dataRow_12.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienvattu"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_13 = new BCDashboardQLTongTheKhoa();
                dataRow_13.BNDangDT_stt = 13;
                dataRow_13.BNDangDT_name = "DV khác";
                dataRow_13.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienkhac"]), 0) + " đ";
                dataRow_13.BNDangDT_unit = "";
                dataRow_13.RaVienChuaTT_stt = 13;
                dataRow_13.RaVienChuaTT_name = "Thuốc";
                dataRow_13.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienthuoc"]), 0) + " đ";
                dataRow_13.RaVienChuaTT_unit = "";
                dataRow_13.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienthuoc"]), 0) + " đ";
                dataRow_13.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_tienthuoc"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_14 = new BCDashboardQLTongTheKhoa();
                dataRow_14.BNDangDT_stt = 14;
                dataRow_14.BNDangDT_name = "Máu, chế phẩm";
                dataRow_14.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienmau"]), 0) + " đ";
                dataRow_14.BNDangDT_unit = "";
                dataRow_14.RaVienChuaTT_stt = 14;
                dataRow_14.RaVienChuaTT_name = "Tỷ lệ thuốc";
                if (Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"]) != 0)
                {
                    dataRow_14.RaVienChuaTT_value = Math.Round(((Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienthuoc"]) / Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_14.DaTT_value = "0 %";
                }
                dataRow_14.RaVienChuaTT_unit = "";
                if (Convert.ToDecimal(dataBC[3][0]["raviendatt_tongtien"]) != 0)
                {
                    dataRow_14.DaTT_value = Math.Round(((Convert.ToDecimal(dataBC[3][0]["raviendatt_tienthuoc"]) / Convert.ToDecimal(dataBC[3][0]["raviendatt_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_14.DaTT_value = "0 %";
                }
                if (Convert.ToDecimal(dataBC[4][0]["doanhthu_tongtien"]) != 0)
                {
                    dataRow_14.DoanhThu_value = Math.Round(((Convert.ToDecimal(dataBC[4][0]["doanhthu_tienthuoc"]) / Convert.ToDecimal(dataBC[4][0]["doanhthu_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_14.DoanhThu_value = "0 %";
                }

                BCDashboardQLTongTheKhoa dataRow_15 = new BCDashboardQLTongTheKhoa();
                dataRow_15.BNDangDT_stt = 15;
                dataRow_15.BNDangDT_name = "Vật tư";
                dataRow_15.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienvattu"]), 0) + " đ";
                dataRow_15.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_16 = new BCDashboardQLTongTheKhoa();
                dataRow_16.BNDangDT_stt = 16;
                dataRow_16.BNDangDT_name = "Thuốc";
                dataRow_16.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienthuoc"]), 0) + " đ";
                dataRow_16.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_17 = new BCDashboardQLTongTheKhoa();
                dataRow_17.BNDangDT_stt = 17;
                dataRow_17.BNDangDT_name = "Tỷ lệ thuốc";
                if (Convert.ToDecimal(dataBC[1][0]["dangdt_tongtien"]) != 0)
                {
                    dataRow_17.BNDangDT_value = Math.Round(((Convert.ToDecimal(dataBC[1][0]["dangdt_tienthuoc"]) / Convert.ToDecimal(dataBC[1][0]["dangdt_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_17.BNDangDT_value = "0 %";
                }
                dataRow_17.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_18 = new BCDashboardQLTongTheKhoa();
                dataRow_18.BNDangDT_stt = 18;
                dataRow_18.BNDangDT_name = "Tạm ứng";
                dataRow_18.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tamung"]), 0) + " đ";
                dataRow_18.BNDangDT_unit = "";

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
