using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crud.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Solde_SoldeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Paye");

            migrationBuilder.DropTable(
                name: "Solde");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SoldeId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SoldeId",
                table: "Employees");

            migrationBuilder.AddColumn<float>(
                name: "solde",
                table: "Employees",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "solde",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "SoldeId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Paye",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    dateOfPayment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    jourPayement = table.Column<int>(type: "int", nullable: false),
                    payeParJour = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Paye_EmployeeId",
                table: "Paye",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Paye_PayeId",
                table: "Paye",
                column: "PayeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Solde_SoldeId",
                table: "Employees",
                column: "SoldeId",
                principalTable: "Solde",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
