//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnCoSo2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GiaoVien
    {
        public int MaGV { get; set; }
        public string HoTen { get; set; }
        public string email { get; set; }
        public Nullable<int> SDT { get; set; }
        public Nullable<int> MaTK { get; set; }
        public string TenDN { get; set; }
        public string MatKhau { get; set; }
        public Nullable<int> MaQuyen { get; set; }
    
        public virtual Quyen Quyen { get; set; }
    }
}
