using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coffee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coffies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CoffeeType = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoffeePhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CoffeeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Patch = table.Column<string>(type: "text", nullable: false),
                    IsMatch = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoffeePhoto_Coffies_CoffeeEntityId",
                        column: x => x.CoffeeEntityId,
                        principalTable: "Coffies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoffeePhoto_CoffeeEntityId",
                table: "CoffeePhoto",
                column: "CoffeeEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeePhoto");

            migrationBuilder.DropTable(
                name: "Coffies");
        }
    }
}
