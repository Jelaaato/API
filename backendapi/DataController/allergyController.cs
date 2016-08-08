using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using backendapi.DataRepositories;
using backendapi.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace backendapi.DataController
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("patients")]
    public class allergyController : ApiController
    {
        static readonly allergy_repository pat_allergy_repo = new allergy_repository();

        [Authorize]
        [Route("allergies")]
        public async Task<IHttpActionResult> GetAllergy(int pageno = 1, int pagesize = 10)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();

            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_allergy.Count();

                    var pat = await db.webapi_patient_allergy
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_allergy>(pat, pageno, pagesize, total));
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Authorize]
        [Route("allergies/{hn}")]
        public IHttpActionResult GetAllergy(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    IEnumerable<webapi_patient_allergy> pat_allergy = pat_allergy_repo.patient_allergy_search_by_hn(hn);
                    if (pat_allergy == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(pat_allergy);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }
       
        [Authorize]
        [Route("allergies/filter")]
        public async Task<IHttpActionResult> GetAllergyFilter(int offset = 0, int limit = 0)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();
            int total = db.webapi_patient_allergy.Count();
            var pat = await db.webapi_patient_allergy.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
            return Ok(new
            {
                Data = pat,
                Paging = new
                {
                    Total = total,
                    Limit = limit,
                    Offset = offset,
                    Returned = pat.Count
                }
            });
        }
    }
}
