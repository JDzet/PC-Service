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
    
    public partial class HistoryTransaction
    {
        public int HistoryId { get; set; }
        public int HistoryNameTransactions { get; set; }
        public Nullable<decimal> Coming { get; set; }
        public Nullable<decimal> Expenses { get; set; }
        public string Description { get; set; }
        public Nullable<int> TransacUser { get; set; }
        public Nullable<int> TransacClient { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Transactions Transactions { get; set; }
        public virtual User User { get; set; }
    }
}