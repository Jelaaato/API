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
    
    public partial class webapi_patient_business_phone
    {
        public System.Guid patient_id { get; set; }
        public string hospital_number { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public string business_phone_country_idd_dialing_code { get; set; }
        public Nullable<decimal> business_phone_area_code { get; set; }
        public string business_phone_number { get; set; }
        public Nullable<decimal> business_phone_extension { get; set; }
    }
}
