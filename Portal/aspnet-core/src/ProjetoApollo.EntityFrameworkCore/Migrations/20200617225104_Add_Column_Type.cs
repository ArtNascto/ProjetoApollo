using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApollo.Migrations
{
    public partial class Add_Column_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Apollo",
                table: "Questionary",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Apollo",
                table: "Questionary");
        }
    }
}
