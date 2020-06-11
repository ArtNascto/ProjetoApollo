using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjetoApollo.Migrations
{
    public partial class Apollo_InstitutionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institution",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CEP = table.Column<string>(nullable: true),
                    AddressLine = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    InstitutionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "Apollo",
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Billing",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountHolderName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    ExpiresDate = table.Column<string>(nullable: true),
                    InstitutionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billing_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "Apollo",
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    InstitutionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "Apollo",
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_InstitutionId",
                schema: "Apollo",
                table: "Address",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_InstitutionId",
                schema: "Apollo",
                table: "Billing",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_InstitutionId",
                schema: "Apollo",
                table: "Contact",
                column: "InstitutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Billing",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Institution",
                schema: "Apollo");
        }
    }
}
