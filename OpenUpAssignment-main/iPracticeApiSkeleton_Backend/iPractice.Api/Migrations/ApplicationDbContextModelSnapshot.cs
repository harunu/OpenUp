﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iPractice.Api;

#nullable disable

namespace iPractice.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("iPractice.Api.Models.Clients.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("iPractice.Api.Models.Psychologists.Psychologist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Psychologists");
                });

            modelBuilder.Entity("iPractice.Api.Models.Clients.Client", b =>
                {
                    b.OwnsOne("iPractice.Api.Models.Clients.Calendar", "Calendar", b1 =>
                        {
                            b1.Property<long>("ClientId")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsInitialized")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER")
                                .HasDefaultValue(true);

                            b1.HasKey("ClientId");

                            b1.ToTable("Clients");

                            b1.WithOwner()
                                .HasForeignKey("ClientId");

                            b1.OwnsMany("iPractice.Api.Models.Clients.Appointment", "Appointments", b2 =>
                                {
                                    b2.Property<string>("Id")
                                        .HasColumnType("TEXT");

                                    b2.Property<long>("ClientId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("From")
                                        .HasColumnType("TEXT");

                                    b2.Property<long>("PsychologistId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("To")
                                        .HasColumnType("TEXT");

                                    b2.HasKey("Id");

                                    b2.HasIndex("ClientId");

                                    b2.ToTable("ClientScheduledAppointments", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("ClientId");
                                });

                            b1.Navigation("Appointments");
                        });

                    b.OwnsOne("iPractice.Api.Models.Clients.PsychologistAssignment", "PsychologistAssignment", b1 =>
                        {
                            b1.Property<long>("ClientId")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsAssigned")
                                .HasColumnType("INTEGER");

                            b1.PrimitiveCollection<string>("PsychologistIds")
                                .HasColumnType("TEXT")
                                .HasColumnName("AssignedPsychologistIds");

                            b1.HasKey("ClientId");

                            b1.ToTable("Clients");

                            b1.WithOwner()
                                .HasForeignKey("ClientId");
                        });

                    b.Navigation("Calendar");

                    b.Navigation("PsychologistAssignment");
                });

            modelBuilder.Entity("iPractice.Api.Models.Psychologists.Psychologist", b =>
                {
                    b.OwnsOne("iPractice.Api.Models.Psychologists.Calendar", "Calendar", b1 =>
                        {
                            b1.Property<long>("PsychologistId")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsInitialized")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER")
                                .HasDefaultValue(true);

                            b1.HasKey("PsychologistId");

                            b1.ToTable("Psychologists");

                            b1.WithOwner()
                                .HasForeignKey("PsychologistId");

                            b1.OwnsMany("AvailableTimeSlot", "AvailableTimeSlots", b2 =>
                                {
                                    b2.Property<string>("Id")
                                        .HasColumnType("TEXT");

                                    b2.Property<DateTime>("From")
                                        .HasColumnType("TEXT");

                                    b2.Property<long?>("PsychologistId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("To")
                                        .HasColumnType("TEXT");

                                    b2.HasKey("Id");

                                    b2.HasIndex("PsychologistId");

                                    b2.ToTable("AvailableTimeSlotsOfPsychologists", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("PsychologistId");
                                });

                            b1.OwnsMany("iPractice.Api.Models.Psychologists.BookedAppointment", "BookedAppointments", b2 =>
                                {
                                    b2.Property<string>("Id")
                                        .HasColumnType("TEXT");

                                    b2.Property<long>("ClientId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("From")
                                        .HasColumnType("TEXT");

                                    b2.Property<long>("PsychologistId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateTime>("To")
                                        .HasColumnType("TEXT");

                                    b2.HasKey("Id");

                                    b2.HasIndex("PsychologistId");

                                    b2.ToTable("BookedAppointmentsOfPsychologists", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("PsychologistId");
                                });

                            b1.Navigation("AvailableTimeSlots");

                            b1.Navigation("BookedAppointments");
                        });

                    b.OwnsOne("iPractice.Api.Models.Psychologists.ClientAssignment", "ClientAssignment", b1 =>
                        {
                            b1.Property<long>("PsychologistId")
                                .HasColumnType("INTEGER");

                            b1.PrimitiveCollection<string>("ClientIds")
                                .HasColumnType("TEXT")
                                .HasColumnName("AssignedClientIds");

                            b1.Property<bool>("IsAssigned")
                                .HasColumnType("INTEGER");

                            b1.HasKey("PsychologistId");

                            b1.ToTable("Psychologists");

                            b1.WithOwner()
                                .HasForeignKey("PsychologistId");
                        });

                    b.Navigation("Calendar");

                    b.Navigation("ClientAssignment");
                });
#pragma warning restore 612, 618
        }
    }
}
