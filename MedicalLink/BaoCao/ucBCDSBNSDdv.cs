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
    public partial class ucBCDSBNSDdv : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public ucBCDSBNSDdv()
        {
            InitializeComponent();
            // Hiển thị Text Hint Mã dịch vụ
            mmeMaDV.ForeColor = SystemColors.GrayText;
            mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)";
            this.mmeMaDV.Leave += new System.EventHandler(this.mmeMaDV_Leave);
            this.mmeMaDV.Enter += new System.EventHandler(this.mmeMaDV_Enter);
        }

        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Leave(object sender, EventArgs e)
        {
            if (mmeMaDV.Text.Length == 0)
            {
                mmeMaDV.Text = "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)";
                mmeMaDV.ForeColor = SystemColors.GrayText;
            }
        }
        // Hiển thị Text Hint Mã dịch vụ
        private void mmeMaDV_Enter(object sender, EventArgs e)
        {
            if (mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)")
            {
                mmeMaDV.Text = "";
                mmeMaDV.ForeColor = SystemColors.WindowText;
            }
        }


        //Sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            gridControlDSDV.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string[] dsdv_temp;
            string dsdv = "";
            string tieuchi, loaivienphiid, doituongbenhnhanid;
            string datetungay = "";
            string datedenngay = "";

            if ((mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)") || (cbbTieuChi.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
            {
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin";
            }
            else
            {
                // Lấy dữ liệu danh sách dịch vụ nhập vào
                dsdv_temp = mmeMaDV.Text.Split(',');
                for (int i = 0; i < dsdv_temp.Length - 1; i++)
                {
                    dsdv += "'" + dsdv_temp[i].ToString() + "',";
                }
                dsdv += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString() + "'";

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
                else //theo ngay duyet BHYT
                {
                    tieuchi = "and vienphi.duyet_ngayduyet ";
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
                    string sqlquerry = "SELECT ROW_NUMBER() OVER (ORDER BY serviceprice.servicepricecode, vienphi.duyet_ngayduyet_vp) as stt, vienphi.patientid as mabn,vienphi.vienphiid as mavp, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoa, department.departmentname as tenphong, serviceprice.servicepricecode as madv, serviceprice.servicepricename as tendv, serviceprice.servicepricemoney as dongia, serviceprice.servicepricedate as thoigianchidinh, serviceprice.soluong as soluong, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, case serviceprice.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end as loaiphieu, vienphi.vienphidate as thoigianvaovien, vienphi.vienphidate_ravien as thoigianravien, vienphi.duyet_ngayduyet_vp as thoigianduyetvp, vienphi.duyet_ngayduyet as thoigianduyetbh, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, vienphi.chandoanravien_code as benhchinh_code, vienphi.chandoanravien as benhchinh_name, vienphi.chandoanravien_kemtheo_code as benhkemtheo_code, vienphi.chandoanravien_kemtheo as benhkemtheo_name, bhyt.bhytcode as bhytcode, case serviceprice.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'CĐHA' when '05TDCN' then 'CĐHA' when '06PTTT' then 'PTTT' when '07KTC' then 'DV KTC' when '12NG' then 'Ngày giường' else '' end as bhyt_groupcode FROM serviceprice INNER JOIN vienphi ON serviceprice.vienphiid=vienphi.vienphiid INNER JOIN hosobenhan ON vienphi.hosobenhanid=hosobenhan.hosobenhanid INNER JOIN departmentgroup ON vienphi.departmentgroupid=departmentgroup.departmentgroupid INNER JOIN department ON vienphi.departmentid=department.departmentid INNER JOIN tools_depatment ON serviceprice.departmentid=tools_depatment.departmentid INNER JOIN bhyt ON bhyt.bhytid=vienphi.bhytid WHERE serviceprice.servicepricecode in (" + dsdv + ") " + tieuchi + " > '" + datetungay + "' " + tieuchi + " < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + " ;";

                    DataView dv = new DataView(condb.getDataTable(sqlquerry));
                    gridControlDSDV.DataSource = dv;

                    if (gridViewDSDV.RowCount == 0)
                    {
                        timerThongBao.Start();
                        lblThongBao.Visible = true;
                        lblThongBao.Text = "Không tìm thấy hồ sơ nào như yêu cầu \n             Vui lòng kiểm tra lại.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            SplashScreenManager.CloseForm();
        }

        /*
        // New: 
                private void btnTimKiem_Click(object sender, EventArgs e)
        {
            lblThongBao.Visible = false;
            gridControlDSDV.DataSource = null;
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            string[] dsdv_temp;
            string dsdv = "";
            string tieuchi, loaivienphiid, doituongbenhnhanid;
            string datetungay = "";
            string datedenngay = "";

            if ((mmeMaDV.Text == "Nhập mã dịch vụ/thuốc cách nhau bởi dấu phẩy (,)") || (cbbTieuChi.Text == "") || (cbbLoaiBA.Text == "") || (chkBHYT.Checked == false && chkVP.Checked == false))
            {
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin";
            }
            else
            {
                // Lấy dữ liệu danh sách dịch vụ nhập vào
                dsdv_temp = mmeMaDV.Text.Split(',');
                for (int i = 0; i < dsdv_temp.Length - 1; i++)
                {
                    dsdv += "'" + dsdv_temp[i].ToString() + "',";
                }
                dsdv += "'" + dsdv_temp[dsdv_temp.Length - 1].ToString() + "'";

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
                else //theo ngay duyet BHYT
                {
                    tieuchi = "and vienphi.duyet_ngayduyet ";
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

                // Lấy xong dữ liệu các trường chọn

                // Thực thi câu lệnh SQL
                try
                {
                    // Tao 1 table luu tru data trung gian.
                    string sql_creater_1 = "CREATE TABLE IF NOT EXISTS tools_tbldata_tmp (data_id serial NOT NULL, stt bigint, mabn integer, mavp integer, tenbn text, tenkhoa text, tenphong text, madv text, tendv text, dongia double precision, thoigianchidinh timestamp without time zone, soluong double precision, khoachidinh text, phongchidinh text, loaiphieu text, thoigianvaovien timestamp without time zone, thoigianravien timestamp without time zone, thoigianduyetvp timestamp without time zone, thoigianduyetbh timestamp without time zone, trangthai text, benhchinh_code text, benhchinh_name text, benhkemtheo_code text, benhkemtheo_name text, CONSTRAINT tools_tbldata_tmp_pkey PRIMARY KEY (data_id));";
                    condb.ExecuteNonQuery(sql_creater_1);

                    string sql_delete = "DROP table tools_tbldata_tmp;";
                    condb.ExecuteNonQuery(sql_delete);

                    string sql_creater = "CREATE TABLE IF NOT EXISTS tools_tbldata_tmp (data_id serial NOT NULL, stt bigint, mabn integer, mavp integer, tenbn text, tenkhoa text, tenphong text, madv text, tendv text, dongia double precision, thoigianchidinh timestamp without time zone, soluong double precision, khoachidinh text, phongchidinh text, loaiphieu text, thoigianvaovien timestamp without time zone, thoigianravien timestamp without time zone, thoigianduyetvp timestamp without time zone, thoigianduyetbh timestamp without time zone, trangthai text, benhchinh_code text, benhchinh_name text, benhkemtheo_code text, benhkemtheo_name text, CONSTRAINT tools_tbldata_tmp_pkey PRIMARY KEY (data_id));";
                    condb.ExecuteNonQuery(sql_creater);
                                        
                    string sql_insertdata = "INSERT INTO tools_tbldata_tmp(stt, mabn, mavp, tenbn, tenkhoa, tenphong, madv, tendv,dongia, thoigianchidinh, soluong, khoachidinh, phongchidinh, loaiphieu, thoigianvaovien, thoigianravien, thoigianduyetvp, thoigianduyetbh, trangthai,benhchinh_code, benhchinh_name, benhkemtheo_code, benhkemtheo_name) SELECT ROW_NUMBER() OVER (ORDER BY serviceprice.servicepricecode, vienphi.duyet_ngayduyet_vp) as stt, vienphi.patientid as mabn,vienphi.vienphiid as mavp, hosobenhan.patientname as tenbn, departmentgroup.departmentgroupname as tenkhoa, department.departmentname as tenphong, serviceprice.servicepricecode as madv, serviceprice.servicepricename as tendv, serviceprice.servicepricemoney as dongia, serviceprice.servicepricedate as thoigianchidinh, serviceprice.soluong as soluong, tools_depatment.departmentgroupname as khoachidinh, tools_depatment.departmentname as phongchidinh, case serviceprice.maubenhphamphieutype when 1 then 'Phiếu trả' else '' end as loaiphieu, vienphi.vienphidate as thoigianvaovien, vienphi.vienphidate_ravien as thoigianravien, vienphi.duyet_ngayduyet_vp as thoigianduyetvp, vienphi.duyet_ngayduyet as thoigianduyetbh, case vienphi.vienphistatus when 2 then 'Đã duyệt VP' when 1 then case vienphi.vienphistatus_vp when 1 then 'Đã duyệt VP' else 'Đã đóng BA' end else 'Đang điều trị' end as trangthai, vienphi.chandoanravien_code as benhchinh_code, vienphi.chandoanravien as benhchinh_name, vienphi.chandoanravien_kemtheo_code as benhkemtheo_code, vienphi.chandoanravien_kemtheo as benhkemtheo_name FROM serviceprice, vienphi, hosobenhan, departmentgroup, department, tools_depatment WHERE serviceprice.vienphiid=vienphi.vienphiid and vienphi.hosobenhanid=hosobenhan.hosobenhanid and vienphi.departmentgroupid=departmentgroup.departmentgroupid and vienphi.departmentid=department.departmentid and serviceprice.departmentid=tools_depatment.departmentid and serviceprice.servicepricecode =" + dsdv + " " + tieuchi + " > '" + datetungay + "' " + tieuchi + " < '" + datedenngay + "' " + loaivienphiid + doituongbenhnhanid + " ;";
                    condb.ExecuteNonQuery(sql_insertdata);

                    // Lay du lieu
                    string sqlquerry = "SELECT * FROM tools_tbldata_tmp";
                    DataView dv = new DataView(condb.getDataTable(sqlquerry));
                    gridControlDSDV.DataSource = dv;

                    if (gridViewDSDV.RowCount == 0)
                    {
                        timerThongBao.Start();
                        lblThongBao.Visible = true;
                        lblThongBao.Text = "Không tìm thấy hồ sơ nào như yêu cầu \n             Vui lòng kiểm tra lại.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            SplashScreenManager.CloseForm();
        }

        */
        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void ucBCDSBNSDdv_Load(object sender, EventArgs e)
        {
            //Lấy thời gian lấy BC mặc định là ngày hiện tại
            DateTime date_tu = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateTuNgay.Value = date_tu;
            DateTime date_den = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            dateDenNgay.Value = date_den;
        }

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
                            timerThongBao.Start();
                            lblThongBao.Visible = true;
                            lblThongBao.Text = "Export dữ liệu thành công!";
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
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Không có dữ liệu!";
            }

        }

    }
}
