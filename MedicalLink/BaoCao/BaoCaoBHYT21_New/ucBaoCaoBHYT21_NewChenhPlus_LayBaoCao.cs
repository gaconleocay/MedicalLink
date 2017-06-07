using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.ChucNang
{
    public partial class ucBaoCaoBHYT21_NewChenh : UserControl
    {
        #region Khai Bao Bien
        private string datetungay = "";
        private string datedenngay = "";
        private string tieuChiBaoCao = "";
        private string loaiBenhAn = "";
        private string danhSachIdKhoa = "";
        private List<long> lstmadungtuyen { get; set; }
        //private List<long> lstVienPhiId_NhomA { get; set; }
        //private List<long> lstVienPhiId_NhomB { get; set; }
        //private List<long> lstVienPhiId_NhomC { get; set; }
        private long maTinh_BV = new long();
        private List<MedicalLink.ClassCommon.classBCBHYT21ChenhNew> lstDataResult_All { get; set; }

        #endregion
        private void LayThoiGianLayBaoCao()
        {
            try
            {
                // Lấy từ ngày, đến ngày
                datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LayDanhSachLocTheoKhoa()
        {
            try
            {
                if (cbbLoaiBA.Text == "Lọc theo khoa")
                {
                    string danhsachkhoacheck = "";
                    for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count - 1; i++)
                    {
                        if (chkcomboListDSKhoa.Properties.Items[i].CheckState == CheckState.Checked)
                        {
                            danhsachkhoacheck += chkcomboListDSKhoa.Properties.Items[i].Value + ",";
                        }
                    }
                    if (danhsachkhoacheck != "")
                    {
                        danhSachIdKhoa = " and vienphi.departmentgroupid in (" + danhsachkhoacheck.Substring(0, danhsachkhoacheck.Length - 1) + ") ";
                        //MessageBox.Show(danhSachIdKhoa);
                    }
                    else
                    {
                        danhSachIdKhoa = " and vienphi.departmentgroupid=-999 ";
                    }
                }

            }
            catch (Exception)
            {

            }
        }
        private void LayTieuChiBaoCao()
        {
            try
            {
                if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuChiBaoCao = " and vienphi.duyet_ngayduyet_vp ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")
                {
                    tieuChiBaoCao = " and vienphi.duyet_ngayduyet ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LayLoaiBenhAn()
        {
            try
            {
                if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    loaiBenhAn = " and vienphi.loaivienphiid=1 ";
                }
                else if (cbbLoaiBA.Text == "Nội trú")
                {
                    loaiBenhAn = " and vienphi.loaivienphiid=0 ";
                }
                else
                {
                    loaiBenhAn = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LayMaDangKyDungTuyen()
        {
            try
            {
                string sql_madungtuyen = "SELECT mayte, listmaytetuyenduoi FROM hospital;";
                DataView data_madungtuyen = new DataView(condb.GetDataTable(sql_madungtuyen));
                if (data_madungtuyen != null)
                {
                    lstmadungtuyen = new List<long>();
                    lstmadungtuyen.Add(Convert.ToInt64(data_madungtuyen[0]["mayte"].ToString()));
                    maTinh_BV = Convert.ToInt64(data_madungtuyen[0]["mayte"].ToString().Substring(0, 2));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //chay bao cao 
        private void ChayLayBaoCao()
        {
            try
            {
                gridControlBHYT21Chenh.DataSource = null;
                string sql_getvienphi = "select vienphi.vienphiid as vienphiid, vienphi.bhytid as bhytid, bhyt.bhytcode as bhytcode, bhyt.macskcbbd as macskcbbd from vienphi,bhyt where vienphi.bhytid=bhyt.bhytid and vienphi.vienphistatus=2 " + loaiBenhAn + danhSachIdKhoa + tieuChiBaoCao + " >'" + datetungay + "' " + tieuChiBaoCao + "<'" + datedenngay + "';";
                DataView data_vienphi = new DataView(condb.GetDataTable(sql_getvienphi));
                if (data_vienphi != null && data_vienphi.Count > 0)
                {
                    //List<MedicalLink.ClassCommon.classVienPhiBHYT> lstvienphiBhyt_A = new List<ClassCommon.classVienPhiBHYT>();
                    List<long> lstVienPhiId_NhomA = new List<long>();
                    List<long> lstVienPhiId_NhomB = new List<long>();
                    List<long> lstVienPhiId_NhomC = new List<long>();

                    lstDataResult_All = new List<ClassCommon.classBCBHYT21ChenhNew>(); //luu tru ket qua

                    for (int i = 0; i < data_vienphi.Count; i++)
                    {
                        string macskcbbd = data_vienphi[i]["macskcbbd"].ToString();
                        long matinh = Convert.ToInt64(data_vienphi[i]["bhytcode"].ToString().Substring(3, 2));

                        //Nhom A - Noi dang ky KCB ban dau
                        var kiemtradungtuyen = lstmadungtuyen.FirstOrDefault(o => o.Equals(macskcbbd));
                        if (kiemtradungtuyen != null && kiemtradungtuyen != 0)
                        {
                            lstVienPhiId_NhomA.Add(Convert.ToInt64(data_vienphi[i]["vienphiid"].ToString()));
                        }
                        else
                        {
                            //nhom B - Noi tinh den
                            if (matinh == maTinh_BV)
                            {
                                lstVienPhiId_NhomB.Add(Convert.ToInt64(data_vienphi[i]["vienphiid"].ToString()));
                            }
                            //Nhom C - Ngoai tinh den
                            else
                            {
                                lstVienPhiId_NhomC.Add(Convert.ToInt64(data_vienphi[i]["vienphiid"].ToString()));
                            }
                        }

                    }
                    //Kiem tra va duyet tung list VienPhiId cac nhom
                    if (lstVienPhiId_NhomA != null && lstVienPhiId_NhomA.Count > 0) //Nhom A
                    {
                        string dsVienPhiIdString = "";
                        //get danh sách vienphiid thành chuỗi
                        for (int itemVPId = 0; itemVPId < lstVienPhiId_NhomA.Count - 1; itemVPId++)
                        {
                            dsVienPhiIdString += lstVienPhiId_NhomA[itemVPId] + ",";
                        }
                        dsVienPhiIdString += lstVienPhiId_NhomA[lstVienPhiId_NhomA.Count - 1];
                        string sql_serviceprice = "SELECT s.servicepricecode as servicecode, s.servicepricename_bhyt as dvkt_ten, s.bhyt_groupcode as bhyt_groupcode, s.servicepricemoney_bhyt as dongia_hientai, SUM(s.soluong) as soluong, rtrim(left(v.duyet_quyduyet,2), '/') as thang FROM serviceprice s join vienphi v on s.vienphiid=v.vienphiid WHERE v.vienphiid in (" + dsVienPhiIdString + ") and s.loaidoituong in (0,2,4,6) and s.bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG') GROUP BY servicecode,dvkt_ten,dongia_hientai,thang,bhyt_groupcode;";
                        DataView data_servicePrice_A = new DataView(condb.GetDataTable(sql_serviceprice));
                        if (data_servicePrice_A != null && data_servicePrice_A.Count > 0)
                        {
                            List<ClassCommon.classBCBHYT21ChenhNew> lstData_servicePrice_A = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            for (int dv_A = 0; dv_A < data_servicePrice_A.Count; dv_A++)
                            {
                                ClassCommon.classBCBHYT21ChenhNew dataService = new ClassCommon.classBCBHYT21ChenhNew();
                                dataService.stt = dv_A + 1;
                                dataService.servicecode = data_servicePrice_A[dv_A]["servicecode"].ToString();
                                dataService.bhyt_groupcode = data_servicePrice_A[dv_A]["bhyt_groupcode"].ToString();
                                dataService.dvkt_code_cu = "";
                                dataService.dvkt_code_moi = "";
                                dataService.dvkt_ten = data_servicePrice_A[dv_A]["dvkt_ten"].ToString();
                                dataService.soluong = Convert.ToDecimal(data_servicePrice_A[dv_A]["soluong"].ToString());
                                dataService.dongia_cu_1 = 0;
                                dataService.thanhtien_cu_1 = 0;
                                dataService.dongia_hientai = Convert.ToDecimal(data_servicePrice_A[dv_A]["dongia_hientai"].ToString());
                                dataService.thanhtien_hientai = 0;
                                dataService.chenhlech_1 = 0;
                                dataService.ghichu = "";
                                dataService.thang = Convert.ToInt64(data_servicePrice_A[dv_A]["thang"].ToString());
                                dataService.tuyen = "";
                                dataService.loaikcb = "1";
                                dataService.dongia_moi_2 = 0;
                                dataService.thanhtien_moi_2 = 0;
                                dataService.chenhlech_2 = 0;
                                lstData_servicePrice_A.Add(dataService);
                            }
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomA = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomA.dvkt_ten = "BỆNH NHÂN ĐĂNG KÝ KCB BAN ĐẦU (TUYẾN 1)";
                            lstDataResult_All.Add(tieudenhomA);

                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_A_KB = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_A_NG = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_A_DVKT = new List<ClassCommon.classBCBHYT21ChenhNew>();

                            ClassCommon.classBCBHYT21ChenhNew tieudenhomA_KB = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomA_KB.dvkt_ten = "Khám bệnh";
                            tieudenhomA_KB.thanhtien_hientai = 0;
                            tieudenhomA_KB.thanhtien_cu_1 = 0;
                            tieudenhomA_KB.thanhtien_moi_2 = 0;
                            tieudenhomA_KB.chenhlech_1 = 0;
                            tieudenhomA_KB.chenhlech_2 = 0;
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomA_NG = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomA_NG.dvkt_ten = "Ngày giường";
                            tieudenhomA_NG.thanhtien_hientai = 0;
                            tieudenhomA_NG.thanhtien_cu_1 = 0;
                            tieudenhomA_NG.thanhtien_moi_2 = 0;
                            tieudenhomA_NG.chenhlech_1 = 0;
                            tieudenhomA_NG.chenhlech_2 = 0;
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomA_DVKT = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomA_DVKT.dvkt_ten = "Dịch vụ kỹ thuật";
                            tieudenhomA_DVKT.thanhtien_hientai = 0;
                            tieudenhomA_DVKT.thanhtien_cu_1 = 0;
                            tieudenhomA_DVKT.thanhtien_moi_2 = 0;
                            tieudenhomA_DVKT.chenhlech_1 = 0;
                            tieudenhomA_DVKT.chenhlech_2 = 0;

                            foreach (var item_DataService in lstData_servicePrice_A)
                            {
                                if (item_DataService.bhyt_groupcode == "01KB")
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomA_KB);
                                    lstDataResult_A_KB.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomA_KB));
                                }
                                else if (item_DataService.bhyt_groupcode == "12NG")
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomA_NG);
                                    lstDataResult_A_NG.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomA_NG));
                                }
                                else
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomA_DVKT);
                                    lstDataResult_A_DVKT.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomA_DVKT));
                                }
                            }
                            //gan du lieu de trả về
                            lstDataResult_All.Add(tieudenhomA_KB);
                            lstDataResult_All.AddRange(lstDataResult_A_KB);
                            lstDataResult_All.Add(tieudenhomA_NG);
                            lstDataResult_All.AddRange(lstDataResult_A_NG);
                            lstDataResult_All.Add(tieudenhomA_DVKT);
                            lstDataResult_All.AddRange(lstDataResult_A_DVKT);
                        }
                    }
                    if (lstVienPhiId_NhomB != null && lstVienPhiId_NhomB.Count > 0) //Nhom B
                    {
                        string dsVienPhiIdString = "";
                        //get danh sách vienphiid thành chuỗi
                        for (int itemVPId = 0; itemVPId < lstVienPhiId_NhomB.Count - 1; itemVPId++)
                        {
                            dsVienPhiIdString += lstVienPhiId_NhomB[itemVPId] + ",";
                        }
                        dsVienPhiIdString += lstVienPhiId_NhomB[lstVienPhiId_NhomB.Count - 1];
                        string sql_serviceprice = "SELECT s.servicepricecode as servicecode, s.servicepricename_bhyt as dvkt_ten, s.bhyt_groupcode as bhyt_groupcode, s.servicepricemoney_bhyt as dongia_hientai, SUM(s.soluong) as soluong, rtrim(left(v.duyet_quyduyet,2), '/') as thang FROM serviceprice s join vienphi v on s.vienphiid=v.vienphiid WHERE v.vienphiid in (" + dsVienPhiIdString + ") and s.loaidoituong in (0,2,4,6) and s.bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG') GROUP BY servicecode,bhyt_groupcode,dvkt_ten,dongia_hientai,thang;";
                        DataView data_servicePrice_B = new DataView(condb.GetDataTable(sql_serviceprice));
                        if (data_servicePrice_B != null && data_servicePrice_B.Count > 0)
                        {
                            List<ClassCommon.classBCBHYT21ChenhNew> lstData_servicePrice_B = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            for (int dv_B = 0; dv_B < data_servicePrice_B.Count; dv_B++)
                            {
                                ClassCommon.classBCBHYT21ChenhNew dataService = new ClassCommon.classBCBHYT21ChenhNew();
                                dataService.stt = dv_B + 1;
                                dataService.servicecode = data_servicePrice_B[dv_B]["servicecode"].ToString();
                                dataService.bhyt_groupcode = data_servicePrice_B[dv_B]["bhyt_groupcode"].ToString();
                                dataService.dvkt_code_cu = "";
                                dataService.dvkt_code_moi = "";
                                dataService.dvkt_ten = data_servicePrice_B[dv_B]["dvkt_ten"].ToString();
                                dataService.soluong = Convert.ToDecimal(data_servicePrice_B[dv_B]["soluong"].ToString());
                                dataService.dongia_cu_1 = 0;
                                dataService.thanhtien_cu_1 = 0;
                                dataService.dongia_hientai = Convert.ToDecimal(data_servicePrice_B[dv_B]["dongia_hientai"].ToString());
                                dataService.thanhtien_hientai = 0;
                                dataService.chenhlech_1 = 0;
                                dataService.ghichu = "";
                                dataService.thang = Convert.ToInt64(data_servicePrice_B[dv_B]["thang"].ToString());
                                dataService.tuyen = "";
                                dataService.loaikcb = "2";
                                dataService.dongia_moi_2 = 0;
                                dataService.thanhtien_moi_2 = 0;
                                dataService.chenhlech_2 = 0;
                                lstData_servicePrice_B.Add(dataService);
                            }
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomB = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomB.dvkt_ten = "BỆNH NHÂN ĐA TUYẾN ĐẾN NỘI TỈNH (TUYẾN 2)";
                            lstDataResult_All.Add(tieudenhomB);

                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_B_KB = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_B_NG = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_B_DVKT = new List<ClassCommon.classBCBHYT21ChenhNew>();

                            ClassCommon.classBCBHYT21ChenhNew tieudenhomB_KB = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomB_KB.dvkt_ten = "Khám bệnh";
                            tieudenhomB_KB.thanhtien_hientai = 0;
                            tieudenhomB_KB.thanhtien_cu_1 = 0;
                            tieudenhomB_KB.thanhtien_moi_2 = 0;
                            tieudenhomB_KB.chenhlech_1 = 0;
                            tieudenhomB_KB.chenhlech_2 = 0;
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomB_NG = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomB_NG.dvkt_ten = "Ngày giường";
                            tieudenhomB_NG.thanhtien_hientai = 0;
                            tieudenhomB_NG.thanhtien_cu_1 = 0;
                            tieudenhomB_NG.thanhtien_moi_2 = 0;
                            tieudenhomB_NG.chenhlech_1 = 0;
                            tieudenhomB_NG.chenhlech_2 = 0;
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomB_DVKT = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomB_DVKT.dvkt_ten = "Dịch vụ kỹ thuật";
                            tieudenhomB_DVKT.thanhtien_hientai = 0;
                            tieudenhomB_DVKT.thanhtien_cu_1 = 0;
                            tieudenhomB_DVKT.thanhtien_moi_2 = 0;
                            tieudenhomB_DVKT.chenhlech_1 = 0;
                            tieudenhomB_DVKT.chenhlech_2 = 0;

                            foreach (var item_DataService in lstData_servicePrice_B)
                            {
                                if (item_DataService.bhyt_groupcode == "01KB")
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomB_KB);
                                    lstDataResult_B_KB.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomB_KB));
                                }
                                else if (item_DataService.bhyt_groupcode == "12NG")
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomB_NG);
                                    lstDataResult_B_NG.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomB_NG));
                                }
                                else
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomB_DVKT);
                                    lstDataResult_B_DVKT.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomB_DVKT));
                                }
                            }
                            //gan du lieu de trả về
                            lstDataResult_All.Add(tieudenhomB_KB);
                            lstDataResult_All.AddRange(lstDataResult_B_KB);
                            lstDataResult_All.Add(tieudenhomB_NG);
                            lstDataResult_All.AddRange(lstDataResult_B_NG);
                            lstDataResult_All.Add(tieudenhomB_DVKT);
                            lstDataResult_All.AddRange(lstDataResult_B_DVKT);
                        }
                    }
                    if (lstVienPhiId_NhomC != null && lstVienPhiId_NhomC.Count > 0) //Nhom C
                    {
                        string dsVienPhiIdString = "";
                        //get danh sách vienphiid thành chuỗi
                        for (int itemVPId = 0; itemVPId < lstVienPhiId_NhomC.Count - 1; itemVPId++)
                        {
                            dsVienPhiIdString += lstVienPhiId_NhomC[itemVPId] + ",";
                        }
                        dsVienPhiIdString += lstVienPhiId_NhomC[lstVienPhiId_NhomC.Count - 1];
                        string sql_serviceprice = "SELECT s.servicepricecode as servicecode, s.servicepricename_bhyt as dvkt_ten, s.bhyt_groupcode as bhyt_groupcode, s.servicepricemoney_bhyt as dongia_hientai, SUM(s.soluong) as soluong, rtrim(left(v.duyet_quyduyet,2), '/') as thang FROM serviceprice s join vienphi v on s.vienphiid=v.vienphiid WHERE v.vienphiid in (" + dsVienPhiIdString + ") and s.loaidoituong in (0,2,4,6) and s.bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG') GROUP BY servicecode,bhyt_groupcode,dvkt_ten,dongia_hientai,thang;";
                        DataView data_servicePrice_C = new DataView(condb.GetDataTable(sql_serviceprice));
                        if (data_servicePrice_C != null && data_servicePrice_C.Count > 0)
                        {
                            List<ClassCommon.classBCBHYT21ChenhNew> lstData_servicePrice_C = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            for (int dv_C = 0; dv_C < data_servicePrice_C.Count; dv_C++)
                            {
                                ClassCommon.classBCBHYT21ChenhNew dataService = new ClassCommon.classBCBHYT21ChenhNew();
                                dataService.stt = dv_C + 1;
                                dataService.servicecode = data_servicePrice_C[dv_C]["servicecode"].ToString();
                                dataService.bhyt_groupcode = data_servicePrice_C[dv_C]["bhyt_groupcode"].ToString();
                                dataService.dvkt_code_cu = "";
                                dataService.dvkt_code_moi = "";
                                dataService.dvkt_ten = data_servicePrice_C[dv_C]["dvkt_ten"].ToString();
                                dataService.soluong = Convert.ToDecimal(data_servicePrice_C[dv_C]["soluong"].ToString());
                                dataService.dongia_cu_1 = 0;
                                dataService.thanhtien_cu_1 = 0;
                                dataService.dongia_hientai = Convert.ToDecimal(data_servicePrice_C[dv_C]["dongia_hientai"].ToString());
                                dataService.thanhtien_hientai = 0;
                                dataService.chenhlech_1 = 0;
                                dataService.ghichu = "";
                                dataService.thang = Convert.ToInt64(data_servicePrice_C[dv_C]["thang"].ToString());
                                dataService.tuyen = "";
                                dataService.loaikcb = "3";
                                dataService.dongia_moi_2 = 0;
                                dataService.thanhtien_moi_2 = 0;
                                dataService.chenhlech_2 = 0;
                                lstData_servicePrice_C.Add(dataService);
                            }
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomC = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomC.dvkt_ten = "BỆNH NHÂN ĐA TUYẾN ĐẾN NGOẠI TỈNH (TUYẾN 3)";
                            lstDataResult_All.Add(tieudenhomC);

                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_C_KB = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_C_NG = new List<ClassCommon.classBCBHYT21ChenhNew>();
                            List<ClassCommon.classBCBHYT21ChenhNew> lstDataResult_C_DVKT = new List<ClassCommon.classBCBHYT21ChenhNew>();

                            ClassCommon.classBCBHYT21ChenhNew tieudenhomC_KB = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomC_KB.dvkt_ten = "Khám bệnh";
                            tieudenhomC_KB.thanhtien_hientai = 0;
                            tieudenhomC_KB.thanhtien_cu_1 = 0;
                            tieudenhomC_KB.thanhtien_moi_2 = 0;
                            tieudenhomC_KB.chenhlech_1 = 0;
                            tieudenhomC_KB.chenhlech_2 = 0;
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomC_NG = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomC_NG.dvkt_ten = "Ngày giường";
                            tieudenhomC_NG.thanhtien_hientai = 0;
                            tieudenhomC_NG.thanhtien_cu_1 = 0;
                            tieudenhomC_NG.thanhtien_moi_2 = 0;
                            tieudenhomC_NG.chenhlech_1 = 0;
                            tieudenhomC_NG.chenhlech_2 = 0;
                            ClassCommon.classBCBHYT21ChenhNew tieudenhomC_DVKT = new ClassCommon.classBCBHYT21ChenhNew();
                            tieudenhomC_DVKT.dvkt_ten = "Dịch vụ kỹ thuật";
                            tieudenhomC_DVKT.thanhtien_hientai = 0;
                            tieudenhomC_DVKT.thanhtien_cu_1 = 0;
                            tieudenhomC_DVKT.thanhtien_moi_2 = 0;
                            tieudenhomC_DVKT.chenhlech_1 = 0;
                            tieudenhomC_DVKT.chenhlech_2 = 0;

                            foreach (var item_DataService in lstData_servicePrice_C)
                            {
                                if (item_DataService.bhyt_groupcode == "01KB")
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomC_KB);
                                    lstDataResult_C_KB.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomC_KB));
                                }
                                else if (item_DataService.bhyt_groupcode == "12NG")
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomC_NG);
                                    lstDataResult_C_NG.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomC_NG));
                                }
                                else
                                {
                                    //GanDuLieuVaoTungNhom(item_DataService, tieudenhomC_DVKT);
                                    lstDataResult_C_DVKT.Add(GanDuLieuVaoTungNhom(item_DataService, tieudenhomC_DVKT));
                                }
                            }
                            //gan du lieu de trả về
                            lstDataResult_All.Add(tieudenhomC_KB);
                            lstDataResult_All.AddRange(lstDataResult_C_KB);
                            lstDataResult_All.Add(tieudenhomC_NG);
                            lstDataResult_All.AddRange(lstDataResult_C_NG);
                            lstDataResult_All.Add(tieudenhomC_DVKT);
                            lstDataResult_All.AddRange(lstDataResult_C_DVKT);
                        }
                    }
                    gridControlBHYT21Chenh.DataSource = lstDataResult_All;

                }
                else
                {
                    gridControlBHYT21Chenh.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                frmthongbao.Show();
            }
        }

        private ClassCommon.classBCBHYT21ChenhNew GanDuLieuVaoTungNhom(ClassCommon.classBCBHYT21ChenhNew item_DataService, ClassCommon.classBCBHYT21ChenhNew tieudenhomA_NG)
        {
            ClassCommon.classBCBHYT21ChenhNew dataService = new ClassCommon.classBCBHYT21ChenhNew();
            try
            {
                dataService = item_DataService;
                var data_maChenh = lstDVKTBHYTChenh.FirstOrDefault(o => o.servicecode == item_DataService.servicecode);
                dataService.thanhtien_hientai = dataService.soluong * dataService.dongia_hientai;

                if (data_maChenh != null)
                {
                    dataService.dvkt_code_cu = data_maChenh.dvkt_code_cu;
                    dataService.dvkt_code_moi = data_maChenh.dvkt_code_moi;
                    dataService.dongia_cu_1 = data_maChenh.dongia_cu_1;
                    dataService.thanhtien_cu_1 = dataService.soluong * data_maChenh.dongia_cu_1;
                    dataService.chenhlech_1 = dataService.thanhtien_hientai - dataService.thanhtien_cu_1;
                    dataService.dongia_moi_2 = data_maChenh.dongia_moi_2;
                    dataService.thanhtien_moi_2 = dataService.soluong * data_maChenh.dongia_moi_2;
                    dataService.chenhlech_2 = dataService.thanhtien_moi_2 - dataService.thanhtien_cu_1;
                }
                else
                {
                    dataService.thanhtien_cu_1 = dataService.soluong * dataService.dongia_cu_1;
                    dataService.chenhlech_1 = dataService.thanhtien_hientai - dataService.thanhtien_cu_1;
                    dataService.thanhtien_moi_2 = dataService.soluong * dataService.dongia_moi_2;
                    dataService.chenhlech_2 = dataService.thanhtien_moi_2 - dataService.thanhtien_cu_1;
                }
                tieudenhomA_NG.thanhtien_cu_1 += dataService.thanhtien_cu_1;
                tieudenhomA_NG.thanhtien_hientai += dataService.thanhtien_hientai;
                tieudenhomA_NG.chenhlech_1 += dataService.chenhlech_1;
                tieudenhomA_NG.thanhtien_moi_2 += dataService.thanhtien_moi_2;
                tieudenhomA_NG.chenhlech_2 += dataService.chenhlech_2;
            }
            catch (Exception)
            {

                throw;
            }
            return dataService;
        }

    }
}
