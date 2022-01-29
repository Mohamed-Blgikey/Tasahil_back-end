using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasahil_.net_core_6_.Entity;
using Tasahil_.net_core_6_.Extend;

namespace Tasahil_.net_core_6_.Database
{
 

public class TasahilDBContext : IdentityDbContext<ApplicationUser>
{
    public TasahilDBContext(DbContextOptions<TasahilDBContext> opt) : base(opt)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SavedPosts>()
            .HasKey(a => new { a.PostId, a.ApplicationUserId });

        builder.Entity<ApplicationUser>()
            .HasMany(a => a.Posts)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);



        builder.Entity<Category>()
            .HasMany(a => a.Posts)
            .WithOne(a => a.Category)
            .HasForeignKey(a => a.CateId)
            .OnDelete(DeleteBehavior.SetNull);




    }
    public virtual DbSet<Post> Posts { set; get; }
    public virtual DbSet<Category> Categories { set; get; }
    public virtual DbSet<Comment> Comments { set; get; }
    public virtual DbSet<SavedPosts> SavedPosts { set; get; }

    }

}