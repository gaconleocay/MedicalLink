using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BCQLTaiChinh
{
    public class BC119DSHuongTienDVMoYCDTO
    {
        public int stt { get; set; }
        public string username { get; set; }
        public string usercode { get; set; }
        public string departmentgroupname { get; set; }
        public string sotaikhoan { get; set; }
        public decimal mochinh_sltt { get; set; }
        public decimal mochinh_sll1 { get; set; }
        public decimal mochinh_sll2 { get; set; }
        public decimal mochinh_sll3 { get; set; }
        public decimal phu_sltt { get; set; }
        public decimal phu_sll1 { get; set; }
        public decimal phu_sll2 { get; set; }
        public decimal phu_sll3 { get; set; }
        public decimal bacsigayme_sltt { get; set; }
        public decimal bacsigayme_sll1 { get; set; }
        public decimal bacsigayme_sll2 { get; set; }
        public decimal bacsigayme_sll3 { get; set; }
        public decimal ktvphume_sltt { get; set; }
        public decimal ktvphume_sll1 { get; set; }
        public decimal ktvphume_sll2 { get; set; }
        public decimal ktvphume_sll3 { get; set; }
        public decimal dungcuvien_sltt { get; set; }
        public decimal dungcuvien_sll1 { get; set; }
        public decimal dungcuvien_sll2 { get; set; }
        public decimal dungcuvien_sll3 { get; set; }
        public decimal ddhoitinh_sltt { get; set; }
        public decimal ddhoitinh_sll1 { get; set; }
        public decimal ddhoitinh_sll2 { get; set; }
        public decimal ddhoitinh_sll3 { get; set; }
        public decimal ktvhoitinh_sltt { get; set; }
        public decimal ktvhoitinh_sll1 { get; set; }
        public decimal ktvhoitinh_sll2 { get; set; }
        public decimal ktvhoitinh_sll3 { get; set; }
        public decimal ddhanhchinh_sltt { get; set; }
        public decimal ddhanhchinh_sll1 { get; set; }
        public decimal ddhanhchinh_sll2 { get; set; }
        public decimal ddhanhchinh_sll3 { get; set; }
        public decimal holy_sltt { get; set; }
        public decimal holy_sll1 { get; set; }
        public decimal holy_sll2 { get; set; }
        public decimal holy_sll3 { get; set; }
        public decimal thuclinh { get; set; }
        public string kynhan { get; set; }
    }


    public class BC119ThucHienPTTTDTO
    {
        public int vienphiid { get; set; }
        public int mochinh { get; set; }
        public int phumo1 { get; set; }
        public int phumo2 { get; set; }
        public int bacsigayme { get; set; }
        public int ktvphume { get; set; }
        public int dungcuvien { get; set; }
        public int ddhoitinh { get; set; }
        public int ktvhoitinh { get; set; }
        public int ddhanhchinh { get; set; }
        public int holy { get; set; }
        public string servicepricecode { get; set; }
        public decimal dongia { get; set; }
        public decimal soluong { get; set; }
        public decimal soluong_phu { get; set; }
    }

}
