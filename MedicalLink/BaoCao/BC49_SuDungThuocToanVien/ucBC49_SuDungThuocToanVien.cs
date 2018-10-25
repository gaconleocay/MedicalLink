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
    public partial class ucBC49_SuDungThuocToanVien : UserControl
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<BC50MedicineRefDTO> lstMedicineRef { get; set; }

        #endregion

        #region Load
        public ucBC49_SuDungThuocToanVien()
        {
            InitializeComponent();
        }
        private void ucBC49_SuDungThuocToanVien_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhSachKhoa();
                LoadDanhSachKhoTT();
                LoadDanhSachNhomThuocVatTu();
                LoadDanhMucThuocVatTu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachKhoTT()
        {
            try
            {
                string _sqlKhoTT = "SELECT ms.medicinestoreid,ms.medicinestorename FROM medicine_store ms WHERE ms.medicinestoretype in (3,7,8,9) ORDER BY ms.medicinestoretype,ms.medicinestorename;";
                DataTable _dataKhoTT = condb.GetDataTable_HIS(_sqlKhoTT);
                chkcomboListDSTuTruc.Properties.DataSource = _dataKhoTT;
                chkcomboListDSTuTruc.Properties.DisplayMember = "medicinestorename";
                chkcomboListDSTuTruc.Properties.ValueMember = "medicinestoreid";
                chkcomboListDSTuTruc.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
                MedicalLink.Base.Logging.Error(ex);
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
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_vp = " and vienphidate>='2018-01-01 00:00:00' ";
                string _tieuchi_ser = " and servicepricedate>='2018-01-01 00:00:00' ";
                string _tieuchi_mbp = " and maubenhphamdate>='2018-01-01 00:00:00' ";
                string _trangthai_vp = "";
                string _doituongbenhnhanid = "";
                string _lstDSThuocVT_Mef = " and medicinegroupcode in ('0'";
                string _lstDSThuocVT_Ser = " and servicepricecode in ('0'";

                string _bhyt_groupcode = " and bhyt_groupcode in ('09TDT','091TDTtrongDM','092TDTngoaiDM','093TDTUngthu','094TDTTyle','10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') ";
                string _lstKhoaChonLayBC = " and departmentgroupid in (0";
                string _lstKhoTTChonLayBC = " and medicinestoreid in (0";


                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //Tieu chi
                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_ser = " and servicepricedate>='" + datetungay + "' ";
                    _tieuchi_mbp = " and maubenhphamdate>='" + datetungay + "' ";
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
                //cboDoiTuongBN
                if (cboDoiTuongBN.Text == "BHYT")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid=1 ";
                }
                else if (cboDoiTuongBN.Text == "Viện phí")
                {
                    _doituongbenhnhanid = " and doituongbenhnhanid<>1 ";
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
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }
                //Kho/tu truc
                List<Object> lstKhoTTCheck = chkcomboListDSTuTruc.Properties.Items.GetCheckedValues();
                if (lstKhoTTCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoTTCheck.Count; i++)
                    {
                        _lstKhoTTChonLayBC += "," + lstKhoTTCheck[i];
                    }
                    _lstKhoTTChonLayBC += ")";
                    _lstKhoTTChonLayBC = _lstKhoTTChonLayBC.Replace("(0,", "(");
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
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


                string _sql_timkiem = $@"SELECT (row_number() OVER (PARTITION BY degp.departmentgroupname ORDER BY mef.medicinename)) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	mef.medicinerefid_org,
	mef.medicinename,
	O.dongia,
	sum(O.noitru_sl) as noitru_sl,
	sum(O.noitru_thanhtien) as noitru_thanhtien,
	sum(O.tutruc_sl) as tutruc_sl,
	sum(O.tutruc_thanhtien) as tutruc_thanhtien,
	sum(O.ton_sl) as ton_sl,
	sum(O.ton_thanhtien) as ton_thanhtien
FROM 
	(select ser.departmentgroupid,ser.dongia,ser.servicepricecode,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong end) as noitru_sl,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia end) as noitru_thanhtien,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong end) as tutruc_sl,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia end) as tutruc_thanhtien,
		0 as ton_sl,
		0 as ton_thanhtien
	from 
		(select vienphiid from vienphi where 1=1 {_tieuchi_vp} {_trangthai_vp} {_doituongbenhnhanid}) vp
		  inner join (select vienphiid,departmentgroupid,servicepricecode,maubenhphamid,
						(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong
					from serviceprice where thuockhobanle=0 and soluong>0 {_bhyt_groupcode} {_tieuchi_ser} {_lstKhoaChonLayBC} {_lstDSThuocVT_Ser}) ser on ser.vienphiid=vp.vienphiid
		  inner join (select maubenhphamid,medicinestoreid from maubenhpham where maubenhphamgrouptype in (5,6) {_tieuchi_mbp} {_lstKhoaChonLayBC} {_lstKhoTTChonLayBC}) mbp on mbp.maubenhphamid=ser.maubenhphamid
	group by ser.departmentgroupid,ser.dongia,ser.servicepricecode) O
	inner join (select medicinerefid_org,medicinecode,medicinename from medicine_ref where 1=1 {_lstDSThuocVT_Mef}) mef on mef.medicinecode=O.servicepricecode
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=O.departmentgroupid
WHERE O.noitru_sl<>0 or O.tutruc_sl<>0 or O.ton_sl<>0
GROUP BY degp.departmentgroupid,degp.departmentgroupname,mef.medicinerefid_org,mef.medicinename,O.dongia;";
                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBaoCao.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                    string fileTemplatePath = "BC_49_SuDungThuocToanVien.xlsx";
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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

                string fileTemplatePath = "BC_49_SuDungThuocToanVien.xlsx";
                DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
            return value;
        }


        #endregion
    }
}
