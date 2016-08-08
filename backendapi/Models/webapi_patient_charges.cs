//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class webapi_patient_charges
    {
        public string item_type_rcd { get; set; }
        public string charge_type_rcd { get; set; }
        public System.Guid charge_detail_id { get; set; }
        public System.Guid patient_visit_id { get; set; }
        public string hospital_number { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> registration_date { get; set; }
        public decimal unit_price { get; set; }
        public Nullable<decimal> quantity { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<System.DateTime> Serviced_Date { get; set; }
        public Nullable<System.TimeSpan> Serviced_Time { get; set; }
        public Nullable<System.DateTime> Charged_Date { get; set; }
        public Nullable<System.TimeSpan> Charged_Time { get; set; }
        public Nullable<System.DateTime> Closed_Date { get; set; }
        public Nullable<System.TimeSpan> Closed_Time { get; set; }
        public System.Guid item_id { get; set; }
        public string item_name { get; set; }
    }
}
