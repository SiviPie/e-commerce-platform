using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class StocProduse
    {
        public int IdStocProdus { get; set; }
        public int IdProdus { get; set; }
        public int IdSediu { get; set; }
        public int Stoc { get; set; }

        public virtual Produse IdProdusNavigation { get; set; } = null!;
        public virtual Sedii IdSediuNavigation { get; set; } = null!;
    }
}
