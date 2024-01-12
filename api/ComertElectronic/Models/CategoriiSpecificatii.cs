using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class CategoriiSpecificatii
    {
        public CategoriiSpecificatii()
        {
            Specificatiis = new HashSet<Specificatii>();
        }

        public int IdCategorieSpecificatii { get; set; }
        public string NumeCategorie { get; set; } = null!;

        public virtual ICollection<Specificatii> Specificatiis { get; set; }
    }
}
