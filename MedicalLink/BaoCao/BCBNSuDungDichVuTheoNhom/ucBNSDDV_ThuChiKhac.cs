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

namespace MedicalLink.BaoCao
{
    public partial class ucBNSDDV_ThuChiKhac : UserControl
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBNSDDV_ThuChiKhac()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBNSDDV_ThuChiKhac_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

            LoadDuLieuMacDinh();

            if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
            {
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_28_NHOMDV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    mmeMaDV.Text = lstOtherList[0].tools_otherlistvalue;
                }
            }
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                radioXemChiTiet.Checked = true;
                radioXemTongHop.Checked = false;
                gridControlDataBaoCao.DataSource = null;
                gridControlDataBaoCao_TH.DataSource = null;

                gridControlDataBaoCao.Visible = true;
                gridControlDataBaoCao.Dock = DockStyle.Fill;
                gridControlDataBaoCao_TH.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _partitiongroup = "vp.departmentgroupid ";
                string _dsnhomdv = "'" + mmeMaDV.Text + "'";
                string _trangthaivienphi = "";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _partitiongroup = "ser.departmentgroupid ";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //
                if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    _trangthaivienphi = " vienphistatus=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaivienphi = " vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    _trangthaivienphi = " vienphistatus>0 and vienphistatus_vp=1 ";
                }

                //
                if (radioXemTongHop.Checked) //xem tong hop
                {
                    string _sql_timkiem = "SELECT (row_number() over (partition by degp.departmentgroupid order by ser.servicepricename)) as stt, '' as vienphiid, '' as patientid, '' as patientname, '' as namsinh, '' as gioitinh, '' as bhytcode, '' as departmentname, '' as doituongbenhnhanid, '' as servicepricedate, '' as loaidoituong, '' as trangthai, '' as vienphidate, '' as vienphidate_ravien, '' as duyet_ngayduyet_vp, degp.departmentgroupid, degp.departmentgroupname, ser.servicepricecode, ser.servicepricename, (case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt when ser.loaidoituong=1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricemoney, sum(ser.soluong) as soluong, sum((case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt when ser.loaidoituong=1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end)*ser.soluong) as thanhtien, sum(case when (ser.billid_thutien<>0 or ser.billid_clbh_thutien<>0) then (case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.loaidoituong=1 then ser.servicepricemoney_nhandan*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as dathu, '0' as isgroup FROM (select servicepricecode from servicepriceref where servicepricegroupcode in (" + _dsnhomdv + ")) serf inner join (select vienphiid,servicepricecode,servicepricename,loaidoituong,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,soluong,departmentgroupid,billid_thutien,billid_clbh_thutien from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') " + _tieuchi_ser + ") ser on ser.servicepricecode=serf.servicepricecode inner join (select vienphiid,doituongbenhnhanid,departmentgroupid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where " + _trangthaivienphi + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname) degp on degp.departmentgroupid=" + _partitiongroup + " GROUP BY degp.departmentgroupid,degp.departmentgroupname,ser.servicepricecode,ser.servicepricename,(case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt when ser.loaidoituong=1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end);";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBaoCao_TH.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBaoCao_TH.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else //xem chi tiet
                {
                    string _sql_timkiem = " SELECT (row_number() over (partition by degp.departmentgroupid order by hsba.patientname)) as stt, vp.patientid, vp.vienphiid, hsba.patientname, to_char(hsba.birthday, 'yyyy') as namsinh, hsba.gioitinhname as gioitinh, hsba.bhytcode, degp.departmentgroupid, degp.departmentgroupname, (case vp.doituongbenhnhanid when 1 then 'BHYT' when 2 then 'Viện phí' when 3 then 'Dịch vụ' when 4 then 'Người nước ngoài' when 5 then 'Miễn phí' when 6 then 'Hợp đồng' end) as doituongbenhnhanid, ser.servicepricecode, ser.servicepricename, ser.servicepricedate, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, (case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt when ser.loaidoituong=1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricemoney, ser.soluong, ((case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt when ser.loaidoituong=1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end)*ser.soluong) as thanhtien, (case when (ser.billid_thutien<>0 or ser.billid_clbh_thutien<>0) then (case when ser.loaidoituong=0 then ser.servicepricemoney_bhyt*ser.soluong when ser.loaidoituong=1 then ser.servicepricemoney_nhandan*ser.soluong else ser.servicepricemoney*ser.soluong end) else 0 end) as dathu, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as trangthai, vp.vienphidate, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, '0' as isgroup FROM (select servicepricecode from servicepriceref where servicepricegroupcode in (" + _dsnhomdv + ")) serf inner join (select vienphiid,servicepricecode,servicepricename,loaidoituong,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,soluong,departmentgroupid,servicepricedate,billid_thutien,billid_clbh_thutien from serviceprice where bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC') " + _tieuchi_ser + ") ser on ser.servicepricecode=serf.servicepricecode inner join (select vienphiid,patientid,hosobenhanid,doituongbenhnhanid,departmentgroupid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where " + _trangthaivienphi + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select hosobenhanid,patientname,birthday,gioitinhname,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select departmentgroupid,departmentgroupname from departmentgroup order by departmentgroupname) degp on degp.departmentgroupid=" + _partitiongroup + "; ";

                    this.dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataBaoCao.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDataBaoCao.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }


            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Export and Print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_SuDungDichVu_ChiPhiKhac_TongHop.xlsx";
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_SuDungDichVu_ChiPhiKhac_ChiTiet.xlsx";
                }
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCBNSDDV_ChiPhiKhacDTO> lstData_XuatBaoCao = new List<ClassCommon.BCBNSDDV_ChiPhiKhacDTO>();
                List<ClassCommon.BCBNSDDV_ChiPhiKhacDTO> lstDataDoanhThu = new List<ClassCommon.BCBNSDDV_ChiPhiKhacDTO>();

                lstDataDoanhThu = DataTables.DataTableToList<ClassCommon.BCBNSDDV_ChiPhiKhacDTO>(this.dataBaoCao);

                List<ClassCommon.BCBNSDDV_ChiPhiKhacDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BCBNSDDV_ChiPhiKhacDTO data_groupname = new ClassCommon.BCBNSDDV_ChiPhiKhacDTO();

                    List<ClassCommon.BCBNSDDV_ChiPhiKhacDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.departmentgroupid == item_group.departmentgroupid).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;
                    decimal sum_dathu = 0;

                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                        sum_dathu += item_tinhsum.dathu;
                    }

                    data_groupname.departmentgroupid = item_group.departmentgroupid;
                    data_groupname.stt = item_group.departmentgroupname;
                    data_groupname.soluong = sum_soluong;
                    data_groupname.thanhtien = sum_thanhtien;
                    data_groupname.dathu = sum_dathu;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_SuDungDichVu_ChiPhiKhac_TongHop.xlsx";
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_SuDungDichVu_ChiPhiKhac_ChiTiet.xlsx";
                }
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }

        #endregion

        #region Event Change
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlDataBaoCao_TH.Visible = true;
                    gridControlDataBaoCao_TH.DataSource = null;
                    gridControlDataBaoCao_TH.Dock = DockStyle.Fill;
                    gridControlDataBaoCao.Visible = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void radioXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemChiTiet.Checked)
                {
                    radioXemTongHop.Checked = false;
                    gridControlDataBaoCao.Visible = true;
                    gridControlDataBaoCao.DataSource = null;
                    gridControlDataBaoCao.Dock = DockStyle.Fill;
                    gridControlDataBaoCao_TH.Visible = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        private void cboTrangThaiVienPhi_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
