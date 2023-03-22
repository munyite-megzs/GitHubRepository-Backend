using GitRepositoryTracker.Data;
using GitRepositoryTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GitRepositoryTracker.DButil
{
    public class GitRepoContext:DbContext
    {
        public DbSet<Repository> Repositories { get; set; } 
        public DbSet<Topic> Topics { get; set; } 
        public DbSet<RepositoryTopic> RepositoryTopics { get; set; }

    
        public GitRepoContext(DbContextOptions<GitRepoContext> options)
           : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepositoryTopic>()
                .HasKey(rt => new {rt.RepositoryId, rt.TopicId});

            modelBuilder.Entity<RepositoryTopic>()
                .HasOne<Repository>(rt => rt.Repository)
                .WithMany(r => r.RepositoryTopics)
                .HasForeignKey(rt => rt.RepositoryId);

            modelBuilder.Entity<RepositoryTopic>()
                .HasOne<Topic>(rt => rt.Topic)
                .WithMany(r => r.RepositoryTopics)
                .HasForeignKey(rt => rt.TopicId);

            modelBuilder.Entity<Repository>()
                .HasIndex(r => r.Url)
                .IsUnique();

            modelBuilder.Entity<Topic>()
                .Property(t => t.TopicId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Topic>()
                .HasIndex(t => t.TopicName)
                .IsUnique();

            //seeddata
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);


        }
    //    public void Initialize()
    //    {
    //        SeedData.Initialize(this);
    //    }
    }

   
}
