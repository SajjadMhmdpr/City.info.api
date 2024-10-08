using City.info.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace City.info.api.DbContexts
{
    public class CityInfoDbContext : DbContext
    {
        public CityInfoDbContext(
            DbContextOptions<CityInfoDbContext> options) : base(options)
        {

        }
        public DbSet<Entities.City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.City>().HasData(

                    new Entities.City("tehran")
                    {
                        Id = 1,
                        Description = "this is tehran"
                    },
                    new Entities.City("amol")
                    {
                        Id = 2,
                        Description = "this is amol"
                    },
                    new Entities.City("noor")
                    {
                        Id = 3,
                        Description = "this is noor"
                    }
                );


            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("niyavaran")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "this is niyavaran"
                },
                 new PointOfInterest("baliran")
                 {
                     Id = 2,
                     CityId = 2,
                     Description = "this is baliran"
                 },
                  new PointOfInterest("kojor")
                  {
                      Id = 3,
                      CityId = 3,
                      Description = "this is kojor"
                  }
                );

        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
