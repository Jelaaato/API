﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace backendapi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AmalgaTESTRefEntities : DbContext
    {
        public AmalgaTESTRefEntities()
            : base("name=AmalgaTESTRefEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<charge_type_ref> charge_type_ref { get; set; }
        public virtual DbSet<country_ref> country_ref { get; set; }
        public virtual DbSet<marital_status_ref> marital_status_ref { get; set; }
        public virtual DbSet<nationality_ref> nationality_ref { get; set; }
        public virtual DbSet<occupation_ref> occupation_ref { get; set; }
        public virtual DbSet<patient_education_level_ref> patient_education_level_ref { get; set; }
        public virtual DbSet<primary_service_ref> primary_service_ref { get; set; }
        public virtual DbSet<sex_ref> sex_ref { get; set; }
        public virtual DbSet<visit_type_ref> visit_type_ref { get; set; }
    }
}