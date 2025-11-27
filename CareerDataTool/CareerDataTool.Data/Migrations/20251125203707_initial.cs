using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerDataTool.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    EnterpriseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.EnterpriseId);
                });

            migrationBuilder.CreateTable(
                name: "KeyWordCategories",
                columns: table => new
                {
                    KeyWordCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWordCategories", x => x.KeyWordCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "WordsToIgnore",
                columns: table => new
                {
                    WordToIgnoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WordLanguage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordsToIgnore", x => x.WordToIgnoreId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyWords",
                columns: table => new
                {
                    KeyWordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyWordCategoryId = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWords", x => x.KeyWordId);
                    table.ForeignKey(
                        name: "FK_KeyWords_KeyWordCategories_KeyWordCategoryId",
                        column: x => x.KeyWordCategoryId,
                        principalTable: "KeyWordCategories",
                        principalColumn: "KeyWordCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyWordVariations",
                columns: table => new
                {
                    KeyWordVariationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyWordId = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWordVariations", x => x.KeyWordVariationId);
                    table.ForeignKey(
                        name: "FK_KeyWordVariations_KeyWords_KeyWordId",
                        column: x => x.KeyWordId,
                        principalTable: "KeyWords",
                        principalColumn: "KeyWordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(type: "int", nullable: true),
                    StateID = table.Column<int>(type: "int", nullable: true),
                    CityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceID);
                    table.ForeignKey(
                        name: "FK_Places_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_Places_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                    table.ForeignKey(
                        name: "FK_Places_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "JobVacancies",
                columns: table => new
                {
                    JobVacancyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnterpriseId = table.Column<int>(type: "int", nullable: false),
                    IdFromSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFromSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobPlacePlaceID = table.Column<int>(type: "int", nullable: false),
                    WhenWasPublishedInSite = table.Column<DateOnly>(type: "date", nullable: false),
                    WhenWasRegister = table.Column<DateOnly>(type: "date", nullable: false),
                    JobLanguage = table.Column<int>(type: "int", nullable: false),
                    JobWorkMode = table.Column<int>(type: "int", nullable: true),
                    JobRole = table.Column<int>(type: "int", nullable: false),
                    JobRoleLevel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancies", x => x.JobVacancyId);
                    table.ForeignKey(
                        name: "FK_JobVacancies_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "EnterpriseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobVacancies_Places_JobPlacePlaceID",
                        column: x => x.JobPlacePlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobVacancyKeyWord",
                columns: table => new
                {
                    KeyWordsKeyWordId = table.Column<int>(type: "int", nullable: false),
                    jobVacanciesJobVacancyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobVacancyKeyWord", x => new { x.KeyWordsKeyWordId, x.jobVacanciesJobVacancyId });
                    table.ForeignKey(
                        name: "FK_JobVacancyKeyWord_JobVacancies_jobVacanciesJobVacancyId",
                        column: x => x.jobVacanciesJobVacancyId,
                        principalTable: "JobVacancies",
                        principalColumn: "JobVacancyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobVacancyKeyWord_KeyWords_KeyWordsKeyWordId",
                        column: x => x.KeyWordsKeyWordId,
                        principalTable: "KeyWords",
                        principalColumn: "KeyWordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancies_EnterpriseId",
                table: "JobVacancies",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancies_JobPlacePlaceID",
                table: "JobVacancies",
                column: "JobPlacePlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancyKeyWord_jobVacanciesJobVacancyId",
                table: "JobVacancyKeyWord",
                column: "jobVacanciesJobVacancyId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_KeyWordCategoryId",
                table: "KeyWords",
                column: "KeyWordCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWordVariations_KeyWordId",
                table: "KeyWordVariations",
                column: "KeyWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CityID",
                table: "Places",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_CountryID",
                table: "Places",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_StateID",
                table: "Places",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobVacancyKeyWord");

            migrationBuilder.DropTable(
                name: "KeyWordVariations");

            migrationBuilder.DropTable(
                name: "WordsToIgnore");

            migrationBuilder.DropTable(
                name: "JobVacancies");

            migrationBuilder.DropTable(
                name: "KeyWords");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "KeyWordCategories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
