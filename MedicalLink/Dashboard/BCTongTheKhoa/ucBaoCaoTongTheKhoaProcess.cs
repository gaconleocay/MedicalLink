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
    public partial class ucBaoCaoTongTheKhoa : UserControl
    {
        private DangDTRaVienChuaDaTTFilterDTO filter { get; set; }
        private void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                filter = new DangDTRaVienChuaDaTTFilterDTO();
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.loaiBaoCao = "REPORT_08";
                filter.dateTu = this.thoiGianTu;
                filter.dateDen = this.thoiGianDen;
                filter.dateKhoangDLTu = this.KhoangThoiGianLayDuLieu;
                filter.departmentgroupid = Convert.ToInt16(cboKhoa.EditValue);
                filter.loaivienphiid = 0;
                filter.chayTuDong = 0;

                //Thread t1 = new Thread(ThreadDangDieuTri);
                //Thread t2 = new Thread(ThreadRaVienChuaThanhToan);
                //Thread t3 = new Thread(ThreadRaVienDaThanhToan);
                //Thread t4 = new Thread(SQLLayDuLieuBaoCao);

                //t1.Start();
                //t2.Start();
                //t3.Start();
                //t1.Join();
                //t2.Join();
                //t3.Join();

                //t4.Start();
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_DangDT_Tmp(filter);
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienChuaTT_Tmp(filter);
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
                SQLLayDuLieuBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void ThreadDangDieuTri()
        {
            try
            {
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_DangDT_Tmp(filter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void ThreadRaVienChuaThanhToan()
        {
            try
            {
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienChuaTT_Tmp(filter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void ThreadRaVienDaThanhToan()
        {
            try
            {
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        private void SQLLayDuLieuBaoCao()
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao_DangDT = "(SELECT departmentgroupid, bn_chuyendi, bn_chuyenden, ravien_slbn, dangdt_slbn_bh, dangdt_slbn_vp, dangdt_slbn, dangdt_tienkb, dangdt_tienxn, dangdt_tiencdhatdcn, dangdt_tienpttt, dangdt_tiendvktc, dangdt_tiengiuong, dangdt_tienkhac, dangdt_tienvattu, dangdt_tienmau, dangdt_tienthuoc_bhyt, dangdt_tienthuoc_vp, dangdt_tienthuoc, dangdt_tongtien_bhyt, dangdt_tongtien_vp, dangdt_tongtien, dangdt_tamung FROM tools_dangdt_tmp WHERE departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and khoangdl_tu='" + KhoangThoiGianLayDuLieu + "' and chaytudong=0 ORDER BY dangdt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_dangdt_tmp) order by departmentgroupid desc; ";
                string sqlBaoCao_RaVienChuaTT = "(SELECT departmentgroupid,ravienchuatt_slbn_bh,ravienchuatt_slbn_vp,ravienchuatt_slbn,ravienchuatt_tienkb,ravienchuatt_tienxn,ravienchuatt_tiencdhatdcn,ravienchuatt_tienpttt,ravienchuatt_tiendvktc,ravienchuatt_tiengiuong,ravienchuatt_tienkhac,ravienchuatt_tienvattu,ravienchuatt_tienmau,ravienchuatt_tienthuoc_bhyt,ravienchuatt_tienthuoc_vp,ravienchuatt_tienthuoc,ravienchuatt_tongtien_bhyt,ravienchuatt_tongtien_vp,ravienchuatt_tongtien,ravienchuatt_tamung FROM tools_ravienchuatt_tmp WHERE departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and khoangdl_tu='" + KhoangThoiGianLayDuLieu + "' and chaytudong=0 ORDER BY ravienchuatt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_ravienchuatt_tmp) order by departmentgroupid desc;";
                string sqlBaoCao_RaVienDaTT = "(SELECT departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuong, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung FROM tools_raviendatt_tmp raviendatt WHERE raviendatt.departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and chaytudong=0 ORDER BY raviendatt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_raviendatt_tmp) order by departmentgroupid desc; ";

                string sqlBNRaVienChuyenDiChuyenDen = "(SELECT 'BN ra vien' as name, count(vp.vienphiid) as ravien_slbn FROM vienphi vp WHERE vp.vienphistatus=1 and vp.vienphidate_ravien>='" + thoiGianTu + "' and vp.vienphidate_ravien<='" + thoiGianDen + "' and vp.departmentgroupid='" + (cboKhoa.EditValue) + "') Union (SELECT 'BN chuyen di' as name, count(A1.vienphiid) as bn_chuyendi FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd  WHERE mrd.departmentgroupid='" + (cboKhoa.EditValue) + "' and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + thoiGianTu + "' and mrd.thoigianravien<='" + thoiGianDen + "') A1 ) Union (SELECT 'BN chuyen den' as name, count(A2.vienphiid) as bn_chuyenden FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd  WHERE mrd.departmentgroupid='" + (cboKhoa.EditValue) + "' and mrd.hinhthucvaovienid=3 and mrd.thoigianravien>='" + thoiGianTu + "' and mrd.thoigianravien<='" + thoiGianDen + "') A2);";

                DataView dataBCTongTheKhoa_DangDT = new DataView(condb.getDataTable(sqlBaoCao_DangDT));
                DataView dataBCTongTheKhoa_RaVienChuaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienChuaTT));
                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienDaTT));
                DataView dataBNRaVienChuyenDiChuyenDen = new DataView(condb.getDataTable(sqlBNRaVienChuyenDiChuyenDen));
                List<DataView> dataBC = new List<DataView>();
                if (dataBCTongTheKhoa_DangDT == null || dataBCTongTheKhoa_DangDT.Count == 0)
                {
                }
                dataBC.Add(dataBNRaVienChuyenDiChuyenDen);
                dataBC.Add(dataBCTongTheKhoa_DangDT);
                dataBC.Add(dataBCTongTheKhoa_RaVienChuaTT);
                dataBC.Add(dataBCTongTheKhoa_RaVienDaTT);
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
                //dataRow_1.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[4][0]["doanhthu_slbn"]), 0);

                BCDashboardQLTongTheKhoa dataRow_2 = new BCDashboardQLTongTheKhoa();
                dataRow_2.BNDangDT_stt = 2;
                dataRow_2.BNDangDT_name = "SL bệnh nhân chuyển đi";
                dataRow_2.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[0][1]["ravien_slbn"]), 0);
                dataRow_2.BNDangDT_unit = "";
                dataRow_2.RaVienChuaTT_stt = 2;
                dataRow_2.RaVienChuaTT_name = "Tổng doanh thu";
                dataRow_2.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"]), 0) + " đ";
                dataRow_2.RaVienChuaTT_unit = "";
                dataRow_2.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tongtien"]), 0) + " đ";
                //dataRow_2.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_3 = new BCDashboardQLTongTheKhoa();
                dataRow_3.BNDangDT_stt = 3;
                dataRow_3.BNDangDT_name = "SL bệnh nhân chuyển đến";
                dataRow_3.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[0][2]["ravien_slbn"]), 0);
                dataRow_3.BNDangDT_unit = "";
                dataRow_3.RaVienChuaTT_stt = 3;
                dataRow_3.RaVienChuaTT_name = "Khám bệnh";
                dataRow_3.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienkb"]), 0) + " đ";
                dataRow_3.RaVienChuaTT_unit = "";
                dataRow_3.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienkb"]), 0) + " đ";
                //dataRow_3.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_4 = new BCDashboardQLTongTheKhoa();
                dataRow_4.BNDangDT_stt = 4;
                dataRow_4.BNDangDT_name = "SL bệnh nhân ra viện";
                dataRow_4.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[0][0]["ravien_slbn"]), 0);
                dataRow_4.BNDangDT_unit = "";
                dataRow_4.RaVienChuaTT_stt = 4;
                dataRow_4.RaVienChuaTT_name = "Xét nghiệm";
                dataRow_4.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienxn"]), 0) + " đ";
                dataRow_4.RaVienChuaTT_unit = "";
                dataRow_4.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienxn"]), 0) + " đ";
                //dataRow_4.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

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
                // dataRow_5.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

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
                //dataRow_6.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

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
                //dataRow_7.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_8 = new BCDashboardQLTongTheKhoa();
                dataRow_8.BNDangDT_stt = 8;
                dataRow_8.BNDangDT_name = "CĐHA-TDCN";
                dataRow_8.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiencdhatdcn"]), 0) + " đ";
                dataRow_8.BNDangDT_unit = "";
                dataRow_8.RaVienChuaTT_stt = 8;
                dataRow_8.RaVienChuaTT_name = "Ngày giường";
                dataRow_8.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tiengiuong"]), 0) + " đ";
                dataRow_8.RaVienChuaTT_unit = "";
                dataRow_8.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tiengiuong"]), 0) + " đ";
                //dataRow_8.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_9 = new BCDashboardQLTongTheKhoa();
                dataRow_9.BNDangDT_stt = 9;
                dataRow_9.BNDangDT_name = "PTTT";
                dataRow_9.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienpttt"]), 0) + " đ";
                dataRow_9.BNDangDT_unit = "";
                dataRow_9.RaVienChuaTT_stt = 9;
                dataRow_9.RaVienChuaTT_name = "DV khác";
                dataRow_9.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienkhac"]), 0) + " đ";
                dataRow_9.RaVienChuaTT_unit = "";
                dataRow_9.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienkhac"]), 0) + " đ";
                //dataRow_9.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_10 = new BCDashboardQLTongTheKhoa();
                dataRow_10.BNDangDT_stt = 10;
                dataRow_10.BNDangDT_name = "DV KTC";
                dataRow_10.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiendvktc"]), 0) + " đ";
                dataRow_10.BNDangDT_unit = "";
                dataRow_10.RaVienChuaTT_stt = 10;
                dataRow_10.RaVienChuaTT_name = "Máu, chế phẩm";
                dataRow_10.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienmau"]), 0) + " đ";
                dataRow_10.RaVienChuaTT_unit = "";
                dataRow_10.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienmau"]), 0) + " đ";
                //dataRow_10.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_11 = new BCDashboardQLTongTheKhoa();
                dataRow_11.BNDangDT_stt = 11;
                dataRow_11.BNDangDT_name = "Ngày giường";
                dataRow_11.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tiengiuong"]), 0) + " đ";
                dataRow_11.BNDangDT_unit = "";
                dataRow_11.RaVienChuaTT_stt = 11;
                dataRow_11.RaVienChuaTT_name = "Vật tư";
                dataRow_11.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienvattu"]), 0) + " đ";
                dataRow_11.RaVienChuaTT_unit = "";
                dataRow_11.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienvattu"]), 0) + " đ";
                //dataRow_11.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_12 = new BCDashboardQLTongTheKhoa();
                dataRow_12.BNDangDT_stt = 12;
                dataRow_12.BNDangDT_name = "DV khác";
                dataRow_12.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienkhac"]), 0) + " đ";
                dataRow_12.BNDangDT_unit = "";
                dataRow_12.RaVienChuaTT_stt = 12;
                dataRow_12.RaVienChuaTT_name = "Thuốc";
                dataRow_12.RaVienChuaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienthuoc"]), 0) + " đ";
                dataRow_12.RaVienChuaTT_unit = "";
                dataRow_12.DaTT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[3][0]["raviendatt_tienthuoc"]), 0) + " đ";
                // dataRow_12.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0) + " đ";

                BCDashboardQLTongTheKhoa dataRow_13 = new BCDashboardQLTongTheKhoa();
                dataRow_13.BNDangDT_stt = 13;
                dataRow_13.BNDangDT_name = "Máu, chế phẩm";
                dataRow_13.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienmau"]), 0) + " đ";
                dataRow_13.BNDangDT_unit = "";
                dataRow_13.RaVienChuaTT_stt = 13;
                dataRow_13.RaVienChuaTT_name = "Tỷ lệ thuốc";
                if (Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"]) != 0)
                {
                    dataRow_13.RaVienChuaTT_value = Math.Round(((Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tienthuoc"]) / Convert.ToDecimal(dataBC[2][0]["ravienchuatt_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_13.DaTT_value = "0 %";
                }
                dataRow_13.RaVienChuaTT_unit = "";
                if (Convert.ToDecimal(dataBC[3][0]["raviendatt_tongtien"]) != 0)
                {
                    dataRow_13.DaTT_value = Math.Round(((Convert.ToDecimal(dataBC[3][0]["raviendatt_tienthuoc"]) / Convert.ToDecimal(dataBC[3][0]["raviendatt_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_13.DaTT_value = "0 %";
                }
                //dataRow_13.DoanhThu_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_slbn"]), 0);

                BCDashboardQLTongTheKhoa dataRow_14 = new BCDashboardQLTongTheKhoa();
                dataRow_14.BNDangDT_stt = 14;
                dataRow_14.BNDangDT_name = "Vật tư";
                dataRow_14.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienvattu"]), 0) + " đ";
                dataRow_14.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_15 = new BCDashboardQLTongTheKhoa();
                dataRow_15.BNDangDT_stt = 15;
                dataRow_15.BNDangDT_name = "Thuốc";
                dataRow_15.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tienthuoc"]), 0) + " đ";
                dataRow_15.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_16 = new BCDashboardQLTongTheKhoa();
                dataRow_16.BNDangDT_stt = 16;
                dataRow_16.BNDangDT_name = "Tỷ lệ thuốc";
                if (Convert.ToDecimal(dataBC[1][0]["dangdt_tongtien"]) != 0)
                {
                    dataRow_16.BNDangDT_value = Math.Round(((Convert.ToDecimal(dataBC[1][0]["dangdt_tienthuoc"]) / Convert.ToDecimal(dataBC[1][0]["dangdt_tongtien"])) * 100), 1).ToString() + " %";
                }
                else
                {
                    dataRow_16.BNDangDT_value = "0 %";
                }
                dataRow_16.BNDangDT_unit = "";

                BCDashboardQLTongTheKhoa dataRow_17 = new BCDashboardQLTongTheKhoa();
                dataRow_17.BNDangDT_stt = 17;
                dataRow_17.BNDangDT_name = "Tạm ứng";
                dataRow_17.BNDangDT_value = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBC[1][0]["dangdt_tamung"]), 0) + " đ";
                dataRow_17.BNDangDT_unit = "";

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

                gridControlDataQLTTKhoa.DataSource = null;
                gridControlDataQLTTKhoa.DataSource = dataBCQLTongTheKhoaSDO;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion



        //#region Hien thi du lieu
        //private void HienThiDuLieuBaoCao_DangDT(DataView dataBCTongTheKhoa, DataView dataBCTongTheKhoa_DangDT_SLBNRV, string bn_chuyenden, string bn_chuyendi)
        //{
        //    try
        //    {
        //        if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
        //        {
        //            lblBNHienDien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_slbn"]), 0);
        //            lblBNChuyenDi.Text = bn_chuyendi;
        //            lblBNChuyenDen.Text = bn_chuyenden;

        //            Decimal tongtien = Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tongtien"]);
        //            lblDangDTSoTien.Text = Util_NumberConvert.NumberToString(tongtien, 0);
        //            lblDangDTKhamBenh.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienkb"]), 0);
        //            lblDangDTXetNghiem.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienxn"]), 0);
        //            lblDangDTCDHA.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tiencdhatdcn"]), 0);
        //            lblDangDTPTTT.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienpttt"]), 0);
        //            lblDangDTDVKTC.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tiendvktc"]), 0);
        //            lblDangDTGiuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tiengiuong"]), 0);
        //            lblDangDTDVKhac.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienkhac"]), 0);
        //            lblDangDTMau.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienmau"]), 0);
        //            lblDangDTVatTu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienvattu"]), 0);
        //            lblDangDTThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienthuoc"]), 0);
        //            if (tongtien != 0)
        //            {
        //                lblDangDTTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienthuoc"]) * 100 / tongtien), 2));
        //            }
        //            else
        //            {
        //                lblDangDTTyLeThuoc.Text = "0";
        //            }
        //            lblDangDTTamUng.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tamung"]), 0);
        //        }
        //        else
        //        {
        //            lblBNHienDien.Text = "0";
        //            lblBNChuyenDi.Text = "0";
        //            lblBNChuyenDen.Text = "0";
        //            lblDangDTSoTien.Text = "0";
        //            lblDangDTKhamBenh.Text = "0";
        //            lblDangDTXetNghiem.Text = "0";
        //            lblDangDTCDHA.Text = "0";
        //            lblDangDTPTTT.Text = "0";
        //            lblDangDTDVKTC.Text = "0";
        //            lblDangDTGiuong.Text = "0";
        //            lblDangDTDVKhac.Text = "0";
        //            lblDangDTMau.Text = "0";
        //            lblDangDTVatTu.Text = "0";
        //            lblDangDTThuoc.Text = "0";
        //            lblDangDTTyLeThuoc.Text = "0";
        //            lblDangDTTamUng.Text = "0";
        //        }
        //        if (dataBCTongTheKhoa_DangDT_SLBNRV != null && dataBCTongTheKhoa_DangDT_SLBNRV.Count > 0)
        //        {
        //            lblBNRaVien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa_DangDT_SLBNRV[0]["ravien_slbn"]), 0);
        //        }
        //        else
        //        {
        //            lblBNRaVien.Text = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}
        //private void HienThiDuLieuBaoCao_RaVienChuaTT(DataView dataBCTongTheKhoa)
        //{
        //    try
        //    {
        //        if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
        //        {
        //            lblDaRVSoLuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_slbn"]), 0);
        //            Decimal tongtien = Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tongtien"]);

        //            lblDaRVDoanhThu.Text = Util_NumberConvert.NumberToString(tongtien, 0);
        //            lblDaRVKhamBenh.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienkb"]), 0);
        //            lblDaRVXetNghiem.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienxn"]), 0);
        //            lblDaRVCDHA.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tiencdhatdcn"]), 0);
        //            lblDaRVPTTT.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienpttt"]), 0);
        //            lblDaRVDVKTC.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tiendvktc"]), 0);
        //            lblDaRVGiuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tiengiuong"]), 0);
        //            lblDaRVDVKhac.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienkhac"]), 0);
        //            lblDaRVVatTu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienvattu"]), 0);
        //            lblDaRVMau.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienmau"]), 0);
        //            lblDaRVThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienthuoc"]), 0);
        //            if (tongtien != 0)
        //            {
        //                lblDaRVTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienthuoc"]) * 100 / tongtien), 2));
        //            }
        //            else
        //            {
        //                lblDaRVTyLeThuoc.Text = "0";
        //            }
        //        }
        //        else
        //        {
        //            lblDaRVSoLuong.Text = "0";
        //            lblDaRVDoanhThu.Text = "0";
        //            lblDaRVKhamBenh.Text = "0";
        //            lblDaRVXetNghiem.Text = "0";
        //            lblDaRVCDHA.Text = "0";
        //            lblDaRVPTTT.Text = "0";
        //            lblDaRVDVKTC.Text = "0";
        //            lblDaRVGiuong.Text = "0";
        //            lblDaRVDVKhac.Text = "0";
        //            lblDaRVMau.Text = "0";
        //            lblDaRVVatTu.Text = "0";
        //            lblDaRVThuoc.Text = "0";
        //            lblDaRVTyLeThuoc.Text = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}

        //private void HienThiDuLieuBaoCao_RaVienDaTT(DataView dataBCTongTheKhoa)
        //{
        //    try
        //    {
        //        if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
        //        {
        //            lblDaTTSoLuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_slbn"]), 0);

        //            Decimal tongtien = Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tongtien"]);
        //            lblDaTTDoanhThu.Text = Util_NumberConvert.NumberToString(tongtien, 0);
        //            lblDaTTKhamBenh.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienkb"]), 0);
        //            lblDaTTXetNghiem.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienxn"]), 0);
        //            lblDaTTCDHA.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tiencdhatdcn"]), 0);
        //            lblDaTTPTTT.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienpttt"]), 0);
        //            lblDaTTDVKTC.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tiendvktc"]), 0);
        //            lblDaTTGiuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tiengiuong"]), 0);
        //            lblDaTTDVKhac.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienkhac"]), 0);
        //            lblDaTTVatTu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienvattu"]), 0);
        //            lblDaTTMau.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienmau"]), 0);
        //            lblDaTTThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienthuoc"]), 0);
        //            if (tongtien != 0)
        //            {
        //                lblDaTTTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienthuoc"]) * 100 / tongtien), 2));
        //            }
        //            else
        //            {
        //                lblDaTTTyLeThuoc.Text = "0";
        //            }
        //        }
        //        else
        //        {
        //            lblDaTTSoLuong.Text = "0";
        //            lblDaTTDoanhThu.Text = "0";
        //            lblDaTTKhamBenh.Text = "0";
        //            lblDaTTXetNghiem.Text = "0";
        //            lblDaTTCDHA.Text = "0";
        //            lblDaTTPTTT.Text = "0";
        //            lblDaTTDVKTC.Text = "0";
        //            lblDaTTGiuong.Text = "0";
        //            lblDaTTDVKhac.Text = "0";
        //            lblDaTTMau.Text = "0";
        //            lblDaTTVatTu.Text = "0";
        //            lblDaTTThuoc.Text = "0";
        //            lblDaTTTyLeThuoc.Text = "0";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}

        //#endregion
    }
}
