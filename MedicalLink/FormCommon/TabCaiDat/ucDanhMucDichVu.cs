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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.Utils.Menu;
using Aspose.Cells;
using MedicalLink.Base;
using MedicalLink.ClassCommon;
using DevExpress.XtraTreeList.Columns;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucDanhMucDichVu : UserControl
    {
        ConnectDatabase condb = new ConnectDatabase();
        List<ToolsServicerefDTO> lsttoolsserviceref { get; set; }
        long tools_servicerefidCurrent { get; set; }
        List<ToolsOtherListDTO> lstDSSoXetNghiem { get; set; }
        public ucDanhMucDichVu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucSuaDanhMucDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                cboSoXetNghiem.Enabled = false;
                btnTimKiem_Click(null, null);
                LoadDanhSachSoXetNghiem();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachSoXetNghiem()
        {
            try
            {
                if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                {
                    this.lstDSSoXetNghiem = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "DS_SOXETNGHIEM").ToList();
                    cboSoXetNghiem.Properties.DataSource = this.lstDSSoXetNghiem;
                    cboSoXetNghiem.Properties.DisplayMember = "tools_otherlistname";
                    cboSoXetNghiem.Properties.ValueMember = "tools_otherlistid";
                }

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Create Tree
        private void HienThiLenTreeList(List<ToolsServicerefDTO> lsttools_serviceref_hienthi)
        {
            try
            {
                if (lsttools_serviceref_hienthi != null && lsttools_serviceref_hienthi.Count > 0)
                {
                    treeListDSDichVu.ClearNodes();
                    TreeListNode parentForRootNodes = null;
                    string servicegrouptype_code = "";
                    string servicegrouptype_name = "";

                    if (radioXetNghiem.Checked)
                    {
                        servicegrouptype_code = "G1";
                        servicegrouptype_name = "XÉT NGHIỆM";
                    }
                    TreeListNode rootNode_0 = treeListDSDichVu.AppendNode(
new object[] { "0", servicegrouptype_code, servicegrouptype_name, null, null, null, null, null, null },
parentForRootNodes, null);
                    CreateChildNodeServiceType(rootNode_0, servicegrouptype_code, lsttools_serviceref_hienthi);
                    treeListDSDichVu.ExpandAll();
                }
                else
                {
                    treeListDSDichVu.ClearNodes();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void CreateChildNodeServiceType(TreeListNode rootNode, string servicegrouptype_code, List<ToolsServicerefDTO> lsttools_serviceref_hienthi)
        {
            try
            {
                var listServiceTypeId = lsttools_serviceref_hienthi.FindAll(o => o.servicepricegroupcode == servicegrouptype_code).ToList().OrderBy(o => o.servicepricename);
                if (listServiceTypeId != null && listServiceTypeId.Count() > 0)
                {
                    foreach (var serviceTypeId in listServiceTypeId)
                    {
                        var serviceTypeObj = lsttools_serviceref_hienthi.Where(o => o.servicepricegroupcode == serviceTypeId.servicepricecode);
                        if (serviceTypeObj != null && serviceTypeObj.Count() > 0)
                        {
                            TreeListNode childNode = treeListDSDichVu.AppendNode(
                    new object[] { serviceTypeId.toolsservicerefid, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, null, null, null, null, null, null },
                    rootNode, null);
                            CreateChildNodeServiceType(childNode, serviceTypeId.servicepricecode, lsttools_serviceref_hienthi);
                        }
                        else //là lá
                        {
                            TreeListNode childChildNode = treeListDSDichVu.AppendNode(
                                new object[] { serviceTypeId.toolsservicerefid, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, serviceTypeId.servicepriceunit, serviceTypeId.servicepricefeebhyt, serviceTypeId.servicepricefeenhandan, serviceTypeId.tools_otherlistname, serviceTypeId.servicepricenamebhyt, serviceTypeId.servicepricenamenuocngoai, serviceTypeId.tools_otherlistid },
                                rootNode, serviceTypeId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void HienThiLenTreeList_TuKhoaTimKiem(List<ToolsServicerefDTO> lsttools_serviceref_hienthi)
        {
            try
            {
                if (lsttools_serviceref_hienthi != null && lsttools_serviceref_hienthi.Count > 0)
                {
                    treeListDSDichVu.ClearNodes();
                    TreeListNode parentForRootNodes = null;
                    foreach (var serviceTypeId in lsttools_serviceref_hienthi)
                    {
                        TreeListNode childChildNode = treeListDSDichVu.AppendNode(
                                 new object[] { serviceTypeId.toolsservicerefid, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, serviceTypeId.servicepriceunit, serviceTypeId.servicepricefeebhyt, serviceTypeId.servicepricefeenhandan, serviceTypeId.tools_otherlistname, serviceTypeId.servicepricenamebhyt, serviceTypeId.servicepricenamenuocngoai, serviceTypeId.tools_otherlistid },
                                 parentForRootNodes, serviceTypeId);
                    }
                }
                else
                {
                    treeListDSDichVu.ClearNodes();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Events
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucDichVu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void GetDataDanhMucDichVu()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string servicelock = " and servicelock=0";
                if (chkDaKhoa.Checked)
                {
                    servicelock = "";
                }

                string sqlLayDanhMuc = " select ROW_NUMBER () OVER (ORDER BY tsef.bhyt_groupcode,tsef.servicepricename) as stt, tsef.toolsservicerefid, tsef.his_servicepricerefid, tsef.servicegrouptype, tsef.servicepricetype, tsef.bhyt_groupcode, tsef.servicepricegroupcode, tsef.servicepricecode, tsef.servicepricename, tsef.servicepricenamenhandan, tsef.servicepricenamebhyt, tsef.servicepricenamenuocngoai, tsef.servicepriceunit, tsef.servicepricefee, tsef.servicepricefeenhandan, tsef.servicepricefeebhyt, tsef.servicepricefeenuocngoai, tsef.servicelock, tsef.servicepricecodeuser, tsef.servicepricesttuser, tsef.pttt_hangid, tsef.pttt_loaiid, tsef.tools_otherlistid, ot.tools_otherlistname from tools_serviceref tsef left join tools_otherlist ot on ot.tools_otherlistid=tsef.tools_otherlistid where tsef.servicegrouptype = 2 and tsef.bhyt_groupcode in ('03XN') " + servicelock + "; ";
                DataView dv_DanhMucDichVu = new DataView(condb.GetDataTable_MeL(sqlLayDanhMuc));
                if (dv_DanhMucDichVu.Count > 0)
                {
                    this.lsttoolsserviceref = new List<ToolsServicerefDTO>();
                    for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                    {
                        ToolsServicerefDTO dmDichVu = new ToolsServicerefDTO();
                        dmDichVu.toolsservicerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["toolsservicerefid"].ToString());
                        dmDichVu.his_servicepricerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["his_servicepricerefid"].ToString());
                        dmDichVu.servicepricetype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["servicepricetype"].ToString());
                        dmDichVu.servicegrouptype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["servicegrouptype"].ToString());
                        dmDichVu.bhyt_groupcode = dv_DanhMucDichVu[i]["bhyt_groupcode"].ToString();
                        dmDichVu.servicepricegroupcode = dv_DanhMucDichVu[i]["servicepricegroupcode"].ToString();
                        dmDichVu.servicepricecode = dv_DanhMucDichVu[i]["servicepricecode"].ToString();
                        dmDichVu.servicepricename = dv_DanhMucDichVu[i]["servicepricename"].ToString();
                        dmDichVu.servicepricenamenhandan = dv_DanhMucDichVu[i]["servicepricenamenhandan"].ToString();
                        dmDichVu.servicepricenamebhyt = dv_DanhMucDichVu[i]["servicepricenamebhyt"].ToString();
                        dmDichVu.servicepricenamenuocngoai = dv_DanhMucDichVu[i]["servicepricenamenuocngoai"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucDichVu[i]["servicepriceunit"].ToString();
                        dmDichVu.servicepricefee = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee"].ToString());
                        dmDichVu.servicepricefeenhandan = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan"].ToString());
                        dmDichVu.servicepricefeebhyt = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt"].ToString());
                        dmDichVu.servicepricefeenuocngoai = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString());
                        dmDichVu.servicelock = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["servicelock"].ToString());
                        dmDichVu.servicepricecodeuser = dv_DanhMucDichVu[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricesttuser = dv_DanhMucDichVu[i]["servicepricesttuser"].ToString();
                        dmDichVu.pttt_hangid = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["pttt_hangid"].ToString());
                        dmDichVu.pttt_loaiid = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["pttt_loaiid"].ToString());
                        dmDichVu.tools_otherlistid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["tools_otherlistid"].ToString());
                        dmDichVu.tools_otherlistname = dv_DanhMucDichVu[i]["tools_otherlistname"].ToString();

                        dmDichVu.servicepricecode_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.servicepricecode).ToLower();
                        dmDichVu.servicepricename_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.servicepricename).ToLower();

                        this.lsttoolsserviceref.Add(dmDichVu);
                    }
                }
                //hien thi
                if (txtTuKhoaTimKiem.Text.Trim() != "")
                {
                    string tukhoa = Utilities.Common.String.Convert.UnSignVNese(txtTuKhoaTimKiem.Text.Trim().ToLower());
                    List<ToolsServicerefDTO> lsttools_serviceref_timkiem = this.lsttoolsserviceref.Where(o => o.servicepricecode_khongdau.Contains(tukhoa) || o.servicepricename_khongdau.Contains(tukhoa)).ToList();
                    HienThiLenTreeList_TuKhoaTimKiem(lsttools_serviceref_timkiem);
                }
                else
                {
                    HienThiLenTreeList(this.lsttoolsserviceref);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                cboSoXetNghiem.Enabled = true;
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
                if (cboSoXetNghiem.EditValue != null && this.tools_servicerefidCurrent != 0)
                {
                    //Update tools_serviceref
                    string updateservicepriceref = "update tools_serviceref set tools_otherlistid='" + cboSoXetNghiem.EditValue + "' where toolsservicerefid=" + this.tools_servicerefidCurrent + " ;";
                    if (condb.ExecuteNonQuery_MeL(updateservicepriceref))
                    {
                        //MessageBox.Show("Thiết lập sổ Xét nghiệm thành công dịch vụ mã [" + txtservicepricecode.Text.Trim() + "] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                        frmthongbao.Show();
                        btnSua.Enabled = true;
                        btnLuu.Enabled = false;
                        btnHuy.Enabled = false;
                        cboSoXetNghiem.Enabled = false;
                        GetDataDanhMucDichVu();
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
                tools_servicerefidCurrent = 0;
                txtservicepricecode.Clear();
                txtservicepricegroupcode.Clear();
                txtservicepricecodeuser.Clear();
                txtservicepricenamenhandan.Clear();
                txtservicepricenamebhyt.Clear();
                txtservicepricenamepttt.Clear();
                txtservicepricefeebhyt.Clear();
                txtservicepricefeenhandan.Clear();
                txtservicepricefee.Clear();
                txtservicepricefeenuocngoai.Clear();
                cboSoXetNghiem.EditValue = null;
                txtbhyt_groupcode.Clear();
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                cboSoXetNghiem.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void treeListDSDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                var nodes = treeListDSDichVu.FocusedNode;
                tools_servicerefidCurrent = Convert.ToInt64(nodes.GetValue(toolsservicerefid).ToString());
                var serviceprice = lsttoolsserviceref.Where(o => o.toolsservicerefid == tools_servicerefidCurrent).FirstOrDefault();
                if (serviceprice != null)
                {
                    txtservicepricecode.Text = serviceprice.servicepricecode;
                    txtservicepricegroupcode.Text = serviceprice.servicepricegroupcode;
                    txtservicepricecodeuser.Text = serviceprice.servicepricecodeuser;
                    txtservicepricenamenhandan.Text = serviceprice.servicepricenamenhandan;
                    txtservicepricenamebhyt.Text = serviceprice.servicepricenamebhyt;
                    txtservicepricenamepttt.Text = serviceprice.servicepricenamenuocngoai;
                    txtservicepricefeebhyt.Text = serviceprice.servicepricefeebhyt.ToString();
                    txtservicepricefeenhandan.Text = serviceprice.servicepricefeenhandan.ToString();
                    txtservicepricefee.Text = serviceprice.servicepricefee.ToString();
                    txtservicepricefeenuocngoai.Text = serviceprice.servicepricefeenuocngoai.ToString();
                    cboSoXetNghiem.EditValue = serviceprice.tools_otherlistid;
                    switch (serviceprice.bhyt_groupcode)
                    {
                        case "01KB":
                            txtbhyt_groupcode.Text = "Khám bệnh";
                            break;
                        case "03XN":
                            txtbhyt_groupcode.Text = "Xét nghiệm";
                            break;
                        case "04CDHA":
                            txtbhyt_groupcode.Text = "Chẩn đoán hình ảnh";
                            break;
                        case "05TDCN":
                            txtbhyt_groupcode.Text = "Thăm dò chức năng";
                            break;
                        case "06PTTT":
                            txtbhyt_groupcode.Text = "Phẫu thuật thủ thuật";
                            break;
                        case "07KTC":
                            txtbhyt_groupcode.Text = "Dịch vụ kỹ thuật cao";
                            break;
                        case "11VC":
                            txtbhyt_groupcode.Text = "Vận chuyển";
                            break;
                        case "12NG":
                            txtbhyt_groupcode.Text = "Ngày giường";
                            break;
                        case "999DVKHAC":
                            txtbhyt_groupcode.Text = "Dịch vụ khác";
                            break;
                        case "1000PhuThu":
                            txtbhyt_groupcode.Text = "Phụ thu";
                            break;
                        default:
                            break;
                    }

                    btnSua.Enabled = true;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;
                }
                else
                {
                    txtservicepricecode.Clear();
                    txtservicepricegroupcode.Clear();
                    txtservicepricecodeuser.Clear();
                    txtservicepricenamenhandan.Clear();
                    txtservicepricenamebhyt.Clear();
                    txtservicepricenamepttt.Clear();
                    txtservicepricefeebhyt.Clear();
                    txtservicepricefeenhandan.Clear();
                    txtservicepricefee.Clear();
                    txtservicepricefeenuocngoai.Clear();
                    cboSoXetNghiem.EditValue = null;
                    txtbhyt_groupcode.Clear();
                    btnSua.Enabled = false;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnCapNhatDanhSach_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                int dem_insert = 0;
                int dem_update = 0;
                if (this.lsttoolsserviceref != null && this.lsttoolsserviceref.Count > 0)
                {
                    //kiemtra so sanh trong danh sach DM XN .
                    string sql_kiemtra = "select servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenhandan, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, bhyt_groupcode, pttt_hangid, pttt_loaiid, servicepricecodeuser, servicepricesttuser from servicepriceref where servicegrouptype = 2; ";
                    DataView dv_DanhMucDichVuHIS = new DataView(condb.GetDataTable_HIS(sql_kiemtra));
                    if (dv_DanhMucDichVuHIS != null && dv_DanhMucDichVuHIS.Count > 0)
                    {
                        for (int i = 0; i < dv_DanhMucDichVuHIS.Count; i++)
                        {
                            List<ToolsServicerefDTO> lstdanhmuc_timkiem = this.lsttoolsserviceref.Where(o => o.servicepricecode == dv_DanhMucDichVuHIS[i]["servicepricecode"].ToString()).ToList();
                            if (lstdanhmuc_timkiem != null && lstdanhmuc_timkiem.Count > 0) //update
                            {
                                string sql_update = "UPDATE tools_serviceref SET his_servicepricerefid='" + dv_DanhMucDichVuHIS[i]["servicepricerefid"].ToString() + "', servicegrouptype='" + dv_DanhMucDichVuHIS[i]["servicegrouptype"].ToString() + "', servicepricetype='" + dv_DanhMucDichVuHIS[i]["servicepricetype"].ToString() + "', bhyt_groupcode='" + dv_DanhMucDichVuHIS[i]["bhyt_groupcode"].ToString() + "', servicepricegroupcode='" + dv_DanhMucDichVuHIS[i]["servicepricegroupcode"].ToString() + "', servicepricename='" + dv_DanhMucDichVuHIS[i]["servicepricename"].ToString() + "', servicepricenamenhandan='" + dv_DanhMucDichVuHIS[i]["servicepricenamenhandan"].ToString() + "', servicepricenamebhyt='" + dv_DanhMucDichVuHIS[i]["servicepricenamebhyt"].ToString() + "', servicepricenamenuocngoai='" + dv_DanhMucDichVuHIS[i]["servicepricenamenuocngoai"].ToString() + "', servicepriceunit='" + dv_DanhMucDichVuHIS[i]["servicepriceunit"].ToString() + "', servicepricefee='" + dv_DanhMucDichVuHIS[i]["servicepricefee"].ToString() + "', servicepricefeenhandan='" + dv_DanhMucDichVuHIS[i]["servicepricefeenhandan"].ToString() + "', servicepricefeebhyt='" + dv_DanhMucDichVuHIS[i]["servicepricefeebhyt"].ToString() + "', servicepricefeenuocngoai='" + dv_DanhMucDichVuHIS[i]["servicepricefeenuocngoai"].ToString() + "', servicelock='" + dv_DanhMucDichVuHIS[i]["servicelock"].ToString() + "', servicepricecodeuser='" + dv_DanhMucDichVuHIS[i]["servicepricecodeuser"].ToString() + "', servicepricesttuser='" + dv_DanhMucDichVuHIS[i]["servicepricesttuser"].ToString() + "', pttt_hangid='" + dv_DanhMucDichVuHIS[i]["pttt_hangid"].ToString() + "', pttt_loaiid='" + dv_DanhMucDichVuHIS[i]["pttt_loaiid"].ToString() + "' WHERE servicepricecode='" + dv_DanhMucDichVuHIS[i]["servicepricecode"].ToString() + "'; ";
                                if (condb.ExecuteNonQuery_MeL(sql_update))
                                {
                                    dem_update += 1;
                                }
                            }
                            else //insert
                            {
                                string sql_insert = " INSERT INTO tools_serviceref(his_servicepricerefid, servicegrouptype, servicepricetype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid) VALUES ('" + dv_DanhMucDichVuHIS[i]["servicepricerefid"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicegrouptype"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricetype"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["bhyt_groupcode"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricegroupcode"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricecode"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricename"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricenamenhandan"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricenamebhyt"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricenamenuocngoai"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepriceunit"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricefee"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricefeenhandan"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricefeebhyt"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricefeenuocngoai"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicelock"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricecodeuser"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["servicepricesttuser"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["pttt_hangid"].ToString() + "', '" + dv_DanhMucDichVuHIS[i]["pttt_loaiid"].ToString() + "');  ";
                                if (condb.ExecuteNonQuery_MeL(sql_insert))
                                {
                                    dem_insert += 1;
                                }
                            }
                        }
                    }
                    MessageBox.Show("Làm mới danh mục thành công (thêm mới SL=[" + dem_insert + "]; cập nhật SL=[" + dem_update + "]) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql_insert = "insert into tools_serviceref(his_servicepricerefid,servicegrouptype,servicepricetype,bhyt_groupcode,servicepricegroupcode,servicepricecode,servicepricename,servicepricenamenhandan,servicepricenamebhyt,servicepricenamenuocngoai,servicepriceunit,servicepricefee,servicepricefeenhandan,servicepricefeebhyt,servicepricefeenuocngoai,servicelock,servicepricecodeuser,servicepricesttuser,pttt_hangid,pttt_loaiid) SELECT serf_his.* FROM dblink('myconn','select servicepricerefid,servicegrouptype,servicepricetype,bhyt_groupcode,servicepricegroupcode,servicepricecode,servicepricename,servicepricenamenhandan,servicepricenamebhyt,servicepricenamenuocngoai,servicepriceunit,servicepricefee,servicepricefeenhandan,servicepricefeebhyt,servicepricefeenuocngoai,servicelock,servicepricecodeuser,servicepricesttuser,pttt_hangid,pttt_loaiid FROM servicepriceref where servicegrouptype = 2 order by servicepricerefid') AS serf_his(servicepricerefid integer,servicegrouptype integer,servicepricetype integer,bhyt_groupcode text,servicepricegroupcode text,servicepricecode text,servicepricename text,servicepricenamenhandan text,servicepricenamebhyt text,servicepricenamenuocngoai text,servicepriceunit text,servicepricefee text,servicepricefeenhandan text,servicepricefeebhyt text,servicepricefeenuocngoai text,servicelock integer,servicepricecodeuser text,servicepricesttuser text,pttt_hangid integer,pttt_loaiid integer); ";
                    if (condb.ExecuteNonQuery_MeLToHIS(sql_insert))
                    {
                        MessageBox.Show("Cập nhật dịch vụ từ HIS sang thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
            GetDataDanhMucDichVu();
        }

        #endregion

        #region Click Thiet Lap So Xet Nghiem
        private void treeListDSDichVu_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                TreeList tL = sender as TreeList;
                TreeListHitInfo hitInfo = tL.CalcHitInfo(e.Point);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {

                    DXSubMenuItem menuChuyenTT = new DXSubMenuItem("Thiết lập Sổ xét nghiệm"); // caption menu
                    menuChuyenTT.Image = imageCollectionMBA.Images[0]; // icon cho menu
                    e.Menu.Items.Add(menuChuyenTT);

                    foreach (var item_So in this.lstDSSoXetNghiem)
                    {
                        DXMenuItem item_menuSo = new DXMenuItem(item_So.tools_otherlistname);
                        item_menuSo.Image = imageCollectionMBA.Images[1];
                        menuChuyenTT.Items.Add(item_menuSo);
                        item_menuSo.Click += new EventHandler(ThietLapSoXetNghiem_Click);// thêm sự kiện click
                    }
                    DXMenuItem item_menuSo_Xoa = new DXMenuItem("Bỏ khỏi Sổ xét nghiệm");
                    item_menuSo_Xoa.Image = imageCollectionMBA.Images[2];
                    menuChuyenTT.Items.Add(item_menuSo_Xoa);
                    item_menuSo_Xoa.Click += new EventHandler(ThietLapSoXetNghiem_Click);// thêm sự kiện click
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void ThietLapSoXetNghiem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolsOtherListDTO loaiso = this.lstDSSoXetNghiem.Where(o => o.tools_otherlistname == (sender as DXMenuItem).Caption).FirstOrDefault();

                var nodes = treeListDSDichVu.Selection;
                //List<string> values = new List<string>();
                foreach (TreeListNode node in nodes)
                {
                    string updateservicepriceref = "";
                    if (loaiso != null)
                    {
                        updateservicepriceref = "update tools_serviceref set tools_otherlistid='" + loaiso.tools_otherlistid + "' where toolsservicerefid=" + node.GetValue(toolsservicerefid).ToString() + " ;";
                    }
                    else
                    {
                        updateservicepriceref = "update tools_serviceref set tools_otherlistid=0 where toolsservicerefid=" + node.GetValue(toolsservicerefid).ToString() + " ;";
                    }
                    condb.ExecuteNonQuery_MeL(updateservicepriceref);
                }
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                GetDataDanhMucDichVu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void txtTuKhoaTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDataDanhMucDichVu();
            }
        }
        private void treeListDSDichVu_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node == (sender as TreeList).FocusedNode)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Event Poup Menu

        #endregion


    }
}
