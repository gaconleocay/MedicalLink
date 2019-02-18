using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MedicalLink.Base;

namespace MedicalLink.ChucNang.ImportDMDichVu
{
    public partial class frmChonNhomDichVu : DevExpress.XtraEditors.XtraForm
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        public frmChonNhomDichVu()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string servicegrouptype_kb = "";
                string servicegrouptype_xn = "";
                string servicegrouptype_cdha = "";
                string servicegrouptype_ck = "";
                if (chkKhamBenh.Checked)
                {
                    servicegrouptype_kb = "1,";
                }
                if (chkXetNghiem.Checked)
                {
                    servicegrouptype_xn = "2,";
                }
                if (chkCDHA.Checked)
                {
                    servicegrouptype_cdha = "3,";
                }
                if (chkChuyenKhoa.Checked)
                {
                    servicegrouptype_ck = "4,";
                }
                string tmp = servicegrouptype_kb + servicegrouptype_xn + servicegrouptype_cdha + servicegrouptype_ck;
                string servicegrouptype = tmp.Substring(0, tmp.Length - 1);

                //
                string sql_laydanhsach = "SELECT CASE serf.servicegrouptype WHEN 1 THEN 'Khám bệnh' WHEN 2 THEN 'Xét nghiệm' WHEN 3 THEN 'Chẩn đoán hình ảnh' WHEN 4 THEN 'Chuyên khoa' ELSE '' END AS SERVICEGROUPTYPE_NAME, serf.servicepricecode AS SERVICEPRICECODE, serf.servicepricegroupcode AS SERVICEPRICEGROUPCODE, serf.servicepricecodeuser AS SERVICEPRICECODEUSER, serf.servicepricesttuser AS SERVICEPRICESTTUSER, serf.servicepricename AS SERVICEPRICENAME, serf.servicepricenamebhyt AS SERVICEPRICENAMEBHYT, serf.servicepriceunit AS SERVICEPRICEUNIT, serf.servicepricefeebhyt AS SERVICEPRICEFEEBHYT, serf.servicepricefeenhandan AS SERVICEPRICEFEENHANDAN, serf.servicepricefee AS SERVICEPRICEFEE, serf.servicepricefeenuocngoai AS SERVICEPRICEFEENUOCNGOAI, serf.servicepricefee_old_date AS SERVICEPRICEFEE_OLD_DATE, case serf.servicepricefee_old_type when 1 then 'Ngày chỉ định' else 'Ngày vào viện' end AS SERVICEPRICEFEE_OLD_TYPE_NAME, case serf.pttt_hangid when 1 then 'Đặc biệt' when 2 then 'Loại 1A' when 3 then 'Loại 1B' when 4 then 'Loại 1C' when 5 then 'Loại 2A' when 6 then 'Loại 2B' when 7 then 'Loại 2C' when 8 then 'Loại 3' when 9 then 'Loại 1' when 10 then 'Loại 2' end AS PTTT_HANGID_NAME, case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' end AS PTTT_LOAIID_NAME, serf.servicelock AS SERVICELOCK, case serf.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'Thăm dò CN' when '06PTTT' then 'PTTT' when '07KTC' then 'DV kỹ thuật cao' when '11VC' then 'Vận chuyển' when '12NG' then 'Ngày giường' when '999DVKHAC' then 'DV khác' when '1000PhuThu' then 'Phụ thu' end AS BHYT_GROUPCODE_NAME, serf.report_groupcode AS REPORT_GROUPCODE, serf.report_tkcode AS REPORT_TKCODE, serf.servicepricetype AS SERVICEPRICETYPE, case when serf.servicegrouptype in (1,3,4) then ser.servicecode end AS SERVICECODE FROM servicepriceref serf inner join serviceref4price ser on ser.servicepricecode=serf.servicepricecode WHERE isremove is null and serf.servicepricecode <>'' and serf.servicegrouptype in (" + servicegrouptype + ") ORDER BY serf.servicegrouptype, serf.servicepricegroupcode, serf.servicepricename; ";
                DataTable dv_dataserviceref = condb.GetDataTable_HIS(sql_laydanhsach);
                if (dv_dataserviceref.Rows.Count > 0)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "0_ToolsDMDV__Exportv2.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dv_dataserviceref);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}