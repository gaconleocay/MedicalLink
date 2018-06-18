using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.BCQLTongTheKhoa
{
    public partial class BCQLTongTheKhoaTuyChonNangCao : Form
    {
        // khai báo 1 hàm delegate
        public delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        public BCQLTongTheKhoaTuyChonNangCao()
        {
            InitializeComponent();
        }

        private void BCTongTheKhoaTuyChonNangCao_Load(object sender, EventArgs e)
        {
            try
            {
                dtTGLayDLTu.Value = Utilities.TypeConvertParse.ToDateTime(GlobalStore.KhoangThoiGianLayDuLieu);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnSettingAdvand_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyGetData != null)
                {// tại đây gọi nó
                    MyGetData(DateTime.ParseExact(dtTGLayDLTu.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
