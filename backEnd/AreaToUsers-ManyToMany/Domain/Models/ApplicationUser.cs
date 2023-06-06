using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AreaApi.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        List<Area> Areas { get; set; }
    }
}
