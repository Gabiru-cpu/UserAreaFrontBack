using System;

namespace AreaApi.Domain.Models.DTOs
{
    public class SsoDTO
    {
        public string acess_token { get; set; }
        public DateTime expiration { get; set; }

        public SsoDTO(string acess_token, DateTime expiration) 
        {
            this.acess_token = acess_token;
            this.expiration = expiration;
        }
    }
}
