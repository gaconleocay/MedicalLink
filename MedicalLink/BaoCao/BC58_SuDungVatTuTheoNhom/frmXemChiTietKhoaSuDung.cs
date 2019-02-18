using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;
using MedicalLink.ClassCommon.BaoCao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao.BC58_SuDungVatTuTheoNhom
{
    public partial class frmXemChiTietKhoaSuDung : Form
    {
        #region Declaration
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        #endregion

        public frmXemChiTietKhoaSuDung()
        {
            InitializeComponent();
        }
        public frmXemChiTietKhoaSuDung(BC58FilterTheoKhoaDTO _filter)
        {
            InitializeComponent();
            LayDuLieuChiTiet(_filter);
        }


        private void LayDuLieuChiTiet(BC58FilterTheoKhoaDTO _filter)
        {
            try
            {
                string _lstservicepricecode = " and servicepricecode in ('000'";
                string _sqlLayDSThuoc = "select medicinecode from medicine_ref where medicinegroupcode='" + _filter.medicinecode + "';";
                DataTable _dataDSThuoc = condb.GetDataTable_HIS(_sqlLayDSThuoc);
                if (_dataDSThuoc.Rows.Count > 0)
                {
                    for (int i = 0; i < _dataDSThuoc.Rows.Count; i++)
                    {
                        _lstservicepricecode += ",'" + _dataDSThuoc.Rows[i]["medicinecode"].ToString() + "'";
                    }
                }
                _lstservicepricecode = _lstservicepricecode.Replace("('000',", "(") + ") ";


                string _sql_timkiem = $@"SELECT row_number () over (order by degp.departmentgroupname) as stt,
	degp.departmentgroupid,
	degp.departmentgroupname,
	sum(O.noitru_sl) as noitru_sl,
	sum(O.noitru_thanhtien) as noitru_thanhtien,
	sum(O.tutruc_sl) as tutruc_sl,
	sum(O.tutruc_thanhtien) as tutruc_thanhtien,
	sum(O.noitru_sl)+sum(O.tutruc_sl) as tong_sl,
	sum(O.noitru_thanhtien)+sum(O.tutruc_thanhtien) as tong_thanhtien
FROM 
	(select ser.departmentgroupid,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong else 0 end) as noitru_sl,
		sum(case when mbp.medicinestoreid in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia else 0 end) as noitru_thanhtien,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong else 0 end) as tutruc_sl,
		sum(case when mbp.medicinestoreid not in (2,88,87,175,94,96,5,183,119,141,124) then ser.soluong*ser.dongia else 0 end) as tutruc_thanhtien
	from 
		(select vienphiid from vienphi where 1=1 {_filter.tieuchi_vp} {_filter.trangthai_vp} {_filter.doituongbenhnhanid}) vp
		  inner join (select vienphiid,departmentgroupid,servicepricecode,maubenhphamid,
						(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia,(case when maubenhphamphieutype=0 then soluong else 0-soluong end) as soluong
					from serviceprice where thuockhobanle=0 and soluong>0 {_lstservicepricecode} {_filter.tieuchi_ser} {_filter.lstKhoaChonLayBC}) ser on ser.vienphiid=vp.vienphiid
		  inner join (select maubenhphamid,medicinestoreid from maubenhpham where maubenhphamgrouptype in (5,6) {_filter.tieuchi_mbp} {_filter.lstKhoaChonLayBC} {_filter.lstKhoTTChonLayBC}) mbp on mbp.maubenhphamid=ser.maubenhphamid
	group by ser.departmentgroupid,ser.dongia,ser.servicepricecode) O
	inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=O.departmentgroupid
WHERE O.noitru_sl<>0 or O.tutruc_sl<>0
GROUP BY degp.departmentgroupid,degp.departmentgroupname;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    gridControlBaoCao.DataSource = _dataBaoCao;
                }
                else
                {
                    gridControlBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewBaoCao.RowCount > 0)
                {
                    DataTable data_XuatBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBaoCao);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("CHI TIẾT KHOA SỬ DỤNG", data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #region Custom
        private void gridViewBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
    }
}
