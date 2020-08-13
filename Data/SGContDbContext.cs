using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGCont.Models;

namespace SGCont.Data
{
    public class SGContDbContext : IdentityDbContext {
        public SGContDbContext (DbContextOptions<SGContDbContext> options) : base (options) {

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            modelBuilder.Entity<IdentityRole> ().HasData (new IdentityRole[] {
                new IdentityRole { Id = "1", Name = "administrador", NormalizedName = "ADMINISTRADOR" },
            });
            modelBuilder.Entity<Usuario> ().HasData (
                new Usuario {
                    Id = "f42559a2-2776-4e9b-9ba1-268597eff72b",
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        Email = "admin@SGCont.cu",
                        NormalizedEmail = "ADMIN@SGCONT.CU",
                        PasswordHash = "AQAAAAEAACcQAAAAEP4OedI6m26WUn/2C4AcBkzdT6SnL/6E+xakQ/9mGAkqqp3t9PwyIR6l9obLouKIVg==",
                        SecurityStamp = "43VMKYQKNTENYZVJNU2TII26X23H5PGV",
                        ConcurrencyStamp = "36fd2616-8e8a-4cc6-8a5a-52d963207836",
                        Activo = true,
                        Nombres = "Administrador",
                        Apellidos = "General",
                }
            );
            modelBuilder.Entity<IdentityUserRole<string>> ().HasData (
                new IdentityUserRole<string> {
                    UserId = "f42559a2-2776-4e9b-9ba1-268597eff72b",
                    RoleId = "1"
                }
            );
            modelBuilder.Entity<EspExternoId_ContratoId> ()
                .HasKey (x => new { x.ContratoId, x.EspecialistaExternoId });

            modelBuilder.ForNpgsqlUseIdentityColumns ();
            modelBuilder.Entity<ContratoId_DepartamentoId> ().HasKey (x => new { x.ContratoId, x.DepartamentoId });
            base.OnModelCreating (modelBuilder);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Licencia> Licencias { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<ContratoId_DepartamentoId> ContratoId_DepartamentoId { get; set; }
        public DbSet<DictaminadorContrato> DictaminadoresContratos { get; set; }
        public DbSet<Entidad> Entidades { get; set; }
        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
        public DbSet<EspecialistaExterno> EspecialistasExternos { get; set; }
        public DbSet<EspExternoId_ContratoId> EspExternoId_ContratoId { get; set; }
        public DbSet<ContratoId_FormaPagoId> ContratoId_FormaPagoId { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<HistoricoEstadoContrato> HistoricosEstadoContratos { get; set; }
        public DbSet<AdminContrato> AdminContratos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<TiempoVenOferta> TiempoVenOfertas { get; set; }
        public DbSet<TiempoVenContrato> TiempoVenContratos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<ComiteContratacion> ComiteContratacion { get; set; }
        public DbSet<Monto> Montos { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
    }
}