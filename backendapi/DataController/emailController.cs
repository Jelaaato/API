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
    public class emailController : ApiController
    {
        static readonly email_repository email_repo = new email_repository();
        private APIEntities db = new APIEntities();

        [Authorize]
        [Route("contacts/email")]
        public async Task<IHttpActionResult> GetEAd(int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_email_add.Count();

                    var pat = await db.webapi_patient_email_add
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_email_add>(pat, pageno, pagesize, total));
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        //Get a specific data based on HN
        [Authorize]
        [Route("contacts/email/{hn}")]
        public IHttpActionResult GetEAd(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_email_add email_ad = email_repo.email_ad_search_by_hn(hn);
                    if (email_ad == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(email_ad);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("contacts/email/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetEAdFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_email_add.Count();
            var pat = await db.webapi_patient_email_add.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
