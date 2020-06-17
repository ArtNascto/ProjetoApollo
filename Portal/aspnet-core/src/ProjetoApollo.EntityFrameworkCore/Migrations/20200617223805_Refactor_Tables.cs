using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjetoApollo.Migrations
{
    public partial class Refactor_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestinaryAnswers",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Questinary",
                schema: "Apollo");

            migrationBuilder.CreateTable(
                name: "Questionary",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    NextQuestion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionaryAnswers",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    QuestionaryId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionaryAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionaryAnswers_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Apollo",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionaryAnswers_Questionary_QuestionaryId",
                        column: x => x.QuestionaryId,
                        principalSchema: "Apollo",
                        principalTable: "Questionary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaryAnswers_ClientId",
                schema: "Apollo",
                table: "QuestionaryAnswers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionaryAnswers_QuestionaryId",
                schema: "Apollo",
                table: "QuestionaryAnswers",
                column: "QuestionaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionaryAnswers",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Questionary",
                schema: "Apollo");

            migrationBuilder.CreateTable(
                name: "Questinary",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Label = table.Column<string>(type: "text", nullable: true),
                    NextQuestion = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questinary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestinaryAnswers",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    QuestinaryId = table.Column<int>(type: "integer", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestinaryAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestinaryAnswers_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Apollo",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestinaryAnswers_Questinary_QuestinaryId",
                        column: x => x.QuestinaryId,
                        principalSchema: "Apollo",
                        principalTable: "Questinary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestinaryAnswers_ClientId",
                schema: "Apollo",
                table: "QuestinaryAnswers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestinaryAnswers_QuestinaryId",
                schema: "Apollo",
                table: "QuestinaryAnswers",
                column: "QuestinaryId");
        }
    }
}
