using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using backendapi.DataRepositories;
using backendapi.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace backendapi.DataController
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("patients")]
    public class contactsController : ApiController
    {
        static readonly contacts_repository con_repo = new contacts_repository();
        private APIEntities db = new APIEntities();

        [Authorize]
        [Route("contacts/business")]
        public async Task<IHttpActionResult> GetBusinessPhone(int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_business_phone.Count();

                    var pat = await db.webapi_patient_business_phone
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_business_phone>(pat, pageno, pagesize, total));
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
        [Route("contacts/business/{hn}")]
        public IHttpActionResult GetBusinessPhone(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_business_phone bus_phone = con_repo.bus_phone_search_by_hn(hn);
                    if (bus_phone == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(bus_phone);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("contacts/business/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetBusPhFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_business_phone.Count();
            var pat = await db.webapi_patient_business_phone.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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

        [Authorize]
        [Route("contacts/home")]
        public async Task<IHttpActionResult> GetHomePhone(int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_home_phone.Count();

                    var pat = await db.webapi_patient_home_phone
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_home_phone>(pat, pageno, pagesize, total));
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
        [Route("contacts/home/{hn}")]
        public IHttpActionResult GetHomePhone(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_home_phone home_phone = con_repo.home_phone_search_by_hn(hn);
                    if (home_phone == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(home_phone);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("contacts/home/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetHomePhFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_home_phone.Count();
            var pat = await db.webapi_patient_home_phone.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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

        [Authorize]
        [Route("contacts/mobile")]
        public async Task<IHttpActionResult> GetMobilePhone(int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_mobile_phone.Count();

                    var pat = await db.webapi_patient_mobile_phone
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_mobile_phone>(pat, pageno, pagesize, total));
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
        [Route("contacts/mobile/{hn}")]
        public IHttpActionResult GetMobilePhone(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_mobile_phone mobile_phone = con_repo.mobile_phone_search_by_hn(hn);
                    if (mobile_phone == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(mobile_phone);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("contacts/mobile/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetMobileFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_mobile_phone.Count();
            var pat = await db.webapi_patient_mobile_phone.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
