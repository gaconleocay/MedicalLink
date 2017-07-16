using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.BaoCao
{
    public partial class ucBCSuDungThuoc : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataBaoCao { get; set; }
        public ucBCSuDungThuoc()
        {
            InitializeComponent();
        }

        #region Load
        private void ucUpdateDataSerPrice_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            gridControlSoCDHA.DataSource = null;
            LoadDanhMucThuoc();
        }
        private void LoadDanhMucThuoc()
        {
            try
            {
                string sqlLayDanhMucThuoc = "select ROW_NUMBER () OVER (ORDER BY trim(medicinename)) as stt, medicinecode, medicinename, donvitinh from medicine_ref where medicinecode not like '%.%' and datatype=0 and medicinegroupcode<>'NHATHUOC' ; ";
                DataTable danhSanhThuoc = condb.GetDataTable_HIS(sqlLayDanhMucThuoc);
                cboDSThuoc.Properties.DataSource = danhSanhThuoc;
                cboDSThuoc.Properties.DisplayMember = "medicinename";
                cboDSThuoc.Properties.ValueMember = "medicinecode";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                if (cboDSThuoc.EditValue == null)
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string lstservicepricecode = "";
                string sql_lstservicepricecode = "select medicinecode from medicine_ref where medicinecode like '" + cboDSThuoc.EditValue + "%';";
                DataTable data_lstservicepricecode = condb.GetDataTable_HIS(sql_lstservicepricecode);
                if (data_lstservicepricecode != null && data_lstservicepricecode.Rows.Count > 0)
                {
                    for (int i = 0; i < data_lstservicepricecode.Rows.Count; i++)
                    {
                        lstservicepricecode = lstservicepricecode + "'" + data_lstservicepricecode.Rows[i]["medicinecode"].ToString() + "',";
                    }
                    lstservicepricecode = lstservicepricecode.Substring(0, lstservicepricecode.Length - 1);
                }

                string sql_timkiem = "SELECT row_number () over (order by degp.departmentgroupname) as stt, degp.departmentgroupname, sum(case when data.datatype=1 then data.sl_1 else 0 end) as sl_dangdt_1, sum(case when data.datatype=2 then data.sl_1 else 0 end) as sl_chuatt_1, sum(case when data.datatype=3 then data.sl_1 else 0 end) as sl_datt_1, sum(data.sl_1) as sl_tong_1, sum(case when data.datatype=1 then data.sl_2 else 0 end) as sl_dangdt_2, sum(case when data.datatype=2 then data.sl_2 else 0 end) as sl_chuatt_2, sum(case when data.datatype=3 then data.sl_2 else 0 end) as sl_datt_2, sum(data.sl_2) as sl_tong_2, sum(case when data.datatype=1 then data.sl_3 else 0 end) as sl_dangdt_3, sum(case when data.datatype=2 then data.sl_3 else 0 end) as sl_chuatt_3, sum(case when data.datatype=3 then data.sl_3 else 0 end) as sl_datt_3, sum(data.sl_3) as sl_tong_3, sum(case when data.datatype=1 then data.sl_4 else 0 end) as sl_dangdt_4, sum(case when data.datatype=2 then data.sl_4 else 0 end) as sl_chuatt_4, sum(case when data.datatype=3 then data.sl_4 else 0 end) as sl_datt_4, sum(data.sl_4) as sl_tong_4, sum(case when data.datatype=1 then data.sl_5 else 0 end) as sl_dangdt_5, sum(case when data.datatype=2 then data.sl_5 else 0 end) as sl_chuatt_5, sum(case when data.datatype=3 then data.sl_5 else 0 end) as sl_datt_5, sum(data.sl_5) as sl_tong_5, sum(case when data.datatype=1 then data.sl_6 else 0 end) as sl_dangdt_6, sum(case when data.datatype=2 then data.sl_6 else 0 end) as sl_chuatt_6, sum(case when data.datatype=3 then data.sl_6 else 0 end) as sl_datt_6, sum(data.sl_6) as sl_tong_6, sum(case when data.datatype=1 then data.sl_7 else 0 end) as sl_dangdt_7, sum(case when data.datatype=2 then data.sl_7 else 0 end) as sl_chuatt_7, sum(case when data.datatype=3 then data.sl_7 else 0 end) as sl_datt_7, sum(data.sl_7) as sl_tong_7, sum(case when data.datatype=1 then data.sl_8 else 0 end) as sl_dangdt_8, sum(case when data.datatype=2 then data.sl_8 else 0 end) as sl_chuatt_8, sum(case when data.datatype=3 then data.sl_8 else 0 end) as sl_datt_8, sum(data.sl_8) as sl_tong_8, sum(case when data.datatype=1 then data.sl_9 else 0 end) as sl_dangdt_9, sum(case when data.datatype=2 then data.sl_9 else 0 end) as sl_chuatt_9, sum(case when data.datatype=3 then data.sl_9 else 0 end) as sl_datt_9, sum(data.sl_9) as sl_tong_9, sum(case when data.datatype=1 then data.sl_10 else 0 end) as sl_dangdt_10, sum(case when data.datatype=2 then data.sl_10 else 0 end) as sl_chuatt_10, sum(case when data.datatype=3 then data.sl_10 else 0 end) as sl_datt_10, sum(data.sl_10) as sl_tong_10, sum(case when data.datatype=1 then data.sl_11 else 0 end) as sl_dangdt_11, sum(case when data.datatype=2 then data.sl_11 else 0 end) as sl_chuatt_11, sum(case when data.datatype=3 then data.sl_11 else 0 end) as sl_datt_11, sum(data.sl_11) as sl_tong_11, sum(case when data.datatype=1 then data.sl_12 else 0 end) as sl_dangdt_12, sum(case when data.datatype=2 then data.sl_12 else 0 end) as sl_chuatt_12, sum(case when data.datatype=3 then data.sl_12 else 0 end) as sl_datt_12, sum(data.sl_12) as sl_tong_12 FROM (select departmentgroupid,departmentgroupname from departmentgroup) degp left join ( select 1 as datatype, ser.departmentgroupid, sum(case when to_char(ser.servicepricedate, 'mm')='01' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_1, sum(case when to_char(ser.servicepricedate, 'mm')='02' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_2, sum(case when to_char(ser.servicepricedate, 'mm')='03' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_3, sum(case when to_char(ser.servicepricedate, 'mm')='04' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_4, sum(case when to_char(ser.servicepricedate, 'mm')='05' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_5, sum(case when to_char(ser.servicepricedate, 'mm')='06' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_6, sum(case when to_char(ser.servicepricedate, 'mm')='07' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_7, sum(case when to_char(ser.servicepricedate, 'mm')='08' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_8, sum(case when to_char(ser.servicepricedate, 'mm')='09' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_9, sum(case when to_char(ser.servicepricedate, 'mm')='10' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_10, sum(case when to_char(ser.servicepricedate, 'mm')='11' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_11, sum(case when to_char(ser.servicepricedate, 'mm')='12' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_12 from (select vienphiid from vienphi where vienphistatus=0 and vienphidate>'2016-01-01 00:00:00') vp inner join (select vienphiid,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype from serviceprice where servicepricecode in (" + lstservicepricecode + ") and thuockhobanle=0 and servicepricedate>'2016-01-01 00:00:00' and soluong>0) ser on ser.vienphiid=vp.vienphiid group by ser.departmentgroupid union all select 2 as datatype, ser.departmentgroupid, sum(case when to_char(ser.servicepricedate, 'mm')='01' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_1, sum(case when to_char(ser.servicepricedate, 'mm')='02' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_2, sum(case when to_char(ser.servicepricedate, 'mm')='03' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_3, sum(case when to_char(ser.servicepricedate, 'mm')='04' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_4, sum(case when to_char(ser.servicepricedate, 'mm')='05' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_5, sum(case when to_char(ser.servicepricedate, 'mm')='06' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_6, sum(case when to_char(ser.servicepricedate, 'mm')='07' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_7, sum(case when to_char(ser.servicepricedate, 'mm')='08' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_8, sum(case when to_char(ser.servicepricedate, 'mm')='09' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_9, sum(case when to_char(ser.servicepricedate, 'mm')='10' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_10, sum(case when to_char(ser.servicepricedate, 'mm')='11' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_11, sum(case when to_char(ser.servicepricedate, 'mm')='12' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_12 from (select vienphiid from vienphi where vienphistatus>0 and coalesce(vienphistatus_vp,0)=0) vp inner join (select vienphiid,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype from serviceprice where servicepricecode in (" + lstservicepricecode + ") and thuockhobanle=0 and soluong>0) ser on ser.vienphiid=vp.vienphiid group by ser.departmentgroupid union all select 3 as datatype, ser.departmentgroupid, sum(case when to_char(ser.servicepricedate, 'mm')='01' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_1, sum(case when to_char(ser.servicepricedate, 'mm')='02' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_2, sum(case when to_char(ser.servicepricedate, 'mm')='03' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_3, sum(case when to_char(ser.servicepricedate, 'mm')='04' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_4, sum(case when to_char(ser.servicepricedate, 'mm')='05' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_5, sum(case when to_char(ser.servicepricedate, 'mm')='06' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_6, sum(case when to_char(ser.servicepricedate, 'mm')='07' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_7, sum(case when to_char(ser.servicepricedate, 'mm')='08' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_8, sum(case when to_char(ser.servicepricedate, 'mm')='09' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_9, sum(case when to_char(ser.servicepricedate, 'mm')='10' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_10, sum(case when to_char(ser.servicepricedate, 'mm')='11' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_11, sum(case when to_char(ser.servicepricedate, 'mm')='12' then (case when ser.maubenhphamphieutype=0 then ser.soluong else 0-ser.soluong end) else 0 end) as sl_12 from (select vienphiid from vienphi where vienphistatus>0 and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "') vp inner join (select vienphiid,departmentgroupid,servicepricedate,soluong,maubenhphamphieutype from serviceprice where servicepricecode in (" + lstservicepricecode + ") and thuockhobanle=0 and soluong>0) ser on ser.vienphiid=vp.vienphiid group by ser.departmentgroupid ) data ON degp.departmentgroupid=data.departmentgroupid GROUP BY degp.departmentgroupname; ";

                this.dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoCDHA.DataSource = this.dataBaoCao;
                }
                else
                {
                    gridControlSoCDHA.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";
                string fileTemplatePath = "BC_SuDungThuoc.xlsx";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_code = new ClassCommon.reportExcelDTO();
                reportitem_code.name = Base.bienTrongBaoCao.MEDICINECODE;
                reportitem_code.value = cboDSThuoc.EditValue.ToString().ToUpper();
                thongTinThem.Add(reportitem_code);
                ClassCommon.reportExcelDTO reportitem_name = new ClassCommon.reportExcelDTO();
                reportitem_name.name = Base.bienTrongBaoCao.MEDICINENAME;
                reportitem_name.value = cboDSThuoc.Text.ToUpper();
                thongTinThem.Add(reportitem_name);

                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void bandedGridViewSoCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
