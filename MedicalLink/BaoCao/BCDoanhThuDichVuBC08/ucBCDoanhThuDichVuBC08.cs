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
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

namespace MedicalLink.BaoCao
{
    public partial class ucBCDoanhThuDichVuBC08 : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        DataTable dataBaoCao { get; set; }
        public ucBCDoanhThuDichVuBC08()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBCDoanhThuDichVuBC08_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            radioXemTongHop.Checked = true;
            LoadDanhSachNhomDichVu();
        }
        private void LoadDanhSachNhomDichVu()
        {
            try
            {
                List<NhomDichVuDTO> lstNhomDichVu = new List<NhomDichVuDTO>();
                NhomDichVuDTO _item_KB = new NhomDichVuDTO();
                _item_KB.servicegrouptype = 1;
                _item_KB.servicegrouptypename = "Khám bệnh";
                _item_KB.bhyt_groupcode = "'01KB'";
                NhomDichVuDTO _item_XN = new NhomDichVuDTO();
                _item_XN.servicegrouptype = 2;
                _item_XN.servicegrouptypename = "Xét nghiệm";
                _item_XN.bhyt_groupcode = "'03XN'";
                NhomDichVuDTO _item_CDHA = new NhomDichVuDTO();
                _item_CDHA.servicegrouptype = 3;
                _item_CDHA.servicegrouptypename = "CĐHA";
                _item_CDHA.bhyt_groupcode = "'04CDHA','05TDCN'";
                NhomDichVuDTO _item_CK = new NhomDichVuDTO();
                _item_CK.servicegrouptype = 4;
                _item_CK.servicegrouptypename = "Chuyên khoa";
                _item_CK.bhyt_groupcode = "'06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC'";
                lstNhomDichVu.Add(_item_KB);
                lstNhomDichVu.Add(_item_XN);
                lstNhomDichVu.Add(_item_CDHA);
                lstNhomDichVu.Add(_item_CK);

                chkcomboNhomDichVu.Properties.DataSource = lstNhomDichVu;
                chkcomboNhomDichVu.Properties.DisplayMember = "servicegrouptypename";
                chkcomboNhomDichVu.Properties.ValueMember = "servicegrouptype";
                chkcomboNhomDichVu.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                this.dataBaoCao = new DataTable();
                string _servicegrouptype = " servicegrouptype in (";
                string _bhyt_groupcode = " bhyt_groupcode in (";
                string _trangthaibenhan = "";
                string _servicepricedate = "";
                //string _vienphidate = "";
                string sql_timkiem = "";
                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTrangThaiVienPhi.Text == "Đang điều trị")
                {
                    _trangthaibenhan = " vienphistatus=0 and vienphidate between '" + datetungay + "' and '" + datedenngay + "'";
                }
                else if (cboTrangThaiVienPhi.Text == "Ra viện chưa thanh toán")
                {
                    _trangthaibenhan = " vienphistatus>0 and coalesce(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    _trangthaibenhan = " vienphistatus>0 and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }

                List<Object> lstNhomDVCheck = chkcomboNhomDichVu.Properties.Items.GetCheckedValues();
                if (lstNhomDVCheck.Count > 0)
                {
                    for (int i = 0; i < lstNhomDVCheck.Count - 1; i++)
                    {
                        _servicegrouptype += "'" + lstNhomDVCheck[i] + "', ";
                    }
                    _servicegrouptype += "'" + lstNhomDVCheck[lstNhomDVCheck.Count - 1] + "') ";
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_NHOM_DICH_VU);
                    frmthongbao.Show();
                }
                if (_servicegrouptype.Contains("1"))
                {
                    _bhyt_groupcode += "'01KB',";
                }
                if (_servicegrouptype.Contains("2"))
                {
                    _bhyt_groupcode += "'03XN',";
                }
                if (_servicegrouptype.Contains("3"))
                {
                    _bhyt_groupcode += "'04CDHA','05TDCN',";
                }
                if (_servicegrouptype.Contains("4"))
                {
                    _bhyt_groupcode += "'06PTTT','07KTC','12NG','999DVKHAC','1000PhuThu','11VC',";
                }
                _bhyt_groupcode = _bhyt_groupcode.Remove(_bhyt_groupcode.Length - 1, 1) + ")";


                if (radioXemTongHop.Checked) //xem tong hop --ngay 23/1/2018
                {
                    sql_timkiem = " SELECT row_number () over (order by serf.servicegrouptype,serf.servicepricegroupcode,serf.servicepricename) as stt, serf.servicepricegroupcode, serf.bhyt_groupcode, serf.servicegrouptype, serf.servicepricetype, (case serf.servicegrouptype when 1 then 'Khám bệnh' when 2 then 'Xét nghiệm' when 3 then 'CĐHA' when 4 then 'Chuyên khoa' end) as servicegrouptype_name, serf.servicepricecode, serf.servicepricename, serf.servicepricenamebhyt, serf.servicepriceunit, ser.loaidoituong, ser.loaidoituong_name, ser.soluong, ser.servicepricemoney, ser.thanhtien, '0' as isgroup FROM (select servicepricegroupcode,bhyt_groupcode,servicegrouptype,servicepricetype,servicepricecode,servicepricename,servicepricenamebhyt,servicepriceunit,servicepricefee,servicepricefeenhandan,servicepricefeebhyt,servicepricefeenuocngoai from servicepriceref where " + _servicegrouptype + ") serf left join (select se.servicepricecode, sum(se.soluong) as soluong, se.loaidoituong, (case se.loaidoituong when 0 then 'BHYT' when 1 then 'VP' when 2 then 'Đi kèm' when 3 then 'YC' when 4 then 'BHYT+YC' when 6 then 'BHYT+YC' when 20 then 'TT riêng' end) as loaidoituong_name, (case when se.loaidoituong in (0,2,20) then se.servicepricemoney_bhyt when se.loaidoituong in (1,8) then (case when se.doituongbenhnhanid=4 then se.servicepricemoney_nuocngoai else se.servicepricemoney_nhandan end) when se.loaidoituong in (3,4,6) then (case when se.doituongbenhnhanid=4 then se.servicepricemoney_nuocngoai else (case when se.servicepricemoney>se.servicepricemoney_bhyt then se.servicepricemoney else servicepricemoney_bhyt end) end) else 0 end) as servicepricemoney, sum((case when se.loaidoituong in (0,2,20) then se.servicepricemoney_bhyt when se.loaidoituong in (1,8) then (case when se.doituongbenhnhanid=4 then se.servicepricemoney_nuocngoai else se.servicepricemoney_nhandan end) when se.loaidoituong in (3,4,6) then (case when se.doituongbenhnhanid=4 then se.servicepricemoney_nuocngoai else (case when se.servicepricemoney>se.servicepricemoney_bhyt then se.servicepricemoney else servicepricemoney_bhyt end) end) else 0 end)*se.soluong) as thanhtien, se.bhyt_groupcode from (select vienphiid,servicepricecode,loaidoituong,bhyt_groupcode,soluong,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,servicepricemoney_nuocngoai,doituongbenhnhanid from serviceprice where " + _bhyt_groupcode + _servicepricedate + ") se inner join (select vienphiid from vienphi where " + _trangthaibenhan + ") vp on vp.vienphiid=se.vienphiid group by se.servicepricecode,se.loaidoituong,se.bhyt_groupcode,se.servicepricemoney_bhyt,se.servicepricemoney_nhandan,se.servicepricemoney,se.servicepricemoney_nuocngoai,se.doituongbenhnhanid) ser on ser.servicepricecode=serf.servicepricecode WHERE ser.soluong>0 or serf.servicepricetype=1;";
                }
                else
                {
                    sql_timkiem = " ";
                }
                this.dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    //List<BCDoanhThuDichVuBC08DTO> lstDataBaoCao = new List<BCDoanhThuDichVuBC08DTO>();
                    //lstDataBaoCao = Utilities.Util_DataTable.DataTableToList<BCDoanhThuDichVuBC08DTO>(_dataBC);
                    gridControlDataBaoCao.DataSource = this.dataBaoCao;
                    //Hien thi du lieu len TreeList
                    //HienThiLenTreeList(lstDataBaoCao);
                }
                else
                {
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

        /*
        #region  Hien thi len Tree list
        private void HienThiLenTreeList(List<BCDoanhThuDichVuBC08DTO> _lstDataBaoCao)
        {
            try
            {
                if (_lstDataBaoCao != null && _lstDataBaoCao.Count > 0)
                {
                    treeListDSDichVu.ClearNodes();
                    //TreeListNode parentForRootNodes = null;
                    List<BCDoanhThuDichVuBC08DTO> _lstData_KB = _lstDataBaoCao.Where(o => o.servicegrouptype == 1).ToList();
                    if (_lstData_KB != null && _lstData_KB.Count > 0)
                    {
                        foreach (var item in _lstData_KB)
                        {
                            if (item.servicepricegroupcode == null || item.servicepricegroupcode == "")
                            {
                                item.servicepricegroupcode = "G0";
                            }
                        }
                        CreateNode_NhomDichVu(_lstData_KB, "G0", "KHÁM BỆNH");
                    }
                    List<BCDoanhThuDichVuBC08DTO> _lstData_XN = _lstDataBaoCao.Where(o => o.servicegrouptype == 2).ToList();
                    if (_lstData_XN != null && _lstData_XN.Count > 0)
                    {
                        CreateNode_NhomDichVu(_lstData_XN, "G1", "XÉT NGHIỆM");
                    }
                    List<BCDoanhThuDichVuBC08DTO> _lstData_CDHA = _lstDataBaoCao.Where(o => o.servicegrouptype == 3).ToList();
                    if (_lstData_CDHA != null && _lstData_CDHA.Count > 0)
                    {
                        CreateNode_NhomDichVu(_lstData_CDHA, "G2", "CĐHA");
                    }
                    List<BCDoanhThuDichVuBC08DTO> _lstData_CK = _lstDataBaoCao.Where(o => o.servicegrouptype == 4).ToList();
                    if (_lstData_CK != null && _lstData_CK.Count > 0)
                    {
                        CreateNode_NhomDichVu(_lstData_CK, "G3", "CHUYÊN KHOA");
                    }
                }
                else
                {
                    treeListDSDichVu.ClearNodes();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void CreateNode_NhomDichVu(List<BCDoanhThuDichVuBC08DTO> _lstData_Group, string servicegrouptype_code, string servicegrouptype_name)
        {
            try
            {
                TreeListNode parentForRootNodes = null;
                TreeListNode rootNode_0 = treeListDSDichVu.AppendNode(
new object[] { "0", servicegrouptype_name, servicegrouptype_code, servicegrouptype_name, null, null, null, null }, parentForRootNodes, null);
                CreateChildNodeServiceType(rootNode_0, servicegrouptype_code, _lstData_Group);
                treeListDSDichVu.ExpandAll();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void CreateChildNodeServiceType(TreeListNode rootNode, string servicegrouptype_code, List<BCDoanhThuDichVuBC08DTO> lsttools_serviceref_hienthi)
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
                    new object[] { serviceTypeId.servicepricegroupcode, serviceTypeId.servicegrouptype_name, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, null, null, null, null },
                    rootNode, null);
                            CreateChildNodeServiceType(childNode, serviceTypeId.servicepricecode, lsttools_serviceref_hienthi);
                        }
                        else //là lá
                        {
                            if (serviceTypeId.soluong > 0)
                            {
                                TreeListNode childChildNode = treeListDSDichVu.AppendNode(
                                    new object[] { serviceTypeId.servicepricegroupcode, serviceTypeId.servicegrouptype_name, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, serviceTypeId.loaidoituong_name, serviceTypeId.soluong, serviceTypeId.servicepricemoney, serviceTypeId.thanhtien }, rootNode, serviceTypeId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion
        */
        #endregion

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

                string fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_TongHop.xlsx";
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
                }
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
            //DataTable _dataBaoCao = TraverseTreeView(treeListDSDichVu);
            //List<BCDoanhThuDichVuBC08DTO> _list = TreeListToList(treeListDSDichVu);
            //try
            //{
            //    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            //    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
            //    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
            //    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

            //    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
            //    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
            //    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
            //    reportitem.value = tungaydenngay;
            //    thongTinThem.Add(reportitem);

            //    string fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_TongHop.xlsx";
            //    if (radioXemChiTiet.Checked)
            //    {
            //        fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
            //    }
            //    DataTable data_XuatBaoCao = ExportExcel_GroupColume();
            //    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
            //    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
            //}
            //catch (Exception ex)
            //{
            //    MedicalLink.Base.Logging.Warn(ex);
            //}
            //SplashScreenManager.CloseForm();
        }

        //public List<BCDoanhThuDichVuBC08DTO> TreeListToList(TreeList treelist)
        //{
        //    List<BCDoanhThuDichVuBC08DTO> result = new List<BCDoanhThuDichVuBC08DTO>();
        //    try
        //    {
        //        var nodes = treeListDSDichVu.Selection;
        //        //List<string> values = new List<string>();
        //        foreach (TreeListNode item in nodes)
        //        {
        //        //    for (int i = 0; i < treelist.AllNodesCount; i++)
        //        //{
        //            //TreeListNode item = treelist.Nodes[i];
        //            //DataRow _dr = (DataRow)treelist.GetDataRecordByNode(i);
        //            BCDoanhThuDichVuBC08DTO _itemData = new BCDoanhThuDichVuBC08DTO();
        //            _itemData.servicepricegroupcode = item.GetValue("servicepricegroupcode").ToString();
        //            _itemData.servicegrouptype_name = item.GetValue("servicegrouptype_name").ToString();
        //            _itemData.servicepricecode = item.GetValue("servicepricecode").ToString();
        //            _itemData.servicepricename = item.GetValue("servicepricename").ToString();
        //            _itemData.loaidoituong_name = (item.GetValue("loaidoituong_name") ?? "").ToString();
        //            _itemData.soluong = Utilities.Util_TypeConvertParse.ToDecimal((item.GetValue("soluong") ?? "0").ToString());
        //            _itemData.servicepricemoney = Utilities.Util_TypeConvertParse.ToDecimal((item.GetValue("servicepricemoney") ?? "0").ToString());
        //            _itemData.thanhtien = Utilities.Util_TypeConvertParse.ToDecimal((item.GetValue("thanhtien") ?? "0").ToString());

        //            result.Add(_itemData);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return result;
        //}



        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                List<ClassCommon.BCDoanhThuDichVuBC08DTO> lstData_XuatBaoCao = new List<ClassCommon.BCDoanhThuDichVuBC08DTO>();
                List<ClassCommon.BCDoanhThuDichVuBC08DTO> lstDataDoanhThu = new List<ClassCommon.BCDoanhThuDichVuBC08DTO>();
                lstDataDoanhThu = Util_DataTable.DataTableToList<ClassCommon.BCDoanhThuDichVuBC08DTO>(this.dataBaoCao);

                List<ClassCommon.BCDoanhThuDichVuBC08DTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.servicepricegroupcode).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BCDoanhThuDichVuBC08DTO data_groupname = new ClassCommon.BCDoanhThuDichVuBC08DTO();
                    List<ClassCommon.BCDoanhThuDichVuBC08DTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.servicepricegroupcode == item_group.servicepricegroupcode).ToList();
                    decimal sum_soluong = 0;
                    decimal sum_thanhtien = 0;
                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_soluong += item_tinhsum.soluong;
                        sum_thanhtien += item_tinhsum.thanhtien;
                    }

                    //data_groupname.departmentgroupid = item_group.departmentgroupid;
                    data_groupname.stt = item_group.servicepricegroupcode;
                    data_groupname.soluong = sum_soluong;
                    data_groupname.thanhtien = sum_thanhtien;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.Util_DataTable.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
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

                string fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_TongHop.xlsx";
                if (radioXemChiTiet.Checked)
                {
                    fileTemplatePath = "BC_DoanhThuTheoNhomDichVu_ChiTiet.xlsx";
                }
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
        private void gridViewDataBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        #region Event Change
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;

                    //gridColumn_PhongChiDinh.Visible = false;
                    //gridColumn_MaVP.Visible = false;
                    //gridColumn_MaBN.Visible = false;
                    //gridColumn_TenBN.Visible = false;
                    //gridColumn_NamSinh.Visible = false;
                    //gridColumn_GioiTinh.Visible = false;
                    //gridColumn_TheBHYT.Visible = false;
                    //gridColumn_TGChiDinh.Visible = false;
                    //btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void radioXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemChiTiet.Checked)
                {
                    radioXemTongHop.Checked = false;

                    //gridColumn_PhongChiDinh.Visible = true;
                    //gridColumn_MaVP.Visible = true;
                    //gridColumn_MaBN.Visible = true;
                    //gridColumn_TenBN.Visible = true;
                    //gridColumn_NamSinh.Visible = true;
                    //gridColumn_GioiTinh.Visible = true;
                    //gridColumn_TheBHYT.Visible = true;
                    //gridColumn_TGChiDinh.Visible = true;
                    //btnTimKiem_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void cboTrangThaiVienPhi_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboTrangThaiVienPhi.Text == "Đã thanh toán")
                {
                    dateTuNgay.Enabled = true;
                    dateDenNgay.Enabled = true;
                }
                else
                {
                    dateTuNgay.Enabled = false;
                    dateDenNgay.Enabled = false;
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
