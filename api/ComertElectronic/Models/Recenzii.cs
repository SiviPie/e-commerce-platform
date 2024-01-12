using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Recenzii
    {
        public Recenzii()
        {
            AprecieriRecenziis = new HashSet<AprecieriRecenzii>();
        }

        public int IdRecenzie { get; set; }
        public int IdUtilizator { get; set; }
        public int IdProdus { get; set; }
        public string? ContinutRecenzie { get; set; }
        public int? Stele { get; set; }
        public DateTime? DataRecenzie { get; set; }

        public virtual Produse IdProdusNavigation { get; set; } = null!;
        public virtual Utilizatori IdUtilizatorNavigation { get; set; } = null!;
        public virtual ICollection<AprecieriRecenzii> AprecieriRecenziis { get; set; }
    }
}
