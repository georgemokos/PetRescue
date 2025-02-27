using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRescue.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdentifierToFavoritePets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdentifier",
                table: "FavoritePets",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdentifier",
                table: "FavoritePets");
        }
    }
}
