using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backendapi.Models;
using System.Web.Http.Cors;

namespace backendapi.DataReferencesController
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("ref")]
    public class refController : ApiController
    {
        public AmalgaTESTRefEntities db = new AmalgaTESTRefEntities();

        [Authorize]
        [Route("sex")]
        public IHttpActionResult GetSexRef()
        {
            return Ok(db.sex_ref.AsEnumerable());
        }

        [Authorize]
        [Route("civil_status")]
        public IHttpActionResult GetCivStatRef()
        {
            return Ok(db.marital_status_ref.AsEnumerable());
        }

        [Authorize]
        [Route("nationality")]
        public IHttpActionResult GetNationalityRef()
        {
            return Ok(db.nationality_ref.AsEnumerable());
        }

        [Authorize]
        [Route("educ_level")]
        public IHttpActionResult GetEducLevelRef()
        {
            return Ok(db.patient_education_level_ref.AsEnumerable());
        }

        [Authorize]
        [Route("occupation")]
        public IHttpActionResult GetOccupationRef()
        {
            return Ok(db.occupation_ref.AsEnumerable());
        }

        [Authorize]
        [Route("country")]
        public IHttpActionResult GetCountryRef()
        {
            return Ok(db.country_ref.AsEnumerable());
        }

        [Authorize]
        [Route("charge_type")]
        public IHttpActionResult GetChargeTypeRef()
        {
            return Ok(db.charge_type_ref.AsEnumerable());
        }

        [Authorize]
        [Route("primary_service")]
        public IHttpActionResult GetPrimaryServiceRef()
        {
            return Ok(db.primary_service_ref.AsEnumerable());
        }

        //[Route("visit_type")]
        //public IHttpActionResult GetVisitTypeRef()
        //{
        //    return Ok(db.visit_type_ref.AsEnumerable());
        //}
    }
}
