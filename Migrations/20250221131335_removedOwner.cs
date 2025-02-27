using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRescue.Migrations
{
    /// <inheritdoc />
    public partial class removedOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePets_AspNetUsers_OwnerId",
                table: "FavoritePets");

            migrationBuilder.DropIndex(
                name: "IX_FavoritePets_OwnerId",
                table: "FavoritePets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "FavoritePets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "FavoritePets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePets_OwnerId",
                table: "FavoritePets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePets_AspNetUsers_OwnerId",
                table: "FavoritePets",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
