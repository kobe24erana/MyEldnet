﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyMvcApplication.Data;

#nullable disable

namespace My_MVC_Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231122064449_initialSetup")]
    partial class initialSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyMvcApplication.Models.EnrollmentDetail", b =>
                {
                    b.Property<long>("ENRDSTUDID")
                        .HasColumnType("bigint");

                    b.Property<string>("ENRDSTUDEDPCODE")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ENRDSTUDSTATUS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ENRDSTUDSUBJCODE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ENRDSTUDID", "ENRDSTUDEDPCODE");

                    b.ToTable("EnrollmentDetails");
                });

            modelBuilder.Entity("MyMvcApplication.Models.EnrollmentHeader", b =>
                {
                    b.Property<long>("ENRHSTUDID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ENRHSTUDID"));

                    b.Property<DateTime>("ENRHSTUDDATEENROLL")
                        .HasColumnType("datetime2");

                    b.Property<string>("ENRHSTUDENCODER")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ENRHSTUDSCHLYR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ENRHSTUDSTATUS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ENRHSTUDTOTALUNITS")
                        .HasColumnType("float");

                    b.HasKey("ENRHSTUDID");

                    b.ToTable("EnrollmentHeaderFiles");
                });

            modelBuilder.Entity("MyMvcApplication.Models.Student", b =>
                {
                    b.Property<string>("STUDID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STUDCOURSE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STUDFNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STUDLNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STUDMNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STUDREMARKS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STUDSTATUS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("STUDYEAR")
                        .HasColumnType("int");

                    b.HasKey("STUDID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MyMvcApplication.Models.SubjectAndSubjectPreq", b =>
                {
                    b.Property<string>("SUBJCODE")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SUBJCOURSECODE")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SFSUBJCATEGORY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SUBJCATEGORY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SUBJCURRCODE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SUBJDESC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SUBJPRECODE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SUBJREGOFRNG")
                        .HasColumnType("int");

                    b.Property<string>("SUBJSTATUS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SUBJUNITS")
                        .HasColumnType("int");

                    b.HasKey("SUBJCODE", "SUBJCOURSECODE");

                    b.ToTable("SubjectAndSubjectPreqs");
                });

            modelBuilder.Entity("MyMvcApplication.Models.SubjectSched", b =>
                {
                    b.Property<string>("EDPCODE")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CLASSSIZE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DAYS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ENDTIME")
                        .HasColumnType("datetime2");

                    b.Property<string>("FXM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MAXSIZE")
                        .HasColumnType("int");

                    b.Property<string>("ROOM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SCHOOLYEAR")
                        .HasColumnType("int");

                    b.Property<string>("SECTION")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("STARTTIME")
                        .HasColumnType("datetime2");

                    b.Property<string>("STATUS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SUBJCODE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EDPCODE");

                    b.ToTable("SubjectScheds");
                });
#pragma warning restore 612, 618
        }
    }
}
