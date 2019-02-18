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

namespace MedicalLink.QLDuoc
{
    public partial class DM_KhoThuocVT : UserControl
    {
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        public DM_KhoThuocVT()
        {
            InitializeComponent();
        }

        #region Load
        private void DM_KhoThuocVT_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachKhoThuoc();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDanhSachKhoThuoc()
        {
            try
            {
                string _sqlDSKho = $@"SELECT row_number () over (order by medicinestoretype,medicinestorename) as stt,
	medicinestoreid,
	(case medicinestoretype
		when 1 then 'Kho tổng'
		when 2 then 'Kho ngoại trú'
		when 3 then 'Kho nội trú'
		when 4 then 'Nhà thuốc'
		when 5 then 'Kho vật tư'
		end) as medicinestoretypename,
	medicinestorecode,
	medicinestorename,
	thukhocode,
	remark,
	loaidoituongbenhnhan,
	(case when islock=1 then 'Đã khóa' end) as islockname	
FROM pm_medicinestore;";
                DataTable _dtDSKho = condb.GetDataTable_Phr(_sqlDSKho);
                if (_dtDSKho != null && _dtDSKho.Rows.Count > 0)
                {
                    gridControlData.DataSource = _dtDSKho;
                }
                else
                {
                    gridControlData.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void gridViewData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
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
                DM_KhoThuocVTDetail _frm = new DM_KhoThuocVTDetail();
                _frm.ShowDialog();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridViewData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gridViewData.RowCount > 0)
                {
                    var rowHandle = gridViewData.FocusedRowHandle;
                    long _medicinestoreid = O2S_Common.TypeConvert.Parse.ToInt64(gridViewData.GetRowCellValue(rowHandle, "medicinestoreid").ToString());
                    DM_KhoThuocVTDetail _frm = new DM_KhoThuocVTDetail(_medicinestoreid);
                    _frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

    }
}
