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
        internal void LayDuLieuBaoCao()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                // Lấy từ ngày, đến ngày
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                TimerServiceProcess.SQLKiemTraVaUpdate_DangDT_Tmp(1, thoiGianTu, thoiGianDen, KhoangThoiGianLayDuLieu, 0, 0);
                TimerServiceProcess.SQLKiemTraVaUpdate_RaVienChuaTT_Tmp(0, thoiGianTu, thoiGianDen, KhoangThoiGianLayDuLieu, 0, 0);
                TimerServiceProcess.SQLKiemTraVaUpdate_RaVienDaTT_Tmp(0, thoiGianTu, thoiGianDen, KhoangThoiGianLayDuLieu, 0);
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

                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao = "SELECT A.departmentgroupid, A.departmentgroupcode, A.departmentgroupname, A.dangdt_slbn_bhyt, A.dangdt_slbn_nhandan, dangdt_slbn_tong, A.dangdt_tamung, A.ravienchuatt_slbn, A.ravienchuatt_tongtien, A.ravienchuatt_tamung, A.dathanhtoan_sotien_bhyt, A.dathanhtoan_sotien_nhandan, A.dathanhtoan_sotien_tong, A.dathanhtoan_thuoc_bhyt, A.dathanhtoan_thuoc_nhandan, A.dathanhtoan_thuoc_tong, case when A.dathanhtoan_sotien_bhyt<>0 then round(cast((A.dathanhtoan_thuoc_bhyt/dathanhtoan_sotien_bhyt) * 100 as numeric) ,2) else 0 end as dathanhtoan_tyle_thuoc_bhyt, case when A.dathanhtoan_sotien_nhandan<>0 then round(cast((A.dathanhtoan_thuoc_nhandan/dathanhtoan_sotien_nhandan) * 100 as numeric),2) else 0 end as dathanhtoan_tyle_thuoc_nhandan, case when A.dathanhtoan_sotien_tong<>0 then round(cast((A.dathanhtoan_thuoc_tong/dathanhtoan_sotien_tong) * 100 as numeric),2) else 0 end as dathanhtoan_tyle_thuoc_tong FROM (SELECT depg.departmentgroupid, depg.departmentgroupcode, depg.departmentgroupname, (select dangdt.dangdt_slbn_bh from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.kieulaydulieu = 1) as dangdt_slbn_bhyt, (select dangdt.dangdt_slbn_vp from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.kieulaydulieu = 1) as dangdt_slbn_nhandan, (select dangdt.dangdt_slbn from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.kieulaydulieu = 1) as dangdt_slbn_tong,(select dangdt.dangdt_tamung from tools_dangdt_tmp dangdt where dangdt.departmentgroupid=depg.departmentgroupid and dangdt.kieulaydulieu = 1) as dangdt_tamung, (select chuatt.ravienchuatt_slbn from tools_ravienchuatt_tmp chuatt where chuatt.departmentgroupid=depg.departmentgroupid) as ravienchuatt_slbn,(select chuatt.ravienchuatt_tongtien from tools_ravienchuatt_tmp chuatt where chuatt.departmentgroupid=depg.departmentgroupid) as ravienchuatt_tongtien,(select chuatt.ravienchuatt_tamung from tools_ravienchuatt_tmp chuatt where chuatt.departmentgroupid=depg.departmentgroupid) as ravienchuatt_tamung,(select datt.raviendatt_tongtien_bhyt from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid) as dathanhtoan_sotien_bhyt,(select datt.raviendatt_tongtien_vp from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid) as dathanhtoan_sotien_nhandan,(select datt.raviendatt_tongtien from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid) as dathanhtoan_sotien_tong,(select datt.raviendatt_tienthuoc_bhyt from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid) as dathanhtoan_thuoc_bhyt,(select datt.raviendatt_tienthuoc_vp from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid) as dathanhtoan_thuoc_nhandan,(select datt.raviendatt_tienthuoc from tools_raviendatt_tmp datt where datt.departmentgroupid=depg.departmentgroupid) as dathanhtoan_thuoc_tong FROM departmentgroup depg WHERE depg.departmentgrouptype in (4) GROUP BY depg.departmentgroupid) A ORDER BY A.departmentgroupname ;";
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
