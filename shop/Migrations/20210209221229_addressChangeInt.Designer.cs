﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shop.Models;

namespace shop.Migrations
{
    [DbContext(typeof(shopContext))]
    [Migration("20210209221229_addressChangeInt")]
    partial class addressChangeInt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("shop.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("address_id")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("country");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("street");

                    b.HasKey("AddressId");

                    b.ToTable("addresses");
                });

            modelBuilder.Entity("shop.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("last_name");

                    b.HasKey("AuthorId");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("shop.Models.AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("author_book");
                });

            modelBuilder.Entity("shop.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("description");

                    b.Property<string>("FigUrl")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("fig_url");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("genre_id");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int")
                        .HasColumnName("publisher_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("BookId");

                    b.HasIndex("GenreId");

                    b.HasIndex("PublisherId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("shop.Models.BooksOrdered", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.ToTable("books_ordered");
                });

            modelBuilder.Entity("shop.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("genre_id")
                        .UseIdentityColumn();

                    b.Property<string>("GenreName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("genre_name");

                    b.HasKey("GenreId");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("shop.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("log_id")
                        .UseIdentityColumn();

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("msg");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("timestamp");

                    b.HasKey("LogId");

                    b.HasIndex("OrderId");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("shop.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id")
                        .UseIdentityColumn();

                    b.Property<int>("BillingAddressId")
                        .HasColumnType("int")
                        .HasColumnName("billing_address_id");

                    b.Property<bool>("Payment")
                        .HasColumnType("bit")
                        .HasColumnName("payment");

                    b.Property<int>("ShippingAddressId")
                        .HasColumnType("int")
                        .HasColumnName("shipping_address_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("OrderId");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("ShippingAddressId");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("shop.Models.Publisher", b =>
                {
                    b.Property<int>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("publisher_id")
                        .UseIdentityColumn();

                    b.Property<string>("PublisherName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("publisher_name");

                    b.HasKey("PublisherId");

                    b.ToTable("publishers");
                });

            modelBuilder.Entity("shop.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Phone")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("phone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_name");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("shop.Models.AuthorBook", b =>
                {
                    b.HasOne("shop.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("author_book_FK_1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shop.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("author_book_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("shop.Models.Book", b =>
                {
                    b.HasOne("shop.Models.Genre", "Genre")
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("books_FK");

                    b.HasOne("shop.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("booksPub_FK");

                    b.Navigation("Genre");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("shop.Models.BooksOrdered", b =>
                {
                    b.HasOne("shop.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("books_ordered_FK_1")
                        .IsRequired();

                    b.HasOne("shop.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .HasConstraintName("books_ordered_FK")
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("shop.Models.Log", b =>
                {
                    b.HasOne("shop.Models.Order", "Order")
                        .WithMany("Logs")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("logs_FK")
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("shop.Models.Order", b =>
                {
                    b.HasOne("shop.Models.Address", "BillingAddress")
                        .WithMany("OrderBillingAddresses")
                        .HasForeignKey("BillingAddressId")
                        .HasConstraintName("orders_FK")
                        .IsRequired();

                    b.HasOne("shop.Models.Address", "ShippingAddress")
                        .WithMany("OrderShippingAddresses")
                        .HasForeignKey("ShippingAddressId")
                        .HasConstraintName("orders_FK_1")
                        .IsRequired();

                    b.HasOne("shop.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .HasConstraintName("orders_FK_2")
                        .IsRequired();

                    b.Navigation("BillingAddress");

                    b.Navigation("ShippingAddress");

                    b.Navigation("User");
                });

            modelBuilder.Entity("shop.Models.Address", b =>
                {
                    b.Navigation("OrderBillingAddresses");

                    b.Navigation("OrderShippingAddresses");
                });

            modelBuilder.Entity("shop.Models.Genre", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("shop.Models.Order", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("shop.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("shop.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
