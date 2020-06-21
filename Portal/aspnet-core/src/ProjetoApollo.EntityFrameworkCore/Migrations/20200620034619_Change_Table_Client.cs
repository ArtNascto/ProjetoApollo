using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoApollo.Migrations
{
    public partial class Change_Table_Client : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Address_ClientId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Uin",
                schema: "Apollo",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "MedicalInsurance",
                schema: "Apollo",
                table: "Client",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "Apollo",
                table: "Client",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                schema: "Apollo",
                table: "Client",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientId",
                schema: "Apollo",
                table: "Address",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AbpUsers_UserId",
                schema: "Apollo",
                table: "Client",
                column: "UserId",
                principalSchema: "Apollo",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AbpUsers_UserId",
                schema: "Apollo",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                schema: "Apollo",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Address_ClientId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MedicalInsurance",
                schema: "Apollo",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Apollo",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "Uin",
                schema: "Apollo",
                table: "Client",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientId",
                schema: "Apollo",
                table: "Address",
                column: "ClientId",
                unique: true);
        }
    }
}
