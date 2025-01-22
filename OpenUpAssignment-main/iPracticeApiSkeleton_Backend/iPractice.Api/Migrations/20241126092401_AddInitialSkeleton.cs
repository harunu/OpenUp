using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace iPractice.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialSkeleton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    AssignedPsychologistIds = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Psychologists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    AssignedClientIds = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psychologists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientScheduledAppointments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PsychologistId = table.Column<long>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<long>(type: "INTEGER", nullable: false),
                    From = table.Column<DateTime>(type: "TEXT", nullable: false),
                    To = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScheduledAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientScheduledAppointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableTimeSlotsOfPsychologists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PsychologistId = table.Column<long>(type: "INTEGER", nullable: false),
                    From = table.Column<DateTime>(type: "TEXT", nullable: false),
                    To = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableTimeSlotsOfPsychologists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableTimeSlotsOfPsychologists_Psychologists_PsychologistId",
                        column: x => x.PsychologistId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookedAppointmentsOfPsychologists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<long>(type: "INTEGER", nullable: false),
                    PsychologistId = table.Column<long>(type: "INTEGER", nullable: false),
                    From = table.Column<DateTime>(type: "TEXT", nullable: false),
                    To = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedAppointmentsOfPsychologists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookedAppointmentsOfPsychologists_Psychologists_PsychologistId",
                        column: x => x.PsychologistId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableTimeSlotsOfPsychologists_PsychologistId",
                table: "AvailableTimeSlotsOfPsychologists",
                column: "PsychologistId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedAppointmentsOfPsychologists_PsychologistId",
                table: "BookedAppointmentsOfPsychologists",
                column: "PsychologistId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientScheduledAppointments_ClientId",
                table: "ClientScheduledAppointments",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableTimeSlotsOfPsychologists");

            migrationBuilder.DropTable(
                name: "BookedAppointmentsOfPsychologists");

            migrationBuilder.DropTable(
                name: "ClientScheduledAppointments");

            migrationBuilder.DropTable(
                name: "Psychologists");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
