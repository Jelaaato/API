using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.Models;

namespace backendapi.DataRepositories
{
    public class basic_info_repository
    {
        private ITWorksDEVEntities db = new ITWorksDEVEntities();
        
        public IEnumerable<webapi_patient_basic_info> basic_info()
        {
            return db.webapi_patient_basic_info.AsEnumerable();
        }

        public webapi_patient_basic_info basic_info_search_by_hn(string hn)
        {
            return db.webapi_patient_basic_info.FirstOrDefault<webapi_patient_basic_info>(p => p.hospital_number == hn);
        }

        public IEnumerable<webapi_patient_basic_info> basic_info_by_reg_date(DateTime reg_date)
        {
            return db.webapi_patient_basic_info.Where(a => a.registration_date == reg_date);
        }
    }
}