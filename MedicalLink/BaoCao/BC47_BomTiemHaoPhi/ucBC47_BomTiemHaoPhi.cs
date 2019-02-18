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
using System.Globalization;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.BaoCao
{
    public partial class ucBC47_BomTiemHaoPhi : UserControl
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        #endregion


        public ucBC47_BomTiemHaoPhi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC47_BomTiemHaoPhi_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuMacDinh();
                LoadDanhSachKhoa();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                gridControlDataBC.Visible = true;
                gridControlDataBC.Dock = DockStyle.Fill;
                gridControlBNDetail.Visible = false;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                var lstDSKhoa = Base.SessionLogin.LstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                chkcomboListDSKhoa.CheckAll();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi_ser = " and servicepricedate>'2018-01-01 00:00:00' ";
                string _tieuchi_vp = " and vienphidate>'2018-01-01 00:00:00' ";
                string _tieuchi_mbp = " and maubenhphamdate>'2018-01-01 00:00:00' ";
                string _tieuchi_hsba = " and hosobenhandate>'2018-01-01 00:00:00' ";
                string _lstKhoaChonLayBC = " and departmentgroupid in (0";
                string _vienphi_stt = "";


                string datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //TIeu chi
                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    _tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    _tieuchi_ser = " and servicepricedate>='" + datetungay + "' ";
                    _tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    _tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                    _tieuchi_hsba = " and hosobenhandate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    _tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    _vienphi_stt = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    _vienphi_stt = " and vienphistatus>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    _vienphi_stt = " and vienphistatus>0 and vienphistatus_vp=1 ";
                }
                //Khoa
                List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        _lstKhoaChonLayBC += "," + lstKhoaCheck[i];
                    }
                    _lstKhoaChonLayBC += ")";
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                    return;
                }
                if (radioXemTongHop.Checked)//tong hop
                {
                    string _sql_timkiem = $@"SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupname,
	count(serxn.*) as soluong_luot,
	sum(serhp.soluong_hpng) as soluong_hpng,
	sum(serhp.soluong_hppttt) as soluong_hppttt
FROM
	(select ser.vienphiid,ser.departmentgroupid from (select vienphiid,departmentgroupid,servicepricedate,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','07KTC') {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 {_tieuchi_mbp}) mbp on mbp.maubenhphamid=ser.maubenhphamid group by ser.vienphiid,ser.departmentgroupid,ser.servicepricedate) serxn
	left join (select ser.vienphiid,ser.departmentgroupid,sum(case when ser.loaidoituong=5 then ser.soluong else 0 end) as soluong_hpng,sum(case when ser.loaidoituong=7 then ser.soluong else 0 end) as soluong_hppttt from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,soluong from serviceprice where bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and loaidoituong in (5,7) {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select medicinecode from medicine_ref where medicinegroupcode='VT02') serf on serf.medicinecode=ser.servicepricecode group by ser.vienphiid,ser.departmentgroupid) serhp on serhp.vienphiid=serxn.vienphiid
	inner join (select vienphiid,patientid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 {_tieuchi_vp} {_vienphi_stt}) vp on vp.vienphiid=serxn.vienphiid
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=serxn.departmentgroupid
GROUP BY degp.departmentgroupname;";
                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlDataBC.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlDataBC.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                    }
                }
                else//chi tiet
                {
                    string _sql_timkiemCT = $@"SELECT row_number () over (order by vp.vienphidate) as stt,
	vp.vienphiid,
	vp.patientid,
	hsba.patientname,
	count(serxn.*) as soluong_luot,
	sum(serhp.soluong_hpng) as soluong_hpng,
	sum(serhp.soluong_hppttt) as soluong_hppttt,
	vp.vienphidate,
	(case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien,
	(case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp,
	(case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus
FROM 
	(select ser.vienphiid,ser.departmentgroupid from (select vienphiid,departmentgroupid,servicepricedate,maubenhphamid from serviceprice where bhyt_groupcode in ('03XN','07KTC') {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select maubenhphamid from maubenhpham where maubenhphamgrouptype=0 {_tieuchi_mbp}) mbp on mbp.maubenhphamid=ser.maubenhphamid group by ser.vienphiid,ser.departmentgroupid,ser.servicepricedate) serxn
	left join (select ser.vienphiid,ser.departmentgroupid,sum(case when ser.loaidoituong=5 then ser.soluong else 0 end) as soluong_hpng,sum(case when ser.loaidoituong=7 then ser.soluong else 0 end) as soluong_hppttt from (select vienphiid,departmentgroupid,servicepricecode,loaidoituong,soluong from serviceprice where bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and loaidoituong in (5,7) {_tieuchi_ser}{_lstKhoaChonLayBC}) ser inner join (select medicinecode from medicine_ref where medicinegroupcode='VT02') serf on serf.medicinecode=ser.servicepricecode group by ser.vienphiid,ser.departmentgroupid) serhp on serhp.vienphiid=serxn.vienphiid
	inner join (select vienphiid,patientid,hosobenhanid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 {_tieuchi_vp} {_vienphi_stt}) vp on vp.vienphiid=serxn.vienphiid
	inner join (select hosobenhanid,patientname from hosobenhan where 1=1 {_tieuchi_hsba}) hsba on hsba.hosobenhanid=vp.hosobenhanid
GROUP BY vp.vienphiid,vp.patientid,hsba.patientname,vp.vienphidate,vp.vienphidate_ravien,vp.vienphistatus_vp,vp.duyet_ngayduyet_vp,vp.vienphistatus";

                    DataTable _dataBCChiTiet = condb.GetDataTable_HIS(_sql_timkiemCT);
                    if (_dataBCChiTiet != null && _dataBCChiTiet.Rows.Count > 0)
                    {
                        gridControlBNDetail.DataSource = _dataBCChiTiet;
                    }
                    else
                    {
                        gridControlBNDetail.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Export va Print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_47_BomKimTiemHaoPhi_TongHop.xlsx";
                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
                }
                else
                {
                    DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBNDetail);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("DANH SÁCH BỆNH NHÂN CHỈ ĐỊNH DỊCH VỤ", _dataBaoCao);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);
                string fileTemplatePath = "BC_47_BomKimTiemHaoPhi_TongHop.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }


        #endregion

        #region Custom
        private void gridViewDataBC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        #region Events
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlDataBC.Visible = true;
                    gridControlDataBC.DataSource = null;
                    gridControlDataBC.Dock = DockStyle.Fill;
                    gridControlBNDetail.Visible = false;
                    btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void radioXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemChiTiet.Checked)
                {
                    radioXemTongHop.Checked = false;
                    gridControlBNDetail.Visible = true;
                    gridControlBNDetail.DataSource = null;
                    gridControlBNDetail.Dock = DockStyle.Fill;
                    gridControlDataBC.Visible = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

    }
}
