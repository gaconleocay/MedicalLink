using MedicalLink.ClassCommon.BaoCao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao.BC01DSBNSDDV
{
    public partial class frmTuyChonCotHienThi : Form
    {
        // khai báo 1 hàm delegate
        public delegate void GetString(List<BC01CotBoSungDTO> _lstCotBoSung);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        public frmTuyChonCotHienThi()
        {
            InitializeComponent();
        }

        #region Load
        private void frmTuyChonCotHienThi_Load(object sender, EventArgs e)
        {
            LoadDanhSachCotHienThi();
        }
        private void LoadDanhSachCotHienThi()
        {
            try
            {
                List<BC01CotBoSungDTO> _lstCotBoSung = new List<BC01CotBoSungDTO>();

                BC01CotBoSungDTO _dsThuocDiKem = new BC01CotBoSungDTO()
                {
                    macot = "dsThuocDiKem",
                    tencot = "DS thuốc đi kèm",
                };
                BC01CotBoSungDTO _dsVatTuDiKem = new BC01CotBoSungDTO()
                {
                    macot = "dsVatTuDiKem",
                    tencot = "DS vật tư đi kèm",
                };
                BC01CotBoSungDTO _maDVKTDiKem = new BC01CotBoSungDTO()
                {
                    macot = "maDVKTDiKem",
                    tencot = "Mã DVKT cha (đi kèm)",
                };
                BC01CotBoSungDTO _tenDVKTDiKem = new BC01CotBoSungDTO()
                {
                    macot = "tenDVKTDiKem",
                    tencot = "Tên DVKT cha (đi kèm)",
                };


                _lstCotBoSung.Add(_dsThuocDiKem);
                _lstCotBoSung.Add(_dsVatTuDiKem);
                _lstCotBoSung.Add(_maDVKTDiKem);
                _lstCotBoSung.Add(_tenDVKTDiKem);


                chkListCotBoSung.Properties.DataSource = _lstCotBoSung;
                chkListCotBoSung.Properties.DisplayMember = "tencot";
                chkListCotBoSung.Properties.ValueMember = "macot";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion

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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
