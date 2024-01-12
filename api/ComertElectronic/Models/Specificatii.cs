using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Specificatii
    {
        public Specificatii()
        {
            ProdusSpecificatiis = new HashSet<ProdusSpecificatii>();
        }

        public int IdSpecificatie { get; set; }
        public string ValoareSpecificatie { get; set; } = null!;
        public int IdCategorieSpecificatii { get; set; }

        public virtual CategoriiSpecificatii IdCategorieSpecificatiiNavigation { get; set; } = null!;
        public virtual ICollection<ProdusSpecificatii> ProdusSpecificatiis { get; set; }
    }
}
