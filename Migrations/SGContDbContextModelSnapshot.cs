﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SGCont.Data;

namespace SGCont.Migrations
{
    [DbContext(typeof(SGContDbContext))]
    partial class SGContDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "009da871-1a31-434c-bdee-279545f4f75f",
                            Name = "administrador",
                            NormalizedName = "ADMINISTRADOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "f42559a2-2776-4e9b-9ba1-268597eff72b",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SGCont.Models.AdminContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminContratoId");

                    b.Property<int>("DepartamentoId");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("AdminContratos");
                });

            modelBuilder.Entity("SGCont.Models.CaracteristicasTrab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColorDeOjos");

                    b.Property<int>("ColorDePiel");

                    b.Property<byte[]>("Foto");

                    b.Property<string>("OtrasCaracteristicas");

                    b.Property<double?>("TallaCalzado");

                    b.Property<int>("TallaDeCamisa");

                    b.Property<string>("TallaPantalon");

                    b.Property<int?>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("TrabajadorId")
                        .IsUnique();

                    b.ToTable("caracteristicas_del_trabjador");
                });

            modelBuilder.Entity("SGCont.Models.ComiteContratacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<int>("TrabComiteContratacionId");

                    b.HasKey("Id");

                    b.ToTable("ComiteContratacion");
                });

            modelBuilder.Entity("SGCont.Models.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminContratoId");

                    b.Property<bool>("AprobComitContratacion");

                    b.Property<bool>("AprobEconomico");

                    b.Property<bool>("AprobJuridico");

                    b.Property<bool>("Cliente");

                    b.Property<int>("EntidadId");

                    b.Property<int>("Estado");

                    b.Property<DateTime>("FechaDeFirmado");

                    b.Property<DateTime>("FechaDeRecepcion");

                    b.Property<DateTime>("FechaDeVenOferta");

                    b.Property<DateTime>("FechaVenContrato");

                    b.Property<string>("FilePath");

                    b.Property<string>("Nombre");

                    b.Property<string>("Numero");

                    b.Property<string>("ObjetoDeContrato");

                    b.Property<int>("TerminoDePago");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("AdminContratoId");

                    b.HasIndex("EntidadId");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("SGCont.Models.ContratoId_DepartamentoId", b =>
                {
                    b.Property<int>("ContratoId");

                    b.Property<int>("DepartamentoId");

                    b.HasKey("ContratoId", "DepartamentoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("ContratoId_DepartamentoId");
                });

            modelBuilder.Entity("SGCont.Models.ContratoId_FormaPagoId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContratoId");

                    b.Property<int>("FormaDePago");

                    b.Property<int>("FormaDePagoId");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("ContratoId_FormaPagoId");
                });

            modelBuilder.Entity("SGCont.Models.CuentaBancaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EntidadId");

                    b.Property<int>("Moneda");

                    b.Property<int>("NombreSucursal");

                    b.Property<string>("NumeroCuenta");

                    b.Property<string>("NumeroSucursal");

                    b.HasKey("Id");

                    b.HasIndex("EntidadId");

                    b.ToTable("CuentasBancarias");
                });

            modelBuilder.Entity("SGCont.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("SGCont.Models.Dictamen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aprobado");

                    b.Property<string>("Consideraciones");

                    b.Property<int>("EspecialistaId");

                    b.Property<string>("FundamentosDeDerecho")
                        .IsRequired();

                    b.Property<string>("NumeroDeDictamen");

                    b.Property<string>("Observaciones");

                    b.Property<string>("OtrosSi");

                    b.Property<string>("Recomendaciones");

                    b.HasKey("Id");

                    b.HasIndex("EspecialistaId");

                    b.ToTable("Dictamen");
                });

            modelBuilder.Entity("SGCont.Models.DictaminadorContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartamentoId");

                    b.Property<int>("DictaminadorId");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("DictaminadoresContratos");
                });

            modelBuilder.Entity("SGCont.Models.Documento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminContratoId");

                    b.Property<string>("Dictamen");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime?>("FechaFirmado");

                    b.Property<DateTime?>("FechaVenContrato");

                    b.Property<decimal?>("MontoCuc");

                    b.Property<decimal?>("MontoCup");

                    b.Property<string>("NoOficial");

                    b.Property<string>("Numero");

                    b.Property<string>("RevisionActual");

                    b.HasKey("Id");

                    b.HasIndex("AdminContratoId");

                    b.ToTable("Documentos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Documento");
                });

            modelBuilder.Entity("SGCont.Models.Entidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CI");

                    b.Property<string>("CarnetTCP");

                    b.Property<string>("Codigo");

                    b.Property<string>("Correo");

                    b.Property<string>("Direccion")
                        .IsRequired();

                    b.Property<string>("Fax");

                    b.Property<string>("Nit")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("ObjetoSocial");

                    b.Property<int>("Sector");

                    b.Property<string>("Siglas");

                    b.HasKey("Id");

                    b.ToTable("Entidades");
                });

            modelBuilder.Entity("SGCont.Models.EspExternoId_ContratoId", b =>
                {
                    b.Property<int>("ContratoId");

                    b.Property<int>("EspecialistaExternoId");

                    b.Property<int>("Id");

                    b.HasKey("ContratoId", "EspecialistaExternoId");

                    b.HasIndex("EspecialistaExternoId");

                    b.ToTable("EspecialistaExternoId_ContratoId");
                });

            modelBuilder.Entity("SGCont.Models.Especialidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DocumentoId");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.ToTable("cont_especialidades");
                });

            modelBuilder.Entity("SGCont.Models.Especialista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("DetallesEspecialista");

                    b.Property<int>("EspecialidadId");

                    b.Property<int>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadId");

                    b.ToTable("Especialista");
                });

            modelBuilder.Entity("SGCont.Models.EspecialistaExterno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos")
                        .IsRequired();

                    b.Property<string>("Area");

                    b.Property<string>("Cargo");

                    b.Property<string>("Departamento");

                    b.Property<int>("EntidadId");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EntidadId");

                    b.ToTable("EspecialistasExternos");
                });

            modelBuilder.Entity("SGCont.Models.HistoricoDeDocumento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Detalles");

                    b.Property<int>("DocumentoId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.ToTable("HistoricoDeDocumento");
                });

            modelBuilder.Entity("SGCont.Models.HistoricoEstadoContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContratoId");

                    b.Property<int>("Estado");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Usuario");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("HistoricosEstadoContratos");
                });

            modelBuilder.Entity("SGCont.Models.Licencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Aplicacion");

                    b.Property<byte[]>("Hash");

                    b.Property<string>("Subscriptor");

                    b.Property<DateTime>("Vencimiento");

                    b.HasKey("Id");

                    b.ToTable("Licencias");
                });

            modelBuilder.Entity("SGCont.Models.Monto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cantidad");

                    b.Property<int>("ContratoId");

                    b.Property<int>("Moneda");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("Montos");
                });

            modelBuilder.Entity("SGCont.Models.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<int>("ProvinciaId");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("municipios");
                });

            modelBuilder.Entity("SGCont.Models.ObjetoDeContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DocumentoId");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DocumentoId");

                    b.ToTable("ObjetoDeContrato");
                });

            modelBuilder.Entity("SGCont.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("provincias");
                });

            modelBuilder.Entity("SGCont.Models.Telefono", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EntidadId");

                    b.Property<string>("Extension");

                    b.Property<string>("Numero");

                    b.HasKey("Id");

                    b.HasIndex("EntidadId");

                    b.ToTable("Telefonos");
                });

            modelBuilder.Entity("SGCont.Models.Trabajador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos")
                        .IsRequired();

                    b.Property<string>("CI")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Codigo");

                    b.Property<string>("Correo");

                    b.Property<string>("Direccion");

                    b.Property<int?>("EstadoTrabajador");

                    b.Property<DateTime>("Fecha_Nac");

                    b.Property<int?>("MunicipioId");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<int?>("Sexo");

                    b.Property<string>("TelefonoFijo");

                    b.Property<string>("TelefonoMovil");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.ToTable("trabajadores");
                });

            modelBuilder.Entity("TiempoVenContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContratoTiempo");

                    b.Property<int>("ContratosCasiVencDesde");

                    b.Property<int>("ContratosCasiVencHasta");

                    b.Property<int>("ContratosProxVencerDesde");

                    b.Property<int>("ContratosProxVencerHasta");

                    b.Property<int>("ContratosVencidos");

                    b.HasKey("Id");

                    b.ToTable("TiempoVenContratos");
                });

            modelBuilder.Entity("TiempoVenOferta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OfertaTiempo");

                    b.Property<int>("OfertasCasiVencDesde");

                    b.Property<int>("OfertasCasiVencHasta");

                    b.Property<int>("OfertasProxVencDesde");

                    b.Property<int>("OfertasProxVencHasta");

                    b.Property<int>("OfertasVencidas");

                    b.HasKey("Id");

                    b.ToTable("TiempoVenOfertas");
                });

            modelBuilder.Entity("SGCont.Models.Usuario", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Activo");

                    b.Property<string>("Apellidos")
                        .IsRequired();

                    b.Property<string>("Nombres")
                        .IsRequired();

                    b.HasDiscriminator().HasValue("Usuario");

                    b.HasData(
                        new
                        {
                            Id = "f42559a2-2776-4e9b-9ba1-268597eff72b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "36fd2616-8e8a-4cc6-8a5a-52d963207836",
                            Email = "admin@SGCont.cu",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@SGCONT.CU",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEP4OedI6m26WUn/2C4AcBkzdT6SnL/6E+xakQ/9mGAkqqp3t9PwyIR6l9obLouKIVg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "43VMKYQKNTENYZVJNU2TII26X23H5PGV",
                            TwoFactorEnabled = false,
                            UserName = "admin",
                            Activo = true,
                            Apellidos = "General",
                            Nombres = "Administrador"
                        });
                });

            modelBuilder.Entity("SGCont.Models.Suplemento", b =>
                {
                    b.HasBaseType("SGCont.Models.Documento");

                    b.Property<int>("ContratoId");

                    b.HasIndex("ContratoId");

                    b.HasDiscriminator().HasValue("Suplemento");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.AdminContrato", b =>
                {
                    b.HasOne("SGCont.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.CaracteristicasTrab", b =>
                {
                    b.HasOne("SGCont.Models.Trabajador", "Trabajador")
                        .WithOne("CaracteristicasTrab")
                        .HasForeignKey("SGCont.Models.CaracteristicasTrab", "TrabajadorId");
                });

            modelBuilder.Entity("SGCont.Models.Contrato", b =>
                {
                    b.HasOne("SGCont.Models.Trabajador", "AdminContrato")
                        .WithMany()
                        .HasForeignKey("AdminContratoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SGCont.Models.Entidad", "Entidad")
                        .WithMany()
                        .HasForeignKey("EntidadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.ContratoId_DepartamentoId", b =>
                {
                    b.HasOne("SGCont.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SGCont.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.ContratoId_FormaPagoId", b =>
                {
                    b.HasOne("SGCont.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.CuentaBancaria", b =>
                {
                    b.HasOne("SGCont.Models.Entidad", "Entidad")
                        .WithMany("CuentasBancarias")
                        .HasForeignKey("EntidadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.Dictamen", b =>
                {
                    b.HasOne("SGCont.Models.Especialista", "Especialista")
                        .WithMany("Dictamenes")
                        .HasForeignKey("EspecialistaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.DictaminadorContrato", b =>
                {
                    b.HasOne("SGCont.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.Documento", b =>
                {
                    b.HasOne("SGCont.Models.AdminContrato", "AdminContrato")
                        .WithMany()
                        .HasForeignKey("AdminContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.EspExternoId_ContratoId", b =>
                {
                    b.HasOne("SGCont.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SGCont.Models.EspecialistaExterno", "EspecialistaExterno")
                        .WithMany("EspExternoId_ContratoId")
                        .HasForeignKey("EspecialistaExternoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.Especialidad", b =>
                {
                    b.HasOne("SGCont.Models.Documento")
                        .WithMany("Especialidades")
                        .HasForeignKey("DocumentoId");
                });

            modelBuilder.Entity("SGCont.Models.Especialista", b =>
                {
                    b.HasOne("SGCont.Models.Especialidad", "Especialidad")
                        .WithMany("Especialistas")
                        .HasForeignKey("EspecialidadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.EspecialistaExterno", b =>
                {
                    b.HasOne("SGCont.Models.Entidad", "Entidad")
                        .WithMany()
                        .HasForeignKey("EntidadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.HistoricoDeDocumento", b =>
                {
                    b.HasOne("SGCont.Models.Documento", "Documento")
                        .WithMany("Historicos")
                        .HasForeignKey("DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.HistoricoEstadoContrato", b =>
                {
                    b.HasOne("SGCont.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.Monto", b =>
                {
                    b.HasOne("SGCont.Models.Contrato", "Contrato")
                        .WithMany("Montos")
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.Municipio", b =>
                {
                    b.HasOne("SGCont.Models.Provincia", "Provincia")
                        .WithMany("Municipios")
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGCont.Models.ObjetoDeContrato", b =>
                {
                    b.HasOne("SGCont.Models.Documento")
                        .WithMany("ObjetosDeContrato")
                        .HasForeignKey("DocumentoId");
                });

            modelBuilder.Entity("SGCont.Models.Telefono", b =>
                {
                    b.HasOne("SGCont.Models.Entidad", "Entidad")
                        .WithMany("Telefonos")
                        .HasForeignKey("EntidadId");
                });

            modelBuilder.Entity("SGCont.Models.Trabajador", b =>
                {
                    b.HasOne("SGCont.Models.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId");
                });

            modelBuilder.Entity("SGCont.Models.Suplemento", b =>
                {
                    b.HasOne("SGCont.Models.Contrato", "Contrato")
                        .WithMany("Suplementos")
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
