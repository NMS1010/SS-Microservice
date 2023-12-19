﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SS_Microservice.SagaOrchestration.DbContext;

#nullable disable

namespace SS_Microservice.SagaOrchestration.Migrations
{
    [DbContext(typeof(SagaAppDBContext))]
    partial class SagaAppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SS_Microservice.SagaOrchestration.StateInstances.Ordering.OrderingStateInstance", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CurrentState")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<string>("OrderCode")
                        .HasColumnType("longtext");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("Receiver")
                        .HasColumnType("longtext");

                    b.Property<string>("ReceiverEmail")
                        .HasColumnType("longtext");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("CorrelationId");

                    b.ToTable("OrderingStateInstance");
                });

            modelBuilder.Entity("SS_Microservice.SagaOrchestration.StateInstances.Ordering.ProductInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("OrderingStateInstanceCorrelationId")
                        .HasColumnType("char(36)");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("VariantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrderingStateInstanceCorrelationId");

                    b.ToTable("ProductInstance");
                });

            modelBuilder.Entity("SS_Microservice.SagaOrchestration.StateInstances.Ordering.ProductInstance", b =>
                {
                    b.HasOne("SS_Microservice.SagaOrchestration.StateInstances.Ordering.OrderingStateInstance", "OrderingStateInstance")
                        .WithMany("ProductInstances")
                        .HasForeignKey("OrderingStateInstanceCorrelationId");

                    b.Navigation("OrderingStateInstance");
                });

            modelBuilder.Entity("SS_Microservice.SagaOrchestration.StateInstances.Ordering.OrderingStateInstance", b =>
                {
                    b.Navigation("ProductInstances");
                });
#pragma warning restore 612, 618
        }
    }
}
