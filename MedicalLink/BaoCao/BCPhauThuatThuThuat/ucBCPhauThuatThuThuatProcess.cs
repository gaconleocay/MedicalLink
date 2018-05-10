﻿using DevExpress.Utils.Menu;
using DevExpress.XtraSplashScreen;
using MedicalLink.Base;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess;
using MedicalLink.DatabaseProcess.FilterDTO;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao
{
    public partial class ucBCPhauThuatThuThuat : UserControl
    {
        #region Lay du lieu
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                EnableAndDisableNutIn();
                this.helper.ClearSelection();
                string sql_laydulieu = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
              //  string tieuchi_date = "";
                string _tieuchi_pttt = "";
                string _tieuchi_ser = "";
                string _tieuchi_vp = "";
                string _departmentid_ser = "";
                string _pttt_loaiid_serf = "";
                string chiachobacsi = "";
                string baocaotungloai = "";
                string _trangthaipttt = "";

                //tieu chi
                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    _tieuchi_vp = " and vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    _tieuchi_pttt = " and phauthuatthuthuatdate between '" + tungay + "' and '" + denngay + "' ";
                }
                else
                {
                    _tieuchi_ser = " and servicepricedate between '" + tungay + "' and '" + denngay + "' ";
                }

                //trang thai
                if (cboTrangThai.Text == "Chưa gửi YC")
                {
                    _trangthaipttt = " and coalesce(duyetpttt_stt,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã gửi YC")
                {
                    _trangthaipttt = " and duyetpttt_stt=1 ";
                }
                else if (cboTrangThai.Text == "Đã tiếp nhận YC")
                {
                    _trangthaipttt = " and duyetpttt_stt=2 ";
                }
                else if (cboTrangThai.Text == "Đã duyệt PTTT")
                {
                    _trangthaipttt = " and duyetpttt_stt=3 ";
                }
                //else if (cboTrangThai.Text == "Đã khóa")
                //{
                //    _trangthaipttt = " and duyetpttt_stt=99 ";
                //}

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001")//phau thuat - lay phong mo phien va phong mo cap cuu: ser.departmentid in (285,34)
                {
                    List<ToolsOtherListDTO> lst_BC_001 = this.lstOtherList.Where(o => o.tools_otherlistcode == "BAOCAO_001").ToList();
                    if (lst_BC_001 != null && lst_BC_001.Count > 0)
                    {
                        _departmentid_ser = " and departmentid in (" + lst_BC_001[0].tools_otherlistvalue + ") ";
                    }
                    else
                    {
                        _departmentid_ser = " and departmentid in (285,34) ";
                    }
                    _pttt_loaiid_serf = " and pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100)) - (A.GAYME_TIEN * (A.TYLE/100)) - (A.PHU1_TIEN * (A.TYLE/100)) - (A.PHU2_TIEN * (A.TYLE/100)) - (A.GIUPVIEC1_TIEN * (A.TYLE/100)) - (A.GIUPVIEC2_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as gayme_tien, pttt.phume2 as moigayme_tenbs, (case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, pttt.phumo2 as phu2_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 else 0 end) * ser.soluong) as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, pttt.phumo4 as giupviec2_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 else 0 end) * ser.soluong) as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002")//phau thuat - lay khoa tai mui hong: ser.departmentid in (122); ser.departmengrouptid in (10)
                {
                    List<ToolsOtherListDTO> lst_BC_002 = this.lstOtherList.Where(o => o.tools_otherlistcode == "BAOCAO_002").ToList();
                    if (lst_BC_002 != null && lst_BC_002.Count > 0)
                    {
                        _departmentid_ser = " and departmentid in (" + lst_BC_002[0].tools_otherlistvalue + ") ";
                    }
                    else
                    {
                        _departmentid_ser = " and departmentid=122 ";
                    }
                    _pttt_loaiid_serf = " and pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, pttt.phumo2 as phu2_tenbs, ((case serf.pttt_loaiid when 2 then 90000 else 0 end) * ser.soluong) as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, pttt.phumo4 as giupviec2_tenbs, ((case serf.pttt_loaiid when 2 then 70000 when 3 then 30000 else 0 end) * ser.soluong) as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003") //phau thuat - lay khoa rang ham mat: ser.departmentid in (116); ser.departmengrouptid in (9)
                {
                    List<ToolsOtherListDTO> lst_BC_003 = this.lstOtherList.Where(o => o.tools_otherlistcode == "BAOCAO_003").ToList();
                    if (lst_BC_003 != null && lst_BC_003.Count > 0)
                    {
                        _departmentid_ser = " and departmentid in (" + lst_BC_003[0].tools_otherlistvalue + ") ";
                    }
                    else
                    {
                        _departmentid_ser = " and departmentid=116 ";
                    }
                    _pttt_loaiid_serf = " and pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004") //phau thuat -Mổ Mắt_B_Điều Trị + phòng khám Mổ mắt+ BDT khoa mắt kkb-khám mắt
                {
                    List<ToolsOtherListDTO> lst_BC_004 = this.lstOtherList.Where(o => o.tools_otherlistcode == "BAOCAO_004").ToList();
                    if (lst_BC_004 != null && lst_BC_004.Count > 0)
                    {
                        _departmentid_ser = " and departmentid in (" + lst_BC_004[0].tools_otherlistvalue + ") ";
                    }
                    else
                    {
                        _departmentid_ser = " and departmentid in (269,335,80,212) ";
                    }
                    _pttt_loaiid_serf = " and pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as gayme_tien, pttt.phume2 as moigayme_tenbs, (case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005") //phau thuat - khoa khac
                {
                    _departmentid_ser = " and departmentid in (";
                    List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        _departmentid_ser += lstPhongCheck[i] + ",";
                    }
                    _departmentid_ser += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";

                    _pttt_loaiid_serf = " and pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as gayme_tien, pttt.phume2 as moigayme_tenbs, (case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, pttt.phumo2 as phu2_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 else 0 end) * ser.soluong) as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, pttt.phumo4 as giupviec2_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 else 0 end) * ser.soluong) as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")// thu thuat - khoa mat + phong kham mat ser.departmentid in (80,212)
                {
                    List<ToolsOtherListDTO> lst_BC_006 = this.lstOtherList.Where(o => o.tools_otherlistcode == "BAOCAO_006").ToList();
                    if (lst_BC_006 != null && lst_BC_006.Count > 0)
                    {
                        _departmentid_ser = " and departmentid in (" + lst_BC_006[0].tools_otherlistvalue + ") ";
                    }
                    else
                    {
                        _departmentid_ser = " and departmentid in (80,212) ";
                    }
                    _pttt_loaiid_serf = " and pttt_loaiid in (5,6,7,8) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    baocaotungloai = "  pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 0 as moimochinh_tenbs, 0 as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, 0 as phu1_tenbs, 0 as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 5 then 36000 when 6 then 21000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt): ser.departmentid not in (80,212)
                {
                    List<ToolsOtherListDTO> lst_BC_006 = this.lstOtherList.Where(o => o.tools_otherlistcode == "BAOCAO_006").ToList();
                    if (lst_BC_006 != null && lst_BC_006.Count > 0)
                    {
                        _departmentid_ser = " and departmentid not in (" + lst_BC_006[0].tools_otherlistvalue + ") ";
                    }
                    else
                    {
                        _departmentid_ser = " and departmentid not in (80,212) ";
                    }
                    _pttt_loaiid_serf = " and pttt_loaiid in (5,6,7,8) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 0 as moimochinh_tenbs, 0 as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 5 then 36000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//Báo cáo Thủ thuật - khoa khac
                {
                    _departmentid_ser = " and departmentid in (";
                    List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        _departmentid_ser += lstPhongCheck[i] + ",";
                    }
                    _departmentid_ser += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";
                    _pttt_loaiid_serf = " and pttt_loaiid in (5,6,7,8) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 0 as moimochinh_tenbs, 0 as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 5 then 36000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }

                if (chkChuaPhanLoaiPTTT.Checked)
                {
                    _pttt_loaiid_serf = " and pttt_loaiid=0";
                }
                //ngay 10/5/2018
                sql_laydulieu = @" SELECT row_number () over (order by A.ngay_chidinh) as stt, A.servicepriceid, coalesce(A.duyetpttt_stt,0) as duyetpttt_stt, (case A.duyetpttt_stt when 1 then 'Đã gửi YC' when 2 then 'Đã tiếp nhận YC' when 3 then 'Đã duyệt PTTT' when 99 then 'Đã khóa' else 'Chưa gửi YC' end) as duyetpttt_sttname, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_date end) as duyetpttt_date, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_usercode end) as duyetpttt_usercode, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_username end) as duyetpttt_username, A.patientid, A.vienphiid, A.maubenhphamid, A.bhyt_groupcode, hsbA.patientname, (case when hsbA.gioitinhcode='01' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nam, (case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, hsba.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, A.ngay_chidinh, A.ngay_thuchien, (case when A.ngay_ketthuc<>'0001-01-01 00:00:00' then A.ngay_ketthuc end) as ngay_ketthuc, kcd.departmentgroupname as khoachuyenden, krv.departmentgroupname as khoaravien, mbp.chandoan as cd_chidinh, A.servicepricecode, A.servicepricename, A.loaipttt_db, A.loaipttt_l1, A.loaipttt_l2, A.loaipttt_l3, A.loaipttt, A.soluong, A.servicepricefee, A.tyle, round(cast(A.thuoc_tronggoi as numeric),0) as thuoc_tronggoi, round(cast(A.vattu_tronggoi as numeric),0) as vattu_tronggoi, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, mc.username as mochinh_tenbs, (case when coalesce(mc.username,'CX')<>'CX' then (A.mochinh_tien * (A.tyle/100)) else 0 end) as mochinh_tien, mmc.username as moimochinh_tenbs, (case when coalesce(mmc.username,'CX')<>'CX' then (A.moimochinh_tien * (A.tyle/100)) else 0 end) as moimochinh_tien, gm.username as gayme_tenbs, (case when coalesce(gm.username,'CX')<>'CX' then (A.gayme_tien * (A.tyle/100)) else 0 end) as gayme_tien, mgm.username as moigayme_tenbs, (case when coalesce(mgm.username,'CX')<>'CX' then (A.moigayme_tien * (A.tyle/100)) else 0 end) as moigayme_tien, p1.username as phu1_tenbs, (case when coalesce(p1.username,'CX')<>'CX' then (A.phu1_tien * (A.tyle/100)) else 0 end) as phu1_tien, p2.username as phu2_tenbs, (case when coalesce(p2.username,'CX')<>'CX' then (A.phu2_tien * (A.tyle/100)) else 0 end) as phu2_tien, gv1.username as giupviec1_tenbs, (case when coalesce(gv1.username,'CX')<>'CX' then (A.giupviec1_tien * (A.tyle/100)) else 0 end) as giupviec1_tien, gv2.username as giupviec2_tenbs, (case when coalesce(gv2.username,'CX')<>'CX' then (A.giupviec2_tien * (A.tyle/100)) else 0 end) as giupviec2_tien, A.ngay_vaovien, A.ngay_ravien, A.ngay_thanhtoan, nnth.username as nguoinhapthuchien FROM (SELECT vp.patientid, vp.vienphiid, vp.hosobenhanid, vp.bhytid, ser.servicepriceid, ser.duyetpttt_stt, ser.duyetpttt_date, ser.duyetpttt_usercode, ser.duyetpttt_username, ser.maubenhphamid, ser.bhyt_groupcode, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as ngay_chidinh, pttt.phauthuatthuthuatdate as ngay_thuchien, pttt.phauthuatthuthuatdate_ketthuc as ngay_ketthuc, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, ser.servicepricecode, (case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, (case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) end) as thuoc_tronggoi, (case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle') and COALESCE(servicepriceid_thanhtoanrieng,0)=0) end) as vattu_tronggoi, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, ser.soluong as soluong, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, " + baocaotungloai + " vp.vienphidate as ngay_vaovien, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan, pttt.userid_gmhs as nguoinhapthuchien FROM (select * from serviceprice where bhyt_groupcode in ('06PTTT','07KTC') " + _trangthaipttt + _tieuchi_ser + _departmentid_ser + " ) ser left join (select (row_number() OVER (PARTITION BY servicepriceid ORDER BY servicepriceid desc)) as stt,* from phauthuatthuthuat) pttt on pttt.servicepriceid=ser.servicepriceid inner join (select patientid, vienphiid, hosobenhanid, bhytid, vienphistatus, departmentgroupid, vienphidate, vienphidate_ravien, vienphistatus_vp, duyet_ngayduyet_vp from vienphi) vp on vp.vienphiid=ser.vienphiid inner join (select servicepricecode, tinhtoanlaigiadvktc, pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') " + _pttt_loaiid_serf + ") serf on serf.servicepricecode=ser.servicepricecode WHERE pttt.stt=1 " + _tieuchi_pttt + ") A INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsbA.hosobenhanid=A.hosobenhanid INNER JOIN (select maubenhphamid, chandoan from maubenhpham) mbp on mbp.maubenhphamid=A.maubenhphamid LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN nhompersonnel mc ON mc.userhisid=A.mochinh_tenbs LEFT JOIN nhompersonnel mmc ON mmc.userhisid=A.moimochinh_tenbs LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_tenbs LEFT JOIN nhompersonnel mgm ON mgm.userhisid=A.moigayme_tenbs LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_tenbs LEFT JOIN nhompersonnel p2 ON p2.userhisid=A.PHU2_TENBS LEFT JOIN nhompersonnel gv1 ON gv1.userhisid=A.giupviec1_tenbs LEFT JOIN nhompersonnel gv2 ON gv2.userhisid=A.giupviec2_tenbs LEFT JOIN nhompersonnel nnth ON nnth.userhisid=A.nguoinhapthuchien; ";

                dataBCPTTT = condb.GetDataTable_HIS(sql_laydulieu);
                if (dataBCPTTT != null && dataBCPTTT.Rows.Count > 0)
                {
                    gridControlDataBCPTTT.DataSource = dataBCPTTT;
                }
                else
                {
                    gridControlDataBCPTTT.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Duyet va go duyet - BỎ
        private void DuyetPTTTDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstDuyetPTTT_User = lstServicepriceids.Where(o => o.duyetpttt_stt == 0).ToList();
                    if (_lstDuyetPTTT_User != null && _lstDuyetPTTT_User.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=1, duyetpttt_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstDuyetPTTT_User) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            MessageBox.Show("Duyệt PTTT thành công SL=" + _lstDuyetPTTT_User.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_DA_DUOC_DUYET_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void GoDuyetPTTTDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstGoDuyetPTTT_User = lstServicepriceids.Where(o => o.duyetpttt_stt == 1 && o.duyetpttt_usercode == Base.SessionLogin.SessionUsercode).ToList();
                    if (_lstGoDuyetPTTT_User != null && _lstGoDuyetPTTT_User.Count > 0)
                    {
                        string _sqlUpdate_GoDuyet = "UPDATE serviceprice SET duyetpttt_stt=0, duyetpttt_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstGoDuyetPTTT_User) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_GoDuyet))
                        {
                            MessageBox.Show("Gỡ duyệt PTTT thành công SL=" + _lstGoDuyetPTTT_User.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_CHUA_DUOC_DUYET_HOAC_NGUOI_KHAC_DUYET);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Process Get ID
        private List<ServicepriceDuyetPTTTDTO> GetIdCollection()
        {
            List<ServicepriceDuyetPTTTDTO> IDs = new List<ServicepriceDuyetPTTTDTO>();
            for (int i = 0; i < helper.SelectedCount; i++)
            {
                ServicepriceDuyetPTTTDTO _serviceID = new ServicepriceDuyetPTTTDTO();
                _serviceID.servicepriceid = (helper.GetSelectedRow(i) as DataRowView)["servicepriceid"].ToString();
                _serviceID.duyetpttt_stt = Utilities.Util_TypeConvertParse.ToInt16((helper.GetSelectedRow(i) as DataRowView)["duyetpttt_stt"].ToString());
                _serviceID.vienphiid = Utilities.Util_TypeConvertParse.ToInt64((helper.GetSelectedRow(i) as DataRowView)["vienphiid"].ToString());
                _serviceID.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64((helper.GetSelectedRow(i) as DataRowView)["maubenhphamid"].ToString());
                _serviceID.bhyt_groupcode = (helper.GetSelectedRow(i) as DataRowView)["bhyt_groupcode"].ToString();
                IDs.Add(_serviceID);
            }
            return IDs;
        }

        private string ConvertListObjToListString(List<ServicepriceDuyetPTTTDTO> IDs)
        {
            string query = "";
            try
            {
                int number = 0;
                foreach (var item in IDs)
                {
                    if (number > 0)
                    {
                        query = query + "," + "'" + item.servicepriceid + "'";
                    }
                    else
                    {
                        query = "'" + item.servicepriceid + "'";
                    }
                    number++;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return query;
        }

        //private string ConvertCollectionToString(Collection<string> IDs)
        //{
        //    string list = "";
        //    bool firstPass = true;
        //    foreach (string x in IDs)
        //    {
        //        if (firstPass)
        //        {
        //            firstPass = false;
        //        }
        //        else
        //        {
        //            list += ",";
        //        }
        //        list += x;
        //    }

        //    int number = 0;
        //    string query = "";
        //    foreach (string item in list.Split(new char[] { ',' }))
        //    {
        //        if (number > 0)
        //        {
        //            query = query + "," + "'" + item + "'";
        //        }
        //        else
        //        {
        //            query = "'" + item + "'";
        //        }
        //        number++;
        //    }

        //    return query;
        //}

        #endregion

        #region Hien Thi nut In xuat Excel
        private void EnableAndDisableNutIn()
        {
            try
            {
                if (cboTrangThai.Text == "Đã duyệt PTTT" || CheckPermission.ChkPerModule("SYS_05") || CheckPermission.ChkPerModule("THAOTAC_06"))
                {
                    dropDownPrint.Enabled = true;
                }
                else
                {
                    dropDownPrint.Enabled = false;
                }

                if (CheckPermission.ChkPerModule("SYS_05") || (CheckPermission.ChkPerModule("THAOTAC_07") && cboTrangThai.Text == "Đã duyệt PTTT") || CheckPermission.ChkPerModule("THAOTAC_06"))
                {
                    dropDownExport.Enabled = true;
                }
                else
                {
                    dropDownExport.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Process - Cập nhật tools_duyet_pttt
        private void CapNhatTools_DuyetPTTT(List<ServicepriceDuyetPTTTDTO> lstServicepriceids, int _duyetpttt_stt)
        {
            try
            {
                string _sqlCapNhatToolsDuyet = "";

                foreach (var item_ser in lstServicepriceids)
                {
                    string _sqlKTDaInsert = "SELECT * FROM tools_duyet_pttt WHERE servicepriceid='" + item_ser.servicepriceid + "'; ";
                    DataTable _DSDVInsert = condb.GetDataTable_HIS(_sqlKTDaInsert);
                    if (_DSDVInsert != null && _DSDVInsert.Rows.Count > 0) //cap nhat
                    {
                        string _update_user = "";
                        switch (_duyetpttt_stt)
                        {
                            case 1:
                                {
                                    _update_user = " , gui_usercode='" + Base.SessionLogin.SessionUsercode + "', gui_username='" + Base.SessionLogin.SessionUsername + "', gui_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 2:
                                {
                                    _update_user = " , tiepnhan_usercode='" + Base.SessionLogin.SessionUsercode + "', tiepnhan_username='" + Base.SessionLogin.SessionUsername + "', tiepnhan_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 3:
                                {
                                    _update_user = " , duyet_usercode='" + Base.SessionLogin.SessionUsercode + "', duyet_username='" + Base.SessionLogin.SessionUsername + "', duyet_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            //case 99:
                            //    {
                            //        _update_user = " , khoa_usercode='" + Base.SessionLogin.SessionUsercode + "', khoa_username='" + Base.SessionLogin.SessionUsername + "', khoa_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                            //        break;
                            //    }
                            default:
                                break;
                        }
                        _sqlCapNhatToolsDuyet += " UPDATE tools_duyet_pttt SET duyetpttt_stt='" + _duyetpttt_stt + "' " + _update_user + " WHERE servicepriceid='" + item_ser.servicepriceid + "'; ";
                    }
                    else //them moi
                    {
                        switch (_duyetpttt_stt)
                        {
                            case 1:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,gui_usercode,gui_username,gui_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 2:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,tiepnhan_usercode,tiepnhan_username,tiepnhan_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 3:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,duyet_usercode,duyet_username,duyet_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "''" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            //case 99:
                            //    {
                            //        _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,khoa_usercode,khoa_username,khoa_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            //        break;
                            //    }
                            default:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "'); ";
                                }
                                break;
                        }
                    }
                }
                condb.ExecuteNonQuery_HIS(_sqlCapNhatToolsDuyet);
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private string GetIdKhoaPhongTheoLoaiBC()
        {
            string result = " and departmentgroupid in (0) ";
            try
            {
                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001")//phau thuat khoa Gay me hoi tinh- lay phong mo phien va phong mo cap cuu: ser.departmentid in (285,34)
                {
                    result = " and departmentgroupid=21 ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002")//phau thuat - lay khoa tai mui hong: ser.departmentid in (122); ser.departmengrouptid in (10)
                {
                    result = " and departmentgroupid=10 ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003") //phau thuat - lay khoa rang ham mat: ser.departmentid in (116); ser.departmengrouptid in (9)
                {
                    result = " and departmentgroupid=9 ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004") //phau thuat -Mổ Mắt_B_Điều Trị + phòng khám Mổ mắt+ BDT khoa mắt kkb-khám mắt
                {
                    result = " and departmentgroupid=26 ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005") //phau thuat - khoa khac
                {
                    result = " and departmentgroupid in (";
                    List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                    {
                        result += lstKhoaCheck[i] + ",";
                    }
                    result += lstKhoaCheck[lstKhoaCheck.Count - 1] + ") ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")// thu thuat - khoa mat + phong kham mat ser.departmentid in (80,212)
                {
                    result = " and departmentgroupid=26 ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt): ser.departmentid not in (80,212)
                {
                    result = " and departmentgroupid<>26 ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//Báo cáo Thủ thuật - khoa khac
                {
                    result = " and departmentgroupid in (";
                    List<Object> lstKhoaCheck = chkcomboListDSKhoa.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstKhoaCheck.Count - 1; i++)
                    {
                        result += lstKhoaCheck[i] + ",";
                    }
                    result += lstKhoaCheck[lstKhoaCheck.Count - 1] + ") ";
                }

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }


        /// <summary>
        /// true:da khoa
        /// </summary>
        /// <param name="_lstDepartmentgroupId"></param>
        /// <returns>true:da khoa</returns>
        private bool KiemTraTrangThaiKhoaGuiPTTT(string _lstDepartmentgroupId)
        {
            bool result = false;
            try
            {
                string _sqlKiemtra = "SELECT departmentgroupid FROM tools_departmentgroup WHERE pttt_khoaguiyc=1 " + _lstDepartmentgroupId + " ; ";
                DataTable _dataTrangThai = condb.GetDataTable_MeL(_sqlKiemtra);
                if (_dataTrangThai != null && _dataTrangThai.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }
        #endregion


    }
}
