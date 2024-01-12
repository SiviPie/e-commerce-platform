using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComertElectronic.Models
{
    public partial class ComertElectronicContext : DbContext
    {
        public ComertElectronicContext()
        {
        }

        public ComertElectronicContext(DbContextOptions<ComertElectronicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administratori> Administratoris { get; set; } = null!;
        public virtual DbSet<AprecieriPostari> AprecieriPostaris { get; set; } = null!;
        public virtual DbSet<AprecieriRecenzii> AprecieriRecenziis { get; set; } = null!;
        public virtual DbSet<Bonuri> Bonuris { get; set; } = null!;
        public virtual DbSet<CategoriiProduse> CategoriiProduses { get; set; } = null!;
        public virtual DbSet<CategoriiSpecificatii> CategoriiSpecificatiis { get; set; } = null!;
        public virtual DbSet<DetaliiBon> DetaliiBons { get; set; } = null!;
        public virtual DbSet<Postari> Postaris { get; set; } = null!;
        public virtual DbSet<ProdusSpecificatii> ProdusSpecificatiis { get; set; } = null!;
        public virtual DbSet<Produse> Produses { get; set; } = null!;
        public virtual DbSet<RaspunsuriPostari> RaspunsuriPostaris { get; set; } = null!;
        public virtual DbSet<Recenzii> Recenziis { get; set; } = null!;
        public virtual DbSet<Reduceri> Reduceris { get; set; } = null!;
        public virtual DbSet<Sedii> Sediis { get; set; } = null!;
        public virtual DbSet<Specificatii> Specificatiis { get; set; } = null!;
        public virtual DbSet<StocProduse> StocProduses { get; set; } = null!;
        public virtual DbSet<Subiecte> Subiectes { get; set; } = null!;
        public virtual DbSet<Utilizatori> Utilizatoris { get; set; } = null!;
        public virtual DbSet<Vouchere> Voucheres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SIVIPIE\\SQLEXPRESS; DataBase=ComertElectronic; TrustServerCertificate=True; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administratori>(entity =>
            {
                entity.HasKey(e => e.IdAdministrator)
                    .HasName("PK__Administ__2160815CA477CEBD");

                entity.ToTable("Administratori");

                entity.HasIndex(e => e.IdUtilizator, "UQ__Administ__3E67D80772D15266")
                    .IsUnique();

                entity.Property(e => e.IdAdministrator).HasColumnName("ID_Administrator");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithOne(p => p.Administratori)
                    .HasForeignKey<Administratori>(d => d.IdUtilizator)
                    .HasConstraintName("FK__Administr__ID_Ut__76177A41");
            });

            modelBuilder.Entity<AprecieriPostari>(entity =>
            {
                entity.HasKey(e => e.IdAprecierePostare)
                    .HasName("PK__Aprecier__1460FD8564358A18");

                entity.ToTable("AprecieriPostari");

                entity.Property(e => e.IdAprecierePostare).HasColumnName("ID_AprecierePostare");

                entity.Property(e => e.DataApreciere).HasColumnType("datetime");

                entity.Property(e => e.IdPostare).HasColumnName("ID_Postare");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.HasOne(d => d.IdPostareNavigation)
                    .WithMany(p => p.AprecieriPostaris)
                    .HasForeignKey(d => d.IdPostare)
                    .HasConstraintName("FK__Aprecieri__ID_Po__3508D0F3");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithMany(p => p.AprecieriPostaris)
                    .HasForeignKey(d => d.IdUtilizator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Aprecieri__ID_Ut__35FCF52C");
            });

            modelBuilder.Entity<AprecieriRecenzii>(entity =>
            {
                entity.HasKey(e => e.IdApreciereRecenzie)
                    .HasName("PK__Aprecier__421ABCD5EC44F55F");

                entity.ToTable("AprecieriRecenzii");

                entity.Property(e => e.IdApreciereRecenzie).HasColumnName("ID_ApreciereRecenzie");

                entity.Property(e => e.IdRecenzie).HasColumnName("ID_Recenzie");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.Property(e => e.Valoare).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdRecenzieNavigation)
                    .WithMany(p => p.AprecieriRecenziis)
                    .HasForeignKey(d => d.IdRecenzie)
                    .HasConstraintName("FK__Aprecieri__ID_Re__1B48FEF0");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithMany(p => p.AprecieriRecenziis)
                    .HasForeignKey(d => d.IdUtilizator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Aprecieri__ID_Ut__1C3D2329");
            });

            modelBuilder.Entity<Bonuri>(entity =>
            {
                entity.HasKey(e => e.IdBon)
                    .HasName("PK__Bonuri__1428132F4E931470");

                entity.ToTable("Bonuri");

                entity.Property(e => e.IdBon).HasColumnName("ID_Bon");

                entity.Property(e => e.DataFacturare).HasColumnType("datetime");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.Property(e => e.IdVoucher).HasColumnName("ID_Voucher");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithMany(p => p.Bonuris)
                    .HasForeignKey(d => d.IdUtilizator)
                    .HasConstraintName("FK__Bonuri__ID_Utili__23DE44F1");

                entity.HasOne(d => d.IdVoucherNavigation)
                    .WithMany(p => p.Bonuris)
                    .HasForeignKey(d => d.IdVoucher)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Bonuri__ID_Vouch__24D2692A");
            });

            modelBuilder.Entity<CategoriiProduse>(entity =>
            {
                entity.HasKey(e => e.IdCategorie)
                    .HasName("PK__Categori__02AA07793B5B57A8");

                entity.ToTable("CategoriiProduse");

                entity.HasIndex(e => e.NumeCategorie, "UQ__Categori__9CCD1076D7585842")
                    .IsUnique();

                entity.Property(e => e.IdCategorie).HasColumnName("ID_Categorie");

                entity.Property(e => e.DescriereCategorie)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeCategorie)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoriiSpecificatii>(entity =>
            {
                entity.HasKey(e => e.IdCategorieSpecificatii)
                    .HasName("PK__Categori__D096AC333DC6EF65");

                entity.ToTable("CategoriiSpecificatii");

                entity.HasIndex(e => e.NumeCategorie, "UQ__Categori__9CCD107673BAF30B")
                    .IsUnique();

                entity.Property(e => e.IdCategorieSpecificatii).HasColumnName("ID_CategorieSpecificatii");

                entity.Property(e => e.NumeCategorie)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetaliiBon>(entity =>
            {
                entity.HasKey(e => e.IdDetaliuBon)
                    .HasName("PK__DetaliiB__6543F105E4C7A012");

                entity.ToTable("DetaliiBon");

                entity.Property(e => e.IdDetaliuBon).HasColumnName("ID_DetaliuBon");

                entity.Property(e => e.IdBon).HasColumnName("ID_Bon");

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");
            });

            modelBuilder.Entity<Postari>(entity =>
            {
                entity.HasKey(e => e.IdPostare)
                    .HasName("PK__Postari__FDA857E8B3977C30");

                entity.ToTable("Postari");

                entity.Property(e => e.IdPostare).HasColumnName("ID_Postare");

                entity.Property(e => e.ContinutPostare)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DataPostarii).HasColumnType("datetime");

                entity.Property(e => e.IdSubiect).HasColumnName("ID_Subiect");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.Property(e => e.TitluPostare)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubiectNavigation)
                    .WithMany(p => p.Postaris)
                    .HasForeignKey(d => d.IdSubiect)
                    .HasConstraintName("FK__Postari__ID_Subi__2E5BD364");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithMany(p => p.Postaris)
                    .HasForeignKey(d => d.IdUtilizator)
                    .HasConstraintName("FK__Postari__ID_Util__2F4FF79D");
            });

            modelBuilder.Entity<ProdusSpecificatii>(entity =>
            {
                entity.HasKey(e => e.IdProdusSpecificatie)
                    .HasName("PK__ProdusSp__5D0B2BD3D976F683");

                entity.ToTable("ProdusSpecificatii");

                entity.Property(e => e.IdProdusSpecificatie).HasColumnName("ID_ProdusSpecificatie");

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");

                entity.Property(e => e.IdSpecificatie).HasColumnName("ID_Specificatie");

                entity.HasOne(d => d.IdProdusNavigation)
                    .WithMany(p => p.ProdusSpecificatiis)
                    .HasForeignKey(d => d.IdProdus)
                    .HasConstraintName("FK__ProdusSpe__ID_Pr__0FD74C44");

                entity.HasOne(d => d.IdSpecificatieNavigation)
                    .WithMany(p => p.ProdusSpecificatiis)
                    .HasForeignKey(d => d.IdSpecificatie)
                    .HasConstraintName("FK__ProdusSpe__ID_Sp__10CB707D");
            });

            modelBuilder.Entity<Produse>(entity =>
            {
                entity.HasKey(e => e.IdProdus)
                    .HasName("PK__Produse__C6921B1205F975A9");

                entity.ToTable("Produse");

                entity.HasIndex(e => e.CodProdus, "UQ__Produse__1F5EFC76D0BD3483")
                    .IsUnique();

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");

                entity.Property(e => e.CodProdus)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DescriereProdus)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdCategorie).HasColumnName("ID_Categorie");

                entity.Property(e => e.IdReducere).HasColumnName("ID_Reducere");

                entity.Property(e => e.ImagineProdus)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeProdus)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategorieNavigation)
                    .WithMany(p => p.Produses)
                    .HasForeignKey(d => d.IdCategorie)
                    .HasConstraintName("FK__Produse__ID_Cate__009508B4");

                entity.HasOne(d => d.IdReducereNavigation)
                    .WithMany(p => p.Produses)
                    .HasForeignKey(d => d.IdReducere)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Produse__ID_Redu__7FA0E47B");
            });

            modelBuilder.Entity<RaspunsuriPostari>(entity =>
            {
                entity.HasKey(e => e.IdRaspunsPostare)
                    .HasName("PK__Raspunsu__D9065587B6A847F7");

                entity.ToTable("RaspunsuriPostari");

                entity.Property(e => e.IdRaspunsPostare).HasColumnName("ID_RaspunsPostare");

                entity.Property(e => e.ContinutRaspuns)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DataRaspuns).HasColumnType("datetime");

                entity.Property(e => e.IdPostare).HasColumnName("ID_Postare");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");
            });

            modelBuilder.Entity<Recenzii>(entity =>
            {
                entity.HasKey(e => e.IdRecenzie)
                    .HasName("PK__Recenzii__150813350A374234");

                entity.ToTable("Recenzii");

                entity.Property(e => e.IdRecenzie).HasColumnName("ID_Recenzie");

                entity.Property(e => e.ContinutRecenzie)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DataRecenzie).HasColumnType("datetime");

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.Property(e => e.Stele).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdProdusNavigation)
                    .WithMany(p => p.Recenziis)
                    .HasForeignKey(d => d.IdProdus)
                    .HasConstraintName("FK__Recenzii__ID_Pro__168449D3");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithMany(p => p.Recenziis)
                    .HasForeignKey(d => d.IdUtilizator)
                    .HasConstraintName("FK__Recenzii__ID_Uti__1590259A");
            });

            modelBuilder.Entity<Reduceri>(entity =>
            {
                entity.HasKey(e => e.IdReducere)
                    .HasName("PK__Reduceri__D690E5CCDB3C664C");

                entity.ToTable("Reduceri");

                entity.Property(e => e.IdReducere).HasColumnName("ID_Reducere");

                entity.Property(e => e.DataAdaugarii).HasColumnType("datetime");

                entity.Property(e => e.ValabilPanaLa).HasColumnType("datetime");
            });

            modelBuilder.Entity<Sedii>(entity =>
            {
                entity.HasKey(e => e.IdSediu)
                    .HasName("PK__Sedii__0375D0A4AF28F66E");

                entity.ToTable("Sedii");

                entity.Property(e => e.IdSediu).HasColumnName("ID_Sediu");

                entity.Property(e => e.Locatie)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeSediu)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Specificatii>(entity =>
            {
                entity.HasKey(e => e.IdSpecificatie)
                    .HasName("PK__Specific__8C10624DE3ED5CEC");

                entity.ToTable("Specificatii");

                entity.Property(e => e.IdSpecificatie).HasColumnName("ID_Specificatie");

                entity.Property(e => e.IdCategorieSpecificatii).HasColumnName("ID_CategorieSpecificatii");

                entity.Property(e => e.ValoareSpecificatie)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategorieSpecificatiiNavigation)
                    .WithMany(p => p.Specificatiis)
                    .HasForeignKey(d => d.IdCategorieSpecificatii)
                    .HasConstraintName("FK__Specifica__ID_Ca__0CFADF99");
            });

            modelBuilder.Entity<StocProduse>(entity =>
            {
                entity.HasKey(e => e.IdStocProdus)
                    .HasName("PK__StocProd__B73A9E5920BA38F0");

                entity.ToTable("StocProduse");

                entity.Property(e => e.IdStocProdus).HasColumnName("ID_StocProdus");

                entity.Property(e => e.IdProdus).HasColumnName("ID_Produs");

                entity.Property(e => e.IdSediu).HasColumnName("ID_Sediu");

                entity.HasOne(d => d.IdProdusNavigation)
                    .WithMany(p => p.StocProduses)
                    .HasForeignKey(d => d.IdProdus)
                    .HasConstraintName("FK__StocProdu__ID_Pr__064DE20A");

                entity.HasOne(d => d.IdSediuNavigation)
                    .WithMany(p => p.StocProduses)
                    .HasForeignKey(d => d.IdSediu)
                    .HasConstraintName("FK__StocProdu__ID_Se__07420643");
            });

            modelBuilder.Entity<Subiecte>(entity =>
            {
                entity.HasKey(e => e.IdSubiect)
                    .HasName("PK__Subiecte__ADC4C5BCC03D28D4");

                entity.ToTable("Subiecte");

                entity.HasIndex(e => e.NumeSubiect, "UQ__Subiecte__964C9AEED535A78D")
                    .IsUnique();

                entity.Property(e => e.IdSubiect).HasColumnName("ID_Subiect");

                entity.Property(e => e.DescriereSubiect)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeSubiect)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Utilizatori>(entity =>
            {
                entity.HasKey(e => e.IdUtilizator)
                    .HasName("PK__Utilizat__3E67D80694E76246");

                entity.ToTable("Utilizatori");

                entity.HasIndex(e => e.Username, "UQ__Utilizat__536C85E4685BDA3C")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Utilizat__A9D10534F96D95FA")
                    .IsUnique();

                entity.Property(e => e.IdUtilizator).HasColumnName("ID_Utilizator");

                entity.Property(e => e.Adresa)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DataCreate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagineProfil)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nume)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Parola)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Prenume)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UltimaAutentificare).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vouchere>(entity =>
            {
                entity.HasKey(e => e.IdVoucher)
                    .HasName("PK__Vouchere__7D975E3ECA5CCCC3");

                entity.ToTable("Vouchere");

                entity.HasIndex(e => e.CodVoucher, "UQ__Vouchere__BAC18FDDDC3B2A14")
                    .IsUnique();

                entity.Property(e => e.IdVoucher).HasColumnName("ID_Voucher");

                entity.Property(e => e.CodVoucher)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DataExpirare).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
