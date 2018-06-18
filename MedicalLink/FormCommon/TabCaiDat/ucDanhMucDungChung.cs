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
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucDanhMucDungChung : UserControl
    {
        #region Khai bao
        ConnectDatabase condb = new ConnectDatabase();
        private DataView loaiDanhMuc { get; set; } 
        private long selecttools_othertypelistid { get; set; }
        private long selecttools_otherlistid { get; set; }

        #endregion
        public ucDanhMucDungChung()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDanhMucDungChung_Load(object sender, EventArgs e)
        {
            try
            {
                Load_ControlDefault();
                LoadDS_LoaiDanhMuc();
                LoadDS_DanhMuc(0);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void Load_ControlDefault()
        {
            try
            {
                btnLoaiDM_Them.Enabled = true;
                btnLoaiDM_Luu.Enabled = false;
                txtLoaiDM_Ma.ReadOnly = true;
                txtLoaiDM_Ten.ReadOnly = true;
                txtLoaiDM_GhiChu.ReadOnly = true;

                btnDM_Them.Enabled = false;
                btnDM_Luu.Enabled = false;
                txtDM_Ma.ReadOnly = true;
                txtDM_Ten.ReadOnly = true;
                txtDM_GiaTri.ReadOnly = true;
                cboDM_LoaiDMTen.ReadOnly = true;
                cboDM_LoaiDMTen.EditValue = null;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDS_LoaiDanhMuc()
        {
            try
            {
                loaiDanhMuc = new DataView();
                string sqlgetdanhsach = "select ROW_NUMBER() OVER (ORDER BY tools_othertypelistid) as stt, tools_othertypelistid, tools_othertypelistcode, tools_othertypelistname, tools_othertypeliststatus, tools_othertypelistnote from tools_othertypelist; ";
                DataView dataDanhSach = new DataView(condb.GetDataTable_MeL(sqlgetdanhsach));
                gridControlLoaiDM.DataSource = dataDanhSach;
                cboDM_LoaiDMTen.Properties.DataSource = dataDanhSach;
                cboDM_LoaiDMTen.Properties.DisplayMember = "tools_othertypelistname";
                cboDM_LoaiDMTen.Properties.ValueMember = "tools_othertypelistid";
                loaiDanhMuc = dataDanhSach;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDS_DanhMuc(long othertypelistid)
        {
            try
            {
                string tools_othertypelistid = "";
                if (othertypelistid != 0)
                {
                    tools_othertypelistid = " where oty.tools_othertypelistid=" + othertypelistid;
                }
                string sqlgetdanhsach = "select ROW_NUMBER() OVER (ORDER BY oty.tools_othertypelistid, o.tools_otherlistname) as stt, oty.tools_othertypelistid, oty.tools_othertypelistcode, oty.tools_othertypelistname, oty.tools_othertypelistnote, o.tools_otherlistid, o.tools_otherlistcode, o.tools_otherlistname, o.tools_otherliststatus, o.tools_otherlistvalue from tools_othertypelist oty inner join tools_otherlist o on o.tools_othertypelistid=oty.tools_othertypelistid " + tools_othertypelistid + "; ";
                DataView dataDanhSach = new DataView(condb.GetDataTable_MeL(sqlgetdanhsach));
                gridControlDM.DataSource = dataDanhSach;
                cboDM_LoaiDMTen.Properties.DataSource = this.loaiDanhMuc;
                cboDM_LoaiDMTen.Properties.DisplayMember = "tools_othertypelistname";
                cboDM_LoaiDMTen.Properties.ValueMember = "tools_othertypelistid";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Loai danh muc
        private void btnLoaiDM_Them_Click(object sender, EventArgs e)
        {
            try
            {
                this.selecttools_othertypelistid = 0;
                btnLoaiDM_Them.Enabled = true;
                btnLoaiDM_Luu.Enabled = true;
                txtLoaiDM_Ma.ReadOnly = false;
                txtLoaiDM_Ten.ReadOnly = false;
                txtLoaiDM_Ten.ReadOnly = false;
                txtLoaiDM_GhiChu.ReadOnly = false;
                txtLoaiDM_Ma.Text = "";
                txtLoaiDM_Ten.Text = "";
                txtLoaiDM_GhiChu.Text = "";

                btnDM_Them.Enabled = false;
                btnDM_Luu.Enabled = false;
                txtDM_Ma.ReadOnly = true;
                txtDM_Ten.ReadOnly = true;
                txtDM_GiaTri.ReadOnly = true;
                txtDM_Ma.Text = "";
                txtDM_Ten.Text = "";
                txtDM_GiaTri.Text = "";
                cboDM_LoaiDMTen.ReadOnly = true;
                cboDM_LoaiDMTen.EditValue = null;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnLoaiDM_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoaiDM_Ma.Text.Trim() != "" && txtLoaiDM_Ten.Text.Trim() != "")
                {
                    if (this.selecttools_othertypelistid == 0)
                    {
                        string kiemtratontai = "select tools_othertypelistid from tools_othertypelist where tools_othertypelistcode='" + txtLoaiDM_Ma.Text.Trim().ToUpper() + "';";
                        DataView dataDanhSach = new DataView(condb.GetDataTable_MeL(kiemtratontai));
                        if (dataDanhSach != null && dataDanhSach.Count > 0)
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_THE_SU_DUNG_MA_NAY);
                            frmthongbao.Show();
                        }
                        else //them moi
                        {
                            string insert = "INSERT INTO tools_othertypelist(tools_othertypelistcode, tools_othertypelistname,tools_othertypelistnote) VALUES ('" + txtLoaiDM_Ma.Text.Trim().ToUpper() + "', '" + txtLoaiDM_Ten.Text.Trim() + "', '" + txtLoaiDM_GhiChu.Text.Trim() + "'); ";
                            if (condb.ExecuteNonQuery_MeL(insert))
                            {
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    else//cap nhat
                    {
                        string insert = "UPDATE tools_othertypelist SET tools_othertypelistname='" + txtLoaiDM_Ten.Text.Trim() + "', tools_othertypelistnote='" + txtLoaiDM_GhiChu.Text.Trim() + "' WHERE tools_othertypelistid=" + this.selecttools_othertypelistid + "; ";
                        if (condb.ExecuteNonQuery_MeL(insert))
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                    LoadDS_LoaiDanhMuc();
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridControlLoaiDM_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewLoaiDM.RowCount > 0)
                {
                    var rowHandle = gridViewLoaiDM.FocusedRowHandle;

                    this.selecttools_othertypelistid = Utilities.TypeConvertParse.ToInt64(gridViewLoaiDM.GetRowCellValue(rowHandle, "tools_othertypelistid").ToString());
                    txtLoaiDM_Ma.Text = gridViewLoaiDM.GetRowCellValue(rowHandle, "tools_othertypelistcode").ToString();
                    txtLoaiDM_Ten.Text = gridViewLoaiDM.GetRowCellValue(rowHandle, "tools_othertypelistname").ToString();
                    txtLoaiDM_GhiChu.Text = gridViewLoaiDM.GetRowCellValue(rowHandle, "tools_othertypelistnote").ToString();
                    btnLoaiDM_Them.Enabled = true;
                    btnLoaiDM_Luu.Enabled = true;
                    txtLoaiDM_Ma.ReadOnly = true;
                    txtLoaiDM_Ten.ReadOnly = false;
                    txtLoaiDM_GhiChu.ReadOnly = false;
                    LoadDS_DanhMuc(this.selecttools_othertypelistid);

                    btnDM_Them.Enabled = true;
                    btnDM_Luu.Enabled = true;
                    txtDM_Ma.ReadOnly = true;
                    txtDM_Ten.ReadOnly = true;
                    txtDM_GiaTri.ReadOnly = true;
                    txtDM_Ma.Text = "";
                    txtDM_Ten.Text = "";
                    txtDM_GiaTri.Text = "";
                    cboDM_LoaiDMTen.ReadOnly = true;
                    cboDM_LoaiDMTen.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Danh muc
        private void btnDM_Them_Click(object sender, EventArgs e)
        {
            try
            {
                this.selecttools_otherlistid = 0;
                btnLoaiDM_Them.Enabled = false;
                btnLoaiDM_Luu.Enabled = false;
                txtLoaiDM_Ma.ReadOnly = true;
                txtLoaiDM_Ten.ReadOnly = true;
                txtLoaiDM_GhiChu.ReadOnly = true;
                txtLoaiDM_Ma.Text = "";
                txtLoaiDM_Ten.Text = "";
                txtLoaiDM_GhiChu.Text = "";

                btnDM_Them.Enabled = true;
                btnDM_Luu.Enabled = true;
                txtDM_Ma.ReadOnly = false;
                txtDM_Ten.ReadOnly = false;
                txtDM_GiaTri.ReadOnly = false;
                txtDM_Ma.Text = "";
                txtDM_Ten.Text = "";
                txtDM_GiaTri.Text = "";
                cboDM_LoaiDMTen.ReadOnly = false;
                cboDM_LoaiDMTen.EditValue = null;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnDM_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDM_Ma.Text.Trim() != "" && txtDM_Ten.Text.Trim() != "" && cboDM_LoaiDMTen.EditValue != null)
                {
                    if (this.selecttools_otherlistid == 0)
                    {
                        string kiemtratontai = "select tools_otherlistid from tools_otherlist where tools_otherlistcode='" + txtDM_Ma.Text.Trim().ToUpper() + "';";
                        DataView dataDanhSach = new DataView(condb.GetDataTable_MeL(kiemtratontai));
                        if (dataDanhSach != null && dataDanhSach.Count > 0)
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_THE_SU_DUNG_MA_NAY);
                            frmthongbao.Show();
                        }
                        else //them moi
                        {
                            string insert = "INSERT INTO tools_otherlist(tools_otherlistcode, tools_otherlistname, tools_othertypelistid, tools_otherlistvalue) VALUES ('" + txtDM_Ma.Text.Trim().ToUpper() + "', '" + txtDM_Ten.Text.Trim().Replace("'", "''") + "','" + cboDM_LoaiDMTen.EditValue.ToString() + "', '" + txtDM_GiaTri.Text.Trim().Replace("'","''") + "'); ";
                            if (condb.ExecuteNonQuery_MeL(insert))
                            {
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    else//cap nhat
                    {
                        string insert = "UPDATE tools_otherlist SET tools_otherlistname='" + txtDM_Ten.Text.Trim().Replace("'","''") + "', tools_otherlistvalue='" + txtDM_GiaTri.Text.Trim().Replace("'", "''") + "' WHERE tools_otherlistid=" + this.selecttools_otherlistid + "; ";
                        if (condb.ExecuteNonQuery_MeL(insert))
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                    LoadDS_DanhMuc(Utilities.TypeConvertParse.ToInt64(cboDM_LoaiDMTen.EditValue.ToString()));
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridControlDM_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDM.RowCount > 0)
                {
                    var rowHandle = gridViewDM.FocusedRowHandle;

                    this.selecttools_otherlistid = Utilities.TypeConvertParse.ToInt64(gridViewDM.GetRowCellValue(rowHandle, "tools_otherlistid").ToString());
                    txtDM_Ma.Text = gridViewDM.GetRowCellValue(rowHandle, "tools_otherlistcode").ToString();
                    txtDM_Ten.Text = gridViewDM.GetRowCellValue(rowHandle, "tools_otherlistname").ToString();
                    cboDM_LoaiDMTen.EditValue = gridViewDM.GetRowCellValue(rowHandle, "tools_othertypelistid");
                    txtDM_GiaTri.Text = gridViewDM.GetRowCellValue(rowHandle, "tools_otherlistvalue").ToString();
                    btnDM_Them.Enabled = true;
                    btnDM_Luu.Enabled = true;
                    txtDM_Ma.ReadOnly = true;
                    txtDM_Ten.ReadOnly = false;
                    txtDM_GiaTri.ReadOnly = false;
                    cboDM_LoaiDMTen.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewLoaiDM_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
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
