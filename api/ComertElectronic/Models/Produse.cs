using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Produse
    {
        public Produse()
        {
            ProdusSpecificatiis = new HashSet<ProdusSpecificatii>();
            Recenziis = new HashSet<Recenzii>();
            StocProduses = new HashSet<StocProduse>();
        }

        public int IdProdus { get; set; }
        public string NumeProdus { get; set; } = null!;
        public int IdCategorie { get; set; }
        public string CodProdus { get; set; } = null!;
        public string? ImagineProdus { get; set; }
        public string? DescriereProdus { get; set; }
        public double PretProdus { get; set; }
        public int? IdReducere { get; set; }

        public virtual CategoriiProduse IdCategorieNavigation { get; set; } = null!;
        public virtual Reduceri? IdReducereNavigation { get; set; }
        public virtual ICollection<ProdusSpecificatii> ProdusSpecificatiis { get; set; }
        public virtual ICollection<Recenzii> Recenziis { get; set; }
        public virtual ICollection<StocProduse> StocProduses { get; set; }
    }
}
