﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesMain : DbContext
    {
        public EntitiesMain()
            : base("name=EntitiesMain")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BrandDevice> BrandDevice { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<DeviceType> DeviceType { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<RegistrationProduct> RegistrationProduct { get; set; }
        public virtual DbSet<ProductHistoryRegistration> ProductHistoryRegistration { get; set; }
        public virtual DbSet<ProductRemnants> ProductRemnants { get; set; }
        public virtual DbSet<InformationService> InformationService { get; set; }
        public virtual DbSet<WarehouseService> WarehouseService { get; set; }
        public virtual DbSet<ProductWriteOff> ProductWriteOff { get; set; }
        public virtual DbSet<ProductWriteOffHistory> ProductWriteOffHistory { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<Capital> Capital { get; set; }
        public virtual DbSet<HistoryTransaction> HistoryTransaction { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionType { get; set; }
    }
}
