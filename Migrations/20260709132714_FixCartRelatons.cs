using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShopMVC.Migrations
{
	/// <inheritdoc />
	public partial class FixCartRelatons : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_CartItems_AppUsers_AppUserId",
				table: "CartItems");

			migrationBuilder.DropForeignKey(
				name: "FK_CartItems_Products_ProductId",
				table: "CartItems");

			migrationBuilder.RenameColumn(
				name: "AppUserId",
				table: "CartItems",
				newName: "CartId");

			migrationBuilder.RenameIndex(
				name: "IX_CartItems_AppUserId",
				table: "CartItems",
				newName: "IX_CartItems_CartId");

			migrationBuilder.CreateTable(
				name: "Carts",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					AppUserId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Carts", x => x.Id);
					table.ForeignKey(
						name: "FK_Carts_AppUsers_AppUserId",
						column: x => x.AppUserId,
						principalTable: "AppUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Carts_AppUserId",
				table: "Carts",
				column: "AppUserId",
				unique: true);

			migrationBuilder.AddForeignKey(
				name: "FK_CartItems_Carts_CartId",
				table: "CartItems",
				column: "CartId",
				principalTable: "Carts",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_CartItems_Products_ProductId",
				table: "CartItems",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_CartItems_Carts_CartId",
				table: "CartItems");

			migrationBuilder.DropForeignKey(
				name: "FK_CartItems_Products_ProductId",
				table: "CartItems");

			migrationBuilder.DropTable(
				name: "Carts");

			migrationBuilder.RenameColumn(
				name: "CartId",
				table: "CartItems",
				newName: "AppUserId");

			migrationBuilder.RenameIndex(
				name: "IX_CartItems_CartId",
				table: "CartItems",
				newName: "IX_CartItems_AppUserId");

			migrationBuilder.AddForeignKey(
				name: "FK_CartItems_AppUsers_AppUserId",
				table: "CartItems",
				column: "AppUserId",
				principalTable: "AppUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_CartItems_Products_ProductId",
				table: "CartItems",
				column: "ProductId",
				principalTable: "Products",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
