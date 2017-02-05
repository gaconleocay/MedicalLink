using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.TimerService
{
    public class TimerServiceProcess
    {
        //private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public static void SQLKiemTraVaUpdateTableTmp()
        {
            try
            {
                MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

                string sqlkiemtraDL = "SELECT bndangdt_date FROM tools_bndangdt_tmp ORDER BY bndangdt_date DESC LIMIT 1";
                DataView dataKiemTra = new DataView(condb.getDataTable(sqlkiemtraDL));
                if (dataKiemTra != null && dataKiemTra.Count > 0)
                {
                    long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                    //DateTime result = Convert.ToDateTime(dataKiemTra[0]["bndangdt_date"].ToString());
                    long thoigianUpdataTabl = Utilities.Util_TypeConvertParse.ToInt64(Convert.ToDateTime(dataKiemTra[0]["bndangdt_date"].ToString()).ToString("yyyyMMddHHmm"));
                    if ((datetime - thoigianUpdataTabl) > GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp)
                    {
                        //Chay cap nhat du lieu
                        String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string updateTmp = "INSERT INTO tools_bndangdt_tmp(departmentgroupid, vienphiid, doituongbenhnhanid, serviceprice_dichvu, serviceprice_thuoc, tam_ung, bndangdt_date) SELECT vp.departmentgroupid, vp.vienphiid, vp.doituongbenhnhanid, (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) as serviceprice_dichvu, (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) as serviceprice_thuoc, (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung, '" + datetimeNow + "' as bndangdt_date FROM vienphi vp WHERE vp.vienphistatus=0 and vp.loaivienphiid=0 ORDER BY vp.departmentgroupid;";
                        string sqlxoadulieuTmp = "DELETE FROM tools_bndangdt_tmp WHERE bndangdt_date < '" + datetimeNow + "';";

                        condb.ExecuteNonQuery(updateTmp);
                        condb.ExecuteNonQuery(sqlxoadulieuTmp);
                    }
                }
                else
                {
                    //Chay cap nhat du lieu
                    String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string updateTmp = "INSERT INTO tools_bndangdt_tmp(departmentgroupid, vienphiid, doituongbenhnhanid, serviceprice_dichvu, serviceprice_thuoc, tam_ung, bndangdt_date) SELECT vp.departmentgroupid, vp.vienphiid, vp.doituongbenhnhanid, (select sum(serdv.sotien_tong) from serviceprice_dichvu serdv where serdv.vienphiid=vp.vienphiid) as serviceprice_dichvu, (select sum(serthuoc.sotien_tong) from serviceprice_thuoc serthuoc where serthuoc.vienphiid=vp.vienphiid) as serviceprice_thuoc, (select sum(bill.datra) from bill where bill.vienphiid=vp.vienphiid and bill.loaiphieuthuid=2 and bill.dahuyphieu=0) as tam_ung, '" + datetimeNow + "' as bndangdt_date FROM vienphi vp WHERE vp.vienphistatus=0 and vp.loaivienphiid=0 ORDER BY vp.departmentgroupid;";
                    string sqlxoadulieuTmp = "DELETE FROM tools_bndangdt_tmp WHERE bndangdt_date < '" + datetimeNow + "';";

                    condb.ExecuteNonQuery(updateTmp);
                    condb.ExecuteNonQuery(sqlxoadulieuTmp);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

    }
}
