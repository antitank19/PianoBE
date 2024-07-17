﻿// <auto-generated />
using System;
using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(PianoContext))]
    [Migration("20240717051933_RemoveChromaticProperty")]
    partial class RemoveChromaticProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataLayer.DbObject.Chord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<int>("MeasureId")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MeasureId");

                    b.ToTable("Chords");
                });

            modelBuilder.Entity("DataLayer.DbObject.ChordNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChordId")
                        .HasColumnType("int");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChordId");

                    b.HasIndex("NoteId");

                    b.ToTable("ChordNotes");
                });

            modelBuilder.Entity("DataLayer.DbObject.Instrument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("DataLayer.DbObject.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Clef")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("SheetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SheetId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("DataLayer.DbObject.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Octave")
                        .HasColumnType("int");

                    b.Property<string>("Pitch")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("DataLayer.DbObject.Sheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BottomSignature")
                        .HasColumnType("int");

                    b.Property<int>("InstrumentId")
                        .HasColumnType("int");

                    b.Property<int?>("LeftHandSheetId")
                        .HasColumnType("int");

                    b.Property<string>("SheetFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<int>("TopSignature")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstrumentId");

                    b.HasIndex("LeftHandSheetId");

                    b.HasIndex("SongId");

                    b.ToTable("Sheets");
                });

            modelBuilder.Entity("DataLayer.DbObject.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Composer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("DataLayer.DbObject.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("DataLayer.DbObject.Chord", b =>
                {
                    b.HasOne("DataLayer.DbObject.Measure", "Measure")
                        .WithMany("Chords")
                        .HasForeignKey("MeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Measure");
                });

            modelBuilder.Entity("DataLayer.DbObject.ChordNote", b =>
                {
                    b.HasOne("DataLayer.DbObject.Chord", "Chord")
                        .WithMany("ChordNotes")
                        .HasForeignKey("ChordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.DbObject.Note", "Note")
                        .WithMany()
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chord");

                    b.Navigation("Note");
                });

            modelBuilder.Entity("DataLayer.DbObject.Measure", b =>
                {
                    b.HasOne("DataLayer.DbObject.Sheet", "Sheet")
                        .WithMany("Measures")
                        .HasForeignKey("SheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sheet");
                });

            modelBuilder.Entity("DataLayer.DbObject.Sheet", b =>
                {
                    b.HasOne("DataLayer.DbObject.Instrument", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.DbObject.Sheet", "LeftHandSheet")
                        .WithMany()
                        .HasForeignKey("LeftHandSheetId");

                    b.HasOne("DataLayer.DbObject.Song", "Song")
                        .WithMany("Sheets")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instrument");

                    b.Navigation("LeftHandSheet");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("DataLayer.DbObject.Song", b =>
                {
                    b.HasOne("DataLayer.DbObject.User", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("DataLayer.DbObject.Chord", b =>
                {
                    b.Navigation("ChordNotes");
                });

            modelBuilder.Entity("DataLayer.DbObject.Measure", b =>
                {
                    b.Navigation("Chords");
                });

            modelBuilder.Entity("DataLayer.DbObject.Sheet", b =>
                {
                    b.Navigation("Measures");
                });

            modelBuilder.Entity("DataLayer.DbObject.Song", b =>
                {
                    b.Navigation("Sheets");
                });

            modelBuilder.Entity("DataLayer.DbObject.User", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
