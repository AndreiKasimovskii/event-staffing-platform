using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventStaffingPlatform.Host.Migrations.VacanciesDb
{
    /// <inheritdoc />
    public partial class Add_Requirements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requirements",
                table: "vacancies");

            migrationBuilder.CreateTable(
                name: "requirement_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    caption = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    value_type = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    value_configuration = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirement_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "requirements",
                columns: table => new
                {
                    requirement_type_id = table.Column<int>(type: "integer", nullable: false),
                    vacancy_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirements", x => new { x.requirement_type_id, x.vacancy_id });
                    table.ForeignKey(
                        name: "FK_requirements_requirement_types_requirement_type_id",
                        column: x => x.requirement_type_id,
                        principalTable: "requirement_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_requirements_vacancies_vacancy_id",
                        column: x => x.vacancy_id,
                        principalTable: "vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_requirement_types_caption",
                table: "requirement_types",
                column: "caption",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requirement_types_name",
                table: "requirement_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requirements_requirement_type_id",
                table: "requirements",
                column: "requirement_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_requirements_vacancy_id",
                table: "requirements",
                column: "vacancy_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requirements");

            migrationBuilder.DropTable(
                name: "requirement_types");

            migrationBuilder.AddColumn<string>(
                name: "requirements",
                table: "vacancies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
