using Microsoft.EntityFrameworkCore;
using StreamingAPI.Models;

public class StreamingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Creator> Creators { get; set; }

    public StreamingContext(DbContextOptions<StreamingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacionamento entre Playlist e Usuario
        modelBuilder.Entity<Playlist>()
            .HasOne<User>() // Define o relacionamento com a entidade User
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.UserId) // Define a chave estrangeira
            .OnDelete(DeleteBehavior.Cascade); // Exclusão em cascata, se necessário

        // Relacionamento entre Conteudo e Criador
        modelBuilder.Entity<Content>()
            .HasOne(c => c.Creator)
            .WithMany(cr => cr.Contents);
    }

}


