﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teplater.SQLite.Context;

namespace Teplater.SQLite.Migrations
{
    [DbContext(typeof(TeplaterSQLDB))]
    [Migration("20220201042311_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Templator.DTO.DTOModels.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeInitial")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ValuesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.HasIndex("ValuesId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MarkKeysId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MarkKeysId");

                    b.ToTable("MarkKey");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkKeys", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MarkKeys");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MarkValuesId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MarkValuesId");

                    b.ToTable("MarkValue");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkValues", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MarkValues");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("KeysId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("KeysId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.Document", b =>
                {
                    b.HasOne("Templator.DTO.DTOModels.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId");

                    b.HasOne("Templator.DTO.DTOModels.MarkValues", "Values")
                        .WithMany()
                        .HasForeignKey("ValuesId");

                    b.Navigation("Template");

                    b.Navigation("Values");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkKey", b =>
                {
                    b.HasOne("Templator.DTO.DTOModels.MarkKeys", null)
                        .WithMany("Keys")
                        .HasForeignKey("MarkKeysId");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkValue", b =>
                {
                    b.HasOne("Templator.DTO.DTOModels.MarkValues", null)
                        .WithMany("Values")
                        .HasForeignKey("MarkValuesId");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.Template", b =>
                {
                    b.HasOne("Templator.DTO.DTOModels.MarkKeys", "Keys")
                        .WithMany()
                        .HasForeignKey("KeysId");

                    b.Navigation("Keys");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkKeys", b =>
                {
                    b.Navigation("Keys");
                });

            modelBuilder.Entity("Templator.DTO.DTOModels.MarkValues", b =>
                {
                    b.Navigation("Values");
                });
#pragma warning restore 612, 618
        }
    }
}