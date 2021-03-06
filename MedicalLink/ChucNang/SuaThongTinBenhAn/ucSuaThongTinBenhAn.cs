﻿using System;
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
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        //public DataView dataView_DanhMucTinh { get; set; }
        //public List<classDanhMuc_Huyen> dataView_DanhMucHuyen { get; set; }
        public List<classDanhMuc_TinhHuyenXa> lstDanhMucTinhHuyenXa { get; set; }
        public List<classBenhVienDTO> lstDanhMucSoSoYTe { get; set; }

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
            LoadDanhMuc_CoSoYTe();
            txtBNBKMaBN.Focus();
        }
        private void LoadDanhMuc_TinhHuyenXa()
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                lstDanhMucTinhHuyenXa = new List<classDanhMuc_TinhHuyenXa>();
                string sql_loadxa = "SELECT DISTINCT hc_tinhcode, hc_tinhname, hc_huyencode, hc_huyenname, hc_xacode, hc_xaname  FROM hosobenhan WHERE hc_tinhcode <> '' and hc_tinhname <>'' and hc_xacode <> '' and hc_huyencode <> '' and hc_huyenname <>'' and hc_xaname <>'' ;";
                DataView dataView_DanhMucTinhHuyenXa = new DataView(condb.GetDataTable_HIS(sql_loadxa));
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
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void LoadDanhMuc_CoSoYTe()
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                lstDanhMucSoSoYTe = new List<classBenhVienDTO>();
                string sql_loadxa = "select benhvienkcbbd, benhviencode, benhvienname from tools_benhvien;";
                DataView dataView_DanhMucBenhvien = new DataView(condb.GetDataTable_MeL(sql_loadxa));
                if (dataView_DanhMucBenhvien != null && dataView_DanhMucBenhvien.Count > 0)
                {
                    for (int i = 0; i < dataView_DanhMucBenhvien.Count; i++)
                    {
                        classBenhVienDTO benhvien = new classBenhVienDTO();
                        benhvien.benhvienkcbbd = dataView_DanhMucBenhvien[i]["benhvienkcbbd"].ToString();
                        benhvien.benhviencode = dataView_DanhMucBenhvien[i]["benhviencode"].ToString();
                        benhvien.benhvienname = dataView_DanhMucBenhvien[i]["benhvienname"].ToString();
                        lstDanhMucSoSoYTe.Add(benhvien);
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                e.Appearance.BackColor = Color.DodgerBlue;
                e.Appearance.ForeColor = Color.White;
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

        #endregion

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
                                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
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
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Events
        // Hiển thị danh sách mã điều trị của BN
        internal void btnBNBKTimKiem_Click(object sender, EventArgs e)
        {
            string sqlquerry = "";
            string timkiemtheo = " ";
            try
            {
                if (txtBNBKMaVP.Text == "Mã viện phí" && txtBNBKMaBN.Text == "Mã bệnh nhân")
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }
                if (txtBNBKMaVP.Text == "Mã viện phí") // tìm theo mã BN
                {
                    timkiemtheo = " vp.patientid='" + txtBNBKMaBN.Text.Trim() + "' ";
                }
                else // tìm theo mã VP
                {
                    timkiemtheo = " vp.vienphiid='" + txtBNBKMaVP.Text.Trim() + "' ";
                }

                sqlquerry = @"SELECT vp.vienphiid as vienphiid, 
                            hsba.patientid as patientid, 
                            bh.bhytid as bhytid, 
                            hsba.hosobenhanid as hosobenhanid, 
                            hsba.patientname as patientname, 
                            case vp.vienphistatus when 2 then 'Đã duyệt BHYT' when 1 then case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, 
                            hsba.gioitinhcode as gioitinhcode, 
                            hsba.gioitinhname as gioitinh, 
                            vp.vienphidate as thoigianvaovien, 
                            vp.vienphidate_ravien as thoigianravien, 
                            vp.duyet_ngayduyet_bh as thoigianduyetbhyt, 
                            krv.departmentgroupname as khoaravien, 
                            prv.departmentname as phongravien, 
                            bh.bhytcode as sothebhyt, 
                            bh.macskcbbd as noidkkcbbd, 
                            bh.bhytfromdate as hanthetu, 
                            bh.bhytutildate as hantheden, 
                            vp.theghep_bhytcode,
                            vp.theghep_bhytfromdate,
                            vp.theghep_bhytutildate,
                            vp.theghep_macskcbbd,
                            hsba.noilamviec as noilamviec, 
                            hsba.birthday as ngaysinh, 
                            hsba.hc_xacode as hc_xacode, 
                            hsba.hc_huyencode as hc_huyencode, 
                            hsba.hc_tinhcode as hc_tinhcode, 
                            hsba.hc_xaname as hc_xaname, 
                            hsba.hc_huyenname as hc_huyenname, 
                            hsba.hc_tinhname as hc_tinhname, 
                            hsba.hc_sonha as hc_sonha, 
                            hc_thon as hc_thon 
                            FROM vienphi vp inner join hosobenhan hsba on vp.hosobenhanid= hsba.hosobenhanid  inner join (select departmentgroupid,departmentgroupname from departmentgroup) krv on vp.departmentgroupid = krv.departmentgroupid  left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) prv on vp.departmentid=prv.departmentid  inner join bhyt bh on vp.bhytid = bh.bhytid  WHERE " + timkiemtheo + " ORDER BY vp.vienphiid; ";
                DataView dv = new DataView(condb.GetDataTable_HIS(sqlquerry));
                if (dv.Count > 0 && dv != null)
                {
                    gridControlSuaPhoiThanhToan.DataSource = dv;
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        private void gridViewSuaPhoiThanhToan_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gridViewSuaPhoiThanhToan.RowCount > 0)
                {
                    SuaThongTin_ThucHien frm = new SuaThongTin_ThucHien(this);
                    frm.ShowDialog();
                    btnBNBKTimKiem_Click(null, null);
                }
            }
            catch (Exception)
            {
            }
        }


        #endregion
    }
}
