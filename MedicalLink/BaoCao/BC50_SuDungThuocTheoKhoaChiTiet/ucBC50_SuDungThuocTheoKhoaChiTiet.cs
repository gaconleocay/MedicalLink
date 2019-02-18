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
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;
using MedicalLink.DatabaseProcess;
using DevExpress.XtraPrinting;
using DevExpress.XtraTreeList.Nodes;

namespace MedicalLink.BaoCao
{
    public partial class ucBC50_SuDungThuocTheoKhoaChiTiet : UserControl
    {
        #region Declaration
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private List<BC50MedicineRefDTO> lstMedicineRef { get; set; }
        #endregion

        #region Load
        public ucBC50_SuDungThuocTheoKhoaChiTiet()
        {
            InitializeComponent();
        }
        private void ucBC50_SuDungThuocTheoKhoaChiTiet_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhSachKhoa();
                LoadDanhSachNhomThuocVatTu();
                LoadDanhMucThuocVatTu();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhMucThuocVatTu()
        {
            try
            {
                string _sqlThuocVT = "select medicinerefid,medicinerefid_org,medicinegroupcode,medicinecode,medicinename,dangdung,hoatchat,nhomthau from medicine_ref where isremove=0;";
                DataTable _dataThuocVT = condb.GetDataTable_HIS(_sqlThuocVT);
                if (_dataThuocVT.Rows.Count > 0)
                {
                    this.lstMedicineRef = Utilities.DataTables.DataTableToList<BC50MedicineRefDTO>(_dataThuocVT);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachNhomThuocVatTu()
        {
            try
            {
                string _sqlDSNhom = @"select medicinerefid,medicinereftype,medicinegroupcode,medicinecode,medicinename from medicine_ref
where medicinecode in (select medicinegroupcode from medicine_ref where isremove=0 and medicinegroupcode not in ('','NHATHUOC','T46603-4518','VT39826-0633','T37527-3420') group by medicinegroupcode)
and medicinegroupcode not in ('','NHATHUOC','T46603-4518','VT39826-0633','T37527-3420')
order by medicinereftype
	,medicinerefid
	,medicinegroupcode;";
                DataTable _dataDSNhom = condb.GetDataTable_HIS(_sqlDSNhom);

                treeListLookUpEdit1TreeList.KeyFieldName = "medicinecode";
                treeListLookUpEdit1TreeList.ParentFieldName = "medicinegroupcode";
                treeListNhomThuoc.Properties.DisplayMember = "medicinename";
                treeListNhomThuoc.Properties.ValueMember = "medicinecode";

                treeListLookUpEdit1TreeList.DataSource = _dataDSNhom;
                treeListLookUpEdit1TreeList.ExpandAll();

                treeListNhomThuoc.EditValue = _dataDSNhom.Rows[2];
                treeListLookUpEdit1TreeList.SelectNode(treeListLookUpEdit1TreeList.FindNodeByFieldValue("medicinecode", _dataDSNhom.Rows[2]["medicinecode"]));
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                DataGridView_ResetLaiCot();

                string _tieuchi_vp = " and vienphidate_ravien>='2018-01-01 00:00:00' ";
                string _tieuchi_ser = " and servicepricedate>='2018-01-01 00:00:00' ";
                //string _tieuchi_mbp = " and maubenhphamdate>='2018-01-01 00:00:00' ";
                string _tieuchi_hsba = " and hosobenhandate>='2018-01-01 00:00:00' ";
                string _tieuchi_bh = " and bhytdate>='2018-01-01 00:00:00' ";
                string _trangthai_vp = "";
                string _lstKhoaChonLayBC = " and departmentgroupid in (0";
                string _lstDSThuocVT_Mef = " and medicinegroupcode in ('0'";
                string _lstDSThuocVT_Ser = " and servicepricecode in ('0'";

                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //Tieu chi
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    //_tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_ser = " and servicepricedate>='" + datetungay + "' ";
                    //_tieuchi_mbp = " and maubenhphamdate>='" + datetungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    _trangthai_vp = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    _trangthai_vp = " and vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    _trangthai_vp = " and vienphistatus>0 and vienphistatus_vp=1 ";
                }
                //Khoa
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        _lstKhoaChonLayBC += "," + lstKhoaCheck[i];
                    }
                    _lstKhoaChonLayBC += ")";
                    _lstKhoaChonLayBC = _lstKhoaChonLayBC.Replace("(0,", "(");
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }
                //DS thuoc Vat tu
                var _lstCheckNode = treeListLookUpEdit1TreeList.GetAllCheckedNodes();
                foreach (var _item in _lstCheckNode)
                {
                    _lstDSThuocVT_Mef += ",'" + _item.GetValue(treeListColumn_medicinecode).ToString() + "'";

                    var _lstThuocSer = this.lstMedicineRef.Where(o => o.medicinegroupcode == _item.GetValue(treeListColumn_medicinecode).ToString()).ToList();
                    foreach (var item in _lstThuocSer)
                    {
                        _lstDSThuocVT_Ser += ",'" + item.medicinecode + "'";
                    }
                }
                _lstDSThuocVT_Mef += ")";
                _lstDSThuocVT_Mef = _lstDSThuocVT_Mef.Replace("'0',", "");
                _lstDSThuocVT_Ser += ")";
                _lstDSThuocVT_Ser = _lstDSThuocVT_Ser.Replace("'0',", "");





                string _sqlDSBN = $@"SELECT row_number () over (order by vp.vienphiid) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	bh.bhytcode
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
	inner join (select bhytid,bhytcode from bhyt where 1=1 {_tieuchi_bh}) bh on bh.bhytid=vp.bhytid
	inner join (select vienphiid from serviceprice where thuockhobanle=0 and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_lstKhoaChonLayBC} {_lstDSThuocVT_Ser} group by vienphiid) ser on ser.vienphiid=vp.vienphiid;";

                string _sqlDSThuocVT = $@"SELECT
	vp.vienphiid,	
	mef.medicinerefid_org,
	ser.servicepricecode,
	TO_CHAR(ser.servicepricedate,'HH24:MI dd/MM/yyyy') as servicepricedate,
	TO_CHAR(ser.servicepricedate,'yyyyMMddHH24MI') as servicepricedatelong,
	ser.soluong,
	(ser.soluong*ser.dongia) as thanhtien
FROM 
	(select vienphiid,hosobenhanid,bhytid,patientid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp}) vp
	inner join (select vienphiid,servicepricecode,servicepricedate,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong from serviceprice where thuockhobanle=0 and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') {_tieuchi_ser} {_lstKhoaChonLayBC} {_lstDSThuocVT_Ser}) ser on ser.vienphiid=vp.vienphiid
	inner join (select medicinerefid_org,medicinecode,medicinename from medicine_ref where 1=1 {_lstDSThuocVT_Mef}) mef on mef.medicinecode=ser.servicepricecode;";

                DataTable _dataDSBN = condb.GetDataTable_HIS(_sqlDSBN);
                DataTable _dataDSThuocVT = condb.GetDataTable_HIS(_sqlDSThuocVT);

                if (_dataDSBN != null && _dataDSBN.Rows.Count > 0)
                {
                    HienThiDuLieuTimKiem(_dataDSBN, _dataDSThuocVT);
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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Xuat bao cao and print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewBaoCao.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                    //string fileTemplatePath = "BC_49_SuDungThuocToanVien.xlsx";
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("BÁO CÁO CHI TIẾT SỬ DỤNG THUỐC THEO KHOA", data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_49_SuDungThuocToanVien.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
        private void gridViewBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
                if (view.GetRowCellValue(e.RowHandle, view.Columns["stt"]).ToString() != "")
                {
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Events
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrangThai.Text == "Đang điều trị" || cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    dateTuNgay.Value = Convert.ToDateTime(GlobalStore.KhoangThoiGianLayDuLieu);
                    dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Process
        private void HienThiDuLieuTimKiem(DataTable _dataDSBN, DataTable _dataDSThuocVT)
        {
            try
            {
                List<BC50DSThuocVTDTO> lstDSThuocVT = Utilities.DataTables.DataTableToList<BC50DSThuocVTDTO>(_dataDSThuocVT);
                List<BC50DSBenhNhanDTO> lstDSBenhNhan = Utilities.DataTables.DataTableToList<BC50DSBenhNhanDTO>(_dataDSBN);

                //Lay DS DM thuoc
                List<BC50DSThuocVTDTO> _lstDMTimKiem = lstDSThuocVT.GroupBy(o => o.medicinerefid_org).Select(n => n.First()).ToList();
                var DMThuocVT = (from mef in this.lstMedicineRef
                                 join tk in _lstDMTimKiem on mef.medicinerefid equals tk.medicinerefid_org
                                 select new
                                 {
                                     medicinerefid_org = mef.medicinerefid_org,
                                     medicinecode = mef.medicinecode,
                                     medicinename = mef.medicinename,
                                     dangdung = mef.dangdung,
                                     hoatchat = mef.hoatchat,
                                     nhomthau = mef.nhomthau,
                                 }).ToList().OrderBy(or => or.medicinename).ToList();

                //Tao gridview
                foreach (var _itemDM in DMThuocVT)
                {
                    //
                    DevExpress.XtraGrid.Views.BandedGrid.GridBand _gridBand_DMThuoc = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();

                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_medicinecode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_medicinename = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_dangdung = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_hoatchat = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_nhomthau = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_soluong = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_thanhtien = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                    //
                    this.gridViewBaoCao.Bands.Add(_gridBand_DMThuoc);
                    //gridBand_TTBN
                    _gridBand_DMThuoc.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    _gridBand_DMThuoc.AppearanceHeader.Options.UseFont = true;
                    _gridBand_DMThuoc.AppearanceHeader.Options.UseTextOptions = true;
                    _gridBand_DMThuoc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridBand_DMThuoc.Caption = _itemDM.medicinename;
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_medicinecode);
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_medicinename);
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_dangdung);
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_hoatchat);
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_nhomthau);
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_soluong);
                    _gridBand_DMThuoc.Columns.Add(_gridColumn_thanhtien);
                    _gridBand_DMThuoc.Name = "gridBand_TTBN";
                    _gridBand_DMThuoc.VisibleIndex = 0;
                    _gridBand_DMThuoc.Width = 540;
                    //=============Column
                    //_gridColumn_medicinecode
                    _gridColumn_medicinecode.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                    _gridColumn_medicinecode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_medicinecode.AppearanceCell.Options.UseFont = true;
                    _gridColumn_medicinecode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_medicinecode.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_medicinecode.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_medicinecode.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_medicinecode.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_medicinecode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_medicinecode.Caption = "Mã thuốc";
                    _gridColumn_medicinecode.FieldName = "MT_" + _itemDM.medicinecode;
                    _gridColumn_medicinecode.Name = "MT_" + _itemDM.medicinecode;
                    _gridColumn_medicinecode.OptionsColumn.ReadOnly = true;
                    _gridColumn_medicinecode.Visible = true;
                    _gridColumn_medicinecode.Width = 150;
                    //_gridColumn_medicinename
                    _gridColumn_medicinename.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_medicinename.AppearanceCell.Options.UseFont = true;
                    _gridColumn_medicinename.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_medicinename.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_medicinename.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_medicinename.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_medicinename.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_medicinename.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_medicinename.Caption = "Tên thuốc";
                    _gridColumn_medicinename.FieldName = "TT_" + _itemDM.medicinecode;
                    _gridColumn_medicinename.Name = "TT_" + _itemDM.medicinecode;
                    _gridColumn_medicinename.OptionsColumn.ReadOnly = true;
                    _gridColumn_medicinename.OptionsColumn.AllowEdit = false;
                    _gridColumn_medicinename.Visible = true;
                    _gridColumn_medicinename.Width = 250;
                    //_gridColumn_dangdung
                    _gridColumn_dangdung.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_dangdung.AppearanceCell.Options.UseFont = true;
                    _gridColumn_dangdung.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_dangdung.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_dangdung.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_dangdung.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_dangdung.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_dangdung.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_dangdung.Caption = "Liều dùng";
                    _gridColumn_dangdung.FieldName = "LD_" + _itemDM.medicinecode;
                    _gridColumn_dangdung.Name = "LD_" + _itemDM.medicinecode;
                    _gridColumn_dangdung.OptionsColumn.ReadOnly = true;
                    _gridColumn_dangdung.OptionsColumn.AllowEdit = false;
                    _gridColumn_dangdung.Visible = true;
                    _gridColumn_dangdung.Width = 150;
                    //_gridColumn_hoatchat
                    _gridColumn_hoatchat.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_hoatchat.AppearanceCell.Options.UseFont = true;
                    _gridColumn_hoatchat.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_hoatchat.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_hoatchat.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_hoatchat.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_hoatchat.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_hoatchat.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_hoatchat.Caption = "Hoạt chất";
                    _gridColumn_hoatchat.FieldName = "HC_" + _itemDM.medicinecode;
                    _gridColumn_hoatchat.Name = "HC_" + _itemDM.medicinecode;
                    _gridColumn_hoatchat.OptionsColumn.ReadOnly = true;
                    _gridColumn_hoatchat.OptionsColumn.AllowEdit = false;
                    _gridColumn_hoatchat.Visible = true;
                    _gridColumn_hoatchat.Width = 180;
                    //_gridColumn_nhomthau
                    _gridColumn_nhomthau.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_nhomthau.AppearanceCell.Options.UseFont = true;
                    _gridColumn_nhomthau.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_nhomthau.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_nhomthau.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_nhomthau.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_nhomthau.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_nhomthau.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_nhomthau.Caption = "Nhóm thầu";
                    _gridColumn_nhomthau.FieldName = "NT_" + _itemDM.medicinecode;
                    _gridColumn_nhomthau.Name = "NT_" + _itemDM.medicinecode;
                    _gridColumn_nhomthau.OptionsColumn.ReadOnly = true;
                    _gridColumn_nhomthau.OptionsColumn.AllowEdit = false;
                    _gridColumn_nhomthau.Visible = true;
                    _gridColumn_nhomthau.Width = 150;
                    // _gridColumn_soluong
                    _gridColumn_soluong.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_soluong.AppearanceCell.Options.UseFont = true;
                    _gridColumn_soluong.AppearanceCell.Options.UseTextOptions = true;
                    _gridColumn_soluong.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    _gridColumn_soluong.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_soluong.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_soluong.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_soluong.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_soluong.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_soluong.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_soluong.Caption = "Số lượng";
                    _gridColumn_soluong.DisplayFormat.FormatString = "#,##0.0";
                    _gridColumn_soluong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    _gridColumn_soluong.FieldName = "SL_" + _itemDM.medicinecode;
                    _gridColumn_soluong.Name = "SL_" + _itemDM.medicinecode;
                    _gridColumn_soluong.OptionsColumn.AllowEdit = false;
                    _gridColumn_soluong.OptionsColumn.ReadOnly = true;
                    _gridColumn_soluong.OptionsColumn.AllowEdit = false;
                    _gridColumn_soluong.Visible = true;
                    _gridColumn_soluong.Width = 80;
                    // _gridColumn_thanhtien
                    _gridColumn_thanhtien.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_thanhtien.AppearanceCell.Options.UseFont = true;
                    _gridColumn_thanhtien.AppearanceCell.Options.UseTextOptions = true;
                    _gridColumn_thanhtien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    _gridColumn_thanhtien.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                    _gridColumn_thanhtien.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                    _gridColumn_thanhtien.AppearanceHeader.Options.UseFont = true;
                    _gridColumn_thanhtien.AppearanceHeader.Options.UseForeColor = true;
                    _gridColumn_thanhtien.AppearanceHeader.Options.UseTextOptions = true;
                    _gridColumn_thanhtien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    _gridColumn_thanhtien.Caption = "Thành tiền";
                    _gridColumn_thanhtien.DisplayFormat.FormatString = "#,##0";
                    _gridColumn_thanhtien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    _gridColumn_thanhtien.FieldName = "TTi_" + _itemDM.medicinecode;
                    _gridColumn_thanhtien.Name = "TTi_" + _itemDM.medicinecode;
                    _gridColumn_thanhtien.OptionsColumn.AllowEdit = false;
                    _gridColumn_thanhtien.OptionsColumn.ReadOnly = true;
                    _gridColumn_thanhtien.OptionsColumn.AllowEdit = false;
                    _gridColumn_thanhtien.Visible = true;
                    _gridColumn_thanhtien.Width = 150;

                }

                //Tao datatable: them cot vao Table _dataDSBN: + cot _DMThuoc   
                DataTable dataBCResult = _dataDSBN.Clone();

                dataBCResult.Columns.Add("servicepricedate", typeof(string));
                foreach (var _itemDM in DMThuocVT)
                {
                    dataBCResult.Columns.Add("MT_" + _itemDM.medicinecode, typeof(string));
                    dataBCResult.Columns.Add("TT_" + _itemDM.medicinecode, typeof(string));
                    dataBCResult.Columns.Add("LD_" + _itemDM.medicinecode, typeof(string));
                    dataBCResult.Columns.Add("HC_" + _itemDM.medicinecode, typeof(string));
                    dataBCResult.Columns.Add("NT_" + _itemDM.medicinecode, typeof(string));
                    dataBCResult.Columns.Add("SL_" + _itemDM.medicinecode, typeof(decimal));
                    dataBCResult.Columns.Add("TTi_" + _itemDM.medicinecode, typeof(decimal));
                }
                //Insert du lieu vao _dataBaoCao
                foreach (var _itemBN in lstDSBenhNhan)
                {
                    //
                    var _lstThuocBN = lstDSThuocVT.Where(o => o.vienphiid == _itemBN.vienphiid).ToList();

                    DataRow newRow = dataBCResult.NewRow();
                    newRow["stt"] = _itemBN.stt.ToString();
                    newRow["vienphiid"] = _itemBN.vienphiid.ToString();
                    newRow["patientid"] = _itemBN.patientid.ToString();
                    newRow["patientname"] = _itemBN.patientname;
                    newRow["bhytcode"] = _itemBN.bhytcode;

                    foreach (var _itemDM in DMThuocVT)
                    {
                        var _soLuongThuocBN = _lstThuocBN.Where(o => o.medicinerefid_org == _itemDM.medicinerefid_org).ToList();
                        if (_soLuongThuocBN != null && _soLuongThuocBN.Count > 0)
                        {
                            newRow["MT_" + _itemDM.medicinecode] = _itemDM.medicinecode;
                            newRow["TT_" + _itemDM.medicinecode] = _itemDM.medicinename;
                            newRow["LD_" + _itemDM.medicinecode] = _itemDM.dangdung;
                            newRow["HC_" + _itemDM.medicinecode] = _itemDM.hoatchat;
                            newRow["NT_" + _itemDM.medicinecode] = _itemDM.nhomthau;
                            //soluong+thanhtien
                            decimal _soluong = 0;
                            decimal _thanhtien = 0;
                            foreach (var _item in _soLuongThuocBN)
                            {
                                _soluong += _item.soluong;
                                _thanhtien += _item.thanhtien;
                            }
                            newRow["SL_" + _itemDM.medicinecode] = _soluong;
                            newRow["TTi_" + _itemDM.medicinecode] = _thanhtien;
                        }
                    }

                    dataBCResult.Rows.Add(newRow);
                    //add chi tiết từng chỉ định
                    var _lstThuocBN_Grdate = _lstThuocBN.GroupBy(o => o.servicepricedate).Select(n => n.First()).OrderBy(or => or.servicepricedatelong).ToList();
                    if (_lstThuocBN_Grdate != null && _lstThuocBN_Grdate.Count > 0)
                    {
                        foreach (var _item in _lstThuocBN_Grdate)
                        {
                            DataRow _rowCD = dataBCResult.NewRow();
                            //_rowCD["stt"] = "";
                            //_rowCD["vienphiid"] = "";
                            //_rowCD["patientid"] = "";
                            _rowCD["patientname"] = "";
                            _rowCD["servicepricedate"] = _item.servicepricedate;

                            foreach (var _itemDM in DMThuocVT)
                            {
                                var _kiemtraTh = _lstThuocBN.Where(o => o.servicepricedatelong == _item.servicepricedatelong && o.medicinerefid_org == _itemDM.medicinerefid_org).ToList();
                                if (_kiemtraTh != null && _kiemtraTh.Count > 0)
                                {
                                    _rowCD["MT_" + _itemDM.medicinecode] = _itemDM.medicinecode;
                                    _rowCD["TT_" + _itemDM.medicinecode] = _itemDM.medicinename;
                                    _rowCD["LD_" + _itemDM.medicinecode] = _itemDM.dangdung;
                                    _rowCD["HC_" + _itemDM.medicinecode] = _itemDM.hoatchat;
                                    _rowCD["NT_" + _itemDM.medicinecode] = _itemDM.nhomthau;
                                    _rowCD["SL_" + _itemDM.medicinecode] = _kiemtraTh[0].soluong;
                                    _rowCD["TTi_" + _itemDM.medicinecode] = _kiemtraTh[0].thanhtien;
                                }
                            }

                            dataBCResult.Rows.Add(_rowCD);
                        }
                    }
                }

                gridControlBaoCao.DataSource = dataBCResult;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void DataGridView_ResetLaiCot()
        {
            try
            {
                //xoa tat ca cac cot
                this.gridViewBaoCao.Bands.Clear();
                this.gridViewBaoCao.Columns.Clear();

                //====================
                DevExpress.XtraGrid.Views.BandedGrid.GridBand _gridBand_TTBN = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_stt = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_vienphiid = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_patientid = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_patientname = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_bhytcode = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn _gridColumn_servicepricedate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                //====================
                this.gridViewBaoCao.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { _gridBand_TTBN });
                // gridBand_TTBN
                _gridBand_TTBN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                _gridBand_TTBN.AppearanceHeader.Options.UseFont = true;
                _gridBand_TTBN.AppearanceHeader.Options.UseTextOptions = true;
                _gridBand_TTBN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridBand_TTBN.Caption = "Thông tin bệnh nhân";
                _gridBand_TTBN.Columns.Add(_gridColumn_stt);
                _gridBand_TTBN.Columns.Add(_gridColumn_vienphiid);
                _gridBand_TTBN.Columns.Add(_gridColumn_patientid);
                _gridBand_TTBN.Columns.Add(_gridColumn_patientname);
                _gridBand_TTBN.Columns.Add(_gridColumn_bhytcode);
                _gridBand_TTBN.Columns.Add(_gridColumn_servicepricedate);
                _gridBand_TTBN.Name = "gridBand_TTBN";
                _gridBand_TTBN.VisibleIndex = 0;
                _gridBand_TTBN.Width = 540;
                //====================

                this.gridViewBaoCao.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            _gridColumn_stt,
            _gridColumn_vienphiid,
            _gridColumn_patientid,
            _gridColumn_patientname,
            _gridColumn_bhytcode,
            _gridColumn_servicepricedate});

                // gridColumn_stt
                _gridColumn_stt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_stt.AppearanceCell.Options.UseFont = true;
                _gridColumn_stt.AppearanceCell.Options.UseTextOptions = true;
                _gridColumn_stt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_stt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_stt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                _gridColumn_stt.AppearanceHeader.Options.UseFont = true;
                _gridColumn_stt.AppearanceHeader.Options.UseForeColor = true;
                _gridColumn_stt.AppearanceHeader.Options.UseTextOptions = true;
                _gridColumn_stt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_stt.Caption = "STT";
                _gridColumn_stt.FieldName = "stt";
                _gridColumn_stt.Name = "gridColumn_stt";
                _gridColumn_stt.OptionsColumn.AllowEdit = false;
                _gridColumn_stt.Visible = true;
                _gridColumn_stt.VisibleIndex = 0;
                _gridColumn_stt.Width = 40;

                // gridColumn_vienphiid
                _gridColumn_vienphiid.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_vienphiid.AppearanceCell.Options.UseFont = true;
                _gridColumn_vienphiid.AppearanceCell.Options.UseTextOptions = true;
                _gridColumn_vienphiid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_vienphiid.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_vienphiid.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                _gridColumn_vienphiid.AppearanceHeader.Options.UseFont = true;
                _gridColumn_vienphiid.AppearanceHeader.Options.UseForeColor = true;
                _gridColumn_vienphiid.AppearanceHeader.Options.UseTextOptions = true;
                _gridColumn_vienphiid.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_vienphiid.Caption = "Mã VP";
                _gridColumn_vienphiid.FieldName = "vienphiid";
                _gridColumn_vienphiid.Name = "gridColumn_vienphiid";
                _gridColumn_vienphiid.OptionsColumn.ReadOnly = true;
                _gridColumn_vienphiid.Visible = true;
                _gridColumn_vienphiid.Width = 75;

                // gridColumn_patientid
                _gridColumn_patientid.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_patientid.AppearanceCell.Options.UseFont = true;
                _gridColumn_patientid.AppearanceCell.Options.UseTextOptions = true;
                _gridColumn_patientid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_patientid.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_patientid.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                _gridColumn_patientid.AppearanceHeader.Options.UseFont = true;
                _gridColumn_patientid.AppearanceHeader.Options.UseForeColor = true;
                _gridColumn_patientid.AppearanceHeader.Options.UseTextOptions = true;
                _gridColumn_patientid.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_patientid.Caption = "Mã BN";
                _gridColumn_patientid.FieldName = "patientid";
                _gridColumn_patientid.Name = "gridColumn_patientid";
                _gridColumn_patientid.OptionsColumn.ReadOnly = true;
                _gridColumn_patientid.Visible = true;
                _gridColumn_patientid.Width = 75;

                // gridColumn_patientname
                _gridColumn_patientname.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_patientname.AppearanceCell.Options.UseFont = true;
                _gridColumn_patientname.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_patientname.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                _gridColumn_patientname.AppearanceHeader.Options.UseFont = true;
                _gridColumn_patientname.AppearanceHeader.Options.UseForeColor = true;
                _gridColumn_patientname.AppearanceHeader.Options.UseTextOptions = true;
                _gridColumn_patientname.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_patientname.Caption = "Tên bệnh nhân";
                _gridColumn_patientname.FieldName = "patientname";
                _gridColumn_patientname.Name = "gridColumn_patientname";
                _gridColumn_patientname.OptionsColumn.ReadOnly = true;
                _gridColumn_patientname.OptionsColumn.AllowEdit = false;
                _gridColumn_patientname.Visible = true;
                _gridColumn_patientname.Width = 180;

                // gridColumn_bhytcode
                _gridColumn_bhytcode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_bhytcode.AppearanceCell.Options.UseFont = true;
                _gridColumn_bhytcode.AppearanceCell.Options.UseTextOptions = true;
                _gridColumn_bhytcode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_bhytcode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_bhytcode.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                _gridColumn_bhytcode.AppearanceHeader.Options.UseFont = true;
                _gridColumn_bhytcode.AppearanceHeader.Options.UseForeColor = true;
                _gridColumn_bhytcode.AppearanceHeader.Options.UseTextOptions = true;
                _gridColumn_bhytcode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_bhytcode.Caption = "Số thẻ BHYT";
                _gridColumn_bhytcode.FieldName = "bhytcode";
                _gridColumn_bhytcode.Name = "gridColumn_bhytcode";
                _gridColumn_bhytcode.OptionsColumn.ReadOnly = true;
                _gridColumn_bhytcode.Visible = true;
                _gridColumn_bhytcode.Width = 140;

                // gridColumn_servicepricedate
                _gridColumn_servicepricedate.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_servicepricedate.AppearanceCell.Options.UseFont = true;
                _gridColumn_servicepricedate.AppearanceCell.Options.UseTextOptions = true;
                _gridColumn_servicepricedate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_servicepricedate.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
                _gridColumn_servicepricedate.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                _gridColumn_servicepricedate.AppearanceHeader.Options.UseFont = true;
                _gridColumn_servicepricedate.AppearanceHeader.Options.UseForeColor = true;
                _gridColumn_servicepricedate.AppearanceHeader.Options.UseTextOptions = true;
                _gridColumn_servicepricedate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                _gridColumn_servicepricedate.Caption = "Ngày chỉ định";
                _gridColumn_servicepricedate.FieldName = "servicepricedate";
                _gridColumn_servicepricedate.Name = "gridColumn_servicepricedate";
                _gridColumn_servicepricedate.OptionsColumn.AllowEdit = false;
                _gridColumn_servicepricedate.Visible = true;
                _gridColumn_servicepricedate.Width = 125;

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }



        #endregion

        #region Tree List
        private void treeListLookUpEdit1TreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                if (e.Node.ParentNode != null)
                {
                    e.Node.ParentNode.Checked = IsAllChecked(e.Node.ParentNode.Nodes);
                    SetCheckedChildNodes(e.Node.Nodes);
                }
                else
                {
                    SetCheckedChildNodes(e.Node.Nodes);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void SetCheckedChildNodes(TreeListNodes nodes)
        {
            try
            {
                foreach (TreeListNode node in nodes)
                {
                    if (node.Nodes.Count > 0)
                    {
                        node.Checked = node.ParentNode.Checked;
                        SetCheckedChildNodes(node.Nodes);
                    }
                    else
                    {
                        node.Checked = node.ParentNode.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private bool IsAllChecked(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes)
        {
            bool value = true;
            try
            {
                foreach (TreeListNode node in nodes)
                {
                    if (!node.Checked)
                    {
                        value = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return value;
        }


        #endregion



    }
}
