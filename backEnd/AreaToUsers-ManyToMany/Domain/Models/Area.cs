using System;
using System.Collections.Generic;

namespace AreaApi.Domain.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }

        public string ApplicationUserId { get; set;}
        public ApplicationUser ApplicationUser { get; set; }
        // um list de usuarios do grupo
        public List<AreaUsers> Usuarios { get; set; }
    }
}
