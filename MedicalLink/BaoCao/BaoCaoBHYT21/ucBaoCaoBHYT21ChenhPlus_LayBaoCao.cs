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
    public partial class ucBCBHYT21Chenh : UserControl
    {
        private string datetungay = "";
        private string datedenngay = "";
        private string tieuChiBaoCao = "";
        private string loaiBenhAn = "";
        private string danhSachIdKhoa = "";

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
        private void ChayLayBaoCao()
        {
            try
            {
                string sql_bcBHYT21 = "select servicepriceref.bhyt_groupcode as dv_nhom_ma, servicepriceref.servicepricecodeuser as dvktdmbyt_dcg, servicepriceref.servicepricesttuser as dvktstt_dcg, servicepriceref.servicepricecode as dvktma_dcg, servicepriceref.servicepricename as tendvkt_dcg, servicepriceref.servicepricefeebhyt as dongia_dcg, sum(serviceprice.soluong) as soluong_dcg from serviceprice, vienphi, servicepriceref where serviceprice.vienphiid=vienphi.vienphiid and servicepriceref.servicepricecode=serviceprice.servicepricecode and serviceprice.loaidoituong <>1 and vienphi.vienphistatus=2 " + loaiBenhAn + danhSachIdKhoa + tieuChiBaoCao + " >'" + datetungay + "' " + tieuChiBaoCao + "<'" + datedenngay + "' group by dvktma_dcg,tendvkt_dcg,dv_nhom_ma,dvktdmbyt_dcg,dvktstt_dcg,dongia_dcg order by dv_nhom_ma;";
                DataView data_bcBHYT21 = new DataView(condb.GetDataTable_HIS(sql_bcBHYT21));

                if (data_bcBHYT21 != null && data_bcBHYT21.Count > 0)
                {
                    List<MedicalLink.ClassCommon.classBCBHYT21Chenh> lstbhyt21Chenh = new List<ClassCommon.classBCBHYT21Chenh>();

                    for (int i = 0; i < data_bcBHYT21.Count; i++)
                    {
                        var kiemtradichvu = data_bcBHYT21[i]["dv_nhom_ma"].ToString();
                        if (kiemtradichvu == "03XN" || kiemtradichvu == "04CDHA" || kiemtradichvu == "06PTTT" || kiemtradichvu == "12NG" || kiemtradichvu == "01KB" || kiemtradichvu == "07KTC")
                        {
                            MedicalLink.ClassCommon.classBCBHYT21Chenh bhyt21Chenh = new ClassCommon.classBCBHYT21Chenh();
                            var dv_timkiemdcg = lstDVKTBHYTChenh.FirstOrDefault(o => o.MaDVKT_Cu == data_bcBHYT21[i]["dvktma_dcg"].ToString());


                            bhyt21Chenh.stt = (i + 1).ToString();
                            bhyt21Chenh.dv_nhom_ma = data_bcBHYT21[i]["dv_nhom_ma"].ToString();
                            string dv_nhom_ma = data_bcBHYT21[i]["dv_nhom_ma"].ToString();
                            if (dv_nhom_ma == "03XN")
                                bhyt21Chenh.dv_nhom_ten = "Xét nghiệm";
                            else if (dv_nhom_ma == "04CDHA")
                                bhyt21Chenh.dv_nhom_ten = "Chẩn đoán hình ảnh";
                            else if (dv_nhom_ma == "06PTTT")
                                bhyt21Chenh.dv_nhom_ten = "Chuyên khoa";
                            else if (dv_nhom_ma == "12NG")
                                bhyt21Chenh.dv_nhom_ten = "Ngày giường";
                            else if (dv_nhom_ma == "01KB")
                                bhyt21Chenh.dv_nhom_ten = "Khám bệnh";
                            else if (dv_nhom_ma == "07KTC")
                                bhyt21Chenh.dv_nhom_ten = "Dịch vụ kỹ thuật cao";
                            bhyt21Chenh.dvktma_dcg = data_bcBHYT21[i]["dvktma_dcg"].ToString();
                            bhyt21Chenh.dvktdmbyt_dcg = data_bcBHYT21[i]["dvktdmbyt_dcg"].ToString();
                            bhyt21Chenh.dvktstt_dcg = data_bcBHYT21[i]["dvktstt_dcg"].ToString();//
                            bhyt21Chenh.tendvkt_dcg = data_bcBHYT21[i]["tendvkt_dcg"].ToString();
                            bhyt21Chenh.dongia_dcg = data_bcBHYT21[i]["dongia_dcg"].ToString();
                            bhyt21Chenh.soluong_dcg = data_bcBHYT21[i]["soluong_dcg"].ToString();

                            if (dv_timkiemdcg != null)
                            {
                                bhyt21Chenh.dvkt_code = dv_timkiemdcg.MaDVKT_Code;
                                bhyt21Chenh.tendvkt_cu = dv_timkiemdcg.TenDVKT_Moi;
                                bhyt21Chenh.madvkt_cu = dv_timkiemdcg.MaDVKT_Moi;
                                bhyt21Chenh.madvkt_tt37 = dv_timkiemdcg.MaDVKT_Cu;
                                bhyt21Chenh.madvkt_tuongduong = dv_timkiemdcg.MaDVKT_TuongDuong;
                                bhyt21Chenh.dongia_moi = dv_timkiemdcg.DonGia_Moi.ToString();
                                bhyt21Chenh.dongia_chenh = (dv_timkiemdcg.DonGia_Moi - Convert.ToDecimal((data_bcBHYT21[i]["dongia_dcg"] ?? 0).ToString())).ToString();
                                bhyt21Chenh.chiphitangthem = (Convert.ToDecimal((data_bcBHYT21[i]["soluong_dcg"] ?? 0).ToString()) * Convert.ToDecimal(bhyt21Chenh.dongia_chenh ?? "0")).ToString();
                            }
                            lstbhyt21Chenh.Add(bhyt21Chenh);
                        }
                    }
                    gridControlBHYT21Chenh.DataSource = null;
                    gridControlBHYT21Chenh.DataSource = lstbhyt21Chenh;
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


    }
}
