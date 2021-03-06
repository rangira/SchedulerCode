namespace Scheduler_mvc.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=validClientDBModel")
        {
        }

        public virtual DbSet<valid_clients> valid_clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<valid_clients>()
                .Property(e => e.client_name)
                .IsUnicode(false);
        }
    }
}
