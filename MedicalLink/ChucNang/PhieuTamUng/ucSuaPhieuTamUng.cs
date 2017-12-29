﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;
using DevExpress.Utils.Menu;
using MedicalLink.ChucNang.PhieuTamUng;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaPhieuTamUng : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public ucSuaPhieuTamUng()
        {
            InitializeComponent();
        }
        private void ucChuyenTien_Load(object sender, EventArgs e)
        {
            txtMaVienPhi.Focus();
        }

        #region Custom
        private void txtMaVienPhi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtMaVienPhi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem.PerformClick();
            }
        }
        private void gridViewChuyenTien_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Tim KIem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlquerry = "select b.billid, b.billcode, b.billgroupcode, vp.patientid, vp.vienphiid, b.patientname, 'Tạm ứng' as loaiphieuthuid, b.datra as sotien, b.billdate, (case vp.vienphistatus_vp when 1 then 'Đã duyệt VP' else '' end) as vienphistatus_vp, b.departmentgroupid, b.departmentid, degp.departmentgroupname, de.departmentname, b.userid, us.username from bill b inner join vienphi vp on vp.vienphiid=b.vienphiid inner join departmentgroup degp on degp.departmentgroupid=b.departmentgroupid left join department de on de.departmentid=b.departmentid left join nhompersonnel us on us.userhisid=b.userid where b.loaiphieuthuid=2 and b.dahuyphieu=0 and vp.vienphiid='" + txtMaVienPhi.Text + "' order by b.billdate; ";

                DataTable _dataTimKiem = condb.GetDataTable_HIS(sqlquerry);
                if (_dataTimKiem != null && _dataTimKiem.Rows.Count > 0)
                {
                    gridControlChuyenTien.DataSource = _dataTimKiem;
                }
                else
                {
                    gridControlChuyenTien.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Events
        private void gridViewChuyenTien_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    //GridView view = sender as GridView;
                    e.Menu.Items.Clear();
                    DXMenuItem itemMoBenhAn = new DXMenuItem("Sửa phiếu tạm ứng"); // caption menu
                    itemMoBenhAn.Image = imageCollectionMBA.Images[0]; // icon cho menu
                    //itemMoBenhAn.Shortcut = Shortcut.F6; // phím tắt
                    itemMoBenhAn.Click += new EventHandler(SuaPhieuTamUng_Click);// thêm sự kiện click
                    e.Menu.Items.Add(itemMoBenhAn);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void SuaPhieuTamUng_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChuyenTien.FocusedRowHandle;
                string trangth = Convert.ToString(gridViewChuyenTien.GetRowCellValue(rowHandle, "vienphistatus_vp").ToString());
                if (trangth == "Đã duyệt VP")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.BENH_NHAN_DA_DUYET_VIEN_PHI);
                    frmthongbao.Show();
                }
                else
                {
                    string _billid = gridViewChuyenTien.GetRowCellValue(rowHandle, "billid").ToString();
                    string _departmentgroupid = gridViewChuyenTien.GetRowCellValue(rowHandle, "departmentgroupid").ToString();
                    string _departmentid = gridViewChuyenTien.GetRowCellValue(rowHandle, "departmentid").ToString();
                    string _userid = gridViewChuyenTien.GetRowCellValue(rowHandle, "userid").ToString();
                    string _vienphiid = gridViewChuyenTien.GetRowCellValue(rowHandle, "vienphiid").ToString(); string _patientid = gridViewChuyenTien.GetRowCellValue(rowHandle, "patientid").ToString();


                    frmSuaPhieuTamUng frmSua = new frmSuaPhieuTamUng(_billid, _departmentgroupid, _departmentid, _userid, _vienphiid, _patientid);
                    frmSua.ShowDialog();
                    btnTimKiem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }






        #endregion


    }
}