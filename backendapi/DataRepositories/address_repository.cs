using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.Models;
namespace backendapi.DataRepositories
{
    public class address_repository
    {
        private ITWorksDEVEntities db = new ITWorksDEVEntities();

        //Business Address
        public IEnumerable<webapi_patient_business_address> bus_ad()
        {
            return db.webapi_patient_business_address.AsEnumerable();
        }

        public webapi_patient_business_address bus_ad_search_by_hn(string hn)
        {
            return db.webapi_patient_business_address.FirstOrDefault<webapi_patient_business_address>(p => p.hospital_number == hn);
        }

        //Home Address
        public IEnumerable<webapi_patient_home_address> home_ad()
        {
            return db.webapi_patient_home_address.AsEnumerable();
        }

        public webapi_patient_home_address home_ad_search_by_hn(string hn)
        {
            return db.webapi_patient_home_address.FirstOrDefault<webapi_patient_home_address>(p => p.hospital_number == hn);
        }

    }
}