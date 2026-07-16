using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniShopMVC.Migrations
{
	/// <inheritdoc />
	public partial class AddCartTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<decimal>(
				name: "UnitPrice",
				table: "CartItems",
				type: "decimal(18,2)",
				nullable: false,
				defaultValue: 0m);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "UnitPrice",
				table: "CartItems");
		}
	}
}
