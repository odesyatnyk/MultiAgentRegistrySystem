﻿// <auto-generated />
using System;
using AgentRegistry.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgentRegistry.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.Agent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AgentId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgentTypeId");

                    b.Property<string>("IpAddress")
                        .IsRequired();

                    b.Property<int>("Port");

                    b.Property<int>("ScannerLogId");

                    b.HasKey("Id");

                    b.HasIndex("AgentTypeId");

                    b.HasIndex("ScannerLogId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.AgentCommand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AgentCommandId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgentTypeId");

                    b.Property<string>("CommandDescription");

                    b.Property<string>("CommandName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AgentTypeId");

                    b.ToTable("AgentCommands");
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.AgentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AgentTypeId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AgentTypeDescription");

                    b.Property<string>("AgentTypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AgentTypes");
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.AgentsCommunicationLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AgentCommunicationLogId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgentFromId");

                    b.Property<int>("AgentToId");

                    b.Property<int>("CommandId");

                    b.Property<DateTime>("DateTimeCommunication");

                    b.Property<string>("ErrorMessage");

                    b.Property<bool?>("IsSuccess");

                    b.Property<string>("StackTrace");

                    b.HasKey("Id");

                    b.HasIndex("AgentFromId");

                    b.HasIndex("AgentToId");

                    b.HasIndex("CommandId");

                    b.ToTable("AgentsCommunicationLogs");
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.ExceptionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ExceptionLogId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeLogging");

                    b.Property<string>("ErrorMessage")
                        .IsRequired();

                    b.Property<string>("InnerExceptionMessage");

                    b.Property<string>("InnerExceptionStackTrace");

                    b.Property<string>("StackTrace")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ExceptionLogs");
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.ScannerLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ScannerLogId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateTimeScanEnd");

                    b.Property<DateTime>("DateTimeScanStart");

                    b.Property<string>("ErrorMessage");

                    b.Property<bool?>("IsSuccess");

                    b.Property<string>("StackTrace");

                    b.HasKey("Id");

                    b.ToTable("ScannerLogs");
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.Agent", b =>
                {
                    b.HasOne("AgentRegistry.Core.System.Entities.AgentType", "AgentType")
                        .WithMany("Agents")
                        .HasForeignKey("AgentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgentRegistry.Core.System.Entities.ScannerLog", "ScannerLog")
                        .WithMany("Agents")
                        .HasForeignKey("ScannerLogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.AgentCommand", b =>
                {
                    b.HasOne("AgentRegistry.Core.System.Entities.AgentType", "AgentType")
                        .WithMany("Commands")
                        .HasForeignKey("AgentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgentRegistry.Core.System.Entities.AgentsCommunicationLog", b =>
                {
                    b.HasOne("AgentRegistry.Core.System.Entities.Agent", "AgentFrom")
                        .WithMany()
                        .HasForeignKey("AgentFromId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgentRegistry.Core.System.Entities.Agent", "AgentTo")
                        .WithMany()
                        .HasForeignKey("AgentToId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgentRegistry.Core.System.Entities.AgentCommand", "Command")
                        .WithMany()
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
