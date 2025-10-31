using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVC_Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addDescriptionColumnToUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Identity",
                table: "Users");
        }
    }
}
