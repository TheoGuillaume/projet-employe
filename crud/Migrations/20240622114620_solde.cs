using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud.Migrations
{
    /// <inheritdoc />
    public partial class solde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SoldeId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Solde",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    solde = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solde", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SoldeId",
                table: "Employees",
                column: "SoldeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Solde_SoldeId",
                table: "Employees",
                column: "SoldeId",
                principalTable: "Solde",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Solde_SoldeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Solde");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SoldeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SoldeId",
                table: "Employees");
        }
    }
}
