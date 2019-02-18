using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MedicalLink.ClassCommon.ChucNang
{
    public class PhuongPhapVoCamDTO
    {
        public int ppvocamid { get; set; }
        public string ppvocamname { get; set; }
    }

    public static class GetPhuongPhapVoCam
    {
        private static DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        public static List<PhuongPhapVoCamDTO> GetListPhuongPhapVoCam()
        {
            List<PhuongPhapVoCamDTO> _lstPPVoCam = new List<PhuongPhapVoCamDTO>();
            try
            {
                PhuongPhapVoCamDTO _item1 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 1,
                    ppvocamname = "Gây mê tĩnh mạch",
                };
                PhuongPhapVoCamDTO _item2 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 2,
                    ppvocamname = "Gây mê nội khí quản",
                };
                PhuongPhapVoCamDTO _item3 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 3,
                    ppvocamname = "Gây tê tại chỗ",
                };
                PhuongPhapVoCamDTO _item4 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 4,
                    ppvocamname = "Tiền mê + gây tê tại chỗ",
                };
                PhuongPhapVoCamDTO _item5 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 5,
                    ppvocamname = "Gây tê tủy sống",
                };
                PhuongPhapVoCamDTO _item6 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 6,
                    ppvocamname = "Gây tê",
                };
                PhuongPhapVoCamDTO _item7 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 7,
                    ppvocamname = "Gây tê màng ngoài cứng",
                };
                PhuongPhapVoCamDTO _item8 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 8,
                    ppvocamname = "Gây tê đám rối thần kinh",
                };
                PhuongPhapVoCamDTO _item9 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 9,
                    ppvocamname = "Gây tê Codan",
                };
                PhuongPhapVoCamDTO _item10 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 10,
                    ppvocamname = "Gây tê nhãn cầu",
                };
                PhuongPhapVoCamDTO _item11 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 11,
                    ppvocamname = "Gây tê cạnh sống",
                };
                PhuongPhapVoCamDTO _item99 = new PhuongPhapVoCamDTO()
                {
                    ppvocamid = 99,
                    ppvocamname = "Khác",
                };
                string _sqlPP = "select pttt_phuongphapvocamid as ppvocamid,pttt_phuongphapvocamname as ppvocamname from pttt_phuongphapvocam order by pttt_phuongphapvocamname;";
                DataTable dataPhuongPhap = condb.GetDataTable_HIS(_sqlPP);
                //
                _lstPPVoCam.Add(_item1);
                _lstPPVoCam.Add(_item2);
                _lstPPVoCam.Add(_item3);
                _lstPPVoCam.Add(_item4);
                _lstPPVoCam.Add(_item5);
                _lstPPVoCam.Add(_item6);
                _lstPPVoCam.Add(_item7);
                _lstPPVoCam.Add(_item8);
                _lstPPVoCam.Add(_item9);
                _lstPPVoCam.Add(_item10);
                _lstPPVoCam.Add(_item11);
                _lstPPVoCam.Add(_item99);

                if (dataPhuongPhap.Rows.Count > 0)
                {
                    List<PhuongPhapVoCamDTO> _lstitem = Utilities.DataTables.DataTableToList<PhuongPhapVoCamDTO>(dataPhuongPhap);
                    _lstPPVoCam.AddRange(_lstitem);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            return _lstPPVoCam;
        }
    }


}
