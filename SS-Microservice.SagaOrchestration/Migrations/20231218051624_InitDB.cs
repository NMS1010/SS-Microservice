using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SS_Microservice.SagaOrchestration.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderingStateInstance",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CurrentState = table.Column<string>(type: "longtext", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderCode = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    UserName = table.Column<string>(type: "longtext", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: true),
                    Image = table.Column<string>(type: "longtext", nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    Receiver = table.Column<string>(type: "longtext", nullable: true),
                    ReceiverEmail = table.Column<string>(type: "longtext", nullable: true),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderingStateInstance", x => x.CorrelationId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductInstance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    VariantId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderingStateInstanceCorrelationId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInstance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInstance_OrderingStateInstance_OrderingStateInstanceC~",
                        column: x => x.OrderingStateInstanceCorrelationId,
                        principalTable: "OrderingStateInstance",
                        principalColumn: "CorrelationId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInstance_OrderingStateInstanceCorrelationId",
                table: "ProductInstance",
                column: "OrderingStateInstanceCorrelationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInstance");

            migrationBuilder.DropTable(
                name: "OrderingStateInstance");
        }
    }
}
