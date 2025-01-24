using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iPractice.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Calendar_IsInitialized",
                table: "Psychologists",
                type: "INTEGER",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ClientAssignment_IsAssigned",
                table: "Psychologists",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Calendar_IsInitialized",
                table: "Clients",
                type: "INTEGER",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "PsychologistAssignment_IsAssigned",
                table: "Clients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PsychologistId",
                table: "AvailableTimeSlotsOfPsychologists",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calendar_IsInitialized",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "ClientAssignment_IsAssigned",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "Calendar_IsInitialized",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PsychologistAssignment_IsAssigned",
                table: "Clients");

            migrationBuilder.AlterColumn<long>(
                name: "PsychologistId",
                table: "AvailableTimeSlotsOfPsychologists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
