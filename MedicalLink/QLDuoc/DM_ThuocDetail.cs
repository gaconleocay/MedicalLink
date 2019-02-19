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
    public partial class DM_ThuocDetail : Form
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private int datatype { get; set; }
        private long MedicinerefID { get; set; }
        #endregion
        public DM_ThuocDetail()
        {
            InitializeComponent();
        }
        public DM_ThuocDetail(int _datatype, long _MedicinerefID)
        {
            InitializeComponent();
            this.datatype = _datatype;
            this.MedicinerefID = _MedicinerefID;
        }

        #region Load
        private void DM_ThuocDetail_Load(object sender, EventArgs e)
        {
            try
            {
                LoadHoatChat();
                LoadBietDuoc();
                LoadDonViTinh();
                LoadDuongDung();
                LoadNuocSanXuat();
                LoadHangSanXuat();
                LoadNhaCungCap();
                LoadMacDinhKhac();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadHoatChat()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadBietDuoc()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDonViTinh()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDuongDung()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadNuocSanXuat()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadHangSanXuat()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadNhaCungCap()
        {
            try
            {
                string _sqldata = "";
                DataTable _dataDM = condb.GetDataTable_Phr(_sqldata);

            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadMacDinhKhac()
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
                //string _sqlKhothuoc = "select medicinestoreid from pm_medicinestore where medicinestorecode='" + txtmedicinestorecode.Text.Trim() + "' and medicinestoreid<>" + this.medicinestoreid ?? 0 + ";";

                //DataTable _dataKhoThuoc = condb.GetDataTable_Phr(_sqlKhothuoc);
                //if (_dataKhoThuoc != null && _dataKhoThuoc.Rows.Count <= 0)
                //{
                //    _result = true;
                //}
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return _result;
        }


        #endregion


    }
}
