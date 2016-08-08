using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace backendapi.EntityClasses
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        public string Secret { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public backendapi.Models.Enum.ApplicationTypes AppType { get; set; }
        public Boolean Active_Flag { get; set; }
        public int RefreshToken_LifeSpan { get; set; }
        public string Allowed_Origin { get; set; }
    }
}