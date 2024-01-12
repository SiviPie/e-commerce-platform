using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Postari
    {
        public Postari()
        {
            AprecieriPostaris = new HashSet<AprecieriPostari>();
        }

        public int IdPostare { get; set; }
        public int IdSubiect { get; set; }
        public int IdUtilizator { get; set; }
        public string TitluPostare { get; set; } = null!;
        public string ContinutPostare { get; set; } = null!;
        public DateTime? DataPostarii { get; set; }

        public virtual Subiecte IdSubiectNavigation { get; set; } = null!;
        public virtual Utilizatori IdUtilizatorNavigation { get; set; } = null!;
        public virtual ICollection<AprecieriPostari> AprecieriPostaris { get; set; }
    }
}
