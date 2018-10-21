using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCorePet.Model
{
    public partial class veterinarioserviceContext : DbContext
    {
        public veterinarioserviceContext()
        {
        }

        public veterinarioserviceContext(DbContextOptions<veterinarioserviceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autenticacao> Autenticacao { get; set; }
        public virtual DbSet<ClienteComercio> ClienteComercio { get; set; }
        public virtual DbSet<ClientePessoa> ClientePessoa { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<PetFotos> PetFotos { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=petapicore_db_1;database=veterinarioservice;user=rodrigo;password=VetDB-436");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autenticacao>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Autenticacao");

                entity.HasIndex(e => e.Id)
                    .HasName("ID")
                    .IsUnique();

                entity.Property(e => e.Email).HasColumnType("varchar(100)");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnType("char(255)");
            });

            modelBuilder.Entity<ClienteComercio>(entity =>
            {
                entity.HasKey(e => e.UsuarioEmail);

                entity.ToTable("Cliente_Comercio");

                entity.HasIndex(e => e.Id)
                    .HasName("ID")
                    .IsUnique();

                entity.Property(e => e.UsuarioEmail)
                    .HasColumnName("Usuario_Email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.SaldoEmConta)
                    .HasColumnName("Saldo_Em_Conta")
                    .HasColumnType("decimal(14,2)");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.ClienteComercio)
                    .HasForeignKey<ClienteComercio>(d => d.UsuarioEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cliente_comercio_ibfk_1");
            });

            modelBuilder.Entity<ClientePessoa>(entity =>
            {
                entity.ToTable("Cliente_Pessoa");

                entity.HasIndex(e => e.UsuarioEmail)
                    .HasName("Usuario_Email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsuarioEmail)
                    .IsRequired()
                    .HasColumnName("Usuario_Email")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.ClientePessoa)
                    .HasForeignKey<ClientePessoa>(d => d.UsuarioEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cliente_pessoa_ibfk_1");
            });

            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.ToTable("Consulta");

                entity.HasIndex(e => e.ClienteComercioEmail)
                    .HasName("Cliente_comercio_Email");

                entity.HasIndex(e => e.ClientePessoaEmail)
                    .HasName("cliente_pessoa_Email");

                entity.HasIndex(e => e.ServicoId)
                    .HasName("servico_ID");

                entity.Property(e => e.ConsultaId)
                    .HasColumnName("Consulta_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClienteComercioEmail)
                    .IsRequired()
                    .HasColumnName("Cliente_comercio_Email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ClientePessoaEmail)
                    .IsRequired()
                    .HasColumnName("cliente_pessoa_Email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DataAgendamento)
                    .HasColumnName("Data_Agendamento")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataHora)
                    .HasColumnName("Data_Hora")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsCancelada).HasColumnType("varchar(10)");

                entity.Property(e => e.IsValida)
                    .HasColumnName("Is_Valida")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Motivo).HasColumnType("varchar(250)");

                entity.Property(e => e.PetsPetId)
                    .HasColumnName("Pets_pet_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Preco).HasColumnType("decimal(10,2)");

                entity.Property(e => e.Quemcancelou).HasColumnType("varchar(20)");

                entity.Property(e => e.ServicoId)
                    .HasColumnName("servico_ID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ClienteComercio)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ClienteComercioEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("consulta_ibfk_1");

                entity.HasOne(d => d.ClientePessoa)
                    .WithMany(p => p.Consulta)
                    .HasPrincipalKey(p => p.UsuarioEmail)
                    .HasForeignKey(d => d.ClientePessoaEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("consulta_ibfk_2");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ServicoId)
                    .HasConstraintName("consulta_ibfk_3");
            });

            modelBuilder.Entity<PetFotos>(entity =>
            {
                entity.ToTable("Pet_fotos");

                entity.HasIndex(e => e.PetId)
                    .HasName("Pet_id");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FotoCaminho).HasColumnType("varchar(200)");

                entity.Property(e => e.PetId)
                    .HasColumnName("Pet_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.PetFotos)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pet_fotos_ibfk_1");
            });

            modelBuilder.Entity<Pets>(entity =>
            {
                entity.HasKey(e => e.PetId);

                entity.ToTable("Pets");

                entity.HasIndex(e => e.ClientePessoaEmail)
                    .HasName("Cliente_pessoa_Email");

                entity.Property(e => e.PetId)
                    .HasColumnName("Pet_ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientePessoaEmail)
                    .IsRequired()
                    .HasColumnName("Cliente_pessoa_Email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Descricao).HasColumnType("varchar(250)");

                entity.Property(e => e.Especie).HasColumnType("varchar(50)");

                entity.Property(e => e.Genero).HasColumnType("varchar(20)");

                entity.Property(e => e.Idade).HasColumnType("varchar(500)");

                entity.Property(e => e.Nome).HasColumnType("varchar(100)");

                entity.Property(e => e.Peso).HasColumnType("varchar(50)");

                entity.Property(e => e.Raca).HasColumnType("varchar(50)");

                entity.Property(e => e.Tamanho).HasColumnType("varchar(20)");

                entity.HasOne(d => d.ClientePessoa)
                    .WithMany(p => p.Pets)
                    .HasPrincipalKey(p => p.UsuarioEmail)
                    .HasForeignKey(d => d.ClientePessoaEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pets_ibfk_1");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.ToTable("Services");

                entity.HasIndex(e => e.ClienteComercioEmail)
                    .HasName("Cliente_Comercio_Email");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClienteComercioEmail)
                    .IsRequired()
                    .HasColumnName("Cliente_Comercio_Email")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Descricao).HasColumnType("varchar(255)");

                entity.Property(e => e.Nome).HasColumnType("varchar(100)");

                entity.Property(e => e.Preco).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.ClienteComercio)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ClienteComercioEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("services_ibfk_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Id)
                    .HasName("ID")
                    .IsUnique();

                entity.Property(e => e.Email).HasColumnType("varchar(100)");

                entity.Property(e => e.Celular).HasColumnType("varchar(30)");

                entity.Property(e => e.Cep).HasColumnType("varchar(20)");

                entity.Property(e => e.CpfCnpj)
                    .HasColumnName("Cpf_Cnpj")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("Data_cadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.Endereco).HasColumnType("varchar(250)");

                entity.Property(e => e.Foto).HasColumnType("varchar(200)");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Idade).HasColumnType("int(11)");

                entity.Property(e => e.Nome).HasColumnType("varchar(100)");

                entity.HasOne(d => d.Autenticacao)
                    .WithOne(p => p.Usuario)
                    .HasForeignKey<Usuario>(d => d.Email)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_ibfk_1");
            });
        }
    }
}
