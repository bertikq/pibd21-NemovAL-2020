using AbstractTravelCompanyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AbstractTravelCompanyDatabaseImplement
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=TravelCompany50;Username=postgres;Password=postgres");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<TourComponent> TourComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Store> Stores { set; get; }
        public virtual DbSet<StoreComponent> StoreComponents { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<MessageInfo> MessageInfos { get; set; }
    }
}
