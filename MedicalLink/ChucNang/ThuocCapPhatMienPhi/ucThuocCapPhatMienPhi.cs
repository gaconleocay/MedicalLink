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
using MedicalLink.Utilities.GridControl;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucThuocCapPhatMienPhi : UserControl
    {
        #region Khai bao
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        #endregion

        public ucThuocCapPhatMienPhi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _trangthaibenhan = "";
                string _tieuchi_vp = "";
                string _tieuchi_ser = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    _trangthaibenhan = " and vienphistatus=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaibenhan = " and vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    _trangthaibenhan = " and vienphistatus>0 and vienphistatus_vp=1 ";
                }

                if (cboTieuChi.Text == "Thời gian vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Thời gian ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Thời gian chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Thời gian thanh toán")
                {
                    _tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                string sql_timkiem = "SELECT row_number () over (order by ser.servicepricedate) as stt, vp.vienphiid, vp.patientid, hsba.patientname, bh.bhytcode, degp.departmentgroupname, de.departmentname, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus, vp.vienphidate, (case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.vienphistatus>0 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, ser.servicepriceid, ser.maubenhphamid, ser.servicepricedate, ser.servicepricecode, ser.servicepricename, ser.servicepricemoney_bhyt, ser.servicepricemoney, ser.soluong, (ser.servicepricemoney*ser.soluong) as thanhtien, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, (case ser.loaidoituong_org when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong_org, kcd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, ser.loaidoituong_remark FROM (select * from serviceprice where bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and loaidoituong=9 and loaidoituong_remark='[AUTO] Kho thuốc miễn phí' " + _tieuchi_ser + ") ser inner join (select vienphiid,patientid,bhytid,hosobenhanid,vienphidate,vienphidate_ravien,vienphistatus,vienphistatus_vp,duyet_ngayduyet_vp,departmentgroupid,departmentid from vienphi where 1=1 " + _trangthaibenhan + _tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select hosobenhanid,patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select bhytid,bhytcode from bhyt) bh on bh.bhytid=vp.bhytid left join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=vp.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=vp.departmentid left join (select departmentgroupid,departmentgroupname from departmentgroup) kcd on kcd.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) pcd on pcd.departmentid=ser.departmentid; ";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
        private void gridViewBaoCao_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Events
        private void gridViewBaoCao_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (cboTrangThaiVienPhi.Text == "Đang điều trị" || cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        //GridView view = sender as GridView;
                        e.Menu.Items.Clear();
                        DXMenuItem itemMoBenhAn = new DXMenuItem("Cập nhật loại hình TT=Đối tượng BN (tất cả)");
                        itemMoBenhAn.Image = imageCollectionMBA.Images[0];
                        itemMoBenhAn.Click += new EventHandler(CapNhatLoaiHinhThanhToan_Click);
                        e.Menu.Items.Add(itemMoBenhAn);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        internal void CapNhatLoaiHinhThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật loại hình thanh toán=Đối tượng bệnh nhân ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                try
                {
                    string _lstservicepriceId = "";
                    for (int i = 0; i < gridViewBaoCao.RowCount - 1; i++)
                    {
                        _lstservicepriceId += gridViewBaoCao.GetRowCellValue(i, "servicepriceid").ToString() + ",";
                    }
                    _lstservicepriceId += gridViewBaoCao.GetRowCellValue(gridViewBaoCao.RowCount - 1, "servicepriceid").ToString();

                    string _sqlUpdate = "UPDATE serviceprice SET loaidoituong=loaidoituong_org WHERE servicepriceid in (" + _lstservicepriceId + ");";
                    if (condb.ExecuteNonQuery_HIS(_sqlUpdate))
                    {
                        //Log vào DB
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Cập nhật đối tượng thanh toán thành công SL=[" + gridViewBaoCao.RowCount + "]; " + _lstservicepriceId + "' ,'" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', 'TOOL_23');";
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        MessageBox.Show("Cập nhật đối tượng thanh toán thành công SL=[" + gridViewBaoCao.RowCount + "]", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnTimKiem.PerformClick();
                    }
                }
                catch (Exception ex)
                {
                    O2S_Common.Logging.LogSystem.Error(ex);
                }
                SplashScreenManager.CloseForm();
            }
        }
        #endregion

        #region Export and Print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                string fileTemplatePath = "TOOL_23_BNCapPhatThuocVatTuMienPhi.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                string fileTemplatePath = "TOOL_23_BNCapPhatThuocVatTuMienPhi.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }



        #endregion


    }
}
