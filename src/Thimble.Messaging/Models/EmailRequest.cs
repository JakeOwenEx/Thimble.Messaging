using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Thimble.Messaging.Models
{
    public class EmailRequest
    {
        public EmailRequest()
        {
            UserId = Guid.NewGuid().ToString();
        }

        [JsonProperty] 
        public string UserId { get; set; }
        
        [Required]
        [JsonProperty] 
        public string Email { get; set; }
    }
}