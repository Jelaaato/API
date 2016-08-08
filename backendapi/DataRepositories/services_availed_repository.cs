using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.DataController;
using backendapi.Models;

namespace backendapi.DataRepositories
{
    public class services_availed_repository
    {
        private APIEntities db = new APIEntities();

        public IEnumerable<webapi_patient_services_availed> services_availed()
        {
            return db.webapi_patient_services_availed.AsEnumerable();
        }

        public IEnumerable<webapi_patient_services_availed> services_availed_search_by_hn(string hn)
        {
            return db.webapi_patient_services_availed.Where(a => a.hospital_number == hn).ToList();
        }
    }
}