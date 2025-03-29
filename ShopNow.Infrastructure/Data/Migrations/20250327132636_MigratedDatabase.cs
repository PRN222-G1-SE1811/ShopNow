using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopNow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigratedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVariant_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariant_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ProductVariant_ProductId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "OrderItemAttributes");

            migrationBuilder.DropTable(
                name: "ProductAsset");

            migrationBuilder.DropTable(
                name: "ProductAssetAttribute");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductVariant");

            migrationBuilder.RenameTable(
                name: "ProductVariant",
                newName: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "ProductVariants",
                newName: "Quantity");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ProductVariants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ProductVariantId",
                table: "Assets",
                column: "ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ProductVariants_ProductVariantId",
                table: "Assets",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVariants_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ProductVariants_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ProductVariants_ProductVariantId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVariants_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ProductVariants_ProductId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ProductVariantId",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariants",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductVariants");

            migrationBuilder.RenameTable(
                name: "ProductVariants",
                newName: "ProductVariant");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProductVariant",
                newName: "Stock");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariant",
                newName: "IX_ProductVariant_ProductId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Assets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Assets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductVariant",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariant",
                table: "ProductVariant",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAsset",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAsset", x => new { x.ProductId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_ProductAsset_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAsset_ProductVariant_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemAttributes_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAssetAttribute",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAssetAttribute", x => new { x.ProductId, x.AssetId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_ProductAssetAttribute_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAssetAttribute_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAssetAttribute_ProductVariant_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAttributes_AttributeId",
                table: "OrderItemAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemAttributes_OrderItemId",
                table: "OrderItemAttributes",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAsset_AssetId",
                table: "ProductAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssetAttribute_AssetId",
                table: "ProductAssetAttribute",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssetAttribute_AttributeId",
                table: "ProductAssetAttribute",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVariant_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariant_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_Products_ProductId",
                table: "ProductVariant",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ProductVariant_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
