using Microsoft.EntityFrameworkCore.Migrations;

namespace DAW.Migrations
{
    public partial class fixedBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Phones",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id_userId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_UserId",
                table: "Phones",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Id_userId",
                table: "Order",
                column: "Id_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_Id_userId",
                table: "Order",
                column: "Id_userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Users_UserId",
                table: "Phones",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_Id_userId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Users_UserId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_UserId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Order_Id_userId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Id_userId",
                table: "Order");
        }
    }
}
