using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digital_Menu.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedColumnInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Orders");
        }
    }
}
