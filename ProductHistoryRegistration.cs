//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PC_Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductHistoryRegistration
    {
        public int ProductHistoryID { get; set; }
        public int ProductHName { get; set; }
        public decimal ProductHPForOne { get; set; }
        public int ProductHQuantity { get; set; }
        public decimal ProductHAmount { get; set; }
        public int RegProduct { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual RegistrationProduct RegistrationProduct { get; set; }
    }
}
