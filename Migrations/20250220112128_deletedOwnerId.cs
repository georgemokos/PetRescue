using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetRescue.Migrations
{
    /// <inheritdoc />
    public partial class deletedOwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePets_AspNetUsers_OwnerId",
                table: "FavoritePets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "FavoritePets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePets_AspNetUsers_OwnerId",
                table: "FavoritePets",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePets_AspNetUsers_OwnerId",
                table: "FavoritePets");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "FavoritePets",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePets_AspNetUsers_OwnerId",
                table: "FavoritePets",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
