using System;
using System.Collections.Generic;

namespace ApiCorePet.Model
{
    public partial class Services
    {
        public Services()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal? Preco { get; set; }
        public string ClienteComercioEmail { get; set; }

        public ClienteComercio ClienteComercio { get; set; }
        public ICollection<Consulta> Consulta { get; set; }
    }
}
