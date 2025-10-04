using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10398576_Disaster_Alleviation_Foundation.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAzureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dispatches");

            migrationBuilder.DropTable(
                name: "ProjectResources");

            migrationBuilder.DropTable(
                name: "ProjectVolunteers");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Volunteers");

            migrationBuilder.CreateTable(
                name: "Dispatches",
                columns: table => new
                {
                    DispatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisasterIncidentID = table.Column<int>(type: "int", nullable: true),
                    ProjectID = table.Column<int>(type: "int", nullable: true),
                    ResourceDonationID = table.Column<int>(type: "int", nullable: true),
                    DispatchDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuantityDispatched = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatches", x => x.DispatchID);
                    table.ForeignKey(
                        name: "FK_Dispatches_DisasterIncidents_DisasterIncidentID",
                        column: x => x.DisasterIncidentID,
                        principalTable: "DisasterIncidents",
                        principalColumn: "DisasterIncidentID");
                    table.ForeignKey(
                        name: "FK_Dispatches_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_Dispatches_ResourceDonations_ResourceDonationID",
                        column: x => x.ResourceDonationID,
                        principalTable: "ResourceDonations",
                        principalColumn: "ResourceDonationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectResources",
                columns: table => new
                {
                    ProjectResourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectResources", x => x.ProjectResourceID);
                    table.ForeignKey(
                        name: "FK_ProjectResources_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVolunteers",
                columns: table => new
                {
                    ProjectVolunteerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    VolunteerID = table.Column<int>(type: "int", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVolunteers", x => x.ProjectVolunteerID);
                    table.ForeignKey(
                        name: "FK_ProjectVolunteers_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectVolunteers_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "VolunteerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_DisasterIncidentID",
                table: "Dispatches",
                column: "DisasterIncidentID");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_ProjectID",
                table: "Dispatches",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Dispatches_ResourceDonationID",
                table: "Dispatches",
                column: "ResourceDonationID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectResources_ProjectID",
                table: "ProjectResources",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVolunteers_ProjectID",
                table: "ProjectVolunteers",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVolunteers_VolunteerID",
                table: "ProjectVolunteers",
                column: "VolunteerID");
        }
    }
}
