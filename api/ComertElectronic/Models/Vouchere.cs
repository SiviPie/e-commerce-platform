using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Vouchere
    {
        public Vouchere()
        {
            Bonuris = new HashSet<Bonuri>();
        }

        public int IdVoucher { get; set; }
        public string CodVoucher { get; set; } = null!;
        public double? ValoareVoucher { get; set; }
        public DateTime? DataExpirare { get; set; }
        public bool Utilizat { get; set; }

        public virtual ICollection<Bonuri> Bonuris { get; set; }
    }
}
