using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using System.IO;
using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaThongTinBenhAn : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //public DataView dataView_DanhMucTinh { get; set; }
        //public List<classDanhMuc_Huyen> dataView_DanhMucHuyen { get; set; }
        public List<classDanhMuc_TinhHuyenXa> lstDanhMucTinhHuyenXa { get; set; }

        public ucSuaThongTinBenhAn()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã viện phí
            txtBNBKMaVP.ForeColor = SystemColors.GrayText;
            txtBNBKMaVP.Text = "Mã viện phí";
            this.txtBNBKMaVP.Leave += new System.EventHandler(this.txtBNBKMaVP_Leave);
            this.txtBNBKMaVP.Enter += new System.EventHandler(this.txtBNBKMaVP_Enter);
            // Hiển thị Text Hint Mã BN
            txtBNBKMaBN.ForeColor = SystemColors.GrayText;
            txtBNBKMaBN.Text = "Mã bệnh nhân";
            this.txtBNBKMaBN.Leave += new System.EventHandler(this.txtBNBKMaBN_Leave);
            this.txtBNBKMaBN.Enter += new System.EventHandler(this.txtBNBKMaBN_Enter);
        }

        #region Load
        private void ucXuLyBNBoKhoa_Load(object sender, EventArgs e)
        {
            LoadDanhMuc_TinhHuyenXa();
            txtBNBKMaBN.Focus();
        }
        private void LoadDanhMuc_TinhHuyenXa()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                lstDanhMucTinhHuyenXa = new List<classDanhMuc_TinhHuyenXa>();
                string sql_loadxa = "SELECT DISTINCT hc_tinhcode, hc_tinhname, hc_huyencode, hc_huyenname, hc_xacode, hc_xaname  FROM hosobenhan WHERE hc_tinhcode <> '' and hc_tinhname <>'' and hc_xacode <> '' and hc_huyencode <> '' and hc_huyenname <>'' and hc_xaname <>'' ;";
                DataView dataView_DanhMucTinhHuyenXa = new DataView(condb.GetDataTable(sql_loadxa));
                if (dataView_DanhMucTinhHuyenXa != null && dataView_DanhMucTinhHuyenXa.Count > 0)
                {
                    for (int i = 0; i < dataView_DanhMucTinhHuyenXa.Count; i++)
                    {
                        classDanhMuc_TinhHuyenXa tinhhuyenxa = new classDanhMuc_TinhHuyenXa();
                        tinhhuyenxa.hc_tinhcode = dataView_DanhMucTinhHuyenXa[i]["hc_tinhcode"].ToString();
                        tinhhuyenxa.hc_tinhname = dataView_DanhMucTinhHuyenXa[i]["hc_tinhname"].ToString();
                        tinhhuyenxa.hc_huyencode = dataView_DanhMucTinhHuyenXa[i]["hc_huyencode"].ToString();
                        tinhhuyenxa.hc_huyenname = dataView_DanhMucTinhHuyenXa[i]["hc_huyenname"].ToString();
                        tinhhuyenxa.hc_xacode = dataView_DanhMucTinhHuyenXa[i]["hc_xacode"].ToString();
                        tinhhuyenxa.hc_xaname = dataView_DanhMucTinhHuyenXa[i]["hc_xaname"].ToString();
                        lstDanhMucTinhHuyenXa.Add(tinhhuyenxa);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
        // Hiển thị Text Hint Mã viện phí
        private void txtBNBKMaVP_Leave(object sender, EventArgs e)
        {
            if (txtBNBKMaVP.Text.Length == 0)
            {
                txtBNBKMaVP.Text = "Mã viện phí";
                txtBNBKMaVP.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã viện phí
        private void txtBNBKMaVP_Enter(object sender, EventArgs e)
        {
            if (txtBNBKMaVP.Text == "Mã viện phí")
            {
                txtBNBKMaVP.Text = "";
                txtBNBKMaVP.ForeColor = SystemColors.WindowText;
            }
        }

        // Hiển thị Text Hint Mã bệnh nhân
        private void txtBNBKMaBN_Leave(object sender, EventArgs e)
        {
            if (txtBNBKMaBN.Text.Length == 0)
            {
                txtBNBKMaBN.Text = "Mã bệnh nhân";
                txtBNBKMaBN.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã bệnh nhân
        private void txtBNBKMaBN_Enter(object sender, EventArgs e)
        {
            if (txtBNBKMaBN.Text == "Mã bệnh nhân")
            {
                txtBNBKMaBN.Text = "";
                txtBNBKMaBN.ForeColor = SystemColors.WindowText;
            }
        }

        private void txtBNBKMaBenhNhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void txtBNBKMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBNBKTimKiem.PerformClick();
            }
        }

        private void txtBNBKMaVP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBNBKTimKiem.PerformClick();
            }
        }

        private void gridViewBNBK_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void txtBNBKMaVP_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBNBKMaVP.Text != "Mã viện phí")
            {
                // Hiển thị Text Hint Mã BN
                txtBNBKMaBN.ForeColor = SystemColors.GrayText;
                txtBNBKMaBN.Text = "Mã bệnh nhân";
                this.txtBNBKMaBN.Leave += new System.EventHandler(this.txtBNBKMaBN_Leave);
                this.txtBNBKMaBN.Enter += new System.EventHandler(this.txtBNBKMaBN_Enter);
            }
        }

        private void txtBNBKMaBN_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBNBKMaBN.Text != "")
            {
                // Hiển thị Text Hint Mã BN
                txtBNBKMaVP.ForeColor = SystemColors.GrayText;
                txtBNBKMaVP.Text = "Mã viện phí";
                this.txtBNBKMaVP.Leave += new System.EventHandler(this.txtBNBKMaVP_Leave);
                this.txtBNBKMaVP.Enter += new System.EventHandler(this.txtBNBKMaVP_Enter);
            }
        }

        // Hiển thị danh sách mã điều trị của BN
        internal void btnBNBKTimKiem_Click(object sender, EventArgs e)
        {
            string sqlquerry;
            try
            {
                if (txtBNBKMaVP.Text == "Mã viện phí") // tìm theo mã BN
                {
                    sqlquerry = "SELECT vienphi.vienphiid as vienphiid, hosobenhan.patientid as patientid, bhyt.bhytid as bhytid, hosobenhan.hosobenhanid as hosobenhanid, hosobenhan.patientname as patientname, case vienphi.vienphistatus when 2 then 'Đã duyệt BHYT' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, hosobenhan.gioitinhcode as gioitinhcode, hosobenhan.gioitinhname as gioitinh, vienphi.vienphidate as thoigianvaovien, vienphi.vienphidate_ravien as thoigianravien, vienphi.duyet_ngayduyet_bh as thoigianduyetbhyt, tools_depatment.departmentgroupname as khoaravien, tools_depatment.departmentname as phongravien, bhyt.bhytcode as sothebhyt, bhyt.macskcbbd as noidkkcbbd, bhyt.bhytfromdate as hanthetu, bhyt.bhytutildate as hantheden, hosobenhan.noilamviec as noilamviec, hosobenhan.birthday as ngaysinh, hosobenhan.hc_xacode as hc_xacode, hosobenhan.hc_huyencode as hc_huyencode, hosobenhan.hc_tinhcode as hc_tinhcode, hosobenhan.hc_xaname as hc_xaname, hosobenhan.hc_huyenname as hc_huyenname, hosobenhan.hc_tinhname as hc_tinhname, hosobenhan.hc_sonha as hc_sonha, hc_thon as hc_thon FROM vienphi, hosobenhan, tools_depatment, bhyt WHERE vienphi.hosobenhanid= hosobenhan.hosobenhanid and vienphi.bhytid = bhyt.bhytid and vienphi.departmentgroupid = tools_depatment.departmentgroupid and vienphi.departmentid = tools_depatment.departmentid and vienphi.patientid='" + txtBNBKMaBN.Text.Trim() + "' ORDER BY vienphiid;";
                }
                else // tìm theo mã VP
                {
                    sqlquerry = "SELECT vienphi.vienphiid as vienphiid, hosobenhan.patientid as patientid, bhyt.bhytid as bhytid, hosobenhan.hosobenhanid as hosobenhanid, hosobenhan.patientname as patientname, case vienphi.vienphistatus when 2 then 'Đã duyệt BHYT' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, hosobenhan.gioitinhcode as gioitinhcode, hosobenhan.gioitinhname as gioitinh, vienphi.vienphidate as thoigianvaovien, vienphi.vienphidate_ravien as thoigianravien, vienphi.duyet_ngayduyet_bh as thoigianduyetbhyt, tools_depatment.departmentgroupname as khoaravien, tools_depatment.departmentname as phongravien, bhyt.bhytcode as sothebhyt, bhyt.macskcbbd as noidkkcbbd, bhyt.bhytfromdate as hanthetu, bhyt.bhytutildate as hantheden, hosobenhan.noilamviec as noilamviec, hosobenhan.birthday as ngaysinh, hosobenhan.hc_xacode as hc_xacode, hosobenhan.hc_huyencode as hc_huyencode, hosobenhan.hc_tinhcode as hc_tinhcode, hosobenhan.hc_xaname as hc_xaname, hosobenhan.hc_huyenname as hc_huyenname, hosobenhan.hc_tinhname as hc_tinhname, hosobenhan.hc_sonha as hc_sonha, hc_thon as hc_thon FROM vienphi, hosobenhan, tools_depatment, bhyt WHERE vienphi.hosobenhanid= hosobenhan.hosobenhanid and vienphi.bhytid = bhyt.bhytid and vienphi.departmentgroupid = tools_depatment.departmentgroupid and vienphi.departmentid = tools_depatment.departmentid and vienphi.vienphiid='" + txtBNBKMaVP.Text.Trim() + "' ORDER BY vienphiid;";
                }

                DataView dv = new DataView(condb.GetDataTable(sqlquerry));
                gridControlSuaPhoiThanhToan.DataSource = dv;

                if (gridViewSuaPhoiThanhToan.RowCount == 0)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region Export data
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewSuaPhoiThanhToan.RowCount > 0)
                {
                    try
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                            if (saveDialog.ShowDialog() != DialogResult.Cancel)
                            {
                                string exportFilePath = saveDialog.FileName;
                                string fileExtenstion = new FileInfo(exportFilePath).Extension;

                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        gridViewSuaPhoiThanhToan.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewSuaPhoiThanhToan.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewSuaPhoiThanhToan.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewSuaPhoiThanhToan.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewSuaPhoiThanhToan.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewSuaPhoiThanhToan.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        private void gridViewSuaPhoiThanhToan_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

            }
        }

        private void repositoryItemButtonEdit_edit_Click(object sender, EventArgs e)
        {
            try
            {
                SuaThongTin_ThucHien frm = new SuaThongTin_ThucHien(this);
                frm.ShowDialog();
                btnBNBKTimKiem_Click(null, null);
            }
            catch (Exception)
            {
            }
        }

        private void gridViewSuaPhoiThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                SuaThongTin_ThucHien frm = new SuaThongTin_ThucHien(this);
                frm.ShowDialog();
                btnBNBKTimKiem_Click(null, null);
            }
            catch (Exception)
            {
            }
        }




    }
}
