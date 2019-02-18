using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using MedicalLink.Base;
using MedicalLink.Model.Models.Pharma;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace MedicalLink.QLDuoc
{
    public partial class DM_Thuoc : UserControl
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        #endregion

        public DM_Thuoc()
        {
            InitializeComponent();
        }

        #region Load
        private void DM_Thuoc_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachThuoc();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhSachThuoc()
        {
            try
            {
                string _sqlDSThuoc = "";
                DataTable _dataThuoc = condb.GetDataTable_Phr(_sqlDSThuoc);
                List<MedicinerefDTO> _lstMedicineref = O2S_Common.DataTables.Convert.DataTableToList<MedicinerefDTO>(_dataThuoc);

                HienThiLenTreeList(_lstMedicineref);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Create Tree
        private void HienThiLenTreeList(List<MedicinerefDTO> _lstMedicineref)
        {
            try
            {
                if (_lstMedicineref != null && _lstMedicineref.Count > 0)
                {
                    treeListDSThuoc.ClearNodes();
                    TreeListNode parentForRootNodes = null;
                    string _medicinecode = "0";
                    string _medicinename = "DANH MỤC THUỐC, VẬT TƯ, HÓA CHẤT";

                    TreeListNode rootNode_0 = treeListDSThuoc.AppendNode(
new object[] { _medicinecode, _medicinename, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, parentForRootNodes, null);
                    CreateChildNodeServiceType(rootNode_0, _medicinecode, _lstMedicineref);

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
                    new object[] { _medi.medicinecode, _medi.medicinename, _medi.donvitinh, _medi.hamluong, _medi.sodangky, _medi.hoatchat, _medi.nongdo, _medi.hamluong, _medi.quycachdonggoi, _medi.nuocsanxuat, _medi.hangsanxuat, _medi.nhacungcap, _medi.gianhap, _medi.vatnhap, _medi.giaban, _medi.vatban, _medi.medicinecodeuser, _medi.medicinerefid }, rootNode, null);
                            CreateChildNodeServiceType(childNode, _medi.medicinecode, lstMedicineref);
                        }
                        else //là lá
                        {
                            TreeListNode childChildNode = treeListDSThuoc.AppendNode(
                                new object[] { _medi.medicinecode, _medi.medicinename, _medi.donvitinh, _medi.hamluong, _medi.sodangky, _medi.hoatchat, _medi.nongdo, _medi.hamluong, _medi.quycachdonggoi, _medi.nuocsanxuat, _medi.hangsanxuat, _medi.nhacungcap, _medi.gianhap, _medi.vatnhap, _medi.giaban, _medi.vatban, _medi.medicinecodeuser, _medi.medicinerefid }, rootNode, _medi);
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


        #region Custom
        private void treeListDSThuoc_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == (sender as TreeList).FocusedNode)
            {
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
            }
        }
        #endregion

        #region Events
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DM_ThuocDetail _frm = new DM_ThuocDetail(0, 0);
                _frm.ShowDialog();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();

                string fileTemplatePath = "0_PharmaDanhMucThuocExport.xlsx";
                string _sqlDMThuoc = "";
                string _sqlKhoa = "";

                DataTable _dataDMThuoc = condb.GetDataTable_HIS(_sqlDMThuoc);
                //DataTable _dataKhoa = condb.GetDataTable_HIS(_sqlKhoa);

                List<DataTable> _lstDataTable = new List<DataTable>();
                _lstDataTable.Add(_dataDMThuoc);
                //_lstDataTable.Add(_dataKhoa);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _lstDataTable);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
