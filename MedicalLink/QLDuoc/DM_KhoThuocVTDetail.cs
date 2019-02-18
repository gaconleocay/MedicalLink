using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using MedicalLink.Model.Models.Pharma;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.QLDuoc
{
    public partial class DM_KhoThuocVTDetail : Form
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private long medicinestoreid { get; set; }
        private List<MedicinerefDTO> lstMedicineref { get; set; }
        #endregion
        public DM_KhoThuocVTDetail()
        {
            InitializeComponent();
        }
        public DM_KhoThuocVTDetail(long _medicinestoreid)
        {
            InitializeComponent();
            this.medicinestoreid = _medicinestoreid;
        }

        #region Load
        private void DM_KhoThuocVTDetail_Load(object sender, EventArgs e)
        {
            LoadDanhSachThuocVatTu();
            HienThiLenTreeList();

            if (this.medicinestoreid != 0)
            {
                LoadDanhSachThuocVatTuCheckTheoKho();
            }
        }
        private void LoadDanhSachThuocVatTu()
        {
            try
            {
                string _sqlDSThuoc = "";
                DataTable _dataThuoc = condb.GetDataTable_Phr(_sqlDSThuoc);
                this.lstMedicineref = O2S_Common.DataTables.Convert.DataTableToList<MedicinerefDTO>(_dataThuoc);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachThuocVatTuCheckTheoKho()
        {
            try
            {

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Create Tree
        private void HienThiLenTreeList()
        {
            try
            {
                if (this.lstMedicineref != null && this.lstMedicineref.Count > 0)
                {
                    treeListDSThuoc.ClearNodes();
                    TreeListNode parentForRootNodes = null;
                    string _medicinecode = "0";
                    string _medicinename = "DANH MỤC THUỐC, VẬT TƯ, HÓA CHẤT";

                    TreeListNode rootNode_0 = treeListDSThuoc.AppendNode(
new object[] { _medicinecode, _medicinename, null, null, null }, parentForRootNodes, null);
                    CreateChildNodeServiceType(rootNode_0, _medicinecode, this.lstMedicineref);

                    treeListDSThuoc.ExpandAll();
                }
                else
                {
                    treeListDSThuoc.ClearNodes();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void CreateChildNodeServiceType(TreeListNode rootNode, string _medicinecode, List<MedicinerefDTO> lstMedicineref)
        {
            try
            {
                var _lstMedicinerefGroup = lstMedicineref.Where(o => o.medicinegroupcode == _medicinecode).ToList().OrderBy(o => o.medicinename);
                if (_lstMedicinerefGroup != null && _lstMedicinerefGroup.Count() > 0)
                {
                    foreach (var _medi in _lstMedicinerefGroup)
                    {
                        var _mediObj = lstMedicineref.Where(o => o.medicinegroupcode == _medi.medicinecode);
                        if (_mediObj != null && _mediObj.Count() > 0)
                        {
                            TreeListNode childNode = treeListDSThuoc.AppendNode(
                    new object[] { _medi.medicinecode, _medi.medicinename, _medi.donvitinh, _medi.hamluong, _medi.medicinerefid }, rootNode, null);
                            CreateChildNodeServiceType(childNode, _medi.medicinecode, lstMedicineref);
                        }
                        else //là lá
                        {
                            TreeListNode childChildNode = treeListDSThuoc.AppendNode(
                                new object[] { _medi.medicinecode, _medi.medicinename, _medi.donvitinh, _medi.hamluong, _medi.medicinerefid }, rootNode, _medi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Events
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraThemMoiKhoThuoc())
                {

                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool KiemTraThemMoiKhoThuoc()
        {
            bool _result = false;
            try
            {
                string _sqlKhothuoc = "select medicinestoreid from pm_medicinestore where medicinestorecode='" + txtmedicinestorecode.Text.Trim() + "' and medicinestoreid<>" + this.medicinestoreid ?? 0 + ";";

                DataTable _dataKhoThuoc = condb.GetDataTable_Phr(_sqlKhothuoc);
                if (_dataKhoThuoc != null && _dataKhoThuoc.Rows.Count <= 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return _result;
        }
        #endregion

        #region Cusstom
        private void treeListDSThuoc_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == (sender as TreeList).FocusedNode)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        #endregion


    }
}
