using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using System.Reflection;
using DevExpress.Utils.Menu;

namespace MedicalLink.BaoCao
{
    public partial class ucBCBNSDDVKetHop : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataDSBenhNhan { get; set; }
        private List<classMedicineRef> lstMedicine { get; set; }

        public ucBCBNSDDVKetHop()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        #region Load
        private void ucBCBNSDDVKetHop_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhMucThuocVatTu();
                LoadDanhSachBaoCaoXuatExcel();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaDV.Text.Length == 0)
            {
                mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*";
                mmeMaDV.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,). Tìm thuốc theo tất cả các lô định dạng: AAA*")
            {
                mmeMaDV.Text = "";
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
        }

        private void LoadDanhMucThuocVatTu()
        {
            try
            {
                //Doi DataTable to List
                string sqlgetdata = "select mef.medicinerefid, mef.medicinecode, mef.datatype, mef.medicinerefid_org, mef_org.medicinecode as medicinerefid_orgcode from medicine_ref mef inner join medicine_ref mef_org on mef_org.medicinerefid=mef.medicinerefid_org where mef.isremove=0 and mef.bhyt_groupcode<>'' and mef.datatype=0 ;";
                DataTable dataMedicine = condb.GetDataTable_HIS(sqlgetdata);
                if (dataMedicine != null && dataMedicine.Rows.Count > 0)
                {
                    lstMedicine = new List<classMedicineRef>();
                    //lstMedicine = Utilities.Util_DataTable.DataTableToList<classMedicineRef>(dataMedicine); 
                    for (int i = 0; i < dataMedicine.Rows.Count; i++)
                    {
                        classMedicineRef medicine = new classMedicineRef();
                        medicine.medicinecode = dataMedicine.Rows[i]["medicinecode"].ToString();
                        medicine.medicinerefid_orgcode = dataMedicine.Rows[i]["medicinerefid_orgcode"].ToString();
                        lstMedicine.Add(medicine);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachBaoCaoXuatExcel()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Xuất danh sách bệnh nhân"));
                menu.Items.Add(new DXMenuItem("Xuất danh sách chi tiết dịch vụ của bệnh nhân"));
                // ... add more items
                dropDownExport.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += item_Export_Click;
                // setup initial selection
                //dropDownExport.Text = menu.Items[0].Caption;
                //...
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion
        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string[] dsdv_temp;
            string innerjoin_serf = "";
            string tieuchi, loaivienphiid, doituongbenhnhanid;

            if ((mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)") || (cbbTieuChi.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
            {
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                frmthongbao.Show();
            }
            else
            {
                gridControlDSBenhNhan.DataSource = null;
                gridControlDSDV.DataSource = null;

                // Lấy dữ liệu danh sách dịch vụ nhập vào
                dsdv_temp = mmeMaDV.Text.Split(',');
                for (int i = 0; i < dsdv_temp.Length; i++)
                {
                    if (dsdv_temp[i].ToString().Contains("*"))
                    {
                        string dsdv_like = "";
                        string medicinerefid_orgcode = dsdv_temp[i].ToString().Replace("*", "").Trim();
                        List<classMedicineRef> lstmedicinecode = lstMedicine.Where(o => o.medicinerefid_orgcode == medicinerefid_orgcode).ToList();
                        if (lstmedicinecode != null && lstmedicinecode.Count > 0)
                        {
                            foreach (var item_medi in lstmedicinecode)
                            {
                                dsdv_like += "'" + item_medi.medicinecode + "',";
                            }
                        }
                        dsdv_like = dsdv_like.Remove(dsdv_like.Length - 1);
                        innerjoin_serf += " INNER JOIN (select se.vienphiid from serviceprice se where se.servicepricecode in (" + dsdv_like + ")) ser_" + i + " ON ser_" + i + ".vienphiid=vp.vienphiid ";
                    }
                    else
                    {
                        string dsdv_servicepricecode = dsdv_temp[i].ToString().Trim();
                        //dsdv_servicepricecode += "ser.servicepricecode='" + dsdv_temp[i].ToString().Trim() + "' and ";
                        innerjoin_serf += " INNER JOIN (select se.vienphiid from serviceprice se where se.servicepricecode='" + dsdv_servicepricecode + "') ser_" + i + " ON ser_" + i + ".vienphiid=vp.vienphiid ";
                    }
                }
                // Lấy Tiêu chí thời gian: tieuchi
                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi = " ser.servicepricedate";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi = " vp.vienphidate";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi = " vp.vienphidate_ravien";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi = " vp.duyet_ngayduyet_vp ";
                }
                else //theo ngay duyet BHYT
                {
                    tieuchi = " vp.duyet_ngayduyet ";
                }

                // Lấy loaivienphiid
                if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    loaivienphiid = "and vp.loaivienphiid=1 ";
                }
                else if (cbbLoaiBA.Text == "Nội trú")
                {
                    loaivienphiid = "and vp.loaivienphiid=0 ";
                }
                else
                {
                    loaivienphiid = " ";
                }
                // Lấy trường đối tượng BN loaidoituong
                if (chkBHYT.Checked == true && chkVP.Checked == false)
                {
                    doituongbenhnhanid = "and vp.doituongbenhnhanid=1 ";
                }
                else if (chkBHYT.Checked == false && chkVP.Checked == true)
                {
                    doituongbenhnhanid = "and vp.doituongbenhnhanid<>1 ";
                }
                else if (chkBHYT.Checked == true && chkVP.Checked == true)
                {
                    doituongbenhnhanid = " ";
                }
                else
                    doituongbenhnhanid = " ";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                try
                {
                    string sqlquerry = "SELECT ROW_NUMBER() OVER (ORDER BY vp.vienphiid, vp.duyet_ngayduyet_vp) as stt, vp.patientid, vp.vienphiid, hsba.patientname, (case hsba.gioitinhcode when '01' then 'Nam' when '02' then 'Nữ' end) as gioitinh, to_char(hsba.birthday, 'yyyy') as namsinh, (case vp.vienphistatus when 2 then 'Đã duyệt VP' when 1 then (case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end) else 'Đang điều trị' end) as trangthai, vp.vienphidate as thoigianvaovien, vp.vienphidate_ravien as thoigianravien, vp.duyet_ngayduyet_vp as thoigianduyetvp, vp.duyet_ngayduyet as thoigianduyetbh, depg.departmentgroupname as khoaravien, de.departmentname as phongravien, bhyt.bhytcode FROM vienphi vp INNER JOIN hosobenhan hsba ON vp.hosobenhanid=hsba.hosobenhanid INNER JOIN departmentgroup depg ON vp.departmentgroupid=depg.departmentgroupid INNER JOIN department de ON vp.departmentid=de.departmentid INNER JOIN bhyt ON bhyt.bhytid=vp.bhytid INNER JOIN serviceprice ser ON ser.vienphiid=vp.vienphiid " + innerjoin_serf + " WHERE " + tieuchi + " >= '" + datetungay + "' and " + tieuchi + " <= '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + " GROUP BY vp.patientid, vp.vienphiid, hsba.patientname, hsba.gioitinhcode, hsba.birthday, vp.vienphistatus, vp.vienphidate, vp.vienphidate_ravien, vp.duyet_ngayduyet_vp, vp.duyet_ngayduyet, depg.departmentgroupname, de.departmentname, bhyt.bhytcode; ";

                    dataDSBenhNhan = condb.GetDataTable_HIS(sqlquerry);
                    gridControlDSBenhNhan.DataSource = dataDSBenhNhan;
                    if (gridViewDSBenhNhan.RowCount == 0)
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                catch (Exception ex)
                {
                    Base.Logging.Error(ex);
                }
            }
            SplashScreenManager.CloseForm();
        }

        //private void tbnExport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dataDSBenhNhan != null && dataDSBenhNhan.Rows.Count > 0)
        //        {
        //            string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
        //            string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

        //            string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

        //            List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
        //            ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
        //            reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
        //            reportitem.value = tungaydenngay;

        //            thongTinThem.Add(reportitem);

        //            string fileTemplatePath = "BC_BenhNhanSuDungDichVu.xlsx";
        //            Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
        //            export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataDSBenhNhan);
        //        }
        //        else
        //        {
        //            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
        //            frmthongbao.Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Warn(ex);
        //    }
        //}

        private void gridViewDSDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        void item_Export_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Xuất danh sách bệnh nhân")
                {
                    if (dataDSBenhNhan != null && dataDSBenhNhan.Rows.Count > 0)
                    {
                        string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                        string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                        string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                        List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                        ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                        reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                        reportitem.value = tungaydenngay;
                        thongTinThem.Add(reportitem);
                        ClassCommon.reportExcelDTO reportitem_DV = new ClassCommon.reportExcelDTO();
                        reportitem_DV.name = Base.bienTrongBaoCao.LST_DICHVU;
                        reportitem_DV.value = mmeMaDV.Text;
                        thongTinThem.Add(reportitem_DV);

                        string fileTemplatePath = "BC_BenhNhanSuDungKetHopDichVu.xlsx";
                        Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                        export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataDSBenhNhan);
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else if (tenbaocao == "Xuất danh sách chi tiết dịch vụ của bệnh nhân")
                {
                    if (gridViewDSDV.RowCount > 0)
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
                                            gridViewDSDV.ExportToXls(exportFilePath);
                                            break;
                                        case ".xlsx":
                                            gridViewDSDV.ExportToXlsx(exportFilePath);
                                            break;
                                        case ".rtf":
                                            gridViewDSDV.ExportToRtf(exportFilePath);
                                            break;
                                        case ".pdf":
                                            gridViewDSDV.ExportToPdf(exportFilePath);
                                            break;
                                        case ".html":
                                            gridViewDSDV.ExportToHtml(exportFilePath);
                                            break;
                                        case ".mht":
                                            gridViewDSDV.ExportToMht(exportFilePath);
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
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridControlDSBenhNhan_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSBenhNhan.FocusedRowHandle;
                string vienphiid = gridViewDSBenhNhan.GetRowCellValue(rowHandle, "vienphiid").ToString();
                string sql_serviceprice = "SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepriceid) as stt, ser.vienphiid, (case ser.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' when '08MA' then 'Máu' when '09TDT' then 'Thuốc' when '091TDTtrongDM' then 'Thuốc' when '092TDTngoaiDM' then 'Thuốc' when '093TDTUngthu' then 'Thuốc' when '094TDTTyle' then 'Thuốc TT tỷ lệ' when '10VT' then 'Vật tư' when '101VTtrongDM' then 'Vật tư' when '101VTtrongDMTT' then 'Vật tư' when '102VTngoaiDM' then 'Vật tư' when '103VTtyle' then 'Vật tư TT tỷ lệ' when '11VC' then 'Vận chuyển' when '999DVKHAC' then 'Khác' when '1000PhuThu' then 'Phụ thu' else '' end) as bhyt_groupcode, ser.maubenhphamid, ser.servicepricecode, ser.servicepricename, ser.soluong, ser.servicepricemoney, ser.servicepricemoney_nhandan, ser.servicepricemoney_bhyt, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC ' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' end) as loaidoituong, ser.servicepricedate, case ser.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end as loaiphieu, degp.departmentgroupname, de.departmentname FROM serviceprice ser inner join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid inner join department de on de.departmentid=ser.departmentid WHERE ser.vienphiid=" + vienphiid + "; ";
                DataView dataServiceprice = new DataView(condb.GetDataTable_HIS(sql_serviceprice));
                gridControlDSDV.DataSource = dataServiceprice;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

    }
}
