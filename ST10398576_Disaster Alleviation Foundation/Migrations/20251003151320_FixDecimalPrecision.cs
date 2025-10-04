using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10398576_Disaster_Alleviation_Foundation.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispatches_DisasterIncidents_DisasterIncidentID",
                table: "Dispatches");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Dispatches");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Projects",
                newName: "ProjectStatus");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Projects",
                newName: "ProjectLocation");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "ProjectDescription");

            migrationBuilder.AlterColumn<int>(
                name: "ResourceDonationID",
                table: "Dispatches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispatchDate",
                table: "Dispatches",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DisasterIncidentID",
                table: "Dispatches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispatches_DisasterIncidents_DisasterIncidentID",
                table: "Dispatches",
                column: "DisasterIncidentID",
                principalTable: "DisasterIncidents",
                principalColumn: "DisasterIncidentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispatches_DisasterIncidents_DisasterIncidentID",
                table: "Dispatches");

            migrationBuilder.RenameColumn(
                name: "ProjectStatus",
                table: "Projects",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "ProjectLocation",
                table: "Projects",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "ProjectDescription",
                table: "Projects",
                newName: "Description");

            migrationBuilder.AlterColumn<int>(
                name: "ResourceDonationID",
                table: "Dispatches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispatchDate",
                table: "Dispatches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisasterIncidentID",
                table: "Dispatches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Dispatches",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispatches_DisasterIncidents_DisasterIncidentID",
                table: "Dispatches",
                column: "DisasterIncidentID",
                principalTable: "DisasterIncidents",
                principalColumn: "DisasterIncidentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
