using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace backendapi.EntityClasses
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(100)]
        public string ClientId { get; set; }
        public DateTime IssuedUTC { get; set; }
        public DateTime ExpiresUTC { get; set; }
        [Required]
        public string Protected_Ticket { get; set; }
    }
}