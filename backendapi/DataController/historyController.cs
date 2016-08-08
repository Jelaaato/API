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
    public class historyController : ApiController
    {
        static readonly history_repository hist_repo = new history_repository();

        [Authorize]
        [Route("history/family")]
        public async Task<IHttpActionResult> GetFamilyHis(int pageno = 1, int pagesize = 10)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();

            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_family_history.Count();

                    var pat = await db.webapi_patient_family_history
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_family_history>(pat, pageno, pagesize, total));
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
        [Route("history/family/{hn}")]
        public IHttpActionResult GetFamilyHis(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_family_history fam_his = hist_repo.fam_his_search_by_hn(hn);
                    if (fam_his == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(fam_his);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("history/family/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetFamFilter(int offset = 0, int limit = 0)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();
            int total = db.webapi_patient_family_history.Count();
            var pat = await db.webapi_patient_family_history.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
        [Route("history/medical")]
        public async Task<IHttpActionResult> GetMedHis(int pageno = 1, int pagesize = 10)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();

            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_medical_history.Count();

                    var pat = await db.webapi_patient_medical_history
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_medical_history>(pat, pageno, pagesize, total));
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
        [Route("history/medical/{hn}")]
        public IHttpActionResult GetMedHis(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_medical_history med_his = hist_repo.med_his_search_by_hn(hn);
                    if (med_his == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(med_his);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("history/medical/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetMedFilter(int offset = 0, int limit = 0)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();
            int total = db.webapi_patient_medical_history.Count();
            var pat = await db.webapi_patient_medical_history.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
        [Route("history/surgical")]
        public async Task<IHttpActionResult> GetSurgHis(int pageno = 1, int pagesize = 10)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();

            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    int skip = (pageno - 1) * pagesize;

                    int total = db.webapi_patient_previous_surgeries.Count();

                    var pat = await db.webapi_patient_previous_surgeries
                        .OrderBy(c => c.hospital_number)
                        .Skip(skip)
                        .Take(pagesize)
                        .ToListAsync();

                    return Ok(new Paging<webapi_patient_previous_surgeries>(pat, pageno, pagesize, total));
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
        [Route("history/surgical/{hn}")]
        public IHttpActionResult GetSurgHis(string hn)
        {
            bool successful = false;
            int retry = 0;
            while (!successful && retry < 3)
            {
                try
                {
                    webapi_patient_previous_surgeries prev_surg = hist_repo.prev_surg_search_by_hn(hn);
                    if (prev_surg == null)
                    {
                        throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No Patient exists with the following hospital number: {0}", hn)));
                    }
                    return Ok(prev_surg);
                }
                catch (Exception)
                {
                    retry++;
                }
            }
            return Ok(new Exception("Database is refreshing"));
        }

        [Route("history/surgical/filter")]
        [Authorize]
        public async Task<IHttpActionResult> GetSurgFilter(int offset = 0, int limit = 0)
        {
            ITWorksDEVEntities db = new ITWorksDEVEntities();
            int total = db.webapi_patient_previous_surgeries.Count();
            var pat = await db.webapi_patient_previous_surgeries.OrderBy(p => p.hospital_number).Skip(offset).Take(limit).ToListAsync();
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
