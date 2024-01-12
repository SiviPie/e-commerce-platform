using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Reduceri
    {
        public Reduceri()
        {
            Produses = new HashSet<Produse>();
        }

        public int IdReducere { get; set; }
        public double PretNou { get; set; }
        public DateTime? ValabilPanaLa { get; set; }
        public DateTime DataAdaugarii { get; set; }

        public virtual ICollection<Produse> Produses { get; set; }
    }
}
