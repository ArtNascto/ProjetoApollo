using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApollo.Migrations
{
    public partial class Apollo_Rectify_InstitutionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contact_InstitutionId",
                schema: "Apollo",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Billing_InstitutionId",
                schema: "Apollo",
                table: "Billing");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_InstitutionId",
                schema: "Apollo",
                table: "Contact",
                column: "InstitutionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Billing_InstitutionId",
                schema: "Apollo",
                table: "Billing",
                column: "InstitutionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contact_InstitutionId",
                schema: "Apollo",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Billing_InstitutionId",
                schema: "Apollo",
                table: "Billing");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_InstitutionId",
                schema: "Apollo",
                table: "Contact",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Billing_InstitutionId",
                schema: "Apollo",
                table: "Billing",
                column: "InstitutionId");
        }
    }
}
