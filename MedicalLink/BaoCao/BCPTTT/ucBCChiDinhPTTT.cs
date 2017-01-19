using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.ChucNang
{
    public partial class ucBCChiDinhPTTT : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string danhSachIdKhoa = "";
        private List<MedicalLink.ClassCommon.classBCChiDinhPTTT_User> lstUser { get; set; }

        public ucBCChiDinhPTTT()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaDV.Text.Length == 0)
            {
                mmeMaDV.Text = "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)";
                mmeMaDV.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaDV.Text == "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)")
            {
                mmeMaDV.Text = "";
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
        }

        private void LayDanhSachLocTheoKhoa()
        {
            try
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
                    danhSachIdKhoa = " and serviceprice.departmentgroupid in (" + danhsachkhoacheck.Substring(0, danhsachkhoacheck.Length - 1) + ") ";
                }
                else
                {
                    danhSachIdKhoa = " ";
                }
            }
            catch (Exception)
            {

            }
        }
        private void LayDanhSachLocTheoPhong()
        {
            try
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
                    danhSachIdKhoa = " and serviceprice.departmentid in (" + danhsachkhoacheck.Substring(0, danhsachkhoacheck.Length - 1) + ") ";
                }
                else
                {
                    danhSachIdKhoa = " ";
                }
            }
            catch (Exception)
            {

            }
        }

        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            gridControlDSDV.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string[] dsnhomdv_temp;
            string dsnhomdv = "";
            string tieuchi, loaivienphiid, doituongbenhnhanid;
            string datetungay = "";
            string datedenngay = "";

            if ((mmeMaDV.Text == "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)") || (cbbTieuChi.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
            {
                Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
            }
            else
            {
                LayDanhSachNguoiDung();
                //LayDanhSachLocTheoKhoa();
                if (cbbLoaiBA.Text == "Nội trú")
                {
                    LayDanhSachLocTheoKhoa();
                }
                else if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    LayDanhSachLocTheoPhong();
                }
                else
                {
                    danhSachIdKhoa = " ";
                }
                // Lấy dữ liệu danh sách dịch vụ nhập vào
                dsnhomdv_temp = mmeMaDV.Text.Split(',');
                for (int i = 0; i < dsnhomdv_temp.Length - 1; i++)
                {
                    dsnhomdv += "'" + dsnhomdv_temp[i].ToString() + "',";
                }
                dsnhomdv += "'" + dsnhomdv_temp[dsnhomdv_temp.Length - 1].ToString() + "'";

                // Lấy Tiêu chí thời gian: tieuchi
                if (cbbTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi = "and serviceprice.servicepricedate";
                }
                else if (cbbTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi = "and vienphi.vienphidate";
                }
                else if (cbbTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi = "and vienphi.vienphidate_ravien";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt VP")
                {
                    tieuchi = "and vienphi.duyet_ngayduyet_vp ";
                }
                else if (cbbTieuChi.Text == "Theo ngày duyệt BHYT")
                {
                    tieuchi = "and vienphi.duyet_ngayduyet ";
                }
                else
                {
                    tieuchi = " ";
                }

                // Lấy loaivienphiid
                if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    loaivienphiid = "and vienphi.loaivienphiid=1 ";
                }
                else if (cbbLoaiBA.Text == "Nội trú")
                {
                    loaivienphiid = "and vienphi.loaivienphiid=0 ";
                }
                else
                {
                    loaivienphiid = " ";
                }

                // Lấy trường đối tượng BN loaidoituong
                if (chkBHYT.Checked == true && chkVP.Checked == false)
                {
                    doituongbenhnhanid = "and vienphi.doituongbenhnhanid=1 ";
                }
                else if (chkBHYT.Checked == false && chkVP.Checked == true)
                {
                    doituongbenhnhanid = "and vienphi.doituongbenhnhanid<>1 ";
                }
                else if (chkBHYT.Checked == true && chkVP.Checked == true)
                {
                    doituongbenhnhanid = " ";
                }
                else
                    doituongbenhnhanid = " ";

                // Lấy từ ngày, đến ngày
                datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                //  Lấy xong dữ liệu các trường chọn
                // Thực thi câu lệnh SQL
                try
                {
                    string sqlquerry = ""; //TODO cau lenh SQL

                    if (chkPTTTAll.Checked)
                    {
                        sqlquerry = "SELECT serviceprice.servicepriceid as servicepriceid, vienphi.patientid as mabn,vienphi.vienphiid as mavp, maubenhpham.maubenhphamid as maubenhphamid, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoaravien, department.departmentname as tenphongravien, serviceprice.servicepricecode as madv, serviceprice.servicepricename as tendv, serviceprice.servicepricemoney as dongia, serviceprice.servicepricemoney_bhyt as dongiabhyt, serviceprice.soluong as soluong, serviceprice.soluong * serviceprice.servicepricemoney as thanhtien, serviceprice.soluong * serviceprice.servicepricemoney_bhyt as thanhtienbhyt, serviceprice.servicepricedate as thoigianchidinh, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, vienphi.vienphidate as thoigianvaovien, vienphi.vienphidate_ravien as thoigianravien, vienphi.duyet_ngayduyet_vp as thoigianduyetvp, vienphi.duyet_ngayduyet as thoigianduyetbh, maubenhpham.usercreate as mauserchidinh, maubenhpham.userid as iduserchidinh, servicepriceref.servicepricegroupcode as manhomdichvu, serviceprice.billid_thutien as billid_thutien, serviceprice.billid_clbh_thutien as billid_clbh_thutien, bhyt.bhytcode as sothebhyt, servicepriceref.bhyt_groupcode as bhyt_groupcode, servicepriceref.servicegrouptype as servicegrouptype, serviceprice.chiphidauvao as chiphidauvao, serviceprice.chiphimaymoc as chiphimaymoc, serviceprice.chiphipttt as chiphipttt, mayyte.mayytename as mayytename, mayyte.chiphiliendoanh as chiphiliendoanh FROM serviceprice INNER JOIN vienphi ON serviceprice.vienphiid=vienphi.vienphiid INNER JOIN hosobenhan ON vienphi.hosobenhanid=hosobenhan.hosobenhanid INNER JOIN departmentgroup ON vienphi.departmentgroupid=departmentgroup.departmentgroupid INNER JOIN department ON vienphi.departmentid=department.departmentid INNER JOIN tools_depatment ON serviceprice.departmentid=tools_depatment.departmentid INNER JOIN maubenhpham ON maubenhpham.maubenhphamid=serviceprice.maubenhphamid INNER JOIN servicepriceref ON serviceprice.servicepricecode=servicepriceref.servicepricecode LEFT JOIN bhyt ON vienphi.bhytid=bhyt.bhytid LEFT JOIN mayyte ON serviceprice.mayytedbid=mayyte.mayytedbid WHERE servicepriceref.bhyt_groupcode in ('06PTTT','07KTC') " + tieuchi + " > '" + datetungay + "' " + tieuchi + " < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + " ORDER BY servicepriceref.servicepricegroupcode;";
                    }
                    else
                    {
                        sqlquerry = "SELECT serviceprice.servicepriceid as servicepriceid, vienphi.patientid as mabn,vienphi.vienphiid as mavp, maubenhpham.maubenhphamid as maubenhphamid, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoaravien, department.departmentname as tenphongravien, serviceprice.servicepricecode as madv, serviceprice.servicepricename as tendv, serviceprice.servicepricemoney as dongia, serviceprice.servicepricemoney_bhyt as dongiabhyt, serviceprice.soluong as soluong, serviceprice.soluong * serviceprice.servicepricemoney as thanhtien, serviceprice.soluong * serviceprice.servicepricemoney_bhyt as thanhtienbhyt, serviceprice.servicepricedate as thoigianchidinh, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, vienphi.vienphidate as thoigianvaovien, vienphi.vienphidate_ravien as thoigianravien, vienphi.duyet_ngayduyet_vp as thoigianduyetvp, vienphi.duyet_ngayduyet as thoigianduyetbh, maubenhpham.usercreate as mauserchidinh, maubenhpham.userid as iduserchidinh, servicepriceref.servicepricegroupcode as manhomdichvu, serviceprice.billid_thutien as billid_thutien, serviceprice.billid_clbh_thutien as billid_clbh_thutien, bhyt.bhytcode as sothebhyt, servicepriceref.bhyt_groupcode as bhyt_groupcode, servicepriceref.servicegrouptype as servicegrouptype, serviceprice.chiphidauvao as chiphidauvao, serviceprice.chiphimaymoc as chiphimaymoc, serviceprice.chiphipttt as chiphipttt, mayyte.mayytename as mayytename, mayyte.chiphiliendoanh as chiphiliendoanh FROM serviceprice INNER JOIN vienphi ON serviceprice.vienphiid=vienphi.vienphiid INNER JOIN hosobenhan ON vienphi.hosobenhanid=hosobenhan.hosobenhanid INNER JOIN departmentgroup ON vienphi.departmentgroupid=departmentgroup.departmentgroupid INNER JOIN department ON vienphi.departmentid=department.departmentid INNER JOIN tools_depatment ON serviceprice.departmentid=tools_depatment.departmentid INNER JOIN maubenhpham ON maubenhpham.maubenhphamid=serviceprice.maubenhphamid INNER JOIN servicepriceref ON serviceprice.servicepricecode=servicepriceref.servicepricecode LEFT JOIN bhyt ON vienphi.bhytid=bhyt.bhytid LEFT JOIN mayyte ON serviceprice.mayytedbid=mayyte.mayytedbid WHERE servicepriceref.servicepricegroupcode in (" + dsnhomdv + ") " + tieuchi + " > '" + datetungay + "' " + tieuchi + " < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + danhSachIdKhoa + " order by servicepriceref.servicepricegroupcode;";
                    }

                    DataView data_BCChiDinhPTTT = new DataView(condb.getDataTable(sqlquerry));
                    //gridControlDSDV.DataSource = data_BCChiDinhPTTT;
                    if (data_BCChiDinhPTTT != null && data_BCChiDinhPTTT.Count > 0)
                    {
                        List<MedicalLink.ClassCommon.classBCChiDinhPTTT> lstBCChiDinhPTTT = new List<ClassCommon.classBCChiDinhPTTT>();
                        for (int i = 0; i < data_BCChiDinhPTTT.Count; i++)
                        {
                            MedicalLink.ClassCommon.classBCChiDinhPTTT chidinhPTTT = new ClassCommon.classBCChiDinhPTTT();
                            chidinhPTTT.serviceprice_id = Convert.ToInt64(data_BCChiDinhPTTT[i]["servicepriceid"].ToString());
                            chidinhPTTT.mabn = data_BCChiDinhPTTT[i]["mabn"].ToString();
                            chidinhPTTT.mavp = data_BCChiDinhPTTT[i]["mavp"].ToString();
                            chidinhPTTT.maubenhphamid = data_BCChiDinhPTTT[i]["maubenhphamid"].ToString();
                            chidinhPTTT.sothebhyt = data_BCChiDinhPTTT[i]["sothebhyt"].ToString();
                            chidinhPTTT.tenbn = data_BCChiDinhPTTT[i]["tenbn"].ToString();
                            chidinhPTTT.tenkhoaravien = data_BCChiDinhPTTT[i]["tenkhoaravien"].ToString();
                            chidinhPTTT.tenphongravien = data_BCChiDinhPTTT[i]["tenphongravien"].ToString();
                            chidinhPTTT.manhomdichvu = data_BCChiDinhPTTT[i]["manhomdichvu"].ToString();
                            chidinhPTTT.madv = data_BCChiDinhPTTT[i]["madv"].ToString();
                            chidinhPTTT.tendv = data_BCChiDinhPTTT[i]["tendv"].ToString();
                            chidinhPTTT.dongia = data_BCChiDinhPTTT[i]["dongia"].ToString();
                            chidinhPTTT.dongiabhyt = data_BCChiDinhPTTT[i]["dongiabhyt"].ToString();
                            chidinhPTTT.soluong = data_BCChiDinhPTTT[i]["soluong"].ToString();
                            chidinhPTTT.thanhtien = data_BCChiDinhPTTT[i]["thanhtien"].ToString();
                            chidinhPTTT.thanhtienbhyt = data_BCChiDinhPTTT[i]["thanhtienbhyt"].ToString();
                            chidinhPTTT.thoigianchidinh = data_BCChiDinhPTTT[i]["thoigianchidinh"].ToString();
                            chidinhPTTT.khoachidinh = data_BCChiDinhPTTT[i]["khoachidinh"].ToString();
                            chidinhPTTT.phongchidinh = data_BCChiDinhPTTT[i]["phongchidinh"].ToString();
                            chidinhPTTT.thoigianvaovien = data_BCChiDinhPTTT[i]["thoigianvaovien"].ToString();
                            chidinhPTTT.thoigianravien = data_BCChiDinhPTTT[i]["thoigianravien"].ToString();
                            chidinhPTTT.thoigianduyetvp = data_BCChiDinhPTTT[i]["thoigianduyetvp"].ToString();
                            //chidinhPTTT.thuocdikem = data_BCChiDinhPTTT[i]["thuocdikem"].ToString();
                            //chidinhPTTT.vattudikem = data_BCChiDinhPTTT[i]["vattudikem"].ToString();
                            chidinhPTTT.iduserchidinh = Convert.ToInt64(data_BCChiDinhPTTT[i]["iduserchidinh"].ToString());
                            chidinhPTTT.chiphidauvao = data_BCChiDinhPTTT[i]["chiphidauvao"].ToString();
                            chidinhPTTT.chiphimaymoc = data_BCChiDinhPTTT[i]["chiphimaymoc"].ToString();
                            chidinhPTTT.chiphipttt = data_BCChiDinhPTTT[i]["chiphipttt"].ToString();
                            chidinhPTTT.mayytename = data_BCChiDinhPTTT[i]["mayytename"].ToString();
                            chidinhPTTT.chiphiliendoanh = data_BCChiDinhPTTT[i]["chiphiliendoanh"].ToString();

                            if (Convert.ToInt64(data_BCChiDinhPTTT[i]["billid_thutien"].ToString()) != 0)
                            {
                                chidinhPTTT.trangthaithutien = "Đã thu tiền";
                            }
                            else if (Convert.ToInt64(data_BCChiDinhPTTT[i]["billid_clbh_thutien"].ToString()) != 0)
                            {
                                chidinhPTTT.trangthaithutien = "Đã thu tiền";
                            }
                            else
                            {
                                chidinhPTTT.trangthaithutien = "";
                            }

                            //ten nguoi chi dinh
                            if (lstUser != null && lstUser.Count > 0)
                            {
                                var user_chidinh = lstUser.FirstOrDefault(o => o.userid == chidinhPTTT.iduserchidinh);
                                if (user_chidinh != null)
                                {
                                    chidinhPTTT.mauserchidinh = MedicalLink.Base.EncryptAndDecrypt.Decrypt(user_chidinh.usercode, true);
                                    chidinhPTTT.tenuserchidinh = MedicalLink.Base.EncryptAndDecrypt.Decrypt(user_chidinh.username, true);
                                }
                            }
                            else
                            {
                                chidinhPTTT.mauserchidinh = data_BCChiDinhPTTT[i]["mauserchidinh"].ToString();
                                chidinhPTTT.tenuserchidinh = "";
                            }

                            //Thuoc di kem

                            string sql_thuocvattudikem = "SELECT serviceprice.servicepricemoney, serviceprice.soluong, serviceprice.bhyt_groupcode from serviceprice WHERE serviceprice.servicepriceid_master=" + chidinhPTTT.serviceprice_id + ";";
                            DataView data_thuocvattudikem = new DataView(condb.getDataTable(sql_thuocvattudikem));
                            if (data_thuocvattudikem != null && data_thuocvattudikem.Count > 0)
                            {
                                try
                                {
                                    for (int data_tvt = 0; data_tvt < data_thuocvattudikem.Count; data_tvt++)
                                    {
                                        string bhyt_groupcode = data_thuocvattudikem[data_tvt]["bhyt_groupcode"].ToString();
                                        if (bhyt_groupcode == "09TDT" || bhyt_groupcode == "091TDTtrongDM" || bhyt_groupcode == "092TDTngoaiDM" || bhyt_groupcode == "093TDTUngthu")//thuoc
                                        {
                                            chidinhPTTT.thuocdikem += Convert.ToDecimal(data_thuocvattudikem[data_tvt]["servicepricemoney"].ToString()) * Convert.ToDecimal(data_thuocvattudikem[data_tvt]["soluong"].ToString());
                                        }
                                        else
                                        {
                                            chidinhPTTT.vattudikem += Convert.ToDecimal(data_thuocvattudikem[data_tvt]["servicepricemoney"].ToString()) * Convert.ToDecimal(data_thuocvattudikem[data_tvt]["soluong"].ToString());
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            else
                            {
                                chidinhPTTT.thuocdikem = 0;
                                chidinhPTTT.vattudikem = 0;
                            }
                            //nhóm BHYT dich vu

                            switch (data_BCChiDinhPTTT[i]["bhyt_groupcode"].ToString())
                            {
                                case "01KB":
                                    chidinhPTTT.bhyt_groupcode = "Khám bệnh";
                                    break;
                                case "03XN":
                                    chidinhPTTT.bhyt_groupcode = "Xét nghiệm";
                                    break;
                                case "04CDHA":
                                case "05TDCN":
                                    chidinhPTTT.bhyt_groupcode = "CĐHA";
                                    break;
                                case "06PTTT":
                                    chidinhPTTT.bhyt_groupcode = "PTTT";
                                    break;
                                case "07KTC":
                                    //chidinhPTTT.bhyt_groupcode = "DV KTC";
                                    switch (data_BCChiDinhPTTT[i]["servicegrouptype"].ToString())
                                    {
                                        case "2":
                                            chidinhPTTT.bhyt_groupcode = "Xét nghiệm";
                                            break;
                                        case "3":
                                            chidinhPTTT.bhyt_groupcode = "CĐHA";
                                            break;
                                        case "4":
                                            chidinhPTTT.bhyt_groupcode = "PTTT";
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case "12NG":
                                    chidinhPTTT.bhyt_groupcode = "Ngày giường";
                                    break;
                                case "999DVKHAC":
                                    chidinhPTTT.bhyt_groupcode = "DV khác";
                                    break;
                                case "1000PhuThu":
                                    chidinhPTTT.bhyt_groupcode = "Phụ thu";
                                    break;
                                case "11VC":
                                    chidinhPTTT.bhyt_groupcode = "Vận chuyển";
                                    break;
                                default:
                                    break;
                            }

                            lstBCChiDinhPTTT.Add(chidinhPTTT);
                        }
                        //Gan du lieu
                        gridControlDSDV.DataSource = lstBCChiDinhPTTT;
                    }


                    if (gridViewDSDV.RowCount == 0)
                    {
                        Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO_THEO_YEU_CAU);
                    }
                }
                catch (Exception ex)
                {
                    MedicalLink.Base.Logging.Warn(ex);
                    //MessageBox.Show(ex.ToString());
                }
            }
            SplashScreenManager.CloseForm();
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        #region Load
        private void ucBCChiDinhPTTT_Load(object sender, EventArgs e)
        {
            try
            {
                //Lấy thời gian lấy BC mặc định là ngày hiện tại
                DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateTuNgay.Value = date_tu;
                DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                dateDenNgay.Value = date_den;
                //LoadDanhSachKhoa();
                CheckDSKhoa();
            }
            catch (Exception)
            {
            }
        }

        private void LoadDanhSachKhoa()
        {
            try
            {
                string sql_lstKhoa = "SELECT departmentgroupid,departmentgroupcode,departmentgroupname,departmentgrouptype FROM departmentgroup WHERE departmentgrouptype in(4,11,1) ORDER BY departmentgroupname;";
                DataView lstDSKhoa = new DataView(condb.getDataTable(sql_lstKhoa));
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    //chkListKhoa.DataSource = lstDSKhoa;
                    //chkListKhoa.DisplayMember = "departmentgroupname";
                    //chkListKhoa.ValueMember = "departmentgroupcode";
                    chkcomboListDSKhoa.Properties.DataSource = lstDSKhoa;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentgroupname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentgroupid";
                }
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count; i++)
                {
                    chkcomboListDSKhoa.Properties.Items[i].CheckState = CheckState.Checked;
                }
            }
            catch (Exception)
            {
            }
        }
        private void LoadDanhSachPhong()
        {
            try
            {
                string sql_lstPhong = "SELECT departmentid,departmentcode,departmentname,departmenttype FROM department WHERE departmenttype=2 AND iskhonghoatdong=0 ORDER BY departmentname;";
                DataView lstDSPhong = new DataView(condb.getDataTable(sql_lstPhong));
                if (lstDSPhong != null && lstDSPhong.Count > 0)
                {
                    chkcomboListDSKhoa.Properties.DataSource = lstDSPhong;
                    chkcomboListDSKhoa.Properties.DisplayMember = "departmentname";
                    chkcomboListDSKhoa.Properties.ValueMember = "departmentid";
                }
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count; i++)
                {
                    chkcomboListDSKhoa.Properties.Items[i].CheckState = CheckState.Checked;
                }
            }
            catch (Exception)
            {
            }
        }
        private void CheckDSKhoa()
        {
            try
            {
                for (int i = 0; i < chkcomboListDSKhoa.Properties.Items.Count; i++)
                {
                    chkcomboListDSKhoa.Properties.Items[i].CheckState = CheckState.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LayDanhSachNguoiDung()
        {
            try
            {
                lstUser = new List<ClassCommon.classBCChiDinhPTTT_User>();
                string sql_laynguoidung = "SELECT DISTINCT userhisid, usercode, username FROM tools_tbluser WHERE userhisid is not null;";
                DataView data_dsnguoidung = new DataView(condb.getDataTable(sql_laynguoidung));
                if (data_dsnguoidung != null && data_dsnguoidung.Count > 0)
                {
                    for (int i = 0; i < data_dsnguoidung.Count; i++)
                    {
                        ClassCommon.classBCChiDinhPTTT_User user = new ClassCommon.classBCChiDinhPTTT_User();
                        user.userid = Convert.ToInt64(data_dsnguoidung[i]["userhisid"].ToString());
                        user.usercode = data_dsnguoidung[i]["usercode"].ToString();
                        user.username = data_dsnguoidung[i]["username"].ToString();
                        lstUser.Add(user);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void tbnExport_Click(object sender, EventArgs e)
        {
            if (gridViewDSDV.RowCount > 0)
            {
                try
                {
                    using (SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                        if (saveDialog.ShowDialog() != DialogResult.Cancel)
                        {
                            string exportFilePath = saveDialog.FileName;
                            string fileExtenstion = new FileInfo(exportFilePath).Extension;

                            switch (fileExtenstion)
                            {
                                case ".xls":
                                    gridControlDSDV.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlDSDV.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlDSDV.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlDSDV.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlDSDV.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlDSDV.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);

                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                }
            }
            else
            {
                Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.KHONG_CO_DU_LIEU);
            }

        }

        private void gridViewDSDV_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == stt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception)
            {

            }
        }

        private void chkPTTTAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPTTTAll.Checked)
                {
                    mmeMaDV.Text = "";
                    mmeMaDV.ReadOnly = true;
                }
                else
                {
                    mmeMaDV.Text = "Nhập mã nhóm dịch vụ PTTT cách nhau bởi dấu phẩy (,)";
                    mmeMaDV.ReadOnly = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnChonNhomDV_Click(object sender, EventArgs e)
        {
            try
            {
                string dsnhomdichvu = "";
                MedicalLink.ChucNang.BCPTTT.frmLayNhomDichVu frmchonnhom = new ChucNang.BCPTTT.frmLayNhomDichVu(dsnhomdichvu);
                frmchonnhom.MyGetData = new ChucNang.BCPTTT.frmLayNhomDichVu.GetString(TranPatiDataDSDichVu);
                frmchonnhom.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal void TranPatiDataDSDichVu(string dsnhomdichvu)
        {
            try
            {
                mmeMaDV.Text = mmeMaDV.Text + "," + dsnhomdichvu;
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
            catch (Exception ex)
            {
            }
        }

        private void cbbLoaiBA_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbLoaiBA.Text == "Nội trú")
                {
                    lblKhoaPhong.Text = "Khoa chỉ định";
                    chkcomboListDSKhoa.Enabled = true;
                    LoadDanhSachKhoa();
                }
                else if (cbbLoaiBA.Text == "Ngoại trú")
                {
                    lblKhoaPhong.Text = "Phòng chỉ định";
                    chkcomboListDSKhoa.Enabled = true;
                    LoadDanhSachPhong();
                }
                else
                {
                    lblKhoaPhong.Text = "Khoa chỉ định";
                    chkcomboListDSKhoa.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
