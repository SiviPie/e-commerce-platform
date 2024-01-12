using System;
using System.Collections.Generic;

namespace ComertElectronic.Models
{
    public partial class Utilizatori
    {
        public Utilizatori()
        {
            AprecieriPostaris = new HashSet<AprecieriPostari>();
            AprecieriRecenziis = new HashSet<AprecieriRecenzii>();
            Bonuris = new HashSet<Bonuri>();
            Postaris = new HashSet<Postari>();
            Recenziis = new HashSet<Recenzii>();
        }

        public int IdUtilizator { get; set; }
        public string Nume { get; set; } = null!;
        public string Prenume { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefon { get; set; }
        public string Parola { get; set; } = null!;
        public string? ImagineProfil { get; set; }
        public string? Adresa { get; set; }
        public int Puncte { get; set; }
        public DateTime DataCreate { get; set; }
        public DateTime? UltimaAutentificare { get; set; }

        public virtual Administratori? Administratori { get; set; }
        public virtual ICollection<AprecieriPostari> AprecieriPostaris { get; set; }
        public virtual ICollection<AprecieriRecenzii> AprecieriRecenziis { get; set; }
        public virtual ICollection<Bonuri> Bonuris { get; set; }
        public virtual ICollection<Postari> Postaris { get; set; }
        public virtual ICollection<Recenzii> Recenziis { get; set; }
    }
}
