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
    public class basic_infoController : ApiController
    {
        static readonly basic_info_repository basic_info_repo = new basic_info_repository();
        private ITWorksDEVEntities db = new ITWorksDEVEntities();


        [Authorize]
        [Route("basic_info")]
        public async Task<IHttpActionResult> Get(int pageno = 1, int pagesize = 10)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_basic_info.Count();

                    var pat = await db.webapi_patient_basic_info
                        .OrderBy(c => c.last_name)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_basic_info>(pat, pageno, pagesize, total));
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        //Get (offset & limit)
        [Route("basic_info/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetFilter(int offset = 0, int limit = 0)
        {
            int total = db.webapi_patient_basic_info.Count();
            var pat = await db.webapi_patient_basic_info.OrderBy(p => p.last_name).Skip(offset).Take(limit).ToListAsync();
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

        //Get a specific data based on HN
        [Authorize]
        [Route("basic_info/{hn}")]
        public IHttpActionResult Get(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_basic_info basic_info = basic_info_repo.basic_info_search_by_hn(hn);
                    if (basic_info == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    successful = true;
                    return Ok(basic_info);
                }
                catch(Exception)
                {
                    retry++;
                }
            }

            return Ok(new Exception("Database is refreshing"));
        }     

        [Route("basic_info/filter/{reg_date}")]
        [Authorize]
        public IHttpActionResult GetByRegDate(DateTime reg_date)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    IEnumerable<webapi_patient_basic_info> basic_info = basic_info_repo.basic_info_by_reg_date(reg_date);
                    if (basic_info == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No record exist for the following date: {0}", reg_date)));
                    }
                    return Ok(basic_info);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }
    }
}
