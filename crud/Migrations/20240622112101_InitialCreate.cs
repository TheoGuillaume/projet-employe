using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paye",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    jourPayement = table.Column<int>(type: "int", nullable: false),
                    payeParJour = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paye", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paye_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Paye_Paye_PayeId",
                        column: x => x.PayeId,
                        principalTable: "Paye",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Paye_EmployeeId",
                table: "Paye",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Paye_PayeId",
                table: "Paye",
                column: "PayeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paye");
        }
    }
}
