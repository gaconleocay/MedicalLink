using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoBenhNhanNoiTru : UserControl
    {
        internal void LayDuLieuBaoCaoKhiClick(string thoiGianTu, string thoiGianDen)
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao = "";
                DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
                HienThiDuLieuBaoCao(dataBCTongTheKhoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        internal void LayDuLieuBaoCaoTuDongCapNhat(decimal thoiGianCapNhat)
        {
            try
            {
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                string sqlBaoCao = "";
                DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlBaoCao));
                HienThiDuLieuBaoCao(dataBCTongTheKhoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void HienThiDuLieuBaoCao(DataView dataBCTongTheKhoa)
        {
            try
            {
                if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
                {
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
