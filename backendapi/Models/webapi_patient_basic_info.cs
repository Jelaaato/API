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
    
    public partial class webapi_patient_basic_info
    {
        public System.Guid patient_id { get; set; }
        public string hospital_number { get; set; }
        public Nullable<System.DateTime> registration_date { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string sex_rcd { get; set; }
        public string sex { get; set; }
        public string civil_status_rcd { get; set; }
        public string civil_status { get; set; }
        public string nationality_rcd { get; set; }
        public string nationality { get; set; }
        public string highest_educ_level_rcd { get; set; }
        public string highest_education_level { get; set; }
        public string occupation_rcd { get; set; }
        public string occupation { get; set; }
    }
}
