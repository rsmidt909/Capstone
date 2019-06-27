using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Filtersid",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSystems",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumberOfFilters = table.Column<int>(nullable: false),
                    SizeOfFilters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Filtersid",
                table: "Customers",
                column: "Filtersid");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Filters_Filtersid",
                table: "Customers",
                column: "Filtersid",
                principalTable: "Filters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Filters_Filtersid",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Filtersid",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Filtersid",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NumberOfSystems",
                table: "Customers");
        }
    }
}
