using Microsoft.EntityFrameworkCore.Migrations;

namespace SGCont.Migrations
{
    public partial class Montos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monto_Contratos_ContratoId",
                table: "Monto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Monto",
                table: "Monto");

            migrationBuilder.DropColumn(
                name: "AperturaSocioId",
                table: "trabajadores");

            migrationBuilder.DropColumn(
                name: "PerfilOcupacionalId",
                table: "trabajadores");

            migrationBuilder.DropColumn(
                name: "PuestoDeTrabajoId",
                table: "trabajadores");

            migrationBuilder.RenameTable(
                name: "Monto",
                newName: "Montos");

            migrationBuilder.RenameIndex(
                name: "IX_Monto_ContratoId",
                table: "Montos",
                newName: "IX_Montos_ContratoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Montos",
                table: "Montos",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "efa77b46-3582-4c7b-b3e8-7dc60f7ee1d4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f42559a2-2776-4e9b-9ba1-268597eff72b",
                columns: new[] { "Email", "NormalizedEmail" },
                values: new object[] { "admin@SGCont.cu", "ADMIN@SGCONT.CU" });

            migrationBuilder.AddForeignKey(
                name: "FK_Montos_Contratos_ContratoId",
                table: "Montos",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Montos_Contratos_ContratoId",
                table: "Montos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Montos",
                table: "Montos");

            migrationBuilder.RenameTable(
                name: "Montos",
                newName: "Monto");

            migrationBuilder.RenameIndex(
                name: "IX_Montos_ContratoId",
                table: "Monto",
                newName: "IX_Monto_ContratoId");

            migrationBuilder.AddColumn<int>(
                name: "AperturaSocioId",
                table: "trabajadores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerfilOcupacionalId",
                table: "trabajadores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuestoDeTrabajoId",
                table: "trabajadores",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Monto",
                table: "Monto",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "abae482f-f28a-42ce-ada3-e72c9a9c14b1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f42559a2-2776-4e9b-9ba1-268597eff72b",
                columns: new[] { "Email", "NormalizedEmail" },
                values: new object[] { "admin@opplat.cu", "ADMIN@OPPLAT.CU" });

            migrationBuilder.AddForeignKey(
                name: "FK_Monto_Contratos_ContratoId",
                table: "Monto",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
