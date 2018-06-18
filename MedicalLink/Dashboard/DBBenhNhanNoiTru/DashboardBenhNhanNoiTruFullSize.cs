using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.DBBenhNhanNoiTru
{
    public partial class DashboardBenhNhanNoiTruFullSize : Form
    {
        public DashboardBenhNhanNoiTruFullSize()
        {
            InitializeComponent();
        }
        public DashboardBenhNhanNoiTruFullSize(DataView dataBC)
        {
            InitializeComponent();
            chartControlBNNoiTru.Series[0].Points.Clear();
            chartControlBNNoiTru.Series[1].Points.Clear();
            chartControlBNNoiTru.Series[2].Points.Clear();
            for (int i = 0; i < dataBC.Count; i++)
            {
                chartControlBNNoiTru.Series[0].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.TypeConvertParse.ToDouble(dataBC[i]["money_tong_bhyt"].ToString())));
            }
            for (int i = 0; i < dataBC.Count; i++)
            {
                chartControlBNNoiTru.Series[1].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.TypeConvertParse.ToDouble(dataBC[i]["money_tong_nhandan"].ToString())));
            }
            for (int i = 0; i < dataBC.Count; i++)
            {
                chartControlBNNoiTru.Series[2].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.TypeConvertParse.ToDouble(dataBC[i]["tam_ung"].ToString())));
            }
        }
    }
}
