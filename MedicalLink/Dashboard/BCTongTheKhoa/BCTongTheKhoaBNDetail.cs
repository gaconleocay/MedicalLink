using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.BCTongTheKhoa
{
    public partial class BCTongTheKhoaBNDetail : Form
    {
        int loaiLayDuLieu { get; set; }
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public BCTongTheKhoaBNDetail()
        {
            InitializeComponent();
        }
        public BCTongTheKhoaBNDetail(int loai)
        {
            //loai:
            //=1: BN hien dien
            //=2: BN chuyen di
            //=3: BN chuyen den
            //=4: BN ra vien
            //=9: SL BN da ra vien chua thanh toan
            //=13: SL BN da thanh toan trong ngay
            InitializeComponent();
            loaiLayDuLieu = loai;
        }

        private void BCTongTheKhoaBNDetail_Load(object sender, EventArgs e)
        {
            try
            {
                DataView dataBNDetail = new DataView();
                string sqlGetData = "";
                switch (loaiLayDuLieu)
                {
                    case 1: //BN hien dien
                        sqlGetData = "";
                        dataBNDetail = new DataView(condb.getDataTable(sqlGetData)); ;
                        break;
                    case 2:
                        sqlGetData = "";
                        dataBNDetail = new DataView(condb.getDataTable(sqlGetData)); ;
                        break;

                    case 3: //=3: BN chuyen den
                        sqlGetData = "";
                        dataBNDetail = new DataView(condb.getDataTable(sqlGetData)); ;
                        break;

                    case 4: //=4: BN ra vien
                        sqlGetData = "";
                        dataBNDetail = new DataView(condb.getDataTable(sqlGetData)); ;
                        break;

                    case 9: //=9: SL BN da ra vien chua thanh toan
                        sqlGetData = "";
                        dataBNDetail = new DataView(condb.getDataTable(sqlGetData)); ;
                        break;

                    case 13:  //SL BN da thanh toan trong ngay
                        sqlGetData = "";
                        dataBNDetail = new DataView(condb.getDataTable(sqlGetData)); ;
                        break;
                    default:
                        break;
                }
                gridControlBNDetail.DataSource = dataBNDetail;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



    }
}
