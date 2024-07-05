using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "text", nullable: false),
                    categoryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_Categories_CategoryId",
                        column: x => x.categoryid,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                                 "delete from products");
        }
    }
}
