using DevExpress.XtraGrid.Views.Grid;
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

namespace MedicalLink.Dashboard.BCTongTheKhoa
{
    public partial class BCTongTheKhoaFullSize : Form
    {
        public BCTongTheKhoaFullSize()
        {
            InitializeComponent();
        }
        public BCTongTheKhoaFullSize(List<BCDashboardQLTongTheKhoa> dataBCQLTongTheKhoaSDO, string tenkhoa)
        {
            InitializeComponent();
            gridControlDataQLTTKhoa.DataSource = dataBCQLTongTheKhoaSDO;
            lblTenThongTinChiTiet.Text = "BÁO CÁO QUẢN LÝ TỔNG THỂ - " + tenkhoa.ToUpper();

        }

        private void bandedGridViewDataBNNT_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightPink;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
