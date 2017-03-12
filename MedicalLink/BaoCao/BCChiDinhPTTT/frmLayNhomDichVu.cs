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
                string sql_laynhomdv = "SELECT DISTINCT servicepricegroupcode,servicepricename FROM servicepriceref WHERE  servicepricegroupcode <>'' and servicepricegroupcode is NOT NULL and bhyt_groupcode in ('06PTTT','07KTC');";
                DataView data_dsnguoidung = new DataView(condb.getDataTable(sql_laynhomdv));
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
                for (int i = 0; i < gridViewNhomDV.SelectedRowsCount; i++)
                {
                    dsnhomdichvu += Convert.ToString(gridViewNhomDV.GetRowCellValue(i, "servicepricegroupcode").ToString()) + ",";
                }

                MyGetData(dsnhomdichvu);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
