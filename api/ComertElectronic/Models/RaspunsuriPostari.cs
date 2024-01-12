using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class RaspunsuriPostari
    {
        public int IdRaspunsPostare { get; set; }
        public int IdUtilizator { get; set; }
        public int IdPostare { get; set; }
        public string ContinutRaspuns { get; set; } = null!;
        public DateTime? DataRaspuns { get; set; }
    }
}
