using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lcw_Project.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lcw_Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerSurname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lcw_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lcw_Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lcw_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lcw_CustomerOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lcw_CustomerOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lcw_CustomerOrder_Lcw_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Lcw_Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lcw_CustomerOrderItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerOrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lcw_CustomerOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lcw_CustomerOrderItem_Lcw_CustomerOrder",
                        column: x => x.CustomerOrderId,
                        principalTable: "Lcw_CustomerOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lcw_CustomerOrderItem_Lcw_Product",
                        column: x => x.ProductId,
                        principalTable: "Lcw_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lcw_CustomerOrder_CustomerId",
                table: "Lcw_CustomerOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lcw_CustomerOrderItem_CustomerOrderId",
                table: "Lcw_CustomerOrderItem",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Lcw_CustomerOrderItem_ProductId",
                table: "Lcw_CustomerOrderItem",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lcw_CustomerOrderItem");

            migrationBuilder.DropTable(
                name: "Lcw_CustomerOrder");

            migrationBuilder.DropTable(
                name: "Lcw_Product");

            migrationBuilder.DropTable(
                name: "Lcw_Customer");
        }
    }
}
