using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueWorkSchedulePerDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_AccountId",
                table: "WorkSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_AccountId_Date",
                table: "WorkSchedules",
                columns: new[] { "AccountId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_AccountId_Date",
                table: "WorkSchedules");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_AccountId",
                table: "WorkSchedules",
                column: "AccountId");
        }
    }
}
