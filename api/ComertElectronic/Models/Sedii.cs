using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Sedii
    {
        public Sedii()
        {
            StocProduses = new HashSet<StocProduse>();
        }

        public int IdSediu { get; set; }
        public string NumeSediu { get; set; } = null!;
        public string Locatie { get; set; } = null!;

        public virtual ICollection<StocProduse> StocProduses { get; set; }
    }
}
