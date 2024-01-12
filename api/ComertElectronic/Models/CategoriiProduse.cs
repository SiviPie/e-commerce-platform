using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class CategoriiProduse
    {
        public CategoriiProduse()
        {
            Produses = new HashSet<Produse>();
        }

        public int IdCategorie { get; set; }
        public string NumeCategorie { get; set; } = null!;
        public string? DescriereCategorie { get; set; }

        public virtual ICollection<Produse> Produses { get; set; }
    }
}
