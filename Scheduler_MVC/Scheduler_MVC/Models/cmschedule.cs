namespace Scheduler_MVC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class cmschedule : DbContext
    {
        public cmschedule()
            : base("name=communityMeetingConn")
        {
        }

        public virtual DbSet<community_meeting_schedule> community_meeting_schedule { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<community_meeting_schedule>()
                .Property(e => e.state)
                .IsFixedLength();
        }
    }
}
