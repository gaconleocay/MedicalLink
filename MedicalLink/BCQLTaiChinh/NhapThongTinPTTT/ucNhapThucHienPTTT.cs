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

namespace MedicalLink.BCQLTaiChinh.NhapThongTinPTTT
{
    public partial class ucNhapThucHienPTTT : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new Base.ConnectDatabase();


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
                LoadDataNguoiThucHien();
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
                string _tieuchi_bill = "";
                string _lstPhongChonLayBC = "";
                string _listuserid = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");


                //phong
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                if (lstPhongCheck.Count > 0)
                {
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        _lstPhongChonLayBC += lstPhongCheck[i] + ",";
                    }
                    _lstPhongChonLayBC += lstPhongCheck[lstPhongCheck.Count - 1];
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }




                string _sqlLayData = @"";
                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sqlLayData);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataDV.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlDataDV.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
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

        #endregion

        #region Process
        private void LoadDataNguoiThucHien()
        {
            try
            {
                //if (this.dataNguoiThucHien == null || this.dataNguoiThucHien.Rows.Count <= 0)
                //{
                //    string getnguoithuchien = "select 0 as userhisid, '' as usercode, '' as username, '' as usercodename union all select A.userhisid, A.usercode, A.username, A.usercodename from (select nv.userhisid, nv.usercode, nv.username, (nv.usercode || ' - ' || nv.username) as usercodename from nhompersonnel nv inner join (select usercode, departmentid from tbldepartment) ude on ude.usercode=nv.usercode inner join (select departmentid from department where departmentgroupid in (" + lstdepartmentgroupid + ")) de on de.departmentid=ude.departmentid group by nv.userhisid, nv.usercode, nv.username order by nv.username) A; ";
                //    this.dataNguoiThucHien = condb.GetDataTable_HIS(getnguoithuchien);
                //}

                ////repositoryItemGridLookUp_MoChinh.DataSource = this.dataNguoiThucHien;
                ////repositoryItemGridLookUp_MoChinh.DisplayMember = "usercodename";
                ////repositoryItemGridLookUp_MoChinh.ValueMember = "usercode";

                //repositoryItemGridLookUp_GayMe.DataSource = dataNguoiThucHien;
                //repositoryItemGridLookUp_GayMe.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_GayMe.ValueMember = "userhisid";

                //repositoryItemGridLookUp_Phu1.DataSource = dataNguoiThucHien;
                //repositoryItemGridLookUp_Phu1.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_Phu1.ValueMember = "userhisid";

                //repositoryItemGridLookUp_Phu2.DataSource = dataNguoiThucHien;
                //repositoryItemGridLookUp_Phu2.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_Phu2.ValueMember = "userhisid";

                //repositoryItemGridLookUp_GiupViec1.DataSource = dataNguoiThucHien;
                //repositoryItemGridLookUp_GiupViec1.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_GiupViec1.ValueMember = "userhisid";

                //repositoryItemGridLookUp_GiupViec2.DataSource = dataNguoiThucHien;
                //repositoryItemGridLookUp_GiupViec2.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_GiupViec2.ValueMember = "userhisid";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

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
