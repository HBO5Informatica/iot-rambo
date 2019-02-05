using Microsoft.EntityFrameworkCore.Migrations;

namespace Group14.Rambo.Api.Migrations
{
    public partial class ChangeOnCapabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Capabilities",
                table: "SensorDevice",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "Capabilities",
                table: "ActorDevice",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "ActorDevice",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Capabilities",
                value: (byte)7);

            migrationBuilder.UpdateData(
                table: "ActorDevice",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Capabilities",
                value: (byte)7);

            migrationBuilder.UpdateData(
                table: "ActorDevice",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Capabilities",
                value: (byte)7);

            migrationBuilder.UpdateData(
                table: "SensorDevice",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Capabilities",
                value: (byte)15);

            migrationBuilder.UpdateData(
                table: "SensorDevice",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Capabilities",
                value: (byte)15);

            migrationBuilder.UpdateData(
                table: "SensorDevice",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Capabilities",
                value: (byte)15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Capabilities",
                table: "SensorDevice",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "Capabilities",
                table: "ActorDevice",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.UpdateData(
                table: "ActorDevice",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Capabilities",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ActorDevice",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Capabilities",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ActorDevice",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Capabilities",
                value: 7);

            migrationBuilder.UpdateData(
                table: "SensorDevice",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Capabilities",
                value: 15);

            migrationBuilder.UpdateData(
                table: "SensorDevice",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Capabilities",
                value: 15);

            migrationBuilder.UpdateData(
                table: "SensorDevice",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Capabilities",
                value: 15);
        }
    }
}
