﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Subscriptions.Features.Infrastructure.EFCore;

#nullable disable

namespace Modules.Subscriptions.Features.Infrastructure.EFCore.Migrations
{
    [DbContext(typeof(SubscriptionsDbContext))]
    partial class SubscriptionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Subscriptions")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.StripeCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("StripePortalCustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("StripeCustomers", "Subscriptions");
                });

            modelBuilder.Entity("Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.StripeSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("PlanType")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("StripeCustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StripePortalSubscriptionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StripeCustomerId");

                    b.ToTable("StripeSubscriptions", "Subscriptions");
                });

            modelBuilder.Entity("Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.StripeSubscription", b =>
                {
                    b.HasOne("Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.StripeCustomer", "StripeCustomer")
                        .WithMany()
                        .HasForeignKey("StripeCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StripeCustomer");
                });
#pragma warning restore 612, 618
        }
    }
}
