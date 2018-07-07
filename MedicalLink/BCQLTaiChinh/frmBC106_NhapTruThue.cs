﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BCQLTaiChinh
{
    public partial class frmBC106_NhapTruThue : Form
    {
        // khai báo 1 hàm delegate
        public delegate void GetTongChi(decimal _TongChi);
        // khai báo 1 kiểu hàm delegate
        public GetTongChi MyGetData;


        public frmBC106_NhapTruThue()
        {
            InitializeComponent();
            txttongchi.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                MyGetData(Utilities.TypeConvertParse.ToDecimal(txttongchi.Text));
                this.Close();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }



    }
}