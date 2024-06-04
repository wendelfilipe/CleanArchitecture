using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                                "INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" +
                                "VALUES('Caderno espiral', 'Caderno espriral 100 folhas', 7.45, 50, 'caderno1.jpg', 1)");
            migrationBuilder.Sql(
                                "INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId)" +
                                "VALUES('Estojo', 'Caderno espriral 80 folhas', 7.40, 40, 'estojo1.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                                 "delete from Products");
        }
    }
}
