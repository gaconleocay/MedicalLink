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
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace MedicalLink.ChucNang.DichVuThanhToanRieng
{
    public partial class ucDichVuThanhToanRieng : UserControl
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #endregion
        public ucDichVuThanhToanRieng()
        {
            InitializeComponent();
        }

        private void ucDichVuThanhToanRieng_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string sqlchaydulieu = "";
                string datetungay_string = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay_string = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                gridControlDSDichVu.DataSource = null;
                if (cboKieuCapNhat.Text == "Đi kèm sang hao phí PTTT")
                {
                    sqlchaydulieu = "select ROW_NUMBER () OVER (ORDER BY vp.vienphiid,maubenhphamid desc) as stt, ser.servicepriceid, ser.maubenhphamid, vp.patientid, vp.vienphiid, hsba.patientname, vp.vienphidate as thoigianvaovien, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, ser.servicepricecode, ser.servicepricename, ser.servicepricedate, degp.departmentgroupname, de.departmentname, ser.soluong, ser.loaidoituong, ser.servicepriceid_master, serm.servicepricecode as servicepricecode_master, serm.servicepricename as servicepricename_master, ser.servicepriceid_thanhtoanrieng, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when COALESCE(vp.vienphistatus_vp,0)=0 then 'Ra viện chưa thanh toán' else 'Đã thanh toán' end) end) as trangthaivienphi from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and coalesce(servicepriceid_thanhtoanrieng,0)=0 and servicepricedate>='" + datetungay_string + "') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between '" + datetungay_string + "' and '" + datedenngay_string + "') vp on vp.vienphiid=ser.vienphiid inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='" + datetungay_string + "') serm on serm.servicepriceid=ser.servicepriceid_master inner join (select servicepricecode from servicepriceref where tinhtoanlaigiadvktc=0) serf on serf.servicepricecode=serm.servicepricecode inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid; ";
                }
                else if (cboKieuCapNhat.Text == "VTTT riêng không có ID DV đi kèm")
                {
                    sqlchaydulieu = "select ROW_NUMBER () OVER (ORDER BY vp.vienphiid,maubenhphamid desc) as stt, ser.servicepriceid, ser.maubenhphamid, vp.patientid, vp.vienphiid, hsba.patientname, vp.vienphidate as thoigianvaovien, (case when vp.vienphidate_ravien<>'0001-01-01 00:00:00' then vp.vienphidate_ravien end) as vienphidate_ravien, (case when vp.duyet_ngayduyet_vp<>'0001-01-01 00:00:00' then vp.duyet_ngayduyet_vp end) as duyet_ngayduyet_vp, ser.servicepricecode, ser.servicepricename, ser.servicepricedate, degp.departmentgroupname, de.departmentname, ser.soluong, ser.loaidoituong, ser.servicepriceid_master, serttr.servicepricecode as servicepricecode_master, serttr.servicepricename as servicepricename_master, ser.servicepriceid_thanhtoanrieng, (case when vp.vienphistatus=0 then 'Đang điều trị' else (case when COALESCE(vp.vienphistatus_vp,0)=0 then 'Ra viện chưa thanh toán' else 'Đã thanh toán' end) end) as trangthaivienphi from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and servicepriceid_thanhtoanrieng>0 and servicepriceid_master=0 and servicepricedate>='" + datetungay_string + "') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between '" + datetungay_string + "' and '" + datedenngay_string + "') vp on vp.vienphiid=ser.vienphiid inner join (select hosobenhanid, patientname from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid left join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='" + datetungay_string + "') serttr on serttr.servicepriceid=ser.servicepriceid_thanhtoanrieng inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid; ";
                }

                DataTable dataDulieu = condb.GetDataTable_HIS(sqlchaydulieu);
                if (dataDulieu != null && dataDulieu.Rows.Count > 0)
                {
                    gridControlDSDichVu.DataSource = dataDulieu;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
                btnCapNhat.Enabled = false;
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
                if (gridViewDSDichVu.RowCount > 0)
                {
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportDataGridViewToFile(gridControlDSDichVu, gridViewDSDichVu);
                    btnCapNhat.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string cauhoicapnhat = "";
                if (cboKieuCapNhat.Text == "Đi kèm sang hao phí PTTT")
                {
                    cauhoicapnhat = "Bạn có chắc chắn muốn cập nhật đi kèm sang hao phí ?";
                }
                else if (cboKieuCapNhat.Text == "VTTT riêng không có ID DV đi kèm")
                {
                    cauhoicapnhat = "Bạn có chắc chắn muốn cập nhật ID dịch vụ đi kèm ?";
                }
                DialogResult dialogResult = MessageBox.Show(cauhoicapnhat, "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    string sqlCapnhatdulieu = "";
                    string sqlbackupdulieu = "";
                    string sqlinsert_log = "";
                    string datetungay_string = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string datedenngay_string = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string dateupdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (cboKieuCapNhat.Text == "Đi kèm sang hao phí PTTT")
                    {
                        sqlbackupdulieu = "insert into tools_serviceprice_ttrieng(servicepriceid,updatetype,dateupdate) SELECT Dser.servicepriceid, '1' as updatetype, '" + dateupdate + "' as dateupdate FROM dblink('myconn','select ser.servicepriceid from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and coalesce(servicepriceid_thanhtoanrieng,0)=0 and servicepricedate>=''" + datetungay_string + "'') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between ''" + datetungay_string + "'' and ''" + datedenngay_string + "'') vp on vp.vienphiid=ser.vienphiid inner join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>=''" + datetungay_string + "'') serm on serm.servicepriceid=ser.servicepriceid_master inner join (select servicepricecode from servicepriceref where tinhtoanlaigiadvktc=0) serf on serf.servicepricecode=serm.servicepricecode inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid') AS Dser(servicepriceid integer);";
                        //update
                        sqlCapnhatdulieu = "update serviceprice set loaidoituong=7 where loaidoituong=2 and servicepriceid in (select ser.servicepriceid from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and coalesce(servicepriceid_thanhtoanrieng,0)=0 and servicepricedate>='" + datetungay_string + "') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between '" + datetungay_string + "' and '" + datedenngay_string + "') vp on vp.vienphiid=ser.vienphiid inner join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='" + datetungay_string + "') serm on serm.servicepriceid=ser.servicepriceid_master inner join (select servicepricecode from servicepriceref where tinhtoanlaigiadvktc=0) serf on serf.servicepricecode=serm.servicepricecode inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid);";
                        //Log
                        sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Cập nhật thành công đối tượng thanh toán Đi kèm sang Hao phí PTTT của dịch vụ/thuốc/vật tư SL=" + gridViewDSDichVu.RowCount + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + dateupdate + "');";
                    }
                    else if (cboKieuCapNhat.Text == "VTTT riêng không có ID DV đi kèm")
                    {
                        sqlbackupdulieu = "insert into tools_serviceprice_ttrieng(servicepriceid,updatetype,dateupdate) SELECT Dser.servicepriceid, '2' as updatetype, '" + dateupdate + "' as dateupdate FROM dblink('myconn','select ser.servicepriceid from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and servicepriceid_thanhtoanrieng>0 and servicepriceid_master=0 and servicepricedate>=''" + datetungay_string + "'') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between ''" + datetungay_string + "'' and ''" + datedenngay_string + "'') vp on vp.vienphiid=ser.vienphiid left join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>=''" + datetungay_string + "'') serttr on serttr.servicepriceid=ser.servicepriceid_thanhtoanrieng inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid') AS Dser(servicepriceid integer);";
                        //update
                        sqlCapnhatdulieu = "update serviceprice set servicepriceid_master=servicepriceid_thanhtoanrieng where servicepriceid_master=0 and servicepriceid in (select ser.servicepriceid from (select servicepriceid,maubenhphamid,vienphiid,servicepricecode,servicepricename,servicepricedate,soluong,bhyt_groupcode,loaidoituong,servicepriceid_master,servicepriceid_thanhtoanrieng,departmentgroupid,departmentid from serviceprice where loaidoituong=2 and servicepriceid_thanhtoanrieng>0 and servicepriceid_master=0 and servicepricedate>='" + datetungay_string + "') ser inner join (select vienphiid,hosobenhanid,patientid,vienphistatus,vienphistatus_vp,vienphidate,vienphidate_ravien,duyet_ngayduyet_vp from vienphi where vienphidate between '" + datetungay_string + "' and '" + datedenngay_string + "') vp on vp.vienphiid=ser.vienphiid left join (select servicepriceid,servicepricecode,servicepricename from serviceprice where servicepricedate>='" + datetungay_string + "') serttr on serttr.servicepriceid=ser.servicepriceid_thanhtoanrieng inner join (select departmentgroupid,departmentgroupname from departmentgroup) degp on degp.departmentgroupid=ser.departmentgroupid inner join (select departmentid,departmentname from department where departmenttype in (2,3,6,7,9)) de on de.departmentid=ser.departmentid);";
                        //Log
                        sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime) VALUES ('" + SessionLogin.SessionUsercode + "', 'Cập nhật thành công ID đi kèm = ID thanh toán riêng của dịch vụ/thuốc/vật tư SL=" + gridViewDSDichVu.RowCount + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + dateupdate + "');";
                    }
                    if (condb.ExecuteNonQuery_Dblink(sqlbackupdulieu))
                    {
                        if (condb.ExecuteNonQuery_HIS(sqlCapnhatdulieu))
                        {
                            MessageBox.Show("Cập nhật thành công SL=" + gridViewDSDichVu.RowCount + "!", "Thông báo !");
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại!", "Thông báo !");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Thông báo !");
                    }
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridViewDSDichVu_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
    }
}
