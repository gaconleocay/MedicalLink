using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MedicalLink.Base;

namespace MedicalLink.ChucNang
{
    public partial class SuaPhoiThanhToan_SoLuong : DevExpress.XtraEditors.XtraForm
    {
        ucSuaPhoiThanhToan suaPhoiTT;
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string soluong_old;


        public SuaPhoiThanhToan_SoLuong()
        {
            InitializeComponent();
        }
        public SuaPhoiThanhToan_SoLuong(ucSuaPhoiThanhToan control)
        {
            InitializeComponent();
            suaPhoiTT = control;
        }

        private void btnSL_OK_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thời gian
                String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // lấy giá trị tại dòng click chuột
                var rowHandle = suaPhoiTT.gridViewSuaPhoiThanhToan.FocusedRowHandle;
                //soluong_old = suaPhoiTT.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "soluong").ToString();
                int servicepriceid = Convert.ToInt32(suaPhoiTT.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                // thực thi câu lệnh update và lưu log
                string sqlxecute = "UPDATE serviceprice SET soluong='" + spinSoLuong.Value.ToString() + "' WHERE servicepriceid=" + servicepriceid + ";";
                string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Sửa số lượng của servicepriceid=" + servicepriceid + " từ " + soluong_old + " thành " + spinSoLuong.Text + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "');";
                condb.ExecuteNonQuery(sqlxecute);
                condb.ExecuteNonQuery(sqlinsert_log);
                suaPhoiTT.timerThongBao.Start();
                suaPhoiTT.lblThongBao.Visible = true;
                suaPhoiTT.lblThongBao.Text = "Sửa số lượng từ " + soluong_old + " thành " + spinSoLuong.Value.ToString() + " thành công.\nVui lòng kiểm tra lại";
                this.Close();
                // load lại dữ liệu của form
                suaPhoiTT.gridControlSuaPhoiThanhToan.DataSource = null;
                suaPhoiTT.btnBNBKTimKiem_Click(null, null);
            }
            catch (Exception)
            {

            }
        }

        private void SuaPhoiThanhToan_SoLuong_Load(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = suaPhoiTT.gridViewSuaPhoiThanhToan.FocusedRowHandle;
                soluong_old = suaPhoiTT.gridViewSuaPhoiThanhToan.GetRowCellValue(rowHandle, "soluong").ToString();
                spinSoLuong.Value = Convert.ToDecimal(soluong_old);
                btnSL_OK.Enabled = false;
            }
            catch (Exception)
            {

            }
        }

        private void spinSoLuong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (spinSoLuong.Value.ToString() != soluong_old)
                {
                    btnSL_OK.Enabled = true;
                }
                else
                {
                    btnSL_OK.Enabled = false;
                }
                if (spinSoLuong.Value < 0)
                {
                    spinSoLuong.Value = 0;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}