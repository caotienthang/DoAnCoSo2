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
    
    public partial class Diem
    {
        public Nullable<int> MaDT { get; set; }
        public string MSSV { get; set; }
        public Nullable<decimal> Diem1 { get; set; }
        public Nullable<int> ThoiGianLam { get; set; }
        public int ID { get; set; }
    
        public virtual DeThi DeThi { get; set; }
        public virtual SinhVien SinhVien { get; set; }
    }
}