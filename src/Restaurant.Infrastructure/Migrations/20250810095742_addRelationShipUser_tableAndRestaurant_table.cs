using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRelationShipUser_tableAndRestaurant_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ownerId",
                table: "restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

               migrationBuilder.Sql(
                "UPDATE Restaurants " +
                "SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers)"
                );

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_ownerId",
                table: "restaurants",
                column: "ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_restaurants_AspNetUsers_ownerId",
                table: "restaurants",
                column: "ownerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_restaurants_AspNetUsers_ownerId",
                table: "restaurants");

            migrationBuilder.DropIndex(
                name: "IX_restaurants_ownerId",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "ownerId",
                table: "restaurants");
        }
    }
}
