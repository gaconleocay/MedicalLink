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
        List<ClassCommon.classDanhMucDichVu> lstDanhMucDichVu { get; set; }
        long servicepricerefidCurrent { get; set; }
        public ucSuaDanhMucDichVu()
        {
            InitializeComponent();
        }

        private void ucSuaDanhMucDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
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
                    GetDataDanhMucDichVu();
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
                    GetDataDanhMucDichVu();
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
                    GetDataDanhMucDichVu();
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
                    GetDataDanhMucDichVu();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion


        private void GetDataDanhMucDichVu()
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
                    lstDanhMucDichVu = new List<ClassCommon.classDanhMucDichVu>();
                    for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                    {
                        ClassCommon.classDanhMucDichVu dmDichVu = new ClassCommon.classDanhMucDichVu();
                        dmDichVu.servicepricerefid = Convert.ToInt64(dv_DanhMucDichVu[i]["servicepricerefid"].ToString());
                        dmDichVu.servicepricegroupcode = dv_DanhMucDichVu[i]["servicepricegroupcode"].ToString();
                        if (dv_DanhMucDichVu[i]["servicepricetype"].ToString() != "")
                        {
                            dmDichVu.servicepricetype = Convert.ToInt64(dv_DanhMucDichVu[i]["servicepricetype"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricetype = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicegrouptype"].ToString() != "")
                        {
                            dmDichVu.servicegrouptype = Convert.ToInt64(dv_DanhMucDichVu[i]["servicegrouptype"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicegrouptype = 0;
                        }
                        dmDichVu.servicepricecode = dv_DanhMucDichVu[i]["servicepricecode"].ToString();
                        dmDichVu.bhyt_groupcode = dv_DanhMucDichVu[i]["bhyt_groupcode"].ToString();
                        dmDichVu.report_groupcode = dv_DanhMucDichVu[i]["report_groupcode"].ToString();
                        dmDichVu.report_tkcode = dv_DanhMucDichVu[i]["report_tkcode"].ToString();
                        dmDichVu.servicepricenamenhandan = dv_DanhMucDichVu[i]["servicepricenamenhandan"].ToString();
                        dmDichVu.servicepricenamebhyt = dv_DanhMucDichVu[i]["servicepricenamebhyt"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucDichVu[i]["servicepriceunit"].ToString();
                        if (dv_DanhMucDichVu[i]["servicepricefee"].ToString() != "")
                        {
                            dmDichVu.servicepricefee = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefee = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefeenhandan"].ToString() != "")
                        {
                            dmDichVu.servicepricefeenhandan = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefeenhandan = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefeebhyt"].ToString() != "")
                        {
                            dmDichVu.servicepricefeebhyt = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefeebhyt = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString() != "" && dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString() != "NULL")
                        {
                            dmDichVu.servicepricefeenuocngoai = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefeenuocngoai = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefee_old_date"].ToString() != "")
                        {
                            dmDichVu.servicepricefee_old_date = Convert.ToDateTime(dv_DanhMucDichVu[i]["servicepricefee_old_date"]);
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefee_old"].ToString() != "")
                        {
                            dmDichVu.servicepricefee_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee_old"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefee_old = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefeenhandan_old"].ToString() != "")
                        {
                            dmDichVu.servicepricefeenhandan_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan_old"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefeenhandan_old = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefeebhyt_old"].ToString() != "")
                        {
                            dmDichVu.servicepricefeebhyt_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt_old"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefeebhyt_old = 0;
                        }
                        if (dv_DanhMucDichVu[i]["servicepricefeenuocngoai_old"].ToString() != "")
                        {
                            dmDichVu.servicepricefeenuocngoai_old = Convert.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai_old"].ToString());
                        }
                        else
                        {
                            dmDichVu.servicepricefeenuocngoai_old = 0;
                        }
                        if (dv_DanhMucDichVu[i]["pttt_hangid"].ToString() != "")
                        {
                            dmDichVu.pttt_hangid = Convert.ToInt16(dv_DanhMucDichVu[i]["pttt_hangid"].ToString());
                        }
                        else
                        {
                            dmDichVu.pttt_hangid = 0;
                        }
                        if (dv_DanhMucDichVu[i]["khongchuyendoituonghaophi"].ToString() != "")
                        {
                            dmDichVu.khongchuyendoituonghaophi = Convert.ToInt16(dv_DanhMucDichVu[i]["khongchuyendoituonghaophi"].ToString());
                        }
                        else
                        {
                            dmDichVu.khongchuyendoituonghaophi = 0;
                        }
                        if (dv_DanhMucDichVu[i]["tinhtoanlaigiadvktc"].ToString() != "")
                        {
                            dmDichVu.tinhtoanlaigiadvktc = Convert.ToInt16(dv_DanhMucDichVu[i]["tinhtoanlaigiadvktc"].ToString());
                        }
                        else
                        {
                            dmDichVu.tinhtoanlaigiadvktc = 0;
                        }
                        dmDichVu.servicepricecodeuser = dv_DanhMucDichVu[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricebhytdinhmuc = dv_DanhMucDichVu[i]["servicepricebhytdinhmuc"].ToString();
                        dmDichVu.ck_groupcode = dv_DanhMucDichVu[i]["ck_groupcode"].ToString();
                        if (dv_DanhMucDichVu[i]["pttt_dinhmucvtth"].ToString() != "")
                        {
                            dmDichVu.pttt_dinhmucvtth = Convert.ToDecimal(dv_DanhMucDichVu[i]["pttt_dinhmucvtth"].ToString());
                        }
                        else
                        {
                            dmDichVu.pttt_dinhmucvtth = 0;
                        }
                        if (dv_DanhMucDichVu[i]["pttt_dinhmucthuoc"].ToString() != "")
                        {
                            dmDichVu.pttt_dinhmucthuoc = Convert.ToDecimal(dv_DanhMucDichVu[i]["pttt_dinhmucthuoc"].ToString());
                        }
                        else
                        {
                            dmDichVu.pttt_dinhmucthuoc = 0;
                        }
                        if (dv_DanhMucDichVu[i]["pttt_loaiid"].ToString() != "")
                        {
                            dmDichVu.pttt_loaiid = Convert.ToInt16(dv_DanhMucDichVu[i]["pttt_loaiid"].ToString());
                        }
                        else
                        {
                            dmDichVu.pttt_loaiid = 0;
                        }
                        lstDanhMucDichVu.Add(dmDichVu);
                    }
                    gridControlDMDichVu.DataSource = lstDanhMucDichVu;
                    LayDanhMucNhomDichVu();
                }
                else
                {
                    gridControlDMDichVu.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                gridControlDMDichVu.DataSource = null;
            }
        }

        private void LayDanhMucNhomDichVu()
        {
            try
            {
                var lstNhomDanhMuc = lstDanhMucDichVu.Where(o => o.servicepricetype == 1).ToList();
                foreach (var item_dm in lstNhomDanhMuc)
                {
                    cbbservicepricegroupcode.Properties.Items.Add(item_dm.servicepricecode);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LayDanhMucNhomDichVuTuongUngVoiBHYT()
        {
            try
            {
                string servicegrouptype = "";
                if (cbbNhomLoaiDichVu.SelectedItem == "Khám bệnh")
                {
                    servicegrouptype = "1";
                }
                else if (cbbNhomLoaiDichVu.SelectedItem == "Xét nghiệm")
                {
                    servicegrouptype = "2";
                }
                else if (cbbNhomLoaiDichVu.SelectedItem == "Chẩn đoán hình ảnh")
                {
                    servicegrouptype = "3";
                }
                else if (cbbNhomLoaiDichVu.SelectedItem == "Chuyên khoa")
                {
                    servicegrouptype = "4";
                }

                string sqlLayDanhMuc = "SELECT serf.servicepricecode FROM servicepriceref serf WHERE serf.servicepricetype = 1 and serf.servicegrouptype=" + servicegrouptype + "; ";
                DataView dv_DanhMucNhom = new DataView(condb.getDataTable(sqlLayDanhMuc));
                if (dv_DanhMucNhom.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucNhom.Count; i++)
                    {
                        cbbservicepricegroupcode.Properties.Items.Add(dv_DanhMucNhom[i]["servicepricecode"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucDichVu();
                if (lstDanhMucDichVu.Count > 0)
                {
                    var lstDanhMucDichVuTimKiem = lstDanhMucDichVu.Where(o => o.servicepricenamebhyt.ToUpper().Contains(txtTuKhoaTimKiem.Text.ToUpper()) || o.servicepricecode.ToUpper().Contains(txtTuKhoaTimKiem.Text.ToUpper()));
                    gridControlDMDichVu.DataSource = null;
                    gridControlDMDichVu.DataSource = lstDanhMucDichVuTimKiem;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void txtTuKhoaTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTimKiem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridControlDMDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDMDichVu.FocusedRowHandle;
                servicepricerefidCurrent = Convert.ToInt64(gridViewDMDichVu.GetRowCellValue(rowHandle, "servicepricerefid").ToString());
                var serviceprice = lstDanhMucDichVu.Where(o => o.servicepricerefid == servicepricerefidCurrent).FirstOrDefault();
                if (serviceprice != null)
                {
                    txtservicepricecode.Text = serviceprice.servicepricecode;
                    cbbservicepricegroupcode.SelectedItem = serviceprice.servicepricegroupcode;
                    txtservicepricecodeuser.Text = serviceprice.servicepricecodeuser;
                    txtservicepricenamebhyt.Text = serviceprice.servicepricenamebhyt;
                    txtservicepricefeebhyt.Text = serviceprice.servicepricefeebhyt.ToString();
                    txtservicepricefeenhandan.Text = serviceprice.servicepricefeenhandan.ToString();
                    txtservicepricefee.Text = serviceprice.servicepricefee.ToString();
                    txtservicepricefeenuocngoai.Text = serviceprice.servicepricefeenuocngoai.ToString();
                    switch (serviceprice.bhyt_groupcode)
                    {
                        case "01KB":
                            cbbbhyt_groupcode.SelectedItem = "Khám bệnh";
                            break;
                        case "03XN":
                            cbbbhyt_groupcode.SelectedItem = "Xét nghiệm";
                            break;
                        case "04CDHA":
                            cbbbhyt_groupcode.SelectedItem = "Chẩn đoán hình ảnh";
                            break;
                        case "05TDCN":
                            cbbbhyt_groupcode.SelectedItem = "Thăm dò chức năng";
                            break;
                        case "06PTTT":
                            cbbbhyt_groupcode.SelectedItem = "Phẫu thuật thủ thuật";
                            break;
                        case "07KTC":
                            cbbbhyt_groupcode.SelectedItem = "Dịch vụ kỹ thuật cao";
                            break;
                        case "11VC":
                            cbbbhyt_groupcode.SelectedItem = "Vận chuyển";
                            break;
                        case "12NG":
                            cbbbhyt_groupcode.SelectedItem = "Ngày giường";
                            break;
                        case "999DVKHAC":
                            cbbbhyt_groupcode.SelectedItem = "Dịch vụ khác";
                            break;
                        case "1000PhuThu":
                            cbbbhyt_groupcode.SelectedItem = "Phụ thu";
                            break;
                        default:
                            break;
                    }
                    if (radioKhamBenh.Checked)
                    {
                        cbbNhomLoaiDichVu.SelectedItem = "Khám bệnh";
                    }
                    if (radioXetNghiem.Checked)
                    {
                        cbbNhomLoaiDichVu.SelectedItem = "Xét nghiệm";
                    }
                    if (radioCDHA.Checked)
                    {
                        cbbNhomLoaiDichVu.SelectedItem = "Chẩn đoán hình ảnh";
                    }
                    if (radioChuyenKhoa.Checked)
                    {
                        cbbNhomLoaiDichVu.SelectedItem = "Chuyên khoa";
                    }

                    btnSua.Enabled = true;
                }
                else
                {
                    txtservicepricecode.Text = "";
                    txtservicepricecodeuser.Text = "";
                    txtservicepricenamebhyt.Text = "";
                    txtservicepricefeebhyt.Text = "";
                    txtservicepricefeenhandan.Text = "";
                    txtservicepricefee.Text = "";
                    txtservicepricefeenuocngoai.Text = "";
                    cbbbhyt_groupcode.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                cbbbhyt_groupcode.Enabled = true;
                cbbservicepricegroupcode.Enabled = true;
                cbbNhomLoaiDichVu.Enabled = true;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string servicegrouptype = "";
                string bhyt_groupcode = "";
                string servicepricegroupcode = "";
                if (cbbNhomLoaiDichVu.SelectedItem.ToString() == "Khám bệnh")
                {
                    servicegrouptype = "1";
                    servicepricegroupcode = "G0";
                }
                else if (cbbNhomLoaiDichVu.SelectedItem.ToString() == "Xét nghiệm")
                {
                    servicegrouptype = "2";
                    servicepricegroupcode = "G1";
                }
                else if (cbbNhomLoaiDichVu.SelectedItem.ToString() == "Chẩn đoán hình ảnh")
                {
                    servicegrouptype = "3";
                    servicepricegroupcode = "G2";
                }
                else if (cbbNhomLoaiDichVu.SelectedItem.ToString() == "Chuyên khoa")
                {
                    servicegrouptype = "4";
                    servicepricegroupcode = "G3";
                }

                switch (cbbbhyt_groupcode.SelectedItem.ToString())
                {
                    case "Khám bệnh":
                        bhyt_groupcode = "01KB";
                        break;
                    case "Xét nghiệm":
                        bhyt_groupcode = "03XN";
                        break;
                    case "Chẩn đoán hình ảnh":
                        bhyt_groupcode = "04CDHA";
                        break;
                    case "Thăm dò chức năng":
                        bhyt_groupcode = "05TDCN";
                        break;
                    case "Phẫu thuật thủ thuật":
                        bhyt_groupcode = "06PTTT";
                        break;
                    case "Dịch vụ kỹ thuật cao":
                        bhyt_groupcode = "07KTC";
                        break;
                    case "Vận chuyển":
                        bhyt_groupcode = "11VC";
                        break;
                    case "Ngày giường":
                        bhyt_groupcode = "12NG";
                        break;
                    case "Dịch vụ khác":
                        bhyt_groupcode = "999DVKHAC";
                        break;
                    case "Phụ thu":
                        bhyt_groupcode = "1000PhuThu";
                        break;
                    default:
                        break;
                }
                if (servicegrouptype != "" && bhyt_groupcode != "" && servicepricegroupcode != "")
                {
                    //Update servicepriceref
                    string updateservicepriceref = "UPDATE servicepriceref SET servicegrouptype=" + servicegrouptype + ", bhyt_groupcode='" + bhyt_groupcode + "',servicepricegroupcode='" + cbbservicepricegroupcode.SelectedItem.ToString() + "' WHERE servicepricerefid=" + servicepricerefidCurrent + ";";
                    string updateservice_ref = "UPDATE service_ref SET servicegrouptype=" + servicegrouptype + ", servicegroupcode='" + servicepricegroupcode + "' WHERE servicecode in (select servicecode from serviceref4price where servicepricecode='" + txtservicepricecode.Text.Trim() + "')";
                    if (condb.ExecuteNonQuery(updateservicepriceref) && condb.ExecuteNonQuery(updateservice_ref))
                    {
                        MessageBox.Show("Cập nhật thành công dịch vụ mã [" + txtservicepricecode.Text.Trim() + "] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSua.Enabled = true;
                        btnLuu.Enabled = false;
                        btnHuy.Enabled = false;
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlDMDichVu_Click(null, null);
                cbbbhyt_groupcode.Enabled = false;
                cbbservicepricegroupcode.Enabled = false;
                cbbNhomLoaiDichVu.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void cbbNhomLoaiDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbbservicepricegroupcode.Properties.Items.Clear();
                LayDanhMucNhomDichVuTuongUngVoiBHYT();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }







    }
}
