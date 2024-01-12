using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class ProdusSpecificatii
    {
        public int IdProdusSpecificatie { get; set; }
        public int IdProdus { get; set; }
        public int IdSpecificatie { get; set; }

        public virtual Produse IdProdusNavigation { get; set; } = null!;
        public virtual Specificatii IdSpecificatieNavigation { get; set; } = null!;
    }
}
