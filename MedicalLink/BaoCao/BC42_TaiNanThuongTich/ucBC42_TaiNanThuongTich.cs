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
using MedicalLink.ClassCommon;

namespace MedicalLink.BaoCao
{
    public partial class ucBC42_TaiNanThuongTich : UserControl
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private List<BC42_TaiNanThuongTichDTO> lstBaoCao { get; set; }
        private List<BC42_TaiNanThuongTich_LableDTO> lstTNTT_Lable { get; set; }
        #endregion
        public ucBC42_TaiNanThuongTich()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC42_TaiNanThuongTich_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            LoadTemplate_LableBaoCao();
        }
        private void LoadTemplate_LableBaoCao()
        {
            try
            {
                string _sqldata = "SELECT * FROM tools_bc_tntt";
                System.Data.DataTable _dataTmp = condb.GetDataTable_MeL(_sqldata);
                lstTNTT_Lable = Utilities.DataTables.DataTableToList<BC42_TaiNanThuongTich_LableDTO>(_dataTmp);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tieuchi = "";
                string _datetungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _datedenngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                _tieuchi = " and tainanthuongtichdate between '"+ _datetungay + "' and '"+ _datedenngay + "' ";
                string _sql_timkiem = " SELECT '1' as stt, '1' as nhom, '1' as noi_dung_code, '' as noi_dung_name, count(*) as tong_m, sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c, sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m, sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c, sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m, sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c, sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m, sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c, '1' as isgroup FROM (select * from tainanthuongtich where 1=1 " + _tieuchi + " ) tntt inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid UNION ALL SELECT '2' as stt, '2' as nhom, (case when hsba.nghenghiepcode in ('07','08','09','15','19') then '3' when hsba.nghenghiepcode in ('05') then '4' when hsba.nghenghiepcode in ('06','18') then '5' when hsba.nghenghiepcode in ('02') then '6' when hsba.nghenghiepcode in ('04','13') then '7' when hsba.nghenghiepcode in ('10') then '8' when hsba.nghenghiepcode in ('99','01','03','12') then '9' end) as noi_dung_code, '' as noi_dung_name, count(*) as tong_m, sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c, sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m, sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c, sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m, sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c, sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m, sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c, '1' as isgroup FROM (select * from tainanthuongtich where 1=1 " + _tieuchi + " ) tntt inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid GROUP BY (case when hsba.nghenghiepcode in ('07','08','09','15','19') then '3' when hsba.nghenghiepcode in ('05') then '4' when hsba.nghenghiepcode in ('06','18') then '5' when hsba.nghenghiepcode in ('02') then '6' when hsba.nghenghiepcode in ('04','13') then '7' when hsba.nghenghiepcode in ('10') then '8' when hsba.nghenghiepcode in ('99','01','03','12') then '9' end) UNION ALL SELECT '3' as stt, '3' as nhom, (case when tntt.tainan_diadiemid=1 then '11' when tntt.tainan_diadiemid=2 then '12' when tntt.tainan_diadiemid=3 then '13' when tntt.tainan_diadiemid=4 then '14' when tntt.tainan_diadiemid=5 then '15' when tntt.tainan_diadiemid=6 then '16' when tntt.tainan_diadiemid in (0,99) then '161' end) as noi_dung_code, '' as noi_dung_name, count(*) as tong_m, sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c, sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m, sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c, sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m, sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c, sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m, sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c, '1' as isgroup FROM (select * from tainanthuongtich where 1=1 " + _tieuchi + " ) tntt inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid GROUP BY (case when tntt.tainan_diadiemid=1 then '11' when tntt.tainan_diadiemid=2 then '12' when tntt.tainan_diadiemid=3 then '13' when tntt.tainan_diadiemid=4 then '14' when tntt.tainan_diadiemid=5 then '15' when tntt.tainan_diadiemid=6 then '16' when tntt.tainan_diadiemid in (0,99) then '161' end) UNION ALL SELECT '4' as stt, '4' as nhom, (case when tntt.tainan_bophanbithuongid=1 then '18' when tntt.tainan_bophanbithuongid=2 then '19' when tntt.tainan_bophanbithuongid=3 then '20' when tntt.tainan_bophanbithuongid=4 then '21' when tntt.tainan_bophanbithuongid in (0,5,99) then '22' end) as noi_dung_code, '' as noi_dung_name, count(*) as tong_m, sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c, sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m, sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c, sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m, sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c, sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m, sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c, '1' as isgroup FROM (select * from tainanthuongtich where 1=1 " + _tieuchi + " ) tntt inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid GROUP BY (case when tntt.tainan_bophanbithuongid=1 then '18' when tntt.tainan_bophanbithuongid=2 then '19' when tntt.tainan_bophanbithuongid=3 then '20' when tntt.tainan_bophanbithuongid=4 then '21' when tntt.tainan_bophanbithuongid in (0,5,99) then '22' end) UNION ALL SELECT '5' as stt, '5' as nhom, (case when tntt.tainan_nguyennhanid=1 then '24' when tntt.tainan_nguyennhanid=3 then '25' when tntt.tainan_nguyennhanid=4 then '26' when tntt.tainan_nguyennhanid=2 then '27' when tntt.tainan_nguyennhanid=5 then '28' when tntt.tainan_nguyennhanid=6 then '29' when tntt.tainan_nguyennhanid=7 then '30' when tntt.tainan_nguyennhanid=8 then '31' when tntt.tainan_nguyennhanid=9 then '32' when tntt.tainan_nguyennhanid in (10,11,99) then '33' end) as noi_dung_code, '' as noi_dung_name, count(*) as tong_m, sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c, sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m, sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c, sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m, sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c, sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m, sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c, '1' as isgroup FROM (select * from tainanthuongtich where 1=1 " + _tieuchi + " ) tntt inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid GROUP BY (case when tntt.tainan_nguyennhanid=1 then '24' when tntt.tainan_nguyennhanid=3 then '25' when tntt.tainan_nguyennhanid=4 then '26' when tntt.tainan_nguyennhanid=2 then '27' when tntt.tainan_nguyennhanid=5 then '28' when tntt.tainan_nguyennhanid=6 then '29' when tntt.tainan_nguyennhanid=7 then '30' when tntt.tainan_nguyennhanid=8 then '31' when tntt.tainan_nguyennhanid=9 then '32' when tntt.tainan_nguyennhanid in (10,11,99) then '33' end) UNION ALL SELECT '6' as stt, '6' as nhom, (case when tntt.tainan_xutriid=1 then '35' when tntt.tainan_xutriid=2 then '36' when tntt.tainan_xutriid=3 then '37' when tntt.tainan_xutriid=4 then '38' when tntt.tainan_xutriid=5 then '39' when tntt.tainan_xutriid=6 then '40' when tntt.tainan_xutriid=7 then '41' when tntt.tainan_xutriid=99 then '42' end) as noi_dung_code, '' as noi_dung_name, count(*) as tong_m, sum(case when tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_c, sum(case when hsba.gioitinhcode='02' then 1 else 0 end) as tong_nu_m, sum(case when hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as tong_nu_c, sum(case when hsba.tuoi<=4 then 1 else 0 end) as t04_m, sum(case when hsba.tuoi<=4 and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_c, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' then 1 else 0 end) as t04_nu_m, sum(case when hsba.tuoi<=4 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t04_nu_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) then 1 else 0 end) as t0514_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_c, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' then 1 else 0 end) as t0514_nu_m, sum(case when (hsba.tuoi>=5 and hsba.tuoi<=14) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t0514_nu_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) then 1 else 0 end) as t1519_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_c, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' then 1 else 0 end) as t1519_nu_m, sum(case when (hsba.tuoi>=15 and hsba.tuoi<=19) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t1519_nu_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) then 1 else 0 end) as t2060_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_c, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' then 1 else 0 end) as t2060_nu_m, sum(case when (hsba.tuoi>=20 and hsba.tuoi<=60) and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as t2060_nu_c, sum(case when hsba.tuoi>60 then 1 else 0 end) as ttren60_m, sum(case when hsba.tuoi>60 and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_c, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' then 1 else 0 end) as ttren60_nu_m, sum(case when hsba.tuoi>60 and hsba.gioitinhcode='02' and tntt.tainan_dienbienid=1 then 1 else 0 end) as ttren60_nu_c, '1' as isgroup FROM (select * from tainanthuongtich where 1=1 " + _tieuchi + ") tntt inner join (select (cast(to_char(hosobenhandate,'yyyy') as integer) - cast(to_char(birthday,'yyyy') as integer)) as tuoi,* from hosobenhan) hsba on hsba.hosobenhanid=tntt.hosobenhanid GROUP BY (case when tntt.tainan_xutriid=1 then '35' when tntt.tainan_xutriid=2 then '36' when tntt.tainan_xutriid=3 then '37' when tntt.tainan_xutriid=4 then '38' when tntt.tainan_xutriid=5 then '39' when tntt.tainan_xutriid=6 then '40' when tntt.tainan_xutriid=7 then '41' when tntt.tainan_xutriid=99 then '42' end); ";

                System.Data.DataTable _dataBaoCao = condb.GetDataTable_HIS(_sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    List<BC42_TaiNanThuongTichDTO> _lstTNTT = DataTables.DataTableToList<BC42_TaiNanThuongTichDTO>(_dataBaoCao);

                    this.lstBaoCao = XuLyKetQuaTimKiem(_lstTNTT);

                    gridControlDataBaoCao.DataSource = this.lstBaoCao;
                }
                else
                {
                    gridControlDataBaoCao.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private List<BC42_TaiNanThuongTichDTO> XuLyKetQuaTimKiem(List<BC42_TaiNanThuongTichDTO> _lstbaocao)
        {
            List<BC42_TaiNanThuongTichDTO> result = new List<BC42_TaiNanThuongTichDTO>();
            try
            {
                result = (from p in this.lstTNTT_Lable
                          join bc in _lstbaocao on p.noi_dung_code equals bc.noi_dung_code into leftjoin
                          from b in leftjoin.DefaultIfEmpty( new BC42_TaiNanThuongTichDTO())
                          select new BC42_TaiNanThuongTichDTO
                          {
                              stt = p.stt,
                              nhom = b.nhom,
                              noi_dung_code = p.noi_dung_code,
                              noi_dung_name = p.noi_dung_name,
                              tong_m = b.tong_m,
                              tong_c = b.tong_c,
                              tong_nu_m = b.tong_nu_m,
                              tong_nu_c = b.tong_nu_c,
                              t04_m = b.t04_m,
                              t04_c = b.t04_c,
                              t04_nu_m = b.t04_nu_m,
                              t04_nu_c = b.t04_nu_c,
                              t0514_m = b.t0514_m,
                              t0514_c = b.t0514_c,
                              t0514_nu_m = b.t0514_nu_m,
                              t0514_nu_c = b.t0514_nu_c,
                              t1519_m = b.t1519_m,
                              t1519_c = b.t1519_c,
                              t1519_nu_m = b.t1519_nu_m,
                              t1519_nu_c = b.t1519_nu_c,
                              t2060_m = b.t2060_m,
                              t2060_c = b.t2060_c,
                              t2060_nu_m = b.t2060_nu_m,
                              t2060_nu_c = b.t2060_nu_c,
                              ttren60_m = b.ttren60_m,
                              ttren60_c = b.ttren60_c,
                              ttren60_nu_m = b.ttren60_nu_m,
                              ttren60_nu_c = b.ttren60_nu_c,
                              isgroup = p.isgroup,
                          }).OrderBy(or=>or.noi_dung_code).ToList();

                //BC42_TaiNanThuongTichDTO _item_group2 = result[1];
                List<BC42_TaiNanThuongTichDTO> _lstgroup2 = result.Where(o => o.nhom == "2").ToList();
                foreach (var item_gr in _lstgroup2)
                {
                    result[1].tong_m += item_gr.tong_m;
                    result[1].tong_c += item_gr.tong_c;
                    result[1].tong_nu_m += item_gr.tong_nu_m;
                    result[1].tong_nu_c += item_gr.tong_nu_c;
                    result[1].t04_m += item_gr.t04_m;
                    result[1].t04_c += item_gr.t04_c;
                    result[1].t04_nu_m += item_gr.t04_nu_m;
                    result[1].t04_nu_c += item_gr.t04_nu_c;
                    result[1].t0514_m += item_gr.t0514_m;
                    result[1].t0514_c += item_gr.t0514_c;
                    result[1].t0514_nu_m += item_gr.t0514_nu_m;
                    result[1].t0514_nu_c += item_gr.t0514_nu_c;
                    result[1].t1519_m += item_gr.t1519_m;
                    result[1].t1519_c += item_gr.t1519_c;
                    result[1].t1519_nu_m += item_gr.t1519_nu_m;
                    result[1].t1519_nu_c += item_gr.t1519_nu_c;
                    result[1].t2060_m += item_gr.t2060_m;
                    result[1].t2060_c += item_gr.t2060_c;
                    result[1].t2060_nu_m += item_gr.t2060_nu_m;
                    result[1].t2060_nu_c += item_gr.t2060_nu_c;
                    result[1].ttren60_m += item_gr.ttren60_m;
                    result[1].ttren60_c += item_gr.ttren60_c;
                    result[1].ttren60_nu_m += item_gr.ttren60_nu_m;
                    result[1].ttren60_nu_c += item_gr.ttren60_nu_c;
                    result[1].isgroup += item_gr.isgroup;
                }
                //group 3
                List<BC42_TaiNanThuongTichDTO> _lstgroup3 = result.Where(o => o.nhom == "3").ToList();
                foreach (var item_gr in _lstgroup3)
                {
                    result[9].tong_m += item_gr.tong_m;
                    result[9].tong_c += item_gr.tong_c;
                    result[9].tong_nu_m += item_gr.tong_nu_m;
                    result[9].tong_nu_c += item_gr.tong_nu_c;
                    result[9].t04_m += item_gr.t04_m;
                    result[9].t04_c += item_gr.t04_c;
                    result[9].t04_nu_m += item_gr.t04_nu_m;
                    result[9].t04_nu_c += item_gr.t04_nu_c;
                    result[9].t0514_m += item_gr.t0514_m;
                    result[9].t0514_c += item_gr.t0514_c;
                    result[9].t0514_nu_m += item_gr.t0514_nu_m;
                    result[9].t0514_nu_c += item_gr.t0514_nu_c;
                    result[9].t1519_m += item_gr.t1519_m;
                    result[9].t1519_c += item_gr.t1519_c;
                    result[9].t1519_nu_m += item_gr.t1519_nu_m;
                    result[9].t1519_nu_c += item_gr.t1519_nu_c;
                    result[9].t2060_m += item_gr.t2060_m;
                    result[9].t2060_c += item_gr.t2060_c;
                    result[9].t2060_nu_m += item_gr.t2060_nu_m;
                    result[9].t2060_nu_c += item_gr.t2060_nu_c;
                    result[9].ttren60_m += item_gr.ttren60_m;
                    result[9].ttren60_c += item_gr.ttren60_c;
                    result[9].ttren60_nu_m += item_gr.ttren60_nu_m;
                    result[9].ttren60_nu_c += item_gr.ttren60_nu_c;
                    result[9].isgroup += item_gr.isgroup;
                }
                //group 4
                List<BC42_TaiNanThuongTichDTO> _lstgroup4 = result.Where(o => o.nhom == "4").ToList();
                foreach (var item_gr in _lstgroup4)
                {
                    result[16].tong_m += item_gr.tong_m;
                    result[16].tong_c += item_gr.tong_c;
                    result[16].tong_nu_m += item_gr.tong_nu_m;
                    result[16].tong_nu_c += item_gr.tong_nu_c;
                    result[16].t04_m += item_gr.t04_m;
                    result[16].t04_c += item_gr.t04_c;
                    result[16].t04_nu_m += item_gr.t04_nu_m;
                    result[16].t04_nu_c += item_gr.t04_nu_c;
                    result[16].t0514_m += item_gr.t0514_m;
                    result[16].t0514_c += item_gr.t0514_c;
                    result[16].t0514_nu_m += item_gr.t0514_nu_m;
                    result[16].t0514_nu_c += item_gr.t0514_nu_c;
                    result[16].t1519_m += item_gr.t1519_m;
                    result[16].t1519_c += item_gr.t1519_c;
                    result[16].t1519_nu_m += item_gr.t1519_nu_m;
                    result[16].t1519_nu_c += item_gr.t1519_nu_c;
                    result[16].t2060_m += item_gr.t2060_m;
                    result[16].t2060_c += item_gr.t2060_c;
                    result[16].t2060_nu_m += item_gr.t2060_nu_m;
                    result[16].t2060_nu_c += item_gr.t2060_nu_c;
                    result[16].ttren60_m += item_gr.ttren60_m;
                    result[16].ttren60_c += item_gr.ttren60_c;
                    result[16].ttren60_nu_m += item_gr.ttren60_nu_m;
                    result[16].ttren60_nu_c += item_gr.ttren60_nu_c;
                    result[16].isgroup += item_gr.isgroup;
                }
                //group 5
                List<BC42_TaiNanThuongTichDTO> _lstgroup5 = result.Where(o => o.nhom == "5").ToList();
                foreach (var item_gr in _lstgroup5)
                {
                    result[22].tong_m += item_gr.tong_m;
                    result[22].tong_c += item_gr.tong_c;
                    result[22].tong_nu_m += item_gr.tong_nu_m;
                    result[22].tong_nu_c += item_gr.tong_nu_c;
                    result[22].t04_m += item_gr.t04_m;
                    result[22].t04_c += item_gr.t04_c;
                    result[22].t04_nu_m += item_gr.t04_nu_m;
                    result[22].t04_nu_c += item_gr.t04_nu_c;
                    result[22].t0514_m += item_gr.t0514_m;
                    result[22].t0514_c += item_gr.t0514_c;
                    result[22].t0514_nu_m += item_gr.t0514_nu_m;
                    result[22].t0514_nu_c += item_gr.t0514_nu_c;
                    result[22].t1519_m += item_gr.t1519_m;
                    result[22].t1519_c += item_gr.t1519_c;
                    result[22].t1519_nu_m += item_gr.t1519_nu_m;
                    result[22].t1519_nu_c += item_gr.t1519_nu_c;
                    result[22].t2060_m += item_gr.t2060_m;
                    result[22].t2060_c += item_gr.t2060_c;
                    result[22].t2060_nu_m += item_gr.t2060_nu_m;
                    result[22].t2060_nu_c += item_gr.t2060_nu_c;
                    result[22].ttren60_m += item_gr.ttren60_m;
                    result[22].ttren60_c += item_gr.ttren60_c;
                    result[22].ttren60_nu_m += item_gr.ttren60_nu_m;
                    result[22].ttren60_nu_c += item_gr.ttren60_nu_c;
                    result[22].isgroup += item_gr.isgroup;
                }
                //group 6
                List<BC42_TaiNanThuongTichDTO> _lstgroup6 = result.Where(o => o.nhom == "6").ToList();
                foreach (var item_gr in _lstgroup6)
                {
                    result[33].tong_m += item_gr.tong_m;
                    result[33].tong_c += item_gr.tong_c;
                    result[33].tong_nu_m += item_gr.tong_nu_m;
                    result[33].tong_nu_c += item_gr.tong_nu_c;
                    result[33].t04_m += item_gr.t04_m;
                    result[33].t04_c += item_gr.t04_c;
                    result[33].t04_nu_m += item_gr.t04_nu_m;
                    result[33].t04_nu_c += item_gr.t04_nu_c;
                    result[33].t0514_m += item_gr.t0514_m;
                    result[33].t0514_c += item_gr.t0514_c;
                    result[33].t0514_nu_m += item_gr.t0514_nu_m;
                    result[33].t0514_nu_c += item_gr.t0514_nu_c;
                    result[33].t1519_m += item_gr.t1519_m;
                    result[33].t1519_c += item_gr.t1519_c;
                    result[33].t1519_nu_m += item_gr.t1519_nu_m;
                    result[33].t1519_nu_c += item_gr.t1519_nu_c;
                    result[33].t2060_m += item_gr.t2060_m;
                    result[33].t2060_c += item_gr.t2060_c;
                    result[33].t2060_nu_m += item_gr.t2060_nu_m;
                    result[33].t2060_nu_c += item_gr.t2060_nu_c;
                    result[33].ttren60_m += item_gr.ttren60_m;
                    result[33].ttren60_c += item_gr.ttren60_c;
                    result[33].ttren60_nu_m += item_gr.ttren60_nu_m;
                    result[33].ttren60_nu_c += item_gr.ttren60_nu_c;
                    result[33].isgroup += item_gr.isgroup;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return result.OrderBy(o=>o.noi_dung_code).ToList();
        }

        #endregion

        #region Export and Print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_42_ThongKeTaiNanThuongTich.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, DataTables.ListToDataTable(this.lstBaoCao));
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));

                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);

                string fileTemplatePath = "BC_42_ThongKeTaiNanThuongTich.xlsx"; Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, DataTables.ListToDataTable(this.lstBaoCao));
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
