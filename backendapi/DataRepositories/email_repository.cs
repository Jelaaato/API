using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.Models;


namespace backendapi.DataRepositories
{
    public class email_repository
    {
        private APIEntities db = new APIEntities();

        public IEnumerable<webapi_patient_email_add> email_ad()
        {
            return db.webapi_patient_email_add.AsEnumerable();
        }

        public webapi_patient_email_add email_ad_search_by_hn(string hn)
        {
            return db.webapi_patient_email_add.FirstOrDefault<webapi_patient_email_add>(p => p.hospital_number == hn);
        }

    }
}