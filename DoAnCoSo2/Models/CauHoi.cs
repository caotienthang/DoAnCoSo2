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
    
    public partial class CauHoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CauHoi()
        {
            this.BaiLams = new HashSet<BaiLam>();
            this.DeThi_CauHoi = new HashSet<DeThi_CauHoi>();
        }
    
        public int MaCH { get; set; }
        public string CauHoi1 { get; set; }
        public string CauA { get; set; }
        public string CauB { get; set; }
        public string CauC { get; set; }
        public string CauD { get; set; }
        public string DapAn { get; set; }
        public Nullable<int> MaMH { get; set; }
        public string Hinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiLam> BaiLams { get; set; }
        public virtual MonHoc MonHoc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeThi_CauHoi> DeThi_CauHoi { get; set; }
    }
}
