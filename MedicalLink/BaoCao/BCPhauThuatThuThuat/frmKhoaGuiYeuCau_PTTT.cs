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

namespace MedicalLink.BaoCao.BCPhauThuatThuThuat
{
    public partial class frmKhoaGuiYeuCau_PTTT : Form
    {
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public frmKhoaGuiYeuCau_PTTT()
        {
            InitializeComponent();
        }

        #region Load
        private void frmKhoaGuiYeuCau_PTTT_Load(object sender, EventArgs e)
        {
            try
            {
                List<Departmentgroup_KhoaPTTTDTO> _lstDepartmetGroup = new List<Departmentgroup_KhoaPTTTDTO>();
                string _sqlDSKhoa = "select row_number () over (order by departmentgroupname) as stt,departmentgroupid,departmentgroupcode,departmentgroupname,pttt_khoaguiyc,(case when pttt_khoaguiyc=1 then 'Đã khóa gửi yêu cầu' end) as pttt_khoaguiyc_name,pttt_lastuserupdated,pttt_lasttimeupdated from tools_departmentgroup;";
                DataTable _dataKhoa = condb.GetDataTable_MeL(_sqlDSKhoa);
                if (_dataKhoa != null && _dataKhoa.Rows.Count > 0)
                {
                    _lstDepartmetGroup = Utilities.Util_DataTable.DataTableToList<Departmentgroup_KhoaPTTTDTO>(_dataKhoa);
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



        #endregion

        #region Events


        #endregion

        private void gridViewKhoaPhong_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var rowHandle = gridViewKhoaPhong.FocusedRowHandle;
                long _departmentgroupid = Utilities.Util_TypeConvertParse.ToInt64(gridViewKhoaPhong.GetRowCellValue(rowHandle, "departmentgroupid").ToString());
                bool _pttt_khoaguiyc = Utilities.Util_TypeConvertParse.ToBoolean(gridViewKhoaPhong.GetRowCellValue(rowHandle, "pttt_khoaguiyc").ToString());

                string _khoagui = "0";
                if (_pttt_khoaguiyc)
                {
                    _khoagui = "1";
                }
                string _sqlupdate = "UPDATE tools_departmentgroup SET pttt_khoaguiyc='" + _khoagui + "', pttt_lastuserupdated='" + Base.SessionLogin.SessionUsercode + " - " + Base.SessionLogin.SessionUsername + "', pttt_lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE departmentgroupid='" + _departmentgroupid + "' ;";
                condb.ExecuteNonQuery_MeL(_sqlupdate);
                frmKhoaGuiYeuCau_PTTT_Load(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnKhoaTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                string _sqlupdate = "UPDATE tools_departmentgroup SET pttt_khoaguiyc='1', pttt_lastuserupdated='" + Base.SessionLogin.SessionUsercode + " - " + Base.SessionLogin.SessionUsername + "', pttt_lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "';";
                condb.ExecuteNonQuery_MeL(_sqlupdate);
                frmKhoaGuiYeuCau_PTTT_Load(null, null);
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
                string _sqlupdate = "UPDATE tools_departmentgroup SET pttt_khoaguiyc='0', pttt_lastuserupdated='" + Base.SessionLogin.SessionUsercode + " - " + Base.SessionLogin.SessionUsername + "', pttt_lasttimeupdated='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "';";
                condb.ExecuteNonQuery_MeL(_sqlupdate);
                frmKhoaGuiYeuCau_PTTT_Load(null, null);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
    }
}
