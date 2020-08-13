using Microsoft.EntityFrameworkCore.Migrations;

namespace SGCont.Migrations
{
    public partial class estadotrabajador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoTrabajador",
                table: "trabajadores",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "009da871-1a31-434c-bdee-279545f4f75f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoTrabajador",
                table: "trabajadores");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "efa77b46-3582-4c7b-b3e8-7dc60f7ee1d4");
        }
    }
}
