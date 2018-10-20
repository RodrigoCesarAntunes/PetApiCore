using System;
using System.Collections.Generic;

namespace ApiCorePet.Model
{
    public partial class Pets
    {
        public Pets()
        {
            PetFotos = new HashSet<PetFotos>();
        }

        public int PetId { get; set; }
        public string Nome { get; set; }
        public string Especie { get; set; }
        public string Raca { get; set; }
        public string Peso { get; set; }
        public string Tamanho { get; set; }
        public string Descricao { get; set; }
        public string Genero { get; set; }
        public string Idade { get; set; }
        public string ClientePessoaEmail { get; set; }

        public ClientePessoa ClientePessoa { get; set; }
        public ICollection<PetFotos> PetFotos { get; set; }
    }
}
