using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendapi.Models;
namespace backendapi.DataRepositories
{
    public class history_repository
    {
        private APIEntities db = new APIEntities();

        //Family History
        public IEnumerable<webapi_patient_family_history> fam_his()
        {
            return db.webapi_patient_family_history.AsEnumerable();
        }

        public webapi_patient_family_history fam_his_search_by_hn(string hn)
        {
            return db.webapi_patient_family_history.FirstOrDefault<webapi_patient_family_history>(p => p.hospital_number == hn);
        }

        //Medical History
        public IEnumerable<webapi_patient_medical_history> med_his()
        {
            return db.webapi_patient_medical_history.AsEnumerable();
        }

        public webapi_patient_medical_history med_his_search_by_hn(string hn)
        {
            return db.webapi_patient_medical_history.FirstOrDefault<webapi_patient_medical_history>(p => p.hospital_number == hn);
        }

        //Prev Surgeries
        public IEnumerable<webapi_patient_previous_surgeries> prev_surg()
        {
            return db.webapi_patient_previous_surgeries.AsEnumerable();
        }

        public webapi_patient_previous_surgeries prev_surg_search_by_hn(string hn)
        {
            return db.webapi_patient_previous_surgeries.FirstOrDefault<webapi_patient_previous_surgeries>(p => p.hospital_number == hn);
        }
    }
}