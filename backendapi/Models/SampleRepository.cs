using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendapi.Models
{
    public class SampleRepository
    {
        private apisampleEntities db = new apisampleEntities();

        public IEnumerable<api_sample> GetAll()
        {
            return db.api_sample.AsEnumerable();
        }

        public api_sample SearchbyID(string id)
        {
            return db.api_sample.FirstOrDefault<api_sample>(p => p.Id == id);
        }
    }
}