using DevExpress.XtraCharts;
using DevExpress.XtraSplashScreen;
using MedicalLink.DatabaseProcess;
using MedicalLink.DatabaseProcess.FilterDTO;
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
    public partial class ucDashboardBenhNhanNoiTru : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                DangDTRaVienChuaDaTTFilterDTO filter = new DangDTRaVienChuaDaTTFilterDTO();
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.loaiBaoCao = "REPORT_09";
                filter.dateTu = this.thoiGianTu;
                filter.dateDen = this.thoiGianDen;
                filter.dateKhoangDLTu = this.KhoangThoiGianLayDuLieu;
                filter.departmentgroupid = 0;
                filter.loaivienphiid = -1;
                filter.chayTuDong = 0;
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_DangDT_Tmp(filter);
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienChuaTT_Tmp(filter);
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);

                SQLLayDuLieuBaoCao_ChayMoi();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void SQLLayDuLieuBaoCao_ChayMoi()
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao = "SELECT depg.departmentgroupid, depg.departmentgroupcode, depg.departmentgroupname, (select datt.raviendatt_tongtien_bhyt from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as dathanhtoan_sotien_bhyt, (select datt.raviendatt_tongtien_vp from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as dathanhtoan_sotien_nhandan, (select datt.raviendatt_tongtien from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as dathanhtoan_sotien_tong, (select datt.raviendatt_tienthuoc_bhyt from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as dathanhtoan_thuoc_bhyt, (select datt.raviendatt_tienthuoc_vp from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as dathanhtoan_thuoc_nhandan, (select datt.raviendatt_tienthuoc from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as dathanhtoan_thuoc_tong, (select datt.raviendatt_tamung from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=0) as raviendatt_tamung FROM departmentgroup depg WHERE depg.departmentgrouptype in (1,4,11,10)  GROUP BY depg.departmentgroupid ORDER BY depg.departmentgroupname;";
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
                    chartControlBNNoiTru.Series[0].Points.Clear();
                    chartControlBNNoiTru.Series[1].Points.Clear();
                    chartControlBNNoiTru.Series[2].Points.Clear();
                    for (int i = 0; i < dataBC.Count; i++)
                    {
                        chartControlBNNoiTru.Series[0].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.Util_TypeConvertParse.ToDouble(dataBC[i]["dathanhtoan_sotien_bhyt"].ToString())));
                    }
                    for (int i = 0; i < dataBC.Count; i++)
                    {
                        chartControlBNNoiTru.Series[1].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.Util_TypeConvertParse.ToDouble(dataBC[i]["dathanhtoan_sotien_nhandan"].ToString())));
                    }
                    for (int i = 0; i < dataBC.Count; i++)
                    {
                        chartControlBNNoiTru.Series[2].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.Util_TypeConvertParse.ToDouble(dataBC[i]["raviendatt_tamung"].ToString())));
                    }
                }
                else
                {
                    dataBCTongTheKhoa = null;
                    chartControlBNNoiTru.Series[0].Points.Clear();
                    chartControlBNNoiTru.Series[1].Points.Clear();
                    chartControlBNNoiTru.Series[2].Points.Clear();
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
