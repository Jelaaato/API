using backendapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace backendapi.DataRepositories
{
    public class charges_repository
    {
        private APIEntities db = new APIEntities();

        public IEnumerable<webapi_patient_charges> charges()
        {
            return db.webapi_patient_charges.AsEnumerable();
        }

        public IEnumerable<webapi_patient_charges> charges_by_hn(Guid pvid)
        {
            return db.webapi_patient_charges.Where(a => a.patient_visit_id == pvid).ToList();
        }
    }
}