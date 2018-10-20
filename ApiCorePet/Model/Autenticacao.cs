using System;
using System.Collections.Generic;

namespace ApiCorePet.Model
{
    public partial class Autenticacao
    {
        public int Id { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public Usuario Usuario { get; set; }
    }
}
