using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class AprecieriPostari
    {
        public int IdAprecierePostare { get; set; }
        public int IdPostare { get; set; }
        public int IdUtilizator { get; set; }
        public int Valoare { get; set; }
        public DateTime? DataApreciere { get; set; }

        public virtual Postari IdPostareNavigation { get; set; } = null!;
        public virtual Utilizatori IdUtilizatorNavigation { get; set; } = null!;
    }
}
