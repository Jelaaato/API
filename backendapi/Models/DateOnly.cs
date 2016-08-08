using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendapi.Models
{
    public class DateOnly : IsoDateTimeConverter
    {
        public DateOnly()
        { 
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}