using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GitRepositoryTracker.Migrations
{
    /// <inheritdoc />
    public partial class Initialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    RepositoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RepositoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StargazersCount = table.Column<int>(type: "int", nullable: false),
                    ForksCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PushedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.RepositoryId);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "RepositoryTopics",
                columns: table => new
                {
                    RepositoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepositoryTopics", x => new { x.RepositoryId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_RepositoryTopics_Repositories_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "RepositoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepositoryTopics_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Repositories",
                columns: new[] { "RepositoryId", "CreatedAt", "Description", "ForksCount", "PushedAt", "RepositoryName", "StargazersCount", "UpdatedAt", "Url", "language" },
                values: new object[,]
                {
                    { "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is an example repository", 2, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Example Repository 1", 10, new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://github.com/example/repository1", "C#" },
                    { "MGFyOlJlcG9zaXRvcnkyODQ1NzgyMt==", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is another example repository", 1, new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Example Repository 2", 5, new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://github.com/example/repository2", "python" }
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "TopicId", "TopicName" },
                values: new object[,]
                {
                    { -12, "learn-to-code" },
                    { -10, "hacktoberfest" },
                    { -9, "freecodecamp" },
                    { -8, "education" },
                    { -7, "curriculum" },
                    { -6, "community" },
                    { -5, "certification" },
                    { -4, "careers" },
                    { -3, "python" },
                    { -2, "javaScript" },
                    { -1, "C#" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_Url",
                table: "Repositories",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepositoryTopics_TopicId",
                table: "RepositoryTopics",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_TopicName",
                table: "Topics",
                column: "TopicName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepositoryTopics");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
