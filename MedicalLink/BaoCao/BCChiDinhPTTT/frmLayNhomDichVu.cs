using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang.BCPTTT
{
    public partial class frmLayNhomDichVu : Form
    {
        public delegate void GetString(string dsnhomdichvu);
        public GetString MyGetData;
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();


        public frmLayNhomDichVu()
        {
            InitializeComponent();
        }
        public frmLayNhomDichVu(string dsnhomdichvu)
        {
            InitializeComponent();
        }
        private void frmLayNhomDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                string sql_laynhomdv = " SELECT ref.servicepricecode, ref.servicepricename FROM servicepriceref ref WHERE ref.servicepricecode in (SELECT servicepricegroupcode FROM servicepriceref WHERE servicepricegroupcode <>'' and servicepricegroupcode is NOT NULL and bhyt_groupcode in ('06PTTT','07KTC') GROUP BY servicepricegroupcode ) ORDER BY ref.bhyt_groupcode, ref.servicepricecode; ";
                DataView data_dsnguoidung = new DataView(condb.GetDataTable(sql_laynhomdv));
                gridControlNhomDV.DataSource = data_dsnguoidung;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string dsnhomdichvu = "";
                for (int i = 0; i < gridViewNhomDV.SelectedRowsCount-1; i++)
                {
                    dsnhomdichvu += Convert.ToString(gridViewNhomDV.GetRowCellValue(i, "servicepricecode").ToString()) + ",";
                }
                dsnhomdichvu += Convert.ToString(gridViewNhomDV.GetRowCellValue(gridViewNhomDV.SelectedRowsCount - 1, "servicepricecode").ToString());
                MyGetData(dsnhomdichvu);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void gridViewNhomDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
    }
}
