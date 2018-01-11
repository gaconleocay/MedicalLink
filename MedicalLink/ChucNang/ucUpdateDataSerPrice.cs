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
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class ucUpdateDataSerPrice : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucUpdateDataSerPrice()
        {
            InitializeComponent();
        }

        #region Custom       
        private void gridViewDichVu_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion

        #region Load
        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateTuNgay.Value = date_tu;
            DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            dateDenNgay.Value = date_den;
        }
        #endregion

        #region Events

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDichVu.RowCount > 0)
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
                                    gridViewDichVu.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridViewDichVu.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridViewDichVu.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridViewDichVu.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridViewDichVu.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridViewDichVu.ExportToMht(exportFilePath);
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
        private void btnSelect_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string trangthaiVP = "";
                gridControlDichVu.DataSource = null;
                string tieuchi_ser = "";
                string tieuchi_vp = "";
                if (cbbChonKieu.Text != "" && cbbTrangThaiVP.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                    if (cbbTrangThaiVP.Text.Trim() == "Đang điều trị")
                    {
                        trangthaiVP = " vienphistatus=0 ";
                    }
                    else if (cbbTrangThaiVP.Text.Trim() == "Đóng BA nhưng chưa duyệt VP")
                    {
                        trangthaiVP = " vienphistatus=1 and COALESCE(vienphistatus_vp,0)=0 ";
                    }
                    else if (cbbTrangThaiVP.Text.Trim() == "Đã duyệt viện phí")
                    {
                        trangthaiVP = " vienphistatus_vp=1 ";
                    }

                    if (cboTieuChi.Text == "Theo ngày chỉ định")
                    {
                        tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày ra viện")
                    {
                        tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày vào viện")
                    {
                        tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    }
                    else if (cboTieuChi.Text == "Theo ngày duyệt viện phí")
                    {
                        tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                    }

                    switch (cbbChonKieu.Text.Trim())
                    {
                        case "Nhóm BHYT-Thuốc/VT trắng":
                            {
                                try
                                {
                                    string sqlquerythuoc = "SELECT ser.servicepriceid as servicepriceid, ser.medicalrecordid as madieutri, ser.vienphiid as mavienphi, ser.hosobenhanid as hosobenhan, ser.maubenhphamid as maubenhpham, ser.servicepricecode as madichvu, ser.servicepricename as tendichvu, ser.servicepricemoney as gia, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_nhandan, ser.servicepricemoney_nuocngoai as gia_nnn, serf.servicepricefee, serf.servicepricefeebhyt, serf.servicepricefeenhandan, serf.servicepricefeenuocngoai, ser.soluong as soluong, ser.bhyt_groupcode as bhyt_groupcode, ser.servicepricedate as ngaychidinh, vp.vienphidate, (case when vp.vienphistatus<>0 then vienphidate_ravien end) as vienphidate_ravien, (case when coalesce(vp.vienphistatus_vp,0)=1 then duyet_ngayduyet_vp end) as duyet_ngayduyet_vp FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where (bhyt_groupcode is null or bhyt_groupcode='') " + tieuchi_ser + ") ser inner join (select medicinecode from medicine_ref) serf on serf.medicinecode=ser.servicepricecode inner join (select vienphiid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where " + trangthaiVP + tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid ORDER BY ser.servicepriceid;";
                                    DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));

                                    // Hiển thị
                                    if (dv_bhytgroup.Count > 0)
                                    {
                                        gridControlDichVu.DataSource = dv_bhytgroup;
                                    }
                                    else
                                    {
                                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                                        frmthongbao.Show();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                                }
                                break;
                            }
                        case "Nhóm BHYT-Dịch vụ trắng":
                            {
                                try
                                {
                                    string sqlquerythuoc = "SELECT ser.servicepriceid as servicepriceid, ser.medicalrecordid as madieutri, ser.vienphiid as mavienphi, ser.hosobenhanid as hosobenhan, ser.maubenhphamid as maubenhpham, ser.servicepricecode as madichvu, ser.servicepricename as tendichvu, ser.servicepricemoney as gia, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_nhandan, ser.servicepricemoney_nuocngoai as gia_nnn, serf.servicepricefee, serf.servicepricefeebhyt, serf.servicepricefeenhandan, serf.servicepricefeenuocngoai, ser.soluong as soluong, ser.bhyt_groupcode as bhyt_groupcode, ser.servicepricedate as ngaychidinh, vp.vienphidate, (case when vp.vienphistatus<>0 then vienphidate_ravien end) as vienphidate_ravien, (case when coalesce(vp.vienphistatus_vp,0)=1 then duyet_ngayduyet_vp end) as duyet_ngayduyet_vp FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where (bhyt_groupcode is null or bhyt_groupcode='') " + tieuchi_ser + ") ser inner join (select servicepricecode from servicepriceref) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where " + trangthaiVP + tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid ORDER BY ser.servicepriceid; ";
                                    DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));

                                    // Hiển thị
                                    if (dv_bhytgroup.Count > 0)
                                    {
                                        gridControlDichVu.DataSource = dv_bhytgroup;
                                    }
                                    else
                                    {
                                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                        frmthongbao.Show();
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Có lỗi xảy ra " + ex, "Thông báo");
                                }
                                break;
                            }
                        case "Sai giá BHYT":
                            {
                                string sqlquerythuoc = "SELECT ser.servicepriceid as servicepriceid, ser.medicalrecordid as madieutri, ser.vienphiid as mavienphi, ser.hosobenhanid as hosobenhan, ser.maubenhphamid as maubenhpham, ser.servicepricecode as madichvu, ser.servicepricename as tendichvu, ser.servicepricemoney as gia, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_nhandan, ser.servicepricemoney_nuocngoai as gia_nnn, serf.servicepricefee, serf.servicepricefeebhyt, serf.servicepricefeenhandan, serf.servicepricefeenuocngoai, ser.soluong as soluong, ser.bhyt_groupcode as bhyt_groupcode, ser.servicepricedate as ngaychidinh, vp.vienphidate, (case when vp.vienphistatus<>0 then vienphidate_ravien end) as vienphidate_ravien, (case when coalesce(vp.vienphistatus_vp,0)=1 then duyet_ngayduyet_vp end) as duyet_ngayduyet_vp FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where bhyt_groupcode<>'' " + tieuchi_ser + ") ser inner join (select servicepricecode,servicepricefee,servicepricefeebhyt from servicepriceref) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where " + trangthaiVP + tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid WHERE cast(ser.servicepricemoney_bhyt as text)<>serf.servicepricefeebhyt and serf.servicepricefeebhyt<>'' ORDER BY ser.servicepriceid;  ";
                                DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));

                                // Hiển thị
                                if (dv_bhytgroup.Count > 0)
                                {
                                    gridControlDichVu.DataSource = dv_bhytgroup;
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        case "Sai giá viện phí":
                            {
                                string sqlquerythuoc = "SELECT ser.servicepriceid as servicepriceid, ser.medicalrecordid as madieutri, ser.vienphiid as mavienphi, ser.hosobenhanid as hosobenhan, ser.maubenhphamid as maubenhpham, ser.servicepricecode as madichvu, ser.servicepricename as tendichvu, ser.servicepricemoney as gia, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_nhandan, ser.servicepricemoney_nuocngoai as gia_nnn, serf.servicepricefee, serf.servicepricefeebhyt, serf.servicepricefeenhandan, serf.servicepricefeenuocngoai, ser.soluong as soluong, ser.bhyt_groupcode as bhyt_groupcode, ser.servicepricedate as ngaychidinh, vp.vienphidate, (case when vp.vienphistatus<>0 then vienphidate_ravien end) as vienphidate_ravien, (case when coalesce(vp.vienphistatus_vp,0)=1 then duyet_ngayduyet_vp end) as duyet_ngayduyet_vp FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where bhyt_groupcode<>'' " + tieuchi_ser + ") ser inner join (select servicepricecode,servicepricefee,servicepricefeebhyt,servicepricefeenhandan,servicepricefeenuocngoai from servicepriceref) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where " + trangthaiVP + tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid WHERE cast(ser.servicepricemoney_nhandan as text)<>serf.servicepricefeenhandan and serf.servicepricefeenhandan<>'' ORDER BY ser.servicepriceid;";
                                DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));

                                // Hiển thị
                                if (dv_bhytgroup.Count > 0)
                                {
                                    gridControlDichVu.DataSource = dv_bhytgroup;
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        case "Sai giá yêu cầu":
                            {
                                string sqlquerythuoc = "SELECT ser.servicepriceid as servicepriceid, ser.medicalrecordid as madieutri, ser.vienphiid as mavienphi, ser.hosobenhanid as hosobenhan, ser.maubenhphamid as maubenhpham, ser.servicepricecode as madichvu, ser.servicepricename as tendichvu, ser.servicepricemoney as gia, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_nhandan, ser.servicepricemoney_nuocngoai as gia_nnn, serf.servicepricefee, serf.servicepricefeebhyt, serf.servicepricefeenhandan, serf.servicepricefeenuocngoai, ser.soluong as soluong, ser.bhyt_groupcode as bhyt_groupcode, ser.servicepricedate as ngaychidinh, vp.vienphidate, (case when vp.vienphistatus<>0 then vienphidate_ravien end) as vienphidate_ravien, (case when coalesce(vp.vienphistatus_vp,0)=1 then duyet_ngayduyet_vp end) as duyet_ngayduyet_vp FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where bhyt_groupcode<>'' " + tieuchi_ser + ") ser inner join (select servicepricecode,servicepricefee,servicepricefeebhyt from servicepriceref) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where " + trangthaiVP + tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid WHERE cast(ser.servicepricemoney as text)<>serf.servicepricefee and serf.servicepricefee<>'' ORDER BY ser.servicepriceid;  ";
                                DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));

                                // Hiển thị
                                if (dv_bhytgroup.Count > 0)
                                {
                                    gridControlDichVu.DataSource = dv_bhytgroup;
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        case "Sai giá người nước ngoài":
                            {
                                string sqlquerythuoc = "SELECT ser.servicepriceid as servicepriceid, ser.medicalrecordid as madieutri, ser.vienphiid as mavienphi, ser.hosobenhanid as hosobenhan, ser.maubenhphamid as maubenhpham, ser.servicepricecode as madichvu, ser.servicepricename as tendichvu, ser.servicepricemoney as gia, ser.servicepricemoney_bhyt as gia_bhyt, ser.servicepricemoney_nhandan as gia_nhandan, ser.servicepricemoney_nuocngoai as gia_nnn, serf.servicepricefee, serf.servicepricefeebhyt, serf.servicepricefeenhandan, serf.servicepricefeenuocngoai, ser.soluong as soluong, ser.bhyt_groupcode as bhyt_groupcode, ser.servicepricedate as ngaychidinh, vp.vienphidate, (case when vp.vienphistatus<>0 then vienphidate_ravien end) as vienphidate_ravien, (case when coalesce(vp.vienphistatus_vp,0)=1 then duyet_ngayduyet_vp end) as duyet_ngayduyet_vp FROM (select servicepriceid,medicalrecordid,vienphiid,hosobenhanid,maubenhphamid,servicepricecode,servicepricename,servicepricemoney,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney_nuocngoai,soluong,bhyt_groupcode,servicepricedate from serviceprice where bhyt_groupcode<>'' " + tieuchi_ser + ") ser inner join (select servicepricecode,servicepricefee,servicepricefeebhyt,servicepricefeenhandan,servicepricefeenuocngoai from servicepriceref) serf on serf.servicepricecode=ser.servicepricecode inner join (select vienphiid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where " + trangthaiVP + tieuchi_vp + ") vp on vp.vienphiid=ser.vienphiid WHERE cast(ser.servicepricemoney_nuocngoai as text)<>serf.servicepricefeenuocngoai and serf.servicepricefeenuocngoai<>'' ORDER BY ser.servicepriceid;";
                                DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));

                                // Hiển thị
                                if (dv_bhytgroup.Count > 0)
                                {
                                    gridControlDichVu.DataSource = dv_bhytgroup;
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void btnUpdateDVOK_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn cập nhật?\nHãy xuất file excel backup trước khi cập nhật", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                try
                {
                    int dem_sl = 0;
                    // String lưu 1 mảng các ServicepriceID để load lại dữ liệu
                    string arrayserID = "";
                    // Lấy thời gian hiện tại
                    String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    switch (cbbChonKieu.Text.Trim())
                    {
                        case "Nhóm BHYT-Thuốc/VT trắng":
                            {
                                if (gridViewDichVu.RowCount > 0)
                                {
                                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                    {
                                        //Tìm mã bhytgroupcode từ medicine_ref
                                        string sqlget_bhytgroupcode = "SELECT medicine_ref.bhyt_groupcode as bhyt_groupcode FROM serviceprice, medicine_ref WHERE medicine_ref.medicinecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                        DataView dv_bhytgroupcode = new DataView(condb.GetDataTable_HIS(sqlget_bhytgroupcode));
                                        if (dv_bhytgroupcode.Count > 0)
                                        {
                                            try
                                            {
                                                string update_bhyt_thuoc = "UPDATE serviceprice SET bhyt_groupcode = '" + dv_bhytgroupcode[0]["bhyt_groupcode"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                                condb.ExecuteNonQuery_HIS(update_bhyt_thuoc);
                                                dem_sl = dem_sl + 1;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }

                        case "Nhóm BHYT-Dịch vụ trắng":
                            {
                                if (gridViewDichVu.RowCount > 0)
                                {
                                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                    {
                                        //Tìm mã bhytgroupcode từ medicine_ref
                                        string sqlget_bhytgroupcode = "SELECT servicepriceref.bhyt_groupcode as bhyt_groupcode FROM serviceprice, servicepriceref WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                        DataView dv_bhytgroupcode = new DataView(condb.GetDataTable_HIS(sqlget_bhytgroupcode));
                                        if (dv_bhytgroupcode.Count > 0)
                                        {
                                            try
                                            {
                                                string update_bhyt_dv = "UPDATE serviceprice SET bhyt_groupcode = '" + dv_bhytgroupcode[0]["bhyt_groupcode"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                                condb.ExecuteNonQuery_HIS(update_bhyt_dv);
                                                dem_sl = dem_sl + 1;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }

                        //
                        case "Sai giá BHYT":
                            {
                                if (gridViewDichVu.RowCount > 0)
                                {
                                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                    {
                                        //Tìm mã bhytgroupcode từ medicine_ref
                                        string sqlget_servicepricefeebhyt = "SELECT servicepriceref.servicepricefeebhyt as servicepricefeebhyt FROM serviceprice, servicepriceref WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                        DataView dv_servicepricefeebhyt = new DataView(condb.GetDataTable_HIS(sqlget_servicepricefeebhyt));
                                        if (dv_servicepricefeebhyt.Count > 0)
                                        {
                                            try
                                            {
                                                string update_bhyt_dv = "UPDATE serviceprice SET servicepricemoney_bhyt = '" + dv_servicepricefeebhyt[0]["servicepricefeebhyt"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                                condb.ExecuteNonQuery_HIS(update_bhyt_dv);
                                                dem_sl = dem_sl + 1;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        case "Sai giá viện phí":
                            {
                                if (gridViewDichVu.RowCount > 0)
                                {
                                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                    {
                                        //Tìm mã servicepricefeenhandan từ servicepriceref
                                        string sqlget_servicepricefeebhyt = "SELECT servicepriceref.servicepricefeenhandan FROM serviceprice, servicepriceref WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                        DataView dv_servicepricefeebhyt = new DataView(condb.GetDataTable_HIS(sqlget_servicepricefeebhyt));
                                        if (dv_servicepricefeebhyt.Count > 0)
                                        {
                                            try
                                            {
                                                string update_bhyt_dv = "UPDATE serviceprice SET servicepricemoney_nhandan = '" + dv_servicepricefeebhyt[0]["servicepricefeenhandan"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                                condb.ExecuteNonQuery_HIS(update_bhyt_dv);
                                                dem_sl = dem_sl + 1;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        case "Sai giá yêu cầu":
                            {
                                if (gridViewDichVu.RowCount > 0)
                                {
                                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                    {
                                        //Tìm mã bhytgroupcode từ medicine_ref
                                        string sqlget_servicepricefee = "SELECT servicepriceref.servicepricefee as servicepricefee FROM serviceprice, servicepriceref WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                        DataView dv_servicepricefee = new DataView(condb.GetDataTable_HIS(sqlget_servicepricefee));
                                        if (dv_servicepricefee.Count > 0)
                                        {
                                            try
                                            {
                                                string update_bhyt_dv = "UPDATE serviceprice SET servicepricemoney = '" + dv_servicepricefee[0]["servicepricefee"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                                condb.ExecuteNonQuery_HIS(update_bhyt_dv);
                                                dem_sl = dem_sl + 1;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }

                        case "Sai giá người nước ngoài":
                            {
                                if (gridViewDichVu.RowCount > 0)
                                {
                                    for (int i = 0; i < gridViewDichVu.RowCount; i++)
                                    {
                                        //Tìm mã servicepricefeenuocngoai từ servicepriceref
                                        string sqlget_servicepricefeebhyt = "SELECT servicepriceref.servicepricefeenuocngoai FROM serviceprice, servicepriceref WHERE servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "'";
                                        DataView dv_servicepricefeebhyt = new DataView(condb.GetDataTable_HIS(sqlget_servicepricefeebhyt));
                                        if (dv_servicepricefeebhyt.Count > 0)
                                        {
                                            try
                                            {
                                                string update_bhyt_dv = "UPDATE serviceprice SET servicepricemoney_nuocngoai = '" + dv_servicepricefeebhyt[0]["servicepricefeenuocngoai"] + "' WHERE serviceprice.servicepriceid='" + gridViewDichVu.GetRowCellValue(i, "servicepriceid") + "' ;";
                                                condb.ExecuteNonQuery_HIS(update_bhyt_dv);
                                                dem_sl = dem_sl + 1;
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                                    frmthongbao.Show();
                                }
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("Chưa chọn kiểu cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }

                    if (gridViewDichVu.RowCount > 0)
                    {
                        // Load lại dữ liệu cho người dùng xem
                        for (int i = 0; i < gridViewDichVu.RowCount - 1; i++)
                        {
                            arrayserID += gridViewDichVu.GetRowCellValue(i, "servicepriceid").ToString() + ",";
                        }
                        arrayserID += gridViewDichVu.GetRowCellValue(gridViewDichVu.RowCount - 1, "servicepriceid").ToString();

                        string sqlquerythuoc = "SELECT serviceprice.servicepriceid as servicepriceid, serviceprice.medicalrecordid as madieutri, serviceprice.vienphiid as mavienphi, serviceprice.hosobenhanid as hosobenhan, serviceprice.maubenhphamid as maubenhpham, serviceprice.servicepricecode as madichvu, serviceprice.servicepricename as tendichvu, serviceprice.servicepricemoney as gia, serviceprice.servicepricemoney_bhyt as gia_bhyt, serviceprice.servicepricemoney_nhandan as gia_nhandan, serviceprice.servicepricemoney_nuocngoai as gia_nnn, serviceprice.soluong as soluong, serviceprice.bhyt_groupcode as bhyt_groupcode, serviceprice.servicepricedate as ngaychidinh FROM serviceprice WHERE serviceprice.servicepriceid in (" + arrayserID + ")  ORDER BY servicepriceid;";
                        DataView dv_bhytgroup = new DataView(condb.GetDataTable_HIS(sqlquerythuoc));
                        gridControlDichVu.DataSource = dv_bhytgroup;
                        // Luu lai log
                        if (dem_sl > 0)
                        {
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Update " + dem_sl + " trường " + cbbChonKieu.Text.Trim() + " thành công. ServicepriceID: " + arrayserID + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        }
                        // Thông báo đã Update Tên dịch vụ
                        MessageBox.Show("Update " + dem_sl + " danh mục \"" + cbbChonKieu.Text + "\" thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Base.Logging.Error(ex);
                }
                SplashScreenManager.CloseForm();
            }
        }

        #endregion

    }
}
