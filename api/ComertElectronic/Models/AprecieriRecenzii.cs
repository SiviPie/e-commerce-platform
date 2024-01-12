using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class AprecieriRecenzii
    {
        public int IdApreciereRecenzie { get; set; }
        public int IdRecenzie { get; set; }
        public int IdUtilizator { get; set; }
        public int Valoare { get; set; }

        public virtual Recenzii IdRecenzieNavigation { get; set; } = null!;
        public virtual Utilizatori IdUtilizatorNavigation { get; set; } = null!;
    }
}
