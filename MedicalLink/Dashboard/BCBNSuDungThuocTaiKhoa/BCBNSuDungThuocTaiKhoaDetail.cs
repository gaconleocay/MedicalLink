using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MedicalLink.Dashboard.BCBNSuDungThuocTaiKhoa
{
    public partial class BCBNSuDungThuocTaiKhoaDetail : Form
    {
        #region Khai bao
        private int tieuChiLayBaoCao { get; set; }
        private string dateTuNgay { get; set; }
        private string dateDenNgay { get; set; }
        private long departmentgroupid { get; set; }
        private string bhyt_groupcode { get; set; }
        List<ClassCommon.classMedicineRef> lstMedicineCurrent { get; set; }

        DataTable dataExport = new DataTable();
        private string lstmedicinecode_string { get; set; }
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #endregion

        public BCBNSuDungThuocTaiKhoaDetail()
        {
            InitializeComponent();
        }
        public BCBNSuDungThuocTaiKhoaDetail(int tieuChiLayBaoCao, string thoiGianTu, string thoiGianDen, long departmentgroupid, string bhyt_groupcode, List<ClassCommon.classMedicineRef> lstMedicineStore)
        {
            try
            {
                InitializeComponent();
                //=1: BN dang dieu tri
                //=2: BN da ra vien chua thanh toan
                //=3: BN da thanh toan
                //=4 doanh thu
                this.tieuChiLayBaoCao = tieuChiLayBaoCao;
                this.dateTuNgay = thoiGianTu;
                this.dateDenNgay = thoiGianDen;
                this.departmentgroupid = departmentgroupid;
                this.bhyt_groupcode = bhyt_groupcode;
                this.lstMedicineCurrent = lstMedicineStore;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Load
        private void BCBNSuDungThuocTaiKhoaDetail_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                GetDanhSachCacLoThuocCon();
                if (this.lstmedicinecode_string != null)
                {
                    LoadDataToGrid();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void GetDanhSachCacLoThuocCon()
        {
            try
            {
                if (this.lstMedicineCurrent != null && this.lstMedicineCurrent.Count > 0)
                {
                    for (int i = 0; i < lstMedicineCurrent.Count - 1; i++)
                    {
                        this.lstmedicinecode_string += "'" + lstMedicineCurrent[i].medicinecode + "',";
                    }
                    this.lstmedicinecode_string += "'" + lstMedicineCurrent[lstMedicineCurrent.Count - 1].medicinecode + "'";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void LoadDataToGrid()
        {
            try
            {
                DataView dataBNDetail = new DataView();
                string sqlGetData = "";
                switch (this.tieuChiLayBaoCao)
                {
                    case 1: //=1: BN dang dieu tri
                        sqlGetData = "SELECT depg.departmentgroupname, vp.patientid, vp.vienphiid, hsba.patientname, bhyt.bhytcode,  ser.servicepricecode,ser.servicepricename, (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong,ser.servicepricedate FROM serviceprice ser inner join vienphi vp on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + this.bhyt_groupcode + ") and ser.thuockhobanle=0  inner join departmentgroup depg on ser.departmentgroupid=depg.departmentgroupid  inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid  inner join bhyt bhyt on vp.bhytid=bhyt.bhytid  WHERE vp.vienphistatus=0 and vp.departmentgroupid=" + this.departmentgroupid + " and ser.servicepricecode in (" + this.lstmedicinecode_string + ") and ser.loaidoituong in (0,1,3,4,6,8,9) ORDER BY ser.servicepricedate, vp.vienphiid;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN SỬ DỤNG THUỐC/VẬT TƯ - ĐANG ĐIỀU TRỊ";
                        break;

                    case 2: //=2: BN da ra vien chua thanh toan
                        sqlGetData = "SELECT depg.departmentgroupname, vp.patientid, vp.vienphiid, hsba.patientname, bhyt.bhytcode, ser.servicepricecode,ser.servicepricename, (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong,ser.servicepricedate FROM serviceprice ser inner join vienphi vp on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + this.bhyt_groupcode + ") and ser.thuockhobanle=0 inner join departmentgroup depg on ser.departmentgroupid=depg.departmentgroupid inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join bhyt bhyt on vp.bhytid=bhyt.bhytid WHERE COALESCE(vp.vienphistatus_vp,0)=0 and vp.vienphistatus<>0 and vp.departmentgroupid=" + this.departmentgroupid + " and ser.servicepricecode in (" + this.lstmedicinecode_string + ") and ser.loaidoituong in (0,1,3,4,6,8,9) ORDER BY ser.servicepricedate, vp.vienphiid;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN SỬ DỤNG THUỐC/VẬT TƯ - RA VIỆN CHƯA THANH TOÁN";
                        break;

                    case 3:  //=3: BN da thanh toan
                        sqlGetData = "SELECT depg.departmentgroupname, vp.patientid, vp.vienphiid, hsba.patientname, bhyt.bhytcode, ser.servicepricecode,ser.servicepricename, (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong,ser.servicepricedate FROM serviceprice ser inner join vienphi vp on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + this.bhyt_groupcode + ") and ser.thuockhobanle=0 and ser.servicepricedate<'" + dateDenNgay + "' inner join departmentgroup depg on ser.departmentgroupid=depg.departmentgroupid inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join bhyt bhyt on vp.bhytid=bhyt.bhytid WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + this.dateTuNgay + "' and vp.duyet_ngayduyet_vp<='" + dateDenNgay + "' and vp.departmentgroupid=" + this.departmentgroupid + " and ser.servicepricecode in (" + this.lstmedicinecode_string + ") and ser.loaidoituong in (0,1,3,4,6,8,9) ORDER BY ser.servicepricedate, vp.vienphiid;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN SỬ DỤNG THUỐC/VẬT TƯ - ĐÃ THANH TOÁN";

                        break;
                    case 4:  //=4 doanh thu
                        sqlGetData = " SELECT depg.departmentgroupname, vp.patientid, vp.vienphiid, hsba.patientname, bhyt.bhytcode, ser.servicepricecode,ser.servicepricename, (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) as soluong,ser.servicepricedate FROM serviceprice ser inner join vienphi vp on vp.vienphiid=ser.vienphiid and ser.bhyt_groupcode in (" + this.bhyt_groupcode + ") and ser.thuockhobanle=0 and ser.servicepricedate<'" + dateDenNgay + "' inner join departmentgroup depg on ser.departmentgroupid=depg.departmentgroupid inner join hosobenhan hsba on hsba.hosobenhanid=vp.hosobenhanid inner join bhyt bhyt on vp.bhytid=bhyt.bhytid WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp>='" + this.dateTuNgay + "' and vp.duyet_ngayduyet_vp<='" + dateDenNgay + "' and ser.departmentgroupid=" + this.departmentgroupid + " and ser.servicepricecode in (" + this.lstmedicinecode_string + ") and ser.loaidoituong in (0,1,3,4,6,8,9) ORDER BY ser.servicepricedate, vp.vienphiid;";
                        lblTenThongTinChiTiet.Text = "DANH SÁCH CHI TIẾT BỆNH NHÂN SỬ DỤNG THUỐC/VẬT TƯ - DOANH THU";
                        break;
                    default:
                        break;
                }
               // dataBNDetail = new DataView(condb.getDataTable(sqlGetData));
                dataExport = condb.GetDataTable(sqlGetData);
                gridControlBNDetail.DataSource = dataExport;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewBNDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewBNDetail.RowCount > 0)
                {
                    try
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                            if (saveDialog.ShowDialog() != DialogResult.Cancel)
                            {
                                string exportFilePath = saveDialog.FileName;
                                string fileExtenstion = new FileInfo(exportFilePath).Extension;

                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        gridViewBNDetail.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewBNDetail.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewBNDetail.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewBNDetail.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewBNDetail.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewBNDetail.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        private void gridViewBNDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }



    }
}
