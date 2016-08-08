using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.Models;

namespace backendapi.DataRepositories
{
    public class contacts_repository
    {
        private APIEntities db = new APIEntities();

        //Business Phone
        public IEnumerable<webapi_patient_business_phone> bus_phone()
        {
            return db.webapi_patient_business_phone.AsEnumerable();
        }

        public webapi_patient_business_phone bus_phone_search_by_hn(string hn)
        {
            return db.webapi_patient_business_phone.FirstOrDefault<webapi_patient_business_phone>(p => p.hospital_number == hn);
        }

        //Home Phone
        public IEnumerable<webapi_patient_home_phone> home_phone()
        {
            return db.webapi_patient_home_phone.AsEnumerable();
        }

        public webapi_patient_home_phone home_phone_search_by_hn(string hn)
        {
            return db.webapi_patient_home_phone.FirstOrDefault<webapi_patient_home_phone>(p => p.hospital_number == hn);
        }

        //Mobile Phone
        public IEnumerable<webapi_patient_mobile_phone> mobile_phone()
        {
            return db.webapi_patient_mobile_phone.AsEnumerable();
        }

        public webapi_patient_mobile_phone mobile_phone_search_by_hn(string hn)
        {
            return db.webapi_patient_mobile_phone.FirstOrDefault<webapi_patient_mobile_phone>(p => p.hospital_number == hn);
        }
    }
}