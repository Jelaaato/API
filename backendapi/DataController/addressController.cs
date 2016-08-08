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
    public class addressController : ApiController
    {
        
        static readonly address_repository add_repo = new address_repository();

        [Authorize]
        [Route("address/business")]
        public async Task<IHttpActionResult> GetBusinessAd(int pageno = 1, int pagesize = 10)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();

            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_business_address.Count();

                    var pat = await db.webapi_patient_business_address
                       .OrderBy(c => c.hospital_number)
                       .Skip(skip)
                       .Take(pagesize)
                       .ToListAsync();

                    successful = true;
                    return Ok(new Paging<webapi_patient_business_address>(pat, pageno, pagesize, total));

                }

                catch (Exception)
                {
                    retry++;
                    //throw new Exception("Database is recovering", ex.InnerException);
                }
            }

            return Ok(new Exception("Database is refreshing"));
            
        }

        //Get a specific data based on HN
        [Authorize]
        [Route("address/business/{hn}")]
        public IHttpActionResult GetBusinessAd(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_business_address bus_ad = add_repo.bus_ad_search_by_hn(hn);
                    if (bus_ad == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(bus_ad);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Authorize]
        [Route("address/business/filter")]
        public async Task<IHttpActionResult> GetBusAdFilter(int offset = 0, int limit = 0)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();

            int total = db.webapi_patient_business_address.Count();
            var pat = await db.webapi_patient_business_address.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
        [Route("address/home")]
        public async Task<IHttpActionResult> GetHomeAd(int pageno = 1, int pagesize = 10)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();
            
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_home_address.Count();

                    var pat = await db.webapi_patient_home_address
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_home_address>(pat, pageno, pagesize, total));
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        //Get a specific data based on ID
        [Authorize]
        [Route("address/home/{hn}")]
        public IHttpActionResult GetHomeAd(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_home_address home_ad = add_repo.home_ad_search_by_hn(hn);
                    if (home_ad == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(home_ad);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Authorize]
        [Route("address/home/filter")]
        public async Task<IHttpActionResult> GetHomeAdFilter(int offset = 0, int limit = 0)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();
            int total = db.webapi_patient_home_address.Count();
            var pat = await db.webapi_patient_home_address.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
