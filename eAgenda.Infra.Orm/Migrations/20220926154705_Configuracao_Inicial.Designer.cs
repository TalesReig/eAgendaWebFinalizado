﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eAgenda.Infra.Orm;

namespace eAgenda.Infra.Orm.Migrations
{
    [DbContext(typeof(eAgendaDbContext))]
    [Migration("20220926154705_Configuracao_Inicial")]
    partial class Configuracao_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CategoriaDespesa", b =>
                {
                    b.Property<Guid>("CategoriasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DespesasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriasId", "DespesasId");

                    b.HasIndex("DespesasId");

                    b.ToTable("TBDespesa_TBCategoria");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloCompromisso.Compromisso", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Assunto")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<Guid?>("ContatoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<long>("HoraInicio")
                        .HasColumnType("bigint");

                    b.Property<long>("HoraTermino")
                        .HasColumnType("bigint");

                    b.Property<string>("Link")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Local")
                        .HasColumnType("varchar(300)");

                    b.Property<int>("TipoLocal")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContatoId");

                    b.ToTable("TBCompromisso");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloContato.Contato", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cargo")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Empresa")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TBContato");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloDespesa.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBCategoria");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloDespesa.Despesa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("FormaPagamento")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TBDespesa");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.ItemTarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Concluido")
                        .HasColumnType("bit");

                    b.Property<Guid>("TarefaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.ToTable("TBItemTarefa");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DataConclusao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PercentualConcluido")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBTarefa");
                });

            modelBuilder.Entity("CategoriaDespesa", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloDespesa.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAgenda.Dominio.ModuloDespesa.Despesa", null)
                        .WithMany()
                        .HasForeignKey("DespesasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloCompromisso.Compromisso", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloContato.Contato", "Contato")
                        .WithMany()
                        .HasForeignKey("ContatoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Contato");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.ItemTarefa", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloTarefa.Tarefa", "Tarefa")
                        .WithMany("Itens")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloTarefa.Tarefa", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}
