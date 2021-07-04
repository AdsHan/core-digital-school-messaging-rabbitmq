﻿// <auto-generated />
using System;
using DSC.IntegrationEventLog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DSC.IntegrationEventLog.Migrations
{
    [DbContext(typeof(IntegrationEventLogDbContext))]
    partial class IntegrationEventLogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DSC.IntegrationEventLog.Entities.IntegrationEventLogModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreateAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateDeleteAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublishStatus")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IntegrationEventLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
