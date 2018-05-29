using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourManagement.Api.Migrations
{
    public partial class ExtendModelandController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateOn",
                table: "Tours",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "UpdateBy",
                table: "Tours",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<Guid>(
                name: "BandId",
                table: "Tours",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Tours",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    BandId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.BandId);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ManagerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ShowId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Venue = table.Column<string>(maxLength: 150, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    TourId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ShowId);
                    table.ForeignKey(
                        name: "FK_Shows_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "TourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_BandId",
                table: "Tours",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ManagerId",
                table: "Tours",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_TourId",
                table: "Shows",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Bands_BandId",
                table: "Tours",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Managers_ManagerId",
                table: "Tours",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Bands_BandId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Managers_ManagerId",
                table: "Tours");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Tours_BandId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_ManagerId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Tours",
                newName: "UpdateOn");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Tours",
                newName: "UpdateBy");
        }
    }
}
