using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang.TOOL27_KiemTraKetNoiPostgre
{
    public partial class frmCauHinhNgatKetNoiTuDong : Form
    {    // khai báo 1 hàm delegate
        public delegate void GetString(string _lblThongBao);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        public frmCauHinhNgatKetNoiTuDong()
        {
            InitializeComponent();
        }

        private void frmCauHinhNgatKetNoiTuDong_Load(object sender, EventArgs e)
        {
            try
            {
                //dtTGLayDLTu.Value = Utilities.TypeConvertParse.ToDateTime(GlobalStore.KhoangThoiGianLayDuLieu);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                if (MyGetData != null)
                {// tại đây gọi nó
                    //MyGetData(DateTime.ParseExact(dtTGLayDLTu.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                this.Close();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
