using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.Models;

namespace backendapi.DataRepositories
{
    public class allergy_repository
    {
        private APIEntities db = new APIEntities();

        public IEnumerable<webapi_patient_allergy> patient_allergy()
        {
            return db.webapi_patient_allergy.AsEnumerable();
        }

        public IEnumerable<webapi_patient_allergy> patient_allergy_search_by_hn(string hn)
        {
            return db.webapi_patient_allergy.Where(a => a.hospital_number == hn);
        }
        
        
        //public webapi_patient_basic_info basic_info_search_by_hn(string hn)
        //{
        //    return db.webapi_patient_basic_info.FirstOrDefault<webapi_patient_basic_info>(p => p.hospital_number == hn);
        //}
    }
}