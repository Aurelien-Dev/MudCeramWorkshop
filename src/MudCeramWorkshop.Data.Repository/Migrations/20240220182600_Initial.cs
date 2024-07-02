using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MudCeramWorkshop.Data.Repository.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Firings",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Duration = table.Column<double>(type: "float", nullable: false),
                TotalKwH = table.Column<double>(type: "float", nullable: false),
                CostKwH = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Firings", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Materials",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsHomeMade = table.Column<bool>(type: "bit", nullable: true),
                Cost = table.Column<double>(type: "float", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Quantity = table.Column<double>(type: "float", nullable: false),
                UniteMesure = table.Column<int>(type: "int", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Materials", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Workshops",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Culture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Workshops", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdWorkshop = table.Column<int>(type: "int", nullable: false),
                Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Height = table.Column<double>(type: "float", nullable: true),
                TopDiameter = table.Column<double>(type: "float", nullable: true),
                BottomDiameter = table.Column<double>(type: "float", nullable: true),
                ShrinkingCoeficient = table.Column<double>(type: "float", nullable: true),
                DesignInstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                GlazingInstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Price = table.Column<double>(type: "float", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    name: "FK_Products_Workshops_IdWorkshop",
                    column: x => x.IdWorkshop,
                    principalTable: "Workshops",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WorkshopParameters",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                WorkshopId = table.Column<int>(type: "int", nullable: false),
                Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkshopParameters", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkshopParameters_Workshops_WorkshopId",
                    column: x => x.WorkshopId,
                    principalTable: "Workshops",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ImageInstruction",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdProduct = table.Column<int>(type: "int", nullable: false),
                UrlMedium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FileLocation = table.Column<int>(type: "int", nullable: false),
                IsFavoriteImage = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ImageInstruction", x => x.Id);
                table.ForeignKey(
                    name: "FK_ImageInstruction_Products_IdProduct",
                    column: x => x.IdProduct,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProductFirings",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdFiring = table.Column<int>(type: "int", nullable: false),
                IdProduct = table.Column<int>(type: "int", nullable: false),
                TotalKwH = table.Column<double>(type: "float", nullable: false),
                CostKwH = table.Column<double>(type: "float", nullable: false),
                NumberProducts = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductFirings", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductFirings_Firings_IdFiring",
                    column: x => x.IdFiring,
                    principalTable: "Firings",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProductFirings_Products_IdProduct",
                    column: x => x.IdProduct,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ProductMaterials",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdMaterial = table.Column<int>(type: "int", nullable: false),
                IdProduct = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<double>(type: "float", nullable: false),
                Cost = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductMaterials", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProductMaterials_Materials_IdMaterial",
                    column: x => x.IdMaterial,
                    principalTable: "Materials",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ProductMaterials_Products_IdProduct",
                    column: x => x.IdProduct,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ImageInstruction_IdProduct",
            table: "ImageInstruction",
            column: "IdProduct");

        migrationBuilder.CreateIndex(
            name: "IX_ProductFirings_IdFiring",
            table: "ProductFirings",
            column: "IdFiring");

        migrationBuilder.CreateIndex(
            name: "IX_ProductFirings_IdProduct",
            table: "ProductFirings",
            column: "IdProduct");

        migrationBuilder.CreateIndex(
            name: "IX_ProductMaterials_IdMaterial",
            table: "ProductMaterials",
            column: "IdMaterial");

        migrationBuilder.CreateIndex(
            name: "IX_ProductMaterials_IdProduct",
            table: "ProductMaterials",
            column: "IdProduct");

        migrationBuilder.CreateIndex(
            name: "IX_Products_IdWorkshop",
            table: "Products",
            column: "IdWorkshop");

        migrationBuilder.CreateIndex(
            name: "IX_WorkshopParameters_WorkshopId",
            table: "WorkshopParameters",
            column: "WorkshopId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ImageInstruction");

        migrationBuilder.DropTable(
            name: "ProductFirings");

        migrationBuilder.DropTable(
            name: "ProductMaterials");

        migrationBuilder.DropTable(
            name: "WorkshopParameters");

        migrationBuilder.DropTable(
            name: "Firings");

        migrationBuilder.DropTable(
            name: "Materials");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "Workshops");
    }
}
