using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WalkingTec.Mvvm.Core;
using Blog.Model;

namespace Blog
{
    public class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMajor> StudentMajors { get; set; }

        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Student>()
            .HasMany(s => s.Majors)
            .WithMany(m => m.Students)
            .UsingEntity<StudentMajor>(
                j => j.HasOne(sm => sm.Major)
                    .WithMany(m => m.StudentMajors)
                    .HasForeignKey(sm => sm.MajorId),
                j => j.HasOne(sm => sm.Student)
                    .WithMany(s => s.StudentMajors)
                    .HasForeignKey(sm => sm.StudentId),
                j =>
                {
                    j.Property(sm => sm.score).HasDefaultValue(0);
                    j.HasIndex(t => t.MajorId);
                    j.HasIndex(t => t.StudentId);
                    j.HasIndex(t => new { t.StudentId, t.MajorId }).IsUnique();
                });
        }

        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };

                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();
            }
            return state;
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("your full connection string", DBTypeEnum.SqlServer);
        }
    }

}
