using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjetoApollo.Migrations
{
    public partial class Apollo_Add_Client_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Institution_InstitutionId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.AlterColumn<long>(
                name: "InstitutionId",
                schema: "Apollo",
                table: "Address",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                schema: "Apollo",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Uin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questinary",
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
                    table.PrimaryKey("PK_Questinary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    InstitutionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speciality_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "Apollo",
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestinaryAnswers",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    QuestinaryId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<long>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Doctors",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    CRM = table.Column<string>(nullable: true),
                    SpecialityId = table.Column<int>(nullable: false),
                    InstitutionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalSchema: "Apollo",
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Speciality_SpecialityId",
                        column: x => x.SpecialityId,
                        principalSchema: "Apollo",
                        principalTable: "Speciality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historic",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ClientId = table.Column<long>(nullable: true),
                    DoctorId = table.Column<long>(nullable: true),
                    DoctorsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historic_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Apollo",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Historic_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalSchema: "Apollo",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConsultation",
                schema: "Apollo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Priority = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<long>(nullable: true),
                    DoctorId = table.Column<long>(nullable: true),
                    DoctorsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConsultation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalConsultation_Client_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "Apollo",
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalConsultation_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalSchema: "Apollo",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientId",
                schema: "Apollo",
                table: "Address",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_InstitutionId",
                schema: "Apollo",
                table: "Doctors",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialityId",
                schema: "Apollo",
                table: "Doctors",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Historic_ClientId",
                schema: "Apollo",
                table: "Historic",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Historic_DoctorsId",
                schema: "Apollo",
                table: "Historic",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_ClientId",
                schema: "Apollo",
                table: "MedicalConsultation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalConsultation_DoctorsId",
                schema: "Apollo",
                table: "MedicalConsultation",
                column: "DoctorsId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Speciality_InstitutionId",
                schema: "Apollo",
                table: "Speciality",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Client_ClientId",
                schema: "Apollo",
                table: "Address",
                column: "ClientId",
                principalSchema: "Apollo",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Institution_InstitutionId",
                schema: "Apollo",
                table: "Address",
                column: "InstitutionId",
                principalSchema: "Apollo",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Client_ClientId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Institution_InstitutionId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.DropTable(
                name: "Historic",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "MedicalConsultation",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "QuestinaryAnswers",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Doctors",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Questinary",
                schema: "Apollo");

            migrationBuilder.DropTable(
                name: "Speciality",
                schema: "Apollo");

            migrationBuilder.DropIndex(
                name: "IX_Address_ClientId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "Apollo",
                table: "Address");

            migrationBuilder.AlterColumn<long>(
                name: "InstitutionId",
                schema: "Apollo",
                table: "Address",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Institution_InstitutionId",
                schema: "Apollo",
                table: "Address",
                column: "InstitutionId",
                principalSchema: "Apollo",
                principalTable: "Institution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
