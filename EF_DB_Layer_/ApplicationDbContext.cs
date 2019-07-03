using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using SportsClubModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EF_DB_Layer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; } 

        public DbSet<User> Users { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>().ToTable("Fields")
                .HasDiscriminator<int>("Sports")
                .HasValue<PaddleCourt>((int)Sports.Paddle)
                .HasValue<TennisCourt>((int)Sports.Tennis)
                .HasValue<SoccerField>((int)Sports.Soccer);

            //modelbuilder.Entity<Challenge>()
            //    .HasOne(c => c.Reservation)
            //    .WithOne(r => r.Challenge)
            //    .HasForeignKey<Challenge>(c => c.ReservationId)
            //    .IsRequired().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Challenge)
                .WithOne(c => c.Reservation)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
              .HasOne(r => r.Field)
              .WithMany(f => f.Reservations)
              .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User) //Reservations ha un Member
                .WithMany(m => m.Reservation) //e un Member ha molte Reservations
                .IsRequired();//OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChallengesUsers>()
        .HasKey(cu => new { cu.ChallengeId, cu.UserId});
            modelBuilder.Entity<ChallengesUsers>()
                .HasOne(cu => cu.User)
                .WithMany(c => c.ChallengesUsers)
                .HasForeignKey(cu => cu.UserId);
            modelBuilder.Entity<ChallengesUsers>()
                .HasOne(cu => cu.Challenge)
                .WithMany(u => u.ChallengesUsers)
                .HasForeignKey(cu => cu.ChallengeId);
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=SportsClub; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }
}
