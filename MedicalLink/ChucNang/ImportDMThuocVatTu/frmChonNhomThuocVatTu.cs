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

namespace MedicalLink.ChucNang.ImportDMThuocVatTu
{
    public partial class frmChonNhomThuocVatTu : DevExpress.XtraEditors.XtraForm
    {
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        public frmChonNhomThuocVatTu()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string datatype_thuoc = "";
                string datatype_vattu = "";
                if (chkThuoc.Checked)
                {
                    datatype_thuoc = "0,";
                }
                if (chkVatTu.Checked)
                {
                    datatype_vattu = "1,";
                }
                string tmp = datatype_thuoc + datatype_vattu;
                string datatype = tmp.Substring(0, tmp.Length - 1);//xoa ky tu cuoi cung

                //
                string sql_laydanhsach = @"select row_number () over (order by medicinename) as stt,
	(case datatype when 0 then 'Thuốc' when 1 then 'Vật tư' end) as datatype_name,
	medicinecode,
	medicinename,
	medicinecodeuser,
	stt_dauthau,
	namcungung,
	danhsttdungthuoc,
	dangdung,
	donggoi,
	sodangky,
	solo,
	nongdo
from medicine_ref
where isremove=0
	and datatype in (" + datatype + ");";
                DataTable dv_datamemdicineref = condb.GetDataTable_HIS(sql_laydanhsach);
                if (dv_datamemdicineref.Rows.Count > 0)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "0_ToolsDMThuoc_Exportv2.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dv_datamemdicineref);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}