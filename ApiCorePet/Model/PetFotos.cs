using System;
using System.Collections.Generic;

namespace ApiCorePet.Model
{
    public partial class PetFotos
    {
        public int Id { get; set; }
        public string FotoCaminho { get; set; }
        public int PetId { get; set; }

        public Pets Pet { get; set; }
    }
}
