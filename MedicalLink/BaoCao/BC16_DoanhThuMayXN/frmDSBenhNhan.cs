using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao.BC16_DoanhThuMayXN
{
    public partial class frmDSBenhNhan : Form
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private DoanhThuTheoMayXNFilterDTO filter { get; set; }

        #endregion


        public frmDSBenhNhan()
        {
            InitializeComponent();
        }
        public frmDSBenhNhan(DoanhThuTheoMayXNFilterDTO _filter)
        {
            InitializeComponent();
            this.filter = _filter;
        }


        #region Load
        private void frmDSBenhNhan_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _servicepricecode = " and servicepricecode='" + this.filter.servicepricecode + "' ";
                string _tieuchi_ser = this.filter.tieuchi_ser;
                string _tieuchi_vp = this.filter.tieuchi_vp;
                string _tieuchi_mbp = this.filter.tieuchi_mbp;
                string _trangthaibenhan = this.filter.trangthaibenhan;
                string _doituong_vp = this.filter.doituong_vp;
                string _doituong_ser = this.filter.doituong_ser;
                string _loaibaocao = this.filter.loaibaocao;
                decimal _gia_bhyt = this.filter.gia_bhyt??0;
                decimal _gia_vp = this.filter.gia_vp ?? 0;
                decimal _gia_yc = this.filter.gia_yc ?? 0;
                decimal _gia_nnn = this.filter.gia_nnn ?? 0;

                string _idmay_xn = "";
                if (this.filter.idmay_xn != "" && this.filter.idmay_xn != null)
                {
                    _idmay_xn = " WHERE ser.idmayxn='" + this.filter.idmay_xn + "'";
                }
                else
                {
                    _idmay_xn = " WHERE ser.idmayxn is null";
                }

                string sql_getdata = @" SELECT ROW_NUMBER() OVER (ORDER BY ser.servicepricedate) as stt, vp.vienphiid, vp.patientid, mbp.maubenhphamid, mbp.maubenhphamdate, hsba.patientname, bh.bhytcode, degp.departmentgroupname, de.departmentname, ngd.username as nguoichidinh, ser.servicepricecode, ser.servicepricename, ser.soluong, ser.servicepricemoney_bhyt, ser.servicepricemoney_nhandan, ser.servicepricemoney, ser.servicepricemoney_nuocngoai, (case ser.loaidoituong when 0 then 'BHYT' when 1 then 'Viện phí' when 2 then 'Đi kèm' when 3 then 'Yêu cầu' when 4 then 'BHYT+YC' when 5 then 'Hao phí giường, CK' when 6 then 'BHYT+phụ thu' when 7 then 'Hao phí PTTT' when 8 then 'Đối tượng khác' when 9 then 'Hao phí khác' when 20 then 'Thanh toán riêng' end) as loaidoituong, vp.vienphidate, (case when vp.vienphistatus>0 then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when vp.vienphistatus_vp=1 then 'Đã thanh toán' else 'Chưa thanh toán' end) end) as vienphistatus, (case when tkq.usercode like '%-sh' then 'Khoa sinh hóa' when tkq.usercode like '%-hh' then 'Khoa huyết học' when tkq.usercode like '%-vs' then 'Khoa vi sinh' when tkq.usercode like '%-gp' then 'Khoa giải phẫu bệnh' when tkq.usercode like '%-xndk' then 'Khoa xét nghiệm đa khoa' else '' end) as khoatra_kq FROM (select se.servicepriceid,se.vienphiid,se.maubenhphamid,se.servicepricecode,se.servicepricename,se.soluong,se.loaidoituong,se.departmentgroupid,se.departmentid,se.servicepricedate,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,servicepricemoney_nuocngoai,(select s.idmayxn from service s where s.servicepriceid=se.servicepriceid and s.servicedate>'2017-05-01 00:00:00' order by coalesce(s.idmayxn,0) desc limit 1) as idmayxn from serviceprice se where se.bhyt_groupcode='03XN' and servicepricemoney_bhyt=" + _gia_bhyt + " and servicepricemoney_nhandan=" + _gia_vp + " and servicepricemoney=" + _gia_yc + " and servicepricemoney_nuocngoai=" + _gia_nnn + " " + _servicepricecode + _tieuchi_ser + _doituong_ser + ") ser inner join (select maubenhphamid,maubenhphamdate,userid,usertrakq from maubenhpham where maubenhphamgrouptype=0 " + _tieuchi_mbp + _loaibaocao + ") mbp on mbp.maubenhphamid=ser.maubenhphamid inner join (select vienphiid,patientid,hosobenhanid,bhytid,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp,vienphistatus,vienphistatus_vp from vienphi where 1=1 " + _tieuchi_vp + _doituong_vp + _trangthaibenhan + ") vp on vp.vienphiid=ser.vienphiid inner join (select hosobenhanid,patientname from hosobenhan where hosobenhandate>'2017-05-01 00:00:00') hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select bhytid,bhytcode from bhyt where bhytdate>'2017-05-01 00:00:00') bh on bh.bhytid=vp.bhytid left join (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userid left join nhompersonnel tkq on tkq.userhisid=mbp.usertrakq inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid left join (select departmentid,departmentname from department where departmenttype in (2,3,9)) de on de.departmentid=ser.departmentid " + _idmay_xn + "; ";

                gridControlBNDetail.DataSource = condb.GetDataTable_HIS(sql_getdata);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion


        #region Custom
        private void gridViewBNDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBNDetail);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("DANH SÁCH BỆNH NHÂN CHỈ ĐỊNH DỊCH VỤ", _dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

    }
}
