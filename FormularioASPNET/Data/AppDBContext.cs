using Microsoft.EntityFrameworkCore;
using FormularioASPNET.Models;
using Microsoft.Extensions.Options;

namespace FormularioASPNET.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Alumno> Alumnos { get; set; }

        public DbSet<Asistencia> Asistencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alumno>(tb =>
            {
                tb.HasKey(col => col.IdAlumno);

                tb.Property(col  => col.IdAlumno)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.NombreAlumno).IsRequired().HasMaxLength(100);

                //tb.ToTable("Alumno");

            });
            modelBuilder.Entity<Alumno>().ToTable("Alumno");

            modelBuilder.Entity<Asistencia>(tb =>
            {
                tb.HasKey(a => a.IdAsistencia);
                tb.HasOne(a => a.Alumno)
                  .WithMany(al => al.Asistencias)
                  .HasForeignKey(a => a.IdAlumno);
                
            });
            modelBuilder.Entity<Asistencia>().ToTable("Asistencia");
        }



    }
}
