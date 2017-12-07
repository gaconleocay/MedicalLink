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
using MedicalLink.Utilities.GridControl;
using MedicalLink.Utilities;

namespace MedicalLink.BaoCao
{
    public partial class ucTinhHinhKhamChuaBenhTongHop : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string _ngoaitinhId = "31";
        public ucTinhHinhKhamChuaBenhTongHop()
        {
            InitializeComponent();
        }

        #region Load
        private void ucTinhHinhKhamChuaBenhTongHop_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LayMaNgoaiTinh();
        }
        private void LayMaNgoaiTinh()
        {
            try
            {
                string _sqlLayNgoaiTinh = "SELECT mayte FROM hospital";
                DataTable _dataYte = condb.GetDataTable_HIS(_sqlLayNgoaiTinh);
                if (_dataYte != null && _dataYte.Rows.Count > 0)
                {
                    this._ngoaitinhId = _dataYte.Rows[0]["mayte"].ToString().Substring(0,2);
                }
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
                string _datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _sql_timkiem = "SELECT vp.thang, sum(case when vp.loaivienphiid<>0 then 1 else 0 end) as kb_tong, sum(case when vp.loaivienphiid<>0 and hsba.gioitinhcode='02' then 1 else 0 end) as kb_nu, sum(case when vp.loaivienphiid<>0 and vp.doituongbenhnhanid=1 then 1 else 0 end) as kb_bhyt, sum(case when vp.loaivienphiid<>0 and vp.doituongbenhnhanid<>1 then 1 else 0 end) as kb_vp, sum(case when vp.loaivienphiid<>0 and hsba.tuoi>=60 then 1 else 0 end) as kb_60, sum(case when vp.loaivienphiid<>0 and hsba.tuoi<15 then 1 else 0 end) as kb_15, sum(case when vp.loaivienphiid<>0 and hsba.tuoi<5 then 1 else 0 end) as kb_5, sum(case when vp.loaivienphiid<>0 and hsba.hc_tinhcode<>'" + _ngoaitinhId + "' then 1 else 0 end) as kb_ngtinh, sum(case when vp.loaivienphiid=0 then 1 else 0 end) as nt_tong, sum(case when vp.loaivienphiid=0 and hsba.gioitinhcode='02' then 1 else 0 end) as nt_nu, sum(case when vp.loaivienphiid=0 and vp.doituongbenhnhanid=1 then 1 else 0 end) as nt_bhyt, sum(case when vp.loaivienphiid=0 and vp.doituongbenhnhanid<>1 then 1 else 0 end) as nt_vp, sum(case when vp.loaivienphiid=0 and hsba.tuoi>=60 then 1 else 0 end) as nt_60, sum(case when vp.loaivienphiid=0 and hsba.tuoi<15 then 1 else 0 end) as nt_15, sum(case when vp.loaivienphiid=0 and hsba.tuoi<5 then 1 else 0 end) as nt_5, sum(case when vp.loaivienphiid=0 and hsba.hc_tinhcode<>'" + _ngoaitinhId + "' then 1 else 0 end) as nt_ngtinh FROM (select loaivienphiid,doituongbenhnhanid,hosobenhanid, to_char(vienphidate,'MM') as thang from vienphi where vienphidate between '" + _datetungay + "' and '" + _datedenngay + "') vp inner join (select hosobenhanid,hc_tinhcode,gioitinhcode,(cast(to_char(now(),'yyyy') as numeric)-cast(to_char(birthday,'yyyy') as numeric)) as tuoi from hosobenhan where hosobenhandate between '" + _datetungay + "' and '" + _datedenngay + "') hsba on hsba.hosobenhanid=vp.hosobenhanid inner join (select hosobenhanid from medicalrecord where hinhthucvaovienid=0 and medicalrecordstatus<>0 and thoigianvaovien between '" + _datetungay + "' and '" + _datedenngay + "') mrd on mrd.hosobenhanid=vp.hosobenhanid GROUP BY vp.thang;";

                DataTable dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    List<ClassCommon.TinhHinhKCB_THDTO> lstDataBaoCao = new List<ClassCommon.TinhHinhKCB_THDTO>();
                    for (int i = 0; i < 16; i++)
                    {
                        ClassCommon.TinhHinhKCB_THDTO _item = new ClassCommon.TinhHinhKCB_THDTO();
                        lstDataBaoCao.Add(_item);
                    }
                    lstDataBaoCao[0].noidung = "Tổng số BN khám bệnh";
                    lstDataBaoCao[1].noidung = "Trong đó: - Nữ";
                    lstDataBaoCao[2].noidung = " - Số BN BHYT";
                    lstDataBaoCao[3].noidung = " - Số BN viện phí";
                    lstDataBaoCao[4].noidung = " - BN khám >= 60 tuổi";
                    lstDataBaoCao[5].noidung = " - BN khám < 15 tuổi";
                    lstDataBaoCao[6].noidung = " - BN khám < 5 tuổi";
                    lstDataBaoCao[7].noidung = " - BN ngoại tỉnh";
                    lstDataBaoCao[8].noidung = "Tổng số BN nội trú";
                    lstDataBaoCao[9].noidung = "Trong đó: - Nữ";
                    lstDataBaoCao[10].noidung = " - BN có BHYT";
                    lstDataBaoCao[11].noidung = " - BN viện phí";
                    lstDataBaoCao[12].noidung = " - BN >= 60 tuổi";
                    lstDataBaoCao[13].noidung = " - BN < 15 tuổi";
                    lstDataBaoCao[14].noidung = " - BN < 5 tuổi";
                    lstDataBaoCao[15].noidung = " - BN ngoại tỉnh";

                    List<ClassCommon.TinhHinhKCBDTO> lstData = Utilities.Util_DataTable.DataTableToList<ClassCommon.TinhHinhKCBDTO>(dataBaoCao);
                    foreach (var item_thang in lstData)
                    {
                        if (item_thang.thang == "01")
                        {
                            lstDataBaoCao[0].thang_1 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_1 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_1 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_1 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_1 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_1 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_1 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_1 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_1 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_1 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_1 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_1 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_1 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_1 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_1 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_1 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "02")
                        {
                            lstDataBaoCao[0].thang_2 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_2 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_2 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_2 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_2 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_2 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_2 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_2 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_2 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_2 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_2 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_2 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_2 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_2 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_2 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_2 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "03")
                        {
                            lstDataBaoCao[0].thang_3 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_3 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_3 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_3 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_3 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_3 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_3 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_3 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_3 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_3 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_3 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_3 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_3 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_3 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_3 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_3 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "04")
                        {
                            lstDataBaoCao[0].thang_4 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_4 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_4 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_4 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_4 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_4 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_4 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_4 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_4 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_4 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_4 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_4 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_4 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_4 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_4 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_4 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "05")
                        {
                            lstDataBaoCao[0].thang_5 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_5 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_5 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_5 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_5 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_5 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_5 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_5 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_5 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_5 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_5 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_5 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_5 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_5 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_5 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_5 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "06")
                        {
                            lstDataBaoCao[0].thang_6 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_6 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_6 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_6 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_6 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_6 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_6 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_6 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_6 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_6 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_6 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_6 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_6 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_6 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_6 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_6 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "07")
                        {
                            lstDataBaoCao[0].thang_7 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_7 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_7 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_7 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_7 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_7 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_7 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_7 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_7 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_7 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_7 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_7 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_7 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_7 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_7 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_7 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "08")
                        {
                            lstDataBaoCao[0].thang_8 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_8 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_8 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_8 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_8 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_8 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_8 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_8 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_8 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_8 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_8 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_8 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_8 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_8 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_8 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_8 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "09")
                        {
                            lstDataBaoCao[0].thang_9 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_9 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_9 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_9 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_9 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_9 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_9 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_9 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_9 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_9 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_9 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_9 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_9 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_9 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_9 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_9 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "10")
                        {
                            lstDataBaoCao[0].thang_10 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_10 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_10 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_10 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_10 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_10 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_10 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_10 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_10 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_10 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_10 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_10 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_10 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_10 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_10 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_10 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "11")
                        {
                            lstDataBaoCao[0].thang_11 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_11 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_11 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_11 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_11 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_11 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_11 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_11 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_11 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_11 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_11 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_11 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_11 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_11 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_11 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_11 = item_thang.nt_ngtinh;
                        }
                        else if (item_thang.thang == "12")
                        {
                            lstDataBaoCao[0].thang_12 = item_thang.kb_tong;
                            lstDataBaoCao[1].thang_12 = item_thang.kb_nu;
                            lstDataBaoCao[2].thang_12 = item_thang.kb_bhyt;
                            lstDataBaoCao[3].thang_12 = item_thang.kb_vp;
                            lstDataBaoCao[4].thang_12 = item_thang.kb_60;
                            lstDataBaoCao[5].thang_12 = item_thang.kb_15;
                            lstDataBaoCao[6].thang_12 = item_thang.kb_5;
                            lstDataBaoCao[7].thang_12 = item_thang.kb_ngtinh;
                            lstDataBaoCao[8].thang_12 = item_thang.nt_tong;
                            lstDataBaoCao[9].thang_12 = item_thang.nt_nu;
                            lstDataBaoCao[10].thang_12 = item_thang.nt_bhyt;
                            lstDataBaoCao[11].thang_12 = item_thang.nt_vp;
                            lstDataBaoCao[12].thang_12 = item_thang.nt_60;
                            lstDataBaoCao[13].thang_12 = item_thang.nt_15;
                            lstDataBaoCao[14].thang_12 = item_thang.nt_5;
                            lstDataBaoCao[15].thang_12 = item_thang.nt_ngtinh;
                        }
                    }
                    gridControlDataBaoCao.DataSource = lstDataBaoCao;

                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
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

        #region Export and Print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_38_TinhHinhKhamChuaBenh_TongHop.xlsx";
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_38_TinhHinhKhamChuaBenh_TongHop.xlsx";
                DataTable dataExportFilter = Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, dataExportFilter);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();

        }
        #endregion

        #region Event Change
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
        #endregion

    }
}
