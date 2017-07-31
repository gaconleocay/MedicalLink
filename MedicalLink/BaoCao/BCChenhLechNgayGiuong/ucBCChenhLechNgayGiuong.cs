using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Utilities.GridControl;
using MedicalLink.Utilities;
using MedicalLink.DatabaseProcess;

namespace MedicalLink.BaoCao
{
    public partial class ucBCChenhLechNgayGiuong : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<ClassCommon.BCChenhLechNgayGiuongDTO> lstDataBaoCaoCurrent { get; set; }
        public ucBCChenhLechNgayGiuong()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCChenhLechNgayGiuong_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlDataBaoCao.DataSource = null;
            LoadDanhSachNhomDichVu();
        }
        private void LoadDanhSachNhomDichVu()
        {
            try
            {
                if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                {
                    List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_26_NHOMDV").ToList();
                    if (lstOtherList != null && lstOtherList.Count > 0)
                    {
                        mmeMaNhomDV.Text = lstOtherList[0].tools_otherlistvalue;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tieuchi_vp = "";
                string tieuchi_ser = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = "where servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " where vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " where vienphistatus>0 and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi_vp = " where vienphistatus_vp=1 and vienphistatus>0 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                string sql_timkiem = " select (row_number() OVER (PARTITION BY kcden.departmentgroupname ORDER BY ser.servicepricecode)) as stt, vp.patientid, vp.vienphiid, hsba.patientname, bh.bhytcode, mrd.backdepartmentid as khoachuyendenid, kcden.departmentgroupname as khoachuyenden, krv.departmentgroupname as khoaravien, prv.departmentname as phongravien, vp.vienphidate, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, ser.servicepricecode, ser.servicepricename, ser.servicepricedate, kcd.departmentgroupname as khoachidinh, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, ser.soluong, (case vp.doituongbenhnhanid when 4 then ser.servicepricemoney_nuocngoai else (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) end) as servicepricemoney, ser.servicepricemoney_bhyt, 0 as thu_chenhlech, 0 as bn_thanhtoan, 0 as bhyt_thanhtoan, 0 as tongthu, vp.loaivienphiid, vp.bhyt_tuyenbenhvien, vp.bhyt_thangluongtoithieu, bh.du5nam6thangluongcoban, bh.bhyt_loaiid from (select patientid,vienphiid,hosobenhanid,bhytid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,doituongbenhnhanid,departmentgroupid,departmentid,loaivienphiid,bhyt_tuyenbenhvien,bhyt_thangluongtoithieu from vienphi " + tieuchi_vp + ") vp inner join (select vienphiid,medicalrecordid,departmentgroupid,servicepricecode,servicepricename,servicepricedate,loaidoituong,soluong,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai from serviceprice " + tieuchi_ser + ") ser on ser.vienphiid=vp.vienphiid inner join (select servicepricecode from servicepriceref where servicepricegroupcode='" + mmeMaNhomDV.Text + "') serf on serf.servicepricecode=ser.servicepricecode inner join (select medicalrecordid,backdepartmentid from medicalrecord) mrd on mrd.medicalrecordid=ser.medicalrecordid inner join (select hosobenhanid,patientname,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select bhytid,bhyt_loaiid,bhytcode,du5nam6thangluongcoban from bhyt) bh on bh.bhytid=vp.bhytid inner join (select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname) kcden ON kcden.departmentgroupid=mrd.backdepartmentid inner join (select departmentgroupid,departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=vp.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv ON prv.departmentid=vp.departmentid inner join (select departmentgroupid,departmentgroupname from departmentgroup) kcd ON kcd.departmentgroupid=ser.departmentgroupid;  ";
                DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);

                this.lstDataBaoCaoCurrent = new List<ClassCommon.BCChenhLechNgayGiuongDTO>();
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    this.lstDataBaoCaoCurrent = Util_DataTable.DataTableToList<ClassCommon.BCChenhLechNgayGiuongDTO>(dataBaoCao);

                    foreach (var item_data in this.lstDataBaoCaoCurrent)
                    {
                        ClassCommon.TinhBHYTThanhToanDTO tinhBHYT = new ClassCommon.TinhBHYTThanhToanDTO();
                        tinhBHYT.bhytcode = item_data.bhytcode;
                        tinhBHYT.bhyt_loaiid = item_data.bhyt_loaiid;
                        tinhBHYT.bhyt_tuyenbenhvien = item_data.bhyt_tuyenbenhvien;
                        tinhBHYT.du5nam6thangluongcoban = item_data.du5nam6thangluongcoban;
                        tinhBHYT.loaivienphiid = item_data.loaivienphiid;
                        tinhBHYT.thangluongcoban = item_data.thangluongcoban;

                        item_data.thu_chenhlech = (item_data.servicepricemoney - item_data.servicepricemoney_bhyt) * item_data.soluong;
                        int tyleBN_Thanhtoan = TinhMucHuongBHYT.TinhMucHuongTheoTheBHYT(tinhBHYT);
                        item_data.bhyt_thanhtoan = (((decimal)tyleBN_Thanhtoan/100) * item_data.servicepricemoney_bhyt) * item_data.soluong;
                        item_data.bn_thanhtoan = (item_data.servicepricemoney_bhyt * item_data.soluong) - item_data.bhyt_thanhtoan;
                        item_data.tongthu = item_data.thu_chenhlech + item_data.bn_thanhtoan;
                    }
                    gridControlDataBaoCao.DataSource = this.lstDataBaoCaoCurrent;
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Export
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_ChenhLechNgayGiuong_KhoaQuocTe.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.BCChenhLechNgayGiuongDTO> lstData_XuatBaoCao = new List<ClassCommon.BCChenhLechNgayGiuongDTO>();
                List<ClassCommon.BCChenhLechNgayGiuongDTO> lstDataDoanhThu = new List<ClassCommon.BCChenhLechNgayGiuongDTO>();
                lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.BCChenhLechNgayGiuongDTO>(Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao));

                List<ClassCommon.BCChenhLechNgayGiuongDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.khoachuyendenid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BCChenhLechNgayGiuongDTO data_groupname = new ClassCommon.BCChenhLechNgayGiuongDTO();
                    List<ClassCommon.BCChenhLechNgayGiuongDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.khoachuyendenid == item_group.khoachuyendenid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thu_chenhlech = 0;
                    decimal sum_bn_thanhtoan = 0;
                    decimal sum_bhyt_thanhtoan = 0;
                    decimal sum_tongthu = 0;
                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thu_chenhlech += item_tinhsum.thu_chenhlech;
                        sum_bn_thanhtoan += item_tinhsum.bn_thanhtoan;
                        sum_bhyt_thanhtoan += item_tinhsum.bhyt_thanhtoan;
                        sum_tongthu += item_tinhsum.tongthu;
                    }

                    data_groupname.khoachuyendenid = item_group.khoachuyendenid;
                    data_groupname.stt = item_group.khoachuyenden;
                    //data_groupname.vienphidate = null;
                    //data_groupname.vienphidate_ravien = null;
                    //data_groupname.duyet_ngayduyet_vp = null;
                    //data_groupname.servicepricedate = null;
                    data_groupname.soluong = sum_soluong;
                    data_groupname.thu_chenhlech = sum_thu_chenhlech;
                    data_groupname.bn_thanhtoan = sum_bn_thanhtoan;
                    data_groupname.bhyt_thanhtoan = sum_bhyt_thanhtoan;
                    data_groupname.tongthu = sum_tongthu;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }

        #endregion
        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_ChenhLechNgayGiuong_KhoaQuocTe.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }



    }
}
