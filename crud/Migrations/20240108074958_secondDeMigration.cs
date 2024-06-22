using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud.Migrations
{
    /// <inheritdoc />
    public partial class secondDeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.CreateTable(
                name: "Poste",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamePoste = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poste", x => x.Id);
                });

            // Then add the PosteId column to the Employees table
            migrationBuilder.AddColumn<Guid>(
                name: "PosteId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true); // Allow null values for flexibility

            // Create the index on PosteId
            migrationBuilder.CreateIndex(
                name: "IX_Employees_PosteId",
                table: "Employees",
                column: "PosteId");

            // Finally, add the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Poste_PosteId",
                table: "Employees",
                column: "PosteId",
                principalTable: "Poste",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // Consider Restrict instead of NoAction
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Poste_PosteId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Poste");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PosteId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PosteId",
                table: "Employees");
        }
    }
}
