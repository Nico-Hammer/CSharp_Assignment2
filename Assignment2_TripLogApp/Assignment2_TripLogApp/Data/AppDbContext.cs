using Microsoft.EntityFrameworkCore;
using Assignment2_TripLogApp.Models;
namespace Assignment2_TripLogApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<TripLog> TripLogs { get; set; }
}