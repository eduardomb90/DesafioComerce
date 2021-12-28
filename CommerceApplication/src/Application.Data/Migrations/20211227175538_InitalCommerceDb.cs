using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Data.Migrations
{
    public partial class InitalCommerceDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(256)", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(256)", nullable: true),
                    FantasyName = table.Column<string>(type: "varchar(256)", nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(256)", nullable: true),
                    OpenDate = table.Column<DateTime>(nullable: true),
                    SupplierPhysical_FantasyName = table.Column<string>(type: "varchar(256)", nullable: true),
                    FullName = table.Column<string>(type: "varchar(256)", nullable: true),
                    Cpf = table.Column<string>(type: "varchar(256)", nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(9)", maxLength: 8, nullable: false),
                    Street = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Number = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Complement = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    City = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    State = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(256)", maxLength: 150, nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    Ddd = table.Column<string>(type: "varchar(256)", nullable: false),
                    Number = table.Column<string>(type: "varchar(256)", nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    BarCode = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    QuantityStock = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    PriceSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricePurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(256)", nullable: true),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SupplierId",
                table: "Addresses",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_SupplierId",
                table: "Emails",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_SupplierId",
                table: "Phones",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
