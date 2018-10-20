using System;
using System.Collections.Generic;

namespace ApiCorePet.Model
{
    public partial class Consulta
    {
        public int ConsultaId { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public DateTime? DataHora { get; set; }
        public string IsValida { get; set; }
        public string ClienteComercioEmail { get; set; }
        public string ClientePessoaEmail { get; set; }
        public int? ServicoId { get; set; }
        public decimal? Preco { get; set; }
        public int PetsPetId { get; set; }
        public string IsCancelada { get; set; }
        public string Quemcancelou { get; set; }
        public string Motivo { get; set; }

        public ClienteComercio ClienteComercio { get; set; }
        public ClientePessoa ClientePessoa { get; set; }
        public Services Servico { get; set; }
    }
}
