//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasterLibrary.Models.DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTHD
    {
        public int MAHD { get; set; }
        public int MASACH { get; set; }
        public Nullable<int> SOLUONG { get; set; }
    
        public virtual HOADON HOADON { get; set; }
        public virtual SACH SACH { get; set; }
    }
}
