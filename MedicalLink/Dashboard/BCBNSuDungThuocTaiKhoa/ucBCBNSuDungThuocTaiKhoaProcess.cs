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
using System.Linq;


namespace MedicalLink.Dashboard
{
    public partial class ucBCBNSuDungThuocTaiKhoa : UserControl
    {
        private DangDTRaVienChuaDaTTFilterDTO filter { get; set; }
        private void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                int datatype = 0;
                string bhyt_groupcode = "'09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle'";
                if (radioVatTu.Checked)
                {
                    datatype = 1;
                    bhyt_groupcode = "'10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle'";
                }
                SQLLayDuLieuBaoCao(thoiGianTu, thoiGianDen, datatype, bhyt_groupcode);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void SQLLayDuLieuBaoCao(string thoiGianTu, string thoiGianDen, int datatype, string bhyt_groupcode)
        {
            try
            {
                string sqlLayDuLieu = "SELECT row_number() OVER () as stt, me.medicinerefid_org, me.medicinegroupcode, (select medi.medicinecode from medicine_ref medi where medi.medicinerefid=me.medicinerefid_org and medi.datatype=" + datatype + ") as medicinecode, me.medicinename, me.donvitinh,me.giaban, sum(SL.dangdt_sl) as dangdt_sl, (sum(SL.dangdt_sl) * me.giaban) as dangdt_thanhtien, sum(SL.daravien_sl) as daravien_sl, (sum(SL.daravien_sl) * me.giaban) as daravien_thanhtien, sum(SL.datt_sl) as datt_sl, (sum(SL.datt_sl) * me.giaban) as datt_thanhtien, sum(SL.doanhthu_sl) as doanhthu_sl, (sum(SL.doanhthu_sl) * me.giaban) as doanhthu_thanhtien FROM (SELECT COALESCE(A.servicepricecode,B.servicepricecode,C.servicepricecode,D.servicepricecode) as medicinecode, A.sl_tong as dangdt_sl, B.sl_tong as daravien_sl, C.sl_tong as datt_sl, D.sl_tong as doanhthu_sl FROM (SELECT ser.servicepricecode, sum(case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as sl_tong FROM vienphi vp inner join serviceprice ser on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + bhyt_groupcode + ") and ser.thuockhobanle=0 and ser.loaidoituong in (0,1,3,4,6,8,9) WHERE vp.vienphistatus=0 and vp.departmentgroupid='" + cboKhoa.EditValue.ToString() + "' GROUP BY ser.servicepricecode) A FULL JOIN (SELECT ser.servicepricecode, sum(case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as sl_tong FROM vienphi vp inner join serviceprice ser on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + bhyt_groupcode + ") and ser.thuockhobanle=0 and ser.loaidoituong in (0,1,3,4,6,8,9) WHERE COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 and vp.departmentgroupid='" + cboKhoa.EditValue.ToString() + "' GROUP BY ser.servicepricecode) B ON A.servicepricecode=B.servicepricecode FULL JOIN (SELECT ser.servicepricecode, sum(case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as sl_tong FROM vienphi vp inner join serviceprice ser on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + bhyt_groupcode + ") and ser.thuockhobanle=0 and ser.loaidoituong in (0,1,3,4,6,8,9) and ser.servicepricedate<'" + thoiGianDen + "' WHERE vp.vienphistatus_vp=1 and vp.departmentgroupid='" + cboKhoa.EditValue.ToString() + "' and vp.duyet_ngayduyet_vp>='" + thoiGianTu + "' and vp.duyet_ngayduyet_vp<='" + thoiGianDen + "' GROUP BY ser.servicepricecode) C ON C.servicepricecode=A.servicepricecode FULL JOIN (SELECT ser.servicepricecode, sum(case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as sl_tong FROM vienphi vp inner join serviceprice ser on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + bhyt_groupcode + ") and ser.thuockhobanle=0 and ser.loaidoituong in (0,1,3,4,6,8,9) and ser.servicepricedate<'" + thoiGianDen + "' WHERE vp.vienphistatus_vp=1 and ser.departmentgroupid='" + cboKhoa.EditValue.ToString() + "' and vp.duyet_ngayduyet_vp>='" + thoiGianTu + "' and vp.duyet_ngayduyet_vp<='" + thoiGianDen + "' GROUP BY ser.servicepricecode) D ON D.servicepricecode=A.servicepricecode ) SL INNER JOIN medicine_ref me on me.medicinecode=SL.medicinecode WHERE me.datatype=" + datatype + " GROUP BY me.medicinerefid_org,me.medicinegroupcode,me.medicinename,me.donvitinh,me.giaban ORDER BY me.medicinegroupcode,me.medicinename;";

                DataView dataBC = new DataView(condb.GetDataTable(sqlLayDuLieu));
                gridControlDataQLTTKhoa.DataSource = dataBC;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #region Xem lich su
        private void itemXemBNDangDT_Click(object sender, EventArgs e)
        {
            try
            {
                HienThiChiTietBNSuDungThuocTaiKhoa(1);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void itemRaVienChuaTT_Click(object sender, EventArgs e)
        {
            try
            {
                HienThiChiTietBNSuDungThuocTaiKhoa(2);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void itemDaThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                HienThiChiTietBNSuDungThuocTaiKhoa(3);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void itemDoanhThu_Click(object sender, EventArgs e)
        {
            try
            {
                HienThiChiTietBNSuDungThuocTaiKhoa(4);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void HienThiChiTietBNSuDungThuocTaiKhoa(int tieuChiLayBaoCao)
        {
            try
            {
                var rowHandle = bandedGridViewDataQLTTKhoa.FocusedRowHandle;
                string thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                // int datatype = 0;
                string bhyt_groupcode = "'09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle'";
                if (radioVatTu.Checked)
                {
                    // datatype = 1;
                    bhyt_groupcode = "'10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle'";
                }
                long departmentgroupid = Utilities.Util_TypeConvertParse.ToInt64(cboKhoa.EditValue.ToString());
                long medicinerefid_org = Utilities.Util_TypeConvertParse.ToInt64(bandedGridViewDataQLTTKhoa.GetRowCellValue(rowHandle, "medicinerefid_org").ToString());
                decimal giaban = Utilities.Util_TypeConvertParse.ToDecimal(bandedGridViewDataQLTTKhoa.GetRowCellValue(rowHandle, "giaban").ToString());

                List<ClassCommon.classMedicineRef> lstMedicine_org = lstMedicineStore.Where(o => o.medicinerefid_org == medicinerefid_org && o.giaban == giaban).ToList();

                if (lstMedicine_org != null && lstMedicine_org.Count>0)
                {
                    BCBNSuDungThuocTaiKhoa.BCBNSuDungThuocTaiKhoaDetail frmChiTietBN = new BCBNSuDungThuocTaiKhoa.BCBNSuDungThuocTaiKhoaDetail(tieuChiLayBaoCao, thoiGianTu, thoiGianDen, departmentgroupid, bhyt_groupcode, lstMedicine_org);
                    frmChiTietBN.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

    }
}
