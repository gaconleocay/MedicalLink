using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MedicalLink.ClassCommon;

namespace MedicalLink.BUS
{
    public class LoadDataSystems
    {
        private static DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        internal static void LoadDanhSachCauHinhDungChung()
        {
            try
            {
                //Danh muc Dung chung
                string sqlNhomDV = "select ot.tools_othertypelistid, ot.tools_othertypelistcode, ot.tools_othertypelistname, ot.tools_othertypeliststatus, ot.tools_othertypelistnote, o.tools_otherlistid, o.tools_otherlistcode, o.tools_otherlistname, o.tools_otherlistvalue, o.tools_otherliststatus from tools_othertypelist ot inner join tools_otherlist o on o.tools_othertypelistid=ot.tools_othertypelistid;";
                DataTable dataLoaiBaoCao = condb.GetDataTable_MeL(sqlNhomDV);
                if (dataLoaiBaoCao != null && dataLoaiBaoCao.Rows.Count > 0)
                {
                    GlobalStore.lstOtherList_Global = new List<ToolsOtherListDTO>();
                    for (int i = 0; i < dataLoaiBaoCao.Rows.Count; i++)
                    {
                        ClassCommon.ToolsOtherListDTO otherList = new ToolsOtherListDTO();
                        otherList.tools_othertypelistid = Utilities.TypeConvertParse.ToInt64(dataLoaiBaoCao.Rows[i]["tools_othertypelistid"].ToString());
                        otherList.tools_othertypelistcode = dataLoaiBaoCao.Rows[i]["tools_othertypelistcode"].ToString();
                        otherList.tools_othertypelistcode = dataLoaiBaoCao.Rows[i]["tools_othertypelistcode"].ToString();
                        //otherList.tools_othertypeliststatus = dataLoaiBaoCao.Rows[i]["tools_othertypeliststatus"].ToString();
                        otherList.tools_othertypelistnote = dataLoaiBaoCao.Rows[i]["tools_othertypelistnote"].ToString();
                        otherList.tools_otherlistid = Utilities.TypeConvertParse.ToInt64(dataLoaiBaoCao.Rows[i]["tools_otherlistid"].ToString());
                        otherList.tools_otherlistcode = dataLoaiBaoCao.Rows[i]["tools_otherlistcode"].ToString();
                        otherList.tools_otherlistname = dataLoaiBaoCao.Rows[i]["tools_otherlistname"].ToString();
                        otherList.tools_otherlistvalue = dataLoaiBaoCao.Rows[i]["tools_otherlistvalue"].ToString();
                        //otherList.tools_otherliststatus = dataLoaiBaoCao.Rows[i]["tools_otherliststatus"].ToString();
                        GlobalStore.lstOtherList_Global.Add(otherList);
                    }
                }

                //Danh muc Option
                //Load thong tin Luu vao GlobalStore
                string sqlDSOption = "SELECT toolsoptionid, toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote,toolsoptionlook,toolsoptiondate,toolsoptioncreateuser FROM tools_option WHERE toolsoptionlook<>'1' ;";
                DataTable dataOption = condb.GetDataTable_MeL(sqlDSOption);
                if (dataOption != null && dataOption.Rows.Count > 0)
                {
                    GlobalStore.lstOption = Utilities.DataTables.DataTableToList<ToolsOptionDTO>(dataOption);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        internal static void LoadCauHinhThoiGianLayDuLieu()
        {
            try
            {
                //Set default
                MedicalLink.GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp = 0;
                MedicalLink.GlobalStore.KhoangThoiGianLayDuLieu = System.DateTime.Now.Year - 1 + "-01-01 00:00:00";
                if (GlobalStore.lstOption != null && GlobalStore.lstOption.Count > 0)
                {
                    //GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp = TypeConvertParse.ToInt64(GlobalStore.lstOption.Where(o => o.toolsoptioncode == "ThoiGianCapNhatTbl_tools_bndangdt_tmp").FirstOrDefault().toolsoptionvalue);
                    GlobalStore.KhoangThoiGianLayDuLieu = GlobalStore.lstOption.Where(o => o.toolsoptioncode == "KhoangThoiGianLayDuLieu").FirstOrDefault().toolsoptionvalue;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        internal static void LoadDanhSachKhoaTrongBenhVien()
        {
            try
            {
                string sqlKhoaBV = "SELECT degp.departmentgroupid,degp.departmentgroupcode,degp.departmentgroupname,degp.departmentgrouptype,de.departmentid,de.departmentcode,de.departmentname,de.departmenttype FROM departmentgroup degp inner join department de on de.departmentgroupid=degp.departmentgroupid; ";
                DataTable dataKhoaBV = condb.GetDataTable_MeL(sqlKhoaBV);
                if (dataKhoaBV != null && dataKhoaBV.Rows.Count > 0)
                {
                    GlobalStore.lstDepartmentBV = new List<DepartmentDTO>();
                    for (int i = 0; i < dataKhoaBV.Rows.Count; i++)
                    {
                        ClassCommon.DepartmentDTO otherList = new DepartmentDTO();
                        otherList.departmentgroupid = Utilities.TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmentgroupid"].ToString());
                        otherList.departmentgroupcode = dataKhoaBV.Rows[i]["departmentgroupcode"].ToString();
                        otherList.departmentgroupname = dataKhoaBV.Rows[i]["departmentgroupname"].ToString();
                        otherList.departmentgrouptype = Utilities.TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmentgrouptype"].ToString());
                        otherList.departmentid = Utilities.TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmentid"].ToString());
                        otherList.departmentcode = dataKhoaBV.Rows[i]["departmentcode"].ToString();
                        otherList.departmentname = dataKhoaBV.Rows[i]["departmentname"].ToString();
                        otherList.departmenttype = Utilities.TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmenttype"].ToString());
                        GlobalStore.lstDepartmentBV.Add(otherList);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        internal static void LoadDanhMucDichVuKyThuat()
        {
            try
            {
                string _sqlDVKT = "select * from ServicePriceRef where servicegrouptype in (1,2,3,4,11);";
                DataTable _dataDVKT = condb.GetDataTable_HIS(_sqlDVKT);
                if (_dataDVKT != null && _dataDVKT.Rows.Count > 0)
                {
                    GlobalStore.lstServicepriceRef = Utilities.DataTables.DataTableToList<ClassCommon.Base.ServicepriceRefDTO>(_dataDVKT);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
