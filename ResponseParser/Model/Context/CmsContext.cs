using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ResponseParser.Model
{
    public class Cms277CAContext : DbContext
    {
        public Cms277CAContext() : base("name=CMS277CAConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Response");
            modelBuilder.Entity<_277CABillProv>().ToTable("277CABillProv");
            modelBuilder.Entity<_277CAFile>().ToTable("277CAFile");
            modelBuilder.Entity<_277CALine>().ToTable("277CALine");
            modelBuilder.Entity<_277CAPatient>().ToTable("277CAPatient");
            modelBuilder.Entity<_277CAStc>().ToTable("277CAStc");
        }
        public virtual DbSet<_277CABillProv> Table277CABillProv { get; set; }
        public virtual DbSet<_277CAFile> Table277CAFile { get; set; }
        public virtual DbSet<_277CALine> Table277CALine { get; set; }
        public virtual DbSet<_277CAPatient> Table277CAPatient { get; set; }
        public virtual DbSet<_277CAStc> Table277CAStc { get; set; }
    }

    public class Cms999Context : DbContext
    {
        public Cms999Context() : base("name=CMS999ConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Response");
            modelBuilder.Entity<_999File>().ToTable("999File");
            modelBuilder.Entity<_999Transaction>().ToTable("999Transaction");
            modelBuilder.Entity<_999Error>().ToTable("999Error");
            modelBuilder.Entity<_999Element>().ToTable("999Element");
        }

        public virtual DbSet<_999File> Table999File { get; set; }
        public virtual DbSet<_999Transaction> Table999Transaction { get; set; }
        public virtual DbSet<_999Error> Table999Error { get; set; }
        public virtual DbSet<_999Element> Table999Element { get; set; }
    }

    public class CmsMao2Context : DbContext
    {
        public CmsMao2Context() : base("name=CMSMAO2ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Response");
            modelBuilder.Entity<MAO2File>().ToTable("MAO2File");
            modelBuilder.Entity<MAO2Detail>().ToTable("MAO2Detail");
        }

        public virtual DbSet<MAO2File> TableMao2File { get; set; }
        public virtual DbSet<MAO2Detail> TableMao2Detail { get; set; }
    }
}
