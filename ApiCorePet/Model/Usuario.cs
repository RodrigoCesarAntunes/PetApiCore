using System;
using System.Collections.Generic;

namespace ApiCorePet.Model
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Celular { get; set; }
        public int? Idade { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Foto { get; set; }
        public DateTime? DataCadastro { get; set; }

        public Autenticacao Autenticacao { get; set; }
        public ClienteComercio ClienteComercio { get; set; }
        public ClientePessoa ClientePessoa { get; set; }
    }
}
