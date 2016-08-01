namespace Scheduler_mvc.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class apponitmentsDBModel : DbContext
    {
        public apponitmentsDBModel()
            : base("name=apponitmentsDBModel")
        {
        }

        public virtual DbSet<appointment> appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<appointment>()
                .Property(e => e.recording_uri)
                .IsUnicode(false);

            modelBuilder.Entity<appointment>()
                .Property(e => e.appt_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<appointment>()
                .Property(e => e.appt_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<appointment>()
                .Property(e => e.appt_zone)
                .IsUnicode(false);

            modelBuilder.Entity<appointment>()
                .Property(e => e.agent_name)
                .IsUnicode(false);

            modelBuilder.Entity<appointment>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<appointment>()
                .Property(e => e.appt_result)
                .IsUnicode(false);
        }
    }
}
