using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
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
    public partial class ucBaoCaoBenhNhanNgoaiTru : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                BCDashboardBenhNhanNgoaiTruFilter filter = new BCDashboardBenhNhanNgoaiTruFilter();
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.loaiBaoCao = "REPORT_10";
                filter.dateTu = this.thoiGianTu;
                filter.dateDen = this.thoiGianDen;
                filter.departmentgroupid = 0;
                filter.chayTuDong = 0;
                List<BCDashboardBenhNhanNgoaiTru> lstBCBNNgoaiTru = BCBenhNhanNgoaiTru_Process.BCBenhNhanNgoaiTru_ChayMoi(filter);
                HienThiDuLieuBaoCao(lstBCBNNgoaiTru);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void LayDuLieuBaoCao_DaChayDuLieu()
        {
            try
            {
                //BCDashboardBenhNhanNgoaiTruFilter filter = new BCDashboardBenhNhanNgoaiTruFilter();
                //thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //filter.loaiBaoCao = "REPORT_09";
                //filter.dateTu = this.thoiGianTu;
                //filter.dateDen = this.thoiGianDen;
                //filter.dateKhoangDLTu = this.KhoangThoiGianLayDuLieu;
                //filter.departmentgroupid = 0;
                //filter.loaivienphiid = 0;
                //filter.chayTuDong = 0;
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
                //SQLLayDuLieuBaoCao_DaChayDuLieu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        //private void SQLLayDuLieuBaoCao_DaChayDuLieu()
        //{
        //    try
        //    {
        //        lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

        //        string sqlBaoCao = "SELECT A.departmentgroupid, A.departmentgroupcode, A.departmentgroupname, A.dangdt_slbn_bhyt, A.dangdt_slbn_nhandan, dangdt_slbn_tong, A.dangdt_tamung, A.ravienchuatt_slbn, A.ravienchuatt_tongtien, A.ravienchuatt_tamung, A.dathanhtoan_sotien_bhyt, A.dathanhtoan_sotien_nhandan, A.dathanhtoan_sotien_tong, A.dathanhtoan_thuoc_bhyt, A.dathanhtoan_thuoc_nhandan, A.dathanhtoan_thuoc_tong, case when A.dathanhtoan_sotien_bhyt<>0 then round(cast((A.dathanhtoan_thuoc_bhyt/dathanhtoan_sotien_bhyt) * 100 as numeric) ,2) else 0 end as dathanhtoan_tyle_thuoc_bhyt, case when A.dathanhtoan_sotien_nhandan<>0 then round(cast((A.dathanhtoan_thuoc_nhandan/dathanhtoan_sotien_nhandan) * 100 as numeric),2) else 0 end as dathanhtoan_tyle_thuoc_nhandan, case when A.dathanhtoan_sotien_tong<>0 then round(cast((A.dathanhtoan_thuoc_tong/dathanhtoan_sotien_tong) * 100 as numeric),2) else 0 end as dathanhtoan_tyle_thuoc_tong FROM ( SELECT depg.departmentgroupid, depg.departmentgroupcode, depg.departmentgroupname, (select dangdt.dangdt_slbn_bh from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.loaibaocao='REPORT_09' and chaytudong=1) as dangdt_slbn_bhyt, (select dangdt.dangdt_slbn_vp from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.loaibaocao='REPORT_09' and chaytudong=1) as dangdt_slbn_nhandan, (select dangdt.dangdt_slbn from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.loaibaocao='REPORT_09' and chaytudong=1) as dangdt_slbn_tong, (select dangdt.dangdt_tamung from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.loaibaocao='REPORT_09' and chaytudong=1) as dangdt_tamung, (select chuatt.ravienchuatt_slbn from tools_ravienchuatt_tmp chuatt where chuatt.departmentgroupid=depg.departmentgroupid and chuatt.loaibaocao='REPORT_09' and chaytudong=1) as ravienchuatt_slbn, (select chuatt.ravienchuatt_tongtien from tools_ravienchuatt_tmp chuatt where chuatt.departmentgroupid=depg.departmentgroupid and chuatt.loaibaocao='REPORT_09' and chaytudong=1) as ravienchuatt_tongtien, (select chuatt.ravienchuatt_tamung from tools_ravienchuatt_tmp chuatt where chuatt.departmentgroupid=depg.departmentgroupid and chuatt.loaibaocao='REPORT_09' and chaytudong=1) as ravienchuatt_tamung, (select datt.raviendatt_tongtien_bhyt from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=1) as dathanhtoan_sotien_bhyt, (select datt.raviendatt_tongtien_vp from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=1) as dathanhtoan_sotien_nhandan, (select datt.raviendatt_tongtien from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=1) as dathanhtoan_sotien_tong, (select datt.raviendatt_tienthuoc_bhyt from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=1) as dathanhtoan_thuoc_bhyt, (select datt.raviendatt_tienthuoc_vp from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=1) as dathanhtoan_thuoc_nhandan, (select datt.raviendatt_tienthuoc from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid and datt.loaibaocao='REPORT_09' and datt.chaytudong=1) as dathanhtoan_thuoc_tong FROM departmentgroup depg WHERE depg.departmentgrouptype in (4) GROUP BY depg.departmentgroupid ) A ORDER BY A.departmentgroupname ;";
        //        DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
        //        // HienThiDuLieuBaoCao(dataBCTongTheKhoa);
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}
        private void HienThiDuLieuBaoCao(List<BCDashboardBenhNhanNgoaiTru> dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Count > 0)
                {
                    lstDataBCBNNgoaiTru = new List<BCDashboardBenhNhanNgoaiTru>();
                    foreach (var item in dataDSPhongKham)
                    {
                        BCDashboardBenhNhanNgoaiTru dataBCNgT = new BCDashboardBenhNhanNgoaiTru();
                        dataBCNgT.departmentid = item.departmentid;
                        dataBCNgT.departmentcode = item.departmentcode;
                        dataBCNgT.departmentname = item.departmentname;

                        var result = dataBC.Where(o=>o.departmentid==item.departmentid).SingleOrDefault();
                        if (result != null)
                        {
                            dataBCNgT.slbn_bh = result.slbn_bh;
                            dataBCNgT.slbn_vp = result.slbn_vp;
                            dataBCNgT.slbn = result.slbn;
                            dataBCNgT.slbn_nhapvien = result.slbn_nhapvien;
                            dataBCNgT.tien_bh = result.tien_bh;
                            dataBCNgT.tien_vp = result.tien_vp;
                            dataBCNgT.tien_tong = result.tien_tong;
                        }
                        else
                        {
                            dataBCNgT.slbn_bh = 0;
                            dataBCNgT.slbn_vp = 0;
                            dataBCNgT.slbn = 0;
                            dataBCNgT.slbn_nhapvien = 0;
                            dataBCNgT.tien_bh = 0;
                            dataBCNgT.tien_vp = 0;
                            dataBCNgT.tien_tong = 0;
                        }
                        lstDataBCBNNgoaiTru.Add(dataBCNgT);
                    }

                    gridControlDataBNNgT.DataSource = lstDataBCBNNgoaiTru;
                }
                else
                {
                    lstDataBCBNNgoaiTru = null;
                    gridControlDataBNNgT.DataSource = null;
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
