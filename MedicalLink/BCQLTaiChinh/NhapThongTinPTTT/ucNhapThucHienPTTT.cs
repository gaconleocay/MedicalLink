using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalLink.ClassCommon;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using MedicalLink.Utilities.GUIGridView;
using MedicalLink.ClassCommon.BCQLTaiChinh;

namespace MedicalLink.BCQLTaiChinh.NhapThongTinPTTT
{
    public partial class ucNhapThucHienPTTT : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new Base.ConnectDatabase();
        private DataTable DataNguoiThucHien { get; set; }
        private List<NhapThucHienPTTTDTO> lstBaoCao { get; set; }

        #endregion
        public ucNhapThucHienPTTT()
        {
            InitializeComponent();
        }

        #region Load
        private void ucNhapThucHienPTTT_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhMucKhoa();
                LoadDataNguoiThucHien();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhMucKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    chkcomboListDSKhoa.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDataNguoiThucHien()
        {
            try
            {
                if (this.DataNguoiThucHien == null || this.DataNguoiThucHien.Rows.Count <= 0)
                {
                    string getnguoithuchien = "select 0 as userhisid, '' as usercode, '' as username, '' as usercodename union all select nv.userhisid, nv.usercode, nv.username, (nv.usercode || ' - ' || nv.username) as usercodename from nhompersonnel nv;";
                    this.DataNguoiThucHien = condb.GetDataTable_HIS(getnguoithuchien);
                }

                repositoryItemGridLookUp_MoChinh.DataSource = DataNguoiThucHien;
                repositoryItemGridLookUp_MoChinh.DisplayMember = "usercodename";
                repositoryItemGridLookUp_MoChinh.ValueMember = "userhisid";

                //repositoryItemGridLookUp_MoiMoChinh.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_MoiMoChinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_MoiMoChinh.ValueMember = "userhisid";

                //repositoryItemGridLookUp_BSGayMe.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_BSGayMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_BSGayMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_MoiGayMe.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_MoiGayMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_MoiGayMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_KTVPhuMe.DataSource = DataNguoiThucHien;
                //repositoryItemGridLookUp_KTVPhuMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_KTVPhuMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_Phu1.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_Phu1.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_Phu1.ValueMember = "usercode";

                //repositoryItemGridLookUp_Phu2.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_Phu2.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_Phu2.ValueMember = "usercode";

                //repositoryItemGridLookUp_KTVHoiTinh.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_KTVHoiTinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_KTVHoiTinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_DDHoiTinh.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_DDHoiTinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_DDHoiTinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_DDHanhChinh.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_DDHanhChinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_DDHanhChinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_HoLy.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_HoLy.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_HoLy.ValueMember = "usercode";

                //repositoryItemGridLookUp_DungCuVien.DataSource = this.DataNguoiThucHien;
                //repositoryItemGridLookUp_DungCuVien.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_DungCuVien.ValueMember = "usercode";

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _tieuchi_mbp = "";
                string _tieuchi_bhyt = "";
                string _trangthainhappttt = "";
                string _trangthaivp = "";
                string _doituongbenhnhanid = "";
                //string _loaivienphiid = "";
                string _tieuchi_hsba = "";
                string _lstPhongChonLayBC = "";
                string _listuserid = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");


                //Lay phong
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        _lstPhongChonLayBC += lstPhongCheck[i] + ",";
                    }
                    _lstPhongChonLayBC += lstPhongCheck[lstPhongCheck.Count - 1];
                    //

                    this.lstBaoCao = new List<NhapThucHienPTTTDTO>();
                    NhapThucHienPTTTDTO _item1 = new NhapThucHienPTTTDTO();
                    lstBaoCao.Add(_item1);
                    gridControlDataDV.DataSource = lstBaoCao;

                    string _sqlLayData = @"";
                    DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlLayData);
                    if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDataDV.DataSource = lstBaoCao;
                    }
                    else
                    {
                        //gridControlDataDV.DataSource = null;
                        //ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        //frmthongbao.Show();
                    }

                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void gridViewDataDV_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void chkcomboListDSKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                chkcomboListDSPhong.Properties.Items.Clear();
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    //Load danh muc phong theo khoa da chon
                    List<ClassCommon.classUserDepartment> lstDSPhong = new List<classUserDepartment>();
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        List<ClassCommon.classUserDepartment> lstdsphongthuockhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt32(lstKhoaCheck[i].ToString())).ToList();
                        lstDSPhong.AddRange(lstdsphongthuockhoa);
                    }
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        chkcomboListDSPhong.Properties.DataSource = lstDSPhong;
                        chkcomboListDSPhong.Properties.DisplayMember = "departmentname";
                        chkcomboListDSPhong.Properties.ValueMember = "departmentid";
                    }
                    chkcomboListDSPhong.CheckAll();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }


        #endregion

        #region Custom
        private void gridViewDataDV_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        private void gridViewDataDV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "img_nhapptttstt")
                {
                    string val = gridViewDataDV.GetRowCellValue(e.RowHandle, "thuchienptttid").ToString();
                    if (val != "0")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imMenu.Images[4]);
                        e.Graphics.DrawImage(imMenu.Images[4], pos);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Process

        #endregion

        #region In va xuat file
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                //string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                //string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                //string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                //List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                //ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                //reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                //reportitem.value = tungaydenngay;
                //thongTinThem.Add(reportitem);
                //ClassCommon.reportExcelDTO reportitem_khoa = new ClassCommon.reportExcelDTO();
                //reportitem_khoa.name = Base.bienTrongBaoCao.DEPARTMENTNAME;
                //reportitem_khoa.value = chkcomboListDSPhong.Text.ToUpper();
                //thongTinThem.Add(reportitem_khoa);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataDV);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("", _dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }




        #endregion





    }
}
