using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud_MVC_EF.Migrations
{
    /// <inheritdoc />
    public partial class thirdmg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "students",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "WorkStatus",
                table: "students",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "students");

            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "students");
        }
    }
}
