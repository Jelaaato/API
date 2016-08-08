using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backendapi.DataRepositories;
using backendapi.Models;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using System.Data.Entity;
using System.Threading.Tasks;

namespace backendapi.DataController
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("patients")]
    public class chargesController : ApiController
    {
        static readonly charges_repository charges_repo = new charges_repository();
        private APIEntities db = new APIEntities();

        //[Route("charges")]
        //public IHttpActionResult GetCharges()
        //{
        //    return Ok(charges_repo.charges());
        //}

        [Authorize]
        [Route("charges")]
        public async Task<IHttpActionResult> GetCharges(int pageno = 1, int pagesize = 2)
        {
                bool successful = false;
                int retry = 0;
                while (!successful && retry < 3)
                {
                    try
                    {
                        int skip = (pageno - 1) * pagesize;

                        int total = db.webapi_patient_charges.Count();

                        var pat = await db.webapi_patient_charges
                            .OrderBy(c => c.hospital_number)
                            .Skip(skip)
                            .Take(pagesize)
                            .ToListAsync();

                        return Ok(new Paging<webapi_patient_charges>(pat, pageno, pagesize, total));
                    }
                    catch (Exception)
                    {
                        retry++;
                    }
                }

            return Ok(new Exception("Database is refreshing"));   
        }

        [Authorize]
        [Route("charges/{pvid}")]
        public IHttpActionResult GetCharges(Guid pvid)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    IEnumerable<webapi_patient_charges> _charges = charges_repo.charges_by_hn(pvid);
                    if (_charges == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", pvid)));
                    }
                    successful = true;
                    return Ok(_charges);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Authorize]
        [Route("charges/total_amount/{pvid}")]
        public IHttpActionResult GetTotalCharge(Guid pvid)
        {
            decimal total = (decimal)(db.webapi_patient_charges.Where(a => a.patient_visit_id == pvid).Sum(a => a.amount));

            return Ok(total);
        }

        [Route("charges/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetChargesFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_charges.Count();
            var pat = await db.webapi_patient_charges.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
