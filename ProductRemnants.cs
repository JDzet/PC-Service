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
    
    public partial class ProductRemnants
    {
        public int RemnantsID { get; set; }
        public int RemnantsProduct { get; set; }
        public int RemnantsQuantity { get; set; }
        public int RemanantsWarehouse { get; set; }
    
        public virtual WarehouseService WarehouseService { get; set; }
        public virtual Product Product { get; set; }
    }
}
