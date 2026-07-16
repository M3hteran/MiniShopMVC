using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShopMVC.Migrations
{
	/// <inheritdoc />
	public partial class ConfigureCartOneToOne : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Carts_AppUsers_AppUserId",
				table: "Carts");

			migrationBuilder.AddForeignKey(
				name: "FK_Carts_AppUsers_AppUserId",
				table: "Carts",
				column: "AppUserId",
				principalTable: "AppUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Carts_AppUsers_AppUserId",
				table: "Carts");

			migrationBuilder.AddForeignKey(
				name: "FK_Carts_AppUsers_AppUserId",
				table: "Carts",
				column: "AppUserId",
				principalTable: "AppUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
