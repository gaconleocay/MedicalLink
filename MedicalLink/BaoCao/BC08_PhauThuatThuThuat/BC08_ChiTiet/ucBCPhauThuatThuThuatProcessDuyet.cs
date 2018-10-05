using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;

namespace MedicalLink.BaoCao
{
    public partial class ucBCPhauThuatThuThuat : UserControl
    {
        #region Menu popup
        private void bandedGridViewDataBCPTTT_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (MedicalLink.Base.CheckPermission.ChkPerModule("THAOTAC_05"))
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        e.Menu.Items.Clear();
                        DXMenuItem item_DuyetPTTTChon = new DXMenuItem("Duyệt PTTT đã chọn");
                        item_DuyetPTTTChon.Image = imMenu.Images[0];
                        item_DuyetPTTTChon.Click += new EventHandler(DuyetPTTTDaChon_Click);
                        e.Menu.Items.Add(item_DuyetPTTTChon);
                        DXMenuItem item_GoDuyetPTTTChon = new DXMenuItem("Gỡ duyệt PTTT đã chọn");
                        item_GoDuyetPTTTChon.Image = imMenu.Images[1];
                        item_GoDuyetPTTTChon.Click += new EventHandler(GoDuyetPTTTDaChon_Click);
                        e.Menu.Items.Add(item_GoDuyetPTTTChon);
                        item_GoDuyetPTTTChon.BeginGroup = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
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
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=1, duyetpttt_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstDuyetPTTT_User) + ");";
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
                        string _sqlUpdate_GoDuyet = "UPDATE serviceprice SET duyetpttt_stt=0, duyetpttt_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstGoDuyetPTTT_User) + ");";
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
                                    _update_user = " , gui_usercode='" + Base.SessionLogin.SessionUsercode + "', gui_username='" + Base.SessionLogin.SessionUsername + "', gui_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 2:
                                {
                                    _update_user = " , tiepnhan_usercode='" + Base.SessionLogin.SessionUsercode + "', tiepnhan_username='" + Base.SessionLogin.SessionUsername + "', tiepnhan_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 3:
                                {
                                    _update_user = " , duyet_usercode='" + Base.SessionLogin.SessionUsercode + "', duyet_username='" + Base.SessionLogin.SessionUsername + "', duyet_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
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
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,gui_usercode,gui_username,gui_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 2:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,tiepnhan_usercode,tiepnhan_username,tiepnhan_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + ", '" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 3:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,duyet_usercode,duyet_username,duyet_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "''" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
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

        #region Process Get ID
        private List<ServicepriceDuyetPTTTDTO> GetIdCollection()
        {
            List<ServicepriceDuyetPTTTDTO> IDs = new List<ServicepriceDuyetPTTTDTO>();
            for (int i = 0; i < helper.SelectedCount; i++)
            {
                ServicepriceDuyetPTTTDTO _serviceID = new ServicepriceDuyetPTTTDTO();
                _serviceID.servicepriceid = (helper.GetSelectedRow(i) as DataRowView)["servicepriceid"].ToString();
                _serviceID.duyetpttt_stt = Utilities.TypeConvertParse.ToInt16((helper.GetSelectedRow(i) as DataRowView)["duyetpttt_stt"].ToString());
                _serviceID.vienphiid = Utilities.TypeConvertParse.ToInt64((helper.GetSelectedRow(i) as DataRowView)["vienphiid"].ToString());
                _serviceID.maubenhphamid = Utilities.TypeConvertParse.ToInt64((helper.GetSelectedRow(i) as DataRowView)["maubenhphamid"].ToString());
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
    }
}
