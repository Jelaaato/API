using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backendapi.DataRepositories;
using backendapi.Models;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Data.Entity;

namespace backendapi.DataController
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("patients")]
    public class services_availedController : ApiController
    {
        static readonly services_availed_repository services_repo = new services_availed_repository();
        private APIEntities db = new APIEntities();

        [Authorize]
        [Route("services_availed")]
        public async Task<IHttpActionResult> GetServices(int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_services_availed.Count();

                    var pat = await db.webapi_patient_services_availed
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_services_availed>(pat, pageno, pagesize, total));
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Authorize]
        [Route("services_availed/{hn}")]
        public IHttpActionResult GetServices(string hn, int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    IEnumerable<webapi_patient_services_availed> _services = services_repo.services_availed_search_by_hn(hn);
                    if (_services == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    successful = true;
                    return Ok(_services);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("services_availed/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetServicesFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_services_availed.Count();
            var pat = await db.webapi_patient_services_availed.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
