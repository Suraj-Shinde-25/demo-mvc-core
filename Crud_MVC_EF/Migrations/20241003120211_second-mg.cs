using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crud_MVC_EF.Migrations
{
    /// <inheritdoc />
    public partial class secondmg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "students",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "students");
        }
    }
}
