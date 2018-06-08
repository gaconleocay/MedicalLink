using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;
using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao.BCThucHienCLS
{
    public partial class frmKhoaGuiYeuCau_CLS : Form
    {
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public frmKhoaGuiYeuCau_CLS()
        {
            InitializeComponent();
        }

        #region Load
        private void frmKhoaGuiYeuCau_CLS_Load(object sender, EventArgs e)
        {
            try
            {
                List<Department_KhoaCLSDTO> _lstDepartmetGroup = new List<Department_KhoaCLSDTO>();
                string _sqlDSKhoa = "select row_number () over (order by departmentname) as stt,departmentid,departmentcode,departmentname,cls_khoaguiyc,(case when cls_khoaguiyc=1 then 'Đã khóa gửi yêu cầu' end) as cls_khoaguiyc_name,cls_lastuserupdated,cls_lasttimeupdated from tools_depatment where departmenttype in (6,7);";
                DataTable _dataKhoa = condb.GetDataTable_MeL(_sqlDSKhoa);
                if (_dataKhoa != null && _dataKhoa.Rows.Count > 0)
                {
                    _lstDepartmetGroup = Utilities.Util_DataTable.DataTableToList<Department_KhoaCLSDTO>(_dataKhoa);
                }
                gridControlKhoaPhong.DataSource = _lstDepartmetGroup;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewKhoaPhong_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        private void gridViewKhoaPhong_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var rowHandle = gridViewKhoaPhong.FocusedRowHandle;
                long _departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewKhoaPhong.GetRowCellValue(rowHandle, "departmentid").ToString());
                bool _cls_khoaguiyc = Utilities.Util_TypeConvertParse.ToBoolean(gridViewKhoaPhong.GetRowCellValue(rowHandle, "cls_khoaguiyc").ToString());

                string _khoagui = "0";
                if (_cls_khoaguiyc)
                {
                    _khoagui = "1";
                }
                string _sqlupdate = "UPDATE tools_depatment SET cls_khoaguiyc='" + _khoagui + "', cls_lastuserupdated='" + Base.SessionLogin.SessionUsercode + " - " + Base.SessionLogin.SessionUsername + "', cls_lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE departmentid='" + _departmentid + "' ;";
                condb.ExecuteNonQuery_MeL(_sqlupdate);
                frmKhoaGuiYeuCau_CLS_Load(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion

        #region Events

        private void btnKhoaTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                string _sqlupdate = "UPDATE tools_depatment SET cls_khoaguiyc='1', cls_lastuserupdated='" + Base.SessionLogin.SessionUsercode + " - " + Base.SessionLogin.SessionUsername + "', cls_lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "';";
                condb.ExecuteNonQuery_MeL(_sqlupdate);
                frmKhoaGuiYeuCau_CLS_Load(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnMoKhoaTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                string _sqlupdate = "UPDATE tools_depatment SET cls_khoaguiyc='0', cls_lastuserupdated='" + Base.SessionLogin.SessionUsercode + " - " + Base.SessionLogin.SessionUsername + "', cls_lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "';";
                condb.ExecuteNonQuery_MeL(_sqlupdate);
                frmKhoaGuiYeuCau_CLS_Load(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion


    }
}
