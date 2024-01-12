using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Bonuri
    {
        public int IdBon { get; set; }
        public int IdUtilizator { get; set; }
        public DateTime? DataFacturare { get; set; }
        public int? IdVoucher { get; set; }

        public virtual Utilizatori IdUtilizatorNavigation { get; set; } = null!;
        public virtual Vouchere? IdVoucherNavigation { get; set; }
    }
}
