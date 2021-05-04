﻿// <auto-generated />
using System;
using Confab.Modules.Tickets.Core.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Confab.Modules.Tickets.Core.Dal.Migrations
{
    [DbContext(typeof(TicketsDbContext))]
    [Migration("20210503141011_Tickets_Module_Init")]
    partial class Tickets_Module_Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Tickets")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Conference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("From")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParticipantsLimit")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("To")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ConferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchasedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("TicketSaleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UsedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .IsConcurrencyToken()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasFilter("[Code] IS NOT NULL");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("TicketSaleId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.TicketSale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("ConferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("From")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.ToTable("TicketSales");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Ticket", b =>
                {
                    b.HasOne("Confab.Modules.Tickets.Core.Entities.Conference", "Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Confab.Modules.Tickets.Core.Entities.TicketSale", "TicketSale")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketSaleId");

                    b.Navigation("Conference");

                    b.Navigation("TicketSale");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.TicketSale", b =>
                {
                    b.HasOne("Confab.Modules.Tickets.Core.Entities.Conference", null)
                        .WithMany("TicketSales")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.Conference", b =>
                {
                    b.Navigation("TicketSales");
                });

            modelBuilder.Entity("Confab.Modules.Tickets.Core.Entities.TicketSale", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
