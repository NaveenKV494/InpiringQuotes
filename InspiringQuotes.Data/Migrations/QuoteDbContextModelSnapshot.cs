﻿// <auto-generated />
using InspiringQuotes.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InspiringQuotes.Data.Migrations
{
    [DbContext(typeof(QuoteDbContext))]
    partial class QuoteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("InspiringQuotes.Data.Models.Quote", b =>
                {
                    b.Property<int>("QuoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuoteText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("QuoteId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("InspiringQuotes.Data.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("QuoteTag", b =>
                {
                    b.Property<int>("QuotesQuoteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("QuotesQuoteId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("QuoteTag");
                });

            modelBuilder.Entity("QuoteTag", b =>
                {
                    b.HasOne("InspiringQuotes.Data.Models.Quote", null)
                        .WithMany()
                        .HasForeignKey("QuotesQuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InspiringQuotes.Data.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
