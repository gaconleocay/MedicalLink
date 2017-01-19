using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang
{
    public partial class ucSuaDanhMucDichVu : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        public ucSuaDanhMucDichVu()
        {
            InitializeComponent();
        }

        #region Radio Changed
        private void radioKhamBenh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioKhamBenh.Checked)
                {
                    radioXetNghiem.Checked = false;
                    radioCDHA.Checked = false;
                    radioChuyenKhoa.Checked = false;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioXetNghiem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXetNghiem.Checked)
                {
                    radioKhamBenh.Checked = false;
                    radioCDHA.Checked = false;
                    radioChuyenKhoa.Checked = false;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioCDHA_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioCDHA.Checked)
                {
                    radioXetNghiem.Checked = false;
                    radioKhamBenh.Checked = false;
                    radioChuyenKhoa.Checked = false;
                    btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void radioChuyenKhoa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioChuyenKhoa.Checked)
                {
                    radioXetNghiem.Checked = false;
                    radioKhamBenh.Checked = false;
                    radioCDHA.Checked = false;
                    btnTimKiem_Click(null,null);
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
            try
            {
                string servicegrouptype = "";
                if (radioKhamBenh.Checked)
                {
                    servicegrouptype = "1";
                }
                if (radioXetNghiem.Checked)
                {
                    servicegrouptype = "2";
                }
                if (radioCDHA.Checked)
                {
                    servicegrouptype = "3";
                }
                if (radioChuyenKhoa.Checked)
                {
                    servicegrouptype = "4";
                }

                string sqlLayDanhMuc = "SELECT serf.servicepricerefid, serf.servicepricegroupcode, serf.servicepricetype, serf.servicegrouptype, serf.servicepricecode, serf.bhyt_groupcode, serf.report_groupcode, serf.report_tkcode, serf.servicepricenamenhandan,  serf.servicepricenamebhyt, serf.servicepriceunit, serf.servicepricefee, serf.servicepricefeenhandan, serf.servicepricefeebhyt,  serf.servicepricefeenuocngoai, serf.listdepartmentphongthuchien, serf.servicepricefee_old_date, serf.servicepricefee_old, serf.servicepricefeenhandan_old, serf.servicepricefeebhyt_old, serf.servicepricefeenuocngoai_old, serf.pttt_hangid,  serf.khongchuyendoituonghaophi, serf.cdha_soluongthuoc, serf.cdha_soluongvattu, serf.tylelaichidinh, serf.tylelaithuchien, serf.luonchuyendoituonghaophi,  serf.tinhtoanlaigiadvktc, serf.servicepricecodeuser,  serf.servicepricebhytdinhmuc, serf.listdepartmentphongthuchienkhamgoi,  serf.ck_groupcode, serf.servicepricecode_ng, serf.pttt_dinhmucvtth, serf.pttt_dinhmucthuoc, serf.servicepricefee_old_type, serf.servicepricesttuser, serf.pttt_loaiid FROM servicepriceref serf WHERE serf.servicegrouptype = " + servicegrouptype + " and serf.isremove is null or serf.isremove=0 and serf.servicelock=0 ORDER BY serf.servicepricegroupcode, serf.servicepricename;";
                DataView dv_DanhMucDichVu = new DataView(condb.getDataTable(sqlLayDanhMuc));
                if (dv_DanhMucDichVu.Count > 0)
                {
                    List<ClassCommon.classDanhMucDichVu> lstDanhMucDichVu = new List<ClassCommon.classDanhMucDichVu>();
                    for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                    {
                        ClassCommon.classDanhMucDichVu dmDichVu = new ClassCommon.classDanhMucDichVu();
                        dmDichVu.servicepricerefid = Convert.ToInt64(dv_DanhMucDichVu[i]["servicepricerefid"].ToString());
                        dmDichVu.servicepricegroupcode = dv_DanhMucDichVu[i]["servicepricegroupcode"].ToString();
                        dmDichVu.servicepricetype = Convert.ToInt64(dv_DanhMucDichVu[i]["servicepricetype"].ToString());
                        dmDichVu.servicegrouptype = Convert.ToInt64(dv_DanhMucDichVu[i]["servicegrouptype"].ToString());
                        dmDichVu.servicepricecode = dv_DanhMucDichVu[i]["servicepricecode"].ToString();
                        dmDichVu.bhyt_groupcode = dv_DanhMucDichVu[i]["bhyt_groupcode"].ToString();
                        dmDichVu.report_groupcode = dv_DanhMucDichVu[i]["report_groupcode"].ToString();
                        dmDichVu.report_tkcode = dv_DanhMucDichVu[i]["report_tkcode"].ToString();
                        dmDichVu.servicepricenamenhandan = dv_DanhMucDichVu[i]["servicepricenamenhandan"].ToString();
                        dmDichVu.servicepricenamebhyt = dv_DanhMucDichVu[i]["servicepricenamebhyt"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucDichVu[i]["servicepriceunit"].ToString();
                        dmDichVu.servicepricefee = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee"].ToString());
                        dmDichVu.servicepricefeenhandan = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan"].ToString());
                        dmDichVu.servicepricefeebhyt = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt"].ToString());
                        dmDichVu.servicepricefeenuocngoai = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString());
                        dmDichVu.servicepricefee_old_date = Convert.ToDateTime(dv_DanhMucDichVu[i]["servicepricefee_old_date"]);
                        dmDichVu.servicepricefee_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee_old"].ToString());
                        dmDichVu.servicepricefeenhandan_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan_old"].ToString());
                        dmDichVu.servicepricefeebhyt_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt_old"].ToString());
                        dmDichVu.servicepricefeenuocngoai_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai_old"].ToString());
                        dmDichVu.pttt_hangid = Convert.ToInt16(dv_DanhMucDichVu[i]["pttt_hangid"].ToString());
                        dmDichVu.khongchuyendoituonghaophi = Convert.ToInt16(dv_DanhMucDichVu[i]["khongchuyendoituonghaophi"].ToString());
                        dmDichVu.tinhtoanlaigiadvktc = Convert.ToInt16(dv_DanhMucDichVu[i]["tinhtoanlaigiadvktc"].ToString());
                        dmDichVu.servicepricecodeuser = dv_DanhMucDichVu[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricebhytdinhmuc = dv_DanhMucDichVu[i]["servicepricebhytdinhmuc"].ToString();
                        dmDichVu.ck_groupcode = dv_DanhMucDichVu[i]["ck_groupcode"].ToString();
                        dmDichVu.pttt_dinhmucvtth = Convert.ToDecimal(dv_DanhMucDichVu[i]["pttt_dinhmucvtth"].ToString());
                        dmDichVu.pttt_dinhmucthuoc = Convert.ToDecimal(dv_DanhMucDichVu[i]["pttt_dinhmucthuoc"].ToString());
                        dmDichVu.pttt_loaiid = Convert.ToInt16(dv_DanhMucDichVu[i]["pttt_loaiid"].ToString());
                        lstDanhMucDichVu.Add(dmDichVu);
                    }
                    gridControlDMDichVu.DataSource = lstDanhMucDichVu;
                }
                else
                {
                    gridControlDMDichVu.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }





    }
}
