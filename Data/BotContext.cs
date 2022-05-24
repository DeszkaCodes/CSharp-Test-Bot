using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using TestBot.Models;

namespace TestBot.Data;

public class BotContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = Program.DBPath };
        var connectionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connectionString);
        optionsBuilder.UseSqlite(connection);
    }
}
