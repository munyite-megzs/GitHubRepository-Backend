using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GitRepositoryTracker.Migrations
{
    /// <inheritdoc />
    public partial class Initialdataset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RepositoryTopics",
                columns: new[] { "RepositoryId", "TopicId" },
                values: new object[,]
                {
                    { "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==", -2 },
                    { "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==", -1 },
                    { "MGFyOlJlcG9zaXRvcnkyODQ1NzgyMt==", -3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RepositoryTopics",
                keyColumns: new[] { "RepositoryId", "TopicId" },
                keyValues: new object[] { "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==", -2 });

            migrationBuilder.DeleteData(
                table: "RepositoryTopics",
                keyColumns: new[] { "RepositoryId", "TopicId" },
                keyValues: new object[] { "MGEwOlJlcG9zaXRvcnkyODQ1NzgyMz==", -1 });

            migrationBuilder.DeleteData(
                table: "RepositoryTopics",
                keyColumns: new[] { "RepositoryId", "TopicId" },
                keyValues: new object[] { "MGFyOlJlcG9zaXRvcnkyODQ1NzgyMt==", -3 });
        }
    }
}
